using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007D3 RID: 2003
	internal class InvalidServerVersionException : ServicePermanentException
	{
		// Token: 0x06003B0E RID: 15118 RVA: 0x000CFB64 File Offset: 0x000CDD64
		public InvalidServerVersionException() : base(CoreResources.IDs.ErrorInvalidServerVersion)
		{
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x000CFB76 File Offset: 0x000CDD76
		public InvalidServerVersionException(Enum messageId) : base(ResponseCodeType.ErrorInvalidServerVersion, messageId)
		{
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06003B10 RID: 15120 RVA: 0x000CFB84 File Offset: 0x000CDD84
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
