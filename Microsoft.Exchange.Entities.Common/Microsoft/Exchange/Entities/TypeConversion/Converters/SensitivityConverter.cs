using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Items;

namespace Microsoft.Exchange.Entities.TypeConversion.Converters
{
	// Token: 0x02000061 RID: 97
	internal struct SensitivityConverter : IConverter<Microsoft.Exchange.Data.Storage.Sensitivity, Microsoft.Exchange.Entities.DataModel.Items.Sensitivity>, IConverter<Microsoft.Exchange.Entities.DataModel.Items.Sensitivity, Microsoft.Exchange.Data.Storage.Sensitivity>
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00007A2B File Offset: 0x00005C2B
		public IConverter<Microsoft.Exchange.Data.Storage.Sensitivity, Microsoft.Exchange.Entities.DataModel.Items.Sensitivity> StorageToEntitiesConverter
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00007A38 File Offset: 0x00005C38
		public IConverter<Microsoft.Exchange.Entities.DataModel.Items.Sensitivity, Microsoft.Exchange.Data.Storage.Sensitivity> EntitiesToStorageConverter
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007A45 File Offset: 0x00005C45
		Microsoft.Exchange.Entities.DataModel.Items.Sensitivity IConverter<Microsoft.Exchange.Data.Storage.Sensitivity, Microsoft.Exchange.Entities.DataModel.Items.Sensitivity>.Convert(Microsoft.Exchange.Data.Storage.Sensitivity value)
		{
			return SensitivityConverter.mappingConverter.Convert(value);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007A52 File Offset: 0x00005C52
		Microsoft.Exchange.Data.Storage.Sensitivity IConverter<Microsoft.Exchange.Entities.DataModel.Items.Sensitivity, Microsoft.Exchange.Data.Storage.Sensitivity>.Convert(Microsoft.Exchange.Entities.DataModel.Items.Sensitivity value)
		{
			return SensitivityConverter.mappingConverter.Reverse(value);
		}

		// Token: 0x040000A8 RID: 168
		private static SimpleMappingConverter<Microsoft.Exchange.Data.Storage.Sensitivity, Microsoft.Exchange.Entities.DataModel.Items.Sensitivity> mappingConverter = SimpleMappingConverter<Microsoft.Exchange.Data.Storage.Sensitivity, Microsoft.Exchange.Entities.DataModel.Items.Sensitivity>.CreateStrictConverter(new Tuple<Microsoft.Exchange.Data.Storage.Sensitivity, Microsoft.Exchange.Entities.DataModel.Items.Sensitivity>[]
		{
			new Tuple<Microsoft.Exchange.Data.Storage.Sensitivity, Microsoft.Exchange.Entities.DataModel.Items.Sensitivity>(Microsoft.Exchange.Data.Storage.Sensitivity.Normal, Microsoft.Exchange.Entities.DataModel.Items.Sensitivity.Normal),
			new Tuple<Microsoft.Exchange.Data.Storage.Sensitivity, Microsoft.Exchange.Entities.DataModel.Items.Sensitivity>(Microsoft.Exchange.Data.Storage.Sensitivity.Personal, Microsoft.Exchange.Entities.DataModel.Items.Sensitivity.Personal),
			new Tuple<Microsoft.Exchange.Data.Storage.Sensitivity, Microsoft.Exchange.Entities.DataModel.Items.Sensitivity>(Microsoft.Exchange.Data.Storage.Sensitivity.Private, Microsoft.Exchange.Entities.DataModel.Items.Sensitivity.Private),
			new Tuple<Microsoft.Exchange.Data.Storage.Sensitivity, Microsoft.Exchange.Entities.DataModel.Items.Sensitivity>(Microsoft.Exchange.Data.Storage.Sensitivity.CompanyConfidential, Microsoft.Exchange.Entities.DataModel.Items.Sensitivity.Confidential)
		});
	}
}
