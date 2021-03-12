using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002B5 RID: 693
	[DataContract]
	public class SetImapSubscription : ImapSubscriptionBaseParameter
	{
		// Token: 0x17001DAA RID: 7594
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x00088777 File Offset: 0x00086977
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-ImapSubscription";
			}
		}

		// Token: 0x17001DAB RID: 7595
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x0008877E File Offset: 0x0008697E
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x17001DAC RID: 7596
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x00088785 File Offset: 0x00086985
		// (set) Token: 0x06002BF9 RID: 11257 RVA: 0x000887A0 File Offset: 0x000869A0
		[DataMember]
		public string ValidateSecret
		{
			get
			{
				return (string)(base["ValidateSecret"] ?? string.Empty);
			}
			set
			{
				base["ValidateSecret"] = value;
			}
		}

		// Token: 0x17001DAD RID: 7597
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x000887AE File Offset: 0x000869AE
		// (set) Token: 0x06002BFB RID: 11259 RVA: 0x000887CA File Offset: 0x000869CA
		[DataMember]
		public bool ResendVerification
		{
			get
			{
				return (bool)(base["ResendVerification"] ?? false);
			}
			set
			{
				base["ResendVerification"] = value;
			}
		}
	}
}
