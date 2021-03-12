using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000048 RID: 72
	public class PostData : Dictionary<string, string>
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x0000C099 File Offset: 0x0000A299
		public PostData()
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000C0A1 File Offset: 0x0000A2A1
		public PostData(Dictionary<string, string> dictionary) : base(dictionary)
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000C0AC File Offset: 0x0000A2AC
		public static PostData FromXml(XmlNode workContext)
		{
			PostData postData = new PostData();
			using (XmlNodeList xmlNodeList = workContext.SelectNodes("PostData"))
			{
				foreach (object obj in xmlNodeList)
				{
					XmlElement xmlElement = (XmlElement)obj;
					string key = Utils.CheckNullOrWhiteSpace(xmlElement.GetAttribute("Key"), "PostData Key");
					XmlAttribute xmlAttribute = xmlElement.Attributes["Value"];
					string value;
					if (xmlAttribute != null && !string.IsNullOrWhiteSpace(xmlAttribute.Value))
					{
						value = xmlAttribute.Value;
					}
					else
					{
						XmlNode xmlNode = xmlElement.SelectSingleNode("Value");
						Utils.CheckNode(xmlNode, "PostData Value");
						value = Utils.CheckNullOrWhiteSpace(xmlNode.InnerText, "PostData Value");
					}
					postData.Add(key, value);
				}
			}
			return postData;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000C1AC File Offset: 0x0000A3AC
		public void AddRange(Dictionary<string, string> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("Value of parameter dictionary cannot be null.");
			}
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				if (!base.ContainsKey(keyValuePair.Key))
				{
					base.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C224 File Offset: 0x0000A424
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000C230 File Offset: 0x0000A430
		public string ToString(bool formEncoded)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in this)
			{
				if (formEncoded)
				{
					string str = HttpUtility.HtmlDecode(keyValuePair.Value);
					stringBuilder.AppendFormat("{0}={1}&", keyValuePair.Key, HttpUtility.UrlEncode(str));
				}
				else
				{
					stringBuilder.Append(keyValuePair.Value);
				}
			}
			if (!formEncoded)
			{
				return stringBuilder.ToString();
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				'&'
			});
		}
	}
}
