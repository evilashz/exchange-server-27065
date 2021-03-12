using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000319 RID: 793
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class CreateUMCallDataRecordType : BaseRequestType
	{
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x00028B13 File Offset: 0x00026D13
		// (set) Token: 0x06001A00 RID: 6656 RVA: 0x00028B1B File Offset: 0x00026D1B
		public CDRDataType CDRData
		{
			get
			{
				return this.cDRDataField;
			}
			set
			{
				this.cDRDataField = value;
			}
		}

		// Token: 0x04001178 RID: 4472
		private CDRDataType cDRDataField;
	}
}
