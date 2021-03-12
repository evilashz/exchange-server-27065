using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000015 RID: 21
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FopePolicyRuleContainsInvalidPatternException : LocalizedException
	{
		// Token: 0x0600038D RID: 909 RVA: 0x0000CEAE File Offset: 0x0000B0AE
		public FopePolicyRuleContainsInvalidPatternException(string ruleName) : base(CoreStrings.FopePolicyRuleContainsInvalidPattern(ruleName))
		{
			this.ruleName = ruleName;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000CEC3 File Offset: 0x0000B0C3
		public FopePolicyRuleContainsInvalidPatternException(string ruleName, Exception innerException) : base(CoreStrings.FopePolicyRuleContainsInvalidPattern(ruleName), innerException)
		{
			this.ruleName = ruleName;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000CED9 File Offset: 0x0000B0D9
		protected FopePolicyRuleContainsInvalidPatternException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleName = (string)info.GetValue("ruleName", typeof(string));
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000CF03 File Offset: 0x0000B103
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleName", this.ruleName);
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000CF1E File Offset: 0x0000B11E
		public string RuleName
		{
			get
			{
				return this.ruleName;
			}
		}

		// Token: 0x04000360 RID: 864
		private readonly string ruleName;
	}
}
