using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001E5 RID: 485
	[OwaEventNamespace("PendingRequest")]
	internal class PendingRequestEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06001113 RID: 4371 RVA: 0x00041741 File Offset: 0x0003F941
		public PendingRequestEventHandler()
		{
			base.DontWriteHeaders = true;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0004175C File Offset: 0x0003F95C
		[OwaEventParameter("A", typeof(long), false, true)]
		[OwaEventParameter("realm", typeof(string), false, true)]
		[OwaEventParameter("n", typeof(string), false, true)]
		[OwaEventParameter("ecnsq", typeof(string), false, true)]
		[OwaEventParameter("X-SuiteServiceProxyOrigin", typeof(string), false, true)]
		[OwaEventParameter("UA", typeof(bool))]
		[OwaEventParameter("X-OWA-CANARY", typeof(string), false, true)]
		[OwaEvent("PendingNotificationRequest")]
		[OwaEventParameter("brwnm", typeof(string), false, true)]
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEventParameter("cid", typeof(string))]
		public IAsyncResult BeginUseNotificationPipe(AsyncCallback callback, object extraData)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "[PendingRequestEventHandler.BeginUseNotificationPipe] called from the client");
			string text = (string)base.GetParameter("cid");
			OwaAsyncResult owaAsyncResult = new OwaAsyncResult(callback, extraData, text);
			PendingRequestEventData pendingRequestEventData = null;
			this.pgEventDataDictionary.TryGetValue(text, out pendingRequestEventData);
			UserContext userContext = UserContextManager.GetUserContext(HttpContext.Current);
			if (userContext == null || (pendingRequestEventData != null && !pendingRequestEventData.AsyncResult.IsCompleted))
			{
				owaAsyncResult.CompleteRequest(true);
				return owaAsyncResult;
			}
			HttpResponse response = HttpContext.Current.Response;
			response.AppendHeader("X-NoCompression", "1");
			response.AppendHeader("X-NoBuffering", "1");
			response.AddHeader("Content-Encoding", "none");
			try
			{
				if (!userContext.GetPendingGetManagerLock())
				{
					owaAsyncResult.CompleteRequest(true);
					return owaAsyncResult;
				}
				if (this.pendingRequestManager == null)
				{
					this.pendingRequestManager = userContext.PendingRequestManager;
				}
				if (this.pendingRequestManager == null)
				{
					owaAsyncResult.CompleteRequest(true);
					return owaAsyncResult;
				}
				PendingRequestChannel pendingRequestChannel = null;
				if (this.pendingRequestManager != null)
				{
					pendingRequestChannel = this.pendingRequestManager.GetPendingGetChannel(text);
				}
				if (pendingRequestChannel == null)
				{
					pendingRequestChannel = this.pendingRequestManager.AddPendingGetChannel(text);
				}
				object parameter = base.GetParameter("A");
				if (parameter != null)
				{
					pendingRequestChannel.MaxTicksPerPendingRequest = (long)parameter * 10000L;
				}
				pendingRequestEventData = new PendingRequestEventData(owaAsyncResult, new ChunkedHttpResponse(this.HttpContext));
				this.pgEventDataDictionary.Add(text, pendingRequestEventData);
				try
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool, string>((long)this.GetHashCode(), "[PendingRequestEventHandler.BeginUseNotificationPipe] calling pendingGetChannel.BeginSendNotification. userContext.HasActiveHierarchySubscription: {0}. ChannelId: {1}", userContext.HasActiveHierarchySubscription, text);
					pendingRequestChannel.BeginSendNotification(new AsyncCallback(this.UseNotificationPipeCallback), this.pgEventDataDictionary[text].Response, this, userContext.HasActiveHierarchySubscription, text);
				}
				catch (Exception ex)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceError<Exception>((long)this.GetHashCode(), "An exception happened while executing BeginUseNotificationPipe. Exception:{0}", ex);
					lock (this.pgEventDataDictionary[text].AsyncResult)
					{
						if (!this.pgEventDataDictionary[text].AsyncResult.IsCompleted)
						{
							this.pgEventDataDictionary[text].AsyncResult.CompleteRequest(true, ex);
						}
					}
				}
			}
			finally
			{
				if (userContext.PendingRequestManager != null && userContext.PendingRequestManager.ShouldDispose)
				{
					userContext.PendingRequestManager.Dispose();
				}
				userContext.ReleasePendingGetManagerLock();
			}
			return owaAsyncResult;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00041A00 File Offset: 0x0003FC00
		[OwaEvent("PendingNotificationRequest")]
		public void EndUseNotificationPipe(IAsyncResult async)
		{
			if (this.isDisposing)
			{
				return;
			}
			OwaAsyncResult owaAsyncResult = (OwaAsyncResult)async;
			try
			{
				string pendingGetId = owaAsyncResult.PendingGetId;
				PendingRequestEventData pendingRequestEventData;
				if (!this.pgEventDataDictionary.TryGetValue(pendingGetId, out pendingRequestEventData))
				{
					ExTraceGlobals.NotificationsCallTracer.TraceWarning<string>((long)this.GetHashCode(), "There is no PendingRequestEventData for PendingGetId {0}", pendingGetId);
				}
				else
				{
					if (owaAsyncResult.Exception != null)
					{
						Exception ex = (owaAsyncResult.Exception.InnerException == null) ? owaAsyncResult.Exception : owaAsyncResult.Exception.InnerException;
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "An exception was thrown during the processing of the async request: {0}", ex.Message);
						if (pendingRequestEventData.Response != null)
						{
							StringBuilder stringBuilder = new StringBuilder(500);
							stringBuilder.Append(ex.Message);
							stringBuilder.Append(":").Append(ex.StackTrace);
							pendingRequestEventData.Response.WriteError(PendingRequestUtilities.JavascriptEncode(stringBuilder.ToString()));
						}
					}
					if (pendingRequestEventData.Response != null)
					{
						pendingRequestEventData.Response.WriteLastChunk();
					}
				}
			}
			catch (OwaPermanentException)
			{
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00041B14 File Offset: 0x0003FD14
		[OwaEventParameter("UA", typeof(string), false, true)]
		[OwaEvent("FinishNotificationRequest")]
		[OwaEventParameter("realm", typeof(string), false, true)]
		[OwaEventParameter("cid", typeof(string), false, true)]
		[OwaEventParameter("Fn", typeof(bool), false, true)]
		[OwaEventVerb(OwaEventVerb.Post)]
		public void DisposePendingNotificationClientRequest()
		{
			IMailboxContext mailboxContext = UserContextManager.GetMailboxContext(HttpContext.Current, null, false);
			HttpResponse response = HttpContext.Current.Response;
			object parameter = base.GetParameter("Fn");
			bool flag = false;
			if (parameter != null)
			{
				flag = (bool)parameter;
			}
			bool flag2 = false;
			string text = (string)base.GetParameter("cid");
			text = this.ProcessChannelIdOnRequestAndGenerateIfNeeded(text, flag);
			if (mailboxContext != null && text != null)
			{
				PendingRequestChannel pendingGetChannel = mailboxContext.PendingRequestManager.GetPendingGetChannel(text);
				if (pendingGetChannel != null)
				{
					flag2 = pendingGetChannel.HandleFinishRequestFromClient();
					if (flag)
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[PendingRequestEventHandler::DisposePendingNotificationClientRequest] userContext.PendingRequestManager.RemovePendingGetChannel. ChannelId: {0}", text);
						mailboxContext.PendingRequestManager.RemovePendingGetChannel(text);
					}
				}
				else
				{
					mailboxContext.PendingRequestManager.AddPendingGetChannel(text);
				}
			}
			response.AppendHeader("X-OWA-EventResult", "0");
			response.Write("{");
			if (text != null)
			{
				response.Write("cid:\"");
				response.Write(text);
				response.Write("\",");
			}
			response.Write("syncFnshRq:");
			response.Write(flag2 ? "1}" : "0}");
			HttpUtilities.MakePageNoCacheNoStore(response);
			response.ContentType = HttpUtilities.GetContentTypeString(base.ResponseContentType);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00041C44 File Offset: 0x0003FE44
		internal static bool IsObsoleteRequest(RequestContext requestContext, IMailboxContext userContext)
		{
			if (requestContext.RequestType == OwaRequestType.Oeh && userContext == null)
			{
				string queryStringParameter = HttpUtilities.GetQueryStringParameter(requestContext.HttpContext.Request, "ns", false);
				string queryStringParameter2 = HttpUtilities.GetQueryStringParameter(requestContext.HttpContext.Request, "ev", false);
				string queryStringParameter3 = HttpUtilities.GetQueryStringParameter(requestContext.HttpContext.Request, "Fn", false);
				return queryStringParameter == "PendingRequest" && queryStringParameter2 == "FinishNotificationRequest" && queryStringParameter3 == "1";
			}
			return false;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00041CCC File Offset: 0x0003FECC
		protected override void InternalDispose(bool isExplicitDispose)
		{
			if (isExplicitDispose && !this.isDisposing && this.pgEventDataDictionary != null)
			{
				lock (this.pgEventDataDictionary)
				{
					this.isDisposing = true;
					foreach (string key in this.pgEventDataDictionary.Keys)
					{
						PendingRequestEventData pendingRequestEventData = this.pgEventDataDictionary[key];
						if (pendingRequestEventData != null)
						{
							if (pendingRequestEventData.AsyncResult != null)
							{
								lock (pendingRequestEventData.AsyncResult)
								{
									if (!pendingRequestEventData.AsyncResult.IsCompleted)
									{
										pendingRequestEventData.AsyncResult.CompleteRequest(false);
									}
								}
							}
							if (pendingRequestEventData.Response != null)
							{
								pendingRequestEventData.Response.Dispose();
							}
						}
					}
				}
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00041DE0 File Offset: 0x0003FFE0
		private string ProcessChannelIdOnRequestAndGenerateIfNeeded(string channelId, bool finalizeNotifiers)
		{
			if (channelId == null)
			{
				if (!finalizeNotifiers)
				{
					channelId = ListenerChannelsManager.GeneratedChannelId();
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[PendingRequestEventHandler::DisposePendingNotificationClientRequest] Generated ChannelId: {0}", channelId);
				}
				else
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "[PendingRequestEventHandler::DisposePendingNotificationClientRequest] No channel specified. Don't generate this is a finalize notifier request. This must be the first request from the client.");
				}
			}
			else
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "[PendingRequestEventHandler::DisposePendingNotificationClientRequest] ChannelId on request: {0}", channelId);
			}
			return channelId;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00041E44 File Offset: 0x00040044
		private void UseNotificationPipeCallback(IAsyncResult async)
		{
			OwaAsyncResult owaAsyncResult = (OwaAsyncResult)async;
			PendingRequestChannel pendingRequestChannel = null;
			if (this.pendingRequestManager == null)
			{
				return;
			}
			pendingRequestChannel = this.pendingRequestManager.GetPendingGetChannel(owaAsyncResult.PendingGetId);
			if (pendingRequestChannel == null)
			{
				return;
			}
			try
			{
				pendingRequestChannel.EndSendNotification(owaAsyncResult);
			}
			catch (Exception exception)
			{
				if (this.pgEventDataDictionary.ContainsKey(owaAsyncResult.PendingGetId))
				{
					lock (this.pgEventDataDictionary[owaAsyncResult.PendingGetId].AsyncResult)
					{
						if (!this.pgEventDataDictionary[owaAsyncResult.PendingGetId].AsyncResult.IsCompleted)
						{
							this.pgEventDataDictionary[owaAsyncResult.PendingGetId].AsyncResult.Exception = exception;
						}
					}
				}
			}
			try
			{
				lock (this.pgEventDataDictionary)
				{
					if (this.pgEventDataDictionary.ContainsKey(owaAsyncResult.PendingGetId))
					{
						lock (this.pgEventDataDictionary[owaAsyncResult.PendingGetId].AsyncResult)
						{
							if (!this.pgEventDataDictionary[owaAsyncResult.PendingGetId].AsyncResult.IsCompleted)
							{
								this.pgEventDataDictionary[owaAsyncResult.PendingGetId].AsyncResult.CompleteRequest(owaAsyncResult.CompletedSynchronously);
							}
						}
						this.pgEventDataDictionary.Remove(owaAsyncResult.PendingGetId);
					}
				}
			}
			finally
			{
				try
				{
					pendingRequestChannel.RecordFinishPendingRequest();
				}
				catch (OwaOperationNotSupportedException ex)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "An exception was thrown during the processing of the async callback: {0}", ex.Message);
				}
			}
		}

		// Token: 0x04000A1D RID: 2589
		internal const string EventNameSpace = "PendingRequest";

		// Token: 0x04000A1E RID: 2590
		internal const string MethodPendingNotificationRequest = "PendingNotificationRequest";

		// Token: 0x04000A1F RID: 2591
		internal const string MethodFinishNotificationRequest = "FinishNotificationRequest";

		// Token: 0x04000A20 RID: 2592
		private const string FinalizeNotifiersParameter = "Fn";

		// Token: 0x04000A21 RID: 2593
		private const string KeepAliveParameter = "A";

		// Token: 0x04000A22 RID: 2594
		private const string UserActivityParameter = "UA";

		// Token: 0x04000A23 RID: 2595
		private const string RealmParameter = "realm";

		// Token: 0x04000A24 RID: 2596
		private const string UniquePendingGetIdentifierParameter = "n";

		// Token: 0x04000A25 RID: 2597
		private const string PendingGetIdentifier = "cid";

		// Token: 0x04000A26 RID: 2598
		private Dictionary<string, PendingRequestEventData> pgEventDataDictionary = new Dictionary<string, PendingRequestEventData>();

		// Token: 0x04000A27 RID: 2599
		private volatile bool isDisposing;

		// Token: 0x04000A28 RID: 2600
		private PendingRequestManager pendingRequestManager;
	}
}
