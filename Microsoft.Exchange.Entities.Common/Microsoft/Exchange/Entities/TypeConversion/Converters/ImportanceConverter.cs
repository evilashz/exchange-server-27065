using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Items;

namespace Microsoft.Exchange.Entities.TypeConversion.Converters
{
	// Token: 0x0200005F RID: 95
	internal struct ImportanceConverter : IConverter<Microsoft.Exchange.Data.Storage.Importance, Microsoft.Exchange.Entities.DataModel.Items.Importance>, IConverter<Microsoft.Exchange.Entities.DataModel.Items.Importance, Microsoft.Exchange.Data.Storage.Importance>
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00007940 File Offset: 0x00005B40
		public IConverter<Microsoft.Exchange.Data.Storage.Importance, Microsoft.Exchange.Entities.DataModel.Items.Importance> StorageToEntitiesConverter
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000794D File Offset: 0x00005B4D
		public IConverter<Microsoft.Exchange.Entities.DataModel.Items.Importance, Microsoft.Exchange.Data.Storage.Importance> EntitiesToStorageConverter
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000795C File Offset: 0x00005B5C
		Microsoft.Exchange.Entities.DataModel.Items.Importance IConverter<Microsoft.Exchange.Data.Storage.Importance, Microsoft.Exchange.Entities.DataModel.Items.Importance>.Convert(Microsoft.Exchange.Data.Storage.Importance value)
		{
			switch (value)
			{
			case Microsoft.Exchange.Data.Storage.Importance.Low:
				return Microsoft.Exchange.Entities.DataModel.Items.Importance.Low;
			case Microsoft.Exchange.Data.Storage.Importance.Normal:
				return Microsoft.Exchange.Entities.DataModel.Items.Importance.Normal;
			case Microsoft.Exchange.Data.Storage.Importance.High:
				return Microsoft.Exchange.Entities.DataModel.Items.Importance.High;
			default:
				throw new ArgumentOutOfRangeException("value");
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007990 File Offset: 0x00005B90
		Microsoft.Exchange.Data.Storage.Importance IConverter<Microsoft.Exchange.Entities.DataModel.Items.Importance, Microsoft.Exchange.Data.Storage.Importance>.Convert(Microsoft.Exchange.Entities.DataModel.Items.Importance value)
		{
			switch (value)
			{
			case Microsoft.Exchange.Entities.DataModel.Items.Importance.Low:
				return Microsoft.Exchange.Data.Storage.Importance.Low;
			case Microsoft.Exchange.Entities.DataModel.Items.Importance.Normal:
				return Microsoft.Exchange.Data.Storage.Importance.Normal;
			case Microsoft.Exchange.Entities.DataModel.Items.Importance.High:
				return Microsoft.Exchange.Data.Storage.Importance.High;
			default:
				throw new ArgumentOutOfRangeException("value");
			}
		}
	}
}
