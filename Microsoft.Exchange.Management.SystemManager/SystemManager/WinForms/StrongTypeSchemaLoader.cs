using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000164 RID: 356
	internal class StrongTypeSchemaLoader : ObjectSchemaLoader
	{
		// Token: 0x06000E88 RID: 3720 RVA: 0x000379CA File Offset: 0x00035BCA
		public StrongTypeSchemaLoader(string schema) : base(2, "StrongTypeEditorSchema/StrongTypeEditor", schema)
		{
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x000379DC File Offset: 0x00035BDC
		public List<string> ArgumentList
		{
			get
			{
				if (this.argumentList == null)
				{
					this.argumentList = new List<string>();
					XmlNode xmlNode = base.ObjectDefinition.SelectSingleNode("ParseStrongType");
					foreach (object obj in xmlNode.SelectNodes("arg"))
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						this.argumentList.Add(xmlNode2.InnerText);
					}
				}
				return this.argumentList;
			}
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00037A70 File Offset: 0x00035C70
		public List<DataColumn> LoadDataColumns(List<string> bindingMapping)
		{
			List<DataColumn> list = new List<DataColumn>();
			bindingMapping.Clear();
			XmlNode xmlNode = base.ObjectDefinition.SelectSingleNode("Columns");
			foreach (object obj in xmlNode.SelectNodes("Column"))
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				string value = xmlNode2.Attributes.GetNamedItem("Name").Value;
				string value2 = xmlNode2.Attributes.GetNamedItem("Type").Value;
				Type typeByString = ObjectSchemaLoader.GetTypeByString(value2);
				ICustomTextConverter customTextConverter = null;
				XmlNode namedItem = xmlNode2.Attributes.GetNamedItem("TextConverter");
				if (namedItem != null && !string.IsNullOrEmpty(namedItem.Value))
				{
					customTextConverter = (ICustomTextConverter)ObjectSchemaLoader.GetTypeByString(namedItem.Value).GetConstructor(new Type[0]).Invoke(new object[0]);
				}
				XmlNode namedItem2 = xmlNode2.Attributes.GetNamedItem("DefaultValue");
				DataColumn dataColumn = new DataColumn(value, typeByString);
				if (namedItem2 != null)
				{
					string value3 = namedItem2.Value;
					dataColumn.DefaultValue = ((customTextConverter != null) ? customTextConverter.Parse(typeByString, value3, null) : value3);
				}
				string value4 = xmlNode2.Attributes.GetNamedItem("UpdateTable").Value;
				if (value4.Equals("true", StringComparison.InvariantCultureIgnoreCase))
				{
					bindingMapping.Add(value);
				}
				XmlNode namedItem3 = xmlNode2.Attributes.GetNamedItem("Expression");
				if (namedItem3 != null && !string.IsNullOrEmpty(namedItem3.Value))
				{
					dataColumn.Expression = namedItem3.Value;
				}
				list.Add(dataColumn);
			}
			return list;
		}

		// Token: 0x040005E1 RID: 1505
		private List<string> argumentList;
	}
}
