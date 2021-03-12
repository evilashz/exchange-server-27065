using System;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B70 RID: 2928
	internal sealed class Timestamp
	{
		// Token: 0x06003EC7 RID: 16071 RVA: 0x000A4BB0 File Offset: 0x000A2DB0
		public Timestamp(string id, DateTime created, DateTime expires)
		{
			this.id = id;
			this.created = created;
			this.expires = expires;
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06003EC8 RID: 16072 RVA: 0x000A4BCD File Offset: 0x000A2DCD
		public DateTime Created
		{
			get
			{
				return this.created;
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x000A4BD5 File Offset: 0x000A2DD5
		public DateTime Expires
		{
			get
			{
				return this.expires;
			}
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x000A4BDD File Offset: 0x000A2DDD
		public static Timestamp Parse(XmlElement timestampXml)
		{
			return new Timestamp(null, Timestamp.GetDateTimeElement(timestampXml, WSSecurityUtility.Created), Timestamp.GetDateTimeElement(timestampXml, WSSecurityUtility.Expires));
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x000A4BFC File Offset: 0x000A2DFC
		public XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlAttribute xmlAttribute = WSSecurityUtility.Id.CreateAttribute(xmlDocument, this.id);
			XmlElement xmlElement = WSSecurityUtility.Created.CreateElement(xmlDocument, Timestamp.FormatDateTimeForSoap(this.created));
			XmlElement xmlElement2 = WSSecurityUtility.Expires.CreateElement(xmlDocument, Timestamp.FormatDateTimeForSoap(this.expires));
			return WSSecurityUtility.Timestamp.CreateElement(xmlDocument, new XmlAttribute[]
			{
				xmlAttribute
			}, new XmlElement[]
			{
				xmlElement,
				xmlElement2
			});
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x000A4C7C File Offset: 0x000A2E7C
		private static DateTime GetDateTimeElement(XmlElement parent, XmlElementDefinition elementDefinition)
		{
			XmlElement singleElementByName = elementDefinition.GetSingleElementByName(parent);
			if (singleElementByName == null)
			{
				Timestamp.Tracer.TraceError<XmlElementDefinition, string>(0L, "Failed to find XML element: {0} in this context {1}", elementDefinition, parent.OuterXml);
				throw new SoapXmlMalformedException(parent, elementDefinition);
			}
			DateTime result;
			if (!DateTime.TryParse(singleElementByName.InnerText, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result))
			{
				throw new SoapXmlMalformedException(parent, elementDefinition);
			}
			return result;
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x000A4CD3 File Offset: 0x000A2ED3
		private static string FormatDateTimeForSoap(DateTime dateTime)
		{
			return dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", CultureInfo.InvariantCulture);
		}

		// Token: 0x04003694 RID: 13972
		private static readonly Trace Tracer = ExTraceGlobals.WSTrustTracer;

		// Token: 0x04003695 RID: 13973
		private string id;

		// Token: 0x04003696 RID: 13974
		private DateTime created;

		// Token: 0x04003697 RID: 13975
		private DateTime expires;
	}
}
