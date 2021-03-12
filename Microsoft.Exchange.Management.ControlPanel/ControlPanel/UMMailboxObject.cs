using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200046E RID: 1134
	[DataContract]
	public class UMMailboxObject : BaseRow
	{
		// Token: 0x0600394D RID: 14669 RVA: 0x000AE612 File Offset: 0x000AC812
		public UMMailboxObject(UMMailbox mailbox) : base(mailbox)
		{
			this.Mailbox = mailbox;
		}

		// Token: 0x170022A9 RID: 8873
		// (get) Token: 0x0600394E RID: 14670 RVA: 0x000AE622 File Offset: 0x000AC822
		// (set) Token: 0x0600394F RID: 14671 RVA: 0x000AE62A File Offset: 0x000AC82A
		public UMMailbox Mailbox { get; private set; }

		// Token: 0x170022AA RID: 8874
		// (get) Token: 0x06003950 RID: 14672 RVA: 0x000AE633 File Offset: 0x000AC833
		// (set) Token: 0x06003951 RID: 14673 RVA: 0x000AE65E File Offset: 0x000AC85E
		[DataMember]
		public virtual string DisplayName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.Mailbox.DisplayName))
				{
					return this.Mailbox.DisplayName;
				}
				return this.Mailbox.Name;
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022AB RID: 8875
		// (get) Token: 0x06003952 RID: 14674 RVA: 0x000AE665 File Offset: 0x000AC865
		// (set) Token: 0x06003953 RID: 14675 RVA: 0x000AE677 File Offset: 0x000AC877
		[DataMember]
		public virtual string[] CallAnsweringRulesExtensions
		{
			get
			{
				return this.Mailbox.CallAnsweringRulesExtensions.ToArray();
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}
	}
}
