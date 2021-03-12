using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001DE RID: 478
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class UserConfigurationType
	{
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0002572C File Offset: 0x0002392C
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x00025734 File Offset: 0x00023934
		public UserConfigurationNameType UserConfigurationName
		{
			get
			{
				return this.userConfigurationNameField;
			}
			set
			{
				this.userConfigurationNameField = value;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x0002573D File Offset: 0x0002393D
		// (set) Token: 0x060013D7 RID: 5079 RVA: 0x00025745 File Offset: 0x00023945
		public ItemIdType ItemId
		{
			get
			{
				return this.itemIdField;
			}
			set
			{
				this.itemIdField = value;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0002574E File Offset: 0x0002394E
		// (set) Token: 0x060013D9 RID: 5081 RVA: 0x00025756 File Offset: 0x00023956
		[XmlArrayItem("DictionaryEntry", IsNullable = false)]
		public UserConfigurationDictionaryEntryType[] Dictionary
		{
			get
			{
				return this.dictionaryField;
			}
			set
			{
				this.dictionaryField = value;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x0002575F File Offset: 0x0002395F
		// (set) Token: 0x060013DB RID: 5083 RVA: 0x00025767 File Offset: 0x00023967
		[XmlElement(DataType = "base64Binary")]
		public byte[] XmlData
		{
			get
			{
				return this.xmlDataField;
			}
			set
			{
				this.xmlDataField = value;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x00025770 File Offset: 0x00023970
		// (set) Token: 0x060013DD RID: 5085 RVA: 0x00025778 File Offset: 0x00023978
		[XmlElement(DataType = "base64Binary")]
		public byte[] BinaryData
		{
			get
			{
				return this.binaryDataField;
			}
			set
			{
				this.binaryDataField = value;
			}
		}

		// Token: 0x04000DAC RID: 3500
		private UserConfigurationNameType userConfigurationNameField;

		// Token: 0x04000DAD RID: 3501
		private ItemIdType itemIdField;

		// Token: 0x04000DAE RID: 3502
		private UserConfigurationDictionaryEntryType[] dictionaryField;

		// Token: 0x04000DAF RID: 3503
		private byte[] xmlDataField;

		// Token: 0x04000DB0 RID: 3504
		private byte[] binaryDataField;
	}
}
