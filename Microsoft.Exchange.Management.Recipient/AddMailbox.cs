using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000064 RID: 100
	[Cmdlet("Add", "Mailbox", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class AddMailbox : RecipientObjectActionTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001BB7F File Offset: 0x00019D7F
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x0001BBA5 File Offset: 0x00019DA5
		[Parameter(Mandatory = false)]
		public SwitchParameter AuxArchive
		{
			get
			{
				return (SwitchParameter)(base.Fields["AuxArchive"] ?? false);
			}
			set
			{
				base.Fields["AuxArchive"] = value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001BBBD File Offset: 0x00019DBD
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x0001BBE3 File Offset: 0x00019DE3
		[Parameter(Mandatory = false)]
		public SwitchParameter AuxPrimary
		{
			get
			{
				return (SwitchParameter)(base.Fields["AuxPrimary"] ?? false);
			}
			set
			{
				base.Fields["AuxPrimary"] = value;
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001BBFC File Offset: 0x00019DFC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			IMailboxLocationCollection mailboxLocations = this.DataObject.MailboxLocations;
			if (this.AuxArchive)
			{
				if (mailboxLocations.GetMailboxLocation(MailboxLocationType.MainArchive) == null)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorMailboxHasNoArchive(this.Identity.ToString())), ExchangeErrorCategory.Client, null);
				}
			}
			else if (this.AuxPrimary && this.DataObject.RecipientTypeDetails != RecipientTypeDetails.AuditLogMailbox)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorMailboxIsNotAudit(this.Identity.ToString())), ExchangeErrorCategory.Client, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001BCA4 File Offset: 0x00019EA4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADUser dataObject = this.DataObject;
			IMailboxLocationCollection mailboxLocations = dataObject.MailboxLocations;
			if (this.AuxArchive)
			{
				IMailboxLocationInfo mailboxLocation = mailboxLocations.GetMailboxLocation(MailboxLocationType.MainArchive);
				mailboxLocations.AddMailboxLocation(Guid.NewGuid(), mailboxLocation.DatabaseLocation, MailboxLocationType.AuxArchive);
			}
			else if (this.AuxPrimary)
			{
				mailboxLocations.AddMailboxLocation(Guid.NewGuid(), dataObject.Database, MailboxLocationType.AuxPrimary);
			}
			base.InternalProcessRecord();
			this.WriteResult();
			TaskLogger.LogExit();
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001BD20 File Offset: 0x00019F20
		private void WriteResult()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Id
			});
			Mailbox sendToPipeline = new Mailbox(this.DataObject);
			base.WriteObject(sendToPipeline);
			TaskLogger.LogExit();
		}
	}
}
