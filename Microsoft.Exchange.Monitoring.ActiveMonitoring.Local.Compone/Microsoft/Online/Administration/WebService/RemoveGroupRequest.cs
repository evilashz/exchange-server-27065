using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000311 RID: 785
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "RemoveGroupRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class RemoveGroupRequest : Request
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x0008B54B File Offset: 0x0008974B
		// (set) Token: 0x0600151E RID: 5406 RVA: 0x0008B553 File Offset: 0x00089753
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

		// Token: 0x04000FA1 RID: 4001
		private Guid ObjectIdField;
	}
}
