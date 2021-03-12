using System;
using System.Net;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200003F RID: 63
	internal abstract class DatacenterRedirectStrategy
	{
		// Token: 0x060001EC RID: 492 RVA: 0x0000AA07 File Offset: 0x00008C07
		public DatacenterRedirectStrategy(IRequestContext requestContext)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			this.RequestContext = requestContext;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000AA24 File Offset: 0x00008C24
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000AA2C File Offset: 0x00008C2C
		public IRequestContext RequestContext { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000AA35 File Offset: 0x00008C35
		public int TraceContext
		{
			get
			{
				return this.RequestContext.TraceContext;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000AA44 File Offset: 0x00008C44
		public static void CheckLiveIdBasicPartialAuthResult(HttpContext httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			IPrincipal user = httpContext.User;
			if (user != null && user.Identity != null && user.GetType().Equals(typeof(GenericPrincipal)) && user.Identity.GetType().Equals(typeof(GenericIdentity)) && string.Equals(user.Identity.AuthenticationType, "LiveIdBasic", StringComparison.OrdinalIgnoreCase))
			{
				throw new HttpException(403, string.Format("Unable to resolve identity: {0}", user.Identity.GetSafeName(true)));
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		public void RedirectMailbox(AnchorMailbox anchorMailbox)
		{
			if (anchorMailbox == null)
			{
				throw new ArgumentNullException("anchorMailbox");
			}
			if (!(anchorMailbox is UserBasedAnchorMailbox))
			{
				throw new ArgumentException("The AnchorMailbox object needs to be user based.");
			}
			string userAddress = this.ResolveUserAddress(anchorMailbox);
			this.RedirectAddress(userAddress);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000AB20 File Offset: 0x00008D20
		protected virtual Uri GetRedirectUrl(string redirectServer)
		{
			return new UriBuilder(this.RequestContext.HttpContext.Request.Url)
			{
				Host = redirectServer
			}.Uri;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000AB58 File Offset: 0x00008D58
		private void RedirectAddress(string userAddress)
		{
			ExTraceGlobals.VerboseTracer.TraceDebug<int, string>((long)this.GetHashCode(), "[DatacenterRedirectStrategy::RedirectAddress]: Context {0}. Will use address {2} to look up MSERV.", this.TraceContext, userAddress);
			string text = this.InvokeMserv(userAddress);
			ExTraceGlobals.VerboseTracer.TraceDebug<int, string, string>((long)this.GetHashCode(), "[DatacenterRedirectStrategy::RedirectAddress]: Context {0}. Will redirect user {1} to server {2}", this.TraceContext, userAddress, text);
			Uri redirectUrl = this.GetRedirectUrl(text);
			ExTraceGlobals.VerboseTracer.TraceDebug<int, string, Uri>((long)this.GetHashCode(), "[DatacenterRedirectStrategy::RedirectAddress]: Context {0}. Will redirect user {1} to URL {2}", this.TraceContext, userAddress, redirectUrl);
			throw new HttpException(302, redirectUrl.AbsoluteUri);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000AC1C File Offset: 0x00008E1C
		private string InvokeMserv(string userAddress)
		{
			int currentSitePartnerId = HttpProxyGlobals.LocalSite.Member.PartnerId;
			string text = null;
			long num = 0L;
			Exception ex = null;
			try
			{
				text = LatencyTracker.GetLatency<string>(() => EdgeSyncMservConnector.GetRedirectServer(DatacenterRedirectStrategy.PodRedirectTemplate.Value, userAddress, currentSitePartnerId, DatacenterRedirectStrategy.PodSiteStartRange.Value, DatacenterRedirectStrategy.PodSiteEndRange.Value, false, true), out num);
			}
			catch (MServTransientException ex2)
			{
				ex = ex2;
			}
			catch (MServPermanentException ex3)
			{
				ex = ex3;
			}
			catch (InvalidOperationException ex4)
			{
				ex = ex4;
			}
			catch (LocalizedException ex5)
			{
				ex = ex5;
			}
			finally
			{
				this.RequestContext.Logger.AppendGenericInfo("MservLatency", num);
			}
			string message = string.Format("Failed to look up MSERV for address {0}.", userAddress);
			if (ex != null)
			{
				ExTraceGlobals.VerboseTracer.TraceError<int, string, Exception>((long)this.GetHashCode(), "[DatacenterRedirectStrategy::InvokeMserv]: Context {0}. Failed to look up MSERV for address {1}. Error: {2}", this.TraceContext, userAddress, ex);
				throw new HttpProxyException(HttpStatusCode.InternalServerError, HttpProxySubErrorCode.MServOperationError, message, ex);
			}
			if (string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.VerboseTracer.TraceError<int, string>((long)this.GetHashCode(), "[DatacenterRedirectStrategy::InvokeMserv]: Context {0}. MSERV did not return redirect server for address {1}.", this.TraceContext, userAddress);
				throw new HttpProxyException(HttpStatusCode.InternalServerError, HttpProxySubErrorCode.MServOperationError, message);
			}
			return text;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000AD7C File Offset: 0x00008F7C
		private string ResolveUserAddress(AnchorMailbox anchorMailbox)
		{
			SidAnchorMailbox sidAnchorMailbox = anchorMailbox as SidAnchorMailbox;
			if (sidAnchorMailbox != null && !string.IsNullOrEmpty(sidAnchorMailbox.SmtpOrLiveId))
			{
				return sidAnchorMailbox.SmtpOrLiveId;
			}
			SmtpAnchorMailbox smtpAnchorMailbox = anchorMailbox as SmtpAnchorMailbox;
			if (smtpAnchorMailbox != null)
			{
				return smtpAnchorMailbox.Smtp;
			}
			UserBasedAnchorMailbox userBasedAnchorMailbox = (UserBasedAnchorMailbox)anchorMailbox;
			return string.Format("anyone@{0}", userBasedAnchorMailbox.GetDomainName());
		}

		// Token: 0x040000F8 RID: 248
		protected static readonly StringAppSettingsEntry PodRedirectTemplate = new StringAppSettingsEntry("PodRedirectTemplate", "pod{0}.outlook.com", ExTraceGlobals.VerboseTracer);

		// Token: 0x040000F9 RID: 249
		protected static readonly IntAppSettingsEntry PodSiteStartRange = new IntAppSettingsEntry("PodSiteStartRange", 5000, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000FA RID: 250
		protected static readonly IntAppSettingsEntry PodSiteEndRange = new IntAppSettingsEntry("PodSiteEndRange", 5009, ExTraceGlobals.VerboseTracer);
	}
}
