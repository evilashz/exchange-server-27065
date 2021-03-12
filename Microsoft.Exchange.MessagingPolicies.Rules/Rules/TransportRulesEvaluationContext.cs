using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.LoggingCommon;
using Microsoft.Filtering;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200003E RID: 62
	internal class TransportRulesEvaluationContext : BaseTransportRulesEvaluationContext
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000C032 File Offset: 0x0000A232
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000C03A File Offset: 0x0000A23A
		public PerTenantTransportSettings PerTenantTransportSettings { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000C043 File Offset: 0x0000A243
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000C054 File Offset: 0x0000A254
		public IAgentLog TheAgentLog
		{
			get
			{
				return this.theAgentLog ?? AgentLog.Instance;
			}
			set
			{
				this.theAgentLog = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000C05D File Offset: 0x0000A25D
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000C065 File Offset: 0x0000A265
		public RuleHealthMonitor RuleExecutionMonitor { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000C06E File Offset: 0x0000A26E
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000C076 File Offset: 0x0000A276
		public TransportRulesCostMonitor RuleSetExecutionMonitor { get; set; }

		// Token: 0x06000220 RID: 544 RVA: 0x0000C080 File Offset: 0x0000A280
		private static PerTenantTransportSettings GetPerTenantTransportSettings(OrganizationId orgId)
		{
			if (null == orgId)
			{
				return null;
			}
			ITransportConfiguration transportConfiguration;
			if (Components.TryGetConfigurationComponent(out transportConfiguration))
			{
				PerTenantTransportSettings result;
				transportConfiguration.TryGetTransportSettings(orgId, out result);
				return result;
			}
			return null;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000C0B0 File Offset: 0x0000A2B0
		private TransportRulesEvaluationContext(TransportRuleCollection rules, MailItem mailItem, SmtpServer server, TransportRulesTracer tracer = null) : base(rules, tracer ?? new TransportRulesTracer(mailItem, false))
		{
			this.mailItem = mailItem;
			this.server = server;
			this.message = this.CreateMailMessage(mailItem);
			this.ExecutedActions = new List<Action>();
			this.ShouldAuditRules = false;
			this.SenderOverridden = false;
			this.SenderOverrideJustification = string.Empty;
			this.FpOverriden = false;
			this.CurrentAuditSeverityLevel = AuditSeverityLevel.Low;
			this.PerTenantTransportSettings = TransportRulesEvaluationContext.GetPerTenantTransportSettings(TransportUtils.GetOrganizationID(mailItem));
			this.RuleExecutionMonitor = TransportRulesEvaluationContext.CreateRuleHealthMonitor(mailItem);
			if (this.server != null)
			{
				this.userComparer = new UserComparer(this.server.AddressBook);
				this.membershipChecker = new MembershipChecker(this.server.AddressBook);
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000C16F File Offset: 0x0000A36F
		public TransportRulesEvaluationContext(TransportRuleCollection rules, MailItem mailItem, SmtpServer server, ReceiveMessageEventSource endOfDataSource, SmtpSession session, TransportRulesCostMonitor ruleSetExecutionMonitor = null) : this(rules, mailItem, server, null)
		{
			this.endOfDataSource = endOfDataSource;
			this.session = session;
			this.eventType = EventType.EndOfData;
			this.RuleSetExecutionMonitor = ruleSetExecutionMonitor;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000C19C File Offset: 0x0000A39C
		public TransportRulesEvaluationContext(TransportRuleCollection rules, MailItem mailItem, SmtpServer server, QueuedMessageEventSource eventSource, TransportRulesCostMonitor ruleSetExecutionMonitor = null, bool shouldAuditRules = false, TenantConfigurationCache<TransportRulesPerTenantSettings> transportRulesCache = null, TransportRulesTracer tracer = null) : this(rules, mailItem, server, tracer)
		{
			if (eventSource == null)
			{
				return;
			}
			if (eventSource is ResolvedMessageEventSource)
			{
				this.eventType = EventType.OnResolvedMessage;
				this.OnResolvedSource = (eventSource as ResolvedMessageEventSource);
			}
			else
			{
				if (!(eventSource is RoutedMessageEventSource))
				{
					throw new InvalidTransportRuleEventSourceTypeException(eventSource.GetType().FullName);
				}
				this.eventType = EventType.OnRoutedMessage;
				this.OnRoutedSource = (eventSource as RoutedMessageEventSource);
			}
			this.eventSource = eventSource;
			this.RuleSetExecutionMonitor = ruleSetExecutionMonitor;
			this.ShouldAuditRules = shouldAuditRules;
			this.RuleExecutionMonitor = TransportRulesEvaluationContext.CreateRuleHealthMonitor(mailItem);
			this.transportRulesCache = transportRulesCache;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000C235 File Offset: 0x0000A435
		public EventType EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000C23D File Offset: 0x0000A43D
		public MailItem MailItem
		{
			get
			{
				return this.mailItem;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000C245 File Offset: 0x0000A445
		public SmtpServer Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000C24D File Offset: 0x0000A44D
		public override IStringComparer UserComparer
		{
			get
			{
				return this.userComparer;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000C255 File Offset: 0x0000A455
		public override IStringComparer MembershipChecker
		{
			get
			{
				return this.membershipChecker;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000C260 File Offset: 0x0000A460
		public bool CanModify
		{
			get
			{
				return (this.mailItem.Message.IsInterpersonalMessage || this.mailItem.Message.MapiMessageClass.StartsWith("IPM.Note", StringComparison.OrdinalIgnoreCase)) && this.mailItem.Message.MessageSecurityType == MessageSecurityType.None;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000C2B1 File Offset: 0x0000A4B1
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000C2B9 File Offset: 0x0000A4B9
		public List<EnvelopeRecipient> MatchedRecipients
		{
			get
			{
				return this.matchedRecipients;
			}
			set
			{
				this.matchedRecipients = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000C2C2 File Offset: 0x0000A4C2
		public ReceiveMessageEventSource EndOfDataSource
		{
			get
			{
				return this.endOfDataSource;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000C2CA File Offset: 0x0000A4CA
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000C2D2 File Offset: 0x0000A4D2
		public ResolvedMessageEventSource OnResolvedSource { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000C2DB File Offset: 0x0000A4DB
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000C2E3 File Offset: 0x0000A4E3
		public RoutedMessageEventSource OnRoutedSource { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000C2EC File Offset: 0x0000A4EC
		public QueuedMessageEventSource EventSource
		{
			get
			{
				return this.eventSource;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000C2F4 File Offset: 0x0000A4F4
		public List<TransportRulesEvaluationContext.AddedRecipient> RecipientsToAdd
		{
			get
			{
				if (this.recipientsToAdd == null)
				{
					this.recipientsToAdd = new List<TransportRulesEvaluationContext.AddedRecipient>();
				}
				return this.recipientsToAdd;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000C30F File Offset: 0x0000A50F
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000C317 File Offset: 0x0000A517
		public RecipientState RecipientState
		{
			get
			{
				return this.recipientState;
			}
			set
			{
				this.recipientState = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000C320 File Offset: 0x0000A520
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000C328 File Offset: 0x0000A528
		public SmtpResponse? EdgeRejectResponse
		{
			get
			{
				return this.edgeResponse;
			}
			set
			{
				this.edgeResponse = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000C331 File Offset: 0x0000A531
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000C339 File Offset: 0x0000A539
		public bool MessageQuarantined
		{
			get
			{
				return this.messageQuarantined;
			}
			set
			{
				this.messageQuarantined = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000C342 File Offset: 0x0000A542
		public SmtpSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000C34A File Offset: 0x0000A54A
		public MailMessage Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000C354 File Offset: 0x0000A554
		internal void ResetRulesCache()
		{
			if (this.transportRulesCache != null)
			{
				OrganizationId organizationID = TransportUtils.GetOrganizationID(this.MailItem);
				if (organizationID != null)
				{
					this.transportRulesCache.RemoveValue(organizationID);
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000C38A File Offset: 0x0000A58A
		protected override FilteringServiceInvokerRequest FilteringServiceInvokerRequest
		{
			get
			{
				if (this.mailItem != null)
				{
					return TransportFilteringServiceInvokerRequest.CreateInstance(this.mailItem, null);
				}
				return null;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000C3A4 File Offset: 0x0000A5A4
		internal static RuleHealthMonitor CreateRuleHealthMonitor(MailItem mailItem)
		{
			int num = 0;
			if (mailItem != null)
			{
				num = (int)mailItem.MimeStreamLength;
			}
			num = ((num == 0) ? TransportRulesEvaluationContext.MinDataLengthForThresholdCalculations : num);
			int mtlLoggingThresholdMs = (int)((double)num / (double)Components.TransportAppConfig.TransportRuleConfig.TransportRuleExecutionTimeReportingThresholdInBytesPerSecond * 1000.0);
			int eventLoggingThresholdMs = (int)((double)num / (double)Components.TransportAppConfig.TransportRuleConfig.TransportRuleExecutionTimeAlertingThresholdInBytesPerSecond * 1000.0);
			return TransportRulesEvaluationContext.CreateRuleHealthMonitor(mtlLoggingThresholdMs, eventLoggingThresholdMs);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000C43A File Offset: 0x0000A63A
		internal static RuleHealthMonitor CreateRuleHealthMonitor(int mtlLoggingThresholdMs, int eventLoggingThresholdMs)
		{
			return new RuleHealthMonitor(RuleHealthMonitor.ActivityType.Execute, (long)mtlLoggingThresholdMs, (long)eventLoggingThresholdMs, delegate(string eventMessageDetails)
			{
				TransportAction.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_RuleExecutionTimeExceededThreshold, null, new object[]
				{
					eventMessageDetails
				});
			});
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000C463 File Offset: 0x0000A663
		protected override void OnDataClassificationsRetrieved(FilteringResults textExtractionResults)
		{
			if (textExtractionResults != null)
			{
				this.message.SetUnifiedContent(textExtractionResults);
			}
			this.AuditMessageClassifications();
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000C47A File Offset: 0x0000A67A
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000C482 File Offset: 0x0000A682
		public List<Action> ExecutedActions { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000C48B File Offset: 0x0000A68B
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000C493 File Offset: 0x0000A693
		public bool ShouldAuditRules { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000C49C File Offset: 0x0000A69C
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		public bool SenderOverridden { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000C4AD File Offset: 0x0000A6AD
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000C4B5 File Offset: 0x0000A6B5
		public string SenderOverrideJustification { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000C4BE File Offset: 0x0000A6BE
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000C4C6 File Offset: 0x0000A6C6
		public bool FpOverriden { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000C4CF File Offset: 0x0000A6CF
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000C4D7 File Offset: 0x0000A6D7
		public AuditSeverityLevel CurrentAuditSeverityLevel { get; set; }

		// Token: 0x0600024C RID: 588 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
		public override void ResetPerRuleData()
		{
			base.ResetPerRuleData();
			this.ExecutedActions.Clear();
			this.CurrentAuditSeverityLevel = AuditSeverityLevel.Low;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000C4FA File Offset: 0x0000A6FA
		public static void AddRuleData(ICollection<KeyValuePair<string, string>> data, string key, string value)
		{
			data.Add(new KeyValuePair<string, string>(key, value));
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000C509 File Offset: 0x0000A709
		internal void Defer(TimeSpan deferTime)
		{
			if (this.EventSource != null)
			{
				if (this.RuleSetExecutionMonitor != null)
				{
					this.RuleSetExecutionMonitor.StopAndSetReporter(null);
				}
				this.EventSource.Defer(deferTime);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000C533 File Offset: 0x0000A733
		internal TransportRulesTracer TransportRulesTracer
		{
			get
			{
				return base.Tracer as TransportRulesTracer;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000C540 File Offset: 0x0000A740
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000C548 File Offset: 0x0000A748
		internal TestMessageConfig TestMessageConfig { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000C551 File Offset: 0x0000A751
		internal bool IsTestMessage
		{
			get
			{
				return this.TestMessageConfig != null && this.TestMessageConfig.IsTestMessage && (this.TestMessageConfig.LogTypes & LogTypesEnum.TransportRules) != LogTypesEnum.None;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000C57F File Offset: 0x0000A77F
		internal bool SuppressDelivery
		{
			get
			{
				return this.TestMessageConfig != null && this.TestMessageConfig.SuppressDelivery;
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000C596 File Offset: 0x0000A796
		protected virtual MailMessage CreateMailMessage(MailItem thisMailItem)
		{
			return new MailMessage(thisMailItem);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		internal static int GetAttachmentTextScanLimit(MailItem mailItem)
		{
			TransportMailItem transportMailItem = TransportUtils.GetTransportMailItem(mailItem);
			if (transportMailItem == null)
			{
				return 0;
			}
			return transportMailItem.TransportSettings.TransportRuleAttachmentTextScanLimit;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		private void AuditMessageClassifications()
		{
			if (!this.ShouldAuditRules || !base.HaveDataClassificationsBeenRetrieved)
			{
				return;
			}
			if (this.MailItem != null)
			{
				object obj;
				if (this.MailItem.Properties.TryGetValue("DCAudited", out obj))
				{
					return;
				}
				this.MailItem.Properties["DCAudited"] = true;
			}
			try
			{
				foreach (DiscoveredDataClassification discoveredDataClassification in base.DataClassifications)
				{
					List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
					TransportRulesEvaluationContext.AddRuleData(data, "dcid", discoveredDataClassification.Id);
					TransportRulesEvaluationContext.AddRuleData(data, "count", discoveredDataClassification.TotalCount.ToString());
					TransportRulesEvaluationContext.AddRuleData(data, "conf", discoveredDataClassification.MaxConfidenceLevel.ToString());
					this.EventSource.TrackAgentInfo(TrackAgentInfoAgentName.TRA.ToString("G"), TrackAgentInfoGroupName.DC.ToString("G"), data);
				}
			}
			catch (InvalidOperationException)
			{
				base.Tracer.TraceWarning("InvalidOperationException thrown while attempting to audit classification information. Expected when data size to Audit is high.");
			}
		}

		// Token: 0x04000191 RID: 401
		private static readonly int MinDataLengthForThresholdCalculations = 1024;

		// Token: 0x04000192 RID: 402
		private readonly MailMessage message;

		// Token: 0x04000193 RID: 403
		private readonly SmtpSession session;

		// Token: 0x04000194 RID: 404
		private readonly EventType eventType;

		// Token: 0x04000195 RID: 405
		private readonly MailItem mailItem;

		// Token: 0x04000196 RID: 406
		private readonly SmtpServer server;

		// Token: 0x04000197 RID: 407
		private readonly ReceiveMessageEventSource endOfDataSource;

		// Token: 0x04000198 RID: 408
		private readonly QueuedMessageEventSource eventSource;

		// Token: 0x04000199 RID: 409
		private TenantConfigurationCache<TransportRulesPerTenantSettings> transportRulesCache;

		// Token: 0x0400019A RID: 410
		private List<TransportRulesEvaluationContext.AddedRecipient> recipientsToAdd;

		// Token: 0x0400019B RID: 411
		private RecipientState recipientState;

		// Token: 0x0400019C RID: 412
		private SmtpResponse? edgeResponse;

		// Token: 0x0400019D RID: 413
		private bool messageQuarantined;

		// Token: 0x0400019E RID: 414
		private List<EnvelopeRecipient> matchedRecipients;

		// Token: 0x0400019F RID: 415
		private IAgentLog theAgentLog;

		// Token: 0x040001A0 RID: 416
		private IStringComparer userComparer;

		// Token: 0x040001A1 RID: 417
		private IStringComparer membershipChecker;

		// Token: 0x0200003F RID: 63
		internal struct AddedRecipient
		{
			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x06000259 RID: 601 RVA: 0x0000C700 File Offset: 0x0000A900
			public string Address
			{
				get
				{
					return this.address;
				}
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x0600025A RID: 602 RVA: 0x0000C708 File Offset: 0x0000A908
			// (set) Token: 0x0600025B RID: 603 RVA: 0x0000C710 File Offset: 0x0000A910
			public string DisplayName { get; set; }

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x0600025C RID: 604 RVA: 0x0000C719 File Offset: 0x0000A919
			public RecipientP2Type RecipientP2Type
			{
				get
				{
					return this.recipientP2Type;
				}
			}

			// Token: 0x0600025D RID: 605 RVA: 0x0000C721 File Offset: 0x0000A921
			public AddedRecipient(string address, string displayName, RecipientP2Type recipientP2Type)
			{
				this = default(TransportRulesEvaluationContext.AddedRecipient);
				this.address = address;
				this.recipientP2Type = recipientP2Type;
				this.DisplayName = displayName;
			}

			// Token: 0x040001AF RID: 431
			private readonly string address;

			// Token: 0x040001B0 RID: 432
			private readonly RecipientP2Type recipientP2Type;
		}
	}
}
