using System;
using System.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D24 RID: 3364
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchObjectPropertyBag : ADPropertyBag
	{
		// Token: 0x0600741F RID: 29727 RVA: 0x00203C28 File Offset: 0x00201E28
		public SearchObjectPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
		}

		// Token: 0x06007420 RID: 29728 RVA: 0x00203C32 File Offset: 0x00201E32
		public SearchObjectPropertyBag() : base(false, 16)
		{
		}

		// Token: 0x17001EF5 RID: 7925
		// (get) Token: 0x06007421 RID: 29729 RVA: 0x00203C3D File Offset: 0x00201E3D
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return SearchObjectBaseSchema.ExchangeVersion;
			}
		}

		// Token: 0x17001EF6 RID: 7926
		// (get) Token: 0x06007422 RID: 29730 RVA: 0x00203C44 File Offset: 0x00201E44
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return SearchObjectBaseSchema.ObjectState;
			}
		}

		// Token: 0x17001EF7 RID: 7927
		// (get) Token: 0x06007423 RID: 29731 RVA: 0x00203C4B File Offset: 0x00201E4B
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return SearchObjectBaseSchema.Id;
			}
		}

		// Token: 0x06007424 RID: 29732 RVA: 0x00203C52 File Offset: 0x00201E52
		internal override MultiValuedPropertyBase CreateMultiValuedProperty(ProviderPropertyDefinition propertyDefinition, bool createAsReadOnly, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage)
		{
			if (propertyDefinition.Type == typeof(KindKeyword))
			{
				return new MultiValuedProperty<KindKeyword>(createAsReadOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage);
			}
			return ADValueConvertor.CreateGenericMultiValuedProperty(propertyDefinition, createAsReadOnly, values, invalidValues, readOnlyErrorMessage);
		}

		// Token: 0x06007425 RID: 29733 RVA: 0x00203C84 File Offset: 0x00201E84
		internal override object SerializeData(ProviderPropertyDefinition propertyDefinition, object input)
		{
			if (typeof(SearchObjectId) == propertyDefinition.Type)
			{
				return input;
			}
			return base.SerializeData(propertyDefinition, input);
		}

		// Token: 0x06007426 RID: 29734 RVA: 0x00203CA7 File Offset: 0x00201EA7
		internal override object DeserializeData(ProviderPropertyDefinition propertyDefinition, object input)
		{
			if (typeof(SearchObjectId) == propertyDefinition.Type)
			{
				return input;
			}
			return base.DeserializeData(propertyDefinition, input);
		}
	}
}
