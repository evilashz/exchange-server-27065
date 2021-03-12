using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x02000361 RID: 865
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ActionBasedCalendarItemStateDefinition : SinglePropertyValueBasedCalendarItemStateDefinition<COWTriggerAction?>
	{
		// Token: 0x06002689 RID: 9865 RVA: 0x0009A824 File Offset: 0x00098A24
		static ActionBasedCalendarItemStateDefinition()
		{
			ActionBasedCalendarItemStateDefinition.deleteActionSet = new HashSet<COWTriggerAction?>();
			ActionBasedCalendarItemStateDefinition.deleteActionSet.Add(new COWTriggerAction?(COWTriggerAction.MoveToDeletedItems));
			ActionBasedCalendarItemStateDefinition.deleteActionSet.Add(new COWTriggerAction?(COWTriggerAction.SoftDelete));
			ActionBasedCalendarItemStateDefinition.deleteActionSet.Add(new COWTriggerAction?(COWTriggerAction.HardDelete));
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x0009A88B File Offset: 0x00098A8B
		public ActionBasedCalendarItemStateDefinition(HashSet<COWTriggerAction?> actionSet) : base(CalendarItemBaseSchema.CalendarLogTriggerAction, actionSet)
		{
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x0009A899 File Offset: 0x00098A99
		public override string SchemaKey
		{
			get
			{
				return "{90B237BC-23D4-4dce-BB8A-B34CF58ECA56}";
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x0009A8A0 File Offset: 0x00098AA0
		public override StorePropertyDefinition[] RequiredProperties
		{
			get
			{
				return ActionBasedCalendarItemStateDefinition.requiredProperties;
			}
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x0009A8A8 File Offset: 0x00098AA8
		protected override COWTriggerAction? GetValueFromPropertyBag(PropertyBag propertyBag, MailboxSession session)
		{
			string underlyingValue = base.GetUnderlyingValue<string>(propertyBag);
			COWTriggerAction? result;
			if (string.IsNullOrEmpty(underlyingValue))
			{
				result = null;
			}
			else
			{
				try
				{
					result = new COWTriggerAction?((COWTriggerAction)Enum.Parse(typeof(COWTriggerAction), underlyingValue));
				}
				catch (ArgumentException)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x0009A908 File Offset: 0x00098B08
		public static ActionBasedCalendarItemStateDefinition CreateDeletedNoneOccurrenceCalendarItemStateDefinition()
		{
			return new ActionBasedCalendarItemStateDefinition(ActionBasedCalendarItemStateDefinition.deleteActionSet);
		}

		// Token: 0x040016FD RID: 5885
		private static readonly HashSet<COWTriggerAction?> deleteActionSet;

		// Token: 0x040016FE RID: 5886
		private static readonly StorePropertyDefinition[] requiredProperties = new StorePropertyDefinition[]
		{
			CalendarItemBaseSchema.ClientIntent,
			CalendarItemBaseSchema.CalendarLogTriggerAction
		};
	}
}
