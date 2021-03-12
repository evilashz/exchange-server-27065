using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200027F RID: 639
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Uninstall", "UnifiedMessagingRole", SupportsShouldProcess = true)]
	public sealed class UninstallUnifiedMessagingRole : ManageUnifiedMessagingRole
	{
		// Token: 0x0600177A RID: 6010 RVA: 0x00063717 File Offset: 0x00061917
		protected override void PopulateContextVariables()
		{
			base.PopulateContextVariables();
			base.LogFilePath = Path.Combine((string)base.Fields["SetupLoggingPath"], "remove-UMLanguagePack.en-us.msilog");
			base.WriteVerbose(Strings.UmLanguagePackLogFile(base.LogFilePath));
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x00063755 File Offset: 0x00061955
		protected override LocalizedString Description
		{
			get
			{
				return Strings.UninstallUnifiedMessagingRoleDescription;
			}
		}
	}
}
