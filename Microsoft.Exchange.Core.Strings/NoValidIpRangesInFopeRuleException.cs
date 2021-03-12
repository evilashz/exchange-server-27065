using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000017 RID: 23
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoValidIpRangesInFopeRuleException : LocalizedException
	{
		// Token: 0x06000397 RID: 919 RVA: 0x0000CF9E File Offset: 0x0000B19E
		public NoValidIpRangesInFopeRuleException(int ruleId) : base(CoreStrings.NoValidIpRangesInFopeRule(ruleId))
		{
			this.ruleId = ruleId;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000CFB3 File Offset: 0x0000B1B3
		public NoValidIpRangesInFopeRuleException(int ruleId, Exception innerException) : base(CoreStrings.NoValidIpRangesInFopeRule(ruleId), innerException)
		{
			this.ruleId = ruleId;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000CFC9 File Offset: 0x0000B1C9
		protected NoValidIpRangesInFopeRuleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000CFF3 File Offset: 0x0000B1F3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000D00E File Offset: 0x0000B20E
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x04000362 RID: 866
		private readonly int ruleId;
	}
}
