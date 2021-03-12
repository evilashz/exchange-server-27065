using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200024A RID: 586
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class OutOfOfficeMailTip
	{
		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00026990 File Offset: 0x00024B90
		// (set) Token: 0x06001604 RID: 5636 RVA: 0x00026998 File Offset: 0x00024B98
		public ReplyBody ReplyBody
		{
			get
			{
				return this.replyBodyField;
			}
			set
			{
				this.replyBodyField = value;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x000269A1 File Offset: 0x00024BA1
		// (set) Token: 0x06001606 RID: 5638 RVA: 0x000269A9 File Offset: 0x00024BA9
		public Duration Duration
		{
			get
			{
				return this.durationField;
			}
			set
			{
				this.durationField = value;
			}
		}

		// Token: 0x04000F24 RID: 3876
		private ReplyBody replyBodyField;

		// Token: 0x04000F25 RID: 3877
		private Duration durationField;
	}
}
