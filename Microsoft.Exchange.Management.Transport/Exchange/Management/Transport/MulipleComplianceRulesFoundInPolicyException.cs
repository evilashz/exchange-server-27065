using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200015C RID: 348
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MulipleComplianceRulesFoundInPolicyException : LocalizedException
	{
		// Token: 0x06000ED1 RID: 3793 RVA: 0x00035801 File Offset: 0x00033A01
		public MulipleComplianceRulesFoundInPolicyException(string policy) : base(Strings.MulipleComplianceRulesFoundInPolicy(policy))
		{
			this.policy = policy;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00035816 File Offset: 0x00033A16
		public MulipleComplianceRulesFoundInPolicyException(string policy, Exception innerException) : base(Strings.MulipleComplianceRulesFoundInPolicy(policy), innerException)
		{
			this.policy = policy;
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0003582C File Offset: 0x00033A2C
		protected MulipleComplianceRulesFoundInPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.policy = (string)info.GetValue("policy", typeof(string));
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00035856 File Offset: 0x00033A56
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("policy", this.policy);
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00035871 File Offset: 0x00033A71
		public string Policy
		{
			get
			{
				return this.policy;
			}
		}

		// Token: 0x0400066D RID: 1645
		private readonly string policy;
	}
}
