using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200033B RID: 827
	[DataContract(Name = "GetPartnerInformationResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetPartnerInformationResponse : Response
	{
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x0008BA42 File Offset: 0x00089C42
		// (set) Token: 0x060015B6 RID: 5558 RVA: 0x0008BA4A File Offset: 0x00089C4A
		[DataMember]
		public PartnerInformation ReturnValue
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

		// Token: 0x04000FD8 RID: 4056
		private PartnerInformation ReturnValueField;
	}
}
