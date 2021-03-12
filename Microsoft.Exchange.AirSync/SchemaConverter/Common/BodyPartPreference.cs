using System;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x0200017E RID: 382
	internal class BodyPartPreference : BodyPreference
	{
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x0005CB3A File Offset: 0x0005AD3A
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x0005CB44 File Offset: 0x0005AD44
		public override BodyType Type
		{
			get
			{
				return base.Type;
			}
			set
			{
				if (value != BodyType.Html)
				{
					throw new AirSyncPermanentException(StatusCode.BodyPartPreferenceTypeNotSupported, null, false)
					{
						ErrorStringForProtocolLogger = "InvalidBodyPartBodyType"
					};
				}
				base.Type = value;
			}
		}
	}
}
