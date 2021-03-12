using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C0C RID: 3084
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal abstract class AggregateRecipientProperty : SmartPropertyDefinition
	{
		// Token: 0x06006E0A RID: 28170 RVA: 0x001D8C00 File Offset: 0x001D6E00
		protected AggregateRecipientProperty(string displayName, NativeStorePropertyDefinition storeComputedProperty, StorePropertyDefinition recipientStringProperty) : base(displayName, typeof(string), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, (storeComputedProperty != null) ? new PropertyDependency[]
		{
			new PropertyDependency(storeComputedProperty, PropertyDependencyType.NeedForRead)
		} : Array<PropertyDependency>.Empty)
		{
			this.storeComputedProperty = storeComputedProperty;
			this.recipientStringProperty = recipientStringProperty;
			this.RegisterFilterTranslation();
		}

		// Token: 0x17001DD5 RID: 7637
		// (get) Token: 0x06006E0B RID: 28171 RVA: 0x001D8C54 File Offset: 0x001D6E54
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				StorePropertyCapabilities storePropertyCapabilities = base.Capabilities;
				if (this.storeComputedProperty != null)
				{
					storePropertyCapabilities |= (StorePropertyCapabilities.CanQuery | StorePropertyCapabilities.CanSortBy);
				}
				return storePropertyCapabilities;
			}
		}

		// Token: 0x06006E0C RID: 28172 RVA: 0x001D8C75 File Offset: 0x001D6E75
		protected sealed override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			base.InternalSetValue(propertyBag, value);
		}

		// Token: 0x06006E0D RID: 28173 RVA: 0x001D8C7F File Offset: 0x001D6E7F
		protected sealed override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			base.InternalDeleteValue(propertyBag);
		}

		// Token: 0x06006E0E RID: 28174 RVA: 0x001D8C88 File Offset: 0x001D6E88
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			MessageItem messageItem = propertyBag.Context.StoreObject as MessageItem;
			CalendarItemBase calendarItemBase = propertyBag.Context.StoreObject as CalendarItemBase;
			if (messageItem != null)
			{
				return this.BuildAggregatedValue<Recipient>(messageItem.Recipients);
			}
			if (calendarItemBase != null)
			{
				return this.BuildAggregatedValue<Attendee>(calendarItemBase.AttendeeCollection);
			}
			if (this.storeComputedProperty != null)
			{
				object value = propertyBag.GetValue(this.storeComputedProperty);
				if (!PropertyError.IsPropertyError(value))
				{
					return value;
				}
			}
			return new PropertyError(this, PropertyErrorCode.GetCalculatedPropertyError);
		}

		// Token: 0x06006E0F RID: 28175 RVA: 0x001D8D00 File Offset: 0x001D6F00
		internal override SortBy[] GetNativeSortBy(SortOrder sortOrder)
		{
			if (this.storeComputedProperty == null)
			{
				return base.GetNativeSortBy(sortOrder);
			}
			return new SortBy[]
			{
				new SortBy(this.storeComputedProperty, sortOrder)
			};
		}

		// Token: 0x06006E10 RID: 28176 RVA: 0x001D8D34 File Offset: 0x001D6F34
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return base.SinglePropertyNativeFilterToSmartFilter(filter, this.storeComputedProperty);
		}

		// Token: 0x06006E11 RID: 28177 RVA: 0x001D8D43 File Offset: 0x001D6F43
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, this.storeComputedProperty);
		}

		// Token: 0x06006E12 RID: 28178 RVA: 0x001D8D52 File Offset: 0x001D6F52
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ExistsFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(TextFilter));
		}

		// Token: 0x06006E13 RID: 28179
		protected abstract bool IsRecipientIncluded(RecipientBase recipientBase);

		// Token: 0x06006E14 RID: 28180 RVA: 0x001D8D84 File Offset: 0x001D6F84
		private void BuildStringForOneRecipient(StringBuilder sb, RecipientBase recipientBase)
		{
			if (!this.IsRecipientIncluded(recipientBase))
			{
				return;
			}
			if (sb.Length > 0)
			{
				sb.Append("; ");
			}
			sb.Append(recipientBase.TryGetProperty(this.recipientStringProperty) as string);
		}

		// Token: 0x06006E15 RID: 28181 RVA: 0x001D8DC0 File Offset: 0x001D6FC0
		private string BuildAggregatedValue<T>(IRecipientBaseCollection<T> recipientBaseCollection) where T : RecipientBase
		{
			StringBuilder stringBuilder = new StringBuilder(recipientBaseCollection.Count * (15 + "; ".Length));
			foreach (T t in recipientBaseCollection)
			{
				RecipientBase recipientBase = t;
				this.BuildStringForOneRecipient(stringBuilder, recipientBase);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04003EF3 RID: 16115
		private const string Separator = "; ";

		// Token: 0x04003EF4 RID: 16116
		private readonly NativeStorePropertyDefinition storeComputedProperty;

		// Token: 0x04003EF5 RID: 16117
		private readonly StorePropertyDefinition recipientStringProperty;
	}
}
