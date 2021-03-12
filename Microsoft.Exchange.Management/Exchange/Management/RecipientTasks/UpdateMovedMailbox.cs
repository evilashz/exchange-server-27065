using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Management.Tasks.UM;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CC5 RID: 3269
	[Cmdlet("Update", "MovedMailbox", DefaultParameterSetName = "UpdateMailbox", ConfirmImpact = ConfirmImpact.None, SupportsShouldProcess = false)]
	public sealed class UpdateMovedMailbox : RecipientObjectActionTask<MailboxOrMailUserIdParameter, ADUser>
	{
		// Token: 0x06007DBE RID: 32190 RVA: 0x00200F96 File Offset: 0x001FF196
		public UpdateMovedMailbox()
		{
			TestIntegration.Instance.ForceRefresh();
		}

		// Token: 0x17002720 RID: 10016
		// (get) Token: 0x06007DBF RID: 32191 RVA: 0x00200FA8 File Offset: 0x001FF1A8
		// (set) Token: 0x06007DC0 RID: 32192 RVA: 0x00200FBF File Offset: 0x001FF1BF
		[Parameter(Mandatory = true)]
		public override MailboxOrMailUserIdParameter Identity
		{
			get
			{
				return (MailboxOrMailUserIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17002721 RID: 10017
		// (get) Token: 0x06007DC1 RID: 32193 RVA: 0x00200FD2 File Offset: 0x001FF1D2
		// (set) Token: 0x06007DC2 RID: 32194 RVA: 0x00200FE9 File Offset: 0x001FF1E9
		[Parameter(Mandatory = false)]
		public byte[] PartitionHint
		{
			get
			{
				if (this.partitionHint != null)
				{
					return this.partitionHint.GetPersistablePartitionHint();
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.partitionHint = TenantPartitionHint.FromPersistablePartitionHint(value);
					return;
				}
				this.partitionHint = null;
			}
		}

		// Token: 0x17002722 RID: 10018
		// (get) Token: 0x06007DC3 RID: 32195 RVA: 0x00201002 File Offset: 0x001FF202
		// (set) Token: 0x06007DC4 RID: 32196 RVA: 0x00201027 File Offset: 0x001FF227
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMailbox")]
		[Parameter(Mandatory = true, ParameterSetName = "MorphToMailbox")]
		public Guid NewHomeMDB
		{
			get
			{
				return (Guid)(base.Fields["NewHomeMDB"] ?? Guid.Empty);
			}
			set
			{
				base.Fields["NewHomeMDB"] = value;
			}
		}

		// Token: 0x17002723 RID: 10019
		// (get) Token: 0x06007DC5 RID: 32197 RVA: 0x0020103F File Offset: 0x001FF23F
		// (set) Token: 0x06007DC6 RID: 32198 RVA: 0x00201056 File Offset: 0x001FF256
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMailbox")]
		public Guid? NewContainerGuid
		{
			get
			{
				return (Guid?)base.Fields["NewContainerGuid"];
			}
			set
			{
				base.Fields["NewContainerGuid"] = value;
			}
		}

		// Token: 0x17002724 RID: 10020
		// (get) Token: 0x06007DC7 RID: 32199 RVA: 0x0020106E File Offset: 0x001FF26E
		// (set) Token: 0x06007DC8 RID: 32200 RVA: 0x00201085 File Offset: 0x001FF285
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMailbox")]
		public CrossTenantObjectId NewUnifiedMailboxId
		{
			get
			{
				return (CrossTenantObjectId)base.Fields["NewUnifiedMailboxId"];
			}
			set
			{
				base.Fields["NewUnifiedMailboxId"] = value;
			}
		}

		// Token: 0x17002725 RID: 10021
		// (get) Token: 0x06007DC9 RID: 32201 RVA: 0x00201098 File Offset: 0x001FF298
		// (set) Token: 0x06007DCA RID: 32202 RVA: 0x002010BD File Offset: 0x001FF2BD
		[Parameter(Mandatory = false)]
		public Guid NewArchiveMDB
		{
			get
			{
				return (Guid)(base.Fields["NewArchiveMDB"] ?? Guid.Empty);
			}
			set
			{
				base.Fields["NewArchiveMDB"] = value;
			}
		}

		// Token: 0x17002726 RID: 10022
		// (get) Token: 0x06007DCB RID: 32203 RVA: 0x002010D5 File Offset: 0x001FF2D5
		// (set) Token: 0x06007DCC RID: 32204 RVA: 0x002010EC File Offset: 0x001FF2EC
		[Parameter(Mandatory = false)]
		public string ArchiveDomain
		{
			get
			{
				return (string)base.Fields["ArchiveDomain"];
			}
			set
			{
				base.Fields["ArchiveDomain"] = value;
			}
		}

		// Token: 0x17002727 RID: 10023
		// (get) Token: 0x06007DCD RID: 32205 RVA: 0x002010FF File Offset: 0x001FF2FF
		// (set) Token: 0x06007DCE RID: 32206 RVA: 0x00201120 File Offset: 0x001FF320
		[Parameter(Mandatory = false)]
		public ArchiveStatusFlags ArchiveStatus
		{
			get
			{
				return (ArchiveStatusFlags)(base.Fields["ArchiveStatus"] ?? ArchiveStatusFlags.None);
			}
			set
			{
				base.Fields["ArchiveStatus"] = value;
			}
		}

		// Token: 0x17002728 RID: 10024
		// (get) Token: 0x06007DCF RID: 32207 RVA: 0x00201138 File Offset: 0x001FF338
		// (set) Token: 0x06007DD0 RID: 32208 RVA: 0x0020115E File Offset: 0x001FF35E
		[Parameter(Mandatory = true, ParameterSetName = "MorphToMailUser")]
		public SwitchParameter MorphToMailUser
		{
			get
			{
				return (SwitchParameter)(base.Fields["MorphToMailUser"] ?? false);
			}
			set
			{
				base.Fields["MorphToMailUser"] = value;
			}
		}

		// Token: 0x17002729 RID: 10025
		// (get) Token: 0x06007DD1 RID: 32209 RVA: 0x00201176 File Offset: 0x001FF376
		// (set) Token: 0x06007DD2 RID: 32210 RVA: 0x0020119C File Offset: 0x001FF39C
		[Parameter(Mandatory = true, ParameterSetName = "MorphToMailbox")]
		public SwitchParameter MorphToMailbox
		{
			get
			{
				return (SwitchParameter)(base.Fields["MorphToMailbox"] ?? false);
			}
			set
			{
				base.Fields["MorphToMailbox"] = value;
			}
		}

		// Token: 0x1700272A RID: 10026
		// (get) Token: 0x06007DD3 RID: 32211 RVA: 0x002011B4 File Offset: 0x001FF3B4
		// (set) Token: 0x06007DD4 RID: 32212 RVA: 0x002011DA File Offset: 0x001FF3DA
		[Parameter(Mandatory = true, ParameterSetName = "UpdateArchive")]
		public SwitchParameter UpdateArchiveOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["UpdateArchiveOnly"] ?? false);
			}
			set
			{
				base.Fields["UpdateArchiveOnly"] = value;
			}
		}

		// Token: 0x1700272B RID: 10027
		// (get) Token: 0x06007DD5 RID: 32213 RVA: 0x002011F2 File Offset: 0x001FF3F2
		// (set) Token: 0x06007DD6 RID: 32214 RVA: 0x00201218 File Offset: 0x001FF418
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMailbox")]
		public SwitchParameter UpdateMailbox
		{
			get
			{
				return (SwitchParameter)(base.Fields["UpdateMailbox"] ?? false);
			}
			set
			{
				base.Fields["UpdateMailbox"] = value;
			}
		}

		// Token: 0x1700272C RID: 10028
		// (get) Token: 0x06007DD7 RID: 32215 RVA: 0x00201230 File Offset: 0x001FF430
		// (set) Token: 0x06007DD8 RID: 32216 RVA: 0x00201247 File Offset: 0x001FF447
		[Parameter(Mandatory = true, ParameterSetName = "MorphToMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateArchive")]
		[Parameter(Mandatory = true, ParameterSetName = "MorphToMailUser")]
		public ADUser RemoteRecipientData
		{
			get
			{
				return (ADUser)base.Fields["RemoteRecipientData"];
			}
			set
			{
				base.Fields["RemoteRecipientData"] = value;
			}
		}

		// Token: 0x1700272D RID: 10029
		// (get) Token: 0x06007DD9 RID: 32217 RVA: 0x0020125A File Offset: 0x001FF45A
		// (set) Token: 0x06007DDA RID: 32218 RVA: 0x00201271 File Offset: 0x001FF471
		[Parameter(Mandatory = false, ParameterSetName = "MorphToMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "MorphToMailUser")]
		public PSCredential Credential
		{
			get
			{
				return (PSCredential)base.Fields["Credential"];
			}
			set
			{
				base.Fields["Credential"] = value;
				base.NetCredential = ((value != null) ? value.GetNetworkCredential() : null);
			}
		}

		// Token: 0x1700272E RID: 10030
		// (get) Token: 0x06007DDB RID: 32219 RVA: 0x00201296 File Offset: 0x001FF496
		// (set) Token: 0x06007DDC RID: 32220 RVA: 0x002012AD File Offset: 0x001FF4AD
		[Parameter(Mandatory = false)]
		public Fqdn ConfigDomainController
		{
			get
			{
				return (Fqdn)base.Fields["ConfigDomainController"];
			}
			set
			{
				base.Fields["ConfigDomainController"] = value;
			}
		}

		// Token: 0x1700272F RID: 10031
		// (get) Token: 0x06007DDD RID: 32221 RVA: 0x002012C0 File Offset: 0x001FF4C0
		// (set) Token: 0x06007DDE RID: 32222 RVA: 0x002012E6 File Offset: 0x001FF4E6
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateArchive")]
		public SwitchParameter SkipMailboxReleaseCheck
		{
			get
			{
				return (SwitchParameter)(base.Fields["SkipMailboxReleaseCheck"] ?? false);
			}
			set
			{
				base.Fields["SkipMailboxReleaseCheck"] = value;
			}
		}

		// Token: 0x17002730 RID: 10032
		// (get) Token: 0x06007DDF RID: 32223 RVA: 0x002012FE File Offset: 0x001FF4FE
		// (set) Token: 0x06007DE0 RID: 32224 RVA: 0x00201324 File Offset: 0x001FF524
		[Parameter(Mandatory = false, ParameterSetName = "MorphToMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMailbox")]
		public SwitchParameter SkipProvisioningCheck
		{
			get
			{
				return (SwitchParameter)(base.Fields["SkipProvisioningCheck"] ?? false);
			}
			set
			{
				base.Fields["SkipProvisioningCheck"] = value;
			}
		}

		// Token: 0x06007DE1 RID: 32225 RVA: 0x0020133C File Offset: 0x001FF53C
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			this.databaseIds = new List<ADObjectId>();
			this.originalMailboxDatabaseId = null;
			this.originalArchiveDatabaseId = null;
			this.newMailboxDatabase = null;
			this.newArchiveDatabase = null;
			base.InternalStateReset();
			this.reportEntries = (base.SessionState.Variables["UMM_ReportEntries"] as List<ReportEntry>);
			if (this.reportEntries == null)
			{
				this.reportEntries = new List<ReportEntry>();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007DE2 RID: 32226 RVA: 0x002013B4 File Offset: 0x001FF5B4
		protected override IConfigDataProvider CreateSession()
		{
			TaskLogger.LogEnter();
			IConfigDataProvider result;
			try
			{
				this.globalConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.ConfigDomainController, true, ConsistencyMode.PartiallyConsistent, base.NetCredential, ADSessionSettings.FromRootOrgScopeSet(), 555, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\UpdateMovedMailbox.cs");
				this.ResolveCurrentOrganization();
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.CurrentOrganizationId);
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.NetCredential, sessionSettings, 565, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\UpdateMovedMailbox.cs");
				tenantOrRootOrgRecipientSession.EnforceDefaultScope = false;
				if (MapiTaskHelper.IsDatacenter || MapiTaskHelper.IsDatacenterDedicated)
				{
					tenantOrRootOrgRecipientSession.SessionSettings.IncludeSoftDeletedObjects = true;
				}
				result = tenantOrRootOrgRecipientSession;
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return result;
		}

		// Token: 0x06007DE3 RID: 32227 RVA: 0x0020147C File Offset: 0x001FF67C
		protected override void ProvisioningUpdateConfigurationObject()
		{
		}

		// Token: 0x06007DE4 RID: 32228 RVA: 0x00201480 File Offset: 0x001FF680
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			IConfigurable result;
			try
			{
				ADUser aduser = (ADUser)base.PrepareDataObject();
				MrsTracer.ActivityID = aduser.ExchangeGuid.GetHashCode();
				this.originalMailboxDatabaseId = aduser.Database;
				this.originalArchiveDatabaseId = aduser.ArchiveDatabase;
				DatabaseInformation? databaseInformation = null;
				if (this.IsFieldSet("NewHomeMDB"))
				{
					DatabaseIdParameter databaseIdParameter = new DatabaseIdParameter(new ADObjectId(this.NewHomeMDB));
					databaseIdParameter.AllowLegacy = true;
					MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(databaseIdParameter, this.globalConfigSession, null, new LocalizedString?(Microsoft.Exchange.Management.Tasks.Strings.ErrorDatabaseNotFound(databaseIdParameter.ToString())), new LocalizedString?(Microsoft.Exchange.Management.Tasks.Strings.ErrorDatabaseNotUnique(databaseIdParameter.ToString())));
					this.newMailboxDatabase = mailboxDatabase;
					databaseInformation = new DatabaseInformation?(this.FindMDBInfo(this.newMailboxDatabase.Id));
					if (CommonUtils.ShouldHonorProvisioningSettings() && !this.newMailboxDatabase.Id.Equals(this.originalMailboxDatabaseId) && !this.SkipProvisioningCheck && this.newMailboxDatabase.IsExcludedFromProvisioning)
					{
						base.WriteError(new DatabaseExcludedFromProvisioningException(this.newMailboxDatabase.Name), ErrorCategory.InvalidArgument, this.Identity);
					}
					if (this.originalMailboxDatabaseId != null && !this.newMailboxDatabase.Id.Equals(this.originalMailboxDatabaseId))
					{
						this.databaseIds.Add(this.originalMailboxDatabaseId);
					}
					this.databaseIds.Add(this.newMailboxDatabase.Id);
				}
				DatabaseInformation? databaseInformation2 = null;
				if (this.IsFieldSet("NewArchiveMDB"))
				{
					if (this.NewArchiveMDB == Guid.Empty)
					{
						this.newArchiveDatabase = null;
					}
					else
					{
						DatabaseIdParameter databaseIdParameter2 = new DatabaseIdParameter(new ADObjectId(this.NewArchiveMDB));
						databaseIdParameter2.AllowLegacy = true;
						MailboxDatabase mailboxDatabase2 = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(databaseIdParameter2, this.globalConfigSession, null, new LocalizedString?(Microsoft.Exchange.Management.Tasks.Strings.ErrorDatabaseNotFound(databaseIdParameter2.ToString())), new LocalizedString?(Microsoft.Exchange.Management.Tasks.Strings.ErrorDatabaseNotUnique(databaseIdParameter2.ToString())));
						this.newArchiveDatabase = mailboxDatabase2;
						databaseInformation2 = new DatabaseInformation?(this.FindMDBInfo(this.newArchiveDatabase.Id));
						if (CommonUtils.ShouldHonorProvisioningSettings() && !ADObjectId.Equals(this.originalArchiveDatabaseId, this.newArchiveDatabase.Id) && !this.SkipProvisioningCheck && this.newArchiveDatabase.IsExcludedFromProvisioning)
						{
							base.WriteError(new DatabaseExcludedFromProvisioningException(this.newArchiveDatabase.Name), ErrorCategory.InvalidArgument, this.Identity);
						}
					}
				}
				if (!this.SkipMailboxReleaseCheck && MailboxTaskHelper.SupportsMailboxReleaseVersioning(aduser) && (databaseInformation != null || databaseInformation2 != null))
				{
					MailboxRelease requiredMailboxRelease = MailboxTaskHelper.ComputeRequiredMailboxRelease(this.ConfigurationSession, aduser, null, new Task.ErrorLoggerDelegate(base.WriteError));
					if (databaseInformation != null)
					{
						MailboxTaskHelper.ValidateMailboxRelease(databaseInformation.Value.MailboxRelease, requiredMailboxRelease, aduser.Id.ToString(), databaseInformation.Value.DatabaseName, new Task.ErrorLoggerDelegate(base.WriteError));
						aduser.MailboxRelease = databaseInformation.Value.MailboxRelease;
					}
					if (databaseInformation2 != null)
					{
						MailboxTaskHelper.ValidateMailboxRelease(databaseInformation2.Value.MailboxRelease, requiredMailboxRelease, aduser.Id.ToString(), databaseInformation2.Value.DatabaseName, new Task.ErrorLoggerDelegate(base.WriteError));
						aduser.ArchiveRelease = databaseInformation2.Value.MailboxRelease;
					}
				}
				bool isArchiveMoving = (this.newArchiveDatabase == null) ? (this.originalArchiveDatabaseId != null) : (!ADObjectId.Equals(this.originalArchiveDatabaseId, this.newArchiveDatabase.Id));
				if (this.MorphToMailUser)
				{
					if (this.originalMailboxDatabaseId != null && !this.databaseIds.Contains(this.originalMailboxDatabaseId))
					{
						this.databaseIds.Add(this.originalMailboxDatabaseId);
					}
					this.ConvertToMailUser(aduser, isArchiveMoving);
				}
				else if (this.MorphToMailbox || this.UpdateMailbox)
				{
					if (this.MorphToMailbox && VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
					{
						IConfigurationSession tenantLocalConfigSession = RecipientTaskHelper.GetTenantLocalConfigSession(aduser.OrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId, true, base.DomainController, base.NetCredential);
						RecipientTaskHelper.ValidateSmtpAddress(tenantLocalConfigSession, aduser.EmailAddresses, aduser, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache, true);
					}
					this.UpdatePrimaryDatabase(aduser);
					this.ConvertToOrUpdateMailbox(aduser, this.MorphToMailbox, isArchiveMoving);
					this.UpdateContainerProperties(aduser);
				}
				else
				{
					MailboxMoveTransition transition;
					ADUser aduser2;
					ADUser aduser3;
					if (this.originalArchiveDatabaseId != null && this.newArchiveDatabase == null)
					{
						if (aduser.IsFromDatacenter)
						{
							aduser.ArchiveRelease = MailboxRelease.None;
						}
						transition = MailboxMoveTransition.UpdateSourceUser;
						aduser2 = aduser;
						aduser3 = this.RemoteRecipientData;
					}
					else if (this.originalArchiveDatabaseId == null && this.newArchiveDatabase != null)
					{
						transition = MailboxMoveTransition.UpdateTargetUser;
						aduser2 = this.RemoteRecipientData;
						aduser3 = aduser;
					}
					else
					{
						if (this.originalArchiveDatabaseId == null || this.newArchiveDatabase == null)
						{
							this.reportEntries.Add(new ReportEntry(MrsStrings.ReportArchiveAlreadyUpdated, ReportEntryType.Warning));
							return aduser;
						}
						transition = MailboxMoveTransition.IntraOrg;
						aduser2 = null;
						aduser3 = aduser;
					}
					MailboxMoveHelper.UpdateRecipientTypeProperties(aduser2, (aduser2 != null) ? (aduser2.IsFromDatacenter ? UserHoster.Datacenter : UserHoster.OnPremise) : UserHoster.None, Server.E14SP1MinVersion, aduser3, aduser3.IsFromDatacenter ? UserHoster.Datacenter : UserHoster.OnPremise, Server.E14SP1MinVersion, MailboxMoveType.IsArchiveMoving, transition);
				}
				this.UpdateArchiveAttributes(aduser);
				result = aduser;
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return result;
		}

		// Token: 0x06007DE5 RID: 32229 RVA: 0x00201A48 File Offset: 0x001FFC48
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				if (this.MorphToMailbox || this.UpdateMailbox)
				{
					this.ApplyMailboxPlan(this.DataObject);
				}
				if (this.MorphToMailbox)
				{
					this.SetUMPropertiesWhenConvertingToMailbox(this.DataObject);
				}
				bool writeShadowProperties = ((IDirectorySession)base.DataSession).ServerSettings.WriteShadowProperties;
				try
				{
					if (base.NetCredential != null)
					{
						((IDirectorySession)base.DataSession).ServerSettings.WriteShadowProperties = false;
					}
					MrsTracer.UpdateMovedMailbox.Debug(string.Format("Saving the ADUser to Domain Controller {0}", base.DataSession.Source), new object[0]);
					base.InternalProcessRecord();
					base.SessionState.Variables["UMM_UpdateSucceeded"] = true;
					base.SessionState.Variables["UMM_DCName"] = base.DataSession.Source;
				}
				finally
				{
					((IDirectorySession)base.DataSession).ServerSettings.WriteShadowProperties = writeShadowProperties;
				}
				if (this.UpdateMailbox)
				{
					this.reportEntries.Add(new ReportEntry(MrsStrings.ReportMovedMailboxUpdated(base.DataSession.Source)));
				}
				else if (this.MorphToMailbox)
				{
					this.reportEntries.Add(new ReportEntry(MrsStrings.ReportMovedMailUserMorphedToMailbox(base.DataSession.Source)));
				}
				else if (this.MorphToMailUser)
				{
					this.reportEntries.Add(new ReportEntry(MrsStrings.ReportMovedMailboxMorphedToMailUser(base.DataSession.Source)));
				}
				else
				{
					this.reportEntries.Add(new ReportEntry(MrsStrings.ReportArchiveUpdated(base.DataSession.Source)));
				}
				try
				{
					CommonUtils.CatchKnownExceptions(delegate
					{
						this.UpdateAllADSites();
					}, delegate(Exception ex)
					{
						LocalizedString error2 = CommonUtils.FullExceptionMessage(ex);
						this.reportEntries.Add(new ReportEntry(MrsStrings.ReportUpdateMovedMailboxFailureAfterADSwitchover(error2), ReportEntryType.WarningCondition, ex, ReportEntryFlags.Cleanup));
					});
				}
				catch (Exception ex)
				{
					Exception ex2;
					LocalizedString error = CommonUtils.FullExceptionMessage(ex2);
					this.reportEntries.Add(new ReportEntry(MrsStrings.ReportUpdateMovedMailboxFailureAfterADSwitchover(error), ReportEntryType.WarningCondition, ex2, ReportEntryFlags.Cleanup));
					ExWatson.SendReport(ex2);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DE6 RID: 32230 RVA: 0x00201CA8 File Offset: 0x001FFEA8
		protected override void InternalEndProcessing()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalEndProcessing();
				if (TestIntegration.Instance.InjectUmmEndProcessingFailure)
				{
					throw new MailboxReplicationPermanentException(new LocalizedString("Injected UMM EndProcessing failure"));
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DE7 RID: 32231 RVA: 0x00201CF4 File Offset: 0x001FFEF4
		protected override bool IsKnownException(Exception exception)
		{
			return RequestTaskHelper.IsKnownExceptionHandler(exception, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose)) || base.IsKnownException(exception);
		}

		// Token: 0x06007DE8 RID: 32232 RVA: 0x00201D13 File Offset: 0x001FFF13
		private bool IsFieldSet(string fieldName)
		{
			return base.Fields.IsChanged(fieldName) || base.Fields.IsModified(fieldName);
		}

		// Token: 0x06007DE9 RID: 32233 RVA: 0x00201D34 File Offset: 0x001FFF34
		private void ClearAttributes(ADUser mailbox, ICollection<PropertyDefinition> properties)
		{
			TaskLogger.LogEnter();
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
				if (!mailbox.ExchangeVersion.IsOlderThan(adpropertyDefinition.VersionAdded))
				{
					if (adpropertyDefinition.IsMultivalued)
					{
						((MultiValuedPropertyBase)mailbox[adpropertyDefinition]).Clear();
					}
					else
					{
						mailbox[adpropertyDefinition] = null;
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007DEA RID: 32234 RVA: 0x00201DBC File Offset: 0x001FFFBC
		private void CopyAttributes(ADUser sourceMailbox, ADUser targetMailbox, PropertyDefinition[] propertiesToCopy)
		{
			TaskLogger.LogEnter();
			foreach (ADPropertyDefinition propertyToCopy in propertiesToCopy)
			{
				this.CopyAttribute(sourceMailbox, targetMailbox, propertyToCopy);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007DEB RID: 32235 RVA: 0x00201DF5 File Offset: 0x001FFFF5
		private void CopyAttribute(ADUser sourceMailbox, ADUser targetMailbox, ADPropertyDefinition propertyToCopy)
		{
			TaskLogger.LogEnter();
			if (propertyToCopy.Type != typeof(ADObjectId))
			{
				this.SetADProperty(targetMailbox, propertyToCopy, sourceMailbox[propertyToCopy]);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007DEC RID: 32236 RVA: 0x00201E27 File Offset: 0x00200027
		private void SetADProperty(ADUser user, ADPropertyDefinition property, object value)
		{
			if (!user.ExchangeVersion.IsOlderThan(property.VersionAdded))
			{
				user[property] = value;
			}
		}

		// Token: 0x06007DED RID: 32237 RVA: 0x00201E44 File Offset: 0x00200044
		private void ConvertToOrUpdateMailbox(ADUser user, bool isCrossOrg, bool isArchiveMoving)
		{
			TaskLogger.LogEnter();
			MrsTracer.UpdateMovedMailbox.Function("UpdateMovedMailbox.ConvertToOrUpdateMailbox", new object[0]);
			try
			{
				IRecipientSession recipientSession = (IRecipientSession)base.DataSession;
				int num;
				string text;
				this.FindMDBServerInfo(this.newMailboxDatabase.Id, out num, out text);
				if (this.newMailboxDatabase.Id.Equals(this.originalMailboxDatabaseId) && num >= Server.E2007MinVersion)
				{
					this.reportEntries.Add(new ReportEntry(Microsoft.Exchange.Management.Tasks.Strings.ErrorUserAlreadyInTargetDatabase(this.Identity.ToString(), this.newMailboxDatabase.Id.ToString()), ReportEntryType.WarningCondition));
				}
				if (!isCrossOrg && this.originalMailboxDatabaseId != null)
				{
					int num2;
					this.FindMDBServerInfo(this.originalMailboxDatabaseId, out num2, out text);
					ExchangeObjectVersion exchangeObjectVersion;
					if (num >= Server.E15MinVersion)
					{
						exchangeObjectVersion = ExchangeObjectVersion.Exchange2012;
					}
					else if (num >= Server.E14MinVersion)
					{
						exchangeObjectVersion = ExchangeObjectVersion.Exchange2010;
					}
					else if (num >= Server.E2007MinVersion)
					{
						exchangeObjectVersion = ExchangeObjectVersion.Exchange2007;
					}
					else
					{
						exchangeObjectVersion = ExchangeObjectVersion.Exchange2003;
					}
					ExchangeObjectVersion other;
					if (num2 >= Server.E15MinVersion)
					{
						other = ExchangeObjectVersion.Exchange2012;
					}
					else if (num2 >= Server.E14MinVersion)
					{
						other = ExchangeObjectVersion.Exchange2010;
					}
					else if (num2 >= Server.E2007MinVersion)
					{
						other = ExchangeObjectVersion.Exchange2007;
					}
					else
					{
						other = ExchangeObjectVersion.Exchange2003;
					}
					if (exchangeObjectVersion.IsOlderThan(other))
					{
						List<PropertyDefinition> list = new List<PropertyDefinition>();
						ADUserSchema instance = ObjectSchema.GetInstance<ADUserSchema>();
						foreach (PropertyDefinition propertyDefinition in instance.AllProperties)
						{
							ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
							if (exchangeObjectVersion.IsOlderThan(adpropertyDefinition.VersionAdded) && !adpropertyDefinition.IsCalculated && !adpropertyDefinition.IsReadOnly && !adpropertyDefinition.IsBackLink)
							{
								list.Add(adpropertyDefinition);
							}
						}
						this.ClearAttributes(user, list);
					}
					if (num >= Server.E15MinVersion)
					{
						user.SetExchangeVersion(ExchangeObjectVersion.Exchange2012);
						if (user.LitigationHoldEnabled)
						{
							user.SetLitigationHoldEnabledWellKnownInPlaceHoldGuid(true);
						}
					}
					else if (num >= Server.E14MinVersion)
					{
						user.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
					}
					else if (num >= Server.E2007MinVersion)
					{
						user.SetExchangeVersion(ExchangeObjectVersion.Exchange2007);
					}
					else
					{
						user.SetExchangeVersion(null);
					}
				}
				else
				{
					if (num >= Server.E15MinVersion)
					{
						user.SetExchangeVersion(ExchangeObjectVersion.Exchange2012);
					}
					else if (num >= Server.E14MinVersion)
					{
						user.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
					}
					else if (num >= Server.E2007MinVersion)
					{
						user.SetExchangeVersion(ExchangeObjectVersion.Exchange2007);
					}
					else
					{
						user.SetExchangeVersion(null);
					}
					if (user.UseDatabaseQuotaDefaults == null)
					{
						user.UseDatabaseQuotaDefaults = new bool?(VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.UseDatabaseQuotaDefaults.Enabled);
					}
					if (num >= Server.E2007MinVersion)
					{
						this.CopyAttributes(this.RemoteRecipientData, user, UpdateMovedMailbox.ElcProperties);
						this.SetElcFlagsForCrossForestMove(user, num);
						this.CopyAttributes(this.RemoteRecipientData, user, UpdateMovedMailbox.ResourceProperties);
						this.CopyAttributes(this.RemoteRecipientData, user, UpdateMovedMailbox.UMProperties);
						this.CopyAttributes(this.RemoteRecipientData, user, UpdateMovedMailbox.OtherProperties);
						if (this.RemoteRecipientData.MailboxPlan == null && !Datacenter.IsMicrosoftHostedOnly(true))
						{
							this.CopyAttributes(this.RemoteRecipientData, user, UpdateMovedMailbox.TransportProperties);
							if (this.RemoteRecipientData.IsOWAEnabledStatusConsistent())
							{
								user.OWAEnabled = this.RemoteRecipientData.OWAEnabled;
							}
						}
					}
				}
				if (isCrossOrg && user.RawExternalEmailAddress != null)
				{
					user.RawExternalEmailAddress = null;
				}
				this.ApplyCustomUpdates(user);
				MailboxMoveHelper.UpdateRecipientTypeProperties(isCrossOrg ? this.RemoteRecipientData : null, isCrossOrg ? (this.RemoteRecipientData.IsFromDatacenter ? UserHoster.Datacenter : UserHoster.OnPremise) : UserHoster.None, 0, user, user.IsFromDatacenter ? UserHoster.Datacenter : UserHoster.OnPremise, num, (!isArchiveMoving) ? MailboxMoveType.IsPrimaryMoving : (MailboxMoveType.IsPrimaryMoving | MailboxMoveType.IsArchiveMoving), isCrossOrg ? MailboxMoveTransition.UpdateTargetUser : MailboxMoveTransition.IntraOrg);
				this.ClearAttributes(user, new PropertyDefinition[]
				{
					ADRecipientSchema.HomeMTA
				});
				this.CheckAndClearLegalHoldPropertiesForDownlevelMove(num, user);
				if (num >= Server.E2007MinVersion)
				{
					this.UpdateRecipient(user);
				}
				if (user.WindowsEmailAddress != user.PrimarySmtpAddress && ADRecipientSchema.WindowsEmailAddress.ValidateValue(user.PrimarySmtpAddress, false) == null)
				{
					user.WindowsEmailAddress = user.PrimarySmtpAddress;
				}
				user.OriginalPrimarySmtpAddress = user.PrimarySmtpAddress;
				user.OriginalWindowsEmailAddress = user.WindowsEmailAddress;
				if (!isCrossOrg && num >= Server.E14MinVersion)
				{
					NetID netID = user.NetID;
					if (netID != null)
					{
						user.NetID = netID;
					}
				}
				if (num >= Server.E14MinVersion)
				{
					this.ApplyRoleAssignmentPolicy(user);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DEE RID: 32238 RVA: 0x002022E0 File Offset: 0x002004E0
		private void CheckAndClearLegalHoldPropertiesForDownlevelMove(int targetServerVersion, ADUser targetUser)
		{
			if (targetServerVersion < Server.E15MinVersion)
			{
				this.ClearAttributes(targetUser, new PropertyDefinition[]
				{
					ADRecipientSchema.InPlaceHoldsRaw
				});
			}
		}

		// Token: 0x06007DEF RID: 32239 RVA: 0x0020230C File Offset: 0x0020050C
		private void ConvertToMailUser(ADUser user, bool isArchiveMoving)
		{
			TaskLogger.LogEnter();
			MrsTracer.UpdateMovedMailbox.Function("UpdateMovedMailbox.ConvertToMailUser", new object[0]);
			try
			{
				if (user.Database == null)
				{
					this.reportEntries.Add(new ReportEntry(MrsStrings.ReportMovedMailboxAlreadyMorphedToMailUser, ReportEntryType.Warning));
				}
				else
				{
					int num;
					string text;
					this.FindMDBServerInfo(user.Database, out num, out text);
					this.SetUMPropertiesWhenConvertingToMailUser(user);
					SmtpAddress primarySmtpAddress = user.PrimarySmtpAddress;
					List<PropertyDefinition> list = new List<PropertyDefinition>();
					foreach (PropertyDefinition propertyDefinition in RecipientConstants.DisableMailbox_PropertiesToReset)
					{
						if (propertyDefinition != ADMailboxRecipientSchema.ExchangeGuid && propertyDefinition != ADRecipientSchema.Alias && propertyDefinition != ADRecipientSchema.EmailAddresses && propertyDefinition != ADRecipientSchema.PoliciesIncluded && propertyDefinition != ADRecipientSchema.PoliciesExcluded && propertyDefinition != ADRecipientSchema.LegacyExchangeDN && propertyDefinition != ADRecipientSchema.MasterAccountSid && propertyDefinition != ADUserSchema.ArchiveGuid && propertyDefinition != ADUserSchema.ArchiveName && propertyDefinition != ADUserSchema.MailboxMoveFlags && propertyDefinition != ADUserSchema.MailboxMoveRemoteHostName && propertyDefinition != ADUserSchema.MailboxMoveStatus && propertyDefinition != ADUserSchema.MailboxMoveSourceMDB && propertyDefinition != ADUserSchema.MailboxMoveTargetMDB && propertyDefinition != ADUserSchema.MailboxMoveBatchName && propertyDefinition != ADUserSchema.UMEnabledFlags && !RecipientConstants.DisableMailUserBase_PropertiesToReset.Contains(propertyDefinition))
						{
							list.Add(propertyDefinition);
						}
					}
					this.ClearAttributes(user, list);
					user.RawExternalEmailAddress = null;
					if (this.RemoteRecipientData.ExternalEmailAddress != null)
					{
						user.ExternalEmailAddress = this.RemoteRecipientData.ExternalEmailAddress;
					}
					this.ApplyCustomUpdates(user);
					if (TestIntegration.Instance.RemoteExchangeGuidOverride == Guid.Empty)
					{
						CustomProxyAddress item = new CustomProxyAddress((CustomProxyAddressPrefix)ProxyAddressPrefix.X500, this.RemoteRecipientData.LegacyExchangeDN, false);
						if (!user.EmailAddresses.Contains(item))
						{
							user.EmailAddresses.Add(item);
						}
					}
					int upgradeSourceUserWhileOnboarding = TestIntegration.Instance.UpgradeSourceUserWhileOnboarding;
					int num2 = (upgradeSourceUserWhileOnboarding == 1) ? Server.E15MinVersion : Server.E14SP1MinVersion;
					bool flag = !user.IsFromDatacenter && this.RemoteRecipientData.IsFromDatacenter;
					if (upgradeSourceUserWhileOnboarding != -1 && num < num2 && flag)
					{
						ExchangeObjectVersion exchangeObjectVersion = (upgradeSourceUserWhileOnboarding == 1) ? ExchangeObjectVersion.Exchange2012 : ExchangeObjectVersion.Exchange2010;
						if (exchangeObjectVersion.IsNewerThan(user.ExchangeVersion ?? ExchangeObjectVersion.Exchange2003))
						{
							user.SetExchangeVersion(exchangeObjectVersion);
						}
					}
					else if (num < Server.E2007MinVersion)
					{
						user.SetExchangeVersion(null);
					}
					MailboxMoveHelper.UpdateRecipientTypeProperties(user, user.IsFromDatacenter ? UserHoster.Datacenter : UserHoster.OnPremise, num, this.RemoteRecipientData, this.RemoteRecipientData.IsFromDatacenter ? UserHoster.Datacenter : UserHoster.OnPremise, 0, (!isArchiveMoving) ? MailboxMoveType.IsPrimaryMoving : (MailboxMoveType.IsPrimaryMoving | MailboxMoveType.IsArchiveMoving), MailboxMoveTransition.UpdateSourceUser);
					this.UpdateRecipient(user);
					if (user.PrimarySmtpAddress == SmtpAddress.Empty && primarySmtpAddress != SmtpAddress.Empty)
					{
						user.PrimarySmtpAddress = primarySmtpAddress;
					}
					user.OriginalPrimarySmtpAddress = user.PrimarySmtpAddress;
					user.OriginalWindowsEmailAddress = user.WindowsEmailAddress;
					user.MailboxRelease = MailboxRelease.None;
					if (isArchiveMoving)
					{
						user.ArchiveRelease = MailboxRelease.None;
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DF0 RID: 32240 RVA: 0x00202614 File Offset: 0x00200814
		private void UpdatePrimaryDatabase(ADUser user)
		{
			TaskLogger.LogEnter();
			MrsTracer.UpdateMovedMailbox.Function("UpdateMovedMailbox.UpdatePrimaryDatabase", new object[0]);
			try
			{
				MrsTracer.UpdateMovedMailbox.Debug("Setting Database to '{0}'", new object[]
				{
					this.newMailboxDatabase.Id
				});
				user.Database = this.newMailboxDatabase.Id;
				int num;
				string text;
				this.FindMDBServerInfo(this.newMailboxDatabase.Id, out num, out text);
				MrsTracer.UpdateMovedMailbox.Debug("Setting ServerLegacyDN to '{0}'", new object[]
				{
					text
				});
				user.ServerLegacyDN = text;
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DF1 RID: 32241 RVA: 0x002026C0 File Offset: 0x002008C0
		private void UpdateContainerProperties(ADUser user)
		{
			TaskLogger.LogEnter();
			MrsTracer.UpdateMovedMailbox.Function("UpdateMovedMailbox.UpdateContainerProperties", new object[0]);
			try
			{
				if (this.IsFieldSet("NewContainerGuid") && user.MailboxContainerGuid != this.NewContainerGuid)
				{
					MrsTracer.UpdateMovedMailbox.Debug("Setting MailboxContainerGuid to '{0}'", new object[]
					{
						this.NewContainerGuid
					});
					user.MailboxContainerGuid = this.NewContainerGuid;
				}
				if (this.IsFieldSet("NewUnifiedMailboxId") && user.UnifiedMailbox != this.NewUnifiedMailboxId)
				{
					MrsTracer.UpdateMovedMailbox.Debug("Setting UnifiedMailbox to '{0}'", new object[]
					{
						this.NewUnifiedMailboxId
					});
					user.UnifiedMailbox = this.NewUnifiedMailboxId;
				}
				if ((this.IsFieldSet("NewContainerGuid") || this.IsFieldSet("NewUnifiedMailboxId")) && user.UnifiedMailbox != null)
				{
					ADRecipient adrecipient;
					ADRecipient.TryGetFromCrossTenantObjectId(user.UnifiedMailbox, out adrecipient);
					ADUser aduser = (ADUser)adrecipient;
					if (!aduser.MailboxContainerGuid.Equals(user.MailboxContainerGuid))
					{
						base.WriteError(new InvalidMailboxContainerGuidException((user.MailboxContainerGuid != null) ? user.MailboxContainerGuid.Value.ToString() : "null", (aduser.MailboxContainerGuid != null) ? aduser.MailboxContainerGuid.Value.ToString() : "null"), ErrorCategory.InvalidArgument, this.Identity);
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DF2 RID: 32242 RVA: 0x002028B8 File Offset: 0x00200AB8
		private void UpdateArchiveAttributes(ADUser user)
		{
			TaskLogger.LogEnter();
			MrsTracer.UpdateMovedMailbox.Function("UpdateMovedMailbox.UpdateArchiveAttributes", new object[0]);
			try
			{
				if ((this.newArchiveDatabase == null) ? (this.originalArchiveDatabaseId != null) : (!ADObjectId.Equals(this.originalArchiveDatabaseId, this.newArchiveDatabase.Id)))
				{
					MrsTracer.UpdateMovedMailbox.Debug("Setting ArchiveDatabase to '{0}'", new object[]
					{
						(this.newArchiveDatabase == null) ? "<null>" : this.newArchiveDatabase.Id.ToString()
					});
					user.ArchiveDatabase = ((this.newArchiveDatabase != null) ? this.newArchiveDatabase.Id : null);
					if (this.originalArchiveDatabaseId != null && !this.databaseIds.Contains(this.originalArchiveDatabaseId))
					{
						this.databaseIds.Add(this.originalArchiveDatabaseId);
					}
					if (this.newArchiveDatabase != null && this.newArchiveDatabase.Id != null)
					{
						if (!this.databaseIds.Contains(this.newArchiveDatabase.Id))
						{
							this.databaseIds.Add(this.newArchiveDatabase.Id);
						}
						if (Datacenter.IsMicrosoftHostedOnly(true) && this.UpdateArchiveOnly && this.originalArchiveDatabaseId == null)
						{
							user.ArchiveQuota = RecipientConstants.ArchiveAddOnQuota;
							user.ArchiveWarningQuota = RecipientConstants.ArchiveAddOnWarningQuota;
						}
					}
				}
				if (this.IsFieldSet("ArchiveDomain"))
				{
					MrsTracer.UpdateMovedMailbox.Debug("Setting ArchiveDomain to '{0}'", new object[]
					{
						this.ArchiveDomain
					});
					if (this.ArchiveDomain == null)
					{
						if (user.ArchiveDomain != null)
						{
							user.ArchiveDomain = null;
						}
					}
					else
					{
						user.ArchiveDomain = new SmtpDomain(this.ArchiveDomain);
					}
				}
				if (this.IsFieldSet("ArchiveStatus"))
				{
					MrsTracer.UpdateMovedMailbox.Debug("Setting ArchiveStatus to '{0}'", new object[]
					{
						this.ArchiveStatus
					});
					if (user.ArchiveStatus != this.ArchiveStatus)
					{
						user.ArchiveStatus = this.ArchiveStatus;
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DF3 RID: 32243 RVA: 0x00202B10 File Offset: 0x00200D10
		private void ApplyMailboxPlan(ADUser user)
		{
			TaskLogger.LogEnter();
			MrsTracer.UpdateMovedMailbox.Function("UpdateMovedMailbox.ApplyMailboxPlan", new object[0]);
			if (!Datacenter.IsMicrosoftHostedOnly(true))
			{
				return;
			}
			try
			{
				if (this.MorphToMailbox)
				{
					ApplyMailboxPlanFlags flags = ConfigBase<MRSConfigSchema>.GetConfig<bool>("CheckMailUserPlanQuotasForOnboarding") ? ApplyMailboxPlanFlags.PreservePreviousExplicitlySetValues : ApplyMailboxPlanFlags.None;
					MrsTracer.UpdateMovedMailbox.Function("ApplyMailboxPlan(): MorphToMailbox is set to true, applying mbxplan", new object[0]);
					MailboxTaskHelper.ApplyMbxPlanSettingsInTargetForest(user, (ADObjectId mbxPlanId) => (ADUser)base.GetDataObject<ADUser>(new MailboxPlanIdParameter(mbxPlanId), base.TenantGlobalCatalogSession, null, new LocalizedString?(Microsoft.Exchange.Management.Tasks.Strings.ErrorMailboxPlanNotFound(mbxPlanId.ToString())), new LocalizedString?(Microsoft.Exchange.Management.Tasks.Strings.ErrorMailboxPlanNotUnique(mbxPlanId.ToString()))), flags);
				}
				else
				{
					MrsTracer.UpdateMovedMailbox.Function("ApplyMailboxPlan(): MorphToMailbox is set to false, no need to apply MailboxPlan, leaving", new object[0]);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DF4 RID: 32244 RVA: 0x00202BBC File Offset: 0x00200DBC
		private void ApplyRoleAssignmentPolicy(ADUser user)
		{
			TaskLogger.LogEnter();
			MrsTracer.UpdateMovedMailbox.Function("UpdateMovedMailbox.ApplyRoleAssignmentPolicy", new object[0]);
			try
			{
				if (user.RoleAssignmentPolicy == null && user.RecipientTypeDetails != RecipientTypeDetails.ArbitrationMailbox)
				{
					IConfigurationSession tenantLocalConfigSession = RecipientTaskHelper.GetTenantLocalConfigSession(user.OrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId, true, base.DomainController, base.NetCredential);
					RoleAssignmentPolicy roleAssignmentPolicy = RecipientTaskHelper.FindDefaultRoleAssignmentPolicy(tenantLocalConfigSession, new Task.ErrorLoggerDelegate(base.WriteError), Microsoft.Exchange.Management.Tasks.Strings.ErrorDefaultRoleAssignmentPolicyNotUnique, Microsoft.Exchange.Management.Tasks.Strings.ErrorDefaultRoleAssignmentPolicyNotFound);
					if (roleAssignmentPolicy != null)
					{
						user.RoleAssignmentPolicy = (ADObjectId)roleAssignmentPolicy.Identity;
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DF5 RID: 32245 RVA: 0x00202C6C File Offset: 0x00200E6C
		private void UpdateRecipient(ADUser user)
		{
			if (base.IsProvisioningLayerAvailable)
			{
				MrsTracer.UpdateMovedMailbox.Debug("Provisioning Layer is available, calling UpdateRecipient", new object[0]);
				ProvisioningLayer.UpdateAffectedIConfigurable(this, RecipientTaskHelper.ConvertRecipientToPresentationObject(user), false);
				return;
			}
			MrsTracer.UpdateMovedMailbox.Error("Provisioning Layer is not available!", new object[0]);
			Exception ex = new InvalidOperationException(Microsoft.Exchange.Configuration.Common.LocStrings.Strings.ErrorNoProvisioningHandlerAvailable);
			this.reportEntries.Add(new ReportEntry(MrsStrings.ReportUpdateMovedMailboxError(new LocalizedString("ErrorNoProvisioningHandlerAvailable")), ReportEntryType.Error, ex, ReportEntryFlags.None));
			base.WriteError(ex, ErrorCategory.InvalidOperation, null);
		}

		// Token: 0x06007DF6 RID: 32246 RVA: 0x00202CF8 File Offset: 0x00200EF8
		private void UpdateAllADSites()
		{
			TaskLogger.LogEnter();
			MrsTracer.UpdateMovedMailbox.Function("UpdateMovedMailbox.UpdateAllADSites", new object[0]);
			if (TestIntegration.Instance.GetIntValue("SquishyLobster-UpdateAllADSitesOnMoveCompletion", 1, 0, 1) == 0 || this.databaseIds == null)
			{
				return;
			}
			MrsTracer.UpdateMovedMailbox.Debug("Preparing to replicate object to all possible AD Sites for Database-Copy locations", new object[0]);
			try
			{
				List<ADObjectId> list = new List<ADObjectId>();
				foreach (ADObjectId adobjectId in this.databaseIds)
				{
					DatabaseIdParameter databaseIdParameter = new DatabaseIdParameter(adobjectId);
					ITopologyConfigurationSession configSessionForDatabase = RequestTaskHelper.GetConfigSessionForDatabase(this.globalConfigSession, adobjectId);
					MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(databaseIdParameter, configSessionForDatabase, null, new LocalizedString?(Microsoft.Exchange.Management.Tasks.Strings.ErrorDatabaseNotFound(databaseIdParameter.ToString())), new LocalizedString?(Microsoft.Exchange.Management.Tasks.Strings.ErrorDatabaseNotUnique(databaseIdParameter.ToString())));
					if (mailboxDatabase == null || mailboxDatabase.Servers == null)
					{
						MrsTracer.UpdateMovedMailbox.Debug("A database could not be found or had no copies.", new object[0]);
					}
					else
					{
						foreach (ADObjectId adObjectId in mailboxDatabase.Servers)
						{
							ServerIdParameter serverIdParameter = new ServerIdParameter(adObjectId);
							Server server = (Server)base.GetDataObject<Server>(serverIdParameter, this.globalConfigSession, null, new LocalizedString?(Microsoft.Exchange.Management.Tasks.Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Microsoft.Exchange.Management.Tasks.Strings.ErrorServerNotUnique(serverIdParameter.ToString())));
							ADObjectId serverSite = server.ServerSite;
							if (!list.Contains(serverSite))
							{
								list.Add(serverSite);
								MrsTracer.UpdateMovedMailbox.Debug("Adding site '{0}' for replication.", new object[]
								{
									serverSite.ToString()
								});
							}
						}
					}
				}
				this.globalConfigSession.DomainController = this.globalConfigSession.Source;
				RootDse rootDse = this.globalConfigSession.GetRootDse();
				ADObjectId site = rootDse.Site;
				if (list.Contains(site))
				{
					MrsTracer.UpdateMovedMailbox.Debug("Removing local site '{0}' from list for replication.", new object[]
					{
						site.ToString()
					});
					list.Remove(site);
				}
				if (list.Count > 0)
				{
					MrsTracer.UpdateMovedMailbox.Debug("Replicating object to all possible AD Sites for Database-Copy locations", new object[0]);
					((IRecipientSession)base.DataSession).ReplicateSingleObject(this.DataObject, list.ToArray());
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DF7 RID: 32247 RVA: 0x00202F7C File Offset: 0x0020117C
		private void FindMDBServerInfo(ADObjectId mdbId, out int serverVersion, out string serverDN)
		{
			DatabaseInformation databaseInformation = this.FindMDBInfo(mdbId);
			serverVersion = databaseInformation.ServerVersion;
			serverDN = databaseInformation.ServerDN;
		}

		// Token: 0x06007DF8 RID: 32248 RVA: 0x00202FA3 File Offset: 0x002011A3
		private DatabaseInformation FindMDBInfo(ADObjectId mdbId)
		{
			return MapiUtils.FindServerForMdb(mdbId, this.ConfigDomainController, base.NetCredential, FindServerFlags.None);
		}

		// Token: 0x06007DF9 RID: 32249 RVA: 0x00203070 File Offset: 0x00201270
		private void SetElcFlagsForCrossForestMove(ADUser user, int targetServerVersion)
		{
			TaskLogger.LogEnter();
			MrsTracer.UpdateMovedMailbox.Function("UpdateMovedMailbox.SetElcFlagsForCrossForestMove", new object[0]);
			try
			{
				CommonUtils.CatchKnownExceptions(delegate
				{
					ElcMailboxFlags elcMailboxFlags = (ElcMailboxFlags)Enum.Parse(typeof(ElcMailboxFlags), this.RemoteRecipientData[ADUserSchema.ElcMailboxFlags].ToString());
					ElcMailboxFlags elcMailboxFlags2 = ElcMailboxFlags.ExpirationSuspended | ElcMailboxFlags.ElcV2 | ElcMailboxFlags.DisableCalendarLogging | ElcMailboxFlags.LitigationHold | ElcMailboxFlags.SingleItemRecovery | ElcMailboxFlags.ShouldUseDefaultRetentionPolicy;
					user[ADUserSchema.ElcMailboxFlags] = (elcMailboxFlags & elcMailboxFlags2);
					if (user.LitigationHoldEnabled && targetServerVersion >= Server.E15MinVersion)
					{
						user.SetLitigationHoldEnabledWellKnownInPlaceHoldGuid(true);
					}
				}, delegate(Exception ex)
				{
					this.reportEntries.Add(new ReportEntry(MrsStrings.ReportUpdateMovedMailboxError(new LocalizedString(CommonUtils.GetFailureType(ex))), ReportEntryType.WarningCondition, ex, ReportEntryFlags.Cleanup));
				});
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DFA RID: 32250 RVA: 0x0020313C File Offset: 0x0020133C
		private void SetUMPropertiesWhenConvertingToMailUser(ADUser user)
		{
			TaskLogger.LogEnter();
			MrsTracer.UpdateMovedMailbox.Function("UpdateMovedMailbox.SetUMPropertiesWhenConvertingToMailUser", new object[0]);
			try
			{
				CommonUtils.CatchKnownExceptions(delegate
				{
					this.SetUMPropertiesWhenConvertingToMailUserWorker(user);
				}, delegate(Exception ex)
				{
					this.reportEntries.Add(new ReportEntry(MrsStrings.ReportUpdateMovedMailboxError(new LocalizedString(CommonUtils.GetFailureType(ex))), ReportEntryType.WarningCondition, ex, ReportEntryFlags.Cleanup));
				});
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DFB RID: 32251 RVA: 0x002031BC File Offset: 0x002013BC
		private void SetUMPropertiesWhenConvertingToMailUserWorker(ADUser user)
		{
			TaskLogger.LogEnter();
			MrsTracer.UpdateMovedMailbox.Function("UpdateMovedMailbox.SetUMPropertiesWhenConvertingToMailUserWorker", new object[0]);
			try
			{
				if (!user.UMEnabled)
				{
					MrsTracer.UpdateMovedMailbox.Debug("User {0} is not enabled for UM, so no changes required", new object[]
					{
						user
					});
				}
				else
				{
					Utility.ResetUMADProperties(user, false);
					this.CopyAttribute(this.RemoteRecipientData, user, ADOrgPersonSchema.VoiceMailSettings);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DFC RID: 32252 RVA: 0x0020323C File Offset: 0x0020143C
		private void ApplyCustomUpdates(ADUser user)
		{
			if (this.RemoteRecipientData == null)
			{
				return;
			}
			PropertyUpdateXML[] updates = XMLSerializableBase.Deserialize<PropertyUpdateXML[]>(this.RemoteRecipientData.LinkedMasterAccount, false);
			PropertyUpdateXML.Apply(updates, user);
		}

		// Token: 0x06007DFD RID: 32253 RVA: 0x002033E4 File Offset: 0x002015E4
		private void SetUMPropertiesWhenConvertingToMailbox(ADUser user)
		{
			TaskLogger.LogEnter();
			MrsTracer.UpdateMovedMailbox.Function("UpdateMovedMailbox.SetUMPropertiesWhenConvertingToMailbox", new object[0]);
			try
			{
				CommonUtils.CatchKnownExceptions(delegate
				{
					if (!this.RemoteRecipientData.UMEnabled)
					{
						MrsTracer.UpdateMovedMailbox.Debug("User {0} is not enabled for UM, so no changes required", new object[]
						{
							user
						});
						return;
					}
					this.EnableUserForUM(user);
					this.SetADProperty(user, ADRecipientSchema.UMProvisioningRequested, true);
				}, delegate(Exception ex1)
				{
					this.reportEntries.Add(new ReportEntry(Microsoft.Exchange.Management.Tasks.Strings.UserCanNotBeEnabledForUM(user.ExchangeGuid.ToString(), new LocalizedString(CommonUtils.GetFailureType(ex1))), ReportEntryType.WarningCondition, ex1, ReportEntryFlags.Cleanup));
					CommonUtils.CatchKnownExceptions(delegate
					{
						Utility.ResetUMADProperties(user, true);
						this.SetADProperty(user, ADRecipientSchema.UMProvisioningRequested, true);
					}, delegate(Exception ex2)
					{
						this.reportEntries.Add(new ReportEntry(Microsoft.Exchange.Management.Tasks.Strings.ReportUpdateMovedMailboxError(new LocalizedString(CommonUtils.GetFailureType(ex1))), ReportEntryType.WarningCondition, ex2, ReportEntryFlags.Cleanup));
					});
				});
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DFE RID: 32254 RVA: 0x00203464 File Offset: 0x00201664
		private void EnableUserForUM(ADUser user)
		{
			TaskLogger.LogEnter();
			MrsTracer.UpdateMovedMailbox.Function("UpdateMovedMailbox.TryEnableUserForUM", new object[0]);
			try
			{
				IRecipientSession tenantLocalRecipientSession = RecipientTaskHelper.GetTenantLocalRecipientSession(user.OrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId, base.DomainController, base.NetCredential);
				IConfigurationSession tenantLocalConfigSession = RecipientTaskHelper.GetTenantLocalConfigSession(user.OrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId, true, base.DomainController, base.NetCredential);
				MigrationHelper.EnableTargetUserForUM(tenantLocalRecipientSession, tenantLocalConfigSession, Datacenter.IsMicrosoftHostedOnly(true), this.RemoteRecipientData, user);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007DFF RID: 32255 RVA: 0x0020350C File Offset: 0x0020170C
		private void ResolveCurrentOrganization()
		{
			if (this.partitionHint == null)
			{
				base.CurrentOrganizationId = OrganizationId.ForestWideOrgId;
				return;
			}
			ADSessionSettings adsessionSettings = ADSessionSettings.FromTenantPartitionHint(this.partitionHint);
			base.CurrentOrganizationId = adsessionSettings.CurrentOrganizationId;
		}

		// Token: 0x04003DF6 RID: 15862
		public const string ParameterIdentity = "Identity";

		// Token: 0x04003DF7 RID: 15863
		public const string ParameterPartitionHint = "PartitionHint";

		// Token: 0x04003DF8 RID: 15864
		public const string ParameterNewHomeMDB = "NewHomeMDB";

		// Token: 0x04003DF9 RID: 15865
		public const string ParameterNewContainerGuid = "NewContainerGuid";

		// Token: 0x04003DFA RID: 15866
		public const string ParameterNewUnifiedMailboxId = "NewUnifiedMailboxId";

		// Token: 0x04003DFB RID: 15867
		public const string ParameterNewArchiveMDB = "NewArchiveMDB";

		// Token: 0x04003DFC RID: 15868
		public const string ParameterArchiveDomain = "ArchiveDomain";

		// Token: 0x04003DFD RID: 15869
		public const string ParameterArchiveStatus = "ArchiveStatus";

		// Token: 0x04003DFE RID: 15870
		public const string ParameterMorphToMailUser = "MorphToMailUser";

		// Token: 0x04003DFF RID: 15871
		public const string ParameterMorphToMailbox = "MorphToMailbox";

		// Token: 0x04003E00 RID: 15872
		public const string ParameterUpdateArchiveOnly = "UpdateArchiveOnly";

		// Token: 0x04003E01 RID: 15873
		public const string ParameterUpdateMailbox = "UpdateMailbox";

		// Token: 0x04003E02 RID: 15874
		public const string ParameterRemoteRecipientData = "RemoteRecipientData";

		// Token: 0x04003E03 RID: 15875
		public const string ParameterCredential = "Credential";

		// Token: 0x04003E04 RID: 15876
		public const string ParameterConfigDomainController = "ConfigDomainController";

		// Token: 0x04003E05 RID: 15877
		public const string ParameterSkipMailboxReleaseCheck = "SkipMailboxReleaseCheck";

		// Token: 0x04003E06 RID: 15878
		public const string ParameterSkipProvisioningCheck = "SkipProvisioningCheck";

		// Token: 0x04003E07 RID: 15879
		private const string ParameterSetIntraOrg = "UpdateMailbox";

		// Token: 0x04003E08 RID: 15880
		private const string ParameterSetMorphToMailbox = "MorphToMailbox";

		// Token: 0x04003E09 RID: 15881
		private const string ParameterSetMorphToMailUser = "MorphToMailUser";

		// Token: 0x04003E0A RID: 15882
		private const string ParameterSetUpdateArchive = "UpdateArchive";

		// Token: 0x04003E0B RID: 15883
		internal static readonly PropertyDefinition[] UMProperties = new PropertyDefinition[]
		{
			ADRecipientSchema.UMDtmfMap,
			ADRecipientSchema.AllowUMCallsFromNonUsers,
			ADRecipientSchema.UMRecipientDialPlanId,
			ADRecipientSchema.UMSpokenName,
			ADUserSchema.UMCallingLineIds,
			ADUserSchema.UMEnabledFlags,
			ADUserSchema.UMEnabledFlags2,
			ADUserSchema.OperatorNumber,
			ADUserSchema.PhoneProviderId,
			ADUserSchema.UMPinChecksum,
			ADUserSchema.UMServerWritableFlags,
			ADUserSchema.CallAnsweringAudioCodecLegacy,
			ADUserSchema.CallAnsweringAudioCodec2
		};

		// Token: 0x04003E0C RID: 15884
		private static readonly PropertyDefinition[] ElcProperties = new PropertyDefinition[]
		{
			ADUserSchema.ElcExpirationSuspensionEndDate,
			ADUserSchema.ElcExpirationSuspensionStartDate,
			ADUserSchema.LitigationHoldDate,
			ADUserSchema.LitigationHoldOwner,
			ADUserSchema.RetentionComment,
			ADUserSchema.RetentionUrl
		};

		// Token: 0x04003E0D RID: 15885
		private static readonly PropertyDefinition[] TransportProperties = new PropertyDefinition[]
		{
			ADRecipientSchema.MessageHygieneFlags,
			ADRecipientSchema.SCLDeleteThresholdInt,
			ADRecipientSchema.SCLRejectThresholdInt,
			ADRecipientSchema.SCLQuarantineThresholdInt,
			ADRecipientSchema.SCLJunkThresholdInt
		};

		// Token: 0x04003E0E RID: 15886
		private static readonly PropertyDefinition[] ResourceProperties = new PropertyDefinition[]
		{
			ADRecipientSchema.ResourceCapacity,
			ADRecipientSchema.ResourceMetaData,
			ADRecipientSchema.ResourcePropertiesDisplay,
			ADRecipientSchema.ResourceSearchProperties
		};

		// Token: 0x04003E0F RID: 15887
		private static readonly PropertyDefinition[] OtherProperties = new PropertyDefinition[]
		{
			ADMailboxRecipientSchema.ExternalOofOptions,
			ADMailboxRecipientSchema.RulesQuota,
			IADMailStorageSchema.SharingPartnerIdentitiesRaw
		};

		// Token: 0x04003E10 RID: 15888
		private MailboxDatabase newMailboxDatabase;

		// Token: 0x04003E11 RID: 15889
		private MailboxDatabase newArchiveDatabase;

		// Token: 0x04003E12 RID: 15890
		private ADObjectId originalMailboxDatabaseId;

		// Token: 0x04003E13 RID: 15891
		private ADObjectId originalArchiveDatabaseId;

		// Token: 0x04003E14 RID: 15892
		private List<ADObjectId> databaseIds;

		// Token: 0x04003E15 RID: 15893
		private ITopologyConfigurationSession globalConfigSession;

		// Token: 0x04003E16 RID: 15894
		private TenantPartitionHint partitionHint;

		// Token: 0x04003E17 RID: 15895
		private List<ReportEntry> reportEntries;
	}
}
