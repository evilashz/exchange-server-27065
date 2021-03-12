using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000208 RID: 520
	[Serializable]
	internal class XsoBusyStatusProperty : XsoProperty, IBusyStatusProperty, IProperty
	{
		// Token: 0x06001426 RID: 5158 RVA: 0x000745F7 File Offset: 0x000727F7
		public XsoBusyStatusProperty() : base(CalendarItemBaseSchema.FreeBusyStatus)
		{
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00074604 File Offset: 0x00072804
		public XsoBusyStatusProperty(BusyType defaultValue) : base(CalendarItemBaseSchema.FreeBusyStatus)
		{
			this.defaultValue = defaultValue;
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00074618 File Offset: 0x00072818
		public XsoBusyStatusProperty(BusyType defaultValue, PropertyType type) : base(CalendarItemBaseSchema.FreeBusyStatus, type)
		{
			this.defaultValue = defaultValue;
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0007462D File Offset: 0x0007282D
		public BusyType BusyStatus
		{
			get
			{
				return (BusyType)base.XsoItem.TryGetProperty(base.PropertyDef);
			}
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00074645 File Offset: 0x00072845
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			base.XsoItem[base.PropertyDef] = ((IBusyStatusProperty)srcProperty).BusyStatus;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00074668 File Offset: 0x00072868
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			base.XsoItem[base.PropertyDef] = this.defaultValue;
		}

		// Token: 0x04000C56 RID: 3158
		private BusyType defaultValue;
	}
}
