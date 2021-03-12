using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200015E RID: 350
	[Serializable]
	internal class AirSyncReminder141Property : AirSyncReminderProperty
	{
		// Token: 0x0600100A RID: 4106 RVA: 0x0005B2C0 File Offset: 0x000594C0
		public AirSyncReminder141Property(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0005B2CC File Offset: 0x000594CC
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			base.InternalCopyFrom(srcProperty);
			IIntegerProperty integerProperty = (IIntegerProperty)srcProperty;
			if (integerProperty.IntegerData == -1)
			{
				base.CreateAirSyncNode(string.Empty, true);
			}
		}
	}
}
