using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200014E RID: 334
	[Serializable]
	internal class AirSyncExDateTimeProperty : AirSyncProperty, IDateTimeProperty, IProperty
	{
		// Token: 0x06000FDE RID: 4062 RVA: 0x0005A458 File Offset: 0x00058658
		public AirSyncExDateTimeProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0005A463 File Offset: 0x00058663
		public ExDateTime DateTime
		{
			get
			{
				return TimeZoneConverter.Parse(base.XmlNode.InnerText, "AirSyncExDateTime");
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0005A47C File Offset: 0x0005867C
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IDateTimeProperty dateTimeProperty = srcProperty as IDateTimeProperty;
			if (dateTimeProperty == null)
			{
				throw new UnexpectedTypeException("IDateTimeProperty", srcProperty);
			}
			base.CreateAirSyncNode(TimeZoneConverter.ToString(dateTimeProperty.DateTime));
		}
	}
}
