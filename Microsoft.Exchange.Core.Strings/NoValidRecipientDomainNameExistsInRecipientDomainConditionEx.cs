using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000019 RID: 25
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoValidRecipientDomainNameExistsInRecipientDomainConditionException : LocalizedException
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x0000D08E File Offset: 0x0000B28E
		public NoValidRecipientDomainNameExistsInRecipientDomainConditionException(int ruleId) : base(CoreStrings.NoValidRecipientDomainNameExistsInRecipientDomainCondition(ruleId))
		{
			this.ruleId = ruleId;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000D0A3 File Offset: 0x0000B2A3
		public NoValidRecipientDomainNameExistsInRecipientDomainConditionException(int ruleId, Exception innerException) : base(CoreStrings.NoValidRecipientDomainNameExistsInRecipientDomainCondition(ruleId), innerException)
		{
			this.ruleId = ruleId;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000D0B9 File Offset: 0x0000B2B9
		protected NoValidRecipientDomainNameExistsInRecipientDomainConditionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000D0E3 File Offset: 0x0000B2E3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000D0FE File Offset: 0x0000B2FE
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x04000364 RID: 868
		private readonly int ruleId;
	}
}
