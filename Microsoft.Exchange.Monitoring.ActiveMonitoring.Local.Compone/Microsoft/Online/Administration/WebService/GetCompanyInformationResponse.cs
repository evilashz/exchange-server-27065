using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200033C RID: 828
	[DataContract(Name = "GetCompanyInformationResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetCompanyInformationResponse : Response
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x0008BA5B File Offset: 0x00089C5B
		// (set) Token: 0x060015B9 RID: 5561 RVA: 0x0008BA63 File Offset: 0x00089C63
		[DataMember]
		public CompanyInformation ReturnValue
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

		// Token: 0x04000FD9 RID: 4057
		private CompanyInformation ReturnValueField;
	}
}
