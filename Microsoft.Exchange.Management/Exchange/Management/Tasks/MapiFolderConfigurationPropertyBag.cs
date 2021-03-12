using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mapi;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200042E RID: 1070
	[Serializable]
	internal class MapiFolderConfigurationPropertyBag : PropertyBag
	{
		// Token: 0x06002583 RID: 9603 RVA: 0x00097461 File Offset: 0x00095661
		public MapiFolderConfigurationPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x0009746B File Offset: 0x0009566B
		public MapiFolderConfigurationPropertyBag() : this(false, 16)
		{
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002585 RID: 9605 RVA: 0x00097476 File Offset: 0x00095676
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return MapiFolderConfigurationSchema.ExchangeVersion;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x0009747D File Offset: 0x0009567D
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return MapiFolderConfigurationSchema.ObjectState;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x00097484 File Offset: 0x00095684
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return MapiFolderConfigurationSchema.Identity;
			}
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x0009748C File Offset: 0x0009568C
		internal override object SerializeData(ProviderPropertyDefinition propertyDefinition, object input)
		{
			if (typeof(MapiObjectId) == propertyDefinition.Type)
			{
				return input;
			}
			if (typeof(ELCFolderIdParameter) == propertyDefinition.Type)
			{
				return input;
			}
			if (typeof(ByteQuantifiedSize) == propertyDefinition.Type)
			{
				return input;
			}
			return base.SerializeData(propertyDefinition, input);
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000974EC File Offset: 0x000956EC
		internal override object DeserializeData(ProviderPropertyDefinition propertyDefinition, object input)
		{
			if (typeof(MapiObjectId) == propertyDefinition.Type)
			{
				return input;
			}
			if (typeof(ELCFolderIdParameter) == propertyDefinition.Type)
			{
				return input;
			}
			if (typeof(ByteQuantifiedSize) == propertyDefinition.Type)
			{
				return input;
			}
			return base.DeserializeData(propertyDefinition, input);
		}
	}
}
