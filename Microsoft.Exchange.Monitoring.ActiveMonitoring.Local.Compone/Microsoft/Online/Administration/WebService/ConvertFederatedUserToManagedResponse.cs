using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000335 RID: 821
	[DebuggerStepThrough]
	[DataContract(Name = "ConvertFederatedUserToManagedResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ConvertFederatedUserToManagedResponse : Response
	{
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0008B902 File Offset: 0x00089B02
		// (set) Token: 0x06001590 RID: 5520 RVA: 0x0008B90A File Offset: 0x00089B0A
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

		// Token: 0x04000FC8 RID: 4040
		private string ReturnValueField;
	}
}
