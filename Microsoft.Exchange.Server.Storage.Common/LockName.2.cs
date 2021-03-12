using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200005A RID: 90
	public class LockName<T1, T2> : ILockName, IEquatable<ILockName>, IComparable<ILockName> where T1 : IComparable<T1>, IEquatable<T1> where T2 : IComparable<T2>, IEquatable<T2>
	{
		// Token: 0x06000530 RID: 1328 RVA: 0x0000E2DF File Offset: 0x0000C4DF
		public LockName(T1 nameValue1, T2 nameValue2, LockManager.LockLevel lockLevel)
		{
			this.nameValue1 = nameValue1;
			this.nameValue2 = nameValue2;
			this.lockLevel = lockLevel;
			this.hashCode = (int)(lockLevel ^ (LockManager.LockLevel)nameValue1.GetHashCode() ^ (LockManager.LockLevel)nameValue2.GetHashCode());
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000E31F File Offset: 0x0000C51F
		public LockManager.LockLevel LockLevel
		{
			get
			{
				return this.lockLevel;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000E327 File Offset: 0x0000C527
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0000E32F File Offset: 0x0000C52F
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

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0000E338 File Offset: 0x0000C538
		public T1 NameValue1
		{
			get
			{
				return this.nameValue1;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000E340 File Offset: 0x0000C540
		public T2 NameValue2
		{
			get
			{
				return this.nameValue2;
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000E348 File Offset: 0x0000C548
		public ILockName GetLockNameToCache()
		{
			return this;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000E34C File Offset: 0x0000C54C
		public bool Equals(ILockName other)
		{
			if (this.lockLevel != other.LockLevel)
			{
				return false;
			}
			LockName<T1, T2> lockName = other as LockName<T1, T2>;
			T1 t = this.nameValue1;
			if (t.Equals(lockName.NameValue1))
			{
				T2 t2 = this.nameValue2;
				return t2.Equals(lockName.NameValue2);
			}
			return false;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000E3A8 File Offset: 0x0000C5A8
		public int CompareTo(ILockName other)
		{
			int num = ((int)this.lockLevel).CompareTo((int)other.LockLevel);
			if (num == 0)
			{
				LockName<T1, T2> lockName = other as LockName<T1, T2>;
				T1 t = this.nameValue1;
				num = t.CompareTo(lockName.NameValue1);
				if (num == 0)
				{
					T2 t2 = this.nameValue2;
					num = t2.CompareTo(lockName.NameValue2);
				}
			}
			return num;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000E40F File Offset: 0x0000C60F
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000E418 File Offset: 0x0000C618
		public override string ToString()
		{
			string[] array = new string[6];
			array[0] = this.lockLevel.ToString();
			array[1] = ":[";
			string[] array2 = array;
			int num = 2;
			T1 t = this.nameValue1;
			array2[num] = t.ToString();
			array[3] = ",";
			string[] array3 = array;
			int num2 = 4;
			T2 t2 = this.nameValue2;
			array3[num2] = t2.ToString();
			array[5] = "]";
			return string.Concat(array);
		}

		// Token: 0x04000550 RID: 1360
		private readonly int hashCode;

		// Token: 0x04000551 RID: 1361
		private readonly LockManager.LockLevel lockLevel;

		// Token: 0x04000552 RID: 1362
		private readonly T1 nameValue1;

		// Token: 0x04000553 RID: 1363
		private readonly T2 nameValue2;

		// Token: 0x04000554 RID: 1364
		private LockManager.NamedLockObject cachedLockObject;
	}
}
