using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000A5 RID: 165
	public abstract class PerUser : ILockName, IEquatable<ILockName>, IComparable<ILockName>
	{
		// Token: 0x0600095C RID: 2396 RVA: 0x0004DF48 File Offset: 0x0004C148
		public static Task<StoreDatabase>.TaskCallback WrappedFlushCallback(Guid databaseGuid)
		{
			return TaskExecutionWrapper<StoreDatabase>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.FlushDirtyPerUserCaches, ClientType.System, databaseGuid), new TaskExecutionWrapper<StoreDatabase>.TaskCallback<Context>(PerUser.PerUserCache.FlushDirtyPerUserCachesTaskCallback));
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0004DF71 File Offset: 0x0004C171
		public static void Initialize()
		{
			PerUser.PerUserCache.Initialize();
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0004DF78 File Offset: 0x0004C178
		public static void MountEventHandler(Context context, StoreDatabase database, bool readOnly)
		{
			PerUser.PerUserCache.MountEventHandler(context, database, readOnly);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0004DF82 File Offset: 0x0004C182
		public static void DismountEventHandler(Context context, StoreDatabase database)
		{
			PerUser.PerUserCache.DismountEventHandler(context, database);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0004DF8C File Offset: 0x0004C18C
		public static bool InsertInResident(Context context, Mailbox mailbox, Guid ownerMailboxGuid, ExchangeId folderId, ExchangeId cn)
		{
			PerUser.PerUserCache perUserCache = PerUser.PerUserCache.GetPerUserCache(context, mailbox.SharedState);
			bool result;
			using (MailboxComponentOperationFrame mailboxComponentOperationFrame = perUserCache.TakeWriteLock(context))
			{
				PerUser.ResidentPerUser residentPerUser = PerUser.ResidentPerUser.Load(context, mailbox, ownerMailboxGuid, folderId);
				if (residentPerUser == null)
				{
					residentPerUser = new PerUser.ResidentPerUser(ownerMailboxGuid, folderId, new IdSet(), mailbox.UtcNow);
					perUserCache.Insert(context, residentPerUser);
				}
				bool flag = residentPerUser.Insert(mailbox, cn);
				mailboxComponentOperationFrame.Success();
				result = flag;
			}
			return result;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0004E00C File Offset: 0x0004C20C
		public static bool RemoveFromResident(Context context, Mailbox mailbox, Guid ownerMailboxGuid, ExchangeId folderId, ExchangeId cn)
		{
			PerUser.PerUserCache perUserCache = PerUser.PerUserCache.GetPerUserCache(context, mailbox.SharedState);
			bool result;
			using (MailboxComponentOperationFrame mailboxComponentOperationFrame = perUserCache.TakeWriteLock(context))
			{
				PerUser.ResidentPerUser residentPerUser = PerUser.ResidentPerUser.Load(context, mailbox, ownerMailboxGuid, folderId);
				if (residentPerUser == null)
				{
					residentPerUser = new PerUser.ResidentPerUser(ownerMailboxGuid, folderId, new IdSet(), mailbox.UtcNow);
					perUserCache.Insert(context, residentPerUser);
				}
				bool flag = residentPerUser.Remove(mailbox, cn);
				mailboxComponentOperationFrame.Success();
				result = flag;
			}
			return result;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0004E08C File Offset: 0x0004C28C
		public static PerUser LoadResident(Context context, Mailbox mailbox, Guid ownerMailboxGuid, ExchangeId folderId)
		{
			using (PerUser.PerUserCache.TakeReadLock(context, mailbox))
			{
				PerUser.ResidentPerUser residentPerUser = PerUser.ResidentPerUser.Load(context, mailbox, ownerMailboxGuid, folderId);
				if (residentPerUser != null)
				{
					return residentPerUser;
				}
			}
			PerUser.PerUserCache perUserCache = PerUser.PerUserCache.GetPerUserCache(context, mailbox.SharedState);
			PerUser result;
			using (MailboxComponentOperationFrame mailboxComponentOperationFrame2 = perUserCache.TakeWriteLock(context))
			{
				PerUser.ResidentPerUser residentPerUser = PerUser.ResidentPerUser.Load(context, mailbox, ownerMailboxGuid, folderId);
				mailboxComponentOperationFrame2.Success();
				result = residentPerUser;
			}
			return result;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0004E11C File Offset: 0x0004C31C
		internal static PerUser CreateResident(Context context, Mailbox mailbox, Guid mailboxGuid, ExchangeId folderId, IdSet cnSet)
		{
			PerUser.PerUserCache perUserCache = PerUser.PerUserCache.GetPerUserCache(context, mailbox.SharedState);
			PerUser result;
			using (MailboxComponentOperationFrame mailboxComponentOperationFrame = perUserCache.TakeWriteLock(context))
			{
				PerUser.ResidentPerUser residentPerUser = new PerUser.ResidentPerUser(mailboxGuid, folderId, cnSet, mailbox.UtcNow);
				perUserCache.Insert(context, residentPerUser);
				mailboxComponentOperationFrame.Success();
				result = residentPerUser;
			}
			return result;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0004E180 File Offset: 0x0004C380
		internal static void CreateResidentAndSave(Context context, Mailbox mailbox, Guid mailboxGuid, ExchangeId folderId, IdSet cnSet, DateTime? lastModifiedTime)
		{
			PerUser.PerUserCache perUserCache = PerUser.PerUserCache.GetPerUserCache(context, mailbox.SharedState);
			using (MailboxComponentOperationFrame mailboxComponentOperationFrame = perUserCache.TakeWriteLock(context))
			{
				PerUser.ResidentPerUser residentPerUser = new PerUser.ResidentPerUser(mailboxGuid, folderId, cnSet, lastModifiedTime ?? mailbox.UtcNow);
				perUserCache.Insert(context, residentPerUser);
				residentPerUser.Save(context, mailbox.SharedState);
				mailboxComponentOperationFrame.Success();
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0004E234 File Offset: 0x0004C434
		public static IEnumerable<PerUser> ResidentEntries(Context context, Mailbox mailbox)
		{
			PerUser.PerUserCache perUserCacheNoCreate = PerUser.PerUserCache.GetPerUserCacheNoCreate(mailbox.SharedState);
			if (perUserCacheNoCreate != null)
			{
				perUserCacheNoCreate.FlushAllDirtyEntries(context);
			}
			PerUserTable perUserTable = DatabaseSchema.PerUserTable(context.Database);
			ReplidGuidMap replidGuidMap = mailbox.ReplidGuidMap;
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailbox.MailboxPartitionNumber,
				true
			});
			List<PerUser> results = new List<PerUser>(100);
			using (PerUser.PerUserCache.TakeReadLock(context, mailbox))
			{
				PerUser.EnumerateRows(context, startStopKey, startStopKey, delegate(DataRow dataRow)
				{
					results.Add(new PerUser.ResidentPerUser(context, perUserTable, dataRow, replidGuidMap));
				});
			}
			return results;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0004E348 File Offset: 0x0004C548
		public static IEnumerable<PerUser> ResidentEntriesForFolder(Context context, Mailbox mailbox, ExchangeId folderId)
		{
			PerUser.PerUserCache perUserCacheNoCreate = PerUser.PerUserCache.GetPerUserCacheNoCreate(mailbox.SharedState);
			if (perUserCacheNoCreate != null)
			{
				perUserCacheNoCreate.FlushAllDirtyEntries(context);
			}
			PerUserTable perUserTable = DatabaseSchema.PerUserTable(context.Database);
			ReplidGuidMap replidGuidMap = mailbox.ReplidGuidMap;
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailbox.MailboxPartitionNumber,
				true,
				folderId.To26ByteArray()
			});
			List<PerUser> results = new List<PerUser>(100);
			using (PerUser.PerUserCache.TakeReadLock(context, mailbox))
			{
				PerUser.EnumerateRows(context, startStopKey, startStopKey, delegate(DataRow dataRow)
				{
					results.Add(new PerUser.ResidentPerUser(context, perUserTable, dataRow, replidGuidMap));
				});
			}
			return results;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0004E438 File Offset: 0x0004C638
		public static void DeleteAllResidentEntriesForFolder(Context context, Folder folder)
		{
			PerUser.PerUserCache perUserCacheNoCreate = PerUser.PerUserCache.GetPerUserCacheNoCreate(folder.Mailbox.SharedState);
			if (perUserCacheNoCreate != null)
			{
				perUserCacheNoCreate.FlushAllDirtyEntries(context);
				perUserCacheNoCreate.Reset();
			}
			ExchangeId id = folder.GetId(context);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				folder.Mailbox.MailboxPartitionNumber,
				true,
				id.To26ByteArray()
			});
			PerUser.DeleteRows(context, startStopKey);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0004E4AC File Offset: 0x0004C6AC
		public static void DeleteAllResidentEntries(Context context, Mailbox mailbox)
		{
			PerUser.PerUserCache perUserCacheNoCreate = PerUser.PerUserCache.GetPerUserCacheNoCreate(mailbox.SharedState);
			if (perUserCacheNoCreate != null)
			{
				perUserCacheNoCreate.FlushAllDirtyEntries(context);
				perUserCacheNoCreate.Reset();
			}
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailbox.MailboxPartitionNumber,
				true
			});
			PerUser.DeleteRows(context, startStopKey);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0004E528 File Offset: 0x0004C728
		public static PerUser LoadForeign(Context context, Mailbox mailbox, byte[] foreignFolderId)
		{
			PerUser.ForeignPerUser perUser = null;
			PerUserTable perUserTable = DatabaseSchema.PerUserTable(context.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailbox.MailboxPartitionNumber,
				false,
				foreignFolderId
			});
			using (PerUser.PerUserCache.TakeReadLock(context, mailbox))
			{
				PerUser.EnumerateRows(context, startStopKey, startStopKey, delegate(DataRow dataRow)
				{
					perUser = new PerUser.ForeignPerUser(context, perUserTable, dataRow);
				});
			}
			return perUser;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0004E5E0 File Offset: 0x0004C7E0
		public static PerUser CreateForeign(Guid replicaGuid, byte[] foreignFolderId, byte[] foreignCNSet)
		{
			return new PerUser.ForeignPerUser(replicaGuid, foreignFolderId, foreignCNSet, DateTime.UtcNow);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0004E5F0 File Offset: 0x0004C7F0
		public static IEnumerable<PerUser> ForeignEntries(Context context, Mailbox mailbox)
		{
			byte[] startFolderId = new byte[22];
			return PerUser.ForeignEntries(context, mailbox, startFolderId);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0004E634 File Offset: 0x0004C834
		public static IEnumerable<PerUser> ForeignEntries(Context context, Mailbox mailbox, byte[] startFolderId)
		{
			PerUserTable perUserTable = DatabaseSchema.PerUserTable(context.Database);
			StartStopKey startKey = new StartStopKey(true, new object[]
			{
				mailbox.MailboxPartitionNumber,
				false,
				startFolderId
			});
			StartStopKey stopKey = new StartStopKey(true, new object[]
			{
				mailbox.MailboxPartitionNumber,
				false
			});
			List<PerUser> results = new List<PerUser>(100);
			using (PerUser.PerUserCache.TakeReadLock(context, mailbox))
			{
				PerUser.EnumerateRows(context, startKey, stopKey, delegate(DataRow dataRow)
				{
					results.Add(new PerUser.ForeignPerUser(context, perUserTable, dataRow));
				});
			}
			return results;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0004E720 File Offset: 0x0004C920
		public static PerUser Parse(Context context, byte[] buffer, ReplidGuidMap replidGuidMap)
		{
			MDBEFCollection mdbefcollection = MDBEFCollection.CreateFrom(buffer, Encoding.UTF8);
			Guid mailboxGuid = Guid.Empty;
			ExchangeId folderId = ExchangeId.Zero;
			IdSet cnSet = null;
			DateTime dateTime = DateTime.MinValue;
			foreach (AnnotatedPropertyValue annotatedPropertyValue in mdbefcollection)
			{
				uint num = annotatedPropertyValue.PropertyTag;
				if (num <= 131330U)
				{
					if (num != 65608U)
					{
						if (num == 131330U)
						{
							folderId = ExchangeId.CreateFrom26ByteArray(context, replidGuidMap, (byte[])annotatedPropertyValue.PropertyValue.Value);
						}
					}
					else
					{
						mailboxGuid = (Guid)annotatedPropertyValue.PropertyValue.Value;
					}
				}
				else if (num != 196866U)
				{
					if (num == 262208U)
					{
						dateTime = (DateTime)((ExDateTime)annotatedPropertyValue.PropertyValue.Value);
					}
				}
				else
				{
					cnSet = IdSet.Parse(context, (byte[])annotatedPropertyValue.PropertyValue.Value);
				}
			}
			return new PerUser.ResidentPerUser(mailboxGuid, folderId, cnSet, dateTime);
		}

		// Token: 0x0600096E RID: 2414
		public abstract byte[] Serialize(Context context);

		// Token: 0x0600096F RID: 2415
		public abstract void Save(Context context, MailboxState mailboxState);

		// Token: 0x06000970 RID: 2416
		public abstract bool Contains(Mailbox mailbox, ExchangeId cn);

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000971 RID: 2417
		public abstract Guid Guid { get; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000972 RID: 2418
		public abstract ExchangeId FolderId { get; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000973 RID: 2419
		public abstract byte[] FolderIdBytes { get; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000974 RID: 2420
		internal abstract IdSet CNSet { get; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000975 RID: 2421
		public abstract byte[] CNSetBytes { get; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x0004E84C File Offset: 0x0004CA4C
		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0004E854 File Offset: 0x0004CA54
		public DateTime LastModificationTime
		{
			get
			{
				return this.lastModificationTime;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x0004E85C File Offset: 0x0004CA5C
		public LockManager.LockLevel LockLevel
		{
			get
			{
				return LockManager.LockLevel.PerUser;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0004E860 File Offset: 0x0004CA60
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x0004E868 File Offset: 0x0004CA68
		public LockManager.NamedLockObject CachedLockObject { get; set; }

		// Token: 0x0600097B RID: 2427
		public abstract ILockName GetLockNameToCache();

		// Token: 0x0600097C RID: 2428 RVA: 0x0004E871 File Offset: 0x0004CA71
		public virtual bool Equals(ILockName other)
		{
			return other != null && this.CompareTo(other) == 0;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0004E884 File Offset: 0x0004CA84
		public virtual int CompareTo(ILockName other)
		{
			int num = this.LockLevel.CompareTo(other.LockLevel);
			if (num == 0)
			{
				PerUser perUser = other as PerUser;
				num = this.Guid.CompareTo(perUser.Guid);
				if (num == 0)
				{
					num = ValueHelper.ArraysCompare<byte>(this.FolderIdBytes, perUser.FolderIdBytes);
				}
			}
			return num;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0004E8E4 File Offset: 0x0004CAE4
		public override int GetHashCode()
		{
			if (this.cachedHashCode == 0)
			{
				this.cachedHashCode = (this.LockLevel.GetHashCode() ^ this.Guid.GetHashCode());
				if (this.FolderIdBytes != null)
				{
					for (int i = 0; i < this.FolderIdBytes.Length; i++)
					{
						this.cachedHashCode ^= ((int)this.FolderIdBytes[i] << 8 * (i % 4)).GetHashCode();
					}
				}
				if (this.cachedHashCode == 0)
				{
					this.cachedHashCode = 1;
				}
			}
			return this.cachedHashCode;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0004E97C File Offset: 0x0004CB7C
		public void SaveWithCacheLock(Context context, Mailbox mailbox)
		{
			using (MailboxComponentOperationFrame mailboxComponentOperationFrame = PerUser.PerUserCache.TakeWriteLock(context, mailbox))
			{
				this.Save(context, mailbox.SharedState);
				mailboxComponentOperationFrame.Success();
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0004E9C8 File Offset: 0x0004CBC8
		private static void EnumerateRows(Context context, StartStopKey startKey, StartStopKey stopKey, Action<DataRow> callback)
		{
			PerUserTable perUserTable = DatabaseSchema.PerUserTable(context.Database);
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, perUserTable.Table, perUserTable.Table.PrimaryKeyIndex, perUserTable.Table.Columns.ToArray<PhysicalColumn>(), null, null, 0, 0, new KeyRange(startKey, stopKey), false, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					while (reader.Read())
					{
						using (DataRow dataRow = Factory.OpenDataRow(context.Culture, context, perUserTable.Table, false, reader))
						{
							if (dataRow == null)
							{
								break;
							}
							callback(dataRow);
						}
					}
				}
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0004EAA0 File Offset: 0x0004CCA0
		protected static void DeleteRows(Context context, StartStopKey startStopKey)
		{
			PerUserTable perUserTable = DatabaseSchema.PerUserTable(context.Database);
			using (DeleteOperator deleteOperator = Factory.CreateDeleteOperator(context.Culture, context, Factory.CreateTableOperator(context.Culture, context, perUserTable.Table, perUserTable.Table.PrimaryKeyIndex, null, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true), true))
			{
				deleteOperator.ExecuteScalar();
			}
		}

		// Token: 0x04000493 RID: 1171
		private const uint MailboxGuidPropertyTagInt = 65608U;

		// Token: 0x04000494 RID: 1172
		private const uint FolderIdPropertyTagInt = 131330U;

		// Token: 0x04000495 RID: 1173
		private const uint CNSetPropertyTagInt = 196866U;

		// Token: 0x04000496 RID: 1174
		private const uint LastModPropertyTagInt = 262208U;

		// Token: 0x04000497 RID: 1175
		private const uint TypeTagInt = 327683U;

		// Token: 0x04000498 RID: 1176
		internal static readonly PropertyTag MailboxGuidPropertyTag = new PropertyTag(65608U);

		// Token: 0x04000499 RID: 1177
		internal static readonly PropertyTag FolderIdPropertyTag = new PropertyTag(131330U);

		// Token: 0x0400049A RID: 1178
		internal static readonly PropertyTag CNSetPropertyTag = new PropertyTag(196866U);

		// Token: 0x0400049B RID: 1179
		internal static readonly PropertyTag LastModPropertyTag = new PropertyTag(262208U);

		// Token: 0x0400049C RID: 1180
		internal static readonly PropertyTag TypeTag = new PropertyTag(327683U);

		// Token: 0x0400049D RID: 1181
		protected DateTime lastModificationTime;

		// Token: 0x0400049E RID: 1182
		protected bool isDirty;

		// Token: 0x0400049F RID: 1183
		protected int cachedHashCode;

		// Token: 0x020000A6 RID: 166
		internal class ResidentPerUser : PerUser
		{
			// Token: 0x06000984 RID: 2436 RVA: 0x0004EB78 File Offset: 0x0004CD78
			internal static IDisposable SetLoadHook(Action action)
			{
				return PerUser.ResidentPerUser.loadHook.SetTestHook(action);
			}

			// Token: 0x06000985 RID: 2437 RVA: 0x0004EBC0 File Offset: 0x0004CDC0
			public static PerUser.ResidentPerUser Load(Context context, Mailbox mailbox, Guid ownerMailboxGuid, ExchangeId folderId)
			{
				if (PerUser.ResidentPerUser.loadHook.Value != null)
				{
					PerUser.ResidentPerUser.loadHook.Value();
				}
				PerUser.PerUserCache perUserCacheNoCreate = PerUser.PerUserCache.GetPerUserCacheNoCreate(mailbox.SharedState);
				PerUser.ResidentPerUser perUser = perUserCacheNoCreate.Find(context, ownerMailboxGuid, folderId);
				if (perUser == null && perUserCacheNoCreate.HasWriteLock())
				{
					ReplidGuidMap replidGuidMap = mailbox.ReplidGuidMap;
					PerUserTable perUserTable = DatabaseSchema.PerUserTable(context.Database);
					StartStopKey startStopKey = new StartStopKey(true, new object[]
					{
						mailbox.MailboxPartitionNumber,
						true,
						folderId.To26ByteArray(),
						ownerMailboxGuid
					});
					PerUser.EnumerateRows(context, startStopKey, startStopKey, delegate(DataRow dataRow)
					{
						perUser = new PerUser.ResidentPerUser(context, perUserTable, dataRow, replidGuidMap);
					});
					if (perUser != null)
					{
						perUserCacheNoCreate.Insert(context, perUser);
					}
				}
				return perUser;
			}

			// Token: 0x06000986 RID: 2438 RVA: 0x0004ECD4 File Offset: 0x0004CED4
			public override byte[] Serialize(Context context)
			{
				MDBEFCollection mdbefcollection = new MDBEFCollection();
				mdbefcollection.AddAnnotatedProperty(new AnnotatedPropertyValue(PerUser.MailboxGuidPropertyTag, new PropertyValue(PerUser.MailboxGuidPropertyTag, this.mailboxGuid), null));
				mdbefcollection.AddAnnotatedProperty(new AnnotatedPropertyValue(PerUser.FolderIdPropertyTag, new PropertyValue(PerUser.FolderIdPropertyTag, this.folderId.To26ByteArray()), null));
				mdbefcollection.AddAnnotatedProperty(new AnnotatedPropertyValue(PerUser.CNSetPropertyTag, new PropertyValue(PerUser.CNSetPropertyTag, this.cnSet.Serialize()), null));
				mdbefcollection.AddAnnotatedProperty(new AnnotatedPropertyValue(PerUser.LastModPropertyTag, new PropertyValue(PerUser.LastModPropertyTag, (ExDateTime)this.lastModificationTime), null));
				return mdbefcollection.Serialize(Encoding.UTF8);
			}

			// Token: 0x06000987 RID: 2439 RVA: 0x0004ED94 File Offset: 0x0004CF94
			public override void Save(Context context, MailboxState mailboxState)
			{
				using (LockManager.Lock(this, LockManager.LockType.PerUserExclusive, context.Diagnostics))
				{
					PerUserTable perUserTable = DatabaseSchema.PerUserTable(context.Database);
					StartStopKey startStopKey = new StartStopKey(true, new object[]
					{
						mailboxState.MailboxPartitionNumber,
						true,
						this.folderId.To26ByteArray(),
						this.mailboxGuid
					});
					PerUser.DeleteRows(context, startStopKey);
					if (!this.cnSet.IsEmpty)
					{
						using (DataRow dataRow = Factory.CreateDataRow(context.Culture, context, perUserTable.Table, false, new ColumnValue[]
						{
							new ColumnValue(perUserTable.MailboxPartitionNumber, mailboxState.MailboxPartitionNumber),
							new ColumnValue(perUserTable.ResidentFolder, true),
							new ColumnValue(perUserTable.FolderId, this.folderId.To26ByteArray()),
							new ColumnValue(perUserTable.Guid, this.mailboxGuid)
						}))
						{
							this.SaveIntoDataRow(context, mailboxState, perUserTable, dataRow);
						}
					}
					this.isDirty = false;
				}
			}

			// Token: 0x06000988 RID: 2440 RVA: 0x0004EF2C File Offset: 0x0004D12C
			internal bool Insert(Mailbox mailbox, ExchangeId cn)
			{
				bool result;
				using (LockManager.Lock(this, LockManager.LockType.PerUserExclusive))
				{
					if (cn.IsValid && this.cnSet.Insert(cn))
					{
						this.lastModificationTime = mailbox.UtcNow;
						this.isDirty = true;
						result = true;
					}
					else
					{
						result = false;
					}
				}
				return result;
			}

			// Token: 0x06000989 RID: 2441 RVA: 0x0004EF94 File Offset: 0x0004D194
			internal bool Remove(Mailbox mailbox, ExchangeId cn)
			{
				bool result;
				using (LockManager.Lock(this, LockManager.LockType.PerUserExclusive))
				{
					if (cn.IsValid && this.cnSet.Remove(cn))
					{
						this.lastModificationTime = mailbox.UtcNow;
						this.isDirty = true;
						result = true;
					}
					else
					{
						result = false;
					}
				}
				return result;
			}

			// Token: 0x0600098A RID: 2442 RVA: 0x0004EFFC File Offset: 0x0004D1FC
			public override bool Contains(Mailbox mailbox, ExchangeId cn)
			{
				bool result;
				using (LockManager.Lock(this, LockManager.LockType.PerUserShared))
				{
					result = (cn.IsValid && this.cnSet.Contains(cn));
				}
				return result;
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x0600098B RID: 2443 RVA: 0x0004F04C File Offset: 0x0004D24C
			public override Guid Guid
			{
				get
				{
					return this.mailboxGuid;
				}
			}

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x0600098C RID: 2444 RVA: 0x0004F054 File Offset: 0x0004D254
			public override ExchangeId FolderId
			{
				get
				{
					return this.folderId;
				}
			}

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x0600098D RID: 2445 RVA: 0x0004F05C File Offset: 0x0004D25C
			public override byte[] FolderIdBytes
			{
				get
				{
					return this.folderId.To26ByteArray();
				}
			}

			// Token: 0x1700020D RID: 525
			// (get) Token: 0x0600098E RID: 2446 RVA: 0x0004F077 File Offset: 0x0004D277
			internal override IdSet CNSet
			{
				get
				{
					return this.cnSet;
				}
			}

			// Token: 0x1700020E RID: 526
			// (get) Token: 0x0600098F RID: 2447 RVA: 0x0004F07F File Offset: 0x0004D27F
			public override byte[] CNSetBytes
			{
				get
				{
					return this.CNSet.Serialize();
				}
			}

			// Token: 0x06000990 RID: 2448 RVA: 0x0004F08C File Offset: 0x0004D28C
			public ResidentPerUser(Context context, PerUserTable perUserTable, DataRow dataRow, ReplidGuidMap replidGuidMap) : this((Guid)dataRow.GetValue(context, perUserTable.Guid), ExchangeId.CreateFrom26ByteArray(context, replidGuidMap, (byte[])dataRow.GetValue(context, perUserTable.FolderId)), IdSet.Parse(context, (byte[])dataRow.GetValue(context, perUserTable.CnsetRead)), (DateTime)dataRow.GetValue(context, perUserTable.LastModificationTime))
			{
				this.isDirty = false;
			}

			// Token: 0x06000991 RID: 2449 RVA: 0x0004F0FC File Offset: 0x0004D2FC
			internal ResidentPerUser(Guid mailboxGuid, ExchangeId folderId, IdSet cnSet, DateTime lastModificationTime)
			{
				this.mailboxGuid = mailboxGuid;
				this.folderId = folderId;
				this.cnSet = cnSet;
				this.lastModificationTime = lastModificationTime;
				this.isDirty = true;
			}

			// Token: 0x06000992 RID: 2450 RVA: 0x0004F128 File Offset: 0x0004D328
			public override ILockName GetLockNameToCache()
			{
				return new PerUser.ResidentPerUser(this.mailboxGuid, this.folderId, null, this.lastModificationTime);
			}

			// Token: 0x06000993 RID: 2451 RVA: 0x0004F144 File Offset: 0x0004D344
			private void SaveIntoDataRow(Context context, MailboxState mailboxState, PerUserTable perUserTable, DataRow dataRow)
			{
				dataRow.SetValue(context, perUserTable.ResidentFolder, true);
				dataRow.SetValue(context, perUserTable.Guid, this.mailboxGuid);
				dataRow.SetValue(context, perUserTable.FolderId, this.FolderIdBytes);
				dataRow.SetValue(context, perUserTable.CnsetRead, this.CNSetBytes);
				dataRow.SetValue(context, perUserTable.LastModificationTime, this.lastModificationTime);
				dataRow.Flush(context);
			}

			// Token: 0x040004A1 RID: 1185
			private static Hookable<Action> loadHook = Hookable<Action>.Create(true, null);

			// Token: 0x040004A2 RID: 1186
			private readonly Guid mailboxGuid;

			// Token: 0x040004A3 RID: 1187
			private readonly ExchangeId folderId;

			// Token: 0x040004A4 RID: 1188
			private readonly IdSet cnSet;
		}

		// Token: 0x020000A7 RID: 167
		internal class ForeignPerUser : PerUser
		{
			// Token: 0x06000995 RID: 2453 RVA: 0x0004F1D8 File Offset: 0x0004D3D8
			public ForeignPerUser(Context context, PerUserTable perUserTable, DataRow dataRow) : this((Guid)dataRow.GetValue(context, perUserTable.Guid), (byte[])dataRow.GetValue(context, perUserTable.FolderId), (byte[])dataRow.GetValue(context, perUserTable.CnsetRead), (DateTime)dataRow.GetValue(context, perUserTable.LastModificationTime))
			{
			}

			// Token: 0x06000996 RID: 2454 RVA: 0x0004F233 File Offset: 0x0004D433
			public ForeignPerUser(Guid replicaGuid, byte[] foreignFolderId, byte[] foreignCNSet, DateTime lastModificationTime)
			{
				this.replicaGuid = replicaGuid;
				this.folderIdBytes = foreignFolderId;
				this.cnSetBytes = foreignCNSet;
				this.lastModificationTime = lastModificationTime;
			}

			// Token: 0x06000997 RID: 2455 RVA: 0x0004F258 File Offset: 0x0004D458
			public override ILockName GetLockNameToCache()
			{
				return new PerUser.ForeignPerUser(this.replicaGuid, this.folderIdBytes, null, this.lastModificationTime);
			}

			// Token: 0x06000998 RID: 2456 RVA: 0x0004F272 File Offset: 0x0004D472
			public override byte[] Serialize(Context context)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x06000999 RID: 2457 RVA: 0x0004F279 File Offset: 0x0004D479
			public override bool Contains(Mailbox mailbox, ExchangeId cn)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x0600099A RID: 2458 RVA: 0x0004F280 File Offset: 0x0004D480
			public override void Save(Context context, MailboxState mailboxState)
			{
				using (LockManager.Lock(this, LockManager.LockType.PerUserExclusive, context.Diagnostics))
				{
					StartStopKey startStopKey = new StartStopKey(true, new object[]
					{
						mailboxState.MailboxPartitionNumber,
						false,
						this.folderIdBytes
					});
					PerUser.DeleteRows(context, startStopKey);
					if (this.cnSetBytes.Length > 0)
					{
						PerUserTable perUserTable = DatabaseSchema.PerUserTable(context.Database);
						using (DataRow dataRow = Factory.CreateDataRow(context.Culture, context, perUserTable.Table, false, new ColumnValue[]
						{
							new ColumnValue(perUserTable.MailboxPartitionNumber, mailboxState.MailboxPartitionNumber),
							new ColumnValue(perUserTable.ResidentFolder, false),
							new ColumnValue(perUserTable.FolderId, this.folderIdBytes),
							new ColumnValue(perUserTable.Guid, this.replicaGuid)
						}))
						{
							this.SaveIntoDataRow(context, perUserTable, dataRow);
						}
					}
				}
			}

			// Token: 0x1700020F RID: 527
			// (get) Token: 0x0600099B RID: 2459 RVA: 0x0004F3EC File Offset: 0x0004D5EC
			public override Guid Guid
			{
				get
				{
					return this.replicaGuid;
				}
			}

			// Token: 0x17000210 RID: 528
			// (get) Token: 0x0600099C RID: 2460 RVA: 0x0004F3F4 File Offset: 0x0004D5F4
			public override ExchangeId FolderId
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17000211 RID: 529
			// (get) Token: 0x0600099D RID: 2461 RVA: 0x0004F3FB File Offset: 0x0004D5FB
			public override byte[] FolderIdBytes
			{
				get
				{
					return this.folderIdBytes;
				}
			}

			// Token: 0x17000212 RID: 530
			// (get) Token: 0x0600099E RID: 2462 RVA: 0x0004F403 File Offset: 0x0004D603
			internal override IdSet CNSet
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17000213 RID: 531
			// (get) Token: 0x0600099F RID: 2463 RVA: 0x0004F40A File Offset: 0x0004D60A
			public override byte[] CNSetBytes
			{
				get
				{
					return this.cnSetBytes;
				}
			}

			// Token: 0x060009A0 RID: 2464 RVA: 0x0004F414 File Offset: 0x0004D614
			private void SaveIntoDataRow(Context context, PerUserTable perUserTable, DataRow dataRow)
			{
				dataRow.SetValue(context, perUserTable.ResidentFolder, false);
				dataRow.SetValue(context, perUserTable.Guid, this.replicaGuid);
				dataRow.SetValue(context, perUserTable.FolderId, this.folderIdBytes);
				dataRow.SetValue(context, perUserTable.CnsetRead, this.cnSetBytes);
				dataRow.SetValue(context, perUserTable.LastModificationTime, this.lastModificationTime);
				dataRow.Flush(context);
			}

			// Token: 0x040004A5 RID: 1189
			private readonly Guid replicaGuid;

			// Token: 0x040004A6 RID: 1190
			private readonly byte[] folderIdBytes;

			// Token: 0x040004A7 RID: 1191
			private readonly byte[] cnSetBytes;
		}

		// Token: 0x020000A8 RID: 168
		internal struct PerUserKey : IComparable, IComparable<PerUser.PerUserKey>, IEquatable<PerUser.PerUserKey>
		{
			// Token: 0x060009A1 RID: 2465 RVA: 0x0004F491 File Offset: 0x0004D691
			public PerUserKey(Guid mailboxGuid, ExchangeId folderId)
			{
				this.mailboxGuid = mailboxGuid;
				this.folderId = folderId;
			}

			// Token: 0x060009A2 RID: 2466 RVA: 0x0004F4A1 File Offset: 0x0004D6A1
			public int CompareTo(object other)
			{
				return this.CompareTo((PerUser.PerUserKey)other);
			}

			// Token: 0x060009A3 RID: 2467 RVA: 0x0004F4B0 File Offset: 0x0004D6B0
			public int CompareTo(PerUser.PerUserKey other)
			{
				if (object.ReferenceEquals(this, other))
				{
					return 0;
				}
				int num = this.folderId.CompareTo(other.folderId);
				if (num == 0)
				{
					num = this.mailboxGuid.CompareTo(other.mailboxGuid);
				}
				return num;
			}

			// Token: 0x060009A4 RID: 2468 RVA: 0x0004F507 File Offset: 0x0004D707
			public override bool Equals(object other)
			{
				return this.Equals((PerUser.PerUserKey)other);
			}

			// Token: 0x060009A5 RID: 2469 RVA: 0x0004F515 File Offset: 0x0004D715
			public bool Equals(PerUser.PerUserKey other)
			{
				return this.CompareTo(other) == 0;
			}

			// Token: 0x060009A6 RID: 2470 RVA: 0x0004F524 File Offset: 0x0004D724
			public override int GetHashCode()
			{
				return this.folderId.GetHashCode() ^ this.mailboxGuid.GetHashCode();
			}

			// Token: 0x040004A8 RID: 1192
			private readonly Guid mailboxGuid;

			// Token: 0x040004A9 RID: 1193
			private readonly ExchangeId folderId;
		}

		// Token: 0x020000A9 RID: 169
		internal class PerUserCache : SingleKeyCache<PerUser.PerUserKey, PerUser.ResidentPerUser>, ICache
		{
			// Token: 0x060009A7 RID: 2471 RVA: 0x0004F55A File Offset: 0x0004D75A
			internal static void Initialize()
			{
			}

			// Token: 0x060009A8 RID: 2472 RVA: 0x0004F55C File Offset: 0x0004D75C
			internal static void MountEventHandler(Context context, StoreDatabase database, bool readOnly)
			{
				if (!readOnly && !PerUser.PerUserCache.skipFlushDirtyPerUserCachesTaskTestHook.Value)
				{
					Task<StoreDatabase>.TaskCallback callback = PerUser.WrappedFlushCallback(database.MdbGuid);
					RecurringTask<StoreDatabase> task = new RecurringTask<StoreDatabase>(callback, database, TimeSpan.FromHours(1.0), false);
					database.TaskList.Add(task, true);
				}
			}

			// Token: 0x060009A9 RID: 2473 RVA: 0x0004F5AC File Offset: 0x0004D7AC
			internal static void DismountEventHandler(Context context, StoreDatabase database)
			{
				using (database.SharedLock(context.Diagnostics))
				{
					PerUser.PerUserCache.FlushDirtyPerUserCaches(context, database, () => true);
				}
			}

			// Token: 0x060009AA RID: 2474 RVA: 0x0004F60C File Offset: 0x0004D80C
			internal static void FlushDirtyPerUserCachesTaskCallback(Context context, StoreDatabase database, Func<bool> shouldTaskContinue)
			{
				using (context.AssociateWithDatabase(database))
				{
					PerUser.PerUserCache.FlushDirtyPerUserCaches(context, database, shouldTaskContinue);
				}
			}

			// Token: 0x060009AB RID: 2475 RVA: 0x0004F64C File Offset: 0x0004D84C
			internal static IDisposable SetSkipFlushDirtyPerUserCachesTaskTestHook()
			{
				return PerUser.PerUserCache.skipFlushDirtyPerUserCachesTaskTestHook.SetTestHook(true);
			}

			// Token: 0x060009AC RID: 2476 RVA: 0x0004F65C File Offset: 0x0004D85C
			private static void FlushDirtyPerUserCaches(Context context, StoreDatabase database, Func<bool> shouldTaskContinue)
			{
				if (database.IsReadOnly)
				{
					return;
				}
				List<int> activeMailboxNumbers = MailboxStateCache.GetActiveMailboxNumbers(context);
				if (activeMailboxNumbers == null)
				{
					return;
				}
				for (int i = 0; i < activeMailboxNumbers.Count; i++)
				{
					if (!shouldTaskContinue())
					{
						return;
					}
					context.ResetFailureHistory();
					context.InitializeMailboxExclusiveOperation(activeMailboxNumbers[i], ExecutionDiagnostics.OperationSource.PerUserCacheFlush, TimeSpan.FromMinutes(1.0));
					bool commit = false;
					try
					{
						ErrorCode first = context.StartMailboxOperation(MailboxCreation.DontAllow, false, true);
						if (!(first != ErrorCode.NoError))
						{
							MailboxState lockedMailboxState = context.LockedMailboxState;
							try
							{
								lockedMailboxState.AddReference();
								if (!lockedMailboxState.CurrentlyActive)
								{
									goto IL_B4;
								}
								ICache perUserCache = lockedMailboxState.PerUserCache;
								if (perUserCache != null)
								{
									perUserCache.FlushAllDirtyEntries(context);
								}
							}
							finally
							{
								lockedMailboxState.ReleaseReference();
							}
							commit = true;
						}
					}
					finally
					{
						if (context.IsMailboxOperationStarted)
						{
							context.EndMailboxOperation(commit);
						}
					}
					IL_B4:;
				}
			}

			// Token: 0x060009AD RID: 2477 RVA: 0x0004F74C File Offset: 0x0004D94C
			public static PerUser.PerUserCache GetPerUserCache(Context context, MailboxState mailboxState)
			{
				PerUser.PerUserCache perUserCache = mailboxState.PerUserCache as PerUser.PerUserCache;
				if (perUserCache == null)
				{
					EvictionPolicy<PerUser.PerUserKey> evictionPolicy = new LRU2WithTimeToLiveExpirationPolicy<PerUser.PerUserKey>(ConfigurationSchema.PerUserCacheSize.Value, ConfigurationSchema.PerUserCacheExpiration.Value, false);
					mailboxState.PerUserCache = new PerUser.PerUserCache(mailboxState, evictionPolicy, null);
					perUserCache = (PerUser.PerUserCache)mailboxState.PerUserCache;
				}
				return perUserCache;
			}

			// Token: 0x060009AE RID: 2478 RVA: 0x0004F7A0 File Offset: 0x0004D9A0
			public static PerUser.PerUserCache GetPerUserCacheNoCreate(MailboxState mailboxState)
			{
				return mailboxState.PerUserCache as PerUser.PerUserCache;
			}

			// Token: 0x060009AF RID: 2479 RVA: 0x0004F7BC File Offset: 0x0004D9BC
			public static MailboxComponentOperationFrame TakeReadLock(Context context, Mailbox mailbox)
			{
				PerUser.PerUserCache perUserCache = PerUser.PerUserCache.GetPerUserCache(context, mailbox.SharedState);
				return perUserCache.TakeReadLock(context);
			}

			// Token: 0x060009B0 RID: 2480 RVA: 0x0004F7E0 File Offset: 0x0004D9E0
			public static MailboxComponentOperationFrame TakeWriteLock(Context context, Mailbox mailbox)
			{
				PerUser.PerUserCache perUserCache = PerUser.PerUserCache.GetPerUserCache(context, mailbox.SharedState);
				return perUserCache.TakeWriteLock(context);
			}

			// Token: 0x060009B1 RID: 2481 RVA: 0x0004F804 File Offset: 0x0004DA04
			[Conditional("DEBUG")]
			public static void AssertWriteLockHeld(MailboxState mailboxState)
			{
				PerUser.PerUserCache perUserCacheNoCreate = PerUser.PerUserCache.GetPerUserCacheNoCreate(mailboxState);
			}

			// Token: 0x060009B2 RID: 2482 RVA: 0x0004F81C File Offset: 0x0004DA1C
			[Conditional("DEBUG")]
			public static void AssertLockHeld(MailboxState mailboxState)
			{
				PerUser.PerUserCache perUserCacheNoCreate = PerUser.PerUserCache.GetPerUserCacheNoCreate(mailboxState);
			}

			// Token: 0x060009B3 RID: 2483 RVA: 0x0004F832 File Offset: 0x0004DA32
			public MailboxComponentOperationFrame TakeReadLock(Context context)
			{
				return context.MailboxComponentReadOperation(this.cacheLock);
			}

			// Token: 0x060009B4 RID: 2484 RVA: 0x0004F840 File Offset: 0x0004DA40
			public bool HasReadLock()
			{
				return this.cacheLock.TestSharedLock();
			}

			// Token: 0x060009B5 RID: 2485 RVA: 0x0004F84D File Offset: 0x0004DA4D
			public MailboxComponentOperationFrame TakeWriteLock(Context context)
			{
				return context.MailboxComponentWriteOperation(this.cacheLock);
			}

			// Token: 0x060009B6 RID: 2486 RVA: 0x0004F85B File Offset: 0x0004DA5B
			public bool HasWriteLock()
			{
				return this.cacheLock.TestExclusiveLock();
			}

			// Token: 0x060009B7 RID: 2487 RVA: 0x0004F868 File Offset: 0x0004DA68
			[Conditional("DEBUG")]
			public void AssertLockHeld()
			{
			}

			// Token: 0x060009B8 RID: 2488 RVA: 0x0004F86A File Offset: 0x0004DA6A
			[Conditional("DEBUG")]
			public void AssertWriteLockHeld()
			{
			}

			// Token: 0x060009B9 RID: 2489 RVA: 0x0004F86C File Offset: 0x0004DA6C
			internal PerUserCache(MailboxState mailboxState, EvictionPolicy<PerUser.PerUserKey> evictionPolicy, ICachePerformanceCounters perfCounters) : base(evictionPolicy, perfCounters)
			{
				this.mailboxState = mailboxState;
				this.cacheLock = new PerUser.PerUserCache.PerUserCacheLockableComponent(this);
			}

			// Token: 0x060009BA RID: 2490 RVA: 0x0004F889 File Offset: 0x0004DA89
			public override void Reset()
			{
				base.Reset();
			}

			// Token: 0x060009BB RID: 2491 RVA: 0x0004F894 File Offset: 0x0004DA94
			public bool FlushAllDirtyEntries(Context context)
			{
				if (this.mailboxState.Status != MailboxStatus.UserAccessible)
				{
					this.mailboxState.PerUserCache = null;
					return false;
				}
				bool result;
				using (MailboxComponentOperationFrame mailboxComponentOperationFrame = this.TakeWriteLock(context))
				{
					SortedDictionary<PerUser.PerUserKey, PerUser.ResidentPerUser> sortedDictionary = new SortedDictionary<PerUser.PerUserKey, PerUser.ResidentPerUser>();
					foreach (KeyValuePair<PerUser.PerUserKey, PerUser.ResidentPerUser> keyValuePair in this.keyToData)
					{
						if (keyValuePair.Value.IsDirty)
						{
							sortedDictionary.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
					if (sortedDictionary.Count != 0)
					{
						this.Flush(context, sortedDictionary);
					}
					mailboxComponentOperationFrame.Success();
					result = (sortedDictionary.Count != 0);
				}
				return result;
			}

			// Token: 0x060009BC RID: 2492 RVA: 0x0004F970 File Offset: 0x0004DB70
			public void Insert(Context context, PerUser.ResidentPerUser perUser)
			{
				PerUser.PerUserKey key = new PerUser.PerUserKey(perUser.Guid, perUser.FolderId);
				try
				{
					PerUser.ResidentPerUser perUser2;
					if (this.keyToData.TryGetValue(key, out perUser2))
					{
						this.RemoveNoLock(context, perUser2);
					}
					this.currentOperationContext = context;
					base.Insert(key, perUser);
				}
				finally
				{
					this.currentOperationContext = null;
				}
			}

			// Token: 0x060009BD RID: 2493 RVA: 0x0004F9D4 File Offset: 0x0004DBD4
			public PerUser.ResidentPerUser Find(Context context, Guid mailboxGuid, ExchangeId folderId)
			{
				PerUser.PerUserKey key = new PerUser.PerUserKey(mailboxGuid, folderId);
				PerUser.ResidentPerUser result;
				using (LockManager.Lock(this, context.Diagnostics))
				{
					result = base.Find(key, false);
				}
				return result;
			}

			// Token: 0x060009BE RID: 2494 RVA: 0x0004FA24 File Offset: 0x0004DC24
			public void Remove(Context context, PerUser.ResidentPerUser perUser)
			{
				using (MailboxComponentOperationFrame mailboxComponentOperationFrame = this.TakeWriteLock(context))
				{
					this.RemoveNoLock(context, perUser);
					mailboxComponentOperationFrame.Success();
				}
			}

			// Token: 0x060009BF RID: 2495 RVA: 0x0004FA68 File Offset: 0x0004DC68
			public void RemoveNoLock(Context context, PerUser.ResidentPerUser perUser)
			{
				PerUser.PerUserKey key = new PerUser.PerUserKey(perUser.Guid, perUser.FolderId);
				try
				{
					this.currentOperationContext = context;
					base.Remove(key);
				}
				finally
				{
					this.currentOperationContext = null;
				}
				if (perUser.IsDirty)
				{
					this.Flush(context, new SortedDictionary<PerUser.PerUserKey, PerUser.ResidentPerUser>
					{
						{
							key,
							perUser
						}
					});
				}
			}

			// Token: 0x060009C0 RID: 2496 RVA: 0x0004FAD0 File Offset: 0x0004DCD0
			public override void EvictionCheckpoint()
			{
				this.evictionPolicy.EvictionCheckpoint();
				if (this.performanceCounters != null)
				{
					this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
				}
				if (this.evictionPolicy.CountOfKeysToCleanup > 0)
				{
					SortedDictionary<PerUser.PerUserKey, PerUser.ResidentPerUser> sortedDictionary = new SortedDictionary<PerUser.PerUserKey, PerUser.ResidentPerUser>();
					foreach (PerUser.PerUserKey key in this.evictionPolicy.GetKeysToCleanup(true))
					{
						PerUser.ResidentPerUser residentPerUser;
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.keyToData.TryGetValue(key, out residentPerUser), "We should have the eviction key in our cache");
						if (residentPerUser.IsDirty)
						{
							sortedDictionary.Add(key, residentPerUser);
						}
						this.keyToData.Remove(key);
						if (this.performanceCounters != null)
						{
							this.performanceCounters.CacheRemoves.Increment();
						}
					}
					if (sortedDictionary.Count != 0)
					{
						this.Flush(this.currentOperationContext, sortedDictionary);
					}
				}
				if (this.performanceCounters != null)
				{
					this.performanceCounters.CacheSize.RawValue = (long)this.keyToData.Count;
					this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
				}
			}

			// Token: 0x060009C1 RID: 2497 RVA: 0x0004FBF4 File Offset: 0x0004DDF4
			private void Flush(Context context, SortedDictionary<PerUser.PerUserKey, PerUser.ResidentPerUser> toFlush)
			{
				foreach (KeyValuePair<PerUser.PerUserKey, PerUser.ResidentPerUser> keyValuePair in toFlush)
				{
					ICachePerformanceCounters performanceCounters = this.performanceCounters;
					keyValuePair.Value.Save(context, this.mailboxState);
				}
			}

			// Token: 0x040004AA RID: 1194
			private static readonly Hookable<bool> skipFlushDirtyPerUserCachesTaskTestHook = Hookable<bool>.Create(true, false);

			// Token: 0x040004AB RID: 1195
			private PerUser.PerUserCache.PerUserCacheLockableComponent cacheLock;

			// Token: 0x040004AC RID: 1196
			private MailboxState mailboxState;

			// Token: 0x040004AD RID: 1197
			private Context currentOperationContext;

			// Token: 0x020000AA RID: 170
			private class PerUserCacheLockableComponent : LockableMailboxComponent
			{
				// Token: 0x060009C4 RID: 2500 RVA: 0x0004FC66 File Offset: 0x0004DE66
				public PerUserCacheLockableComponent(PerUser.PerUserCache perUserCache)
				{
					this.perUserCache = perUserCache;
				}

				// Token: 0x17000214 RID: 532
				// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0004FC75 File Offset: 0x0004DE75
				public override int MailboxPartitionNumber
				{
					get
					{
						return this.perUserCache.mailboxState.MailboxPartitionNumber;
					}
				}

				// Token: 0x17000215 RID: 533
				// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0004FC87 File Offset: 0x0004DE87
				public override Guid DatabaseGuid
				{
					get
					{
						return this.perUserCache.mailboxState.DatabaseGuid;
					}
				}

				// Token: 0x17000216 RID: 534
				// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0004FC99 File Offset: 0x0004DE99
				public override LockManager.LockType ReaderLockType
				{
					get
					{
						return LockManager.LockType.PerUserCacheShared;
					}
				}

				// Token: 0x17000217 RID: 535
				// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0004FC9D File Offset: 0x0004DE9D
				public override LockManager.LockType WriterLockType
				{
					get
					{
						return LockManager.LockType.PerUserCacheExclusive;
					}
				}

				// Token: 0x17000218 RID: 536
				// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0004FCA1 File Offset: 0x0004DEA1
				public override MailboxComponentId MailboxComponentId
				{
					get
					{
						return MailboxComponentId.PerUserCache;
					}
				}

				// Token: 0x060009CA RID: 2506 RVA: 0x0004FCA4 File Offset: 0x0004DEA4
				public override bool IsValidTableOperation(Context context, Connection.OperationType operationType, Table table, IList<object> partitionValues)
				{
					switch (operationType)
					{
					case Connection.OperationType.CreateTable:
					case Connection.OperationType.DeleteTable:
						return this.TestExclusiveLock();
					default:
					{
						PerUserTable perUserTable = DatabaseSchema.PerUserTable(this.perUserCache.mailboxState.Database);
						bool flag = table.Equals(perUserTable.Table);
						switch (operationType)
						{
						case Connection.OperationType.Query:
							return (this.TestSharedLock() || this.TestExclusiveLock()) && flag;
						case Connection.OperationType.Insert:
						case Connection.OperationType.Update:
						case Connection.OperationType.Delete:
							return this.TestExclusiveLock() && flag;
						default:
							return flag;
						}
						break;
					}
					}
				}

				// Token: 0x040004AF RID: 1199
				private readonly PerUser.PerUserCache perUserCache;
			}
		}
	}
}
