using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000171 RID: 369
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PolicyAndIdentityParameterUsedTogetherException : LocalizedException
	{
		// Token: 0x06000F30 RID: 3888 RVA: 0x00035EDF File Offset: 0x000340DF
		public PolicyAndIdentityParameterUsedTogetherException(string policy, string identity) : base(Strings.PolicyAndIdentityParameterUsedTogether(policy, identity))
		{
			this.policy = policy;
			this.identity = identity;
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00035EFC File Offset: 0x000340FC
		public PolicyAndIdentityParameterUsedTogetherException(string policy, string identity, Exception innerException) : base(Strings.PolicyAndIdentityParameterUsedTogether(policy, identity), innerException)
		{
			this.policy = policy;
			this.identity = identity;
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00035F1C File Offset: 0x0003411C
		protected PolicyAndIdentityParameterUsedTogetherException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.policy = (string)info.GetValue("policy", typeof(string));
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00035F71 File Offset: 0x00034171
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("policy", this.policy);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00035F9D File Offset: 0x0003419D
		public string Policy
		{
			get
			{
				return this.policy;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00035FA5 File Offset: 0x000341A5
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04000678 RID: 1656
		private readonly string policy;

		// Token: 0x04000679 RID: 1657
		private readonly string identity;
	}
}
