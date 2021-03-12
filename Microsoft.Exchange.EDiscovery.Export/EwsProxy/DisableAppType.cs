using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200032C RID: 812
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class DisableAppType : BaseRequestType
	{
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x00028DD4 File Offset: 0x00026FD4
		// (set) Token: 0x06001A54 RID: 6740 RVA: 0x00028DDC File Offset: 0x00026FDC
		public string ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x00028DE5 File Offset: 0x00026FE5
		// (set) Token: 0x06001A56 RID: 6742 RVA: 0x00028DED File Offset: 0x00026FED
		public DisableReasonType DisableReason
		{
			get
			{
				return this.disableReasonField;
			}
			set
			{
				this.disableReasonField = value;
			}
		}

		// Token: 0x040011A3 RID: 4515
		private string idField;

		// Token: 0x040011A4 RID: 4516
		private DisableReasonType disableReasonField;
	}
}
