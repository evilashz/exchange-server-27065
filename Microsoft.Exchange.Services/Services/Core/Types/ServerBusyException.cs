using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200087B RID: 2171
	[Serializable]
	internal sealed class ServerBusyException : ServicePermanentException
	{
		// Token: 0x06003E4D RID: 15949 RVA: 0x000D84A9 File Offset: 0x000D66A9
		public ServerBusyException(Exception innerException) : base((CoreResources.IDs)3655513582U, innerException)
		{
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x000D84BC File Offset: 0x000D66BC
		public ServerBusyException() : base((CoreResources.IDs)3655513582U)
		{
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06003E4F RID: 15951 RVA: 0x000D84CE File Offset: 0x000D66CE
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
