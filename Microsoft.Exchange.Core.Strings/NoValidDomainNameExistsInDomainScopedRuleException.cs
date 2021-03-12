using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x0200001A RID: 26
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoValidDomainNameExistsInDomainScopedRuleException : LocalizedException
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x0000D106 File Offset: 0x0000B306
		public NoValidDomainNameExistsInDomainScopedRuleException(int ruleId) : base(CoreStrings.NoValidDomainNameExistsInDomainScopedRule(ruleId))
		{
			this.ruleId = ruleId;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000D11B File Offset: 0x0000B31B
		public NoValidDomainNameExistsInDomainScopedRuleException(int ruleId, Exception innerException) : base(CoreStrings.NoValidDomainNameExistsInDomainScopedRule(ruleId), innerException)
		{
			this.ruleId = ruleId;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000D131 File Offset: 0x0000B331
		protected NoValidDomainNameExistsInDomainScopedRuleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000D15B File Offset: 0x0000B35B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000D176 File Offset: 0x0000B376
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x04000365 RID: 869
		private readonly int ruleId;
	}
}
