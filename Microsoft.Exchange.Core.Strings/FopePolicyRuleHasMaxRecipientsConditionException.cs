using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x0200000E RID: 14
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FopePolicyRuleHasMaxRecipientsConditionException : LocalizedException
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000C879 File Offset: 0x0000AA79
		public FopePolicyRuleHasMaxRecipientsConditionException(int ruleId, int maxRecipients) : base(CoreStrings.FopePolicyRuleHasMaxRecipientsCondition(ruleId, maxRecipients))
		{
			this.ruleId = ruleId;
			this.maxRecipients = maxRecipients;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000C896 File Offset: 0x0000AA96
		public FopePolicyRuleHasMaxRecipientsConditionException(int ruleId, int maxRecipients, Exception innerException) : base(CoreStrings.FopePolicyRuleHasMaxRecipientsCondition(ruleId, maxRecipients), innerException)
		{
			this.ruleId = ruleId;
			this.maxRecipients = maxRecipients;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		protected FopePolicyRuleHasMaxRecipientsConditionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
			this.maxRecipients = (int)info.GetValue("maxRecipients", typeof(int));
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000C909 File Offset: 0x0000AB09
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
			info.AddValue("maxRecipients", this.maxRecipients);
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000C935 File Offset: 0x0000AB35
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000C93D File Offset: 0x0000AB3D
		public int MaxRecipients
		{
			get
			{
				return this.maxRecipients;
			}
		}

		// Token: 0x04000350 RID: 848
		private readonly int ruleId;

		// Token: 0x04000351 RID: 849
		private readonly int maxRecipients;
	}
}
