using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200012D RID: 301
	[Serializable]
	internal class AirSyncBinaryTimeZoneProperty : AirSyncProperty, ITimeZoneProperty, IProperty
	{
		// Token: 0x06000F5B RID: 3931 RVA: 0x00058438 File Offset: 0x00056638
		public AirSyncBinaryTimeZoneProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00058444 File Offset: 0x00056644
		public ExTimeZone TimeZone
		{
			get
			{
				byte[] timeZoneInformation = Convert.FromBase64String(base.XmlNode.InnerText);
				return TimeZoneConverter.GetExTimeZone(timeZoneInformation);
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x00058468 File Offset: 0x00056668
		public ExDateTime EffectiveTime
		{
			get
			{
				return ExDateTime.MinValue;
			}
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00058470 File Offset: 0x00056670
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			ITimeZoneProperty timeZoneProperty = srcProperty as ITimeZoneProperty;
			if (timeZoneProperty == null)
			{
				throw new UnexpectedTypeException("ITimeZoneProperty", srcProperty);
			}
			string strData = Convert.ToBase64String(TimeZoneConverter.GetBytes(timeZoneProperty.TimeZone, timeZoneProperty.EffectiveTime));
			base.CreateAirSyncNode(strData);
		}
	}
}
