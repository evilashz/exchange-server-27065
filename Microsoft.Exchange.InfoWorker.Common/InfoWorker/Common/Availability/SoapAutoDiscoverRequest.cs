using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000076 RID: 118
	internal abstract class SoapAutoDiscoverRequest : AsyncWebRequest, IDisposable
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000DA2B File Offset: 0x0000BC2B
		public SoapAutoDiscoverRequest(Application application, ClientContext clientContext, RequestLogger requestLogger, string label, AutoDiscoverAuthenticator authenticator, Uri targetUri, EmailAddress[] emailAddresses, AutodiscoverType autodiscoverType) : base(application, clientContext, requestLogger, label)
		{
			this.authenticator = authenticator;
			this.TargetUri = SoapAutoDiscoverRequest.FixTargetUri(targetUri);
			this.EmailAddresses = emailAddresses;
			this.AutodiscoverType = autodiscoverType;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000DA60 File Offset: 0x0000BC60
		private static Uri FixTargetUri(Uri uri)
		{
			if (uri.AbsolutePath.EndsWith("/autodiscover.xml", StringComparison.OrdinalIgnoreCase))
			{
				return new UriBuilder(uri)
				{
					Path = "/autodiscover/autodiscover.svc"
				}.Uri;
			}
			return uri;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000DA9A File Offset: 0x0000BC9A
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000DAA2 File Offset: 0x0000BCA2
		public EmailAddress[] EmailAddresses { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000DAAB File Offset: 0x0000BCAB
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000DAB3 File Offset: 0x0000BCB3
		public Exception Exception { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000DABC File Offset: 0x0000BCBC
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000DAC4 File Offset: 0x0000BCC4
		public string FrontEndServerName { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000DACD File Offset: 0x0000BCCD
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000DAD5 File Offset: 0x0000BCD5
		public string BackEndServerName { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000DADE File Offset: 0x0000BCDE
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000DAE6 File Offset: 0x0000BCE6
		public AutoDiscoverRequestResult[] Results { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000DAEF File Offset: 0x0000BCEF
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000DAF7 File Offset: 0x0000BCF7
		public Uri TargetUri { get; private set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000DB00 File Offset: 0x0000BD00
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000DB08 File Offset: 0x0000BD08
		public AutodiscoverType AutodiscoverType { get; private set; }

		// Token: 0x06000300 RID: 768 RVA: 0x0000DB11 File Offset: 0x0000BD11
		public override void Abort()
		{
			base.Abort();
			if (this.client != null)
			{
				this.client.Abort();
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		public void Dispose()
		{
			if (this.client != null)
			{
				this.client.Dispose();
			}
		}

		// Token: 0x06000302 RID: 770
		protected abstract IAsyncResult BeginGetSettings(AsyncCallback callback);

		// Token: 0x06000303 RID: 771
		protected abstract AutodiscoverResponse EndGetSettings(IAsyncResult asyncResult);

		// Token: 0x06000304 RID: 772
		protected abstract void HandleResponse(AutodiscoverResponse response);

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000DB41 File Offset: 0x0000BD41
		protected override bool IsImpersonating
		{
			get
			{
				return this.authenticator.ProxyAuthenticator != null && this.authenticator.ProxyAuthenticator.AuthenticatorType == AuthenticatorType.NetworkCredentials;
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000DB94 File Offset: 0x0000BD94
		protected override IAsyncResult BeginInvoke()
		{
			this.autoDiscoverTimer = Stopwatch.StartNew();
			UserAgent userAgent = new UserAgent("ASAutoDiscover", "CrossForest", "EmailDomain", null);
			this.client = new DefaultBinding_Autodiscover(Globals.CertificateValidationComponentId, new RemoteCertificateValidationCallback(CertificateErrorHandler.CertValidationCallback));
			this.client.Url = this.TargetUri.ToString();
			this.client.UserAgent = userAgent.ToString();
			this.client.RequestedServerVersionValue = SoapAutoDiscoverRequest.Exchange2010RequestedServerVersion;
			Server localServer = LocalServerCache.LocalServer;
			if (localServer != null && localServer.InternetWebProxy != null)
			{
				SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceDebug<SoapAutoDiscoverRequest, Uri>((long)this.GetHashCode(), "{0}: Using custom InternetWebProxy {1}.", this, localServer.InternetWebProxy);
				this.client.Proxy = new WebProxy(localServer.InternetWebProxy);
			}
			this.authenticator.Authenticate(this.client);
			SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceDebug<object, SoapAutoDiscoverRequest>((long)this.GetHashCode(), "{0}: Requesting: {1}", TraceContext.Get(), this);
			IAsyncResult result = null;
			Exception ex = this.ExecuteAndHandleException(delegate
			{
				result = this.BeginGetSettings(new AsyncCallback(this.Complete));
			});
			if (ex != null)
			{
				SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError<object, SoapAutoDiscoverRequest, Exception>((long)this.GetHashCode(), "{0}: Request '{1}' failed due exception: {2}", TraceContext.Get(), this, ex);
				this.HandleException(ex);
				return null;
			}
			return result;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000DCE2 File Offset: 0x0000BEE2
		protected override bool ShouldCallBeginInvokeByNewThread
		{
			get
			{
				return this.authenticator.ProxyAuthenticator != null && this.authenticator.ProxyAuthenticator.AuthenticatorType == AuthenticatorType.OAuth;
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000DD28 File Offset: 0x0000BF28
		protected override void EndInvoke(IAsyncResult asyncResult)
		{
			AutodiscoverResponse response = null;
			Exception ex = this.ExecuteAndHandleException(delegate
			{
				response = this.EndGetSettings(asyncResult);
			});
			Dictionary<string, string> responseHttpHeaders = this.client.ResponseHttpHeaders;
			if (responseHttpHeaders.ContainsKey(WellKnownHeader.XFEServer))
			{
				this.FrontEndServerName = responseHttpHeaders[WellKnownHeader.XFEServer];
			}
			if (responseHttpHeaders.ContainsKey(WellKnownHeader.XBEServer))
			{
				this.BackEndServerName = responseHttpHeaders[WellKnownHeader.XBEServer];
			}
			if (ex != null)
			{
				SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError<object, SoapAutoDiscoverRequest, Exception>((long)this.GetHashCode(), "{0}: Request '{1}' failed due exception: {2}", TraceContext.Get(), this, ex);
				this.HandleException(ex);
				return;
			}
			if (response == null)
			{
				SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError<object, SoapAutoDiscoverRequest>((long)this.GetHashCode(), "{0}: Request '{1}' succeeded, but received empty response", TraceContext.Get(), this);
				this.HandleException(new AutoDiscoverFailedException(Strings.descSoapAutoDiscoverInvalidResponseError(this.client.Url), 59196U));
				return;
			}
			if (response.ErrorCodeSpecified && response.ErrorCode != ErrorCode.NoError)
			{
				SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError((long)this.GetHashCode(), "{0}: Request '{1}' failed with error {2}:{3}", new object[]
				{
					TraceContext.Get(),
					this,
					response.ErrorCode,
					response.ErrorMessage
				});
				this.HandleException(new AutoDiscoverFailedException(Strings.descSoapAutoDiscoverResponseError(this.client.Url, response.ErrorMessage), 34620U));
				return;
			}
			this.HandleResponse(response);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		protected AutoDiscoverRequestResult GetAutodiscoverResult(string urlValue, string versionValue, EmailAddress emailAddress)
		{
			if (string.IsNullOrEmpty(urlValue) || !Uri.IsWellFormedUriString(urlValue, UriKind.Absolute))
			{
				SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError((long)this.GetHashCode(), "{0}: Request '{1}' got ExternalEwsUrl setting for user {2} has invalid value: {3}", new object[]
				{
					TraceContext.Get(),
					this,
					emailAddress.Address,
					urlValue
				});
				return null;
			}
			int serverVersion = Globals.E14Version;
			if (!string.IsNullOrEmpty(versionValue))
			{
				Exception ex = null;
				try
				{
					Version version = new Version(versionValue);
					serverVersion = version.ToInt();
				}
				catch (ArgumentException ex2)
				{
					ex = ex2;
				}
				catch (FormatException ex3)
				{
					ex = ex3;
				}
				catch (OverflowException ex4)
				{
					ex = ex4;
				}
				if (ex != null)
				{
					SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError<object, Exception>((long)this.GetHashCode(), "{0}: Exception parsing version: {1}", TraceContext.Get(), ex);
				}
			}
			string url = urlValue;
			if (this.authenticator.ProxyAuthenticator != null && this.authenticator.ProxyAuthenticator.AuthenticatorType == AuthenticatorType.WSSecurity)
			{
				url = EwsWsSecurityUrl.Fix(url);
			}
			return new AutoDiscoverRequestResult(this.TargetUri, null, null, new WebServiceUri(url, null, UriSource.EmailDomain, serverVersion), null, null);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		private Exception ExecuteAndHandleException(SoapAutoDiscoverRequest.ExecuteAndHandleExceptionDelegate operation)
		{
			Exception ex = null;
			try
			{
				operation();
				return null;
			}
			catch (LocalizedException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			catch (WebException ex4)
			{
				ex = ex4;
			}
			catch (SoapException ex5)
			{
				ex = ex5;
			}
			catch (InvalidOperationException ex6)
			{
				ex = ex6;
			}
			SoapAutoDiscoverRequest.AutoDiscoverTracer.TraceError<object, SoapAutoDiscoverRequest, Exception>((long)this.GetHashCode(), "{0}: Request '{1}' failed due exception: {2}", TraceContext.Get(), this, ex);
			return ex;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000E074 File Offset: 0x0000C274
		protected override void HandleException(Exception exception)
		{
			this.Exception = exception;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000E080 File Offset: 0x0000C280
		protected void HandleResult(AutoDiscoverRequestResult[] autoDiscoverResults)
		{
			this.autoDiscoverTimer.Stop();
			base.RequestLogger.Add(RequestStatistics.Create(RequestStatisticsType.AutoDiscoverRequest, this.autoDiscoverTimer.ElapsedMilliseconds, autoDiscoverResults.Length, this.TargetUri.ToString()));
			this.Results = autoDiscoverResults;
		}

		// Token: 0x040001CF RID: 463
		protected const string ExternalEwsUrl = "ExternalEwsUrl";

		// Token: 0x040001D0 RID: 464
		protected const string InteropExternalEwsUrl = "InteropExternalEwsUrl";

		// Token: 0x040001D1 RID: 465
		protected const string ExternalEwsVersion = "ExternalEwsVersion";

		// Token: 0x040001D2 RID: 466
		protected const string InteropExternalEwsVersion = "InteropExternalEwsVersion";

		// Token: 0x040001D3 RID: 467
		protected AutoDiscoverAuthenticator authenticator;

		// Token: 0x040001D4 RID: 468
		private Stopwatch autoDiscoverTimer;

		// Token: 0x040001D5 RID: 469
		protected DefaultBinding_Autodiscover client;

		// Token: 0x040001D6 RID: 470
		private static readonly RequestedServerVersion Exchange2010RequestedServerVersion = new RequestedServerVersion
		{
			Text = new string[]
			{
				"Exchange2010"
			}
		};

		// Token: 0x040001D7 RID: 471
		protected static readonly Microsoft.Exchange.Diagnostics.Trace AutoDiscoverTracer = ExTraceGlobals.AutoDiscoverTracer;

		// Token: 0x02000077 RID: 119
		// (Invoke) Token: 0x0600030F RID: 783
		private delegate void ExecuteAndHandleExceptionDelegate();
	}
}
