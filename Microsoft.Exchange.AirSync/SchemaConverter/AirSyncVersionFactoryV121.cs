using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV121;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001B8 RID: 440
	internal class AirSyncVersionFactoryV121 : IAirSyncVersionFactory
	{
		// Token: 0x06001288 RID: 4744 RVA: 0x00063464 File Offset: 0x00061664
		static AirSyncVersionFactoryV121()
		{
			AirSyncVersionFactoryV121.classToQueryFilterDictionary.Add("Email", EmailPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV121.classToQueryFilterDictionary.Add("Calendar", CalendarPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV121.classToQueryFilterDictionary.Add("Contacts", ContactsPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV121.classToQueryFilterDictionary.Add("Tasks", TasksPrototypeSchemaState.SupportedClassQueryFilter);
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x000634CC File Offset: 0x000616CC
		public string VersionString
		{
			get
			{
				return "12.1";
			}
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000634D3 File Offset: 0x000616D3
		public AirSyncSchemaState CreateCalendarSchema()
		{
			return new CalendarPrototypeSchemaState();
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000634DA File Offset: 0x000616DA
		public AirSyncSchemaState CreateEmailSchema(IdMapping identifierMapping)
		{
			return new EmailPrototypeSchemaState(identifierMapping);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000634E2 File Offset: 0x000616E2
		public AirSyncSchemaState CreateContactsSchema()
		{
			return new ContactsPrototypeSchemaState();
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x000634E9 File Offset: 0x000616E9
		public AirSyncSchemaState CreateTasksSchema()
		{
			return new TasksPrototypeSchemaState();
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x000634F0 File Offset: 0x000616F0
		public AirSyncSchemaState CreateNotesSchema()
		{
			return null;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x000634F3 File Offset: 0x000616F3
		public AirSyncSchemaState CreateSmsSchema()
		{
			return null;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000634F6 File Offset: 0x000616F6
		public AirSyncSchemaState CreateConsumerSmsAndMmsSchema()
		{
			return null;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x000634F9 File Offset: 0x000616F9
		public AirSyncSchemaState CreateRecipientInfoCacheSchema()
		{
			return null;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000634FC File Offset: 0x000616FC
		public IAirSyncMissingPropertyStrategy CreateMissingPropertyStrategy(Dictionary<string, bool> supportedTags)
		{
			return new AirSyncSetToDefaultStrategy(supportedTags);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00063504 File Offset: 0x00061704
		public IAirSyncMissingPropertyStrategy CreateReadFlagMissingPropertyStrategy()
		{
			return new AirSyncSetToUnmodifiedStrategy();
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0006350C File Offset: 0x0006170C
		public string GetClassFromMessageClass(string messageClass)
		{
			SinglePropertyBag propertyBag = new SinglePropertyBag(StoreObjectSchema.ItemClass, messageClass);
			foreach (KeyValuePair<string, QueryFilter> keyValuePair in AirSyncVersionFactoryV121.classToQueryFilterDictionary)
			{
				if (EvaluatableFilter.Evaluate(keyValuePair.Value, propertyBag))
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x04000B5E RID: 2910
		private static Dictionary<string, QueryFilter> classToQueryFilterDictionary = new Dictionary<string, QueryFilter>(4);
	}
}
