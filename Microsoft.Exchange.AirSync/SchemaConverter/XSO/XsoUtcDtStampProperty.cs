using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000240 RID: 576
	[Serializable]
	internal class XsoUtcDtStampProperty : XsoUtcDateTimeProperty
	{
		// Token: 0x0600152D RID: 5421 RVA: 0x0007BFF9 File Offset: 0x0007A1F9
		public XsoUtcDtStampProperty() : base(null)
		{
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0007C002 File Offset: 0x0007A202
		public XsoUtcDtStampProperty(PropertyType type) : base(null, type)
		{
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x0007C00C File Offset: 0x0007A20C
		public override ExDateTime DateTime
		{
			get
			{
				CalendarItemBase calendarItemBase = base.XsoItem as CalendarItemBase;
				if (calendarItemBase == null)
				{
					string message = string.Format("[XsoUtcDtStampProperty.get_DateTime] XsoItem is NOT a CalendarItemBase (cast resulted in null).  Actual item type: {0}", (base.XsoItem == null) ? "<NULL>" : base.XsoItem.GetType().FullName);
					AirSyncDiagnostics.TraceError(ExTraceGlobals.XsoTracer, this, message);
					throw new InvalidOperationException(message);
				}
				object obj = calendarItemBase.TryGetProperty(CalendarItemBaseSchema.OwnerCriticalChangeTime);
				if (obj != null && obj is ExDateTime)
				{
					return ExTimeZone.UtcTimeZone.ConvertDateTime((ExDateTime)obj);
				}
				return ExTimeZone.UtcTimeZone.ConvertDateTime(calendarItemBase.LastModifiedTime);
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0007C0A0 File Offset: 0x0007A2A0
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			if (PropertyState.SetToDefault == srcProperty.State)
			{
				throw new ConversionException("Object type does not support setting to default");
			}
			ExDateTime exDateTime = ((IDateTimeProperty)srcProperty).DateTime;
			exDateTime = ExTimeZone.UtcTimeZone.ConvertDateTime(exDateTime);
			base.XsoItem[StoreObjectSchema.LastModifiedTime] = exDateTime;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0007C0F0 File Offset: 0x0007A2F0
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			base.XsoItem.DeleteProperties(new PropertyDefinition[]
			{
				StoreObjectSchema.LastModifiedTime
			});
		}
	}
}
