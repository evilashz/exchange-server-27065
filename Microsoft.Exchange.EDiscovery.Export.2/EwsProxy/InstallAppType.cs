using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200032F RID: 815
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class InstallAppType : BaseRequestType
	{
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x00028E17 File Offset: 0x00027017
		// (set) Token: 0x06001A5C RID: 6748 RVA: 0x00028E1F File Offset: 0x0002701F
		[XmlElement(DataType = "base64Binary")]
		public byte[] Manifest
		{
			get
			{
				return this.manifestField;
			}
			set
			{
				this.manifestField = value;
			}
		}

		// Token: 0x040011AB RID: 4523
		private byte[] manifestField;
	}
}
