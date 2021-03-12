using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001B7 RID: 439
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MapiFastTransferStream : MapiUnk
	{
		// Token: 0x0600064D RID: 1613 RVA: 0x00014874 File Offset: 0x00012A74
		internal MapiFastTransferStream(SafeExFastTransferStreamHandle iMAPIFxStream, MapiFastTransferStreamMode mode, MapiStore mapiStore) : base(iMAPIFxStream, null, mapiStore)
		{
			this.mode = mode;
			this.iMAPIFxStream = iMAPIFxStream;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001488D File Offset: 0x00012A8D
		protected override void MapiInternalDispose()
		{
			this.iMAPIFxStream = null;
			base.MapiInternalDispose();
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001489C File Offset: 0x00012A9C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiFastTransferStream>(this);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000148A4 File Offset: 0x00012AA4
		public unsafe void Configure(ICollection<PropValue> properties)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			if (properties == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("properties");
			}
			if (properties.Count <= 0)
			{
				throw MapiExceptionHelper.ArgumentOutOfRangeException("properties", "pva must contain at least 1 element");
			}
			base.LockStore();
			try
			{
				int num = 0;
				foreach (PropValue propValue in properties)
				{
					num += propValue.GetBytesToMarshal();
				}
				try
				{
					fixed (byte* ptr = new byte[num])
					{
						PropValue.MarshalToNative(properties, ptr);
						int num2 = this.iMAPIFxStream.Configure(properties.Count, (SPropValue*)ptr);
						if (num2 != 0)
						{
							base.ThrowIfError("Unable to configure the object.", num2);
						}
					}
				}
				finally
				{
					byte* ptr = null;
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000149A0 File Offset: 0x00012BA0
		public byte[] Download()
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			if (this.mode != MapiFastTransferStreamMode.Download)
			{
				throw MapiExceptionHelper.NotSupportedException("Download is not supported because object was created for upload.");
			}
			base.LockStore();
			byte[] result;
			try
			{
				SafeExMemoryHandle safeExMemoryHandle = null;
				byte[] array = null;
				try
				{
					int num = 0;
					int num2 = this.iMAPIFxStream.Download(out num, out safeExMemoryHandle);
					if (num2 != 0)
					{
						base.ThrowIfError("Unable to download data.", num2);
					}
					if (num > 0 && safeExMemoryHandle != null)
					{
						array = new byte[num];
						safeExMemoryHandle.CopyTo(array, 0, num);
					}
				}
				finally
				{
					if (safeExMemoryHandle != null)
					{
						safeExMemoryHandle.Dispose();
					}
				}
				result = array;
			}
			finally
			{
				base.UnlockStore();
			}
			return result;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00014A48 File Offset: 0x00012C48
		public void Upload(ArraySegment<byte> buffer)
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			if (this.mode != MapiFastTransferStreamMode.Upload)
			{
				throw MapiExceptionHelper.NotSupportedException("Upload is not supported because object was created for download.");
			}
			if (buffer.Count == 0)
			{
				throw MapiExceptionHelper.ArgumentException("buffer", "Buffer is empty.");
			}
			base.LockStore();
			try
			{
				int num = this.iMAPIFxStream.Upload(buffer);
				if (num != 0)
				{
					base.ThrowIfError("Unable to upload data.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00014AC8 File Offset: 0x00012CC8
		public void Flush()
		{
			base.CheckDisposed();
			base.BlockExternalObjectCheck();
			if (this.mode != MapiFastTransferStreamMode.Upload)
			{
				throw MapiExceptionHelper.NotSupportedException("Flush is not supported because object was created for download.");
			}
			base.LockStore();
			try
			{
				int num = this.iMAPIFxStream.Flush();
				if (num != 0)
				{
					base.ThrowIfError("Unable to flush data.", num);
				}
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x040005A6 RID: 1446
		private SafeExFastTransferStreamHandle iMAPIFxStream;

		// Token: 0x040005A7 RID: 1447
		private MapiFastTransferStreamMode mode;

		// Token: 0x020001B8 RID: 440
		public static class WellKnownIds
		{
			// Token: 0x040005A8 RID: 1448
			public static readonly Guid InferenceLog = new Guid("8ebdc484-475b-4d27-aaad-647e1cac4144");
		}
	}
}
