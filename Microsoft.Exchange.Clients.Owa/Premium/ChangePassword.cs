using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000443 RID: 1091
	public class ChangePassword : OwaPage, IRegistryOnlyForm
	{
		// Token: 0x0600275A RID: 10074 RVA: 0x000E0444 File Offset: 0x000DE644
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			try
			{
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.IsLogonFormatEmail.Enabled)
				{
					this.userName = Utilities.HtmlEncode(base.OwaContext.LogonIdentity.PrimarySmtpAddress.ToString());
				}
				else
				{
					this.userName = Utilities.HtmlEncode(base.OwaContext.LogonIdentity.GetLogonName());
				}
			}
			catch (OwaIdentityException innerException)
			{
				throw new OwaChangePasswordTransientException(Strings.ChangePasswordFailedGetName, innerException);
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x000E04E0 File Offset: 0x000DE6E0
		protected string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x000E04E8 File Offset: 0x000DE6E8
		protected string UserNameLabel
		{
			get
			{
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.IsLogonFormatEmail.Enabled)
				{
					return LocalizedStrings.GetHtmlEncoded(-1568335488);
				}
				return LocalizedStrings.GetHtmlEncoded(50262124);
			}
		}

		// Token: 0x04001B8F RID: 7055
		private string userName;
	}
}
