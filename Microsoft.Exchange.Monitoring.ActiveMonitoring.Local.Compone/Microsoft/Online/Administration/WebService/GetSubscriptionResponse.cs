using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200033D RID: 829
	[DebuggerStepThrough]
	[DataContract(Name = "GetSubscriptionResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetSubscriptionResponse : Response
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x0008BA74 File Offset: 0x00089C74
		// (set) Token: 0x060015BC RID: 5564 RVA: 0x0008BA7C File Offset: 0x00089C7C
		[DataMember]
		public Subscription ReturnValue
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

		// Token: 0x04000FDA RID: 4058
		private Subscription ReturnValueField;
	}
}
