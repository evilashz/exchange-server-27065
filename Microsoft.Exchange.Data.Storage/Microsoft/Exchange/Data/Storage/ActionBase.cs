using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B70 RID: 2928
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ActionBase
	{
		// Token: 0x060069FD RID: 27133 RVA: 0x001C57DC File Offset: 0x001C39DC
		private static Dictionary<ActionType, ActionOrder> BuildOrder(Dictionary<ActionType, ActionOrder> order)
		{
			return new Dictionary<ActionType, ActionOrder>(16)
			{
				{
					ActionType.MoveToFolderAction,
					ActionOrder.MoveToFolderAction
				},
				{
					ActionType.DeleteAction,
					ActionOrder.DeleteAction
				},
				{
					ActionType.CopyToFolderAction,
					ActionOrder.CopyToFolderAction
				},
				{
					ActionType.ForwardToRecipientsAction,
					ActionOrder.ForwardToRecipientsAction
				},
				{
					ActionType.ForwardAsAttachmentToRecipientsAction,
					ActionOrder.ForwardAsAttachmentToRecipientsAction
				},
				{
					ActionType.SendSmsAlertToRecipientsAction,
					ActionOrder.SendSmsAlertToRecipientsAction
				},
				{
					ActionType.RedirectToRecipientsAction,
					ActionOrder.RedirectToRecipientsAction
				},
				{
					ActionType.ServerReplyMessageAction,
					ActionOrder.ServerReplyMessageAction
				},
				{
					ActionType.MarkImportanceAction,
					ActionOrder.MarkImportanceAction
				},
				{
					ActionType.MarkSensitivityAction,
					ActionOrder.MarkSensitivityAction
				},
				{
					ActionType.AssignCategoriesAction,
					ActionOrder.AssignCategoriesAction
				},
				{
					ActionType.FlagMessageAction,
					ActionOrder.FlagMessageAction
				},
				{
					ActionType.MarkAsReadAction,
					ActionOrder.MarkAsReadAction
				},
				{
					ActionType.StopProcessingAction,
					ActionOrder.StopProcessingAction
				},
				{
					ActionType.PermanentDeleteAction,
					ActionOrder.PermanentDeleteAction
				}
			};
		}

		// Token: 0x060069FE RID: 27134 RVA: 0x001C58AC File Offset: 0x001C3AAC
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
				IList<Participant> list = parameters[i] as IList<Participant>;
				if (list != null && list.Count == 0)
				{
					rule.ThrowValidateException(delegate
					{
						throw new ArgumentException("participants");
					}, "participants");
				}
			}
		}

		// Token: 0x060069FF RID: 27135 RVA: 0x001C5967 File Offset: 0x001C3B67
		protected ActionBase(ActionType actionType, Rule rule)
		{
			this.actionType = actionType;
			this.rule = rule;
		}

		// Token: 0x17001CF9 RID: 7417
		// (get) Token: 0x06006A00 RID: 27136 RVA: 0x001C597D File Offset: 0x001C3B7D
		public ActionType ActionType
		{
			get
			{
				return this.actionType;
			}
		}

		// Token: 0x17001CFA RID: 7418
		// (get) Token: 0x06006A01 RID: 27137 RVA: 0x001C5985 File Offset: 0x001C3B85
		public virtual Rule.ProviderIdEnum ProviderId
		{
			get
			{
				return Rule.ProviderIdEnum.OL98Plus;
			}
		}

		// Token: 0x17001CFB RID: 7419
		// (get) Token: 0x06006A02 RID: 27138 RVA: 0x001C5988 File Offset: 0x001C3B88
		internal Rule Rule
		{
			get
			{
				return this.rule;
			}
		}

		// Token: 0x17001CFC RID: 7420
		// (get) Token: 0x06006A03 RID: 27139 RVA: 0x001C5990 File Offset: 0x001C3B90
		internal ActionOrder ActionOrder
		{
			get
			{
				return ActionBase.ActionBuildOrder[this.ActionType];
			}
		}

		// Token: 0x06006A04 RID: 27140
		internal abstract RuleAction BuildRuleAction();

		// Token: 0x04003C60 RID: 15456
		internal static Dictionary<ActionType, ActionOrder> ActionBuildOrder = ActionBase.BuildOrder(ActionBase.ActionBuildOrder);

		// Token: 0x04003C61 RID: 15457
		internal static ActionOrderComparer ActionOrderComparer = new ActionOrderComparer();

		// Token: 0x04003C62 RID: 15458
		private readonly ActionType actionType;

		// Token: 0x04003C63 RID: 15459
		private readonly Rule rule;
	}
}
