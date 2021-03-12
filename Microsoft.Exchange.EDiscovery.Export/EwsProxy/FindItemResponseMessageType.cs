using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000255 RID: 597
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class FindItemResponseMessageType : ResponseMessageType
	{
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x00026BAB File Offset: 0x00024DAB
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x00026BB3 File Offset: 0x00024DB3
		public FindItemParentType RootFolder
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

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x00026BBC File Offset: 0x00024DBC
		// (set) Token: 0x06001646 RID: 5702 RVA: 0x00026BC4 File Offset: 0x00024DC4
		[XmlArrayItem("Term", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public HighlightTermType[] HighlightTerms
		{
			get
			{
				return this.highlightTermsField;
			}
			set
			{
				this.highlightTermsField = value;
			}
		}

		// Token: 0x04000F43 RID: 3907
		private FindItemParentType rootFolderField;

		// Token: 0x04000F44 RID: 3908
		private HighlightTermType[] highlightTermsField;
	}
}
