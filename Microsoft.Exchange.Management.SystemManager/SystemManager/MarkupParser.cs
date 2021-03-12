using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000042 RID: 66
	public class MarkupParser
	{
		// Token: 0x0600029C RID: 668 RVA: 0x00009F24 File Offset: 0x00008124
		public MarkupParser()
		{
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.LoadXml("<linkLabel></linkLabel>");
			this.xml = safeXmlDocument.DocumentElement;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00009F54 File Offset: 0x00008154
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00009F61 File Offset: 0x00008161
		public string Markup
		{
			get
			{
				return this.xml.InnerXml;
			}
			set
			{
				this.xml.InnerXml = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00009F70 File Offset: 0x00008170
		public string Text
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this.xml.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					stringBuilder.Append(xmlNode.InnerText);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00009FE0 File Offset: 0x000081E0
		public XmlNodeList Nodes
		{
			get
			{
				return this.xml.ChildNodes;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00009FF0 File Offset: 0x000081F0
		public void ReplaceAnchorValues(object dataSource, string listSeparator)
		{
			if (dataSource != null)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataSource);
				foreach (object obj in this.xml.SelectNodes("a"))
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (!string.IsNullOrEmpty(xmlNode.InnerText))
					{
						string value = xmlNode.Attributes["id"].Value;
						PropertyDescriptor propertyDescriptor = properties[value];
						if (propertyDescriptor != null)
						{
							object value2 = propertyDescriptor.GetValue(dataSource);
							if (!WinformsHelper.IsEmptyValue(value2))
							{
								xmlNode.InnerText = MarkupParser.ValueToString(value2, listSeparator);
							}
						}
					}
				}
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000A0B0 File Offset: 0x000082B0
		public Dictionary<string, bool> GetEditingProperties()
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			foreach (object obj in this.xml.SelectNodes("a"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				string value = xmlNode.Attributes["id"].Value;
				if (!string.IsNullOrEmpty(value) && !dictionary.ContainsKey(value))
				{
					dictionary.Add(value, !string.IsNullOrEmpty(xmlNode.InnerText));
				}
			}
			return dictionary;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A154 File Offset: 0x00008354
		public static string ValueToString(object value, string listSeparator)
		{
			string result = string.Empty;
			listSeparator = ((!string.IsNullOrEmpty(listSeparator)) ? listSeparator : CultureInfo.CurrentCulture.TextInfo.ListSeparator);
			if (value != null)
			{
				result = value.ToUserFriendText(listSeparator, new ObjectExtension.IsQuotationRequiredDelegate(MarkupParser.IsQuotationRequiredType));
			}
			return result;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A19C File Offset: 0x0000839C
		private static bool IsQuotationRequiredType(object value)
		{
			Type type = typeof(ICollection);
			if (value.GetType().IsGenericType)
			{
				type = typeof(ICollection<>).MakeGenericType(value.GetType().GetGenericArguments());
			}
			return !type.IsAssignableFrom(value.GetType());
		}

		// Token: 0x040000B2 RID: 178
		private XmlElement xml;
	}
}
