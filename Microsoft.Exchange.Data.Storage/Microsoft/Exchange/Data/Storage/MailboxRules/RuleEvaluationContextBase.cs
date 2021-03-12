using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BEE RID: 3054
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RuleEvaluationContextBase : DisposeTrackableBase, IRuleEvaluationContext, IDisposable
	{
		// Token: 0x06006C9B RID: 27803 RVA: 0x001D0DA8 File Offset: 0x001CEFA8
		protected RuleEvaluationContextBase(Folder folder, MessageItem message, StoreSession session, ProxyAddress recipient, IADRecipientCache recipientCache, long mimeSize, IRuleConfig ruleConfig, Trace tracer)
		{
			if (message != null)
			{
				this.ruleHistory = message.GetRuleHistory(session);
			}
			this.message = message;
			this.session = session;
			this.cultureInfo = this.session.PreferedCulture;
			this.folderSet = new Dictionary<StoreId, Folder>();
			this.TrackFolder(folder.Id, folder);
			this.folder = folder;
			this.recipientAddress = recipient;
			this.recipientCache = recipientCache;
			this.mimeSize = mimeSize;
			this.ruleConfig = ruleConfig;
			this.tracer = tracer;
		}

		// Token: 0x06006C9C RID: 27804 RVA: 0x001D0E34 File Offset: 0x001CF034
		protected RuleEvaluationContextBase(RuleEvaluationContextBase parentContext)
		{
			this.message = parentContext.message;
			this.ruleHistory = parentContext.ruleHistory;
			this.session = parentContext.session;
			this.cultureInfo = parentContext.cultureInfo;
			this.folderSet = parentContext.folderSet;
			this.folder = parentContext.folder;
			this.recipientAddress = parentContext.recipientAddress;
			this.recipientCache = parentContext.recipientCache;
			this.mimeSize = parentContext.mimeSize;
			this.traceFormatter = parentContext.traceFormatter;
			this.currentRule = parentContext.currentRule;
			this.limitChecker = parentContext.limitChecker;
			this.ruleConfig = parentContext.ruleConfig;
			this.tracer = parentContext.tracer;
			this.ShouldExecuteDisabledAndInErrorRules = parentContext.ShouldExecuteDisabledAndInErrorRules;
		}

		// Token: 0x06006C9D RID: 27805 RVA: 0x001D0EFB File Offset: 0x001CF0FB
		protected RuleEvaluationContextBase()
		{
		}

		// Token: 0x17001D89 RID: 7561
		// (get) Token: 0x06006C9E RID: 27806 RVA: 0x001D0F03 File Offset: 0x001CF103
		// (set) Token: 0x06006C9F RID: 27807 RVA: 0x001D0F0B File Offset: 0x001CF10B
		public CultureInfo CultureInfo
		{
			get
			{
				return this.cultureInfo;
			}
			internal set
			{
				this.cultureInfo = value;
			}
		}

		// Token: 0x17001D8A RID: 7562
		// (get) Token: 0x06006CA0 RID: 27808 RVA: 0x001D0F14 File Offset: 0x001CF114
		// (set) Token: 0x06006CA1 RID: 27809 RVA: 0x001D0F1C File Offset: 0x001CF11C
		public Folder CurrentFolder
		{
			get
			{
				return this.folder;
			}
			set
			{
				this.folder = value;
			}
		}

		// Token: 0x17001D8B RID: 7563
		// (get) Token: 0x06006CA2 RID: 27810 RVA: 0x001D0F25 File Offset: 0x001CF125
		public string CurrentFolderDisplayName
		{
			get
			{
				return this.CurrentFolder.DisplayName;
			}
		}

		// Token: 0x17001D8C RID: 7564
		// (get) Token: 0x06006CA3 RID: 27811 RVA: 0x001D0F32 File Offset: 0x001CF132
		// (set) Token: 0x06006CA4 RID: 27812 RVA: 0x001D0F3A File Offset: 0x001CF13A
		public Rule CurrentRule
		{
			get
			{
				return this.currentRule;
			}
			set
			{
				this.currentRule = value;
			}
		}

		// Token: 0x17001D8D RID: 7565
		// (get) Token: 0x06006CA5 RID: 27813
		public abstract string DefaultDomainName { get; }

		// Token: 0x17001D8E RID: 7566
		// (get) Token: 0x06006CA6 RID: 27814 RVA: 0x001D0F43 File Offset: 0x001CF143
		// (set) Token: 0x06006CA7 RID: 27815 RVA: 0x001D0F4B File Offset: 0x001CF14B
		public MessageItem DeliveredMessage
		{
			get
			{
				return this.deliveredMessage;
			}
			set
			{
				this.deliveredMessage = value;
			}
		}

		// Token: 0x17001D8F RID: 7567
		// (get) Token: 0x06006CA8 RID: 27816 RVA: 0x001D0F54 File Offset: 0x001CF154
		// (set) Token: 0x06006CA9 RID: 27817 RVA: 0x001D0F5C File Offset: 0x001CF15C
		public Folder DeliveryFolder
		{
			get
			{
				return this.deliveryFolder;
			}
			set
			{
				this.deliveryFolder = value;
			}
		}

		// Token: 0x17001D90 RID: 7568
		// (get) Token: 0x06006CAA RID: 27818 RVA: 0x001D0F65 File Offset: 0x001CF165
		public List<string> ErrorRecords
		{
			get
			{
				return this.errorRecords;
			}
		}

		// Token: 0x17001D91 RID: 7569
		// (get) Token: 0x06006CAB RID: 27819 RVA: 0x001D0F6D File Offset: 0x001CF16D
		// (set) Token: 0x06006CAC RID: 27820 RVA: 0x001D0F75 File Offset: 0x001CF175
		public ExecutionStage ExecutionStage
		{
			get
			{
				return this.executionStage;
			}
			set
			{
				this.executionStage = value;
			}
		}

		// Token: 0x17001D92 RID: 7570
		// (get) Token: 0x06006CAD RID: 27821
		public abstract List<KeyValuePair<string, string>> ExtraTrackingEventData { get; }

		// Token: 0x17001D93 RID: 7571
		// (get) Token: 0x06006CAE RID: 27822 RVA: 0x001D0F7E File Offset: 0x001CF17E
		// (set) Token: 0x06006CAF RID: 27823 RVA: 0x001D0F86 File Offset: 0x001CF186
		public StoreId FinalDeliveryFolderId
		{
			get
			{
				return this.finalDeliveryFolderId;
			}
			set
			{
				this.finalDeliveryFolderId = value;
			}
		}

		// Token: 0x17001D94 RID: 7572
		// (get) Token: 0x06006CB0 RID: 27824 RVA: 0x001D0F8F File Offset: 0x001CF18F
		// (set) Token: 0x06006CB1 RID: 27825 RVA: 0x001D0F97 File Offset: 0x001CF197
		public virtual bool IsOof
		{
			get
			{
				return this.isOof;
			}
			protected set
			{
				this.isOof = value;
			}
		}

		// Token: 0x17001D95 RID: 7573
		// (get) Token: 0x06006CB2 RID: 27826 RVA: 0x001D0FA0 File Offset: 0x001CF1A0
		public bool IsInternetMdnDisabled
		{
			get
			{
				object obj = this.StoreSession.Mailbox.TryGetProperty(MailboxSchema.InternetMdns);
				return obj != null && obj is bool && (bool)obj;
			}
		}

		// Token: 0x17001D96 RID: 7574
		// (get) Token: 0x06006CB3 RID: 27827
		public abstract IsMemberOfResolver<string> IsMemberOfResolver { get; }

		// Token: 0x17001D97 RID: 7575
		// (get) Token: 0x06006CB4 RID: 27828
		public abstract string LocalServerFqdn { get; }

		// Token: 0x17001D98 RID: 7576
		// (get) Token: 0x06006CB5 RID: 27829
		public abstract IPAddress LocalServerNetworkAddress { get; }

		// Token: 0x17001D99 RID: 7577
		// (get) Token: 0x06006CB6 RID: 27830 RVA: 0x001D0FD6 File Offset: 0x001CF1D6
		// (set) Token: 0x06006CB7 RID: 27831 RVA: 0x001D0FDE File Offset: 0x001CF1DE
		public MessageItem Message
		{
			get
			{
				return this.message;
			}
			protected set
			{
				this.message = value;
			}
		}

		// Token: 0x17001D9A RID: 7578
		// (get) Token: 0x06006CB8 RID: 27832 RVA: 0x001D0FE7 File Offset: 0x001CF1E7
		public long MimeSize
		{
			get
			{
				return this.mimeSize;
			}
		}

		// Token: 0x17001D9B RID: 7579
		// (get) Token: 0x06006CB9 RID: 27833 RVA: 0x001D0FEF File Offset: 0x001CF1EF
		// (set) Token: 0x06006CBA RID: 27834 RVA: 0x001D0FFC File Offset: 0x001CF1FC
		public int NestedLevel
		{
			get
			{
				return this.traceFormatter.NestedLevel;
			}
			set
			{
				this.traceFormatter.NestedLevel = value;
			}
		}

		// Token: 0x17001D9C RID: 7580
		// (get) Token: 0x06006CBB RID: 27835 RVA: 0x001D100A File Offset: 0x001CF20A
		// (set) Token: 0x06006CBC RID: 27836 RVA: 0x001D1012 File Offset: 0x001CF212
		public Dictionary<PropertyDefinition, object> PropertiesForAllMessageCopies
		{
			get
			{
				return this.propertiesForAllMessageCopies;
			}
			set
			{
				this.propertiesForAllMessageCopies = value;
			}
		}

		// Token: 0x17001D9D RID: 7581
		// (get) Token: 0x06006CBD RID: 27837 RVA: 0x001D101B File Offset: 0x001CF21B
		// (set) Token: 0x06006CBE RID: 27838 RVA: 0x001D1023 File Offset: 0x001CF223
		public Dictionary<PropertyDefinition, object> PropertiesForDelegateForward
		{
			get
			{
				return this.propertiesForDelegateForward;
			}
			set
			{
				this.propertiesForDelegateForward = value;
			}
		}

		// Token: 0x17001D9E RID: 7582
		// (get) Token: 0x06006CBF RID: 27839 RVA: 0x001D102C File Offset: 0x001CF22C
		// (set) Token: 0x06006CC0 RID: 27840 RVA: 0x001D1034 File Offset: 0x001CF234
		public Dictionary<PropertyDefinition, object> SharedPropertiesBetweenAgents
		{
			get
			{
				return this.sharedPropertiesBetweenAgents;
			}
			set
			{
				this.sharedPropertiesBetweenAgents = value;
			}
		}

		// Token: 0x17001D9F RID: 7583
		// (get) Token: 0x06006CC1 RID: 27841 RVA: 0x001D103D File Offset: 0x001CF23D
		// (set) Token: 0x06006CC2 RID: 27842 RVA: 0x001D1045 File Offset: 0x001CF245
		public ProxyAddress Recipient
		{
			get
			{
				return this.recipientAddress;
			}
			protected set
			{
				this.recipientAddress = value;
			}
		}

		// Token: 0x17001DA0 RID: 7584
		// (get) Token: 0x06006CC3 RID: 27843 RVA: 0x001D104E File Offset: 0x001CF24E
		public IADRecipientCache RecipientCache
		{
			get
			{
				return this.recipientCache;
			}
		}

		// Token: 0x17001DA1 RID: 7585
		// (get) Token: 0x06006CC4 RID: 27844
		public abstract ExEventLog.EventTuple OofHistoryCorruption { get; }

		// Token: 0x17001DA2 RID: 7586
		// (get) Token: 0x06006CC5 RID: 27845
		public abstract ExEventLog.EventTuple OofHistoryFolderMissing { get; }

		// Token: 0x17001DA3 RID: 7587
		// (get) Token: 0x06006CC6 RID: 27846 RVA: 0x001D1056 File Offset: 0x001CF256
		public bool ProcessingTestMessage
		{
			get
			{
				return this.traceFormatter.HasTraceHistory;
			}
		}

		// Token: 0x17001DA4 RID: 7588
		// (get) Token: 0x06006CC7 RID: 27847 RVA: 0x001D1063 File Offset: 0x001CF263
		// (set) Token: 0x06006CC8 RID: 27848 RVA: 0x001D106B File Offset: 0x001CF26B
		public bool ShouldExecuteDisabledAndInErrorRules { get; protected set; }

		// Token: 0x17001DA5 RID: 7589
		// (get) Token: 0x06006CC9 RID: 27849 RVA: 0x001D1074 File Offset: 0x001CF274
		public ProxyAddress Sender
		{
			get
			{
				if (this.message.Sender == null)
				{
					return null;
				}
				return ProxyAddress.Parse(this.message.Sender.RoutingType ?? string.Empty, this.message.Sender.EmailAddress ?? string.Empty);
			}
		}

		// Token: 0x17001DA6 RID: 7590
		// (get) Token: 0x06006CCA RID: 27850 RVA: 0x001D10CF File Offset: 0x001CF2CF
		// (set) Token: 0x06006CCB RID: 27851 RVA: 0x001D10D7 File Offset: 0x001CF2D7
		public string SenderAddress
		{
			get
			{
				return this.senderAddress;
			}
			set
			{
				this.senderAddress = value;
			}
		}

		// Token: 0x17001DA7 RID: 7591
		// (get) Token: 0x06006CCC RID: 27852 RVA: 0x001D10E0 File Offset: 0x001CF2E0
		// (set) Token: 0x06006CCD RID: 27853 RVA: 0x001D10E8 File Offset: 0x001CF2E8
		public bool ShouldSkipMoveRule
		{
			get
			{
				return this.shouldSkipMoveRule;
			}
			set
			{
				this.shouldSkipMoveRule = value;
			}
		}

		// Token: 0x17001DA8 RID: 7592
		// (get) Token: 0x06006CCE RID: 27854 RVA: 0x001D10F1 File Offset: 0x001CF2F1
		// (set) Token: 0x06006CCF RID: 27855 RVA: 0x001D10F9 File Offset: 0x001CF2F9
		public StoreSession StoreSession
		{
			get
			{
				return this.session;
			}
			protected set
			{
				this.session = value;
			}
		}

		// Token: 0x17001DA9 RID: 7593
		public object this[PropTag tag]
		{
			get
			{
				tag = RuleUtil.NormalizeTag(tag);
				object obj = this.CalculatePropertyValue(tag);
				if (obj == null)
				{
					obj = this.GetPropertyValue(tag);
				}
				RuleUtil.CheckValueType(obj, tag);
				return obj;
			}
		}

		// Token: 0x17001DAA RID: 7594
		// (get) Token: 0x06006CD1 RID: 27857 RVA: 0x001D1134 File Offset: 0x001CF334
		public string XLoopValue
		{
			get
			{
				return this.recipientAddress.AddressString;
			}
		}

		// Token: 0x17001DAB RID: 7595
		// (get) Token: 0x06006CD2 RID: 27858 RVA: 0x001D1141 File Offset: 0x001CF341
		// (set) Token: 0x06006CD3 RID: 27859 RVA: 0x001D1149 File Offset: 0x001CF349
		public LimitChecker LimitChecker
		{
			get
			{
				return this.limitChecker;
			}
			protected set
			{
				this.limitChecker = value;
			}
		}

		// Token: 0x17001DAC RID: 7596
		// (get) Token: 0x06006CD4 RID: 27860 RVA: 0x001D1152 File Offset: 0x001CF352
		public IRuleConfig RuleConfig
		{
			get
			{
				return this.ruleConfig;
			}
		}

		// Token: 0x17001DAD RID: 7597
		// (get) Token: 0x06006CD5 RID: 27861
		protected abstract IStorePropertyBag PropertyBag { get; }

		// Token: 0x06006CD6 RID: 27862 RVA: 0x001D115C File Offset: 0x001CF35C
		protected static string GetMessageBody(RuleEvaluationContextBase context)
		{
			string text;
			using (TextReader textReader = context.Message.Body.OpenTextReader(BodyFormat.TextPlain))
			{
				text = textReader.ReadToEnd();
				if (text == null)
				{
					text = string.Empty;
				}
			}
			return text;
		}

		// Token: 0x06006CD7 RID: 27863 RVA: 0x001D11A8 File Offset: 0x001CF3A8
		protected static RecipientItemType GetRecipientType(RuleEvaluationContextBase context)
		{
			RecipientItemType result = RecipientItemType.Unknown;
			string text = context.PropertyBag[StoreObjectSchema.ItemClass] as string;
			if (!string.IsNullOrEmpty(text) && ObjectClass.IsReport(text))
			{
				result = RecipientItemType.To;
			}
			else
			{
				RecipientCollection recipients = context.Message.Recipients;
				for (int i = 0; i < recipients.Count; i++)
				{
					if (recipients[i].Participant.RoutingType != null && recipients[i].Participant.EmailAddress != null)
					{
						ProxyAddress addressToResolve = ProxyAddress.Parse(recipients[i].Participant.RoutingType, recipients[i].Participant.EmailAddress);
						if (RuleUtil.IsSameUser(context, context.RecipientCache, addressToResolve, context.Recipient))
						{
							result = recipients[i].RecipientItemType;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06006CD8 RID: 27864 RVA: 0x001D127C File Offset: 0x001CF47C
		public virtual bool CompareSingleValue(PropTag tag, Restriction.RelOp op, object x, object y)
		{
			if (x == null)
			{
				return y == null;
			}
			if (y == null)
			{
				return false;
			}
			if (!RestrictionEvaluator.IsSupportedOperation(op))
			{
				throw new InvalidRuleException(string.Format("Operation {0} is not supported", op));
			}
			if (x.GetType() != y.GetType() && (!(x.GetType() == typeof(ExDateTime)) || !(y.GetType() == typeof(DateTime))))
			{
				throw new InvalidRuleException(string.Format("Can not compare values of type {0} and type {1}", x.GetType(), y.GetType()));
			}
			if (op == Restriction.RelOp.MemberOfDL)
			{
				byte[] array = x as byte[];
				if (array == null)
				{
					this.TraceDebug("CompareSingleValue: MemberOf, recipientEntryId is not a byte array.");
					this.RecordError(ServerStrings.FolderRuleErrorInvalidRecipientEntryId);
					return false;
				}
				byte[] array2 = y as byte[];
				if (array2 == null)
				{
					this.TraceDebug("CompareSingleValue: MemberOf, groupEntryId is not a byte array, marking rule in error.");
					throw new InvalidRuleException(ServerStrings.FolderRuleErrorInvalidGroup(y.GetType().Name));
				}
				return RuleUtil.IsMemberOf(this, array, array2);
			}
			else
			{
				int? num = RestrictionEvaluator.CompareValue(this.CultureInfo, tag, x, y);
				if (num == null)
				{
					throw new InvalidRuleException(string.Format("Can't compare value '{0}' and '{1}'", x, y));
				}
				return RestrictionEvaluator.GetOperationResultFromOrder(op, num.Value);
			}
		}

		// Token: 0x06006CD9 RID: 27865 RVA: 0x001D13B8 File Offset: 0x001CF5B8
		public void CopyProperty(MessageItem src, MessageItem target, PropertyDefinition property)
		{
			object obj = src.TryGetProperty(property);
			if (!(obj is PropertyError))
			{
				target[property] = obj;
				return;
			}
			this.TraceDebug<object>("Can't read property: {0}", obj);
		}

		// Token: 0x06006CDA RID: 27866
		public abstract MessageItem CreateMessageItem(PropertyDefinition[] prefetchProperties);

		// Token: 0x06006CDB RID: 27867 RVA: 0x001D13EA File Offset: 0x001CF5EA
		public virtual void DisableAndMarkRuleInError(Rule rule, RuleAction.Type actionType, int actionIndex, DeferredError.RuleError errorCode)
		{
			this.TraceError<Rule, DeferredError.RuleError>("Rule {0} will be disabled due to error: {1}", rule, errorCode);
			RuleUtil.DisableRule(rule, this.folder.MapiFolder);
			this.MarkRuleInError(rule, actionType, actionIndex, errorCode);
		}

		// Token: 0x06006CDC RID: 27868 RVA: 0x001D1416 File Offset: 0x001CF616
		public virtual void MarkRuleInError(Rule rule, RuleAction.Type actionType, int actionIndex, DeferredError.RuleError errorCode)
		{
			RuleUtil.MarkRuleInError(rule, this.folder.MapiFolder);
		}

		// Token: 0x06006CDD RID: 27869 RVA: 0x001D142C File Offset: 0x001CF62C
		public virtual void RecordError(Exception exception, string stage)
		{
			string text2;
			if (this.currentRule != null)
			{
				string text = ((ulong)this.currentRule.ID).ToString();
				this.TraceError<string, Exception>("Unexpected exception while processing rule \"{0}\": {1}", text, exception);
				text2 = ServerStrings.FolderRuleErrorRecordForSpecificRule(text, this.recipientAddress.ToString(), stage, exception.GetType().Name, exception.Message);
			}
			else
			{
				this.TraceError<Exception>("Unexpected exception while processing rules: {0}", exception);
				text2 = ServerStrings.FolderRuleErrorRecord(this.recipientAddress.ToString(), stage, exception.GetType().Name, exception.Message);
			}
			RuleStatistics.LogException(exception);
			this.RecordError(text2);
		}

		// Token: 0x06006CDE RID: 27870 RVA: 0x001D14CE File Offset: 0x001CF6CE
		public virtual void RecordError(string message)
		{
			if (this.errorRecords == null)
			{
				this.errorRecords = new List<string>(1);
			}
			this.errorRecords.Add(message);
		}

		// Token: 0x06006CDF RID: 27871
		public abstract ISubmissionItem GenerateSubmissionItem(MessageItem item, WorkItem workItem);

		// Token: 0x06006CE0 RID: 27872 RVA: 0x001D14F0 File Offset: 0x001CF6F0
		public virtual IRuleEvaluationContext GetAttachmentContext(Attachment attachment)
		{
			throw new InvalidRuleException("Attachment sub restriction can only be used in message context");
		}

		// Token: 0x06006CE1 RID: 27873
		public abstract Folder GetDeletedItemsFolder();

		// Token: 0x06006CE2 RID: 27874 RVA: 0x001D14FC File Offset: 0x001CF6FC
		public StorePropertyDefinition GetPropertyDefinitionForTag(PropTag tag)
		{
			if (tag == PropTag.DisplayBcc)
			{
				return ItemSchema.DisplayBcc;
			}
			if (tag == PropTag.DisplayCc)
			{
				return ItemSchema.DisplayCc;
			}
			if (tag == PropTag.DisplayTo)
			{
				return ItemSchema.DisplayTo;
			}
			NativeStorePropertyDefinition[] array = PropertyTagCache.Cache.PropertyDefinitionsFromPropTags(NativeStorePropertyDefinition.TypeCheckingFlag.DoNotCreateInvalidType, this.StoreSession.Mailbox.MapiStore, this.StoreSession, new PropTag[]
			{
				tag
			});
			if (array == null || array.Length == 0 || array[0] == null)
			{
				throw new InvalidRuleException(string.Format("Can't get property definition for tag {0}", tag));
			}
			return array[0];
		}

		// Token: 0x06006CE3 RID: 27875 RVA: 0x001D1589 File Offset: 0x001CF789
		public virtual IRuleEvaluationContext GetRecipientContext(Recipient recipient)
		{
			throw new InvalidRuleException("Recipient sub restriction can only be used in message context");
		}

		// Token: 0x06006CE4 RID: 27876
		public abstract void SetMailboxOwnerAsSender(MessageItem message);

		// Token: 0x06006CE5 RID: 27877 RVA: 0x001D1595 File Offset: 0x001CF795
		public virtual bool HasRuleFiredBefore(Rule rule)
		{
			if (this.ruleHistory.Count > 11)
			{
				this.TraceDebug("Rule history length has exceeded the limit. Treat this as loop.");
				return true;
			}
			return this.ruleHistory.Contains(rule.ID);
		}

		// Token: 0x06006CE6 RID: 27878 RVA: 0x001D15C4 File Offset: 0x001CF7C4
		public void AddCurrentFolderIdTo(HashSet<StoreId> hashSet)
		{
			hashSet.Add(this.CurrentFolder.Id);
		}

		// Token: 0x06006CE7 RID: 27879 RVA: 0x001D15D7 File Offset: 0x001CF7D7
		public virtual List<Rule> LoadRules()
		{
			return RuleLoader.LoadRules(this);
		}

		// Token: 0x06006CE8 RID: 27880
		public abstract void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs);

		// Token: 0x06006CE9 RID: 27881 RVA: 0x001D15DF File Offset: 0x001CF7DF
		public virtual void LogWorkItemExecution(WorkItem workItem)
		{
			this.TraceDebug<WorkItem>("Executed work item: {0}", workItem);
		}

		// Token: 0x06006CEA RID: 27882 RVA: 0x001D15F0 File Offset: 0x001CF7F0
		public Folder OpenFolder(StoreId folderId)
		{
			Folder result;
			if (this.folderSet.TryGetValue(folderId, out result))
			{
				return result;
			}
			result = Folder.Bind(this.session, folderId, RuleEvaluationContextBase.AdditionalFolderProperties);
			this.TrackFolder(folderId, result);
			return result;
		}

		// Token: 0x06006CEB RID: 27883 RVA: 0x001D162C File Offset: 0x001CF82C
		public bool DetectLoop()
		{
			string[] valueOrDefault = this.message.GetValueOrDefault<string[]>(MessageItemSchema.XLoop, null);
			if (valueOrDefault == null)
			{
				this.TraceDebug("No X-Loop values present.");
				return false;
			}
			if (this.limitChecker.DoesExceedXLoopMaxCount(valueOrDefault.Length))
			{
				return true;
			}
			this.TraceDebug<string>("Looking for X-Loop header containing: {0}", this.XLoopValue);
			string[] array = valueOrDefault;
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i];
				this.TraceDebug<string>("X-Loop: {0}", text);
				bool result;
				if (text.Length >= 1000)
				{
					this.TraceDebug("Possible loop detected: value exceeds allowable length.");
					result = true;
				}
				else
				{
					if (!this.XLoopValue.Equals(text, StringComparison.OrdinalIgnoreCase))
					{
						i++;
						continue;
					}
					this.TraceDebug("Loop detected: value matches the user's X-Loop token.");
					result = true;
				}
				return result;
			}
			this.TraceDebug("No loop detected.");
			return false;
		}

		// Token: 0x06006CEC RID: 27884 RVA: 0x001D16EC File Offset: 0x001CF8EC
		public void TraceDebug(string message)
		{
			bool flag = this.tracer.IsTraceEnabled(TraceType.DebugTrace);
			if (flag || this.ProcessingTestMessage)
			{
				string text = this.traceFormatter.Format(message);
				if (flag)
				{
					this.tracer.TraceDebug(0L, text);
				}
			}
		}

		// Token: 0x06006CED RID: 27885 RVA: 0x001D1730 File Offset: 0x001CF930
		public void TraceDebug<T>(string format, T argument)
		{
			bool flag = this.tracer.IsTraceEnabled(TraceType.DebugTrace);
			if (flag || this.ProcessingTestMessage)
			{
				string text = this.traceFormatter.Format(format, argument);
				if (flag)
				{
					this.tracer.TraceDebug(0L, text);
				}
			}
		}

		// Token: 0x06006CEE RID: 27886 RVA: 0x001D177C File Offset: 0x001CF97C
		public void TraceDebug<T1, T2>(string format, T1 argument1, T2 argument2)
		{
			bool flag = this.tracer.IsTraceEnabled(TraceType.DebugTrace);
			if (flag || this.ProcessingTestMessage)
			{
				string text = this.traceFormatter.Format(format, argument1, argument2);
				if (flag)
				{
					this.tracer.TraceDebug(0L, text);
				}
			}
		}

		// Token: 0x06006CEF RID: 27887 RVA: 0x001D17CC File Offset: 0x001CF9CC
		public void TraceError(string message)
		{
			bool flag = this.tracer.IsTraceEnabled(TraceType.ErrorTrace);
			if (flag || this.ProcessingTestMessage)
			{
				string text = this.traceFormatter.Format(message);
				if (flag)
				{
					this.tracer.TraceError(0L, text);
				}
			}
		}

		// Token: 0x06006CF0 RID: 27888 RVA: 0x001D1810 File Offset: 0x001CFA10
		public void TraceError<T>(string format, T argument)
		{
			bool flag = this.tracer.IsTraceEnabled(TraceType.ErrorTrace);
			if (flag || this.ProcessingTestMessage)
			{
				string text = this.traceFormatter.Format(format, argument);
				if (flag)
				{
					this.tracer.TraceError(0L, text);
				}
			}
		}

		// Token: 0x06006CF1 RID: 27889 RVA: 0x001D185C File Offset: 0x001CFA5C
		public void TraceError<T1, T2>(string format, T1 argument1, T2 argument2)
		{
			bool flag = this.tracer.IsTraceEnabled(TraceType.ErrorTrace);
			if (flag || this.ProcessingTestMessage)
			{
				string text = this.traceFormatter.Format(format, argument1, argument2);
				if (flag)
				{
					this.tracer.TraceError(0L, text);
				}
			}
		}

		// Token: 0x06006CF2 RID: 27890 RVA: 0x001D18AC File Offset: 0x001CFAAC
		public void TraceError<T1, T2, T3>(string format, T1 argument1, T2 argument2, T3 argument3)
		{
			bool flag = this.tracer.IsTraceEnabled(TraceType.ErrorTrace);
			if (flag || this.ProcessingTestMessage)
			{
				string text = this.traceFormatter.Format(format, argument1, argument2, argument3);
				if (flag)
				{
					this.tracer.TraceError(0L, text);
				}
			}
		}

		// Token: 0x06006CF3 RID: 27891 RVA: 0x001D1904 File Offset: 0x001CFB04
		public void TraceFunction(string message)
		{
			bool flag = this.tracer.IsTraceEnabled(TraceType.FunctionTrace);
			if (flag || this.ProcessingTestMessage)
			{
				string text = this.traceFormatter.Format(message);
				if (flag)
				{
					this.tracer.TraceFunction(0L, text);
				}
			}
		}

		// Token: 0x06006CF4 RID: 27892 RVA: 0x001D1948 File Offset: 0x001CFB48
		public void TraceFunction<T>(string format, T argument)
		{
			bool flag = this.tracer.IsTraceEnabled(TraceType.FunctionTrace);
			if (flag || this.ProcessingTestMessage)
			{
				string text = this.traceFormatter.Format(format, argument);
				if (flag)
				{
					this.tracer.TraceFunction(0L, text);
				}
			}
		}

		// Token: 0x06006CF5 RID: 27893 RVA: 0x001D1994 File Offset: 0x001CFB94
		public void TraceFunction<T1, T2>(string format, T1 argument1, T2 argument2)
		{
			bool flag = this.tracer.IsTraceEnabled(TraceType.FunctionTrace);
			if (flag || this.ProcessingTestMessage)
			{
				string text = this.traceFormatter.Format(format, argument1, argument2);
				if (flag)
				{
					this.tracer.TraceFunction(0L, text);
				}
			}
		}

		// Token: 0x06006CF6 RID: 27894 RVA: 0x001D19E4 File Offset: 0x001CFBE4
		public void TraceFunction<T1, T2, T3>(string format, T1 argument1, T2 argument2, T3 argument3)
		{
			bool flag = this.tracer.IsTraceEnabled(TraceType.FunctionTrace);
			if (flag || this.ProcessingTestMessage)
			{
				string text = this.traceFormatter.Format(format, argument1, argument2, argument3);
				if (flag)
				{
					this.tracer.TraceFunction(0L, text);
				}
			}
		}

		// Token: 0x06006CF7 RID: 27895 RVA: 0x001D1A3C File Offset: 0x001CFC3C
		public void TraceFunction<T1, T2, T3, T4>(string format, T1 argument1, T2 argument2, T3 argument3, T4 argument4)
		{
			bool flag = this.tracer.IsTraceEnabled(TraceType.FunctionTrace);
			if (flag || this.ProcessingTestMessage)
			{
				string text = this.traceFormatter.Format(format, argument1, argument2, argument3, argument4);
				if (flag)
				{
					this.tracer.TraceFunction(0L, text);
				}
			}
		}

		// Token: 0x06006CF8 RID: 27896 RVA: 0x001D1A9C File Offset: 0x001CFC9C
		public bool TryOpenLocalStore(byte[] folderEntryId, out Folder folder)
		{
			folder = null;
			StoreObjectId storeObjectId = null;
			if (!IdConverter.IsFolderId(folderEntryId))
			{
				this.TraceDebug<byte[]>("Can't open folder, id {0} is not an Exchange folder entry id.", folderEntryId);
				return false;
			}
			try
			{
				storeObjectId = StoreObjectId.FromProviderSpecificId(folderEntryId, StoreObjectType.Folder);
			}
			catch (CorruptDataException argument)
			{
				this.TraceDebug<byte[], CorruptDataException>("Can't open folder, id {0} is corrupt: {1}", folderEntryId, argument);
				return false;
			}
			bool result;
			try
			{
				folder = this.OpenFolder(storeObjectId);
				result = true;
			}
			catch (ObjectNotFoundException argument2)
			{
				this.TraceDebug<StoreObjectId, ObjectNotFoundException>("Can't open the folder with id {0} due to error {1}", storeObjectId, argument2);
				result = false;
			}
			return result;
		}

		// Token: 0x06006CF9 RID: 27897
		public abstract ExTimeZone DetermineRecipientTimeZone();

		// Token: 0x06006CFA RID: 27898 RVA: 0x001D1B1C File Offset: 0x001CFD1C
		protected override void InternalDispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (this.folderSet != null)
			{
				foreach (Folder folder in this.folderSet.Values)
				{
					folder.Dispose();
				}
				this.folderSet = null;
			}
			if (this.traceFormatter != null)
			{
				this.traceFormatter.Dispose();
				this.traceFormatter = null;
			}
		}

		// Token: 0x06006CFB RID: 27899 RVA: 0x001D1BA0 File Offset: 0x001CFDA0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RuleEvaluationContextBase>(this);
		}

		// Token: 0x06006CFC RID: 27900 RVA: 0x001D1BA8 File Offset: 0x001CFDA8
		protected virtual object CalculatePropertyValue(PropTag tag)
		{
			return null;
		}

		// Token: 0x06006CFD RID: 27901 RVA: 0x001D1BAC File Offset: 0x001CFDAC
		public bool CompareAddresses(object messageValue, object ruleValue)
		{
			ProxyAddress proxyAddressFromSearchKey = RuleUtil.GetProxyAddressFromSearchKey(messageValue);
			ProxyAddress proxyAddressFromSearchKey2 = RuleUtil.GetProxyAddressFromSearchKey(ruleValue);
			if (proxyAddressFromSearchKey2 == null || proxyAddressFromSearchKey2 is InvalidProxyAddress || string.IsNullOrEmpty(proxyAddressFromSearchKey2.ValueString))
			{
				string recipient = ServerStrings.Null;
				if (proxyAddressFromSearchKey2 != null)
				{
					recipient = proxyAddressFromSearchKey2.ToString();
				}
				this.DisableAndMarkRuleInError(this.CurrentRule, RuleAction.Type.OP_INVALID, 0, DeferredError.RuleError.Parsing);
				this.RecordError(ServerStrings.FolderRuleErrorInvalidRecipient(recipient));
				return false;
			}
			this.TraceDebug<ProxyAddress, ProxyAddress>("Comparing recipients, message address {0}, rule address {1}", proxyAddressFromSearchKey, proxyAddressFromSearchKey2);
			RuleUtil.FaultInjection((FaultInjectionLid)4257530192U);
			return RuleUtil.IsSameUser(this, this.RecipientCache, proxyAddressFromSearchKey, proxyAddressFromSearchKey2);
		}

		// Token: 0x06006CFE RID: 27902 RVA: 0x001D1C48 File Offset: 0x001CFE48
		protected object GetPropertyValue(PropTag tag)
		{
			StorePropertyDefinition propertyDefinitionForTag = this.GetPropertyDefinitionForTag(tag);
			IStorePropertyBag propertyBag = this.PropertyBag;
			object obj = propertyBag.TryGetProperty(propertyDefinitionForTag);
			if (obj is PropertyError)
			{
				if (!PropertyError.IsPropertyValueTooBig(obj))
				{
					this.TraceError<object, StorePropertyDefinition>("Encountered error {0} while reading value of property {1}", obj, propertyDefinitionForTag);
					return null;
				}
				obj = RuleUtil.ReadStreamedProperty(propertyBag, propertyDefinitionForTag);
			}
			if (obj.GetType().GetTypeInfo().IsEnum)
			{
				obj = RuleUtil.ConvertEnumValue(tag, obj);
			}
			return obj;
		}

		// Token: 0x06006CFF RID: 27903 RVA: 0x001D1CB2 File Offset: 0x001CFEB2
		internal virtual void SetRecipient(ProxyAddress recipient)
		{
			this.Recipient = recipient;
		}

		// Token: 0x06006D00 RID: 27904 RVA: 0x001D1CBB File Offset: 0x001CFEBB
		private void TrackFolder(StoreId folderId, Folder folder)
		{
			this.folderSet[folderId] = folder;
			if (!folder.Id.Equals(folderId))
			{
				this.folderSet[folder.Id] = folder;
			}
		}

		// Token: 0x04003DFC RID: 15868
		private const int MaxNumberOfRulesInHistory = 11;

		// Token: 0x04003DFD RID: 15869
		internal static readonly PropertyDefinition[] AdditionalFolderProperties = new PropertyDefinition[]
		{
			FolderSchema.FolderRulesSize,
			StoreObjectSchema.EntryId,
			StoreObjectSchema.PolicyTag,
			StoreObjectSchema.RetentionPeriod,
			StoreObjectSchema.RetentionFlags,
			StoreObjectSchema.ArchiveTag,
			StoreObjectSchema.ArchivePeriod,
			FolderSchema.RetentionTagEntryId,
			FolderSchema.MailEnabled,
			FolderSchema.ProxyGuid
		};

		// Token: 0x04003DFE RID: 15870
		private CultureInfo cultureInfo;

		// Token: 0x04003DFF RID: 15871
		private Rule currentRule;

		// Token: 0x04003E00 RID: 15872
		private MessageItem deliveredMessage;

		// Token: 0x04003E01 RID: 15873
		private Folder deliveryFolder;

		// Token: 0x04003E02 RID: 15874
		private List<string> errorRecords;

		// Token: 0x04003E03 RID: 15875
		private ExecutionStage executionStage;

		// Token: 0x04003E04 RID: 15876
		private StoreId finalDeliveryFolderId;

		// Token: 0x04003E05 RID: 15877
		private Folder folder;

		// Token: 0x04003E06 RID: 15878
		private Dictionary<StoreId, Folder> folderSet;

		// Token: 0x04003E07 RID: 15879
		private bool isOof;

		// Token: 0x04003E08 RID: 15880
		private LimitChecker limitChecker;

		// Token: 0x04003E09 RID: 15881
		private MessageItem message;

		// Token: 0x04003E0A RID: 15882
		private long mimeSize;

		// Token: 0x04003E0B RID: 15883
		private Dictionary<PropertyDefinition, object> propertiesForAllMessageCopies;

		// Token: 0x04003E0C RID: 15884
		private Dictionary<PropertyDefinition, object> propertiesForDelegateForward;

		// Token: 0x04003E0D RID: 15885
		private Dictionary<PropertyDefinition, object> sharedPropertiesBetweenAgents;

		// Token: 0x04003E0E RID: 15886
		private ProxyAddress recipientAddress;

		// Token: 0x04003E0F RID: 15887
		private IADRecipientCache recipientCache;

		// Token: 0x04003E10 RID: 15888
		private IRuleConfig ruleConfig;

		// Token: 0x04003E11 RID: 15889
		protected RuleHistory ruleHistory;

		// Token: 0x04003E12 RID: 15890
		private string senderAddress;

		// Token: 0x04003E13 RID: 15891
		private StoreSession session;

		// Token: 0x04003E14 RID: 15892
		private bool shouldSkipMoveRule;

		// Token: 0x04003E15 RID: 15893
		protected TraceFormatter traceFormatter;

		// Token: 0x04003E16 RID: 15894
		protected Trace tracer;
	}
}
