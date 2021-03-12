using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Clients.Owa.Server.LyncIMLogging;
using Microsoft.Exchange.InstantMessaging;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000147 RID: 327
	internal sealed class InstantMessageQueue
	{
		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002D192 File Offset: 0x0002B392
		internal InstantMessageQueue(IUserContext userContext, IConversation conversation, InstantMessageNotifier notifier)
		{
			this.userContext = userContext;
			this.Conversation = conversation;
			this.notifier = notifier;
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0002D1BA File Offset: 0x0002B3BA
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x0002D1C2 File Offset: 0x0002B3C2
		public IConversation Conversation { get; set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0002D1CB File Offset: 0x0002B3CB
		public List<Tuple<string, string>> MessageList
		{
			get
			{
				return this.messageList;
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002D1D4 File Offset: 0x0002B3D4
		public void AddMessage(string contentType, string message)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageQueue.AddMessage");
			Interlocked.CompareExchange<List<Tuple<string, string>>>(ref this.messageList, new List<Tuple<string, string>>(), null);
			bool flag = false;
			lock (this.lockObject)
			{
				if (this.messageList.Count < 20)
				{
					if (string.IsNullOrWhiteSpace(contentType))
					{
						contentType = "text/plain;charset=utf-8";
					}
					this.messageList.Add(new Tuple<string, string>(contentType, message));
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError((long)this.GetHashCode(), "InstantMessageQueue.AddMessage. Message queued count: {0}", new object[]
				{
					this.messageList.Count
				});
				InstantMessagePayloadUtilities.GenerateMessageNotDeliveredPayload(this.notifier, "InstantMessageQueue.AddMessage", this.Conversation.Cid, UserActivityType.FailedDelivery);
			}
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002D2C0 File Offset: 0x0002B4C0
		public void Clear()
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageQueue.Clear");
			lock (this.lockObject)
			{
				if (this.messageList != null && this.messageList.Count > 0)
				{
					ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageQueue.Clear. Message list count: {0}", new object[]
					{
						this.messageList.Count
					});
					this.messageList.Clear();
					this.messageList = null;
				}
			}
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002D36C File Offset: 0x0002B56C
		public void SendAndClearMessageList()
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageQueue.SendAndClearMessageList");
			if (this.Conversation == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError((long)this.GetHashCode(), "InstantMessageQueue.SendAndClearMessageList. Conversation is null.");
				return;
			}
			if (this.messageList != null && this.messageList.Count > 0)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageQueue.SendAndClearMessageList. Message list count: {0}", new object[]
				{
					this.messageList.Count
				});
				IIMModality iimmodality = this.Conversation.GetModality(1) as IIMModality;
				if (iimmodality == null)
				{
					ExTraceGlobals.InstantMessagingTracer.TraceError((long)this.GetHashCode(), "InstantMessageQueue.SendAndClearMessageList. IIMModality is null.");
					return;
				}
				lock (this.lockObject)
				{
					foreach (Tuple<string, string> tuple in this.messageList)
					{
						iimmodality.BeginSendMessage(tuple.Item1, tuple.Item2, new AsyncCallback(this.SendMessageCallback), iimmodality);
					}
					this.messageList.Clear();
					this.messageList = null;
				}
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002D4C4 File Offset: 0x0002B6C4
		private void SendMessageCallback(IAsyncResult result)
		{
			IIMModality iimmodality = null;
			try
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageQueue.SendMessageCallback");
				iimmodality = (result.AsyncState as IIMModality);
				if (iimmodality == null)
				{
					ExTraceGlobals.InstantMessagingTracer.TraceError((long)this.GetHashCode(), "InstantMessageQueue.SendMessageCallback. Instant Messaging Modality is null.");
				}
				else
				{
					iimmodality.EndSendMessage(result);
				}
			}
			catch (InstantMessagingException ex)
			{
				InstantMessagePayloadUtilities.GenerateMessageNotDeliveredPayload(this.notifier, "InstantMessageQueue.SendMessageCallback", (iimmodality == null || iimmodality.Conversation == null) ? 0 : iimmodality.Conversation.Cid, ex);
				InstantMessagingError code = ex.Code;
				if (code <= 18102)
				{
					if (code == 0)
					{
						goto IL_DB;
					}
					if (code == 18102)
					{
						InstantMessagingErrorSubCode subCode = ex.SubCode;
						if (subCode != 9)
						{
							InstantMessageUtilities.SendWatsonReport("InstantMessageQueue.SendMessageCallback", this.userContext, ex);
							goto IL_DB;
						}
						goto IL_DB;
					}
				}
				else if (code == 18201 || code == 18204)
				{
					goto IL_DB;
				}
				InstantMessageUtilities.SendWatsonReport("InstantMessageQueue.SendMessageCallback", this.userContext, ex);
				IL_DB:;
			}
			catch (Exception exception)
			{
				InstantMessageUtilities.SendWatsonReport("InstantMessageQueue.SendMessageCallback", this.userContext, exception);
			}
		}

		// Token: 0x0400077D RID: 1917
		private const int MaxMessageCount = 20;

		// Token: 0x0400077E RID: 1918
		private IUserContext userContext;

		// Token: 0x0400077F RID: 1919
		private InstantMessageNotifier notifier;

		// Token: 0x04000780 RID: 1920
		private List<Tuple<string, string>> messageList;

		// Token: 0x04000781 RID: 1921
		private object lockObject = new object();
	}
}
