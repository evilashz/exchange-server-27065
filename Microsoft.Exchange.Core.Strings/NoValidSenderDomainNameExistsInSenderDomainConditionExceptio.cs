using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000018 RID: 24
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoValidSenderDomainNameExistsInSenderDomainConditionException : LocalizedException
	{
		// Token: 0x0600039C RID: 924 RVA: 0x0000D016 File Offset: 0x0000B216
		public NoValidSenderDomainNameExistsInSenderDomainConditionException(int ruleId) : base(CoreStrings.NoValidSenderDomainNameExistsInSenderDomainCondition(ruleId))
		{
			this.ruleId = ruleId;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000D02B File Offset: 0x0000B22B
		public NoValidSenderDomainNameExistsInSenderDomainConditionException(int ruleId, Exception innerException) : base(CoreStrings.NoValidSenderDomainNameExistsInSenderDomainCondition(ruleId), innerException)
		{
			this.ruleId = ruleId;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000D041 File Offset: 0x0000B241
		protected NoValidSenderDomainNameExistsInSenderDomainConditionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000D06B File Offset: 0x0000B26B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000D086 File Offset: 0x0000B286
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x04000363 RID: 867
		private readonly int ruleId;
	}
}
