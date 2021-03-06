using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001C9 RID: 457
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiHierarchyCollectorEx : MapiHierarchyCollectorBase
	{
		// Token: 0x060006C4 RID: 1732 RVA: 0x00018D84 File Offset: 0x00016F84
		internal MapiHierarchyCollectorEx(SafeExImportHierarchyChanges2Handle iExchangeImportHierarchyChanges2, MapiStore mapiStore, byte[] stateIdsetGiven, byte[] stateCnsetSeen, CollectorConfigFlags flags) : base(iExchangeImportHierarchyChanges2, mapiStore)
		{
			if (stateIdsetGiven == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("stateIdsetGiven");
			}
			if (stateCnsetSeen == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("stateCnsetSeen");
			}
			base.LockStore();
			try
			{
				this.iExchangeImportHierarchyChanges2 = iExchangeImportHierarchyChanges2;
				int num = this.iExchangeImportHierarchyChanges2.ConfigEx(stateIdsetGiven, stateIdsetGiven.Length, stateCnsetSeen, stateCnsetSeen.Length, (int)flags);
				if (num != 0)
				{
					base.ThrowIfErrorOrWarning("Unable to configure ICS collector.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00018E04 File Offset: 0x00017004
		protected override void MapiInternalDispose()
		{
			this.iExchangeImportHierarchyChanges2 = null;
			base.MapiInternalDispose();
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00018E13 File Offset: 0x00017013
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiHierarchyCollectorEx>(this);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00018E1C File Offset: 0x0001701C
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
				int num3 = this.iExchangeImportHierarchyChanges2.UpdateStateEx(out zero, out num, out zero2, out num2);
				if (num3 != 0)
				{
					base.ThrowIfErrorOrWarning("Unable to get collector state.", num3);
				}
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

		// Token: 0x04000616 RID: 1558
		private SafeExImportHierarchyChanges2Handle iExchangeImportHierarchyChanges2;
	}
}
