using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PswsClient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000039 RID: 57
	[Cmdlet("New", "EOPMailUser", DefaultParameterSetName = "Identity")]
	public sealed class NewEOPMailUser : EOPTask
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000D2D0 File Offset: 0x0000B4D0
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		[Parameter(Mandatory = false)]
		public string Alias { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000D2E1 File Offset: 0x0000B4E1
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000D2E9 File Offset: 0x0000B4E9
		[Parameter(Mandatory = false)]
		public string DisplayName { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000D2F2 File Offset: 0x0000B4F2
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000D2FA File Offset: 0x0000B4FA
		[Parameter(Mandatory = false)]
		public ProxyAddress ExternalEmailAddress { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000D303 File Offset: 0x0000B503
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000D30B File Offset: 0x0000B50B
		[Parameter(Mandatory = false)]
		public string FirstName { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000D314 File Offset: 0x0000B514
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000D31C File Offset: 0x0000B51C
		[Parameter(Mandatory = false)]
		public string Initials { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000D325 File Offset: 0x0000B525
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000D32D File Offset: 0x0000B52D
		[Parameter(Mandatory = false)]
		public string LastName { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000D336 File Offset: 0x0000B536
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000D33E File Offset: 0x0000B53E
		[Parameter(Mandatory = true)]
		public WindowsLiveId MicrosoftOnlineServicesID { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000D347 File Offset: 0x0000B547
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000D34F File Offset: 0x0000B54F
		[Parameter(Mandatory = true)]
		public string Name { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000D358 File Offset: 0x0000B558
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000D360 File Offset: 0x0000B560
		[Parameter(Mandatory = true)]
		public SecureString Password { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000D369 File Offset: 0x0000B569
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000D371 File Offset: 0x0000B571
		[Parameter(Mandatory = false)]
		public SmtpAddress PrimarySmtpAddress { get; set; }

		// Token: 0x060002BA RID: 698 RVA: 0x0000D37C File Offset: 0x0000B57C
		protected override void InternalProcessRecord()
		{
			try
			{
				NewMailUserCmdlet newMailUserCmdlet = new NewMailUserCmdlet();
				ADObjectId executingUserId;
				base.ExchangeRunspaceConfig.TryGetExecutingUserId(out executingUserId);
				newMailUserCmdlet.Authenticator = Authenticator.Create(base.CurrentOrganizationId, executingUserId);
				newMailUserCmdlet.HostServerName = EOPRecipient.GetPsWsHostServerName();
				EOPRecipient.SetProperty(newMailUserCmdlet, Parameters.FirstName, this.FirstName);
				EOPRecipient.SetProperty(newMailUserCmdlet, Parameters.Initials, this.Initials);
				EOPRecipient.SetProperty(newMailUserCmdlet, Parameters.LastName, this.LastName);
				EOPRecipient.SetProperty(newMailUserCmdlet, Parameters.ExternalEmailAddress, this.ExternalEmailAddress);
				EOPRecipient.SetProperty(newMailUserCmdlet, Parameters.Alias, this.Alias);
				EOPRecipient.SetProperty(newMailUserCmdlet, Parameters.PrimarySmtpAddress, this.PrimarySmtpAddress);
				EOPRecipient.SetProperty(newMailUserCmdlet, Parameters.Organization, base.Organization);
				EOPRecipient.SetProperty(newMailUserCmdlet, Parameters.MicrosoftOnlineServicesID, this.MicrosoftOnlineServicesID);
				EOPRecipient.SetProperty(newMailUserCmdlet, Parameters.DisplayName, this.DisplayName);
				EOPRecipient.SetProperty(newMailUserCmdlet, Parameters.Name, this.Name);
				EOPRecipient.SetProperty(newMailUserCmdlet, Parameters.Password, this.Password);
				newMailUserCmdlet.Run();
				EOPRecipient.CheckForError(this, newMailUserCmdlet);
			}
			catch (Exception e)
			{
				base.ThrowAndLogTaskError(e);
			}
		}
	}
}
