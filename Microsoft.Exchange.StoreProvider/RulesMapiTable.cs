using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000248 RID: 584
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RulesMapiTable : MapiTable
	{
		// Token: 0x06000A51 RID: 2641 RVA: 0x000325E4 File Offset: 0x000307E4
		internal RulesMapiTable(IExMapiTable iMAPITable, MapiFolder folder, MapiStore mapiStore) : base(iMAPITable, mapiStore)
		{
			this.folderEntryId = (byte[])folder.GetProp(PropTag.EntryId).Value;
			base.Restrict(Rule.MiddleTierRule, RestrictFlags.Batch);
			base.SortTable(RulesMapiTable.sortSequenceAscending, SortTableFlags.Batch);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0003262F File Offset: 0x0003082F
		public override MapiNotificationHandle Advise(MapiNotificationHandler handler)
		{
			throw MapiExceptionHelper.NotSupportedException("Notifications are not supported on rules table.");
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0003263B File Offset: 0x0003083B
		public override MapiNotificationHandle Advise(MapiNotificationHandler handler, NotificationCallbackMode mode)
		{
			throw MapiExceptionHelper.NotSupportedException("Notifications are not supported on rules table.");
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00032648 File Offset: 0x00030848
		public override void SetColumns(ICollection<PropTag> propTags)
		{
			base.CheckDisposed();
			ICollection<PropTag> columns = RulesMapiTable.ConvertPropertyTagsFromRuleToMessage(propTags);
			base.SetColumns(columns);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0003266C File Offset: 0x0003086C
		public override bool FindRow(Restriction restriction, BookMark bookmark, FindRowFlag flag)
		{
			base.CheckDisposed();
			if (restriction == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("restriction");
			}
			Restriction restriction2 = Restriction.And(new Restriction[]
			{
				Rule.MiddleTierRule,
				RulesMapiTable.ConvertRestrictionFromRuleToMessage(restriction)
			});
			return base.FindRow(restriction2, bookmark, flag);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000326B5 File Offset: 0x000308B5
		public override void SortTable(SortOrder sortOrder, SortTableFlags flags)
		{
			throw MapiExceptionHelper.NotSupportedException("Sorting is not supported on rules table.");
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000326C4 File Offset: 0x000308C4
		public override void Restrict(Restriction restriction, RestrictFlags flags)
		{
			base.CheckDisposed();
			Restriction restriction2 = RulesMapiTable.ConvertRestrictionFromRuleToMessage(restriction);
			Restriction restriction3 = Restriction.And(new Restriction[]
			{
				restriction2,
				Rule.MiddleTierRule
			});
			base.Restrict(restriction3, flags);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00032700 File Offset: 0x00030900
		public override PropValue[][] QueryRows(int crows, QueryRowsFlags flags)
		{
			base.CheckDisposed();
			PropValue[][] messagePropertyValues = base.QueryRows(crows, flags);
			PropValue[][] result;
			using (MapiFolder mapiFolder = (MapiFolder)base.MapiStore.OpenEntry(this.folderEntryId))
			{
				result = RulesMapiTable.ConvertPropertyValuesFromMessageToRule(mapiFolder, messagePropertyValues);
			}
			return result;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00032758 File Offset: 0x00030958
		public override PropValue[][] ExpandRow(long categoryId, int maxRows, int flags, out int expandedRows)
		{
			throw MapiExceptionHelper.NotSupportedException("Expand/Collapse is not supported on rules table.");
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00032764 File Offset: 0x00030964
		public override int CollapseRow(long categoryId, int flags)
		{
			throw MapiExceptionHelper.NotSupportedException("Expand/Collapse is not supported on rules table.");
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00032770 File Offset: 0x00030970
		public override BookMark CreateBookmark()
		{
			throw MapiExceptionHelper.NotSupportedException("Bookmarks are not supported on rules table.");
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0003277C File Offset: 0x0003097C
		public override void FreeBookmark(BookMark bookmark)
		{
			throw MapiExceptionHelper.NotSupportedException("Bookmarks are not supported on rules table.");
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00032788 File Offset: 0x00030988
		public override byte[] GetCollapseState(byte[] instanceKey)
		{
			throw MapiExceptionHelper.NotSupportedException("Expand/Collapse is not supported on rules table.");
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00032794 File Offset: 0x00030994
		public override BookMark SetCollapseState(byte[] collapseState)
		{
			throw MapiExceptionHelper.NotSupportedException("Expand/Collapse is not supported on rules table.");
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000327A0 File Offset: 0x000309A0
		public override PropTag[] QueryColumns(QueryColumnsFlags flags)
		{
			base.CheckDisposed();
			ICollection<PropTag> messagePropertyTags = base.QueryColumns(flags);
			return RulesMapiTable.ConvertPropertyTagsFromMessageToRule(messagePropertyTags).ToArray<PropTag>();
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000327C6 File Offset: 0x000309C6
		public override void Unadvise(MapiNotificationHandle handle)
		{
			throw MapiExceptionHelper.NotSupportedException("Notifications are not supported on rules table.");
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000327D4 File Offset: 0x000309D4
		private static PropTag ConvertPropertyTagFromRuleToMessage(PropTag ruleTag)
		{
			PropTag result;
			if (!RulesMapiTable.mapFromRuleToMessagePropTag.TryGetValue(ruleTag, out result))
			{
				result = ruleTag;
			}
			return result;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000327F4 File Offset: 0x000309F4
		private static ICollection<PropTag> ConvertPropertyTagsFromRuleToMessage(ICollection<PropTag> rulePropertyTags)
		{
			List<PropTag> list = new List<PropTag>();
			foreach (PropTag ruleTag in rulePropertyTags)
			{
				list.Add(RulesMapiTable.ConvertPropertyTagFromRuleToMessage(ruleTag));
			}
			return list;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00032848 File Offset: 0x00030A48
		private static ICollection<PropTag> ConvertPropertyTagsFromMessageToRule(ICollection<PropTag> messagePropertyTags)
		{
			List<PropTag> list = new List<PropTag>();
			foreach (PropTag propTag in messagePropertyTags)
			{
				PropTag item;
				if (!RulesMapiTable.mapFromMessageToRulePropTag.TryGetValue(propTag, out item))
				{
					item = propTag;
				}
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000329FB File Offset: 0x00030BFB
		private static Restriction ConvertRestrictionFromRuleToMessage(Restriction ruleRestriction)
		{
			ruleRestriction.EnumerateRestriction(delegate(Restriction restriction, object ctx)
			{
				switch (restriction.Type)
				{
				case Restriction.ResType.Content:
				{
					Restriction.ContentRestriction contentRestriction = (Restriction.ContentRestriction)restriction;
					contentRestriction.PropValue = RulesMapiTable.ConvertPropertyValueFromRuleToMessage(contentRestriction.PropValue);
					contentRestriction.PropTag = RulesMapiTable.ConvertPropertyTagFromRuleToMessage(contentRestriction.PropTag);
					return;
				}
				case Restriction.ResType.Property:
				{
					Restriction.PropertyRestriction propertyRestriction = (Restriction.PropertyRestriction)restriction;
					propertyRestriction.PropValue = RulesMapiTable.ConvertPropertyValueFromRuleToMessage(propertyRestriction.PropValue);
					propertyRestriction.PropTag = RulesMapiTable.ConvertPropertyTagFromRuleToMessage(propertyRestriction.PropTag);
					return;
				}
				case Restriction.ResType.CompareProps:
				{
					Restriction.ComparePropertyRestriction comparePropertyRestriction = (Restriction.ComparePropertyRestriction)restriction;
					comparePropertyRestriction.TagLeft = RulesMapiTable.ConvertPropertyTagFromRuleToMessage(comparePropertyRestriction.TagLeft);
					comparePropertyRestriction.TagRight = RulesMapiTable.ConvertPropertyTagFromRuleToMessage(comparePropertyRestriction.TagRight);
					return;
				}
				case Restriction.ResType.BitMask:
				{
					Restriction.BitMaskRestriction bitMaskRestriction = (Restriction.BitMaskRestriction)restriction;
					bitMaskRestriction.Tag = RulesMapiTable.ConvertPropertyTagFromRuleToMessage(bitMaskRestriction.Tag);
					return;
				}
				case Restriction.ResType.Size:
				{
					Restriction.SizeRestriction sizeRestriction = (Restriction.SizeRestriction)restriction;
					sizeRestriction.Tag = RulesMapiTable.ConvertPropertyTagFromRuleToMessage(sizeRestriction.Tag);
					return;
				}
				case Restriction.ResType.Exist:
				{
					Restriction.ExistRestriction existRestriction = (Restriction.ExistRestriction)restriction;
					existRestriction.Tag = RulesMapiTable.ConvertPropertyTagFromRuleToMessage(existRestriction.Tag);
					return;
				}
				case Restriction.ResType.SubRestriction:
					break;
				case Restriction.ResType.Comment:
				{
					Restriction.CommentRestriction commentRestriction = (Restriction.CommentRestriction)restriction;
					PropValue[] values = commentRestriction.Values;
					for (int i = 0; i < values.Length; i++)
					{
						values[i] = RulesMapiTable.ConvertPropertyValueFromRuleToMessage(values[i]);
					}
					break;
				}
				default:
					return;
				}
			}, null);
			return ruleRestriction;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00032A24 File Offset: 0x00030C24
		private static PropValue[][] ConvertPropertyValuesFromMessageToRule(MapiFolder folder, PropValue[][] messagePropertyValues)
		{
			foreach (PropValue[] array in messagePropertyValues)
			{
				for (int j = 0; j < array.Length; j++)
				{
					PropTag propTag = array[j].PropTag;
					PropTag propTag2;
					if (RulesMapiTable.mapFromMessageToRulePropTag.TryGetValue(propTag, out propTag2))
					{
						object obj = array[j].Value;
						if (propTag2 == PropTag.RuleActions)
						{
							obj = folder.DeserializeActions((byte[])obj);
						}
						else if (propTag2 == PropTag.RuleCondition)
						{
							obj = folder.DeserializeRestriction((byte[])obj);
						}
						array[j] = new PropValue(propTag2, obj);
					}
					else if (array[j].PropTag.Id() == Rule.PR_EX_RULE_CONDITION.Id() && array[j].IsError() && array[j].GetErrorValue() == -2147221233)
					{
						array[j] = new PropValue(PropTag.RuleCondition, null);
					}
				}
			}
			return messagePropertyValues;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00032B30 File Offset: 0x00030D30
		private static PropValue ConvertPropertyValueFromRuleToMessage(PropValue rulePropertyValue)
		{
			PropTag propTag = rulePropertyValue.PropTag;
			PropTag propTag2;
			if (RulesMapiTable.mapFromRuleToMessagePropTag.TryGetValue(propTag, out propTag2))
			{
				return new PropValue(propTag2, rulePropertyValue.Value);
			}
			return rulePropertyValue;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00032B64 File Offset: 0x00030D64
		private static Dictionary<PropTag, PropTag> BuildPropTagMap(PropTag[] from, PropTag[] to)
		{
			Dictionary<PropTag, PropTag> dictionary = new Dictionary<PropTag, PropTag>();
			for (int i = 0; i < from.Length; i++)
			{
				dictionary.Add(from[i], to[i]);
			}
			return dictionary;
		}

		// Token: 0x0400103E RID: 4158
		private static readonly PropTag[] rulePropTags = new PropTag[]
		{
			PropTag.RuleID,
			PropTag.RuleSequence,
			PropTag.RuleState,
			PropTag.RuleUserFlags,
			PropTag.RuleProvider,
			PropTag.RuleName,
			PropTag.RuleLevel,
			PropTag.RuleProviderData,
			PropTag.RuleActions,
			PropTag.RuleCondition
		};

		// Token: 0x0400103F RID: 4159
		private static readonly PropTag[] messagePropTags = new PropTag[]
		{
			Rule.PR_EX_RULE_ID,
			Rule.PR_EX_RULE_SEQUENCE,
			Rule.PR_EX_RULE_STATE,
			Rule.PR_EX_RULE_USER_FLAGS,
			Rule.PR_EX_RULE_PROVIDER,
			Rule.PR_EX_RULE_NAME,
			Rule.PR_EX_RULE_LEVEL,
			Rule.PR_EX_RULE_PROVIDER_DATA,
			Rule.PR_EX_RULE_ACTIONS,
			Rule.PR_EX_RULE_CONDITION
		};

		// Token: 0x04001040 RID: 4160
		private static Dictionary<PropTag, PropTag> mapFromRuleToMessagePropTag = RulesMapiTable.BuildPropTagMap(RulesMapiTable.rulePropTags, RulesMapiTable.messagePropTags);

		// Token: 0x04001041 RID: 4161
		private static Dictionary<PropTag, PropTag> mapFromMessageToRulePropTag = RulesMapiTable.BuildPropTagMap(RulesMapiTable.messagePropTags, RulesMapiTable.rulePropTags);

		// Token: 0x04001042 RID: 4162
		private static readonly SortOrder sortSequenceAscending = new SortOrder(Rule.PR_EX_RULE_SEQUENCE, SortFlags.Ascend);

		// Token: 0x04001043 RID: 4163
		private readonly byte[] folderEntryId;
	}
}
