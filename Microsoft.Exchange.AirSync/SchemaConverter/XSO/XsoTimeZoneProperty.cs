using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200023C RID: 572
	[Serializable]
	internal class XsoTimeZoneProperty : XsoProperty, ITimeZoneProperty, IProperty
	{
		// Token: 0x0600151D RID: 5405 RVA: 0x0007BB88 File Offset: 0x00079D88
		public XsoTimeZoneProperty() : base(null, new PropertyDefinition[]
		{
			CalendarItemInstanceSchema.StartTime,
			CalendarItemBaseSchema.TimeZoneBlob,
			CalendarItemBaseSchema.TimeZoneDefinitionRecurring,
			ItemSchema.TimeZoneDefinitionStart,
			CalendarItemBaseSchema.TimeZoneDefinitionEnd
		})
		{
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0007BBCC File Offset: 0x00079DCC
		public XsoTimeZoneProperty(PropertyType type) : base(null, type, new PropertyDefinition[]
		{
			CalendarItemInstanceSchema.StartTime,
			CalendarItemBaseSchema.TimeZoneBlob,
			CalendarItemBaseSchema.TimeZoneDefinitionRecurring,
			ItemSchema.TimeZoneDefinitionStart,
			CalendarItemBaseSchema.TimeZoneDefinitionEnd
		})
		{
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x0007BC14 File Offset: 0x00079E14
		public ExTimeZone TimeZone
		{
			get
			{
				ExTimeZone promotedTimeZoneFromItem = TimeZoneHelper.GetPromotedTimeZoneFromItem(base.XsoItem as Item);
				if (promotedTimeZoneFromItem != null)
				{
					return promotedTimeZoneFromItem;
				}
				return ExTimeZone.CurrentTimeZone;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0007BC3C File Offset: 0x00079E3C
		public ExDateTime EffectiveTime
		{
			get
			{
				return (ExDateTime)base.XsoItem.TryGetProperty(CalendarItemInstanceSchema.StartTime);
			}
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0007BC54 File Offset: 0x00079E54
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			ITimeZoneProperty timeZoneProperty = srcProperty as ITimeZoneProperty;
			if (timeZoneProperty == null)
			{
				throw new UnexpectedTypeException("ITimeZoneProperty", srcProperty);
			}
			if (!this.TimeZoneExistsOnItem() || !TimeZoneConverter.IsClientTimeZoneEquivalentToServerTimeZoneRule(timeZoneProperty.TimeZone, this.TimeZone, this.EffectiveTime))
			{
				base.XsoItem[CalendarItemBaseSchema.StartTimeZone] = timeZoneProperty.TimeZone;
				base.XsoItem[CalendarItemBaseSchema.EndTimeZone] = timeZoneProperty.TimeZone;
				base.XsoItem.Delete(CalendarItemBaseSchema.TimeZoneBlob);
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0007BCD4 File Offset: 0x00079ED4
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			if (!(srcProperty is ITimeZoneProperty))
			{
				throw new UnexpectedTypeException("ITimeZoneProperty", srcProperty);
			}
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0007BCF7 File Offset: 0x00079EF7
		internal bool TimeZoneExistsOnItem()
		{
			return base.XsoItem.GetValueOrDefault<byte[]>(ItemSchema.TimeZoneDefinitionStart) != null;
		}
	}
}
