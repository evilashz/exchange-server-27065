using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200062E RID: 1582
	[LocDescription(Strings.IDs.SetImap4ConfigurationTask)]
	[Cmdlet("Set", "ImapSettings", SupportsShouldProcess = true)]
	public sealed class SetImap4Configuration : SetPopImapConfiguration<Imap4AdConfiguration>
	{
		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x060037DA RID: 14298 RVA: 0x000E7D56 File Offset: 0x000E5F56
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetImap4Config;
			}
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000E7D60 File Offset: 0x000E5F60
		protected override void ValidateSetServerRoleSpecificParameters()
		{
			base.ValidateSetServerRoleSpecificParameters();
			if ((base.ServerObject.IsClientAccessServer && base.ServerObject.IsCafeServer) || !base.ServerObject.IsE15OrLater)
			{
				return;
			}
			if (base.ServerObject.IsCafeServer)
			{
				foreach (string text in this.InvalidCafeRoleFieldsForImap4)
				{
					if (base.UserSpecifiedParameters[text] != null)
					{
						this.WriteError(new ExInvalidArgumentForServerRoleException(text, Strings.InstallCafeRoleDescription), ErrorCategory.InvalidArgument, null, false);
					}
				}
			}
		}

		// Token: 0x040025BB RID: 9659
		private readonly string[] InvalidCafeRoleFieldsForImap4 = new string[]
		{
			"ShowHiddenFoldersEnabled"
		};
	}
}
