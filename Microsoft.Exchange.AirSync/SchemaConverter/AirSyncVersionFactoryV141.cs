using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV141;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001BA RID: 442
	internal class AirSyncVersionFactoryV141 : IAirSyncVersionFactory
	{
		// Token: 0x060012A4 RID: 4772 RVA: 0x000636E4 File Offset: 0x000618E4
		static AirSyncVersionFactoryV141()
		{
			AirSyncVersionFactoryV141.classToQueryFilterDictionary.Add("Email", EmailPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV141.classToQueryFilterDictionary.Add("Calendar", CalendarPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV141.classToQueryFilterDictionary.Add("Contacts", ContactsPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV141.classToQueryFilterDictionary.Add("Tasks", TasksPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV141.classToQueryFilterDictionary.Add("Notes", NotesPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV141.classToQueryFilterDictionary.Add("SMS", SmsPrototypeSchemaState.SupportedClassQueryFilter);
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x00063774 File Offset: 0x00061974
		public string VersionString
		{
			get
			{
				return "14.1";
			}
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0006377B File Offset: 0x0006197B
		public AirSyncSchemaState CreateCalendarSchema()
		{
			return new CalendarPrototypeSchemaState();
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00063782 File Offset: 0x00061982
		public AirSyncSchemaState CreateEmailSchema(IdMapping identifierMapping)
		{
			return new EmailPrototypeSchemaState(identifierMapping);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0006378A File Offset: 0x0006198A
		public AirSyncSchemaState CreateContactsSchema()
		{
			return new ContactsPrototypeSchemaState();
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00063791 File Offset: 0x00061991
		public AirSyncSchemaState CreateNotesSchema()
		{
			return new NotesPrototypeSchemaState();
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00063798 File Offset: 0x00061998
		public AirSyncSchemaState CreateSmsSchema()
		{
			return new SmsPrototypeSchemaState();
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0006379F File Offset: 0x0006199F
		public AirSyncSchemaState CreateConsumerSmsAndMmsSchema()
		{
			return null;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x000637A2 File Offset: 0x000619A2
		public AirSyncSchemaState CreateTasksSchema()
		{
			return new TasksPrototypeSchemaState();
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x000637A9 File Offset: 0x000619A9
		public AirSyncSchemaState CreateRecipientInfoCacheSchema()
		{
			return new RecipientInfoCacheSchemaState();
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000637B0 File Offset: 0x000619B0
		public IAirSyncMissingPropertyStrategy CreateMissingPropertyStrategy(Dictionary<string, bool> supportedTags)
		{
			return new AirSyncSetToDefaultStrategy(supportedTags);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x000637B8 File Offset: 0x000619B8
		public IAirSyncMissingPropertyStrategy CreateReadFlagMissingPropertyStrategy()
		{
			return new AirSyncSetToUnmodifiedStrategy();
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x000637C0 File Offset: 0x000619C0
		public string GetClassFromMessageClass(string messageClass)
		{
			SinglePropertyBag propertyBag = new SinglePropertyBag(StoreObjectSchema.ItemClass, messageClass);
			foreach (KeyValuePair<string, QueryFilter> keyValuePair in AirSyncVersionFactoryV141.classToQueryFilterDictionary)
			{
				if (EvaluatableFilter.Evaluate(keyValuePair.Value, propertyBag))
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x04000B60 RID: 2912
		private static Dictionary<string, QueryFilter> classToQueryFilterDictionary = new Dictionary<string, QueryFilter>(6);
	}
}
