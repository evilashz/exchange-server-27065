using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000068 RID: 104
	internal abstract class TagEnforcerBase
	{
		// Token: 0x060003AB RID: 939 RVA: 0x0001A5A6 File Offset: 0x000187A6
		internal TagEnforcerBase(MailboxDataForTags mailboxDataForTags, ElcTagSubAssistant elcTagSubAssistant)
		{
			this.MailboxDataForTags = mailboxDataForTags;
			this.ElcTagSubAssistant = elcTagSubAssistant;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0001A5BC File Offset: 0x000187BC
		internal ElcTagSubAssistant Assistant
		{
			get
			{
				return this.ElcTagSubAssistant;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0001A5C4 File Offset: 0x000187C4
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0001A5CC File Offset: 0x000187CC
		protected MailboxDataForTags MailboxDataForTags { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0001A5D5 File Offset: 0x000187D5
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0001A5DD File Offset: 0x000187DD
		protected ElcTagSubAssistant ElcTagSubAssistant { get; set; }

		// Token: 0x060003B1 RID: 945
		internal abstract bool IsEnabled();

		// Token: 0x060003B2 RID: 946
		internal abstract void Invoke();
	}
}
