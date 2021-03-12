using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005B0 RID: 1456
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConversionRecipientEntry
	{
		// Token: 0x06003BDD RID: 15325 RVA: 0x000F65F9 File Offset: 0x000F47F9
		internal ConversionRecipientEntry()
		{
			this.propertyValues = new MemoryPropertyBag();
			this.propertyValues.SetAllPropertiesLoaded();
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x000F6617 File Offset: 0x000F4817
		internal ConversionRecipientEntry(Participant participant, RecipientItemType recipientItemType)
		{
			this.propertyValues = new MemoryPropertyBag();
			this.propertyValues.SetAllPropertiesLoaded();
			this.Participant = participant;
			this.recipientItemType = recipientItemType;
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x000F6644 File Offset: 0x000F4844
		internal void SetProperty(StorePropertyDefinition property, object value, bool resetOldValue)
		{
			if (property.Equals(InternalSchema.RowId))
			{
				return;
			}
			if (property.Equals(InternalSchema.RecipientType))
			{
				this.recipientItemType = MapiUtil.MapiRecipientTypeToRecipientItemType((RecipientType)((int)value));
				return;
			}
			if (resetOldValue || !this.IsPropertySetOrDeleted(property))
			{
				this.propertyValues[property] = value;
			}
			this.cachedParticipant = null;
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x000F669E File Offset: 0x000F489E
		private void DeleteProperty(StorePropertyDefinition property, bool resetOldValue)
		{
			if (resetOldValue || !this.IsPropertySetOrDeleted(property))
			{
				this.propertyValues.Delete(property);
			}
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x000F66B8 File Offset: 0x000F48B8
		private bool IsPropertySetOrDeleted(StorePropertyDefinition property)
		{
			if (this.propertyValues.DeleteList.Contains(property))
			{
				return true;
			}
			object obj = this.propertyValues.TryGetProperty(property);
			return !(obj is PropertyError);
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x000F66F3 File Offset: 0x000F48F3
		internal object TryGetProperty(StorePropertyDefinition property)
		{
			return this.propertyValues.TryGetProperty(property);
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06003BE3 RID: 15331 RVA: 0x000F6701 File Offset: 0x000F4901
		internal RecipientItemType RecipientItemType
		{
			get
			{
				return this.recipientItemType;
			}
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06003BE4 RID: 15332 RVA: 0x000F6709 File Offset: 0x000F4909
		internal ICollection<NativeStorePropertyDefinition> AllNativeProperties
		{
			get
			{
				return this.propertyValues.AllNativeProperties;
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06003BE5 RID: 15333 RVA: 0x000F6716 File Offset: 0x000F4916
		// (set) Token: 0x06003BE6 RID: 15334 RVA: 0x000F6742 File Offset: 0x000F4942
		internal Participant Participant
		{
			get
			{
				if (this.cachedParticipant == null)
				{
					this.cachedParticipant = this.propertyValues.GetValueOrDefault<Participant>(InternalSchema.RecipientBaseParticipant);
				}
				return this.cachedParticipant;
			}
			set
			{
				this.cachedParticipant = null;
				this.propertyValues.SetOrDeleteProperty(InternalSchema.RecipientBaseParticipant, value);
			}
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x000F675C File Offset: 0x000F495C
		internal object TryGetProperty(NativeStorePropertyDefinition property)
		{
			return this.propertyValues.TryGetProperty(property);
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x000F676C File Offset: 0x000F496C
		internal T GetValueOrDefault<T>(NativeStorePropertyDefinition propertyDefinition)
		{
			return this.GetValueOrDefault<T>(propertyDefinition, default(T));
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x000F6789 File Offset: 0x000F4989
		internal T GetValueOrDefault<T>(NativeStorePropertyDefinition propertyDefinition, T defaultValue)
		{
			return PropertyBag.CheckPropertyValue<T>(propertyDefinition, this.TryGetProperty(propertyDefinition), defaultValue);
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x000F679C File Offset: 0x000F499C
		internal void CopyDependentProperties(ConversionRecipientEntry dependentEntry)
		{
			if (dependentEntry == null)
			{
				return;
			}
			foreach (NativeStorePropertyDefinition property in dependentEntry.AllNativeProperties)
			{
				object obj = dependentEntry.TryGetProperty(property);
				if (obj != null && !(obj is PropertyError))
				{
					this.SetProperty(property, obj, false);
				}
			}
			foreach (PropertyDefinition propertyDefinition in dependentEntry.propertyValues.DeleteList)
			{
				StorePropertyDefinition property2 = (StorePropertyDefinition)propertyDefinition;
				this.DeleteProperty(property2, false);
			}
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x000F6850 File Offset: 0x000F4A50
		public override bool Equals(object obj)
		{
			ConversionRecipientEntry conversionRecipientEntry = obj as ConversionRecipientEntry;
			return conversionRecipientEntry != null && (object.ReferenceEquals(this.Participant, conversionRecipientEntry.Participant) || (this.Participant != null && this.RecipientItemType == conversionRecipientEntry.RecipientItemType && this.Participant.AreAddressesEqual(conversionRecipientEntry.Participant)));
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x000F68B0 File Offset: 0x000F4AB0
		public override int GetHashCode()
		{
			int num = (int)((int)this.RecipientItemType << 16);
			if (this.Participant != null)
			{
				int num2 = num;
				string text;
				if ((text = this.Participant.RoutingType) == null)
				{
					text = (this.Participant.DisplayName ?? string.Empty);
				}
				num = (num2 ^ text.GetHashCode());
				num ^= (this.Participant.EmailAddress ?? string.Empty).ToLowerInvariant().GetHashCode();
			}
			return num;
		}

		// Token: 0x04001FD4 RID: 8148
		private RecipientItemType recipientItemType;

		// Token: 0x04001FD5 RID: 8149
		private MemoryPropertyBag propertyValues;

		// Token: 0x04001FD6 RID: 8150
		private Participant cachedParticipant;
	}
}
