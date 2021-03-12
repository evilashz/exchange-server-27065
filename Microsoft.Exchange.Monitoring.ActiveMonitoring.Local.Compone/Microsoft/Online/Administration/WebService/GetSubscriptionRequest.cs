using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002FA RID: 762
	[DataContract(Name = "GetSubscriptionRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetSubscriptionRequest : Request
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0008B284 File Offset: 0x00089484
		// (set) Token: 0x060014C9 RID: 5321 RVA: 0x0008B28C File Offset: 0x0008948C
		[DataMember]
		public Guid SubscriptionId
		{
			get
			{
				return this.SubscriptionIdField;
			}
			set
			{
				this.SubscriptionIdField = value;
			}
		}

		// Token: 0x04000F82 RID: 3970
		private Guid SubscriptionIdField;
	}
}
