using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000267 RID: 615
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class FindFolderResponseMessageType : ResponseMessageType
	{
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x000272AC File Offset: 0x000254AC
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x000272B4 File Offset: 0x000254B4
		public FindFolderParentType RootFolder
		{
			get
			{
				return this.rootFolderField;
			}
			set
			{
				this.rootFolderField = value;
			}
		}

		// Token: 0x04000FA4 RID: 4004
		private FindFolderParentType rootFolderField;
	}
}
