using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002CC RID: 716
	[DataContract(Name = "RemoveServicePrincipalCredentialsBySpnRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class RemoveServicePrincipalCredentialsBySpnRequest : Request
	{
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0008AAE7 File Offset: 0x00088CE7
		// (set) Token: 0x060013E1 RID: 5089 RVA: 0x0008AAEF File Offset: 0x00088CEF
		[DataMember]
		public Guid[] KeyIds
		{
			get
			{
				return this.KeyIdsField;
			}
			set
			{
				this.KeyIdsField = value;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x0008AAF8 File Offset: 0x00088CF8
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x0008AB00 File Offset: 0x00088D00
		[DataMember]
		public string ServicePrincipalName
		{
			get
			{
				return this.ServicePrincipalNameField;
			}
			set
			{
				this.ServicePrincipalNameField = value;
			}
		}

		// Token: 0x04000F25 RID: 3877
		private Guid[] KeyIdsField;

		// Token: 0x04000F26 RID: 3878
		private string ServicePrincipalNameField;
	}
}
