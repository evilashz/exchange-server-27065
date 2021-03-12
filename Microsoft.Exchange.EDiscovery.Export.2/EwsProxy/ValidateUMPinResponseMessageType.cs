using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000BF RID: 191
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ValidateUMPinResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0001FF47 File Offset: 0x0001E147
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x0001FF4F File Offset: 0x0001E14F
		public PinInfoType PinInfo
		{
			get
			{
				return this.pinInfoField;
			}
			set
			{
				this.pinInfoField = value;
			}
		}

		// Token: 0x04000575 RID: 1397
		private PinInfoType pinInfoField;
	}
}
