using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AF1 RID: 2801
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FolderPropertyStream : LockableStream, IDisposeTrackable, IDisposable
	{
		// Token: 0x060065B2 RID: 26034 RVA: 0x001B0668 File Offset: 0x001AE868
		internal FolderPropertyStream(MapiPropertyBag mapiPropertyBag, NativeStorePropertyDefinition property, PropertyOpenMode streamOpenMode)
		{
			Util.ThrowOnNullArgument(mapiPropertyBag, "mapiPropertyBag");
			Util.ThrowOnNullArgument(property, "property");
			EnumValidator.ThrowIfInvalid<PropertyOpenMode>(streamOpenMode, "streamOpenMode");
			this.mapiPropertyBag = mapiPropertyBag;
			this.property = property;
			this.openMode = streamOpenMode;
			this.CreateStream();
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060065B3 RID: 26035 RVA: 0x001B06C3 File Offset: 0x001AE8C3
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FolderPropertyStream>(this);
		}

		// Token: 0x060065B4 RID: 26036 RVA: 0x001B06CB File Offset: 0x001AE8CB
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x17001C10 RID: 7184
		// (get) Token: 0x060065B5 RID: 26037 RVA: 0x001B06E0 File Offset: 0x001AE8E0
		public override bool CanRead
		{
			get
			{
				this.CheckDisposed();
				return true;
			}
		}

		// Token: 0x17001C11 RID: 7185
		// (get) Token: 0x060065B6 RID: 26038 RVA: 0x001B06E9 File Offset: 0x001AE8E9
		public override bool CanWrite
		{
			get
			{
				this.CheckDisposed();
				return this.openMode == PropertyOpenMode.Modify || this.openMode == PropertyOpenMode.Create;
			}
		}

		// Token: 0x17001C12 RID: 7186
		// (get) Token: 0x060065B7 RID: 26039 RVA: 0x001B0705 File Offset: 0x001AE905
		public override bool CanSeek
		{
			get
			{
				this.CheckDisposed();
				return true;
			}
		}

		// Token: 0x17001C13 RID: 7187
		// (get) Token: 0x060065B8 RID: 26040 RVA: 0x001B070E File Offset: 0x001AE90E
		public override long Length
		{
			get
			{
				this.CheckDisposed();
				return this.mapiStream.Length;
			}
		}

		// Token: 0x17001C14 RID: 7188
		// (get) Token: 0x060065B9 RID: 26041 RVA: 0x001B0721 File Offset: 0x001AE921
		// (set) Token: 0x060065BA RID: 26042 RVA: 0x001B0734 File Offset: 0x001AE934
		public override long Position
		{
			get
			{
				this.CheckDisposed();
				return this.mapiStream.Position;
			}
			set
			{
				this.CheckDisposed();
				Util.ThrowOnArgumentOutOfRangeOnLessThan(value, 0L, "Position");
				this.mapiStream.Position = value;
			}
		}

		// Token: 0x060065BB RID: 26043 RVA: 0x001B0758 File Offset: 0x001AE958
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			Util.ThrowOnNullArgument(buffer, "buffer");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(offset, 0, "offset");
			Util.ThrowOnArgumentInvalidOnGreaterThan(offset, buffer.Length, "offset");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(count, 0, "count");
			Util.ThrowOnArgumentInvalidOnGreaterThan(offset + count, buffer.Length, "count");
			if (this.openMode == PropertyOpenMode.ReadOnly)
			{
				throw new NotSupportedException();
			}
			if (count == 0)
			{
				return;
			}
			this.mapiStream.Write(buffer, offset, count);
		}

		// Token: 0x060065BC RID: 26044 RVA: 0x001B07CC File Offset: 0x001AE9CC
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			Util.ThrowOnNullArgument(buffer, "buffer");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(offset, 0, "offset");
			Util.ThrowOnArgumentInvalidOnGreaterThan(offset, buffer.Length, "offset");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(count, 0, "count");
			Util.ThrowOnArgumentInvalidOnGreaterThan(offset + count, buffer.Length, "count");
			if (count == 0)
			{
				return 0;
			}
			return this.mapiStream.Read(buffer, offset, count);
		}

		// Token: 0x060065BD RID: 26045 RVA: 0x001B0833 File Offset: 0x001AEA33
		public override void SetLength(long length)
		{
			this.CheckDisposed();
			if (this.openMode == PropertyOpenMode.ReadOnly)
			{
				throw new NotSupportedException();
			}
			Util.ThrowOnArgumentOutOfRangeOnLessThan(length, 0L, "length");
			this.mapiStream.SetLength(length);
		}

		// Token: 0x060065BE RID: 26046 RVA: 0x001B0862 File Offset: 0x001AEA62
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed();
			return this.mapiStream.Seek(offset, origin);
		}

		// Token: 0x060065BF RID: 26047 RVA: 0x001B0877 File Offset: 0x001AEA77
		public override void Flush()
		{
			this.CheckDisposed();
			this.mapiStream.Flush();
		}

		// Token: 0x060065C0 RID: 26048 RVA: 0x001B088C File Offset: 0x001AEA8C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					try
					{
						if (this.disposeTracker != null)
						{
							this.disposeTracker.Dispose();
						}
						if (this.mapiStream != null)
						{
							this.mapiStream.Dispose();
							this.mapiStream = null;
						}
					}
					finally
					{
						this.mapiStream = null;
						this.isClosed = true;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060065C1 RID: 26049 RVA: 0x001B0900 File Offset: 0x001AEB00
		public override void LockRegion(long offset, long cb, int lockType)
		{
			this.CheckDisposed();
			Util.ThrowOnArgumentOutOfRangeOnLessThan(offset, 0L, "offset");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(cb, 0L, "cb");
			this.mapiStream.LockRegion(offset, cb, lockType);
		}

		// Token: 0x060065C2 RID: 26050 RVA: 0x001B0930 File Offset: 0x001AEB30
		public override void UnlockRegion(long offset, long cb, int lockType)
		{
			this.CheckDisposed();
			Util.ThrowOnArgumentOutOfRangeOnLessThan(offset, 0L, "offset");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(cb, 0L, "cb");
			this.mapiStream.UnlockRegion(offset, cb, lockType);
		}

		// Token: 0x060065C3 RID: 26051 RVA: 0x001B0960 File Offset: 0x001AEB60
		private void CreateStream()
		{
			if (this.property.MapiPropertyType != PropType.String && this.property.MapiPropertyType != PropType.Binary)
			{
				throw new InvalidOperationException(ServerStrings.ExPropertyNotStreamable(this.property.ToString()));
			}
			Stream stream = null;
			try
			{
				ExTraceGlobals.PropertyBagTracer.TraceDebug<NativeStorePropertyDefinition, PropertyOpenMode>(0L, "FolderPropertyStream::CreateStream property = {0}, openMode = {1}.", this.property, this.openMode);
				if ((this.property.PropertyFlags & PropertyFlags.Streamable) == PropertyFlags.None)
				{
					ExTraceGlobals.PropertyBagTracer.TraceDebug<NativeStorePropertyDefinition>(0L, "FolderPropertyStream::CreateStream property {0} is not marked as streamable.", this.property);
				}
				stream = this.mapiPropertyBag.OpenPropertyStream(this.property, this.openMode, false);
			}
			catch (ObjectNotFoundException)
			{
				if (this.openMode != PropertyOpenMode.Modify)
				{
					throw new ObjectNotFoundException(ServerStrings.ExUnableToGetStreamProperty(this.property.Name));
				}
				stream = this.mapiPropertyBag.OpenPropertyStream(this.property, PropertyOpenMode.Create, false);
			}
			if (stream == null)
			{
				throw new InvalidOperationException("Received null from a call to OpenPropertyStream!");
			}
			this.mapiStream = (stream as MapiStreamWrapper);
			if (this.mapiStream == null)
			{
				throw new InvalidOperationException("MapiPropertyBag.OpenPropertyStream did not return a MapiStreamWrapper!");
			}
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x001B0A80 File Offset: 0x001AEC80
		private void CheckDisposed()
		{
			if (this.isClosed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x040039D9 RID: 14809
		private readonly MapiPropertyBag mapiPropertyBag;

		// Token: 0x040039DA RID: 14810
		private readonly PropertyOpenMode openMode;

		// Token: 0x040039DB RID: 14811
		private readonly NativeStorePropertyDefinition property;

		// Token: 0x040039DC RID: 14812
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040039DD RID: 14813
		private bool isClosed;

		// Token: 0x040039DE RID: 14814
		private MapiStreamWrapper mapiStream;
	}
}
