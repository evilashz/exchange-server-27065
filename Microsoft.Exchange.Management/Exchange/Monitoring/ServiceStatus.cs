using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005C1 RID: 1473
	[Serializable]
	public class ServiceStatus : ConfigurableObject
	{
		// Token: 0x060033A2 RID: 13218 RVA: 0x000D1945 File Offset: 0x000CFB45
		public ServiceStatus() : base(new ServiceStatusPropertyBag())
		{
			this.statusList = new MultiValuedProperty<Status>();
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x060033A3 RID: 13219 RVA: 0x000D195D File Offset: 0x000CFB5D
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ServiceStatus.schema;
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x060033A4 RID: 13220 RVA: 0x000D1964 File Offset: 0x000CFB64
		// (set) Token: 0x060033A5 RID: 13221 RVA: 0x000D197B File Offset: 0x000CFB7B
		public new ObjectId Identity
		{
			get
			{
				return (ObjectId)this.propertyBag[ServiceStatusSchema.Identity];
			}
			internal set
			{
				this.propertyBag[ServiceStatusSchema.Identity] = value;
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x060033A6 RID: 13222 RVA: 0x000D198E File Offset: 0x000CFB8E
		private new bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x000D1991 File Offset: 0x000CFB91
		// (set) Token: 0x060033A8 RID: 13224 RVA: 0x000D1999 File Offset: 0x000CFB99
		public MultiValuedProperty<Status> StatusList
		{
			get
			{
				return this.statusList;
			}
			internal set
			{
				this.statusList = value;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x060033A9 RID: 13225 RVA: 0x000D19A2 File Offset: 0x000CFBA2
		// (set) Token: 0x060033AA RID: 13226 RVA: 0x000D19B9 File Offset: 0x000CFBB9
		public uint MaintenanceWindowDays
		{
			get
			{
				return (uint)this.propertyBag[ServiceStatusSchema.MaintenanceWindowDays];
			}
			internal set
			{
				this.propertyBag[ServiceStatusSchema.MaintenanceWindowDays] = value;
			}
		}

		// Token: 0x040023E2 RID: 9186
		private static ServiceStatusSchema schema = ObjectSchema.GetInstance<ServiceStatusSchema>();

		// Token: 0x040023E3 RID: 9187
		private MultiValuedProperty<Status> statusList;
	}
}
