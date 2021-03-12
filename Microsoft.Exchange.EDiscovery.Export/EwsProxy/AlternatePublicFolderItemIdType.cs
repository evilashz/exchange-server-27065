using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001F8 RID: 504
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AlternatePublicFolderItemIdType : AlternatePublicFolderIdType
	{
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x00025AFC File Offset: 0x00023CFC
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x00025B04 File Offset: 0x00023D04
		[XmlAttribute]
		public string ItemId
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

		// Token: 0x04000E02 RID: 3586
		private string itemIdField;
	}
}
