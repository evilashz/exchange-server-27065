using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200063D RID: 1597
	[LocDescription(Strings.IDs.SetPop3ConfigurationTask)]
	[Cmdlet("Set", "PopSettings", SupportsShouldProcess = true)]
	public sealed class SetPop3Configuration : SetPopImapConfiguration<Pop3AdConfiguration>
	{
		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06003814 RID: 14356 RVA: 0x000E8162 File Offset: 0x000E6362
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetPop3Config;
			}
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000E816C File Offset: 0x000E636C
		protected override void ValidateSetServerRoleSpecificParameters()
		{
			base.ValidateSetServerRoleSpecificParameters();
			if ((base.ServerObject.IsClientAccessServer && base.ServerObject.IsCafeServer) || !base.ServerObject.IsE15OrLater)
			{
				return;
			}
			if (base.ServerObject.IsCafeServer)
			{
				foreach (string text in this.InvalidCafeRoleFieldsForPop3)
				{
					if (base.UserSpecifiedParameters[text] != null)
					{
						this.WriteError(new ExInvalidArgumentForServerRoleException(text, Strings.InstallCafeRoleDescription), ErrorCategory.InvalidArgument, null, false);
					}
				}
			}
		}

		// Token: 0x040025BC RID: 9660
		private readonly string[] InvalidCafeRoleFieldsForPop3 = new string[]
		{
			"MessageRetrievalSortOrder"
		};
	}
}
