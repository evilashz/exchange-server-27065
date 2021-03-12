using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200006E RID: 110
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ManifestCheckpoint
	{
		// Token: 0x060002DC RID: 732 RVA: 0x0000C478 File Offset: 0x0000A678
		public ManifestCheckpoint(MapiStore mapiStore, Stream checkpointState, IExExportManifest iExchangeExportManifest, int maxUncoalescedCount)
		{
			this.mapiStore = mapiStore;
			this.iExchangeExportManifest = iExchangeExportManifest;
			this.maxUncoalescedCount = maxUncoalescedCount;
			this.idsGiven = new List<long>(maxUncoalescedCount);
			this.cnsSeen = new List<long>(maxUncoalescedCount);
			this.cnsSeenAssociated = new List<long>(maxUncoalescedCount);
			this.idsDeleted = new List<long>(maxUncoalescedCount);
			this.cnsRead = new List<long>(maxUncoalescedCount);
			this.checkpointState = checkpointState;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000C4E9 File Offset: 0x0000A6E9
		public void CnSeen(bool isAssociated, long cn)
		{
			if (isAssociated)
			{
				this.cnsSeenAssociated.Add(cn);
			}
			else
			{
				this.cnsSeen.Add(cn);
			}
			this.CheckpointIfNeeded();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000C50E File Offset: 0x0000A70E
		public void IdGiven(long id)
		{
			this.idsGiven.Add(id);
			this.CheckpointIfNeeded();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000C522 File Offset: 0x0000A722
		public void IdDeleted(long id)
		{
			this.idsDeleted.Add(id);
			this.CheckpointIfNeeded();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000C536 File Offset: 0x0000A736
		public void CnRead(long cn)
		{
			if (cn != 0L)
			{
				this.cnsRead.Add(cn);
				this.CheckpointIfNeeded();
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000C550 File Offset: 0x0000A750
		public void Checkpoint()
		{
			this.checkpointState.Seek(0L, SeekOrigin.Begin);
			if (this.IsCoalesced)
			{
				return;
			}
			MapiIStream iStream = null;
			if (this.checkpointState != null)
			{
				iStream = new MapiIStream(this.checkpointState);
			}
			int num = this.iExchangeExportManifest.Checkpoint(iStream, false, ManifestCheckpoint.ToLengthPrefixedArray(this.idsGiven), ManifestCheckpoint.ToLengthPrefixedArray(this.cnsSeen), ManifestCheckpoint.ToLengthPrefixedArray(this.cnsSeenAssociated), ManifestCheckpoint.ToLengthPrefixedArray(this.idsDeleted), ManifestCheckpoint.ToLengthPrefixedArray(this.cnsRead));
			if (num != 0)
			{
				MapiExceptionHelper.ThrowIfError("Unable to checkpoint ICS state.", num, this.iExchangeExportManifest, this.mapiStore.LastLowLevelException);
			}
			this.checkpointState.Seek(0L, SeekOrigin.Begin);
			this.idsGiven.Clear();
			this.cnsSeen.Clear();
			this.cnsSeenAssociated.Clear();
			this.idsDeleted.Clear();
			this.cnsRead.Clear();
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000C637 File Offset: 0x0000A837
		private bool IsCoalesced
		{
			get
			{
				return this.CoalesceCount == 0;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000C642 File Offset: 0x0000A842
		private int CoalesceCount
		{
			get
			{
				return this.idsDeleted.Count + this.idsGiven.Count + this.cnsSeen.Count + this.cnsSeenAssociated.Count + this.cnsRead.Count;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000C67F File Offset: 0x0000A87F
		private void CheckpointIfNeeded()
		{
			if (this.CoalesceCount >= this.maxUncoalescedCount)
			{
				this.Checkpoint();
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C698 File Offset: 0x0000A898
		private static long[] ToLengthPrefixedArray(List<long> list)
		{
			long[] array = new long[list.Count + 1];
			array[0] = (long)list.Count;
			list.CopyTo(array, 1);
			return array;
		}

		// Token: 0x0400049D RID: 1181
		private readonly MapiStore mapiStore;

		// Token: 0x0400049E RID: 1182
		private readonly List<long> idsGiven;

		// Token: 0x0400049F RID: 1183
		private readonly List<long> cnsSeen;

		// Token: 0x040004A0 RID: 1184
		private readonly List<long> cnsSeenAssociated;

		// Token: 0x040004A1 RID: 1185
		private readonly List<long> idsDeleted;

		// Token: 0x040004A2 RID: 1186
		private readonly List<long> cnsRead;

		// Token: 0x040004A3 RID: 1187
		private readonly int maxUncoalescedCount;

		// Token: 0x040004A4 RID: 1188
		private readonly IExExportManifest iExchangeExportManifest;

		// Token: 0x040004A5 RID: 1189
		private readonly Stream checkpointState;
	}
}
