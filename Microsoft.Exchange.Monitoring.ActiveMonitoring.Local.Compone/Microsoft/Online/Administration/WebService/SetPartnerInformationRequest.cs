using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002FC RID: 764
	[DataContract(Name = "SetPartnerInformationRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class SetPartnerInformationRequest : Request
	{
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x0008B2B6 File Offset: 0x000894B6
		// (set) Token: 0x060014CF RID: 5327 RVA: 0x0008B2BE File Offset: 0x000894BE
		[DataMember]
		public PartnerInformation PartnerInformation
		{
			get
			{
				return this.PartnerInformationField;
			}
			set
			{
				this.PartnerInformationField = value;
			}
		}

		// Token: 0x04000F84 RID: 3972
		private PartnerInformation PartnerInformationField;
	}
}
