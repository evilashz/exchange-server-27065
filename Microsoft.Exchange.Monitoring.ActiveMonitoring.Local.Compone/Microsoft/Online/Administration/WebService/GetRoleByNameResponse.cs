using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000325 RID: 805
	[DebuggerStepThrough]
	[DataContract(Name = "GetRoleByNameResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetRoleByNameResponse : Response
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x0008B772 File Offset: 0x00089972
		// (set) Token: 0x06001560 RID: 5472 RVA: 0x0008B77A File Offset: 0x0008997A
		[DataMember]
		public Role ReturnValue
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

		// Token: 0x04000FB8 RID: 4024
		private Role ReturnValueField;
	}
}
