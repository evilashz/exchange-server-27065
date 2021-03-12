using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Engine;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x02000155 RID: 341
	[Cmdlet("Get", "FailedContentIndexDocuments", SupportsShouldProcess = true, DefaultParameterSetName = "mailbox")]
	public sealed class GetFailedDocuments : GetRecipientObjectTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x000389BF File Offset: 0x00036BBF
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x000389D6 File Offset: 0x00036BD6
		[Parameter(Mandatory = true, ParameterSetName = "mailbox", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[Alias(new string[]
		{
			"mailbox"
		})]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x000389E9 File Offset: 0x00036BE9
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x00038A00 File Offset: 0x00036C00
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "server", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00038A13 File Offset: 0x00036C13
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x00038A2A File Offset: 0x00036C2A
		[Parameter(Mandatory = true, ParameterSetName = "database", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNullOrEmpty]
		public DatabaseIdParameter MailboxDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["MailboxDatabase"];
			}
			set
			{
				base.Fields["MailboxDatabase"] = value;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00038A3D File Offset: 0x00036C3D
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x00038A63 File Offset: 0x00036C63
		[Parameter(Mandatory = false, ParameterSetName = "mailbox")]
		public SwitchParameter Archive
		{
			get
			{
				return (SwitchParameter)(base.Fields["Archive"] ?? false);
			}
			set
			{
				base.Fields["Archive"] = value;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00038A7B File Offset: 0x00036C7B
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00038A9C File Offset: 0x00036C9C
		[Parameter(Mandatory = false)]
		public FailureMode FailureMode
		{
			get
			{
				return (FailureMode)(base.Fields["FailureMode"] ?? FailureMode.Permanent);
			}
			set
			{
				base.Fields["FailureMode"] = value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00038AB4 File Offset: 0x00036CB4
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00038ACB File Offset: 0x00036CCB
		[Parameter(Mandatory = false)]
		[ValidateRange(1, EvaluationErrors.MaxFailureId)]
		public int? ErrorCode
		{
			get
			{
				return (int?)base.Fields["ErrorCode"];
			}
			set
			{
				base.Fields["ErrorCode"] = value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00038AE3 File Offset: 0x00036CE3
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x00038AFA File Offset: 0x00036CFA
		[Parameter(Mandatory = false)]
		public DateTime? StartDate
		{
			get
			{
				return (DateTime?)base.Fields["StartDate"];
			}
			set
			{
				base.Fields["StartDate"] = value;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00038B12 File Offset: 0x00036D12
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x00038B29 File Offset: 0x00036D29
		[Parameter(Mandatory = false)]
		public DateTime? EndDate
		{
			get
			{
				return (DateTime?)base.Fields["EndDate"];
			}
			set
			{
				base.Fields["EndDate"] = value;
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00038B41 File Offset: 0x00036D41
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is RecipientTaskException;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00038B58 File Offset: 0x00036D58
		protected override void InternalValidate()
		{
			if (this.Identity != null)
			{
				base.InternalValidate();
				this.mailbox = (ADUser)base.GetDataObject<ADUser>(this.Identity, base.DataSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(this.Identity)), new LocalizedString?(Strings.ErrorRecipientNotUnique(this.Identity)));
				if (!this.Archive)
				{
					MailboxDatabase item = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(new DatabaseIdParameter(this.mailbox.Database), base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.mailbox.Database.Name)), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.mailbox.Database.Name)));
					this.databases = new List<MailboxDatabase>(1);
					this.databases.Add(item);
					return;
				}
				if (this.mailbox.ArchiveDatabase != null)
				{
					if (this.mailbox.ArchiveGuid != Guid.Empty)
					{
						if (this.mailbox.ArchiveDomain != null)
						{
							base.WriteError(new MdbAdminTaskException(Strings.ErrorRemoteArchiveNoStats(this.mailbox.ToString())), (ErrorCategory)1003, this.Identity);
						}
					}
					else
					{
						base.WriteError(new MdbAdminTaskException(Strings.ErrorArchiveNotEnabled(this.mailbox.ToString())), ErrorCategory.InvalidArgument, this.Identity);
					}
					MailboxDatabase item2 = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(new DatabaseIdParameter(this.mailbox.ArchiveDatabase), base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.mailbox.ArchiveDatabase.Name)), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.mailbox.ArchiveDatabase.Name)));
					this.databases = new List<MailboxDatabase>(1);
					this.databases.Add(item2);
					return;
				}
				base.WriteError(new MdbAdminTaskException(Strings.ErrorArchiveNotEnabled(this.mailbox.ToString())), ErrorCategory.InvalidArgument, this.Identity);
				return;
			}
			else
			{
				if (this.MailboxDatabase != null)
				{
					MailboxDatabase item3 = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.MailboxDatabase, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.MailboxDatabase.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.MailboxDatabase.ToString())));
					this.databases = new List<MailboxDatabase>(1);
					this.databases.Add(item3);
					return;
				}
				if (this.Server != null)
				{
					Server server = (Server)base.GetDataObject<Server>(this.Server, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server)), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server)));
					this.databases = new List<MailboxDatabase>(server.GetMailboxDatabases());
				}
				return;
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00038DF8 File Offset: 0x00036FF8
		protected override void InternalProcessRecord()
		{
			this.InitSubjectRetrievalEnabled();
			int num = 0;
			foreach (MailboxDatabase mdb in this.databases)
			{
				num += this.WriteFailures(mdb, this.ErrorCode, this.FailureMode, (ExDateTime?)this.StartDate, (ExDateTime?)this.EndDate);
			}
			if (this.Identity != null)
			{
				if (num == 0)
				{
					base.WriteVerbose(Strings.FailedDocumentsNoResultsMailbox(this.Identity.ToString()));
					return;
				}
				base.WriteVerbose(Strings.FailedDocumentsResultsMailbox(this.Identity.ToString(), num));
				return;
			}
			else
			{
				if (this.MailboxDatabase == null)
				{
					if (this.Server != null)
					{
						if (num == 0)
						{
							base.WriteVerbose(Strings.FailedDocumentsNoResultsServer(this.Server.ToString()));
							return;
						}
						base.WriteVerbose(Strings.FailedDocumentsResultsServer(this.Server.ToString(), num));
					}
					return;
				}
				if (num == 0)
				{
					base.WriteVerbose(Strings.FailedDocumentsNoResultsDatabase(this.MailboxDatabase.ToString()));
					return;
				}
				base.WriteVerbose(Strings.FailedDocumentsResultsDatabase(this.MailboxDatabase.ToString(), num));
				return;
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00038F28 File Offset: 0x00037128
		private int WriteFailures(MailboxDatabase mdb, int? errorcode, FailureMode failureMode, ExDateTime? startDate, ExDateTime? endDate)
		{
			int num = 0;
			StoreSession storeSession = null;
			ADUser aduser = null;
			Guid? mailboxGuid = null;
			if (this.Identity != null)
			{
				mailboxGuid = new Guid?(this.Archive ? this.mailbox.ArchiveGuid : this.mailbox.ExchangeGuid);
				aduser = this.mailbox;
			}
			MdbInfo mdbInfo = new MdbInfo(mdb);
			mdbInfo.EnableOwningServerUpdate = true;
			mdbInfo.UpdateDatabaseLocationInfo();
			Guid? guid = null;
			try
			{
				using (IFailedItemStorage failedItemStorage = Factory.Current.CreateFailedItemStorage(Factory.Current.CreateSearchServiceConfig(mdbInfo.Guid), mdbInfo.IndexSystemName, mdbInfo.OwningServer))
				{
					FailedItemParameters parameters = new FailedItemParameters(failureMode, FieldSet.Default)
					{
						MailboxGuid = mailboxGuid,
						ErrorCode = errorcode,
						StartDate = startDate,
						EndDate = endDate
					};
					foreach (IFailureEntry failureEntry in failedItemStorage.GetFailedItems(parameters))
					{
						if (failureEntry.MailboxGuid != guid)
						{
							guid = new Guid?(failureEntry.MailboxGuid);
							if (storeSession != null)
							{
								storeSession.Dispose();
								storeSession = null;
							}
							if (mailboxGuid == null)
							{
								aduser = this.GetADUser(guid.Value);
							}
							if (aduser != null && aduser.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox)
							{
								storeSession = this.OpenPublicFolderMailboxSession(mdbInfo.Guid, aduser);
							}
							else
							{
								storeSession = this.OpenMailboxSession(mdbInfo.Guid, guid.Value);
							}
						}
						if (storeSession != null)
						{
							int num2 = (int)storeSession.Mailbox.TryGetProperty(MailboxSchema.MailboxNumber);
							if (num2 != ((MdbItemIdentity)failureEntry.ItemId).MailboxNumber)
							{
								continue;
							}
						}
						string subject = this.GetSubject(failureEntry, storeSession);
						this.WriteResult(new FailedDocument(failureEntry, subject, mdbInfo.Name, aduser));
						num++;
					}
				}
			}
			catch (ComponentException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, null);
			}
			finally
			{
				if (storeSession != null)
				{
					storeSession.Dispose();
				}
			}
			return num;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x000391A8 File Offset: 0x000373A8
		private ADUser GetADUser(Guid mailboxGuid)
		{
			string text = mailboxGuid.ToString();
			try
			{
				return (ADUser)base.GetDataObject<ADUser>(new MailboxIdParameter(text), base.DataSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(text)), new LocalizedString?(Strings.ErrorRecipientNotUnique(text)));
			}
			catch (ManagementObjectNotFoundException)
			{
				return null;
			}
			catch (ManagementObjectAmbiguousException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, null);
			}
			return null;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00039228 File Offset: 0x00037428
		private string GetSubject(IFailureEntry failure, StoreSession session)
		{
			if (this.subjectRetrievalEnabled && session != null)
			{
				try
				{
					using (Item item = Item.Bind(session, StoreObjectId.Deserialize(failure.EntryId)))
					{
						return (item.TryGetProperty(ItemSchema.Subject) as string) ?? string.Empty;
					}
				}
				catch (StoragePermanentException ex)
				{
					base.WriteWarning(ex.Message);
					return string.Empty;
				}
				catch (StorageTransientException ex2)
				{
					base.WriteWarning(ex2.Message);
					return string.Empty;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x000392D4 File Offset: 0x000374D4
		private StoreSession OpenMailboxSession(Guid mdbGuid, Guid mailboxGuid)
		{
			if (mailboxGuid == Guid.Empty)
			{
				return null;
			}
			try
			{
				ExchangePrincipal mailboxOwner = ExchangePrincipal.FromMailboxData(mailboxGuid, mdbGuid, GetFailedDocuments.EmptyCultureInfoCollection, RemotingOptions.AllowCrossSite);
				return MailboxSession.OpenAsSystemService(mailboxOwner, CultureInfo.InvariantCulture, "Client=Management;Action=GetFailedDocuments");
			}
			catch (StoragePermanentException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, null);
			}
			catch (StorageTransientException exception2)
			{
				base.WriteError(exception2, ErrorCategory.ReadError, null);
			}
			return null;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0003934C File Offset: 0x0003754C
		private StoreSession OpenPublicFolderMailboxSession(Guid mdbGuid, ADUser adUser)
		{
			try
			{
				return PublicFolderSession.OpenAsAdmin(adUser.OrganizationId, null, adUser.ExchangeGuid, null, CultureInfo.InvariantCulture, "Client=Management;Action=GetFailedDocuments", null);
			}
			catch (StoragePermanentException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, null);
			}
			catch (StorageTransientException exception2)
			{
				base.WriteError(exception2, ErrorCategory.ReadError, null);
			}
			return null;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x000393B4 File Offset: 0x000375B4
		private void InitSubjectRetrievalEnabled()
		{
			if (base.NeedSuppressingPiiData)
			{
				this.subjectRetrievalEnabled = false;
				return;
			}
			if (this.Identity != null)
			{
				this.subjectRetrievalEnabled = true;
				return;
			}
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ContentIndex\\"))
				{
					if (registryKey != null)
					{
						string value = registryKey.GetValue("SubjectEnabled") as string;
						bool flag;
						if (!string.IsNullOrEmpty(value) && bool.TryParse(value, out flag))
						{
							this.subjectRetrievalEnabled = flag;
							return;
						}
					}
				}
			}
			catch (SecurityException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
			}
			this.subjectRetrievalEnabled = false;
		}

		// Token: 0x04000605 RID: 1541
		private const string ServerParam = "Server";

		// Token: 0x04000606 RID: 1542
		private const string MailboxDatabaseIDParam = "MailboxDatabase";

		// Token: 0x04000607 RID: 1543
		private const string ContentIndexKeyName = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ContentIndex\\";

		// Token: 0x04000608 RID: 1544
		private const string SubjectEnabledKeyName = "SubjectEnabled";

		// Token: 0x04000609 RID: 1545
		private const string ParameterIsArchive = "Archive";

		// Token: 0x0400060A RID: 1546
		private const string ParameterErrorCode = "ErrorCode";

		// Token: 0x0400060B RID: 1547
		private const string ParameterFailureMode = "FailureMode";

		// Token: 0x0400060C RID: 1548
		private const string ParameterStartDate = "StartDate";

		// Token: 0x0400060D RID: 1549
		private const string ParameterEndDate = "EndDate";

		// Token: 0x0400060E RID: 1550
		private static readonly ICollection<CultureInfo> EmptyCultureInfoCollection = new CultureInfo[0];

		// Token: 0x0400060F RID: 1551
		private List<MailboxDatabase> databases;

		// Token: 0x04000610 RID: 1552
		private bool subjectRetrievalEnabled;

		// Token: 0x04000611 RID: 1553
		private ADUser mailbox;
	}
}
