using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV160;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001BB RID: 443
	internal class AirSyncVersionFactoryV160 : IAirSyncVersionFactory
	{
		// Token: 0x060012B2 RID: 4786 RVA: 0x0006383C File Offset: 0x00061A3C
		static AirSyncVersionFactoryV160()
		{
			AirSyncVersionFactoryV160.classToQueryFilterDictionary.Add("Email", EmailPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV160.classToQueryFilterDictionary.Add("Calendar", CalendarPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV160.classToQueryFilterDictionary.Add("Contacts", ContactsPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV160.classToQueryFilterDictionary.Add("Tasks", TasksPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV160.classToQueryFilterDictionary.Add("Notes", NotesPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV160.classToQueryFilterDictionary.Add("SMS", SmsPrototypeSchemaState.SupportedClassQueryFilter);
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x000638CC File Offset: 0x00061ACC
		public string VersionString
		{
			get
			{
				return "16.0";
			}
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x000638D3 File Offset: 0x00061AD3
		public AirSyncSchemaState CreateCalendarSchema()
		{
			return new CalendarPrototypeSchemaState();
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x000638DA File Offset: 0x00061ADA
		public AirSyncSchemaState CreateEmailSchema(IdMapping identifierMapping)
		{
			return new EmailPrototypeSchemaState(identifierMapping);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x000638E2 File Offset: 0x00061AE2
		public AirSyncSchemaState CreateContactsSchema()
		{
			return new ContactsPrototypeSchemaState();
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x000638E9 File Offset: 0x00061AE9
		public AirSyncSchemaState CreateNotesSchema()
		{
			return new NotesPrototypeSchemaState();
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x000638F0 File Offset: 0x00061AF0
		public AirSyncSchemaState CreateSmsSchema()
		{
			return new SmsPrototypeSchemaState();
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x000638F7 File Offset: 0x00061AF7
		public AirSyncSchemaState CreateConsumerSmsAndMmsSchema()
		{
			return null;
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x000638FA File Offset: 0x00061AFA
		public AirSyncSchemaState CreateTasksSchema()
		{
			return new TasksPrototypeSchemaState();
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00063901 File Offset: 0x00061B01
		public AirSyncSchemaState CreateRecipientInfoCacheSchema()
		{
			return new RecipientInfoCacheSchemaState();
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00063908 File Offset: 0x00061B08
		public IAirSyncMissingPropertyStrategy CreateMissingPropertyStrategy(Dictionary<string, bool> supportedTags)
		{
			return new AirSyncSetToDefaultStrategy(supportedTags);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00063910 File Offset: 0x00061B10
		public IAirSyncMissingPropertyStrategy CreateReadFlagMissingPropertyStrategy()
		{
			return new AirSyncSetToUnmodifiedStrategy();
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00063918 File Offset: 0x00061B18
		public string GetClassFromMessageClass(string messageClass)
		{
			SinglePropertyBag propertyBag = new SinglePropertyBag(StoreObjectSchema.ItemClass, messageClass);
			foreach (KeyValuePair<string, QueryFilter> keyValuePair in AirSyncVersionFactoryV160.classToQueryFilterDictionary)
			{
				if (EvaluatableFilter.Evaluate(keyValuePair.Value, propertyBag))
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x04000B61 RID: 2913
		private static Dictionary<string, QueryFilter> classToQueryFilterDictionary = new Dictionary<string, QueryFilter>(6);
	}
}
