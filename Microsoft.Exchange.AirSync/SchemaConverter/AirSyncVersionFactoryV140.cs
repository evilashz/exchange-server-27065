using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV140;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001B9 RID: 441
	internal class AirSyncVersionFactoryV140 : IAirSyncVersionFactory
	{
		// Token: 0x06001296 RID: 4758 RVA: 0x00063588 File Offset: 0x00061788
		static AirSyncVersionFactoryV140()
		{
			AirSyncVersionFactoryV140.classToQueryFilterDictionary.Add("Email", EmailPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV140.classToQueryFilterDictionary.Add("Calendar", CalendarPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV140.classToQueryFilterDictionary.Add("Contacts", ContactsPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV140.classToQueryFilterDictionary.Add("Tasks", TasksPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV140.classToQueryFilterDictionary.Add("Notes", NotesPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV140.classToQueryFilterDictionary.Add("SMS", ConsumerSmsAndMmsPrototypeSchemaState.SupportedClassQueryFilter);
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x00063618 File Offset: 0x00061818
		public string VersionString
		{
			get
			{
				return "14.0";
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0006361F File Offset: 0x0006181F
		public AirSyncSchemaState CreateCalendarSchema()
		{
			return new CalendarPrototypeSchemaState();
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00063626 File Offset: 0x00061826
		public AirSyncSchemaState CreateEmailSchema(IdMapping identifierMapping)
		{
			return new EmailPrototypeSchemaState(identifierMapping);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0006362E File Offset: 0x0006182E
		public AirSyncSchemaState CreateContactsSchema()
		{
			return new ContactsPrototypeSchemaState();
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00063635 File Offset: 0x00061835
		public AirSyncSchemaState CreateNotesSchema()
		{
			return new NotesPrototypeSchemaState();
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0006363C File Offset: 0x0006183C
		public AirSyncSchemaState CreateSmsSchema()
		{
			return new SmsPrototypeSchemaState();
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00063643 File Offset: 0x00061843
		public AirSyncSchemaState CreateConsumerSmsAndMmsSchema()
		{
			return new ConsumerSmsAndMmsPrototypeSchemaState();
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0006364A File Offset: 0x0006184A
		public AirSyncSchemaState CreateTasksSchema()
		{
			return new TasksPrototypeSchemaState();
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00063651 File Offset: 0x00061851
		public AirSyncSchemaState CreateRecipientInfoCacheSchema()
		{
			return new RecipientInfoCacheSchemaState();
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00063658 File Offset: 0x00061858
		public IAirSyncMissingPropertyStrategy CreateMissingPropertyStrategy(Dictionary<string, bool> supportedTags)
		{
			return new AirSyncSetToDefaultStrategy(supportedTags);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00063660 File Offset: 0x00061860
		public IAirSyncMissingPropertyStrategy CreateReadFlagMissingPropertyStrategy()
		{
			return new AirSyncSetToUnmodifiedStrategy();
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00063668 File Offset: 0x00061868
		public string GetClassFromMessageClass(string messageClass)
		{
			SinglePropertyBag propertyBag = new SinglePropertyBag(StoreObjectSchema.ItemClass, messageClass);
			foreach (KeyValuePair<string, QueryFilter> keyValuePair in AirSyncVersionFactoryV140.classToQueryFilterDictionary)
			{
				if (EvaluatableFilter.Evaluate(keyValuePair.Value, propertyBag))
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x04000B5F RID: 2911
		private static Dictionary<string, QueryFilter> classToQueryFilterDictionary = new Dictionary<string, QueryFilter>(6);
	}
}
