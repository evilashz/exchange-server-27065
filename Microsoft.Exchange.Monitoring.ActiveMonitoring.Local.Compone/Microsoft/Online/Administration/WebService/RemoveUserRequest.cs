using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002E3 RID: 739
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "RemoveUserRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class RemoveUserRequest : Request
	{
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0008AE25 File Offset: 0x00089025
		// (set) Token: 0x06001444 RID: 5188 RVA: 0x0008AE2D File Offset: 0x0008902D
		[DataMember]
		public Guid ObjectId
		{
			get
			{
				return this.ObjectIdField;
			}
			set
			{
				this.ObjectIdField = value;
			}
		}

		// Token: 0x04000F4B RID: 3915
		private Guid ObjectIdField;
	}
}
