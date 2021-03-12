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
	// Token: 0x02000CB5 RID: 3253
	[Cmdlet("New", "PublicFolderMailboxMigrationRequest", SupportsShouldProcess = true, DefaultParameterSetName = "MailboxMigrationLocalPublicFolder")]
	public sealed class NewPublicFolderMailboxMigrationRequest : NewRequest<PublicFolderMailboxMigrationRequest>
	{
		// Token: 0x170026A8 RID: 9896
		// (get) Token: 0x06007CB2 RID: 31922 RVA: 0x001FE6D8 File Offset: 0x001FC8D8
		// (set) Token: 0x06007CB3 RID: 31923 RVA: 0x001FE6EF File Offset: 0x001FC8EF
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "MailboxMigrationLocalPublicFolder")]
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

		// Token: 0x170026A9 RID: 9897
		// (get) Token: 0x06007CB4 RID: 31924 RVA: 0x001FE702 File Offset: 0x001FC902
		// (set) Token: 0x06007CB5 RID: 31925 RVA: 0x001FE70A File Offset: 0x001FC90A
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
		public Stream CSVStream { get; set; }

		// Token: 0x170026AA RID: 9898
		// (get) Token: 0x06007CB6 RID: 31926 RVA: 0x001FE713 File Offset: 0x001FC913
		// (set) Token: 0x06007CB7 RID: 31927 RVA: 0x001FE72A File Offset: 0x001FC92A
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
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

		// Token: 0x170026AB RID: 9899
		// (get) Token: 0x06007CB8 RID: 31928 RVA: 0x001FE73D File Offset: 0x001FC93D
		// (set) Token: 0x06007CB9 RID: 31929 RVA: 0x001FE754 File Offset: 0x001FC954
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
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

		// Token: 0x170026AC RID: 9900
		// (get) Token: 0x06007CBA RID: 31930 RVA: 0x001FE767 File Offset: 0x001FC967
		// (set) Token: 0x06007CBB RID: 31931 RVA: 0x001FE77E File Offset: 0x001FC97E
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "MailboxMigrationOutlookAnywherePublicFolder")]
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

		// Token: 0x170026AD RID: 9901
		// (get) Token: 0x06007CBC RID: 31932 RVA: 0x001FE791 File Offset: 0x001FC991
		// (set) Token: 0x06007CBD RID: 31933 RVA: 0x001FE7A8 File Offset: 0x001FC9A8
		[Parameter(Mandatory = true, ParameterSetName = "MailboxMigrationOutlookAnywherePublicFolder")]
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

		// Token: 0x170026AE RID: 9902
		// (get) Token: 0x06007CBE RID: 31934 RVA: 0x001FE7BB File Offset: 0x001FC9BB
		// (set) Token: 0x06007CBF RID: 31935 RVA: 0x001FE7D2 File Offset: 0x001FC9D2
		[Parameter(Mandatory = true, ParameterSetName = "MailboxMigrationOutlookAnywherePublicFolder")]
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

		// Token: 0x170026AF RID: 9903
		// (get) Token: 0x06007CC0 RID: 31936 RVA: 0x001FE7E5 File Offset: 0x001FC9E5
		// (set) Token: 0x06007CC1 RID: 31937 RVA: 0x001FE806 File Offset: 0x001FCA06
		[Parameter(Mandatory = false, ParameterSetName = "MailboxMigrationOutlookAnywherePublicFolder")]
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

		// Token: 0x170026B0 RID: 9904
		// (get) Token: 0x06007CC2 RID: 31938 RVA: 0x001FE81E File Offset: 0x001FCA1E
		// (set) Token: 0x06007CC3 RID: 31939 RVA: 0x001FE835 File Offset: 0x001FCA35
		[Parameter(Mandatory = true)]
		[ValidateNotNull]
		public MailboxIdParameter TargetMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["TargetMailbox"];
			}
			set
			{
				base.Fields["TargetMailbox"] = value;
			}
		}

		// Token: 0x170026B1 RID: 9905
		// (get) Token: 0x06007CC4 RID: 31940 RVA: 0x001FE848 File Offset: 0x001FCA48
		// (set) Token: 0x06007CC5 RID: 31941 RVA: 0x001FE850 File Offset: 0x001FCA50
		[Parameter(Mandatory = true, ParameterSetName = "MailboxMigrationOutlookAnywherePublicFolder")]
		[ValidateNotNull]
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

		// Token: 0x170026B2 RID: 9906
		// (get) Token: 0x06007CC6 RID: 31942 RVA: 0x001FE859 File Offset: 0x001FCA59
		// (set) Token: 0x06007CC7 RID: 31943 RVA: 0x001FE861 File Offset: 0x001FCA61
		internal ADUser TargetMailboxUser { get; set; }

		// Token: 0x170026B3 RID: 9907
		// (get) Token: 0x06007CC8 RID: 31944 RVA: 0x001FE86A File Offset: 0x001FCA6A
		// (set) Token: 0x06007CC9 RID: 31945 RVA: 0x001FE872 File Offset: 0x001FCA72
		internal TenantPublicFolderConfiguration PublicFolderConfiguration { get; set; }

		// Token: 0x170026B4 RID: 9908
		// (get) Token: 0x06007CCA RID: 31946 RVA: 0x001FE87B File Offset: 0x001FCA7B
		// (set) Token: 0x06007CCB RID: 31947 RVA: 0x001FE883 File Offset: 0x001FCA83
		private new string Name
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

		// Token: 0x170026B5 RID: 9909
		// (get) Token: 0x06007CCC RID: 31948 RVA: 0x001FE88C File Offset: 0x001FCA8C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewPublicFolderMailboxMigrationRequest((this.DataObject == null) ? base.RequestName : this.DataObject.ToString());
			}
		}

		// Token: 0x06007CCD RID: 31949 RVA: 0x001FE8B0 File Offset: 0x001FCAB0
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

		// Token: 0x06007CCE RID: 31950 RVA: 0x001FE90F File Offset: 0x001FCB0F
		protected override void InternalStateReset()
		{
			this.sourceDatabase = null;
			base.InternalStateReset();
		}

		// Token: 0x06007CCF RID: 31951 RVA: 0x001FE920 File Offset: 0x001FCB20
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				string empty = string.Empty;
				string empty2 = string.Empty;
				int num = 0;
				ADObjectId adobjectId = null;
				base.OrganizationId = OrganizationId.ForestWideOrgId;
				if (base.ParameterSetName.Equals("MailboxMigrationOutlookAnywherePublicFolder"))
				{
					IConfigurationSession session = RequestTaskHelper.CreateOrganizationFindingSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
					if (this.Organization == null && base.CurrentOrganizationId != OrganizationId.ForestWideOrgId)
					{
						this.Organization = new OrganizationIdParameter(base.CurrentOrganizationId.OrganizationalUnit);
					}
					if (this.Organization != null)
					{
						ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, session, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
						base.OrganizationId = adorganizationalUnit.OrganizationId;
					}
					base.RescopeToOrgId(base.OrganizationId);
					base.Flags = (RequestFlags.CrossOrg | RequestFlags.Pull);
				}
				else if (base.ParameterSetName.Equals("MailboxMigrationLocalPublicFolder"))
				{
					base.Flags = (RequestFlags.IntraOrg | RequestFlags.Pull);
					PublicFolderDatabase publicFolderDatabase = base.CheckDatabase<PublicFolderDatabase>(this.SourceDatabase, NewRequest<PublicFolderMailboxMigrationRequest>.DatabaseSide.Source, this.SourceDatabase, out empty, out empty2, out adobjectId, out num);
					this.sourceDatabase = publicFolderDatabase.Id;
				}
				this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.FullyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.OrganizationId), 408, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\PublicFolderMailboxMigrationRequest\\NewPublicFolderMailboxMigrationRequest.cs");
				this.TargetMailboxUser = (ADUser)base.GetDataObject<ADUser>(this.TargetMailbox, this.recipientSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(this.TargetMailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(this.TargetMailbox.ToString())));
				TenantPublicFolderConfigurationCache.Instance.RemoveValue(base.OrganizationId);
				this.PublicFolderConfiguration = TenantPublicFolderConfigurationCache.Instance.GetValue(base.OrganizationId);
				if (this.PublicFolderConfiguration.HeuristicsFlags.HasFlag(HeuristicsFlags.PublicFolderMigrationComplete))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorPublicFolderMigrationCompletedPreviously), ExchangeErrorCategory.Client, null);
				}
				if (this.PublicFolderConfiguration.GetHierarchyMailboxInformation().HierarchyMailboxGuid == Guid.Empty)
				{
					base.WriteError(new RecipientTaskException(MrsStrings.PublicFolderMailboxesNotProvisionedForMigration), ExchangeErrorCategory.ServerOperation, null);
				}
				if (this.PublicFolderConfiguration.GetLocalMailboxRecipient(this.TargetMailboxUser.ExchangeGuid) == null)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorCannotMigratePublicFolderIntoNonPublicFolderMailbox), ExchangeErrorCategory.Client, this.TargetMailboxUser);
				}
				if (base.WorkloadType == RequestWorkloadType.None)
				{
					base.WorkloadType = (((base.Flags & RequestFlags.IntraOrg) == RequestFlags.IntraOrg) ? RequestWorkloadType.Local : RequestWorkloadType.Onboarding);
				}
				base.RequestName = "PublicFolderMailboxMigration" + this.TargetMailboxUser.ExchangeGuid;
				ADObjectId requestQueueForMailboxMigrationRequest = this.GetRequestQueueForMailboxMigrationRequest();
				DatabaseIdParameter databaseIdParameter = new DatabaseIdParameter(requestQueueForMailboxMigrationRequest);
				ADObjectId mdbServerSite = null;
				MailboxDatabase mailboxDatabase = base.CheckDatabase<MailboxDatabase>(databaseIdParameter, NewRequest<PublicFolderMailboxMigrationRequest>.DatabaseSide.RequestStorage, this.Organization, out empty, out empty2, out mdbServerSite, out num);
				MailboxTaskHelper.VerifyDatabaseIsWithinScopeForRecipientCmdlets(base.CurrentOrgConfigSession.SessionSettings, mailboxDatabase, new Task.ErrorLoggerDelegate(base.WriteError));
				base.MdbId = mailboxDatabase.Id;
				base.MdbServerSite = mdbServerSite;
				base.RequestName = this.CheckRequestNameAvailability(base.RequestName, null, false, MRSRequestType.PublicFolderMailboxMigration, this.TargetMailbox, false);
				if (base.CheckRequestOfTypeExists(MRSRequestType.PublicFolderMigration))
				{
					base.WriteError(new MultiplePublicFolderMigrationTypesNotAllowedException(), ExchangeErrorCategory.Client, this.Organization);
				}
				this.ValidateCSV();
				base.InternalValidate();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007CD0 RID: 31952 RVA: 0x001FEC94 File Offset: 0x001FCE94
		protected override void SetRequestProperties(TransactionalRequestJob dataObject)
		{
			base.SetRequestProperties(dataObject);
			dataObject.JobType = MRSJobType.RequestJobE15_CreatePublicFoldersUnderParentInSecondary;
			dataObject.RequestType = MRSRequestType.PublicFolderMailboxMigration;
			dataObject.WorkloadType = base.WorkloadType;
			if (base.ParameterSetName.Equals("MailboxMigrationOutlookAnywherePublicFolder"))
			{
				dataObject.AuthenticationMethod = new AuthenticationMethod?(this.AuthenticationMethod);
				dataObject.OutlookAnywhereHostName = this.OutlookAnywhereHostName;
				dataObject.RemoteCredential = RequestTaskHelper.GetNetworkCredential(this.RemoteCredential, new AuthenticationMethod?(this.AuthenticationMethod));
				dataObject.RemoteMailboxLegacyDN = this.RemoteMailboxLegacyDN;
				dataObject.RemoteMailboxServerLegacyDN = this.RemoteMailboxServerLegacyDN;
			}
			else
			{
				dataObject.SourceDatabase = this.sourceDatabase;
				dataObject.SourceExchangeGuid = this.sourceDatabase.ObjectGuid;
			}
			dataObject.PreserveMailboxSignature = false;
			dataObject.PreventCompletion = true;
			dataObject.SuspendWhenReadyToComplete = true;
			dataObject.AllowedToFinishMove = false;
			dataObject.SourceDatabase = this.sourceDatabase;
			dataObject.TargetDatabase = base.MdbId;
			dataObject.TargetUserId = this.TargetMailboxUser.Id;
			dataObject.ExchangeGuid = this.TargetMailboxUser.ExchangeGuid;
			dataObject.TargetExchangeGuid = this.TargetMailboxUser.ExchangeGuid;
			dataObject.FolderToMailboxMap = this.folderToMailboxMapping;
			dataObject.AllowLargeItems = false;
			dataObject.SkipFolderPromotedProperties = true;
			dataObject.SkipFolderViews = true;
			dataObject.SkipFolderRestrictions = true;
			dataObject.SkipContentVerification = true;
		}

		// Token: 0x06007CD1 RID: 31953 RVA: 0x001FEDE4 File Offset: 0x001FCFE4
		protected override PublicFolderMailboxMigrationRequest ConvertToPresentationObject(TransactionalRequestJob dataObject)
		{
			if (dataObject.IndexEntries != null && dataObject.IndexEntries.Count >= 1)
			{
				return new PublicFolderMailboxMigrationRequest(dataObject.IndexEntries[0]);
			}
			base.WriteError(new RequestIndexEntriesAbsentPermanentException(dataObject.ToString()), ErrorCategory.InvalidArgument, this.Organization);
			return null;
		}

		// Token: 0x06007CD2 RID: 31954 RVA: 0x001FEE32 File Offset: 0x001FD032
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is CsvValidationException;
		}

		// Token: 0x06007CD3 RID: 31955 RVA: 0x001FEE74 File Offset: 0x001FD074
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
						ADUser aduser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, this.recipientSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(mailboxIdParameter.ToString())));
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

		// Token: 0x06007CD4 RID: 31956 RVA: 0x001FF078 File Offset: 0x001FD278
		private Dictionary<string, Guid> CreateMailboxNameToGuidMap()
		{
			Dictionary<string, Guid> dictionary = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase);
			foreach (PublicFolderRecipient publicFolderRecipient in this.PublicFolderConfiguration.GetAllMailboxRecipients())
			{
				dictionary.Add(publicFolderRecipient.MailboxName, publicFolderRecipient.MailboxGuid);
			}
			return dictionary;
		}

		// Token: 0x06007CD5 RID: 31957 RVA: 0x001FF0C4 File Offset: 0x001FD2C4
		private ADObjectId GetRequestQueueForMailboxMigrationRequest()
		{
			PublicFolderRecipient localMailboxRecipient = this.PublicFolderConfiguration.GetLocalMailboxRecipient(this.TargetMailboxUser.ExchangeGuid);
			if (localMailboxRecipient == null)
			{
				return null;
			}
			return localMailboxRecipient.Database;
		}

		// Token: 0x04003D8D RID: 15757
		private const string DefaultPublicFolderMailboxMigrationName = "PublicFolderMailboxMigration";

		// Token: 0x04003D8E RID: 15758
		private const string ParameterSetLocalPublicFolderMailboxMigration = "MailboxMigrationLocalPublicFolder";

		// Token: 0x04003D8F RID: 15759
		private const string ParameterSetOutlookAnywherePublicFolderMailboxMigration = "MailboxMigrationOutlookAnywherePublicFolder";

		// Token: 0x04003D90 RID: 15760
		private const string TaskNoun = "PublicFolderMailboxMigrationRequest";

		// Token: 0x04003D91 RID: 15761
		private const string ParameterSourceDatabase = "SourceDatabase";

		// Token: 0x04003D92 RID: 15762
		private const string ParameterCSVData = "CSVData";

		// Token: 0x04003D93 RID: 15763
		private const string ParameterCSVStream = "CSVStream";

		// Token: 0x04003D94 RID: 15764
		private const string ParameterOrganization = "Organization";

		// Token: 0x04003D95 RID: 15765
		private const string ParameterTargetMailbox = "TargetMailbox";

		// Token: 0x04003D96 RID: 15766
		private const string ParameterRemoteMailboxLegacyDN = "RemoteMailboxLegacyDN";

		// Token: 0x04003D97 RID: 15767
		private const string ParameterRemoteMailboxServerLegacyDN = "RemoteMailboxServerLegacyDN";

		// Token: 0x04003D98 RID: 15768
		private const string ParameterOutlookAnywhereHostName = "OutlookAnywhereHostName";

		// Token: 0x04003D99 RID: 15769
		private const string ParameterAuthenticationMethod = "AuthenticationMethod";

		// Token: 0x04003D9A RID: 15770
		private ADObjectId sourceDatabase;

		// Token: 0x04003D9B RID: 15771
		private List<FolderToMailboxMapping> folderToMailboxMapping = new List<FolderToMailboxMapping>();

		// Token: 0x04003D9C RID: 15772
		private IRecipientSession recipientSession;
	}
}
