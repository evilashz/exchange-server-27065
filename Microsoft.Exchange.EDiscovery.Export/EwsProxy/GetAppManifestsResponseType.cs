using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200010A RID: 266
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetAppManifestsResponseType : ResponseMessageType
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00021302 File Offset: 0x0001F502
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x0002130A File Offset: 0x0001F50A
		[XmlArrayItem("Manifest", DataType = "base64Binary", IsNullable = false)]
		public byte[][] Manifests
		{
			get
			{
				return this.manifestsField;
			}
			set
			{
				this.manifestsField = value;
			}
		}

		// Token: 0x04000869 RID: 2153
		private byte[][] manifestsField;
	}
}
