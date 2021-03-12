using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x0200014E RID: 334
	[Serializable]
	public class InboxRuleId : XsoMailboxObjectId
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x000258AA File Offset: 0x00023AAA
		internal RuleId RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x000258B4 File Offset: 0x00023AB4
		internal ulong? StoreObjectId
		{
			get
			{
				if (this.ruleId != null)
				{
					return new ulong?(InboxRuleTaskHelper.GetRuleIdentity(this.ruleId));
				}
				return null;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x000258E3 File Offset: 0x00023AE3
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000258EB File Offset: 0x00023AEB
		internal InboxRuleId(ADObjectId mailboxOwnerId, string name, RuleId ruleId) : base(mailboxOwnerId)
		{
			this.ruleId = ruleId;
			this.name = name;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00025904 File Offset: 0x00023B04
		public override byte[] GetBytes()
		{
			byte[] array = (this.StoreObjectId != null) ? BitConverter.GetBytes(this.StoreObjectId.Value) : new byte[0];
			byte[] bytes = base.MailboxOwnerId.GetBytes();
			byte[] array2 = new byte[array.Length + bytes.Length + 2];
			int num = 0;
			array2[num++] = (byte)(bytes.Length & 255);
			array2[num++] = (byte)(bytes.Length >> 8 & 255);
			Array.Copy(bytes, 0, array2, num, bytes.Length);
			num += bytes.Length;
			Array.Copy(array, 0, array2, num, array.Length);
			return array2;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000259A0 File Offset: 0x00023BA0
		public override int GetHashCode()
		{
			return base.MailboxOwnerId.GetHashCode() ^ ((this.StoreObjectId != null) ? this.StoreObjectId.Value.GetHashCode() : 0);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000259E4 File Offset: 0x00023BE4
		public override bool Equals(XsoMailboxObjectId other)
		{
			InboxRuleId inboxRuleId = other as InboxRuleId;
			return !(null == inboxRuleId) && ADObjectId.Equals(base.MailboxOwnerId, other.MailboxOwnerId) && object.Equals(this.ruleId, inboxRuleId.RuleId);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00025A2C File Offset: 0x00023C2C
		public override string ToString()
		{
			string arg;
			if (this.StoreObjectId != null)
			{
				arg = this.StoreObjectId.Value.ToString();
			}
			else if (!string.IsNullOrEmpty(this.name))
			{
				arg = this.name;
			}
			else
			{
				arg = string.Empty;
			}
			return string.Format("{0}{1}{2}", base.MailboxOwnerId, '\\', arg);
		}

		// Token: 0x040002B4 RID: 692
		public const char MailboxAndRuleSeparator = '\\';

		// Token: 0x040002B5 RID: 693
		public const string RuleNameEscapedSeparator = "\\\\";

		// Token: 0x040002B6 RID: 694
		private RuleId ruleId;

		// Token: 0x040002B7 RID: 695
		private string name;
	}
}
