using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000156 RID: 342
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ComplianceRuleAlreadyExistsException : LocalizedException
	{
		// Token: 0x06000EB2 RID: 3762 RVA: 0x000354DC File Offset: 0x000336DC
		public ComplianceRuleAlreadyExistsException(string ruleName) : base(Strings.ComplianceRuleAlreadyExists(ruleName))
		{
			this.ruleName = ruleName;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x000354F1 File Offset: 0x000336F1
		public ComplianceRuleAlreadyExistsException(string ruleName, Exception innerException) : base(Strings.ComplianceRuleAlreadyExists(ruleName), innerException)
		{
			this.ruleName = ruleName;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00035507 File Offset: 0x00033707
		protected ComplianceRuleAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleName = (string)info.GetValue("ruleName", typeof(string));
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00035531 File Offset: 0x00033731
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleName", this.ruleName);
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0003554C File Offset: 0x0003374C
		public string RuleName
		{
			get
			{
				return this.ruleName;
			}
		}

		// Token: 0x04000666 RID: 1638
		private readonly string ruleName;
	}
}
