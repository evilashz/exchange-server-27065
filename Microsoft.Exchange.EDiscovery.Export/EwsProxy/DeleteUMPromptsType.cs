using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000373 RID: 883
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class DeleteUMPromptsType : BaseRequestType
	{
		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x00029C9F File Offset: 0x00027E9F
		// (set) Token: 0x06001C15 RID: 7189 RVA: 0x00029CA7 File Offset: 0x00027EA7
		public string ConfigurationObject
		{
			get
			{
				return this.configurationObjectField;
			}
			set
			{
				this.configurationObjectField = value;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x00029CB0 File Offset: 0x00027EB0
		// (set) Token: 0x06001C17 RID: 7191 RVA: 0x00029CB8 File Offset: 0x00027EB8
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] PromptNames
		{
			get
			{
				return this.promptNamesField;
			}
			set
			{
				this.promptNamesField = value;
			}
		}

		// Token: 0x040012A1 RID: 4769
		private string configurationObjectField;

		// Token: 0x040012A2 RID: 4770
		private string[] promptNamesField;
	}
}
