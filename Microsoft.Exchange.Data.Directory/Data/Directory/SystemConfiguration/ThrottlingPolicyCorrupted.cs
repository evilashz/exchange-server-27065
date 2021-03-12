using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000AA8 RID: 2728
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ThrottlingPolicyCorrupted : ADExternalException
	{
		// Token: 0x06008010 RID: 32784 RVA: 0x001A4CF0 File Offset: 0x001A2EF0
		public ThrottlingPolicyCorrupted(string policyId) : base(DirectoryStrings.ThrottlingPolicyCorrupted(policyId))
		{
			this.policyId = policyId;
		}

		// Token: 0x06008011 RID: 32785 RVA: 0x001A4D05 File Offset: 0x001A2F05
		public ThrottlingPolicyCorrupted(string policyId, Exception innerException) : base(DirectoryStrings.ThrottlingPolicyCorrupted(policyId), innerException)
		{
			this.policyId = policyId;
		}

		// Token: 0x06008012 RID: 32786 RVA: 0x001A4D1B File Offset: 0x001A2F1B
		protected ThrottlingPolicyCorrupted(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.policyId = (string)info.GetValue("policyId", typeof(string));
		}

		// Token: 0x06008013 RID: 32787 RVA: 0x001A4D45 File Offset: 0x001A2F45
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("policyId", this.policyId);
		}

		// Token: 0x17002EC7 RID: 11975
		// (get) Token: 0x06008014 RID: 32788 RVA: 0x001A4D60 File Offset: 0x001A2F60
		public string PolicyId
		{
			get
			{
				return this.policyId;
			}
		}

		// Token: 0x040055A1 RID: 21921
		private readonly string policyId;
	}
}
