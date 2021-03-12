using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002D8 RID: 728
	[DebuggerStepThrough]
	[DataContract(Name = "GetRoleRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetRoleRequest : Request
	{
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x0008AC79 File Offset: 0x00088E79
		// (set) Token: 0x06001411 RID: 5137 RVA: 0x0008AC81 File Offset: 0x00088E81
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

		// Token: 0x04000F37 RID: 3895
		private Guid ObjectIdField;
	}
}
