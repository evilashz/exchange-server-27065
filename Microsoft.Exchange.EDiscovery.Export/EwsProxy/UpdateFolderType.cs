using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200039C RID: 924
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class UpdateFolderType : BaseRequestType
	{
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0002A4C2 File Offset: 0x000286C2
		// (set) Token: 0x06001D0B RID: 7435 RVA: 0x0002A4CA File Offset: 0x000286CA
		[XmlArrayItem("FolderChange", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public FolderChangeType[] FolderChanges
		{
			get
			{
				return this.folderChangesField;
			}
			set
			{
				this.folderChangesField = value;
			}
		}

		// Token: 0x04001345 RID: 4933
		private FolderChangeType[] folderChangesField;
	}
}
