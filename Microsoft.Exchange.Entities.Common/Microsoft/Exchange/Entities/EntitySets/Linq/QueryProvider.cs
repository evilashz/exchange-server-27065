using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors;

namespace Microsoft.Exchange.Entities.EntitySets.Linq
{
	// Token: 0x0200003C RID: 60
	public abstract class QueryProvider : IQueryProvider
	{
		// Token: 0x06000140 RID: 320 RVA: 0x00004AF8 File Offset: 0x00002CF8
		IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
		{
			IQueryable queryable = this.NormalizeExpressionAndCreateQuery(typeof(Query<TElement>), typeof(IQueryable<TElement>), expression);
			return (IQueryable<TElement>)queryable;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004B28 File Offset: 0x00002D28
		IQueryable IQueryProvider.CreateQuery(Expression expression)
		{
			Type enumerableElementTypeOrSameType = expression.Type.GetEnumerableElementTypeOrSameType();
			Type queryType = typeof(Query<>).MakeGenericType(new Type[]
			{
				enumerableElementTypeOrSameType
			});
			return this.NormalizeExpressionAndCreateQuery(queryType, typeof(IQueryable), expression);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004B74 File Offset: 0x00002D74
		TResult IQueryProvider.Execute<TResult>(Expression expression)
		{
			object obj = this.OnExecute(expression);
			if (obj is IConvertible)
			{
				return (TResult)((object)Convert.ChangeType(obj, typeof(TResult)));
			}
			return (TResult)((object)obj);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004BAD File Offset: 0x00002DAD
		object IQueryProvider.Execute(Expression expression)
		{
			return this.OnExecute(expression);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004BB6 File Offset: 0x00002DB6
		protected virtual Expression Normalize(Expression expression)
		{
			return expression.Normalize();
		}

		// Token: 0x06000145 RID: 325
		protected abstract object OnExecute(Expression expression);

		// Token: 0x06000146 RID: 326
		protected abstract void Validate(Expression expression);

		// Token: 0x06000147 RID: 327 RVA: 0x00004BC0 File Offset: 0x00002DC0
		private IQueryable NormalizeExpressionAndCreateQuery(Type queryType, Type queryInterface, Expression expression)
		{
			Expression expression2 = this.Normalize(expression);
			ConstantExpression constantExpression = expression2 as ConstantExpression;
			if (constantExpression != null && queryInterface.IsInstanceOfType(constantExpression.Value))
			{
				return (IQueryable)constantExpression.Value;
			}
			this.Validate(expression2);
			return (IQueryable)Activator.CreateInstance(queryType, new object[]
			{
				this,
				expression2
			});
		}
	}
}
