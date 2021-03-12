using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002E8 RID: 744
	[DataContract]
	public class SetSendAddressDefaultConfiguration : SetObjectProperties
	{
		// Token: 0x17001E1B RID: 7707
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x0008A1E7 File Offset: 0x000883E7
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-MailboxMessageConfiguration";
			}
		}

		// Token: 0x17001E1C RID: 7708
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x0008A1EE File Offset: 0x000883EE
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x17001E1D RID: 7709
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x0008A1F5 File Offset: 0x000883F5
		// (set) Token: 0x06002D0D RID: 11533 RVA: 0x0008A207 File Offset: 0x00088407
		[DataMember]
		public string SendAddressDefault
		{
			get
			{
				return (string)base[MailboxMessageConfigurationSchema.SendAddressDefault];
			}
			set
			{
				base[MailboxMessageConfigurationSchema.SendAddressDefault] = value.Trim();
			}
		}
	}
}
