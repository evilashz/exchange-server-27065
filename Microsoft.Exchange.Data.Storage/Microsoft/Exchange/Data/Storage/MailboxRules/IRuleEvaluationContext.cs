using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BED RID: 3053
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IRuleEvaluationContext : IDisposable
	{
		// Token: 0x17001D66 RID: 7526
		// (get) Token: 0x06006C4A RID: 27722
		CultureInfo CultureInfo { get; }

		// Token: 0x17001D67 RID: 7527
		// (get) Token: 0x06006C4B RID: 27723
		// (set) Token: 0x06006C4C RID: 27724
		Folder CurrentFolder { get; set; }

		// Token: 0x17001D68 RID: 7528
		// (get) Token: 0x06006C4D RID: 27725
		string CurrentFolderDisplayName { get; }

		// Token: 0x17001D69 RID: 7529
		// (get) Token: 0x06006C4E RID: 27726
		// (set) Token: 0x06006C4F RID: 27727
		Rule CurrentRule { get; set; }

		// Token: 0x17001D6A RID: 7530
		// (get) Token: 0x06006C50 RID: 27728
		string DefaultDomainName { get; }

		// Token: 0x17001D6B RID: 7531
		// (get) Token: 0x06006C51 RID: 27729
		// (set) Token: 0x06006C52 RID: 27730
		MessageItem DeliveredMessage { get; set; }

		// Token: 0x17001D6C RID: 7532
		// (get) Token: 0x06006C53 RID: 27731
		// (set) Token: 0x06006C54 RID: 27732
		Folder DeliveryFolder { get; set; }

		// Token: 0x17001D6D RID: 7533
		// (get) Token: 0x06006C55 RID: 27733
		// (set) Token: 0x06006C56 RID: 27734
		ExecutionStage ExecutionStage { get; set; }

		// Token: 0x17001D6E RID: 7534
		// (get) Token: 0x06006C57 RID: 27735
		List<KeyValuePair<string, string>> ExtraTrackingEventData { get; }

		// Token: 0x17001D6F RID: 7535
		// (get) Token: 0x06006C58 RID: 27736
		// (set) Token: 0x06006C59 RID: 27737
		StoreId FinalDeliveryFolderId { get; set; }

		// Token: 0x17001D70 RID: 7536
		// (get) Token: 0x06006C5A RID: 27738
		bool IsOof { get; }

		// Token: 0x17001D71 RID: 7537
		// (get) Token: 0x06006C5B RID: 27739
		bool IsInternetMdnDisabled { get; }

		// Token: 0x17001D72 RID: 7538
		// (get) Token: 0x06006C5C RID: 27740
		IsMemberOfResolver<string> IsMemberOfResolver { get; }

		// Token: 0x17001D73 RID: 7539
		// (get) Token: 0x06006C5D RID: 27741
		string LocalServerFqdn { get; }

		// Token: 0x17001D74 RID: 7540
		// (get) Token: 0x06006C5E RID: 27742
		IPAddress LocalServerNetworkAddress { get; }

		// Token: 0x17001D75 RID: 7541
		// (get) Token: 0x06006C5F RID: 27743
		MessageItem Message { get; }

		// Token: 0x17001D76 RID: 7542
		// (get) Token: 0x06006C60 RID: 27744
		long MimeSize { get; }

		// Token: 0x17001D77 RID: 7543
		// (get) Token: 0x06006C61 RID: 27745
		// (set) Token: 0x06006C62 RID: 27746
		int NestedLevel { get; set; }

		// Token: 0x17001D78 RID: 7544
		// (get) Token: 0x06006C63 RID: 27747
		// (set) Token: 0x06006C64 RID: 27748
		Dictionary<PropertyDefinition, object> PropertiesForAllMessageCopies { get; set; }

		// Token: 0x17001D79 RID: 7545
		// (get) Token: 0x06006C65 RID: 27749
		// (set) Token: 0x06006C66 RID: 27750
		Dictionary<PropertyDefinition, object> PropertiesForDelegateForward { get; set; }

		// Token: 0x17001D7A RID: 7546
		// (get) Token: 0x06006C67 RID: 27751
		// (set) Token: 0x06006C68 RID: 27752
		Dictionary<PropertyDefinition, object> SharedPropertiesBetweenAgents { get; set; }

		// Token: 0x17001D7B RID: 7547
		// (get) Token: 0x06006C69 RID: 27753
		ProxyAddress Recipient { get; }

		// Token: 0x17001D7C RID: 7548
		// (get) Token: 0x06006C6A RID: 27754
		IADRecipientCache RecipientCache { get; }

		// Token: 0x17001D7D RID: 7549
		// (get) Token: 0x06006C6B RID: 27755
		ExEventLog.EventTuple OofHistoryCorruption { get; }

		// Token: 0x17001D7E RID: 7550
		// (get) Token: 0x06006C6C RID: 27756
		ExEventLog.EventTuple OofHistoryFolderMissing { get; }

		// Token: 0x17001D7F RID: 7551
		// (get) Token: 0x06006C6D RID: 27757
		bool ProcessingTestMessage { get; }

		// Token: 0x17001D80 RID: 7552
		// (get) Token: 0x06006C6E RID: 27758
		bool ShouldExecuteDisabledAndInErrorRules { get; }

		// Token: 0x17001D81 RID: 7553
		// (get) Token: 0x06006C6F RID: 27759
		ProxyAddress Sender { get; }

		// Token: 0x17001D82 RID: 7554
		// (get) Token: 0x06006C70 RID: 27760
		// (set) Token: 0x06006C71 RID: 27761
		string SenderAddress { get; set; }

		// Token: 0x17001D83 RID: 7555
		// (get) Token: 0x06006C72 RID: 27762
		// (set) Token: 0x06006C73 RID: 27763
		bool ShouldSkipMoveRule { get; set; }

		// Token: 0x17001D84 RID: 7556
		// (get) Token: 0x06006C74 RID: 27764
		StoreSession StoreSession { get; }

		// Token: 0x17001D85 RID: 7557
		object this[PropTag tag]
		{
			get;
		}

		// Token: 0x17001D86 RID: 7558
		// (get) Token: 0x06006C76 RID: 27766
		string XLoopValue { get; }

		// Token: 0x17001D87 RID: 7559
		// (get) Token: 0x06006C77 RID: 27767
		LimitChecker LimitChecker { get; }

		// Token: 0x17001D88 RID: 7560
		// (get) Token: 0x06006C78 RID: 27768
		IRuleConfig RuleConfig { get; }

		// Token: 0x06006C79 RID: 27769
		bool CompareSingleValue(PropTag tag, Restriction.RelOp op, object x, object y);

		// Token: 0x06006C7A RID: 27770
		void CopyProperty(MessageItem src, MessageItem target, PropertyDefinition property);

		// Token: 0x06006C7B RID: 27771
		MessageItem CreateMessageItem(PropertyDefinition[] prefetchProperties);

		// Token: 0x06006C7C RID: 27772
		void DisableAndMarkRuleInError(Rule rule, RuleAction.Type actionType, int actionIndex, DeferredError.RuleError errorCode);

		// Token: 0x06006C7D RID: 27773
		void MarkRuleInError(Rule rule, RuleAction.Type actionType, int actionIndex, DeferredError.RuleError errorCode);

		// Token: 0x06006C7E RID: 27774
		void RecordError(Exception exception, string stage);

		// Token: 0x06006C7F RID: 27775
		void RecordError(string message);

		// Token: 0x06006C80 RID: 27776
		ISubmissionItem GenerateSubmissionItem(MessageItem item, WorkItem workItem);

		// Token: 0x06006C81 RID: 27777
		IRuleEvaluationContext GetAttachmentContext(Attachment attachment);

		// Token: 0x06006C82 RID: 27778
		Folder GetDeletedItemsFolder();

		// Token: 0x06006C83 RID: 27779
		StorePropertyDefinition GetPropertyDefinitionForTag(PropTag tag);

		// Token: 0x06006C84 RID: 27780
		IRuleEvaluationContext GetRecipientContext(Recipient recipient);

		// Token: 0x06006C85 RID: 27781
		void SetMailboxOwnerAsSender(MessageItem message);

		// Token: 0x06006C86 RID: 27782
		bool HasRuleFiredBefore(Rule rule);

		// Token: 0x06006C87 RID: 27783
		void AddCurrentFolderIdTo(HashSet<StoreId> hashSet);

		// Token: 0x06006C88 RID: 27784
		List<Rule> LoadRules();

		// Token: 0x06006C89 RID: 27785
		void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs);

		// Token: 0x06006C8A RID: 27786
		void LogWorkItemExecution(WorkItem workItem);

		// Token: 0x06006C8B RID: 27787
		Folder OpenFolder(StoreId folderId);

		// Token: 0x06006C8C RID: 27788
		bool DetectLoop();

		// Token: 0x06006C8D RID: 27789
		void TraceDebug(string message);

		// Token: 0x06006C8E RID: 27790
		void TraceDebug<T>(string format, T argument);

		// Token: 0x06006C8F RID: 27791
		void TraceDebug<T1, T2>(string format, T1 argument1, T2 argument2);

		// Token: 0x06006C90 RID: 27792
		void TraceError(string message);

		// Token: 0x06006C91 RID: 27793
		void TraceError<T>(string format, T argument);

		// Token: 0x06006C92 RID: 27794
		void TraceError<T1, T2>(string format, T1 argument1, T2 argument2);

		// Token: 0x06006C93 RID: 27795
		void TraceError<T1, T2, T3>(string format, T1 argument1, T2 argument2, T3 argument3);

		// Token: 0x06006C94 RID: 27796
		void TraceFunction(string message);

		// Token: 0x06006C95 RID: 27797
		void TraceFunction<T>(string format, T argument);

		// Token: 0x06006C96 RID: 27798
		void TraceFunction<T1, T2>(string format, T1 argument1, T2 argument2);

		// Token: 0x06006C97 RID: 27799
		void TraceFunction<T1, T2, T3>(string format, T1 argument1, T2 argument2, T3 argument3);

		// Token: 0x06006C98 RID: 27800
		void TraceFunction<T1, T2, T3, T4>(string format, T1 argument1, T2 argument2, T3 argument3, T4 argument4);

		// Token: 0x06006C99 RID: 27801
		bool TryOpenLocalStore(byte[] folderEntryId, out Folder folder);

		// Token: 0x06006C9A RID: 27802
		ExTimeZone DetermineRecipientTimeZone();
	}
}
