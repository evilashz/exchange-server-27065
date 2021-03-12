using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV20;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001B5 RID: 437
	internal class AirSyncVersionFactoryV21 : IAirSyncVersionFactory
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x00063110 File Offset: 0x00061310
		static AirSyncVersionFactoryV21()
		{
			AirSyncVersionFactoryV21.classToQueryFilterDictionary.Add("Email", EmailPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV21.classToQueryFilterDictionary.Add("Calendar", CalendarPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV21.classToQueryFilterDictionary.Add("Contacts", ContactsPrototypeSchemaState.SupportedClassQueryFilter);
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00063164 File Offset: 0x00061364
		public string VersionString
		{
			get
			{
				return "2.1";
			}
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0006316B File Offset: 0x0006136B
		public AirSyncSchemaState CreateCalendarSchema()
		{
			return new CalendarPrototypeSchemaState();
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00063172 File Offset: 0x00061372
		public AirSyncSchemaState CreateEmailSchema(IdMapping identifierMapping)
		{
			return new EmailPrototypeSchemaState(identifierMapping);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0006317A File Offset: 0x0006137A
		public AirSyncSchemaState CreateContactsSchema()
		{
			return new ContactsPrototypeSchemaState();
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00063181 File Offset: 0x00061381
		public AirSyncSchemaState CreateTasksSchema()
		{
			return null;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00063184 File Offset: 0x00061384
		public AirSyncSchemaState CreateNotesSchema()
		{
			return null;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00063187 File Offset: 0x00061387
		public AirSyncSchemaState CreateSmsSchema()
		{
			return null;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0006318A File Offset: 0x0006138A
		public AirSyncSchemaState CreateConsumerSmsAndMmsSchema()
		{
			return null;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0006318D File Offset: 0x0006138D
		public AirSyncSchemaState CreateRecipientInfoCacheSchema()
		{
			return null;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00063190 File Offset: 0x00061390
		public IAirSyncMissingPropertyStrategy CreateMissingPropertyStrategy(Dictionary<string, bool> supportedTags)
		{
			return new AirSyncSetToDefaultStrategy(supportedTags);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00063198 File Offset: 0x00061398
		public IAirSyncMissingPropertyStrategy CreateReadFlagMissingPropertyStrategy()
		{
			return new AirSyncSetToUnmodifiedStrategy();
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000631A0 File Offset: 0x000613A0
		public string GetClassFromMessageClass(string messageClass)
		{
			SinglePropertyBag propertyBag = new SinglePropertyBag(StoreObjectSchema.ItemClass, messageClass);
			foreach (KeyValuePair<string, QueryFilter> keyValuePair in AirSyncVersionFactoryV21.classToQueryFilterDictionary)
			{
				if (EvaluatableFilter.Evaluate(keyValuePair.Value, propertyBag))
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x04000B5B RID: 2907
		private static Dictionary<string, QueryFilter> classToQueryFilterDictionary = new Dictionary<string, QueryFilter>(3);
	}
}
