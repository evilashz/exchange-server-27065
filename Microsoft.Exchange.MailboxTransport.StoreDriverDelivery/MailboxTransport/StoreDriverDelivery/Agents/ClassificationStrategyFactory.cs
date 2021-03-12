using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Clutter;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000077 RID: 119
	internal class ClassificationStrategyFactory
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x00014F44 File Offset: 0x00013144
		public static IDeliveryClassificationStrategy Create(MailboxSession session, VariantConfigurationSnapshot snapshot)
		{
			IDeliveryClassificationStrategy result = null;
			if (snapshot != null && ClutterUtilities.IsClassificationEnabled(session, snapshot))
			{
				result = new FolderBasedClassificationStrategy();
			}
			return result;
		}
	}
}
