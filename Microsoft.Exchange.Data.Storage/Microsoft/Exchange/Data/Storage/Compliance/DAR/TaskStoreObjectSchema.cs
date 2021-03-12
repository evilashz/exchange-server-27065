using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Compliance.DAR
{
	// Token: 0x02000459 RID: 1113
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TaskStoreObjectSchema : EwsStoreObjectSchema
	{
		// Token: 0x0600318F RID: 12687 RVA: 0x000CA758 File Offset: 0x000C8958
		public static GuidNamePropertyDefinition CreateStorePropertyDefinition(EwsStoreObjectPropertyDefinition ewsStorePropertyDefinition)
		{
			return GuidNamePropertyDefinition.CreateCustom(ewsStorePropertyDefinition.Name, ewsStorePropertyDefinition.IsMultivalued ? ewsStorePropertyDefinition.Type.MakeArrayType() : ewsStorePropertyDefinition.Type, ((ExtendedPropertyDefinition)ewsStorePropertyDefinition.StorePropertyDefinition).PropertySetId.Value, ewsStorePropertyDefinition.Name, PropertyFlags.None);
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000CA7AA File Offset: 0x000C89AA
		public static EwsStoreObjectPropertyDefinition CreateEwsPropertyDefinition(ExtendedPropertyDefinition definition, object defaultValue = null)
		{
			if (defaultValue == null && definition.Type.IsValueType)
			{
				defaultValue = Activator.CreateInstance(definition.Type);
			}
			return new EwsStoreObjectPropertyDefinition(definition.Name, ExchangeObjectVersion.Exchange2012, definition.Type, PropertyDefinitionFlags.ReturnOnBind, defaultValue, defaultValue, definition);
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000CA7E7 File Offset: 0x000C89E7
		public static EwsStoreObjectPropertyDefinition CreateEwsDefinitionFromServiceDefinition(ServiceObjectPropertyDefinition definition, string name, object defaultValue = null)
		{
			return new EwsStoreObjectPropertyDefinition(name, ExchangeObjectVersion.Exchange2012, definition.Type, PropertyDefinitionFlags.ReadOnly, defaultValue, defaultValue, definition);
		}

		// Token: 0x04001AB4 RID: 6836
		internal const int CurrentSchemaVersion = 0;

		// Token: 0x04001AB5 RID: 6837
		public static readonly EwsStoreObjectPropertyDefinition Id = EwsStoreObjectSchema.AlternativeId;

		// Token: 0x04001AB6 RID: 6838
		public static readonly EwsStoreObjectPropertyDefinition LastModifiedTime = TaskStoreObjectSchema.CreateEwsDefinitionFromServiceDefinition(ItemSchema.LastModifiedTime, "LastModifiedTime", null);

		// Token: 0x04001AB7 RID: 6839
		public static readonly EwsStoreObjectPropertyDefinition Category = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.Category, null);

		// Token: 0x04001AB8 RID: 6840
		public static readonly EwsStoreObjectPropertyDefinition Priority = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.Priority, null);

		// Token: 0x04001AB9 RID: 6841
		public static readonly EwsStoreObjectPropertyDefinition TaskState = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.TaskState, null);

		// Token: 0x04001ABA RID: 6842
		public static readonly EwsStoreObjectPropertyDefinition TaskType = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.TaskType, null);

		// Token: 0x04001ABB RID: 6843
		public static readonly EwsStoreObjectPropertyDefinition SerializedTaskData = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.SerializedTaskData, null);

		// Token: 0x04001ABC RID: 6844
		public static readonly EwsStoreObjectPropertyDefinition TenantId = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.TenantId, null);

		// Token: 0x04001ABD RID: 6845
		public static readonly EwsStoreObjectPropertyDefinition MinTaskScheduleTime = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.MinTaskScheduleTime, null);

		// Token: 0x04001ABE RID: 6846
		public static readonly EwsStoreObjectPropertyDefinition TaskCompletionTime = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.TaskCompletionTime, null);

		// Token: 0x04001ABF RID: 6847
		public static readonly EwsStoreObjectPropertyDefinition TaskExecutionStartTime = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.TaskExecutionStartTime, null);

		// Token: 0x04001AC0 RID: 6848
		public static readonly EwsStoreObjectPropertyDefinition TaskQueuedTime = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.TaskQueuedTime, null);

		// Token: 0x04001AC1 RID: 6849
		public static readonly EwsStoreObjectPropertyDefinition TaskScheduledTime = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.TaskScheduledTime, null);

		// Token: 0x04001AC2 RID: 6850
		public static readonly EwsStoreObjectPropertyDefinition TaskRetryInterval = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.TaskRetryInterval, null);

		// Token: 0x04001AC3 RID: 6851
		public static readonly EwsStoreObjectPropertyDefinition TaskRetryCurrentCount = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.TaskRetryCurrentCount, null);

		// Token: 0x04001AC4 RID: 6852
		public static readonly EwsStoreObjectPropertyDefinition TaskRetryTotalCount = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.TaskRetryTotalCount, null);

		// Token: 0x04001AC5 RID: 6853
		public static readonly EwsStoreObjectPropertyDefinition TaskSynchronizationOption = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.TaskSynchronizationOption, null);

		// Token: 0x04001AC6 RID: 6854
		public static readonly EwsStoreObjectPropertyDefinition TaskSynchronizationKey = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.TaskSynchronizationKey, null);

		// Token: 0x04001AC7 RID: 6855
		public static readonly EwsStoreObjectPropertyDefinition ExecutionContainer = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.ExecutionContainer, null);

		// Token: 0x04001AC8 RID: 6856
		public static readonly EwsStoreObjectPropertyDefinition ExecutionTarget = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.ExecutionTarget, null);

		// Token: 0x04001AC9 RID: 6857
		public static readonly EwsStoreObjectPropertyDefinition ExecutionLockExpiryTime = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.ExecutionLockExpiryTime, null);

		// Token: 0x04001ACA RID: 6858
		public static readonly EwsStoreObjectPropertyDefinition SchemaVersion = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.SchemaVersion, 0);
	}
}
