using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.ProvisionResponse
{
	// Token: 0x0200089E RID: 2206
	[XmlRoot(Namespace = "DeltaSyncV2:", IsNullable = false)]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[Serializable]
	public class Fault
	{
		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06002F37 RID: 12087 RVA: 0x0006A886 File Offset: 0x00068A86
		// (set) Token: 0x06002F38 RID: 12088 RVA: 0x0006A88E File Offset: 0x00068A8E
		public string Faultcode
		{
			get
			{
				return this.faultcodeField;
			}
			set
			{
				this.faultcodeField = value;
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x0006A897 File Offset: 0x00068A97
		// (set) Token: 0x06002F3A RID: 12090 RVA: 0x0006A89F File Offset: 0x00068A9F
		public string Faultstring
		{
			get
			{
				return this.faultstringField;
			}
			set
			{
				this.faultstringField = value;
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x0006A8A8 File Offset: 0x00068AA8
		// (set) Token: 0x06002F3C RID: 12092 RVA: 0x0006A8B0 File Offset: 0x00068AB0
		public string Detail
		{
			get
			{
				return this.detailField;
			}
			set
			{
				this.detailField = value;
			}
		}

		// Token: 0x04002901 RID: 10497
		private string faultcodeField;

		// Token: 0x04002902 RID: 10498
		private string faultstringField;

		// Token: 0x04002903 RID: 10499
		private string detailField;
	}
}
