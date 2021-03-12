using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001B3 RID: 435
	internal interface IAirSyncVersionFactory
	{
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001244 RID: 4676
		string VersionString { get; }

		// Token: 0x06001245 RID: 4677
		AirSyncSchemaState CreateCalendarSchema();

		// Token: 0x06001246 RID: 4678
		AirSyncSchemaState CreateEmailSchema(IdMapping idMapping);

		// Token: 0x06001247 RID: 4679
		AirSyncSchemaState CreateContactsSchema();

		// Token: 0x06001248 RID: 4680
		AirSyncSchemaState CreateNotesSchema();

		// Token: 0x06001249 RID: 4681
		AirSyncSchemaState CreateSmsSchema();

		// Token: 0x0600124A RID: 4682
		AirSyncSchemaState CreateConsumerSmsAndMmsSchema();

		// Token: 0x0600124B RID: 4683
		AirSyncSchemaState CreateTasksSchema();

		// Token: 0x0600124C RID: 4684
		AirSyncSchemaState CreateRecipientInfoCacheSchema();

		// Token: 0x0600124D RID: 4685
		IAirSyncMissingPropertyStrategy CreateMissingPropertyStrategy(Dictionary<string, bool> supportedTags);

		// Token: 0x0600124E RID: 4686
		IAirSyncMissingPropertyStrategy CreateReadFlagMissingPropertyStrategy();

		// Token: 0x0600124F RID: 4687
		string GetClassFromMessageClass(string messageClass);
	}
}
