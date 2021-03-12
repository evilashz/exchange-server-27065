using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000052 RID: 82
	internal static class RulesStrings
	{
		// Token: 0x06000247 RID: 583 RVA: 0x00009DE4 File Offset: 0x00007FE4
		static RulesStrings()
		{
			RulesStrings.stringIDs.Add(3561617716U, "InvalidPropertyType");
			RulesStrings.stringIDs.Add(2809257232U, "RuleSubtypeNone");
			RulesStrings.stringIDs.Add(800827665U, "ValueTextNotFound");
			RulesStrings.stringIDs.Add(853321755U, "StateDisabled");
			RulesStrings.stringIDs.Add(230388220U, "ModeAuditAndNotify");
			RulesStrings.stringIDs.Add(3847357176U, "ArgumentIncorrect");
			RulesStrings.stringIDs.Add(2209094239U, "StringArrayPropertyRequiredForMultiValue");
			RulesStrings.stringIDs.Add(250418233U, "EmptyPropertyName");
			RulesStrings.stringIDs.Add(3358696673U, "TemplateTypeAll");
			RulesStrings.stringIDs.Add(2539054471U, "MissingValue");
			RulesStrings.stringIDs.Add(4131692042U, "RuleSubtypeDlp");
			RulesStrings.stringIDs.Add(1535753010U, "ConditionTagNotFound");
			RulesStrings.stringIDs.Add(3988942463U, "RuleDescriptionExpiry");
			RulesStrings.stringIDs.Add(1547451386U, "RuleErrorActionIgnore");
			RulesStrings.stringIDs.Add(2828094232U, "RuleDescriptionAndDelimiter");
			RulesStrings.stringIDs.Add(181941971U, "TemplateTypeDistributed");
			RulesStrings.stringIDs.Add(3623941994U, "TooManyRules");
			RulesStrings.stringIDs.Add(2040889586U, "SearchablePropertyRequired");
			RulesStrings.stringIDs.Add(508904078U, "TemplateTypeArchived");
			RulesStrings.stringIDs.Add(2588488610U, "RuleDescriptionActivation");
			RulesStrings.stringIDs.Add(242810468U, "RuleErrorActionDefer");
			RulesStrings.stringIDs.Add(41715449U, "ModeEnforce");
			RulesStrings.stringIDs.Add(3213119304U, "StateEnabled");
			RulesStrings.stringIDs.Add(1688265212U, "RuleDescriptionTakeActions");
			RulesStrings.stringIDs.Add(3869829980U, "ModeAudit");
			RulesStrings.stringIDs.Add(2173155634U, "EndOfStream");
			RulesStrings.stringIDs.Add(2986652960U, "InconsistentValueTypesInConditionProperties");
			RulesStrings.stringIDs.Add(2264090780U, "RuleDescriptionExceptIf");
			RulesStrings.stringIDs.Add(250901884U, "RuleDescriptionOrDelimiter");
			RulesStrings.stringIDs.Add(1859427639U, "RuleDescriptionIf");
			RulesStrings.stringIDs.Add(2445813381U, "NoMultiValueForActionArgument");
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000A08C File Offset: 0x0000828C
		public static LocalizedString InvalidPropertyType
		{
			get
			{
				return new LocalizedString("InvalidPropertyType", "Ex825852", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000A0AA File Offset: 0x000082AA
		public static LocalizedString RuleSubtypeNone
		{
			get
			{
				return new LocalizedString("RuleSubtypeNone", "", false, false, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000A0C8 File Offset: 0x000082C8
		public static LocalizedString ValueTextNotFound
		{
			get
			{
				return new LocalizedString("ValueTextNotFound", "ExC7F50D", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000A0E6 File Offset: 0x000082E6
		public static LocalizedString StateDisabled
		{
			get
			{
				return new LocalizedString("StateDisabled", "Ex726DDA", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000A104 File Offset: 0x00008304
		public static LocalizedString InvalidAttribute(string attributeName, string tagName, string tagValue)
		{
			return new LocalizedString("InvalidAttribute", "Ex8A23FC", false, true, RulesStrings.ResourceManager, new object[]
			{
				attributeName,
				tagName,
				tagValue
			});
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000A13C File Offset: 0x0000833C
		public static LocalizedString RuleInvalidOperationDescription(string details)
		{
			return new LocalizedString("RuleInvalidOperationDescription", "ExDDC7D0", false, true, RulesStrings.ResourceManager, new object[]
			{
				details
			});
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A16B File Offset: 0x0000836B
		public static LocalizedString ModeAuditAndNotify
		{
			get
			{
				return new LocalizedString("ModeAuditAndNotify", "", false, false, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A189 File Offset: 0x00008389
		public static LocalizedString ArgumentIncorrect
		{
			get
			{
				return new LocalizedString("ArgumentIncorrect", "Ex7F8483", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000A1A8 File Offset: 0x000083A8
		public static LocalizedString InvalidActionName(string name)
		{
			return new LocalizedString("InvalidActionName", "ExB6A978", false, true, RulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A1D7 File Offset: 0x000083D7
		public static LocalizedString StringArrayPropertyRequiredForMultiValue
		{
			get
			{
				return new LocalizedString("StringArrayPropertyRequiredForMultiValue", "Ex12CF73", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A1F8 File Offset: 0x000083F8
		public static LocalizedString DelimeterNotFound(string propertyName)
		{
			return new LocalizedString("DelimeterNotFound", "Ex8C7A04", false, true, RulesStrings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A228 File Offset: 0x00008428
		public static LocalizedString NameTooLong(string node, int maxLength)
		{
			return new LocalizedString("NameTooLong", "Ex1053AF", false, true, RulesStrings.ResourceManager, new object[]
			{
				node,
				maxLength
			});
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000A260 File Offset: 0x00008460
		public static LocalizedString InvalidPropertyForRule(string property, string rule)
		{
			return new LocalizedString("InvalidPropertyForRule", "Ex9EAEE4", false, true, RulesStrings.ResourceManager, new object[]
			{
				property,
				rule
			});
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000A294 File Offset: 0x00008494
		public static LocalizedString InvalidValue(string valueName)
		{
			return new LocalizedString("InvalidValue", "", false, false, RulesStrings.ResourceManager, new object[]
			{
				valueName
			});
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000A2C3 File Offset: 0x000084C3
		public static LocalizedString EmptyPropertyName
		{
			get
			{
				return new LocalizedString("EmptyPropertyName", "Ex324B29", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000A2E1 File Offset: 0x000084E1
		public static LocalizedString TemplateTypeAll
		{
			get
			{
				return new LocalizedString("TemplateTypeAll", "Ex332B3A", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000A2FF File Offset: 0x000084FF
		public static LocalizedString MissingValue
		{
			get
			{
				return new LocalizedString("MissingValue", "Ex88B534", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A320 File Offset: 0x00008520
		public static LocalizedString ValueIsNotAllowed(string name)
		{
			return new LocalizedString("ValueIsNotAllowed", "Ex79F9E4", false, true, RulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000A34F File Offset: 0x0000854F
		public static LocalizedString RuleSubtypeDlp
		{
			get
			{
				return new LocalizedString("RuleSubtypeDlp", "", false, false, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A370 File Offset: 0x00008570
		public static LocalizedString InvalidArgumentForType(string argument, string type)
		{
			return new LocalizedString("InvalidArgumentForType", "Ex6597F9", false, true, RulesStrings.ResourceManager, new object[]
			{
				argument,
				type
			});
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000A3A3 File Offset: 0x000085A3
		public static LocalizedString ConditionTagNotFound
		{
			get
			{
				return new LocalizedString("ConditionTagNotFound", "Ex6DDB27", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000A3C1 File Offset: 0x000085C1
		public static LocalizedString RuleDescriptionExpiry
		{
			get
			{
				return new LocalizedString("RuleDescriptionExpiry", "", false, false, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A3E0 File Offset: 0x000085E0
		public static LocalizedString PropertyTypeIsFixed(string name)
		{
			return new LocalizedString("PropertyTypeIsFixed", "ExE3AFB8", false, true, RulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A410 File Offset: 0x00008610
		public static LocalizedString ActionNotFound(string name)
		{
			return new LocalizedString("ActionNotFound", "Ex4EB782", false, true, RulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000A43F File Offset: 0x0000863F
		public static LocalizedString RuleErrorActionIgnore
		{
			get
			{
				return new LocalizedString("RuleErrorActionIgnore", "", false, false, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A460 File Offset: 0x00008660
		public static LocalizedString RuleNameExists(string ruleName)
		{
			return new LocalizedString("RuleNameExists", "Ex17E8C7", false, true, RulesStrings.ResourceManager, new object[]
			{
				ruleName
			});
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000A48F File Offset: 0x0000868F
		public static LocalizedString RuleDescriptionAndDelimiter
		{
			get
			{
				return new LocalizedString("RuleDescriptionAndDelimiter", "Ex318FEA", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000A4AD File Offset: 0x000086AD
		public static LocalizedString TemplateTypeDistributed
		{
			get
			{
				return new LocalizedString("TemplateTypeDistributed", "Ex98338D", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A4CB File Offset: 0x000086CB
		public static LocalizedString TooManyRules
		{
			get
			{
				return new LocalizedString("TooManyRules", "Ex79D216", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000A4E9 File Offset: 0x000086E9
		public static LocalizedString SearchablePropertyRequired
		{
			get
			{
				return new LocalizedString("SearchablePropertyRequired", "Ex9FDCE3", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000A507 File Offset: 0x00008707
		public static LocalizedString TemplateTypeArchived
		{
			get
			{
				return new LocalizedString("TemplateTypeArchived", "ExC49CEF", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A528 File Offset: 0x00008728
		public static LocalizedString EndTagNotFound(string elementName)
		{
			return new LocalizedString("EndTagNotFound", "Ex0A7237", false, true, RulesStrings.ResourceManager, new object[]
			{
				elementName
			});
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000A557 File Offset: 0x00008757
		public static LocalizedString RuleDescriptionActivation
		{
			get
			{
				return new LocalizedString("RuleDescriptionActivation", "", false, false, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000A575 File Offset: 0x00008775
		public static LocalizedString RuleErrorActionDefer
		{
			get
			{
				return new LocalizedString("RuleErrorActionDefer", "", false, false, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A594 File Offset: 0x00008794
		public static LocalizedString InvalidPropertyName(string Name)
		{
			return new LocalizedString("InvalidPropertyName", "Ex032889", false, true, RulesStrings.ResourceManager, new object[]
			{
				Name
			});
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000A5C3 File Offset: 0x000087C3
		public static LocalizedString ModeEnforce
		{
			get
			{
				return new LocalizedString("ModeEnforce", "", false, false, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000A5E1 File Offset: 0x000087E1
		public static LocalizedString StateEnabled
		{
			get
			{
				return new LocalizedString("StateEnabled", "Ex1B5E80", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000A5FF File Offset: 0x000087FF
		public static LocalizedString RuleDescriptionTakeActions
		{
			get
			{
				return new LocalizedString("RuleDescriptionTakeActions", "Ex5F7B8F", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000A61D File Offset: 0x0000881D
		public static LocalizedString ModeAudit
		{
			get
			{
				return new LocalizedString("ModeAudit", "", false, false, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000A63B File Offset: 0x0000883B
		public static LocalizedString EndOfStream
		{
			get
			{
				return new LocalizedString("EndOfStream", "ExE9105D", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000A659 File Offset: 0x00008859
		public static LocalizedString InconsistentValueTypesInConditionProperties
		{
			get
			{
				return new LocalizedString("InconsistentValueTypesInConditionProperties", "", false, false, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A678 File Offset: 0x00008878
		public static LocalizedString TagNotFound(string elementName)
		{
			return new LocalizedString("TagNotFound", "ExD072AB", false, true, RulesStrings.ResourceManager, new object[]
			{
				elementName
			});
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A6A8 File Offset: 0x000088A8
		public static LocalizedString ActionRequiresConstantArguments(string name)
		{
			return new LocalizedString("ActionRequiresConstantArguments", "Ex04D6DE", false, true, RulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A6D8 File Offset: 0x000088D8
		public static LocalizedString InvalidKeyValueParameter(string valueType)
		{
			return new LocalizedString("InvalidKeyValueParameter", "", false, false, RulesStrings.ResourceManager, new object[]
			{
				valueType
			});
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A708 File Offset: 0x00008908
		public static LocalizedString RuleParsingError(string diagnostic, int lineNumber, int linePosition)
		{
			return new LocalizedString("RuleParsingError", "Ex119BBB", false, true, RulesStrings.ResourceManager, new object[]
			{
				diagnostic,
				lineNumber,
				linePosition
			});
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A74C File Offset: 0x0000894C
		public static LocalizedString InvalidArgumentType(string type)
		{
			return new LocalizedString("InvalidArgumentType", "Ex42DFEA", false, true, RulesStrings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A77C File Offset: 0x0000897C
		public static LocalizedString InvalidCondition(string conditionName)
		{
			return new LocalizedString("InvalidCondition", "Ex3B434A", false, true, RulesStrings.ResourceManager, new object[]
			{
				conditionName
			});
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A7AC File Offset: 0x000089AC
		public static LocalizedString ActionArgumentMismatch(string name)
		{
			return new LocalizedString("ActionArgumentMismatch", "ExE8E9BF", false, true, RulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000A7DB File Offset: 0x000089DB
		public static LocalizedString RuleDescriptionExceptIf
		{
			get
			{
				return new LocalizedString("RuleDescriptionExceptIf", "Ex00BFD1", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000A7F9 File Offset: 0x000089F9
		public static LocalizedString RuleDescriptionOrDelimiter
		{
			get
			{
				return new LocalizedString("RuleDescriptionOrDelimiter", "ExAB3F67", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A818 File Offset: 0x00008A18
		public static LocalizedString StringPropertyOrValueRequired(string name)
		{
			return new LocalizedString("StringPropertyOrValueRequired", "Ex16C9F7", false, true, RulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A848 File Offset: 0x00008A48
		public static LocalizedString AttributeNotFound(string attributeName, string elementName)
		{
			return new LocalizedString("AttributeNotFound", "Ex0E7C3B", false, true, RulesStrings.ResourceManager, new object[]
			{
				attributeName,
				elementName
			});
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A87C File Offset: 0x00008A7C
		public static LocalizedString PropertyNotFound(string name)
		{
			return new LocalizedString("PropertyNotFound", "Ex768F43", false, true, RulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A8AC File Offset: 0x00008AAC
		public static LocalizedString NumericalPropertyRequiredForPredicate(string name)
		{
			return new LocalizedString("NumericalPropertyRequiredForPredicate", "Ex6E1D0E", false, true, RulesStrings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A8DC File Offset: 0x00008ADC
		public static LocalizedString EmptyTag(string elementName)
		{
			return new LocalizedString("EmptyTag", "Ex73F5EB", false, true, RulesStrings.ResourceManager, new object[]
			{
				elementName
			});
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000A90B File Offset: 0x00008B0B
		public static LocalizedString RuleDescriptionIf
		{
			get
			{
				return new LocalizedString("RuleDescriptionIf", "Ex3C413B", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000A929 File Offset: 0x00008B29
		public static LocalizedString NoMultiValueForActionArgument
		{
			get
			{
				return new LocalizedString("NoMultiValueForActionArgument", "Ex36F9A8", false, true, RulesStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A947 File Offset: 0x00008B47
		public static LocalizedString GetLocalizedString(RulesStrings.IDs key)
		{
			return new LocalizedString(RulesStrings.stringIDs[(uint)key], RulesStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000105 RID: 261
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(31);

		// Token: 0x04000106 RID: 262
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MessagingPolicies.Rules.RulesStrings", typeof(RulesStrings).GetTypeInfo().Assembly);

		// Token: 0x02000053 RID: 83
		public enum IDs : uint
		{
			// Token: 0x04000108 RID: 264
			InvalidPropertyType = 3561617716U,
			// Token: 0x04000109 RID: 265
			RuleSubtypeNone = 2809257232U,
			// Token: 0x0400010A RID: 266
			ValueTextNotFound = 800827665U,
			// Token: 0x0400010B RID: 267
			StateDisabled = 853321755U,
			// Token: 0x0400010C RID: 268
			ModeAuditAndNotify = 230388220U,
			// Token: 0x0400010D RID: 269
			ArgumentIncorrect = 3847357176U,
			// Token: 0x0400010E RID: 270
			StringArrayPropertyRequiredForMultiValue = 2209094239U,
			// Token: 0x0400010F RID: 271
			EmptyPropertyName = 250418233U,
			// Token: 0x04000110 RID: 272
			TemplateTypeAll = 3358696673U,
			// Token: 0x04000111 RID: 273
			MissingValue = 2539054471U,
			// Token: 0x04000112 RID: 274
			RuleSubtypeDlp = 4131692042U,
			// Token: 0x04000113 RID: 275
			ConditionTagNotFound = 1535753010U,
			// Token: 0x04000114 RID: 276
			RuleDescriptionExpiry = 3988942463U,
			// Token: 0x04000115 RID: 277
			RuleErrorActionIgnore = 1547451386U,
			// Token: 0x04000116 RID: 278
			RuleDescriptionAndDelimiter = 2828094232U,
			// Token: 0x04000117 RID: 279
			TemplateTypeDistributed = 181941971U,
			// Token: 0x04000118 RID: 280
			TooManyRules = 3623941994U,
			// Token: 0x04000119 RID: 281
			SearchablePropertyRequired = 2040889586U,
			// Token: 0x0400011A RID: 282
			TemplateTypeArchived = 508904078U,
			// Token: 0x0400011B RID: 283
			RuleDescriptionActivation = 2588488610U,
			// Token: 0x0400011C RID: 284
			RuleErrorActionDefer = 242810468U,
			// Token: 0x0400011D RID: 285
			ModeEnforce = 41715449U,
			// Token: 0x0400011E RID: 286
			StateEnabled = 3213119304U,
			// Token: 0x0400011F RID: 287
			RuleDescriptionTakeActions = 1688265212U,
			// Token: 0x04000120 RID: 288
			ModeAudit = 3869829980U,
			// Token: 0x04000121 RID: 289
			EndOfStream = 2173155634U,
			// Token: 0x04000122 RID: 290
			InconsistentValueTypesInConditionProperties = 2986652960U,
			// Token: 0x04000123 RID: 291
			RuleDescriptionExceptIf = 2264090780U,
			// Token: 0x04000124 RID: 292
			RuleDescriptionOrDelimiter = 250901884U,
			// Token: 0x04000125 RID: 293
			RuleDescriptionIf = 1859427639U,
			// Token: 0x04000126 RID: 294
			NoMultiValueForActionArgument = 2445813381U
		}

		// Token: 0x02000054 RID: 84
		private enum ParamIDs
		{
			// Token: 0x04000128 RID: 296
			InvalidAttribute,
			// Token: 0x04000129 RID: 297
			RuleInvalidOperationDescription,
			// Token: 0x0400012A RID: 298
			InvalidActionName,
			// Token: 0x0400012B RID: 299
			DelimeterNotFound,
			// Token: 0x0400012C RID: 300
			NameTooLong,
			// Token: 0x0400012D RID: 301
			InvalidPropertyForRule,
			// Token: 0x0400012E RID: 302
			InvalidValue,
			// Token: 0x0400012F RID: 303
			ValueIsNotAllowed,
			// Token: 0x04000130 RID: 304
			InvalidArgumentForType,
			// Token: 0x04000131 RID: 305
			PropertyTypeIsFixed,
			// Token: 0x04000132 RID: 306
			ActionNotFound,
			// Token: 0x04000133 RID: 307
			RuleNameExists,
			// Token: 0x04000134 RID: 308
			EndTagNotFound,
			// Token: 0x04000135 RID: 309
			InvalidPropertyName,
			// Token: 0x04000136 RID: 310
			TagNotFound,
			// Token: 0x04000137 RID: 311
			ActionRequiresConstantArguments,
			// Token: 0x04000138 RID: 312
			InvalidKeyValueParameter,
			// Token: 0x04000139 RID: 313
			RuleParsingError,
			// Token: 0x0400013A RID: 314
			InvalidArgumentType,
			// Token: 0x0400013B RID: 315
			InvalidCondition,
			// Token: 0x0400013C RID: 316
			ActionArgumentMismatch,
			// Token: 0x0400013D RID: 317
			StringPropertyOrValueRequired,
			// Token: 0x0400013E RID: 318
			AttributeNotFound,
			// Token: 0x0400013F RID: 319
			PropertyNotFound,
			// Token: 0x04000140 RID: 320
			NumericalPropertyRequiredForPredicate,
			// Token: 0x04000141 RID: 321
			EmptyTag
		}
	}
}
