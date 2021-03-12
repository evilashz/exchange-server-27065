using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000013 RID: 19
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FopePolicyRuleHasProhibitedRegularExpressionsException : LocalizedException
	{
		// Token: 0x06000380 RID: 896 RVA: 0x0000CCC9 File Offset: 0x0000AEC9
		public FopePolicyRuleHasProhibitedRegularExpressionsException(int ruleId, string reason) : base(CoreStrings.FopePolicyRuleHasProhibitedRegularExpressions(ruleId, reason))
		{
			this.ruleId = ruleId;
			this.reason = reason;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000CCE6 File Offset: 0x0000AEE6
		public FopePolicyRuleHasProhibitedRegularExpressionsException(int ruleId, string reason, Exception innerException) : base(CoreStrings.FopePolicyRuleHasProhibitedRegularExpressions(ruleId, reason), innerException)
		{
			this.ruleId = ruleId;
			this.reason = reason;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000CD04 File Offset: 0x0000AF04
		protected FopePolicyRuleHasProhibitedRegularExpressionsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000CD85 File Offset: 0x0000AF85
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000CD8D File Offset: 0x0000AF8D
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x0400035B RID: 859
		private readonly int ruleId;

		// Token: 0x0400035C RID: 860
		private readonly string reason;
	}
}
