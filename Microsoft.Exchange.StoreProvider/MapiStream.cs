using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001E6 RID: 486
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiStream : Stream, IForceReportDisposeTrackable, IDisposeTrackable, IDisposable
	{
		// Token: 0x060007DE RID: 2014 RVA: 0x00020810 File Offset: 0x0001EA10
		internal MapiStream(IExMapiStream iStream, MapiStore mapiStore)
		{
			if (iStream == null || iStream.IsInvalid)
			{
				throw MapiExceptionHelper.ArgumentException("iStream", "Unable to create MapiStream object with null or invalid interface handle.");
			}
			if (ComponentTrace<MapiNetTags>.CheckEnabled(25))
			{
				ComponentTrace<MapiNetTags>.Trace<string>(11874, 25, (long)this.GetHashCode(), "MapiStream.MapiStream: this={0}", TraceUtils.MakeHash(this));
			}
			this.iStream = iStream;
			this.childRef = null;
			this.mapiStore = mapiStore;
			if (this.mapiStore != null)
			{
				this.childRef = mapiStore.AddChildReference(this);
			}
			this.disposeTracker = this.GetDisposeTracker();
			this.disposed = false;
			this.dirty = false;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x000208A9 File Offset: 0x0001EAA9
		internal void CheckDisposed()
		{
			if (this.disposed)
			{
				throw MapiExceptionHelper.ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000208C4 File Offset: 0x0001EAC4
		internal void LockStore()
		{
			if (this.mapiStore != null)
			{
				this.mapiStore.LockConnection();
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000208D9 File Offset: 0x0001EAD9
		internal void UnlockStore()
		{
			if (this.mapiStore != null)
			{
				this.mapiStore.UnlockConnection();
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x000208F0 File Offset: 0x0001EAF0
		public override bool CanRead
		{
			get
			{
				this.CheckDisposed();
				this.LockStore();
				bool result;
				try
				{
					result = true;
				}
				finally
				{
					this.UnlockStore();
				}
				return result;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00020928 File Offset: 0x0001EB28
		public override bool CanSeek
		{
			get
			{
				this.CheckDisposed();
				this.LockStore();
				bool result;
				try
				{
					result = true;
				}
				finally
				{
					this.UnlockStore();
				}
				return result;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00020960 File Offset: 0x0001EB60
		public override bool CanWrite
		{
			get
			{
				this.CheckDisposed();
				this.LockStore();
				bool result;
				try
				{
					result = true;
				}
				finally
				{
					this.UnlockStore();
				}
				return result;
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00020998 File Offset: 0x0001EB98
		public override void Flush()
		{
			this.CheckDisposed();
			this.LockStore();
			try
			{
				if (this.dirty)
				{
					int num = this.iStream.Commit(0);
					if (num != 0)
					{
						MapiExceptionHelper.ThrowIfError("Unable to flush stream.", num, this.iStream, this.LastLowLevelException);
					}
					this.dirty = false;
				}
			}
			finally
			{
				this.UnlockStore();
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00020A00 File Offset: 0x0001EC00
		public override long Length
		{
			get
			{
				this.CheckDisposed();
				this.LockStore();
				long cbSize;
				try
				{
					STATSTG statstg;
					int num = this.iStream.Stat(out statstg, 0);
					if (num != 0)
					{
						MapiExceptionHelper.ThrowIfError("Unable to get Length of stream.", num, this.iStream, this.LastLowLevelException);
					}
					cbSize = statstg.cbSize;
				}
				finally
				{
					this.UnlockStore();
				}
				return cbSize;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00020A64 File Offset: 0x0001EC64
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x00020AC4 File Offset: 0x0001ECC4
		public override long Position
		{
			get
			{
				this.CheckDisposed();
				this.LockStore();
				long result;
				try
				{
					long num2;
					int num = this.iStream.Seek(0L, 1, out num2);
					if (num != 0)
					{
						MapiExceptionHelper.ThrowIfError("Unable to get Position of stream.", num, this.iStream, this.LastLowLevelException);
					}
					result = num2;
				}
				finally
				{
					this.UnlockStore();
				}
				return result;
			}
			set
			{
				this.CheckDisposed();
				this.LockStore();
				try
				{
					long num2;
					int num = this.iStream.Seek(value, 0, out num2);
					if (num != 0)
					{
						MapiExceptionHelper.ThrowIfError("Unable to set Position of stream.", num, this.iStream, this.LastLowLevelException);
					}
				}
				finally
				{
					this.UnlockStore();
				}
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00020B20 File Offset: 0x0001ED20
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw MapiExceptionHelper.ArgumentOutOfRangeException("offset", "cannot be negative.");
			}
			if (count < 0)
			{
				throw MapiExceptionHelper.ArgumentOutOfRangeException("count", "cannot be negative.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count == 0)
			{
				return 0;
			}
			this.LockStore();
			int result;
			try
			{
				uint num;
				int hresult = this.iStream.Read(buffer, checked((uint)count), out num);
				MapiExceptionHelper.ThrowIfError("Unable to Read from stream.", hresult, this.iStream, this.LastLowLevelException);
				if (ComponentTrace<MapiNetTags>.CheckEnabled(25))
				{
					ComponentTrace<MapiNetTags>.Trace<int, int, uint>(12386, 25, (long)this.GetHashCode(), "MapiStream.Read: offset={0}, count={1}, result={2}", offset, count, num);
				}
				result = checked((int)num);
			}
			finally
			{
				this.UnlockStore();
			}
			return result;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00020BF0 File Offset: 0x0001EDF0
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed();
			this.LockStore();
			long result;
			try
			{
				long num2;
				int num = this.iStream.Seek(offset, (int)origin, out num2);
				if (num != 0)
				{
					MapiExceptionHelper.ThrowIfError("Unable to Seek to stream position.", num, this.iStream, this.LastLowLevelException);
				}
				long num3 = num2;
				if (ComponentTrace<MapiNetTags>.CheckEnabled(25))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string, string>(10338, 25, (long)this.GetHashCode(), "MapiStream.Seek: offset={0}, origin={1}, result={2}", offset.ToString(), origin.ToString(), num3.ToString());
				}
				result = num3;
			}
			finally
			{
				this.UnlockStore();
			}
			return result;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00020C8C File Offset: 0x0001EE8C
		public int LockRegion(long offset, long count, int lockType)
		{
			this.CheckDisposed();
			this.LockStore();
			int result;
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(25))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string, string>(14435, 25, (long)this.GetHashCode(), "MapiStream.LockRegion params: offset={0}, count={0}, lockType={0}", offset.ToString(), count.ToString(), lockType.ToString());
				}
				int num = this.iStream.LockRegion(offset, count, lockType);
				if (num != 0)
				{
					MapiExceptionHelper.ThrowIfError("Unable to LockRegion of stream.", num, this.iStream, this.LastLowLevelException);
				}
				result = num;
			}
			finally
			{
				this.UnlockStore();
			}
			return result;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00020D20 File Offset: 0x0001EF20
		public int UnlockRegion(long offset, long count, int lockType)
		{
			this.CheckDisposed();
			this.LockStore();
			int result;
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(25))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string, string>(14436, 25, (long)this.GetHashCode(), "MapiStream.UnlockRegion params: offset={0}, count={0}, lockType={0}", offset.ToString(), count.ToString(), lockType.ToString());
				}
				int num = this.iStream.UnlockRegion(offset, count, lockType);
				if (num != 0)
				{
					MapiExceptionHelper.ThrowIfError("Unable to UnlockRegion of stream.", num, this.iStream, this.LastLowLevelException);
				}
				result = num;
			}
			finally
			{
				this.UnlockStore();
			}
			return result;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00020DB4 File Offset: 0x0001EFB4
		public override void SetLength(long length)
		{
			this.CheckDisposed();
			this.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(25))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(14434, 25, (long)this.GetHashCode(), "MapiStream.SetLength params: length={0}", length.ToString());
				}
				int num = this.iStream.SetSize(length);
				if (num != 0)
				{
					MapiExceptionHelper.ThrowIfError("Unable to SetLength of stream.", num, this.iStream, this.LastLowLevelException);
				}
				this.dirty = true;
			}
			finally
			{
				this.UnlockStore();
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00020E3C File Offset: 0x0001F03C
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			this.LockStore();
			try
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(25))
				{
					ComponentTrace<MapiNetTags>.Trace<string, string>(9314, 25, (long)this.GetHashCode(), "MapiStream.Write params: offset={0}, count={1}", offset.ToString(), count.ToString());
				}
				if (count > 0)
				{
					if (offset == 0)
					{
						int num2;
						int num = this.iStream.Write(buffer, count, out num2);
						if (num != 0)
						{
							MapiExceptionHelper.ThrowIfError("Unable to Write to stream.", num, this.iStream, this.LastLowLevelException);
						}
					}
					else
					{
						byte[] array = new byte[count];
						Array.Copy(buffer, offset, array, 0, count);
						int num2;
						int num = this.iStream.Write(array, count, out num2);
						if (num != 0)
						{
							MapiExceptionHelper.ThrowIfError("Unable to Write to stream at offset.", num, this.iStream, this.LastLowLevelException);
						}
					}
					this.dirty = true;
				}
			}
			finally
			{
				this.UnlockStore();
			}
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00020F14 File Offset: 0x0001F114
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (ComponentTrace<MapiNetTags>.CheckEnabled(25))
				{
					ComponentTrace<MapiNetTags>.Trace<string>(15970, 25, (long)this.GetHashCode(), "MapiStream.Dispose: this={0}", TraceUtils.MakeHash(this));
				}
				if (!this.disposed)
				{
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
					this.LockStore();
					try
					{
						try
						{
							if (this.iStream != null)
							{
								this.Flush();
							}
						}
						finally
						{
							this.iStream.DisposeIfValid();
							this.iStream = null;
							if (this.childRef != null)
							{
								DisposableRef.RemoveFromList(this.childRef);
								this.childRef.Dispose();
								this.childRef = null;
							}
						}
					}
					finally
					{
						this.UnlockStore();
						this.mapiStore = null;
						this.disposed = true;
						FaultInjectionUtils.FaultInjectionTracer.TraceTest(3110481213U);
					}
				}
				base.Dispose(disposing);
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0002100C File Offset: 0x0001F20C
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MapiStream>(this);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00021014 File Offset: 0x0001F214
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00021029 File Offset: 0x0001F229
		public void ForceLeakReport()
		{
			this.disposeTracker = null;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00021032 File Offset: 0x0001F232
		private Exception LastLowLevelException
		{
			get
			{
				if (this.mapiStore != null)
				{
					return this.mapiStore.LastLowLevelException;
				}
				return null;
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00021049 File Offset: 0x0001F249
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x04000698 RID: 1688
		private IExMapiStream iStream;

		// Token: 0x04000699 RID: 1689
		private MapiStore mapiStore;

		// Token: 0x0400069A RID: 1690
		private DisposableRef childRef;

		// Token: 0x0400069B RID: 1691
		private bool disposed;

		// Token: 0x0400069C RID: 1692
		private bool dirty;

		// Token: 0x0400069D RID: 1693
		private DisposeTracker disposeTracker;

		// Token: 0x020001E7 RID: 487
		internal enum LockType
		{
			// Token: 0x0400069F RID: 1695
			LockWrite = 1,
			// Token: 0x040006A0 RID: 1696
			LockExclusive,
			// Token: 0x040006A1 RID: 1697
			LockOnlyOnce = 4
		}
	}
}
