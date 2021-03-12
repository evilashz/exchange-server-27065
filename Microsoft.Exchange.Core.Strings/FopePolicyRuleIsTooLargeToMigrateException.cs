using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000014 RID: 20
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FopePolicyRuleIsTooLargeToMigrateException : LocalizedException
	{
		// Token: 0x06000386 RID: 902 RVA: 0x0000CD95 File Offset: 0x0000AF95
		public FopePolicyRuleIsTooLargeToMigrateException(string ruleName, ulong ruleSize, ulong maxRuleSize) : base(CoreStrings.FopePolicyRuleIsTooLargeToMigrate(ruleName, ruleSize, maxRuleSize))
		{
			this.ruleName = ruleName;
			this.ruleSize = ruleSize;
			this.maxRuleSize = maxRuleSize;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000CDBA File Offset: 0x0000AFBA
		public FopePolicyRuleIsTooLargeToMigrateException(string ruleName, ulong ruleSize, ulong maxRuleSize, Exception innerException) : base(CoreStrings.FopePolicyRuleIsTooLargeToMigrate(ruleName, ruleSize, maxRuleSize), innerException)
		{
			this.ruleName = ruleName;
			this.ruleSize = ruleSize;
			this.maxRuleSize = maxRuleSize;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		protected FopePolicyRuleIsTooLargeToMigrateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleName = (string)info.GetValue("ruleName", typeof(string));
			this.ruleSize = (ulong)info.GetValue("ruleSize", typeof(ulong));
			this.maxRuleSize = (ulong)info.GetValue("maxRuleSize", typeof(ulong));
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000CE59 File Offset: 0x0000B059
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleName", this.ruleName);
			info.AddValue("ruleSize", this.ruleSize);
			info.AddValue("maxRuleSize", this.maxRuleSize);
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000CE96 File Offset: 0x0000B096
		public string RuleName
		{
			get
			{
				return this.ruleName;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000CE9E File Offset: 0x0000B09E
		public ulong RuleSize
		{
			get
			{
				return this.ruleSize;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000CEA6 File Offset: 0x0000B0A6
		public ulong MaxRuleSize
		{
			get
			{
				return this.maxRuleSize;
			}
		}

		// Token: 0x0400035D RID: 861
		private readonly string ruleName;

		// Token: 0x0400035E RID: 862
		private readonly ulong ruleSize;

		// Token: 0x0400035F RID: 863
		private readonly ulong maxRuleSize;
	}
}
