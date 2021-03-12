using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002A6 RID: 678
	[KnownType(typeof(ForwardEmailMailbox))]
	[DataContract]
	public class ForwardEmailMailbox : BaseRow
	{
		// Token: 0x06002B97 RID: 11159 RVA: 0x0008810D File Offset: 0x0008630D
		public ForwardEmailMailbox(Mailbox mailbox) : base(mailbox)
		{
			this.Mailbox = mailbox;
		}

		// Token: 0x17001D7E RID: 7550
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x0008811D File Offset: 0x0008631D
		// (set) Token: 0x06002B99 RID: 11161 RVA: 0x00088125 File Offset: 0x00086325
		private Mailbox Mailbox { get; set; }

		// Token: 0x17001D7F RID: 7551
		// (get) Token: 0x06002B9A RID: 11162 RVA: 0x0008812E File Offset: 0x0008632E
		// (set) Token: 0x06002B9B RID: 11163 RVA: 0x00088159 File Offset: 0x00086359
		[DataMember]
		public string ForwardingSmtpAddress
		{
			get
			{
				if (!(this.Mailbox.ForwardingSmtpAddress == null))
				{
					return this.Mailbox.ForwardingSmtpAddress.AddressString;
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D80 RID: 7552
		// (get) Token: 0x06002B9C RID: 11164 RVA: 0x00088160 File Offset: 0x00086360
		// (set) Token: 0x06002B9D RID: 11165 RVA: 0x00088182 File Offset: 0x00086382
		[DataMember]
		public bool DeliverToMailboxAndForward
		{
			get
			{
				return this.Mailbox.ForwardingSmtpAddress == null || this.Mailbox.DeliverToMailboxAndForward;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
