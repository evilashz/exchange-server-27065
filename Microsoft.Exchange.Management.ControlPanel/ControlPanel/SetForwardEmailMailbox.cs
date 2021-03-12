using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002A7 RID: 679
	[DataContract]
	public class SetForwardEmailMailbox : SetObjectProperties
	{
		// Token: 0x06002B9E RID: 11166 RVA: 0x00088189 File Offset: 0x00086389
		public SetForwardEmailMailbox()
		{
			base.IgnoreNullOrEmpty = false;
		}

		// Token: 0x17001D81 RID: 7553
		// (get) Token: 0x06002B9F RID: 11167 RVA: 0x00088198 File Offset: 0x00086398
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-Mailbox";
			}
		}

		// Token: 0x17001D82 RID: 7554
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x0008819F File Offset: 0x0008639F
		public override string RbacScope
		{
			get
			{
				return "@W:Self|Organization";
			}
		}

		// Token: 0x17001D83 RID: 7555
		// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x000881A6 File Offset: 0x000863A6
		// (set) Token: 0x06002BA2 RID: 11170 RVA: 0x000881B8 File Offset: 0x000863B8
		[DataMember]
		public string ForwardingSmtpAddress
		{
			get
			{
				return (string)base[MailboxSchema.ForwardingSmtpAddress];
			}
			set
			{
				base[MailboxSchema.ForwardingSmtpAddress] = value;
			}
		}

		// Token: 0x17001D84 RID: 7556
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x000881C6 File Offset: 0x000863C6
		// (set) Token: 0x06002BA4 RID: 11172 RVA: 0x000881E2 File Offset: 0x000863E2
		[DataMember]
		public bool DeliverToMailboxAndForward
		{
			get
			{
				return (bool)(base[MailboxSchema.DeliverToMailboxAndForward] ?? false);
			}
			set
			{
				base[MailboxSchema.DeliverToMailboxAndForward] = value;
			}
		}
	}
}
