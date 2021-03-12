using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000060 RID: 96
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class HierarchyManifestCheckpoint
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x0000BA20 File Offset: 0x00009C20
		public HierarchyManifestCheckpoint(MapiStore mapiStore, byte[] stateIdsetGiven, byte[] stateCnsetSeen, SafeExExportHierManifestExHandle iExchangeExportHierManifestEx, int maxUncoalescedCount)
		{
			this.mapiStore = mapiStore;
			this.iExchangeExportHierManifestEx = iExchangeExportHierManifestEx;
			this.maxUncoalescedCount = maxUncoalescedCount;
			this.idsGiven = new List<long>(maxUncoalescedCount);
			this.cnsSeen = new List<long>(maxUncoalescedCount);
			this.idsDeleted = new List<long>(maxUncoalescedCount);
			this.checkpointStateIdsetGiven = HierarchyManifestCheckpoint.DuplicateArray(stateIdsetGiven);
			this.checkpointStateCnsetSeen = HierarchyManifestCheckpoint.DuplicateArray(stateCnsetSeen);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000BA89 File Offset: 0x00009C89
		public void CnSeen(long cn)
		{
			this.cnsSeen.Add(cn);
			this.CheckpointIfNeeded();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000BA9D File Offset: 0x00009C9D
		public void IdGiven(long id)
		{
			this.idsGiven.Add(id);
			this.CheckpointIfNeeded();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000BAB1 File Offset: 0x00009CB1
		public void IdDeleted(long id)
		{
			this.idsDeleted.Add(id);
			this.CheckpointIfNeeded();
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000BAC8 File Offset: 0x00009CC8
		public void Checkpoint(out byte[] stateIdsetGiven, out byte[] stateCnsetSeen)
		{
			if (this.IsCoalesced)
			{
				stateIdsetGiven = HierarchyManifestCheckpoint.DuplicateArray(this.checkpointStateIdsetGiven);
				stateCnsetSeen = HierarchyManifestCheckpoint.DuplicateArray(this.checkpointStateCnsetSeen);
				return;
			}
			SafeExMemoryHandle safeExMemoryHandle = null;
			SafeExMemoryHandle safeExMemoryHandle2 = null;
			try
			{
				int num = 0;
				int num2 = 0;
				int num3 = this.iExchangeExportHierManifestEx.Checkpoint(this.checkpointStateIdsetGiven, this.checkpointStateIdsetGiven.Length, this.checkpointStateCnsetSeen, this.checkpointStateCnsetSeen.Length, HierarchyManifestCheckpoint.ToLengthPrefixedArray(this.idsGiven), HierarchyManifestCheckpoint.ToLengthPrefixedArray(this.cnsSeen), HierarchyManifestCheckpoint.ToLengthPrefixedArray(this.idsDeleted), out safeExMemoryHandle, out num, out safeExMemoryHandle2, out num2);
				if (num3 != 0)
				{
					MapiExceptionHelper.ThrowIfError("Unable to checkpoint ICS state.", num3, this.iExchangeExportHierManifestEx, this.mapiStore.LastLowLevelException);
				}
				byte[] array = Array<byte>.Empty;
				if (num > 0 && safeExMemoryHandle != null)
				{
					array = new byte[num];
					Marshal.Copy(safeExMemoryHandle.DangerousGetHandle(), array, 0, num);
				}
				byte[] array2 = Array<byte>.Empty;
				if (num2 > 0 && safeExMemoryHandle2 != null)
				{
					array2 = new byte[num2];
					Marshal.Copy(safeExMemoryHandle2.DangerousGetHandle(), array2, 0, num2);
				}
				stateIdsetGiven = HierarchyManifestCheckpoint.DuplicateArray(array);
				stateCnsetSeen = HierarchyManifestCheckpoint.DuplicateArray(array2);
				this.checkpointStateIdsetGiven = array;
				this.checkpointStateCnsetSeen = array2;
				this.idsGiven.Clear();
				this.cnsSeen.Clear();
				this.idsDeleted.Clear();
			}
			finally
			{
				if (safeExMemoryHandle != null)
				{
					safeExMemoryHandle.Dispose();
				}
				if (safeExMemoryHandle2 != null)
				{
					safeExMemoryHandle2.Dispose();
				}
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000BC34 File Offset: 0x00009E34
		private static byte[] DuplicateArray(byte[] byteArray)
		{
			byte[] array = Array<byte>.Empty;
			if (byteArray != null && byteArray.Length > 0)
			{
				array = new byte[byteArray.Length];
				Array.Copy(byteArray, 0, array, 0, byteArray.Length);
			}
			return array;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000BC66 File Offset: 0x00009E66
		private bool IsCoalesced
		{
			get
			{
				return this.CoalesceCount == 0;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000BC71 File Offset: 0x00009E71
		private int CoalesceCount
		{
			get
			{
				return this.idsDeleted.Count + this.idsGiven.Count + this.cnsSeen.Count;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000BC98 File Offset: 0x00009E98
		private void CheckpointIfNeeded()
		{
			if (this.CoalesceCount >= this.maxUncoalescedCount)
			{
				byte[] array;
				byte[] array2;
				this.Checkpoint(out array, out array2);
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000BCC0 File Offset: 0x00009EC0
		private static long[] ToLengthPrefixedArray(List<long> list)
		{
			long[] array = new long[list.Count + 1];
			array[0] = (long)list.Count;
			list.CopyTo(array, 1);
			return array;
		}

		// Token: 0x04000466 RID: 1126
		private readonly MapiStore mapiStore;

		// Token: 0x04000467 RID: 1127
		private readonly List<long> idsGiven;

		// Token: 0x04000468 RID: 1128
		private readonly List<long> cnsSeen;

		// Token: 0x04000469 RID: 1129
		private readonly List<long> idsDeleted;

		// Token: 0x0400046A RID: 1130
		private readonly int maxUncoalescedCount;

		// Token: 0x0400046B RID: 1131
		private readonly SafeExExportHierManifestExHandle iExchangeExportHierManifestEx;

		// Token: 0x0400046C RID: 1132
		private byte[] checkpointStateIdsetGiven;

		// Token: 0x0400046D RID: 1133
		private byte[] checkpointStateCnsetSeen;
	}
}
