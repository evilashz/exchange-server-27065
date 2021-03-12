using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002C9 RID: 713
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ConfigurationRequestDetailsType
	{
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x00027BF5 File Offset: 0x00025DF5
		// (set) Token: 0x06001835 RID: 6197 RVA: 0x00027BFD File Offset: 0x00025DFD
		[XmlAnyElement]
		public XmlElement Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x0400106A RID: 4202
		private XmlElement itemField;
	}
}
