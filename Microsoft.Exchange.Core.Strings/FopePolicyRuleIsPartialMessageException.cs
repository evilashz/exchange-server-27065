using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x0200000C RID: 12
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FopePolicyRuleIsPartialMessageException : LocalizedException
	{
		// Token: 0x06000356 RID: 854 RVA: 0x0000C733 File Offset: 0x0000A933
		public FopePolicyRuleIsPartialMessageException(int ruleId) : base(CoreStrings.FopePolicyRuleIsPartialMessage(ruleId))
		{
			this.ruleId = ruleId;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000C748 File Offset: 0x0000A948
		public FopePolicyRuleIsPartialMessageException(int ruleId, Exception innerException) : base(CoreStrings.FopePolicyRuleIsPartialMessage(ruleId), innerException)
		{
			this.ruleId = ruleId;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000C75E File Offset: 0x0000A95E
		protected FopePolicyRuleIsPartialMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000C788 File Offset: 0x0000A988
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000C7A3 File Offset: 0x0000A9A3
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x0400034D RID: 845
		private readonly int ruleId;
	}
}
