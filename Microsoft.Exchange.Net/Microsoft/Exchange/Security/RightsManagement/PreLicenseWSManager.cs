using System;
using System.IO;
using System.Net;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.RightsManagement.SOAP.License;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009C9 RID: 2505
	internal sealed class PreLicenseWSManager : LicenseManager
	{
		// Token: 0x0600369E RID: 13982 RVA: 0x0008B788 File Offset: 0x00089988
		internal PreLicenseWSManager(Uri url, IWSManagerPerfCounters perfcounters, IRmsLatencyTracker latencyTracker, WebProxy proxy, TimeSpan timeout) : base(perfcounters, latencyTracker)
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
			this.baseTimeOutInMilliseconds = (int)timeout.TotalMilliseconds;
			this.wsProxy.Credentials = CredentialCache.DefaultCredentials;
			this.wsProxy.VersionDataValue = new VersionData();
			this.wsProxy.VersionDataValue.MaximumVersion = "1.1.0.0";
			this.wsProxy.VersionDataValue.MinimumVersion = "1.1.0.0";
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x0008B84F File Offset: 0x00089A4F
		public override void Dispose()
		{
			if (this.wsProxy != null)
			{
				this.wsProxy.Dispose();
				this.wsProxy = null;
			}
			base.Dispose();
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x0008B874 File Offset: 0x00089A74
		protected override void IssueNewWebRequest(LazyAsyncResult asyncResult)
		{
			LicenseManager.LicenseState licenseState = asyncResult.AsyncObject as LicenseManager.LicenseState;
			ExTraceGlobals.RightsManagementTracer.TraceDebug<string>((long)this.GetHashCode(), "IssueNewWebRequest for the next batch from {0}", this.wsProxy.Url);
			LicenseIdentity[] identitiesForNextBatch = licenseState.GetIdentitiesForNextBatch();
			if (identitiesForNextBatch != null)
			{
				AcquirePreLicenseParams acquirePreLicenseParams = new AcquirePreLicenseParams();
				acquirePreLicenseParams.IssuanceLicense = licenseState.IssuanceLicense;
				acquirePreLicenseParams.LicenseeIdentities = new string[identitiesForNextBatch.Length];
				for (int i = 0; i < identitiesForNextBatch.Length; i++)
				{
					acquirePreLicenseParams.LicenseeIdentities[i] = identitiesForNextBatch[i].Email;
				}
				AcquirePreLicenseParams[] requestParams = new AcquirePreLicenseParams[]
				{
					acquirePreLicenseParams
				};
				this.wsProxy.Timeout = this.baseTimeOutInMilliseconds + licenseState.GetCurrentBatchLength() * 1000;
				this.rmsLatencyTracker.BeginTrackRmsLatency(RmsOperationType.AcquirePrelicense);
				this.wsProxy.BeginAcquirePreLicense(requestParams, WsAsyncProxyWrapper.WrapCallbackWithUnhandledExceptionHandlerAndCrash(new AsyncCallback(this.AcquireLicenseCallback)), asyncResult);
				return;
			}
			if (licenseState.Exception != null)
			{
				this.perfcounters.AcquirePreLicenseFailed();
			}
			else
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug((long)this.GetHashCode(), "Successfully processed requesting all the identities");
			}
			asyncResult.InvokeCallback(licenseState.Exception);
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x0008B990 File Offset: 0x00089B90
		protected override void AcquireLicenseCallback(IAsyncResult asyncResult)
		{
			ExTraceGlobals.RightsManagementTracer.TraceDebug<string>((long)this.GetHashCode(), "AcquirePreLicenseCallback invoked. Uri {0}", this.wsProxy.Url);
			if (asyncResult == null)
			{
				throw new InvalidOperationException("asyncResult cannot be null here.");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult.AsyncState as LazyAsyncResult;
			if (lazyAsyncResult == null)
			{
				throw new InvalidOperationException("asyncResult.AsyncState cannot be null and has to be of type LazyAsyncResult.");
			}
			LicenseManager.LicenseState licenseState = lazyAsyncResult.AsyncObject as LicenseManager.LicenseState;
			if (licenseState == null)
			{
				throw new InvalidOperationException("result.AsyncObject cannot be null and has to be of type LicenseState.");
			}
			AcquirePreLicenseResponse[] responses = null;
			Exception ex = null;
			try
			{
				responses = this.wsProxy.EndAcquirePreLicense(asyncResult);
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
			this.rmsLatencyTracker.EndTrackRmsLatency(RmsOperationType.AcquirePrelicense);
			if (ex != null)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<string, Exception>((long)this.GetHashCode(), "Failed to acquire prelicenses from {0}. Error: {1}", this.wsProxy.Url, ex);
				licenseState.Exception = new RightsManagementException(RightsManagementFailureCode.PreLicenseAcquisitionFailed, DrmStrings.FailedToAcquirePreLicense(this.wsProxy.Url), ex, this.wsProxy.Url);
			}
			else
			{
				this.ParseResponses(responses, licenseState);
			}
			this.IssueNewWebRequest(lazyAsyncResult);
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x0008BAE4 File Offset: 0x00089CE4
		private void ParseResponses(AcquirePreLicenseResponse[] responses, LicenseManager.LicenseState licenseState)
		{
			int currentBatchLength = licenseState.GetCurrentBatchLength();
			if (responses == null || responses.Length != 1 || responses[0].Licenses == null || responses[0].Licenses.Length != currentBatchLength)
			{
				this.perfcounters.AcquirePreLicenseFailed();
				ExTraceGlobals.RightsManagementTracer.TraceError((long)this.GetHashCode(), "Invalid response for the prelicense request from {0}. Length of the response {1}, Number of licenses {2}.  Expected: {3}", new object[]
				{
					this.wsProxy.Url,
					(responses != null) ? responses.Length : 0,
					(responses != null && responses.Length == 1 && responses[0].Licenses != null) ? responses[0].Licenses.Length : 0,
					currentBatchLength
				});
				licenseState.SetFailureForCurrentBatch(new RightsManagementException(RightsManagementFailureCode.PreLicenseAcquisitionFailed, DrmStrings.FailedToAcquirePreLicense(this.wsProxy.Url))
				{
					IsPermanent = false
				});
				return;
			}
			XmlNode[] certificateChain = responses[0].CertificateChain;
			if (!RMUtil.IsValidCertificateChain(certificateChain))
			{
				this.perfcounters.AcquirePreLicenseFailed();
				ExTraceGlobals.RightsManagementTracer.TraceError<string>((long)this.GetHashCode(), "Invalid response for the prelicense request from {0}. Cert chain is not valid", this.wsProxy.Url);
				RightsManagementException failureForCurrentBatch = new RightsManagementException(RightsManagementFailureCode.InvalidCertificateChain, DrmStrings.InvalidResponseToPrelicensingRequest(this.wsProxy.Url));
				licenseState.SetFailureForCurrentBatch(failureForCurrentBatch);
				return;
			}
			for (int i = 0; i < currentBatchLength; i++)
			{
				XmlNode xmlNode = responses[0].Licenses[i];
				RightsManagementFailureCode failureCodeFromXml = RightsManagementException.GetFailureCodeFromXml(xmlNode);
				if (failureCodeFromXml == RightsManagementFailureCode.Success)
				{
					licenseState.SetResponseForCurrentBatchElement(i, certificateChain, xmlNode);
				}
				else
				{
					licenseState.SetFailureForCurrentBatchElement(i, new RightsManagementException(failureCodeFromXml, (xmlNode != null) ? new LocalizedString(xmlNode.InnerText) : LocalizedString.Empty));
				}
			}
			ExTraceGlobals.RightsManagementTracer.TraceDebug<string>((long)this.GetHashCode(), "Successfully parsed the prelicense responses from {0}", this.wsProxy.Url);
			this.perfcounters.AcquirePreLicenseSuccessful(this.wsProxy.GetElapsedMilliseconds());
		}

		// Token: 0x04002EC7 RID: 11975
		private LicenseWS wsProxy;

		// Token: 0x04002EC8 RID: 11976
		private int baseTimeOutInMilliseconds;
	}
}
