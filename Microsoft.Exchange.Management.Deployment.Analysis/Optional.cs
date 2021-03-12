using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000039 RID: 57
	public struct Optional<T>
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x00007A72 File Offset: 0x00005C72
		public Optional(T value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007A82 File Offset: 0x00005C82
		public T Value
		{
			get
			{
				if (this.hasValue)
				{
					return this.value;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00007A98 File Offset: 0x00005C98
		public bool HasValue
		{
			get
			{
				return this.hasValue;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007AA0 File Offset: 0x00005CA0
		public static implicit operator Optional<T>(T value)
		{
			return new Optional<T>(value);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007AA8 File Offset: 0x00005CA8
		public T DefaultTo(T defaultValue)
		{
			if (!this.hasValue)
			{
				return defaultValue;
			}
			return this.value;
		}

		// Token: 0x04000089 RID: 137
		private readonly T value;

		// Token: 0x0400008A RID: 138
		private readonly bool hasValue;
	}
}
