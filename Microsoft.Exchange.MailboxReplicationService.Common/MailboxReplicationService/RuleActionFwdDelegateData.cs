using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200006D RID: 109
	[DataContract]
	internal abstract class RuleActionFwdDelegateData : RuleActionData
	{
		// Token: 0x06000522 RID: 1314 RVA: 0x00009B44 File Offset: 0x00007D44
		public RuleActionFwdDelegateData()
		{
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00009B4C File Offset: 0x00007D4C
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00009B54 File Offset: 0x00007D54
		[DataMember(EmitDefaultValue = false)]
		public AdrEntryData[] Recipients { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00009B5D File Offset: 0x00007D5D
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00009B65 File Offset: 0x00007D65
		[DataMember(EmitDefaultValue = false)]
		public uint Flags { get; set; }

		// Token: 0x06000527 RID: 1319 RVA: 0x00009B6E File Offset: 0x00007D6E
		public RuleActionFwdDelegateData(RuleAction.FwdDelegate ruleAction, uint flags) : base(ruleAction)
		{
			this.Recipients = DataConverter<AdrEntryConverter, AdrEntry, AdrEntryData>.GetData(ruleAction.Recipients);
			this.Flags = flags;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00009B90 File Offset: 0x00007D90
		protected override void EnumPropTagsInternal(CommonUtils.EnumPropTagDelegate del)
		{
			foreach (AdrEntryData adrEntryData in this.Recipients)
			{
				foreach (PropValueData propValueData in adrEntryData.Values)
				{
					int propTag = propValueData.PropTag;
					del(ref propTag);
					propValueData.PropTag = propTag;
				}
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00009BF4 File Offset: 0x00007DF4
		protected override void EnumAdrEntriesInternal(CommonUtils.EnumAdrEntryDelegate del)
		{
			base.EnumAdrEntriesInternal(del);
			foreach (AdrEntryData aed in this.Recipients)
			{
				del(aed);
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00009C28 File Offset: 0x00007E28
		protected override string ToStringInternal()
		{
			return string.Format("{0}, Flags:{1}", CommonUtils.ConcatEntries<AdrEntryData>(this.Recipients, null), this.Flags);
		}
	}
}
