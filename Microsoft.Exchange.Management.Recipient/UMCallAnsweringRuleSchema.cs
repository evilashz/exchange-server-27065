using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000FA RID: 250
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UMCallAnsweringRuleSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000382 RID: 898
		public const string PriorityParameterName = "Priority";

		// Token: 0x04000383 RID: 899
		public const string EnabledParameterName = "Enabled";

		// Token: 0x04000384 RID: 900
		public const string NameParameterName = "Name";

		// Token: 0x04000385 RID: 901
		public const string MailboxParameterName = "Mailbox";

		// Token: 0x04000386 RID: 902
		public const string ScheduleStatusParameterName = "ScheduleStatus";

		// Token: 0x04000387 RID: 903
		public const string TimeOfDayParameterName = "TimeOfDay";

		// Token: 0x04000388 RID: 904
		public const string CheckAutomaticRepliesParameterName = "CheckAutomaticReplies";

		// Token: 0x04000389 RID: 905
		public const string ExtensionsDialedParameterName = "ExtensionsDialed";

		// Token: 0x0400038A RID: 906
		public const string KeyMappingsParameterName = "KeyMappings";

		// Token: 0x0400038B RID: 907
		public const string CallersCanInterruptGreetingParameterName = "CallersCanInterruptGreeting";

		// Token: 0x0400038C RID: 908
		public const string CallerIdsParameterName = "CallerIds";

		// Token: 0x0400038D RID: 909
		private const int MaxScheduleStatus = 15;

		// Token: 0x0400038E RID: 910
		private const int MaximumPriority = 9;

		// Token: 0x0400038F RID: 911
		private const int MaxRuleNameLength = 256;

		// Token: 0x04000390 RID: 912
		public const int MaxKeyMappings = 10;

		// Token: 0x04000391 RID: 913
		public static readonly SimpleProviderPropertyDefinition Priority = new SimpleProviderPropertyDefinition("Priority", ExchangeObjectVersion.Exchange2012, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 9, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 9)
		});

		// Token: 0x04000392 RID: 914
		public static readonly SimpleProviderPropertyDefinition Enabled = new SimpleProviderPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000393 RID: 915
		public static readonly SimpleProviderPropertyDefinition Name = new SimpleProviderPropertyDefinition("Name", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 256)
		});

		// Token: 0x04000394 RID: 916
		public static readonly SimpleProviderPropertyDefinition CallerIds = new SimpleProviderPropertyDefinition("CallerIds", ExchangeObjectVersion.Exchange2012, typeof(CallerIdItem), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000395 RID: 917
		public static readonly SimpleProviderPropertyDefinition CallersCanInterruptGreeting = new SimpleProviderPropertyDefinition("CallersCanInterruptGreeting", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000396 RID: 918
		public static readonly SimpleProviderPropertyDefinition CheckAutomaticReplies = new SimpleProviderPropertyDefinition("CheckAutomaticReplies", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000397 RID: 919
		public static readonly SimpleProviderPropertyDefinition ExtensionsDialed = new SimpleProviderPropertyDefinition("ExtensionsDialed", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000398 RID: 920
		public static readonly SimpleProviderPropertyDefinition KeyMappings = new SimpleProviderPropertyDefinition("KeyMappings", ExchangeObjectVersion.Exchange2012, typeof(KeyMapping), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000399 RID: 921
		public static readonly SimpleProviderPropertyDefinition ScheduleStatus = new SimpleProviderPropertyDefinition("ScheduleStatus", ExchangeObjectVersion.Exchange2012, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 15)
		});

		// Token: 0x0400039A RID: 922
		public static readonly SimpleProviderPropertyDefinition TimeOfDay = new SimpleProviderPropertyDefinition("TimeOfDay", ExchangeObjectVersion.Exchange2012, typeof(TimeOfDay), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
