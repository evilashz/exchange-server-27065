using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000BC RID: 188
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetUMPinResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0001FEC9 File Offset: 0x0001E0C9
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x0001FED1 File Offset: 0x0001E0D1
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

		// Token: 0x0400056F RID: 1391
		private PinInfoType pinInfoField;
	}
}
