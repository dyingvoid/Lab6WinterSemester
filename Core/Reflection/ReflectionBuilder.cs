using System.Reflection;
using System.Reflection.Emit;

namespace Core.Reflection;

public class ReflectionBuilder
{
    public ReflectionBuilder(string typeName, Dictionary<string, Type> typeDescription)
    {
        TypeName = typeName.Substring(0, typeName.IndexOf("."));
        TypeDescription = typeDescription;
        Builder = GetTypeBuilder();
        BuildedType = BuildType();
    }

    public string TypeName { get; }
    public Dictionary<string, Type> TypeDescription { get; }
    public Type BuildedType { get; private set; }
    public TypeBuilder Builder { get; private set; }

    public object CreateInstance(object[] properties)
    {
        var instance = Activator.CreateInstance(BuildedType);

        var counter = 0;
        foreach (var (propertyName, type) in TypeDescription)
        {
            BuildedType.GetProperty(propertyName)
                .SetValue(instance, Convert.ChangeType(properties[counter], type));
            counter++;
        }

        return instance;
    }
    
    private Type BuildType()
    {
        foreach (var (name, type) in TypeDescription)
        {
            CreateProperty(Builder, name, type);
        }
        var constructor = Builder.DefineDefaultConstructor(MethodAttributes.Public);
        
        return Builder.CreateType();
    }

    private TypeBuilder GetTypeBuilder()
    {
        var assemblyName = new AssemblyName(TypeName);
        var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        var moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
        return moduleBuilder.DefineType(TypeName, TypeAttributes.Public);
    }

    private void CreateProperty(TypeBuilder typeBuilder, string propertyName, Type propertyType)
    {
        var fieldBuilder = typeBuilder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

        var propertyBuilder = typeBuilder.DefineProperty(propertyName, 
            PropertyAttributes.HasDefault, 
            propertyType, 
            null);
        
        var getMethodBuilder = typeBuilder.DefineMethod(
            "get_" + propertyName, 
            MethodAttributes.Public |
            MethodAttributes.SpecialName |
            MethodAttributes.HideBySig, 
            propertyType, 
            Type.EmptyTypes);
        var getIl = getMethodBuilder.GetILGenerator();

        getIl.Emit(OpCodes.Ldarg_0);
        getIl.Emit(OpCodes.Ldfld, fieldBuilder);
        getIl.Emit(OpCodes.Ret);

        var setMethodBuilder = typeBuilder.DefineMethod(
            "set_" + propertyName,
            MethodAttributes.Public | 
            MethodAttributes.SpecialName | 
            MethodAttributes.HideBySig, 
            null, 
            new[] { propertyType });
        
        var setIl = setMethodBuilder.GetILGenerator();

        setIl.Emit(OpCodes.Ldarg_0);
        setIl.Emit(OpCodes.Ldarg_1);
        setIl.Emit(OpCodes.Stfld, fieldBuilder);
        setIl.Emit(OpCodes.Ret);

        propertyBuilder.SetGetMethod(getMethodBuilder);
        propertyBuilder.SetSetMethod(setMethodBuilder);
    }

    public static List<object> GetProperties(object element)
    {
        var elementType = element.GetType();
        var properties = elementType.GetProperties();
        var objectProperties = new List<object>();

        foreach (var property in properties)
        {
            objectProperties.Add(property.GetValue(element, null));
        }

        return objectProperties;
    }
}