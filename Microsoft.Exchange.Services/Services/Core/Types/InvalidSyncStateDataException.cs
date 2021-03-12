using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007D7 RID: 2007
	internal sealed class InvalidSyncStateDataException : ServicePermanentException
	{
		// Token: 0x06003B1A RID: 15130 RVA: 0x000CFC0A File Offset: 0x000CDE0A
		public InvalidSyncStateDataException() : base(CoreResources.IDs.ErrorInvalidSyncStateData)
		{
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x000CFC1C File Offset: 0x000CDE1C
		public InvalidSyncStateDataException(Exception innerException) : base(CoreResources.IDs.ErrorInvalidSyncStateData, innerException)
		{
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06003B1C RID: 15132 RVA: 0x000CFC2F File Offset: 0x000CDE2F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
