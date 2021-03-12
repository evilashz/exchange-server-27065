using System;
using System.ServiceModel;
using System.Xml;
using Microsoft.com.IPC.WSService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000A10 RID: 2576
	internal sealed class ServerCertificationWCFManager : IDisposeTrackable, IDisposable
	{
		// Token: 0x06003829 RID: 14377 RVA: 0x0008DD1C File Offset: 0x0008BF1C
		internal ServerCertificationWCFManager(ChannelFactory<IWSCertificationServiceChannel> channelFactory, Uri targetUri, IWSManagerPerfCounters perfcounters, IRmsLatencyTracker latencyTracker)
		{
			this.channelFactory = channelFactory;
			this.channel = channelFactory.CreateChannel();
			this.targetUri = targetUri;
			this.perfcounters = (perfcounters ?? NoopWSManagerPerfCounters.Instance);
			this.rmsLatencyTracker = (latencyTracker ?? NoopRmsLatencyTracker.Instance);
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x0008DD6C File Offset: 0x0008BF6C
		public void Dispose()
		{
			if (this.channel != null)
			{
				if (this.channel.State == CommunicationState.Faulted)
				{
					this.channel.Abort();
				}
				else
				{
					try
					{
						this.channel.Close();
					}
					catch (TimeoutException)
					{
					}
				}
				this.channel = null;
			}
			if (this.channelFactory != null)
			{
				if (this.channelFactory.State == CommunicationState.Faulted)
				{
					this.channelFactory.Abort();
				}
				else
				{
					try
					{
						this.channelFactory.Close();
					}
					catch (TimeoutException)
					{
					}
				}
				this.channelFactory = null;
			}
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x0008DE2C File Offset: 0x0008C02C
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ServerCertificationWCFManager>(this);
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x0008DE34 File Offset: 0x0008C034
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x0008DE88 File Offset: 0x0008C088
		internal IAsyncResult BeginAcquireRac(XmlNode[] machineCertificateChain, AsyncCallback callback, object state)
		{
			if (machineCertificateChain == null)
			{
				throw new ArgumentNullException("machineCertificateChain");
			}
			VersionData versionData = new VersionData();
			versionData.MaximumVersion = "1.0.0.0";
			versionData.MinimumVersion = "1.0.0.0";
			XrmlCertificateChain machineCertChain = new XrmlCertificateChain();
			machineCertChain.CertificateChain = DrmClientUtils.ConvertXmlNodeArrayToStringArray(machineCertificateChain);
			IAsyncResult result = null;
			RightsManagementException ex = DrmClientUtils.RunWCFOperation(delegate
			{
				result = this.channel.BeginCertify(new CertifyRequestMessage(versionData, machineCertChain), callback, state);
			}, this.channelFactory.Endpoint.Address.Uri, this.targetUri);
			if (ex == null)
			{
				return result;
			}
			ExTraceGlobals.RightsManagementTracer.TraceError<Uri, Exception>(0L, "Hit an exception when talking to the external RMS server pipeline {0} for certify request. Error {1}", this.channelFactory.Endpoint.Address.Uri, ex);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(null, state, callback);
			lazyAsyncResult.InvokeCallback(ex);
			return lazyAsyncResult;
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x0008DFAC File Offset: 0x0008C1AC
		internal XmlNode[] EndAcquireRac(IAsyncResult asyncResult)
		{
			ExTraceGlobals.RightsManagementTracer.TraceDebug<Uri>((long)this.GetHashCode(), "EndAcquireRac invoked. Uri {0}", this.channelFactory.Endpoint.Address.Uri);
			if (asyncResult == null)
			{
				throw new InvalidOperationException("asyncResult cannot be null here.");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult != null)
			{
				RightsManagementException ex = lazyAsyncResult.Result as RightsManagementException;
				if (ex == null)
				{
					throw new InvalidOperationException("exception cannot be null here and has to be of type RightsManagementException.");
				}
				this.perfcounters.WCFCertifyFailed();
				ExTraceGlobals.RightsManagementTracer.TraceError<RightsManagementException>(0L, "EndAcquireRac hit an exception {0}", ex);
				throw ex;
			}
			else
			{
				CertifyResponseMessage response = null;
				RightsManagementException ex2 = DrmClientUtils.RunWCFOperation(delegate
				{
					response = this.channel.EndCertify(asyncResult);
				}, this.channelFactory.Endpoint.Address.Uri, this.targetUri);
				this.rmsLatencyTracker.EndTrackRmsLatency(RmsOperationType.AcquireB2BRac);
				if (ex2 != null)
				{
					this.perfcounters.WCFCertifyFailed();
					ExTraceGlobals.RightsManagementTracer.TraceError<Uri, RightsManagementException>((long)this.GetHashCode(), "Failed to acquire the RAC from {0}. Error: {1}", this.channelFactory.Endpoint.Address.Uri, ex2);
					throw ex2;
				}
				XmlNode[] array = null;
				if (response != null && response.GroupIdentityCredential != null)
				{
					RMUtil.TryConvertCertChainStringArrayToXmlNodeArray(response.GroupIdentityCredential.CertificateChain, out array);
				}
				if (!RMUtil.IsValidCertificateChain(array))
				{
					this.perfcounters.WCFCertifyFailed();
					ExTraceGlobals.RightsManagementTracer.TraceError<Uri>((long)this.GetHashCode(), "Invalid response for the certification request from {0}. Cert chain is not valid", this.channelFactory.Endpoint.Address.Uri);
					throw new RightsManagementException(RightsManagementFailureCode.InvalidRightsAccountCertificate, DrmStrings.InvalidResponseToCertificationRequest(this.channelFactory.Endpoint.Address.Uri.AbsoluteUri));
				}
				this.perfcounters.WCFCertifySuccessful();
				ExTraceGlobals.RightsManagementTracer.TraceDebug<Uri>((long)this.GetHashCode(), "Successfully parsed the license responses from {0}", this.channelFactory.Endpoint.Address.Uri);
				return array;
			}
		}

		// Token: 0x04002F7A RID: 12154
		private DisposeTracker disposeTracker;

		// Token: 0x04002F7B RID: 12155
		private IWSManagerPerfCounters perfcounters;

		// Token: 0x04002F7C RID: 12156
		private IRmsLatencyTracker rmsLatencyTracker;

		// Token: 0x04002F7D RID: 12157
		private ChannelFactory<IWSCertificationServiceChannel> channelFactory;

		// Token: 0x04002F7E RID: 12158
		private IWSCertificationServiceChannel channel;

		// Token: 0x04002F7F RID: 12159
		private Uri targetUri;
	}
}
