using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PswsClient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200003F RID: 63
	[Cmdlet("Set", "EOPMailUser", DefaultParameterSetName = "Identity")]
	public sealed class SetEOPMailUser : EOPTask
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000DAA8 File Offset: 0x0000BCA8
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000DAB0 File Offset: 0x0000BCB0
		[Parameter(Mandatory = false)]
		public MailUserIdParameter Identity { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000DAB9 File Offset: 0x0000BCB9
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0000DAC1 File Offset: 0x0000BCC1
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000DACA File Offset: 0x0000BCCA
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000DAD2 File Offset: 0x0000BCD2
		[Parameter(Mandatory = false)]
		public string Alias { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000DADB File Offset: 0x0000BCDB
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000DAE3 File Offset: 0x0000BCE3
		[Parameter(Mandatory = false)]
		public string DisplayName { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000DAEC File Offset: 0x0000BCEC
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		[Parameter(Mandatory = false)]
		public ProxyAddressCollection EmailAddresses { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000DAFD File Offset: 0x0000BCFD
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000DB05 File Offset: 0x0000BD05
		[Parameter(Mandatory = false)]
		public SmtpAddress MicrosoftOnlineServicesID { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000DB0E File Offset: 0x0000BD0E
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000DB16 File Offset: 0x0000BD16
		[Parameter(Mandatory = false)]
		public SecureString Password { get; set; }

		// Token: 0x060002F1 RID: 753 RVA: 0x0000DB20 File Offset: 0x0000BD20
		protected override void InternalProcessRecord()
		{
			try
			{
				SetMailUserCmdlet setMailUserCmdlet = new SetMailUserCmdlet();
				ADObjectId executingUserId;
				base.ExchangeRunspaceConfig.TryGetExecutingUserId(out executingUserId);
				setMailUserCmdlet.Authenticator = Authenticator.Create(base.CurrentOrganizationId, executingUserId);
				setMailUserCmdlet.HostServerName = EOPRecipient.GetPsWsHostServerName();
				if (string.IsNullOrEmpty(this.ExternalDirectoryObjectId) && this.Identity == null)
				{
					base.ThrowTaskError(new ArgumentException(CoreStrings.MissingIdentityParameter.ToString()));
				}
				EOPRecipient.SetProperty(setMailUserCmdlet, Parameters.Identity, string.IsNullOrEmpty(this.ExternalDirectoryObjectId) ? this.Identity.ToString() : this.ExternalDirectoryObjectId);
				EOPRecipient.SetProperty(setMailUserCmdlet, Parameters.Alias, this.Alias);
				EOPRecipient.SetProperty(setMailUserCmdlet, Parameters.DisplayName, this.DisplayName);
				EOPRecipient.SetProperty(setMailUserCmdlet, Parameters.EmailAddresses, this.EmailAddresses);
				EOPRecipient.SetProperty(setMailUserCmdlet, Parameters.MicrosoftOnlineServicesID, this.MicrosoftOnlineServicesID);
				EOPRecipient.SetProperty(setMailUserCmdlet, Parameters.Password, this.Password);
				EOPRecipient.SetProperty(setMailUserCmdlet, Parameters.Organization, base.Organization);
				setMailUserCmdlet.Run();
				EOPRecipient.CheckForError(this, setMailUserCmdlet);
			}
			catch (Exception e)
			{
				base.ThrowAndLogTaskError(e);
			}
		}
	}
}
