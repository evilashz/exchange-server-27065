using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x0200000F RID: 15
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FopePolicyRuleIsSkippableAntiSpamRuleException : LocalizedException
	{
		// Token: 0x06000367 RID: 871 RVA: 0x0000C945 File Offset: 0x0000AB45
		public FopePolicyRuleIsSkippableAntiSpamRuleException(int ruleId) : base(CoreStrings.FopePolicyRuleIsSkippableAntiSpamRule(ruleId))
		{
			this.ruleId = ruleId;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000C95A File Offset: 0x0000AB5A
		public FopePolicyRuleIsSkippableAntiSpamRuleException(int ruleId, Exception innerException) : base(CoreStrings.FopePolicyRuleIsSkippableAntiSpamRule(ruleId), innerException)
		{
			this.ruleId = ruleId;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000C970 File Offset: 0x0000AB70
		protected FopePolicyRuleIsSkippableAntiSpamRuleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000C99A File Offset: 0x0000AB9A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000C9B5 File Offset: 0x0000ABB5
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x04000352 RID: 850
		private readonly int ruleId;
	}
}
