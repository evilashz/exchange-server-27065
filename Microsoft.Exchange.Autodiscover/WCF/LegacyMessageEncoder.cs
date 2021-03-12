using System;
using System.Globalization;
using System.IO;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200006A RID: 106
	public class LegacyMessageEncoder : MessageEncoder
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x00013478 File Offset: 0x00011678
		public LegacyMessageEncoder(MessageVersion version)
		{
			this.version = version;
			this.Initialize();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00013490 File Offset: 0x00011690
		private void Initialize()
		{
			this.mediaType = "text/xml";
			this.contentType = string.Format(CultureInfo.InvariantCulture, "{0}; charset={1}", new object[]
			{
				this.mediaType,
				Encoding.UTF8.WebName
			});
			TextMessageEncodingBindingElement textMessageEncodingBindingElement = new TextMessageEncodingBindingElement(this.MessageVersion, Encoding.UTF8);
			MessageEncoderFactory messageEncoderFactory = textMessageEncodingBindingElement.CreateMessageEncoderFactory();
			this.textEncoder = messageEncoderFactory.Encoder;
			this.xmlSettings = new XmlWriterSettings();
			this.xmlSettings.Indent = true;
			this.xmlSettings.IndentChars = "  ";
			this.xmlSettings.OmitXmlDeclaration = false;
			this.xmlSettings.ConformanceLevel = ConformanceLevel.Document;
			this.xmlSettings.Encoding = LegacyMessageEncoder.utf8Encoding;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0001354E File Offset: 0x0001174E
		public override string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00013556 File Offset: 0x00011756
		public override string MediaType
		{
			get
			{
				return this.mediaType;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0001355E File Offset: 0x0001175E
		public override MessageVersion MessageVersion
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00013566 File Offset: 0x00011766
		public override bool IsContentTypeSupported(string contentType)
		{
			return base.IsContentTypeSupported(contentType) || (contentType.Trim().Length == this.MediaType.Length && contentType.Trim().Equals(this.MediaType, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00013608 File Offset: 0x00011808
		public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
		{
			MemoryStream stream = null;
			Common.SendWatsonReportOnUnhandledException(delegate
			{
				byte[] array = new byte[buffer.Count];
				Array.Copy(buffer.Array, buffer.Offset, array, 0, array.Length);
				bufferManager.ReturnBuffer(buffer.Array);
				stream = new MemoryStream(array);
			});
			return this.ReadMessage(stream, int.MaxValue, contentType);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00013730 File Offset: 0x00011930
		public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string streamContentType)
		{
			Message message = null;
			Common.SendWatsonReportOnUnhandledException(delegate
			{
				RequestData property;
				bool flag = this.ValidateAndParseRequest(stream, out property);
				MemoryStream memoryStream = new MemoryStream();
				XmlWriter xmlWriter = XmlWriter.Create(memoryStream);
				xmlWriter.WriteStartDocument();
				xmlWriter.WriteStartElement("Input");
				xmlWriter.WriteEndElement();
				xmlWriter.Flush();
				memoryStream.Flush();
				memoryStream.Seek(0L, SeekOrigin.Begin);
				message = this.textEncoder.ReadMessage(memoryStream, maxSizeOfHeaders, streamContentType);
				message.Properties.Add("ValidationError", this.validationError);
				message.Properties.Add("ParseSuccess", flag);
				message.Properties.Add("RequestData", property);
			});
			return message;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00013780 File Offset: 0x00011980
		public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
		{
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter xmlWriter = XmlWriter.Create(memoryStream, this.xmlSettings);
			message.WriteMessage(xmlWriter);
			xmlWriter.Flush();
			xmlWriter.Dispose();
			byte[] buffer = memoryStream.GetBuffer();
			int num = (int)memoryStream.Position;
			memoryStream.Dispose();
			int bufferSize = num + messageOffset;
			byte[] array = bufferManager.TakeBuffer(bufferSize);
			Array.Copy(buffer, 0, array, messageOffset, num);
			ArraySegment<byte> result = new ArraySegment<byte>(array, messageOffset, num);
			return result;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000137F4 File Offset: 0x000119F4
		public override void WriteMessage(Message message, Stream stream)
		{
			XmlWriter xmlWriter = XmlWriter.Create(stream, this.xmlSettings);
			message.WriteMessage(xmlWriter);
			xmlWriter.Flush();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001381C File Offset: 0x00011A1C
		private bool ValidateAndParseRequest(Stream stream, out RequestData requestData)
		{
			Stream stream2 = null;
			bool useClientCertificateAuthentication = Common.CheckClientCertificate(HttpContext.Current.Request);
			requestData = new RequestData(null, useClientCertificateAuthentication, CallerRequestedCapabilities.GetInstance(HttpContext.Current));
			string a = string.Empty;
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.CheckCharacters = true;
			xmlReaderSettings.IgnoreWhitespace = true;
			xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			xmlReaderSettings.Schemas = ProvidersTable.RequestSchemaSet;
			xmlReaderSettings.ValidationEventHandler += this.ValidationEventHandler;
			requestData.UserAuthType = HttpContext.Current.Request.ServerVariables["AUTH_TYPE"];
			string text = HttpContext.Current.Request.Headers.Get("X-MapiHttpCapability");
			int num;
			if (string.IsNullOrEmpty(text) || !int.TryParse(text, out num) || num < 0)
			{
				num = 0;
			}
			requestData.MapiHttpVersion = num;
			if (!requestData.CallerCapabilities.CanFollowRedirect)
			{
				requestData.ProxyRequestData = new ProxyRequestData(HttpContext.Current.Request, HttpContext.Current.Response, ref stream);
				stream2 = requestData.ProxyRequestData.RequestStream;
				stream = stream2;
			}
			this.validationError = false;
			try
			{
				XmlReader xmlReader = XmlReader.Create(stream, xmlReaderSettings);
				while (!xmlReader.EOF && !this.validationError)
				{
					switch (xmlReader.NodeType)
					{
					case XmlNodeType.Element:
						a = xmlReader.LocalName;
						while (xmlReader.MoveToNextAttribute())
						{
							if (xmlReader.Name == "xmlns")
							{
								requestData.RequestSchemas.Add(xmlReader.Value);
							}
						}
						break;
					case XmlNodeType.Text:
						if (xmlReader.HasValue)
						{
							if (a == "AcceptableResponseSchema")
							{
								requestData.ResponseSchemas.Add(xmlReader.Value);
							}
							else if (a == "EMailAddress")
							{
								requestData.EMailAddress = xmlReader.Value;
							}
							else if (a == "LegacyDN")
							{
								requestData.LegacyDN = xmlReader.Value;
							}
						}
						break;
					}
					xmlReader.Read();
				}
			}
			catch (XmlException ex)
			{
				ExTraceGlobals.FrameworkTracer.TraceError<string, int, int>((long)this.GetHashCode(), "[ValidateAndParseRequest()] Message=\"{0}\";LineNumber=\"{1}\";LinePosition=\"{2}\"", ex.Message, ex.LineNumber, ex.LinePosition);
				Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_WarnCoreValidationException, Common.PeriodicKey, new object[]
				{
					ex.Message
				});
				return false;
			}
			finally
			{
				if (stream2 != null)
				{
					stream2.Dispose();
				}
			}
			if (this.validationError)
			{
				return false;
			}
			if (string.IsNullOrEmpty(requestData.EMailAddress) && string.IsNullOrEmpty(requestData.LegacyDN))
			{
				ExTraceGlobals.FrameworkTracer.TraceError<string, string>((long)this.GetHashCode(), "[ValidateAndParseRequest()] 'Both \"{0}\" and \"{1}\" are empty'", "EMailAddress", "LegacyDN");
				Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_WarnCoreElementsAreEmpty, Common.PeriodicKey, new object[]
				{
					"EMailAddress,LegacyDN"
				});
			}
			else
			{
				if (requestData.ResponseSchemas.Count != 0 && !string.IsNullOrEmpty(requestData.ResponseSchemas[0]))
				{
					return true;
				}
				ExTraceGlobals.FrameworkTracer.TraceError<string>((long)this.GetHashCode(), "[ValidateAndParseRequest()] 'Element \"{0}\" is empty'", "AcceptableResponseSchema");
				Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_WarnCoreElementIsEmpty, Common.PeriodicKey, new object[]
				{
					"AcceptableResponseSchema"
				});
			}
			return false;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00013BB4 File Offset: 0x00011DB4
		private void ValidationEventHandler(object sender, ValidationEventArgs e)
		{
			ExTraceGlobals.FrameworkTracer.TraceError((long)this.GetHashCode(), "[ValidationEventHandler()] 'ValidationError' Severity=\"{0}\";Message=\"{1}\";LineNumber=\"{2}\";LinePosition=\"{3}\"", new object[]
			{
				e.Severity,
				e.Message,
				e.Exception.LineNumber,
				e.Exception.LinePosition
			});
			Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_WarnCoreValidationError, Common.PeriodicKey, new object[]
			{
				e.Severity.ToString(),
				e.Message,
				e.Exception.LineNumber.ToString(),
				e.Exception.LinePosition.ToString()
			});
			this.validationError = true;
		}

		// Token: 0x040002CE RID: 718
		private static readonly UTF8Encoding utf8Encoding = new UTF8Encoding(false);

		// Token: 0x040002CF RID: 719
		private MessageEncoder textEncoder;

		// Token: 0x040002D0 RID: 720
		private MessageVersion version;

		// Token: 0x040002D1 RID: 721
		private string contentType;

		// Token: 0x040002D2 RID: 722
		private string mediaType;

		// Token: 0x040002D3 RID: 723
		private bool validationError;

		// Token: 0x040002D4 RID: 724
		private XmlWriterSettings xmlSettings;
	}
}
