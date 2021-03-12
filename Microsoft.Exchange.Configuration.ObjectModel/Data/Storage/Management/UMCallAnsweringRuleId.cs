using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x0200015B RID: 347
	[Serializable]
	public class UMCallAnsweringRuleId : XsoMailboxObjectId
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x000275AD File Offset: 0x000257AD
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x000275B5 File Offset: 0x000257B5
		public Guid RuleIdGuid { get; private set; }

		// Token: 0x06000C82 RID: 3202 RVA: 0x000275BE File Offset: 0x000257BE
		internal UMCallAnsweringRuleId(ADObjectId mailboxOwnerId, Guid ruleId) : base(mailboxOwnerId)
		{
			this.RuleIdGuid = ruleId;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x000275D0 File Offset: 0x000257D0
		public override int GetHashCode()
		{
			return base.MailboxOwnerId.GetHashCode() ^ this.RuleIdGuid.GetHashCode();
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00027600 File Offset: 0x00025800
		public override bool Equals(XsoMailboxObjectId other)
		{
			UMCallAnsweringRuleId umcallAnsweringRuleId = other as UMCallAnsweringRuleId;
			return !(null == umcallAnsweringRuleId) && ADObjectId.Equals(base.MailboxOwnerId, other.MailboxOwnerId) && object.Equals(this.RuleIdGuid, umcallAnsweringRuleId.RuleIdGuid);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00027650 File Offset: 0x00025850
		public override byte[] GetBytes()
		{
			byte[] bytes = base.MailboxOwnerId.GetBytes();
			byte[] array = new byte[16 + bytes.Length + 2];
			ExBitConverter.Write((short)bytes.Length, array, 0);
			int num = 2;
			Array.Copy(bytes, 0, array, num, bytes.Length);
			num += bytes.Length;
			ExBitConverter.Write(this.RuleIdGuid, array, num);
			return array;
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x000276A6 File Offset: 0x000258A6
		public override string ToString()
		{
			return string.Format("{0}{1}{2}", base.MailboxOwnerId, "\\", this.RuleIdGuid);
		}

		// Token: 0x040002DC RID: 732
		public const string MailboxIdSeparator = "\\";
	}
}
