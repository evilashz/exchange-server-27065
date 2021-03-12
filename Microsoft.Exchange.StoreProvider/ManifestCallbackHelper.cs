using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000074 RID: 116
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ManifestCallbackHelper : ContentsManifestCallbackHelperBase<IMapiManifestCallback>, IExchangeManifestCallback
	{
		// Token: 0x060002EE RID: 750 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		public ManifestCallbackHelper(ManifestCheckpoint checkpoint, bool conversations) : base(conversations)
		{
			this.checkpoint = checkpoint;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000CA87 File Offset: 0x0000AC87
		public ManifestCallbackQueue<IMapiManifestCallback> SavedReads
		{
			get
			{
				return this.savedReadList;
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000CA90 File Offset: 0x0000AC90
		internal void SerializeReadCallbacks(Stream stream, MapiStore mapiStore)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			int value = this.SavedReads.Count + base.Reads.Count;
			binaryWriter.Write(value);
			IMapiManifestCallback callback = new ManifestCallbackHelper.CallbackSerializer(binaryWriter, mapiStore);
			this.SavedReads.ExecuteNoDequeue(callback);
			base.Reads.ExecuteNoDequeue(callback);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000CAE8 File Offset: 0x0000ACE8
		internal void DeserializeReadCallbacks(Stream stream, MapiStore mapiStore, bool skipOver)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			ManifestCallbackQueue<IMapiManifestCallback> savedReadsQueue = skipOver ? new ManifestCallbackQueue<IMapiManifestCallback>() : this.SavedReads;
			int num = binaryReader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				ManifestCallbackHelper.CallbackSerializer.DeserializeReadCallback(binaryReader, savedReadsQueue, mapiStore);
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000CB28 File Offset: 0x0000AD28
		unsafe int IExchangeManifestCallback.Change(ExchangeManifestCallbackChangeFlags flags, int cpvalHeader, SPropValue* ppvalHeader, int cpvalProps, SPropValue* ppvalProps)
		{
			return base.Change(flags, cpvalHeader, ppvalHeader, cpvalProps, ppvalProps);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000CB88 File Offset: 0x0000AD88
		unsafe int IExchangeManifestCallback.Delete(ExchangeManifestCallbackDeleteFlags flags, int cElements, _CallbackInfo* lpCallbackInfo)
		{
			int i = 0;
			while (i < cElements)
			{
				byte[] entryId = new byte[lpCallbackInfo->cb];
				Marshal.Copy((IntPtr)((void*)lpCallbackInfo->pb), entryId, 0, lpCallbackInfo->cb);
				long mid = lpCallbackInfo->id;
				ExchangeManifestCallbackDeleteFlags localFlags = flags;
				base.Deletes.Enqueue(delegate(IMapiManifestCallback callback)
				{
					ManifestCallbackStatus result = callback.Delete(entryId, (localFlags & ExchangeManifestCallbackDeleteFlags.SoftDelete) == ExchangeManifestCallbackDeleteFlags.SoftDelete, (localFlags & ExchangeManifestCallbackDeleteFlags.Expiry) == ExchangeManifestCallbackDeleteFlags.Expiry);
					this.checkpoint.IdDeleted(mid);
					return result;
				});
				i++;
				lpCallbackInfo++;
			}
			return 0;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000CC30 File Offset: 0x0000AE30
		unsafe int IExchangeManifestCallback.Read(ExchangeManifestCallbackReadFlags flags, int cElements, _CallbackInfo* lpCallbackInfo)
		{
			int i = 0;
			while (i < cElements)
			{
				byte[] entryId = new byte[lpCallbackInfo->cb];
				Marshal.Copy((IntPtr)((void*)lpCallbackInfo->pb), entryId, 0, lpCallbackInfo->cb);
				bool isRead = (flags & ExchangeManifestCallbackReadFlags.Read) == ExchangeManifestCallbackReadFlags.Read;
				base.Reads.Enqueue((IMapiManifestCallback callback) => callback.ReadUnread(entryId, isRead));
				i++;
				lpCallbackInfo++;
			}
			return 0;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000CCAC File Offset: 0x0000AEAC
		protected override ManifestCallbackStatus DoChangeCallback(IMapiManifestCallback callback, ManifestChangeType changeType, PropValue[] headerProps, PropValue[] messageProps)
		{
			ManifestCallbackStatus result = ManifestCallbackStatus.Continue;
			if (base.Conversations)
			{
				if (changeType != (ManifestChangeType)0)
				{
					result = callback.Change(headerProps[4].GetBytes(), null, null, null, headerProps[2].GetDateTime(), changeType, false, messageProps);
				}
				this.checkpoint.CnSeen(false, headerProps[1].GetLong());
			}
			else if (headerProps.Length == 9)
			{
				result = callback.Change(headerProps[8].GetBytes(), headerProps[0].GetBytes(), headerProps[2].GetBytes(), headerProps[3].GetBytes(), headerProps[1].GetDateTime(), changeType, headerProps[4].GetBoolean(), messageProps);
				this.checkpoint.IdGiven(headerProps[5].GetLong());
				this.checkpoint.CnSeen(headerProps[4].GetBoolean(), headerProps[7].GetLong());
			}
			else
			{
				result = callback.Change(headerProps[9].GetBytes(), headerProps[0].GetBytes(), headerProps[2].GetBytes(), headerProps[3].GetBytes(), headerProps[1].GetDateTime(), changeType, headerProps[4].GetBoolean(), messageProps);
				this.checkpoint.IdGiven(headerProps[5].GetLong());
				this.checkpoint.CnSeen(headerProps[4].GetBoolean(), headerProps[7].GetLong());
				this.checkpoint.CnRead(headerProps[8].GetLong());
			}
			return result;
		}

		// Token: 0x040004C3 RID: 1219
		private readonly ManifestCheckpoint checkpoint;

		// Token: 0x040004C4 RID: 1220
		private readonly ManifestCallbackQueue<IMapiManifestCallback> savedReadList = new ManifestCallbackQueue<IMapiManifestCallback>();

		// Token: 0x02000075 RID: 117
		private sealed class CallbackSerializer : IMapiManifestCallback
		{
			// Token: 0x060002F6 RID: 758 RVA: 0x0000CE49 File Offset: 0x0000B049
			public CallbackSerializer(BinaryWriter writer, MapiStore mapiStore)
			{
				this.writer = writer;
				this.mapiStore = mapiStore;
			}

			// Token: 0x060002F7 RID: 759 RVA: 0x0000CE7C File Offset: 0x0000B07C
			public static void DeserializeReadCallback(BinaryReader reader, ManifestCallbackQueue<IMapiManifestCallback> savedReadsQueue, MapiStore mapiStore)
			{
				int num = reader.ReadInt32();
				byte[] entryId;
				if (num > 0)
				{
					byte[] entryId2 = reader.ReadBytes(num);
					entryId = mapiStore.ExpandEntryId(entryId2);
				}
				else
				{
					entryId = Array<byte>.Empty;
				}
				bool read = reader.ReadBoolean();
				savedReadsQueue.Enqueue((IMapiManifestCallback callback) => callback.ReadUnread(entryId, read));
			}

			// Token: 0x060002F8 RID: 760 RVA: 0x0000CEDA File Offset: 0x0000B0DA
			public ManifestCallbackStatus Change(byte[] entryId, byte[] sourceKey, byte[] changeKey, byte[] changeList, DateTime lastModificationTime, ManifestChangeType changeType, bool associated, PropValue[] props)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060002F9 RID: 761 RVA: 0x0000CEE1 File Offset: 0x0000B0E1
			public ManifestCallbackStatus Delete(byte[] entryId, bool softDelete, bool expiry)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060002FA RID: 762 RVA: 0x0000CEE8 File Offset: 0x0000B0E8
			public ManifestCallbackStatus ReadUnread(byte[] entryId, bool read)
			{
				byte[] array = (entryId != null) ? this.mapiStore.CompressEntryId(entryId) : null;
				int num = (array != null) ? array.Length : 0;
				this.writer.Write(num);
				if (num > 0)
				{
					this.writer.Write(array);
				}
				this.writer.Write(read);
				return ManifestCallbackStatus.Continue;
			}

			// Token: 0x040004C5 RID: 1221
			private readonly BinaryWriter writer;

			// Token: 0x040004C6 RID: 1222
			private readonly MapiStore mapiStore;
		}
	}
}
