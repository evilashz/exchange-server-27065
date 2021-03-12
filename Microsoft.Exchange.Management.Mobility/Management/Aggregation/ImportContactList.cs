using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.ImportContacts;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000022 RID: 34
	[Cmdlet("Import", "ContactList", DefaultParameterSetName = "Data", SupportsShouldProcess = true)]
	public sealed class ImportContactList : RecipientObjectActionTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00007848 File Offset: 0x00005A48
		// (set) Token: 0x06000125 RID: 293 RVA: 0x0000785F File Offset: 0x00005A5F
		[Parameter(Mandatory = true, ParameterSetName = "Data", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "Stream", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override MailboxIdParameter Identity
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00007872 File Offset: 0x00005A72
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00007898 File Offset: 0x00005A98
		[Parameter(Mandatory = true, ParameterSetName = "Data")]
		[Parameter(Mandatory = true, ParameterSetName = "Stream")]
		public SwitchParameter CSV
		{
			get
			{
				return (SwitchParameter)(base.Fields["CSV"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["CSV"] = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000078B0 File Offset: 0x00005AB0
		// (set) Token: 0x06000129 RID: 297 RVA: 0x000078B8 File Offset: 0x00005AB8
		[Parameter(Mandatory = true, ParameterSetName = "Stream")]
		public Stream CSVStream
		{
			get
			{
				return this.csvStream;
			}
			set
			{
				this.csvStream = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000078C1 File Offset: 0x00005AC1
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000078D8 File Offset: 0x00005AD8
		[Parameter(Mandatory = true, ParameterSetName = "Data")]
		public byte[] CSVData
		{
			get
			{
				return (byte[])base.Fields["CSVData"];
			}
			set
			{
				base.Fields["CSVData"] = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000078EB File Offset: 0x00005AEB
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ImportContactListConfirmation(this.Identity);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000078F8 File Offset: 0x00005AF8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				ADSessionSettings adSettings = ADSessionSettings.RescopeToOrganization(base.SessionSettings, this.DataObject.OrganizationId, true);
				ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(adSettings, this.DataObject, RemotingOptions.AllowCrossSite);
				if (this.CSV == true)
				{
					CommonLoggingHelper.SyncLogSession.LogDebugging((TSLID)1260UL, "Importing contacts from CSV.", new object[0]);
					if (this.CSVStream != null)
					{
						this.ImportContactsFromStream(this.CSVStream, exchangePrincipal);
					}
					else
					{
						using (Stream stream = new MemoryStream(this.CSVData))
						{
							this.ImportContactsFromStream(stream, exchangePrincipal);
						}
					}
				}
				base.InternalProcessRecord();
			}
			catch (ObjectNotFoundException innerException)
			{
				this.WriteDebugInfoAndError(new MailboxFailureException(innerException), (ErrorCategory)1002, this.DataObject);
			}
			finally
			{
				this.WriteDebugInfo();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000079F0 File Offset: 0x00005BF0
		private void ImportContactsFromStream(Stream csvStream, ExchangePrincipal exchangePrincipal)
		{
			int num = 0;
			LocalizedException ex = null;
			try
			{
				CommonLoggingHelper.SyncLogSession.LogDebugging((TSLID)1261UL, "Opening Mailbox Session to mailbox {0}.", new object[]
				{
					this.DataObject.Identity
				});
				using (MailboxSession mailboxSession = SubscriptionManager.OpenMailbox(exchangePrincipal, ExchangeMailboxOpenType.AsAdministrator, ImportContactList.ClientInfoString))
				{
					CommonLoggingHelper.SyncLogSession.LogDebugging((TSLID)1262UL, "Start Contacts Import to mailbox {0}.", new object[]
					{
						this.DataObject.Identity
					});
					OutlookCsvImportContact outlookCsvImportContact = new OutlookCsvImportContact(mailboxSession);
					num = outlookCsvImportContact.ImportContactsFromOutlookCSV(csvStream);
				}
			}
			catch (ImportContactsException ex2)
			{
				ex = ex2;
			}
			catch (LocalizedException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				this.WriteDebugInfoAndError(ex, (ErrorCategory)1003, this.DataObject);
			}
			CommonLoggingHelper.SyncLogSession.LogDebugging((TSLID)1263UL, "Imported {0} contacts to mailbox {1}.", new object[]
			{
				num,
				this.DataObject.Identity
			});
			ImportContactListResult importContactListResult = new ImportContactListResult(this.DataObject.Identity);
			importContactListResult.ContactsImported = num;
			importContactListResult.ResetChangeTracking();
			base.WriteObject(importContactListResult);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007B44 File Offset: 0x00005D44
		private void WriteDebugInfoAndError(Exception exception, ErrorCategory category, object target)
		{
			this.WriteDebugInfo();
			base.WriteError(exception, category, target);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007B55 File Offset: 0x00005D55
		private void WriteDebugInfo()
		{
			if (base.IsDebugOn)
			{
				base.WriteDebug(CommonLoggingHelper.SyncLogSession.GetBlackBoxText());
			}
			CommonLoggingHelper.SyncLogSession.ClearBlackBox();
		}

		// Token: 0x04000075 RID: 117
		private const string ParameterSetNameStream = "Stream";

		// Token: 0x04000076 RID: 118
		private const string ParameterSetNameData = "Data";

		// Token: 0x04000077 RID: 119
		private static readonly string ClientInfoString = "Client=TransportSync;Action=ImportContacts";

		// Token: 0x04000078 RID: 120
		private Stream csvStream;
	}
}
