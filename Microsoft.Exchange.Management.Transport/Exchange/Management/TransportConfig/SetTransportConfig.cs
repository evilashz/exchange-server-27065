using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies;
using Microsoft.Exchange.MessagingPolicies.Journaling;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.TransportConfig
{
	// Token: 0x020000B6 RID: 182
	[Cmdlet("Set", "TransportConfig", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetTransportConfig : SetMultitenancySingletonSystemConfigurationObjectTask<TransportConfigContainer>
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001AB30 File Offset: 0x00018D30
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x0001AB47 File Offset: 0x00018D47
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan QueueDiagnosticsAggregationInterval
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["QueueDiagnosticsAggregationInterval"];
			}
			set
			{
				base.Fields["QueueDiagnosticsAggregationInterval"] = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0001AB5F File Offset: 0x00018D5F
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x0001AB76 File Offset: 0x00018D76
		[Parameter(Mandatory = false)]
		public int DiagnosticsAggregationServicePort
		{
			get
			{
				return (int)base.Fields["DiagnosticsAggregationServicePort"];
			}
			set
			{
				base.Fields["DiagnosticsAggregationServicePort"] = value;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001AB8E File Offset: 0x00018D8E
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x0001ABA5 File Offset: 0x00018DA5
		[Parameter(Mandatory = false)]
		public bool AgentGeneratedMessageLoopDetectionInSubmissionEnabled
		{
			get
			{
				return (bool)base.Fields["AgentGeneratedMessageLoopDetectionInSubmissionEnabled"];
			}
			set
			{
				base.Fields["AgentGeneratedMessageLoopDetectionInSubmissionEnabled"] = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0001ABBD File Offset: 0x00018DBD
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x0001ABD4 File Offset: 0x00018DD4
		[Parameter(Mandatory = false)]
		public bool AgentGeneratedMessageLoopDetectionInSmtpEnabled
		{
			get
			{
				return (bool)base.Fields["AgentGeneratedMessageLoopDetectionInSmtpEnabled"];
			}
			set
			{
				base.Fields["AgentGeneratedMessageLoopDetectionInSmtpEnabled"] = value;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001ABEC File Offset: 0x00018DEC
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x0001AC03 File Offset: 0x00018E03
		[Parameter(Mandatory = false)]
		public uint MaxAllowedAgentGeneratedMessageDepth
		{
			get
			{
				return (uint)base.Fields["MaxAllowedAgentGeneratedMessageDepth"];
			}
			set
			{
				base.Fields["MaxAllowedAgentGeneratedMessageDepth"] = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001AC1B File Offset: 0x00018E1B
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x0001AC32 File Offset: 0x00018E32
		[Parameter(Mandatory = false)]
		public uint MaxAllowedAgentGeneratedMessageDepthPerAgent
		{
			get
			{
				return (uint)base.Fields["MaxAllowedAgentGeneratedMessageDepthPerAgent"];
			}
			set
			{
				base.Fields["MaxAllowedAgentGeneratedMessageDepthPerAgent"] = value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001AC4A File Offset: 0x00018E4A
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x0001AC70 File Offset: 0x00018E70
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? false);
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001AC88 File Offset: 0x00018E88
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetTransportConfig;
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001AC90 File Offset: 0x00018E90
		protected override void InternalValidate()
		{
			base.InternalValidate();
			TransportConfigContainer transportConfigContainer = (TransportConfigContainer)this.DataObject.GetOriginalObject();
			if (!(this.DataObject.Schema is TransportConfigContainerSchema))
			{
				if (this.DataObject.IsModified(TransportConfigContainerSchema.AnonymousSenderToRecipientRatePerHour) || this.DataObject.IsModified(TransportConfigContainerSchema.MaxDumpsterSizePerDatabase) || this.DataObject.IsModified(TransportConfigContainerSchema.MaxDumpsterTime) || this.DataObject.IsModified(TransportConfigContainerSchema.MaxReceiveSize) || this.DataObject.IsModified(TransportConfigContainerSchema.MaxRecipientEnvelopeLimit) || this.DataObject.IsModified(TransportConfigContainerSchema.SupervisionTags) || this.DataObject.IsModified(TransportConfigContainerSchema.ShadowHeartbeatFrequency) || this.DataObject.IsModified(TransportConfigContainerSchema.ShadowResubmitTimeSpan) || base.Fields.IsModified("QueueDiagnosticsAggregationInterval") || base.Fields.IsModified("DiagnosticsAggregationServicePort") || base.Fields.IsModified("AgentGeneratedMessageLoopDetectionInSubmissionEnabled") || base.Fields.IsModified("AgentGeneratedMessageLoopDetectionInSmtpEnabled") || base.Fields.IsModified("MaxAllowedAgentGeneratedMessageDepth") || base.Fields.IsModified("MaxAllowedAgentGeneratedMessageDepthPerAgent"))
				{
					this.WriteWarning(Strings.WarningUnsupportedEdgeTransportConfigProperty);
				}
				if (Server.IsSubscribedGateway(base.GlobalConfigSession))
				{
					ADPropertyDefinition[] array = new ADPropertyDefinition[]
					{
						ADAMTransportConfigContainerSchema.InternalSMTPServers,
						ADAMTransportConfigContainerSchema.TLSReceiveDomainSecureList,
						ADAMTransportConfigContainerSchema.TLSSendDomainSecureList,
						ADAMTransportConfigContainerSchema.ShadowHeartbeatRetryCount,
						ADAMTransportConfigContainerSchema.ShadowHeartbeatTimeoutInterval,
						ADAMTransportConfigContainerSchema.ShadowMessageAutoDiscardInterval,
						ADAMTransportConfigContainerSchema.RejectMessageOnShadowFailure,
						ADAMTransportConfigContainerSchema.ShadowMessagePreferenceSetting,
						ADAMTransportConfigContainerSchema.MaxRetriesForLocalSiteShadow,
						ADAMTransportConfigContainerSchema.MaxRetriesForRemoteSiteShadow
					};
					foreach (ADPropertyDefinition adpropertyDefinition in array)
					{
						if (this.DataObject.IsModified(adpropertyDefinition))
						{
							base.WriteError(new CannotSetTransportServerPropertyOnSubscribedEdgeException(adpropertyDefinition.Name), ErrorCategory.InvalidOperation, base.Identity);
						}
					}
					this.ValidateFlagSettingUnchanged<bool>(transportConfigContainer, ADAMTransportConfigContainerSchema.ShadowRedundancyDisabled, "ShadowRedundancyEnabled");
					this.ValidateFlagSettingUnchanged<ShadowMessagePreference>(transportConfigContainer, ADAMTransportConfigContainerSchema.ShadowMessagePreferenceSetting, "ShadowMessagePreferenceSetting");
					this.ValidateFlagSettingUnchanged<bool>(transportConfigContainer, ADAMTransportConfigContainerSchema.RejectMessageOnShadowFailure, "RejectMessageOnShadowFailure");
				}
			}
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				this.ValidateLegacyArchiveLiveJournalingConfiguration(transportConfigContainer);
				if (this.DataObject.IsModified(ADAMTransportConfigContainerSchema.JournalingReportNdrTo))
				{
					SmtpAddress journalingReportNdrTo = this.DataObject.JournalingReportNdrTo;
					if (this.DataObject.JournalingReportNdrTo.IsValidAddress)
					{
						JournalNdrValidationCheckResult journalNdrValidationCheckResult = JournalRuleObject.ValidateJournalNdrMailboxSetting(base.DataSession, this.DataObject.JournalingReportNdrTo);
						if (journalNdrValidationCheckResult == JournalNdrValidationCheckResult.JournalNdrCannotBeNullReversePath)
						{
							base.WriteError(new InvalidOperationException(Strings.JournalNdrMailboxCannotBeNull), ErrorCategory.InvalidOperation, null);
						}
						else if (journalNdrValidationCheckResult == JournalNdrValidationCheckResult.JournalNdrExistInJournalRuleRecipient)
						{
							base.WriteError(new InvalidOperationException(Strings.JournalNdrMailboxInJournalRuleRecipient), ErrorCategory.InvalidOperation, null);
						}
						else if (journalNdrValidationCheckResult == JournalNdrValidationCheckResult.JournalNdrExistInJournalRuleJournalEmailAddress)
						{
							this.WriteWarning(Strings.JournalNdrMailboxInJournalRuleJournalEmailAddress);
						}
					}
				}
			}
			this.WarnForJournalNdrMailboxSetting(transportConfigContainer);
			if (this.DataObject.IsModified(ADAMTransportConfigContainerSchema.TLSReceiveDomainSecureList) && this.DataObject.TLSReceiveDomainSecureList.Count > 256)
			{
				base.WriteError(new ExceededMaximumCollectionCountException(ADAMTransportConfigContainerSchema.TLSReceiveDomainSecureList.Name, 256, this.DataObject.TLSReceiveDomainSecureList.Count), ErrorCategory.InvalidOperation, base.Identity);
			}
			if (this.DataObject.IsModified(ADAMTransportConfigContainerSchema.TLSSendDomainSecureList) && this.DataObject.TLSSendDomainSecureList.Count > 256)
			{
				base.WriteError(new ExceededMaximumCollectionCountException(ADAMTransportConfigContainerSchema.TLSSendDomainSecureList.Name, 256, this.DataObject.TLSSendDomainSecureList.Count), ErrorCategory.InvalidOperation, base.Identity);
			}
			if (this.DataObject.IsModified(ADAMTransportConfigContainerSchema.ExternalPostmasterAddress) && this.DataObject.ExternalPostmasterAddress != null && (!this.DataObject.ExternalPostmasterAddress.Value.IsValidAddress || this.DataObject.ExternalPostmasterAddress.Value == SmtpAddress.NullReversePath))
			{
				base.WriteError(new InvalidPostMasterAddressException(), ErrorCategory.InvalidOperation, base.Identity);
			}
			if (this.DataObject.IsModified(TransportConfigContainerSchema.TransportRuleRegexValidationTimeout) && this.DataObject.TransportRuleRegexValidationTimeout.TotalMilliseconds <= 0.0)
			{
				base.WriteError(new InvalidArgumentException("TransportRuleRegexValidationTimeout"), ErrorCategory.InvalidArgument, this.DataObject.TransportRuleRegexValidationTimeout);
			}
			if (this.DataObject.IsChanged(TransportConfigContainerSchema.SupervisionTags))
			{
				HashSet<string> hashSet = new HashSet<string>(transportConfigContainer.SupervisionTags, StringComparer.OrdinalIgnoreCase);
				foreach (string item in this.DataObject.SupervisionTags)
				{
					hashSet.Remove(item);
				}
				if (hashSet.Count != 0)
				{
					this.WriteWarning(Strings.WarningSupervisionTagsRemoved);
				}
			}
			this.ValidateShadowRedundancyPreference(transportConfigContainer);
			if (this.DataObject.IsModified(ADAMTransportConfigContainerSchema.SafetyNetHoldTime))
			{
				this.WriteWarning(Strings.WarningSafetyNetHoldTimeMustBeGreaterThanReplayLagTime);
			}
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.LimitTransportRules.Enabled && this.DataObject.IsModified(TransportConfigContainerSchema.TransportRuleLimit))
			{
				int num = (int)this.DataObject[TransportConfigContainerSchema.TransportRuleLimit];
				int num2 = 0;
				try
				{
					ADRuleStorageManager adruleStorageManager = new ADRuleStorageManager(Utils.RuleCollectionNameFromRole(), base.DataSession);
					adruleStorageManager.LoadRuleCollection();
					num2 = adruleStorageManager.Count;
				}
				catch (RuleCollectionNotInAdException)
				{
				}
				if (num2 > num)
				{
					base.WriteError(new TransportRuleLimitTooSmallException(num, num2), ErrorCategory.InvalidOperation, base.Identity);
				}
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001B250 File Offset: 0x00019450
		private void WarnForJournalNdrMailboxSetting(TransportConfigContainer originalObject)
		{
			if (this.DataObject.IsModified(ADAMTransportConfigContainerSchema.JournalingReportNdrTo))
			{
				SmtpAddress journalingReportNdrTo = this.DataObject.JournalingReportNdrTo;
				if (this.DataObject.JournalingReportNdrTo.IsValidAddress)
				{
					RoutingAddress value = new RoutingAddress(this.DataObject.JournalingReportNdrTo.ToString());
					if (value != RoutingAddress.NullReversePath)
					{
						RecipientIdParameter recipId = new RecipientIdParameter(this.DataObject.JournalingReportNdrTo.ToString());
						SmtpAddress smtpAddress;
						if (!JournalRuleObject.LookupAndCheckAllowedTypes(recipId, base.TenantGlobalCatalogSession, this.DataObject.OrganizationId, true, out smtpAddress))
						{
							this.WriteWarning(Strings.JournalNdrMailboxWarning);
						}
					}
				}
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001B308 File Offset: 0x00019508
		protected override void InternalProcessRecord()
		{
			if (!this.Force && this.originalJournalArchivingEnabled != this.DataObject.JournalArchivingEnabled && !this.DataObject.JournalArchivingEnabled && !base.ShouldContinue(Strings.ConfirmationTurnOffJournalArchiving))
			{
				TaskLogger.LogExit();
				return;
			}
			if (base.Fields.IsModified("DiagnosticsAggregationServicePort"))
			{
				this.DataObject.DiagnosticsAggregationServicePort = this.DiagnosticsAggregationServicePort;
			}
			if (base.Fields.IsModified("QueueDiagnosticsAggregationInterval"))
			{
				this.DataObject.QueueDiagnosticsAggregationInterval = this.QueueDiagnosticsAggregationInterval;
			}
			if (base.Fields.IsModified("AgentGeneratedMessageLoopDetectionInSubmissionEnabled"))
			{
				this.DataObject.AgentGeneratedMessageLoopDetectionInSubmissionEnabled = this.AgentGeneratedMessageLoopDetectionInSubmissionEnabled;
			}
			if (base.Fields.IsModified("AgentGeneratedMessageLoopDetectionInSmtpEnabled"))
			{
				this.DataObject.AgentGeneratedMessageLoopDetectionInSmtpEnabled = this.AgentGeneratedMessageLoopDetectionInSmtpEnabled;
			}
			if (base.Fields.IsModified("MaxAllowedAgentGeneratedMessageDepth"))
			{
				this.DataObject.MaxAllowedAgentGeneratedMessageDepth = this.MaxAllowedAgentGeneratedMessageDepth;
			}
			if (base.Fields.IsModified("MaxAllowedAgentGeneratedMessageDepthPerAgent"))
			{
				this.DataObject.MaxAllowedAgentGeneratedMessageDepthPerAgent = this.MaxAllowedAgentGeneratedMessageDepthPerAgent;
			}
			this.UpdateTransportRuleSettings();
			bool flag = this.DataObject.IsModified(TransportConfigContainerSchema.MaxRecipientEnvelopeLimit);
			bool flag2 = this.DataObject.IsModified(TransportConfigContainerSchema.MaxReceiveSize);
			bool flag3 = this.DataObject.IsModified(ADAMTransportConfigContainerSchema.MaxSendSize);
			base.InternalProcessRecord();
			if (flag || flag2 || flag3)
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				MessageDeliveryGlobalSettings[] array = configurationSession.Find<MessageDeliveryGlobalSettings>(configurationSession.GetOrgContainerId(), QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, MessageDeliveryGlobalSettings.DefaultName), null, 1);
				if (array.Length > 0)
				{
					if (flag)
					{
						array[0].MaxRecipientEnvelopeLimit = this.DataObject.MaxRecipientEnvelopeLimit;
					}
					if (flag2)
					{
						array[0].MaxReceiveSize = this.DataObject.MaxReceiveSize;
					}
					if (flag3)
					{
						array[0].MaxSendSize = this.DataObject.MaxSendSize;
					}
					configurationSession.Save(array[0]);
				}
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001B4F8 File Offset: 0x000196F8
		protected override void ResolveCurrentOrgIdBasedOnIdentity(IIdentityParameter identity)
		{
			try
			{
				base.ResolveCurrentOrgIdBasedOnIdentity(identity);
			}
			catch (ManagementObjectNotFoundException)
			{
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001B524 File Offset: 0x00019724
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TransportConfigContainer transportConfigContainer = (TransportConfigContainer)dataObject;
			this.originalJournalArchivingEnabled = transportConfigContainer.JournalArchivingEnabled;
			base.StampChangesOn(dataObject);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001B54C File Offset: 0x0001974C
		private void ValidateLegacyArchiveLiveJournalingConfiguration(TransportConfigContainer originalObject)
		{
			if (this.DataObject.IsModified(ADAMTransportConfigContainerSchema.LegacyArchiveLiveJournalingEnabled) && !this.DataObject.IsModified(ADAMTransportConfigContainerSchema.JournalingReportNdrTo))
			{
				if (this.DataObject.LegacyArchiveLiveJournalingEnabled && (!originalObject.JournalingReportNdrTo.IsValidAddress || originalObject.JournalingReportNdrTo == SmtpAddress.NullReversePath))
				{
					base.WriteError(new InvalidLegacyArchiveJournalingConfigurationException(Strings.ErrorCannotSetLegacyArchiveJournalingEnabled), ErrorCategory.InvalidOperation, base.Identity);
				}
			}
			else if (!this.DataObject.IsModified(ADAMTransportConfigContainerSchema.LegacyArchiveLiveJournalingEnabled) && this.DataObject.IsModified(ADAMTransportConfigContainerSchema.JournalingReportNdrTo))
			{
				if (originalObject.LegacyArchiveLiveJournalingEnabled && (!this.DataObject.JournalingReportNdrTo.IsValidAddress || this.DataObject.JournalingReportNdrTo == SmtpAddress.NullReversePath))
				{
					base.WriteError(new InvalidLegacyArchiveJournalingConfigurationException(Strings.ErrorCannotSetJournalingReportNdrTo), ErrorCategory.InvalidOperation, base.Identity);
				}
			}
			else if (this.DataObject.IsModified(ADAMTransportConfigContainerSchema.LegacyArchiveLiveJournalingEnabled) && this.DataObject.IsModified(ADAMTransportConfigContainerSchema.JournalingReportNdrTo) && this.DataObject.LegacyArchiveLiveJournalingEnabled && (!this.DataObject.JournalingReportNdrTo.IsValidAddress || this.DataObject.JournalingReportNdrTo == SmtpAddress.NullReversePath))
			{
				base.WriteError(new InvalidLegacyArchiveJournalingConfigurationException(Strings.ErrorCannotSetJournalingReportNdrTo), ErrorCategory.InvalidOperation, base.Identity);
			}
			if ((this.DataObject.IsModified(ADAMTransportConfigContainerSchema.LegacyArchiveLiveJournalingEnabled) || this.DataObject.IsModified(ADAMTransportConfigContainerSchema.JournalArchivingEnabled)) && this.DataObject.LegacyArchiveLiveJournalingEnabled && this.DataObject.JournalArchivingEnabled)
			{
				base.WriteError(new InvalidLegacyArchiveJournalingConfigurationException(Strings.ErrorCannotEnableJournalArchive), ErrorCategory.InvalidOperation, base.Identity);
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001B70E File Offset: 0x0001990E
		private void ValidateFlagSettingUnchanged<T>(TransportConfigContainer originalObject, ADPropertyDefinition config, string configName) where T : struct
		{
			if (this.IsFlagValueModified<T>(originalObject, config))
			{
				base.WriteError(new CannotSetTransportServerPropertyOnSubscribedEdgeException(configName), ErrorCategory.InvalidOperation, base.Identity);
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001B730 File Offset: 0x00019930
		private bool IsFlagValueModified<T>(TransportConfigContainer originalObject, ADPropertyDefinition config) where T : struct
		{
			T t = (T)((object)this.DataObject[config]);
			T t2 = (T)((object)originalObject[config]);
			return !t2.Equals(t);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001B774 File Offset: 0x00019974
		private void ValidateShadowRedundancyPreference(TransportConfigContainer originalObject)
		{
			if (this.DataObject.IsModified(ADAMTransportConfigContainerSchema.MaxRetriesForRemoteSiteShadow) || this.DataObject.IsModified(ADAMTransportConfigContainerSchema.MaxRetriesForLocalSiteShadow) || this.IsFlagValueModified<ShadowMessagePreference>(originalObject, ADAMTransportConfigContainerSchema.ShadowMessagePreferenceSetting))
			{
				int value = (int)this.DataObject[ADAMTransportConfigContainerSchema.MaxRetriesForLocalSiteShadow];
				int value2 = (int)this.DataObject[ADAMTransportConfigContainerSchema.MaxRetriesForRemoteSiteShadow];
				ShadowMessagePreference shadowMessagePreference = (ShadowMessagePreference)this.DataObject[ADAMTransportConfigContainerSchema.ShadowMessagePreferenceSetting];
				if (ShadowMessagePreference.LocalOnly == shadowMessagePreference)
				{
					this.ValidateShadowRetryValueZero(value2, "MaxRetriesForRemoteSiteShadow", shadowMessagePreference);
					this.ValidateShadowRetryValueGreaterThanZero(value, "MaxRetriesForLocalSiteShadow", shadowMessagePreference);
					return;
				}
				if (ShadowMessagePreference.RemoteOnly == shadowMessagePreference)
				{
					this.ValidateShadowRetryValueZero(value, "MaxRetriesForLocalSiteShadow", shadowMessagePreference);
					this.ValidateShadowRetryValueGreaterThanZero(value2, "MaxRetriesForRemoteSiteShadow", shadowMessagePreference);
					return;
				}
				if (shadowMessagePreference == ShadowMessagePreference.PreferRemote)
				{
					this.ValidateShadowRetryValueGreaterThanZero(value, "MaxRetriesForLocalSiteShadow", shadowMessagePreference);
					this.ValidateShadowRetryValueGreaterThanZero(value2, "MaxRetriesForRemoteSiteShadow", shadowMessagePreference);
				}
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001B853 File Offset: 0x00019A53
		private void ValidateShadowRetryValueZero(int value, string name, ShadowMessagePreference preference)
		{
			if (value != 0)
			{
				base.WriteError(new InvalidShadowRedundancyConfigurationException(Strings.ErrorNonZeroValueForShadowRetry(name, preference.ToString())), ErrorCategory.InvalidOperation, base.Identity);
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001B87B File Offset: 0x00019A7B
		private void ValidateShadowRetryValueGreaterThanZero(int value, string name, ShadowMessagePreference preference)
		{
			if (value <= 0)
			{
				base.WriteError(new InvalidShadowRedundancyConfigurationException(Strings.ErrorZeroOrLessValueForShadowRetry(name, preference.ToString())), ErrorCategory.InvalidOperation, base.Identity);
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001B8A4 File Offset: 0x00019AA4
		private void UpdateTransportRuleSettings()
		{
			if (this.DataObject.IsModified(TransportConfigContainerSchema.TransportRuleCollectionAddedRecipientsLimit))
			{
				MultivaluedPropertyAccessors.UpdateMultivaluedProperty<object>(this.DataObject[TransportConfigContainerSchema.TransportRuleCollectionAddedRecipientsLimit], TransportConfigContainerSchema.TransportRuleCollectionAddedRecipientsLimit.Name, this.DataObject.TransportRuleConfig);
			}
			if (this.DataObject.IsModified(TransportConfigContainerSchema.TransportRuleLimit))
			{
				MultivaluedPropertyAccessors.UpdateMultivaluedProperty<object>(this.DataObject[TransportConfigContainerSchema.TransportRuleLimit], TransportConfigContainerSchema.TransportRuleLimit.Name, this.DataObject.TransportRuleConfig);
			}
			if (this.DataObject.IsModified(TransportConfigContainerSchema.TransportRuleCollectionRegexCharsLimit))
			{
				MultivaluedPropertyAccessors.UpdateMultivaluedProperty<object>(this.DataObject[TransportConfigContainerSchema.TransportRuleCollectionRegexCharsLimit], TransportConfigContainerSchema.TransportRuleCollectionRegexCharsLimit.Name, this.DataObject.TransportRuleConfig);
			}
			if (this.DataObject.IsModified(TransportConfigContainerSchema.TransportRuleSizeLimit))
			{
				MultivaluedPropertyAccessors.UpdateMultivaluedProperty<object>(this.DataObject[TransportConfigContainerSchema.TransportRuleSizeLimit], TransportConfigContainerSchema.TransportRuleSizeLimit.Name, this.DataObject.TransportRuleConfig);
			}
			if (this.DataObject.IsModified(TransportConfigContainerSchema.TransportRuleAttachmentTextScanLimit))
			{
				MultivaluedPropertyAccessors.UpdateMultivaluedProperty<object>(this.DataObject[TransportConfigContainerSchema.TransportRuleAttachmentTextScanLimit], TransportConfigContainerSchema.TransportRuleAttachmentTextScanLimit.Name, this.DataObject.TransportRuleConfig);
			}
			if (this.DataObject.IsModified(TransportConfigContainerSchema.TransportRuleRegexValidationTimeout))
			{
				MultivaluedPropertyAccessors.UpdateMultivaluedProperty<object>(this.DataObject[TransportConfigContainerSchema.TransportRuleRegexValidationTimeout], TransportConfigContainerSchema.TransportRuleRegexValidationTimeout.Name, this.DataObject.TransportRuleConfig);
			}
			if (this.DataObject.IsModified(TransportConfigContainerSchema.TransportRuleMinProductVersion))
			{
				MultivaluedPropertyAccessors.UpdateMultivaluedProperty<object>(this.DataObject[TransportConfigContainerSchema.TransportRuleMinProductVersion], TransportConfigContainerSchema.TransportRuleMinProductVersion.Name, this.DataObject.TransportRuleConfig);
			}
		}

		// Token: 0x04000262 RID: 610
		private const string QueueDiagnosticsAggregationIntervalKey = "QueueDiagnosticsAggregationInterval";

		// Token: 0x04000263 RID: 611
		private const string DiagnosticsAggregationServicePortKey = "DiagnosticsAggregationServicePort";

		// Token: 0x04000264 RID: 612
		private const string AgentGeneratedMessageLoopDetectionInSubmissionEnabledKey = "AgentGeneratedMessageLoopDetectionInSubmissionEnabled";

		// Token: 0x04000265 RID: 613
		private const string AgentGeneratedMessageLoopDetectionInSmtpEnabledKey = "AgentGeneratedMessageLoopDetectionInSmtpEnabled";

		// Token: 0x04000266 RID: 614
		private const string MaxAllowedAgentGeneratedMessageDepthKey = "MaxAllowedAgentGeneratedMessageDepth";

		// Token: 0x04000267 RID: 615
		private const string MaxAllowedAgentGeneratedMessageDepthPerAgentKey = "MaxAllowedAgentGeneratedMessageDepthPerAgent";

		// Token: 0x04000268 RID: 616
		private bool originalJournalArchivingEnabled;
	}
}
