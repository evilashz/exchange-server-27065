using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200023D RID: 573
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetUMPromptNamesResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00026742 File Offset: 0x00024942
		// (set) Token: 0x060015BE RID: 5566 RVA: 0x0002674A File Offset: 0x0002494A
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

		// Token: 0x04000EEB RID: 3819
		private string[] promptNamesField;
	}
}
