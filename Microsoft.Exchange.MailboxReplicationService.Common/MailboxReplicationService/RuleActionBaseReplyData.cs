using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000068 RID: 104
	[DataContract]
	internal abstract class RuleActionBaseReplyData : RuleActionData
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x00009791 File Offset: 0x00007991
		public RuleActionBaseReplyData()
		{
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00009799 File Offset: 0x00007999
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x000097A1 File Offset: 0x000079A1
		[DataMember(EmitDefaultValue = false)]
		public byte[] ReplyTemplateMessageEntryID { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000097AA File Offset: 0x000079AA
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x000097B2 File Offset: 0x000079B2
		[DataMember(EmitDefaultValue = false)]
		public Guid ReplyTemplateGuid { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000097BB File Offset: 0x000079BB
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x000097C3 File Offset: 0x000079C3
		[DataMember(EmitDefaultValue = false)]
		public uint Flags { get; set; }

		// Token: 0x06000505 RID: 1285 RVA: 0x000097CC File Offset: 0x000079CC
		public RuleActionBaseReplyData(RuleAction.BaseReply ruleAction, byte[] replyTemplateMessageEntryID, Guid replyTemplateGuid, uint flags) : base(ruleAction)
		{
			this.ReplyTemplateMessageEntryID = replyTemplateMessageEntryID;
			this.ReplyTemplateGuid = replyTemplateGuid;
			this.Flags = flags;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000097EC File Offset: 0x000079EC
		protected override void EnumPropValuesInternal(CommonUtils.EnumPropValueDelegate del)
		{
			base.EnumPropValuesInternal(del);
			PropValueData propValueData = new PropValueData(PropTag.ReplyTemplateID, this.ReplyTemplateMessageEntryID);
			del(propValueData);
			this.ReplyTemplateMessageEntryID = (byte[])propValueData.Value;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00009829 File Offset: 0x00007A29
		protected override string ToStringInternal()
		{
			return string.Format("TemplateEID:{0}, TemplateGuid:{1}, Flags:{2}", TraceUtils.DumpEntryId(this.ReplyTemplateMessageEntryID), this.ReplyTemplateGuid, this.Flags);
		}
	}
}
