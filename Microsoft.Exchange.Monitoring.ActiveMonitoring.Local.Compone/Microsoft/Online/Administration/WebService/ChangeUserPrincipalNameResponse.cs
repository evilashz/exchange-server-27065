using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200032C RID: 812
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ChangeUserPrincipalNameResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ChangeUserPrincipalNameResponse : Response
	{
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x0008B821 File Offset: 0x00089A21
		// (set) Token: 0x06001575 RID: 5493 RVA: 0x0008B829 File Offset: 0x00089A29
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

		// Token: 0x04000FBF RID: 4031
		private string ReturnValueField;
	}
}
