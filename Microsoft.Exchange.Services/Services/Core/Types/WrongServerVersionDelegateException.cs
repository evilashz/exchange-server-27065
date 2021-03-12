using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008C4 RID: 2244
	internal sealed class WrongServerVersionDelegateException : ServicePermanentException
	{
		// Token: 0x06003F88 RID: 16264 RVA: 0x000DB9DF File Offset: 0x000D9BDF
		public WrongServerVersionDelegateException() : base(ResponseCodeType.ErrorWrongServerVersionDelegate, (CoreResources.IDs)3778961523U)
		{
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x000DB9F6 File Offset: 0x000D9BF6
		public WrongServerVersionDelegateException(Exception innerException) : base(ResponseCodeType.ErrorWrongServerVersionDelegate, (CoreResources.IDs)3778961523U, innerException)
		{
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x000DBA0E File Offset: 0x000D9C0E
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
