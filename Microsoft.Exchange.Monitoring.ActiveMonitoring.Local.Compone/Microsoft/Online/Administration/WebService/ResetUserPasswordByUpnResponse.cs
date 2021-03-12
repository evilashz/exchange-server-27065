using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200032F RID: 815
	[DataContract(Name = "ResetUserPasswordByUpnResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class ResetUserPasswordByUpnResponse : Response
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x0008B86C File Offset: 0x00089A6C
		// (set) Token: 0x0600157E RID: 5502 RVA: 0x0008B874 File Offset: 0x00089A74
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

		// Token: 0x04000FC2 RID: 4034
		private string ReturnValueField;
	}
}
