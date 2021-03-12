using System;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000A7 RID: 167
	internal class AutodiscoverProxyRequestHandler : EwsAutodiscoverProxyRequestHandler
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x000251D6 File Offset: 0x000233D6
		protected override ClientAccessType ClientAccessType
		{
			get
			{
				return ClientAccessType.InternalNLBBypass;
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000251DC File Offset: 0x000233DC
		protected override void OnInitializingHandler()
		{
			base.OnInitializingHandler();
			if (!base.ClientRequest.IsAuthenticated)
			{
				base.IsWsSecurityRequest = base.ClientRequest.IsAnyWsSecurityRequest();
				if (base.IsWsSecurityRequest && !AutodiscoverEwsWebConfiguration.WsSecurityEndpointEnabled)
				{
					throw new HttpException(404, "WS-Security endpoint is not supported");
				}
			}
			if (base.ClientRequest.Url.ToString().EndsWith("autodiscover.xml", StringComparison.OrdinalIgnoreCase) || ProtocolHelper.IsAutodiscoverV2Request(base.ClientRequest.Url.AbsolutePath))
			{
				base.PreferAnchorMailboxHeader = true;
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00025268 File Offset: 0x00023468
		protected override void DoProtocolSpecificBeginProcess()
		{
			if (!base.ClientRequest.IsAuthenticated)
			{
				try
				{
					if (this.IsSimpleSoapRequest())
					{
						base.ParseClientRequest<bool>(new Func<Stream, bool>(this.ParseRequest), 81820);
					}
				}
				catch (FormatException innerException)
				{
					throw new HttpException(400, "FormatException parsing Autodiscover request", innerException);
				}
				catch (XmlException innerException2)
				{
					throw new HttpException(400, "XmlException parsing Autodiscover request", innerException2);
				}
				if (!base.IsWsSecurityRequest && !base.IsDomainBasedRequest && !ProtocolHelper.IsAutodiscoverV2Request(base.ClientRequest.Url.AbsolutePath))
				{
					throw new HttpProxyException(HttpStatusCode.Unauthorized, HttpProxySubErrorCode.UnauthenticatedRequest, "Unauthenticated AutoDiscover request.");
				}
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00025324 File Offset: 0x00023524
		protected override void AddProtocolSpecificHeadersToServerRequest(WebHeaderCollection headers)
		{
			if (base.ClientRequest.IsAuthenticated && base.ProxyToDownLevel)
			{
				headers["X-AutodiscoverProxySecurityContext"] = base.HttpContext.GetSerializedAccessTokenString();
			}
			base.AddProtocolSpecificHeadersToServerRequest(headers);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00025358 File Offset: 0x00023558
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			if (AutodiscoverProxyRequestHandler.LoadBalancedPartnerRouting.Value && base.ClientRequest.Url.AbsolutePath.ToLower().Contains("/wssecurity/x509cert"))
			{
				string text = base.ClientRequest.Headers[Constants.AnchorMailboxHeaderName];
				string text2 = null;
				if (string.IsNullOrEmpty(text))
				{
					AnchorMailbox anchorMailbox = base.TryGetAnchorMailboxFromWsSecurityRequest();
					if (anchorMailbox != null)
					{
						SmtpAnchorMailbox smtpAnchorMailbox = anchorMailbox as SmtpAnchorMailbox;
						if (smtpAnchorMailbox != null)
						{
							text2 = smtpAnchorMailbox.Smtp;
						}
						else
						{
							DomainAnchorMailbox domainAnchorMailbox = anchorMailbox as DomainAnchorMailbox;
							if (domainAnchorMailbox != null)
							{
								text2 = domainAnchorMailbox.Domain;
							}
						}
					}
				}
				else
				{
					text2 = text;
				}
				if (!string.IsNullOrEmpty(text2))
				{
					bool flag = text2.EndsWith(AutodiscoverProxyRequestHandler.BlackBerryTenantName.Value, StringComparison.OrdinalIgnoreCase);
					if (flag)
					{
						base.Logger.SafeSet(HttpProxyMetadata.RoutingHint, "PartnerX509Request");
						return new TargetForestAnchorMailbox(this, AutodiscoverProxyRequestHandler.BlackBerryTenantName.Value, false);
					}
				}
			}
			return base.ResolveAnchorMailbox();
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0002543A File Offset: 0x0002363A
		protected override bool ShouldCopyHeaderToServerRequest(string headerName)
		{
			return !string.Equals(headerName, "X-AutodiscoverProxySecurityContext", StringComparison.OrdinalIgnoreCase) && base.ShouldCopyHeaderToServerRequest(headerName);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00025454 File Offset: 0x00023654
		protected override Uri UpdateExternalRedirectUrl(Uri originalRedirectUrl)
		{
			return new UriBuilder(base.ClientRequest.Url)
			{
				Host = originalRedirectUrl.Host,
				Port = originalRedirectUrl.Port
			}.Uri;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00025490 File Offset: 0x00023690
		private bool IsSimpleSoapRequest()
		{
			return base.ClientRequest.Url.LocalPath.EndsWith(".svc", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000254B0 File Offset: 0x000236B0
		private bool ParseRequest(Stream stream)
		{
			long position = stream.Position;
			XmlReader xmlReader = XmlReader.Create(stream);
			if (xmlReader.Settings != null && xmlReader.Settings.DtdProcessing != DtdProcessing.Prohibit)
			{
				xmlReader.Settings.DtdProcessing = DtdProcessing.Prohibit;
			}
			XmlElement xmlElement = null;
			xmlReader.MoveToContent();
			bool flag = true;
			while (flag && xmlReader.Read())
			{
				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "Header")
				{
					if (!(xmlReader.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/"))
					{
						if (!(xmlReader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope"))
						{
							continue;
						}
					}
					while (flag && xmlReader.Read())
					{
						if (stream.Position > 81820L)
						{
							throw new System.ServiceModel.QuotaExceededException();
						}
						if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.LocalName == "Header" && (xmlReader.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/" || xmlReader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope"))
						{
							ExTraceGlobals.VerboseTracer.TraceDebug(0L, "[AutodiscoverProxyModule::ParseRequest]: Hit the end of the SOAP header unexpectedly");
							flag = false;
							break;
						}
						if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "Action" && xmlReader.NamespaceURI == "http://www.w3.org/2005/08/addressing")
						{
							using (XmlReader xmlReader2 = xmlReader.ReadSubtree())
							{
								XmlDocument xmlDocument = new XmlDocument();
								xmlDocument.Load(xmlReader2);
								XmlElement documentElement = xmlDocument.DocumentElement;
								if (documentElement == null)
								{
									flag = false;
									break;
								}
								if (!documentElement.InnerText.Trim().EndsWith("/GetFederationInformation", StringComparison.OrdinalIgnoreCase))
								{
									if (documentElement.InnerText.Trim().EndsWith("/GetOrganizationRelationshipSettings", StringComparison.OrdinalIgnoreCase))
									{
										base.SkipTargetBackEndCalculation = true;
									}
									flag = false;
									break;
								}
							}
							base.IsDomainBasedRequest = true;
							while (flag && xmlReader.Read())
							{
								if (stream.Position > 81820L)
								{
									throw new System.ServiceModel.QuotaExceededException();
								}
								if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "Body")
								{
									if (!(xmlReader.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/"))
									{
										if (!(xmlReader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope"))
										{
											continue;
										}
									}
									while (flag && xmlReader.Read())
									{
										if (stream.Position > 81820L)
										{
											throw new System.ServiceModel.QuotaExceededException();
										}
										if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.LocalName == "Body" && (xmlReader.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/" || xmlReader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope"))
										{
											ExTraceGlobals.VerboseTracer.TraceDebug(0L, "[AutodiscoverProxyModule::ParseRequest]: Hit the end of the SOAP body unexpectedly");
											flag = false;
											break;
										}
										if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "GetFederationInformationRequestMessage")
										{
											using (XmlReader xmlReader3 = xmlReader.ReadSubtree())
											{
												XmlDocument xmlDocument2 = new XmlDocument();
												xmlDocument2.Load(xmlReader3);
												xmlElement = xmlDocument2.DocumentElement;
											}
											flag = false;
											break;
										}
									}
								}
							}
						}
					}
				}
			}
			if (xmlElement != null && xmlElement.ChildNodes != null)
			{
				foreach (object obj in xmlElement.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.LocalName == "Request" && xmlNode.FirstChild != null && xmlNode.FirstChild.LocalName == "Domain")
					{
						base.Domain = xmlNode.FirstChild.InnerText;
					}
				}
			}
			return true;
		}

		// Token: 0x04000416 RID: 1046
		internal const string AutodiscoverProxySecurityContext = "X-AutodiscoverProxySecurityContext";

		// Token: 0x04000417 RID: 1047
		private const string GetFederationInformationAction = "/GetFederationInformation";

		// Token: 0x04000418 RID: 1048
		private const string GetOrganizationRelationshipSettingsAction = "/GetOrganizationRelationshipSettings";

		// Token: 0x04000419 RID: 1049
		private const string X509CertUrlSuffix = "/wssecurity/x509cert";

		// Token: 0x0400041A RID: 1050
		private const string SoapRequestEnd = ".svc";

		// Token: 0x0400041B RID: 1051
		private const int MaxSizeOfDomainBasedRequest = 81820;

		// Token: 0x0400041C RID: 1052
		private static readonly StringAppSettingsEntry BlackBerryTenantName = new StringAppSettingsEntry(HttpProxySettings.Prefix("BlackBerryTenantName"), "service.businesscloud.blackberry.com", ExTraceGlobals.VerboseTracer);

		// Token: 0x0400041D RID: 1053
		private static readonly BoolAppSettingsEntry LoadBalancedPartnerRouting = new BoolAppSettingsEntry(HttpProxySettings.Prefix("LoadBalancedPartnerRouting"), VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.LoadBalancedPartnerRouting.Enabled, ExTraceGlobals.VerboseTracer);
	}
}
