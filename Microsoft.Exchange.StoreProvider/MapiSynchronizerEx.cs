using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001E9 RID: 489
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MapiSynchronizerEx : MapiSynchronizerExBase
	{
		// Token: 0x060007FB RID: 2043 RVA: 0x000212C4 File Offset: 0x0001F4C4
		internal unsafe MapiSynchronizerEx(SafeExExportContentsChangesExHandle iExchangeExportContentsChangesEx, MapiStore mapiStore, byte[] stateIdsetGiven, byte[] stateCnsetSeen, byte[] stateCnsetSeenFAI, byte[] stateCnsetRead, SyncConfigFlags flags, Restriction restriction, ICollection<PropTag> tagsInclude, ICollection<PropTag> tagsExclude, int fastTransferBlockSize) : base(iExchangeExportContentsChangesEx, mapiStore)
		{
			if (stateIdsetGiven == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("stateIdsetGiven");
			}
			if (stateCnsetSeen == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("stateCnsetSeen");
			}
			if (stateCnsetSeenFAI == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("stateCnsetSeenFAI");
			}
			if (stateCnsetRead == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("stateCnsetRead");
			}
			this.iExchangeExportContentsChangesEx = iExchangeExportContentsChangesEx;
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
							num = this.iExchangeExportContentsChangesEx.Config(stateIdsetGiven, stateIdsetGiven.Length, stateCnsetSeen, stateCnsetSeen.Length, stateCnsetSeenFAI, stateCnsetSeenFAI.Length, stateCnsetRead, stateCnsetRead.Length, flags, ptr3, (tagsInclude != null && tagsInclude.Count > 0) ? PropTagHelper.SPropTagArray(tagsInclude) : null, (tagsExclude != null && tagsExclude.Count > 0) ? PropTagHelper.SPropTagArray(tagsExclude) : null, fastTransferBlockSize);
							goto IL_155;
						}
					}
					finally
					{
						byte* ptr = null;
					}
				}
				num = this.iExchangeExportContentsChangesEx.Config(stateIdsetGiven, stateIdsetGiven.Length, stateCnsetSeen, stateCnsetSeen.Length, stateCnsetSeenFAI, stateCnsetSeenFAI.Length, stateCnsetRead, stateCnsetRead.Length, flags, null, (tagsInclude != null && tagsInclude.Count > 0) ? PropTagHelper.SPropTagArray(tagsInclude) : null, (tagsExclude != null && tagsExclude.Count > 0) ? PropTagHelper.SPropTagArray(tagsExclude) : null, fastTransferBlockSize);
				IL_155:
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

		// Token: 0x060007FC RID: 2044 RVA: 0x00021474 File Offset: 0x0001F674
		protected override void MapiInternalDispose()
		{
			this.iExchangeExportContentsChangesEx = null;
			base.MapiInternalDispose();
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00021483 File Offset: 0x0001F683
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiSynchronizerEx>(this);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002148C File Offset: 0x0001F68C
		public void GetState(out byte[] stateIdsetGiven, out byte[] stateCnsetSeen, out byte[] stateCnsetSeenFAI, out byte[] stateCnsetRead)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				IntPtr zero = IntPtr.Zero;
				IntPtr zero2 = IntPtr.Zero;
				IntPtr zero3 = IntPtr.Zero;
				IntPtr zero4 = IntPtr.Zero;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int state = this.iExchangeExportContentsChangesEx.GetState(out zero, out num, out zero2, out num2, out zero3, out num3, out zero4, out num4);
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
				stateCnsetSeenFAI = new byte[num3];
				if (num3 > 0)
				{
					Marshal.Copy(zero3, stateCnsetSeenFAI, 0, num3);
				}
				stateCnsetRead = new byte[num4];
				if (num4 > 0)
				{
					Marshal.Copy(zero4, stateCnsetRead, 0, num4);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00021574 File Offset: 0x0001F774
		protected override int GetBlocks(out SafeExLinkedMemoryHandle ppBlocks, out int cBlocks)
		{
			return this.iExchangeExportContentsChangesEx.GetBuffers(out ppBlocks, out cBlocks);
		}

		// Token: 0x040006A3 RID: 1699
		private SafeExExportContentsChangesExHandle iExchangeExportContentsChangesEx;
	}
}
