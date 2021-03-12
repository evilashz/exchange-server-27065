using System;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000224 RID: 548
	internal class Referenced<T> : IDisposable where T : IDisposable
	{
		// Token: 0x06000F16 RID: 3862 RVA: 0x00043D4F File Offset: 0x00041F4F
		private Referenced(T value)
		{
			this.value = value;
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00043D5E File Offset: 0x00041F5E
		// (set) Token: 0x06000F18 RID: 3864 RVA: 0x00043D66 File Offset: 0x00041F66
		internal int Reference
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00043D6F File Offset: 0x00041F6F
		internal T Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00043D77 File Offset: 0x00041F77
		public static implicit operator T(Referenced<T> refObj)
		{
			return refObj.Value;
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00043D80 File Offset: 0x00041F80
		public void Dispose()
		{
			lock (this)
			{
				this.Release();
			}
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00043DBC File Offset: 0x00041FBC
		internal static Referenced<T> Acquire(T value)
		{
			Referenced<T> referenced = new Referenced<T>(value);
			referenced.AddRef();
			return referenced;
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00043DD8 File Offset: 0x00041FD8
		internal Referenced<T> Reacquire()
		{
			lock (this)
			{
				if (this.Reference == 0)
				{
					return null;
				}
				this.AddRef();
			}
			return this;
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00043E24 File Offset: 0x00042024
		private int AddRef()
		{
			return ++this.reference;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00043E44 File Offset: 0x00042044
		private int Release()
		{
			if (--this.reference == 0)
			{
				T t = this.Value;
				t.Dispose();
			}
			return this.reference;
		}

		// Token: 0x04000A62 RID: 2658
		private T value;

		// Token: 0x04000A63 RID: 2659
		private int reference;
	}
}
