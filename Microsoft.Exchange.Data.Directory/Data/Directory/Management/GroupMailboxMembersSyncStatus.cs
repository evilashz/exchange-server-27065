using System;
using System.Linq;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000719 RID: 1817
	[Serializable]
	public class GroupMailboxMembersSyncStatus
	{
		// Token: 0x060055BC RID: 21948 RVA: 0x00135A84 File Offset: 0x00133C84
		internal GroupMailboxMembersSyncStatus(ADObjectId[] membersInAD, ADObjectId[] membersInMailbox)
		{
			this.MembersInADOnly = membersInAD.Except(membersInMailbox).ToArray<ADObjectId>();
			this.MembersInMailboxOnly = membersInMailbox.Except(membersInAD).ToArray<ADObjectId>();
			this.IsSynced = (this.MembersInADOnly.Length == 0 && this.MembersInMailboxOnly.Length == 0);
		}

		// Token: 0x17001C9E RID: 7326
		// (get) Token: 0x060055BD RID: 21949 RVA: 0x00135AD9 File Offset: 0x00133CD9
		// (set) Token: 0x060055BE RID: 21950 RVA: 0x00135AE1 File Offset: 0x00133CE1
		public ADObjectId[] MembersInADOnly { get; set; }

		// Token: 0x17001C9F RID: 7327
		// (get) Token: 0x060055BF RID: 21951 RVA: 0x00135AEA File Offset: 0x00133CEA
		// (set) Token: 0x060055C0 RID: 21952 RVA: 0x00135AF2 File Offset: 0x00133CF2
		public ADObjectId[] MembersInMailboxOnly { get; set; }

		// Token: 0x17001CA0 RID: 7328
		// (get) Token: 0x060055C1 RID: 21953 RVA: 0x00135AFB File Offset: 0x00133CFB
		// (set) Token: 0x060055C2 RID: 21954 RVA: 0x00135B03 File Offset: 0x00133D03
		public bool IsSynced { get; private set; }

		// Token: 0x060055C3 RID: 21955 RVA: 0x00135B0C File Offset: 0x00133D0C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"IsSynced: ",
				this.IsSynced.ToString(),
				". MembersInADOnly: ",
				this.MembersInADOnly.Length,
				", MembersInMailboxOnly: ",
				this.MembersInMailboxOnly.Length
			});
		}
	}
}
