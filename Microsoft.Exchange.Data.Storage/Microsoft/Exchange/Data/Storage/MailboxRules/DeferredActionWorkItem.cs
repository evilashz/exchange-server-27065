using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BDA RID: 3034
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DeferredActionWorkItem : WorkItem
	{
		// Token: 0x06006BCE RID: 27598 RVA: 0x001CE1EE File Offset: 0x001CC3EE
		public DeferredActionWorkItem(IRuleEvaluationContext context, string provider, int actionIndex) : base(context, actionIndex)
		{
			this.provider = provider;
			this.actions = new List<DeferredActionWorkItem.ActionInfo>();
			this.folder = context.CurrentFolder;
		}

		// Token: 0x17001D43 RID: 7491
		// (get) Token: 0x06006BCF RID: 27599 RVA: 0x001CE218 File Offset: 0x001CC418
		public bool HasMoveOrCopy
		{
			get
			{
				foreach (DeferredActionWorkItem.ActionInfo actionInfo in this.actions)
				{
					if (actionInfo.Action is RuleAction.MoveCopy)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17001D44 RID: 7492
		// (get) Token: 0x06006BD0 RID: 27600 RVA: 0x001CE27C File Offset: 0x001CC47C
		public string Provider
		{
			get
			{
				return this.provider;
			}
		}

		// Token: 0x17001D45 RID: 7493
		// (get) Token: 0x06006BD1 RID: 27601 RVA: 0x001CE284 File Offset: 0x001CC484
		public override ExecutionStage Stage
		{
			get
			{
				return ExecutionStage.OnCreatedMessage | ExecutionStage.OnDeliveredMessage;
			}
		}

		// Token: 0x17001D46 RID: 7494
		// (get) Token: 0x06006BD2 RID: 27602 RVA: 0x001CE287 File Offset: 0x001CC487
		// (set) Token: 0x06006BD3 RID: 27603 RVA: 0x001CE28F File Offset: 0x001CC48F
		public byte[] MoveToFolderEntryId
		{
			get
			{
				return this.moveToFolderEntryId;
			}
			set
			{
				this.moveToFolderEntryId = value;
			}
		}

		// Token: 0x17001D47 RID: 7495
		// (get) Token: 0x06006BD4 RID: 27604 RVA: 0x001CE298 File Offset: 0x001CC498
		// (set) Token: 0x06006BD5 RID: 27605 RVA: 0x001CE2A0 File Offset: 0x001CC4A0
		public byte[] MoveToStoreEntryId
		{
			get
			{
				return this.moveToStoreEntryId;
			}
			set
			{
				this.moveToStoreEntryId = value;
			}
		}

		// Token: 0x06006BD6 RID: 27606 RVA: 0x001CE2A9 File Offset: 0x001CC4A9
		public void AddAction(RuleAction action)
		{
			this.actions.Add(new DeferredActionWorkItem.ActionInfo(action, base.Rule));
		}

		// Token: 0x06006BD7 RID: 27607 RVA: 0x001CE2C4 File Offset: 0x001CC4C4
		public override void Execute()
		{
			if (base.Context.DeliveredMessage == null)
			{
				base.Context.TraceDebug("Deferred action: Message was not delivered, deferred actions will be ignored.");
				return;
			}
			if (base.Context.ExecutionStage == ExecutionStage.OnDeliveredMessage)
			{
				this.CreateDAM();
				return;
			}
			if (base.Context.ExecutionStage == ExecutionStage.OnCreatedMessage)
			{
				this.UpdateDeliveredMessage();
			}
		}

		// Token: 0x06006BD8 RID: 27608 RVA: 0x001CE318 File Offset: 0x001CC518
		private void CreateDAM()
		{
			MailboxSession mailboxSession = base.Context.StoreSession as MailboxSession;
			base.Context.TraceDebug("Deferred action: Creating deferred action message.");
			using (DeferredAction deferredAction = RuleMessageUtils.CreateDAM(mailboxSession, this.folder.Id.ObjectId, this.provider))
			{
				foreach (DeferredActionWorkItem.ActionInfo actionInfo in this.actions)
				{
					base.Context.TraceDebug<RuleAction>("Deferred action: Adding deferred action {0}.", actionInfo.Action);
					deferredAction.AddAction(actionInfo.RuleId, actionInfo.Action);
				}
				StoreId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
				bool flag = RuleUtil.EqualsStoreId(defaultFolderId, base.Context.FinalDeliveryFolderId);
				deferredAction.Message[ItemSchema.DeferredActionMessageBackPatched] = !flag;
				base.Context.DeliveredMessage.Load(DeferredError.EntryId);
				byte[] value = (byte[])base.Context.DeliveredMessage[StoreObjectSchema.EntryId];
				deferredAction.Message[ItemSchema.OriginalMessageEntryId] = value;
				base.Context.TraceDebug("Deferred action: Saving deferred action message.");
				deferredAction.SerializeActionsAndSave();
				base.Context.TraceDebug("Deferred action: Deferred action message saved.");
			}
		}

		// Token: 0x06006BD9 RID: 27609 RVA: 0x001CE484 File Offset: 0x001CC684
		private void UpdateDeliveredMessage()
		{
			base.Context.TraceDebug("Deferred action: Updating delivered message.");
			MessageItem deliveredMessage = base.Context.DeliveredMessage;
			if (!RuleUtil.IsNullOrEmpty(this.MoveToFolderEntryId) && !RuleUtil.IsNullOrEmpty(this.MoveToStoreEntryId))
			{
				deliveredMessage[ItemSchema.MoveToStoreEntryId] = this.MoveToStoreEntryId;
				deliveredMessage[ItemSchema.MoveToFolderEntryId] = this.MoveToFolderEntryId;
			}
			deliveredMessage[ItemSchema.HasDeferredActionMessage] = true;
		}

		// Token: 0x04003DAB RID: 15787
		private List<DeferredActionWorkItem.ActionInfo> actions;

		// Token: 0x04003DAC RID: 15788
		private string provider;

		// Token: 0x04003DAD RID: 15789
		private byte[] moveToStoreEntryId;

		// Token: 0x04003DAE RID: 15790
		private byte[] moveToFolderEntryId;

		// Token: 0x04003DAF RID: 15791
		private Folder folder;

		// Token: 0x02000BDB RID: 3035
		private struct ActionInfo
		{
			// Token: 0x06006BDA RID: 27610 RVA: 0x001CE4FA File Offset: 0x001CC6FA
			public ActionInfo(RuleAction action, Rule rule)
			{
				this.action = action;
				this.ruleId = rule.ID;
			}

			// Token: 0x17001D48 RID: 7496
			// (get) Token: 0x06006BDB RID: 27611 RVA: 0x001CE50F File Offset: 0x001CC70F
			public RuleAction Action
			{
				get
				{
					return this.action;
				}
			}

			// Token: 0x17001D49 RID: 7497
			// (get) Token: 0x06006BDC RID: 27612 RVA: 0x001CE517 File Offset: 0x001CC717
			public long RuleId
			{
				get
				{
					return this.ruleId;
				}
			}

			// Token: 0x04003DB0 RID: 15792
			private RuleAction action;

			// Token: 0x04003DB1 RID: 15793
			private long ruleId;
		}
	}
}
