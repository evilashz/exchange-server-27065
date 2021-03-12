using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C76 RID: 3190
	[Cmdlet("Get", "MoveRequest", DefaultParameterSetName = "Identity")]
	public sealed class GetMoveRequest : GetRecipientBase<MoveRequestIdParameter, ADUser>
	{
		// Token: 0x0600799C RID: 31132 RVA: 0x001EF9D0 File Offset: 0x001EDBD0
		public GetMoveRequest()
		{
			this.targetDatabase = null;
			this.sourceDatabase = null;
			QueryFilter queryFilter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.NotEqual, ADUserSchema.MailboxMoveStatus, RequestStatus.None),
				new ExistsFilter(ADUserSchema.MailboxMoveStatus)
			});
			if (base.OptionalIdentityData.AdditionalFilter == null)
			{
				base.OptionalIdentityData.AdditionalFilter = queryFilter;
				return;
			}
			base.OptionalIdentityData.AdditionalFilter = new AndFilter(new QueryFilter[]
			{
				queryFilter,
				base.OptionalIdentityData.AdditionalFilter
			});
		}

		// Token: 0x170025A1 RID: 9633
		// (get) Token: 0x0600799D RID: 31133 RVA: 0x001EFA64 File Offset: 0x001EDC64
		// (set) Token: 0x0600799E RID: 31134 RVA: 0x001EFA7B File Offset: 0x001EDC7B
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public DatabaseIdParameter TargetDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["TargetDatabase"];
			}
			set
			{
				base.Fields["TargetDatabase"] = value;
			}
		}

		// Token: 0x170025A2 RID: 9634
		// (get) Token: 0x0600799F RID: 31135 RVA: 0x001EFA8E File Offset: 0x001EDC8E
		// (set) Token: 0x060079A0 RID: 31136 RVA: 0x001EFAA5 File Offset: 0x001EDCA5
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
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

		// Token: 0x170025A3 RID: 9635
		// (get) Token: 0x060079A1 RID: 31137 RVA: 0x001EFAB8 File Offset: 0x001EDCB8
		// (set) Token: 0x060079A2 RID: 31138 RVA: 0x001EFAD9 File Offset: 0x001EDCD9
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public RequestStatus MoveStatus
		{
			get
			{
				return (RequestStatus)(base.Fields["MoveStatus"] ?? RequestStatus.None);
			}
			set
			{
				base.Fields["MoveStatus"] = value;
			}
		}

		// Token: 0x170025A4 RID: 9636
		// (get) Token: 0x060079A3 RID: 31139 RVA: 0x001EFAF1 File Offset: 0x001EDCF1
		// (set) Token: 0x060079A4 RID: 31140 RVA: 0x001EFB08 File Offset: 0x001EDD08
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public Fqdn RemoteHostName
		{
			get
			{
				return (Fqdn)base.Fields["RemoteHostName"];
			}
			set
			{
				base.Fields["RemoteHostName"] = value;
			}
		}

		// Token: 0x170025A5 RID: 9637
		// (get) Token: 0x060079A5 RID: 31141 RVA: 0x001EFB1B File Offset: 0x001EDD1B
		// (set) Token: 0x060079A6 RID: 31142 RVA: 0x001EFB32 File Offset: 0x001EDD32
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public string BatchName
		{
			get
			{
				return (string)base.Fields["BatchName"];
			}
			set
			{
				base.Fields["BatchName"] = value;
			}
		}

		// Token: 0x170025A6 RID: 9638
		// (get) Token: 0x060079A7 RID: 31143 RVA: 0x001EFB45 File Offset: 0x001EDD45
		// (set) Token: 0x060079A8 RID: 31144 RVA: 0x001EFB66 File Offset: 0x001EDD66
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public bool Protect
		{
			get
			{
				return (bool)(base.Fields["Protect"] ?? false);
			}
			set
			{
				base.Fields["Protect"] = value;
			}
		}

		// Token: 0x170025A7 RID: 9639
		// (get) Token: 0x060079A9 RID: 31145 RVA: 0x001EFB7E File Offset: 0x001EDD7E
		// (set) Token: 0x060079AA RID: 31146 RVA: 0x001EFB9F File Offset: 0x001EDD9F
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public bool Offline
		{
			get
			{
				return (bool)(base.Fields["Offline"] ?? false);
			}
			set
			{
				base.Fields["Offline"] = value;
			}
		}

		// Token: 0x170025A8 RID: 9640
		// (get) Token: 0x060079AB RID: 31147 RVA: 0x001EFBB7 File Offset: 0x001EDDB7
		// (set) Token: 0x060079AC RID: 31148 RVA: 0x001EFBD8 File Offset: 0x001EDDD8
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public bool Suspend
		{
			get
			{
				return (bool)(base.Fields["Suspend"] ?? false);
			}
			set
			{
				base.Fields["Suspend"] = value;
			}
		}

		// Token: 0x170025A9 RID: 9641
		// (get) Token: 0x060079AD RID: 31149 RVA: 0x001EFBF0 File Offset: 0x001EDDF0
		// (set) Token: 0x060079AE RID: 31150 RVA: 0x001EFC11 File Offset: 0x001EDE11
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public bool SuspendWhenReadyToComplete
		{
			get
			{
				return (bool)(base.Fields["SuspendWhenReadyToComplete"] ?? false);
			}
			set
			{
				base.Fields["SuspendWhenReadyToComplete"] = value;
			}
		}

		// Token: 0x170025AA RID: 9642
		// (get) Token: 0x060079AF RID: 31151 RVA: 0x001EFC29 File Offset: 0x001EDE29
		// (set) Token: 0x060079B0 RID: 31152 RVA: 0x001EFC4A File Offset: 0x001EDE4A
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public bool HighPriority
		{
			get
			{
				return (bool)(base.Fields["HighPriority"] ?? false);
			}
			set
			{
				base.Fields["HighPriority"] = value;
			}
		}

		// Token: 0x170025AB RID: 9643
		// (get) Token: 0x060079B1 RID: 31153 RVA: 0x001EFC62 File Offset: 0x001EDE62
		// (set) Token: 0x060079B2 RID: 31154 RVA: 0x001EFC6A File Offset: 0x001EDE6A
		[Parameter(Mandatory = false, ParameterSetName = "Filtering", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[ValidateNotNull]
		public new AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return base.AccountPartition;
			}
			set
			{
				base.AccountPartition = value;
			}
		}

		// Token: 0x170025AC RID: 9644
		// (get) Token: 0x060079B3 RID: 31155 RVA: 0x001EFC73 File Offset: 0x001EDE73
		// (set) Token: 0x060079B4 RID: 31156 RVA: 0x001EFC94 File Offset: 0x001EDE94
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public RequestFlags Flags
		{
			get
			{
				return (RequestFlags)(base.Fields["Flags"] ?? RequestFlags.None);
			}
			set
			{
				base.Fields["Flags"] = value;
			}
		}

		// Token: 0x170025AD RID: 9645
		// (get) Token: 0x060079B5 RID: 31157 RVA: 0x001EFCAC File Offset: 0x001EDEAC
		// (set) Token: 0x060079B6 RID: 31158 RVA: 0x001EFCD2 File Offset: 0x001EDED2
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter IncludeSoftDeletedObjects
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeSoftDeletedObjects"] ?? false);
			}
			set
			{
				base.Fields["IncludeSoftDeletedObjects"] = value;
			}
		}

		// Token: 0x170025AE RID: 9646
		// (get) Token: 0x060079B7 RID: 31159 RVA: 0x001EFCEA File Offset: 0x001EDEEA
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<MoveRequestUserSchema>();
			}
		}

		// Token: 0x170025AF RID: 9647
		// (get) Token: 0x060079B8 RID: 31160 RVA: 0x001EFCF1 File Offset: 0x001EDEF1
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetMoveRequest.SortPropertiesArray;
			}
		}

		// Token: 0x170025B0 RID: 9648
		// (get) Token: 0x060079B9 RID: 31161 RVA: 0x001EFCF8 File Offset: 0x001EDEF8
		protected override QueryFilter InternalFilter
		{
			get
			{
				List<QueryFilter> list = new List<QueryFilter>();
				QueryFilter internalFilter = base.InternalFilter;
				if (internalFilter != null)
				{
					list.Add(internalFilter);
				}
				if (this.targetDatabase != null)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.MailboxMoveTargetMDB, ((MailboxDatabase)this.targetDatabase).Id));
				}
				if (this.sourceDatabase != null)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.MailboxMoveSourceMDB, ((MailboxDatabase)this.sourceDatabase).Id));
				}
				if (!this.IsFieldSet("MoveStatus"))
				{
					list.Add(new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.NotEqual, ADUserSchema.MailboxMoveStatus, RequestStatus.None),
						new ExistsFilter(ADUserSchema.MailboxMoveStatus)
					}));
				}
				else if (this.MoveStatus != RequestStatus.None)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.MailboxMoveStatus, this.MoveStatus));
				}
				else
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorCannotFindMoveRequestWithStatusNone), ErrorCategory.InvalidArgument, this.MoveStatus);
				}
				if (this.IsFieldSet("RemoteHostName"))
				{
					if (string.IsNullOrEmpty(this.RemoteHostName))
					{
						list.Add(QueryFilter.NotFilter(new ExistsFilter(ADUserSchema.MailboxMoveRemoteHostName)));
					}
					else
					{
						list.Add(new TextFilter(ADUserSchema.MailboxMoveRemoteHostName, this.RemoteHostName, MatchOptions.WildcardString, MatchFlags.IgnoreCase));
					}
				}
				if (this.IsFieldSet("BatchName"))
				{
					if (string.IsNullOrEmpty(this.BatchName))
					{
						list.Add(new NotFilter(new ExistsFilter(ADUserSchema.MailboxMoveBatchName)));
					}
					else
					{
						list.Add(new TextFilter(ADUserSchema.MailboxMoveBatchName, this.BatchName, MatchOptions.WildcardString, MatchFlags.IgnoreCase));
					}
				}
				if (this.IsFieldSet("Protect"))
				{
					QueryFilter queryFilter = new BitMaskAndFilter(ADUserSchema.MailboxMoveFlags, 32UL);
					if (this.Protect)
					{
						list.Add(queryFilter);
					}
					else
					{
						list.Add(new NotFilter(queryFilter));
					}
				}
				if (this.IsFieldSet("Offline"))
				{
					QueryFilter queryFilter2 = new BitMaskAndFilter(ADUserSchema.MailboxMoveFlags, 16UL);
					if (this.Offline)
					{
						list.Add(queryFilter2);
					}
					else
					{
						list.Add(new NotFilter(queryFilter2));
					}
				}
				if (this.IsFieldSet("Suspend"))
				{
					QueryFilter queryFilter3 = new BitMaskAndFilter(ADUserSchema.MailboxMoveFlags, 256UL);
					if (this.Suspend)
					{
						list.Add(queryFilter3);
					}
					else
					{
						list.Add(new NotFilter(queryFilter3));
					}
				}
				if (this.IsFieldSet("SuspendWhenReadyToComplete"))
				{
					QueryFilter queryFilter4 = new BitMaskAndFilter(ADUserSchema.MailboxMoveFlags, 512UL);
					if (this.SuspendWhenReadyToComplete)
					{
						list.Add(queryFilter4);
					}
					else
					{
						list.Add(new NotFilter(queryFilter4));
					}
				}
				if (this.IsFieldSet("HighPriority"))
				{
					QueryFilter queryFilter5 = new BitMaskAndFilter(ADUserSchema.MailboxMoveFlags, 128UL);
					if (this.HighPriority)
					{
						list.Add(queryFilter5);
					}
					else
					{
						list.Add(new NotFilter(queryFilter5));
					}
				}
				if (this.IsFieldSet("Flags"))
				{
					QueryFilter item = new BitMaskAndFilter(ADUserSchema.MailboxMoveFlags, (ulong)((long)this.Flags));
					list.Add(item);
				}
				return new AndFilter(list.ToArray());
			}
		}

		// Token: 0x060079BA RID: 31162 RVA: 0x001EFFF0 File Offset: 0x001EE1F0
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			if (this.IncludeSoftDeletedObjects)
			{
				recipientSession.SessionSettings.IncludeSoftDeletedObjects = true;
			}
			return recipientSession;
		}

		// Token: 0x170025B1 RID: 9649
		// (get) Token: 0x060079BB RID: 31163 RVA: 0x001F0023 File Offset: 0x001EE223
		// (set) Token: 0x060079BC RID: 31164 RVA: 0x001F002B File Offset: 0x001EE22B
		private new string Anr
		{
			get
			{
				return base.Anr;
			}
			set
			{
				base.Anr = value;
			}
		}

		// Token: 0x170025B2 RID: 9650
		// (get) Token: 0x060079BD RID: 31165 RVA: 0x001F0034 File Offset: 0x001EE234
		// (set) Token: 0x060079BE RID: 31166 RVA: 0x001F003C File Offset: 0x001EE23C
		private new string Filter
		{
			get
			{
				return base.Filter;
			}
			set
			{
				base.Filter = value;
			}
		}

		// Token: 0x170025B3 RID: 9651
		// (get) Token: 0x060079BF RID: 31167 RVA: 0x001F0045 File Offset: 0x001EE245
		// (set) Token: 0x060079C0 RID: 31168 RVA: 0x001F004D File Offset: 0x001EE24D
		private new SwitchParameter ReadFromDomainController
		{
			get
			{
				return base.ReadFromDomainController;
			}
			set
			{
				base.ReadFromDomainController = value;
			}
		}

		// Token: 0x170025B4 RID: 9652
		// (get) Token: 0x060079C1 RID: 31169 RVA: 0x001F0056 File Offset: 0x001EE256
		// (set) Token: 0x060079C2 RID: 31170 RVA: 0x001F005E File Offset: 0x001EE25E
		private new SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return base.IgnoreDefaultScope;
			}
			set
			{
				base.IgnoreDefaultScope = value;
			}
		}

		// Token: 0x060079C3 RID: 31171 RVA: 0x001F0068 File Offset: 0x001EE268
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.TargetDatabase != null)
			{
				this.TargetDatabase.AllowLegacy = true;
				this.targetDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.TargetDatabase, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.TargetDatabase.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.TargetDatabase.ToString())));
			}
			if (this.SourceDatabase != null)
			{
				this.SourceDatabase.AllowLegacy = true;
				this.sourceDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.SourceDatabase, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.SourceDatabase.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.SourceDatabase.ToString())));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060079C4 RID: 31172 RVA: 0x001F0146 File Offset: 0x001EE346
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return MoveRequest.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x060079C5 RID: 31173 RVA: 0x001F0154 File Offset: 0x001EE354
		protected override void WriteResult(IConfigurable dataObject)
		{
			if (!base.ParameterSetName.Equals("Identity"))
			{
				base.WriteResult(dataObject);
				return;
			}
			ADUser aduser = (ADUser)dataObject;
			if (aduser.MailboxMoveStatus != RequestStatus.None)
			{
				base.WriteResult(dataObject);
				return;
			}
			this.WriteError(new ManagementObjectNotFoundException(Strings.ErrorUserNotBeingMoved(dataObject.ToString())), ErrorCategory.InvalidArgument, this.Identity, false);
		}

		// Token: 0x060079C6 RID: 31174 RVA: 0x001F01B0 File Offset: 0x001EE3B0
		private bool IsFieldSet(string fieldName)
		{
			return base.Fields.IsChanged(fieldName) || base.Fields.IsModified(fieldName);
		}

		// Token: 0x04003C67 RID: 15463
		public const string ParameterTargetDatabase = "TargetDatabase";

		// Token: 0x04003C68 RID: 15464
		public const string ParameterSourceDatabase = "SourceDatabase";

		// Token: 0x04003C69 RID: 15465
		public const string ParameterMoveStatus = "MoveStatus";

		// Token: 0x04003C6A RID: 15466
		public const string ParameterRemoteHostName = "RemoteHostName";

		// Token: 0x04003C6B RID: 15467
		public const string ParameterBatchName = "BatchName";

		// Token: 0x04003C6C RID: 15468
		public const string ParameterProtect = "Protect";

		// Token: 0x04003C6D RID: 15469
		public const string ParameterOffline = "Offline";

		// Token: 0x04003C6E RID: 15470
		public const string ParameterHighPriority = "HighPriority";

		// Token: 0x04003C6F RID: 15471
		public const string ParameterFlags = "Flags";

		// Token: 0x04003C70 RID: 15472
		public const string ParameterSuspend = "Suspend";

		// Token: 0x04003C71 RID: 15473
		public const string ParameterSuspendWhenReadyToComplete = "SuspendWhenReadyToComplete";

		// Token: 0x04003C72 RID: 15474
		public const string ParameterIncludeSoftDeletedObjects = "IncludeSoftDeletedObjects";

		// Token: 0x04003C73 RID: 15475
		public const string FiltersSet = "Filtering";

		// Token: 0x04003C74 RID: 15476
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			MailEnabledRecipientSchema.DisplayName,
			MailEnabledRecipientSchema.Alias
		};

		// Token: 0x04003C75 RID: 15477
		private ADObject targetDatabase;

		// Token: 0x04003C76 RID: 15478
		private ADObject sourceDatabase;
	}
}
