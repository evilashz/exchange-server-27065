using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x0200004C RID: 76
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxPolicyConstraint
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00017CFE File Offset: 0x00015EFE
		internal static MailboxPolicyConstraint Instance
		{
			get
			{
				return MailboxPolicyConstraint.instance;
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00017D05 File Offset: 0x00015F05
		private MailboxPolicyConstraint()
		{
			this.probeInterval = ContentAggregationConfig.OwaMailboxPolicyProbeInterval;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00017D18 File Offset: 0x00015F18
		internal bool WantsDispositionChangedToDeletion(DispatchEntry dispatchEntry, SyncLogSession syncLogSession)
		{
			return this.IsRelevantSubscriptionType(dispatchEntry) && this.PolicyCheckIsWarranted(dispatchEntry) && this.PolicyIsDisabledFor(dispatchEntry, syncLogSession);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00017D38 File Offset: 0x00015F38
		private bool IsRelevantSubscriptionType(DispatchEntry dispatchEntry)
		{
			AggregationSubscriptionType subscriptionType = dispatchEntry.MiniSubscriptionInformation.SubscriptionType;
			return AggregationSubscriptionType.AllThatSupportPolicyInducedDeletion.HasFlag(subscriptionType);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00017D65 File Offset: 0x00015F65
		internal bool PolicyCheckIsWarranted(DispatchEntry dispatchEntry)
		{
			return dispatchEntry.WorkType != WorkType.PolicyInducedDelete && this.RequisiteTimeHasElapsed(dispatchEntry);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00017D7C File Offset: 0x00015F7C
		internal bool RequisiteTimeHasElapsed(DispatchEntry dispatchEntry)
		{
			ExDateTime nextOwaMailboxPolicyProbeTime = dispatchEntry.MiniSubscriptionInformation.NextOwaMailboxPolicyProbeTime;
			return ExDateTime.Compare(ExDateTime.UtcNow, nextOwaMailboxPolicyProbeTime) > 0;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00017DA4 File Offset: 0x00015FA4
		private bool PolicyIsDisabledFor(DispatchEntry dispatchEntry, SyncLogSession syncLogSession)
		{
			ConnectSubscriptionPolicySettings connectSubscriptionPolicySettings = this.PolicySettingsFor(dispatchEntry, syncLogSession);
			AggregationSubscriptionType subscriptionType = dispatchEntry.MiniSubscriptionInformation.SubscriptionType;
			AggregationSubscriptionType aggregationSubscriptionType = subscriptionType;
			if (aggregationSubscriptionType == AggregationSubscriptionType.Facebook)
			{
				return connectSubscriptionPolicySettings.IsFacebookDisabled;
			}
			if (aggregationSubscriptionType != AggregationSubscriptionType.LinkedIn)
			{
				throw new ArgumentException("Unsupported subscriptionType:" + subscriptionType);
			}
			return connectSubscriptionPolicySettings.IsLinkedInDisabled;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00017DF8 File Offset: 0x00015FF8
		private ConnectSubscriptionPolicySettings PolicySettingsFor(DispatchEntry dispatchEntry, SyncLogSession syncLogSession)
		{
			MiniSubscriptionInformation miniSubscriptionInformation = dispatchEntry.MiniSubscriptionInformation;
			miniSubscriptionInformation.NextOwaMailboxPolicyProbeTime = ExDateTime.UtcNow + this.probeInterval;
			if (!miniSubscriptionInformation.ExternalDirectoryOrgId.Equals(Guid.Empty))
			{
				try
				{
					ADSessionSettings adsessionSettings = ADSessionSettings.FromExternalDirectoryOrganizationId(miniSubscriptionInformation.ExternalDirectoryOrgId);
					ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromMailboxGuid(adsessionSettings, miniSubscriptionInformation.MailboxGuid, null);
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, adsessionSettings, 143, "PolicySettingsFor", "f:\\15.00.1497\\sources\\dev\\transportSync\\src\\Manager\\Throttling\\MailboxPolicyConstraint.cs");
					OwaSegmentationSettings owaSegmentationSettings = OwaSegmentationSettings.GetInstance(tenantOrTopologyConfigurationSession, exchangePrincipal.MailboxInfo.Configuration.OwaMailboxPolicy, exchangePrincipal.MailboxInfo.OrganizationId);
					return new ConnectSubscriptionPolicySettings(owaSegmentationSettings);
				}
				catch (LocalizedException ex)
				{
					syncLogSession.LogError((TSLID)238UL, "MPC.PolicySettingsFor: exception trying to read mailbox policy, ex:{0}", new object[]
					{
						ex
					});
				}
			}
			return ConnectSubscriptionPolicySettings.GetFallbackInstance();
		}

		// Token: 0x0400021A RID: 538
		private static readonly MailboxPolicyConstraint instance = new MailboxPolicyConstraint();

		// Token: 0x0400021B RID: 539
		private readonly TimeSpan probeInterval;
	}
}
