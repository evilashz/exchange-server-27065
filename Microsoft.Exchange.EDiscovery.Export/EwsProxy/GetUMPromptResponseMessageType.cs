using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200023C RID: 572
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetUMPromptResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00026729 File Offset: 0x00024929
		// (set) Token: 0x060015BB RID: 5563 RVA: 0x00026731 File Offset: 0x00024931
		[XmlElement(DataType = "base64Binary")]
		public byte[] AudioData
		{
			get
			{
				return this.audioDataField;
			}
			set
			{
				this.audioDataField = value;
			}
		}

		// Token: 0x04000EEA RID: 3818
		private byte[] audioDataField;
	}
}
