using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200032B RID: 811
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "AddUserResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class AddUserResponse : Response
	{
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0008B808 File Offset: 0x00089A08
		// (set) Token: 0x06001572 RID: 5490 RVA: 0x0008B810 File Offset: 0x00089A10
		[DataMember]
		public UserExtended ReturnValue
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

		// Token: 0x04000FBE RID: 4030
		private UserExtended ReturnValueField;
	}
}
