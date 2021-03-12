using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Tasks;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ReportingTask.Query
{
	// Token: 0x020006B4 RID: 1716
	internal class ExpressionDecorator<TReportObject> : QueryDecorator<TReportObject> where TReportObject : ReportObject
	{
		// Token: 0x06003CB1 RID: 15537 RVA: 0x0010236B File Offset: 0x0010056B
		public ExpressionDecorator(ITaskContext taskContext) : base(taskContext)
		{
			base.IsEnforced = true;
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06003CB2 RID: 15538 RVA: 0x0010237B File Offset: 0x0010057B
		// (set) Token: 0x06003CB3 RID: 15539 RVA: 0x00102383 File Offset: 0x00100583
		public Expression Expression { get; set; }

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06003CB4 RID: 15540 RVA: 0x0010238C File Offset: 0x0010058C
		public override QueryOrder QueryOrder
		{
			get
			{
				return QueryOrder.Expression;
			}
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x0010238F File Offset: 0x0010058F
		public override IQueryable<TReportObject> GetQuery(IQueryable<TReportObject> query)
		{
			if (this.methodCallExpressionStack == null || this.methodCallExpressionStack.Count < 1)
			{
				return query;
			}
			query = this.CreateNewQuery(query);
			return query;
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x001023B3 File Offset: 0x001005B3
		public override void Validate()
		{
			base.Validate();
			if (ExTraceGlobals.LogTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ExTraceGlobals.LogTracer.Information<Expression>(0L, "Expression: {0}", this.Expression);
			}
			this.ValidateType();
			this.ExtractMethodCallExpressionStack();
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x001023EC File Offset: 0x001005EC
		private void ExtractMethodCallExpressionStack()
		{
			if (this.Expression == null)
			{
				return;
			}
			this.methodCallExpressionStack = new Stack<MethodCallExpression>(100);
			for (MethodCallExpression methodCallExpression = this.Expression as MethodCallExpression; methodCallExpression != null; methodCallExpression = (methodCallExpression.Arguments[0] as MethodCallExpression))
			{
				if (this.methodCallExpressionStack.Count >= 100)
				{
					throw new InvalidExpressionException(Strings.TooManyEmbeddedExpressions(100));
				}
				if (methodCallExpression.Arguments == null || methodCallExpression.Arguments.Count < 2 || methodCallExpression.Method == null || methodCallExpression.Arguments[0] == null || methodCallExpression.Arguments[1] == null)
				{
					throw new InvalidExpressionException(Strings.InvalidExpression(this.Expression.ToString()));
				}
				this.methodCallExpressionStack.Push(methodCallExpression);
			}
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x001024B8 File Offset: 0x001006B8
		private void ValidateType()
		{
			if (this.Expression != null)
			{
				Type elementType = ExpressionDecorator<TReportObject>.TypeSystem.GetElementType(this.Expression.Type);
				if (elementType != null && elementType != typeof(TReportObject))
				{
					throw new InvalidExpressionException(Strings.InvalidTypeOfExpression(typeof(TReportObject).ToString(), elementType.ToString()));
				}
			}
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x0010251C File Offset: 0x0010071C
		private IQueryable<TReportObject> CreateNewQuery(IQueryable<TReportObject> query)
		{
			IQueryable<TReportObject> result;
			try
			{
				Expression expression = query.Expression;
				foreach (MethodCallExpression methodCallExpression in this.methodCallExpressionStack)
				{
					MethodInfo method = methodCallExpression.Method;
					List<Expression> list = new List<Expression>(methodCallExpression.Arguments.Count);
					list.Add(expression);
					list.AddRange(methodCallExpression.Arguments.Skip(1));
					expression = Expression.Call(method, list);
				}
				IQueryable<TReportObject> queryable = query.Provider.CreateQuery<TReportObject>(expression);
				result = queryable;
			}
			catch (ArgumentException innerException)
			{
				throw new InvalidExpressionException(Strings.InvalidExpression(this.Expression.ToString()), innerException);
			}
			return result;
		}

		// Token: 0x0400274E RID: 10062
		public const int MaxEmbeddedExpressionCount = 100;

		// Token: 0x0400274F RID: 10063
		private Stack<MethodCallExpression> methodCallExpressionStack;

		// Token: 0x020006B5 RID: 1717
		private static class TypeSystem
		{
			// Token: 0x06003CBA RID: 15546 RVA: 0x001025E8 File Offset: 0x001007E8
			internal static Type GetElementType(Type seqType)
			{
				Type type = ExpressionDecorator<TReportObject>.TypeSystem.FindIEnumerable(seqType);
				if (type == null)
				{
					return seqType;
				}
				return type.GetGenericArguments()[0];
			}

			// Token: 0x06003CBB RID: 15547 RVA: 0x00102610 File Offset: 0x00100810
			private static Type FindIEnumerable(Type seqType)
			{
				if (seqType == null || seqType == typeof(string))
				{
					return null;
				}
				if (seqType.IsArray)
				{
					return typeof(IEnumerable<>).MakeGenericType(new Type[]
					{
						seqType.GetElementType()
					});
				}
				if (seqType.IsGenericType)
				{
					foreach (Type type in seqType.GetGenericArguments())
					{
						Type type2 = typeof(IEnumerable<>).MakeGenericType(new Type[]
						{
							type
						});
						if (type2.IsAssignableFrom(seqType))
						{
							return type2;
						}
					}
				}
				Type[] interfaces = seqType.GetInterfaces();
				if (interfaces != null && interfaces.Length > 0)
				{
					foreach (Type seqType2 in interfaces)
					{
						Type type3 = ExpressionDecorator<TReportObject>.TypeSystem.FindIEnumerable(seqType2);
						if (type3 != null)
						{
							return type3;
						}
					}
				}
				if (seqType.BaseType != null && seqType.BaseType != typeof(object))
				{
					return ExpressionDecorator<TReportObject>.TypeSystem.FindIEnumerable(seqType.BaseType);
				}
				return null;
			}
		}
	}
}
