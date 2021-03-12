using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003CF RID: 975
	[DataContract(Name = "ServiceStatus", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ServiceStatus : IExtensibleDataObject
	{
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x0008CAC2 File Offset: 0x0008ACC2
		// (set) Token: 0x060017AF RID: 6063 RVA: 0x0008CACA File Offset: 0x0008ACCA
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x0008CAD3 File Offset: 0x0008ACD3
		// (set) Token: 0x060017B1 RID: 6065 RVA: 0x0008CADB File Offset: 0x0008ACDB
		[DataMember]
		public ProvisioningStatus ProvisioningStatus
		{
			get
			{
				return this.ProvisioningStatusField;
			}
			set
			{
				this.ProvisioningStatusField = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x0008CAE4 File Offset: 0x0008ACE4
		// (set) Token: 0x060017B3 RID: 6067 RVA: 0x0008CAEC File Offset: 0x0008ACEC
		[DataMember]
		public ServicePlan ServicePlan
		{
			get
			{
				return this.ServicePlanField;
			}
			set
			{
				this.ServicePlanField = value;
			}
		}

		// Token: 0x040010BD RID: 4285
		private ExtensionDataObject extensionDataField;

		// Token: 0x040010BE RID: 4286
		private ProvisioningStatus ProvisioningStatusField;

		// Token: 0x040010BF RID: 4287
		private ServicePlan ServicePlanField;
	}
}
