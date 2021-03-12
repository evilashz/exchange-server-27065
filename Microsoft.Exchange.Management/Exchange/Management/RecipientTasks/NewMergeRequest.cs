using System;
using System.Globalization;
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
	// Token: 0x02000C86 RID: 3206
	[Cmdlet("New", "MergeRequest", SupportsShouldProcess = true, DefaultParameterSetName = "MigrationLocalMerge")]
	public sealed class NewMergeRequest : NewRequest<MergeRequest>
	{
		// Token: 0x17002621 RID: 9761
		// (get) Token: 0x06007B29 RID: 31529 RVA: 0x001F9504 File Offset: 0x001F7704
		// (set) Token: 0x06007B2A RID: 31530 RVA: 0x001F951B File Offset: 0x001F771B
		[Parameter(Mandatory = true, ParameterSetName = "MigrationLocalMerge")]
		[ValidateNotNull]
		public MailboxOrMailUserIdParameter SourceMailbox
		{
			get
			{
				return (MailboxOrMailUserIdParameter)base.Fields["SourceMailbox"];
			}
			set
			{
				base.Fields["SourceMailbox"] = value;
			}
		}

		// Token: 0x17002622 RID: 9762
		// (get) Token: 0x06007B2B RID: 31531 RVA: 0x001F952E File Offset: 0x001F772E
		// (set) Token: 0x06007B2C RID: 31532 RVA: 0x001F9545 File Offset: 0x001F7745
		[Parameter(Mandatory = true, ParameterSetName = "MigrationOutlookAnywhereMergePull")]
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "MigrationLocalMerge")]
		public MailboxOrMailUserIdParameter TargetMailbox
		{
			get
			{
				return (MailboxOrMailUserIdParameter)base.Fields["TargetMailbox"];
			}
			set
			{
				base.Fields["TargetMailbox"] = value;
			}
		}

		// Token: 0x17002623 RID: 9763
		// (get) Token: 0x06007B2D RID: 31533 RVA: 0x001F9558 File Offset: 0x001F7758
		// (set) Token: 0x06007B2E RID: 31534 RVA: 0x001F956F File Offset: 0x001F776F
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMerge")]
		public string SourceRootFolder
		{
			get
			{
				return (string)base.Fields["SourceRootFolder"];
			}
			set
			{
				base.Fields["SourceRootFolder"] = value;
			}
		}

		// Token: 0x17002624 RID: 9764
		// (get) Token: 0x06007B2F RID: 31535 RVA: 0x001F9582 File Offset: 0x001F7782
		// (set) Token: 0x06007B30 RID: 31536 RVA: 0x001F9599 File Offset: 0x001F7799
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMerge")]
		public string TargetRootFolder
		{
			get
			{
				return (string)base.Fields["TargetRootFolder"];
			}
			set
			{
				base.Fields["TargetRootFolder"] = value;
			}
		}

		// Token: 0x17002625 RID: 9765
		// (get) Token: 0x06007B31 RID: 31537 RVA: 0x001F95AC File Offset: 0x001F77AC
		// (set) Token: 0x06007B32 RID: 31538 RVA: 0x001F95D2 File Offset: 0x001F77D2
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMerge")]
		public SwitchParameter SourceIsArchive
		{
			get
			{
				return (SwitchParameter)(base.Fields["SourceIsArchive"] ?? false);
			}
			set
			{
				base.Fields["SourceIsArchive"] = value;
			}
		}

		// Token: 0x17002626 RID: 9766
		// (get) Token: 0x06007B33 RID: 31539 RVA: 0x001F95EA File Offset: 0x001F77EA
		// (set) Token: 0x06007B34 RID: 31540 RVA: 0x001F9610 File Offset: 0x001F7810
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutlookAnywhereMergePull")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMerge")]
		public SwitchParameter TargetIsArchive
		{
			get
			{
				return (SwitchParameter)(base.Fields["TargetIsArchive"] ?? false);
			}
			set
			{
				base.Fields["TargetIsArchive"] = value;
			}
		}

		// Token: 0x17002627 RID: 9767
		// (get) Token: 0x06007B35 RID: 31541 RVA: 0x001F9628 File Offset: 0x001F7828
		// (set) Token: 0x06007B36 RID: 31542 RVA: 0x001F963F File Offset: 0x001F783F
		[Parameter(Mandatory = true, ParameterSetName = "MigrationOutlookAnywhereMergePull")]
		public string RemoteSourceMailboxLegacyDN
		{
			get
			{
				return (string)base.Fields["RemoteSourceMailboxLegacyDN"];
			}
			set
			{
				base.Fields["RemoteSourceMailboxLegacyDN"] = value;
			}
		}

		// Token: 0x17002628 RID: 9768
		// (get) Token: 0x06007B37 RID: 31543 RVA: 0x001F9652 File Offset: 0x001F7852
		// (set) Token: 0x06007B38 RID: 31544 RVA: 0x001F9669 File Offset: 0x001F7869
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutlookAnywhereMergePull")]
		public string RemoteSourceUserLegacyDN
		{
			get
			{
				return (string)base.Fields["RemoteSourceUserLegacyDN"];
			}
			set
			{
				base.Fields["RemoteSourceUserLegacyDN"] = value;
			}
		}

		// Token: 0x17002629 RID: 9769
		// (get) Token: 0x06007B39 RID: 31545 RVA: 0x001F967C File Offset: 0x001F787C
		// (set) Token: 0x06007B3A RID: 31546 RVA: 0x001F9693 File Offset: 0x001F7893
		[Parameter(Mandatory = true, ParameterSetName = "MigrationOutlookAnywhereMergePull")]
		public string RemoteSourceMailboxServerLegacyDN
		{
			get
			{
				return (string)base.Fields["RemoteSourceMailboxServerLegacyDN"];
			}
			set
			{
				base.Fields["RemoteSourceMailboxServerLegacyDN"] = value;
			}
		}

		// Token: 0x1700262A RID: 9770
		// (get) Token: 0x06007B3B RID: 31547 RVA: 0x001F96A6 File Offset: 0x001F78A6
		// (set) Token: 0x06007B3C RID: 31548 RVA: 0x001F96BD File Offset: 0x001F78BD
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutlookAnywhereMergePull")]
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

		// Token: 0x1700262B RID: 9771
		// (get) Token: 0x06007B3D RID: 31549 RVA: 0x001F96D0 File Offset: 0x001F78D0
		// (set) Token: 0x06007B3E RID: 31550 RVA: 0x001F96F1 File Offset: 0x001F78F1
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutlookAnywhereMergePull")]
		public bool IsAdministrativeCredential
		{
			get
			{
				return (bool)(base.Fields["IsAdministrativeCredential"] ?? false);
			}
			set
			{
				base.Fields["IsAdministrativeCredential"] = value;
			}
		}

		// Token: 0x1700262C RID: 9772
		// (get) Token: 0x06007B3F RID: 31551 RVA: 0x001F9709 File Offset: 0x001F7909
		// (set) Token: 0x06007B40 RID: 31552 RVA: 0x001F972A File Offset: 0x001F792A
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutlookAnywhereMergePull")]
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

		// Token: 0x1700262D RID: 9773
		// (get) Token: 0x06007B41 RID: 31553 RVA: 0x001F9742 File Offset: 0x001F7942
		// (set) Token: 0x06007B42 RID: 31554 RVA: 0x001F9763 File Offset: 0x001F7963
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMerge")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutlookAnywhereMergePull")]
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

		// Token: 0x1700262E RID: 9774
		// (get) Token: 0x06007B43 RID: 31555 RVA: 0x001F977B File Offset: 0x001F797B
		// (set) Token: 0x06007B44 RID: 31556 RVA: 0x001F97A0 File Offset: 0x001F79A0
		[Parameter(Mandatory = false)]
		public TimeSpan IncrementalSyncInterval
		{
			get
			{
				return (TimeSpan)(base.Fields["IncrementalSyncInterval"] ?? TimeSpan.Zero);
			}
			set
			{
				base.Fields["IncrementalSyncInterval"] = value;
			}
		}

		// Token: 0x1700262F RID: 9775
		// (get) Token: 0x06007B45 RID: 31557 RVA: 0x001F97B8 File Offset: 0x001F79B8
		// (set) Token: 0x06007B46 RID: 31558 RVA: 0x001F97C0 File Offset: 0x001F79C0
		[Parameter(Mandatory = true, ParameterSetName = "MigrationOutlookAnywhereMergePull")]
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

		// Token: 0x17002630 RID: 9776
		// (get) Token: 0x06007B47 RID: 31559 RVA: 0x001F97C9 File Offset: 0x001F79C9
		// (set) Token: 0x06007B48 RID: 31560 RVA: 0x001F97D1 File Offset: 0x001F79D1
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMerge")]
		public new SwitchParameter AllowLegacyDNMismatch
		{
			get
			{
				return base.AllowLegacyDNMismatch;
			}
			set
			{
				base.AllowLegacyDNMismatch = value;
			}
		}

		// Token: 0x17002631 RID: 9777
		// (get) Token: 0x06007B49 RID: 31561 RVA: 0x001F97DA File Offset: 0x001F79DA
		// (set) Token: 0x06007B4A RID: 31562 RVA: 0x001F97E2 File Offset: 0x001F79E2
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMerge")]
		public new string ContentFilter
		{
			get
			{
				return base.ContentFilter;
			}
			set
			{
				base.ContentFilter = value;
			}
		}

		// Token: 0x17002632 RID: 9778
		// (get) Token: 0x06007B4B RID: 31563 RVA: 0x001F97EB File Offset: 0x001F79EB
		// (set) Token: 0x06007B4C RID: 31564 RVA: 0x001F97F3 File Offset: 0x001F79F3
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMerge")]
		public new CultureInfo ContentFilterLanguage
		{
			get
			{
				return base.ContentFilterLanguage;
			}
			set
			{
				base.ContentFilterLanguage = value;
			}
		}

		// Token: 0x17002633 RID: 9779
		// (get) Token: 0x06007B4D RID: 31565 RVA: 0x001F97FC File Offset: 0x001F79FC
		// (set) Token: 0x06007B4E RID: 31566 RVA: 0x001F9804 File Offset: 0x001F7A04
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMerge")]
		public new string[] IncludeFolders
		{
			get
			{
				return base.IncludeFolders;
			}
			set
			{
				base.IncludeFolders = value;
			}
		}

		// Token: 0x17002634 RID: 9780
		// (get) Token: 0x06007B4F RID: 31567 RVA: 0x001F980D File Offset: 0x001F7A0D
		// (set) Token: 0x06007B50 RID: 31568 RVA: 0x001F9815 File Offset: 0x001F7A15
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMerge")]
		public new string[] ExcludeFolders
		{
			get
			{
				return base.ExcludeFolders;
			}
			set
			{
				base.ExcludeFolders = value;
			}
		}

		// Token: 0x17002635 RID: 9781
		// (get) Token: 0x06007B51 RID: 31569 RVA: 0x001F981E File Offset: 0x001F7A1E
		// (set) Token: 0x06007B52 RID: 31570 RVA: 0x001F9826 File Offset: 0x001F7A26
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMerge")]
		public new SwitchParameter ExcludeDumpster
		{
			get
			{
				return base.ExcludeDumpster;
			}
			set
			{
				base.ExcludeDumpster = value;
			}
		}

		// Token: 0x17002636 RID: 9782
		// (get) Token: 0x06007B53 RID: 31571 RVA: 0x001F982F File Offset: 0x001F7A2F
		// (set) Token: 0x06007B54 RID: 31572 RVA: 0x001F9837 File Offset: 0x001F7A37
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMerge")]
		public new ConflictResolutionOption ConflictResolutionOption
		{
			get
			{
				return base.ConflictResolutionOption;
			}
			set
			{
				base.ConflictResolutionOption = value;
			}
		}

		// Token: 0x17002637 RID: 9783
		// (get) Token: 0x06007B55 RID: 31573 RVA: 0x001F9840 File Offset: 0x001F7A40
		// (set) Token: 0x06007B56 RID: 31574 RVA: 0x001F9848 File Offset: 0x001F7A48
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMerge")]
		public new FAICopyOption AssociatedMessagesCopyOption
		{
			get
			{
				return base.AssociatedMessagesCopyOption;
			}
			set
			{
				base.AssociatedMessagesCopyOption = value;
			}
		}

		// Token: 0x17002638 RID: 9784
		// (get) Token: 0x06007B57 RID: 31575 RVA: 0x001F9851 File Offset: 0x001F7A51
		// (set) Token: 0x06007B58 RID: 31576 RVA: 0x001F9859 File Offset: 0x001F7A59
		[Parameter(Mandatory = false, ParameterSetName = "MigrationLocalMerge")]
		[Parameter(Mandatory = false, ParameterSetName = "MigrationOutlookAnywhereMergePull")]
		public new DateTime StartAfter
		{
			get
			{
				return base.StartAfter;
			}
			set
			{
				base.StartAfter = value;
			}
		}

		// Token: 0x17002639 RID: 9785
		// (get) Token: 0x06007B59 RID: 31577 RVA: 0x001F9862 File Offset: 0x001F7A62
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMergeRequest(base.RequestName);
			}
		}

		// Token: 0x06007B5A RID: 31578 RVA: 0x001F986F File Offset: 0x001F7A6F
		protected override void InternalStateReset()
		{
			this.sourceUser = null;
			this.targetUser = null;
			base.InternalStateReset();
		}

		// Token: 0x06007B5B RID: 31579 RVA: 0x001F9885 File Offset: 0x001F7A85
		protected override void InternalBeginProcessing()
		{
			if (base.WorkloadType == RequestWorkloadType.None)
			{
				if (base.ParameterSetName.Equals("MigrationLocalMerge"))
				{
					base.WorkloadType = RequestWorkloadType.Local;
				}
				else
				{
					base.WorkloadType = RequestWorkloadType.Onboarding;
				}
			}
			base.InternalBeginProcessing();
		}

		// Token: 0x06007B5C RID: 31580 RVA: 0x001F98B8 File Offset: 0x001F7AB8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.ValidateRootFolders(this.SourceRootFolder, this.TargetRootFolder);
				if (this.SourceMailbox != null)
				{
					this.sourceUser = RequestTaskHelper.ResolveADUser(base.RecipSession, base.GCSession, base.ServerSettings, this.SourceMailbox, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), true);
					if (this.SourceIsArchive && (this.sourceUser.ArchiveGuid == Guid.Empty || this.sourceUser.ArchiveDatabase == null))
					{
						base.WriteError(new MailboxLacksArchivePermanentException(this.sourceUser.ToString()), ErrorCategory.InvalidArgument, this.SourceIsArchive);
					}
					if (!this.SourceIsArchive && this.sourceUser.Database == null)
					{
						base.WriteError(new MailboxLacksDatabasePermanentException(this.sourceUser.ToString()), ErrorCategory.InvalidArgument, this.SourceMailbox);
					}
				}
				else
				{
					this.sourceUser = null;
				}
				if (this.TargetMailbox != null)
				{
					this.targetUser = RequestTaskHelper.ResolveADUser(base.RecipSession, base.GCSession, base.ServerSettings, this.TargetMailbox, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), true);
					if (this.TargetIsArchive && (this.targetUser.ArchiveGuid == Guid.Empty || this.targetUser.ArchiveDatabase == null))
					{
						base.WriteError(new MailboxLacksArchivePermanentException(this.targetUser.ToString()), ErrorCategory.InvalidArgument, this.TargetIsArchive);
					}
					if (!this.TargetIsArchive && this.targetUser.Database == null)
					{
						base.WriteError(new MailboxLacksDatabasePermanentException(this.targetUser.ToString()), ErrorCategory.InvalidArgument, this.TargetMailbox);
					}
				}
				else
				{
					this.targetUser = null;
				}
				this.DisallowMergeRequestForPublicFolderMailbox();
				bool wildcardedSearch = false;
				if (!string.IsNullOrEmpty(base.Name))
				{
					base.ValidateName();
					base.RequestName = base.Name;
				}
				else
				{
					wildcardedSearch = true;
					base.RequestName = "Merge";
				}
				AuthenticationMethod authenticationMethod = this.AuthenticationMethod;
				switch (authenticationMethod)
				{
				case AuthenticationMethod.Basic:
				case AuthenticationMethod.Ntlm:
					goto IL_282;
				case AuthenticationMethod.Digest:
					break;
				default:
					if (authenticationMethod == AuthenticationMethod.LiveIdBasic)
					{
						goto IL_282;
					}
					break;
				}
				base.WriteError(new UnsupportedAuthMethodPermanentException(this.AuthenticationMethod.ToString()), ErrorCategory.InvalidArgument, this.AuthenticationMethod);
				IL_282:
				if (base.ParameterSetName.Equals("MigrationLocalMerge"))
				{
					if (!object.Equals(this.sourceUser.OrganizationId, this.targetUser.OrganizationId))
					{
						base.WriteError(new UsersNotInSameOrganizationPermanentException(this.sourceUser.ToString(), this.targetUser.ToString()), ErrorCategory.InvalidArgument, this.TargetMailbox);
					}
					base.RescopeToOrgId(this.sourceUser.OrganizationId);
					base.ValidateLegacyDNMatch(this.sourceUser.LegacyExchangeDN, this.targetUser, this.TargetMailbox);
					ADObjectId mdbId = null;
					ADObjectId mdbServerSite = null;
					RequestFlags requestFlags = this.LocateAndChooseMdb(this.SourceIsArchive ? this.sourceUser.ArchiveDatabase : this.sourceUser.Database, this.TargetIsArchive ? this.targetUser.ArchiveDatabase : this.targetUser.Database, this.SourceMailbox, this.TargetMailbox, this.TargetMailbox, out mdbId, out mdbServerSite);
					base.MdbId = mdbId;
					base.MdbServerSite = mdbServerSite;
					base.Flags = (RequestFlags.IntraOrg | requestFlags);
				}
				else
				{
					base.RescopeToOrgId(this.targetUser.OrganizationId);
					ADObjectId mdbId2 = null;
					ADObjectId mdbServerSite2 = null;
					this.LocateAndChooseMdb(null, this.TargetIsArchive ? this.targetUser.ArchiveDatabase : this.targetUser.Database, null, this.TargetMailbox, this.TargetMailbox, out mdbId2, out mdbServerSite2);
					base.MdbId = mdbId2;
					base.MdbServerSite = mdbServerSite2;
					base.Flags = (RequestFlags.CrossOrg | RequestFlags.Pull);
				}
				ADUser aduser;
				if ((base.Flags & RequestFlags.Pull) == RequestFlags.Pull)
				{
					aduser = this.targetUser;
				}
				else
				{
					aduser = this.sourceUser;
				}
				base.RequestName = this.CheckRequestNameAvailability(base.RequestName, aduser.Id, true, MRSRequestType.Merge, this.TargetMailbox, wildcardedSearch);
				if (base.IsFieldSet("IncrementalSyncInterval") && base.IsFieldSet("SuspendWhenReadyToComplete") && this.SuspendWhenReadyToComplete)
				{
					base.WriteError(new SuspendWhenReadyToCompleteCannotBeSetWithIncrementalSyncIntervalException(), ErrorCategory.InvalidArgument, this.SuspendWhenReadyToComplete);
				}
				DateTime utcNow = DateTime.UtcNow;
				if (base.IsFieldSet("StartAfter"))
				{
					RequestTaskHelper.ValidateStartAfterTime(this.StartAfter.ToUniversalTime(), new Task.TaskErrorLoggingDelegate(base.WriteError), utcNow);
				}
				if (base.IsFieldSet("IncrementalSyncInterval"))
				{
					RequestTaskHelper.ValidateIncrementalSyncInterval(this.IncrementalSyncInterval, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
				base.InternalValidate();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007B5D RID: 31581 RVA: 0x001F9DC0 File Offset: 0x001F7FC0
		protected override void SetRequestProperties(TransactionalRequestJob dataObject)
		{
			base.SetRequestProperties(dataObject);
			dataObject.RequestType = MRSRequestType.Merge;
			if (this.sourceUser != null)
			{
				dataObject.SourceUserId = this.sourceUser.Id;
				dataObject.SourceUser = this.sourceUser;
				dataObject.SourceIsArchive = this.SourceIsArchive;
				if (this.SourceIsArchive)
				{
					dataObject.SourceExchangeGuid = this.sourceUser.ArchiveGuid;
					dataObject.SourceDatabase = ADObjectIdResolutionHelper.ResolveDN(this.sourceUser.ArchiveDatabase);
				}
				else
				{
					dataObject.SourceExchangeGuid = this.sourceUser.ExchangeGuid;
					dataObject.SourceDatabase = ADObjectIdResolutionHelper.ResolveDN(this.sourceUser.Database);
				}
				dataObject.SourceAlias = this.sourceUser.Alias;
			}
			if (this.targetUser != null)
			{
				dataObject.TargetUserId = this.targetUser.Id;
				dataObject.TargetUser = this.targetUser;
				dataObject.TargetIsArchive = this.TargetIsArchive;
				if (this.TargetIsArchive)
				{
					dataObject.TargetExchangeGuid = this.targetUser.ArchiveGuid;
					dataObject.TargetDatabase = ADObjectIdResolutionHelper.ResolveDN(this.targetUser.ArchiveDatabase);
				}
				else
				{
					dataObject.TargetExchangeGuid = this.targetUser.ExchangeGuid;
					dataObject.TargetDatabase = ADObjectIdResolutionHelper.ResolveDN(this.targetUser.Database);
				}
				dataObject.TargetAlias = this.targetUser.Alias;
			}
			if (base.ParameterSetName.Equals("MigrationLocalMerge"))
			{
				if (!string.IsNullOrEmpty(this.SourceRootFolder))
				{
					dataObject.SourceRootFolder = this.SourceRootFolder;
				}
				if (!string.IsNullOrEmpty(this.TargetRootFolder))
				{
					dataObject.TargetRootFolder = this.TargetRootFolder;
				}
			}
			else
			{
				dataObject.IsAdministrativeCredential = new bool?(this.IsAdministrativeCredential);
				dataObject.AuthenticationMethod = new AuthenticationMethod?(this.AuthenticationMethod);
				dataObject.RemoteMailboxLegacyDN = this.RemoteSourceMailboxLegacyDN;
				dataObject.RemoteUserLegacyDN = this.RemoteSourceUserLegacyDN;
				dataObject.RemoteMailboxServerLegacyDN = this.RemoteSourceMailboxServerLegacyDN;
				dataObject.OutlookAnywhereHostName = this.OutlookAnywhereHostName;
				dataObject.RemoteCredential = RequestTaskHelper.GetNetworkCredential(this.RemoteCredential, new AuthenticationMethod?(this.AuthenticationMethod));
				dataObject.IncludeFolders = this.IncludeListForIncrementalMerge;
				dataObject.ExcludeDumpster = false;
				dataObject.ExcludeFolders = null;
				dataObject.ContentFilter = null;
				dataObject.ConflictResolutionOption = new ConflictResolutionOption?(ConflictResolutionOption.KeepSourceItem);
				dataObject.AssociatedMessagesCopyOption = new FAICopyOption?(FAICopyOption.Copy);
			}
			dataObject.SuspendWhenReadyToComplete = this.SuspendWhenReadyToComplete;
			if (base.IsFieldSet("IncrementalSyncInterval"))
			{
				dataObject.IncrementalSyncInterval = this.IncrementalSyncInterval;
				dataObject.TimeTracker.SetTimestamp(RequestJobTimestamp.CompleteAfter, new DateTime?(DateTime.MaxValue));
				dataObject.JobType = MRSJobType.RequestJobE15_AutoResumeMerges;
			}
			if (this.SuspendWhenReadyToComplete || base.IsFieldSet("IncrementalSyncInterval"))
			{
				dataObject.IncludeFolders = this.IncludeListForIncrementalMerge;
				dataObject.ExcludeDumpster = false;
				dataObject.ExcludeFolders = null;
				dataObject.ContentFilter = null;
				dataObject.ConflictResolutionOption = new ConflictResolutionOption?(ConflictResolutionOption.KeepSourceItem);
				dataObject.AssociatedMessagesCopyOption = new FAICopyOption?(FAICopyOption.Copy);
				dataObject.AllowedToFinishMove = false;
			}
			if (base.IsFieldSet("StartAfter"))
			{
				RequestTaskHelper.SetStartAfter(new DateTime?(this.StartAfter), dataObject, null);
			}
		}

		// Token: 0x06007B5E RID: 31582 RVA: 0x001FA0DC File Offset: 0x001F82DC
		protected override MergeRequest ConvertToPresentationObject(TransactionalRequestJob dataObject)
		{
			if (dataObject.IndexEntries != null && dataObject.IndexEntries.Count >= 1)
			{
				return new MergeRequest(dataObject.IndexEntries[0]);
			}
			base.WriteError(new RequestIndexEntriesAbsentPermanentException(base.RequestName), ErrorCategory.InvalidArgument, this.TargetMailbox);
			return null;
		}

		// Token: 0x06007B5F RID: 31583 RVA: 0x001FA12C File Offset: 0x001F832C
		private void DisallowMergeRequestForPublicFolderMailbox()
		{
			if (this.sourceUser != null && this.sourceUser.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorDisallowMergeRequestForPublicFolderMailbox(this.sourceUser.Name)), ErrorCategory.InvalidArgument, this.SourceMailbox);
			}
			if (this.targetUser != null && this.targetUser.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorDisallowMergeRequestForPublicFolderMailbox(this.targetUser.Name)), ErrorCategory.InvalidArgument, this.TargetMailbox);
			}
		}

		// Token: 0x04003D30 RID: 15664
		public const string DefaultMergeName = "Merge";

		// Token: 0x04003D31 RID: 15665
		public const string TaskNoun = "MergeRequest";

		// Token: 0x04003D32 RID: 15666
		public const string ParameterSourceMailbox = "SourceMailbox";

		// Token: 0x04003D33 RID: 15667
		public const string ParameterTargetMailbox = "TargetMailbox";

		// Token: 0x04003D34 RID: 15668
		public const string ParameterSourceRootFolder = "SourceRootFolder";

		// Token: 0x04003D35 RID: 15669
		public const string ParameterTargetRootFolder = "TargetRootFolder";

		// Token: 0x04003D36 RID: 15670
		public const string ParameterSourceIsArchive = "SourceIsArchive";

		// Token: 0x04003D37 RID: 15671
		public const string ParameterTargetIsArchive = "TargetIsArchive";

		// Token: 0x04003D38 RID: 15672
		public const string ParameterRemoteSourceMailboxLegacyDN = "RemoteSourceMailboxLegacyDN";

		// Token: 0x04003D39 RID: 15673
		public const string ParameterRemoteSourceUserLegacyDN = "RemoteSourceUserLegacyDN";

		// Token: 0x04003D3A RID: 15674
		public const string ParameterRemoteSourceMailboxServerLegacyDN = "RemoteSourceMailboxServerLegacyDN";

		// Token: 0x04003D3B RID: 15675
		public const string ParameterOutlookAnywhereHostName = "OutlookAnywhereHostName";

		// Token: 0x04003D3C RID: 15676
		public const string ParameterIsAdministrativeCredential = "IsAdministrativeCredential";

		// Token: 0x04003D3D RID: 15677
		public const string ParameterAuthenticationMethod = "AuthenticationMethod";

		// Token: 0x04003D3E RID: 15678
		public const string ParameterSuspendWhenReadyToComplete = "SuspendWhenReadyToComplete";

		// Token: 0x04003D3F RID: 15679
		public const string ParameterIncrementalSyncInterval = "IncrementalSyncInterval";

		// Token: 0x04003D40 RID: 15680
		public const string ParameterSetLocalMerge = "MigrationLocalMerge";

		// Token: 0x04003D41 RID: 15681
		public const string ParameterSetOutlookAnywhereMergePull = "MigrationOutlookAnywhereMergePull";

		// Token: 0x04003D42 RID: 15682
		private readonly string[] IncludeListForIncrementalMerge = new string[]
		{
			FolderFilterParser.GetAlias(WellKnownFolderType.Root) + "/*"
		};

		// Token: 0x04003D43 RID: 15683
		private ADUser sourceUser;

		// Token: 0x04003D44 RID: 15684
		private ADUser targetUser;
	}
}
