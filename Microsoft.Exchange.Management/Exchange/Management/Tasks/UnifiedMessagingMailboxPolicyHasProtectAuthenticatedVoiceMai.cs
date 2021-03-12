using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010D6 RID: 4310
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnifiedMessagingMailboxPolicyHasProtectAuthenticatedVoiceMailSetToException : LocalizedException
	{
		// Token: 0x0600B31F RID: 45855 RVA: 0x0029AC56 File Offset: 0x00298E56
		public UnifiedMessagingMailboxPolicyHasProtectAuthenticatedVoiceMailSetToException(string policyName, DRMProtectionOptions drmProtectionOption) : base(Strings.UnifiedMessagingMailboxPolicyHasProtectAuthenticatedVoiceMailSetTo(policyName, drmProtectionOption))
		{
			this.policyName = policyName;
			this.drmProtectionOption = drmProtectionOption;
		}

		// Token: 0x0600B320 RID: 45856 RVA: 0x0029AC73 File Offset: 0x00298E73
		public UnifiedMessagingMailboxPolicyHasProtectAuthenticatedVoiceMailSetToException(string policyName, DRMProtectionOptions drmProtectionOption, Exception innerException) : base(Strings.UnifiedMessagingMailboxPolicyHasProtectAuthenticatedVoiceMailSetTo(policyName, drmProtectionOption), innerException)
		{
			this.policyName = policyName;
			this.drmProtectionOption = drmProtectionOption;
		}

		// Token: 0x0600B321 RID: 45857 RVA: 0x0029AC94 File Offset: 0x00298E94
		protected UnifiedMessagingMailboxPolicyHasProtectAuthenticatedVoiceMailSetToException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.policyName = (string)info.GetValue("policyName", typeof(string));
			this.drmProtectionOption = (DRMProtectionOptions)info.GetValue("drmProtectionOption", typeof(DRMProtectionOptions));
		}

		// Token: 0x0600B322 RID: 45858 RVA: 0x0029ACE9 File Offset: 0x00298EE9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("policyName", this.policyName);
			info.AddValue("drmProtectionOption", this.drmProtectionOption);
		}

		// Token: 0x170038E4 RID: 14564
		// (get) Token: 0x0600B323 RID: 45859 RVA: 0x0029AD1A File Offset: 0x00298F1A
		public string PolicyName
		{
			get
			{
				return this.policyName;
			}
		}

		// Token: 0x170038E5 RID: 14565
		// (get) Token: 0x0600B324 RID: 45860 RVA: 0x0029AD22 File Offset: 0x00298F22
		public DRMProtectionOptions DrmProtectionOption
		{
			get
			{
				return this.drmProtectionOption;
			}
		}

		// Token: 0x0400624A RID: 25162
		private readonly string policyName;

		// Token: 0x0400624B RID: 25163
		private readonly DRMProtectionOptions drmProtectionOption;
	}
}
