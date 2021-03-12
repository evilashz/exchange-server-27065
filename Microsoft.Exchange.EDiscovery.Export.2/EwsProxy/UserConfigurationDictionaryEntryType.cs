using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001DF RID: 479
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class UserConfigurationDictionaryEntryType
	{
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00025789 File Offset: 0x00023989
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x00025791 File Offset: 0x00023991
		public UserConfigurationDictionaryObjectType DictionaryKey
		{
			get
			{
				return this.dictionaryKeyField;
			}
			set
			{
				this.dictionaryKeyField = value;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0002579A File Offset: 0x0002399A
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x000257A2 File Offset: 0x000239A2
		[XmlElement(IsNullable = true)]
		public UserConfigurationDictionaryObjectType DictionaryValue
		{
			get
			{
				return this.dictionaryValueField;
			}
			set
			{
				this.dictionaryValueField = value;
			}
		}

		// Token: 0x04000DB1 RID: 3505
		private UserConfigurationDictionaryObjectType dictionaryKeyField;

		// Token: 0x04000DB2 RID: 3506
		private UserConfigurationDictionaryObjectType dictionaryValueField;
	}
}
