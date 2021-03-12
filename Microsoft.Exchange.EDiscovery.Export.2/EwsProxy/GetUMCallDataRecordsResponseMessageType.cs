using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000C6 RID: 198
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetUMCallDataRecordsResponseMessageType : ResponseMessageType
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x000200F5 File Offset: 0x0001E2F5
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x000200FD File Offset: 0x0001E2FD
		[XmlArrayItem("CDRData", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public CDRDataType[] CallDataRecords
		{
			get
			{
				return this.callDataRecordsField;
			}
			set
			{
				this.callDataRecordsField = value;
			}
		}

		// Token: 0x0400058B RID: 1419
		private CDRDataType[] callDataRecordsField;
	}
}
