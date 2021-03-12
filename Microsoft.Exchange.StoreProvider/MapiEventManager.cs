using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200008B RID: 139
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiEventManager
	{
		// Token: 0x06000391 RID: 913 RVA: 0x0000FB44 File Offset: 0x0000DD44
		private MapiEventManager(ExRpcAdmin exRpcAdmin, Guid consumerGuid, Guid mdbGuid) : this(exRpcAdmin, consumerGuid, mdbGuid, Guid.Empty)
		{
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000FB54 File Offset: 0x0000DD54
		private MapiEventManager(ExRpcAdmin exRpcAdmin, Guid consumerGuid, Guid mdbGuid, byte[] mdbVersionInfo) : this(exRpcAdmin, consumerGuid, mdbGuid, new Guid(mdbVersionInfo))
		{
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000FB66 File Offset: 0x0000DD66
		private MapiEventManager(ExRpcAdmin exRpcAdmin, Guid consumerGuid, Guid mdbGuid, Guid mdbVersionGuid)
		{
			if (exRpcAdmin == null)
			{
				throw MapiExceptionHelper.ArgumentNullException("exRpcAdmin");
			}
			this.exrpcAdmin = exRpcAdmin;
			this.consumerGuid = consumerGuid;
			this.mdbGuid = mdbGuid;
			this.mdbVersionGuid = mdbVersionGuid;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000FB99 File Offset: 0x0000DD99
		public Guid ConsumerGuid
		{
			get
			{
				this.exrpcAdmin.CheckDisposed();
				return this.consumerGuid;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		public Guid MdbGuid
		{
			get
			{
				this.exrpcAdmin.CheckDisposed();
				return this.mdbGuid;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000FBBF File Offset: 0x0000DDBF
		public byte[] MdbVersion
		{
			get
			{
				this.exrpcAdmin.CheckDisposed();
				return this.mdbVersionGuid.ToByteArray();
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000FBD7 File Offset: 0x0000DDD7
		public static MapiEventManager Create(ExRpcAdmin exRpcAdmin, Guid consumerGuid, Guid mdbGuid, byte[] mdbVersionInfo)
		{
			return new MapiEventManager(exRpcAdmin, consumerGuid, mdbGuid, mdbVersionInfo);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000FBE2 File Offset: 0x0000DDE2
		public static MapiEventManager Create(ExRpcAdmin exRpcAdmin, Guid consumerGuid, Guid mdbGuid)
		{
			return new MapiEventManager(exRpcAdmin, consumerGuid, mdbGuid);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000FBEC File Offset: 0x0000DDEC
		public unsafe void SaveWatermarks(params Watermark[] watermarks)
		{
			this.exrpcAdmin.CheckDisposed();
			this.exrpcAdmin.Lock();
			try
			{
				if (watermarks == null)
				{
					throw MapiExceptionHelper.ArgumentNullException("watermarks");
				}
				if (watermarks.Length == 0)
				{
					throw MapiExceptionHelper.ArgumentException("watermarks", "need at least one watermark.");
				}
				int hresult = 0;
				Guid pguidMdb = this.mdbGuid;
				Guid guid = this.mdbVersionGuid;
				int num = WatermarkNative.SizeOf + 7 & -8;
				try
				{
					fixed (byte* ptr = new byte[num * watermarks.Length])
					{
						WatermarkNative* ptr2 = (WatermarkNative*)ptr;
						for (int i = 0; i < watermarks.Length; i++)
						{
							this.WatermarkToNativeWatermark(watermarks[i], ptr2);
							ptr2++;
						}
						hresult = this.exrpcAdmin.IExRpcAdmin.HrSaveWatermarks(pguidMdb, ref guid, watermarks.Length, (IntPtr)((void*)ptr));
					}
				}
				finally
				{
					byte* ptr = null;
				}
				MapiExceptionHelper.ThrowIfErrorOrWarning("Unable to save watermarks.", hresult, true, this.exrpcAdmin.IExRpcAdmin, null);
				this.mdbVersionGuid = guid;
			}
			finally
			{
				this.exrpcAdmin.Unlock();
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000FD10 File Offset: 0x0000DF10
		public Watermark GetWatermark(Guid mailboxGuid)
		{
			Watermark[] watermarks = this.GetWatermarks(mailboxGuid);
			if (watermarks.Length == 0)
			{
				return null;
			}
			return watermarks[0];
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000FD2F File Offset: 0x0000DF2F
		public Watermark[] GetWatermarks()
		{
			return this.GetWatermarks(MapiEventManager.AllWatermarks);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000FD3C File Offset: 0x0000DF3C
		internal Watermark[] GetWatermarks(Guid guidMailbox)
		{
			this.exrpcAdmin.CheckDisposed();
			this.exrpcAdmin.Lock();
			Watermark[] result;
			try
			{
				Guid pguidMdb = this.mdbGuid;
				Guid guid = this.mdbVersionGuid;
				Guid pguidConsumer = this.consumerGuid;
				Watermark[] array = null;
				int countWatermarks = 0;
				SafeExMemoryHandle safeExMemoryHandle = null;
				try
				{
					int num = this.exrpcAdmin.IExRpcAdmin.HrGetWatermarks(pguidMdb, ref guid, pguidConsumer, guidMailbox, out countWatermarks, out safeExMemoryHandle);
					if (num != 0)
					{
						MapiExceptionHelper.ThrowIfError("Unable to get watermarks for consumer " + this.consumerGuid, num, this.exrpcAdmin.IExRpcAdmin, null);
					}
					this.mdbVersionGuid = guid;
					array = Watermark.Unmarshal(safeExMemoryHandle, countWatermarks);
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
				this.exrpcAdmin.Unlock();
			}
			return result;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000FE14 File Offset: 0x0000E014
		public void DeleteWatermark(Guid mailboxGuid)
		{
			this.exrpcAdmin.CheckDisposed();
			this.exrpcAdmin.Lock();
			try
			{
				int num = 0;
				Guid pguidMdb = this.mdbGuid;
				Guid guid = this.mdbVersionGuid;
				Guid pguidConsumer = this.consumerGuid;
				int num2 = this.exrpcAdmin.IExRpcAdmin.HrDeleteWatermarksForConsumer(pguidMdb, ref guid, mailboxGuid, pguidConsumer, out num);
				if (num2 != 0)
				{
					MapiExceptionHelper.ThrowIfError("Unable to delete watermarks.", num2, this.exrpcAdmin.IExRpcAdmin, null);
				}
				this.mdbVersionGuid = guid;
			}
			finally
			{
				this.exrpcAdmin.Unlock();
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		public void DeleteWatermarks()
		{
			this.exrpcAdmin.CheckDisposed();
			this.exrpcAdmin.Lock();
			try
			{
				int num = 0;
				Guid pguidMdb = this.mdbGuid;
				Guid guid = this.mdbVersionGuid;
				Guid pguidConsumer = this.consumerGuid;
				Guid allWatermarks = MapiEventManager.AllWatermarks;
				int num2 = this.exrpcAdmin.IExRpcAdmin.HrDeleteWatermarksForConsumer(pguidMdb, ref guid, allWatermarks, pguidConsumer, out num);
				if (num2 != 0)
				{
					MapiExceptionHelper.ThrowIfError("Unable to delete watermarks.", num2, this.exrpcAdmin.IExRpcAdmin, null);
				}
				this.mdbVersionGuid = guid;
			}
			finally
			{
				this.exrpcAdmin.Unlock();
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000FF4C File Offset: 0x0000E14C
		public MapiEvent[] ReadEvents(long startCounter, int eventCountWanted)
		{
			long num = 0L;
			return this.ReadEvents(startCounter, eventCountWanted, 0, null, ReadEventsFlags.None, out num);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000FF69 File Offset: 0x0000E169
		public MapiEvent[] ReadEvents(long startCounter, int eventCountWanted, ReadEventsFlags flags)
		{
			return this.ReadEvents(startCounter, eventCountWanted, flags, true);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000FF78 File Offset: 0x0000E178
		public MapiEvent[] ReadEvents(long startCounter, int eventCountWanted, ReadEventsFlags flags, bool includeSid)
		{
			long num = 0L;
			return this.ReadEvents(startCounter, eventCountWanted, 0, null, flags, includeSid, out num);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000FF97 File Offset: 0x0000E197
		public MapiEvent[] ReadEvents(long startCounter, int eventCountWanted, int eventCountToCheck, Restriction filter, out long endCounter)
		{
			return this.ReadEvents(startCounter, eventCountWanted, eventCountToCheck, filter, ReadEventsFlags.None, out endCounter);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000FFA7 File Offset: 0x0000E1A7
		public MapiEvent[] ReadEvents(long startCounter, int eventCountWanted, int eventCountToCheck, Restriction filter, ReadEventsFlags flags, out long endCounter)
		{
			return this.ReadEvents(startCounter, eventCountWanted, eventCountToCheck, filter, flags, true, out endCounter);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000FFBC File Offset: 0x0000E1BC
		public unsafe MapiEvent[] ReadEvents(long startCounter, int eventCountWanted, int eventCountToCheck, Restriction filter, ReadEventsFlags flags, bool includeSid, out long endCounter)
		{
			FaultInjectionUtils.FaultInjectionTracer.TraceTest(3462802749U);
			this.exrpcAdmin.CheckDisposed();
			this.exrpcAdmin.Lock();
			MapiEvent[] result;
			try
			{
				MapiEvent[] array = null;
				SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
				Guid pguidMdb = this.mdbGuid;
				Guid guid = this.mdbVersionGuid;
				int num = 0;
				SRestriction* ptr = null;
				try
				{
					if (startCounter < 0L)
					{
						throw MapiExceptionHelper.ArgumentOutOfRangeException("startCounter", "Invalid start event counter: " + startCounter);
					}
					if (eventCountWanted < 0)
					{
						throw MapiExceptionHelper.ArgumentOutOfRangeException("eventCountWanted", "Invalid count of events requested: " + eventCountWanted);
					}
					if (eventCountToCheck < 0)
					{
						throw MapiExceptionHelper.ArgumentOutOfRangeException("eventCountToCheck", "Invalid count of events to check requested: " + eventCountToCheck);
					}
					if (filter != null)
					{
						int bytesToMarshal = filter.GetBytesToMarshal();
						byte* ptr2 = stackalloc byte[(UIntPtr)bytesToMarshal];
						ptr = (SRestriction*)ptr2;
						ptr2 += (SRestriction.SizeOf + 7 & -8);
						filter.MarshalToNative(ptr, ref ptr2);
					}
					int num2 = this.exrpcAdmin.IExRpcAdmin.HrReadMapiEvents(pguidMdb, ref guid, startCounter, eventCountWanted, eventCountToCheck, ptr, (int)flags, out num, out safeExLinkedMemoryHandle, out endCounter);
					if (num2 != 0)
					{
						MapiExceptionHelper.ThrowIfError("Unable to read events.", num2, this.exrpcAdmin.IExRpcAdmin, null);
					}
					this.mdbVersionGuid = guid;
					array = new MapiEvent[num];
					if (num > 0)
					{
						IntPtr intPtr = safeExLinkedMemoryHandle.DangerousGetHandle();
						for (int i = 0; i < num; i++)
						{
							MapiEventNative mapiEventNative = (MapiEventNative)Marshal.PtrToStructure(intPtr, typeof(MapiEventNative));
							intPtr = (IntPtr)((long)intPtr + (long)MapiEventNative.SizeOf);
							array[i] = new MapiEvent(ref mapiEventNative, includeSid);
						}
					}
				}
				finally
				{
					if (safeExLinkedMemoryHandle != null)
					{
						safeExLinkedMemoryHandle.Dispose();
					}
				}
				result = array;
			}
			finally
			{
				this.exrpcAdmin.Unlock();
			}
			return result;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00010194 File Offset: 0x0000E394
		public MapiEvent ReadLastEvent()
		{
			return this.ReadLastEvent(true);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000101A0 File Offset: 0x0000E3A0
		public MapiEvent ReadLastEvent(bool includeSid)
		{
			this.exrpcAdmin.CheckDisposed();
			this.exrpcAdmin.Lock();
			MapiEvent result;
			try
			{
				MapiEvent mapiEvent = null;
				SafeExLinkedMemoryHandle safeExLinkedMemoryHandle = null;
				Guid pguidMdb = this.mdbGuid;
				Guid guid = this.mdbVersionGuid;
				try
				{
					int num = this.exrpcAdmin.IExRpcAdmin.HrReadLastMapiEvent(pguidMdb, ref guid, out safeExLinkedMemoryHandle);
					if (num != 0)
					{
						MapiExceptionHelper.ThrowIfError("Unable to read the last event.", num, this.exrpcAdmin.IExRpcAdmin, null);
					}
					this.mdbVersionGuid = guid;
					MapiEventNative mapiEventNative = (MapiEventNative)Marshal.PtrToStructure(safeExLinkedMemoryHandle.DangerousGetHandle(), typeof(MapiEventNative));
					mapiEvent = new MapiEvent(ref mapiEventNative, includeSid);
				}
				finally
				{
					if (safeExLinkedMemoryHandle != null)
					{
						safeExLinkedMemoryHandle.Dispose();
					}
				}
				result = mapiEvent;
			}
			finally
			{
				this.exrpcAdmin.Unlock();
			}
			return result;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00010270 File Offset: 0x0000E470
		private unsafe void WatermarkToNativeWatermark(Watermark watermark, WatermarkNative* pwatermarkNative)
		{
			pwatermarkNative->consumerGuid = this.consumerGuid;
			pwatermarkNative->mailboxGuid = watermark.MailboxGuid;
			pwatermarkNative->llEventCounter = watermark.EventCounter;
		}

		// Token: 0x04000564 RID: 1380
		private static readonly Guid AllWatermarks = new Guid("bb6de7aa-f803-4cee-a985-86f568f3a9c9");

		// Token: 0x04000565 RID: 1381
		private ExRpcAdmin exrpcAdmin;

		// Token: 0x04000566 RID: 1382
		private Guid consumerGuid;

		// Token: 0x04000567 RID: 1383
		private Guid mdbGuid;

		// Token: 0x04000568 RID: 1384
		private Guid mdbVersionGuid;
	}
}
