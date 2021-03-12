using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200029C RID: 668
	public class ExpectedResult
	{
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x00047674 File Offset: 0x00045874
		// (set) Token: 0x06001655 RID: 5717 RVA: 0x0004767C File Offset: 0x0004587C
		public ExpectedResultType Type
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

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x00047685 File Offset: 0x00045885
		// (set) Token: 0x06001657 RID: 5719 RVA: 0x0004768D File Offset: 0x0004588D
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

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x00047696 File Offset: 0x00045896
		// (set) Token: 0x06001659 RID: 5721 RVA: 0x0004769E File Offset: 0x0004589E
		public bool IsRegEx
		{
			get
			{
				return this.isRegEx;
			}
			set
			{
				this.isRegEx = value;
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000476A8 File Offset: 0x000458A8
		public static ICollection<ExpectedResult> FromXml(XmlNode workContext)
		{
			List<ExpectedResult> list = new List<ExpectedResult>();
			using (XmlNodeList xmlNodeList = workContext.SelectNodes("ExpectedResult"))
			{
				foreach (object obj in xmlNodeList)
				{
					XmlElement xmlElement = (XmlElement)obj;
					ExpectedResult expectedResult = new ExpectedResult();
					ExpectedResultType expectedResultType = ExpectedResultType.Body;
					string attribute = xmlElement.GetAttribute("Type");
					if (string.IsNullOrWhiteSpace(attribute) || !Enum.TryParse<ExpectedResultType>(attribute, true, out expectedResultType))
					{
						throw new ArgumentException("Expected result type is a required argument but was not specified or was not a valid value.");
					}
					expectedResult.Type = expectedResultType;
					XmlAttribute xmlAttribute = xmlElement.Attributes["Value"];
					if (xmlAttribute != null && !string.IsNullOrWhiteSpace(xmlAttribute.Value))
					{
						expectedResult.Value = xmlAttribute.Value;
					}
					else
					{
						XmlNode xmlNode = xmlElement.SelectSingleNode("Value");
						Utils.CheckNode(xmlNode, "ExpectedResult Value");
						expectedResult.Value = Utils.CheckNullOrWhiteSpace(xmlNode.InnerText, "ExpectedResult Value");
					}
					attribute = xmlElement.GetAttribute("IsRegEx");
					expectedResult.IsRegEx = false;
					bool flag;
					if (!string.IsNullOrWhiteSpace(attribute) && bool.TryParse(attribute, out flag))
					{
						expectedResult.IsRegEx = flag;
					}
					list.Add(expectedResult);
				}
			}
			return list;
		}

		// Token: 0x04000AE0 RID: 2784
		private ExpectedResultType type;

		// Token: 0x04000AE1 RID: 2785
		private string value;

		// Token: 0x04000AE2 RID: 2786
		private bool isRegEx;
	}
}
