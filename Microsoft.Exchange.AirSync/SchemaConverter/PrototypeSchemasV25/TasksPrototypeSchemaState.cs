using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV25
{
	// Token: 0x020001CF RID: 463
	internal class TasksPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x06001302 RID: 4866 RVA: 0x000697E7 File Offset: 0x000679E7
		public TasksPrototypeSchemaState() : base(TasksPrototypeSchemaState.supportedClassFilter)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x00069801 File Offset: 0x00067A01
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return TasksPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00069808 File Offset: 0x00067A08
		private void CreatePropertyConversionTable()
		{
			bool nodelete = true;
			string xmlNodeNamespace = "Tasks:";
			base.AddProperty(new IProperty[]
			{
				new AirSyncBodyProperty(xmlNodeNamespace, "Body", "Rtf", "BodyTruncated", "BodySize", false, false, true),
				new XsoBodyProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Subject", true),
				new XsoStringProperty(ItemSchema.Subject)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace, "Importance", true),
				new XsoIntegerProperty(ItemSchema.Importance, nodelete)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncMultiValuedStringProperty(xmlNodeNamespace, "Categories", "Category", true),
				new XsoCategoriesProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStartDueDateProperty(xmlNodeNamespace),
				new XsoStartDueDateProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncRecurrenceProperty(xmlNodeNamespace, "Recurrence", TypeOfRecurrence.Task, true, 25),
				new XsoRecurrenceProperty(TypeOfRecurrence.Task, 25)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncTaskStateProperty(xmlNodeNamespace, "Complete", "DateCompleted", true),
				new XsoTaskStateProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace, "Sensitivity", true),
				new XsoSensitivityProperty(ItemSchema.Sensitivity)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "ReminderTime", AirSyncDateFormat.Punctuate, true),
				new XsoUtcDateTimeProperty(ItemSchema.ReminderDueBy)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncBooleanProperty(xmlNodeNamespace, "ReminderSet", true),
				new XsoBooleanProperty(ItemSchema.ReminderIsSet, nodelete)
			});
		}

		// Token: 0x04000B9C RID: 2972
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.TASK"
		};

		// Token: 0x04000B9D RID: 2973
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(TasksPrototypeSchemaState.supportedClassTypes);
	}
}
