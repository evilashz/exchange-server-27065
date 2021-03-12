using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007D0 RID: 2000
	internal class InvalidSchemaVersionForMailboxVersionException : ServicePermanentException
	{
		// Token: 0x06003B06 RID: 15110 RVA: 0x000CFAF7 File Offset: 0x000CDCF7
		public InvalidSchemaVersionForMailboxVersionException() : base(CoreResources.IDs.ErrorInvalidSchemaVersionForMailboxVersion)
		{
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06003B07 RID: 15111 RVA: 0x000CFB09 File Offset: 0x000CDD09
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
