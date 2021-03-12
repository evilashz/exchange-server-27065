using System;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.MapiHttp;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000B9 RID: 185
	internal class MapiProxyRequestHandler : BEServerCookieProxyRequestHandler<WebServicesService>
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0002A72D File Offset: 0x0002892D
		protected override ClientAccessType ClientAccessType
		{
			get
			{
				return ClientAccessType.InternalNLBBypass;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0002A730 File Offset: 0x00028930
		protected override bool ShouldForceUnbufferedClientResponseOutput
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0002A733 File Offset: 0x00028933
		protected override bool ShouldSendFullActivityScope
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0002A738 File Offset: 0x00028938
		protected override BufferPool GetResponseBufferPool()
		{
			if (MapiProxyRequestHandler.UseCustomNotificationWaitBuffers.Value)
			{
				string text = base.ClientRequest.Headers["X-RequestType"];
				if (!string.IsNullOrEmpty(text) && string.Equals(text, "NotificationWait", StringComparison.OrdinalIgnoreCase))
				{
					return MapiProxyRequestHandler.NotificationWaitBufferPool;
				}
			}
			return base.GetResponseBufferPool();
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0002A789 File Offset: 0x00028989
		protected override bool ShouldCopyHeaderToServerRequest(string headerName)
		{
			return !MapiProxyRequestHandler.ProtectedHeaderNames.Contains(headerName, StringComparer.OrdinalIgnoreCase) && base.ShouldCopyHeaderToServerRequest(headerName);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0002A7A6 File Offset: 0x000289A6
		protected override void DoProtocolSpecificBeginRequestLogging()
		{
			this.LogClientRequestInfo();
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0002A7B0 File Offset: 0x000289B0
		protected override void AddProtocolSpecificHeadersToServerRequest(WebHeaderCollection headers)
		{
			DatabaseBasedAnchorMailbox databaseBasedAnchorMailbox = base.AnchoredRoutingTarget.AnchorMailbox as DatabaseBasedAnchorMailbox;
			if (databaseBasedAnchorMailbox != null)
			{
				ADObjectId database = databaseBasedAnchorMailbox.GetDatabase();
				if (database != null)
				{
					headers[WellKnownHeader.MailboxDatabaseGuid] = database.ObjectGuid.ToString();
				}
			}
			base.AddProtocolSpecificHeadersToServerRequest(headers);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0002A804 File Offset: 0x00028A04
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			string text = base.ClientRequest.QueryString["mailboxId"];
			if (!string.IsNullOrEmpty(text))
			{
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "MailboxGuidWithDomain");
				return this.GetAnchorMailboxFromMailboxId(text);
			}
			text = base.ClientRequest.QueryString["smtpAddress"];
			if (!string.IsNullOrEmpty(text))
			{
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "SMTP");
				return this.GetAnchorMailboxFromSmtpAddress(text);
			}
			text = base.ClientRequest.QueryString["useMailboxOfAuthenticatedUser"];
			bool flag = false;
			if (!string.IsNullOrEmpty(text) && bool.TryParse(text, out flag) && flag)
			{
				return base.ResolveAnchorMailbox();
			}
			if (string.Compare(base.ClientRequest.RequestType, "GET", true) == 0)
			{
				return base.ResolveAnchorMailbox();
			}
			throw new HttpProxyException(HttpStatusCode.BadRequest, HttpProxySubErrorCode.MailboxGuidWithDomainNotFound, "No target mailbox specified.");
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0002A8F4 File Offset: 0x00028AF4
		private AnchorMailbox GetAnchorMailboxFromMailboxId(string mailboxId)
		{
			Guid guid = Guid.Empty;
			string domain = string.Empty;
			if (!SmtpAddress.IsValidSmtpAddress(mailboxId))
			{
				throw new HttpProxyException(HttpStatusCode.BadRequest, HttpProxySubErrorCode.MailboxGuidWithDomainNotFound, "Malformed mailbox id.");
			}
			try
			{
				SmtpAddress smtpAddress = new SmtpAddress(mailboxId);
				guid = new Guid(smtpAddress.Local);
				domain = smtpAddress.Domain;
			}
			catch (FormatException innerException)
			{
				throw new HttpProxyException(HttpStatusCode.BadRequest, HttpProxySubErrorCode.MailboxGuidWithDomainNotFound, string.Format("Invalid mailboxGuid {0}", guid), innerException);
			}
			return new MailboxGuidAnchorMailbox(guid, domain, this);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0002A984 File Offset: 0x00028B84
		private AnchorMailbox GetAnchorMailboxFromSmtpAddress(string smtpAddress)
		{
			if (!SmtpAddress.IsValidSmtpAddress(smtpAddress))
			{
				throw new HttpProxyException(HttpStatusCode.BadRequest, HttpProxySubErrorCode.MailboxGuidWithDomainNotFound, "Malformed smtp address.");
			}
			return new SmtpAnchorMailbox(smtpAddress, this);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0002A9AC File Offset: 0x00028BAC
		private void LogClientRequestInfo()
		{
			if (string.Compare(base.ClientRequest.RequestType, "POST", true) != 0)
			{
				return;
			}
			string clientRequestInfo = MapiHttpEndpoints.GetClientRequestInfo(base.HttpContext);
			base.ClientResponse.AppendToLog("&ClientRequestInfo=" + clientRequestInfo);
			base.Logger.Set(ActivityStandardMetadata.ClientRequestId, clientRequestInfo);
		}

		// Token: 0x0400048F RID: 1167
		private const string MailboxIdParameter = "mailboxId";

		// Token: 0x04000490 RID: 1168
		private const string SmtpAddressParameter = "smtpAddress";

		// Token: 0x04000491 RID: 1169
		private const string UseMailboxOfAuthenticatedUserParameter = "useMailboxOfAuthenticatedUser";

		// Token: 0x04000492 RID: 1170
		private const string HttpVerbGet = "GET";

		// Token: 0x04000493 RID: 1171
		private const string HttpVerbPost = "POST";

		// Token: 0x04000494 RID: 1172
		private const string XRequestType = "X-RequestType";

		// Token: 0x04000495 RID: 1173
		private const string ClientRequestInfoLogParameter = "&ClientRequestInfo=";

		// Token: 0x04000496 RID: 1174
		private const string RequestTypeEmsmdbNotificationWait = "NotificationWait";

		// Token: 0x04000497 RID: 1175
		private static readonly string[] ProtectedHeaderNames = new string[]
		{
			WellKnownHeader.MailboxDatabaseGuid
		};

		// Token: 0x04000498 RID: 1176
		private static readonly BoolAppSettingsEntry UseCustomNotificationWaitBuffers = new BoolAppSettingsEntry(HttpProxySettings.Prefix("UseCustomNotificationWaitBuffers"), true, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000499 RID: 1177
		private static readonly IntAppSettingsEntry NotificationWaitBufferSize = new IntAppSettingsEntry(HttpProxySettings.Prefix("NotificationWaitBufferSize"), 256, ExTraceGlobals.VerboseTracer);

		// Token: 0x0400049A RID: 1178
		private static readonly IntAppSettingsEntry NotificationWaitBuffersPerProcessor = new IntAppSettingsEntry(HttpProxySettings.Prefix("NotificationWaitBuffersPerProcessor"), 512, ExTraceGlobals.VerboseTracer);

		// Token: 0x0400049B RID: 1179
		private static readonly BufferPool NotificationWaitBufferPool = new BufferPool(MapiProxyRequestHandler.NotificationWaitBufferSize.Value, MapiProxyRequestHandler.NotificationWaitBuffersPerProcessor.Value, false);
	}
}
