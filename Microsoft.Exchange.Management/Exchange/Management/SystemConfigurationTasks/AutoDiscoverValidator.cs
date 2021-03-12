using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200080D RID: 2061
	internal sealed class AutoDiscoverValidator : ServiceValidatorBase
	{
		// Token: 0x0600479A RID: 18330 RVA: 0x001261EF File Offset: 0x001243EF
		public AutoDiscoverValidator(string uri, NetworkCredential credentials, string emailAddress) : base(uri, credentials)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				throw new ArgumentNullException("emailAddress");
			}
			this.EmailAddress = emailAddress;
		}

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x0600479B RID: 18331 RVA: 0x00126213 File Offset: 0x00124413
		// (set) Token: 0x0600479C RID: 18332 RVA: 0x0012621B File Offset: 0x0012441B
		public AutoDiscoverValidator.ProviderSchema Provider { get; set; }

		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x0600479D RID: 18333 RVA: 0x00126224 File Offset: 0x00124424
		// (set) Token: 0x0600479E RID: 18334 RVA: 0x0012622C File Offset: 0x0012442C
		public string EmailAddress { get; private set; }

		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x0600479F RID: 18335 RVA: 0x00126235 File Offset: 0x00124435
		// (set) Token: 0x060047A0 RID: 18336 RVA: 0x0012623D File Offset: 0x0012443D
		public string EwsUrl { get; private set; }

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x060047A1 RID: 18337 RVA: 0x00126246 File Offset: 0x00124446
		// (set) Token: 0x060047A2 RID: 18338 RVA: 0x0012624E File Offset: 0x0012444E
		public string OabUrl { get; private set; }

		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x060047A3 RID: 18339 RVA: 0x00126257 File Offset: 0x00124457
		protected override string Name
		{
			get
			{
				return Strings.ServiceNameAutoDiscover;
			}
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x00126263 File Offset: 0x00124463
		protected override void PreCreateRequest()
		{
			base.PreCreateRequest();
			this.OabUrl = null;
			this.EwsUrl = null;
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x00126279 File Offset: 0x00124479
		protected override void FillRequestProperties(HttpWebRequest request)
		{
			base.FillRequestProperties(request);
			request.ContentType = "text/xml; charset=utf-8";
			request.Method = "POST";
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x00126298 File Offset: 0x00124498
		protected override bool FillRequestStream(Stream requestStream)
		{
			AutoDiscoverValidator.ProviderStrategy.GetStrategy(this.Provider).GenerateRequest(base.Uri, this.EmailAddress, requestStream);
			return true;
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x001262B8 File Offset: 0x001244B8
		protected override Exception ValidateResponse(Stream responseStream)
		{
			string ewsUrl;
			string oabUrl;
			Exception ex = AutoDiscoverValidator.ProviderStrategy.GetStrategy(this.Provider).ValidateResponse(responseStream, out ewsUrl, out oabUrl);
			this.EwsUrl = ewsUrl;
			this.OabUrl = oabUrl;
			if (ex != null)
			{
				return ex;
			}
			return ex;
		}

		// Token: 0x0200080E RID: 2062
		public enum ProviderSchema
		{
			// Token: 0x04002B60 RID: 11104
			Outlook,
			// Token: 0x04002B61 RID: 11105
			Soap
		}

		// Token: 0x0200080F RID: 2063
		private abstract class ProviderStrategy
		{
			// Token: 0x060047A8 RID: 18344 RVA: 0x001262EF File Offset: 0x001244EF
			public static AutoDiscoverValidator.ProviderStrategy GetStrategy(AutoDiscoverValidator.ProviderSchema schema)
			{
				return AutoDiscoverValidator.ProviderStrategy.factory[schema]();
			}

			// Token: 0x060047A9 RID: 18345
			public abstract void GenerateRequest(string url, string emailAddress, Stream requestStream);

			// Token: 0x060047AA RID: 18346
			public abstract Exception ValidateResponse(Stream responseStream, out string ewsUrl, out string oabUrl);

			// Token: 0x060047AC RID: 18348 RVA: 0x00126310 File Offset: 0x00124510
			// Note: this type is marked as 'beforefieldinit'.
			static ProviderStrategy()
			{
				Dictionary<AutoDiscoverValidator.ProviderSchema, Func<AutoDiscoverValidator.ProviderStrategy>> dictionary = new Dictionary<AutoDiscoverValidator.ProviderSchema, Func<AutoDiscoverValidator.ProviderStrategy>>();
				dictionary.Add(AutoDiscoverValidator.ProviderSchema.Outlook, () => new AutoDiscoverValidator.OutlookStrategy());
				dictionary.Add(AutoDiscoverValidator.ProviderSchema.Soap, () => new AutoDiscoverValidator.SoapStrategy());
				AutoDiscoverValidator.ProviderStrategy.factory = dictionary;
			}

			// Token: 0x04002B62 RID: 11106
			private static Dictionary<AutoDiscoverValidator.ProviderSchema, Func<AutoDiscoverValidator.ProviderStrategy>> factory;
		}

		// Token: 0x02000810 RID: 2064
		private class OutlookStrategy : AutoDiscoverValidator.ProviderStrategy
		{
			// Token: 0x060047AF RID: 18351 RVA: 0x0012637C File Offset: 0x0012457C
			public override void GenerateRequest(string url, string emailAddress, Stream requestStream)
			{
				SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(AutoDiscoverRequestXML));
				AutoDiscoverRequestXML o = AutoDiscoverRequestXML.NewRequest(emailAddress);
				safeXmlSerializer.Serialize(requestStream, o);
			}

			// Token: 0x060047B0 RID: 18352 RVA: 0x001263C4 File Offset: 0x001245C4
			public override Exception ValidateResponse(Stream responseStream, out string ewsUrl, out string oabUrl)
			{
				ewsUrl = null;
				oabUrl = null;
				SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(AutoDiscoverResponseXML));
				AutoDiscoverResponseXML autoDiscoverResponseXML = null;
				XDocument xdocument = null;
				try
				{
					autoDiscoverResponseXML = (AutoDiscoverResponseXML)safeXmlSerializer.Deserialize(responseStream);
					responseStream.Seek(0L, SeekOrigin.Begin);
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						xdocument = XDocument.Load(streamReader);
					}
				}
				catch (XmlException innerException)
				{
					return new ServiceValidatorException(Strings.ErrorInvalidResponseFormat, innerException);
				}
				if (autoDiscoverResponseXML.ErrorResponse != null)
				{
					if (autoDiscoverResponseXML.ErrorResponse.Error == null)
					{
						return new ServiceValidatorException(Strings.ErrorInvalidResponseXml(xdocument.ToString()));
					}
					return new ServiceValidatorException(Strings.ErrorResponseContainsError(autoDiscoverResponseXML.ErrorResponse.Error.ErrorCode, autoDiscoverResponseXML.ErrorResponse.Error.Message));
				}
				else
				{
					if (autoDiscoverResponseXML.DataResponse == null || autoDiscoverResponseXML.DataResponse.Account == null || autoDiscoverResponseXML.DataResponse.Account.Action == null)
					{
						return new ServiceValidatorException(Strings.ErrorInvalidResponseXml(xdocument.ToString()));
					}
					if (autoDiscoverResponseXML.DataResponse.Account.Action.Equals("redirectAddr", StringComparison.OrdinalIgnoreCase))
					{
						string redirectAddr = autoDiscoverResponseXML.DataResponse.Account.RedirectAddr;
						return new ServiceValidatorException(Strings.ErrorAutoDiscoverValidatorRequiresRedirection(redirectAddr));
					}
					if (autoDiscoverResponseXML.DataResponse.Account.Protocol == null)
					{
						return new ServiceValidatorException(Strings.ErrorInvalidResponseXml(xdocument.ToString()));
					}
					AutoDiscoverProtocol[] protocol = autoDiscoverResponseXML.DataResponse.Account.Protocol;
					string[] array = new string[]
					{
						"EXCH",
						"EXPR"
					};
					for (int i = 0; i < array.Length; i++)
					{
						string protocolType = array[i];
						AutoDiscoverProtocol autoDiscoverProtocol = Array.Find<AutoDiscoverProtocol>(protocol, (AutoDiscoverProtocol p) => p.Type.Equals(protocolType, StringComparison.OrdinalIgnoreCase));
						if (autoDiscoverProtocol != null)
						{
							if (!string.IsNullOrEmpty(autoDiscoverProtocol.EwsUrl))
							{
								ewsUrl = autoDiscoverProtocol.EwsUrl;
							}
							if (!string.IsNullOrEmpty(autoDiscoverProtocol.OABUrl))
							{
								oabUrl = autoDiscoverProtocol.OABUrl;
							}
						}
					}
					if (string.IsNullOrEmpty(ewsUrl))
					{
						return new ServiceValidatorException(Strings.ErrorAutoDiscoverValidatorEwsNotFound(xdocument.ToString()));
					}
					if (string.IsNullOrEmpty(oabUrl))
					{
						return new ServiceValidatorException(Strings.ErrorAutoDiscoverValidatorOabNotFound(xdocument.ToString()));
					}
					return null;
				}
				Exception result;
				return result;
			}
		}

		// Token: 0x02000811 RID: 2065
		private class SoapStrategy : AutoDiscoverValidator.ProviderStrategy
		{
			// Token: 0x060047B2 RID: 18354 RVA: 0x00126620 File Offset: 0x00124820
			static SoapStrategy()
			{
				AutoDiscoverValidator.SoapStrategy.namespaceManager.AddNamespace("a", AutoDiscoverValidator.SoapStrategy.a.NamespaceName);
				AutoDiscoverValidator.SoapStrategy.namespaceManager.AddNamespace("wsa", AutoDiscoverValidator.SoapStrategy.wsa.NamespaceName);
				AutoDiscoverValidator.SoapStrategy.namespaceManager.AddNamespace("xsi", AutoDiscoverValidator.SoapStrategy.xsi.NamespaceName);
				AutoDiscoverValidator.SoapStrategy.namespaceManager.AddNamespace("soap", AutoDiscoverValidator.SoapStrategy.soap.NamespaceName);
			}

			// Token: 0x060047B3 RID: 18355 RVA: 0x00126930 File Offset: 0x00124B30
			public override void GenerateRequest(string url, string emailAddress, Stream requestStream)
			{
				XDocument xdocument = new XDocument(AutoDiscoverValidator.SoapStrategy.RequestTemplate);
				XElement xelement = xdocument.XPathSelectElement(".//soap:Header/wsa:To", AutoDiscoverValidator.SoapStrategy.namespaceManager);
				xelement.Value = url;
				XElement xelement2 = xdocument.XPathSelectElement(".//soap:Body/a:GetUserSettingsRequestMessage/a:Request/a:Users/a:User/a:Mailbox", AutoDiscoverValidator.SoapStrategy.namespaceManager);
				xelement2.Value = emailAddress;
				using (StreamWriter streamWriter = new StreamWriter(requestStream))
				{
					streamWriter.Write(xdocument.Declaration.ToString() + "\r\n" + xdocument.ToString());
				}
			}

			// Token: 0x060047B4 RID: 18356 RVA: 0x001269BC File Offset: 0x00124BBC
			public override Exception ValidateResponse(Stream responseStream, out string ewsUrl, out string oabUrl)
			{
				ewsUrl = null;
				oabUrl = null;
				XDocument xdocument = null;
				try
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						xdocument = XDocument.Load(streamReader);
					}
				}
				catch (XmlException innerException)
				{
					return new ServiceValidatorException(Strings.ErrorInvalidResponseFormat, innerException);
				}
				Exception ex = this.CheckResponseErrors(xdocument);
				if (ex != null)
				{
					return ex;
				}
				XElement xelement = xdocument.XPathSelectElement(".//soap:Body/a:GetUserSettingsResponseMessage/a:Response/a:UserResponses/a:UserResponse/a:UserSettings/a:UserSetting[./a:Name=\"ExternalEwsUrl\"]", AutoDiscoverValidator.SoapStrategy.namespaceManager);
				if (xelement != null)
				{
					XElement xelement2 = xelement.XPathSelectElement("./a:Value", AutoDiscoverValidator.SoapStrategy.namespaceManager);
					if (xelement2 != null)
					{
						ewsUrl = xelement2.Value;
					}
				}
				if (string.IsNullOrEmpty(ewsUrl))
				{
					XElement xelement3 = xdocument.XPathSelectElement(".//soap:Body/a:GetUserSettingsResponseMessage/a:Response/a:UserResponses/a:UserResponse/a:UserSettings/a:UserSetting[./a:Name=\"InternalEwsUrl\"]", AutoDiscoverValidator.SoapStrategy.namespaceManager);
					if (xelement3 != null)
					{
						XElement xelement4 = xelement3.XPathSelectElement("./a:Value", AutoDiscoverValidator.SoapStrategy.namespaceManager);
						if (xelement4 != null)
						{
							ewsUrl = xelement4.Value;
						}
					}
				}
				if (string.IsNullOrEmpty(ewsUrl))
				{
					return new ServiceValidatorException(Strings.ErrorAutoDiscoverValidatorEwsNotFound(xdocument.ToString()));
				}
				return null;
			}

			// Token: 0x060047B5 RID: 18357 RVA: 0x00126ABC File Offset: 0x00124CBC
			private Exception CheckResponseErrors(XDocument responseXml)
			{
				XElement xelement = responseXml.XPathSelectElement(".//soap:Body/a:GetUserSettingsResponseMessage/a:Response/a:ErrorCode", AutoDiscoverValidator.SoapStrategy.namespaceManager);
				if (xelement == null)
				{
					return new ServiceValidatorException(Strings.ErrorInvalidResponseXml(responseXml.ToString()));
				}
				if (!string.Equals(xelement.Value, "NoError", StringComparison.OrdinalIgnoreCase))
				{
					XElement xelement2 = responseXml.XPathSelectElement(".//soap:Body/a:GetUserSettingsResponseMessage/a:Response/a:ErrorMessage", AutoDiscoverValidator.SoapStrategy.namespaceManager);
					if (xelement2 == null)
					{
						return new ServiceValidatorException(Strings.ErrorInvalidResponseXml(responseXml.ToString()));
					}
					return new ServiceValidatorException(Strings.ErrorResponseContainsError(xelement.Value, xelement2.Value));
				}
				else
				{
					XElement xelement3 = responseXml.XPathSelectElement(".//soap:Body/a:GetUserSettingsResponseMessage/a:Response/a:UserResponses/a:UserResponse/a:ErrorCode", AutoDiscoverValidator.SoapStrategy.namespaceManager);
					if (xelement3 == null)
					{
						return new ServiceValidatorException(Strings.ErrorInvalidResponseXml(responseXml.ToString()));
					}
					if (string.Equals(xelement3.Value, "NoError", StringComparison.OrdinalIgnoreCase))
					{
						return null;
					}
					XElement xelement4 = responseXml.XPathSelectElement(".//soap:Body/GetUserSettingsResponseMessage/a:Response/a:UserResponses/a:UserResponse/a:ErrorMessage", AutoDiscoverValidator.SoapStrategy.namespaceManager);
					if (xelement4 == null)
					{
						return new ServiceValidatorException(Strings.ErrorInvalidResponseXml(responseXml.ToString()));
					}
					return new ServiceValidatorException(Strings.ErrorResponseContainsError(xelement3.Value, xelement4.Value));
				}
			}

			// Token: 0x04002B65 RID: 11109
			private static readonly XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

			// Token: 0x04002B66 RID: 11110
			private static readonly XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";

			// Token: 0x04002B67 RID: 11111
			private static readonly XNamespace wsa = "http://www.w3.org/2005/08/addressing";

			// Token: 0x04002B68 RID: 11112
			private static readonly XNamespace a = "http://schemas.microsoft.com/exchange/2010/Autodiscover";

			// Token: 0x04002B69 RID: 11113
			private static readonly XmlNamespaceManager namespaceManager = new XmlNamespaceManager(new NameTable());

			// Token: 0x04002B6A RID: 11114
			private static readonly XDocument RequestTemplate = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new object[]
			{
				new XElement(AutoDiscoverValidator.SoapStrategy.soap + "Envelope", new object[]
				{
					new XAttribute(XNamespace.Xmlns + "a", AutoDiscoverValidator.SoapStrategy.a.NamespaceName),
					new XAttribute(XNamespace.Xmlns + "wsa", AutoDiscoverValidator.SoapStrategy.wsa.NamespaceName),
					new XAttribute(XNamespace.Xmlns + "xsi", AutoDiscoverValidator.SoapStrategy.xsi.NamespaceName),
					new XAttribute(XNamespace.Xmlns + "soap", AutoDiscoverValidator.SoapStrategy.soap.NamespaceName),
					new XElement(AutoDiscoverValidator.SoapStrategy.soap + "Header", new object[]
					{
						new XElement(AutoDiscoverValidator.SoapStrategy.a + "RequestedServerVersion", "Exchange2010"),
						new XElement(AutoDiscoverValidator.SoapStrategy.wsa + "Action", "http://schemas.microsoft.com/exchange/2010/Autodiscover/Autodiscover/GetUserSettings"),
						new XElement(AutoDiscoverValidator.SoapStrategy.wsa + "To", "url-placeholder")
					}),
					new XElement(AutoDiscoverValidator.SoapStrategy.soap + "Body", new XElement(AutoDiscoverValidator.SoapStrategy.a + "GetUserSettingsRequestMessage", new object[]
					{
						new XAttribute(XNamespace.Xmlns + "a", AutoDiscoverValidator.SoapStrategy.a.NamespaceName),
						new XElement(AutoDiscoverValidator.SoapStrategy.a + "Request", new object[]
						{
							new XElement(AutoDiscoverValidator.SoapStrategy.a + "Users", new XElement(AutoDiscoverValidator.SoapStrategy.a + "User", new XElement(AutoDiscoverValidator.SoapStrategy.a + "Mailbox", "email-address-placeholder"))),
							new XElement(AutoDiscoverValidator.SoapStrategy.a + "RequestedSettings", new object[]
							{
								new XElement(AutoDiscoverValidator.SoapStrategy.a + "Setting", "ExternalEwsUrl"),
								new XElement(AutoDiscoverValidator.SoapStrategy.a + "Setting", "InternalEwsUrl")
							})
						})
					}))
				})
			});
		}
	}
}
