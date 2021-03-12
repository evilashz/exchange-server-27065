using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000330 RID: 816
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetUserResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class GetUserResponse : Response
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x0008B885 File Offset: 0x00089A85
		// (set) Token: 0x06001581 RID: 5505 RVA: 0x0008B88D File Offset: 0x00089A8D
		[DataMember]
		public User ReturnValue
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

		// Token: 0x04000FC3 RID: 4035
		private User ReturnValueField;
	}
}
