using System;
using System.IO;
using System.Net;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.RightsManagement.SOAP.License;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009C8 RID: 2504
	internal sealed class LicenseWSManager : IDisposeTrackable, IDisposable
	{
		// Token: 0x06003696 RID: 13974 RVA: 0x0008B2D8 File Offset: 0x000894D8
		public LicenseWSManager(Uri url, IWSManagerPerfCounters perfcounters, IRmsLatencyTracker latencyTracker, WebProxy proxy, TimeSpan timeout)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			this.wsProxy = new LicenseWS();
			this.wsProxy.Url = RMUtil.ConvertUriToLicenseUrl(url);
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

		// Token: 0x06003697 RID: 13975 RVA: 0x0008B3CE File Offset: 0x000895CE
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

		// Token: 0x06003698 RID: 13976 RVA: 0x0008B40A File Offset: 0x0008960A
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<LicenseWSManager>(this);
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x0008B412 File Offset: 0x00089612
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x0008B428 File Offset: 0x00089628
		public IAsyncResult BeginAcquireUseLicense(XmlNode[] rac, XmlNode[] issuanceLicense, AsyncCallback callback, object state)
		{
			if (rac == null)
			{
				throw new ArgumentNullException("rac");
			}
			if (issuanceLicense == null)
			{
				throw new ArgumentNullException("issuanceLicense");
			}
			AcquireLicenseParams[] requestParams = new AcquireLicenseParams[]
			{
				new AcquireLicenseParams
				{
					LicenseeCerts = rac,
					IssuanceLicense = issuanceLicense
				}
			};
			ExTraceGlobals.RightsManagementTracer.TraceDebug<string>((long)this.GetHashCode(), "BeginAcquireUseLicense: Fetching the use license async from {0}", this.wsProxy.Url);
			this.rmsLatencyTracker.BeginTrackRmsLatency(RmsOperationType.AcquireLicense);
			return this.wsProxy.BeginAcquireLicense(requestParams, callback, state);
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x0008B4B0 File Offset: 0x000896B0
		public string EndAcquireUseLicense(IAsyncResult asyncResult)
		{
			ExTraceGlobals.RightsManagementTracer.TraceDebug<string>((long)this.GetHashCode(), "EndAcquireUseLicense invoked. Uri {0}", this.wsProxy.Url);
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			AcquireLicenseResponse[] responses = null;
			Exception ex = null;
			try
			{
				responses = this.wsProxy.EndAcquireLicense(asyncResult);
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
			this.rmsLatencyTracker.EndTrackRmsLatency(RmsOperationType.AcquireLicense);
			if (ex != null)
			{
				this.perfcounters.AcquireLicenseFailed();
				ExTraceGlobals.RightsManagementTracer.TraceError<string, Exception>((long)this.GetHashCode(), "Failed to acquire the use license from {0}. Error: {1}", this.wsProxy.Url, ex);
				throw new RightsManagementException(RightsManagementFailureCode.UseLicenseAcquisitionFailed, DrmStrings.FailedToAcquireUseLicense(this.wsProxy.Url), ex, this.wsProxy.Url);
			}
			return this.GetUseLicenseOrExceptionFromResponse(responses);
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x0008B5C8 File Offset: 0x000897C8
		private static LocalizedString GetExceptionString(XmlNode node)
		{
			XmlNode xmlNode = node.SelectSingleNode("descendant::ExceptionString");
			if (xmlNode == null || string.IsNullOrEmpty(xmlNode.InnerText))
			{
				return DrmStrings.RmExceptionGenericMessage;
			}
			return new LocalizedString(xmlNode.InnerText);
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x0008B604 File Offset: 0x00089804
		private string GetUseLicenseOrExceptionFromResponse(AcquireLicenseResponse[] responses)
		{
			if (responses == null || responses.Length <= 0 || responses[0] == null || !RMUtil.IsValidCertificateChain(responses[0].CertificateChain))
			{
				this.perfcounters.AcquireLicenseFailed();
				ExTraceGlobals.RightsManagementTracer.TraceError<string>((long)this.GetHashCode(), "Invalid or empty response for the call to acquire use license from {0} ", this.wsProxy.Url);
				throw new RightsManagementException(RightsManagementFailureCode.UseLicenseAcquisitionFailed, DrmStrings.InvalidUseLicenseResponse(this.wsProxy.Url));
			}
			XmlNode xmlNode = responses[0].CertificateChain[0];
			if (string.Equals(xmlNode.Name, "XrML", StringComparison.OrdinalIgnoreCase))
			{
				this.perfcounters.AcquireLicenseSuccessful(this.wsProxy.GetElapsedMilliseconds());
				ExTraceGlobals.RightsManagementTracer.TraceDebug<string>((long)this.GetHashCode(), "Succesfully acquired the use license from {0} ", this.wsProxy.Url);
				return RMUtil.ConvertXmlNodeArrayToString(responses[0].CertificateChain);
			}
			if (string.Equals(xmlNode.Name, "AcquireLicenseException", StringComparison.OrdinalIgnoreCase))
			{
				this.perfcounters.AcquireLicenseFailed();
				LocalizedString exceptionString = LicenseWSManager.GetExceptionString(xmlNode);
				ExTraceGlobals.RightsManagementTracer.TraceError<string, LocalizedString>((long)this.GetHashCode(), "Hit an error when acquiring use license from {0}. Error {1}", this.wsProxy.Url, exceptionString);
				throw new RightsManagementException(RightsManagementFailureCode.UseLicenseAcquisitionFailed, exceptionString, this.wsProxy.Url);
			}
			this.perfcounters.AcquireLicenseFailed();
			ExTraceGlobals.RightsManagementTracer.TraceError<string>((long)this.GetHashCode(), "Invalid response when acquiring use license from {0}. Neither the license or the exception node is set", this.wsProxy.Url);
			throw new RightsManagementException(RightsManagementFailureCode.UseLicenseAcquisitionFailed, DrmStrings.RmExceptionGenericMessage, this.wsProxy.Url);
		}

		// Token: 0x04002EC0 RID: 11968
		private const string EulElementName = "XrML";

		// Token: 0x04002EC1 RID: 11969
		private const string ExceptionElementName = "AcquireLicenseException";

		// Token: 0x04002EC2 RID: 11970
		private const string ExceptionXPath = "descendant::ExceptionString";

		// Token: 0x04002EC3 RID: 11971
		private LicenseWS wsProxy;

		// Token: 0x04002EC4 RID: 11972
		private DisposeTracker disposeTracker;

		// Token: 0x04002EC5 RID: 11973
		private IWSManagerPerfCounters perfcounters;

		// Token: 0x04002EC6 RID: 11974
		private IRmsLatencyTracker rmsLatencyTracker;
	}
}
