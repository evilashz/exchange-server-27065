using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Approval;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x0200005B RID: 91
	internal class ApprovalEngine
	{
		// Token: 0x06000390 RID: 912 RVA: 0x0000FD1B File Offset: 0x0000DF1B
		private ApprovalEngine(EmailMessage incomingMessage, RoutingAddress sender, RoutingAddress recipient, MessageItem messageItem, MbxTransportMailItem mbxTransportMailItem, ApprovalEngine.ApprovalRequestCreateDelegate requestCreate)
		{
			this.message = incomingMessage;
			this.sender = sender;
			this.recipient = recipient;
			this.requestCreate = requestCreate;
			this.messageItem = messageItem;
			this.mbxTransportMailItem = mbxTransportMailItem;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000FD50 File Offset: 0x0000DF50
		public static ApprovalEngine GetApprovalEngineInstance(EmailMessage incomingMessage, RoutingAddress sender, RoutingAddress recipient, MessageItem messageItem, MbxTransportMailItem mbxTransportMailItem, ApprovalEngine.ApprovalRequestCreateDelegate requestCreate)
		{
			return new ApprovalEngine(incomingMessage, sender, recipient, messageItem, mbxTransportMailItem, requestCreate);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000FD60 File Offset: 0x0000DF60
		public ApprovalEngine.ApprovalProcessResults ProcessMessage()
		{
			if (!MultilevelAuth.IsInternalMail(this.message))
			{
				return ApprovalEngine.ApprovalProcessResults.Invalid;
			}
			DecisionHandler decisionHandler = null;
			ApprovalEngine.ApprovalProcessResults result;
			try
			{
				InitiationMessage initiationMessage;
				NdrOofHandler ndrOofHandler;
				if (InitiationMessage.TryCreate(this.message, out initiationMessage))
				{
					result = this.HandleInitiationMessage(initiationMessage);
				}
				else if (DecisionHandler.TryCreate(this.messageItem, this.sender.ToString(), this.mbxTransportMailItem.OrganizationId, out decisionHandler))
				{
					ApprovalEngine.ApprovalProcessResults approvalProcessResults = decisionHandler.Process();
					result = approvalProcessResults;
				}
				else if (NdrOofHandler.TryCreate(this.messageItem, out ndrOofHandler))
				{
					result = ndrOofHandler.Process();
				}
				else
				{
					result = ApprovalEngine.ApprovalProcessResults.Invalid;
				}
			}
			finally
			{
				if (decisionHandler != null)
				{
					decisionHandler.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000FE14 File Offset: 0x0000E014
		public ApprovalEngine.ProcessResult CreateAndSubmitApprovalRequests(int? messageLocaleId)
		{
			InitiationMessage initiationMessage;
			if (InitiationMessage.TryCreate(this.message, out initiationMessage))
			{
				InitiationProcessor initiationProcessor = new InitiationProcessor(this.mbxTransportMailItem, initiationMessage, this.messageItem, this.requestCreate, this.recipient);
				return initiationProcessor.CreateAndSubmitApprovalRequests(messageLocaleId);
			}
			return ApprovalEngine.ProcessResult.Invalid;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000FE58 File Offset: 0x0000E058
		private ApprovalEngine.ApprovalProcessResults HandleInitiationMessage(InitiationMessage initiationMessage)
		{
			if (initiationMessage.IsMapiInitiator)
			{
				if (!this.sender.Equals(this.recipient))
				{
					return ApprovalEngine.ApprovalProcessResults.Invalid;
				}
			}
			else if (string.Equals("ModeratedTransport", initiationMessage.ApprovalInitiator, StringComparison.OrdinalIgnoreCase))
			{
				this.messageItem[MessageItemSchema.ApprovalApplicationId] = 1;
				HeaderList headers = this.message.MimeDocument.RootPart.Headers;
				TextHeader textHeader = headers.FindFirst("X-MS-Exchange-Organization-Moderation-Data") as TextHeader;
				string value;
				if (textHeader != null && textHeader.TryGetValue(out value) && !string.IsNullOrEmpty(value))
				{
					this.messageItem[MessageItemSchema.ApprovalApplicationData] = value;
				}
				this.StampModeratedTransportExpiry();
			}
			if (initiationMessage.DecisionMakers == null)
			{
				return ApprovalEngine.ApprovalProcessResults.Invalid;
			}
			if (InitiationProcessor.CheckDuplicateInitiationAndUpdateIdIfNecessary(this.messageItem))
			{
				return ApprovalEngine.ApprovalProcessResults.DuplicateInitiation;
			}
			InitiationProcessor initiationProcessor = new InitiationProcessor(this.mbxTransportMailItem, initiationMessage, this.messageItem, this.requestCreate, this.recipient);
			return initiationProcessor.PrepareApprovalRequestData();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000100D4 File Offset: 0x0000E2D4
		private void StampModeratedTransportExpiry()
		{
			byte[] policyTag = null;
			string text = string.Empty;
			int retentionPeriod = 2;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.mbxTransportMailItem.OrganizationId), 361, "StampModeratedTransportExpiry", "f:\\15.00.1497\\sources\\dev\\MailboxTransport\\src\\MailboxTransportDelivery\\StoreDriver\\agents\\approval\\ApprovalEngine.cs");
				ADObjectId descendantId = tenantOrTopologyConfigurationSession.GetOrgContainerId().GetDescendantId(ApprovalApplication.ParentPathInternal);
				ADObjectId childId = descendantId.GetChildId("ModeratedRecipients");
				ApprovalEngine.diag.TraceDebug<ADObjectId>((long)this.GetHashCode(), "Reading ModeratedRecipient app from {0}", childId);
				if (childId != null)
				{
					ApprovalApplication approvalApplication = tenantOrTopologyConfigurationSession.Read<ApprovalApplication>(childId);
					if (approvalApplication != null)
					{
						ADObjectId elcretentionPolicyTag = approvalApplication.ELCRetentionPolicyTag;
						ApprovalEngine.diag.TraceDebug<ADObjectId>((long)this.GetHashCode(), "Read ModeratedRecipient, now reading Recipient Policy Tag {0}", elcretentionPolicyTag);
						RetentionPolicyTag retentionPolicyTag = null;
						if (elcretentionPolicyTag != null)
						{
							retentionPolicyTag = tenantOrTopologyConfigurationSession.Read<RetentionPolicyTag>(elcretentionPolicyTag);
						}
						else
						{
							IConfigurationSession configurationSession = SharedConfiguration.CreateScopedToSharedConfigADSession(this.mbxTransportMailItem.OrganizationId);
							if (configurationSession != null)
							{
								IList<RetentionPolicyTag> defaultRetentionPolicyTag = ApprovalUtils.GetDefaultRetentionPolicyTag(configurationSession, ApprovalApplicationId.ModeratedRecipient, 1);
								if (defaultRetentionPolicyTag != null && defaultRetentionPolicyTag.Count > 0)
								{
									retentionPolicyTag = defaultRetentionPolicyTag[0];
								}
							}
						}
						if (retentionPolicyTag != null)
						{
							ADPagedReader<ElcContentSettings> elccontentSettings = retentionPolicyTag.GetELCContentSettings();
							using (IEnumerator<ElcContentSettings> enumerator = elccontentSettings.GetEnumerator())
							{
								if (enumerator.MoveNext())
								{
									ElcContentSettings elcContentSettings = enumerator.Current;
									retentionPeriod = (int)elcContentSettings.AgeLimitForRetention.Value.TotalDays;
								}
							}
							policyTag = retentionPolicyTag.RetentionId.ToByteArray();
						}
					}
				}
			});
			if (!adoperationResult.Succeeded)
			{
				if (adoperationResult.Exception is TransientException)
				{
					throw adoperationResult.Exception;
				}
				text = adoperationResult.Exception.ToString();
				ApprovalEngine.diag.TraceError<string>((long)this.GetHashCode(), "Can't get PolicyTag guid {0}, NDRing.", text);
			}
			if (policyTag == null)
			{
				ApprovalEngine.diag.TraceError((long)this.GetHashCode(), "PolicyTag not read. NDRing");
				string text2 = this.mbxTransportMailItem.OrganizationId.ToString();
				StoreDriverDeliveryDiagnostics.LogEvent(MailboxTransportEventLogConstants.Tuple_ApprovalCannotStampExpiry, text2, new object[]
				{
					text2,
					text
				});
				throw new SmtpResponseException(AckReason.ApprovalCannotReadExpiryPolicy);
			}
			if (retentionPeriod < 2)
			{
				retentionPeriod = 2;
			}
			else if (retentionPeriod > 30)
			{
				retentionPeriod = 30;
			}
			this.messageItem[ItemSchema.RetentionDate] = ExDateTime.UtcNow.AddDays((double)retentionPeriod);
			this.messageItem[StoreObjectSchema.RetentionPeriod] = retentionPeriod;
			this.messageItem[StoreObjectSchema.PolicyTag] = policyTag;
		}

		// Token: 0x040001D4 RID: 468
		private const int MailRetentionInDaysMin = 2;

		// Token: 0x040001D5 RID: 469
		private const int MailRetentionInDaysMax = 30;

		// Token: 0x040001D6 RID: 470
		private static readonly Trace diag = ExTraceGlobals.ApprovalAgentTracer;

		// Token: 0x040001D7 RID: 471
		private EmailMessage message;

		// Token: 0x040001D8 RID: 472
		private RoutingAddress sender;

		// Token: 0x040001D9 RID: 473
		private RoutingAddress recipient;

		// Token: 0x040001DA RID: 474
		private ApprovalEngine.ApprovalRequestCreateDelegate requestCreate;

		// Token: 0x040001DB RID: 475
		private MessageItem messageItem;

		// Token: 0x040001DC RID: 476
		private MbxTransportMailItem mbxTransportMailItem;

		// Token: 0x0200005C RID: 92
		// (Invoke) Token: 0x06000398 RID: 920
		public delegate MessageItemApprovalRequest ApprovalRequestCreateDelegate(MbxTransportMailItem originalMailItem);

		// Token: 0x0200005D RID: 93
		public enum ProcessResult
		{
			// Token: 0x040001DE RID: 478
			Invalid,
			// Token: 0x040001DF RID: 479
			InitiationMessageOk,
			// Token: 0x040001E0 RID: 480
			UnauthorizedMessage,
			// Token: 0x040001E1 RID: 481
			InitiationNotFoundForDecision,
			// Token: 0x040001E2 RID: 482
			DecisionMarked,
			// Token: 0x040001E3 RID: 483
			DecisionAlreadyMade,
			// Token: 0x040001E4 RID: 484
			InitiationMessageDuplicate,
			// Token: 0x040001E5 RID: 485
			NdrOrOofInvalid,
			// Token: 0x040001E6 RID: 486
			NdrOrOofUpdated,
			// Token: 0x040001E7 RID: 487
			NdrOrOofUpdateSkipped,
			// Token: 0x040001E8 RID: 488
			InitiationNotFoundForNdrOrOof
		}

		// Token: 0x0200005E RID: 94
		internal class ApprovalProcessResults
		{
			// Token: 0x0600039B RID: 923 RVA: 0x00010237 File Offset: 0x0000E437
			public ApprovalProcessResults()
			{
			}

			// Token: 0x0600039C RID: 924 RVA: 0x0001023F File Offset: 0x0000E43F
			public ApprovalProcessResults(ApprovalEngine.ProcessResult processResults)
			{
				this.processResults = processResults;
			}

			// Token: 0x0600039D RID: 925 RVA: 0x0001024E File Offset: 0x0000E44E
			public ApprovalProcessResults(ApprovalEngine.ProcessResult processResults, long initiationSearchTimeMilliseconds)
			{
				this.processResults = processResults;
				this.initiationMessageSearchTimeMilliseconds = initiationSearchTimeMilliseconds;
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x0600039E RID: 926 RVA: 0x00010264 File Offset: 0x0000E464
			// (set) Token: 0x0600039F RID: 927 RVA: 0x0001026C File Offset: 0x0000E46C
			public byte[] ApprovalTrackingBlob
			{
				get
				{
					return this.approvalTrackingBlob;
				}
				internal set
				{
					this.approvalTrackingBlob = value;
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x060003A0 RID: 928 RVA: 0x00010275 File Offset: 0x0000E475
			// (set) Token: 0x060003A1 RID: 929 RVA: 0x0001027D File Offset: 0x0000E47D
			public string ApprovalRequestMessageId
			{
				get
				{
					return this.approvalRequestMessageId;
				}
				internal set
				{
					this.approvalRequestMessageId = value;
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x060003A2 RID: 930 RVA: 0x00010286 File Offset: 0x0000E486
			// (set) Token: 0x060003A3 RID: 931 RVA: 0x0001028E File Offset: 0x0000E48E
			public int TotalDecisionMakers
			{
				get
				{
					return this.totalDecisionMakers;
				}
				internal set
				{
					this.totalDecisionMakers = value;
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x060003A4 RID: 932 RVA: 0x00010297 File Offset: 0x0000E497
			// (set) Token: 0x060003A5 RID: 933 RVA: 0x0001029F File Offset: 0x0000E49F
			public ApprovalEngine.ProcessResult ProcessResults
			{
				get
				{
					return this.processResults;
				}
				internal set
				{
					this.processResults = value;
				}
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x060003A6 RID: 934 RVA: 0x000102A8 File Offset: 0x0000E4A8
			// (set) Token: 0x060003A7 RID: 935 RVA: 0x000102B0 File Offset: 0x0000E4B0
			public long InitiationMessageSearchTimeMilliseconds
			{
				get
				{
					return this.initiationMessageSearchTimeMilliseconds;
				}
				internal set
				{
					this.initiationMessageSearchTimeMilliseconds = value;
				}
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x060003A8 RID: 936 RVA: 0x000102B9 File Offset: 0x0000E4B9
			// (set) Token: 0x060003A9 RID: 937 RVA: 0x000102C1 File Offset: 0x0000E4C1
			public string ExistingDecisionMakerAddress
			{
				get
				{
					return this.existingDecisionMakerAddress;
				}
				internal set
				{
					this.existingDecisionMakerAddress = value;
				}
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x060003AA RID: 938 RVA: 0x000102CA File Offset: 0x0000E4CA
			// (set) Token: 0x060003AB RID: 939 RVA: 0x000102D2 File Offset: 0x0000E4D2
			public ApprovalStatus? ExistingApprovalStatus
			{
				get
				{
					return this.existingApprovalStatus;
				}
				internal set
				{
					this.existingApprovalStatus = value;
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x060003AC RID: 940 RVA: 0x000102DB File Offset: 0x0000E4DB
			// (set) Token: 0x060003AD RID: 941 RVA: 0x000102E3 File Offset: 0x0000E4E3
			public ExDateTime? ExistingDecisionTime
			{
				get
				{
					return this.existingDecisionTime;
				}
				internal set
				{
					this.existingDecisionTime = value;
				}
			}

			// Token: 0x040001E9 RID: 489
			public static readonly ApprovalEngine.ApprovalProcessResults Invalid = new ApprovalEngine.ApprovalProcessResults(ApprovalEngine.ProcessResult.Invalid);

			// Token: 0x040001EA RID: 490
			public static readonly ApprovalEngine.ApprovalProcessResults NdrOofInvalid = new ApprovalEngine.ApprovalProcessResults(ApprovalEngine.ProcessResult.NdrOrOofInvalid);

			// Token: 0x040001EB RID: 491
			public static readonly ApprovalEngine.ApprovalProcessResults DuplicateInitiation = new ApprovalEngine.ApprovalProcessResults(ApprovalEngine.ProcessResult.InitiationMessageDuplicate);

			// Token: 0x040001EC RID: 492
			public static readonly ApprovalEngine.ApprovalProcessResults InitiationNotFoundForDecision = new ApprovalEngine.ApprovalProcessResults(ApprovalEngine.ProcessResult.InitiationNotFoundForDecision);

			// Token: 0x040001ED RID: 493
			private byte[] approvalTrackingBlob;

			// Token: 0x040001EE RID: 494
			private string approvalRequestMessageId;

			// Token: 0x040001EF RID: 495
			private int totalDecisionMakers;

			// Token: 0x040001F0 RID: 496
			private ApprovalEngine.ProcessResult processResults;

			// Token: 0x040001F1 RID: 497
			private long initiationMessageSearchTimeMilliseconds;

			// Token: 0x040001F2 RID: 498
			private string existingDecisionMakerAddress;

			// Token: 0x040001F3 RID: 499
			private ApprovalStatus? existingApprovalStatus;

			// Token: 0x040001F4 RID: 500
			private ExDateTime? existingDecisionTime;
		}
	}
}
