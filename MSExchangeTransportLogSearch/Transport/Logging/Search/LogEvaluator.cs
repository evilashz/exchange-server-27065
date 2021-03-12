using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.LogSearch;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000006 RID: 6
	internal abstract class LogEvaluator
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000339E File Offset: 0x0000159E
		public List<IndexedSearch> Searches
		{
			get
			{
				return this.searches;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000033A8 File Offset: 0x000015A8
		public static LogEvaluator FromCondition(LogCondition condition, CsvTable table)
		{
			Dictionary<string, LogVariableEvaluator> variables = new Dictionary<string, LogVariableEvaluator>();
			LogEvaluator logEvaluator = LogEvaluator.FromCondition(condition, table, variables, 100);
			LogEvaluator.CheckOptimization(condition, logEvaluator.searches, table.IndexedFields, 0);
			return logEvaluator;
		}

		// Token: 0x06000029 RID: 41
		public abstract bool Evaluate(LogCursor row);

		// Token: 0x0600002A RID: 42 RVA: 0x000033DC File Offset: 0x000015DC
		private static bool CheckOptimization(LogCondition condition, List<IndexedSearch> searches, CsvField[] indexedFields, int optimizationCheckRecursionDepth)
		{
			if (optimizationCheckRecursionDepth > 2)
			{
				return false;
			}
			LogAndCondition logAndCondition = condition as LogAndCondition;
			if (logAndCondition != null)
			{
				foreach (LogCondition condition2 in logAndCondition.Conditions)
				{
					if (LogEvaluator.CheckOptimization(condition2, searches, indexedFields, optimizationCheckRecursionDepth + 1))
					{
						return true;
					}
				}
				return false;
			}
			return LogEvaluator.CheckFieldEqualsConstantCondition(condition, searches, indexedFields);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000345C File Offset: 0x0000165C
		private static bool CheckFieldEqualsConstantCondition(LogCondition condition, List<IndexedSearch> searches, CsvField[] indexedFields)
		{
			LogOrCondition logOrCondition = condition as LogOrCondition;
			IndexedSearch indexedSearch;
			if (logOrCondition != null)
			{
				List<IndexedSearch> list = new List<IndexedSearch>();
				foreach (LogCondition condition2 in logOrCondition.Conditions)
				{
					indexedSearch = LogEvaluator.CheckSimpleFieldEqualsConstantCondition(condition2, indexedFields);
					if (indexedSearch == null)
					{
						return false;
					}
					list.Add(indexedSearch);
				}
				searches.AddRange(list);
				return true;
			}
			indexedSearch = LogEvaluator.CheckSimpleFieldEqualsConstantCondition(condition, indexedFields);
			if (indexedSearch != null)
			{
				searches.Add(indexedSearch);
				return true;
			}
			return false;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000034F4 File Offset: 0x000016F4
		private static IndexedSearch CheckSimpleFieldEqualsConstantCondition(LogCondition condition, CsvField[] indexedFields)
		{
			LogStringComparisonCondition logStringComparisonCondition = condition as LogStringComparisonCondition;
			if (logStringComparisonCondition == null || logStringComparisonCondition.Operator != LogComparisonOperator.Equals)
			{
				return null;
			}
			LogConditionField logConditionField = logStringComparisonCondition.Left as LogConditionField;
			LogConditionConstant logConditionConstant = logStringComparisonCondition.Right as LogConditionConstant;
			if (logConditionField == null || logConditionConstant == null)
			{
				logConditionField = (logStringComparisonCondition.Right as LogConditionField);
				logConditionConstant = (logStringComparisonCondition.Left as LogConditionConstant);
			}
			if (logConditionField == null || logConditionConstant == null)
			{
				return null;
			}
			string text = logConditionConstant.Value as string;
			foreach (CsvField csvField in indexedFields)
			{
				if (logConditionField.Name.Equals(csvField.Name, StringComparison.OrdinalIgnoreCase) && text != null)
				{
					return new IndexedSearch(csvField.Name, csvField.NormalizeMethod(text));
				}
			}
			return null;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000035B8 File Offset: 0x000017B8
		private static LogEvaluator FromCondition(LogCondition condition, CsvTable table, Dictionary<string, LogVariableEvaluator> variables, int depthAllowed)
		{
			if (depthAllowed == 0)
			{
				throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_QUERY_TOO_COMPLEX);
			}
			if (condition == null)
			{
				throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_MISSING_QUERY_CONDITION);
			}
			if (condition is LogTrueCondition)
			{
				return LogEvaluator.True;
			}
			if (condition is LogFalseCondition)
			{
				return LogEvaluator.False;
			}
			LogCompoundCondition logCompoundCondition = condition as LogCompoundCondition;
			if (logCompoundCondition != null)
			{
				LogCompoundEvaluator logCompoundEvaluator;
				if (logCompoundCondition is LogAndCondition)
				{
					logCompoundEvaluator = new LogAndEvaluator();
				}
				else
				{
					if (!(logCompoundCondition is LogOrCondition))
					{
						throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_INVALID_QUERY_CONDITION);
					}
					logCompoundEvaluator = new LogOrEvaluator();
				}
				foreach (LogCondition condition2 in logCompoundCondition.Conditions)
				{
					logCompoundEvaluator.Conditions.Add(LogEvaluator.FromCondition(condition2, table, variables, depthAllowed - 1));
				}
				return logCompoundEvaluator;
			}
			LogUnaryCondition logUnaryCondition = condition as LogUnaryCondition;
			if (logUnaryCondition != null)
			{
				LogUnaryEvaluator logUnaryEvaluator = null;
				LogQuantifierCondition logQuantifierCondition = condition as LogQuantifierCondition;
				if (logQuantifierCondition != null)
				{
					LogQuantifierEvaluator logQuantifierEvaluator;
					if (logQuantifierCondition is LogForAnyCondition)
					{
						logQuantifierEvaluator = new LogForAnyEvaluator();
					}
					else
					{
						if (!(logQuantifierCondition is LogForEveryCondition))
						{
							throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_INVALID_QUERY_CONDITION);
						}
						logQuantifierEvaluator = new LogForEveryEvaluator();
					}
					if (logQuantifierCondition.Field.Name == null)
					{
						throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_UNRECOGNIZED_QUERY_FIELD);
					}
					logQuantifierEvaluator.FieldIndex = table.NameToIndex(logQuantifierCondition.Field.Name);
					if (logQuantifierEvaluator.FieldIndex == -1)
					{
						throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_UNRECOGNIZED_QUERY_FIELD);
					}
					Type type = table.Fields[logQuantifierEvaluator.FieldIndex].Type;
					if (!type.IsArray)
					{
						throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_INCOMPATIBLE_QUERY_OPERAND_TYPES);
					}
					if (logQuantifierCondition.Variable.Name == null)
					{
						throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_MISSING_BOUND_VARIABLE_NAME);
					}
					if (variables.ContainsKey(logQuantifierCondition.Variable.Name))
					{
						throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_DUPLICATE_BOUND_VARIABLE_DECLARATION);
					}
					logQuantifierEvaluator.Variable = new LogVariableEvaluator();
					logQuantifierEvaluator.Variable.BoundFieldIndex = logQuantifierEvaluator.FieldIndex;
					variables.Add(logQuantifierCondition.Variable.Name, logQuantifierEvaluator.Variable);
					logUnaryEvaluator = logQuantifierEvaluator;
				}
				else if (logUnaryCondition is LogNotCondition)
				{
					logUnaryEvaluator = new LogNotEvaluator();
				}
				logUnaryEvaluator.Condition = LogEvaluator.FromCondition(logUnaryCondition.Condition, table, variables, depthAllowed - 1);
				if (logQuantifierCondition != null)
				{
					variables.Remove(logQuantifierCondition.Variable.Name);
				}
				return logUnaryEvaluator;
			}
			LogBinaryOperatorCondition logBinaryOperatorCondition = condition as LogBinaryOperatorCondition;
			if (logBinaryOperatorCondition != null)
			{
				bool convertToUppercase = false;
				LogComparisonCondition logComparisonCondition = logBinaryOperatorCondition as LogComparisonCondition;
				LogBinaryOperatorEvaluator logBinaryOperatorEvaluator;
				if (logComparisonCondition != null)
				{
					LogStringComparisonCondition logStringComparisonCondition = logComparisonCondition as LogStringComparisonCondition;
					LogComparisonEvaluator logComparisonEvaluator;
					if (logStringComparisonCondition != null)
					{
						LogStringComparisonEvaluator logStringComparisonEvaluator = new LogStringComparisonEvaluator();
						convertToUppercase = logStringComparisonCondition.IgnoreCase;
						logStringComparisonEvaluator.IgnoreCase = logStringComparisonCondition.IgnoreCase;
						logComparisonEvaluator = logStringComparisonEvaluator;
					}
					else
					{
						logComparisonEvaluator = new LogComparisonEvaluator();
					}
					logComparisonEvaluator.Operator = logComparisonCondition.Operator;
					logBinaryOperatorEvaluator = logComparisonEvaluator;
				}
				else
				{
					if (!(logBinaryOperatorCondition is LogBinaryStringOperatorCondition))
					{
						throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_INVALID_QUERY_CONDITION);
					}
					LogBinaryStringOperatorEvaluator logBinaryStringOperatorEvaluator;
					if (logBinaryOperatorCondition is LogStringStartsWithCondition)
					{
						logBinaryStringOperatorEvaluator = new LogStringStartsWithEvaluator();
					}
					else if (logBinaryOperatorCondition is LogStringEndsWithCondition)
					{
						logBinaryStringOperatorEvaluator = new LogStringEndsWithEvaluator();
					}
					else
					{
						if (!(logBinaryOperatorCondition is LogStringContainsCondition))
						{
							throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_INVALID_QUERY_CONDITION);
						}
						logBinaryStringOperatorEvaluator = new LogStringContainsEvaluator();
					}
					convertToUppercase = ((LogBinaryStringOperatorCondition)logBinaryOperatorCondition).IgnoreCase;
					logBinaryStringOperatorEvaluator.IgnoreCase = ((LogBinaryStringOperatorCondition)logBinaryOperatorCondition).IgnoreCase;
					logBinaryOperatorEvaluator = logBinaryStringOperatorEvaluator;
				}
				logBinaryOperatorEvaluator.Left = LogEvaluator.FromConditionOperand(logBinaryOperatorCondition.Left, table, variables, convertToUppercase);
				logBinaryOperatorEvaluator.Right = LogEvaluator.FromConditionOperand(logBinaryOperatorCondition.Right, table, variables, convertToUppercase);
				Type valueType = logBinaryOperatorEvaluator.Left.GetValueType(table);
				Type valueType2 = logBinaryOperatorEvaluator.Right.GetValueType(table);
				if (valueType != valueType2)
				{
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_INCOMPATIBLE_QUERY_OPERAND_TYPES);
				}
				return logBinaryOperatorEvaluator;
			}
			else
			{
				LogUnaryOperatorCondition logUnaryOperatorCondition = condition as LogUnaryOperatorCondition;
				if (logUnaryOperatorCondition == null)
				{
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_INVALID_QUERY_CONDITION);
				}
				if (logUnaryOperatorCondition is LogIsNullOrEmptyCondition)
				{
					return new LogIsNullOrEmptyEvaluator
					{
						Operand = LogEvaluator.FromConditionOperand(logUnaryOperatorCondition.Operand, table, variables, false)
					};
				}
				throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_INVALID_QUERY_CONDITION);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003998 File Offset: 0x00001B98
		private static LogOperandEvaluator FromConditionOperand(LogConditionOperand operand, CsvTable table, Dictionary<string, LogVariableEvaluator> variables, bool convertToUppercase)
		{
			LogConditionConstant logConditionConstant = operand as LogConditionConstant;
			if (logConditionConstant != null)
			{
				LogConstantEvaluator logConstantEvaluator = new LogConstantEvaluator();
				logConstantEvaluator.Value = logConditionConstant.Value;
				if (convertToUppercase)
				{
					string text = logConstantEvaluator.Value as string;
					if (text != null)
					{
						logConstantEvaluator.Value = text.ToUpperInvariant();
						logConstantEvaluator.AssumeUppercase = true;
					}
				}
				return logConstantEvaluator;
			}
			LogConditionVariable logConditionVariable = operand as LogConditionVariable;
			if (logConditionVariable != null)
			{
				if (logConditionVariable.Name == null)
				{
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_MISSING_BOUND_VARIABLE_NAME);
				}
				LogVariableEvaluator result;
				if (!variables.TryGetValue(logConditionVariable.Name, out result))
				{
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_UNBOUND_QUERY_VARIABLE);
				}
				return result;
			}
			else
			{
				LogConditionField logConditionField = operand as LogConditionField;
				if (logConditionField == null)
				{
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_INVALID_OPERAND_CLASS);
				}
				if (logConditionField.Name == null)
				{
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_UNRECOGNIZED_QUERY_FIELD);
				}
				LogFieldEvaluator logFieldEvaluator = new LogFieldEvaluator();
				logFieldEvaluator.Index = table.NameToIndex(logConditionField.Name);
				if (logFieldEvaluator.Index == -1)
				{
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_UNRECOGNIZED_QUERY_FIELD);
				}
				return logFieldEvaluator;
			}
		}

		// Token: 0x04000022 RID: 34
		public const int MaxQueryDepth = 100;

		// Token: 0x04000023 RID: 35
		private const int OptimizationCheckMaxRecursion = 2;

		// Token: 0x04000024 RID: 36
		private List<IndexedSearch> searches = new List<IndexedSearch>();

		// Token: 0x04000025 RID: 37
		public static readonly LogEvaluator True = new LogTrueEvaluator();

		// Token: 0x04000026 RID: 38
		public static readonly LogEvaluator False = new LogFalseEvaluator();
	}
}
