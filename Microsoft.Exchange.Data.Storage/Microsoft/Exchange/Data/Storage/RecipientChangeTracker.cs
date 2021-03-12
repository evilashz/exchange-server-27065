using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200085B RID: 2139
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RecipientChangeTracker : IRecipientChangeTracker
	{
		// Token: 0x06005070 RID: 20592 RVA: 0x0014E3B4 File Offset: 0x0014C5B4
		void IRecipientChangeTracker.AddRecipient(CoreRecipient coreRecipient, out bool considerRecipientModified)
		{
			considerRecipientModified = false;
			int num = this.removedRecipientIds.IndexOf(coreRecipient.RowId);
			if (num != -1)
			{
				this.removedRecipientIds.RemoveAt(num);
				this.modifiedRecipients.Add(coreRecipient);
				considerRecipientModified = true;
				return;
			}
			int num2 = this.addedRecipients.BinarySearch(coreRecipient, RecipientChangeTracker.CoreRecipientComparer.Default);
			this.addedRecipients.Insert(~num2, coreRecipient);
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x0014E418 File Offset: 0x0014C618
		void IRecipientChangeTracker.RemoveAddedRecipient(CoreRecipient coreRecipient)
		{
			int index = this.addedRecipients.BinarySearch(coreRecipient, RecipientChangeTracker.CoreRecipientComparer.Default);
			this.addedRecipients.RemoveAt(index);
		}

		// Token: 0x06005072 RID: 20594 RVA: 0x0014E443 File Offset: 0x0014C643
		void IRecipientChangeTracker.RemoveUnchangedRecipient(CoreRecipient coreRecipient)
		{
			if (coreRecipient.Participant != null)
			{
				this.removedRecipients[coreRecipient.Participant] = coreRecipient;
			}
			this.removedRecipientIds.Add(coreRecipient.RowId);
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x0014E478 File Offset: 0x0014C678
		void IRecipientChangeTracker.RemoveModifiedRecipient(CoreRecipient coreRecipient)
		{
			for (int i = 0; i < this.modifiedRecipients.Count; i++)
			{
				if (this.modifiedRecipients[i] == coreRecipient)
				{
					this.modifiedRecipients.RemoveAt(i);
					break;
				}
			}
			if (coreRecipient.Participant != null)
			{
				this.removedRecipients[coreRecipient.Participant] = coreRecipient;
			}
			this.removedRecipientIds.Add(coreRecipient.RowId);
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x0014E4E9 File Offset: 0x0014C6E9
		void IRecipientChangeTracker.OnModifyRecipient(CoreRecipient coreRecipient)
		{
			this.modifiedRecipients.Add(coreRecipient);
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x06005075 RID: 20597 RVA: 0x0014E4F7 File Offset: 0x0014C6F7
		internal bool IsDirty
		{
			get
			{
				return this.addedRecipients.Count + this.modifiedRecipients.Count + this.removedRecipientIds.Count > 0;
			}
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x0014E51F File Offset: 0x0014C71F
		internal void ClearRemovedRecipients()
		{
			this.removedRecipientIds.Clear();
			this.removedRecipients.Clear();
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x0014E537 File Offset: 0x0014C737
		internal void ClearAddedRecipients()
		{
			this.addedRecipients.Clear();
		}

		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x06005078 RID: 20600 RVA: 0x0014E544 File Offset: 0x0014C744
		internal IList<CoreRecipient> AddedRecipients
		{
			get
			{
				return this.addedRecipients;
			}
		}

		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x06005079 RID: 20601 RVA: 0x0014E54C File Offset: 0x0014C74C
		internal IList<int> RemovedRecipientIds
		{
			get
			{
				return this.removedRecipientIds;
			}
		}

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x0600507A RID: 20602 RVA: 0x0014E554 File Offset: 0x0014C754
		internal IList<CoreRecipient> ModifiedRecipients
		{
			get
			{
				return this.modifiedRecipients;
			}
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x0014E55C File Offset: 0x0014C75C
		internal bool FindRemovedRecipient(Participant participant, out CoreRecipient recipient)
		{
			return this.removedRecipients.TryGetValue(participant, out recipient);
		}

		// Token: 0x04002C21 RID: 11297
		private readonly List<CoreRecipient> addedRecipients = new List<CoreRecipient>();

		// Token: 0x04002C22 RID: 11298
		private readonly Dictionary<Participant, CoreRecipient> removedRecipients = new Dictionary<Participant, CoreRecipient>(Participant.AddressEqualityComparer);

		// Token: 0x04002C23 RID: 11299
		private readonly List<int> removedRecipientIds = new List<int>();

		// Token: 0x04002C24 RID: 11300
		private readonly List<CoreRecipient> modifiedRecipients = new List<CoreRecipient>();

		// Token: 0x0200085C RID: 2140
		private class CoreRecipientComparer : IComparer<CoreRecipient>
		{
			// Token: 0x0600507D RID: 20605 RVA: 0x0014E5A4 File Offset: 0x0014C7A4
			private CoreRecipientComparer()
			{
			}

			// Token: 0x0600507E RID: 20606 RVA: 0x0014E5AC File Offset: 0x0014C7AC
			int IComparer<CoreRecipient>.Compare(CoreRecipient x, CoreRecipient y)
			{
				return x.RowId.CompareTo(y.RowId);
			}

			// Token: 0x04002C25 RID: 11301
			internal static RecipientChangeTracker.CoreRecipientComparer Default = new RecipientChangeTracker.CoreRecipientComparer();
		}
	}
}
