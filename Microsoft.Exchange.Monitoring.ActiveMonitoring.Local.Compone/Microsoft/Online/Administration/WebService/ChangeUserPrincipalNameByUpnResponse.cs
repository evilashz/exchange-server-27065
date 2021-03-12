using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200032D RID: 813
	[DataContract(Name = "ChangeUserPrincipalNameByUpnResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class ChangeUserPrincipalNameByUpnResponse : Response
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x0008B83A File Offset: 0x00089A3A
		// (set) Token: 0x06001578 RID: 5496 RVA: 0x0008B842 File Offset: 0x00089A42
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

		// Token: 0x04000FC0 RID: 4032
		private string ReturnValueField;
	}
}
