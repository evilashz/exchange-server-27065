using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001F5 RID: 501
	[XmlInclude(typeof(AlternatePublicFolderItemIdType))]
	[DebuggerStepThrough]
	[XmlInclude(typeof(AlternateIdType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(AlternatePublicFolderIdType))]
	[Serializable]
	public abstract class AlternateIdBaseType
	{
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00025ACA File Offset: 0x00023CCA
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x00025AD2 File Offset: 0x00023CD2
		[XmlAttribute]
		public IdFormatType Format
		{
			get
			{
				return this.formatField;
			}
			set
			{
				this.formatField = value;
			}
		}

		// Token: 0x04000DF9 RID: 3577
		private IdFormatType formatField;
	}
}
