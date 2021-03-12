using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x0200000B RID: 11
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FopePolicyRuleDisabledException : LocalizedException
	{
		// Token: 0x06000351 RID: 849 RVA: 0x0000C6BB File Offset: 0x0000A8BB
		public FopePolicyRuleDisabledException(int ruleId) : base(CoreStrings.FopePolicyRuleDisabled(ruleId))
		{
			this.ruleId = ruleId;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		public FopePolicyRuleDisabledException(int ruleId, Exception innerException) : base(CoreStrings.FopePolicyRuleDisabled(ruleId), innerException)
		{
			this.ruleId = ruleId;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000C6E6 File Offset: 0x0000A8E6
		protected FopePolicyRuleDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000C710 File Offset: 0x0000A910
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000C72B File Offset: 0x0000A92B
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x0400034C RID: 844
		private readonly int ruleId;
	}
}
