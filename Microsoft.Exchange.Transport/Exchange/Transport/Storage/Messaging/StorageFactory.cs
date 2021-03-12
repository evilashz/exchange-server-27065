using System;
using Microsoft.Exchange.Transport.Storage.Messaging.Null;
using Microsoft.Exchange.Transport.Storage.Messaging.Utah;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x02000104 RID: 260
	internal class StorageFactory
	{
		// Token: 0x06000B2D RID: 2861 RVA: 0x00026CE8 File Offset: 0x00024EE8
		public static IMessagingDatabase GetNewDatabaseInstance()
		{
			switch (StorageFactory.SchemaToUse)
			{
			case StorageFactory.Schema.NullSchema:
				return new Microsoft.Exchange.Transport.Storage.Messaging.Null.MessagingDatabase();
			case StorageFactory.Schema.UtahSchema:
				return new Microsoft.Exchange.Transport.Storage.Messaging.Utah.MessagingDatabase();
			}
			return null;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00026D1C File Offset: 0x00024F1C
		public static IBootLoader CreateBootScanner()
		{
			StorageFactory.Schema schemaToUse = StorageFactory.SchemaToUse;
			if (schemaToUse == StorageFactory.Schema.UtahSchema)
			{
				return new BootScanner();
			}
			return null;
		}

		// Token: 0x040004CE RID: 1230
		public static readonly StorageFactory.Schema DefaultSchema = TransportAppConfig.GetConfigEnum<StorageFactory.Schema>("QueueDatabaseSchema", StorageFactory.Schema.UtahSchema);

		// Token: 0x040004CF RID: 1231
		public static StorageFactory.Schema SchemaToUse = StorageFactory.DefaultSchema;

		// Token: 0x02000105 RID: 261
		public enum Schema
		{
			// Token: 0x040004D1 RID: 1233
			NullSchema,
			// Token: 0x040004D2 RID: 1234
			UtahSchema = 2
		}
	}
}
