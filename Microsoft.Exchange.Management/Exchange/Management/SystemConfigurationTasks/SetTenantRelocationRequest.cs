using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000694 RID: 1684
	[Cmdlet("Set", "TenantRelocationRequest", SupportsShouldProcess = true, DefaultParameterSetName = "DefaultParameterSet")]
	public sealed class SetTenantRelocationRequest : SetSystemConfigurationObjectTask<TenantRelocationRequestIdParameter, TenantRelocationRequest>
	{
		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06003BBD RID: 15293 RVA: 0x000FF2DC File Offset: 0x000FD4DC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetTenantRelocationRequest(this.Identity.ToString());
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06003BBE RID: 15294 RVA: 0x000FF2EE File Offset: 0x000FD4EE
		protected override bool RehomeDataSession
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06003BBF RID: 15295 RVA: 0x000FF2F1 File Offset: 0x000FD4F1
		// (set) Token: 0x06003BC0 RID: 15296 RVA: 0x000FF308 File Offset: 0x000FD508
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override TenantRelocationRequestIdParameter Identity
		{
			get
			{
				return (TenantRelocationRequestIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x000FF31B File Offset: 0x000FD51B
		// (set) Token: 0x06003BC2 RID: 15298 RVA: 0x000FF332 File Offset: 0x000FD532
		[Parameter(Mandatory = false, ParameterSetName = "DefaultParameterSet")]
		public RelocationStateRequestedByCmdlet RelocationStateRequested
		{
			get
			{
				return (RelocationStateRequestedByCmdlet)base.Fields[TenantRelocationRequestSchema.RelocationStateRequested];
			}
			set
			{
				base.Fields[TenantRelocationRequestSchema.RelocationStateRequested] = value;
			}
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x000FF34A File Offset: 0x000FD54A
		// (set) Token: 0x06003BC4 RID: 15300 RVA: 0x000FF36B File Offset: 0x000FD56B
		[Parameter(Mandatory = false, ParameterSetName = "DefaultParameterSet")]
		public bool AutoCompletionEnabled
		{
			get
			{
				return (bool)(base.Fields[TenantRelocationRequestSchema.AutoCompletionEnabled] ?? false);
			}
			set
			{
				base.Fields[TenantRelocationRequestSchema.AutoCompletionEnabled] = value;
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06003BC5 RID: 15301 RVA: 0x000FF383 File Offset: 0x000FD583
		// (set) Token: 0x06003BC6 RID: 15302 RVA: 0x000FF3A4 File Offset: 0x000FD5A4
		[Parameter(Mandatory = false, ParameterSetName = "DefaultParameterSet")]
		public bool LargeTenantModeEnabled
		{
			get
			{
				return (bool)(base.Fields[TenantRelocationRequestSchema.LargeTenantModeEnabled] ?? false);
			}
			set
			{
				base.Fields[TenantRelocationRequestSchema.LargeTenantModeEnabled] = value;
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x000FF3BC File Offset: 0x000FD5BC
		// (set) Token: 0x06003BC8 RID: 15304 RVA: 0x000FF3D3 File Offset: 0x000FD5D3
		[Parameter(Mandatory = false, ParameterSetName = "DefaultParameterSet")]
		public Schedule SafeLockdownSchedule
		{
			get
			{
				return (Schedule)base.Fields[TenantRelocationRequestSchema.SafeLockdownSchedule];
			}
			set
			{
				base.Fields[TenantRelocationRequestSchema.SafeLockdownSchedule] = value;
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06003BC9 RID: 15305 RVA: 0x000FF3E6 File Offset: 0x000FD5E6
		// (set) Token: 0x06003BCA RID: 15306 RVA: 0x000FF40C File Offset: 0x000FD60C
		[Parameter(Mandatory = true, ParameterSetName = "SuspendParameterSet")]
		public SwitchParameter Suspend
		{
			get
			{
				return (SwitchParameter)(base.Fields["SuspendParameter"] ?? false);
			}
			set
			{
				base.Fields["SuspendParameter"] = value;
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06003BCB RID: 15307 RVA: 0x000FF424 File Offset: 0x000FD624
		// (set) Token: 0x06003BCC RID: 15308 RVA: 0x000FF44A File Offset: 0x000FD64A
		[Parameter(Mandatory = true, ParameterSetName = "ResumeParameterSet")]
		public SwitchParameter Resume
		{
			get
			{
				return (SwitchParameter)(base.Fields["ResumeParameter"] ?? false);
			}
			set
			{
				base.Fields["ResumeParameter"] = value;
			}
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06003BCD RID: 15309 RVA: 0x000FF462 File Offset: 0x000FD662
		// (set) Token: 0x06003BCE RID: 15310 RVA: 0x000FF488 File Offset: 0x000FD688
		[Parameter(Mandatory = false, ParameterSetName = "ResetPermanentErrorParameterSet")]
		public SwitchParameter ResetPermanentError
		{
			get
			{
				return (SwitchParameter)(base.Fields["ResetPermanentErrorParameter"] ?? false);
			}
			set
			{
				base.Fields["ResetPermanentErrorParameter"] = value;
			}
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06003BCF RID: 15311 RVA: 0x000FF4A0 File Offset: 0x000FD6A0
		// (set) Token: 0x06003BD0 RID: 15312 RVA: 0x000FF4C6 File Offset: 0x000FD6C6
		[Parameter(Mandatory = false, ParameterSetName = "ResetPermanentErrorParameterSet")]
		public SwitchParameter ResetStartSyncTime
		{
			get
			{
				return (SwitchParameter)(base.Fields["ResetStartSyncTimeParameter"] ?? false);
			}
			set
			{
				base.Fields["ResetStartSyncTimeParameter"] = value;
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06003BD1 RID: 15313 RVA: 0x000FF4DE File Offset: 0x000FD6DE
		// (set) Token: 0x06003BD2 RID: 15314 RVA: 0x000FF504 File Offset: 0x000FD704
		[Parameter(Mandatory = true, ParameterSetName = "ResetTransitionCounterParameterSet")]
		public SwitchParameter ResetTransitionCounter
		{
			get
			{
				return (SwitchParameter)(base.Fields["ResetTransitionCounterParameter"] ?? false);
			}
			set
			{
				base.Fields["ResetTransitionCounterParameter"] = value;
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06003BD3 RID: 15315 RVA: 0x000FF51C File Offset: 0x000FD71C
		// (set) Token: 0x06003BD4 RID: 15316 RVA: 0x000FF542 File Offset: 0x000FD742
		[Parameter(Mandatory = false, ParameterSetName = "DefaultParameterSet")]
		public SwitchParameter RollbackGls
		{
			get
			{
				return (SwitchParameter)(base.Fields["RollbackGlsParameter"] ?? false);
			}
			set
			{
				base.Fields["RollbackGlsParameter"] = value;
			}
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x000FF55C File Offset: 0x000FD75C
		protected override IConfigurable ResolveDataObject()
		{
			ADObject adobject = (ADObject)base.ResolveDataObject();
			base.RebindDataSessionToDataObjectPartitionRidMasterIncludeRetiredTenants(((ADObjectId)adobject.Identity).GetPartitionId());
			return (ADObject)base.ResolveDataObject();
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x000FF598 File Offset: 0x000FD798
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.RollbackGls)
			{
				if (!this.DataObject.InPostGLSSwitchState())
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorRollbackGlsExpectsPostGlsState(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
				if (!base.Fields.IsModified(TenantRelocationRequestSchema.RelocationStateRequested))
				{
					this.RelocationStateRequested = RelocationStateRequestedByCmdlet.SynchronizationFinishedFullSync;
				}
				else if (this.RelocationStateRequested != RelocationStateRequestedByCmdlet.SynchronizationFinishedFullSync)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorRollbackGlsExpectsSynchronizationFinishedFullSync(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
			}
			if (base.Fields.IsModified(TenantRelocationRequestSchema.RelocationStateRequested) && (this.AutoCompletionEnabled || (!base.Fields.IsModified(TenantRelocationRequestSchema.AutoCompletionEnabled) && this.DataObject.AutoCompletionEnabled)))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorRelocationStateRequestedIsNotAllowed(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (base.Fields.IsModified(TenantRelocationRequestSchema.AutoCompletionEnabled) && !this.AutoCompletionEnabled && !base.Fields.IsModified(TenantRelocationRequestSchema.RelocationStateRequested))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorRelocationStateRequestedIsMandatory(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (base.Fields.IsModified(TenantRelocationRequestSchema.RelocationStateRequested))
			{
				if (this.RollbackGls || (this.RelocationStateRequested == RelocationStateRequestedByCmdlet.SynchronizationFinishedFullSync && this.DataObject.InLockdownBeforeGLSSwitchState()) || (this.RelocationStateRequested == RelocationStateRequestedByCmdlet.SynchronizationFinishedFullSync && this.DataObject.RelocationStatusDetailsSource == RelocationStatusDetailsSource.SynchronizationFinishedDeltaSync))
				{
					this.applyTransitionFromCmdlet = true;
				}
				else if (this.RelocationStateRequested < (RelocationStateRequestedByCmdlet)this.DataObject.RelocationStatusDetailsSource)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorRelocationStateRequestedIsTooLow(this.Identity.ToString(), this.RelocationStateRequested.ToString(), this.DataObject.RelocationStatusDetailsSource.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
				if (this.DataObject.RelocationStateRequested == Microsoft.Exchange.Data.Directory.SystemConfiguration.RelocationStateRequested.Cleanup && this.RelocationStateRequested != (RelocationStateRequestedByCmdlet)this.DataObject.RelocationStateRequested)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorCleanupRequestedNoRollback(this.Identity.ToString(), this.RelocationStateRequested.ToString(), this.DataObject.RelocationStateRequested.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
				if (this.RelocationStateRequested == RelocationStateRequestedByCmdlet.Cleanup)
				{
					if (this.DataObject.RelocationStatusDetailsSource != RelocationStatusDetailsSource.RetiredUpdatedTargetForest)
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorCleanupRequestedAtWrongStage(this.Identity.ToString(), this.RelocationStateRequested.ToString(), this.DataObject.RelocationStatusDetailsSource.ToString(), RelocationStatusDetailsSource.RetiredUpdatedTargetForest.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
					}
					if (!this.DataObject.IsRetiredSourceHoldTimedOut())
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorSourceHoldNotTimedOut(this.Identity.ToString(), TenantRelocationRequest.WaitTimeBeforeRemoveSourceReplicaDays.ToString(), this.DataObject.RetiredStartTime.Value.ToUniversalTime().ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
					}
				}
			}
			if ((base.Fields.IsModified("ResumeParameter") && !this.Resume) || (base.Fields.IsModified("SuspendParameter") && !this.Suspend))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorSuspendAndResumeDontSupportFalse), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (this.Resume && !this.DataObject.Suspended)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorCannotResumeIfNotSuspended), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x000FF9C4 File Offset: 0x000FDBC4
		protected override void InternalProcessRecord()
		{
			string forestFQDN = this.DataObject.OrganizationId.PartitionId.ForestFQDN;
			if (this.applyTransitionFromCmdlet)
			{
				LocalizedString? localizedString = null;
				if (!this.DataObject.AutoCompletionEnabled && this.DataObject.RelocationStatusDetailsSource != (RelocationStatusDetailsSource)this.DataObject.RelocationStateRequested)
				{
					localizedString = new LocalizedString?(Strings.WarningApplyingTransitionWhileRelocationStatusNotReachedStateRequested(this.Identity.ToString(), this.DataObject.RelocationStateRequested.ToString(), this.DataObject.RelocationStatusDetailsSource.ToString()));
				}
				else if (this.DataObject.AutoCompletionEnabled && this.DataObject.RelocationStatusDetailsSource != RelocationStatusDetailsSource.RetiredUpdatedTargetForest)
				{
					localizedString = new LocalizedString?(Strings.WarningApplyingTransitionWhileRelocationStatusNotReachedStateRequested(this.Identity.ToString(), this.DataObject.RelocationStateRequested.ToString(), this.DataObject.RelocationStatusDetailsSource.ToString()));
				}
				if (localizedString != null && !base.ShouldContinue(localizedString.Value))
				{
					return;
				}
				if (this.RollbackGls && this.DataObject.RelocationStatusDetailsSource == RelocationStatusDetailsSource.RetiredUpdatedTargetForest)
				{
					localizedString = new LocalizedString?(Strings.WarningPossibleDataLossWithGlsRollback(this.Identity.ToString(), this.DataObject.TargetForest, forestFQDN));
					if (!base.ShouldContinue(localizedString.Value))
					{
						return;
					}
				}
			}
			if (this.Suspend && this.DataObject.RelocationStatusDetailsSource > RelocationStatusDetailsSource.SynchronizationStartedDeltaSync)
			{
				LocalizedString? localizedString2 = new LocalizedString?(Strings.WarningSuspendSupportedOnlyDuringSync(this.DataObject.RelocationStatusDetailsSource.ToString(), RelocationStatusDetailsSource.SynchronizationStartedFullSync.ToString(), RelocationStatusDetailsSource.SynchronizationStartedDeltaSync.ToString()));
				if (!base.ShouldContinue(localizedString2.Value))
				{
					return;
				}
			}
			this.DataObject.RelocationLastError = RelocationError.None;
			if (base.Fields.IsChanged(TenantRelocationRequestSchema.RelocationStateRequested))
			{
				this.DataObject.RelocationStateRequested = (RelocationStateRequested)this.RelocationStateRequested;
			}
			if (base.Fields.IsChanged(TenantRelocationRequestSchema.LargeTenantModeEnabled))
			{
				this.DataObject.LargeTenantModeEnabled = this.LargeTenantModeEnabled;
			}
			if (base.Fields.IsChanged(TenantRelocationRequestSchema.AutoCompletionEnabled))
			{
				this.DataObject.AutoCompletionEnabled = this.AutoCompletionEnabled;
				if (this.AutoCompletionEnabled)
				{
					this.DataObject.RelocationStateRequested = Microsoft.Exchange.Data.Directory.SystemConfiguration.RelocationStateRequested.None;
				}
			}
			if (base.Fields.IsChanged(TenantRelocationRequestSchema.SafeLockdownSchedule))
			{
				this.DataObject.SafeLockdownSchedule = this.SafeLockdownSchedule;
			}
			if (this.Suspend)
			{
				this.DataObject.Suspended = true;
			}
			else if (this.Resume)
			{
				this.DataObject.Suspended = false;
			}
			if (this.applyTransitionFromCmdlet)
			{
				Exception ex;
				TenantRelocationRequest.PopulatePresentationObject(this.DataObject, null, out ex);
				if (ex != null)
				{
					throw ex;
				}
				RelocationStatusDetails relocationStatusDetailsRaw = this.DataObject.RelocationStatusDetailsRaw;
				this.DataObject.RelocationStatusDetailsRaw = RelocationStatusDetails.SynchronizationFinishedFullSync;
				this.DataObject.AutoCompletionEnabled = false;
				if (this.RollbackGls)
				{
					this.RevertGlsAccountForestToSource(forestFQDN);
					this.RevertTargetTenantStateToArriving();
					this.DataObject.IncrementTransitionCounter(TenantRelocationTransition.RetiredToSync);
					this.DataObject.OrganizationStatus = OrganizationStatus.Active;
					TenantRelocationRequest.SetRelocationCompletedOnOU((ITenantConfigurationSession)base.DataSession, this.DataObject.OrganizationId);
				}
				else
				{
					TenantRelocationTransition transition;
					if (relocationStatusDetailsRaw == RelocationStatusDetails.SynchronizationFinishedDeltaSync)
					{
						transition = TenantRelocationTransition.DeltaSyncToSync;
					}
					else
					{
						transition = TenantRelocationTransition.LockdownToSync;
					}
					this.DataObject.IncrementTransitionCounter(transition);
				}
			}
			if (this.ResetTransitionCounter)
			{
				this.DataObject.TransitionCounter = new MultiValuedProperty<TransitionCount>();
			}
			if (this.ResetPermanentError)
			{
				this.DataObject.RelocationLastError = RelocationError.None;
			}
			if (this.ResetStartSyncTime)
			{
				this.DataObject.LastSuccessfulRelocationSyncStart = new DateTime?(DateTime.UtcNow);
			}
			((IDirectorySession)base.DataSession).SessionSettings.RetiredTenantModificationAllowed = true;
			base.InternalProcessRecord();
			string ridMasterName = ForestTenantRelocationsCache.GetRidMasterName(new PartitionId(forestFQDN));
			if (this.DataObject.OriginatingServer != ridMasterName)
			{
				this.WriteWarning(Strings.WarningShouldWriteToRidMaster(this.DataObject.OriginatingServer, ridMasterName));
			}
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x000FFDC8 File Offset: 0x000FDFC8
		private void RevertGlsAccountForestToSource(string sourceForest)
		{
			if (!ADSessionSettings.IsGlsDisabled)
			{
				Guid externalDirectoryOrganizationId = new Guid(this.DataObject.ExternalDirectoryOrganizationId);
				GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
				glsDirectorySession.SetAccountForest(externalDirectoryOrganizationId, sourceForest, this.DataObject.OrganizationId.OrganizationalUnit.Name);
			}
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x000FFE14 File Offset: 0x000FE014
		private void RevertTargetTenantStateToArriving()
		{
			ITenantConfigurationSession tenantConfigurationSession = SetTenantRelocationRequest.CreateWritableTenantSession(this.DataObject.TargetOrganizationId);
			TenantRelocationRequest tenantRelocationRequest = tenantConfigurationSession.Read<TenantRelocationRequest>(this.DataObject.TargetOrganizationId.ConfigurationUnit);
			tenantRelocationRequest.RelocationStatusDetailsRaw = RelocationStatusDetails.Arriving;
			tenantRelocationRequest.OrganizationStatus = OrganizationStatus.PendingArrival;
			tenantConfigurationSession.Save(tenantRelocationRequest);
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x000FFE60 File Offset: 0x000FE060
		private static ITenantConfigurationSession CreateWritableTenantSession(OrganizationId organizationId)
		{
			string ridMasterName = ForestTenantRelocationsCache.GetRidMasterName(organizationId.PartitionId);
			ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId, false);
			adsessionSettings.RetiredTenantModificationAllowed = true;
			adsessionSettings.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
			return DirectorySessionFactory.Default.CreateTenantConfigurationSession(ridMasterName, false, ConsistencyMode.PartiallyConsistent, adsessionSettings, 541, "CreateWritableTenantSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Relocation\\SetTenantRelocationRequest.cs");
		}

		// Token: 0x040026D9 RID: 9945
		private const string DefaultParameterSet = "DefaultParameterSet";

		// Token: 0x040026DA RID: 9946
		private const string SuspendParameterSet = "SuspendParameterSet";

		// Token: 0x040026DB RID: 9947
		private const string ResumeParameterSet = "ResumeParameterSet";

		// Token: 0x040026DC RID: 9948
		private const string ResetTransitionCounterParameterSet = "ResetTransitionCounterParameterSet";

		// Token: 0x040026DD RID: 9949
		private const string ResetPermanentErrorParameterSet = "ResetPermanentErrorParameterSet";

		// Token: 0x040026DE RID: 9950
		private const string ResetStartSyncTimeParameter = "ResetStartSyncTimeParameter";

		// Token: 0x040026DF RID: 9951
		private const string SuspendParameter = "SuspendParameter";

		// Token: 0x040026E0 RID: 9952
		private const string ResumeParameter = "ResumeParameter";

		// Token: 0x040026E1 RID: 9953
		private const string RollbackGlsParameter = "RollbackGlsParameter";

		// Token: 0x040026E2 RID: 9954
		private const string ResetTransitionCounterParameter = "ResetTransitionCounterParameter";

		// Token: 0x040026E3 RID: 9955
		private const string ResetPermanentErrorParameter = "ResetPermanentErrorParameter";

		// Token: 0x040026E4 RID: 9956
		private bool applyTransitionFromCmdlet;
	}
}
