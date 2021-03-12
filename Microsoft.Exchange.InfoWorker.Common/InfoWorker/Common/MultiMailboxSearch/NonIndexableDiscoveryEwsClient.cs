using System;
using System.Net.Security;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001FE RID: 510
	internal class NonIndexableDiscoveryEwsClient : INonIndexableDiscoveryEwsClient
	{
		// Token: 0x06000D98 RID: 3480 RVA: 0x0003B854 File Offset: 0x00039A54
		public NonIndexableDiscoveryEwsClient(GroupId groupId, MailboxInfo[] mailboxes, ExTimeZone timeZone, CallerInfo caller)
		{
			Util.ThrowOnNull(groupId, "groupId");
			Util.ThrowOnNull(mailboxes, "mailboxes");
			Util.ThrowOnNull(timeZone, "timeZone");
			Util.ThrowOnNull(caller, "caller");
			this.groupId = groupId;
			this.mailboxes = mailboxes;
			this.callerInfo = caller;
			CertificateValidationManager.RegisterCallback(base.GetType().FullName, new RemoteCertificateValidationCallback(CertificateValidation.CertificateErrorHandler));
			this.service = new ExchangeService(4, NonIndexableDiscoveryEwsClient.GetTimeZoneInfoFromExTimeZone(timeZone));
			this.service.Url = this.groupId.Uri;
			this.service.HttpHeaders[CertificateValidationManager.ComponentIdHeaderName] = base.GetType().FullName;
			if (this.groupId.GroupType != GroupType.CrossPremise)
			{
				this.service.UserAgent = WellKnownUserAgent.GetEwsNegoAuthUserAgent(base.GetType().FullName);
			}
			this.service.ClientRequestId = this.callerInfo.QueryCorrelationId.ToString("N");
			this.Authenticate();
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0003B960 File Offset: 0x00039B60
		public IAsyncResult BeginGetNonIndexableItemStatistics(AsyncCallback callback, object state, GetNonIndexableItemStatisticsParameters parameters)
		{
			return this.service.BeginGetNonIndexableItemStatistics(callback, state, parameters);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0003B970 File Offset: 0x00039B70
		public GetNonIndexableItemStatisticsResponse EndGetNonIndexableItemStatistics(IAsyncResult result)
		{
			return this.service.EndGetNonIndexableItemStatistics(result);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0003B97E File Offset: 0x00039B7E
		public IAsyncResult BeginGetNonIndexableItemDetails(AsyncCallback callback, object state, GetNonIndexableItemDetailsParameters parameters)
		{
			return this.service.BeginGetNonIndexableItemDetails(callback, state, parameters);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0003B98E File Offset: 0x00039B8E
		public GetNonIndexableItemDetailsResponse EndGetNonIndexableItemDetails(IAsyncResult result)
		{
			return this.service.EndGetNonIndexableItemDetails(result);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0003B99C File Offset: 0x00039B9C
		private static TimeZoneInfo GetTimeZoneInfoFromExTimeZone(ExTimeZone timeZone)
		{
			return TimeZoneInfo.Utc;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0003B9A4 File Offset: 0x00039BA4
		private void Authenticate()
		{
			switch (this.groupId.GroupType)
			{
			case GroupType.CrossServer:
				this.service.OnSerializeCustomSoapHeaders += new CustomXmlSerializationDelegate(this.OnSerializeCustomSoapHeaders);
				return;
			case GroupType.CrossPremise:
				this.service.ManagementRoles = new ManagementRoles(null, DiscoveryEwsClient.MailboxSearchApplicationRole);
				this.service.Credentials = new OAuthCredentials(OAuthCredentials.GetOAuthCredentialsForAppToken(this.callerInfo.OrganizationId, this.mailboxes[0].GetDomain()));
				return;
			default:
				return;
			}
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0003BA29 File Offset: 0x00039C29
		private void OnSerializeCustomSoapHeaders(XmlWriter writer)
		{
			Util.SerializeIdentityCustomSoapHeaders(NonIndexableDiscoveryEwsClient.securityContextSerializer, writer, this.callerInfo.PrimarySmtpAddress);
		}

		// Token: 0x04000968 RID: 2408
		private static XmlSerializer securityContextSerializer = new XmlSerializer(typeof(OpenAsAdminOrSystemServiceType));

		// Token: 0x04000969 RID: 2409
		private readonly ExchangeService service;

		// Token: 0x0400096A RID: 2410
		private readonly CallerInfo callerInfo;

		// Token: 0x0400096B RID: 2411
		private readonly MailboxInfo[] mailboxes;

		// Token: 0x0400096C RID: 2412
		private readonly GroupId groupId;
	}
}
