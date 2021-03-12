using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001E0 RID: 480
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class UserConfigurationDictionaryObjectType
	{
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x000257B3 File Offset: 0x000239B3
		// (set) Token: 0x060013E5 RID: 5093 RVA: 0x000257BB File Offset: 0x000239BB
		public UserConfigurationDictionaryObjectTypesType Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x000257C4 File Offset: 0x000239C4
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x000257CC File Offset: 0x000239CC
		[XmlElement("Value")]
		public string[] Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04000DB3 RID: 3507
		private UserConfigurationDictionaryObjectTypesType typeField;

		// Token: 0x04000DB4 RID: 3508
		private string[] valueField;
	}
}
