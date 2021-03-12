using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV120
{
	// Token: 0x020001C1 RID: 449
	internal class TasksPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x060012D0 RID: 4816 RVA: 0x0006517F File Offset: 0x0006337F
		public TasksPrototypeSchemaState() : base(TasksPrototypeSchemaState.supportedClassFilter)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00065199 File Offset: 0x00063399
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return TasksPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x000651A0 File Offset: 0x000633A0
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
				new AirSyncRecurrenceProperty(xmlNodeNamespace, "Recurrence", TypeOfRecurrence.Task, true, 120),
				new XsoRecurrenceProperty(TypeOfRecurrence.Task, 120)
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

		// Token: 0x04000B72 RID: 2930
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.TASK"
		};

		// Token: 0x04000B73 RID: 2931
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(TasksPrototypeSchemaState.supportedClassTypes);
	}
}
