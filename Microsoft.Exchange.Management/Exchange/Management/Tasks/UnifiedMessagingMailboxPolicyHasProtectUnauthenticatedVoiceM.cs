using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010D7 RID: 4311
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnifiedMessagingMailboxPolicyHasProtectUnauthenticatedVoiceMailSetToException : LocalizedException
	{
		// Token: 0x0600B325 RID: 45861 RVA: 0x0029AD2A File Offset: 0x00298F2A
		public UnifiedMessagingMailboxPolicyHasProtectUnauthenticatedVoiceMailSetToException(string policyName, DRMProtectionOptions drmProtectionOption) : base(Strings.UnifiedMessagingMailboxPolicyHasProtectUnauthenticatedVoiceMailSetTo(policyName, drmProtectionOption))
		{
			this.policyName = policyName;
			this.drmProtectionOption = drmProtectionOption;
		}

		// Token: 0x0600B326 RID: 45862 RVA: 0x0029AD47 File Offset: 0x00298F47
		public UnifiedMessagingMailboxPolicyHasProtectUnauthenticatedVoiceMailSetToException(string policyName, DRMProtectionOptions drmProtectionOption, Exception innerException) : base(Strings.UnifiedMessagingMailboxPolicyHasProtectUnauthenticatedVoiceMailSetTo(policyName, drmProtectionOption), innerException)
		{
			this.policyName = policyName;
			this.drmProtectionOption = drmProtectionOption;
		}

		// Token: 0x0600B327 RID: 45863 RVA: 0x0029AD68 File Offset: 0x00298F68
		protected UnifiedMessagingMailboxPolicyHasProtectUnauthenticatedVoiceMailSetToException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.policyName = (string)info.GetValue("policyName", typeof(string));
			this.drmProtectionOption = (DRMProtectionOptions)info.GetValue("drmProtectionOption", typeof(DRMProtectionOptions));
		}

		// Token: 0x0600B328 RID: 45864 RVA: 0x0029ADBD File Offset: 0x00298FBD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("policyName", this.policyName);
			info.AddValue("drmProtectionOption", this.drmProtectionOption);
		}

		// Token: 0x170038E6 RID: 14566
		// (get) Token: 0x0600B329 RID: 45865 RVA: 0x0029ADEE File Offset: 0x00298FEE
		public string PolicyName
		{
			get
			{
				return this.policyName;
			}
		}

		// Token: 0x170038E7 RID: 14567
		// (get) Token: 0x0600B32A RID: 45866 RVA: 0x0029ADF6 File Offset: 0x00298FF6
		public DRMProtectionOptions DrmProtectionOption
		{
			get
			{
				return this.drmProtectionOption;
			}
		}

		// Token: 0x0400624C RID: 25164
		private readonly string policyName;

		// Token: 0x0400624D RID: 25165
		private readonly DRMProtectionOptions drmProtectionOption;
	}
}
