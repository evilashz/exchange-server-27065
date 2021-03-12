using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000D7 RID: 215
	[DataContract]
	[KnownType(typeof(GroupRecipientRow))]
	public class GroupRecipientRow : RecipientRow
	{
		// Token: 0x06001D80 RID: 7552 RVA: 0x0005A62A File Offset: 0x0005882A
		public GroupRecipientRow(ReducedRecipient recipient) : base(recipient)
		{
			this.DistinguishedName = recipient.DistinguishedName;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0005A63F File Offset: 0x0005883F
		public GroupRecipientRow(MailEnabledRecipient recipient) : base(recipient)
		{
			this.DistinguishedName = recipient.DistinguishedName;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0005A654 File Offset: 0x00058854
		public GroupRecipientRow(WindowsGroup group) : base(group)
		{
			this.DistinguishedName = group.DistinguishedName;
		}

		// Token: 0x17001958 RID: 6488
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x0005A669 File Offset: 0x00058869
		// (set) Token: 0x06001D84 RID: 7556 RVA: 0x0005A671 File Offset: 0x00058871
		[DataMember]
		public string DistinguishedName { get; protected set; }
	}
}
