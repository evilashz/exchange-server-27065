using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV20;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001B4 RID: 436
	internal class AirSyncVersionFactoryV20 : IAirSyncVersionFactory
	{
		// Token: 0x06001250 RID: 4688 RVA: 0x00063004 File Offset: 0x00061204
		static AirSyncVersionFactoryV20()
		{
			AirSyncVersionFactoryV20.classToQueryFilterDictionary.Add("Email", EmailPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV20.classToQueryFilterDictionary.Add("Calendar", CalendarPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV20.classToQueryFilterDictionary.Add("Contacts", ContactsPrototypeSchemaState.SupportedClassQueryFilter);
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x00063058 File Offset: 0x00061258
		public string VersionString
		{
			get
			{
				return "2.0";
			}
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0006305F File Offset: 0x0006125F
		public AirSyncSchemaState CreateCalendarSchema()
		{
			return new CalendarPrototypeSchemaState();
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00063066 File Offset: 0x00061266
		public AirSyncSchemaState CreateEmailSchema(IdMapping identifierMapping)
		{
			return new EmailPrototypeSchemaState(identifierMapping);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0006306E File Offset: 0x0006126E
		public AirSyncSchemaState CreateContactsSchema()
		{
			return new ContactsPrototypeSchemaState();
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00063075 File Offset: 0x00061275
		public AirSyncSchemaState CreateTasksSchema()
		{
			return null;
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00063078 File Offset: 0x00061278
		public AirSyncSchemaState CreateNotesSchema()
		{
			return null;
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0006307B File Offset: 0x0006127B
		public AirSyncSchemaState CreateSmsSchema()
		{
			return null;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0006307E File Offset: 0x0006127E
		public AirSyncSchemaState CreateConsumerSmsAndMmsSchema()
		{
			return null;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00063081 File Offset: 0x00061281
		public AirSyncSchemaState CreateRecipientInfoCacheSchema()
		{
			return null;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00063084 File Offset: 0x00061284
		public IAirSyncMissingPropertyStrategy CreateMissingPropertyStrategy(Dictionary<string, bool> supportedTags)
		{
			return new AirSyncSetToDefaultStrategy(supportedTags);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0006308C File Offset: 0x0006128C
		public IAirSyncMissingPropertyStrategy CreateReadFlagMissingPropertyStrategy()
		{
			return new AirSyncSetToUnmodifiedStrategy();
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00063094 File Offset: 0x00061294
		public string GetClassFromMessageClass(string messageClass)
		{
			SinglePropertyBag propertyBag = new SinglePropertyBag(StoreObjectSchema.ItemClass, messageClass);
			foreach (KeyValuePair<string, QueryFilter> keyValuePair in AirSyncVersionFactoryV20.classToQueryFilterDictionary)
			{
				if (EvaluatableFilter.Evaluate(keyValuePair.Value, propertyBag))
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x04000B5A RID: 2906
		private static Dictionary<string, QueryFilter> classToQueryFilterDictionary = new Dictionary<string, QueryFilter>(3);
	}
}
