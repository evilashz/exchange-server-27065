using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV120;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001B7 RID: 439
	internal class AirSyncVersionFactoryV120 : IAirSyncVersionFactory
	{
		// Token: 0x0600127A RID: 4730 RVA: 0x00063340 File Offset: 0x00061540
		static AirSyncVersionFactoryV120()
		{
			AirSyncVersionFactoryV120.classToQueryFilterDictionary.Add("Email", EmailPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV120.classToQueryFilterDictionary.Add("Calendar", CalendarPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV120.classToQueryFilterDictionary.Add("Contacts", ContactsPrototypeSchemaState.SupportedClassQueryFilter);
			AirSyncVersionFactoryV120.classToQueryFilterDictionary.Add("Tasks", TasksPrototypeSchemaState.SupportedClassQueryFilter);
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x000633A8 File Offset: 0x000615A8
		public string VersionString
		{
			get
			{
				return "12.0";
			}
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x000633AF File Offset: 0x000615AF
		public AirSyncSchemaState CreateCalendarSchema()
		{
			return new CalendarPrototypeSchemaState();
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x000633B6 File Offset: 0x000615B6
		public AirSyncSchemaState CreateEmailSchema(IdMapping identifierMapping)
		{
			return new EmailPrototypeSchemaState(identifierMapping);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x000633BE File Offset: 0x000615BE
		public AirSyncSchemaState CreateContactsSchema()
		{
			return new ContactsPrototypeSchemaState();
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x000633C5 File Offset: 0x000615C5
		public AirSyncSchemaState CreateTasksSchema()
		{
			return new TasksPrototypeSchemaState();
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x000633CC File Offset: 0x000615CC
		public AirSyncSchemaState CreateNotesSchema()
		{
			return null;
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x000633CF File Offset: 0x000615CF
		public AirSyncSchemaState CreateSmsSchema()
		{
			return null;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x000633D2 File Offset: 0x000615D2
		public AirSyncSchemaState CreateConsumerSmsAndMmsSchema()
		{
			return null;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x000633D5 File Offset: 0x000615D5
		public AirSyncSchemaState CreateRecipientInfoCacheSchema()
		{
			return null;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x000633D8 File Offset: 0x000615D8
		public IAirSyncMissingPropertyStrategy CreateMissingPropertyStrategy(Dictionary<string, bool> supportedTags)
		{
			return new AirSyncSetToDefaultStrategy(supportedTags);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x000633E0 File Offset: 0x000615E0
		public IAirSyncMissingPropertyStrategy CreateReadFlagMissingPropertyStrategy()
		{
			return new AirSyncSetToUnmodifiedStrategy();
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x000633E8 File Offset: 0x000615E8
		public string GetClassFromMessageClass(string messageClass)
		{
			SinglePropertyBag propertyBag = new SinglePropertyBag(StoreObjectSchema.ItemClass, messageClass);
			foreach (KeyValuePair<string, QueryFilter> keyValuePair in AirSyncVersionFactoryV120.classToQueryFilterDictionary)
			{
				if (EvaluatableFilter.Evaluate(keyValuePair.Value, propertyBag))
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x04000B5D RID: 2909
		private static Dictionary<string, QueryFilter> classToQueryFilterDictionary = new Dictionary<string, QueryFilter>(4);
	}
}
