using System;
using System.IO;
using System.Net;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.RightsManagement.SOAP.Publish;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000A0F RID: 2575
	internal sealed class PublishWSManager : IDisposeTrackable, IDisposable
	{
		// Token: 0x06003822 RID: 14370 RVA: 0x0008D994 File Offset: 0x0008BB94
		public PublishWSManager(Uri url, IWSManagerPerfCounters perfcounters, IRmsLatencyTracker latencyTracker, WebProxy proxy, TimeSpan timeout)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			this.wsProxy = new PublishWS();
			this.wsProxy.Url = RMUtil.ConvertUriToPublishUrl(url);
			if (this.wsProxy.Url == null)
			{
				throw new InvalidRmsUrlException(url.OriginalString);
			}
			if (proxy != null)
			{
				this.wsProxy.Proxy = proxy;
			}
			this.wsProxy.Timeout = (int)timeout.TotalMilliseconds;
			this.wsProxy.Credentials = CredentialCache.DefaultCredentials;
			this.wsProxy.VersionDataValue = new VersionData();
			this.wsProxy.VersionDataValue.MaximumVersion = "1.1.0.0";
			this.wsProxy.VersionDataValue.MinimumVersion = "1.1.0.0";
			this.disposeTracker = this.GetDisposeTracker();
			this.perfcounters = (perfcounters ?? NoopWSManagerPerfCounters.Instance);
			this.rmsLatencyTracker = (latencyTracker ?? NoopRmsLatencyTracker.Instance);
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x0008DA8A File Offset: 0x0008BC8A
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.wsProxy != null)
			{
				this.wsProxy.Dispose();
				this.wsProxy = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x0008DAC6 File Offset: 0x0008BCC6
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PublishWSManager>(this);
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x0008DACE File Offset: 0x0008BCCE
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x0008DAE4 File Offset: 0x0008BCE4
		internal IAsyncResult BeginAcquireClc(XmlNode[] rac, AsyncCallback callback, object state)
		{
			if (rac == null)
			{
				throw new ArgumentNullException("rac");
			}
			GetClientLicensorCertParams[] requestParams = new GetClientLicensorCertParams[]
			{
				new GetClientLicensorCertParams
				{
					PersonaCerts = rac
				}
			};
			ExTraceGlobals.RightsManagementTracer.TraceDebug<string>((long)this.GetHashCode(), "BeginAcquireClc: Fetching CLC async from {0}", this.wsProxy.Url);
			this.rmsLatencyTracker.BeginTrackRmsLatency(RmsOperationType.AcquireClc);
			return this.wsProxy.BeginGetClientLicensorCert(requestParams, callback, state);
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x0008DB54 File Offset: 0x0008BD54
		internal XmlNode[] EndAcquireClc(IAsyncResult asyncResult)
		{
			ExTraceGlobals.RightsManagementTracer.TraceDebug<string>((long)this.GetHashCode(), "EndAcquireClc invoked. Uri {0}", this.wsProxy.Url);
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			GetClientLicensorCertResponse[] responses = null;
			Exception ex = null;
			try
			{
				responses = this.wsProxy.EndGetClientLicensorCert(asyncResult);
			}
			catch (SoapException ex2)
			{
				ex = ex2;
			}
			catch (WebException ex3)
			{
				ex = ex3;
			}
			catch (InvalidOperationException ex4)
			{
				ex = ex4;
			}
			catch (IOException ex5)
			{
				ex = ex5;
			}
			catch (UnauthorizedAccessException ex6)
			{
				ex = ex6;
			}
			this.rmsLatencyTracker.EndTrackRmsLatency(RmsOperationType.AcquireClc);
			if (ex != null)
			{
				this.perfcounters.GetClientLicensorCertFailed();
				ExTraceGlobals.RightsManagementTracer.TraceError<string, Exception>((long)this.GetHashCode(), "Failed to acquire the CLC from {0}. Error: {1}", this.wsProxy.Url, ex);
				throw new RightsManagementException(RightsManagementFailureCode.ClcAcquisitionFailed, DrmStrings.FailedToAcquireClc(this.wsProxy.Url), ex, this.wsProxy.Url);
			}
			return this.GetClcOrExceptionFromResponse(responses);
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x0008DC6C File Offset: 0x0008BE6C
		private XmlNode[] GetClcOrExceptionFromResponse(GetClientLicensorCertResponse[] responses)
		{
			if (responses != null && responses.Length == 1 && responses[0] != null && RMUtil.IsValidCertificateChain(responses[0].CertificateChain))
			{
				this.perfcounters.GetClientLicensorCertSuccessful(this.wsProxy.GetElapsedMilliseconds());
				ExTraceGlobals.RightsManagementTracer.TraceDebug<string>((long)this.GetHashCode(), "Succesfully acquired the CLC from {0}", this.wsProxy.Url);
				return responses[0].CertificateChain;
			}
			this.perfcounters.GetClientLicensorCertFailed();
			ExTraceGlobals.RightsManagementTracer.TraceError<string>((long)this.GetHashCode(), "Invalid or empty response for the call to acquire CLC from {0} ", this.wsProxy.Url);
			throw new RightsManagementException(RightsManagementFailureCode.ClcAcquisitionFailed, DrmStrings.InvalidResponseToClcRequest(this.wsProxy.Url));
		}

		// Token: 0x04002F76 RID: 12150
		private PublishWS wsProxy;

		// Token: 0x04002F77 RID: 12151
		private DisposeTracker disposeTracker;

		// Token: 0x04002F78 RID: 12152
		private IWSManagerPerfCounters perfcounters;

		// Token: 0x04002F79 RID: 12153
		private IRmsLatencyTracker rmsLatencyTracker;
	}
}
