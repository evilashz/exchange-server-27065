using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000017 RID: 23
	internal class MembershipReactor<T>
	{
		// Token: 0x0600005B RID: 91 RVA: 0x000028AA File Offset: 0x00000AAA
		internal MembershipReactor(IEqualityComparer<T> comparer)
		{
			this.comparer = comparer;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000028B9 File Offset: 0x00000AB9
		internal IEnumerable<T> Added
		{
			get
			{
				return this.addedMembers;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000028C1 File Offset: 0x00000AC1
		internal IEnumerable<T> Removed
		{
			get
			{
				return this.removedMembers;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000028C9 File Offset: 0x00000AC9
		internal IEnumerable<T> Modified
		{
			get
			{
				return this.modifiedMembers;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000028D4 File Offset: 0x00000AD4
		internal void Fusion(IEnumerable<T> existingParticipants, IEnumerable<T> updatedParticipants)
		{
			this.addedMembers = updatedParticipants.Except(existingParticipants, this.comparer);
			this.removedMembers = existingParticipants.Except(updatedParticipants, this.comparer);
			this.modifiedMembers = updatedParticipants.Except(this.addedMembers, this.comparer).Except(this.removedMembers, this.comparer);
		}

		// Token: 0x040000C1 RID: 193
		private readonly IEqualityComparer<T> comparer;

		// Token: 0x040000C2 RID: 194
		private IEnumerable<T> addedMembers;

		// Token: 0x040000C3 RID: 195
		private IEnumerable<T> modifiedMembers;

		// Token: 0x040000C4 RID: 196
		private IEnumerable<T> removedMembers;
	}
}
