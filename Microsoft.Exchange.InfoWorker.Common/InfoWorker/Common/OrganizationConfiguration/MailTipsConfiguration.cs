using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.MailTips;
using Microsoft.Exchange.MessagingPolicies.Rules.PolicyNudges;

namespace Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration
{
	// Token: 0x0200014A RID: 330
	internal class MailTipsConfiguration
	{
		// Token: 0x06000906 RID: 2310 RVA: 0x00026EB4 File Offset: 0x000250B4
		public MailTipsConfiguration(int traceId)
		{
			this.traceId = traceId;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00026EC3 File Offset: 0x000250C3
		public MailTipsConfiguration(int maxMessageSize, int largeAudienceThreshold, bool showExternalRecipientCount, bool policyTipsEnabled)
		{
			this.maxMessageSize = maxMessageSize;
			this.largeAudienceThreshold = largeAudienceThreshold;
			this.showExternalRecipientCount = showExternalRecipientCount;
			this.policyTipsEnabled = policyTipsEnabled;
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00026EE8 File Offset: 0x000250E8
		public int MaxMessageSize
		{
			get
			{
				return this.maxMessageSize;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00026EF0 File Offset: 0x000250F0
		public int LargeAudienceThreshold
		{
			get
			{
				return this.largeAudienceThreshold;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00026EF8 File Offset: 0x000250F8
		public bool ShowExternalRecipientCount
		{
			get
			{
				return this.showExternalRecipientCount;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00026F00 File Offset: 0x00025100
		public bool PolicyTipsEnabled
		{
			get
			{
				return this.policyTipsEnabled;
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00026F10 File Offset: 0x00025110
		public void Initialize(CachedOrganizationConfiguration configuration, ADRawEntry sender)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (sender == null)
			{
				throw new ArgumentNullException("sender");
			}
			MailTipsConfiguration.GetMailTipsConfigurationTracer.TraceFunction<CachedOrganizationConfiguration, string>((long)this.traceId, "Entering MailTipsConfiguration.Initialize({0}, {1})", configuration, sender.Id.ToString());
			this.DetermineMaxMessageSize(configuration.TransportSettings.Configuration, sender);
			Organization configuration2 = configuration.OrganizationConfiguration.Configuration;
			this.showExternalRecipientCount = configuration2.MailTipsExternalRecipientsTipsEnabled;
			this.largeAudienceThreshold = (int)configuration2.MailTipsLargeAudienceThreshold;
			this.policyTipsEnabled = (from rule in configuration.PolicyNudgeRules.Rules
			where rule.IsEnabled
			select rule).Any<PolicyNudgeRule>();
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00026FCC File Offset: 0x000251CC
		private void DetermineMaxMessageSize(TransportConfigContainer transportConfiguration, ADRawEntry sender)
		{
			if (transportConfiguration.MaxSendSize.IsUnlimited)
			{
				this.maxMessageSize = int.MaxValue;
			}
			else
			{
				this.maxMessageSize = (int)transportConfiguration.MaxSendSize.Value.ToBytes();
			}
			MailTipsConfiguration.GetMailTipsConfigurationTracer.TraceDebug<int>((long)this.traceId, "Organization's max message size is ", this.maxMessageSize);
			Unlimited<ByteQuantifiedSize> unlimited = (Unlimited<ByteQuantifiedSize>)sender[ADRecipientSchema.MaxSendSize];
			if (!unlimited.IsUnlimited)
			{
				this.maxMessageSize = (int)unlimited.Value.ToBytes();
				MailTipsConfiguration.GetMailTipsConfigurationTracer.TraceDebug<int>((long)this.traceId, "Recipient's max message size is ", this.maxMessageSize);
			}
		}

		// Token: 0x04000708 RID: 1800
		public const int MaxRecipientsPerGetMailTipsCall = 50;

		// Token: 0x04000709 RID: 1801
		public const int MailTipsLargeAudienceCap = 1000;

		// Token: 0x0400070A RID: 1802
		private static readonly Trace GetMailTipsConfigurationTracer = ExTraceGlobals.GetMailTipsConfigurationTracer;

		// Token: 0x0400070B RID: 1803
		private int traceId;

		// Token: 0x0400070C RID: 1804
		private int maxMessageSize;

		// Token: 0x0400070D RID: 1805
		private int largeAudienceThreshold;

		// Token: 0x0400070E RID: 1806
		private bool showExternalRecipientCount;

		// Token: 0x0400070F RID: 1807
		private bool policyTipsEnabled;
	}
}
