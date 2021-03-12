using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003A6 RID: 934
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class UploadItemsType : BaseRequestType
	{
		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x0002A61A File Offset: 0x0002881A
		// (set) Token: 0x06001D34 RID: 7476 RVA: 0x0002A622 File Offset: 0x00028822
		[XmlArrayItem("Item", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public UploadItemType[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x04001359 RID: 4953
		private UploadItemType[] itemsField;
	}
}
