using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x0200000A RID: 10
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InboundFopePolicyRuleWithDuplicateDomainNameException : LocalizedException
	{
		// Token: 0x0600034C RID: 844 RVA: 0x0000C643 File Offset: 0x0000A843
		public InboundFopePolicyRuleWithDuplicateDomainNameException(int ruleId) : base(CoreStrings.InboundFopePolicyRuleWithDuplicateDomainName(ruleId))
		{
			this.ruleId = ruleId;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000C658 File Offset: 0x0000A858
		public InboundFopePolicyRuleWithDuplicateDomainNameException(int ruleId, Exception innerException) : base(CoreStrings.InboundFopePolicyRuleWithDuplicateDomainName(ruleId), innerException)
		{
			this.ruleId = ruleId;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000C66E File Offset: 0x0000A86E
		protected InboundFopePolicyRuleWithDuplicateDomainNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000C698 File Offset: 0x0000A898
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000C6B3 File Offset: 0x0000A8B3
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x0400034B RID: 843
		private readonly int ruleId;
	}
}
