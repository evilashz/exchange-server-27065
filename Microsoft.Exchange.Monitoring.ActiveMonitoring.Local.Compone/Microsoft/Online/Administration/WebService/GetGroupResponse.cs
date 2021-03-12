using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200034B RID: 843
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetGroupResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class GetGroupResponse : Response
	{
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x0008BBD2 File Offset: 0x00089DD2
		// (set) Token: 0x060015E6 RID: 5606 RVA: 0x0008BBDA File Offset: 0x00089DDA
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

		// Token: 0x04000FE8 RID: 4072
		private Group ReturnValueField;
	}
}
