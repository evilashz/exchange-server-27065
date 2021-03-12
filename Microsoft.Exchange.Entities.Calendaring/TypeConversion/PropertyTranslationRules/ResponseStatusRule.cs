using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.TypeConversion;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyTranslationRules
{
	// Token: 0x0200009B RID: 155
	internal class ResponseStatusRule : IStorageTranslationRule<ICalendarItemBase, IEvent>, IPropertyValueCollectionTranslationRule<ICalendarItemBase, Microsoft.Exchange.Data.PropertyDefinition, IEvent>, ITranslationRule<ICalendarItemBase, IEvent>
	{
		// Token: 0x060003AA RID: 938 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		public ResponseStatusRule()
		{
			this.StorageDependencies = CalendarItemAccessors.ResponseType.Dependencies.Union(CalendarItemAccessors.ReplyTime.Dependencies);
			this.StoragePropertyGroup = PropertyChangeMetadata.PropertyGroup.Response;
			this.EntityProperties = new Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[]
			{
				Event.Accessors.ResponseStatus.PropertyDefinition
			};
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000DD54 File Offset: 0x0000BF54
		public IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> StorageDependencies { get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000DD5D File Offset: 0x0000BF5D
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000DD65 File Offset: 0x0000BF65
		public PropertyChangeMetadata.PropertyGroup StoragePropertyGroup { get; private set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000DD6E File Offset: 0x0000BF6E
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000DD76 File Offset: 0x0000BF76
		public IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition> EntityProperties { get; private set; }

		// Token: 0x060003B1 RID: 945 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public void FromLeftToRightType(ICalendarItemBase calendarItemBase, IEvent theEvent)
		{
			this.FromLeftToRight(theEvent, delegate(out ResponseType value)
			{
				return CalendarItemAccessors.ResponseType.TryGetValue(calendarItemBase, out value);
			}, delegate(out ExDateTime value)
			{
				return CalendarItemAccessors.ReplyTime.TryGetValue(calendarItemBase, out value);
			});
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000DDEC File Offset: 0x0000BFEC
		public void FromRightToLeftType(ICalendarItemBase calendarItemBase, IEvent theEvent)
		{
			ResponseStatus container;
			if (Event.Accessors.ResponseStatus.TryGetValue(theEvent, out container))
			{
				ResponseType value;
				if (ResponseStatus.Accessors.Response.TryGetValue(container, out value))
				{
					CalendarItemAccessors.ResponseType.Set(calendarItemBase, this.responseTypeConverter.Convert(value));
				}
				ExDateTime value2;
				if (ResponseStatus.Accessors.Time.TryGetValue(container, out value2))
				{
					CalendarItemAccessors.ReplyTime.Set(calendarItemBase, value2);
				}
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000DE84 File Offset: 0x0000C084
		public void FromPropertyValues(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, IEvent right)
		{
			this.FromLeftToRight(right, delegate(out ResponseType value)
			{
				return CalendarItemAccessors.ResponseType.TryGetValue(propertyIndices, values, out value);
			}, delegate(out ExDateTime value)
			{
				return CalendarItemAccessors.ReplyTime.TryGetValue(propertyIndices, values, out value);
			});
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		private void FromLeftToRight(IEvent entity, ResponseStatusRule.TryGetValueFunc<ResponseType> responseGetter, ResponseStatusRule.TryGetValueFunc<ExDateTime> replyTimeGetter)
		{
			ResponseStatus responseStatus = null;
			ResponseType value;
			if (responseGetter != null && responseGetter(out value))
			{
				responseStatus = new ResponseStatus();
				ResponseStatus.Accessors.Response.Set(responseStatus, this.responseTypeConverter.Convert(value));
			}
			ExDateTime value2;
			if (replyTimeGetter != null && replyTimeGetter(out value2))
			{
				if (responseStatus == null)
				{
					responseStatus = new ResponseStatus();
				}
				ResponseStatus.Accessors.Time.Set(responseStatus, value2);
			}
			if (responseStatus != null)
			{
				Event.Accessors.ResponseStatus.Set(entity, responseStatus);
			}
		}

		// Token: 0x04000157 RID: 343
		private ResponseTypeConverter responseTypeConverter = default(ResponseTypeConverter);

		// Token: 0x0200009C RID: 156
		// (Invoke) Token: 0x060003B6 RID: 950
		public delegate bool TryGetValueFunc<TValue>(out TValue value);
	}
}
