using System;
using System.Security;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000059 RID: 89
	internal class WacRequest
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0000A148 File Offset: 0x00008348
		private WacRequest(WacRequestType requestType, WacFileRep fileRep, SmtpAddress mailboxSmtpAddress, string exchangeSessionId, string ewsAttachmentId, string culture, string clientVersion, string machineName, bool perfTraceRequested, string correlationID)
		{
			this.RequestType = requestType;
			this.WacFileRep = fileRep;
			this.MailboxSmtpAddress = mailboxSmtpAddress;
			this.ExchangeSessionId = exchangeSessionId;
			this.EwsAttachmentId = ewsAttachmentId;
			this.CultureName = culture;
			this.ClientVersion = clientVersion;
			this.MachineName = machineName;
			this.PerfTraceRequested = perfTraceRequested;
			this.CorrelationID = correlationID;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000A1A8 File Offset: 0x000083A8
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000A1B0 File Offset: 0x000083B0
		public string CultureName { get; private set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000A1B9 File Offset: 0x000083B9
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000A1C1 File Offset: 0x000083C1
		public WacFileRep WacFileRep { get; private set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000A1CA File Offset: 0x000083CA
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000A1D2 File Offset: 0x000083D2
		public WacRequestType RequestType { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000A1DB File Offset: 0x000083DB
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000A1E3 File Offset: 0x000083E3
		public SmtpAddress MailboxSmtpAddress { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000A1EC File Offset: 0x000083EC
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000A1F4 File Offset: 0x000083F4
		public string ExchangeSessionId { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000A1FD File Offset: 0x000083FD
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000A205 File Offset: 0x00008405
		public string EwsAttachmentId { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000A20E File Offset: 0x0000840E
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000A216 File Offset: 0x00008416
		public string ClientVersion { get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000A21F File Offset: 0x0000841F
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000A227 File Offset: 0x00008427
		public string MachineName { get; private set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000A230 File Offset: 0x00008430
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000A238 File Offset: 0x00008438
		public bool PerfTraceRequested { get; private set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000A241 File Offset: 0x00008441
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000A249 File Offset: 0x00008449
		public string CorrelationID { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000A254 File Offset: 0x00008454
		public string CacheKey
		{
			get
			{
				string primarySmtpAddress = (string)this.MailboxSmtpAddress;
				string ewsAttachmentId = this.EwsAttachmentId;
				return CachedAttachmentInfo.GetCacheKey(primarySmtpAddress, ewsAttachmentId);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A280 File Offset: 0x00008480
		public static WacRequest ParseWacRequest(string mailboxSmtpAddress, HttpRequest request)
		{
			if (request == null)
			{
				throw new OwaInvalidRequestException("Request object is null");
			}
			if (!UrlUtilities.IsWacRequest(request))
			{
				throw new OwaInvalidRequestException("Expected a WAC request, but got this instead: " + request.Url.AbsoluteUri);
			}
			WacRequestType requestType = WacRequest.GetRequestType(request);
			string text = request.QueryString["access_token"] ?? string.Empty;
			string exchangeSessionId = WacUtilities.GetExchangeSessionId(text);
			string ewsAttachmentId;
			WacRequest.ParseAccessToken(text, out ewsAttachmentId);
			string fileRepAsString = request.QueryString["owaatt"] ?? string.Empty;
			WacFileRep fileRep = WacFileRep.Parse(fileRepAsString);
			string value = request.Headers["X-WOPI-PerfTraceRequested"] ?? string.Empty;
			bool perfTraceRequested;
			if (!bool.TryParse(value, out perfTraceRequested))
			{
				perfTraceRequested = false;
			}
			return new WacRequest(requestType, fileRep, (SmtpAddress)mailboxSmtpAddress, exchangeSessionId, ewsAttachmentId, request.QueryString["ui"] ?? "en-us", request.Headers["X-WOPI-InterfaceVersion"] ?? string.Empty, request.Headers["X-WOPI-MachineName"] ?? string.Empty, perfTraceRequested, request.Headers["X-WOPI-CorrelationID"] ?? string.Empty);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000A3B5 File Offset: 0x000085B5
		public override string ToString()
		{
			return string.Format("{0} request for {1}, session started at {2}", this.RequestType, this.MailboxSmtpAddress, this.WacFileRep.CreationTime);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A3E8 File Offset: 0x000085E8
		internal static WacRequestType GetRequestType(HttpRequest request)
		{
			if (string.Equals(request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
			{
				string a = HttpUtility.UrlDecode(request.Url.AbsolutePath);
				WacRequestType result;
				if (string.Equals(a, "/owa/wopi/files/@/owaatt/contents", StringComparison.OrdinalIgnoreCase) || (Globals.IsPreCheckinApp && request.Url.AbsolutePath.Contains("/contents")))
				{
					result = WacRequestType.GetFile;
				}
				else
				{
					result = WacRequestType.CheckFile;
				}
				return result;
			}
			if (string.Equals(request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
			{
				string text = request.Headers["X-WOPI-Override"];
				string a2;
				if ((a2 = text) != null)
				{
					WacRequestType result2;
					if (!(a2 == "PUT"))
					{
						if (!(a2 == "COBALT"))
						{
							if (!(a2 == "REFRESH_LOCK"))
							{
								if (!(a2 == "LOCK"))
								{
									if (!(a2 == "UNLOCK"))
									{
										goto IL_E1;
									}
									result2 = WacRequestType.UnLock;
								}
								else
								{
									result2 = WacRequestType.Lock;
								}
							}
							else
							{
								result2 = WacRequestType.RefreshLock;
							}
						}
						else
						{
							result2 = WacRequestType.Cobalt;
						}
					}
					else
					{
						result2 = WacRequestType.PutFile;
					}
					return result2;
				}
				IL_E1:
				throw new OwaInvalidRequestException("Unknown WOPI request: " + text);
			}
			return WacRequestType.Unknown;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A4EC File Offset: 0x000086EC
		internal bool IsExpired()
		{
			DateTime t = this.WacFileRep.CreationTime + WacConfiguration.Instance.AccessTokenExpirationDuration;
			return DateTime.UtcNow > t;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000A520 File Offset: 0x00008720
		internal TimeSpan GetElapsedTime()
		{
			ExDateTime value = new ExDateTime(ExTimeZone.UtcTimeZone, this.WacFileRep.CreationTime);
			return ExDateTime.Now.Subtract(value);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000A554 File Offset: 0x00008754
		private static void ParseAccessToken(string rawToken, out string ewsAttachmentId)
		{
			ewsAttachmentId = null;
			try
			{
				ewsAttachmentId = OAuthTokenHandler.ValidateWacCallbackToken(rawToken);
			}
			catch (SecurityException innerException)
			{
				throw new OwaInvalidRequestException("Unable to parse WAC access token.", innerException);
			}
			catch (ArgumentException innerException2)
			{
				throw new OwaInvalidRequestException("Unable to parse WAC access token.", innerException2);
			}
			catch (InvalidOperationException innerException3)
			{
				throw new OwaInvalidRequestException("Unable to parse WAC access token.", innerException3);
			}
		}
	}
}
