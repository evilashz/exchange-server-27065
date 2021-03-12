using System;
using System.ServiceModel.Channels;
using System.Web;
using System.Xml;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200024F RID: 591
	internal class OwaMessageHeaderProcessor : JsonMessageHeaderProcessor
	{
		// Token: 0x06001643 RID: 5699 RVA: 0x00051214 File Offset: 0x0004F414
		internal override void ProcessMessageHeaders(Message request)
		{
			OwaServiceMessage owaServiceMessage = request as OwaServiceMessage;
			BaseJsonRequest baseJsonRequest = owaServiceMessage.Request as BaseJsonRequest;
			if (baseJsonRequest != null && baseJsonRequest.Header != null)
			{
				base.RequestVersion = JsonMessageHeaderProcessor.ReadRequestVersionHeader(baseJsonRequest.Header.RequestServerVersion.ToString());
				base.MailboxCulture = baseJsonRequest.Header.MailboxCulture;
				base.TimeZoneContext = baseJsonRequest.Header.TimeZoneContext;
				base.DateTimePrecision = new DateTimePrecision?(baseJsonRequest.Header.DateTimePrecision);
				base.IsBackgroundLoad = baseJsonRequest.Header.BackgroundLoad;
				base.ManagementRoleType = baseJsonRequest.Header.ManagementRole;
			}
			base.ProcessRequestVersion(request);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000512D4 File Offset: 0x0004F4D4
		internal override void ProcessMessageHeadersFromQueryString(Message request)
		{
			OwaServiceMessage owaServiceMessage = request as OwaServiceMessage;
			HttpRequest httpRequest = owaServiceMessage.HttpRequest;
			bool flag = false;
			foreach (object obj in httpRequest.QueryString.Keys)
			{
				string text = (string)obj;
				string a;
				if ((a = text) != null)
				{
					if (!(a == "ManagementRole"))
					{
						if (a == "RequestServerVersion")
						{
							base.RequestVersion = JsonMessageHeaderProcessor.ReadRequestVersionHeader(httpRequest.QueryString.Get(text));
						}
					}
					else
					{
						base.QueryStringXmlDictionaryReaderAction(httpRequest.QueryString.Get(text), delegate(XmlDictionaryReader reader)
						{
							base.ManagementRoleType = JsonMessageHeaderProcessor.ReadManagementRoleHeader(reader);
						});
						flag = true;
					}
				}
			}
			if (flag)
			{
				base.ProcessRequestVersion(request);
			}
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000513B4 File Offset: 0x0004F5B4
		internal override void ProcessHttpHeaders(Message request, ExchangeVersion defaultVersion)
		{
			OwaServiceMessage owaServiceMessage = request as OwaServiceMessage;
			HttpRequest httpRequest = owaServiceMessage.HttpRequest;
			if (httpRequest == null)
			{
				return;
			}
			string text = httpRequest.Headers["X-MailboxCulture"];
			if (!string.IsNullOrEmpty(text))
			{
				base.MailboxCulture = text;
			}
			string text2 = httpRequest.Headers["X-TimeZoneContext"];
			if (!string.IsNullOrEmpty(text2))
			{
				base.TimeZoneContext = new TimeZoneContextType
				{
					TimeZoneDefinition = new TimeZoneDefinitionType
					{
						Id = text2
					}
				};
			}
			string value = httpRequest.Headers["X-DateTimePrecision"];
			if (!string.IsNullOrEmpty(value))
			{
				base.DateTimePrecision = new DateTimePrecision?(JsonMessageHeaderProcessor.ReadDateTimePrecisionHeader(value));
			}
			string value2 = httpRequest.Headers["X-BackgroundLoad"];
			if (!string.IsNullOrEmpty(value2))
			{
				base.IsBackgroundLoad = bool.Parse(value2);
			}
			ExchangeVersion ewsVersionFromHttpHeaders = this.GetEwsVersionFromHttpHeaders(request, httpRequest);
			if (ewsVersionFromHttpHeaders != null)
			{
				base.RequestVersion = ewsVersionFromHttpHeaders;
			}
			else
			{
				string text3 = httpRequest.Headers["X-RequestServerVersion"];
				base.RequestVersion = (string.IsNullOrEmpty(text3) ? defaultVersion : JsonMessageHeaderProcessor.ReadRequestVersionHeader(text3));
			}
			base.ProcessRequestVersion(request);
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000514D8 File Offset: 0x0004F6D8
		internal override void ProcessEwsVersionFromHttpHeaders(Message request)
		{
			OwaServiceMessage owaServiceMessage = request as OwaServiceMessage;
			HttpRequest httpRequest = owaServiceMessage.HttpRequest;
			if (httpRequest == null)
			{
				return;
			}
			ExchangeVersion ewsVersionFromHttpHeaders = this.GetEwsVersionFromHttpHeaders(request, httpRequest);
			if (ewsVersionFromHttpHeaders != null)
			{
				base.RequestVersion = ewsVersionFromHttpHeaders;
				base.ProcessRequestVersion(request);
			}
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00051518 File Offset: 0x0004F718
		internal override ProxyRequestType? ProcessRequestTypeHeader(Message request)
		{
			OwaServiceMessage owaServiceMessage = request as OwaServiceMessage;
			if (owaServiceMessage != null)
			{
				return base.ParseProxyRequestType(owaServiceMessage.HttpRequest.Headers["RequestType"]);
			}
			return null;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00051554 File Offset: 0x0004F754
		private ExchangeVersion GetEwsVersionFromHttpHeaders(Message request, HttpRequest httpRequest)
		{
			ExchangeVersion result = null;
			string headerValue = httpRequest.Headers["X-EWS-TargetVersion"];
			ExchangeVersionHeader exchangeVersionHeader = new ExchangeVersionHeader(headerValue);
			if (!exchangeVersionHeader.IsMissing)
			{
				ExchangeVersionType version = exchangeVersionHeader.CheckAndGetRequestedVersion();
				result = new ExchangeVersion(version);
			}
			return result;
		}
	}
}
