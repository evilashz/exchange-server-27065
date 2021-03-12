using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000312 RID: 786
	[DataContract(Name = "SetGroupRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class SetGroupRequest : Request
	{
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0008B564 File Offset: 0x00089764
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x0008B56C File Offset: 0x0008976C
		[DataMember]
		public Group Group
		{
			get
			{
				return this.GroupField;
			}
			set
			{
				this.GroupField = value;
			}
		}

		// Token: 0x04000FA2 RID: 4002
		private Group GroupField;
	}
}
