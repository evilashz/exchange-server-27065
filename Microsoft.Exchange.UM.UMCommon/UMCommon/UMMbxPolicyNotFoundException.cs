using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001E8 RID: 488
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMMbxPolicyNotFoundException : LocalizedException
	{
		// Token: 0x06000FA6 RID: 4006 RVA: 0x00036BA0 File Offset: 0x00034DA0
		public UMMbxPolicyNotFoundException(string policy, string user) : base(Strings.UMMbxPolicyNotFound(policy, user))
		{
			this.policy = policy;
			this.user = user;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00036BBD File Offset: 0x00034DBD
		public UMMbxPolicyNotFoundException(string policy, string user, Exception innerException) : base(Strings.UMMbxPolicyNotFound(policy, user), innerException)
		{
			this.policy = policy;
			this.user = user;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00036BDC File Offset: 0x00034DDC
		protected UMMbxPolicyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.policy = (string)info.GetValue("policy", typeof(string));
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00036C31 File Offset: 0x00034E31
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("policy", this.policy);
			info.AddValue("user", this.user);
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00036C5D File Offset: 0x00034E5D
		public string Policy
		{
			get
			{
				return this.policy;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00036C65 File Offset: 0x00034E65
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x040007B6 RID: 1974
		private readonly string policy;

		// Token: 0x040007B7 RID: 1975
		private readonly string user;
	}
}
