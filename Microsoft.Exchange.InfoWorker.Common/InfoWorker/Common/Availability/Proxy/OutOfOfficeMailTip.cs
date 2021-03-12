using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x02000131 RID: 305
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class OutOfOfficeMailTip
	{
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00025087 File Offset: 0x00023287
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x0002508F File Offset: 0x0002328F
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

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00025098 File Offset: 0x00023298
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x000250A0 File Offset: 0x000232A0
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

		// Token: 0x0400067F RID: 1663
		private ReplyBody replyBodyField;

		// Token: 0x04000680 RID: 1664
		private Duration durationField;
	}
}
