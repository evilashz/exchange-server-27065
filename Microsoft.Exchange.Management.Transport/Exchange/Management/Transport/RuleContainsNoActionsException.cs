using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200016D RID: 365
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RuleContainsNoActionsException : InvalidComplianceRuleActionException
	{
		// Token: 0x06000F1D RID: 3869 RVA: 0x00035D48 File Offset: 0x00033F48
		public RuleContainsNoActionsException(string ruleName) : base(Strings.ErrorRuleContainsNoActions(ruleName))
		{
			this.ruleName = ruleName;
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00035D5D File Offset: 0x00033F5D
		public RuleContainsNoActionsException(string ruleName, Exception innerException) : base(Strings.ErrorRuleContainsNoActions(ruleName), innerException)
		{
			this.ruleName = ruleName;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00035D73 File Offset: 0x00033F73
		protected RuleContainsNoActionsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleName = (string)info.GetValue("ruleName", typeof(string));
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00035D9D File Offset: 0x00033F9D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleName", this.ruleName);
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00035DB8 File Offset: 0x00033FB8
		public string RuleName
		{
			get
			{
				return this.ruleName;
			}
		}

		// Token: 0x04000675 RID: 1653
		private readonly string ruleName;
	}
}
