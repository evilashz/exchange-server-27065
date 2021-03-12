using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000397 RID: 919
	[DataContract(Name = "TenantNotPartnerTypeException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class TenantNotPartnerTypeException : InvalidParameterException
	{
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0008C140 File Offset: 0x0008A340
		// (set) Token: 0x0600168E RID: 5774 RVA: 0x0008C148 File Offset: 0x0008A348
		[DataMember]
		public string PartnerId
		{
			get
			{
				return this.PartnerIdField;
			}
			set
			{
				this.PartnerIdField = value;
			}
		}

		// Token: 0x04001016 RID: 4118
		private string PartnerIdField;
	}
}
