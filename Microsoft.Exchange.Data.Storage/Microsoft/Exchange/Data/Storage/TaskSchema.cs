using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CE0 RID: 3296
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TaskSchema : ItemSchema
	{
		// Token: 0x17001E6F RID: 7791
		// (get) Token: 0x06007207 RID: 29191 RVA: 0x001F8DEB File Offset: 0x001F6FEB
		public new static TaskSchema Instance
		{
			get
			{
				if (TaskSchema.instance == null)
				{
					TaskSchema.instance = new TaskSchema();
				}
				return TaskSchema.instance;
			}
		}

		// Token: 0x06007208 RID: 29192 RVA: 0x001F8E04 File Offset: 0x001F7004
		protected override void AddConstraints(List<StoreObjectConstraint> constraints)
		{
			base.AddConstraints(constraints);
			constraints.Add(new CustomConstraint("Supported Recurrences for Tasks constraint", new PropertyDefinition[]
			{
				InternalSchema.TaskRecurrence,
				InternalSchema.IsTaskRecurring,
				InternalSchema.IsOneOff,
				InternalSchema.IconIndex
			}, new IsObjectValidDelegate(Task.IsTaskRecurrenceSupported), true));
		}

		// Token: 0x06007209 RID: 29193 RVA: 0x001F8E5D File Offset: 0x001F705D
		internal override void CoreObjectUpdate(CoreItem coreItem, CoreItemOperation operation)
		{
			base.CoreObjectUpdate(coreItem, operation);
			Task.CoreObjectUpdateTaskStatus(coreItem);
			Task.CoreObjectUpdateRecurrence(coreItem);
			Task.CoreObjectUpdateTaskDates(coreItem);
		}

		// Token: 0x17001E70 RID: 7792
		// (get) Token: 0x0600720A RID: 29194 RVA: 0x001F8E79 File Offset: 0x001F7079
		protected override ICollection<PropertyRule> PropertyRules
		{
			get
			{
				if (this.propertyRulesCache == null)
				{
					this.propertyRulesCache = base.PropertyRules.Concat(TaskSchema.taskPropertyRules);
				}
				return this.propertyRulesCache;
			}
		}

		// Token: 0x04004F1E RID: 20254
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition TaskOwner = InternalSchema.TaskOwner;

		// Token: 0x04004F1F RID: 20255
		[Autoload]
		internal static readonly StorePropertyDefinition TaskRecurrencePattern = InternalSchema.TaskRecurrence;

		// Token: 0x04004F20 RID: 20256
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition TaskChangeCount = InternalSchema.TaskChangeCount;

		// Token: 0x04004F21 RID: 20257
		[LegalTracking]
		public static readonly StorePropertyDefinition StatusDescription = InternalSchema.StatusDescription;

		// Token: 0x04004F22 RID: 20258
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition IsTaskRecurring = InternalSchema.IsTaskRecurring;

		// Token: 0x04004F23 RID: 20259
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition TotalWork = InternalSchema.TotalWork;

		// Token: 0x04004F24 RID: 20260
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition ActualWork = InternalSchema.ActualWork;

		// Token: 0x04004F25 RID: 20261
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition Mileage = InternalSchema.Mileage;

		// Token: 0x04004F26 RID: 20262
		[LegalTracking]
		public static readonly StorePropertyDefinition Contacts = InternalSchema.Contacts;

		// Token: 0x04004F27 RID: 20263
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition RecurrenceType = InternalSchema.CalculatedRecurrenceType;

		// Token: 0x04004F28 RID: 20264
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition IsRecurring = InternalSchema.IsRecurring;

		// Token: 0x04004F29 RID: 20265
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition TaskResetReminder = InternalSchema.TaskResetReminder;

		// Token: 0x04004F2A RID: 20266
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition IsOneOff = InternalSchema.IsOneOff;

		// Token: 0x04004F2B RID: 20267
		[LegalTracking]
		public static readonly StorePropertyDefinition LastUser = InternalSchema.LastModifiedBy;

		// Token: 0x04004F2C RID: 20268
		[LegalTracking]
		public static readonly StorePropertyDefinition TaskDelegator = InternalSchema.TaskDelegator;

		// Token: 0x04004F2D RID: 20269
		[LegalTracking]
		public static readonly StorePropertyDefinition AssignedTime = InternalSchema.AssignedTime;

		// Token: 0x04004F2E RID: 20270
		[LegalTracking]
		public static readonly StorePropertyDefinition OwnershipState = InternalSchema.OwnershipState;

		// Token: 0x04004F2F RID: 20271
		[LegalTracking]
		public static readonly StorePropertyDefinition DelegationState = InternalSchema.DelegationState;

		// Token: 0x04004F30 RID: 20272
		[LegalTracking]
		public static readonly StorePropertyDefinition IsAssignmentEditable = InternalSchema.IsAssignmentEditable;

		// Token: 0x04004F31 RID: 20273
		[LegalTracking]
		public static readonly StorePropertyDefinition TaskType = InternalSchema.TaskType;

		// Token: 0x04004F32 RID: 20274
		[LegalTracking]
		public static readonly StorePropertyDefinition IsTeamTask = InternalSchema.IsTeamTask;

		// Token: 0x04004F33 RID: 20275
		[LegalTracking]
		public static readonly StorePropertyDefinition LastUpdateType = InternalSchema.LastUpdateType;

		// Token: 0x04004F34 RID: 20276
		[LegalTracking]
		[ConditionallyRequired(CustomConstraintDelegateEnum.IsStartDateDefined)]
		[Autoload]
		public static StorePropertyDefinition DueDate = InternalSchema.DueDate;

		// Token: 0x04004F35 RID: 20277
		[LegalTracking]
		[Autoload]
		public static StorePropertyDefinition StartDate = InternalSchema.StartDate;

		// Token: 0x04004F36 RID: 20278
		[Autoload]
		public static StorePropertyDefinition DoItTime = InternalSchema.DoItTime;

		// Token: 0x04004F37 RID: 20279
		[LegalTracking]
		public static readonly StorePropertyDefinition TaskAccepted = InternalSchema.TaskAccepted;

		// Token: 0x04004F38 RID: 20280
		[LegalTracking]
		public static readonly StorePropertyDefinition BillingInformation = InternalSchema.BillingInformation;

		// Token: 0x04004F39 RID: 20281
		[LegalTracking]
		public static readonly StorePropertyDefinition Companies = InternalSchema.Companies;

		// Token: 0x04004F3A RID: 20282
		public static readonly StorePropertyDefinition QuickCaptureReminders = InternalSchema.ModernReminders;

		// Token: 0x04004F3B RID: 20283
		public static readonly StorePropertyDefinition ModernReminders = InternalSchema.ModernReminders;

		// Token: 0x04004F3C RID: 20284
		public static readonly StorePropertyDefinition ModernRemindersState = InternalSchema.ModernRemindersState;

		// Token: 0x04004F3D RID: 20285
		public static readonly StorePropertyDefinition EventTimeBasedInboxReminders = InternalSchema.EventTimeBasedInboxReminders;

		// Token: 0x04004F3E RID: 20286
		public static readonly StorePropertyDefinition EventTimeBasedInboxRemindersState = InternalSchema.EventTimeBasedInboxRemindersState;

		// Token: 0x04004F3F RID: 20287
		private static TaskSchema instance = null;

		// Token: 0x04004F40 RID: 20288
		private ICollection<PropertyRule> propertyRulesCache;

		// Token: 0x04004F41 RID: 20289
		private static PropertyRule[] taskPropertyRules = new PropertyRule[]
		{
			PropertyRuleLibrary.NoEmptyTaskRecurrenceBlob,
			PropertyRuleLibrary.DoItTimeProperty
		};
	}
}
