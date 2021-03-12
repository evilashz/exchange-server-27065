﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001D3 RID: 467
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiManifestEx : MapiUnk
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x0001A8A0 File Offset: 0x00018AA0
		internal unsafe MapiManifestEx(SafeExExportManifestExHandle iExchangeExportManifestEx, MapiStore mapiStore, SyncConfigFlags flags, Restriction restriction, byte[] stateIdsetGiven, byte[] stateCnsetSeen, byte[] stateCnsetSeenFAI, byte[] stateCnsetRead, IMapiManifestExCallback iMapiManifestCallback, ICollection<PropTag> tags) : base(iExchangeExportManifestEx, null, mapiStore)
		{
			if (iMapiManifestCallback == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("iMapiManifestCallback");
			}
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
			if ((flags & SyncConfigFlags.Conversations) == SyncConfigFlags.None && (flags & SyncConfigFlags.Associated) == SyncConfigFlags.None && (flags & SyncConfigFlags.Normal) == SyncConfigFlags.None)
			{
				throw MapiExceptionHelper.ArgumentException("flags", "Need SyncConfigFlags.Normal, SyncConfigFlags.Associated or both.");
			}
			if ((flags & SyncConfigFlags.Conversations) != SyncConfigFlags.None && (flags & (SyncConfigFlags.Associated | SyncConfigFlags.ReevaluateOnRestrictionChange)) != SyncConfigFlags.None)
			{
				throw MapiExceptionHelper.ArgumentException("flags", "SyncConfigFlags.Conversations is not compatible with other specified flags.");
			}
			if ((flags & SyncConfigFlags.Conversations) != SyncConfigFlags.None && restriction != null)
			{
				throw MapiExceptionHelper.ArgumentException("flags", "SyncConfigFlags.Conversations is not compatible with restriction.");
			}
			if (tags != null)
			{
				foreach (PropTag propTag in tags)
				{
					if (propTag == PropTag.MessageAttachments || propTag == PropTag.MessageRecipients)
					{
						throw MapiExceptionHelper.ArgumentException("tags", "Cannot request PropTag.MessageAttachments or PropTag.MessageRecipients");
					}
				}
			}
			base.LockStore();
			try
			{
				this.iExchangeExportManifestEx = iExchangeExportManifestEx;
				this.manifestCallbackHelper = new ManifestExCallbackHelper();
				int hr = 0;
				this.iMapiManifestCallback = iMapiManifestCallback;
				if (restriction != null)
				{
					int num = 4;
					num += restriction.GetBytesToMarshal();
					try
					{
						fixed (byte* ptr = new byte[num])
						{
							SRestriction* ptr2 = null;
							byte* ptr3 = ptr;
							ptr2 = (SRestriction*)ptr;
							ptr3 += (Marshal.SizeOf(typeof(SRestriction)) + 7 & -8);
							restriction.MarshalToNative(ptr2, ref ptr3);
							hr = this.iExchangeExportManifestEx.Config(stateIdsetGiven, stateIdsetGiven.Length, stateCnsetSeen, stateCnsetSeen.Length, stateCnsetSeenFAI, stateCnsetSeenFAI.Length, stateCnsetRead, stateCnsetRead.Length, flags, this.manifestCallbackHelper, ptr2, (tags != null && tags.Count > 0) ? PropTagHelper.SPropTagArray(tags) : null);
							goto IL_21A;
						}
					}
					finally
					{
						byte* ptr = null;
					}
				}
				hr = this.iExchangeExportManifestEx.Config(stateIdsetGiven, stateIdsetGiven.Length, stateCnsetSeen, stateCnsetSeen.Length, stateCnsetSeenFAI, stateCnsetSeenFAI.Length, stateCnsetRead, stateCnsetRead.Length, flags, this.manifestCallbackHelper, null, (tags != null && tags.Count > 0) ? PropTagHelper.SPropTagArray(tags) : null);
				IL_21A:
				base.ThrowIfError("Unable to configure MapiManifestEx ICS synchronizer.", hr);
				this.done = false;
				this.synchronizationDone = false;
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001AB38 File Offset: 0x00018D38
		protected override void MapiInternalDispose()
		{
			this.iExchangeExportManifestEx = null;
			base.MapiInternalDispose();
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001AB47 File Offset: 0x00018D47
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiManifestEx>(this);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001AB50 File Offset: 0x00018D50
		public ManifestStatus Synchronize()
		{
			base.CheckDisposed();
			ManifestStatus manifestStatus = ManifestStatus.Done;
			base.LockStore();
			ManifestStatus result;
			try
			{
				if (!this.done && !this.synchronizationDone)
				{
					int num = 264224;
					while (264224 == num)
					{
						manifestStatus = this.manifestCallbackHelper.DoCallbacks(this.iMapiManifestCallback, new ManifestCallbackQueue<IMapiManifestExCallback>[]
						{
							this.manifestCallbackHelper.Changes,
							this.manifestCallbackHelper.Deletes
						});
						if (manifestStatus != ManifestStatus.Done)
						{
							break;
						}
						num = this.iExchangeExportManifestEx.Synchronize(0);
						if (num != 0 && num != 264224)
						{
							base.ThrowIfError("Unable to synchronize manifest.", num);
						}
						if (num == 0)
						{
							this.synchronizationDone = true;
						}
					}
				}
				if (manifestStatus == ManifestStatus.Done)
				{
					manifestStatus = this.manifestCallbackHelper.DoCallbacks(this.iMapiManifestCallback, new ManifestCallbackQueue<IMapiManifestExCallback>[]
					{
						this.manifestCallbackHelper.Changes,
						this.manifestCallbackHelper.Deletes
					});
				}
				if (manifestStatus == ManifestStatus.Done)
				{
					this.done = true;
					manifestStatus = this.manifestCallbackHelper.DoCallbacks(this.iMapiManifestCallback, new ManifestCallbackQueue<IMapiManifestExCallback>[]
					{
						this.manifestCallbackHelper.Reads
					});
				}
				result = manifestStatus;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001AC80 File Offset: 0x00018E80
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
				int state = this.iExchangeExportManifestEx.GetState(out zero, out num, out zero2, out num2, out zero3, out num3, out zero4, out num4);
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

		// Token: 0x04000640 RID: 1600
		private SafeExExportManifestExHandle iExchangeExportManifestEx;

		// Token: 0x04000641 RID: 1601
		private IMapiManifestExCallback iMapiManifestCallback;

		// Token: 0x04000642 RID: 1602
		private ManifestExCallbackHelper manifestCallbackHelper;

		// Token: 0x04000643 RID: 1603
		private bool done;

		// Token: 0x04000644 RID: 1604
		private bool synchronizationDone;
	}
}
