using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Exchange.Management.ReportingTask;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x020003D3 RID: 979
	internal class FfoExpressionVisitor<TReportObject> : ExpressionVisitor where TReportObject : new()
	{
		// Token: 0x06002308 RID: 8968 RVA: 0x0008E3CC File Offset: 0x0008C5CC
		private FfoExpressionVisitor(object task)
		{
			this.task = task;
			foreach (Tuple<PropertyInfo, ODataInput> tuple in Schema.Utilities.GetProperties<ODataInput>(typeof(TReportObject)))
			{
				this.odataFields.Add(tuple.Item1.Name, tuple.Item2);
			}
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x0008E450 File Offset: 0x0008C650
		public static void Parse(Expression node, object task)
		{
			FfoExpressionVisitor<TReportObject> ffoExpressionVisitor = new FfoExpressionVisitor<TReportObject>(task);
			ffoExpressionVisitor.Visit(node);
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x0008E46C File Offset: 0x0008C66C
		protected override Expression VisitBinary(BinaryExpression node)
		{
			if (node.NodeType == ExpressionType.Equal || node.NodeType == ExpressionType.GreaterThan)
			{
				MemberExpression memberExpression = node.Left as MemberExpression;
				if (memberExpression != null)
				{
					PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
					ConstantExpression constantExpression = node.Right as ConstantExpression;
					if (propertyInfo != null && constantExpression != null)
					{
						if (node.NodeType == ExpressionType.Equal)
						{
							ODataInput odataInput;
							if (this.odataFields.TryGetValue(propertyInfo.Name, out odataInput))
							{
								odataInput.SetCmdletProperty(this.task, Convert.ChangeType(constantExpression.Value, propertyInfo.PropertyType));
								return node;
							}
						}
						else if (propertyInfo.Name == "Index")
						{
							this.SetSkipToken((int)constantExpression.Value + 1);
							return node;
						}
					}
				}
			}
			else if (node.NodeType == ExpressionType.AndAlso || node.NodeType == ExpressionType.OrElse)
			{
				return base.VisitBinary(node);
			}
			throw new InvalidExpressionException(Strings.InvalidQueryParameters);
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x0008E558 File Offset: 0x0008C758
		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			string name;
			if ((name = node.Method.Name) != null && name == "Take")
			{
				IPageableTask pageableTask = this.task as IPageableTask;
				if (pageableTask != null)
				{
					pageableTask.PageSize = (int)((ConstantExpression)node.Arguments[1]).Value;
					this.UpdateCmdletPageParameter();
				}
			}
			return base.VisitMethodCall(node);
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x0008E5C0 File Offset: 0x0008C7C0
		private void SetSkipToken(int skipValue)
		{
			if (!(this.task is IPageableTask))
			{
				throw new NotSupportedException("Paging is not supported.");
			}
			this.skiptoken = new int?(skipValue);
			this.UpdateCmdletPageParameter();
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x0008E5FC File Offset: 0x0008C7FC
		private void UpdateCmdletPageParameter()
		{
			if (this.skiptoken != null)
			{
				IPageableTask pageableTask = (IPageableTask)this.task;
				pageableTask.Page = this.skiptoken.Value / pageableTask.PageSize + 1;
			}
		}

		// Token: 0x04001BA1 RID: 7073
		private const string SkipToken = "Index";

		// Token: 0x04001BA2 RID: 7074
		private const string Top = "Take";

		// Token: 0x04001BA3 RID: 7075
		private object task;

		// Token: 0x04001BA4 RID: 7076
		private Dictionary<string, ODataInput> odataFields = new Dictionary<string, ODataInput>();

		// Token: 0x04001BA5 RID: 7077
		private int? skiptoken;
	}
}
