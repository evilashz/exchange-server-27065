using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000038 RID: 56
	public abstract class SetSubscriptionSendAsVerifiedBase<TSubscription> : SetSubscriptionBase<TSubscription> where TSubscription : IConfigurable, new()
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000AD8A File Offset: 0x00008F8A
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000AD92 File Offset: 0x00008F92
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "DisableSubscriptionAsPoison", ValueFromPipeline = true)]
		[Parameter(Mandatory = true, ParameterSetName = "SubscriptionModification", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "ResendVerificationEmail", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "ValidateSendAs", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override AggregationSubscriptionIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000AD9B File Offset: 0x00008F9B
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000ADA3 File Offset: 0x00008FA3
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "ResendVerificationEmail", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "ValidateSendAs", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "DisableSubscriptionAsPoison", ValueFromPipeline = true)]
		public override MailboxIdParameter Mailbox
		{
			get
			{
				return base.Mailbox;
			}
			set
			{
				base.Mailbox = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000ADAC File Offset: 0x00008FAC
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000ADC3 File Offset: 0x00008FC3
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification")]
		public SmtpAddress EmailAddress
		{
			get
			{
				return (SmtpAddress)base.Fields["EmailAddress"];
			}
			set
			{
				base.Fields["EmailAddress"] = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000ADDB File Offset: 0x00008FDB
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000ADF2 File Offset: 0x00008FF2
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification")]
		public string IncomingUserName
		{
			get
			{
				return (string)base.Fields["IncomingUserName"];
			}
			set
			{
				base.Fields["IncomingUserName"] = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000AE05 File Offset: 0x00009005
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000AE0D File Offset: 0x0000900D
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification")]
		public SecureString IncomingPassword
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000AE16 File Offset: 0x00009016
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000AE2D File Offset: 0x0000902D
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification")]
		public Fqdn IncomingServer
		{
			get
			{
				return (Fqdn)base.Fields["IncomingServer"];
			}
			set
			{
				base.Fields["IncomingServer"] = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000AE40 File Offset: 0x00009040
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000AE57 File Offset: 0x00009057
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification")]
		public int IncomingPort
		{
			get
			{
				return (int)base.Fields["IncomingPort"];
			}
			set
			{
				base.Fields["IncomingPort"] = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000AE6F File Offset: 0x0000906F
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000AE95 File Offset: 0x00009095
		[Parameter(Mandatory = false, ParameterSetName = "SubscriptionModification")]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000AEAD File Offset: 0x000090AD
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000AEC4 File Offset: 0x000090C4
		[Parameter(Mandatory = false, ParameterSetName = "ValidateSendAs")]
		public string ValidateSecret
		{
			get
			{
				return (string)base.Fields["ValidateSecret"];
			}
			set
			{
				base.Fields["ValidateSecret"] = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000AED7 File Offset: 0x000090D7
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000AEFD File Offset: 0x000090FD
		[Parameter(Mandatory = false, ParameterSetName = "ResendVerificationEmail")]
		public SwitchParameter ResendVerification
		{
			get
			{
				return (SwitchParameter)(base.Fields["ResendVerification"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ResendVerification"] = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000AF15 File Offset: 0x00009115
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000AF1D File Offset: 0x0000911D
		protected bool NeedsSendAsCheck
		{
			get
			{
				return this.sendAsCheckNeeded;
			}
			set
			{
				this.sendAsCheckNeeded = value;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000AF28 File Offset: 0x00009128
		protected override IConfigurable PrepareDataObject()
		{
			IConfigurable configurable = base.PrepareDataObject();
			if (this.ValidateSecret == null && this.ResendVerification == false)
			{
				return configurable;
			}
			PimSubscriptionProxy pimSubscriptionProxy = (PimSubscriptionProxy)configurable;
			AggregationSubscriptionDataProvider dataProvider = (AggregationSubscriptionDataProvider)base.DataSession;
			AggregationTaskUtils.ProcessSendAsSpecificParameters(pimSubscriptionProxy, this.ValidateSecret, this.ResendVerification, dataProvider, new Task.TaskErrorLoggingDelegate(base.WriteDebugInfoAndError));
			base.WriteDebugInfo();
			return pimSubscriptionProxy;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000AF92 File Offset: 0x00009192
		protected override bool SendAsCheckNeeded()
		{
			return this.NeedsSendAsCheck;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000AF9A File Offset: 0x0000919A
		protected bool ShouldSkipAccountValidation()
		{
			return this.Force || this.ValidateSecret != null || this.ResendVerification || !base.Enabled || base.DisableAsPoison;
		}

		// Token: 0x040000A0 RID: 160
		private bool sendAsCheckNeeded;
	}
}
