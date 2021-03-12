using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000059 RID: 89
	public class LockName<T> : ILockName, IEquatable<ILockName>, IComparable<ILockName> where T : IComparable<T>, IEquatable<T>
	{
		// Token: 0x06000526 RID: 1318 RVA: 0x0000E1C3 File Offset: 0x0000C3C3
		public LockName(T nameValue, LockManager.LockLevel lockLevel)
		{
			this.nameValue = nameValue;
			this.lockLevel = lockLevel;
			this.hashCode = (int)(lockLevel ^ (LockManager.LockLevel)nameValue.GetHashCode());
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000E1EE File Offset: 0x0000C3EE
		public LockManager.LockLevel LockLevel
		{
			get
			{
				return this.lockLevel;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0000E1F6 File Offset: 0x0000C3F6
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0000E1FE File Offset: 0x0000C3FE
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

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0000E207 File Offset: 0x0000C407
		public T NameValue
		{
			get
			{
				return this.nameValue;
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000E20F File Offset: 0x0000C40F
		public ILockName GetLockNameToCache()
		{
			return this;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000E214 File Offset: 0x0000C414
		public bool Equals(ILockName other)
		{
			if (this.lockLevel != other.LockLevel)
			{
				return false;
			}
			LockName<T> lockName = other as LockName<T>;
			T t = this.nameValue;
			return t.Equals(lockName.NameValue);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000E254 File Offset: 0x0000C454
		public int CompareTo(ILockName other)
		{
			int num = ((int)this.lockLevel).CompareTo((int)other.LockLevel);
			if (num == 0)
			{
				LockName<T> lockName = other as LockName<T>;
				T t = this.nameValue;
				num = t.CompareTo(lockName.NameValue);
			}
			return num;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000E29C File Offset: 0x0000C49C
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000E2A4 File Offset: 0x0000C4A4
		public override string ToString()
		{
			string str = this.lockLevel.ToString();
			string str2 = " ";
			T t = this.nameValue;
			return str + str2 + t.ToString();
		}

		// Token: 0x0400054C RID: 1356
		private readonly int hashCode;

		// Token: 0x0400054D RID: 1357
		private readonly LockManager.LockLevel lockLevel;

		// Token: 0x0400054E RID: 1358
		private readonly T nameValue;

		// Token: 0x0400054F RID: 1359
		private LockManager.NamedLockObject cachedLockObject;
	}
}
