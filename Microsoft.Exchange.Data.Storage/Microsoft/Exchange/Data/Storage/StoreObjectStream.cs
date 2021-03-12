using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AFD RID: 2813
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class StoreObjectStream : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x06006626 RID: 26150 RVA: 0x001B15CC File Offset: 0x001AF7CC
		internal StoreObjectStream(StoreObjectPropertyBag storePropertyBag, NativeStorePropertyDefinition property, PropertyOpenMode streamOpenMode)
		{
			Util.ThrowOnNullArgument(storePropertyBag, "storePropertyBag");
			Util.ThrowOnNullArgument(property, "property");
			this.storePropertyBag = storePropertyBag;
			this.property = property;
			this.openMode = streamOpenMode;
			this.CreateStream();
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06006627 RID: 26151 RVA: 0x001B161C File Offset: 0x001AF81C
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<StoreObjectStream>(this);
		}

		// Token: 0x06006628 RID: 26152 RVA: 0x001B1624 File Offset: 0x001AF824
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x17001C2D RID: 7213
		// (get) Token: 0x06006629 RID: 26153 RVA: 0x001B1639 File Offset: 0x001AF839
		public override bool CanRead
		{
			get
			{
				this.CheckDisposed();
				return true;
			}
		}

		// Token: 0x17001C2E RID: 7214
		// (get) Token: 0x0600662A RID: 26154 RVA: 0x001B1642 File Offset: 0x001AF842
		public override bool CanWrite
		{
			get
			{
				this.CheckDisposed();
				return this.openMode == PropertyOpenMode.Modify || this.openMode == PropertyOpenMode.Create;
			}
		}

		// Token: 0x17001C2F RID: 7215
		// (get) Token: 0x0600662B RID: 26155 RVA: 0x001B165E File Offset: 0x001AF85E
		public override bool CanSeek
		{
			get
			{
				this.CheckDisposed();
				return true;
			}
		}

		// Token: 0x17001C30 RID: 7216
		// (get) Token: 0x0600662C RID: 26156 RVA: 0x001B1667 File Offset: 0x001AF867
		public override long Length
		{
			get
			{
				this.CheckDisposed();
				return this.ActiveStream.Length;
			}
		}

		// Token: 0x17001C31 RID: 7217
		// (get) Token: 0x0600662D RID: 26157 RVA: 0x001B167A File Offset: 0x001AF87A
		// (set) Token: 0x0600662E RID: 26158 RVA: 0x001B168D File Offset: 0x001AF88D
		public override long Position
		{
			get
			{
				this.CheckDisposed();
				return this.ActiveStream.Position;
			}
			set
			{
				this.CheckDisposed();
				Util.ThrowOnArgumentOutOfRangeOnLessThan(value, 0L, "Position");
				this.ActiveStream.Position = value;
			}
		}

		// Token: 0x0600662F RID: 26159 RVA: 0x001B16B0 File Offset: 0x001AF8B0
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			Util.ThrowOnNullArgument(buffer, "buffer");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(offset, 0, "offset");
			Util.ThrowOnArgumentInvalidOnGreaterThan(offset, buffer.Length, "offset");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(count, 0, "count");
			Util.ThrowOnArgumentInvalidOnGreaterThan(offset + count, buffer.Length, "offset + count exceeds buffer size");
			this.CheckNotReadOnly();
			if (count == 0)
			{
				return;
			}
			ExtendedRuleConditionConstraint.ValidateStreamIfApplicable(this.ActiveStream.Position + (long)count, this.property, this.storePropertyBag);
			this.dataChanged = true;
			if (this.cache != null && this.NeedToSwitchToMapiStream(this.cache.Position + (long)count))
			{
				this.OpenMapiStream(true);
			}
			this.ActiveStream.Write(buffer, offset, count);
		}

		// Token: 0x06006630 RID: 26160 RVA: 0x001B1768 File Offset: 0x001AF968
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			Util.ThrowOnNullArgument(buffer, "buffer");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(offset, 0, "offset");
			Util.ThrowOnArgumentInvalidOnGreaterThan(offset, buffer.Length, "offset");
			Util.ThrowOnArgumentOutOfRangeOnLessThan(count, 0, "count");
			Util.ThrowOnArgumentInvalidOnGreaterThan(offset + count, buffer.Length, "offset + count exceeds buffer size");
			return this.ActiveStream.Read(buffer, offset, count);
		}

		// Token: 0x06006631 RID: 26161 RVA: 0x001B17CC File Offset: 0x001AF9CC
		public override void SetLength(long length)
		{
			this.CheckDisposed();
			this.CheckNotReadOnly();
			Util.ThrowOnArgumentOutOfRangeOnLessThan(length, 0L, "length");
			ExtendedRuleConditionConstraint.ValidateStreamIfApplicable(length, this.property, this.storePropertyBag);
			if (this.NeedToSwitchToMapiStream(length))
			{
				this.OpenMapiStream(false);
			}
			this.dataChanged = true;
			this.ActiveStream.SetLength(length);
		}

		// Token: 0x06006632 RID: 26162 RVA: 0x001B1827 File Offset: 0x001AFA27
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed();
			return this.ActiveStream.Seek(offset, origin);
		}

		// Token: 0x06006633 RID: 26163 RVA: 0x001B183C File Offset: 0x001AFA3C
		public override void Flush()
		{
			this.CheckDisposed();
			if (this.mapiStream != null)
			{
				this.mapiStream.Flush();
			}
			else if (this.dataChanged)
			{
				this.FlushCacheIntoPropertyBag();
			}
			this.dataChanged = false;
		}

		// Token: 0x06006634 RID: 26164 RVA: 0x001B1870 File Offset: 0x001AFA70
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
					}
					try
					{
						if (this.cache != null && this.dataChanged)
						{
							this.FlushCacheIntoPropertyBag();
						}
						if (this.ActiveStream != null)
						{
							this.ActiveStream.Dispose();
						}
					}
					finally
					{
						this.OnStreamClose();
						this.cache = null;
						if (this.mapiStream != null)
						{
							this.mapiStream.Dispose();
							this.mapiStream = null;
						}
						this.dataChanged = false;
						this.isClosed = true;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06006635 RID: 26165 RVA: 0x001B191C File Offset: 0x001AFB1C
		private static int GetByteCount(byte[] content)
		{
			int num = 0;
			while (num < content.Length - 1 && (content[num] != 0 || content[num + 1] != 0))
			{
				num += 2;
			}
			return num;
		}

		// Token: 0x06006636 RID: 26166 RVA: 0x001B1946 File Offset: 0x001AFB46
		private static Encoding GetUnicodeEncoding()
		{
			return new UnicodeEncoding(false, false);
		}

		// Token: 0x06006637 RID: 26167 RVA: 0x001B1950 File Offset: 0x001AFB50
		private static PooledMemoryStream CreateExpandableMemoryStream(byte[] bytes)
		{
			PooledMemoryStream pooledMemoryStream;
			if (bytes != null)
			{
				pooledMemoryStream = new PooledMemoryStream(bytes.Length);
				pooledMemoryStream.Write(bytes, 0, bytes.Length);
				pooledMemoryStream.Seek(0L, SeekOrigin.Begin);
			}
			else
			{
				pooledMemoryStream = new PooledMemoryStream(16384);
			}
			return pooledMemoryStream;
		}

		// Token: 0x06006638 RID: 26168 RVA: 0x001B1990 File Offset: 0x001AFB90
		private void CreateStream()
		{
			if ((this.property.PropertyFlags & PropertyFlags.Streamable) == PropertyFlags.None)
			{
				ExTraceGlobals.PropertyBagTracer.TraceDebug<NativeStorePropertyDefinition>(0L, "StoreObjectStream::CreateStream property {0} is not marked as streamable.", this.property);
			}
			if (this.property.MapiPropertyType == PropType.Object)
			{
				this.OpenMapiStream(false);
				return;
			}
			if (this.property.MapiPropertyType != PropType.String && this.property.MapiPropertyType != PropType.Binary)
			{
				throw new InvalidOperationException(ServerStrings.ExPropertyNotStreamable(this.property.ToString()));
			}
			switch (this.openMode)
			{
			case PropertyOpenMode.ReadOnly:
			case PropertyOpenMode.Modify:
			{
				if (!((IDirectPropertyBag)this.storePropertyBag.MemoryPropertyBag).IsLoaded(this.property))
				{
					this.OpenMapiStream(false);
					return;
				}
				object obj = this.storePropertyBag.MemoryPropertyBag.TryGetProperty(this.property);
				PropertyError propertyError = obj as PropertyError;
				if (propertyError == null)
				{
					this.CreateCache(obj);
					return;
				}
				if (PropertyError.IsPropertyValueTooBig(obj))
				{
					this.OpenMapiStream(false);
					return;
				}
				if (propertyError.PropertyErrorCode != PropertyErrorCode.NotFound)
				{
					throw PropertyError.ToException(new PropertyError[]
					{
						propertyError
					});
				}
				if (Array.IndexOf<StorePropertyDefinition>(StoreObjectStream.bodyProperties, this.property) != -1)
				{
					this.OpenMapiStream(false);
					return;
				}
				if (this.openMode != PropertyOpenMode.Modify)
				{
					throw new ObjectNotFoundException(ServerStrings.ExUnableToGetStreamProperty(this.property.Name));
				}
				break;
			}
			case PropertyOpenMode.Create:
				break;
			default:
				return;
			}
			this.CreateCache(null);
			this.dataChanged = true;
		}

		// Token: 0x06006639 RID: 26169 RVA: 0x001B1AF0 File Offset: 0x001AFCF0
		private void OpenMapiStream(bool flushCacheToPosition)
		{
			PropertyOpenMode propertyOpenMode = this.openMode;
			if (propertyOpenMode == PropertyOpenMode.Modify && this.cache != null)
			{
				propertyOpenMode = PropertyOpenMode.Create;
			}
			Stream stream = null;
			try
			{
				ExTraceGlobals.PropertyBagTracer.TraceDebug(0L, "StoreObejctStream::OpenMapiStream. Open MapiStream. property = {0}, localOpenMode = {1}, openMode = {2}, flushCacheToPosition = {3}.", new object[]
				{
					this.property,
					propertyOpenMode,
					this.openMode,
					flushCacheToPosition
				});
				stream = this.storePropertyBag.MapiPropertyBag.OpenPropertyStream(this.property, propertyOpenMode);
				if (this.cache != null && (propertyOpenMode == PropertyOpenMode.Create || propertyOpenMode == PropertyOpenMode.Modify))
				{
					long position = this.cache.Position;
					if (this.cache.Length > 0L)
					{
						this.cache.Position = 0L;
						Util.StreamHandler.CopyStreamData(this.cache, stream, new int?((int)this.cache.Length));
					}
					if (position != this.cache.Length)
					{
						stream.Position = position;
					}
					this.cache.Dispose();
					this.cache = null;
				}
				if (this.openMode != PropertyOpenMode.ReadOnly)
				{
					this.SetPropertyRequiresStreaming();
				}
				this.mapiStream = stream;
				stream = null;
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
				}
			}
		}

		// Token: 0x0600663A RID: 26170 RVA: 0x001B1C20 File Offset: 0x001AFE20
		private bool NeedToSwitchToMapiStream(long streamLocation)
		{
			return this.cache != null && this.openMode != PropertyOpenMode.ReadOnly && streamLocation > (long)this.maximumCacheSize;
		}

		// Token: 0x0600663B RID: 26171 RVA: 0x001B1C40 File Offset: 0x001AFE40
		private void CreateCache(object value)
		{
			if (value == null)
			{
				this.cache = StoreObjectStream.CreateExpandableMemoryStream(null);
			}
			else if (this.property.MapiPropertyType == PropType.Binary)
			{
				this.cache = StoreObjectStream.CreateExpandableMemoryStream((byte[])value);
			}
			else if (this.property.MapiPropertyType == PropType.String)
			{
				byte[] bytes = StoreObjectStream.GetUnicodeEncoding().GetBytes((string)value);
				PooledMemoryStream pooledMemoryStream = StoreObjectStream.CreateExpandableMemoryStream(bytes);
				pooledMemoryStream.Position = 0L;
				this.cache = pooledMemoryStream;
			}
			this.maximumCacheSize = ((32768 > (int)this.cache.Length) ? 32768 : ((int)this.cache.Length));
		}

		// Token: 0x0600663C RID: 26172 RVA: 0x001B1CE8 File Offset: 0x001AFEE8
		private void FlushCacheIntoPropertyBag()
		{
			if (this.storePropertyBag == null)
			{
				return;
			}
			this.storePropertyBag.Load(null);
			byte[] array = this.cache.ToArray();
			if (this.property.MapiPropertyType == PropType.Binary)
			{
				this.storePropertyBag.MemoryPropertyBag[this.property] = array;
				return;
			}
			if (this.property.MapiPropertyType == PropType.String)
			{
				this.storePropertyBag.MemoryPropertyBag[this.property] = StoreObjectStream.GetUnicodeEncoding().GetString(array, 0, StoreObjectStream.GetByteCount(array));
			}
		}

		// Token: 0x0600663D RID: 26173 RVA: 0x001B1D77 File Offset: 0x001AFF77
		private void SetPropertyRequiresStreaming()
		{
			this.storePropertyBag.MemoryPropertyBag.MarkPropertyAsRequireStreamed(this.property);
		}

		// Token: 0x0600663E RID: 26174 RVA: 0x001B1D8F File Offset: 0x001AFF8F
		private void CheckNotReadOnly()
		{
			if (this.openMode == PropertyOpenMode.ReadOnly)
			{
				throw new NotSupportedException();
			}
			if (this.storePropertyBag == null)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600663F RID: 26175 RVA: 0x001B1DAD File Offset: 0x001AFFAD
		private void CheckDisposed()
		{
			if (this.isClosed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x17001C32 RID: 7218
		// (get) Token: 0x06006640 RID: 26176 RVA: 0x001B1DC8 File Offset: 0x001AFFC8
		private Stream ActiveStream
		{
			get
			{
				return this.mapiStream ?? this.cache;
			}
		}

		// Token: 0x06006641 RID: 26177 RVA: 0x001B1DDA File Offset: 0x001AFFDA
		internal void DetachPropertyBag()
		{
			this.storePropertyBag = null;
		}

		// Token: 0x06006642 RID: 26178 RVA: 0x001B1DE3 File Offset: 0x001AFFE3
		private void OnStreamClose()
		{
			if (this.storePropertyBag != null)
			{
				this.storePropertyBag.OnStreamClose(this);
				this.storePropertyBag = null;
			}
		}

		// Token: 0x04003A09 RID: 14857
		private const int StandardMaximumCacheSize = 32768;

		// Token: 0x04003A0A RID: 14858
		private const int InitialCacheSize = 16384;

		// Token: 0x04003A0B RID: 14859
		private static readonly StorePropertyDefinition[] bodyProperties = new StorePropertyDefinition[]
		{
			InternalSchema.TextBody,
			InternalSchema.RtfBody,
			InternalSchema.HtmlBody
		};

		// Token: 0x04003A0C RID: 14860
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04003A0D RID: 14861
		private bool isClosed;

		// Token: 0x04003A0E RID: 14862
		private StoreObjectPropertyBag storePropertyBag;

		// Token: 0x04003A0F RID: 14863
		private PropertyOpenMode openMode;

		// Token: 0x04003A10 RID: 14864
		private NativeStorePropertyDefinition property;

		// Token: 0x04003A11 RID: 14865
		private PooledMemoryStream cache;

		// Token: 0x04003A12 RID: 14866
		private Stream mapiStream;

		// Token: 0x04003A13 RID: 14867
		private int maximumCacheSize;

		// Token: 0x04003A14 RID: 14868
		private bool dataChanged;
	}
}
