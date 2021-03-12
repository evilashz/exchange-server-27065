using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000029 RID: 41
	internal sealed class InterceptorAgentPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x0600016F RID: 367 RVA: 0x0000787C File Offset: 0x00005A7C
		internal InterceptorAgentPerfCountersInstance(string instanceName, InterceptorAgentPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Interceptor Agent")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.MessagesPermanentlyRejected = new ExPerformanceCounter(base.CategoryName, "Messages Permanently Rejected", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesPermanentlyRejected);
				this.MessagesPermanentlyRejectedRate = new ExPerformanceCounter(base.CategoryName, "Messages Permanently Rejected Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesPermanentlyRejectedRate);
				this.MessagesTransientlyRejected = new ExPerformanceCounter(base.CategoryName, "Messages Transiently Rejected", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesTransientlyRejected);
				this.MessagesTransientlyRejectedRate = new ExPerformanceCounter(base.CategoryName, "Messages Transiently Rejected Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesTransientlyRejectedRate);
				this.MessagesDropped = new ExPerformanceCounter(base.CategoryName, "Messages Dropped", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDropped);
				this.MessagesDroppedRate = new ExPerformanceCounter(base.CategoryName, "Messages Dropped Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDroppedRate);
				this.MessagesDeferred = new ExPerformanceCounter(base.CategoryName, "Messages Deferred", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDeferred);
				this.MessagesDeferredRate = new ExPerformanceCounter(base.CategoryName, "Messages Deferred Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDeferredRate);
				this.MessagesDelayed = new ExPerformanceCounter(base.CategoryName, "Messages Delayed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDelayed);
				this.MessagesDelayedRate = new ExPerformanceCounter(base.CategoryName, "Messages Delayed Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDelayedRate);
				this.MessagesArchived = new ExPerformanceCounter(base.CategoryName, "Messages Archived", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesArchived);
				this.MessagesArchivedRate = new ExPerformanceCounter(base.CategoryName, "Messages Archived Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesArchivedRate);
				this.MessageHeadersArchived = new ExPerformanceCounter(base.CategoryName, "Message Headers Archived", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessageHeadersArchived);
				this.MessageHeadersArchivedRate = new ExPerformanceCounter(base.CategoryName, "Message Headers Archived Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessageHeadersArchivedRate);
				this.MatchedOnMailFromMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnMailFrom Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnMailFromMessages);
				this.MatchedOnMailFromMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnMailFrom Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnMailFromMessagesRate);
				this.EvaluatedOnMailFromMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnMailFrom Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnMailFromMessages);
				this.EvaluatedOnMailFromMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnMailFrom Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnMailFromMessagesRate);
				this.MatchedOnRcptToMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnRcptTo Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnRcptToMessages);
				this.MatchedOnRcptToMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnRcptTo Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnRcptToMessagesRate);
				this.EvaluatedOnRcptToMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnRcptTo Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnRcptToMessages);
				this.EvaluatedOnRcptToMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnRcptTo Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnRcptToMessagesRate);
				this.MatchedOnEndOfHeadersMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnEndOfHeaders Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnEndOfHeadersMessages);
				this.MatchedOnEndOfHeadersMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnEndOfHeaders Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnEndOfHeadersMessagesRate);
				this.EvaluatedOnEndOfHeadersMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnEndOfHeaders Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnEndOfHeadersMessages);
				this.EvaluatedOnEndOfHeadersMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnEndOfHeaders Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnEndOfHeadersMessagesRate);
				this.MatchedOnEndOfDataMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnEndOfData Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnEndOfDataMessages);
				this.MatchedOnEndOfDataMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnEndOfData Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnEndOfDataMessagesRate);
				this.EvaluatedOnEndOfDataMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnEndOfData Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnEndOfDataMessages);
				this.EvaluatedOnEndOfDataMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnEndOfData Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnEndOfDataMessagesRate);
				this.MatchedOnSubmittedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnSubmittedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnSubmittedMessageMessages);
				this.MatchedOnSubmittedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnSubmittedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnSubmittedMessageMessagesRate);
				this.EvaluatedOnSubmittedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnSubmittedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnSubmittedMessageMessages);
				this.EvaluatedOnSubmittedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnSubmittedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnSubmittedMessageMessagesRate);
				this.MatchedOnResolvedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnResolvedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnResolvedMessageMessages);
				this.MatchedOnResolvedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnResolvedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnResolvedMessageMessagesRate);
				this.EvaluatedOnResolvedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnResolvedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnResolvedMessageMessages);
				this.EvaluatedOnResolvedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnResolvedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnResolvedMessageMessagesRate);
				this.MatchedOnRoutedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnRoutedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnRoutedMessageMessages);
				this.MatchedOnRoutedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnRoutedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnRoutedMessageMessagesRate);
				this.EvaluatedOnRoutedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnRoutedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnRoutedMessageMessages);
				this.EvaluatedOnRoutedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnRoutedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnRoutedMessageMessagesRate);
				this.MatchedOnCategorizedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnCategorizedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnCategorizedMessageMessages);
				this.MatchedOnCategorizedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnCategorizedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnCategorizedMessageMessagesRate);
				this.EvaluatedOnCategorizedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnCategorizedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnCategorizedMessageMessages);
				this.EvaluatedOnCategorizedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnCategorizedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnCategorizedMessageMessagesRate);
				this.MatchedOnInitMsgMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnInitMsg Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnInitMsgMessages);
				this.MatchedOnInitMsgMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnInitMsg Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnInitMsgMessagesRate);
				this.EvaluatedOnInitMsgMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnInitMsg Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnInitMsgMessages);
				this.EvaluatedOnInitMsgMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnInitMsg Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnInitMsgMessagesRate);
				this.MatchedOnPromotedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnPromotedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnPromotedMessageMessages);
				this.MatchedOnPromotedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnPromotedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnPromotedMessageMessagesRate);
				this.EvaluatedOnPromotedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnPromotedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnPromotedMessageMessages);
				this.EvaluatedOnPromotedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnPromotedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnPromotedMessageMessagesRate);
				this.MatchedOnCreatedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnCreatedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnCreatedMessageMessages);
				this.MatchedOnCreatedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnCreatedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnCreatedMessageMessagesRate);
				this.EvaluatedOnCreatedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnCreatedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnCreatedMessageMessages);
				this.EvaluatedOnCreatedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnCreatedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnCreatedMessageMessagesRate);
				this.MatchedOnDemotedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnDemotedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnDemotedMessageMessages);
				this.MatchedOnDemotedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnDemotedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnDemotedMessageMessagesRate);
				this.EvaluatedOnDemotedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnDemotedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnDemotedMessageMessages);
				this.EvaluatedOnDemotedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnDemotedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnDemotedMessageMessagesRate);
				this.MatchedOnLoadedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnLoadedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnLoadedMessageMessages);
				this.MatchedOnLoadedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnLoadedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnLoadedMessageMessagesRate);
				this.EvaluatedOnLoadedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnLoadedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnLoadedMessageMessages);
				this.EvaluatedOnLoadedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnLoadedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnLoadedMessageMessagesRate);
				long num = this.MessagesPermanentlyRejected.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008440 File Offset: 0x00006640
		internal InterceptorAgentPerfCountersInstance(string instanceName) : base(instanceName, "MSExchange Interceptor Agent")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.MessagesPermanentlyRejected = new ExPerformanceCounter(base.CategoryName, "Messages Permanently Rejected", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesPermanentlyRejected);
				this.MessagesPermanentlyRejectedRate = new ExPerformanceCounter(base.CategoryName, "Messages Permanently Rejected Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesPermanentlyRejectedRate);
				this.MessagesTransientlyRejected = new ExPerformanceCounter(base.CategoryName, "Messages Transiently Rejected", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesTransientlyRejected);
				this.MessagesTransientlyRejectedRate = new ExPerformanceCounter(base.CategoryName, "Messages Transiently Rejected Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesTransientlyRejectedRate);
				this.MessagesDropped = new ExPerformanceCounter(base.CategoryName, "Messages Dropped", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDropped);
				this.MessagesDroppedRate = new ExPerformanceCounter(base.CategoryName, "Messages Dropped Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDroppedRate);
				this.MessagesDeferred = new ExPerformanceCounter(base.CategoryName, "Messages Deferred", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDeferred);
				this.MessagesDeferredRate = new ExPerformanceCounter(base.CategoryName, "Messages Deferred Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDeferredRate);
				this.MessagesDelayed = new ExPerformanceCounter(base.CategoryName, "Messages Delayed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDelayed);
				this.MessagesDelayedRate = new ExPerformanceCounter(base.CategoryName, "Messages Delayed Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesDelayedRate);
				this.MessagesArchived = new ExPerformanceCounter(base.CategoryName, "Messages Archived", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesArchived);
				this.MessagesArchivedRate = new ExPerformanceCounter(base.CategoryName, "Messages Archived Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesArchivedRate);
				this.MessageHeadersArchived = new ExPerformanceCounter(base.CategoryName, "Message Headers Archived", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessageHeadersArchived);
				this.MessageHeadersArchivedRate = new ExPerformanceCounter(base.CategoryName, "Message Headers Archived Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessageHeadersArchivedRate);
				this.MatchedOnMailFromMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnMailFrom Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnMailFromMessages);
				this.MatchedOnMailFromMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnMailFrom Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnMailFromMessagesRate);
				this.EvaluatedOnMailFromMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnMailFrom Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnMailFromMessages);
				this.EvaluatedOnMailFromMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnMailFrom Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnMailFromMessagesRate);
				this.MatchedOnRcptToMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnRcptTo Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnRcptToMessages);
				this.MatchedOnRcptToMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnRcptTo Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnRcptToMessagesRate);
				this.EvaluatedOnRcptToMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnRcptTo Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnRcptToMessages);
				this.EvaluatedOnRcptToMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnRcptTo Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnRcptToMessagesRate);
				this.MatchedOnEndOfHeadersMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnEndOfHeaders Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnEndOfHeadersMessages);
				this.MatchedOnEndOfHeadersMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnEndOfHeaders Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnEndOfHeadersMessagesRate);
				this.EvaluatedOnEndOfHeadersMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnEndOfHeaders Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnEndOfHeadersMessages);
				this.EvaluatedOnEndOfHeadersMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnEndOfHeaders Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnEndOfHeadersMessagesRate);
				this.MatchedOnEndOfDataMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnEndOfData Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnEndOfDataMessages);
				this.MatchedOnEndOfDataMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnEndOfData Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnEndOfDataMessagesRate);
				this.EvaluatedOnEndOfDataMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnEndOfData Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnEndOfDataMessages);
				this.EvaluatedOnEndOfDataMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnEndOfData Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnEndOfDataMessagesRate);
				this.MatchedOnSubmittedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnSubmittedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnSubmittedMessageMessages);
				this.MatchedOnSubmittedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnSubmittedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnSubmittedMessageMessagesRate);
				this.EvaluatedOnSubmittedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnSubmittedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnSubmittedMessageMessages);
				this.EvaluatedOnSubmittedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnSubmittedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnSubmittedMessageMessagesRate);
				this.MatchedOnResolvedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnResolvedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnResolvedMessageMessages);
				this.MatchedOnResolvedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnResolvedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnResolvedMessageMessagesRate);
				this.EvaluatedOnResolvedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnResolvedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnResolvedMessageMessages);
				this.EvaluatedOnResolvedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnResolvedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnResolvedMessageMessagesRate);
				this.MatchedOnRoutedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnRoutedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnRoutedMessageMessages);
				this.MatchedOnRoutedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnRoutedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnRoutedMessageMessagesRate);
				this.EvaluatedOnRoutedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnRoutedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnRoutedMessageMessages);
				this.EvaluatedOnRoutedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnRoutedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnRoutedMessageMessagesRate);
				this.MatchedOnCategorizedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnCategorizedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnCategorizedMessageMessages);
				this.MatchedOnCategorizedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnCategorizedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnCategorizedMessageMessagesRate);
				this.EvaluatedOnCategorizedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnCategorizedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnCategorizedMessageMessages);
				this.EvaluatedOnCategorizedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnCategorizedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnCategorizedMessageMessagesRate);
				this.MatchedOnInitMsgMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnInitMsg Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnInitMsgMessages);
				this.MatchedOnInitMsgMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnInitMsg Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnInitMsgMessagesRate);
				this.EvaluatedOnInitMsgMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnInitMsg Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnInitMsgMessages);
				this.EvaluatedOnInitMsgMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnInitMsg Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnInitMsgMessagesRate);
				this.MatchedOnPromotedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnPromotedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnPromotedMessageMessages);
				this.MatchedOnPromotedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnPromotedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnPromotedMessageMessagesRate);
				this.EvaluatedOnPromotedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnPromotedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnPromotedMessageMessages);
				this.EvaluatedOnPromotedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnPromotedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnPromotedMessageMessagesRate);
				this.MatchedOnCreatedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnCreatedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnCreatedMessageMessages);
				this.MatchedOnCreatedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnCreatedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnCreatedMessageMessagesRate);
				this.EvaluatedOnCreatedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnCreatedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnCreatedMessageMessages);
				this.EvaluatedOnCreatedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnCreatedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnCreatedMessageMessagesRate);
				this.MatchedOnDemotedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnDemotedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnDemotedMessageMessages);
				this.MatchedOnDemotedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnDemotedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnDemotedMessageMessagesRate);
				this.EvaluatedOnDemotedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnDemotedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnDemotedMessageMessages);
				this.EvaluatedOnDemotedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnDemotedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnDemotedMessageMessagesRate);
				this.MatchedOnLoadedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Matched OnLoadedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnLoadedMessageMessages);
				this.MatchedOnLoadedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Matched OnLoadedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MatchedOnLoadedMessageMessagesRate);
				this.EvaluatedOnLoadedMessageMessages = new ExPerformanceCounter(base.CategoryName, "Evaluated OnLoadedMessage Messages", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnLoadedMessageMessages);
				this.EvaluatedOnLoadedMessageMessagesRate = new ExPerformanceCounter(base.CategoryName, "Evaluated OnLoadedMessage Messages Rate", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.EvaluatedOnLoadedMessageMessagesRate);
				long num = this.MessagesPermanentlyRejected.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00009004 File Offset: 0x00007204
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x040000D0 RID: 208
		public readonly ExPerformanceCounter MessagesPermanentlyRejected;

		// Token: 0x040000D1 RID: 209
		public readonly ExPerformanceCounter MessagesPermanentlyRejectedRate;

		// Token: 0x040000D2 RID: 210
		public readonly ExPerformanceCounter MessagesTransientlyRejected;

		// Token: 0x040000D3 RID: 211
		public readonly ExPerformanceCounter MessagesTransientlyRejectedRate;

		// Token: 0x040000D4 RID: 212
		public readonly ExPerformanceCounter MessagesDropped;

		// Token: 0x040000D5 RID: 213
		public readonly ExPerformanceCounter MessagesDroppedRate;

		// Token: 0x040000D6 RID: 214
		public readonly ExPerformanceCounter MessagesDeferred;

		// Token: 0x040000D7 RID: 215
		public readonly ExPerformanceCounter MessagesDeferredRate;

		// Token: 0x040000D8 RID: 216
		public readonly ExPerformanceCounter MessagesDelayed;

		// Token: 0x040000D9 RID: 217
		public readonly ExPerformanceCounter MessagesDelayedRate;

		// Token: 0x040000DA RID: 218
		public readonly ExPerformanceCounter MessagesArchived;

		// Token: 0x040000DB RID: 219
		public readonly ExPerformanceCounter MessagesArchivedRate;

		// Token: 0x040000DC RID: 220
		public readonly ExPerformanceCounter MessageHeadersArchived;

		// Token: 0x040000DD RID: 221
		public readonly ExPerformanceCounter MessageHeadersArchivedRate;

		// Token: 0x040000DE RID: 222
		public readonly ExPerformanceCounter MatchedOnMailFromMessages;

		// Token: 0x040000DF RID: 223
		public readonly ExPerformanceCounter MatchedOnMailFromMessagesRate;

		// Token: 0x040000E0 RID: 224
		public readonly ExPerformanceCounter EvaluatedOnMailFromMessages;

		// Token: 0x040000E1 RID: 225
		public readonly ExPerformanceCounter EvaluatedOnMailFromMessagesRate;

		// Token: 0x040000E2 RID: 226
		public readonly ExPerformanceCounter MatchedOnRcptToMessages;

		// Token: 0x040000E3 RID: 227
		public readonly ExPerformanceCounter MatchedOnRcptToMessagesRate;

		// Token: 0x040000E4 RID: 228
		public readonly ExPerformanceCounter EvaluatedOnRcptToMessages;

		// Token: 0x040000E5 RID: 229
		public readonly ExPerformanceCounter EvaluatedOnRcptToMessagesRate;

		// Token: 0x040000E6 RID: 230
		public readonly ExPerformanceCounter MatchedOnEndOfHeadersMessages;

		// Token: 0x040000E7 RID: 231
		public readonly ExPerformanceCounter MatchedOnEndOfHeadersMessagesRate;

		// Token: 0x040000E8 RID: 232
		public readonly ExPerformanceCounter EvaluatedOnEndOfHeadersMessages;

		// Token: 0x040000E9 RID: 233
		public readonly ExPerformanceCounter EvaluatedOnEndOfHeadersMessagesRate;

		// Token: 0x040000EA RID: 234
		public readonly ExPerformanceCounter MatchedOnEndOfDataMessages;

		// Token: 0x040000EB RID: 235
		public readonly ExPerformanceCounter MatchedOnEndOfDataMessagesRate;

		// Token: 0x040000EC RID: 236
		public readonly ExPerformanceCounter EvaluatedOnEndOfDataMessages;

		// Token: 0x040000ED RID: 237
		public readonly ExPerformanceCounter EvaluatedOnEndOfDataMessagesRate;

		// Token: 0x040000EE RID: 238
		public readonly ExPerformanceCounter MatchedOnSubmittedMessageMessages;

		// Token: 0x040000EF RID: 239
		public readonly ExPerformanceCounter MatchedOnSubmittedMessageMessagesRate;

		// Token: 0x040000F0 RID: 240
		public readonly ExPerformanceCounter EvaluatedOnSubmittedMessageMessages;

		// Token: 0x040000F1 RID: 241
		public readonly ExPerformanceCounter EvaluatedOnSubmittedMessageMessagesRate;

		// Token: 0x040000F2 RID: 242
		public readonly ExPerformanceCounter MatchedOnResolvedMessageMessages;

		// Token: 0x040000F3 RID: 243
		public readonly ExPerformanceCounter MatchedOnResolvedMessageMessagesRate;

		// Token: 0x040000F4 RID: 244
		public readonly ExPerformanceCounter EvaluatedOnResolvedMessageMessages;

		// Token: 0x040000F5 RID: 245
		public readonly ExPerformanceCounter EvaluatedOnResolvedMessageMessagesRate;

		// Token: 0x040000F6 RID: 246
		public readonly ExPerformanceCounter MatchedOnRoutedMessageMessages;

		// Token: 0x040000F7 RID: 247
		public readonly ExPerformanceCounter MatchedOnRoutedMessageMessagesRate;

		// Token: 0x040000F8 RID: 248
		public readonly ExPerformanceCounter EvaluatedOnRoutedMessageMessages;

		// Token: 0x040000F9 RID: 249
		public readonly ExPerformanceCounter EvaluatedOnRoutedMessageMessagesRate;

		// Token: 0x040000FA RID: 250
		public readonly ExPerformanceCounter MatchedOnCategorizedMessageMessages;

		// Token: 0x040000FB RID: 251
		public readonly ExPerformanceCounter MatchedOnCategorizedMessageMessagesRate;

		// Token: 0x040000FC RID: 252
		public readonly ExPerformanceCounter EvaluatedOnCategorizedMessageMessages;

		// Token: 0x040000FD RID: 253
		public readonly ExPerformanceCounter EvaluatedOnCategorizedMessageMessagesRate;

		// Token: 0x040000FE RID: 254
		public readonly ExPerformanceCounter MatchedOnInitMsgMessages;

		// Token: 0x040000FF RID: 255
		public readonly ExPerformanceCounter MatchedOnInitMsgMessagesRate;

		// Token: 0x04000100 RID: 256
		public readonly ExPerformanceCounter EvaluatedOnInitMsgMessages;

		// Token: 0x04000101 RID: 257
		public readonly ExPerformanceCounter EvaluatedOnInitMsgMessagesRate;

		// Token: 0x04000102 RID: 258
		public readonly ExPerformanceCounter MatchedOnPromotedMessageMessages;

		// Token: 0x04000103 RID: 259
		public readonly ExPerformanceCounter MatchedOnPromotedMessageMessagesRate;

		// Token: 0x04000104 RID: 260
		public readonly ExPerformanceCounter EvaluatedOnPromotedMessageMessages;

		// Token: 0x04000105 RID: 261
		public readonly ExPerformanceCounter EvaluatedOnPromotedMessageMessagesRate;

		// Token: 0x04000106 RID: 262
		public readonly ExPerformanceCounter MatchedOnCreatedMessageMessages;

		// Token: 0x04000107 RID: 263
		public readonly ExPerformanceCounter MatchedOnCreatedMessageMessagesRate;

		// Token: 0x04000108 RID: 264
		public readonly ExPerformanceCounter EvaluatedOnCreatedMessageMessages;

		// Token: 0x04000109 RID: 265
		public readonly ExPerformanceCounter EvaluatedOnCreatedMessageMessagesRate;

		// Token: 0x0400010A RID: 266
		public readonly ExPerformanceCounter MatchedOnDemotedMessageMessages;

		// Token: 0x0400010B RID: 267
		public readonly ExPerformanceCounter MatchedOnDemotedMessageMessagesRate;

		// Token: 0x0400010C RID: 268
		public readonly ExPerformanceCounter EvaluatedOnDemotedMessageMessages;

		// Token: 0x0400010D RID: 269
		public readonly ExPerformanceCounter EvaluatedOnDemotedMessageMessagesRate;

		// Token: 0x0400010E RID: 270
		public readonly ExPerformanceCounter MatchedOnLoadedMessageMessages;

		// Token: 0x0400010F RID: 271
		public readonly ExPerformanceCounter MatchedOnLoadedMessageMessagesRate;

		// Token: 0x04000110 RID: 272
		public readonly ExPerformanceCounter EvaluatedOnLoadedMessageMessages;

		// Token: 0x04000111 RID: 273
		public readonly ExPerformanceCounter EvaluatedOnLoadedMessageMessagesRate;
	}
}
