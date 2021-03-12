using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FFF RID: 4095
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveAssociatedThrottlingPolicyException : LocalizedException
	{
		// Token: 0x0600AEB7 RID: 44727 RVA: 0x002934B2 File Offset: 0x002916B2
		public CannotRemoveAssociatedThrottlingPolicyException(string policyName) : base(Strings.ErrorCannotRemoveAssociatedThrottlingPolicy(policyName))
		{
			this.policyName = policyName;
		}

		// Token: 0x0600AEB8 RID: 44728 RVA: 0x002934C7 File Offset: 0x002916C7
		public CannotRemoveAssociatedThrottlingPolicyException(string policyName, Exception innerException) : base(Strings.ErrorCannotRemoveAssociatedThrottlingPolicy(policyName), innerException)
		{
			this.policyName = policyName;
		}

		// Token: 0x0600AEB9 RID: 44729 RVA: 0x002934DD File Offset: 0x002916DD
		protected CannotRemoveAssociatedThrottlingPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.policyName = (string)info.GetValue("policyName", typeof(string));
		}

		// Token: 0x0600AEBA RID: 44730 RVA: 0x00293507 File Offset: 0x00291707
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("policyName", this.policyName);
		}

		// Token: 0x170037D8 RID: 14296
		// (get) Token: 0x0600AEBB RID: 44731 RVA: 0x00293522 File Offset: 0x00291722
		public string PolicyName
		{
			get
			{
				return this.policyName;
			}
		}

		// Token: 0x0400613E RID: 24894
		private readonly string policyName;
	}
}
