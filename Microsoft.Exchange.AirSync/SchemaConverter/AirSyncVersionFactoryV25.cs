using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV25;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001B6 RID: 438
	internal class AirSyncVersionFactoryV25 : IAirSyncVersionFactory
	{
		// Token: 0x0600126C RID: 4716 RVA: 0x0006321C File Offset: 0x0006141C
		static AirSyncVersionFactoryV25()
		{
			AirSyncVersionFactoryV25.classToQueryFilterDictionary.Add("Email", EmailPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV25.classToQueryFilterDictionary.Add("Calendar", CalendarPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV25.classToQueryFilterDictionary.Add("Contacts", ContactsPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV25.classToQueryFilterDictionary.Add("Tasks", TasksPrototypeSchemaState.SupportedClassQueryFilter);
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00063284 File Offset: 0x00061484
		public string VersionString
		{
			get
			{
				return "2.5";
			}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0006328B File Offset: 0x0006148B
		public AirSyncSchemaState CreateCalendarSchema()
		{
			return new CalendarPrototypeSchemaState();
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00063292 File Offset: 0x00061492
		public AirSyncSchemaState CreateEmailSchema(IdMapping identifierMapping)
		{
			return new EmailPrototypeSchemaState(identifierMapping);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0006329A File Offset: 0x0006149A
		public AirSyncSchemaState CreateContactsSchema()
		{
			return new ContactsPrototypeSchemaState();
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x000632A1 File Offset: 0x000614A1
		public AirSyncSchemaState CreateTasksSchema()
		{
			return new TasksPrototypeSchemaState();
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x000632A8 File Offset: 0x000614A8
		public AirSyncSchemaState CreateNotesSchema()
		{
			return null;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x000632AB File Offset: 0x000614AB
		public AirSyncSchemaState CreateSmsSchema()
		{
			return null;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000632AE File Offset: 0x000614AE
		public AirSyncSchemaState CreateConsumerSmsAndMmsSchema()
		{
			return null;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000632B1 File Offset: 0x000614B1
		public AirSyncSchemaState CreateRecipientInfoCacheSchema()
		{
			return null;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x000632B4 File Offset: 0x000614B4
		public IAirSyncMissingPropertyStrategy CreateMissingPropertyStrategy(Dictionary<string, bool> supportedTags)
		{
			return new AirSyncSetToDefaultStrategy(supportedTags);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x000632BC File Offset: 0x000614BC
		public IAirSyncMissingPropertyStrategy CreateReadFlagMissingPropertyStrategy()
		{
			return new AirSyncSetToUnmodifiedStrategy();
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000632C4 File Offset: 0x000614C4
		public string GetClassFromMessageClass(string messageClass)
		{
			SinglePropertyBag propertyBag = new SinglePropertyBag(StoreObjectSchema.ItemClass, messageClass);
			foreach (KeyValuePair<string, QueryFilter> keyValuePair in AirSyncVersionFactoryV25.classToQueryFilterDictionary)
			{
				if (EvaluatableFilter.Evaluate(keyValuePair.Value, propertyBag))
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x04000B5C RID: 2908
		private static Dictionary<string, QueryFilter> classToQueryFilterDictionary = new Dictionary<string, QueryFilter>(4);
	}
}
