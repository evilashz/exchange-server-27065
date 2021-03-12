using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B77 RID: 2935
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ServerReplyMessageAction : IdAction
	{
		// Token: 0x06006A17 RID: 27159 RVA: 0x001C5BB4 File Offset: 0x001C3DB4
		private ServerReplyMessageAction(StoreObjectId id, Guid guidTemplate, Rule rule) : base(ActionType.ServerReplyMessageAction, id, rule)
		{
			this.guidTemplate = guidTemplate;
		}

		// Token: 0x06006A18 RID: 27160 RVA: 0x001C5BC8 File Offset: 0x001C3DC8
		public static ServerReplyMessageAction Create(StoreObjectId messageId, Guid guidTemplate, Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule,
				messageId
			});
			if (!IdConverter.IsMessageId(messageId))
			{
				throw new ArgumentException("messageId");
			}
			return new ServerReplyMessageAction(messageId, guidTemplate, rule);
		}

		// Token: 0x06006A19 RID: 27161 RVA: 0x001C5C08 File Offset: 0x001C3E08
		internal override RuleAction BuildRuleAction()
		{
			return new RuleAction.Reply(base.Id.ProviderLevelItemId, this.GuidTemplate, RuleAction.Reply.ActionFlags.None);
		}

		// Token: 0x17001CFE RID: 7422
		// (get) Token: 0x06006A1A RID: 27162 RVA: 0x001C5C2E File Offset: 0x001C3E2E
		public Guid GuidTemplate
		{
			get
			{
				return this.guidTemplate;
			}
		}

		// Token: 0x04003C67 RID: 15463
		private readonly Guid guidTemplate;
	}
}
