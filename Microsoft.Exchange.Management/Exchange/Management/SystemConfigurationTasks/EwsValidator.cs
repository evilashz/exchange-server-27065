using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000812 RID: 2066
	internal class EwsValidator : ServiceValidatorBase
	{
		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x060047B7 RID: 18359 RVA: 0x00126BBA File Offset: 0x00124DBA
		// (set) Token: 0x060047B8 RID: 18360 RVA: 0x00126BC2 File Offset: 0x00124DC2
		public Guid PublicFolderMailboxGuid { get; set; }

		// Token: 0x060047B9 RID: 18361 RVA: 0x00126BCB File Offset: 0x00124DCB
		public EwsValidator(string uri, NetworkCredential credentials) : base(uri, credentials)
		{
		}

		// Token: 0x060047BA RID: 18362 RVA: 0x00126BD5 File Offset: 0x00124DD5
		public EwsValidator(string uri, NetworkCredential credentials, string emailAddress) : base(uri, credentials)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				throw new ArgumentNullException("emailAddress");
			}
			this.EmailAddress = emailAddress;
		}

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x060047BB RID: 18363 RVA: 0x00126BF9 File Offset: 0x00124DF9
		// (set) Token: 0x060047BC RID: 18364 RVA: 0x00126C01 File Offset: 0x00124E01
		public EwsValidator.RequestOperation Operation { get; set; }

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x060047BD RID: 18365 RVA: 0x00126C0A File Offset: 0x00124E0A
		protected override string Name
		{
			get
			{
				return (this.Operation == EwsValidator.RequestOperation.GetUserAvailability) ? Strings.ServiceNameAs : Strings.ServiceNameEws;
			}
		}

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x060047BE RID: 18366 RVA: 0x00126C26 File Offset: 0x00124E26
		// (set) Token: 0x060047BF RID: 18367 RVA: 0x00126C2E File Offset: 0x00124E2E
		public string EmailAddress { get; set; }

		// Token: 0x060047C0 RID: 18368 RVA: 0x00126C38 File Offset: 0x00124E38
		protected override void FillRequestProperties(HttpWebRequest request)
		{
			base.FillRequestProperties(request);
			request.Method = "POST";
			request.Accept = "text/xml";
			if (this.PublicFolderMailboxGuid != Guid.Empty)
			{
				request.Headers[WellKnownHeader.PublicFolderMailbox] = this.PublicFolderMailboxGuid.ToString();
			}
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x00126C98 File Offset: 0x00124E98
		protected override bool FillRequestStream(Stream requestStream)
		{
			EwsValidator.OperationStrategy.GetStrategy(this.Operation).GenerateRequest(requestStream, this.EmailAddress, this.PublicFolderMailboxGuid != Guid.Empty);
			return true;
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x00126CC2 File Offset: 0x00124EC2
		protected override Exception ValidateResponse(Stream responseStream)
		{
			return EwsValidator.OperationStrategy.GetStrategy(this.Operation).ValidateResponse(responseStream);
		}

		// Token: 0x02000813 RID: 2067
		public enum RequestOperation
		{
			// Token: 0x04002B6F RID: 11119
			ConvertId,
			// Token: 0x04002B70 RID: 11120
			GetFolder,
			// Token: 0x04002B71 RID: 11121
			GetUserAvailability
		}

		// Token: 0x02000814 RID: 2068
		private abstract class OperationStrategy
		{
			// Token: 0x060047C3 RID: 18371 RVA: 0x00126CEC File Offset: 0x00124EEC
			static OperationStrategy()
			{
				Dictionary<EwsValidator.RequestOperation, Func<EwsValidator.OperationStrategy>> dictionary = new Dictionary<EwsValidator.RequestOperation, Func<EwsValidator.OperationStrategy>>();
				dictionary.Add(EwsValidator.RequestOperation.ConvertId, () => new EwsValidator.ConvertIdStrategy());
				dictionary.Add(EwsValidator.RequestOperation.GetFolder, () => new EwsValidator.GetFolderStrategy());
				dictionary.Add(EwsValidator.RequestOperation.GetUserAvailability, () => new EwsValidator.GetUserAvailabilityStrategy());
				EwsValidator.OperationStrategy.factory = dictionary;
				EwsValidator.OperationStrategy.namespaceManager.AddNamespace("xsi", EwsValidator.OperationStrategy.xsi.NamespaceName);
				EwsValidator.OperationStrategy.namespaceManager.AddNamespace("soap", EwsValidator.OperationStrategy.soap.NamespaceName);
				EwsValidator.OperationStrategy.namespaceManager.AddNamespace("t", EwsValidator.OperationStrategy.t.NamespaceName);
				EwsValidator.OperationStrategy.namespaceManager.AddNamespace("m", EwsValidator.OperationStrategy.m.NamespaceName);
			}

			// Token: 0x060047C4 RID: 18372 RVA: 0x00126E20 File Offset: 0x00125020
			public static EwsValidator.OperationStrategy GetStrategy(EwsValidator.RequestOperation operation)
			{
				return EwsValidator.OperationStrategy.factory[operation]();
			}

			// Token: 0x060047C5 RID: 18373 RVA: 0x00126E34 File Offset: 0x00125034
			public void GenerateRequest(Stream requestStream, string emailAddress, bool isPublicFolderMailbox)
			{
				using (StreamWriter streamWriter = new StreamWriter(requestStream))
				{
					XDocument requestXml = this.GetRequestXml(emailAddress, isPublicFolderMailbox);
					streamWriter.Write(requestXml.Declaration.ToString() + "\r\n" + requestXml.ToString());
				}
			}

			// Token: 0x060047C6 RID: 18374 RVA: 0x00126E90 File Offset: 0x00125090
			public Exception ValidateResponse(Stream responseStream)
			{
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
				XElement xelement = xdocument.XPathSelectElement(this.ResponseMessageElementPath, EwsValidator.OperationStrategy.namespaceManager);
				if (xelement == null || xelement.FirstAttribute == null || xelement.FirstAttribute.Name == null || !string.Equals(xelement.FirstAttribute.Name.LocalName, "ResponseClass"))
				{
					return new ServiceValidatorException(Strings.ErrorInvalidResponseXml(xdocument.ToString()));
				}
				XElement xelement2 = xelement.XPathSelectElement(".//m:ResponseCode", EwsValidator.OperationStrategy.namespaceManager);
				if (xelement2 == null)
				{
					return new ServiceValidatorException(Strings.ErrorInvalidResponseXml(xdocument.ToString()));
				}
				string value = xelement.FirstAttribute.Value;
				if (!value.Equals("Error"))
				{
					return null;
				}
				XElement xelement3 = xelement.XPathSelectElement(".//m:MessageText", EwsValidator.OperationStrategy.namespaceManager);
				if (xelement3 == null)
				{
					return new ServiceValidatorException(Strings.ErrorInvalidResponseXml(xdocument.ToString()));
				}
				return new ServiceValidatorException(Strings.ErrorResponseContainsError(xelement2.Value, xelement3.Value));
			}

			// Token: 0x060047C7 RID: 18375
			protected abstract XDocument GetRequestXml(string emailAddress, bool isPublicFolderMailbox);

			// Token: 0x170015B0 RID: 5552
			// (get) Token: 0x060047C8 RID: 18376
			protected abstract string ResponseMessageElementPath { get; }

			// Token: 0x04002B72 RID: 11122
			protected static readonly XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

			// Token: 0x04002B73 RID: 11123
			protected static readonly XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";

			// Token: 0x04002B74 RID: 11124
			protected static readonly XNamespace t = "http://schemas.microsoft.com/exchange/services/2006/types";

			// Token: 0x04002B75 RID: 11125
			protected static readonly XNamespace m = "http://schemas.microsoft.com/exchange/services/2006/messages";

			// Token: 0x04002B76 RID: 11126
			protected static readonly XmlNamespaceManager namespaceManager = new XmlNamespaceManager(new NameTable());

			// Token: 0x04002B77 RID: 11127
			private static Dictionary<EwsValidator.RequestOperation, Func<EwsValidator.OperationStrategy>> factory;
		}

		// Token: 0x02000815 RID: 2069
		private class ConvertIdStrategy : EwsValidator.OperationStrategy
		{
			// Token: 0x060047CD RID: 18381 RVA: 0x00126FD8 File Offset: 0x001251D8
			protected override XDocument GetRequestXml(string emailAddress, bool isPublicFolderMailbox)
			{
				return EwsValidator.ConvertIdStrategy.RequestTemplate;
			}

			// Token: 0x170015B1 RID: 5553
			// (get) Token: 0x060047CE RID: 18382 RVA: 0x00126FDF File Offset: 0x001251DF
			protected override string ResponseMessageElementPath
			{
				get
				{
					return ".//soap:Body/m:ConvertIdResponse/m:ResponseMessages/m:ConvertIdResponseMessage";
				}
			}

			// Token: 0x04002B7B RID: 11131
			private static readonly XDocument RequestTemplate = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new object[]
			{
				new XElement(EwsValidator.OperationStrategy.soap + "Envelope", new object[]
				{
					new XAttribute(XNamespace.Xmlns + "xsi", EwsValidator.OperationStrategy.xsi.NamespaceName),
					new XAttribute(XNamespace.Xmlns + "m", EwsValidator.OperationStrategy.m.NamespaceName),
					new XAttribute(XNamespace.Xmlns + "t", EwsValidator.OperationStrategy.t.NamespaceName),
					new XAttribute(XNamespace.Xmlns + "soap", EwsValidator.OperationStrategy.soap.NamespaceName),
					new XElement(EwsValidator.OperationStrategy.soap + "Header", new XElement(EwsValidator.OperationStrategy.t + "RequestServerVersion", new XAttribute("Version", "Exchange2007_SP1"))),
					new XElement(EwsValidator.OperationStrategy.soap + "Body", new XElement(EwsValidator.OperationStrategy.m + "ConvertId", new object[]
					{
						new XAttribute("DestinationFormat", "OwaId"),
						new XElement(EwsValidator.OperationStrategy.m + "SourceIds", new XElement(EwsValidator.OperationStrategy.t + "AlternateId", new object[]
						{
							new XAttribute("Format", "EntryId"),
							new XAttribute("Id", "AAAAAJrt68tcvoJHj4EZX4tVxJkHAJkQvNaLMepFoct50Pnbm4gAAAHZbQQAAF1I2v4S5IFMi6vJDIXLpOEAAAAI6ocAAA=="),
							new XAttribute("Mailbox", "nobody@contoso.com")
						}))
					}))
				})
			});
		}

		// Token: 0x02000816 RID: 2070
		private class GetFolderStrategy : EwsValidator.OperationStrategy
		{
			// Token: 0x060047D1 RID: 18385 RVA: 0x001271C8 File Offset: 0x001253C8
			private XDocument GetRequestTemplate(bool isPublicFolderMailbox)
			{
				XElement content;
				if (isPublicFolderMailbox)
				{
					content = new XElement(EwsValidator.OperationStrategy.t + "DistinguishedFolderId", new XAttribute("Id", "publicfoldersroot"));
				}
				else
				{
					content = new XElement(EwsValidator.OperationStrategy.t + "DistinguishedFolderId", new object[]
					{
						new XElement(EwsValidator.OperationStrategy.t + "Mailbox", new XElement(EwsValidator.OperationStrategy.t + "EmailAddress", "email-address-placeholder")),
						new XAttribute("Id", "inbox")
					});
				}
				return new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new object[]
				{
					new XElement(EwsValidator.OperationStrategy.soap + "Envelope", new object[]
					{
						new XAttribute(XNamespace.Xmlns + "xsi", EwsValidator.OperationStrategy.xsi.NamespaceName),
						new XAttribute(XNamespace.Xmlns + "m", EwsValidator.OperationStrategy.m.NamespaceName),
						new XAttribute(XNamespace.Xmlns + "t", EwsValidator.OperationStrategy.t.NamespaceName),
						new XAttribute(XNamespace.Xmlns + "soap", EwsValidator.OperationStrategy.soap.NamespaceName),
						new XElement(EwsValidator.OperationStrategy.soap + "Header", new XElement(EwsValidator.OperationStrategy.t + "RequestServerVersion", new XAttribute("Version", "Exchange2007_SP1"))),
						new XElement(EwsValidator.OperationStrategy.soap + "Body", new XElement(EwsValidator.OperationStrategy.m + "GetFolder", new object[]
						{
							new XElement(EwsValidator.OperationStrategy.m + "FolderShape", new XElement(EwsValidator.OperationStrategy.t + "BaseShape", "IdOnly")),
							new XElement(EwsValidator.OperationStrategy.m + "FolderIds", content)
						}))
					})
				});
			}

			// Token: 0x060047D2 RID: 18386 RVA: 0x001273EC File Offset: 0x001255EC
			protected override XDocument GetRequestXml(string emailAddress, bool isPublicFolderMailbox)
			{
				if (string.IsNullOrEmpty(emailAddress))
				{
					throw new ArgumentNullException("emailAddress");
				}
				XDocument requestTemplate = this.GetRequestTemplate(isPublicFolderMailbox);
				if (!isPublicFolderMailbox)
				{
					XElement xelement = requestTemplate.XPathSelectElement(".//soap:Body/m:GetFolder/m:FolderIds/t:DistinguishedFolderId/t:Mailbox/t:EmailAddress", EwsValidator.OperationStrategy.namespaceManager);
					xelement.Value = emailAddress;
				}
				return requestTemplate;
			}

			// Token: 0x170015B2 RID: 5554
			// (get) Token: 0x060047D3 RID: 18387 RVA: 0x00127430 File Offset: 0x00125630
			protected override string ResponseMessageElementPath
			{
				get
				{
					return ".//soap:Body/m:GetFolderResponse/m:ResponseMessages/m:GetFolderResponseMessage";
				}
			}
		}

		// Token: 0x02000817 RID: 2071
		private class GetUserAvailabilityStrategy : EwsValidator.OperationStrategy
		{
			// Token: 0x060047D5 RID: 18389 RVA: 0x00127440 File Offset: 0x00125640
			protected override XDocument GetRequestXml(string emailAddress, bool isPublicFolderMailbox)
			{
				if (string.IsNullOrEmpty(emailAddress))
				{
					throw new ArgumentNullException("emailAddress");
				}
				XDocument xdocument = new XDocument(EwsValidator.GetUserAvailabilityStrategy.RequestTemplate);
				XElement xelement = xdocument.XPathSelectElement(".//soap:Body/m:GetUserAvailabilityRequest/m:MailboxDataArray/t:MailboxData/t:Email/t:Address", EwsValidator.OperationStrategy.namespaceManager);
				xelement.Value = emailAddress;
				DateTime dateTime = DateTime.Today.AddDays(2.0);
				DateTime dateTime2 = dateTime.AddHours(1.0);
				XElement xelement2 = xdocument.XPathSelectElement(".//soap:Body/m:GetUserAvailabilityRequest/t:FreeBusyViewOptions/t:TimeWindow/t:StartTime", EwsValidator.OperationStrategy.namespaceManager);
				xelement2.Value = dateTime.ToString("s");
				XElement xelement3 = xdocument.XPathSelectElement(".//soap:Body/m:GetUserAvailabilityRequest/t:FreeBusyViewOptions/t:TimeWindow/t:EndTime", EwsValidator.OperationStrategy.namespaceManager);
				xelement3.Value = dateTime2.ToString("s");
				return xdocument;
			}

			// Token: 0x170015B3 RID: 5555
			// (get) Token: 0x060047D6 RID: 18390 RVA: 0x001274F7 File Offset: 0x001256F7
			protected override string ResponseMessageElementPath
			{
				get
				{
					return ".//soap:Body/m:GetUserAvailabilityResponse/m:FreeBusyResponseArray/m:FreeBusyResponse/m:ResponseMessage";
				}
			}

			// Token: 0x04002B7C RID: 11132
			private static readonly XDocument RequestTemplate = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new object[]
			{
				new XElement(EwsValidator.OperationStrategy.soap + "Envelope", new object[]
				{
					new XAttribute(XNamespace.Xmlns + "xsi", EwsValidator.OperationStrategy.xsi.NamespaceName),
					new XAttribute(XNamespace.Xmlns + "m", EwsValidator.OperationStrategy.m.NamespaceName),
					new XAttribute(XNamespace.Xmlns + "t", EwsValidator.OperationStrategy.t.NamespaceName),
					new XAttribute(XNamespace.Xmlns + "soap", EwsValidator.OperationStrategy.soap.NamespaceName),
					new XElement(EwsValidator.OperationStrategy.soap + "Header", new object[]
					{
						new XElement(EwsValidator.OperationStrategy.t + "RequestServerVersion", new XAttribute("Version", "Exchange2010")),
						new XElement(EwsValidator.OperationStrategy.t + "TimeZoneContext", new XElement(EwsValidator.OperationStrategy.t + "TimeZoneDefinition", new object[]
						{
							new XAttribute("Name", "(UTC-08:00) Pacific Time (US & Canada)"),
							new XAttribute("Id", "Pacific Standard Time")
						}))
					}),
					new XElement(EwsValidator.OperationStrategy.soap + "Body", new XElement(EwsValidator.OperationStrategy.m + "GetUserAvailabilityRequest", new object[]
					{
						new XElement(EwsValidator.OperationStrategy.m + "MailboxDataArray", new XElement(EwsValidator.OperationStrategy.t + "MailboxData", new object[]
						{
							new XElement(EwsValidator.OperationStrategy.t + "Email", new XElement(EwsValidator.OperationStrategy.t + "Address", "email-address-placeholder")),
							new XElement(EwsValidator.OperationStrategy.t + "AttendeeType", "Required"),
							new XElement(EwsValidator.OperationStrategy.t + "ExcludeConflicts", "false")
						})),
						new XElement(EwsValidator.OperationStrategy.t + "FreeBusyViewOptions", new object[]
						{
							new XElement(EwsValidator.OperationStrategy.t + "TimeWindow", new object[]
							{
								new XElement(EwsValidator.OperationStrategy.t + "StartTime", "start-time-placeholder"),
								new XElement(EwsValidator.OperationStrategy.t + "EndTime", "end-time-placeholder")
							}),
							new XElement(EwsValidator.OperationStrategy.t + "MergedFreeBusyIntervalInMinutes", "30"),
							new XElement(EwsValidator.OperationStrategy.t + "RequestedView", "Detailed")
						})
					}))
				})
			});
		}
	}
}
