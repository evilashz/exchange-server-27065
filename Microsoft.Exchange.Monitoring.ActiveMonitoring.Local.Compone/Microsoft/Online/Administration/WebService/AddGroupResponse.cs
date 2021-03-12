using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200034A RID: 842
	[DataContract(Name = "AddGroupResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class AddGroupResponse : Response
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x0008BBB9 File Offset: 0x00089DB9
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x0008BBC1 File Offset: 0x00089DC1
		[DataMember]
		public Group ReturnValue
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

		// Token: 0x04000FE7 RID: 4071
		private Group ReturnValueField;
	}
}
