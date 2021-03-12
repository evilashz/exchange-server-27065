using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000864 RID: 2148
	internal class ResolveNamesExceptionInvalidFolderType : ServicePermanentException
	{
		// Token: 0x06003DB0 RID: 15792 RVA: 0x000D7BA4 File Offset: 0x000D5DA4
		public ResolveNamesExceptionInvalidFolderType() : base(CoreResources.IDs.ErrorResolveNamesInvalidFolderType)
		{
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06003DB1 RID: 15793 RVA: 0x000D7BB6 File Offset: 0x000D5DB6
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
