using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x0200000D RID: 13
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FopePolicyRuleExpiredException : LocalizedException
	{
		// Token: 0x0600035B RID: 859 RVA: 0x0000C7AB File Offset: 0x0000A9AB
		public FopePolicyRuleExpiredException(int ruleId, DateTime expiredOn) : base(CoreStrings.FopePolicyRuleExpired(ruleId, expiredOn))
		{
			this.ruleId = ruleId;
			this.expiredOn = expiredOn;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		public FopePolicyRuleExpiredException(int ruleId, DateTime expiredOn, Exception innerException) : base(CoreStrings.FopePolicyRuleExpired(ruleId, expiredOn), innerException)
		{
			this.ruleId = ruleId;
			this.expiredOn = expiredOn;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		protected FopePolicyRuleExpiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
			this.expiredOn = (DateTime)info.GetValue("expiredOn", typeof(DateTime));
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000C83D File Offset: 0x0000AA3D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
			info.AddValue("expiredOn", this.expiredOn);
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000C869 File Offset: 0x0000AA69
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000C871 File Offset: 0x0000AA71
		public DateTime ExpiredOn
		{
			get
			{
				return this.expiredOn;
			}
		}

		// Token: 0x0400034E RID: 846
		private readonly int ruleId;

		// Token: 0x0400034F RID: 847
		private readonly DateTime expiredOn;
	}
}
