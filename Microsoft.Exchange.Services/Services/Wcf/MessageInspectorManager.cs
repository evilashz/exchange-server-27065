using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.DispatchPipe.Ews;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DB7 RID: 3511
	internal class MessageInspectorManager : IDispatchMessageInspector
	{
		// Token: 0x06005955 RID: 22869 RVA: 0x00117272 File Offset: 0x00115472
		internal MessageInspectorManager()
		{
			this.inboundInspectors = new List<IInboundInspector>();
			this.outboundInspectors = new List<IOutboundInspector>();
			this.AddRequestResponseInspectors();
		}

		// Token: 0x06005956 RID: 22870 RVA: 0x001172AC File Offset: 0x001154AC
		public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			return this.InternalAfterReceiveRequest(ref request, channel, instanceContext, null);
		}

		// Token: 0x06005957 RID: 22871 RVA: 0x001173BC File Offset: 0x001155BC
		protected virtual object InternalAfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext, MessageBuffer buffer)
		{
			this.TraceCorrelationHeader(EWSSettings.RequestCorrelation);
			DispatchByBodyElementOperationSelector.CheckWcfDelayedException(request);
			this.CheckForDuplicateHeaders(request);
			CallContext callContext = null;
			try
			{
				FaultInjection.GenerateFault((FaultInjection.LIDs)3286641981U);
				MessageHeaderProcessor messageHeaderProcessor = (MessageHeaderProcessor)request.Properties["MessageHeaderProcessor"];
				messageHeaderProcessor.MarkMessageHeaderAsUnderstoodIfExists(request, "RequestServerVersion", "http://schemas.microsoft.com/exchange/services/2006/types");
				Message requestRef = request;
				RequestDetailsLogger.Current.TrackLatency(ServiceLatencyMetadata.CallContextInitLatency, delegate()
				{
					callContext = CallContext.CreateFromRequest(messageHeaderProcessor, requestRef);
					HttpContext.Current.Items["CallContext"] = callContext;
					if (Global.ChargePreExecuteToBudgetEnabled)
					{
						IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
						TimeSpan timeSpan = TimeSpan.Zero;
						if (currentActivityScope != null && currentActivityScope.TotalMilliseconds >= 0.0)
						{
							timeSpan = TimeSpan.FromMilliseconds(Math.Max(currentActivityScope.TotalMilliseconds - EWSSettings.WcfDispatchLatency, 0.0));
							ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<TimeSpan, double, double>((long)this.GetHashCode(), "[MessageInspectorManager::AfterReceiveRequest] preExecuteCharge = {0}; preExecuteLatency = {1}, WcfDispatchlatency = {2}", timeSpan, currentActivityScope.TotalMilliseconds, EWSSettings.WcfDispatchLatency);
						}
						callContext.Budget.StartLocal("MessageInspectorManager.AfterReceiveRequest[" + callContext.MethodName + "]", timeSpan);
					}
				});
			}
			catch (LocalizedException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string, string>((long)this.GetHashCode(), "[MessageInspectorManager::AfterReceiveRequest] Caught localized exception trying to create callcontext.  Class: {0}, Message: {1}", ex.GetType().FullName, ex.Message);
				throw FaultExceptionUtilities.CreateFault(ex, FaultParty.Receiver);
			}
			catch (AuthzException innerException)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug(0L, "[MessageInspectorManager::AfterReceiveRequest] Trying to create a CallContext failed with AuthZException.");
				AuthZFailureException exception = new AuthZFailureException(innerException);
				throw FaultExceptionUtilities.CreateFault(exception, FaultParty.Sender);
			}
			bool flag = false;
			try
			{
				if (callContext.AccessingPrincipal != null && ExUserTracingAdaptor.Instance.IsTracingEnabledUser(callContext.AccessingPrincipal.LegacyDn))
				{
					flag = true;
					BaseTrace.CurrentThreadSettings.EnableTracing();
				}
				if (!callContext.CallerHasAccess())
				{
					EWSAuthorizationManager.Return403ForbiddenResponse(EwsOperationContextBase.Current, "User not allowed to access EWS");
					throw FaultExceptionUtilities.CreateFault(new ServiceAccessDeniedException(), FaultParty.Receiver);
				}
				if (buffer == null)
				{
					buffer = this.CreateMessageBuffer(request);
					if (Global.UseGcCollect && buffer.BufferSize > Global.CreateItemRequestSizeThreshold)
					{
						using (Process currentProcess = Process.GetCurrentProcess())
						{
							if (currentProcess.PrivateMemorySize64 > (long)Global.PrivateWorkingSetThreshold)
							{
								this.CheckCollectIntervalAndCollect();
							}
						}
					}
				}
				EWSSettings.MessageCopyForProxyOnly = buffer.CreateMessage();
				foreach (IInboundInspector inboundInspector in this.inboundInspectors)
				{
					Message request2 = buffer.CreateMessage();
					inboundInspector.ProcessInbound(ExchangeVersion.Current, request2);
				}
				request = buffer.CreateMessage();
			}
			finally
			{
				if (flag)
				{
					BaseTrace.CurrentThreadSettings.DisableTracing();
				}
			}
			return null;
		}

		// Token: 0x06005958 RID: 22872 RVA: 0x00117658 File Offset: 0x00115858
		protected MessageBuffer CreateMessageBuffer(Message request)
		{
			MessageBuffer result = null;
			try
			{
				result = request.CreateBufferedCopy(int.MaxValue);
			}
			catch (ArgumentException ex)
			{
				throw FaultExceptionUtilities.CreateFault(new SchemaValidationException(ex, 0, 0, ex.Message), FaultParty.Sender);
			}
			catch (SchemaValidationException exception)
			{
				throw FaultExceptionUtilities.DealWithSchemaViolation(exception, request);
			}
			catch (XmlException ex2)
			{
				throw FaultExceptionUtilities.CreateFault(new SchemaValidationException(ex2, ex2.LineNumber, ex2.LinePosition, ex2.Message), FaultParty.Sender);
			}
			return result;
		}

		// Token: 0x06005959 RID: 22873 RVA: 0x001176E0 File Offset: 0x001158E0
		private void CheckCollectIntervalAndCollect()
		{
			lock (this.collectTimeLock)
			{
				if (DateTime.UtcNow > this.lastCollectTime.AddMilliseconds((double)Global.CollectIntervalInMilliseconds))
				{
					GC.Collect();
					this.lastCollectTime = DateTime.UtcNow;
				}
			}
		}

		// Token: 0x0600595A RID: 22874 RVA: 0x00117748 File Offset: 0x00115948
		private static bool IsOperationIn(string[] operationsToCheckAgainst, string methodName)
		{
			if (methodName == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError(0L, "[MessageInspectorManager::IsOperationIn] message did not have Action header.");
				return false;
			}
			foreach (string value in operationsToCheckAgainst)
			{
				if (methodName.Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x00117790 File Offset: 0x00115990
		private void TraceCorrelationHeader(Guid correlationGuid)
		{
			if (ExTraceGlobals.AllRequestsTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				string traceCorrelationHeader = MessageEncoderWithXmlDeclaration.GetTraceCorrelationHeader(correlationGuid);
				ExTraceGlobals.AllRequestsTracer.TraceDebug((long)this.GetHashCode(), traceCorrelationHeader);
			}
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x001177C4 File Offset: 0x001159C4
		private void CreateReplyFromHTTPProxyResponse(MessageVersion messageVersion, string action, out Message reply)
		{
			try
			{
				FaultInjection.GenerateFault((FaultInjection.LIDs)4259720509U);
				reply = ProxyResponseMessage.Create();
			}
			catch (WebException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string, string>((long)this.GetHashCode(), "[MessageInspectorManager::CreateReplyFromHTTPProxyResponse] Caught WebException trying to create XmlReader from proxy response stream.  WebException status: {0}, Message: {1}", ex.Status.ToString(), ex.Message);
				if (ex.Status != WebExceptionStatus.RequestCanceled)
				{
					throw;
				}
				FaultException ex2 = FaultExceptionUtilities.CreateFault(new TransientException(CoreResources.GetLocalizedString((CoreResources.IDs)3995283118U), ex), FaultParty.Receiver);
				MessageFault fault = ex2.CreateMessageFault();
				reply = Message.CreateMessage(messageVersion, fault, action);
			}
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x00117858 File Offset: 0x00115A58
		public void BeforeSendReply(ref Message reply, object correlationState)
		{
			this.InternalBeforeSendReply(ref reply, correlationState);
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x00117864 File Offset: 0x00115A64
		public Message BeforeSendReply(HttpResponse httpResponse, MessageVersion messageVersion, string action)
		{
			Message result = null;
			this.InternalBeforeSendReply(httpResponse, ref result, messageVersion, action, null);
			return result;
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x00117880 File Offset: 0x00115A80
		protected virtual void InternalBeforeSendReply(ref Message reply, object correlationState)
		{
			this.InternalBeforeSendReply((HttpContext.Current != null && HttpContext.Current.Response != null) ? HttpContext.Current.Response : null, ref reply, (reply != null) ? reply.Version : null, (reply != null) ? reply.Headers.Action : null, correlationState);
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x001178D8 File Offset: 0x00115AD8
		private void InternalBeforeSendReply(HttpResponse httpResponse, ref Message reply, MessageVersion messageVersion, string action, object correlationState)
		{
			if (EWSSettings.ProxyResponse != null)
			{
				this.CreateReplyFromHTTPProxyResponse(messageVersion, action, out reply);
				Dictionary<string, string> proxyHopHeaders = EWSSettings.ProxyHopHeaders;
				if (!Global.WriteProxyHopHeaders || proxyHopHeaders == null || httpResponse == null)
				{
					return;
				}
				using (Dictionary<string, string>.Enumerator enumerator = proxyHopHeaders.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, string> keyValuePair = enumerator.Current;
						httpResponse.AppendHeader(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			ExchangeVersion requestVersion = ExchangeVersion.Current;
			if (httpResponse != null)
			{
				if (Global.WriteFailoverTypeHeader && EWSSettings.FailoverType != null)
				{
					httpResponse.AppendHeader("FailoverType", EWSSettings.FailoverType);
				}
				if (EWSSettings.ExceptionType != null)
				{
					httpResponse.AppendHeader("X-BEServerException", EWSSettings.ExceptionType);
				}
			}
			if (reply != null)
			{
				MessageBuffer messageBuffer = (this.outboundInspectors.Count > 0) ? reply.CreateBufferedCopy(int.MaxValue) : null;
				try
				{
					foreach (IOutboundInspector outboundInspector in this.outboundInspectors)
					{
						Message reply2 = messageBuffer.CreateMessage();
						outboundInspector.ProcessOutbound(requestVersion, reply2);
					}
					if (messageBuffer != null)
					{
						reply = messageBuffer.CreateMessage();
					}
				}
				catch (FaultException ex)
				{
					MessageFault fault = ex.CreateMessageFault();
					reply = Message.CreateMessage(messageVersion, fault, action);
				}
			}
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x00117A48 File Offset: 0x00115C48
		private void CheckForDuplicateHeaders(Message request)
		{
			bool flag = false;
			bool flag2 = false;
			foreach (MessageHeaderInfo messageHeaderInfo in request.Headers)
			{
				MessageHeader header = (MessageHeader)messageHeaderInfo;
				this.ValidateS2SHeaderDups(header, ref flag);
				this.ValidateProxyHeaderDups(header, ref flag2);
			}
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x00117AAC File Offset: 0x00115CAC
		private void ValidateS2SHeaderDups(MessageHeader header, ref bool seenS2SHeader)
		{
			if ((header.Name == "ExchangeImpersonation" || header.Name == "SerializedSecurityContext") && header.Namespace == "http://schemas.microsoft.com/exchange/services/2006/types")
			{
				if (seenS2SHeader)
				{
					throw FaultExceptionUtilities.CreateFault(new MoreThanOneAccessModeSpecifiedException(), FaultParty.Sender);
				}
				seenS2SHeader = true;
			}
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x00117B04 File Offset: 0x00115D04
		private void ValidateProxyHeaderDups(MessageHeader header, ref bool seenProxyHeader)
		{
			if ((header.Name == "ProxySecurityContext" || header.Name == "ProxySuggesterSid" || header.Name == "ProxyPartnerToken") && header.Namespace == "http://schemas.microsoft.com/exchange/services/2006/types")
			{
				if (seenProxyHeader)
				{
					throw FaultExceptionUtilities.CreateFault(new InvalidProxySecurityContextException(), FaultParty.Sender);
				}
				seenProxyHeader = true;
			}
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x00117B6C File Offset: 0x00115D6C
		protected virtual void AddRequestResponseInspectors()
		{
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x00117B6E File Offset: 0x00115D6E
		internal static bool IsEWSRequest(string methodName)
		{
			return !MessageInspectorManager.IsAvailabilityRequest(methodName);
		}

		// Token: 0x06005966 RID: 22886 RVA: 0x00117B79 File Offset: 0x00115D79
		internal static bool IsAvailabilityRequest(string methodName)
		{
			return MessageInspectorManager.IsOperationIn(MessageInspectorManager.ASOperations, methodName);
		}

		// Token: 0x04003180 RID: 12672
		internal const string ResponseHasBegunKey = "ResponseHasBegun";

		// Token: 0x04003181 RID: 12673
		private static readonly string[] ASOperations = new string[]
		{
			"GetUserAvailability",
			"GetUserOofSettings",
			"SetUserOofSettings"
		};

		// Token: 0x04003182 RID: 12674
		private static readonly string[] DelayExecutedOperations = new string[]
		{
			"GetItem",
			"GetAttachment",
			"ExportItems"
		};

		// Token: 0x04003183 RID: 12675
		protected List<IInboundInspector> inboundInspectors;

		// Token: 0x04003184 RID: 12676
		protected List<IOutboundInspector> outboundInspectors;

		// Token: 0x04003185 RID: 12677
		private object collectTimeLock = new object();

		// Token: 0x04003186 RID: 12678
		private DateTime lastCollectTime = DateTime.UtcNow;
	}
}
