using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000338 RID: 824
	[DataContract(Name = "AddServicePrincipalResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class AddServicePrincipalResponse : Response
	{
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x0008B94D File Offset: 0x00089B4D
		// (set) Token: 0x06001599 RID: 5529 RVA: 0x0008B955 File Offset: 0x00089B55
		[DataMember]
		public ServicePrincipalExtended ReturnValue
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

		// Token: 0x04000FCB RID: 4043
		private ServicePrincipalExtended ReturnValueField;
	}
}
