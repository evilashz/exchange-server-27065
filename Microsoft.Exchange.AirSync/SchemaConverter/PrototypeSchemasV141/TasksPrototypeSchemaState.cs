using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV141
{
	// Token: 0x020001E0 RID: 480
	internal class TasksPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x0600133E RID: 4926 RVA: 0x0006DDBD File Offset: 0x0006BFBD
		public TasksPrototypeSchemaState() : base(TasksPrototypeSchemaState.supportedClassFilter)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0006DDD7 File Offset: 0x0006BFD7
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return TasksPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0006DDE0 File Offset: 0x0006BFE0
		private void CreatePropertyConversionTable()
		{
			bool nodelete = true;
			string xmlNodeNamespace = "Tasks:";
			string xmlNodeNamespace2 = "AirSyncBase:";
			base.AddProperty(new IProperty[]
			{
				new AirSyncContent14Property(xmlNodeNamespace2, "Body", false),
				new XsoContent14Property()
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
				new AirSyncRecurrenceProperty(xmlNodeNamespace, "Recurrence", TypeOfRecurrence.Task, true, 141),
				new XsoRecurrenceProperty(TypeOfRecurrence.Task, 141)
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

		// Token: 0x04000BCA RID: 3018
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.TASK"
		};

		// Token: 0x04000BCB RID: 3019
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(TasksPrototypeSchemaState.supportedClassTypes);
	}
}
