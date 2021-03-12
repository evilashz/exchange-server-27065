using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200024C RID: 588
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class Duration
	{
		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x000269E4 File Offset: 0x00024BE4
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x000269EC File Offset: 0x00024BEC
		public DateTime StartTime
		{
			get
			{
				return this.startTimeField;
			}
			set
			{
				this.startTimeField = value;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x000269F5 File Offset: 0x00024BF5
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x000269FD File Offset: 0x00024BFD
		public DateTime EndTime
		{
			get
			{
				return this.endTimeField;
			}
			set
			{
				this.endTimeField = value;
			}
		}

		// Token: 0x04000F28 RID: 3880
		private DateTime startTimeField;

		// Token: 0x04000F29 RID: 3881
		private DateTime endTimeField;
	}
}
