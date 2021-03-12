using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200072D RID: 1837
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FilterNotSupportedException : StoragePermanentException
	{
		// Token: 0x060047D3 RID: 18387 RVA: 0x00130606 File Offset: 0x0012E806
		public FilterNotSupportedException(LocalizedString message, QueryFilter filter, params PropertyDefinition[] properties) : base(message)
		{
			this.filter = filter;
			this.properties = properties;
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x060047D4 RID: 18388 RVA: 0x0013061D File Offset: 0x0012E81D
		public PropertyDefinition[] Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x060047D5 RID: 18389 RVA: 0x00130625 File Offset: 0x0012E825
		public QueryFilter Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x04002732 RID: 10034
		private readonly QueryFilter filter;

		// Token: 0x04002733 RID: 10035
		private readonly PropertyDefinition[] properties;
	}
}
