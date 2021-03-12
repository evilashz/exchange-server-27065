using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001B9 RID: 441
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class UserConfigurationNameType : TargetFolderIdType
	{
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00024FB3 File Offset: 0x000231B3
		// (set) Token: 0x060012F2 RID: 4850 RVA: 0x00024FBB File Offset: 0x000231BB
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x04000D3A RID: 3386
		private string nameField;
	}
}
