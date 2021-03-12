using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200032E RID: 814
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ResetUserPasswordResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ResetUserPasswordResponse : Response
	{
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0008B853 File Offset: 0x00089A53
		// (set) Token: 0x0600157B RID: 5499 RVA: 0x0008B85B File Offset: 0x00089A5B
		[DataMember]
		public string ReturnValue
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

		// Token: 0x04000FC1 RID: 4033
		private string ReturnValueField;
	}
}
