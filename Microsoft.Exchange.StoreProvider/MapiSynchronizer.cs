using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001E8 RID: 488
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiSynchronizer : MapiUnk
	{
		// Token: 0x060007F5 RID: 2037 RVA: 0x00021052 File Offset: 0x0001F252
		internal MapiSynchronizer(IExExportChanges iExchangeExportChanges, MapiStore mapiStore) : base(iExchangeExportChanges, null, mapiStore)
		{
			this.iExchangeExportChanges = iExchangeExportChanges;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00021064 File Offset: 0x0001F264
		protected override void MapiInternalDispose()
		{
			this.iExchangeExportChanges = null;
			base.MapiInternalDispose();
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00021073 File Offset: 0x0001F273
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiSynchronizer>(this);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0002107C File Offset: 0x0001F27C
		public unsafe void Config(Stream stream, SyncConfigFlags flags, object mapiCollector, Restriction restriction, PropTag[] propsInclude, PropTag[] propsExclude)
		{
			base.CheckDisposed();
			if (mapiCollector == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("mapiCollector");
			}
			IntPtr iCollector = IntPtr.Zero;
			if (mapiCollector is MapiCollectorBase)
			{
				iCollector = ((MapiCollectorBase)mapiCollector).ExchangeCollector;
			}
			else if (mapiCollector is MapiHierarchyCollectorBase)
			{
				iCollector = ((MapiHierarchyCollectorBase)mapiCollector).ExchangeCollector;
			}
			else
			{
				if (!(mapiCollector is MapiManifestCollector))
				{
					throw MapiExceptionHelper.ArgumentException("mapiCollector", "argument is not a MapiCollector");
				}
				if ((flags & SyncConfigFlags.OnlySpecifiedProps) == SyncConfigFlags.None)
				{
					throw MapiExceptionHelper.ArgumentException("mapiCollector", "When MapiManifestCollector is used, SyncConfigFlags.OnlySpecifiedProps must be specified.");
				}
				iCollector = ((MapiManifestCollector)mapiCollector).ExchangeCollector;
			}
			base.LockStore();
			try
			{
				int num = 0;
				if (stream == null)
				{
					throw MapiExceptionHelper.ArgumentNullException("stream");
				}
				stream.Seek(0L, SeekOrigin.Begin);
				MapiIStream iStream = new MapiIStream(stream);
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
							num = this.iExchangeExportChanges.Config(iStream, (int)flags, iCollector, ptr3, PropTagHelper.SPropTagArray(propsInclude), PropTagHelper.SPropTagArray(propsExclude), 0);
							goto IL_148;
						}
					}
					finally
					{
						byte* ptr = null;
					}
				}
				num = this.iExchangeExportChanges.Config(iStream, (int)flags, iCollector, null, PropTagHelper.SPropTagArray(propsInclude), PropTagHelper.SPropTagArray(propsExclude), 0);
				IL_148:
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

		// Token: 0x060007F9 RID: 2041 RVA: 0x00021208 File Offset: 0x0001F408
		public int Synchronize()
		{
			base.CheckDisposed();
			base.LockStore();
			int result;
			try
			{
				int num = 0;
				int num2 = 0;
				int num3 = this.iExchangeExportChanges.Synchronize(out num, out num2);
				if (num3 != 0)
				{
					base.ThrowIfError("Synchronization failued.", num3);
				}
				result = num3;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00021260 File Offset: 0x0001F460
		public void UpdateState(Stream stream)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				MapiIStream iStream = null;
				if (stream != null)
				{
					stream.Seek(0L, SeekOrigin.Begin);
					iStream = new MapiIStream(stream);
				}
				int num = this.iExchangeExportChanges.UpdateState(iStream);
				if (num != 0)
				{
					base.ThrowIfError("Unable to update ICS synchronizer.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x040006A2 RID: 1698
		private IExExportChanges iExchangeExportChanges;
	}
}
