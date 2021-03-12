using System;
using System.Linq.Expressions;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E8F RID: 3727
	internal class DataEntityPropertyProvider<T> : GenericPropertyProvider<T>, IExpressionQueryBuilder where T : IEntity
	{
		// Token: 0x0600611D RID: 24861 RVA: 0x0012EC7E File Offset: 0x0012CE7E
		public DataEntityPropertyProvider(string propertyName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("propertyName", propertyName);
			this.PropertyName = propertyName;
		}

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x0600611E RID: 24862 RVA: 0x0012EC98 File Offset: 0x0012CE98
		// (set) Token: 0x0600611F RID: 24863 RVA: 0x0012ECA0 File Offset: 0x0012CEA0
		public string PropertyName { get; private set; }

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06006120 RID: 24864 RVA: 0x0012ECA9 File Offset: 0x0012CEA9
		// (set) Token: 0x06006121 RID: 24865 RVA: 0x0012ECB1 File Offset: 0x0012CEB1
		public Func<object, ConstantExpression> QueryConstantBuilder { get; set; }

		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x06006122 RID: 24866 RVA: 0x0012ECBA File Offset: 0x0012CEBA
		// (set) Token: 0x06006123 RID: 24867 RVA: 0x0012ECC2 File Offset: 0x0012CEC2
		public Func<MemberExpression> PropertyExpressionBuilder { get; set; }

		// Token: 0x06006124 RID: 24868 RVA: 0x0012ECCB File Offset: 0x0012CECB
		public virtual MemberExpression GetQueryPropertyExpression()
		{
			if (this.PropertyExpressionBuilder != null)
			{
				return this.PropertyExpressionBuilder();
			}
			return Expression.Property(DataEntityPropertyProvider<T>.LamdaParameter, typeof(T), this.PropertyName);
		}

		// Token: 0x06006125 RID: 24869 RVA: 0x0012ECFB File Offset: 0x0012CEFB
		public virtual ConstantExpression GetQueryConstant(object value)
		{
			if (this.QueryConstantBuilder != null)
			{
				return this.QueryConstantBuilder(value);
			}
			return Expression.Constant(value);
		}

		// Token: 0x04003498 RID: 13464
		private static readonly ParameterExpression LamdaParameter = Expression.Parameter(typeof(T), "x");
	}
}
