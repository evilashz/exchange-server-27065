using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008C5 RID: 2245
	internal sealed class WrongServerVersionException : ServicePermanentException
	{
		// Token: 0x06003F8B RID: 16267 RVA: 0x000DBA15 File Offset: 0x000D9C15
		public WrongServerVersionException(string smtpAddress) : base(ResponseCodeType.ErrorWrongServerVersion, (CoreResources.IDs)3533302998U)
		{
			base.ConstantValues.Add("AutodiscoverSmtpAddress", smtpAddress);
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06003F8C RID: 16268 RVA: 0x000DBA3D File Offset: 0x000D9C3D
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}

		// Token: 0x04002461 RID: 9313
		private const string AutodiscoverSmtpAddress = "AutodiscoverSmtpAddress";
	}
}
