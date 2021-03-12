using System;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001D0 RID: 464
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiManifest : MapiUnk
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x00019C0D File Offset: 0x00017E0D
		internal MapiManifest(IExExportManifest iExchangeExportManifest, MapiStore mapiStore) : base(iExchangeExportManifest, null, mapiStore)
		{
			this.iExchangeExportManifest = iExchangeExportManifest;
			this.checkpointState = new MemoryStream();
			this.finalState = new MemoryStream();
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00019C38 File Offset: 0x00017E38
		protected override void MapiInternalDispose()
		{
			if (this.checkpointState != null)
			{
				this.checkpointState.Dispose();
			}
			if (this.finalState != null)
			{
				this.finalState.Dispose();
			}
			this.iMapiManifestCallback = null;
			this.manifestCallbackHelper = null;
			this.checkpointState = null;
			this.finalState = null;
			this.iExchangeExportManifest = null;
			base.MapiInternalDispose();
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00019C94 File Offset: 0x00017E94
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiManifest>(this);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00019C9C File Offset: 0x00017E9C
		internal static SyncConfigFlags ConvertManifestFlagsToSyncFlags(ManifestConfigFlags flags)
		{
			SyncConfigFlags syncConfigFlags = SyncConfigFlags.None;
			if ((flags & ManifestConfigFlags.Associated) == ManifestConfigFlags.Associated)
			{
				syncConfigFlags |= SyncConfigFlags.Associated;
			}
			if ((flags & ManifestConfigFlags.Normal) == ManifestConfigFlags.Normal)
			{
				syncConfigFlags |= SyncConfigFlags.Normal;
			}
			if ((flags & ManifestConfigFlags.NoChanges) == ManifestConfigFlags.NoChanges)
			{
				syncConfigFlags |= SyncConfigFlags.NoChanges;
			}
			if ((flags & ManifestConfigFlags.NoDeletions) == ManifestConfigFlags.NoDeletions)
			{
				syncConfigFlags |= SyncConfigFlags.NoDeletions;
			}
			if ((flags & ManifestConfigFlags.NoSoftDeletions) == ManifestConfigFlags.NoSoftDeletions)
			{
				syncConfigFlags |= SyncConfigFlags.NoSoftDeletions;
			}
			if ((flags & ManifestConfigFlags.NoReadUnread) != ManifestConfigFlags.NoReadUnread && (flags & ManifestConfigFlags.Conversations) != ManifestConfigFlags.Conversations)
			{
				syncConfigFlags |= SyncConfigFlags.ReadState;
			}
			if ((flags & ManifestConfigFlags.OrderByDeliveryTime) == ManifestConfigFlags.OrderByDeliveryTime)
			{
				syncConfigFlags |= SyncConfigFlags.OrderByDeliveryTime;
			}
			if ((flags & ManifestConfigFlags.ReevaluateOnRestrictionChange) == ManifestConfigFlags.ReevaluateOnRestrictionChange)
			{
				syncConfigFlags |= SyncConfigFlags.ReevaluateOnRestrictionChange;
			}
			if ((flags & ManifestConfigFlags.Catchup) == ManifestConfigFlags.Catchup)
			{
				syncConfigFlags |= SyncConfigFlags.Catchup;
			}
			if ((flags & ManifestConfigFlags.Conversations) == ManifestConfigFlags.Conversations)
			{
				syncConfigFlags |= SyncConfigFlags.Conversations;
			}
			return syncConfigFlags;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00019D54 File Offset: 0x00017F54
		public static MemoryStream ExtractIdSetBlobFromManifestBlob(byte[] mapiManifestBlob)
		{
			if (mapiManifestBlob == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("mapiManifestBlob");
			}
			MemoryStream memoryStream = new MemoryStream();
			using (MemoryStream memoryStream2 = new MemoryStream(mapiManifestBlob))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream2))
				{
					byte[] array = new byte[8];
					int num = binaryReader.Read(array, 0, 8);
					if (num == 8 && array.SequenceEqual(MapiManifest.stateSignatureVersion0))
					{
						int num2 = binaryReader.ReadInt32();
						for (int i = 0; i < num2; i++)
						{
							int num3 = binaryReader.ReadInt32();
							if (num3 > 0)
							{
								binaryReader.ReadBytes(num3);
							}
							binaryReader.ReadBoolean();
						}
					}
					else
					{
						memoryStream2.Seek(0L, SeekOrigin.Begin);
					}
					byte[] array2 = new byte[4096];
					memoryStream.SetLength(0L);
					do
					{
						num = memoryStream2.Read(array2, 0, array2.Length);
						if (num > 0)
						{
							memoryStream.Write(array2, 0, num);
						}
					}
					while (num > 0);
					memoryStream.Seek(0L, SeekOrigin.Begin);
				}
			}
			return memoryStream;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00019E60 File Offset: 0x00018060
		public unsafe void Configure(ManifestConfigFlags flags, Restriction restriction, Stream stream, IMapiManifestCallback iMapiManifestCallback, params PropTag[] tags)
		{
			base.CheckDisposed();
			if (iMapiManifestCallback == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("IMapiManifestCallback");
			}
			if ((flags & ManifestConfigFlags.Conversations) == (ManifestConfigFlags)0 && (flags & ManifestConfigFlags.Associated) == (ManifestConfigFlags)0 && (flags & ManifestConfigFlags.Normal) == (ManifestConfigFlags)0)
			{
				throw MapiExceptionHelper.ArgumentException("flags", "Need ManifestConfigFlags.Normal, ManifestConfigFlags.Associated or both, or ManifestConfigFlags.Conversations.");
			}
			if ((flags & ManifestConfigFlags.Conversations) != (ManifestConfigFlags)0 && (flags & (ManifestConfigFlags.Associated | ManifestConfigFlags.ReevaluateOnRestrictionChange)) != (ManifestConfigFlags)0)
			{
				throw MapiExceptionHelper.ArgumentException("flags", "ManifestConfigFlags.Conversations is not compatible with other specified flags.");
			}
			if ((flags & ManifestConfigFlags.Conversations) != (ManifestConfigFlags)0 && restriction != null)
			{
				throw MapiExceptionHelper.ArgumentException("flags", "ManifestConfigFlags.Conversations is not compatible with restriction.");
			}
			base.LockStore();
			try
			{
				SyncConfigFlags syncConfigFlags = MapiManifest.ConvertManifestFlagsToSyncFlags(flags);
				if (tags != null && tags.Length > 0)
				{
					foreach (PropTag propTag in tags)
					{
						if (propTag == PropTag.MessageAttachments || propTag == PropTag.MessageRecipients)
						{
							throw MapiExceptionHelper.ArgumentException("tags", "Cannot request PropTag.MessageAttachments or PropTag.MessageRecipients");
						}
					}
				}
				this.manifestCheckpoint = new ManifestCheckpoint(base.MapiStore, this.checkpointState, this.iExchangeExportManifest, MapiManifest.maxUncoalescedCount);
				this.manifestCallbackHelper = new ManifestCallbackHelper(this.manifestCheckpoint, (syncConfigFlags & SyncConfigFlags.Conversations) != SyncConfigFlags.None);
				if (stream != null)
				{
					byte[] array = new byte[8];
					byte[] array2 = new byte[4096];
					bool flag = false;
					int num = 0;
					byte b = 0;
					if (!stream.CanRead || !stream.CanSeek || !stream.CanWrite)
					{
						throw MapiExceptionHelper.ArgumentException("stream", "Stream not marked for full access (read, write, seek).");
					}
					stream.Seek(0L, SeekOrigin.Begin);
					int num2 = stream.Read(array, 0, 8);
					if (num2 == 8 && array[0] == 77 && array[1] == 65 && array[2] == 80 && array[3] == 73 && array[4] == 77 && array[5] == 65 && array[6] == 78)
					{
						b = array[7];
						flag = true;
						num = 8;
					}
					stream.Seek((long)num, SeekOrigin.Begin);
					if (flag)
					{
						if (b != 0)
						{
							throw MapiExceptionHelper.ArgumentException("stream", "Stream contains unsupported format version.");
						}
						this.manifestCallbackHelper.DeserializeReadCallbacks(stream, base.MapiStore, (flags & ManifestConfigFlags.Catchup) == ManifestConfigFlags.Catchup);
					}
					this.checkpointState.SetLength(0L);
					this.finalState.SetLength(0L);
					do
					{
						num2 = stream.Read(array2, 0, array2.Length);
						if (num2 > 0)
						{
							this.checkpointState.Write(array2, 0, num2);
							this.finalState.Write(array2, 0, num2);
						}
					}
					while (num2 > 0);
					stream.Seek(0L, SeekOrigin.Begin);
					this.checkpointState.Seek(0L, SeekOrigin.Begin);
					this.finalState.Seek(0L, SeekOrigin.Begin);
				}
				MapiIStream iStream = new MapiIStream(this.finalState);
				int num3 = 4;
				this.iMapiManifestCallback = iMapiManifestCallback;
				if (restriction != null)
				{
					num3 += restriction.GetBytesToMarshal();
				}
				try
				{
					fixed (byte* ptr = new byte[num3])
					{
						SRestriction* ptr2 = null;
						if (restriction != null)
						{
							byte* ptr3 = ptr;
							ptr2 = (SRestriction*)ptr;
							ptr3 += (SRestriction.SizeOf + 7 & -8);
							restriction.MarshalToNative(ptr2, ref ptr3);
						}
						int num4 = this.iExchangeExportManifest.Config(iStream, syncConfigFlags, this.manifestCallbackHelper, ptr2, (tags != null && tags.Length > 0) ? PropTagHelper.SPropTagArray(tags) : null);
						if (num4 != 0)
						{
							base.ThrowIfError("Unable to configure MapiManifest ICS synchronizer.", num4);
						}
					}
				}
				finally
				{
					byte* ptr = null;
				}
				if ((flags & ManifestConfigFlags.ReevaluateOnRestrictionChange) == ManifestConfigFlags.ReevaluateOnRestrictionChange)
				{
					iStream = new MapiIStream(this.checkpointState);
					int num4 = this.iExchangeExportManifest.Checkpoint(iStream, true, null, null, null, null, null);
					if (num4 != 0)
					{
						base.ThrowIfError("Unable to clear checkpoint state.", num4);
					}
				}
				this.done = false;
				this.synchronizationDone = false;
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001A224 File Offset: 0x00018424
		public ManifestStatus Synchronize()
		{
			base.CheckDisposed();
			base.LockStore();
			ManifestStatus result;
			try
			{
				ManifestStatus manifestStatus = this.manifestCallbackHelper.DoCallbacks(this.iMapiManifestCallback, new ManifestCallbackQueue<IMapiManifestCallback>[]
				{
					this.manifestCallbackHelper.SavedReads
				});
				if (manifestStatus == ManifestStatus.Done && !this.done && !this.synchronizationDone)
				{
					int num = 264224;
					while (264224 == num)
					{
						manifestStatus = this.manifestCallbackHelper.DoCallbacks(this.iMapiManifestCallback, new ManifestCallbackQueue<IMapiManifestCallback>[]
						{
							this.manifestCallbackHelper.Changes,
							this.manifestCallbackHelper.Deletes
						});
						if (manifestStatus != ManifestStatus.Done)
						{
							break;
						}
						num = this.iExchangeExportManifest.Synchronize(0);
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
					manifestStatus = this.manifestCallbackHelper.DoCallbacks(this.iMapiManifestCallback, new ManifestCallbackQueue<IMapiManifestCallback>[]
					{
						this.manifestCallbackHelper.Changes,
						this.manifestCallbackHelper.Deletes
					});
				}
				if (manifestStatus == ManifestStatus.Done)
				{
					this.done = true;
					manifestStatus = this.manifestCallbackHelper.DoCallbacks(this.iMapiManifestCallback, new ManifestCallbackQueue<IMapiManifestCallback>[]
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

		// Token: 0x060006EC RID: 1772 RVA: 0x0001A394 File Offset: 0x00018594
		public void GetState(Stream stream)
		{
			base.CheckDisposed();
			if (stream == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("stream");
			}
			if (!stream.CanRead || !stream.CanSeek || !stream.CanWrite)
			{
				throw MapiExceptionHelper.ArgumentException("stream", "Stream not marked for full access (read, write, seek).");
			}
			base.LockStore();
			try
			{
				stream.Seek(0L, SeekOrigin.Begin);
				stream.Write(MapiManifest.stateSignatureVersion0, 0, MapiManifest.stateSignatureVersion0.Length);
				this.manifestCallbackHelper.SerializeReadCallbacks(stream, base.MapiStore);
				BufferPool bufferPool;
				byte[] array = BufferPools.GetBuffer(98304, out bufferPool);
				try
				{
					if (this.done && this.manifestCallbackHelper.Changes.Count == 0 && this.manifestCallbackHelper.Deletes.Count == 0)
					{
						this.finalState.Seek(0L, SeekOrigin.Begin);
						int num;
						do
						{
							num = this.finalState.Read(array, 0, array.Length);
							if (num > 0)
							{
								stream.Write(array, 0, num);
							}
						}
						while (num > 0);
						this.finalState.Seek(0L, SeekOrigin.Begin);
					}
					else
					{
						this.manifestCheckpoint.Checkpoint();
						this.checkpointState.Seek(0L, SeekOrigin.Begin);
						int num;
						do
						{
							num = this.checkpointState.Read(array, 0, array.Length);
							if (num > 0)
							{
								stream.Write(array, 0, num);
							}
						}
						while (num > 0);
					}
				}
				finally
				{
					if (bufferPool != null && array != null)
					{
						bufferPool.Release(array);
						array = null;
					}
				}
				long length = stream.Seek(0L, SeekOrigin.Current);
				stream.SetLength(length);
				stream.Seek(0L, SeekOrigin.Begin);
			}
			finally
			{
				base.UnlockStore();
			}
		}

		// Token: 0x04000632 RID: 1586
		private IExExportManifest iExchangeExportManifest;

		// Token: 0x04000633 RID: 1587
		private IMapiManifestCallback iMapiManifestCallback;

		// Token: 0x04000634 RID: 1588
		private ManifestCallbackHelper manifestCallbackHelper;

		// Token: 0x04000635 RID: 1589
		private ManifestCheckpoint manifestCheckpoint;

		// Token: 0x04000636 RID: 1590
		private MemoryStream checkpointState;

		// Token: 0x04000637 RID: 1591
		private MemoryStream finalState;

		// Token: 0x04000638 RID: 1592
		private bool done;

		// Token: 0x04000639 RID: 1593
		private bool synchronizationDone;

		// Token: 0x0400063A RID: 1594
		private static readonly int maxUncoalescedCount = 250;

		// Token: 0x0400063B RID: 1595
		private static readonly byte[] stateSignatureVersion0 = new byte[]
		{
			77,
			65,
			80,
			73,
			77,
			65,
			78,
			0
		};
	}
}
