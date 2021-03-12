using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CAF RID: 3247
	[Cmdlet("New", "PublicFolderMigrationRequest", SupportsShouldProcess = true, DefaultParameterSetName = "MigrationLocalPublicFolder")]
	public sealed class NewPublicFolderMigrationRequest : NewRequest<PublicFolderMigrationRequest>
	{
		// Token: 0x17002697 RID: 9879
		// (get) Token: 0x06007C7C RID: 31868 RVA: 0x001FDB65 File Offset: 0x001FBD65
		// (set) Token: 0x06007C7D RID: 31869 RVA: 0x001FDB6D File Offset: 0x001FBD6D
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17002698 RID: 9880
		// (get) Token: 0x06007C7E RID: 31870 RVA: 0x001FDB76 File Offset: 0x001FBD76
		// (set) Token: 0x06007C7F RID: 31871 RVA: 0x001FDB8D File Offset: 0x001FBD8D
		[Parameter(Mandatory = true, ParameterSetName = "MigrationLocalPublicFolder")]
		[ValidateNotNull]
		public DatabaseIdParameter SourceDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["SourceDatabase"];
			}
			set
			{
				base.Fields["SourceDatabase"] = value;
			}
		}

		// Token: 0x17002699 RID: 9881
		// (get) Token: 0x06007C80 RID: 31872 RVA: 0x001FDBA0 File Offset: 0x001FBDA0
		// (set) Token: 0x06007C81 RID: 31873 RVA: 0x001FDBA8 File Offset: 0x001FBDA8
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public Stream CSVStream { get; set; }

		// Token: 0x1700269A RID: 9882
		// (get) Token: 0x06007C82 RID: 31874 RVA: 0x001FDBB1 File Offset: 0x001FBDB1
		// (set) Token: 0x06007C83 RID: 31875 RVA: 0x001FDBC8 File Offset: 0x001FBDC8
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
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

		// Token: 0x1700269B RID: 9883
		// (get) Token: 0x06007C84 RID: 31876 RVA: 0x001FDBDB File Offset: 0x001FBDDB
		// (set) Token: 0x06007C85 RID: 31877 RVA: 0x001FDBF2 File Offset: 0x001FBDF2
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutlookAnywherePublicFolder")]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x1700269C RID: 9884
		// (get) Token: 0x06007C86 RID: 31878 RVA: 0x001FDC05 File Offset: 0x001FBE05
		// (set) Token: 0x06007C87 RID: 31879 RVA: 0x001FDC1C File Offset: 0x001FBE1C
		[Parameter(Mandatory = true, ParameterSetName = "MigrationOutlookAnywherePublicFolder")]
		[ValidateNotNullOrEmpty]
		public string RemoteMailboxLegacyDN
		{
			get
			{
				return (string)base.Fields["RemoteMailboxLegacyDN"];
			}
			set
			{
				base.Fields["RemoteMailboxLegacyDN"] = value;
			}
		}

		// Token: 0x1700269D RID: 9885
		// (get) Token: 0x06007C88 RID: 31880 RVA: 0x001FDC2F File Offset: 0x001FBE2F
		// (set) Token: 0x06007C89 RID: 31881 RVA: 0x001FDC46 File Offset: 0x001FBE46
		[Parameter(Mandatory = true, ParameterSetName = "MigrationOutlookAnywherePublicFolder")]
		[ValidateNotNullOrEmpty]
		public string RemoteMailboxServerLegacyDN
		{
			get
			{
				return (string)base.Fields["RemoteMailboxServerLegacyDN"];
			}
			set
			{
				base.Fields["RemoteMailboxServerLegacyDN"] = value;
			}
		}

		// Token: 0x1700269E RID: 9886
		// (get) Token: 0x06007C8A RID: 31882 RVA: 0x001FDC59 File Offset: 0x001FBE59
		// (set) Token: 0x06007C8B RID: 31883 RVA: 0x001FDC70 File Offset: 0x001FBE70
		[Parameter(Mandatory = true, ParameterSetName = "MigrationOutlookAnywherePublicFolder")]
		[ValidateNotNull]
		public Fqdn OutlookAnywhereHostName
		{
			get
			{
				return (Fqdn)base.Fields["OutlookAnywhereHostName"];
			}
			set
			{
				base.Fields["OutlookAnywhereHostName"] = value;
			}
		}

		// Token: 0x1700269F RID: 9887
		// (get) Token: 0x06007C8C RID: 31884 RVA: 0x001FDC83 File Offset: 0x001FBE83
		// (set) Token: 0x06007C8D RID: 31885 RVA: 0x001FDCA4 File Offset: 0x001FBEA4
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutlookAnywherePublicFolder")]
		public AuthenticationMethod AuthenticationMethod
		{
			get
			{
				return (AuthenticationMethod)(base.Fields["AuthenticationMethod"] ?? AuthenticationMethod.Basic);
			}
			set
			{
				base.Fields["AuthenticationMethod"] = value;
			}
		}

		// Token: 0x170026A0 RID: 9888
		// (get) Token: 0x06007C8E RID: 31886 RVA: 0x001FDCBC File Offset: 0x001FBEBC
		// (set) Token: 0x06007C8F RID: 31887 RVA: 0x001FDCC4 File Offset: 0x001FBEC4
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "MigrationOutlookAnywherePublicFolder")]
		public new PSCredential RemoteCredential
		{
			get
			{
				return base.RemoteCredential;
			}
			set
			{
				base.RemoteCredential = value;
			}
		}

		// Token: 0x170026A1 RID: 9889
		// (get) Token: 0x06007C90 RID: 31888 RVA: 0x001FDCCD File Offset: 0x001FBECD
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewPublicFolderMigrationRequest((this.DataObject == null) ? base.RequestName : this.DataObject.ToString());
			}
		}

		// Token: 0x06007C91 RID: 31889 RVA: 0x001FDCF0 File Offset: 0x001FBEF0
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.CSVData == null && this.CSVStream == null)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorAtleastOneOfCSVInput), ExchangeErrorCategory.Client, null);
			}
			if (this.CSVData != null && this.CSVStream != null)
			{
				base.WriteError(new RecipientTaskException(Strings.MigrationCsvParameterInvalid), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x06007C92 RID: 31890 RVA: 0x001FDD4F File Offset: 0x001FBF4F
		protected override void InternalStateReset()
		{
			this.sourceDatabase = null;
			base.InternalStateReset();
		}

		// Token: 0x06007C93 RID: 31891 RVA: 0x001FDD60 File Offset: 0x001FBF60
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				string empty = string.Empty;
				string empty2 = string.Empty;
				int num = 0;
				ADObjectId adobjectId = null;
				if (base.ParameterSetName.Equals("MigrationOutlookAnywherePublicFolder"))
				{
					IConfigurationSession session = RequestTaskHelper.CreateOrganizationFindingSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
					if (this.Organization == null)
					{
						this.Organization = new OrganizationIdParameter(base.CurrentOrganizationId.OrganizationalUnit);
					}
					ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, session, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
					base.RescopeToOrgId(adorganizationalUnit.OrganizationId);
					base.Flags = (RequestFlags.CrossOrg | RequestFlags.Pull);
				}
				else if (base.ParameterSetName.Equals("MigrationLocalPublicFolder"))
				{
					base.OrganizationId = OrganizationId.ForestWideOrgId;
					base.Flags = (RequestFlags.IntraOrg | RequestFlags.Pull);
					PublicFolderDatabase publicFolderDatabase = base.CheckDatabase<PublicFolderDatabase>(this.SourceDatabase, NewRequest<PublicFolderMigrationRequest>.DatabaseSide.Source, this.SourceDatabase, out empty, out empty2, out adobjectId, out num);
					this.sourceDatabase = publicFolderDatabase.Id;
				}
				if (base.WorkloadType == RequestWorkloadType.None)
				{
					base.WorkloadType = (((base.Flags & RequestFlags.IntraOrg) == RequestFlags.IntraOrg) ? RequestWorkloadType.Local : RequestWorkloadType.Onboarding);
				}
				if (!string.IsNullOrEmpty(this.Name))
				{
					base.ValidateName();
					base.RequestName = this.Name;
				}
				else
				{
					base.RequestName = "PublicFolderMigration";
				}
				ADObjectId adObjectId = this.AutoSelectRequestQueueForPFRequest(base.OrganizationId);
				DatabaseIdParameter databaseIdParameter = new DatabaseIdParameter(adObjectId);
				ADObjectId mdbServerSite = null;
				MailboxDatabase mailboxDatabase = base.CheckDatabase<MailboxDatabase>(databaseIdParameter, NewRequest<PublicFolderMigrationRequest>.DatabaseSide.RequestStorage, this.Organization, out empty, out empty2, out mdbServerSite, out num);
				MailboxTaskHelper.VerifyDatabaseIsWithinScopeForRecipientCmdlets(base.CurrentOrgConfigSession.SessionSettings, mailboxDatabase, new Task.ErrorLoggerDelegate(base.WriteError));
				base.MdbId = mailboxDatabase.Id;
				base.MdbServerSite = mdbServerSite;
				this.CheckRequestNameAvailability(null, null, false, MRSRequestType.PublicFolderMigration, this.Organization, false);
				if (base.CheckRequestOfTypeExists(MRSRequestType.PublicFolderMailboxMigration))
				{
					base.WriteError(new MultiplePublicFolderMigrationTypesNotAllowedException(), ExchangeErrorCategory.Client, this.Organization);
				}
				this.publicFolderConfiguration = TenantPublicFolderConfigurationCache.Instance.GetValue(base.OrganizationId);
				if (this.publicFolderConfiguration.HeuristicsFlags.HasFlag(HeuristicsFlags.PublicFolderMigrationComplete))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorPublicFolderMigrationCompletedPreviously), ExchangeErrorCategory.Client, null);
				}
				this.ValidateCSV();
				base.InternalValidate();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007C94 RID: 31892 RVA: 0x001FDFCC File Offset: 0x001FC1CC
		protected override void SetRequestProperties(TransactionalRequestJob dataObject)
		{
			base.SetRequestProperties(dataObject);
			dataObject.JobType = MRSJobType.RequestJobE15_CreatePublicFoldersUnderParentInSecondary;
			dataObject.RequestType = MRSRequestType.PublicFolderMigration;
			dataObject.WorkloadType = base.WorkloadType;
			dataObject.AuthenticationMethod = new AuthenticationMethod?(this.AuthenticationMethod);
			dataObject.RemoteMailboxLegacyDN = this.RemoteMailboxLegacyDN;
			dataObject.RemoteMailboxServerLegacyDN = this.RemoteMailboxServerLegacyDN;
			dataObject.OutlookAnywhereHostName = this.OutlookAnywhereHostName;
			dataObject.RemoteCredential = RequestTaskHelper.GetNetworkCredential(this.RemoteCredential, new AuthenticationMethod?(this.AuthenticationMethod));
			dataObject.PreserveMailboxSignature = false;
			dataObject.PreventCompletion = true;
			dataObject.SuspendWhenReadyToComplete = true;
			dataObject.AllowedToFinishMove = false;
			dataObject.SourceDatabase = this.sourceDatabase;
			dataObject.TargetDatabase = base.MdbId;
			dataObject.ExchangeGuid = this.publicFolderConfiguration.GetHierarchyMailboxInformation().HierarchyMailboxGuid;
			dataObject.SourceExchangeGuid = dataObject.ExchangeGuid;
			dataObject.TargetExchangeGuid = dataObject.ExchangeGuid;
			dataObject.FolderToMailboxMap = this.folderToMailboxMapping;
			dataObject.AllowLargeItems = false;
			dataObject.SkipFolderPromotedProperties = true;
			dataObject.SkipFolderViews = true;
			dataObject.SkipFolderRestrictions = true;
			dataObject.SkipContentVerification = true;
		}

		// Token: 0x06007C95 RID: 31893 RVA: 0x001FE0E4 File Offset: 0x001FC2E4
		protected override PublicFolderMigrationRequest ConvertToPresentationObject(TransactionalRequestJob dataObject)
		{
			if (dataObject.IndexEntries != null && dataObject.IndexEntries.Count >= 1)
			{
				return new PublicFolderMigrationRequest(dataObject.IndexEntries[0]);
			}
			base.WriteError(new RequestIndexEntriesAbsentPermanentException(dataObject.ToString()), ErrorCategory.InvalidArgument, this.Organization);
			return null;
		}

		// Token: 0x06007C96 RID: 31894 RVA: 0x001FE132 File Offset: 0x001FC332
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is CsvValidationException;
		}

		// Token: 0x06007C97 RID: 31895 RVA: 0x001FE174 File Offset: 0x001FC374
		private void ValidateCSV()
		{
			Stream stream = null;
			try
			{
				Stream stream2;
				if (this.CSVStream != null)
				{
					stream2 = this.CSVStream;
				}
				else
				{
					stream2 = new MemoryStream(this.CSVData);
					stream = stream2;
				}
				PublicFolderMigrationRequestCsvSchema publicFolderMigrationRequestCsvSchema = new PublicFolderMigrationRequestCsvSchema();
				publicFolderMigrationRequestCsvSchema.PropertyValidationError += delegate(CsvRow row, string columnName, PropertyValidationError error)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorParsingCSV(row.Index, columnName, error.Description)), ExchangeErrorCategory.Client, null);
				};
				Dictionary<string, Guid> dictionary = new Dictionary<string, Guid>();
				Dictionary<string, Guid> dictionary2 = null;
				bool flag = false;
				foreach (CsvRow csvRow in publicFolderMigrationRequestCsvSchema.Read(stream2))
				{
					if (csvRow.Index != 0)
					{
						if (!flag)
						{
							dictionary2 = this.CreateMailboxNameToGuidMap();
							flag = true;
						}
						string text = csvRow["FolderPath"];
						string identity = csvRow["TargetMailbox"];
						MailboxIdParameter mailboxIdParameter = MailboxIdParameter.Parse(identity);
						ADUser aduser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, base.RecipSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(mailboxIdParameter.ToString())));
						if (dictionary.ContainsKey(text))
						{
							base.WriteError(new RecipientTaskException(Strings.ErrorParsingCSV(csvRow.Index, "FolderPath", "DuplicateFolderPathEntry")), ExchangeErrorCategory.Client, null);
						}
						else if (!dictionary2.ContainsValue(aduser.ExchangeGuid))
						{
							base.WriteError(new RecipientTaskException(Strings.ErrorParsingCSV(csvRow.Index, "TargetMailbox", "InvalidPublicFolderMailbox")), ExchangeErrorCategory.Client, null);
						}
						dictionary.Add(text, aduser.ExchangeGuid);
						this.folderToMailboxMapping.Add(new FolderToMailboxMapping(text, aduser.ExchangeGuid));
					}
				}
				if (!flag)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorParsingCSV(1, "FolderPath", "NoInputDataFound")), ExchangeErrorCategory.Client, null);
				}
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
					stream = null;
				}
			}
		}

		// Token: 0x06007C98 RID: 31896 RVA: 0x001FE378 File Offset: 0x001FC578
		private Dictionary<string, Guid> CreateMailboxNameToGuidMap()
		{
			Dictionary<string, Guid> dictionary = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase);
			foreach (PublicFolderRecipient publicFolderRecipient in this.publicFolderConfiguration.GetAllMailboxRecipients())
			{
				dictionary.Add(publicFolderRecipient.MailboxName, publicFolderRecipient.MailboxGuid);
			}
			return dictionary;
		}

		// Token: 0x04003D76 RID: 15734
		private const string DefaultPublicFolderMigrationName = "PublicFolderMigration";

		// Token: 0x04003D77 RID: 15735
		private const string ParameterSetLocalPublicFolderMigration = "MigrationLocalPublicFolder";

		// Token: 0x04003D78 RID: 15736
		internal const string ParameterSetOutlookAnywherePublicFolderMigration = "MigrationOutlookAnywherePublicFolder";

		// Token: 0x04003D79 RID: 15737
		internal const string TaskNoun = "PublicFolderMigrationRequest";

		// Token: 0x04003D7A RID: 15738
		internal const string ParameterSourceDatabase = "SourceDatabase";

		// Token: 0x04003D7B RID: 15739
		internal const string ParameterCSVData = "CSVData";

		// Token: 0x04003D7C RID: 15740
		internal const string ParameterCSVStream = "CSVStream";

		// Token: 0x04003D7D RID: 15741
		internal const string ParameterOrganization = "Organization";

		// Token: 0x04003D7E RID: 15742
		internal const string ParameterRemoteMailboxLegacyDN = "RemoteMailboxLegacyDN";

		// Token: 0x04003D7F RID: 15743
		internal const string ParameterRemoteMailboxServerLegacyDN = "RemoteMailboxServerLegacyDN";

		// Token: 0x04003D80 RID: 15744
		internal const string ParameterOutlookAnywhereHostName = "OutlookAnywhereHostName";

		// Token: 0x04003D81 RID: 15745
		internal const string ParameterAuthenticationMethod = "AuthenticationMethod";

		// Token: 0x04003D82 RID: 15746
		private ADObjectId sourceDatabase;

		// Token: 0x04003D83 RID: 15747
		private TenantPublicFolderConfiguration publicFolderConfiguration;

		// Token: 0x04003D84 RID: 15748
		private List<FolderToMailboxMapping> folderToMailboxMapping = new List<FolderToMailboxMapping>();
	}
}
