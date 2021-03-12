using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200002A RID: 42
	internal abstract class MailboxFileStoreBase : RecipientObjectActionTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000A02C File Offset: 0x0000822C
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000A034 File Offset: 0x00008234
		[Parameter(Mandatory = true)]
		public OrganizationCapability OrganizationCapability { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000A03D File Offset: 0x0000823D
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000A045 File Offset: 0x00008245
		[Parameter(Mandatory = true)]
		public string FolderName { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000A04E File Offset: 0x0000824E
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000A056 File Offset: 0x00008256
		[Parameter(Mandatory = true)]
		public string FileSetId { get; set; }

		// Token: 0x0600021C RID: 540
		protected abstract void Process(MailboxSession mailboxSession, MailboxFileStore mailboxFileStore);

		// Token: 0x0600021D RID: 541 RVA: 0x0000A060 File Offset: 0x00008260
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			ADUser localOrganizationMailbox = this.GetLocalOrganizationMailbox();
			if (localOrganizationMailbox != null)
			{
				MailboxFileStore mailboxFileStore = new MailboxFileStore(this.FolderName);
				ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(localOrganizationMailbox, null);
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=Management"))
				{
					this.Process(mailboxSession, mailboxFileStore);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A0E0 File Offset: 0x000082E0
		private ADUser GetLocalOrganizationMailbox()
		{
			Server localServer = LocalServerCache.LocalServer;
			ADUser[] array = OrganizationMailbox.FindByOrganizationId(this.DataObject.OrganizationId, this.OrganizationCapability);
			foreach (ADUser aduser in array)
			{
				if (this.DataObject.Identity.Equals(aduser.Id))
				{
					string activeServerFqdn = OrganizationMailbox.GetActiveServerFqdn(aduser.Id);
					if (StringComparer.OrdinalIgnoreCase.Equals(localServer.Fqdn, activeServerFqdn))
					{
						return aduser;
					}
				}
			}
			base.WriteError(new LocalizedException(Strings.ErrorNoLocalOrganizationMailbox(this.DataObject.Identity.ToString())), ErrorCategory.ObjectNotFound, this.Identity);
			return null;
		}
	}
}
