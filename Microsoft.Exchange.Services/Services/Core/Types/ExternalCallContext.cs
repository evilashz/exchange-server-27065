using System;
using System.ServiceModel.Channels;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.InfoWorker.Common.Sharing;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Net.WSSecurity;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000777 RID: 1911
	internal class ExternalCallContext : CallContext
	{
		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x0600391B RID: 14619 RVA: 0x000CA11C File Offset: 0x000C831C
		internal Offer Offer
		{
			get
			{
				return this.offer;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x0600391C RID: 14620 RVA: 0x000CA124 File Offset: 0x000C8324
		internal SmtpAddress ExternalId
		{
			get
			{
				return this.externalId;
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x000CA12C File Offset: 0x000C832C
		internal SmtpAddress EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000CA134 File Offset: 0x000C8334
		public override string ToString()
		{
			return "External call context for " + this.emailAddress;
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000CA14C File Offset: 0x000C834C
		internal ExternalCallContext(MessageHeaderProcessor headerProcessor, Message request, ExternalIdentity externalIdentity, UserWorkloadManager workloadManager)
		{
			this.emailAddress = externalIdentity.EmailAddress;
			this.offer = externalIdentity.Offer;
			this.externalId = externalIdentity.ExternalId;
			this.wsSecurityHeader = externalIdentity.WSSecurityHeader;
			this.sharingSecurityHeader = externalIdentity.SharingSecurityHeader;
			if (headerProcessor.SeeksProxyingOrS2S(request))
			{
				throw FaultExceptionUtilities.CreateFault(new ImpersonationFailedException(null), FaultParty.Sender);
			}
			this.availabilityProxyRequestType = headerProcessor.ProcessRequestTypeHeader(request);
			this.userKind = CallContext.UserKind.External;
			headerProcessor.ProcessMailboxCultureHeader(request);
			this.clientCulture = EWSSettings.ClientCulture;
			this.serverCulture = EWSSettings.ServerCulture;
			try
			{
				headerProcessor.ProcessTimeZoneContextHeader(request);
			}
			catch (LocalizedException exception)
			{
				throw FaultExceptionUtilities.CreateFault(exception, FaultParty.Sender);
			}
			headerProcessor.ProcessDateTimePrecisionHeader(request);
			this.sessionCache = new SessionCache(null, this);
			this.workloadManager = workloadManager;
			this.authZBehavior = AuthZBehavior.DefaultBehavior;
			this.requestedLogonType = RequestedLogonType.Default;
			this.adRecipientSessionContext = this.CreateADRecipientSessionContext();
			base.MethodName = CallContext.GetMethodName(request);
			HttpContext.Current.Items["CallContext"] = this;
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06003920 RID: 14624 RVA: 0x000CA264 File Offset: 0x000C8464
		internal WSSecurityHeader WSSecurityHeader
		{
			get
			{
				return this.wsSecurityHeader;
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06003921 RID: 14625 RVA: 0x000CA26C File Offset: 0x000C846C
		internal SharingSecurityHeader SharingSecurityHeader
		{
			get
			{
				return this.sharingSecurityHeader;
			}
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000CA274 File Offset: 0x000C8474
		private ADRecipientSessionContext CreateADRecipientSessionContext()
		{
			string text = base.HttpContext.Request.Headers[WellKnownHeader.AnchorMailbox];
			if (string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SmtpAddress>(0L, "AnchorMailbox header not passed by {0} client", this.emailAddress);
				return ADRecipientSessionContext.CreateForRootOrganization();
			}
			return ADRecipientSessionContext.CreateFromSmtpAddress(text);
		}

		// Token: 0x04001FDC RID: 8156
		private Offer offer;

		// Token: 0x04001FDD RID: 8157
		private SmtpAddress emailAddress;

		// Token: 0x04001FDE RID: 8158
		private SmtpAddress externalId;

		// Token: 0x04001FDF RID: 8159
		private WSSecurityHeader wsSecurityHeader;

		// Token: 0x04001FE0 RID: 8160
		private SharingSecurityHeader sharingSecurityHeader;
	}
}
