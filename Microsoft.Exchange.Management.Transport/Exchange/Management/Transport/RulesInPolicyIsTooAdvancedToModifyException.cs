using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200017B RID: 379
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RulesInPolicyIsTooAdvancedToModifyException : TaskObjectIsTooAdvancedException
	{
		// Token: 0x06000F60 RID: 3936 RVA: 0x00036302 File Offset: 0x00034502
		public RulesInPolicyIsTooAdvancedToModifyException(string policy, string rule) : base(Strings.ErrorRulesInPolicyIsTooAdvancedToModify(policy, rule))
		{
			this.policy = policy;
			this.rule = rule;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0003631F File Offset: 0x0003451F
		public RulesInPolicyIsTooAdvancedToModifyException(string policy, string rule, Exception innerException) : base(Strings.ErrorRulesInPolicyIsTooAdvancedToModify(policy, rule), innerException)
		{
			this.policy = policy;
			this.rule = rule;
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x00036340 File Offset: 0x00034540
		protected RulesInPolicyIsTooAdvancedToModifyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.policy = (string)info.GetValue("policy", typeof(string));
			this.rule = (string)info.GetValue("rule", typeof(string));
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00036395 File Offset: 0x00034595
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("policy", this.policy);
			info.AddValue("rule", this.rule);
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x000363C1 File Offset: 0x000345C1
		public string Policy
		{
			get
			{
				return this.policy;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x000363C9 File Offset: 0x000345C9
		public string Rule
		{
			get
			{
				return this.rule;
			}
		}

		// Token: 0x04000680 RID: 1664
		private readonly string policy;

		// Token: 0x04000681 RID: 1665
		private readonly string rule;
	}
}
