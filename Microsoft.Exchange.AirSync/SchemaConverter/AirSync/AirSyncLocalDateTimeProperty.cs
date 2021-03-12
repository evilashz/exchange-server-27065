using System;
using System.Globalization;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000157 RID: 343
	[Serializable]
	internal class AirSyncLocalDateTimeProperty : AirSyncProperty, IDateTimeProperty, IProperty
	{
		// Token: 0x06000FF4 RID: 4084 RVA: 0x0005ACF4 File Offset: 0x00058EF4
		public AirSyncLocalDateTimeProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0005ACFF File Offset: 0x00058EFF
		public AirSyncLocalDateTimeProperty(string xmlNodeNamespace, string airSyncTagName, AirSyncDateFormat format, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
			this.format = format;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x0005AD12 File Offset: 0x00058F12
		public ExDateTime DateTime
		{
			get
			{
				return ExDateTime.ParseExact(base.XmlNode.InnerText, AirSyncLocalDateTimeProperty.formatString[(int)this.format], DateTimeFormatInfo.InvariantInfo);
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0005AD38 File Offset: 0x00058F38
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IDateTimeProperty dateTimeProperty = srcProperty as IDateTimeProperty;
			if (dateTimeProperty == null)
			{
				throw new UnexpectedTypeException("IDateTimeProperty", srcProperty);
			}
			base.CreateAirSyncNode(dateTimeProperty.DateTime.ToString(AirSyncLocalDateTimeProperty.formatString[(int)this.format], DateTimeFormatInfo.InvariantInfo));
		}

		// Token: 0x04000A7B RID: 2683
		private static readonly string[] formatString = new string[]
		{
			"yyyyMMdd\\THHmmss",
			"yyyy-MM-dd\\THH:mm:ss.fff"
		};

		// Token: 0x04000A7C RID: 2684
		private AirSyncDateFormat format;
	}
}
