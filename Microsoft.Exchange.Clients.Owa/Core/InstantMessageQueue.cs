using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.InstantMessaging;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000146 RID: 326
	internal sealed class InstantMessageQueue
	{
		// Token: 0x06000B15 RID: 2837 RVA: 0x0004E7FC File Offset: 0x0004C9FC
		internal InstantMessageQueue(UserContext userContext, IConversation conversation, InstantMessagePayload payload)
		{
			this.userContext = userContext;
			this.conversation = conversation;
			this.payload = payload;
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0004E824 File Offset: 0x0004CA24
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x0004E82C File Offset: 0x0004CA2C
		public IConversation Conversation
		{
			get
			{
				return this.conversation;
			}
			set
			{
				this.conversation = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0004E835 File Offset: 0x0004CA35
		public List<InstantMessageChat> MessageList
		{
			get
			{
				return this.messageList;
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0004E840 File Offset: 0x0004CA40
		public void AddMessage(string contentType, string message)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageQueue.AddMessage");
			Interlocked.CompareExchange<List<InstantMessageChat>>(ref this.messageList, new List<InstantMessageChat>(), null);
			bool flag = false;
			lock (this.lockObject)
			{
				if (this.messageList.Count < 20)
				{
					this.messageList.Add(new InstantMessageChat("text/plain;charset=utf-8", message));
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError<int>((long)this.GetHashCode(), "InstantMessageQueue.AddMessage. Message queued count: {0}", this.messageList.Count);
				InstantMessagePayloadUtilities.GenerateMessageNotDeliveredPayload(this.payload, this.conversation.Cid.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0004E914 File Offset: 0x0004CB14
		public void Clear()
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageQueue.Clear");
			lock (this.lockObject)
			{
				if (this.messageList != null && this.messageList.Count > 0)
				{
					ExTraceGlobals.InstantMessagingTracer.TraceDebug<int>((long)this.GetHashCode(), "InstantMessageQueue.Clear. Message list count: {0}", this.messageList.Count);
					this.messageList.Clear();
					this.messageList = null;
				}
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0004E9B0 File Offset: 0x0004CBB0
		public void SendAndClearMessageList()
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageQueue.SendAndClearMessageList");
			if (this.conversation == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError((long)this.GetHashCode(), "InstantMessageQueue.SendAndClearMessageList. Conversation is null.");
				return;
			}
			if (this.messageList != null && this.messageList.Count > 0)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug<int>((long)this.GetHashCode(), "InstantMessageQueue.SendAndClearMessageList. Message list count: {0}", this.messageList.Count);
				IIMModality iimmodality = this.conversation.GetModality(1) as IIMModality;
				if (iimmodality == null)
				{
					ExTraceGlobals.InstantMessagingTracer.TraceError((long)this.GetHashCode(), "InstantMessageQueue.SendAndClearMessageList. IIMModality is null.");
					return;
				}
				lock (this.lockObject)
				{
					foreach (InstantMessageChat instantMessageChat in this.messageList)
					{
						iimmodality.BeginSendMessage(instantMessageChat.ContentType, instantMessageChat.Message, new AsyncCallback(this.SendMessageCallback), iimmodality);
					}
					this.messageList.Clear();
					this.messageList = null;
				}
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0004EAF8 File Offset: 0x0004CCF8
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
				InstantMessagePayloadUtilities.GenerateMessageNotDeliveredPayload(this.payload, "InstantMessageQueue.SendMessageCallback", (iimmodality == null || iimmodality.Conversation == null) ? 0 : iimmodality.Conversation.Cid, ex);
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

		// Token: 0x040007EB RID: 2027
		private const int MaxMessageCount = 20;

		// Token: 0x040007EC RID: 2028
		private UserContext userContext;

		// Token: 0x040007ED RID: 2029
		private IConversation conversation;

		// Token: 0x040007EE RID: 2030
		private InstantMessagePayload payload;

		// Token: 0x040007EF RID: 2031
		private List<InstantMessageChat> messageList;

		// Token: 0x040007F0 RID: 2032
		private object lockObject = new object();
	}
}
