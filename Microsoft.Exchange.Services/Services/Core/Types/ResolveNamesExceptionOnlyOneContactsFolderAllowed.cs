using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000865 RID: 2149
	internal class ResolveNamesExceptionOnlyOneContactsFolderAllowed : ServicePermanentException
	{
		// Token: 0x06003DB2 RID: 15794 RVA: 0x000D7BBD File Offset: 0x000D5DBD
		public ResolveNamesExceptionOnlyOneContactsFolderAllowed() : base((CoreResources.IDs)2683464521U)
		{
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06003DB3 RID: 15795 RVA: 0x000D7BCF File Offset: 0x000D5DCF
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
