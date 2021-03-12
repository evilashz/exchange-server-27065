using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.MailboxRules;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.StoreDriver.Shared;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.MailboxTransport.StoreDriverDelivery;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Transport.MailboxRules
{
	// Token: 0x0200008A RID: 138
	internal abstract class RuleEvaluationContext : RuleEvaluationContextBase
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0001844B File Offset: 0x0001664B
		internal StoreDriverServer Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00018454 File Offset: 0x00016654
		protected RuleEvaluationContext(Folder folder, MessageItem message, StoreSession session, ProxyAddress recipient, ADRecipientCache<TransportMiniRecipient> recipientCache, long mimeSize, MailItemDeliver mailItemDeliver) : base(folder, message, session, recipient, recipientCache, mimeSize, Microsoft.Exchange.Transport.MailboxRules.RuleConfig.Instance, ExTraceGlobals.MailboxRuleTracer)
		{
			MailboxSession mailboxSession = session as MailboxSession;
			this.initialFolder = folder;
			this.mailItemDeliver = mailItemDeliver;
			if (mailboxSession != null)
			{
				object obj = mailboxSession.Mailbox.TryGetProperty(MailboxSchema.MailboxOofState);
				if (obj is PropertyError)
				{
					this.IsOof = mailboxSession.IsMailboxOof();
				}
				else
				{
					this.IsOof = (bool)obj;
				}
			}
			base.LimitChecker = new StoreDriverLimitChecker(this);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000184D2 File Offset: 0x000166D2
		protected RuleEvaluationContext(RuleEvaluationContext parentContext) : base(parentContext)
		{
			this.mailItemDeliver = parentContext.mailItemDeliver;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000184E7 File Offset: 0x000166E7
		protected RuleEvaluationContext()
		{
			this.traceFormatter = new TraceFormatter(false);
			this.tracer = ExTraceGlobals.MailboxRuleTracer;
			base.LimitChecker = new StoreDriverLimitChecker(this);
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00018512 File Offset: 0x00016712
		public override string DefaultDomainName
		{
			get
			{
				return Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00018523 File Offset: 0x00016723
		public override IsMemberOfResolver<string> IsMemberOfResolver
		{
			get
			{
				return Components.MailboxRulesIsMemberOfResolverComponent.IsMemberOfResolver;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0001852F File Offset: 0x0001672F
		public override string LocalServerFqdn
		{
			get
			{
				return Components.Configuration.LocalServer.TransportServer.Fqdn;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00018545 File Offset: 0x00016745
		public override IPAddress LocalServerNetworkAddress
		{
			get
			{
				return StoreDriverDelivery.LocalIP.AddressList[0];
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00018553 File Offset: 0x00016753
		public override ExEventLog.EventTuple OofHistoryCorruption
		{
			get
			{
				return MailboxTransportEventLogConstants.Tuple_OofHistoryCorruption;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0001855A File Offset: 0x0001675A
		public override ExEventLog.EventTuple OofHistoryFolderMissing
		{
			get
			{
				return MailboxTransportEventLogConstants.Tuple_OofHistoryFolderMissing;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00018561 File Offset: 0x00016761
		public DeliveryPriority Priority
		{
			get
			{
				return this.mailItemDeliver.MailItemWrapper.DeliveryPriority;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00018573 File Offset: 0x00016773
		public string PrioritizationReason
		{
			get
			{
				return this.mailItemDeliver.MailItemWrapper.PrioritizationReason;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00018585 File Offset: 0x00016785
		public MimePart RootPart
		{
			get
			{
				return this.mailItemDeliver.MbxTransportMailItem.RootPart;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00018597 File Offset: 0x00016797
		public MbxTransportMailItem MbxTransportMailItem
		{
			get
			{
				if (this.mailItemDeliver != null)
				{
					return this.mailItemDeliver.MbxTransportMailItem;
				}
				return null;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000185AE File Offset: 0x000167AE
		public override List<KeyValuePair<string, string>> ExtraTrackingEventData
		{
			get
			{
				if (this.mailItemDeliver.ExtraTrackingEventData == null)
				{
					this.mailItemDeliver.ExtraTrackingEventData = new List<KeyValuePair<string, string>>();
				}
				return this.mailItemDeliver.ExtraTrackingEventData;
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000185D8 File Offset: 0x000167D8
		public static RuleEvaluationContext Create(StoreDriverServer server, Folder folder, MessageItem message, StoreSession session, string recipientAddress, ADRecipientCache<TransportMiniRecipient> recipientCache, long mimeSize, bool processingTestMessage, bool shouldExecuteDisabledAndInErrorRules, MailItemDeliver mailItemDeliver)
		{
			return new MessageContext(folder, message, session, new SmtpProxyAddress(recipientAddress, true), recipientCache, mimeSize, mailItemDeliver)
			{
				server = server,
				traceFormatter = new TraceFormatter(processingTestMessage),
				ShouldExecuteDisabledAndInErrorRules = shouldExecuteDisabledAndInErrorRules
			};
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00018619 File Offset: 0x00016819
		public override MessageItem CreateMessageItem(PropertyDefinition[] prefetchProperties)
		{
			return MessageItem.CreateInMemory(prefetchProperties);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000186A8 File Offset: 0x000168A8
		public virtual void UpdateDeferredError()
		{
			if (this.daeMessageEntryIds == null || this.daeMessageEntryIds.Count == 0)
			{
				return;
			}
			base.DeliveredMessage.Load(DeferredError.EntryId);
			byte[] deliveredMessageEntryId = base.DeliveredMessage[StoreObjectSchema.EntryId] as byte[];
			if (deliveredMessageEntryId == null)
			{
				base.TraceDebug("Delivered Message EntryId is null");
				return;
			}
			using (List<byte[]>.Enumerator enumerator = this.daeMessageEntryIds.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					byte[] daeEntryId = enumerator.Current;
					if (daeEntryId != null)
					{
						Exception argument;
						if (!RuleUtil.TryRunStoreCode(delegate
						{
							StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(daeEntryId);
							if (storeObjectId != null)
							{
								using (Item item = Microsoft.Exchange.Data.Storage.Item.Bind(this.StoreSession, storeObjectId))
								{
									item.OpenAsReadWrite();
									item[ItemSchema.OriginalMessageEntryId] = deliveredMessageEntryId;
									item.Save(SaveMode.NoConflictResolution);
								}
							}
						}, out argument))
						{
							base.TraceDebug<byte[], Exception>("Can't set pr_dam_original_entryid on DeferredError message id {0}, and the exception is: {1}", daeEntryId, argument);
						}
					}
				}
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000187A0 File Offset: 0x000169A0
		public string GetTraces()
		{
			string result;
			using (Stream stream = new MemoryStream())
			{
				this.traceFormatter.CopyDataTo(stream);
				stream.Position = 0L;
				using (TextReader textReader = new StreamReader(stream))
				{
					string text = textReader.ReadToEnd();
					result = text;
				}
			}
			return result;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001880C File Offset: 0x00016A0C
		public override ISubmissionItem GenerateSubmissionItem(MessageItem item, WorkItem workItem)
		{
			return new SmtpSubmissionItem(this, item, workItem);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000188D8 File Offset: 0x00016AD8
		public EmailMessage GenerateTraceReport(SmtpAddress reportToAddress)
		{
			if (base.ProcessingTestMessage && reportToAddress.IsValidAddress)
			{
				EmailMessage traceReport = EmailMessage.Create();
				RoutingAddress address = GlobalConfigurationBase<MicrosoftExchangeRecipient, MicrosoftExchangeRecipientConfiguration>.Instance.Address;
				traceReport.From = new EmailRecipient(null, (string)address);
				traceReport.To.Add(new EmailRecipient(null, (string)reportToAddress));
				traceReport.Subject = "Tracing Report: " + base.Message.Subject;
				using (Stream contentWriteStream = traceReport.Body.GetContentWriteStream())
				{
					this.traceFormatter.CopyDataTo(contentWriteStream);
					contentWriteStream.Flush();
				}
				List<Exception> list = new List<Exception>();
				Exception item;
				if (!RuleUtil.TryRunStoreCode(delegate
				{
					this.AddRuleDumpAttachment(traceReport, this.initialFolder, "mailbox-rules.xml");
				}, out item))
				{
					list.Add(item);
				}
				if (!RuleUtil.TryRunStoreCode(delegate
				{
					MailboxSession mailboxSession = this.StoreSession as MailboxSession;
					StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
					if (defaultFolderId != this.initialFolder.Id.ObjectId)
					{
						using (Folder folder = Folder.Bind(mailboxSession, defaultFolderId))
						{
							this.AddRuleDumpAttachment(traceReport, folder, "inbox-rules.xml");
						}
					}
				}, out item))
				{
					list.Add(item);
				}
				if (!RuleUtil.TryRunStoreCode(delegate
				{
					this.AddOofHistoryAttachment(traceReport, "automatic-reply-history.xml");
				}, out item))
				{
					list.Add(item);
				}
				if (list.Count > 0)
				{
					Attachment attachment = traceReport.Attachments.Add("inboxrule-report-exceptions.txt");
					using (Stream contentWriteStream2 = attachment.GetContentWriteStream())
					{
						using (TextWriter textWriter = new StreamWriter(contentWriteStream2))
						{
							foreach (Exception ex in list)
							{
								textWriter.WriteLine(ex.ToString());
								textWriter.WriteLine();
							}
						}
					}
				}
				return traceReport;
			}
			return null;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00018AD8 File Offset: 0x00016CD8
		public override Folder GetDeletedItemsFolder()
		{
			MailboxSession mailboxSession = base.StoreSession as MailboxSession;
			return base.OpenFolder(mailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems));
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00018B00 File Offset: 0x00016D00
		public override void SetMailboxOwnerAsSender(MessageItem message)
		{
			MailboxSession mailboxSession = base.StoreSession as MailboxSession;
			Participant participant = new Participant(mailboxSession.MailboxOwner);
			message.Sender = participant;
			message.From = participant;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00018B80 File Offset: 0x00016D80
		public override ExTimeZone DetermineRecipientTimeZone()
		{
			if (this.timeZoneRetrieved)
			{
				base.TraceDebug<ExTimeZone>("TimeZone retrieved before, returning it. TimeZone: {0}", this.timeZone);
				return this.timeZone;
			}
			MailboxSession mailboxSession = base.StoreSession as MailboxSession;
			if (!this.CheckMailboxSessionForTimeZone(mailboxSession))
			{
				return this.timeZone;
			}
			bool found = false;
			bool flag = false;
			MessageStatus messageStatus = this.RunUnderStorageExceptionHandler(delegate
			{
				found = this.TryFindOwaTimeZone(mailboxSession, out this.timeZone);
			});
			if (found)
			{
				this.timeZoneRetrieved = true;
				base.TraceDebug<ProxyAddress, ExTimeZone>("Found OWA user TimeZone configuration, using it. Recipient: {0}, TimeZone: {1}", base.Recipient, this.timeZone);
				return this.timeZone;
			}
			if (MessageStatus.Success != messageStatus)
			{
				base.TraceDebug<ProxyAddress, Exception>("Unable to retrieve OWA user configuration, trying to get Outlook configuration next. Recipient: {0}, Exception: {2}", base.Recipient, messageStatus.Exception);
				switch (messageStatus.Action)
				{
				case MessageAction.NDR:
				case MessageAction.Throw:
					flag = true;
					break;
				}
			}
			byte[] blob = null;
			messageStatus = this.RunUnderStorageExceptionHandler(delegate
			{
				found = this.TryFindOutlookTimeZone(mailboxSession, out blob);
			});
			if (!found)
			{
				if (MessageStatus.Success != messageStatus)
				{
					base.TraceDebug<ProxyAddress, Exception>("Unable to retrieve Outlook user configuration. Using server TimeZone instead. Recipient: {0}, Exception: {2}", base.Recipient, messageStatus.Exception);
					if (flag)
					{
						switch (messageStatus.Action)
						{
						case MessageAction.NDR:
						case MessageAction.Throw:
							this.timeZoneRetrieved = true;
							base.TraceDebug("Setting timeZoneRetrieved to true due to double permanent storage exception encountered.");
							break;
						}
					}
				}
				this.timeZone = ExTimeZone.CurrentTimeZone;
				base.TraceDebug<ProxyAddress, ExTimeZone>("Neither OWA nor Outlook user TimeZone configuration were found, Using server TimeZone instead. Recipient: {0}, TimeZone: {1}", base.Recipient, this.timeZone);
				return this.timeZone;
			}
			this.timeZoneRetrieved = true;
			if (this.TryParseTimeZoneBlob(blob, string.Empty, out this.timeZone))
			{
				base.TraceDebug<ProxyAddress, ExTimeZone>("Found Outlook user TimeZone configuration, using it. Recipient: {0}, TimeZone: {1}", base.Recipient, this.timeZone);
				return this.timeZone;
			}
			this.timeZone = ExTimeZone.CurrentTimeZone;
			base.TraceDebug<ProxyAddress, ExTimeZone>("Outlook user TimeZone blob could not be parsed in O12 TimeZone format. Using server TimeZone instead. Recipient: {0}, TimeZone: {1}", base.Recipient, this.timeZone);
			return this.timeZone;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00018D6B File Offset: 0x00016F6B
		public override void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			StoreDriverDeliveryDiagnostics.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00018D78 File Offset: 0x00016F78
		public override void MarkRuleInError(Rule rule, RuleAction.Type actionType, int actionIndex, DeferredError.RuleError errorCode)
		{
			base.MarkRuleInError(rule, actionType, actionIndex, errorCode);
			using (DeferredError deferredError = DeferredError.Create(base.StoreSession as MailboxSession, base.CurrentFolder.StoreObjectId, rule.Provider, rule.ID, actionType, actionIndex, errorCode))
			{
				byte[] array = deferredError.Save();
				if (array != null)
				{
					if (this.daeMessageEntryIds == null)
					{
						this.daeMessageEntryIds = new List<byte[]>();
					}
					this.daeMessageEntryIds.Add(array);
				}
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00018E04 File Offset: 0x00017004
		protected virtual MessageStatus RunUnderStorageExceptionHandler(StoreDriverDelegate action)
		{
			return StorageExceptionHandler.RunUnderExceptionHandler(this.mailItemDeliver, action);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00018E12 File Offset: 0x00017012
		protected virtual bool TryFindOwaTimeZone(MailboxSession mailboxSession, out ExTimeZone timeZone)
		{
			return TimeZoneSettings.TryFindOwaTimeZone(mailboxSession, out timeZone);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00018E1B File Offset: 0x0001701B
		protected virtual bool TryFindOutlookTimeZone(MailboxSession mailboxSession, out byte[] blob)
		{
			return TimeZoneSettings.TryFindOutlookTimeZone(mailboxSession, out blob);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00018E24 File Offset: 0x00017024
		protected virtual bool TryParseTimeZoneBlob(byte[] blob, string displayName, out ExTimeZone timeZone)
		{
			return O12TimeZoneFormatter.TryParseTimeZoneBlob(blob, displayName, out timeZone);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00018E2E File Offset: 0x0001702E
		protected virtual bool CheckMailboxSessionForTimeZone(MailboxSession mailboxSession)
		{
			if (mailboxSession == null)
			{
				this.timeZoneRetrieved = true;
				this.timeZone = ExTimeZone.CurrentTimeZone;
				base.TraceDebug<Type, ExTimeZone>("Session is not MailboxSession, using server time zone instead. SessionType: {0}, TimeZone: {1}", base.StoreSession.GetType(), this.timeZone);
				return false;
			}
			return true;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00018E64 File Offset: 0x00017064
		private void AddRuleDumpAttachment(EmailMessage traceReport, Folder folder, string attachmentFileName)
		{
			Attachment attachment = traceReport.Attachments.Add(attachmentFileName);
			using (Stream contentWriteStream = attachment.GetContentWriteStream())
			{
				MailboxSession session = base.StoreSession as MailboxSession;
				using (RuleWriter ruleWriter = new RuleWriter(session, folder, contentWriteStream))
				{
					ruleWriter.WriteRules();
				}
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00018ED4 File Offset: 0x000170D4
		private void AddOofHistoryAttachment(EmailMessage traceReport, string attachmentFileName)
		{
			Attachment attachment = traceReport.Attachments.Add(attachmentFileName);
			using (Stream contentWriteStream = attachment.GetContentWriteStream())
			{
				using (StreamWriter streamWriter = new StreamWriter(contentWriteStream))
				{
					StoreSession storeSession = base.StoreSession;
					using (OofHistory oofHistory = new OofHistory(null, null, this))
					{
						if (oofHistory.TryInitialize())
						{
							oofHistory.DumpHistory(streamWriter);
						}
						else
						{
							streamWriter.WriteLine("Automatic reply history does not exist.");
						}
					}
				}
			}
		}

		// Token: 0x040002A0 RID: 672
		private List<byte[]> daeMessageEntryIds;

		// Token: 0x040002A1 RID: 673
		private Folder initialFolder;

		// Token: 0x040002A2 RID: 674
		private bool timeZoneRetrieved;

		// Token: 0x040002A3 RID: 675
		private ExTimeZone timeZone;

		// Token: 0x040002A4 RID: 676
		private MailItemDeliver mailItemDeliver;

		// Token: 0x040002A5 RID: 677
		protected StoreDriverServer server;
	}
}
