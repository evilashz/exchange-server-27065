using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B88 RID: 2952
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class Condition
	{
		// Token: 0x06006A47 RID: 27207 RVA: 0x001C6148 File Offset: 0x001C4348
		private static Dictionary<ConditionType, ConditionOrder> BuildOrder(Dictionary<ConditionType, ConditionOrder> order)
		{
			return new Dictionary<ConditionType, ConditionOrder>
			{
				{
					ConditionType.FromRecipientsCondition,
					ConditionOrder.FromRecipientsCondition
				},
				{
					ConditionType.ContainsSubjectStringCondition,
					ConditionOrder.ContainsSubjectStringCondition
				},
				{
					ConditionType.SentOnlyToMeCondition,
					ConditionOrder.SentOnlyToMeCondition
				},
				{
					ConditionType.SentToMeCondition,
					ConditionOrder.SentToMeCondition
				},
				{
					ConditionType.MarkedAsImportanceCondition,
					ConditionOrder.MarkedAsImportanceCondition
				},
				{
					ConditionType.MarkedAsSensitivityCondition,
					ConditionOrder.MarkedAsSensitivityCondition
				},
				{
					ConditionType.SentCcMeCondition,
					ConditionOrder.SentCcMeCondition
				},
				{
					ConditionType.SentToOrCcMeCondition,
					ConditionOrder.SentToOrCcMeCondition
				},
				{
					ConditionType.NotSentToMeCondition,
					ConditionOrder.NotSentToMeCondition
				},
				{
					ConditionType.SentToRecipientsCondition,
					ConditionOrder.SentToRecipientsCondition
				},
				{
					ConditionType.ContainsBodyStringCondition,
					ConditionOrder.ContainsBodyStringCondition
				},
				{
					ConditionType.ContainsSubjectOrBodyStringCondition,
					ConditionOrder.ContainsSubjectOrBodyStringCondition
				},
				{
					ConditionType.ContainsHeaderStringCondition,
					ConditionOrder.ContainsHeaderStringCondition
				},
				{
					ConditionType.ContainsSenderStringCondition,
					ConditionOrder.ContainsSenderStringCondition
				},
				{
					ConditionType.MarkedAsOofCondition,
					ConditionOrder.MarkedAsOofCondition
				},
				{
					ConditionType.HasAttachmentCondition,
					ConditionOrder.HasAttachmentCondition
				},
				{
					ConditionType.WithinSizeRangeCondition,
					ConditionOrder.WithinSizeRangeCondition
				},
				{
					ConditionType.WithinDateRangeCondition,
					ConditionOrder.WithinDateRangeCondition
				},
				{
					ConditionType.MeetingMessageCondition,
					ConditionOrder.MeetingMessageCondition
				},
				{
					ConditionType.MeetingResponseCondition,
					ConditionOrder.MeetingResponseCondition
				},
				{
					ConditionType.ContainsRecipientStringCondition,
					ConditionOrder.ContainsRecipientStringCondition
				},
				{
					ConditionType.AssignedCategoriesCondition,
					ConditionOrder.AssignedCategoriesCondition
				},
				{
					ConditionType.FormsCondition,
					ConditionOrder.FormsCondition
				},
				{
					ConditionType.MessageClassificationCondition,
					ConditionOrder.MessageClassificationCondition
				},
				{
					ConditionType.NdrCondition,
					ConditionOrder.NdrCondition
				},
				{
					ConditionType.AutomaticForwardCondition,
					ConditionOrder.AutomaticForwardCondition
				},
				{
					ConditionType.EncryptedCondition,
					ConditionOrder.EncryptedCondition
				},
				{
					ConditionType.SignedCondition,
					ConditionOrder.SignedCondition
				},
				{
					ConditionType.ReadReceiptCondition,
					ConditionOrder.ReadReceiptCondition
				},
				{
					ConditionType.PermissionControlledCondition,
					ConditionOrder.PermissionControlledCondition
				},
				{
					ConditionType.ApprovalRequestCondition,
					ConditionOrder.ApprovalRequestCondition
				},
				{
					ConditionType.VoicemailCondition,
					ConditionOrder.VoicemailCondition
				},
				{
					ConditionType.FlaggedForActionCondition,
					ConditionOrder.FlaggedForActionCondition
				},
				{
					ConditionType.FromSubscriptionCondition,
					ConditionOrder.FromSubscriptionCondition
				}
			};
		}

		// Token: 0x06006A48 RID: 27208 RVA: 0x001C62A4 File Offset: 0x001C44A4
		private static Dictionary<ConditionType, ExceptionOrder> BuildExceptionOrder(Dictionary<ConditionType, ExceptionOrder> exceptionOrder)
		{
			return new Dictionary<ConditionType, ExceptionOrder>
			{
				{
					ConditionType.FromRecipientsCondition,
					ExceptionOrder.FromRecipientsCondition
				},
				{
					ConditionType.ContainsSubjectStringCondition,
					ExceptionOrder.ContainsSubjectStringCondition
				},
				{
					ConditionType.SentOnlyToMeCondition,
					ExceptionOrder.SentOnlyToMeCondition
				},
				{
					ConditionType.SentToMeCondition,
					ExceptionOrder.SentToMeCondition
				},
				{
					ConditionType.MarkedAsImportanceCondition,
					ExceptionOrder.MarkedAsImportanceCondition
				},
				{
					ConditionType.MarkedAsSensitivityCondition,
					ExceptionOrder.MarkedAsSensitivityCondition
				},
				{
					ConditionType.SentCcMeCondition,
					ExceptionOrder.SentCcMeCondition
				},
				{
					ConditionType.SentToOrCcMeCondition,
					ExceptionOrder.SentToOrCcMeCondition
				},
				{
					ConditionType.NotSentToMeCondition,
					ExceptionOrder.NotSentToMeCondition
				},
				{
					ConditionType.SentToRecipientsCondition,
					ExceptionOrder.SentToRecipientsCondition
				},
				{
					ConditionType.ContainsBodyStringCondition,
					ExceptionOrder.ContainsBodyStringCondition
				},
				{
					ConditionType.ContainsSubjectOrBodyStringCondition,
					ExceptionOrder.ContainsSubjectOrBodyStringCondition
				},
				{
					ConditionType.ContainsHeaderStringCondition,
					ExceptionOrder.ContainsHeaderStringCondition
				},
				{
					ConditionType.ContainsSenderStringCondition,
					ExceptionOrder.ContainsSenderStringCondition
				},
				{
					ConditionType.MarkedAsOofCondition,
					ExceptionOrder.MarkedAsOofCondition
				},
				{
					ConditionType.HasAttachmentCondition,
					ExceptionOrder.HasAttachmentCondition
				},
				{
					ConditionType.WithinSizeRangeCondition,
					ExceptionOrder.WithinSizeRangeCondition
				},
				{
					ConditionType.WithinDateRangeCondition,
					ExceptionOrder.WithinDateRangeCondition
				},
				{
					ConditionType.MeetingMessageCondition,
					ExceptionOrder.MeetingMessageCondition
				},
				{
					ConditionType.MeetingResponseCondition,
					ExceptionOrder.MeetingResponseCondition
				},
				{
					ConditionType.ContainsRecipientStringCondition,
					ExceptionOrder.ContainsRecipientStringCondition
				},
				{
					ConditionType.AssignedCategoriesCondition,
					ExceptionOrder.AssignedCategoriesCondition
				},
				{
					ConditionType.FormsCondition,
					ExceptionOrder.FormsCondition
				},
				{
					ConditionType.MessageClassificationCondition,
					ExceptionOrder.MessageClassificationCondition
				},
				{
					ConditionType.NdrCondition,
					ExceptionOrder.NdrCondition
				},
				{
					ConditionType.AutomaticForwardCondition,
					ExceptionOrder.AutomaticForwardCondition
				},
				{
					ConditionType.EncryptedCondition,
					ExceptionOrder.EncryptedCondition
				},
				{
					ConditionType.SignedCondition,
					ExceptionOrder.SignedCondition
				},
				{
					ConditionType.ReadReceiptCondition,
					ExceptionOrder.ReadReceiptCondition
				},
				{
					ConditionType.PermissionControlledCondition,
					ExceptionOrder.PermissionControlledCondition
				},
				{
					ConditionType.ApprovalRequestCondition,
					ExceptionOrder.ApprovalRequestCondition
				},
				{
					ConditionType.VoicemailCondition,
					ExceptionOrder.VoicemailCondition
				},
				{
					ConditionType.FlaggedForActionCondition,
					ExceptionOrder.FlaggedForActionCondition
				},
				{
					ConditionType.FromSubscriptionCondition,
					ExceptionOrder.FromSubscriptionCondition
				}
			};
		}

		// Token: 0x06006A49 RID: 27209 RVA: 0x001C63FF File Offset: 0x001C45FF
		protected Condition(ConditionType conditionType, Rule rule)
		{
			EnumValidator.ThrowIfInvalid<ConditionType>(conditionType, "conditionType");
			this.conditionType = conditionType;
			this.rule = rule;
		}

		// Token: 0x17001D09 RID: 7433
		// (get) Token: 0x06006A4A RID: 27210 RVA: 0x001C6420 File Offset: 0x001C4620
		public ConditionType ConditionType
		{
			get
			{
				return this.conditionType;
			}
		}

		// Token: 0x17001D0A RID: 7434
		// (get) Token: 0x06006A4B RID: 27211 RVA: 0x001C6428 File Offset: 0x001C4628
		public Rule Rule
		{
			get
			{
				return this.rule;
			}
		}

		// Token: 0x17001D0B RID: 7435
		// (get) Token: 0x06006A4C RID: 27212 RVA: 0x001C6430 File Offset: 0x001C4630
		public virtual Rule.ProviderIdEnum ProviderId
		{
			get
			{
				return Rule.ProviderIdEnum.OL98Plus;
			}
		}

		// Token: 0x17001D0C RID: 7436
		// (get) Token: 0x06006A4D RID: 27213 RVA: 0x001C6433 File Offset: 0x001C4633
		internal ConditionOrder ConditionOrder
		{
			get
			{
				return Condition.ConditionsOrder[this.ConditionType];
			}
		}

		// Token: 0x17001D0D RID: 7437
		// (get) Token: 0x06006A4E RID: 27214 RVA: 0x001C6445 File Offset: 0x001C4645
		internal ExceptionOrder ExceptionOrder
		{
			get
			{
				return Condition.ExceptionsOrder[this.ConditionType];
			}
		}

		// Token: 0x06006A4F RID: 27215
		internal abstract Restriction BuildRestriction();

		// Token: 0x06006A50 RID: 27216 RVA: 0x001C6458 File Offset: 0x001C4658
		protected static Restriction CreatePropertyRestriction<T>(PropTag propertyTag, T value)
		{
			Restriction result;
			if ((PropTag)4096U == ((PropTag)4096U & propertyTag))
			{
				PropTag tag = propertyTag & (PropTag)4294963199U;
				result = new Restriction.PropertyRestriction(Restriction.RelOp.Equal, tag, true, value);
			}
			else
			{
				result = Restriction.EQ(propertyTag, value);
			}
			return result;
		}

		// Token: 0x06006A51 RID: 27217 RVA: 0x001C649C File Offset: 0x001C469C
		protected static Restriction CreateAndStringPropertyRestriction(PropTag propertyTag, string[] values)
		{
			Restriction[] array = new Restriction.PropertyRestriction[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = Condition.CreatePropertyRestriction<string>(propertyTag, values[i]);
			}
			return Condition.CreateAndRestriction(array);
		}

		// Token: 0x06006A52 RID: 27218 RVA: 0x001C64D4 File Offset: 0x001C46D4
		protected static Restriction CreateAndRestriction(Restriction[] subRestrictions)
		{
			return Restriction.And(subRestrictions);
		}

		// Token: 0x06006A53 RID: 27219 RVA: 0x001C64EC File Offset: 0x001C46EC
		protected static Restriction CreateSearchKeyContentRestriction(PropTag propertyTag, byte[] value, ContentFlags flags)
		{
			return Restriction.Content(propertyTag, value, flags);
		}

		// Token: 0x06006A54 RID: 27220 RVA: 0x001C6504 File Offset: 0x001C4704
		protected static Restriction CreateORSearchKeyContentRestriction(byte[][] values, PropTag propertyTag, ContentFlags flags)
		{
			if (values.Length > 1)
			{
				Restriction[] array = new Restriction.ContentRestriction[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					array[i] = Restriction.Content(propertyTag, values[i], flags);
				}
				return Restriction.Or(array);
			}
			if (values.Length == 1)
			{
				return Condition.CreateSearchKeyContentRestriction(propertyTag, values[0], flags);
			}
			return null;
		}

		// Token: 0x06006A55 RID: 27221 RVA: 0x001C6558 File Offset: 0x001C4758
		protected static Restriction CreateORStringContentRestriction(string[] values, PropTag propertyTag, ContentFlags flags)
		{
			if (values.Length > 1)
			{
				Restriction[] array = new Restriction.ContentRestriction[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					array[i] = Restriction.Content(propertyTag, values[i], flags);
				}
				return Restriction.Or(array);
			}
			if (values.Length == 1)
			{
				return Condition.CreateStringContentRestriction(propertyTag, values[0], flags);
			}
			return null;
		}

		// Token: 0x06006A56 RID: 27222 RVA: 0x001C65AC File Offset: 0x001C47AC
		protected static Restriction CreateORGuidContentRestriction(Guid[] values, PropTag propertyTag)
		{
			if (values.Length > 1)
			{
				Restriction[] array = new Restriction.PropertyRestriction[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					array[i] = Condition.CreatePropertyRestriction<Guid>(propertyTag, values[i]);
				}
				return Restriction.Or(array);
			}
			if (values.Length == 1)
			{
				return Condition.CreatePropertyRestriction<Guid>(propertyTag, values[0]);
			}
			return null;
		}

		// Token: 0x06006A57 RID: 27223 RVA: 0x001C6610 File Offset: 0x001C4810
		protected static Restriction CreateStringContentRestriction(PropTag propertyTag, string value, ContentFlags flags)
		{
			return Restriction.Content(propertyTag, value, flags);
		}

		// Token: 0x06006A58 RID: 27224 RVA: 0x001C6628 File Offset: 0x001C4828
		protected static Restriction CreateIntPropertyRestriction(PropTag propertyTag, int value, Restriction.RelOp relop)
		{
			return new Restriction.PropertyRestriction(relop, propertyTag, value);
		}

		// Token: 0x06006A59 RID: 27225 RVA: 0x001C6644 File Offset: 0x001C4844
		protected static Restriction CreateBooleanPropertyRestriction(PropTag propertyTag, bool value, Restriction.RelOp relop)
		{
			return new Restriction.PropertyRestriction(relop, propertyTag, value);
		}

		// Token: 0x06006A5A RID: 27226 RVA: 0x001C6660 File Offset: 0x001C4860
		protected static Restriction CreateDateTimePropertyRestriction(PropTag propertyTag, ExDateTime dateTime, Restriction.RelOp relop)
		{
			return new Restriction.PropertyRestriction(relop, propertyTag, (DateTime)dateTime.ToUtc());
		}

		// Token: 0x06006A5B RID: 27227 RVA: 0x001C6688 File Offset: 0x001C4888
		protected static Restriction CreateSubjectOrBodyRestriction(string[] values)
		{
			Restriction[] array = new Restriction.ContentRestriction[values.Length * 2];
			if (values.Length > 0)
			{
				int i = 0;
				int num = 0;
				while (i < values.Length)
				{
					array[num] = Condition.CreateStringContentRestriction(PropTag.Subject, values[i], ContentFlags.SubString | ContentFlags.IgnoreCase);
					array[num + 1] = Condition.CreateStringContentRestriction(PropTag.Body, values[i], ContentFlags.SubString | ContentFlags.IgnoreCase);
					i++;
					num += 2;
				}
				return Restriction.Or(array);
			}
			return null;
		}

		// Token: 0x06006A5C RID: 27228 RVA: 0x001C66F0 File Offset: 0x001C48F0
		protected static Restriction CreateFromRestriction(IList<Participant> participants)
		{
			if (participants.Count > 0)
			{
				return Rule.OrAddressList(participants, PropTag.SenderName);
			}
			return null;
		}

		// Token: 0x06006A5D RID: 27229 RVA: 0x001C6708 File Offset: 0x001C4908
		protected static Restriction CreateRecipientRestriction(IList<Participant> participants)
		{
			if (participants.Count > 0)
			{
				Restriction restriction = Rule.OrAddressList(participants, PropTag.DisplayName);
				return new Restriction.RecipientRestriction(restriction);
			}
			return null;
		}

		// Token: 0x06006A5E RID: 27230 RVA: 0x001C6734 File Offset: 0x001C4934
		protected static Restriction CreateOnlyToMeRestriction()
		{
			Restriction[] array = new Restriction[3];
			array[0] = Condition.CreateBooleanPropertyRestriction(PropTag.MessageToMe, true, Restriction.RelOp.Equal);
			Restriction restriction = Condition.CreateStringContentRestriction(PropTag.DisplayTo, ";", ContentFlags.SubString);
			array[1] = Restriction.Not(restriction);
			array[2] = Condition.CreatePropertyRestriction<string>(PropTag.DisplayCc, string.Empty);
			return Restriction.And(array);
		}

		// Token: 0x06006A5F RID: 27231 RVA: 0x001C678C File Offset: 0x001C498C
		protected static Restriction CreateCcToMeRestriction()
		{
			return Restriction.And(new Restriction[]
			{
				Condition.CreateBooleanPropertyRestriction(PropTag.MessageCcMe, true, Restriction.RelOp.Equal),
				Condition.CreateBooleanPropertyRestriction(PropTag.MessageRecipMe, true, Restriction.RelOp.Equal),
				Condition.CreateBooleanPropertyRestriction(PropTag.MessageToMe, false, Restriction.RelOp.Equal)
			});
		}

		// Token: 0x06006A60 RID: 27232 RVA: 0x001C67D8 File Offset: 0x001C49D8
		protected static Restriction CreateHasAttachmentRestriction()
		{
			return Restriction.BitMaskNonZero(PropTag.MessageFlags, 16);
		}

		// Token: 0x06006A61 RID: 27233 RVA: 0x001C67F4 File Offset: 0x001C49F4
		protected static Restriction CreateSizeRestriction(int? atLeast, int? atMost)
		{
			Restriction[] array = null;
			if (atLeast != null && atMost != null)
			{
				array = new Restriction[2];
			}
			if (atLeast != null)
			{
				Restriction restriction = Condition.CreateIntPropertyRestriction(PropTag.MessageSize, (atLeast * 1024).Value, Restriction.RelOp.GreaterThan);
				if (atMost == null)
				{
					return restriction;
				}
				array[0] = restriction;
			}
			if (atMost != null)
			{
				Restriction restriction2 = Condition.CreateIntPropertyRestriction(PropTag.MessageSize, (atMost * 1024).Value, Restriction.RelOp.LessThan);
				if (atLeast == null)
				{
					return restriction2;
				}
				array[1] = restriction2;
			}
			return Restriction.And(array);
		}

		// Token: 0x06006A62 RID: 27234 RVA: 0x001C68D8 File Offset: 0x001C4AD8
		protected static Restriction CreateOneOrTwoTimesRestrictions(ExDateTime? before, ExDateTime? after)
		{
			Restriction[] array = null;
			if (before != null && after != null)
			{
				array = new Restriction[2];
			}
			if (before != null)
			{
				Restriction restriction = Condition.CreateDateTimePropertyRestriction(PropTag.MessageDeliveryTime, before.Value, Restriction.RelOp.LessThan);
				if (after == null)
				{
					return restriction;
				}
				array[0] = restriction;
			}
			if (after != null)
			{
				Restriction restriction2 = Condition.CreateDateTimePropertyRestriction(PropTag.MessageDeliveryTime, after.Value, Restriction.RelOp.GreaterThan);
				if (before == null)
				{
					return restriction2;
				}
				array[1] = restriction2;
			}
			return Restriction.And(array);
		}

		// Token: 0x06006A63 RID: 27235 RVA: 0x001C6964 File Offset: 0x001C4B64
		protected static Restriction CreateIsNdrRestrictions()
		{
			return Condition.CreateAndRestriction(new Restriction[]
			{
				Condition.CreateStringContentRestriction(PropTag.MessageClass, "REPORT", ContentFlags.Prefix | ContentFlags.IgnoreCase),
				Condition.CreateStringContentRestriction(PropTag.MessageClass, ".NDR", ContentFlags.SubString | ContentFlags.IgnoreCase)
			});
		}

		// Token: 0x06006A64 RID: 27236 RVA: 0x001C6A0C File Offset: 0x001C4C0C
		protected static void CheckParams(params object[] parameters)
		{
			Rule rule = Rule.CheckRuleParameter(parameters);
			int i;
			for (i = 1; i < parameters.Length; i++)
			{
				if (parameters[i] == null)
				{
					rule.ThrowValidateException(delegate
					{
						throw new ArgumentNullException("parameter " + i);
					}, "parameter " + i);
				}
				string[] array = parameters[i] as string[];
				if (array != null)
				{
					if (array.Length == 0)
					{
						rule.ThrowValidateException(delegate
						{
							throw new ArgumentException("parameter " + i);
						}, "parameter " + i);
					}
				}
				else
				{
					string text = parameters[i] as string;
					if (text != null && text.Length == 0)
					{
						rule.ThrowValidateException(delegate
						{
							throw new ArgumentException("parameter " + i);
						}, "parameter " + i);
					}
				}
			}
		}

		// Token: 0x04003CD7 RID: 15575
		internal const PropTag MultiValueTag = (PropTag)4096U;

		// Token: 0x04003CD8 RID: 15576
		internal static Dictionary<ConditionType, ConditionOrder> ConditionsOrder = Condition.BuildOrder(Condition.ConditionsOrder);

		// Token: 0x04003CD9 RID: 15577
		internal static Dictionary<ConditionType, ExceptionOrder> ExceptionsOrder = Condition.BuildExceptionOrder(Condition.ExceptionsOrder);

		// Token: 0x04003CDA RID: 15578
		internal static ConditionOrderComparer ConditionOrderComparer = new ConditionOrderComparer();

		// Token: 0x04003CDB RID: 15579
		internal static ExceptionOrderComparer ExceptionOrderComparer = new ExceptionOrderComparer();

		// Token: 0x04003CDC RID: 15580
		private readonly ConditionType conditionType;

		// Token: 0x04003CDD RID: 15581
		private readonly Rule rule;
	}
}
