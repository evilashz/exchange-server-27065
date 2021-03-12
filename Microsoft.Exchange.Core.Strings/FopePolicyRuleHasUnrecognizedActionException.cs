using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000010 RID: 16
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FopePolicyRuleHasUnrecognizedActionException : LocalizedException
	{
		// Token: 0x0600036C RID: 876 RVA: 0x0000C9BD File Offset: 0x0000ABBD
		public FopePolicyRuleHasUnrecognizedActionException(int ruleId, int actionId) : base(CoreStrings.FopePolicyRuleHasUnrecognizedAction(ruleId, actionId))
		{
			this.ruleId = ruleId;
			this.actionId = actionId;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000C9DA File Offset: 0x0000ABDA
		public FopePolicyRuleHasUnrecognizedActionException(int ruleId, int actionId, Exception innerException) : base(CoreStrings.FopePolicyRuleHasUnrecognizedAction(ruleId, actionId), innerException)
		{
			this.ruleId = ruleId;
			this.actionId = actionId;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		protected FopePolicyRuleHasUnrecognizedActionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
			this.actionId = (int)info.GetValue("actionId", typeof(int));
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000CA4D File Offset: 0x0000AC4D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
			info.AddValue("actionId", this.actionId);
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000CA79 File Offset: 0x0000AC79
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000CA81 File Offset: 0x0000AC81
		public int ActionId
		{
			get
			{
				return this.actionId;
			}
		}

		// Token: 0x04000353 RID: 851
		private readonly int ruleId;

		// Token: 0x04000354 RID: 852
		private readonly int actionId;
	}
}
