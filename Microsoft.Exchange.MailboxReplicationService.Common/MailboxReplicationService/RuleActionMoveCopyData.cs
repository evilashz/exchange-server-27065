using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200006A RID: 106
	[DataContract]
	internal abstract class RuleActionMoveCopyData : RuleActionData
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x000098A8 File Offset: 0x00007AA8
		public RuleActionMoveCopyData()
		{
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x000098B0 File Offset: 0x00007AB0
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x000098B8 File Offset: 0x00007AB8
		[DataMember(EmitDefaultValue = false)]
		public byte[] FolderEntryID { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x000098C1 File Offset: 0x00007AC1
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x000098C9 File Offset: 0x00007AC9
		[DataMember(EmitDefaultValue = false)]
		public byte[] StoreEntryID { get; set; }

		// Token: 0x06000513 RID: 1299 RVA: 0x000098D2 File Offset: 0x00007AD2
		public RuleActionMoveCopyData(RuleAction.MoveCopy ruleAction) : base(ruleAction)
		{
			this.FolderEntryID = ruleAction.FolderEntryID;
			this.StoreEntryID = ruleAction.StoreEntryID;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000098F4 File Offset: 0x00007AF4
		public static RuleActionMoveCopyData ConvertToUplevel(RuleActionMoveCopyData downlevelRA, bool folderIsLocal)
		{
			RuleActionMoveData ruleActionMoveData = downlevelRA as RuleActionMoveData;
			if (ruleActionMoveData != null)
			{
				if (folderIsLocal)
				{
					return new RuleActionInMailboxMoveData
					{
						FolderEntryID = ruleActionMoveData.FolderEntryID
					};
				}
				return new RuleActionExternalMoveData
				{
					FolderEntryID = ruleActionMoveData.FolderEntryID,
					StoreEntryID = ruleActionMoveData.StoreEntryID
				};
			}
			else
			{
				RuleActionCopyData ruleActionCopyData = downlevelRA as RuleActionCopyData;
				if (folderIsLocal)
				{
					return new RuleActionInMailboxCopyData
					{
						FolderEntryID = ruleActionCopyData.FolderEntryID
					};
				}
				return new RuleActionExternalCopyData
				{
					FolderEntryID = ruleActionCopyData.FolderEntryID,
					StoreEntryID = ruleActionCopyData.StoreEntryID
				};
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00009988 File Offset: 0x00007B88
		public static RuleActionMoveCopyData ConvertToDownlevel(RuleActionMoveCopyData uplevelRA)
		{
			RuleActionInMailboxMoveCopyData ruleActionInMailboxMoveCopyData = uplevelRA as RuleActionInMailboxMoveCopyData;
			if (ruleActionInMailboxMoveCopyData != null)
			{
				RuleActionInMailboxMoveData ruleActionInMailboxMoveData = ruleActionInMailboxMoveCopyData as RuleActionInMailboxMoveData;
				if (ruleActionInMailboxMoveData != null)
				{
					return new RuleActionMoveData
					{
						FolderEntryID = ruleActionInMailboxMoveData.FolderEntryID,
						StoreEntryID = RuleAction.MoveCopy.InThisStoreBytes
					};
				}
				RuleActionInMailboxCopyData ruleActionInMailboxCopyData = ruleActionInMailboxMoveCopyData as RuleActionInMailboxCopyData;
				return new RuleActionCopyData
				{
					FolderEntryID = ruleActionInMailboxCopyData.FolderEntryID,
					StoreEntryID = RuleAction.MoveCopy.InThisStoreBytes
				};
			}
			else
			{
				RuleActionExternalMoveCopyData ruleActionExternalMoveCopyData = uplevelRA as RuleActionExternalMoveCopyData;
				RuleActionExternalMoveData ruleActionExternalMoveData = ruleActionExternalMoveCopyData as RuleActionExternalMoveData;
				if (ruleActionExternalMoveData != null)
				{
					return new RuleActionMoveData
					{
						FolderEntryID = ruleActionExternalMoveData.FolderEntryID,
						StoreEntryID = ruleActionExternalMoveData.StoreEntryID
					};
				}
				RuleActionExternalCopyData ruleActionExternalCopyData = ruleActionExternalMoveCopyData as RuleActionExternalCopyData;
				return new RuleActionCopyData
				{
					FolderEntryID = ruleActionExternalCopyData.FolderEntryID,
					StoreEntryID = ruleActionExternalCopyData.StoreEntryID
				};
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00009A5C File Offset: 0x00007C5C
		protected override void EnumPropValuesInternal(CommonUtils.EnumPropValueDelegate del)
		{
			base.EnumPropValuesInternal(del);
			PropValueData propValueData = new PropValueData(PropTag.RuleFolderEntryID, this.FolderEntryID);
			del(propValueData);
			this.FolderEntryID = (byte[])propValueData.Value;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00009A99 File Offset: 0x00007C99
		protected override string ToStringInternal()
		{
			return string.Format("FolderEID:{0}, StoreEID:{1}", TraceUtils.DumpEntryId(this.FolderEntryID), TraceUtils.DumpEntryId(this.StoreEntryID));
		}
	}
}
