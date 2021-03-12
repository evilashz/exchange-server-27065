using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Common;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200004E RID: 78
	public static class LockManager
	{
		// Token: 0x060004CE RID: 1230 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		internal static void Initialize()
		{
			LockManager.trackAllLockAcquisition = ConfigurationSchema.TrackAllLockAcquisition.Value;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000D209 File Offset: 0x0000B409
		public static TimeSpan CrashingThresholdTimeout
		{
			get
			{
				return LockManager.crashingThresholdTimeout.Value;
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000D215 File Offset: 0x0000B415
		public static void SetExternalConditionValidator(Func<LockManager.LockType, TimeSpan, bool> externalConditionValidator)
		{
			LockManager.externalConditionValidator = externalConditionValidator;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000D220 File Offset: 0x0000B420
		private static LockManager.NamedLockObjectsPartition[] CreateNamedLockObjectsPartitionArray()
		{
			LockManager.NamedLockObjectsPartition[] array = new LockManager.NamedLockObjectsPartition[40];
			for (int i = 0; i < 40; i++)
			{
				array[i] = new LockManager.NamedLockObjectsPartition();
			}
			return array;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000D24B File Offset: 0x0000B44B
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x0000D252 File Offset: 0x0000B452
		internal static TimeSpan StaleLockCleanupInterval
		{
			get
			{
				return LockManager.staleLockCleanupInterval;
			}
			set
			{
				LockManager.staleLockCleanupInterval = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000D25A File Offset: 0x0000B45A
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x0000D261 File Offset: 0x0000B461
		internal static int StaleLockCleanupSkipCount
		{
			get
			{
				return LockManager.staleLockCleanupSkipCount;
			}
			set
			{
				LockManager.staleLockCleanupSkipCount = value;
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000D269 File Offset: 0x0000B469
		public static LockManager.NamedLockFrame Lock(ILockName lockName, LockManager.LockType lockType)
		{
			return LockManager.Lock(lockName, lockType, null);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000D274 File Offset: 0x0000B474
		public static LockManager.NamedLockFrame Lock(ILockName lockName, LockManager.LockType lockType, ILockStatistics lockStats)
		{
			LockManager.NamedLockObject namedLockObject = lockName.CachedLockObject;
			if (namedLockObject == null || !namedLockObject.TryAddRef())
			{
				namedLockObject = (lockName.CachedLockObject = LockManager.GetLockObject(lockName, true));
			}
			return new LockManager.NamedLockFrame(namedLockObject, lockType, lockStats);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000D2AC File Offset: 0x0000B4AC
		[Obsolete("Locking by string name is now obsolete and should not be used.")]
		public static LockManager.ObjectLockFrame Lock(string lockName, LockManager.LockType lockType)
		{
			return default(LockManager.ObjectLockFrame);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000D2C2 File Offset: 0x0000B4C2
		public static LockManager.ObjectLockFrame Lock(object lockObject, LockManager.LockType lockType)
		{
			return new LockManager.ObjectLockFrame(lockObject, lockType, null);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		public static LockManager.ObjectLockFrame Lock(object lockObject, LockManager.LockType lockType, ILockStatistics lockStats)
		{
			return new LockManager.ObjectLockFrame(lockObject, lockType, lockStats);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000D2D6 File Offset: 0x0000B4D6
		public static LockManager.ObjectLockFrame Lock(object lockObject)
		{
			return new LockManager.ObjectLockFrame(lockObject, LockManager.LockType.LeafMonitorLock, null);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000D2E1 File Offset: 0x0000B4E1
		public static LockManager.ObjectLockFrame Lock(object lockObject, ILockStatistics lockStats)
		{
			return new LockManager.ObjectLockFrame(lockObject, LockManager.LockType.LeafMonitorLock, lockStats);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000D2EC File Offset: 0x0000B4EC
		public static void GetLock(ILockName lockName, LockManager.LockType lockType)
		{
			LockManager.GetLock(lockName, lockType, null);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
		public static void GetLock(ILockName lockName, LockManager.LockType lockType, ILockStatistics lockStats)
		{
			LockManager.NamedLockObject namedLockObject = lockName.CachedLockObject;
			if (namedLockObject == null || !namedLockObject.TryAddRef())
			{
				namedLockObject = (lockName.CachedLockObject = LockManager.GetLockObject(lockName, true));
			}
			LockManager.GetNamedLockImpl(namedLockObject, lockType, LockManager.InfiniteTimeout, lockStats);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000D334 File Offset: 0x0000B534
		public static bool HasContention(ILockName lockName)
		{
			return LockManager.simulateContention.Value || LockManager.GetWaitingCount(lockName) != 0;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000D350 File Offset: 0x0000B550
		public static int GetWaitingCount(ILockName lockName)
		{
			LockManager.NamedLockObject namedLockObject = lockName.CachedLockObject;
			int waitingCount;
			try
			{
				if (namedLockObject == null || !namedLockObject.TryAddRef())
				{
					namedLockObject = (lockName.CachedLockObject = LockManager.GetLockObject(lockName, true));
				}
				waitingCount = namedLockObject.WaitingCount;
			}
			finally
			{
				namedLockObject.ReleaseRef();
			}
			return waitingCount;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		public static IDisposable SimulateContentionForTest()
		{
			return LockManager.simulateContention.SetTestHook(true);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000D3AD File Offset: 0x0000B5AD
		public static IDisposable SetCrashingThresholdTimeoutTestHook(TimeSpan newTimeout)
		{
			return LockManager.crashingThresholdTimeout.SetTestHook(newTimeout);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000D3BA File Offset: 0x0000B5BA
		[Obsolete("Locking by string name is now obsolete and should not be used.")]
		public static void GetLock(string lockName, LockManager.LockType lockType)
		{
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000D3BC File Offset: 0x0000B5BC
		public static void GetLock(object lockObject, LockManager.LockType lockType)
		{
			LockManager.GetLock(lockObject, lockType, null);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000D3C6 File Offset: 0x0000B5C6
		public static void GetLock(object lockObject, LockManager.LockType lockType, ILockStatistics lockStats)
		{
			LockManager.GetObjectLockImpl(lockObject, lockType, LockManager.InfiniteTimeout, lockStats);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000D3D6 File Offset: 0x0000B5D6
		public static bool TryGetLock(ILockName lockName, LockManager.LockType lockType)
		{
			return LockManager.TryGetLock(lockName, lockType, TimeSpan.FromMilliseconds(0.0), null);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000D3EE File Offset: 0x0000B5EE
		public static bool TryGetLock(ILockName lockName, LockManager.LockType lockType, ILockStatistics lockStats)
		{
			return LockManager.TryGetLock(lockName, lockType, TimeSpan.FromMilliseconds(0.0), lockStats);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000D408 File Offset: 0x0000B608
		public static bool TryGetLock(ILockName lockName, LockManager.LockType lockType, TimeSpan timeout, ILockStatistics lockStats)
		{
			LockManager.NamedLockObject namedLockObject = lockName.CachedLockObject;
			if (namedLockObject == null || !namedLockObject.TryAddRef())
			{
				namedLockObject = (lockName.CachedLockObject = LockManager.GetLockObject(lockName, true));
			}
			return LockManager.GetNamedLockImpl(namedLockObject, lockType, timeout, lockStats);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000D43F File Offset: 0x0000B63F
		public static bool TryGetLock(object lockObject, LockManager.LockType lockType)
		{
			return LockManager.TryGetLock(lockObject, lockType, null);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000D449 File Offset: 0x0000B649
		public static bool TryGetLock(object lockObject, LockManager.LockType lockType, ILockStatistics lockStats)
		{
			return LockManager.TryGetLock(lockObject, lockType, TimeSpan.FromMilliseconds(0.0), lockStats);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000D461 File Offset: 0x0000B661
		public static bool TryGetLock(object lockObject, LockManager.LockType lockType, TimeSpan timeout, ILockStatistics lockStats)
		{
			return LockManager.GetObjectLockImpl(lockObject, lockType, timeout, lockStats);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000D46C File Offset: 0x0000B66C
		public static void ReleaseLock(ILockName lockName, LockManager.LockType lockType)
		{
			LockManager.NamedLockObject namedLockObject = lockName.CachedLockObject;
			if (namedLockObject == null)
			{
				namedLockObject = (lockName.CachedLockObject = LockManager.GetLockObject(lockName, false));
			}
			LockManager.ReleaseNamedLockImpl(namedLockObject, lockType);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000D499 File Offset: 0x0000B699
		[Obsolete("Locking by string name is now obsolete and should not be used.")]
		public static void ReleaseLock(string lockName, LockManager.LockType lockType)
		{
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000D49B File Offset: 0x0000B69B
		public static void ReleaseLock(object lockObject, LockManager.LockType lockType)
		{
			LockManager.ReleaseObjectLockImpl(lockObject, lockType);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000D4A4 File Offset: 0x0000B6A4
		public static void ReleaseAnyLock(LockManager.LockType lockType)
		{
			List<LockManager.LockHeldEntry> locksHeld = LockManager.LocksHeld;
			Globals.AssertRetail(locksHeld != null && locksHeld.Count > 0, "Releasing a lock the user does not hold");
			object lockObject = locksHeld[locksHeld.Count - 1].LockObject;
			LockManager.NamedLockObject namedLockObject = lockObject as LockManager.NamedLockObject;
			if (namedLockObject != null)
			{
				LockManager.ReleaseNamedLockImpl(namedLockObject, lockType);
				return;
			}
			LockManager.ReleaseObjectLockImpl(lockObject, lockType);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000D4FD File Offset: 0x0000B6FD
		public static bool TestLock(ILockName lockName, LockManager.LockType lockType)
		{
			return LockManager.TestLockImpl(lockName, lockType);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000D506 File Offset: 0x0000B706
		[Obsolete("Locking by string name is now obsolete and should not be used.")]
		public static bool TestLock(string lockName, LockManager.LockType lockType)
		{
			return false;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000D509 File Offset: 0x0000B709
		public static bool TestLock(object lockObject, LockManager.LockType lockType)
		{
			return LockManager.TestLockImpl(lockObject, lockType);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000D512 File Offset: 0x0000B712
		[Conditional("DEBUG")]
		public static void AssertLockHeld(ILockName lockName, LockManager.LockType lockType)
		{
			LockManager.TestLockImpl(lockName, lockType);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000D51C File Offset: 0x0000B71C
		[Obsolete("Locking by string name is now obsolete and should not be used.")]
		[Conditional("DEBUG")]
		public static void AssertLockHeld(string lockName, LockManager.LockType lockType)
		{
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000D51E File Offset: 0x0000B71E
		[Conditional("DEBUG")]
		public static void AssertLockHeld(object lockObject, LockManager.LockType lockType)
		{
			LockManager.TestLockImpl(lockObject, lockType);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000D528 File Offset: 0x0000B728
		[Conditional("DEBUG")]
		public static void AssertLockNotHeld(ILockName lockName, LockManager.LockType lockType)
		{
			LockManager.TestLockImpl(lockName, lockType);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000D532 File Offset: 0x0000B732
		[Conditional("DEBUG")]
		[Obsolete("Locking by string name is now obsolete and should not be used.")]
		public static void AssertLockNotHeld(string lockName, LockManager.LockType lockType)
		{
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000D534 File Offset: 0x0000B734
		[Conditional("DEBUG")]
		public static void AssertLockNotHeld(object lockObject, LockManager.LockType lockType)
		{
			LockManager.TestLockImpl(lockObject, lockType);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000D53E File Offset: 0x0000B73E
		[Conditional("DEBUG")]
		public static void AssertNoLocksHeld(LockManager.LockType lockType)
		{
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000D540 File Offset: 0x0000B740
		public static bool IsLockHeld(LockManager.LockType lockType)
		{
			List<LockManager.LockHeldEntry> locksHeld = LockManager.LocksHeld;
			if (locksHeld != null)
			{
				for (int i = 0; i < locksHeld.Count; i++)
				{
					if (locksHeld[i].LockType == lockType)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000D57C File Offset: 0x0000B77C
		public static void AssertNoLocksHeld()
		{
			List<LockManager.LockHeldEntry> locksHeld = LockManager.LocksHeld;
			Globals.AssertRetail(locksHeld == null || locksHeld.Count == 0, "Thread still holds some locks");
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		private static bool ValidateExternalCondition(LockManager.LockType lockType, TimeSpan timeout)
		{
			return LockManager.externalConditionValidator == null || LockManager.externalConditionValidator(lockType, timeout);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
		private static bool GetNamedLockImpl(LockManager.NamedLockObject lockObject, LockManager.LockType lockType, TimeSpan timeout, ILockStatistics lockStats)
		{
			LockManager.LockLevel lockLevel = LockManager.LockLevelFromLockType(lockType);
			List<LockManager.LockHeldEntry> list = LockManager.LocksHeld;
			if (list == null)
			{
				list = (LockManager.LocksHeld = new List<LockManager.LockHeldEntry>(8));
			}
			if (list.Count != 0)
			{
				LockManager.LockHeldEntry lockHeldEntry = list[list.Count - 1];
				LockManager.LockLevel lockLevel2 = LockManager.LockLevelFromLockType(lockHeldEntry.LockType);
				if (lockLevel < lockLevel2 || (lockLevel == lockLevel2 && (LockManager.LockKindFromLockType(lockType) != LockManager.LockKindFromLockType(lockHeldEntry.LockType) || !(lockHeldEntry.LockObject is LockManager.NamedLockObject) || lockObject.LockName.CompareTo(((LockManager.NamedLockObject)lockHeldEntry.LockObject).LockName) <= 0)))
				{
					Globals.AssertRetail(false, string.Format("Lock Hierarchy violation: Taking {0} violates {1}", lockType, lockHeldEntry.LockType));
				}
			}
			bool flag = false;
			if (timeout == LockManager.InfiniteTimeout || timeout >= LockManager.CrashingThresholdTimeout)
			{
				timeout = LockManager.CrashingThresholdTimeout;
				flag = true;
			}
			bool flag2 = lockObject.TryGetLock(lockType, timeout, lockStats);
			if (flag2)
			{
				list.Add(new LockManager.LockHeldEntry(lockType, lockObject));
			}
			else
			{
				try
				{
					if (flag)
					{
						throw new InvalidOperationException("Waiting time reached CrashingThresholdTimeout");
					}
				}
				finally
				{
					lockObject.ReleaseRef();
				}
			}
			return flag2;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000D6EC File Offset: 0x0000B8EC
		private static bool GetObjectLockImpl(object lockObject, LockManager.LockType lockType, TimeSpan timeout, ILockStatistics lockStats)
		{
			LockManager.LockLevel lockLevel = LockManager.LockLevelFromLockType(lockType);
			List<LockManager.LockHeldEntry> list = LockManager.LocksHeld;
			if (list == null)
			{
				list = (LockManager.LocksHeld = new List<LockManager.LockHeldEntry>(8));
			}
			if (list.Count != 0 && lockLevel <= LockManager.LockLevelFromLockType(list[list.Count - 1].LockType))
			{
				Globals.AssertRetail(false, string.Format("Lock Hierarchy violation: Taking {0} violates {1}", lockType, list[list.Count - 1].LockType));
			}
			bool flag = Monitor.TryEnter(lockObject);
			bool flag2 = false;
			TimeSpan timeSpentWaiting = TimeSpan.Zero;
			bool flag3 = false;
			if (!flag)
			{
				flag2 = true;
				if (timeout != TimeSpan.Zero)
				{
					if (timeout == LockManager.InfiniteTimeout || timeout >= LockManager.CrashingThresholdTimeout)
					{
						timeout = LockManager.CrashingThresholdTimeout;
						flag3 = true;
					}
					StopwatchStamp stamp = StopwatchStamp.GetStamp();
					flag = Monitor.TryEnter(lockObject, timeout);
					timeSpentWaiting = stamp.ElapsedTime;
				}
			}
			if ((LockManager.trackAllLockAcquisition || flag2) && lockStats != null)
			{
				lockStats.OnAfterLockAcquisition(lockType, flag, flag2, null, timeSpentWaiting);
			}
			if (flag)
			{
				list.Add(new LockManager.LockHeldEntry(lockType, lockObject));
			}
			else if (flag3)
			{
				throw new InvalidOperationException("Waiting time reached CrashingThresholdTimeout");
			}
			return flag;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000D804 File Offset: 0x0000BA04
		private static void ReleaseNamedLockImpl(LockManager.NamedLockObject lockObject, LockManager.LockType lockType)
		{
			LockManager.LockLevelFromLockType(lockType);
			List<LockManager.LockHeldEntry> locksHeld = LockManager.LocksHeld;
			if (locksHeld == null || locksHeld.Count == 0)
			{
				Globals.AssertRetail(false, string.Format("Releasing a lock {0} the user does not hold", lockType));
			}
			if (locksHeld[locksHeld.Count - 1].LockType != lockType || !object.ReferenceEquals(lockObject, locksHeld[locksHeld.Count - 1].LockObject))
			{
				Globals.AssertRetail(false, string.Format("Lock Hierarchy violation: Releasing {0} violates {1}", lockType, locksHeld[locksHeld.Count - 1].LockType));
			}
			lockObject.ReleaseLock(LockManager.LockKindFromLockType(lockType));
			lockObject.ReleaseRef();
			locksHeld.RemoveAt(locksHeld.Count - 1);
			int num = (lockObject.LockName.GetHashCode() & int.MaxValue) % 40;
			LockManager.NamedLockObjectsPartition namedLockObjectsPartition = LockManager.NamedLockObjects[num];
			if (!namedLockObjectsPartition.ShouldSkipCleanup())
			{
				LockManager.CleanupUnusedNamedLocks(namedLockObjectsPartition);
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		private static void ReleaseObjectLockImpl(object lockObject, LockManager.LockType lockType)
		{
			LockManager.LockLevelFromLockType(lockType);
			List<LockManager.LockHeldEntry> locksHeld = LockManager.LocksHeld;
			if (locksHeld == null || locksHeld.Count == 0)
			{
				Globals.AssertRetail(false, string.Format("Releasing a lock {0} the user does not hold", lockType));
			}
			if (locksHeld[locksHeld.Count - 1].LockType != lockType || !object.ReferenceEquals(locksHeld[locksHeld.Count - 1].LockObject, lockObject))
			{
				Globals.AssertRetail(false, string.Format("Lock Hierarchy violation: Releasing {0} violates {1}", lockType, locksHeld[locksHeld.Count - 1].LockType));
			}
			Monitor.Exit(lockObject);
			locksHeld.RemoveAt(locksHeld.Count - 1);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000D99C File Offset: 0x0000BB9C
		private static bool TestLockImpl(ILockName lockName, LockManager.LockType lockType)
		{
			LockManager.LockLevelFromLockType(lockType);
			List<LockManager.LockHeldEntry> locksHeld = LockManager.LocksHeld;
			if (locksHeld != null)
			{
				for (int i = 0; i < locksHeld.Count; i++)
				{
					if (locksHeld[i].LockType == lockType && lockName.Equals(((LockManager.NamedLockObject)locksHeld[i].LockObject).LockName))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		private static bool TestLockImpl(object lockObject, LockManager.LockType lockType)
		{
			LockManager.LockLevelFromLockType(lockType);
			List<LockManager.LockHeldEntry> locksHeld = LockManager.LocksHeld;
			if (locksHeld != null)
			{
				for (int i = 0; i < locksHeld.Count; i++)
				{
					if (locksHeld[i].LockType == lockType && object.ReferenceEquals(locksHeld[i].LockObject, lockObject))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000DA50 File Offset: 0x0000BC50
		private static LockManager.NamedLockObject GetLockObject(ILockName lockName, bool addref)
		{
			int num = (lockName.GetHashCode() & int.MaxValue) % 40;
			LockManager.NamedLockObjectsPartition namedLockObjectsPartition = LockManager.NamedLockObjects[num];
			LockManager.NamedLockObject namedLockObject;
			lock (namedLockObjectsPartition)
			{
				if (!namedLockObjectsPartition.LockObjectsDictionary.TryGetValue(lockName, out namedLockObject))
				{
					ILockName lockNameToCache = lockName.GetLockNameToCache();
					namedLockObject = new LockManager.NamedLockObject(lockNameToCache);
					namedLockObjectsPartition.LockObjectsDictionary.Add(lockNameToCache, namedLockObject);
				}
				if (addref)
				{
					namedLockObject.TryAddRef();
				}
			}
			return namedLockObject;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000DAD8 File Offset: 0x0000BCD8
		internal static LockManager.LockType GetLockType(LockManager.LockKind lockKind, LockManager.LockLevel lockLevel)
		{
			return (LockManager.LockType)(lockKind | (LockManager.LockKind)lockLevel);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
		internal static LockManager.LockKind LockKindFromLockType(LockManager.LockType lockType)
		{
			return (LockManager.LockKind)(lockType & (LockManager.LockType)96);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000DAF3 File Offset: 0x0000BCF3
		internal static LockManager.LockLevel LockLevelFromLockType(LockManager.LockType lockType)
		{
			return (LockManager.LockLevel)(lockType & (LockManager.LockType)31);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000DAFC File Offset: 0x0000BCFC
		private static void CleanupUnusedNamedLocks(LockManager.NamedLockObjectsPartition partition)
		{
			try
			{
				DateTime cutoffTime;
				if (Monitor.TryEnter(partition, 0) && partition.TimeToRunCleanup(out cutoffTime))
				{
					List<LockManager.NamedLockObject> list = null;
					foreach (LockManager.NamedLockObject namedLockObject in partition.LockObjectsDictionary.Values)
					{
						if (namedLockObject.CanDispose(cutoffTime))
						{
							if (list == null)
							{
								list = new List<LockManager.NamedLockObject>(10);
							}
							list.Add(namedLockObject);
						}
					}
					if (list != null)
					{
						foreach (LockManager.NamedLockObject namedLockObject2 in list)
						{
							if (namedLockObject2.TryDispose())
							{
								partition.LockObjectsDictionary.Remove(namedLockObject2.LockName);
							}
						}
					}
				}
			}
			finally
			{
				if (Monitor.IsEntered(partition))
				{
					Monitor.Exit(partition);
				}
			}
		}

		// Token: 0x040004E3 RID: 1251
		private const int EstNumberOfObjects = 4000;

		// Token: 0x040004E4 RID: 1252
		private const int NumberOfObjectHashes = 40;

		// Token: 0x040004E5 RID: 1253
		private const int EstNumberOfLocksPerThread = 8;

		// Token: 0x040004E6 RID: 1254
		internal const int LockLevelBitCount = 5;

		// Token: 0x040004E7 RID: 1255
		internal const int LockKindBitCount = 2;

		// Token: 0x040004E8 RID: 1256
		public static readonly TimeSpan InfiniteTimeout = TimeSpan.FromMilliseconds(-1.0);

		// Token: 0x040004E9 RID: 1257
		private static Hookable<TimeSpan> crashingThresholdTimeout = Hookable<TimeSpan>.Create(false, DefaultSettings.Get.LockManagerCrashingThresholdTimeout);

		// Token: 0x040004EA RID: 1258
		private static int staleLockCleanupSkipCount = 1000;

		// Token: 0x040004EB RID: 1259
		private static TimeSpan staleLockCleanupInterval = TimeSpan.FromMinutes(10.0);

		// Token: 0x040004EC RID: 1260
		private static Hookable<bool> simulateContention = Hookable<bool>.Create(false, false);

		// Token: 0x040004ED RID: 1261
		private static Func<LockManager.LockType, TimeSpan, bool> externalConditionValidator = null;

		// Token: 0x040004EE RID: 1262
		private static bool trackAllLockAcquisition;

		// Token: 0x040004EF RID: 1263
		[ThreadStatic]
		internal static List<LockManager.LockHeldEntry> LocksHeld;

		// Token: 0x040004F0 RID: 1264
		internal static LockManager.NamedLockObjectsPartition[] NamedLockObjects = LockManager.CreateNamedLockObjectsPartitionArray();

		// Token: 0x040004F1 RID: 1265
		private static long[] lockTimes = new long[128];

		// Token: 0x040004F2 RID: 1266
		private static Microsoft.Exchange.Diagnostics.Trace lockWaitTimeTracer = ExTraceGlobals.LockWaitTimeTracer;

		// Token: 0x0200004F RID: 79
		public enum LockLevel
		{
			// Token: 0x040004F4 RID: 1268
			First,
			// Token: 0x040004F5 RID: 1269
			AdminRpcInterface,
			// Token: 0x040004F6 RID: 1270
			Session,
			// Token: 0x040004F7 RID: 1271
			Database,
			// Token: 0x040004F8 RID: 1272
			Mailbox,
			// Token: 0x040004F9 RID: 1273
			UserInformation,
			// Token: 0x040004FA RID: 1274
			User,
			// Token: 0x040004FB RID: 1275
			LogicalIndexCache,
			// Token: 0x040004FC RID: 1276
			LogicalIndex,
			// Token: 0x040004FD RID: 1277
			PerUserCache,
			// Token: 0x040004FE RID: 1278
			PerUser,
			// Token: 0x040004FF RID: 1279
			ChangeNumberAndIdCounters,
			// Token: 0x04000500 RID: 1280
			MailboxComponents,
			// Token: 0x04000501 RID: 1281
			PhysicalIndexCache,
			// Token: 0x04000502 RID: 1282
			WatermarkTable,
			// Token: 0x04000503 RID: 1283
			WatermarkConsumer,
			// Token: 0x04000504 RID: 1284
			MailboxStateCache,
			// Token: 0x04000505 RID: 1285
			NotificationContext,
			// Token: 0x04000506 RID: 1286
			EventCounterBounds,
			// Token: 0x04000507 RID: 1287
			TaskList,
			// Token: 0x04000508 RID: 1288
			Task,
			// Token: 0x04000509 RID: 1289
			GlobalsTableRowUpdate,
			// Token: 0x0400050A RID: 1290
			BlockModeReplicationEmitter,
			// Token: 0x0400050B RID: 1291
			BlockModeSender,
			// Token: 0x0400050C RID: 1292
			Leaf,
			// Token: 0x0400050D RID: 1293
			Breadcrumbs,
			// Token: 0x0400050E RID: 1294
			Last = 25
		}

		// Token: 0x02000050 RID: 80
		internal enum LockKind
		{
			// Token: 0x04000510 RID: 1296
			Monitor,
			// Token: 0x04000511 RID: 1297
			Exclusive = 32,
			// Token: 0x04000512 RID: 1298
			Shared = 64,
			// Token: 0x04000513 RID: 1299
			Last
		}

		// Token: 0x02000051 RID: 81
		public enum LockType
		{
			// Token: 0x04000515 RID: 1301
			AdminRpcInterfaceExclusive = 33,
			// Token: 0x04000516 RID: 1302
			AdminRpcInterfaceShared = 65,
			// Token: 0x04000517 RID: 1303
			Session = 2,
			// Token: 0x04000518 RID: 1304
			UserInformationExclusive = 37,
			// Token: 0x04000519 RID: 1305
			UserInformationShared = 69,
			// Token: 0x0400051A RID: 1306
			MailboxExclusive = 36,
			// Token: 0x0400051B RID: 1307
			MailboxShared = 68,
			// Token: 0x0400051C RID: 1308
			UserExclusive = 38,
			// Token: 0x0400051D RID: 1309
			UserShared = 70,
			// Token: 0x0400051E RID: 1310
			PerUserCacheShared = 73,
			// Token: 0x0400051F RID: 1311
			PerUserCacheExclusive = 41,
			// Token: 0x04000520 RID: 1312
			PerUserShared = 74,
			// Token: 0x04000521 RID: 1313
			PerUserExclusive = 42,
			// Token: 0x04000522 RID: 1314
			LogicalIndexCacheShared = 71,
			// Token: 0x04000523 RID: 1315
			LogicalIndexCacheExclusive = 39,
			// Token: 0x04000524 RID: 1316
			LogicalIndexShared = 72,
			// Token: 0x04000525 RID: 1317
			LogicalIndexExclusive = 40,
			// Token: 0x04000526 RID: 1318
			ChangeNumberAndIdCountersShared = 75,
			// Token: 0x04000527 RID: 1319
			ChangeNumberAndIdCountersExclusive = 43,
			// Token: 0x04000528 RID: 1320
			MailboxComponentsShared = 76,
			// Token: 0x04000529 RID: 1321
			MailboxComponentsExclusive = 44,
			// Token: 0x0400052A RID: 1322
			DatabaseExclusive = 35,
			// Token: 0x0400052B RID: 1323
			DatabaseShared = 67,
			// Token: 0x0400052C RID: 1324
			PhysicalIndexCache = 13,
			// Token: 0x0400052D RID: 1325
			WatermarkTableExclusive = 46,
			// Token: 0x0400052E RID: 1326
			WatermarkTableShared = 78,
			// Token: 0x0400052F RID: 1327
			WatermarkConsumer = 15,
			// Token: 0x04000530 RID: 1328
			MailboxStateCache,
			// Token: 0x04000531 RID: 1329
			NotificationContext,
			// Token: 0x04000532 RID: 1330
			EventCounterBounds,
			// Token: 0x04000533 RID: 1331
			TaskList,
			// Token: 0x04000534 RID: 1332
			Task,
			// Token: 0x04000535 RID: 1333
			GlobalsTableRowUpdate,
			// Token: 0x04000536 RID: 1334
			BlockModeReplicationEmitter,
			// Token: 0x04000537 RID: 1335
			BlockModeSender,
			// Token: 0x04000538 RID: 1336
			LeafMonitorLock,
			// Token: 0x04000539 RID: 1337
			Breadcrumbs
		}

		// Token: 0x02000052 RID: 82
		internal struct LockHeldEntry
		{
			// Token: 0x06000509 RID: 1289 RVA: 0x0000DC7F File Offset: 0x0000BE7F
			public LockHeldEntry(LockManager.LockType lockType, object lockObject)
			{
				this.LockType = lockType;
				this.LockObject = lockObject;
			}

			// Token: 0x0400053A RID: 1338
			public LockManager.LockType LockType;

			// Token: 0x0400053B RID: 1339
			public object LockObject;
		}

		// Token: 0x02000053 RID: 83
		public class NamedLockObject
		{
			// Token: 0x0600050A RID: 1290 RVA: 0x0000DC8F File Offset: 0x0000BE8F
			internal NamedLockObject(ILockName lockName)
			{
				this.lockName = lockName;
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000DC9E File Offset: 0x0000BE9E
			internal ILockName LockName
			{
				get
				{
					return this.lockName;
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
			internal int RefCount
			{
				get
				{
					int num = this.refCount;
					if (num != -1)
					{
						return num & 65535;
					}
					return -1;
				}
			}

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000DCC9 File Offset: 0x0000BEC9
			internal DateTime LastUsed
			{
				get
				{
					return this.lastUsed;
				}
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x0600050E RID: 1294 RVA: 0x0000DCD1 File Offset: 0x0000BED1
			internal int WaitingCount
			{
				get
				{
					return this.waitingCount;
				}
			}

			// Token: 0x0600050F RID: 1295 RVA: 0x0000DCDC File Offset: 0x0000BEDC
			internal bool TryAddRef()
			{
				for (;;)
				{
					int num = this.RefCount;
					if (num < 0)
					{
						return false;
					}
					int num2 = num + 1;
					if (num2 < 65535)
					{
						if (num == Interlocked.CompareExchange(ref this.refCount, num2, num))
						{
							break;
						}
					}
					else
					{
						Globals.AssertRetail(false, "lock object refcount overflow");
					}
				}
				return true;
			}

			// Token: 0x06000510 RID: 1296 RVA: 0x0000DD20 File Offset: 0x0000BF20
			internal void ReleaseRef()
			{
				int num = Interlocked.Decrement(ref this.refCount);
				Globals.AssertRetail((num & -65536) == 0, "At this point we should have only ref counter bits");
				if (num <= 0)
				{
					Globals.AssertRetail(num == 0, "lock object refcounting problem - refcount goes negative");
					if (this.rwLock != null && Interlocked.CompareExchange(ref this.refCount, 16711680, 0) == 0)
					{
						ReaderWriterLockSlim readerWriterLockSlim = this.rwLock;
						this.rwLock = null;
						int num2 = Interlocked.Exchange(ref this.refCount, 0);
						Globals.AssertRetail(num2 == 16711680, "How could it be different?");
						if (readerWriterLockSlim != null && !ConcurrentLookAside<ReaderWriterLockSlim>.Pool.Put(readerWriterLockSlim))
						{
							readerWriterLockSlim.Dispose();
						}
					}
					this.lastUsed = DateTime.UtcNow;
				}
			}

			// Token: 0x06000511 RID: 1297 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
			internal bool CanDispose(DateTime cutoffTime)
			{
				return this.RefCount == 0 && this.lastUsed <= cutoffTime;
			}

			// Token: 0x06000512 RID: 1298 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
			internal bool TryDispose()
			{
				if (Interlocked.CompareExchange(ref this.refCount, -1, 0) == 0)
				{
					if (this.rwLock != null)
					{
						if (!ConcurrentLookAside<ReaderWriterLockSlim>.Pool.Put(this.rwLock))
						{
							this.rwLock.Dispose();
						}
						this.rwLock = null;
					}
					return true;
				}
				return false;
			}

			// Token: 0x06000513 RID: 1299 RVA: 0x0000DE20 File Offset: 0x0000C020
			internal bool TryGetLock(LockManager.LockType lockType, TimeSpan timeout, ILockStatistics lockStats)
			{
				ILockStatistics lockStatistics = this.mostRecentOwner;
				LockManager.LockKind lockKind = LockManager.LockKindFromLockType(lockType);
				TimeSpan timeSpentWaiting = TimeSpan.Zero;
				bool flag = false;
				bool flag2 = false;
				LockManager.LockKind lockKind2 = lockKind;
				if (lockKind2 != LockManager.LockKind.Monitor)
				{
					if (lockKind2 != LockManager.LockKind.Exclusive)
					{
						if (lockKind2 == LockManager.LockKind.Shared)
						{
							flag = this.GetReaderWriterLock().TryEnterReadLock(0);
							if (!flag)
							{
								flag2 = true;
								if (timeout != TimeSpan.Zero)
								{
									StopwatchStamp stamp = StopwatchStamp.GetStamp();
									Interlocked.Increment(ref this.waitingCount);
									try
									{
										if (lockStatistics == null)
										{
											lockStatistics = this.mostRecentOwner;
										}
										flag = this.GetReaderWriterLock().TryEnterReadLock(timeout);
									}
									finally
									{
										Interlocked.Decrement(ref this.waitingCount);
										timeSpentWaiting = stamp.ElapsedTime;
									}
								}
							}
							if (flag)
							{
								this.mostRecentOwner = lockStats;
							}
						}
					}
					else
					{
						flag = this.GetReaderWriterLock().TryEnterWriteLock(0);
						if (!flag)
						{
							flag2 = true;
							if (timeout != TimeSpan.Zero)
							{
								StopwatchStamp stamp2 = StopwatchStamp.GetStamp();
								Interlocked.Increment(ref this.waitingCount);
								try
								{
									if (lockStatistics == null)
									{
										lockStatistics = this.mostRecentOwner;
									}
									flag = this.GetReaderWriterLock().TryEnterWriteLock(timeout);
								}
								finally
								{
									Interlocked.Decrement(ref this.waitingCount);
									timeSpentWaiting = stamp2.ElapsedTime;
								}
							}
						}
						if (flag)
						{
							this.mostRecentOwner = lockStats;
						}
					}
				}
				else
				{
					flag = Monitor.TryEnter(this);
					if (!flag)
					{
						flag2 = true;
						if (timeout != TimeSpan.Zero)
						{
							StopwatchStamp stamp3 = StopwatchStamp.GetStamp();
							Interlocked.Increment(ref this.waitingCount);
							try
							{
								if (lockStatistics == null)
								{
									lockStatistics = this.mostRecentOwner;
								}
								flag = Monitor.TryEnter(this, timeout);
							}
							finally
							{
								Interlocked.Decrement(ref this.waitingCount);
								timeSpentWaiting = stamp3.ElapsedTime;
							}
						}
					}
					if (flag)
					{
						this.mostRecentOwner = lockStats;
					}
				}
				if ((LockManager.trackAllLockAcquisition || flag2) && lockStats != null)
				{
					lockStats.OnAfterLockAcquisition(lockType, flag, flag2, lockStatistics, timeSpentWaiting);
				}
				if (flag)
				{
					this.lastOwnerThreadId = Environment.CurrentManagedThreadId;
				}
				return flag;
			}

			// Token: 0x06000514 RID: 1300 RVA: 0x0000DFF4 File Offset: 0x0000C1F4
			internal void ReleaseLock(LockManager.LockKind lockKind)
			{
				bool flag = true;
				if (lockKind != LockManager.LockKind.Monitor)
				{
					if (lockKind != LockManager.LockKind.Exclusive)
					{
						if (lockKind == LockManager.LockKind.Shared)
						{
							this.rwLock.ExitReadLock();
							flag = (this.rwLock.CurrentReadCount == 0);
						}
					}
					else
					{
						this.rwLock.ExitWriteLock();
					}
				}
				else
				{
					Monitor.Exit(this);
				}
				if (flag)
				{
					this.mostRecentOwner = null;
				}
			}

			// Token: 0x06000515 RID: 1301 RVA: 0x0000E04E File Offset: 0x0000C24E
			public override string ToString()
			{
				return this.lockName.ToString();
			}

			// Token: 0x06000516 RID: 1302 RVA: 0x0000E05C File Offset: 0x0000C25C
			private ReaderWriterLockSlim GetReaderWriterLock()
			{
				if (this.rwLock == null)
				{
					ReaderWriterLockSlim readerWriterLockSlim = ConcurrentLookAside<ReaderWriterLockSlim>.Pool.Get();
					if (readerWriterLockSlim == null)
					{
						readerWriterLockSlim = new ReaderWriterLockSlim();
					}
					if (Interlocked.CompareExchange<ReaderWriterLockSlim>(ref this.rwLock, readerWriterLockSlim, null) != null && !ConcurrentLookAside<ReaderWriterLockSlim>.Pool.Put(readerWriterLockSlim))
					{
						readerWriterLockSlim.Dispose();
					}
				}
				return this.rwLock;
			}

			// Token: 0x0400053C RID: 1340
			private const int RefCounterMask = 65535;

			// Token: 0x0400053D RID: 1341
			private const int LockReleaserFlag = 16711680;

			// Token: 0x0400053E RID: 1342
			private readonly ILockName lockName;

			// Token: 0x0400053F RID: 1343
			private ReaderWriterLockSlim rwLock;

			// Token: 0x04000540 RID: 1344
			private int refCount;

			// Token: 0x04000541 RID: 1345
			private int lastOwnerThreadId;

			// Token: 0x04000542 RID: 1346
			private DateTime lastUsed;

			// Token: 0x04000543 RID: 1347
			private int waitingCount;

			// Token: 0x04000544 RID: 1348
			private ILockStatistics mostRecentOwner;
		}

		// Token: 0x02000054 RID: 84
		public struct ObjectLockFrame : IDisposable
		{
			// Token: 0x06000517 RID: 1303 RVA: 0x0000E0AD File Offset: 0x0000C2AD
			internal ObjectLockFrame(object lockObject, LockManager.LockType lockType, ILockStatistics lockStats)
			{
				LockManager.GetObjectLockImpl(lockObject, lockType, LockManager.InfiniteTimeout, lockStats);
				this.lockObject = lockObject;
				this.lockType = lockType;
			}

			// Token: 0x06000518 RID: 1304 RVA: 0x0000E0CB File Offset: 0x0000C2CB
			public void Dispose()
			{
				if (this.lockObject != null)
				{
					LockManager.ReleaseObjectLockImpl(this.lockObject, this.lockType);
				}
			}

			// Token: 0x04000545 RID: 1349
			private object lockObject;

			// Token: 0x04000546 RID: 1350
			private LockManager.LockType lockType;
		}

		// Token: 0x02000055 RID: 85
		public struct NamedLockFrame : IDisposable
		{
			// Token: 0x06000519 RID: 1305 RVA: 0x0000E0E6 File Offset: 0x0000C2E6
			internal NamedLockFrame(LockManager.NamedLockObject lockObject, LockManager.LockType lockType, ILockStatistics lockStats)
			{
				LockManager.GetNamedLockImpl(lockObject, lockType, LockManager.InfiniteTimeout, lockStats);
				this.lockObject = lockObject;
				this.lockType = lockType;
			}

			// Token: 0x0600051A RID: 1306 RVA: 0x0000E104 File Offset: 0x0000C304
			public void Dispose()
			{
				if (this.lockObject != null)
				{
					LockManager.ReleaseNamedLockImpl(this.lockObject, this.lockType);
				}
			}

			// Token: 0x04000547 RID: 1351
			private LockManager.NamedLockObject lockObject;

			// Token: 0x04000548 RID: 1352
			private LockManager.LockType lockType;
		}

		// Token: 0x02000056 RID: 86
		internal class NamedLockObjectsPartition
		{
			// Token: 0x0600051B RID: 1307 RVA: 0x0000E11F File Offset: 0x0000C31F
			public NamedLockObjectsPartition()
			{
				this.cleanupSkipCounter = 0;
				this.lastCleanupTime = DateTime.UtcNow;
				this.lockObjectsDictionary = new Dictionary<ILockName, LockManager.NamedLockObject>(100);
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000E146 File Offset: 0x0000C346
			public Dictionary<ILockName, LockManager.NamedLockObject> LockObjectsDictionary
			{
				get
				{
					return this.lockObjectsDictionary;
				}
			}

			// Token: 0x0600051D RID: 1309 RVA: 0x0000E150 File Offset: 0x0000C350
			public bool ShouldSkipCleanup()
			{
				return 0 != ++this.cleanupSkipCounter % LockManager.StaleLockCleanupSkipCount;
			}

			// Token: 0x0600051E RID: 1310 RVA: 0x0000E17C File Offset: 0x0000C37C
			public bool TimeToRunCleanup(out DateTime freeLocksUsedBeforeTime)
			{
				DateTime utcNow = DateTime.UtcNow;
				freeLocksUsedBeforeTime = utcNow.Add(-LockManager.StaleLockCleanupInterval);
				if (this.lastCleanupTime <= freeLocksUsedBeforeTime)
				{
					this.lastCleanupTime = utcNow;
					return true;
				}
				return false;
			}

			// Token: 0x04000549 RID: 1353
			private int cleanupSkipCounter;

			// Token: 0x0400054A RID: 1354
			private DateTime lastCleanupTime;

			// Token: 0x0400054B RID: 1355
			private Dictionary<ILockName, LockManager.NamedLockObject> lockObjectsDictionary;
		}
	}
}
