using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200033E RID: 830
	[DataContract(Name = "ListSubscriptionsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ListSubscriptionsResponse : Response
	{
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x0008BA8D File Offset: 0x00089C8D
		// (set) Token: 0x060015BF RID: 5567 RVA: 0x0008BA95 File Offset: 0x00089C95
		[DataMember]
		public Subscription[] ReturnValue
		{
			get
			{
				return this.ReturnValueField;
			}
			set
			{
				this.ReturnValueField = value;
			}
		}

		// Token: 0x04000FDB RID: 4059
		private Subscription[] ReturnValueField;
	}
}
