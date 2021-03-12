using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000080 RID: 128
	public static class MapiStreamLock
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0001F620 File Offset: 0x0001D820
		public static void Release(MapiStream stream)
		{
			if (stream.Logon == null)
			{
				return;
			}
			MapiStreamLock.ListOfLocks listOfLocks = (MapiStreamLock.ListOfLocks)stream.Logon.MapiMailbox.SharedState.GetComponentData(MapiStreamLock.mapiStreamLockSlot);
			if (listOfLocks == null)
			{
				return;
			}
			using (LockManager.Lock(listOfLocks))
			{
				MapiStreamLock.ListOfLocks listOfLocks2 = new MapiStreamLock.ListOfLocks(1);
				foreach (MapiStreamLock.LockOnStream lockOnStream in listOfLocks)
				{
					if (object.ReferenceEquals(lockOnStream.StreamObject, stream))
					{
						listOfLocks2.Add(lockOnStream);
					}
				}
				foreach (MapiStreamLock.LockOnStream item in listOfLocks2)
				{
					listOfLocks.Remove(item);
				}
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001F714 File Offset: 0x0001D914
		public static ErrorCode LockRegion(MapiStream stream, ulong regionStart, ulong regionLength, bool exclusive)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			ulong num = regionStart + regionLength - 1UL;
			if (num < regionStart)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "Region overflow in Lock Region");
				return ErrorCode.CreateInvalidParameter((LID)42392U);
			}
			MapiStreamLock.ListOfLocks listOfLocks = (MapiStreamLock.ListOfLocks)stream.Logon.MapiMailbox.SharedState.GetComponentData(MapiStreamLock.mapiStreamLockSlot);
			if (listOfLocks == null)
			{
				listOfLocks = new MapiStreamLock.ListOfLocks(2);
				MapiStreamLock.ListOfLocks listOfLocks2 = (MapiStreamLock.ListOfLocks)stream.Logon.MapiMailbox.SharedState.CompareExchangeComponentData(MapiStreamLock.mapiStreamLockSlot, null, listOfLocks);
				if (listOfLocks2 != null)
				{
					listOfLocks = listOfLocks2;
				}
			}
			using (LockManager.Lock(listOfLocks))
			{
				foreach (MapiStreamLock.LockOnStream lockOnStream in listOfLocks)
				{
					if (lockOnStream.PropertyTag == stream.Ptag && lockOnStream.ObjectId == MapiStreamLock.GetMapiObjectExchangeId(stream.ParentObject) && !lockOnStream.IsExpired)
					{
						if (regionStart >= lockOnStream.RegionStart && regionStart <= lockOnStream.RegionEnd && (lockOnStream.Exclusive || exclusive))
						{
							errorCode = ErrorCode.CreateLockViolation((LID)34200U);
							break;
						}
						if (num >= lockOnStream.RegionStart && num <= lockOnStream.RegionEnd && (lockOnStream.Exclusive || exclusive))
						{
							errorCode = ErrorCode.CreateLockViolation((LID)50584U);
							break;
						}
						if (regionStart <= lockOnStream.RegionStart && num >= lockOnStream.RegionEnd && (lockOnStream.Exclusive || exclusive))
						{
							errorCode = ErrorCode.CreateLockViolation((LID)47512U);
							break;
						}
					}
				}
				if (errorCode == ErrorCode.NoError)
				{
					MapiStreamLock.LockOnStream item = new MapiStreamLock.LockOnStream(stream, regionStart, regionLength, exclusive);
					listOfLocks.Add(item);
				}
			}
			return errorCode;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001F918 File Offset: 0x0001DB18
		public static ErrorCode UnLockRegion(MapiStream stream, ulong regionStart, ulong regionLength, bool exclusive)
		{
			ErrorCode result = ErrorCode.NoError;
			ulong num = regionStart + regionLength - 1UL;
			if (num < regionStart)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "Region overflow in Unlock Region");
				return ErrorCode.CreateInvalidParameter((LID)63896U);
			}
			MapiStreamLock.ListOfLocks listOfLocks = (MapiStreamLock.ListOfLocks)stream.Logon.MapiMailbox.SharedState.GetComponentData(MapiStreamLock.mapiStreamLockSlot);
			if (listOfLocks == null)
			{
				return ErrorCode.NoError;
			}
			using (LockManager.Lock(listOfLocks))
			{
				MapiStreamLock.ListOfLocks listOfLocks2 = new MapiStreamLock.ListOfLocks(1);
				foreach (MapiStreamLock.LockOnStream lockOnStream in listOfLocks)
				{
					if (object.ReferenceEquals(lockOnStream.StreamObject, stream) && lockOnStream.Exclusive == exclusive && lockOnStream.RegionStart >= regionStart && lockOnStream.RegionEnd <= num)
					{
						listOfLocks2.Add(lockOnStream);
						if (lockOnStream.IsExpired)
						{
							result = ErrorCode.CreateNetworkError((LID)39320U);
						}
					}
				}
				foreach (MapiStreamLock.LockOnStream item in listOfLocks2)
				{
					listOfLocks.Remove(item);
				}
			}
			return result;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001FA78 File Offset: 0x0001DC78
		public static void CanAccess(LID lid, MapiStream stream, ulong regionStart, ulong regionLength, bool exclusive)
		{
			ulong num = (regionLength != 0UL) ? (regionStart + regionLength - 1UL) : regionStart;
			if (num < regionStart)
			{
				DiagnosticContext.TraceLocation(lid);
				throw new StoreException((LID)55704U, ErrorCodeValue.InvalidParameter);
			}
			MapiStreamLock.ListOfLocks listOfLocks = (MapiStreamLock.ListOfLocks)stream.Logon.MapiMailbox.SharedState.GetComponentData(MapiStreamLock.mapiStreamLockSlot);
			if (listOfLocks == null)
			{
				return;
			}
			using (LockManager.Lock(listOfLocks))
			{
				foreach (MapiStreamLock.LockOnStream lockOnStream in listOfLocks)
				{
					if (!object.ReferenceEquals(lockOnStream.StreamObject, stream))
					{
						if (lockOnStream.IsExpired)
						{
							DiagnosticContext.TraceLocation(lid);
							throw new StoreException((LID)51608U, ErrorCodeValue.NetworkError);
						}
						if (regionLength != 0UL && lockOnStream.PropertyTag == stream.Ptag && lockOnStream.ObjectId == MapiStreamLock.GetMapiObjectExchangeId(stream.ParentObject))
						{
							if (regionStart >= lockOnStream.RegionStart && regionStart <= lockOnStream.RegionEnd)
							{
								if (lockOnStream.Exclusive || exclusive)
								{
									DiagnosticContext.TraceLocation(lid);
									throw new StoreException((LID)43416U, ErrorCodeValue.LockViolation);
								}
								break;
							}
							else if (num >= lockOnStream.RegionStart && num <= lockOnStream.RegionEnd)
							{
								if (lockOnStream.Exclusive || exclusive)
								{
									DiagnosticContext.TraceLocation(lid);
									throw new StoreException((LID)59800U, ErrorCodeValue.LockViolation);
								}
								break;
							}
							else if (regionStart <= lockOnStream.RegionStart && num >= lockOnStream.RegionEnd)
							{
								if (lockOnStream.Exclusive || exclusive)
								{
									DiagnosticContext.TraceLocation(lid);
									throw new StoreException((LID)35224U, ErrorCodeValue.LockViolation);
								}
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001FC70 File Offset: 0x0001DE70
		internal static void Initialize()
		{
			if (MapiStreamLock.mapiStreamLockSlot == -1)
			{
				MapiStreamLock.mapiStreamLockSlot = MailboxState.AllocateComponentDataSlot(false);
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001FC88 File Offset: 0x0001DE88
		private static ExchangeId GetMapiObjectExchangeId(MapiBase mapiObject)
		{
			switch (mapiObject.MapiObjectType)
			{
			case MapiObjectType.Attachment:
				return ((MapiAttachment)mapiObject).Atid;
			case MapiObjectType.Folder:
				return ((MapiFolder)mapiObject).Fid;
			case MapiObjectType.Message:
			case MapiObjectType.EmbeddedMessage:
				return ((MapiMessage)mapiObject).Mid;
			}
			throw new ExExceptionNoSupport((LID)44856U, "This object does not have the Id");
		}

		// Token: 0x040002B5 RID: 693
		internal const int DefaultListSize = 2;

		// Token: 0x040002B6 RID: 694
		private static int mapiStreamLockSlot = -1;

		// Token: 0x02000081 RID: 129
		private class ListOfLocks : List<MapiStreamLock.LockOnStream>, IComponentData
		{
			// Token: 0x0600047B RID: 1147 RVA: 0x0001FCFE File Offset: 0x0001DEFE
			internal ListOfLocks(int inititalCapacity) : base(inititalCapacity)
			{
			}

			// Token: 0x0600047C RID: 1148 RVA: 0x0001FD07 File Offset: 0x0001DF07
			bool IComponentData.DoCleanup(Context context)
			{
				return base.Count == 0;
			}
		}

		// Token: 0x02000082 RID: 130
		private class LockOnStream
		{
			// Token: 0x0600047D RID: 1149 RVA: 0x0001FD14 File Offset: 0x0001DF14
			public LockOnStream(MapiStream stream, ulong regionStart, ulong regionLength, bool exclusive)
			{
				this.streamObject = stream;
				this.propertyTag = stream.Ptag;
				this.objectId = MapiStreamLock.GetMapiObjectExchangeId(stream.ParentObject);
				this.regionStart = regionStart;
				this.regionLength = regionLength;
				this.exclusive = exclusive;
				this.lockGrantTime = DateTime.UtcNow;
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x0600047E RID: 1150 RVA: 0x0001FD6C File Offset: 0x0001DF6C
			public bool IsExpired
			{
				get
				{
					return this.lockGrantTime + MapiStreamLock.LockOnStream.expirationTime < DateTime.UtcNow;
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x0600047F RID: 1151 RVA: 0x0001FD88 File Offset: 0x0001DF88
			public object StreamObject
			{
				get
				{
					return this.streamObject;
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000480 RID: 1152 RVA: 0x0001FD90 File Offset: 0x0001DF90
			public StorePropTag PropertyTag
			{
				get
				{
					return this.propertyTag;
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001FD98 File Offset: 0x0001DF98
			public ExchangeId ObjectId
			{
				get
				{
					return this.objectId;
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001FDA0 File Offset: 0x0001DFA0
			public ulong RegionStart
			{
				get
				{
					return this.regionStart;
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001FDA8 File Offset: 0x0001DFA8
			public ulong RegionLength
			{
				get
				{
					return this.regionLength;
				}
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001FDB0 File Offset: 0x0001DFB0
			public ulong RegionEnd
			{
				get
				{
					return this.regionStart + this.regionLength - 1UL;
				}
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001FDC2 File Offset: 0x0001DFC2
			public bool Exclusive
			{
				get
				{
					return this.exclusive;
				}
			}

			// Token: 0x040002B7 RID: 695
			private static TimeSpan expirationTime = TimeSpan.FromMinutes(30.0);

			// Token: 0x040002B8 RID: 696
			private readonly object streamObject;

			// Token: 0x040002B9 RID: 697
			private readonly StorePropTag propertyTag;

			// Token: 0x040002BA RID: 698
			private readonly ExchangeId objectId;

			// Token: 0x040002BB RID: 699
			private readonly ulong regionStart;

			// Token: 0x040002BC RID: 700
			private readonly ulong regionLength;

			// Token: 0x040002BD RID: 701
			private readonly bool exclusive;

			// Token: 0x040002BE RID: 702
			private readonly DateTime lockGrantTime;
		}
	}
}
