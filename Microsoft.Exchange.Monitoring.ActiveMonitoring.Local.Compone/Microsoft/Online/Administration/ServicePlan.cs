using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003D0 RID: 976
	[DataContract(Name = "ServicePlan", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ServicePlan : IExtensibleDataObject
	{
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0008CAFD File Offset: 0x0008ACFD
		// (set) Token: 0x060017B6 RID: 6070 RVA: 0x0008CB05 File Offset: 0x0008AD05
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

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x0008CB0E File Offset: 0x0008AD0E
		// (set) Token: 0x060017B8 RID: 6072 RVA: 0x0008CB16 File Offset: 0x0008AD16
		[DataMember]
		public string ServiceName
		{
			get
			{
				return this.ServiceNameField;
			}
			set
			{
				this.ServiceNameField = value;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x0008CB1F File Offset: 0x0008AD1F
		// (set) Token: 0x060017BA RID: 6074 RVA: 0x0008CB27 File Offset: 0x0008AD27
		[DataMember]
		public Guid? ServicePlanId
		{
			get
			{
				return this.ServicePlanIdField;
			}
			set
			{
				this.ServicePlanIdField = value;
			}
		}

		// Token: 0x040010C0 RID: 4288
		private ExtensionDataObject extensionDataField;

		// Token: 0x040010C1 RID: 4289
		private string ServiceNameField;

		// Token: 0x040010C2 RID: 4290
		private Guid? ServicePlanIdField;
	}
}
