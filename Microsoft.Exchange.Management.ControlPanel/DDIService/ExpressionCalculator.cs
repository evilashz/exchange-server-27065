using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Management.ControlPanel.DDI;
using Microsoft.Exchange.Management.SystemManager;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000163 RID: 355
	public sealed class ExpressionCalculator
	{
		// Token: 0x060021EF RID: 8687 RVA: 0x00066374 File Offset: 0x00064574
		public static ExpressionCalculator Parse(DataTable table)
		{
			return ExpressionCalculator.Parse(table, "LambdaExpression");
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0006639C File Offset: 0x0006459C
		public static ExpressionCalculator Parse(DataTable table, string lambdaExpressionKey)
		{
			ExpressionCalculator expressionCalculator = new ExpressionCalculator();
			string text = string.Empty;
			try
			{
				IEnumerable<DataColumn> enumerable = from DataColumn o in table.Columns
				where o.ExtendedProperties.Contains(lambdaExpressionKey)
				select o;
				foreach (DataColumn dataColumn in enumerable)
				{
					text = dataColumn.ExtendedProperties[lambdaExpressionKey].ToString();
					ColumnExpression columnExpression = ExpressionCalculator.BuildColumnExpression(text);
					columnExpression.ResultColumn = dataColumn.ColumnName;
					expressionCalculator.query.Add(columnExpression);
				}
			}
			catch (ParseException innerException)
			{
				throw new LambdaExpressionException(text, innerException);
			}
			catch (TargetInvocationException innerException2)
			{
				throw new LambdaExpressionException(text, innerException2);
			}
			return expressionCalculator;
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x00066494 File Offset: 0x00064694
		public static ColumnExpression BuildColumnExpression(string lambdaExpression)
		{
			ColumnExpression result;
			try
			{
				result = ExpressionCalculator.BuildColumnExpression(lambdaExpression);
			}
			catch (ParseException innerException)
			{
				throw new LambdaExpressionException(lambdaExpression, innerException);
			}
			catch (TargetInvocationException innerException2)
			{
				throw new LambdaExpressionException(lambdaExpression, innerException2);
			}
			return result;
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x000664DC File Offset: 0x000646DC
		public static object CalculateLambdaExpression(ColumnExpression expression, Type dataType, DataRow dataRow, DataRow inputRow)
		{
			object result;
			try
			{
				Type[] servicePredefinedTypes = null;
				if (inputRow != null)
				{
					DataObjectStore dataObjectStore = inputRow.Table.ExtendedProperties["DataSourceStore"] as DataObjectStore;
					if (dataObjectStore != null)
					{
						servicePredefinedTypes = dataObjectStore.ServicePredefinedTypes;
					}
				}
				result = ExpressionCalculator.CalculateLambdaExpression(expression, dataType, servicePredefinedTypes, dataRow, inputRow);
			}
			catch (ParseException innerException)
			{
				throw new LambdaExpressionException(expression.Expression, innerException);
			}
			catch (TargetInvocationException innerException2)
			{
				throw new LambdaExpressionException(expression.Expression, innerException2);
			}
			return result;
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0006655C File Offset: 0x0006475C
		internal static Delegate CompileLambdaExpression(ColumnExpression expression, Type dataType, Type[] servicePredefinedTypes, DataRow dataRow, DataRow inputRow)
		{
			Delegate result;
			try
			{
				result = ExpressionCalculator.CompileLambdaExpression(expression, dataType, servicePredefinedTypes, dataRow, inputRow);
			}
			catch (ParseException innerException)
			{
				throw new LambdaExpressionException(expression.Expression, innerException);
			}
			catch (TargetInvocationException innerException2)
			{
				throw new LambdaExpressionException(expression.Expression, innerException2);
			}
			return result;
		}

		// Token: 0x17001A88 RID: 6792
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x000665B0 File Offset: 0x000647B0
		public int Count
		{
			get
			{
				return this.query.Count;
			}
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x000665BD File Offset: 0x000647BD
		public IList<KeyValuePair<string, object>> CalculateAll(DataRow dataRow, DataRow inputRow)
		{
			return this.CalculateCore(dataRow, inputRow, this.query);
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x000665CD File Offset: 0x000647CD
		public IList<KeyValuePair<string, object>> CalculateSpecifiedColumn(string column, DataRow dataRow)
		{
			return this.CalculateSpecifiedColumn(column, dataRow, null);
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000665F4 File Offset: 0x000647F4
		public IList<KeyValuePair<string, object>> CalculateAffectedColumns(string changedColumn, DataRow dataRow, DataRow inputRow)
		{
			IEnumerable<ColumnExpression> source = from c in this.query
			where c.DependentColumns.Contains(changedColumn)
			select c;
			return this.CalculateCore(dataRow, inputRow, source.ToArray<ColumnExpression>());
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00066650 File Offset: 0x00064850
		public IList<KeyValuePair<string, object>> CalculateSpecifiedColumn(string column, DataRow dataRow, DataRow inputRow)
		{
			IEnumerable<ColumnExpression> source = from c in this.query
			where c.ResultColumn == column
			select c;
			return this.CalculateCore(dataRow, inputRow, source.ToArray<ColumnExpression>());
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x00066690 File Offset: 0x00064890
		private Type GetColumnRealDataType(DataColumn dataColumn)
		{
			return ((Type)dataColumn.ExtendedProperties["RealDataType"]) ?? dataColumn.DataType;
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000666B4 File Offset: 0x000648B4
		private IList<KeyValuePair<string, object>> CalculateCore(DataRow dataRow, DataRow inputRow, IList<ColumnExpression> query)
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			foreach (ColumnExpression columnExpression in query)
			{
				list.Add(new KeyValuePair<string, object>(columnExpression.ResultColumn, ExpressionCalculator.CalculateLambdaExpression(columnExpression, this.GetColumnRealDataType(dataRow.Table.Columns[columnExpression.ResultColumn]), dataRow, inputRow)));
			}
			return list;
		}

		// Token: 0x04001D55 RID: 7509
		private IList<ColumnExpression> query = new List<ColumnExpression>();
	}
}
