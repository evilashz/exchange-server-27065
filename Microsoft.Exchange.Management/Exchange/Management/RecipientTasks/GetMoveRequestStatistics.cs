using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C77 RID: 3191
	[Cmdlet("Get", "MoveRequestStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetMoveRequestStatistics : GetTaskBase<MoveRequestStatistics>
	{
		// Token: 0x060079C8 RID: 31176 RVA: 0x001F0202 File Offset: 0x001EE402
		public GetMoveRequestStatistics()
		{
			this.fromMdb = null;
			this.gcSession = null;
			this.configSession = null;
			this.mrProvider = null;
		}

		// Token: 0x170025B5 RID: 9653
		// (get) Token: 0x060079C9 RID: 31177 RVA: 0x001F0226 File Offset: 0x001EE426
		// (set) Token: 0x060079CA RID: 31178 RVA: 0x001F023D File Offset: 0x001EE43D
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public MoveRequestIdParameter Identity
		{
			get
			{
				return (MoveRequestIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170025B6 RID: 9654
		// (get) Token: 0x060079CB RID: 31179 RVA: 0x001F0250 File Offset: 0x001EE450
		// (set) Token: 0x060079CC RID: 31180 RVA: 0x001F0276 File Offset: 0x001EE476
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeReport
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeReport"] ?? false);
			}
			set
			{
				base.Fields["IncludeReport"] = value;
			}
		}

		// Token: 0x170025B7 RID: 9655
		// (get) Token: 0x060079CD RID: 31181 RVA: 0x001F028E File Offset: 0x001EE48E
		// (set) Token: 0x060079CE RID: 31182 RVA: 0x001F02A5 File Offset: 0x001EE4A5
		[Parameter(Mandatory = true, ParameterSetName = "MigrationMoveRequestQueue")]
		[ValidateNotNull]
		public DatabaseIdParameter MoveRequestQueue
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["MoveRequestQueue"];
			}
			set
			{
				base.Fields["MoveRequestQueue"] = value;
			}
		}

		// Token: 0x170025B8 RID: 9656
		// (get) Token: 0x060079CF RID: 31183 RVA: 0x001F02B8 File Offset: 0x001EE4B8
		// (set) Token: 0x060079D0 RID: 31184 RVA: 0x001F02DD File Offset: 0x001EE4DD
		[Parameter(Mandatory = false, ParameterSetName = "MigrationMoveRequestQueue")]
		public Guid MailboxGuid
		{
			get
			{
				return (Guid)(base.Fields["MailboxGuid"] ?? Guid.Empty);
			}
			set
			{
				base.Fields["MailboxGuid"] = value;
			}
		}

		// Token: 0x170025B9 RID: 9657
		// (get) Token: 0x060079D1 RID: 31185 RVA: 0x001F02F5 File Offset: 0x001EE4F5
		// (set) Token: 0x060079D2 RID: 31186 RVA: 0x001F02FD File Offset: 0x001EE4FD
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x170025BA RID: 9658
		// (get) Token: 0x060079D3 RID: 31187 RVA: 0x001F0306 File Offset: 0x001EE506
		// (set) Token: 0x060079D4 RID: 31188 RVA: 0x001F032C File Offset: 0x001EE52C
		[Parameter(Mandatory = false)]
		public SwitchParameter Diagnostic
		{
			get
			{
				return (SwitchParameter)(base.Fields["Diagnostic"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Diagnostic"] = value;
			}
		}

		// Token: 0x170025BB RID: 9659
		// (get) Token: 0x060079D5 RID: 31189 RVA: 0x001F0344 File Offset: 0x001EE544
		// (set) Token: 0x060079D6 RID: 31190 RVA: 0x001F035B File Offset: 0x001EE55B
		[ValidateLength(1, 1048576)]
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
		public string DiagnosticArgument
		{
			get
			{
				return (string)base.Fields["DiagnosticArgument"];
			}
			set
			{
				base.Fields["DiagnosticArgument"] = value;
			}
		}

		// Token: 0x170025BC RID: 9660
		// (get) Token: 0x060079D7 RID: 31191 RVA: 0x001F036E File Offset: 0x001EE56E
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (base.ParameterSetName.Equals("MigrationMoveRequestQueue"))
				{
					return new RequestJobQueryFilter(this.MailboxGuid, this.fromMdb.ObjectGuid, MRSRequestType.Move);
				}
				return null;
			}
		}

		// Token: 0x060079D8 RID: 31192 RVA: 0x001F039C File Offset: 0x001EE59C
		protected override IConfigDataProvider CreateSession()
		{
			ADObjectId rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerId(this.DomainController, null);
			ADSessionSettings adsessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, rootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			adsessionSettings = ADSessionSettings.RescopeToSubtree(adsessionSettings);
			if (MapiTaskHelper.IsDatacenter || MapiTaskHelper.IsDatacenterDedicated)
			{
				adsessionSettings.IncludeSoftDeletedObjects = true;
			}
			this.gcSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, adsessionSettings, 273, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\MoveRequest\\GetMoveRequestStatistics.cs");
			this.adSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, adsessionSettings, 280, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\MoveRequest\\GetMoveRequestStatistics.cs");
			this.configSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 286, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\MoveRequest\\GetMoveRequestStatistics.cs");
			if (this.mrProvider != null)
			{
				this.mrProvider.Dispose();
				this.mrProvider = null;
			}
			if (base.ParameterSetName.Equals("MigrationMoveRequestQueue"))
			{
				MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.MoveRequestQueue, this.configSession, null, new LocalizedString?(Strings.ErrorMailboxDatabaseNotFound(this.MoveRequestQueue.ToString())), new LocalizedString?(Strings.ErrorMailboxDatabaseNotUnique(this.MoveRequestQueue.ToString())));
				this.mrProvider = new RequestJobProvider(mailboxDatabase.Guid);
			}
			else
			{
				this.mrProvider = new RequestJobProvider(this.gcSession, this.configSession);
			}
			this.mrProvider.LoadReport = this.IncludeReport;
			return this.mrProvider;
		}

		// Token: 0x060079D9 RID: 31193 RVA: 0x001F0528 File Offset: 0x001EE728
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.mrProvider != null)
			{
				this.mrProvider.Dispose();
				this.mrProvider = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060079DA RID: 31194 RVA: 0x001F0550 File Offset: 0x001EE750
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				if (base.ParameterSetName.Equals("Identity"))
				{
					ADUser aduser = (ADUser)RecipientTaskHelper.ResolveDataObject<ADUser>(this.adSession, this.gcSession, base.ServerSettings, this.Identity, null, base.OptionalIdentityData, this.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
					if (aduser.MailboxMoveStatus == RequestStatus.None)
					{
						base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorUserNotBeingMoved(aduser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
					}
					else
					{
						MoveRequestStatistics moveRequestStatistics = (MoveRequestStatistics)this.mrProvider.Read<MoveRequestStatistics>(new RequestJobObjectId(aduser));
						if (moveRequestStatistics == null || moveRequestStatistics.Status == RequestStatus.None)
						{
							base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorUserNotBeingMoved(aduser.ToString())), ErrorCategory.InvalidArgument, this.Identity);
						}
						else
						{
							this.WriteResult(moveRequestStatistics);
						}
					}
				}
				else if (base.ParameterSetName.Equals("MigrationMoveRequestQueue"))
				{
					if (this.MoveRequestQueue != null)
					{
						MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.MoveRequestQueue, this.configSession, null, new LocalizedString?(Strings.ErrorMailboxDatabaseNotFound(this.MoveRequestQueue.ToString())), new LocalizedString?(Strings.ErrorMailboxDatabaseNotUnique(this.MoveRequestQueue.ToString())));
						this.fromMdb = mailboxDatabase.Id;
					}
					this.mrProvider.AllowInvalid = true;
					base.InternalProcessRecord();
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060079DB RID: 31195 RVA: 0x001F06E8 File Offset: 0x001EE8E8
		protected override bool IsKnownException(Exception exception)
		{
			return SetMoveRequestBase.IsKnownExceptionHandler(exception, new WriteVerboseDelegate(base.WriteVerbose)) || base.IsKnownException(exception);
		}

		// Token: 0x060079DC RID: 31196 RVA: 0x001F0708 File Offset: 0x001EE908
		protected override void TranslateException(ref Exception e, out ErrorCategory category)
		{
			LocalizedException ex = SetMoveRequestBase.TranslateExceptionHandler(e);
			if (ex == null)
			{
				ErrorCategory errorCategory;
				base.TranslateException(ref e, out errorCategory);
				category = errorCategory;
				return;
			}
			e = ex;
			category = ErrorCategory.ResourceUnavailable;
		}

		// Token: 0x060079DD RID: 31197 RVA: 0x001F0734 File Offset: 0x001EE934
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject
			});
			MoveRequestStatistics moveRequestStatistics = (MoveRequestStatistics)dataObject;
			try
			{
				RequestTaskHelper.GetUpdatedMRSRequestInfo(moveRequestStatistics, this.Diagnostic, this.DiagnosticArgument);
				if (moveRequestStatistics.Status == RequestStatus.Queued)
				{
					moveRequestStatistics.PositionInQueue = this.mrProvider.ComputePositionInQueue(moveRequestStatistics.ExchangeGuid);
				}
				base.WriteResult(moveRequestStatistics);
				string identity;
				if (moveRequestStatistics.UserId != null)
				{
					identity = moveRequestStatistics.UserId.ToString();
				}
				else if (moveRequestStatistics.Identity != null)
				{
					identity = moveRequestStatistics.Identity.ToString();
				}
				else
				{
					identity = moveRequestStatistics.ExchangeGuid.ToString();
				}
				if (moveRequestStatistics.ValidationResult != RequestJobBase.ValidationResultEnum.Valid)
				{
					this.WriteWarning(Strings.ErrorInvalidMoveRequest(identity, moveRequestStatistics.ValidationMessage));
				}
				if (moveRequestStatistics.PoisonCount > 5)
				{
					this.WriteWarning(Strings.WarningJobIsPoisoned(identity, moveRequestStatistics.PoisonCount));
				}
				if (base.ParameterSetName.Equals("MigrationMoveRequestQueue"))
				{
					base.WriteVerbose(Strings.RawRequestJobDump(CommonUtils.ConfigurableObjectToString(moveRequestStatistics)));
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x04003C77 RID: 15479
		public const string ParameterIncludeReport = "IncludeReport";

		// Token: 0x04003C78 RID: 15480
		public const string ParameterMoveRequestQueue = "MoveRequestQueue";

		// Token: 0x04003C79 RID: 15481
		public const string ParameterMailboxGuid = "MailboxGuid";

		// Token: 0x04003C7A RID: 15482
		public const string ParameterIdentity = "Identity";

		// Token: 0x04003C7B RID: 15483
		public const string MoveRequestQueueSet = "MigrationMoveRequestQueue";

		// Token: 0x04003C7C RID: 15484
		public const string ParameterDiagnostic = "Diagnostic";

		// Token: 0x04003C7D RID: 15485
		public const string ParameterDiagnosticArgument = "DiagnosticArgument";

		// Token: 0x04003C7E RID: 15486
		private ADObjectId fromMdb;

		// Token: 0x04003C7F RID: 15487
		private IRecipientSession adSession;

		// Token: 0x04003C80 RID: 15488
		private IRecipientSession gcSession;

		// Token: 0x04003C81 RID: 15489
		private ITopologyConfigurationSession configSession;

		// Token: 0x04003C82 RID: 15490
		private RequestJobProvider mrProvider;
	}
}
