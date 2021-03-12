using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200007C RID: 124
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiCollector : MapiCollectorBase
	{
		// Token: 0x06000341 RID: 833 RVA: 0x0000E948 File Offset: 0x0000CB48
		internal MapiCollector(IExImportContentsChanges iExchangeImportContentsChanges, MapiStore mapiStore) : base(iExchangeImportContentsChanges, mapiStore)
		{
			this.iExchangeImportContentsChanges = iExchangeImportContentsChanges;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000E959 File Offset: 0x0000CB59
		protected override void MapiInternalDispose()
		{
			this.iExchangeImportContentsChanges = null;
			base.MapiInternalDispose();
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000E968 File Offset: 0x0000CB68
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiCollector>(this);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000E970 File Offset: 0x0000CB70
		public void Config(Stream stream, CollectorConfigFlags flags)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				if (stream == null)
				{
					throw MapiExceptionHelper.ArgumentNullException("stream");
				}
				MapiIStream iStream = new MapiIStream(stream);
				int num = this.iExchangeImportContentsChanges.Config(iStream, (int)flags);
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

		// Token: 0x06000345 RID: 837 RVA: 0x0000E9D8 File Offset: 0x0000CBD8
		public void UpdateState(Stream stream)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				MapiIStream iStream = null;
				if (stream != null)
				{
					iStream = new MapiIStream(stream);
				}
				int num = this.iExchangeImportContentsChanges.UpdateState(iStream);
				if (num != 0)
				{
					base.ThrowIfErrorOrWarning("Unable to update collector state.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000EA34 File Offset: 0x0000CC34
		public void ImportMessageMove(PropValue sourceFolderKey, PropValue sourceMessageKey, PropValue changeListKey, PropValue destMessageKey, PropValue changeNumberKey)
		{
			base.CheckDisposed();
			base.LockStore();
			try
			{
				int num = base.InternalImportMessageMove(sourceFolderKey, sourceMessageKey, changeListKey, destMessageKey, changeNumberKey);
				if (num != 0)
				{
					base.ThrowIfErrorOrWarning("Unable to import message move.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000EA84 File Offset: 0x0000CC84
		public MapiMessage ImportMessageChange(PropValue[] propValues, ImportMessageChangeFlags importMessageChangeFlags)
		{
			base.CheckDisposed();
			base.LockStore();
			MapiMessage result;
			try
			{
				MapiMessage mapiMessage = null;
				bool flag = false;
				try
				{
					int num = base.InternalImportMessageChange(propValues, importMessageChangeFlags, out mapiMessage);
					if (num != 0)
					{
						base.ThrowIfErrorOrWarning("Unable to import message change.", num);
					}
					flag = true;
					result = mapiMessage;
				}
				finally
				{
					if (!flag && mapiMessage != null)
					{
						mapiMessage.Dispose();
						mapiMessage = null;
					}
				}
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x040004D0 RID: 1232
		private IExImportContentsChanges iExchangeImportContentsChanges;
	}
}
