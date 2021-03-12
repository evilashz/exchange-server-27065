using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001C2 RID: 450
	internal class DeviceDataBatch : ConfigurablePropertyBag
	{
		// Token: 0x06001309 RID: 4873 RVA: 0x00039B3A File Offset: 0x00037D3A
		public DeviceDataBatch(IEnumerable<DeviceData> batch)
		{
			this.DeviceProperties = DalHelper.CreateDataTable(DeviceDataBatch.DevicePropertiesTvp, DeviceData.propertydefinitions, batch);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00039B58 File Offset: 0x00037D58
		internal DeviceDataBatch(DataTable items)
		{
			this.DeviceProperties = items;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x00039B68 File Offset: 0x00037D68
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00039B8D File Offset: 0x00037D8D
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x00039B9F File Offset: 0x00037D9F
		public DataTable DeviceProperties
		{
			get
			{
				return (DataTable)this[DeviceDataBatchSchema.DeviceDataTableProperty];
			}
			private set
			{
				this[DeviceDataBatchSchema.DeviceDataTableProperty] = value;
			}
		}

		// Token: 0x04000922 RID: 2338
		internal static readonly string DevicePropertiesTvp = "deviceProperties";

		// Token: 0x04000923 RID: 2339
		private static readonly HygienePropertyDefinition[] DataTableProperties = new HygienePropertyDefinition[]
		{
			DeviceDataBatchSchema.DeviceDataTableProperty
		};
	}
}
