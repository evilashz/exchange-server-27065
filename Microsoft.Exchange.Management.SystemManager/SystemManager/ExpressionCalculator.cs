using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200009E RID: 158
	public sealed class ExpressionCalculator
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x000137D0 File Offset: 0x000119D0
		public int Count
		{
			get
			{
				return this.query.Count;
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000137DD File Offset: 0x000119DD
		public IList<KeyValuePair<string, object>> CalculateAll(DataRow dataRow, DataRow inputRow)
		{
			return this.CalculateCore(dataRow, inputRow, this.query);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000137ED File Offset: 0x000119ED
		public IList<KeyValuePair<string, object>> CalculateSpecifiedColumn(string column, DataRow dataRow)
		{
			return this.CalculateSpecifiedColumn(column, dataRow, null);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00013814 File Offset: 0x00011A14
		public IList<KeyValuePair<string, object>> CalculateAffectedColumns(string changedColumn, DataRow dataRow, DataRow inputRow)
		{
			IEnumerable<ColumnExpression> source = from c in this.query
			where c.DependentColumns.Contains(changedColumn)
			select c;
			return this.CalculateCore(dataRow, inputRow, source.ToArray<ColumnExpression>());
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00013870 File Offset: 0x00011A70
		public IList<KeyValuePair<string, object>> CalculateSpecifiedColumn(string column, DataRow dataRow, DataRow inputRow)
		{
			IEnumerable<ColumnExpression> source = from c in this.query
			where c.ResultColumn == column
			select c;
			return this.CalculateCore(dataRow, inputRow, source.ToArray<ColumnExpression>());
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000138B0 File Offset: 0x00011AB0
		public static ExpressionCalculator Parse(DataTable table)
		{
			return ExpressionCalculator.Parse(table, "LambdaExpression");
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000138D8 File Offset: 0x00011AD8
		public static ExpressionCalculator Parse(DataTable table, string lambdaExpressionKey)
		{
			IEnumerable<DataColumn> enumerable = from DataColumn o in table.Columns
			where o.ExtendedProperties.Contains(lambdaExpressionKey)
			select o;
			ExpressionCalculator expressionCalculator = new ExpressionCalculator();
			foreach (DataColumn dataColumn in enumerable)
			{
				string lambdaExpression = dataColumn.ExtendedProperties[lambdaExpressionKey].ToString();
				ColumnExpression columnExpression = ExpressionCalculator.BuildColumnExpression(lambdaExpression);
				columnExpression.ResultColumn = dataColumn.ColumnName;
				expressionCalculator.query.Add(columnExpression);
			}
			return expressionCalculator;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001398C File Offset: 0x00011B8C
		public static ColumnExpression BuildColumnExpression(string lambdaExpression)
		{
			ColumnExpression columnExpression = new ColumnExpression();
			string[] array = lambdaExpression.Split(new string[]
			{
				"=>"
			}, StringSplitOptions.None);
			foreach (string text in array[0].Split(new char[]
			{
				','
			}, StringSplitOptions.None))
			{
				columnExpression.DependentColumns.Add(text.Trim());
			}
			columnExpression.Expression = array[1];
			return columnExpression;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00013A06 File Offset: 0x00011C06
		public static object CalculateLambdaExpression(ColumnExpression expression, Type dataType, DataRow dataRow, DataRow inputRow)
		{
			return ExpressionCalculator.CalculateLambdaExpression(expression, dataType, null, dataRow, inputRow);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00013A14 File Offset: 0x00011C14
		public static object CalculateLambdaExpression(ColumnExpression expression, Type dataType, Type[] servicePredefinedTypes, DataRow dataRow, DataRow inputRow)
		{
			if (!ExpressionCalculator.IsCacheValid(expression, dataRow, inputRow))
			{
				expression.CachedDelegate = new CachedDelegate
				{
					TemplateDataRow = ((dataRow != null) ? dataRow.Table.Clone().NewRow() : null),
					TemplateInputRow = ((inputRow != null) ? inputRow.Table.Clone().NewRow() : null)
				};
				expression.CachedDelegate.CompiledDelegate = ExpressionCalculator.CompileLambdaExpression(expression, dataType, servicePredefinedTypes, expression.CachedDelegate.TemplateDataRow, expression.CachedDelegate.TemplateInputRow);
			}
			if (expression.CachedDelegate.TemplateDataRow != null)
			{
				expression.CachedDelegate.TemplateDataRow.ItemArray = dataRow.ItemArray;
			}
			if (expression.CachedDelegate.TemplateInputRow != null)
			{
				expression.CachedDelegate.TemplateInputRow.ItemArray = inputRow.ItemArray;
			}
			return expression.CachedDelegate.CompiledDelegate.DynamicInvoke(expression.DependentColumns.ToArray());
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00013B00 File Offset: 0x00011D00
		private static bool IsCacheValid(ColumnExpression expression, DataRow dataRow, DataRow inputRow)
		{
			if (expression.CachedDelegate == null)
			{
				return false;
			}
			bool flag = (dataRow == null && expression.CachedDelegate.TemplateDataRow == null) || (dataRow != null && expression.CachedDelegate.TemplateDataRow != null);
			bool flag2 = (inputRow == null && expression.CachedDelegate.TemplateInputRow == null) || (inputRow != null && expression.CachedDelegate.TemplateInputRow != null);
			return flag && flag2;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00013B73 File Offset: 0x00011D73
		internal static Delegate CompileLambdaExpression(ColumnExpression expression, Type dataType, DataRow dataRow, DataRow inputRow)
		{
			return ExpressionCalculator.CompileLambdaExpression(expression, dataType, null, dataRow, inputRow);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00013B80 File Offset: 0x00011D80
		internal static Delegate CompileLambdaExpression(ColumnExpression expression, Type dataType, Type[] servicePredefinedTypes, DataRow dataRow, DataRow inputRow)
		{
			ParameterExpression[] array = new ParameterExpression[expression.DependentColumns.Count];
			for (int i = 0; i < expression.DependentColumns.Count; i++)
			{
				array[i] = Expression.Parameter(typeof(string), expression.DependentColumns[i]);
			}
			LambdaExpression lambdaExpression = DynamicExpression.ParseLambda(array, dataType, expression.Expression, servicePredefinedTypes, new object[]
			{
				dataRow,
				inputRow
			});
			return lambdaExpression.Compile();
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00013BF8 File Offset: 0x00011DF8
		private Type GetColumnRealDataType(DataColumn dataColumn)
		{
			return ((Type)dataColumn.ExtendedProperties["RealDataType"]) ?? dataColumn.DataType;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00013C1C File Offset: 0x00011E1C
		private IList<KeyValuePair<string, object>> CalculateCore(DataRow dataRow, DataRow inputRow, IList<ColumnExpression> query)
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			foreach (ColumnExpression columnExpression in query)
			{
				list.Add(new KeyValuePair<string, object>(columnExpression.ResultColumn, ExpressionCalculator.CalculateLambdaExpression(columnExpression, this.GetColumnRealDataType(dataRow.Table.Columns[columnExpression.ResultColumn]), dataRow, inputRow)));
			}
			return list;
		}

		// Token: 0x040001B3 RID: 435
		private IList<ColumnExpression> query = new List<ColumnExpression>();
	}
}
