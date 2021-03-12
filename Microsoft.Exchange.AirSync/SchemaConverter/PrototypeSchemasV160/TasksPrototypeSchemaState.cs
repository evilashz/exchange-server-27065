using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV160
{
	// Token: 0x020001E7 RID: 487
	internal class TasksPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x06001378 RID: 4984 RVA: 0x0007042D File Offset: 0x0006E62D
		public TasksPrototypeSchemaState() : base(TasksPrototypeSchemaState.supportedClassFilter)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x00070447 File Offset: 0x0006E647
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return TasksPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00070450 File Offset: 0x0006E650
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

		// Token: 0x04000C00 RID: 3072
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.TASK"
		};

		// Token: 0x04000C01 RID: 3073
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(TasksPrototypeSchemaState.supportedClassTypes);
	}
}
