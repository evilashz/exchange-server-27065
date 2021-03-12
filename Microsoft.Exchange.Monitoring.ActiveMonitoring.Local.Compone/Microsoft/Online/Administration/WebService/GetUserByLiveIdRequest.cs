using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002EC RID: 748
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetUserByLiveIdRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class GetUserByLiveIdRequest : Request
	{
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0008AFD2 File Offset: 0x000891D2
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x0008AFDA File Offset: 0x000891DA
		[DataMember]
		public string LiveId
		{
			get
			{
				return this.LiveIdField;
			}
			set
			{
				this.LiveIdField = value;
			}
		}

		// Token: 0x04000F60 RID: 3936
		private string LiveIdField;
	}
}
