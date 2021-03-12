using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001CC RID: 460
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MapiHierarchySynchronizerEx : MapiSynchronizerExBase
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x000195AC File Offset: 0x000177AC
		internal unsafe MapiHierarchySynchronizerEx(SafeExExportHierarchyChangesExHandle iExchangeExportHierarchyChangesEx, MapiStore mapiStore, byte[] stateIdsetGiven, byte[] stateCnsetSeen, SyncConfigFlags flags, Restriction restriction, ICollection<PropTag> tagsInclude, ICollection<PropTag> tagsExclude, int fastTransferBlockSize) : base(iExchangeExportHierarchyChangesEx, mapiStore)
		{
			if (stateIdsetGiven == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("stateIdsetGiven");
			}
			if (stateCnsetSeen == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("stateCnsetSeen");
			}
			this.iExchangeExportHierarchyChangesEx = iExchangeExportHierarchyChangesEx;
			base.LockStore();
			try
			{
				int num = 0;
				if (restriction != null)
				{
					int bytesToMarshal = restriction.GetBytesToMarshal();
					try
					{
						fixed (byte* ptr = new byte[bytesToMarshal])
						{
							byte* ptr2 = ptr;
							SRestriction* ptr3 = (SRestriction*)ptr;
							ptr2 += (SRestriction.SizeOf + 7 & -8);
							restriction.MarshalToNative(ptr3, ref ptr2);
							num = this.iExchangeExportHierarchyChangesEx.Config(stateIdsetGiven, stateIdsetGiven.Length, stateCnsetSeen, stateCnsetSeen.Length, flags, ptr3, (tagsInclude != null && tagsInclude.Count > 0) ? PropTagHelper.SPropTagArray(tagsInclude) : null, (tagsExclude != null && tagsExclude.Count > 0) ? PropTagHelper.SPropTagArray(tagsExclude) : null, fastTransferBlockSize);
							goto IL_11F;
						}
					}
					finally
					{
						byte* ptr = null;
					}
				}
				num = this.iExchangeExportHierarchyChangesEx.Config(stateIdsetGiven, stateIdsetGiven.Length, stateCnsetSeen, stateCnsetSeen.Length, flags, null, (tagsInclude != null && tagsInclude.Count > 0) ? PropTagHelper.SPropTagArray(tagsInclude) : null, (tagsExclude != null && tagsExclude.Count > 0) ? PropTagHelper.SPropTagArray(tagsExclude) : null, fastTransferBlockSize);
				IL_11F:
				if (num != 0)
				{
					base.ThrowIfError("Unable to configure ICS synchronizer.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001970C File Offset: 0x0001790C
		protected override void MapiInternalDispose()
		{
			this.iExchangeExportHierarchyChangesEx = null;
			base.MapiInternalDispose();
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001971B File Offset: 0x0001791B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiHierarchySynchronizerEx>(this);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00019724 File Offset: 0x00017924
		public void GetState(out byte[] stateIdsetGiven, out byte[] stateCnsetSeen)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				IntPtr zero = IntPtr.Zero;
				IntPtr zero2 = IntPtr.Zero;
				int num = 0;
				int num2 = 0;
				int state = this.iExchangeExportHierarchyChangesEx.GetState(out zero, out num, out zero2, out num2);
				base.ThrowIfError("Unable to get ICS state.", state);
				stateIdsetGiven = new byte[num];
				if (num > 0)
				{
					Marshal.Copy(zero, stateIdsetGiven, 0, num);
				}
				stateCnsetSeen = new byte[num2];
				if (num2 > 0)
				{
					Marshal.Copy(zero2, stateCnsetSeen, 0, num2);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000197B4 File Offset: 0x000179B4
		protected override int GetBlocks(out SafeExLinkedMemoryHandle ppBlocks, out int cBlocks)
		{
			return this.iExchangeExportHierarchyChangesEx.GetBuffers(out ppBlocks, out cBlocks);
		}

		// Token: 0x04000621 RID: 1569
		private SafeExExportHierarchyChangesExHandle iExchangeExportHierarchyChangesEx;
	}
}
