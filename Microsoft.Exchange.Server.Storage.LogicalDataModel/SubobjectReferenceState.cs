using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000DB RID: 219
	internal class SubobjectReferenceState : IComponentData
	{
		// Token: 0x06000C00 RID: 3072 RVA: 0x000607E6 File Offset: 0x0005E9E6
		private SubobjectReferenceState()
		{
			this.lockObject = new object();
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000607F9 File Offset: 0x0005E9F9
		internal static void Initialize()
		{
			if (SubobjectReferenceState.subobjectReferenceStateSlot == -1)
			{
				SubobjectReferenceState.subobjectReferenceStateSlot = MailboxState.AllocateComponentDataSlot(false);
			}
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00060810 File Offset: 0x0005EA10
		internal static SubobjectReferenceState GetState(Mailbox mailbox, bool create)
		{
			SubobjectReferenceState subobjectReferenceState = (SubobjectReferenceState)mailbox.SharedState.GetComponentData(SubobjectReferenceState.subobjectReferenceStateSlot);
			if (subobjectReferenceState == null && create)
			{
				subobjectReferenceState = new SubobjectReferenceState();
				SubobjectReferenceState subobjectReferenceState2 = (SubobjectReferenceState)mailbox.SharedState.CompareExchangeComponentData(SubobjectReferenceState.subobjectReferenceStateSlot, null, subobjectReferenceState);
				if (subobjectReferenceState2 != null)
				{
					subobjectReferenceState = subobjectReferenceState2;
				}
			}
			return subobjectReferenceState;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0006085D File Offset: 0x0005EA5D
		internal static SubobjectReferenceState GetState(MailboxState mailboxState)
		{
			return (SubobjectReferenceState)mailboxState.GetComponentData(SubobjectReferenceState.subobjectReferenceStateSlot);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00060870 File Offset: 0x0005EA70
		bool IComponentData.DoCleanup(Context context)
		{
			bool result;
			using (LockManager.Lock(this.lockObject, LockManager.LockType.LeafMonitorLock, context.Diagnostics))
			{
				result = (this.inmemoryReferences == null || this.inmemoryReferences.Count == 0);
			}
			return result;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000608CC File Offset: 0x0005EACC
		public void Addref(long inid)
		{
			using (LockManager.Lock(this.lockObject, LockManager.LockType.LeafMonitorLock))
			{
				if (this.inmemoryReferences == null)
				{
					this.inmemoryReferences = new Dictionary<long, SubobjectReferenceState.ReferenceState>(4);
				}
				if (this.inmemoryReferences.ContainsKey(inid))
				{
					SubobjectReferenceState.ReferenceState referenceState = this.inmemoryReferences[inid];
					if (referenceState.RefCounter == 2147483647)
					{
						throw new InvalidOperationException("refcount overflow");
					}
					this.inmemoryReferences[inid] = new SubobjectReferenceState.ReferenceState(referenceState.RefCounter + 1, referenceState.InTombStone);
				}
				else
				{
					this.inmemoryReferences.Add(inid, new SubobjectReferenceState.ReferenceState(1, false));
				}
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00060988 File Offset: 0x0005EB88
		public void Release(Context context, long inid, Mailbox mailbox)
		{
			ILockStatistics lockStats = (context != null) ? context.Diagnostics : null;
			bool flag = false;
			using (LockManager.Lock(this.lockObject, LockManager.LockType.LeafMonitorLock, lockStats))
			{
				SubobjectReferenceState.ReferenceState referenceState;
				if (this.inmemoryReferences != null && this.inmemoryReferences.TryGetValue(inid, out referenceState))
				{
					if (referenceState.RefCounter == 1)
					{
						this.inmemoryReferences.Remove(inid);
						if (this.inmemoryReferences.Count == 0)
						{
							this.inmemoryReferences = null;
						}
						if (context != null && referenceState.InTombStone)
						{
							flag = true;
						}
					}
					else
					{
						this.inmemoryReferences[inid] = new SubobjectReferenceState.ReferenceState(referenceState.RefCounter - 1, referenceState.InTombStone);
					}
				}
			}
			if (flag)
			{
				SubobjectCleanup.NotifyCleanupMaintenanceIsRequired(context);
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00060A50 File Offset: 0x0005EC50
		public bool IsReferenced(long inid)
		{
			bool result;
			using (LockManager.Lock(this.lockObject, LockManager.LockType.LeafMonitorLock))
			{
				SubobjectReferenceState.ReferenceState referenceState;
				if (this.inmemoryReferences != null && this.inmemoryReferences.TryGetValue(inid, out referenceState))
				{
					result = (referenceState.RefCounter > 0);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00060AB4 File Offset: 0x0005ECB4
		public bool IsInTombstone(long inid)
		{
			bool result;
			using (LockManager.Lock(this.lockObject, LockManager.LockType.LeafMonitorLock))
			{
				SubobjectReferenceState.ReferenceState referenceState;
				if (this.inmemoryReferences != null && this.inmemoryReferences.TryGetValue(inid, out referenceState))
				{
					result = referenceState.InTombStone;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00060B14 File Offset: 0x0005ED14
		public void OnAddToTombstone(long inid)
		{
			using (LockManager.Lock(this.lockObject, LockManager.LockType.LeafMonitorLock))
			{
				SubobjectReferenceState.ReferenceState referenceState;
				if (this.inmemoryReferences.TryGetValue(inid, out referenceState))
				{
					this.inmemoryReferences[inid] = new SubobjectReferenceState.ReferenceState(referenceState.RefCounter, true);
				}
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00060B78 File Offset: 0x0005ED78
		public void OnRemoveFromTombstone(long inid)
		{
			using (LockManager.Lock(this.lockObject, LockManager.LockType.LeafMonitorLock))
			{
				SubobjectReferenceState.ReferenceState referenceState;
				if (this.inmemoryReferences != null && this.inmemoryReferences.TryGetValue(inid, out referenceState))
				{
					this.inmemoryReferences[inid] = new SubobjectReferenceState.ReferenceState(referenceState.RefCounter, false);
				}
			}
		}

		// Token: 0x04000592 RID: 1426
		private static int subobjectReferenceStateSlot = -1;

		// Token: 0x04000593 RID: 1427
		private Dictionary<long, SubobjectReferenceState.ReferenceState> inmemoryReferences;

		// Token: 0x04000594 RID: 1428
		private object lockObject;

		// Token: 0x020000DC RID: 220
		private struct ReferenceState
		{
			// Token: 0x06000C0C RID: 3084 RVA: 0x00060BEC File Offset: 0x0005EDEC
			internal ReferenceState(int refCounter, bool inTombstone)
			{
				this.refCounter = refCounter;
				this.inTombStone = inTombstone;
			}

			// Token: 0x17000267 RID: 615
			// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00060BFC File Offset: 0x0005EDFC
			internal int RefCounter
			{
				get
				{
					return this.refCounter;
				}
			}

			// Token: 0x17000268 RID: 616
			// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00060C04 File Offset: 0x0005EE04
			internal bool InTombStone
			{
				get
				{
					return this.inTombStone;
				}
			}

			// Token: 0x04000595 RID: 1429
			private int refCounter;

			// Token: 0x04000596 RID: 1430
			private bool inTombStone;
		}
	}
}
