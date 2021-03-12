using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000016 RID: 22
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FopePolicyRuleContainsIncompatibleConditionsException : LocalizedException
	{
		// Token: 0x06000392 RID: 914 RVA: 0x0000CF26 File Offset: 0x0000B126
		public FopePolicyRuleContainsIncompatibleConditionsException(int ruleId) : base(CoreStrings.FopePolicyRuleContainsIncompatibleConditions(ruleId))
		{
			this.ruleId = ruleId;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000CF3B File Offset: 0x0000B13B
		public FopePolicyRuleContainsIncompatibleConditionsException(int ruleId, Exception innerException) : base(CoreStrings.FopePolicyRuleContainsIncompatibleConditions(ruleId), innerException)
		{
			this.ruleId = ruleId;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000CF51 File Offset: 0x0000B151
		protected FopePolicyRuleContainsIncompatibleConditionsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000CF7B File Offset: 0x0000B17B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000CF96 File Offset: 0x0000B196
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x04000361 RID: 865
		private readonly int ruleId;
	}
}
