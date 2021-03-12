using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Security.PartnerToken;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200069A RID: 1690
	internal class ProxyServiceTask<T> : BaseServiceTask<T>, IDisposeTrackable, IDisposable
	{
		// Token: 0x060033E3 RID: 13283 RVA: 0x000B9AD8 File Offset: 0x000B7CD8
		static ProxyServiceTask()
		{
			CertificateValidationManager.RegisterCallback("EWS", Global.RemoteCertificateValidationCallback);
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000B9B57 File Offset: 0x000B7D57
		public ProxyServiceTask(BaseRequest request, CallContext callContext, ServiceAsyncResult<T> serviceAsyncResult, WebServicesInfo[] services) : this(request, callContext, serviceAsyncResult, services, ProxyServiceTask<T>.ConstructServersAttemptedString(services))
		{
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x000B9B74 File Offset: 0x000B7D74
		private ProxyServiceTask(BaseRequest request, CallContext callContext, ServiceAsyncResult<T> serviceAsyncResult, WebServicesInfo[] services, string serversAttemptedForLog) : base(request, callContext, serviceAsyncResult)
		{
			this.services = services;
			this.disposeTracker = this.GetDisposeTracker();
			this.budget = callContext.Budget;
			this.serversAttemptedForLog = serversAttemptedForLog;
			if (services != null)
			{
				this.ProxyToCafe = services.Any((WebServicesInfo wsInfo) => wsInfo.IsCafeUrl);
			}
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x000B9C04 File Offset: 0x000B7E04
		private static string ConstructServersAttemptedString(WebServicesInfo[] services)
		{
			if (services == null || services.Length == 0)
			{
				return string.Empty;
			}
			string[] array = new string[services.Length];
			for (int i = 0; i < services.Length; i++)
			{
				array[i] = services[i].ServerFullyQualifiedDomainName;
			}
			return string.Join("|", array);
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x060033E7 RID: 13287 RVA: 0x000B9C4C File Offset: 0x000B7E4C
		// (set) Token: 0x060033E8 RID: 13288 RVA: 0x000B9C54 File Offset: 0x000B7E54
		public bool ProxyToCafe { get; private set; }

		// Token: 0x060033E9 RID: 13289 RVA: 0x000B9C5D File Offset: 0x000B7E5D
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ProxyServiceTask<T>>(this);
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000B9C65 File Offset: 0x000B7E65
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000B9C7A File Offset: 0x000B7E7A
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000B9C9C File Offset: 0x000B7E9C
		private void Dispose(bool fromDispose)
		{
			if (!this.disposed)
			{
				if (fromDispose)
				{
					lock (this.instanceLock)
					{
						this.InternalDispose();
						return;
					}
				}
				this.InternalDispose();
			}
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x000B9CF0 File Offset: 0x000B7EF0
		protected virtual void InternalDispose()
		{
			if (!this.disposed)
			{
				lock (this.eventLock)
				{
					if (this.proxyDoneEvent != null)
					{
						if (this.registeredWaitHandle != null)
						{
							this.registeredWaitHandle.Unregister(this.proxyDoneEvent);
							this.registeredWaitHandle = null;
						}
						this.proxyDoneEvent.Close();
						this.proxyDoneEvent = null;
						if (this.authenticationContext != null)
						{
							this.authenticationContext.Dispose();
						}
						this.authenticationContext = null;
					}
				}
			}
			this.disposed = true;
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x060033EE RID: 13294 RVA: 0x000B9D90 File Offset: 0x000B7F90
		public override IBudget Budget
		{
			get
			{
				return this.budget;
			}
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000B9E08 File Offset: 0x000B8008
		protected internal override TaskExecuteResult InternalExecute(TimeSpan queueAndDelay, TimeSpan totalTime)
		{
			base.SendWatsonReportOnGrayException(delegate()
			{
				try
				{
					if (!this.ProxyRequest())
					{
						ExTraceGlobals.ProxyEvaluatorTracer.TraceError((long)this.GetHashCode(), "[ProxyServiceTask.InternalExecute] Failed to proxy task.");
						this.executionException = FaultExceptionUtilities.CreateFault(new NoRespondingCASInDestinationSiteException(), FaultParty.Receiver);
					}
				}
				catch (FaultException ex)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceError<FaultException>((long)this.GetHashCode(), "[ProxyServiceTask.InternalExecute] Caught FaultException when proxying task.  Exception: {0}", ex);
					this.executionException = ex;
				}
			});
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000B9E20 File Offset: 0x000B8020
		protected internal override TaskExecuteResult InternalCancelStep(LocalizedException exception)
		{
			if (exception != null)
			{
				this.executionException = exception;
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(base.CallContext.ProtocolLog, exception, "ProxyServiceTask_Cancel");
			}
			else
			{
				this.executionException = new TransientException(CoreResources.GetLocalizedString((CoreResources.IDs)3995283118U), new Exception("[ProxyServiceTask.InternalCancelStep()] called without an exception."));
			}
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000B9E70 File Offset: 0x000B8070
		protected internal override void InternalComplete(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			if (this.executionException == null)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<int>((long)this.GetHashCode(), "[{0}] [ProxyServiceTask.InternalComplete] Was called no exception. No-op.", this.GetHashCode());
				this.queueAndDelayTime = queueAndDelayTime;
				return;
			}
			string arg = (this.proxiedToService == null) ? string.Empty : this.proxiedToService.Url.ToString();
			ExTraceGlobals.ProxyEvaluatorTracer.TraceError<int, string>((long)this.GetHashCode(), "[{0}] [ProxyServiceTask.InternalComplete] Encountered error during execution. Exception: {1}", this.GetHashCode(), this.executionException.Message);
			this.FinishRequest(string.Format("[CWE,P({0})]", arg), queueAndDelayTime, TimeSpan.Zero, this.executionException);
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000B9F10 File Offset: 0x000B8110
		protected internal override void InternalTimeout(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			string arg = (this.proxiedToService == null) ? "<NULL>" : this.proxiedToService.Url.ToString();
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, TimeSpan, TimeSpan>((long)this.GetHashCode(), "[ProxyServiceTask.InternalTimeout] Encountered timeout for proxy task sending to CAS: '{0}'.  QueueAndDelay: {1}, TotalTime: {2}", arg, queueAndDelayTime, totalTime);
			this.FinishRequest(string.Format("[T,P({0})]", arg), queueAndDelayTime, TimeSpan.Zero, null);
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000B9F70 File Offset: 0x000B8170
		protected override void FinishRequest(string logType, TimeSpan queueAndDelayTime, TimeSpan totalTime, Exception exception)
		{
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<int, string, string>((long)this.GetHashCode(), "[{0}] [ProxyServiceTask.FinishRequest] Called. Exception={1}, logType={2}", this.GetHashCode(), (exception != null) ? exception.ToString() : "<no exception>", (!string.IsNullOrEmpty(logType)) ? logType : "<null/empty>");
			bool flag = false;
			if (!this.completed)
			{
				lock (this.instanceLock)
				{
					if (!this.completed)
					{
						ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug((long)this.GetHashCode(), "[ProxyServiceTask.FinishRequest] Completing request");
						base.FinishRequest(logType, queueAndDelayTime, totalTime, exception);
						flag = true;
						this.completed = true;
					}
				}
			}
			if (!flag)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug((long)this.GetHashCode(), "[ProxyServiceTask.FinishRequest] WCF request was already finished, so there is nothing for us to complete.");
			}
			this.Dispose();
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000BA044 File Offset: 0x000B8244
		protected internal override ResourceKey[] InternalGetResources()
		{
			return null;
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000BA048 File Offset: 0x000B8248
		private void TimeoutCallback(object state, bool timedOut)
		{
			if (!this.completed)
			{
				lock (this.instanceLock)
				{
					if (!this.completed && timedOut)
					{
						ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, Uri>((long)this.GetHashCode(), "[ProxyServiceTask.TimeoutCallback] Request timed out during proxy for task {0}, Url: {1}", base.Description, this.webRequest.Address);
						this.webRequest.Abort();
					}
				}
			}
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000BA0C8 File Offset: 0x000B82C8
		private bool ProxyRequest()
		{
			IIdentity identity = base.CallContext.HttpContext.User.Identity;
			WindowsIdentity windowsIdentity = identity as WindowsIdentity;
			OAuthIdentity oauthIdentity = identity as OAuthIdentity;
			ClientSecurityContextIdentity clientSecurityContextIdentity = identity as ClientSecurityContextIdentity;
			ExternalIdentity externalIdentity = identity as ExternalIdentity;
			PartnerIdentity partnerIdentity = identity as PartnerIdentity;
			WebServicesInfo[] array = this.services;
			int i = 0;
			while (i < array.Length)
			{
				WebServicesInfo webServicesInfo = array[i];
				bool result;
				if (!base.CallContext.HttpContext.Response.IsClientConnected)
				{
					this.HandleClientDisconnect();
					result = false;
				}
				else
				{
					bool flag = false;
					if (oauthIdentity != null)
					{
						flag = this.ProxyRequestToInternalCAS(webServicesInfo, oauthIdentity);
					}
					else if (externalIdentity != null)
					{
						flag = this.ProxyRequestToExternalCAS(webServicesInfo, externalIdentity);
					}
					else if (partnerIdentity != null)
					{
						flag = this.ProxyRequestToInternalCAS(webServicesInfo, partnerIdentity);
					}
					else if (clientSecurityContextIdentity != null)
					{
						flag = this.ProxyRequestToInternalCAS(webServicesInfo, clientSecurityContextIdentity);
					}
					else if (windowsIdentity != null)
					{
						flag = this.ProxyRequestToInternalCAS(webServicesInfo, windowsIdentity);
					}
					if (!flag)
					{
						ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>((long)this.GetHashCode(), "[ProxyServiceTask.ProxyRequest] An attempt to proxy has failed for task {0}", base.Description);
						PerformanceMonitor.UpdateTotalProxyFailoversCount();
						i++;
						continue;
					}
					result = true;
				}
				return result;
			}
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>((long)this.GetHashCode(), "[ProxyServiceTask.ProxyRequest] Could not proxy request for task {0} after trying all applicable CAS servers.", base.Description);
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(base.CallContext.ProtocolLog, "ProxyServersAttempted", this.serversAttemptedForLog);
			ProxyEventLogHelper.LogNoRespondingDestinationCAS(this.services[0].SiteDistinguishedName);
			return false;
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000BA22C File Offset: 0x000B842C
		private void HandleClientDisconnect()
		{
			base.CallContext.HttpContext.Response.Clear();
			base.CallContext.HttpContext.Response.StatusCode = 8;
			this.LogProxyException("N/A", "ClientDisconnectedFromRequest");
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>(0L, "[ProxyServiceTask.HandleClientDisconnect] Client disconnected for task {0}", base.Description);
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000BA28C File Offset: 0x000B848C
		private HttpWebRequest CreateWebRequest(string targetUrl)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(targetUrl);
			httpWebRequest.Method = "POST";
			HttpRequest request = base.CallContext.HttpContext.Request;
			this.CopyHttpHeaders(request, httpWebRequest);
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.GlobalCriminalCompliance.Enabled && EWSSettings.AreGccStoredSecretKeysValid)
			{
				this.CopyOrCreateNewXGccProxyInfoHeader(httpWebRequest);
			}
			httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
			httpWebRequest.ServicePoint.Expect100Continue = false;
			httpWebRequest.KeepAlive = false;
			httpWebRequest.Timeout = (int)this.MaxExecutionTime.TotalMilliseconds + 60000;
			httpWebRequest.Proxy = new WebProxy();
			httpWebRequest.ServerCertificateValidationCallback = Global.RemoteCertificateValidationCallback;
			CertificateValidationManager.SetComponentId(httpWebRequest, "EWS");
			return httpWebRequest;
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000BA34C File Offset: 0x000B854C
		private void CopyOrCreateNewXGccProxyInfoHeader(HttpWebRequest toRequest)
		{
			string value;
			if ((GccUtils.TryGetGccProxyInfo(base.CallContext.HttpContext, out value) || GccUtils.TryCreateGccProxyInfo(base.CallContext.HttpContext, out value)) && !string.IsNullOrEmpty(value))
			{
				toRequest.Headers.Add("X-GCC-PROXYINFO", value);
			}
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000BA39C File Offset: 0x000B859C
		private void CopyHttpHeaders(HttpRequest originalRequest, HttpWebRequest toRequest)
		{
			toRequest.ContentType = originalRequest.ContentType;
			toRequest.TransferEncoding = null;
			toRequest.SendChunked = false;
			toRequest.UserAgent = ProxyServiceTask<T>.userAgentPrefix + originalRequest.UserAgent;
			string[] allKeys = originalRequest.Headers.AllKeys;
			foreach (string text in allKeys)
			{
				if (!ProxyServiceTask<T>.specialHeaders.Member.Contains(text.ToUpperInvariant()))
				{
					string value = originalRequest.Headers[text];
					toRequest.Headers.Add(text, value);
				}
			}
			if (originalRequest.RequestContext.HttpContext.Items.Contains("AnchorMailboxHintKey"))
			{
				string text2 = originalRequest.RequestContext.HttpContext.Items["AnchorMailboxHintKey"] as string;
				if (SmtpAddress.IsValidSmtpAddress(text2))
				{
					toRequest.Headers[WellKnownHeader.AnchorMailbox] = text2;
				}
				toRequest.Headers[WellKnownHeader.Authorization] = originalRequest.Headers[WellKnownHeader.Authorization];
				toRequest.Headers[WellKnownHeader.XIsFromBackend] = Global.BooleanTrue;
			}
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000BA4BC File Offset: 0x000B86BC
		private bool ProxyRequestToInternalCAS(WebServicesInfo service, WindowsIdentity windowsIdentity)
		{
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, string>(0L, "[ProxyServiceTask.ProxyRequestToInternalCAS] Attempting to proxy request internally for task {0} to CAS {1} (called with a WindowsIdentity)", base.Description, service.ServerFullyQualifiedDomainName);
			ProxyHeaderValue proxyHeaderValue = null;
			this.suggesterSid = this.GetProxyHeaderValue(windowsIdentity, service, ref proxyHeaderValue);
			this.proxyHeaderValue = proxyHeaderValue;
			Message messageToProxy = this.GetMessageToProxy();
			ProxyHeaderXmlWriter.AddProxyHeader(messageToProxy, this.proxyHeaderValue);
			return this.ProxyRequestToSingleCAS(service, messageToProxy, service.Url.ToString(), true, null);
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000BA528 File Offset: 0x000B8728
		private bool ProxyRequestToInternalCAS(WebServicesInfo service, ClientSecurityContextIdentity cscIdentity)
		{
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, string>(0L, "[ProxyServiceTask.ProxyRequestToInternalCAS] Attempting to proxy request internally for task {0} to CAS {1} (called with a ClientSecurityContextIdentity)", base.Description, service.ServerFullyQualifiedDomainName);
			ProxyHeaderValue proxyHeaderValue = null;
			this.suggesterSid = this.GetProxyHeaderValue(cscIdentity, service, ref proxyHeaderValue);
			this.proxyHeaderValue = proxyHeaderValue;
			Message messageToProxy = this.GetMessageToProxy();
			ProxyHeaderXmlWriter.AddProxyHeader(messageToProxy, this.proxyHeaderValue);
			return this.ProxyRequestToSingleCAS(service, messageToProxy, service.Url.ToString(), true, null);
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000BA594 File Offset: 0x000B8794
		private bool ProxyRequestToInternalCAS(WebServicesInfo service, OAuthIdentity oauthIdentity)
		{
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, string>(0L, "[ProxyServiceTask.ProxyRequestToInternalCAS] Attempting to proxy request internally for task {0} to CAS {1} (called with OAuthIdentity)", base.Description, service.ServerFullyQualifiedDomainName);
			if (service.ServerVersionNumber < Server.E15MinVersion)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceWarning<int>(0L, "[ProxyServiceTask.ProxyRequestToInternalCAS] returning false since the backend version is {0}, not E15+", service.ServerVersionNumber);
				this.LogProxyException(this.proxiedToService.ServerFullyQualifiedDomainName, "NotE15BE_" + service.ServerVersionNumber);
				return false;
			}
			Message messageToProxy = this.GetMessageToProxy();
			ProxyHeaderXmlWriter.RemoveProxyHeaders(messageToProxy);
			return this.ProxyRequestToSingleCAS(service, messageToProxy, service.Url.ToString(), true, oauthIdentity.ToCommonAccessToken(service.ServerVersionNumber));
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000BA638 File Offset: 0x000B8838
		private bool ProxyRequestToInternalCAS(WebServicesInfo service, PartnerIdentity partnerIdentity)
		{
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, string, DelegatedPrincipal>(0L, "[ProxyServiceTask.ProxyRequestToInternalCAS] Attempting to proxy request internally for task {0} to CAS {1}, the caller is {2}", base.Description, service.ServerFullyQualifiedDomainName, partnerIdentity.DelegatedPrincipal);
			PartnerAccessToken partnerAccessToken = new PartnerAccessToken(partnerIdentity);
			this.proxyHeaderValue = new ProxyHeaderValue(ProxyHeaderType.PartnerToken, partnerAccessToken.GetBytes());
			Message messageToProxy = this.GetMessageToProxy();
			ProxyHeaderXmlWriter.AddProxyHeader(messageToProxy, this.proxyHeaderValue);
			return this.ProxyRequestToSingleCAS(service, messageToProxy, service.Url.ToString(), true, null);
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000BA6AC File Offset: 0x000B88AC
		private bool ProxyRequestToExternalCAS(WebServicesInfo casInstance, ExternalIdentity externalIdentity)
		{
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, string>(0L, "[ProxyServiceTask.ProxyRequestToExternalCAS] Attempting to proxy request externally for task {0} to CAS {1}", base.Description, casInstance.ServerFullyQualifiedDomainName);
			Message messageToProxy = this.GetMessageToProxy();
			ProxyHeaderXmlWriter.RemoveProxyHeaders(messageToProxy);
			string text = casInstance.Url.ToString();
			if (externalIdentity.WSSecurityHeader != null)
			{
				text = EwsWsSecurityUrl.Fix(text);
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>((long)this.GetHashCode(), "[ProxyServiceTask.ProxyRequestToExternalCAS] Correcting EWS target due to WSSecurity Header.  Corrected target: '{0}'", text);
			}
			return this.ProxyRequestToSingleCAS(casInstance, messageToProxy, text, false, null);
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000BA720 File Offset: 0x000B8920
		private Message GetMessageToProxy()
		{
			Message message = EWSSettings.MessageCopyForProxyOnly;
			Message result;
			using (MessageBuffer messageBuffer = message.CreateBufferedCopy(int.MaxValue))
			{
				message = messageBuffer.CreateMessage();
				EWSSettings.MessageCopyForProxyOnly = messageBuffer.CreateMessage();
				result = message;
			}
			return result;
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000BA770 File Offset: 0x000B8970
		private bool ProxyRequestToSingleCAS(WebServicesInfo casInstance, Message messageToProxy, string targetUrl, bool needsImpersonation, CommonAccessToken commonAccessToken = null)
		{
			byte[] array;
			int num;
			this.GetMessageBuffer(messageToProxy, out array, out num);
			this.requestStart = ExDateTime.UtcNow;
			this.webRequest = this.CreateWebRequest(targetUrl);
			if (commonAccessToken != null)
			{
				this.webRequest.Headers["X-CommonAccessToken"] = commonAccessToken.Serialize();
			}
			this.webRequest.ContentLength = (long)num;
			PerformanceMonitor.UpdateTotalProxyRequestBytesCount((long)num);
			this.requestBuffer = array;
			this.proxiedToService = casInstance;
			try
			{
				if (!Global.ProxyToSelf && needsImpersonation && this.proxiedToService != null && !this.proxiedToService.IsCafeUrl)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug((long)this.GetHashCode(), "[ProxyServiceTask.ProxyRequestToSingleCAS] Impersonating NetworkService for HTTP proxy call.");
					this.webRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
					if (Global.SendXBEServerExceptionHeaderToCafe)
					{
						Stopwatch stopwatch = new Stopwatch();
						try
						{
							stopwatch.Start();
							string value = this.GenerateKerberosAuthHeader(this.webRequest.Address.Host);
							if (!string.IsNullOrEmpty(value))
							{
								this.webRequest.Headers[WellKnownHeader.Authorization] = value;
							}
						}
						finally
						{
							stopwatch.Stop();
							long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
							RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(base.CallContext.ProtocolLog, "KerbAuthLatency", elapsedMilliseconds);
						}
					}
				}
				if (needsImpersonation)
				{
					IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
					if (currentActivityScope != null)
					{
						currentActivityScope.SerializeTo(this.webRequest);
					}
				}
				this.webRequest.BeginGetRequestStream(new AsyncCallback(this.RequestCallback), null);
			}
			catch (WebException ex)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceError<string, WebException>((long)this.GetHashCode(), "[ProxyServiceTask.ProxyRequestToSingleCAS] Encountered WebException sending request for task {0}. Exception: {1}", base.Description, ex);
				this.LogProxyException(this.webRequest.Host, string.Format("BeginGetRequestStream_{0}_{1}", ex.Status, ex.Message));
				return false;
			}
			lock (this.eventLock)
			{
				if (this.proxyDoneEvent != null)
				{
					this.registeredWaitHandle = ThreadPool.RegisterWaitForSingleObject(this.proxyDoneEvent, new WaitOrTimerCallback(this.TimeoutCallback), null, Global.ProxyTimeout, true);
				}
			}
			return true;
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000BA9D4 File Offset: 0x000B8BD4
		private string GenerateKerberosAuthHeader(string host)
		{
			byte[] bytes = null;
			if (this.authenticationContext != null)
			{
				this.authenticationContext.Dispose();
				this.authenticationContext = null;
			}
			this.authenticationContext = new AuthenticationContext();
			string result;
			try
			{
				string text = "HTTP/" + host;
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>((long)this.GetHashCode(), "[ProxyServiceTask::GenerateKerberosAuthHeader]: SPN {0}", text);
				this.authenticationContext.InitializeForOutboundNegotiate(AuthenticationMechanism.Kerberos, text, null, null);
				SecurityStatus securityStatus = this.authenticationContext.NegotiateSecurityContext(null, out bytes);
				if (securityStatus != SecurityStatus.OK && securityStatus != SecurityStatus.ContinueNeeded)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<SecurityStatus>((long)this.GetHashCode(), "[ProxyServiceTask::GenerateKerberosAuthHeader]: NegotiateSecurityContext failed with {0}", securityStatus);
					throw new HttpException(500, string.Format("NegotiateSecurityContext failed with for host '{0}' with status '{1}'", host, securityStatus));
				}
				string @string = Encoding.ASCII.GetString(bytes);
				result = "Negotiate " + @string;
			}
			catch (HttpException ex)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(base.CallContext.ProtocolLog, "GenerateKerberosAuthHeaderFailure", ex.Message);
				result = string.Empty;
			}
			finally
			{
				if (this.authenticationContext != null)
				{
					this.authenticationContext.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000BAAFC File Offset: 0x000B8CFC
		private void GetMessageBuffer(Message message, out byte[] messageBuffer, out int messageBufferLength)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream))
				{
					message.WriteMessage(xmlWriter);
					xmlWriter.Flush();
					memoryStream.Flush();
					messageBufferLength = (int)memoryStream.Position;
					messageBuffer = memoryStream.GetBuffer();
				}
			}
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000BAC5C File Offset: 0x000B8E5C
		private void RequestCallback(IAsyncResult asyncResult)
		{
			this.ExecuteWithinCallContext("RequestCallback", delegate(ref bool success)
			{
				Stream stream = null;
				try
				{
					stream = this.webRequest.EndGetRequestStream(asyncResult);
					stream.BeginWrite(this.requestBuffer, 0, (int)this.webRequest.ContentLength, new AsyncCallback(this.BeginWriteCallback), stream);
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<long>((long)this.GetHashCode(), "[ProxyServiceTask.RequestCallback] Request callback was called and a write was started of length {0} bytes", this.webRequest.ContentLength);
					success = true;
				}
				catch (WebException ex)
				{
					if (stream != null)
					{
						stream.Close();
					}
					ExTraceGlobals.ProxyEvaluatorTracer.TraceError<string, WebException>((long)this.GetHashCode(), "[ProxyServiceTask.RequestCallback] Encountered WebException for task {0}, Exception: {1}", this.Description, ex);
					this.ProcessAsyncWebException(ex, "RequestCallback");
				}
			});
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000BAD10 File Offset: 0x000B8F10
		private void ExecuteWithinCallContext(string info, ProxyServiceTask<T>.ExecuteWithinCallContextDelegate action)
		{
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Starting {0}: {1}", info, base.Description);
			bool success = false;
			if (!this.completed)
			{
				lock (this.instanceLock)
				{
					if (!this.completed)
					{
						CallContext current = CallContext.Current;
						try
						{
							CallContext.SetCurrent(base.CallContext);
							bool flag2 = false;
							if (base.CallContext.AccessingPrincipal != null && ExUserTracingAdaptor.Instance.IsTracingEnabledUser(base.CallContext.AccessingPrincipal.LegacyDn))
							{
								flag2 = true;
								BaseTrace.CurrentThreadSettings.EnableTracing();
							}
							try
							{
								base.SendWatsonReportOnGrayException(delegate()
								{
									action(ref success);
								}, delegate()
								{
									lock (this.eventLock)
									{
										if (this.proxyDoneEvent != null)
										{
											this.proxyDoneEvent.Set();
										}
									}
									base.Complete(TimeSpan.Zero, TimeSpan.Zero);
								});
							}
							finally
							{
								if (flag2)
								{
									BaseTrace.CurrentThreadSettings.DisableTracing();
								}
							}
						}
						finally
						{
							CallContext.SetCurrent(current);
							ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Finished {0}: {1}", info, base.Description);
							if (!success)
							{
								lock (this.eventLock)
								{
									if (this.proxyDoneEvent != null)
									{
										this.proxyDoneEvent.Set();
									}
								}
								this.Dispose();
							}
						}
					}
				}
			}
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000BAFEC File Offset: 0x000B91EC
		private void BeginWriteCallback(IAsyncResult result)
		{
			this.ExecuteWithinCallContext("BeginWriteCallback", delegate(ref bool success)
			{
				Stream stream = result.AsyncState as Stream;
				try
				{
					try
					{
						stream.EndWrite(result);
					}
					catch (IOException arg)
					{
						ExTraceGlobals.ProxyEvaluatorTracer.TraceError<IOException>((long)this.GetHashCode(), "[ProxyServiceTask::BeginWriteCallback] Encountered IOException: {0}", arg);
						this.ResubmitTask(false);
						return;
					}
					finally
					{
						stream.Close();
					}
					this.webRequest.BeginGetResponse(new AsyncCallback(this.ResponseCallback), null);
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug((long)this.GetHashCode(), "[ProxyServiceTask.BeginWriteCallback] Write callback was called, starting to get response.");
					success = true;
				}
				catch (WebException ex)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceError<WebException>((long)this.GetHashCode(), "[ProxyServiceTask::BeginWriteCallback] Encountered WebException: {0}", ex);
					this.ProcessAsyncWebException(ex, "BeginWriteCallback");
				}
			});
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x000BB024 File Offset: 0x000B9224
		private bool ProcessAsyncWebException(WebException webException, string caller)
		{
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug(0L, "[ProxyServiceTask::ProcessAsyncWebException] Web exception encountered.  URL: '{0}', Server: '{1}', Code: '{2}', Message: '{3}'", new object[]
			{
				this.proxiedToService.Url,
				this.proxiedToService.ServerFullyQualifiedDomainName,
				webException.Status,
				webException.Message
			});
			string text = null;
			HttpWebResponse httpWebResponse = webException.Response as HttpWebResponse;
			if (httpWebResponse != null)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, int, string>(0L, "[ProxyServiceTask::ProcessAsyncWebException] HTTP Response code: {0}, Value: {1}, Description: {2}", httpWebResponse.StatusCode.ToString(), (int)httpWebResponse.StatusCode, httpWebResponse.StatusDescription);
				if (httpWebResponse.StatusCode == HttpStatusCode.Unauthorized)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceError(0L, "[ProxyServiceTask::ProcessAsyncWebException] Proxy request failed with Unauthorized.");
					ProxyEventLogHelper.LogKerberosConfigurationProblem(this.proxiedToService);
					text = string.Format("{0}_{1}", httpWebResponse.StatusCode.ToString(), httpWebResponse.StatusDescription);
				}
			}
			else
			{
				if (webException.Status == WebExceptionStatus.RequestCanceled || webException.Status == WebExceptionStatus.Timeout)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug(0L, "[ProxyServiceTask::ProcessAsyncWebException] Web request timed out.");
				}
				else if (webException.Status == WebExceptionStatus.ConnectFailure)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceError<Uri>(0L, "[ProxyServiceTask::ProcessAsyncWebException] Failed to connect to destination CAS '{0}'", this.proxiedToService.Url);
					BadCASCache.Singleton.Add(this.proxiedToService.ServerFullyQualifiedDomainName, string.Empty);
				}
				else if (webException.Status == WebExceptionStatus.TrustFailure)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceError<string, bool>(0L, "[ProxyServiceTask::ProcessAsyncWebException] Could not establish trust relationship with destination CAS '{0}'.  AllowInternalUntrustedCerts is set to {1}.", this.proxiedToService.ServerFullyQualifiedDomainName, EWSSettings.AllowInternalUntrustedCerts);
					ProxyEventLogHelper.LogNoTrustedCertificateOnDestinationCAS(this.proxiedToService.ServerFullyQualifiedDomainName);
					BadCASCache.Singleton.Add(this.proxiedToService.ServerFullyQualifiedDomainName, string.Empty);
				}
				else
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug(0L, "[ProxyServiceTask::ProcessAsyncWebException] webException.Response was either null or not an HttpWebResponse.");
				}
				text = string.Format("{0}_{1}", webException.Status, webException.Message);
			}
			base.Request.HandleStaleCache();
			if (!string.IsNullOrEmpty(text))
			{
				this.LogProxyException(this.proxiedToService.ServerFullyQualifiedDomainName, caller + "_" + text);
				this.ResubmitTask(true);
				return true;
			}
			return false;
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x000BB237 File Offset: 0x000B9437
		private void LogProxyException(string fqdn, string exceptionMessage)
		{
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericError(base.CallContext.ProtocolLog, "ProxyException['" + fqdn + "']", exceptionMessage);
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x000BB460 File Offset: 0x000B9660
		public void ResponseCallback(IAsyncResult result)
		{
			this.ExecuteWithinCallContext("ResponseCallback", delegate(ref bool success)
			{
				TimeSpan timeSpan = ExDateTime.UtcNow - this.requestStart;
				try
				{
					HttpWebResponse response = this.webRequest.EndGetResponse(result) as HttpWebResponse;
					PerformanceMonitor.UpdateProxyResponseTimePerformanceCounter((long)timeSpan.TotalMilliseconds);
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<TimeSpan>((long)this.GetHashCode(), "[ProxyServiceTask.ResponseCallback] Response callback was called. Total proxy time was {0}", timeSpan);
					if (this.suggesterSid != null)
					{
						ProxySuggesterSidCache.Singleton.Add(this.suggesterSid, string.Empty);
					}
					this.ProcessResponseMessageAndCompleteIfNecessary(timeSpan, response, ProxyResult.Success);
					success = true;
				}
				catch (WebException ex)
				{
					if (!this.ProcessAsyncWebException(ex, "ResponseCallback"))
					{
						HttpWebResponse response2 = (HttpWebResponse)ex.Response;
						string empty = string.Empty;
						switch (this.GetSoapFaultType(ex, response2, out empty))
						{
						case ProxyServiceTask<T>.SoapFaultType.NotSoapFault:
							ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>(0L, "[ProxyServiceTask::ProxyRequestToSingleCAS] Web exception was not soap fault.  Failing this proxy '{0}' and adding this server to the bad CAS cache.", this.proxiedToService.ServerFullyQualifiedDomainName);
							BadCASCache.Singleton.Add(this.proxiedToService.ServerFullyQualifiedDomainName, string.Empty);
							this.LogProxyException(this.proxiedToService.ServerFullyQualifiedDomainName, "ResponseCallback_" + empty);
							this.ResubmitTask(true);
							break;
						case ProxyServiceTask<T>.SoapFaultType.Proxier:
							ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug(0L, "[ProxyServiceTask::ProxyRequestToSingleCAS] Proxy token has expired.  Must resend.");
							ProxySuggesterSidCache.Singleton.Remove(this.suggesterSid);
							this.LogProxyException(this.proxiedToService.ServerFullyQualifiedDomainName, "ResponseCallback_" + empty);
							this.ResubmitTask(false);
							break;
						case ProxyServiceTask<T>.SoapFaultType.Client:
							this.ProcessResponseMessageAndCompleteIfNecessary(timeSpan, response2, ProxyResult.SoapFault);
							break;
						}
					}
				}
			});
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x000BB498 File Offset: 0x000B9698
		private void ResubmitTask(bool useNextCAS)
		{
			try
			{
				lock (this.instanceLock)
				{
					this.completed = true;
				}
				WebServicesInfo[] array = null;
				if (useNextCAS)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<int>((long)this.GetHashCode(), "[ProxyServiceTask.ResubmitTask] Moving to next CAS for proxy attempt.  CAS Array length: {0}", this.services.Length - 1);
					if (this.services.Length <= 1)
					{
						ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug((long)this.GetHashCode(), "[ProxyServiceTask.ResubmitTask] Needed to move to next CAS for proxying, but there are no more in the list.");
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(base.CallContext.ProtocolLog, "ProxyServersAttempted", this.serversAttemptedForLog);
						base.CompleteWCFRequest(new NoRespondingCASInDestinationSiteException());
						return;
					}
					array = new WebServicesInfo[this.services.Length - 1];
					Array.Copy(this.services, 1, array, 0, array.Length);
				}
				else
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug((long)this.GetHashCode(), "[ProxyServiceTask.ResubmitTask] Resubmitting request to same CAS.");
					array = this.services;
				}
				lock (this.eventLock)
				{
					if (this.proxyDoneEvent != null)
					{
						this.proxyDoneEvent.Set();
					}
				}
				ProxyServiceTask<T> task = new ProxyServiceTask<T>(base.Request, base.CallContext, base.ServiceAsyncResult, array, this.serversAttemptedForLog);
				if (base.CallContext.HttpContext != null)
				{
					base.CallContext.HttpContext.Items["WlmQueueReSubmitTime"] = Stopwatch.GetTimestamp();
				}
				if (!base.CallContext.WorkloadManager.TrySubmitNewTask(task))
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceError((long)this.GetHashCode(), "[ProxyServiceTask.ResubmitTask] Failed to resubmit task to the workload manager.");
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericError(base.CallContext.ProtocolLog, "ProxyServiceTask_ResubmitTask", "ErrorServerBusy: Failed to resubmit task to the workload manager.");
					base.CompleteWCFRequest(new ServerBusyException());
				}
			}
			finally
			{
				this.Dispose();
			}
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x000BB6A8 File Offset: 0x000B98A8
		protected virtual void ProcessResponseMessageAndCompleteIfNecessary(TimeSpan elapsed, HttpWebResponse response, ProxyResult result)
		{
			lock (this.eventLock)
			{
				if (this.proxyDoneEvent != null)
				{
					this.proxyDoneEvent.Set();
				}
			}
			EWSSettings.ProxyResponse = response;
			this.SetProxyHopHeaders(this.proxiedToService);
			ExTraceGlobals.ThrottlingTracer.TraceDebug<ProxyResult, string>((long)this.GetHashCode(), "[ProxyServiceTask.SetResponseMessageAndComplete] Proxied response returned with result {0} for task '{1}'", result, base.Description);
			this.FinishRequest(string.Format("[C,P({0})]", this.proxiedToService.Url.ToString()), this.queueAndDelayTime, elapsed, null);
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x000BB750 File Offset: 0x000B9950
		private ProxyServiceTask<T>.SoapFaultType GetSoapFaultType(WebException webException, HttpWebResponse response, out string exceptionMessage)
		{
			exceptionMessage = string.Format("{0}_{1}", response.StatusCode.ToString(), response.StatusDescription);
			if (response.StatusCode != HttpStatusCode.InternalServerError)
			{
				return ProxyServiceTask<T>.SoapFaultType.NotSoapFault;
			}
			Stream responseStream = response.GetResponseStream();
			if (!responseStream.CanSeek)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceError<string, string>((long)this.GetHashCode(), "[ProxyServiceTask::GetSoapFaultType] Web Response stream is non-seekable.  Returning SoapFaultType.Client instead.  ExceptionType: {0}, ExceptionMessage: {1}", webException.GetType().FullName, webException.Message);
				return ProxyServiceTask<T>.SoapFaultType.Client;
			}
			long position = responseStream.Position;
			ProxyServiceTask<T>.SoapFaultType result;
			try
			{
				StreamReader streamReader = new StreamReader(responseStream);
				char[] array = new char["<?xml version=\"1.0\"".Length];
				int num = streamReader.Read(array, 0, "<?xml version=\"1.0\"".Length);
				if (num < "<?xml version=\"1.0\"".Length)
				{
					result = ProxyServiceTask<T>.SoapFaultType.NotSoapFault;
				}
				else
				{
					string strA = new string(array, 0, array.Length);
					if (string.Compare(strA, "<?xml version=\"1.0\"", StringComparison.CurrentCultureIgnoreCase) != 0)
					{
						result = ProxyServiceTask<T>.SoapFaultType.NotSoapFault;
					}
					else
					{
						responseStream.Position = position;
						using (XmlReader xmlReader = SafeXmlFactory.CreateSafeXmlReader(responseStream))
						{
							while (xmlReader.Read())
							{
								if (xmlReader.LocalName == "Subcode" && (xmlReader.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/" || xmlReader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope"))
								{
									xmlReader.Read();
									string text = xmlReader.ReadElementContentAsString();
									string[] array2 = text.Split(new char[]
									{
										':'
									});
									text = array2[array2.Length - 1];
									exceptionMessage = text;
									return this.DetermineFaultResponseCodeType(text);
								}
								if (xmlReader.LocalName == "ResponseCode" && xmlReader.NamespaceURI == "http://schemas.microsoft.com/exchange/services/2006/errors")
								{
									string text2 = xmlReader.ReadElementContentAsString();
									exceptionMessage = text2;
									return this.DetermineFaultResponseCodeType(text2);
								}
							}
						}
						result = ProxyServiceTask<T>.SoapFaultType.NotSoapFault;
					}
				}
			}
			finally
			{
				responseStream.Position = position;
			}
			return result;
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x000BB960 File Offset: 0x000B9B60
		private ProxyServiceTask<T>.SoapFaultType DetermineFaultResponseCodeType(string responseCodeString)
		{
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>((long)this.GetHashCode(), "[ProxyServiceTask.DetermineFaultResponseCodeType] Encountered response code {0} from destination CAS.", responseCodeString);
			if (responseCodeString == ResponseCodeType.ErrorProxyTokenExpired.ToString())
			{
				return ProxyServiceTask<T>.SoapFaultType.Proxier;
			}
			return ProxyServiceTask<T>.SoapFaultType.Client;
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x000BB994 File Offset: 0x000B9B94
		protected void SetProxyHopHeaders(WebServicesInfo casInstance)
		{
			if (Global.WriteProxyHopHeaders)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(4);
				dictionary.Add("InitialProxyCAS", this.TruncateHeader(LocalServer.GetServer().Id.DistinguishedName));
				dictionary.Add("InitialProxySite", this.TruncateHeader(LocalServer.GetServer().ServerSite.DistinguishedName));
				if (!string.IsNullOrEmpty(casInstance.ServerDistinguishedName))
				{
					dictionary.Add("FinalProxyCAS", this.TruncateHeader(casInstance.ServerDistinguishedName));
				}
				if (!string.IsNullOrEmpty(casInstance.SiteDistinguishedName))
				{
					dictionary.Add("FinalProxySite", this.TruncateHeader(casInstance.SiteDistinguishedName));
				}
				EWSSettings.ProxyHopHeaders = dictionary;
			}
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000BBA40 File Offset: 0x000B9C40
		private string TruncateHeader(string inHeader)
		{
			if (inHeader.Length > ProxyServiceTask<T>.MaxHeaderLength)
			{
				return inHeader.Substring(0, ProxyServiceTask<T>.MaxHeaderLength) + "...";
			}
			return inHeader;
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000BBA68 File Offset: 0x000B9C68
		private SuggesterSidCompositeKey GetProxyHeaderValue(WindowsIdentity windowsIdentity, WebServicesInfo service, ref ProxyHeaderValue proxyHeaderValue)
		{
			SuggesterSidCompositeKey suggesterSidCompositeKey = new SuggesterSidCompositeKey(windowsIdentity.User, service.ServerFullyQualifiedDomainName);
			try
			{
				if (ProxySuggesterSidCache.Singleton.Contains(suggesterSidCompositeKey) || proxyHeaderValue == null || proxyHeaderValue.ProxyHeaderType == ProxyHeaderType.SuggesterSid)
				{
					using (ClientSecurityContext clientSecurityContext = new ClientSecurityContext(windowsIdentity))
					{
						proxyHeaderValue = ProxySecurityContextEncoder.GetHeaderValueForCAS(clientSecurityContext, service, base.CallContext.GetEffectiveAccessingSmtpAddress());
					}
				}
			}
			catch (InvalidProxySecurityContextException exception)
			{
				throw FaultExceptionUtilities.CreateFault(exception, FaultParty.Sender);
			}
			return suggesterSidCompositeKey;
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000BBAF0 File Offset: 0x000B9CF0
		private SuggesterSidCompositeKey GetProxyHeaderValue(ClientSecurityContextIdentity cscIdentity, WebServicesInfo service, ref ProxyHeaderValue proxyHeaderValue)
		{
			SuggesterSidCompositeKey suggesterSidCompositeKey = new SuggesterSidCompositeKey(cscIdentity.Sid, service.ServerFullyQualifiedDomainName);
			try
			{
				if (ProxySuggesterSidCache.Singleton.Contains(suggesterSidCompositeKey) || proxyHeaderValue == null || proxyHeaderValue.ProxyHeaderType == ProxyHeaderType.SuggesterSid)
				{
					using (ClientSecurityContext clientSecurityContext = cscIdentity.CreateClientSecurityContext())
					{
						proxyHeaderValue = ProxySecurityContextEncoder.GetHeaderValueForCAS(clientSecurityContext, service, base.CallContext.GetEffectiveAccessingSmtpAddress());
					}
				}
			}
			catch (AuthzException innerException)
			{
				throw FaultExceptionUtilities.CreateFault(new InternalServerErrorException(innerException), FaultParty.Receiver);
			}
			catch (InvalidProxySecurityContextException exception)
			{
				throw FaultExceptionUtilities.CreateFault(exception, FaultParty.Sender);
			}
			return suggesterSidCompositeKey;
		}

		// Token: 0x04001D53 RID: 7507
		private const string XmlDeclaration = "<?xml version=\"1.0\"";

		// Token: 0x04001D54 RID: 7508
		private const string HTTPHeaderAccept = "ACCEPT";

		// Token: 0x04001D55 RID: 7509
		private const string HTTPHeaderAcceptEncoding = "ACCEPT-ENCODING";

		// Token: 0x04001D56 RID: 7510
		private const string HTTPHeaderConnection = "CONNECTION";

		// Token: 0x04001D57 RID: 7511
		private const string HTTPHeaderTransferEncoding = "TRANSFER-ENCODING";

		// Token: 0x04001D58 RID: 7512
		private const string HTTPHeaderXGccProxyInfo = "X-GCC-PROXYINFO";

		// Token: 0x04001D59 RID: 7513
		private const string WlmQueueReSubmitKey = "WlmQueueReSubmitTime";

		// Token: 0x04001D5A RID: 7514
		private const string SpnPrefixForHttp = "HTTP/";

		// Token: 0x04001D5B RID: 7515
		private const string PrefixForKerbAuthBlob = "Negotiate ";

		// Token: 0x04001D5C RID: 7516
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04001D5D RID: 7517
		private static readonly int MaxHeaderLength = 256;

		// Token: 0x04001D5E RID: 7518
		private readonly string serversAttemptedForLog;

		// Token: 0x04001D5F RID: 7519
		private AuthenticationContext authenticationContext;

		// Token: 0x04001D60 RID: 7520
		private static readonly FileVersionInfo VersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

		// Token: 0x04001D61 RID: 7521
		private static readonly string userAgentPrefix = "ExchangeWebServicesProxy/CrossSite/EXCH/" + ProxyServiceTask<T>.VersionInfo.FileVersion + "/";

		// Token: 0x04001D62 RID: 7522
		private static LazyMember<List<string>> specialHeaders = new LazyMember<List<string>>(() => new List<string>
		{
			"AUTHORIZATION",
			"ACCEPT",
			"ACCEPT-ENCODING",
			"CONNECTION",
			"CONTENT-LENGTH",
			"CONTENT-TYPE",
			"EXPECT",
			"DATE",
			"HOST",
			"IF-MODIFIED-SINCE",
			"MSEXCHPROXYURI",
			"PROXY-CONNECTION",
			"RANGE",
			"REFERER",
			"TRANSFER-ENCODING",
			"USER-AGENT",
			"X-COMMONACCESSTOKEN",
			"X-GCC-PROXYINFO",
			"X-ISFROMCAFE"
		});

		// Token: 0x04001D63 RID: 7523
		private WebServicesInfo[] services;

		// Token: 0x04001D64 RID: 7524
		private ExDateTime requestStart;

		// Token: 0x04001D65 RID: 7525
		private SuggesterSidCompositeKey suggesterSid;

		// Token: 0x04001D66 RID: 7526
		private ProxyHeaderValue proxyHeaderValue;

		// Token: 0x04001D67 RID: 7527
		private byte[] requestBuffer;

		// Token: 0x04001D68 RID: 7528
		private bool disposed;

		// Token: 0x04001D69 RID: 7529
		private RegisteredWaitHandle registeredWaitHandle;

		// Token: 0x04001D6A RID: 7530
		private object instanceLock = new object();

		// Token: 0x04001D6B RID: 7531
		private bool completed;

		// Token: 0x04001D6C RID: 7532
		private HttpWebRequest webRequest;

		// Token: 0x04001D6D RID: 7533
		protected ManualResetEvent proxyDoneEvent = new ManualResetEvent(false);

		// Token: 0x04001D6E RID: 7534
		protected object eventLock = new object();

		// Token: 0x04001D6F RID: 7535
		protected WebServicesInfo proxiedToService;

		// Token: 0x04001D70 RID: 7536
		protected TimeSpan queueAndDelayTime;

		// Token: 0x04001D71 RID: 7537
		protected IBudget budget;

		// Token: 0x0200069B RID: 1691
		// (Invoke) Token: 0x06003417 RID: 13335
		private delegate void ExecuteWithinCallContextDelegate(ref bool success);

		// Token: 0x0200069C RID: 1692
		internal enum SoapFaultType
		{
			// Token: 0x04001D76 RID: 7542
			NotSoapFault,
			// Token: 0x04001D77 RID: 7543
			Proxier,
			// Token: 0x04001D78 RID: 7544
			Client
		}
	}
}
