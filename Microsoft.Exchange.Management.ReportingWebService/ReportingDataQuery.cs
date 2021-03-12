using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x0200002A RID: 42
	internal class ReportingDataQuery<T> : IOrderedQueryable<T>, IQueryable<T>, IEnumerable<T>, IOrderedQueryable, IQueryable, IEnumerable, IQueryProvider
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00004B4E File Offset: 0x00002D4E
		public ReportingDataQuery(IReportingDataSource dataSource, IEntity entity) : this(dataSource, entity, null)
		{
			this.expression = Expression.Constant(this);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004B65 File Offset: 0x00002D65
		public ReportingDataQuery(IReportingDataSource dataSource, IEntity entity, Expression expression)
		{
			if (!typeof(T).IsAssignableFrom(entity.ClrType))
			{
				throw new ArgumentException("The underline clr type of the resource entity doesn't match the generic type T");
			}
			this.dataSource = dataSource;
			this.entity = entity;
			this.expression = expression;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004BA4 File Offset: 0x00002DA4
		public ReportingDataQuery(IReportingDataSource dataSource, IEntity entity, Expression expression, Expression expressionWithouSelect)
		{
			if (expressionWithouSelect == null)
			{
				throw new ArgumentNullException("expressionWithouSelect");
			}
			this.dataSource = dataSource;
			this.entity = entity;
			this.expression = expression;
			this.expressionWithouSelect = expressionWithouSelect;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004BD8 File Offset: 0x00002DD8
		public Type ElementType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004BE4 File Offset: 0x00002DE4
		public Expression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004BEC File Offset: 0x00002DEC
		public IQueryProvider Provider
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004BEF File Offset: 0x00002DEF
		internal Expression ExpressionWithouSelect
		{
			get
			{
				return this.expressionWithouSelect;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004C34 File Offset: 0x00002E34
		public IEnumerator<T> GetEnumerator()
		{
			if (!typeof(T).IsAssignableFrom(this.entity.ClrType))
			{
				throw new NotSupportedException("The underline clr type of the resouce entity doesn't match the generic type T");
			}
			IEnumerator<T> enumerator = null;
			ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.CmdletResponseTime, delegate
			{
				enumerator = this.dataSource.GetData<T>(this.entity, this.expression).GetEnumerator();
			});
			return enumerator;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004CCC File Offset: 0x00002ECC
		IEnumerator IEnumerable.GetEnumerator()
		{
			Expression queryExpression = this.expressionWithouSelect ?? this.expression;
			IEnumerator enumerator = null;
			ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.CmdletResponseTime, delegate
			{
				enumerator = this.dataSource.GetData(this.entity, queryExpression).GetEnumerator();
			});
			return enumerator;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004D1C File Offset: 0x00002F1C
		public IQueryable CreateQuery(Expression expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			Type elementType = ReportingDataQuery<T>.TypeSystem.GetElementType(expression.Type);
			Type type = typeof(IQueryable<>).MakeGenericType(new Type[]
			{
				elementType
			});
			if (!type.IsAssignableFrom(expression.Type))
			{
				throw new ArgumentException("The underline clr type of the expression doesn't match the generic types");
			}
			Expression expression2;
			object[] args;
			if (this.IsNewQueryableForSelect(this.Expression, expression, out expression2))
			{
				args = new object[]
				{
					this.dataSource,
					this.entity,
					expression,
					expression2
				};
			}
			else
			{
				args = new object[]
				{
					this.dataSource,
					this.entity,
					expression
				};
			}
			return (IQueryable)Activator.CreateInstance(typeof(ReportingDataQuery<>).MakeGenericType(new Type[]
			{
				elementType
			}), args);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004E04 File Offset: 0x00003004
		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (!typeof(IQueryable<TElement>).IsAssignableFrom(expression.Type))
			{
				throw new ArgumentException("The underline clr type of the expression doesn't match the generic type TElement");
			}
			Expression expression2;
			if (this.IsNewQueryableForSelect(this.Expression, expression, out expression2))
			{
				return new ReportingDataQuery<TElement>(this.dataSource, this.entity, expression, expression2);
			}
			return new ReportingDataQuery<TElement>(this.dataSource, this.entity, expression);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004E78 File Offset: 0x00003078
		public object Execute(Expression expression)
		{
			return this.Execute<object>(expression);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004EBC File Offset: 0x000030BC
		public TElement Execute<TElement>(Expression expression)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			IQueryable result = null;
			ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.CmdletResponseTime, delegate
			{
				result = this.dataSource.GetData(this.entity, this.expression).AsQueryable();
			});
			Expression expression2 = ReportingDataQuery<T>.DecorateExpression(result, (MethodCallExpression)expression);
			return result.Provider.Execute<TElement>(expression2);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004F24 File Offset: 0x00003124
		private static Expression DecorateExpression(IQueryable query, MethodCallExpression oldExpression)
		{
			Stack<MethodCallExpression> stack = new Stack<MethodCallExpression>();
			while (oldExpression != null && oldExpression.Arguments != null && oldExpression.Arguments.Count >= 1 && !(oldExpression.Method == null) && oldExpression.Arguments[0] != null)
			{
				stack.Push(oldExpression);
				oldExpression = (oldExpression.Arguments[0] as MethodCallExpression);
			}
			Expression expression = query.Expression;
			foreach (MethodCallExpression methodCallExpression in stack)
			{
				MethodInfo method = methodCallExpression.Method;
				List<Expression> list = new List<Expression>(methodCallExpression.Arguments.Count);
				list.Add(expression);
				list.AddRange(methodCallExpression.Arguments.Skip(1));
				expression = Expression.Call(method, list);
			}
			return expression;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005008 File Offset: 0x00003208
		private bool IsNewQueryableForSelect(Expression currentQuery, Expression newQuery, out Expression newQueryWithoutSelect)
		{
			ReportingDataQuery<T>.SelectQueryFilter selectQueryFilter = new ReportingDataQuery<T>.SelectQueryFilter();
			ReportingDataQuery<T>.SelectQueryFilter selectQueryFilter2 = new ReportingDataQuery<T>.SelectQueryFilter();
			selectQueryFilter.Visit(currentQuery);
			newQueryWithoutSelect = selectQueryFilter2.Visit(newQuery);
			return !selectQueryFilter.HasSelectQuery && selectQueryFilter2.HasSelectQuery;
		}

		// Token: 0x04000061 RID: 97
		private readonly IReportingDataSource dataSource;

		// Token: 0x04000062 RID: 98
		private readonly IEntity entity;

		// Token: 0x04000063 RID: 99
		private readonly Expression expression;

		// Token: 0x04000064 RID: 100
		private readonly Expression expressionWithouSelect;

		// Token: 0x0200002B RID: 43
		private static class TypeSystem
		{
			// Token: 0x060000F1 RID: 241 RVA: 0x00005044 File Offset: 0x00003244
			internal static Type GetElementType(Type seqType)
			{
				Type type = ReportingDataQuery<T>.TypeSystem.FindIEnumerable(seqType);
				if (type == null)
				{
					return seqType;
				}
				return type.GetGenericArguments()[0];
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x0000506C File Offset: 0x0000326C
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
						Type type3 = ReportingDataQuery<T>.TypeSystem.FindIEnumerable(seqType2);
						if (type3 != null)
						{
							return type3;
						}
					}
				}
				if (seqType.BaseType != null && seqType.BaseType != typeof(object))
				{
					return ReportingDataQuery<T>.TypeSystem.FindIEnumerable(seqType.BaseType);
				}
				return null;
			}
		}

		// Token: 0x0200002C RID: 44
		private class SelectQueryFilter : ExpressionVisitor
		{
			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005194 File Offset: 0x00003394
			// (set) Token: 0x060000F4 RID: 244 RVA: 0x0000519C File Offset: 0x0000339C
			internal bool HasSelectQuery { get; private set; }

			// Token: 0x060000F5 RID: 245 RVA: 0x000051A8 File Offset: 0x000033A8
			protected override Expression VisitMethodCall(MethodCallExpression node)
			{
				if (node.Method.DeclaringType == typeof(Queryable) && node.Method.Name.Equals("select", StringComparison.InvariantCultureIgnoreCase))
				{
					this.HasSelectQuery = true;
					return this.Visit(node.Arguments[0]);
				}
				return base.VisitMethodCall(node);
			}
		}
	}
}
