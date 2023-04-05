﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Loggers;
using Core.Managers;
using Core.TableClasses;

namespace Lab5WinterSemester.Core.Testers;

public class DataBaseTester : ITester<IDataBase>
{
    private ITable _table;

    public DataBaseTester(ILogger logger)
    {
        Logger = logger;
    }

    public bool Test(IDataBase dataBase)
    {
        bool answer = true;
        
        foreach (var table in dataBase.Tables)
        {
            answer = answer && Test(table);
        }

        return answer;
    }
    
    public ILogger Logger { get; set; }
    public string testFailures { get; private set; }
    
    private bool Test(ITable table)
    {
        _table = table;
        testFailures += table.Name + "\n";
        
        var answer= CheckStructureEquality() &&
                    CheckTableDimensionsEquality() &&
                    CheckColumnsDataTypeEquality();
        
        if(!answer)
            Logger.Log(testFailures);

        return answer;
    }

    private bool CheckStructureEquality()
    {

        var extraColumns = _table.Elements.Keys.Except(_table.Types.Keys).ToArray();
        var missingColumns = _table.Types.Keys.Except(_table.Elements.Keys).ToArray();

        testFailures += "Extra columns not found in config: " + String.Join(", ", extraColumns) +
                        "\nMissing columns: " + String.Join(", ", missingColumns) + "\n";


        return extraColumns.Length == 0 && missingColumns.Length == 0;
    }
    
    private bool CheckTableDimensionsEquality()
    {
        var wrongSizedColumns = new List<string>();
        
        foreach (var (key, column) in _table.Elements)
        {
            if (column.Count != _table.Shape.Item2)
            {
                wrongSizedColumns.Add(key);
            }
        }

        testFailures += "Columns of wrong size(size is set according to first column): " +
                        String.Join(", ", wrongSizedColumns) + "\n";

        return wrongSizedColumns.Count == 0;
    }
    
    private bool CheckColumnsDataTypeEquality()
    {
        var columnsWithWrongTypeElements = new List<string>();
        var wrongElements = new List<object?>();
        
        foreach (var (columnName, column) in _table.Elements)
        {
            var state = _table.Types.TryGetValue(columnName, out var columnType);

            if (!CheckColumnDataTypeEquality(column, columnType, out var wrongElementsColumn))
            {
                columnsWithWrongTypeElements.Add(columnName);
                wrongElements.AddRange(wrongElementsColumn);
            }
        }

        testFailures += "Columns with elements of wrong type: " +
                        String.Join(", ", columnsWithWrongTypeElements) + "\n" +
                        "Wrong elements: " + String.Join(", ", wrongElements) + "\n";

        return columnsWithWrongTypeElements.Count == 0;
    }
    
    private bool CheckColumnDataTypeEquality(List<object?> column, Type type, out List<object?> wrongElements)
    {
        wrongElements = new List<object?>();
        
        if (type == typeof(string))
            return true;
        
        bool answer = true;

        MethodInfo? castGenericMethod = null;
        try
        {
            castGenericMethod = ReflectionManager.TryMakeGenericWithType(type);
        }
        catch (Exception)
        {
            testFailures += "Unknown type was met: " + type  + "\n";
            answer = false;
        }
        
        foreach (var element in column)
        {
            try
            {
                ReflectionManager.TryCastToType(type, castGenericMethod, element);
            }
            catch (Exception)
            {
                wrongElements.Add(element);
                answer = false;
            }
        }

        return answer;
    }
}