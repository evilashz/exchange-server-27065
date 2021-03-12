using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BAC RID: 2988
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FlaggedForActionCondition : Condition
	{
		// Token: 0x06006AD8 RID: 27352 RVA: 0x001C7E01 File Offset: 0x001C6001
		public static int GetRequestedActionLocalizedStringEnumIndex(string actionString)
		{
			return FlaggedForActionCondition.GetRequestedActionLocalizedStringEnumIndex(actionString, null);
		}

		// Token: 0x06006AD9 RID: 27353 RVA: 0x001C7E0C File Offset: 0x001C600C
		public static int GetRequestedActionLocalizedStringEnumIndex(string actionString, CultureInfo culture)
		{
			Array values = Enum.GetValues(FlaggedForActionCondition.RequestedActionType);
			for (int i = 0; i < values.Length; i++)
			{
				string a = (culture != null) ? LocalizedDescriptionAttribute.FromEnum(FlaggedForActionCondition.RequestedActionType, values.GetValue(i), culture) : LocalizedDescriptionAttribute.FromEnum(FlaggedForActionCondition.RequestedActionType, values.GetValue(i));
				if (string.Equals(a, actionString, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x17001D19 RID: 7449
		// (get) Token: 0x06006ADA RID: 27354 RVA: 0x001C7E6B File Offset: 0x001C606B
		public string Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x06006ADB RID: 27355 RVA: 0x001C7E73 File Offset: 0x001C6073
		private FlaggedForActionCondition(Rule rule, string action) : base(ConditionType.FlaggedForActionCondition, rule)
		{
			this.action = action;
		}

		// Token: 0x06006ADC RID: 27356 RVA: 0x001C7E88 File Offset: 0x001C6088
		public static FlaggedForActionCondition Create(Rule rule, string action)
		{
			Condition.CheckParams(new object[]
			{
				rule
			});
			RequestedAction requestedAction;
			if (!RequestedAction.Any.ToString().Equals(action, StringComparison.OrdinalIgnoreCase) && EnumValidator.TryParse<RequestedAction>(action, EnumParseOptions.IgnoreCase, out requestedAction))
			{
				return new FlaggedForActionCondition(rule, LocalizedDescriptionAttribute.FromEnum(FlaggedForActionCondition.RequestedActionType, requestedAction));
			}
			return new FlaggedForActionCondition(rule, action);
		}

		// Token: 0x06006ADD RID: 27357 RVA: 0x001C7EE4 File Offset: 0x001C60E4
		internal override Restriction BuildRestriction()
		{
			if (string.Equals(this.action, RequestedAction.Any.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				return Condition.CreatePropertyRestriction<int>((PropTag)277872643U, 2);
			}
			PropTag propertyTag = base.Rule.PropertyDefinitionToPropTagFromCache(Rule.NamedDefinitions[5]);
			return Condition.CreateAndRestriction(new Restriction[]
			{
				Condition.CreatePropertyRestriction<int>((PropTag)277872643U, 2),
				Condition.CreatePropertyRestriction<string>(propertyTag, this.action)
			});
		}

		// Token: 0x04003CEF RID: 15599
		internal const int MapiFlagStatusValue = 2;

		// Token: 0x04003CF0 RID: 15600
		private readonly string action;

		// Token: 0x04003CF1 RID: 15601
		public static readonly Type RequestedActionType = typeof(RequestedAction);
	}
}
