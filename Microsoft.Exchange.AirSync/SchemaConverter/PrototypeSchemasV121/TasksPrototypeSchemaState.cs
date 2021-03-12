using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV121
{
	// Token: 0x020001C6 RID: 454
	internal class TasksPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x060012E2 RID: 4834 RVA: 0x00066AE7 File Offset: 0x00064CE7
		public TasksPrototypeSchemaState() : base(TasksPrototypeSchemaState.supportedClassFilter)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00066B01 File Offset: 0x00064D01
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return TasksPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00066B08 File Offset: 0x00064D08
		private void CreatePropertyConversionTable()
		{
			bool nodelete = true;
			string xmlNodeNamespace = "Tasks:";
			string xmlNodeNamespace2 = "AirSyncBase:";
			base.AddProperty(new IProperty[]
			{
				new AirSyncContentProperty(xmlNodeNamespace2, "Body", false),
				new XsoContentProperty()
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
				new AirSyncRecurrenceProperty(xmlNodeNamespace, "Recurrence", TypeOfRecurrence.Task, true, 121),
				new XsoRecurrenceProperty(TypeOfRecurrence.Task, 121)
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

		// Token: 0x04000B84 RID: 2948
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.TASK"
		};

		// Token: 0x04000B85 RID: 2949
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(TasksPrototypeSchemaState.supportedClassTypes);
	}
}
