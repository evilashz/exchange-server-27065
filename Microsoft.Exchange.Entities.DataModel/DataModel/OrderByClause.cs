using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Microsoft.Exchange.Entities.DataModel
{
	// Token: 0x02000099 RID: 153
	[ImmutableObject(true)]
	public sealed class OrderByClause
	{
		// Token: 0x060003E6 RID: 998 RVA: 0x00007478 File Offset: 0x00005678
		public OrderByClause(Expression expression, ListSortDirection direction)
		{
			this.Expression = expression;
			this.Direction = direction;
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000748E File Offset: 0x0000568E
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00007496 File Offset: 0x00005696
		public ListSortDirection Direction { get; private set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000749F File Offset: 0x0000569F
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x000074A7 File Offset: 0x000056A7
		public Expression Expression { get; private set; }
	}
}
