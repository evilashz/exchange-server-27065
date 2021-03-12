using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F38 RID: 3896
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MultipleDefaultFoldersInPolicyException : LocalizedException
	{
		// Token: 0x0600AB12 RID: 43794 RVA: 0x0028E849 File Offset: 0x0028CA49
		public MultipleDefaultFoldersInPolicyException(string policyName, string userName) : base(Strings.MultipleDefaultFoldersInPolicyException(policyName, userName))
		{
			this.policyName = policyName;
			this.userName = userName;
		}

		// Token: 0x0600AB13 RID: 43795 RVA: 0x0028E866 File Offset: 0x0028CA66
		public MultipleDefaultFoldersInPolicyException(string policyName, string userName, Exception innerException) : base(Strings.MultipleDefaultFoldersInPolicyException(policyName, userName), innerException)
		{
			this.policyName = policyName;
			this.userName = userName;
		}

		// Token: 0x0600AB14 RID: 43796 RVA: 0x0028E884 File Offset: 0x0028CA84
		protected MultipleDefaultFoldersInPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.policyName = (string)info.GetValue("policyName", typeof(string));
			this.userName = (string)info.GetValue("userName", typeof(string));
		}

		// Token: 0x0600AB15 RID: 43797 RVA: 0x0028E8D9 File Offset: 0x0028CAD9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("policyName", this.policyName);
			info.AddValue("userName", this.userName);
		}

		// Token: 0x1700374F RID: 14159
		// (get) Token: 0x0600AB16 RID: 43798 RVA: 0x0028E905 File Offset: 0x0028CB05
		public string PolicyName
		{
			get
			{
				return this.policyName;
			}
		}

		// Token: 0x17003750 RID: 14160
		// (get) Token: 0x0600AB17 RID: 43799 RVA: 0x0028E90D File Offset: 0x0028CB0D
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x040060B5 RID: 24757
		private readonly string policyName;

		// Token: 0x040060B6 RID: 24758
		private readonly string userName;
	}
}
