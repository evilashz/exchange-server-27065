using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000E1 RID: 225
	public class UserInformation : PrivateObjectPropertyBag
	{
		// Token: 0x06000C1E RID: 3102 RVA: 0x000614E0 File Offset: 0x0005F6E0
		internal static void Initialize()
		{
			UserInfoUpgrader.InitializeUpgraderAction(delegate(Context context)
			{
				UserInfoTable userInfoTable = DatabaseSchema.UserInfoTable(context.Database);
				userInfoTable.Table.CreateTable(context, UserInfoUpgrader.Instance.To.Value);
			}, delegate(StoreDatabase database)
			{
				UserInfoTable userInfoTable = DatabaseSchema.UserInfoTable(database);
				userInfoTable.Table.MinVersion = UserInfoUpgrader.Instance.To.Value;
			});
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0006152C File Offset: 0x0005F72C
		internal static void LockUserEntryForModification(Guid userInformationGuid)
		{
			using (LockManager.Lock(UserInformation.lockedEntryGuids, LockManager.LockType.LeafMonitorLock))
			{
				UserInformation.lockedEntryGuids.Add(userInformationGuid);
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00061574 File Offset: 0x0005F774
		internal static void UnlockUserEntryForModification(Guid userInformationGuid)
		{
			using (LockManager.Lock(UserInformation.lockedEntryGuids, LockManager.LockType.LeafMonitorLock))
			{
				UserInformation.lockedEntryGuids.Remove(userInformationGuid);
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000615BC File Offset: 0x0005F7BC
		internal static bool IsEntryLockedForModification(Guid userInformationGuid)
		{
			bool result;
			using (LockManager.Lock(UserInformation.lockedEntryGuids, LockManager.LockType.LeafMonitorLock))
			{
				result = UserInformation.lockedEntryGuids.Contains(userInformationGuid);
			}
			return result;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00061604 File Offset: 0x0005F804
		private UserInformation(Context context, ColumnValue userInformationGuid) : base(context, DatabaseSchema.UserInfoTable(context.Database).Table, true, false, true, true, new ColumnValue[]
		{
			userInformationGuid
		})
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.currentOperationContext = context;
				base.LoadData(context);
				disposeGuard.Success();
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00061680 File Offset: 0x0005F880
		private UserInformation(Context context, Reader reader) : base(context, DatabaseSchema.UserInfoTable(context.Database).Table, false, true, reader)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.currentOperationContext = context;
				base.LoadData(context);
				disposeGuard.Success();
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x000616E4 File Offset: 0x0005F8E4
		public static void Create(Context context, Guid userInformationGuid, Properties initialProperties)
		{
			UserInformation.Create(context, userInformationGuid, initialProperties, false);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x000616F0 File Offset: 0x0005F8F0
		public static void Create(Context context, Guid userInformationGuid, Properties initialProperties, bool moveMailbox)
		{
			if (!DefaultSettings.Get.UserInformationIsEnabled)
			{
				throw new StoreException((LID)62028U, ErrorCodeValue.NotSupported);
			}
			if (!UserInfoUpgrader.IsReady(context, context.Database))
			{
				throw new StoreException((LID)47868U, ErrorCodeValue.NotSupported);
			}
			UserInformation.ValidatePropertiesOnSet(context, initialProperties, moveMailbox);
			UserInfoTable userInfoTable = DatabaseSchema.UserInfoTable(context.Database);
			using (UserInformation.LockExclusive(context, userInformationGuid))
			{
				if (UserInformation.IsEntryLockedForModification(userInformationGuid))
				{
					throw new StoreException((LID)44492U, ErrorCodeValue.UserInformationNoAccess);
				}
				try
				{
					using (TableOperator tableOperator = UserInformation.CreateTableOperator(context, userInformationGuid))
					{
						using (Reader reader = tableOperator.ExecuteReader(false))
						{
							if (reader.Read())
							{
								UserInformation.UserInformationStatus @int = (UserInformation.UserInformationStatus)reader.GetInt16(userInfoTable.Status);
								if (moveMailbox || @int == UserInformation.UserInformationStatus.SoftDeleted)
								{
									DiagnosticContext.TraceLocation((LID)46156U);
									using (UserInformation userInformation = new UserInformation(context, reader))
									{
										userInformation.Delete(context, false);
										goto IL_EE;
									}
								}
								throw new StoreException((LID)64252U, ErrorCodeValue.UserInformationAlreadyExists);
							}
							IL_EE:;
						}
					}
					using (UserInformation userInformation2 = new UserInformation(context, new ColumnValue(userInfoTable.UserGuid, userInformationGuid)))
					{
						foreach (Property property in initialProperties)
						{
							ErrorCode errorCode = userInformation2.SetProperty(context, property.Tag, property.Value);
							if (errorCode != ErrorCode.NoError)
							{
								DiagnosticContext.TracePropTagError((LID)56060U, (uint)((int)errorCode), property.Tag.PropTag);
								throw new StoreException((LID)43772U, errorCode);
							}
						}
						DateTime utcNow = DateTime.UtcNow;
						userInformation2.SetColumn(context, userInfoTable.Status, 1);
						if (userInformation2.GetColumnValue(context, userInfoTable.CreationTime) == null)
						{
							userInformation2.SetColumn(context, userInfoTable.CreationTime, utcNow);
						}
						if (userInformation2.GetColumnValue(context, userInfoTable.LastModificationTime) == null)
						{
							userInformation2.SetColumn(context, userInfoTable.LastModificationTime, utcNow);
						}
						if (userInformation2.GetColumnValue(context, userInfoTable.ChangeNumber) == null)
						{
							userInformation2.SetColumn(context, userInfoTable.ChangeNumber, 1L);
						}
						userInformation2.Flush(context);
					}
					context.Commit();
				}
				finally
				{
					context.Abort();
				}
			}
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00061A18 File Offset: 0x0005FC18
		public static Properties Read(Context context, Guid userInformationGuid, StorePropTag[] propertyTags)
		{
			if (!DefaultSettings.Get.UserInformationIsEnabled)
			{
				throw new StoreException((LID)48588U, ErrorCodeValue.NotSupported);
			}
			if (!UserInfoUpgrader.IsReady(context, context.Database))
			{
				throw new StoreException((LID)60156U, ErrorCodeValue.NotSupported);
			}
			UserInformation.ValidatePropertiesOnGet(context, propertyTags);
			Properties result;
			using (UserInformation.LockShared(context, userInformationGuid))
			{
				using (TableOperator tableOperator = UserInformation.CreateTableOperator(context, userInformationGuid))
				{
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						if (!reader.Read())
						{
							throw new StoreException((LID)51964U, ErrorCodeValue.UserInformationNotFound);
						}
						using (UserInformation userInformation = new UserInformation(context, reader))
						{
							Properties properties = new Properties(propertyTags.Length);
							foreach (StorePropTag storePropTag in propertyTags)
							{
								object propertyValue = userInformation.GetPropertyValue(context, storePropTag);
								if (propertyValue != null)
								{
									properties.Add(storePropTag, propertyValue);
								}
								else
								{
									properties.Add(Property.NotFoundError(storePropTag));
								}
							}
							result = properties;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00061B70 File Offset: 0x0005FD70
		public static void Update(Context context, Guid userInformationGuid, Properties properties)
		{
			if (!DefaultSettings.Get.UserInformationIsEnabled)
			{
				throw new StoreException((LID)36300U, ErrorCodeValue.NotSupported);
			}
			if (!UserInfoUpgrader.IsReady(context, context.Database))
			{
				throw new StoreException((LID)45820U, ErrorCodeValue.NotSupported);
			}
			UserInformation.ValidatePropertiesOnSet(context, properties, false);
			UserInfoTable userInfoTable = DatabaseSchema.UserInfoTable(context.Database);
			using (UserInformation.LockExclusive(context, userInformationGuid))
			{
				if (UserInformation.IsEntryLockedForModification(userInformationGuid))
				{
					throw new StoreException((LID)49740U, ErrorCodeValue.UserInformationNoAccess);
				}
				try
				{
					using (TableOperator tableOperator = UserInformation.CreateTableOperator(context, userInformationGuid))
					{
						using (Reader reader = tableOperator.ExecuteReader(false))
						{
							if (!reader.Read())
							{
								throw new StoreException((LID)62204U, ErrorCodeValue.UserInformationNotFound);
							}
							using (UserInformation userInformation = new UserInformation(context, reader))
							{
								UserInformation.UserInformationStatus @int = (UserInformation.UserInformationStatus)reader.GetInt16(userInfoTable.Status);
								if (@int == UserInformation.UserInformationStatus.SoftDeleted)
								{
									throw new StoreException((LID)37452U, ErrorCodeValue.UserInformationSoftDeleted);
								}
								if (@int == UserInformation.UserInformationStatus.Disabled)
								{
									userInformation.SetColumn(context, userInfoTable.Status, 1);
								}
								foreach (Property property in properties)
								{
									ErrorCode errorCode = userInformation.SetProperty(context, property.Tag, property.Value);
									if (errorCode != ErrorCode.NoError)
									{
										DiagnosticContext.TracePropTagError((LID)54012U, (uint)((int)errorCode), property.Tag.PropTag);
										throw new StoreException((LID)33532U, errorCode);
									}
								}
								userInformation.SetColumn(context, userInfoTable.LastModificationTime, DateTime.UtcNow);
								userInformation.SetColumn(context, userInfoTable.ChangeNumber, (long)userInformation.GetColumnValue(context, userInfoTable.ChangeNumber) + 1L);
								userInformation.Flush(context);
							}
						}
					}
					context.Commit();
				}
				finally
				{
					context.Abort();
				}
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00061E1C File Offset: 0x0006001C
		public static bool TryMarkAsSoftDeleted(Context context, Guid userInformationGuid)
		{
			if (!DefaultSettings.Get.UserInformationIsEnabled)
			{
				throw new StoreException((LID)35404U, ErrorCodeValue.NotSupported);
			}
			if (!UserInfoUpgrader.IsReady(context, context.Database))
			{
				throw new StoreException((LID)53836U, ErrorCodeValue.NotSupported);
			}
			UserInfoTable userInfoTable = DatabaseSchema.UserInfoTable(context.Database);
			using (UserInformation.LockExclusive(context, userInformationGuid))
			{
				using (TableOperator tableOperator = UserInformation.CreateTableOperator(context, userInformationGuid))
				{
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						if (!reader.Read())
						{
							return false;
						}
						using (UserInformation userInformation = new UserInformation(context, reader))
						{
							userInformation.SetColumn(context, userInfoTable.Status, 3);
							userInformation.SetColumn(context, userInfoTable.DeletedOn, DateTime.UtcNow);
							userInformation.Flush(context);
						}
					}
				}
				context.Commit();
			}
			return true;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00061F4C File Offset: 0x0006014C
		public static void Delete(Context context, Guid userInformationGuid)
		{
			if (!DefaultSettings.Get.UserInformationIsEnabled)
			{
				throw new StoreException((LID)52684U, ErrorCodeValue.NotSupported);
			}
			if (!UserInfoUpgrader.IsReady(context, context.Database))
			{
				throw new StoreException((LID)64828U, ErrorCodeValue.NotSupported);
			}
			using (UserInformation.LockExclusive(context, userInformationGuid))
			{
				using (TableOperator tableOperator = UserInformation.CreateTableOperator(context, userInformationGuid))
				{
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						if (!reader.Read())
						{
							throw new StoreException((LID)58108U, ErrorCodeValue.UserInformationNotFound);
						}
						using (UserInformation userInformation = new UserInformation(context, reader))
						{
							userInformation.Delete(context, false);
						}
					}
				}
				context.Commit();
			}
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00062068 File Offset: 0x00060268
		public static Properties GetAllProperties(Context context, Guid userInformationGuid)
		{
			if (!DefaultSettings.Get.UserInformationIsEnabled)
			{
				throw new StoreException((LID)46540U, ErrorCodeValue.NotSupported);
			}
			if (!UserInfoUpgrader.IsReady(context, context.Database))
			{
				throw new StoreException((LID)64588U, ErrorCodeValue.NotSupported);
			}
			Properties result;
			using (UserInformation.LockShared(context, userInformationGuid))
			{
				using (TableOperator tableOperator = UserInformation.CreateTableOperator(context, userInformationGuid))
				{
					using (Reader reader = tableOperator.ExecuteReader(false))
					{
						if (!reader.Read())
						{
							DiagnosticContext.TraceLocation((LID)52300U);
							result = Microsoft.Exchange.Server.Storage.StoreCommonServices.Properties.Empty;
						}
						else
						{
							using (UserInformation userInformation = new UserInformation(context, reader))
							{
								Properties properties = new Properties(10);
								userInformation.EnumerateProperties(context, delegate(StorePropTag propTag, object propValue)
								{
									properties.Add(propTag, propValue);
									return true;
								}, true);
								result = properties;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00062194 File Offset: 0x00060394
		private static TableOperator CreateTableOperator(Context context, Guid userInformationGuid)
		{
			UserInfoTable userInfoTable = DatabaseSchema.UserInfoTable(context.Database);
			KeyRange keyRange = new KeyRange(new StartStopKey(true, new object[]
			{
				userInformationGuid
			}), new StartStopKey(true, new object[]
			{
				userInformationGuid
			}));
			List<Column> list = new List<Column>(userInfoTable.Table.CommonColumns.Count + 1);
			list.AddRange(userInfoTable.Table.CommonColumns);
			list.Add(userInfoTable.UserGuid);
			return Factory.CreateTableOperator(context.Culture, context, userInfoTable.Table, userInfoTable.UserInfoPK, list, null, null, 0, 0, keyRange, false, false);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0006223C File Offset: 0x0006043C
		private static void ValidatePropertiesOnSet(Context context, Properties properties, bool moveMailbox)
		{
			foreach (Property property in properties)
			{
				if (property.Tag.PropInfo != null && property.Tag.IsCategory(3) && (!moveMailbox || !property.Tag.IsCategory(4)))
				{
					DiagnosticContext.TracePropTagError((LID)49916U, 2833U, property.Tag.PropTag);
					throw new StoreException((LID)41724U, ErrorCodeValue.UserInformationNoAccess);
				}
				if (property.Tag.IsNamedProperty)
				{
					DiagnosticContext.TracePropTagError((LID)63308U, 2834U, property.Tag.PropTag);
					throw new StoreException((LID)47948U, ErrorCodeValue.UserInformationPropertyError);
				}
				if (property.Value != null && property.Value is byte[] && ((byte[])property.Value).Length > 65536)
				{
					DiagnosticContext.TracePropTagError((LID)40252U, 2834U, property.Tag.PropTag);
					throw new StoreException((LID)56636U, ErrorCodeValue.UserInformationPropertyError);
				}
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000623B8 File Offset: 0x000605B8
		private static void ValidatePropertiesOnGet(Context context, StorePropTag[] propertyTags)
		{
			foreach (StorePropTag storePropTag in propertyTags)
			{
				if (storePropTag.IsNamedProperty)
				{
					DiagnosticContext.TracePropTagError((LID)55116U, 2834U, storePropTag.PropTag);
					throw new StoreException((LID)35660U, ErrorCodeValue.UserInformationPropertyError);
				}
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0006241B File Offset: 0x0006061B
		protected override ObjectType GetObjectType()
		{
			return ObjectType.UserInfo;
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0006241F File Offset: 0x0006061F
		public override Context CurrentOperationContext
		{
			get
			{
				return this.currentOperationContext;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00062427 File Offset: 0x00060627
		public override ObjectPropertySchema Schema
		{
			get
			{
				if (this.propertySchema == null)
				{
					this.propertySchema = PropertySchema.GetObjectSchema(this.CurrentOperationContext.Database, ObjectType.UserInfo);
				}
				return this.propertySchema;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0006244F File Offset: 0x0006064F
		public override ReplidGuidMap ReplidGuidMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00062452 File Offset: 0x00060652
		protected override StorePropTag MapPropTag(Context context, uint propertyTag)
		{
			return WellKnownProperties.GetPropTag(propertyTag, ObjectType.UserInfo);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0006245C File Offset: 0x0006065C
		private static UserInformation.UserInformationLockFrame LockShared(Context context, Guid userInformationGuid)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(!context.TransactionStarted, "Transaction leaked.");
			return new UserInformation.UserInformationLockFrame(new UserInformation.UserInformationLockName(userInformationGuid), true, context.Diagnostics);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00062483 File Offset: 0x00060683
		private static UserInformation.UserInformationLockFrame LockExclusive(Context context, Guid userInformationGuid)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(!context.TransactionStarted, "Transaction leaked.");
			return new UserInformation.UserInformationLockFrame(new UserInformation.UserInformationLockName(userInformationGuid), false, context.Diagnostics);
		}

		// Token: 0x040005AC RID: 1452
		private const int MaxBinaryPropertySize = 65536;

		// Token: 0x040005AD RID: 1453
		private static readonly TimeSpan lockTimeout = TimeSpan.FromSeconds(20.0);

		// Token: 0x040005AE RID: 1454
		private static readonly HashSet<Guid> lockedEntryGuids = new HashSet<Guid>();

		// Token: 0x040005AF RID: 1455
		private readonly Context currentOperationContext;

		// Token: 0x040005B0 RID: 1456
		private ObjectPropertySchema propertySchema;

		// Token: 0x020000E2 RID: 226
		private enum UserInformationStatus
		{
			// Token: 0x040005B4 RID: 1460
			Invalid,
			// Token: 0x040005B5 RID: 1461
			Active,
			// Token: 0x040005B6 RID: 1462
			Disabled,
			// Token: 0x040005B7 RID: 1463
			SoftDeleted
		}

		// Token: 0x020000E3 RID: 227
		private class UserInformationLockName : ILockName, IEquatable<ILockName>, IComparable<ILockName>
		{
			// Token: 0x06000C38 RID: 3128 RVA: 0x000624C9 File Offset: 0x000606C9
			internal UserInformationLockName(Guid userInformationGuid)
			{
				this.userInformationGuid = userInformationGuid;
			}

			// Token: 0x17000271 RID: 625
			// (get) Token: 0x06000C39 RID: 3129 RVA: 0x000624D8 File Offset: 0x000606D8
			public LockManager.LockLevel LockLevel
			{
				get
				{
					return LockManager.LockLevel.UserInformation;
				}
			}

			// Token: 0x17000272 RID: 626
			// (get) Token: 0x06000C3A RID: 3130 RVA: 0x000624DB File Offset: 0x000606DB
			// (set) Token: 0x06000C3B RID: 3131 RVA: 0x000624E3 File Offset: 0x000606E3
			public LockManager.NamedLockObject CachedLockObject
			{
				get
				{
					return this.cachedLockObject;
				}
				set
				{
					this.cachedLockObject = value;
				}
			}

			// Token: 0x06000C3C RID: 3132 RVA: 0x000624EC File Offset: 0x000606EC
			public virtual ILockName GetLockNameToCache()
			{
				return this;
			}

			// Token: 0x06000C3D RID: 3133 RVA: 0x000624EF File Offset: 0x000606EF
			public override bool Equals(object other)
			{
				return this.Equals(other as ILockName);
			}

			// Token: 0x06000C3E RID: 3134 RVA: 0x000624FD File Offset: 0x000606FD
			public virtual bool Equals(ILockName other)
			{
				return other != null && this.CompareTo(other) == 0;
			}

			// Token: 0x06000C3F RID: 3135 RVA: 0x00062510 File Offset: 0x00060710
			public virtual int CompareTo(ILockName other)
			{
				int num = ((int)this.LockLevel).CompareTo((int)other.LockLevel);
				if (num == 0)
				{
					UserInformation.UserInformationLockName userInformationLockName = other as UserInformation.UserInformationLockName;
					num = this.userInformationGuid.CompareTo(userInformationLockName.userInformationGuid);
				}
				return num;
			}

			// Token: 0x06000C40 RID: 3136 RVA: 0x00062554 File Offset: 0x00060754
			public override int GetHashCode()
			{
				return (int)(this.LockLevel ^ (LockManager.LockLevel)this.userInformationGuid.GetHashCode());
			}

			// Token: 0x06000C41 RID: 3137 RVA: 0x0006257C File Offset: 0x0006077C
			public override string ToString()
			{
				return "UserInformation " + this.userInformationGuid.ToString();
			}

			// Token: 0x040005B8 RID: 1464
			private readonly Guid userInformationGuid;

			// Token: 0x040005B9 RID: 1465
			private LockManager.NamedLockObject cachedLockObject;
		}

		// Token: 0x020000E4 RID: 228
		private struct UserInformationLockFrame : IDisposable
		{
			// Token: 0x06000C42 RID: 3138 RVA: 0x000625A8 File Offset: 0x000607A8
			internal UserInformationLockFrame(UserInformation.UserInformationLockName lockName, bool sharedLock, ILockStatistics lockStats)
			{
				if (!LockManager.TryGetLock(lockName, sharedLock ? LockManager.LockType.UserInformationShared : LockManager.LockType.UserInformationExclusive, UserInformation.lockTimeout, lockStats))
				{
					throw new StoreException((LID)48444U, ErrorCodeValue.UserInformationLockTimeout);
				}
				this.lockName = lockName;
				this.sharedLock = sharedLock;
			}

			// Token: 0x06000C43 RID: 3139 RVA: 0x000625F1 File Offset: 0x000607F1
			public void Dispose()
			{
				if (this.lockName != null)
				{
					LockManager.ReleaseLock(this.lockName, this.sharedLock ? LockManager.LockType.UserInformationShared : LockManager.LockType.UserInformationExclusive);
					this.lockName = null;
				}
			}

			// Token: 0x040005BA RID: 1466
			private UserInformation.UserInformationLockName lockName;

			// Token: 0x040005BB RID: 1467
			private bool sharedLock;
		}
	}
}
