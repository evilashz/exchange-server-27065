using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200085E RID: 2142
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BlobRecipient
	{
		// Token: 0x06005088 RID: 20616 RVA: 0x0014E6F4 File Offset: 0x0014C8F4
		internal BlobRecipient(ExTimeZone timeZone)
		{
			this.propertyBag = new MemoryPropertyBag();
			this.propertyBag.ExTimeZone = timeZone;
			this.propertyBag.SetAllPropertiesLoaded();
		}

		// Token: 0x06005089 RID: 20617 RVA: 0x0014E71E File Offset: 0x0014C91E
		internal BlobRecipient(RecipientBase recipient)
		{
			this.propertyBag = new MemoryPropertyBag(recipient.CoreRecipient.GetMemoryPropertyBag());
			this.propertyBag.SetAllPropertiesLoaded();
		}

		// Token: 0x0600508A RID: 20618 RVA: 0x0014E747 File Offset: 0x0014C947
		internal BlobRecipient(Participant participant)
		{
			this.propertyBag = new MemoryPropertyBag();
			this.propertyBag[InternalSchema.RecipientBaseParticipant] = participant;
			this.propertyBag.SetAllPropertiesLoaded();
		}

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x0600508B RID: 20619 RVA: 0x0014E776 File Offset: 0x0014C976
		internal Participant Participant
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<Participant>(InternalSchema.RecipientBaseParticipant);
			}
		}

		// Token: 0x0600508C RID: 20620 RVA: 0x0014E788 File Offset: 0x0014C988
		internal object TryGetProperty(PropertyDefinition property)
		{
			StorePropertyDefinition property2 = InternalSchema.ToStorePropertyDefinition(property);
			return this.TryGetProperty(property2);
		}

		// Token: 0x0600508D RID: 20621 RVA: 0x0014E7A3 File Offset: 0x0014C9A3
		internal object TryGetProperty(StorePropertyDefinition property)
		{
			return this.propertyBag.TryGetProperty(property);
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x0014E7B4 File Offset: 0x0014C9B4
		internal T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition)
		{
			return this.propertyBag.GetValueOrDefault<T>(propertyDefinition, default(T));
		}

		// Token: 0x0600508F RID: 20623 RVA: 0x0014E7D6 File Offset: 0x0014C9D6
		internal T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition, T defaultValue)
		{
			return this.propertyBag.GetValueOrDefault<T>(propertyDefinition, defaultValue);
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x0014E7E5 File Offset: 0x0014C9E5
		internal T? GetValueAsNullable<T>(StorePropertyDefinition propertyDefinition) where T : struct
		{
			return this.propertyBag.GetValueAsNullable<T>(propertyDefinition);
		}

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x06005091 RID: 20625 RVA: 0x0014E7F3 File Offset: 0x0014C9F3
		internal IDictionary<PropertyDefinition, object> PropertyValues
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x06005092 RID: 20626 RVA: 0x0014E7FB File Offset: 0x0014C9FB
		internal PropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x1700169D RID: 5789
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return this.propertyBag[propertyDefinition];
			}
			set
			{
				this.propertyBag[propertyDefinition] = value;
			}
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x0014E820 File Offset: 0x0014CA20
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			return this.propertyBag.GetProperties<PropertyDefinition>(propertyDefinitionArray);
		}

		// Token: 0x04002C26 RID: 11302
		private MemoryPropertyBag propertyBag;
	}
}
