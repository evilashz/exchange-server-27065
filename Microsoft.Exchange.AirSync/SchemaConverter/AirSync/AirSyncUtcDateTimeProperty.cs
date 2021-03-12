using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200016C RID: 364
	[Serializable]
	internal class AirSyncUtcDateTimeProperty : AirSyncProperty, IDateTimeProperty, IProperty
	{
		// Token: 0x0600103B RID: 4155 RVA: 0x0005BB53 File Offset: 0x00059D53
		public AirSyncUtcDateTimeProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0005BB5E File Offset: 0x00059D5E
		public AirSyncUtcDateTimeProperty(string xmlNodeNamespace, string airSyncTagName, AirSyncDateFormat format, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
			this.format = format;
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0005BB74 File Offset: 0x00059D74
		public ExDateTime DateTime
		{
			get
			{
				ExDateTime result;
				if (!ExDateTime.TryParseExact(base.XmlNode.InnerText, AirSyncUtcDateTimeProperty.formatString[(int)this.format], DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result))
				{
					throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidDateTime, null, false)
					{
						ErrorStringForProtocolLogger = "InvalidDateTimeInAirSyncUtcDateTime"
					};
				}
				return result;
			}
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0005BBC8 File Offset: 0x00059DC8
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IDateTimeProperty dateTimeProperty = srcProperty as IDateTimeProperty;
			if (dateTimeProperty == null)
			{
				throw new UnexpectedTypeException("IDateTimeProperty", srcProperty);
			}
			base.CreateAirSyncNode(dateTimeProperty.DateTime.ToString(AirSyncUtcDateTimeProperty.formatString[(int)this.format], DateTimeFormatInfo.InvariantInfo));
		}

		// Token: 0x04000A86 RID: 2694
		private static readonly string[] formatString = new string[]
		{
			"yyyyMMdd\\THHmmss\\Z",
			"yyyy-MM-dd\\THH:mm:ss.fff\\Z"
		};

		// Token: 0x04000A87 RID: 2695
		private AirSyncDateFormat format;
	}
}
