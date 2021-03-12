using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200029D RID: 669
	public class ResponseCapture
	{
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x00047828 File Offset: 0x00045A28
		// (set) Token: 0x0600165D RID: 5725 RVA: 0x00047830 File Offset: 0x00045A30
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x00047839 File Offset: 0x00045A39
		// (set) Token: 0x0600165F RID: 5727 RVA: 0x00047841 File Offset: 0x00045A41
		public string Pattern
		{
			get
			{
				return this.pattern;
			}
			set
			{
				this.pattern = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x0004784A File Offset: 0x00045A4A
		// (set) Token: 0x06001661 RID: 5729 RVA: 0x00047852 File Offset: 0x00045A52
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0004785B File Offset: 0x00045A5B
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x00047863 File Offset: 0x00045A63
		public CaptureType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x0004786C File Offset: 0x00045A6C
		// (set) Token: 0x06001665 RID: 5733 RVA: 0x00047874 File Offset: 0x00045A74
		public string PersistInStateAttributeName
		{
			get
			{
				return this.persistInStateAttributeName;
			}
			set
			{
				this.persistInStateAttributeName = value;
			}
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00047880 File Offset: 0x00045A80
		public static ICollection<ResponseCapture> FromXml(XmlNode workContext)
		{
			List<ResponseCapture> list = new List<ResponseCapture>();
			using (XmlNodeList xmlNodeList = workContext.SelectNodes("Capture"))
			{
				foreach (object obj in xmlNodeList)
				{
					XmlElement xmlElement = (XmlElement)obj;
					ResponseCapture responseCapture = new ResponseCapture();
					responseCapture.Name = Utils.CheckNullOrWhiteSpace(xmlElement.GetAttribute("Name"), "Capture Name");
					XmlAttribute xmlAttribute = xmlElement.Attributes["Pattern"];
					if (xmlAttribute != null && !string.IsNullOrWhiteSpace(xmlAttribute.Value))
					{
						responseCapture.Pattern = xmlAttribute.Value;
					}
					else
					{
						XmlNode xmlNode = xmlElement.SelectSingleNode("Pattern");
						Utils.CheckNode(xmlNode, "Capture Pattern");
						responseCapture.Pattern = Utils.CheckNullOrWhiteSpace(xmlNode.InnerText, "Capture Pattern");
					}
					responseCapture.Type = CaptureType.ResponseText;
					string attribute = xmlElement.GetAttribute("Type");
					if (!string.IsNullOrWhiteSpace(attribute))
					{
						responseCapture.Type = Utils.GetEnumValue<CaptureType>(attribute, "Capture Type");
					}
					responseCapture.persistInStateAttributeName = Utils.GetOptionalXmlAttribute<string>(xmlElement, "PersistInStateAttributeName", null);
					responseCapture.Value = string.Empty;
					list.Add(responseCapture);
				}
			}
			return list;
		}

		// Token: 0x04000AE3 RID: 2787
		private string name;

		// Token: 0x04000AE4 RID: 2788
		private string pattern;

		// Token: 0x04000AE5 RID: 2789
		private string value;

		// Token: 0x04000AE6 RID: 2790
		private CaptureType type;

		// Token: 0x04000AE7 RID: 2791
		private string persistInStateAttributeName;
	}
}
