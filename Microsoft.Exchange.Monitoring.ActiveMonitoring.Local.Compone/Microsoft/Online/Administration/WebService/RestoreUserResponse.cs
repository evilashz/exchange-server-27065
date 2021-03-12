using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000336 RID: 822
	[DataContract(Name = "RestoreUserResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class RestoreUserResponse : Response
	{
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0008B91B File Offset: 0x00089B1B
		// (set) Token: 0x06001593 RID: 5523 RVA: 0x0008B923 File Offset: 0x00089B23
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

		// Token: 0x04000FC9 RID: 4041
		private User ReturnValueField;
	}
}
