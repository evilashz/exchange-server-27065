using System;

namespace Microsoft.Exchange.Configuration.FailFast
{
	// Token: 0x02000009 RID: 9
	internal class FailFastUserCacheValue
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002C0E File Offset: 0x00000E0E
		internal FailFastUserCacheValue(BlockedType blockedType, TimeSpan blockedTime)
		{
			this.BlockedType = blockedType;
			this.AddedTime = DateTime.UtcNow;
			this.BlockedTime = blockedTime;
			this.HitCount = 1;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002C36 File Offset: 0x00000E36
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002C3E File Offset: 0x00000E3E
		internal BlockedType BlockedType { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002C47 File Offset: 0x00000E47
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002C4F File Offset: 0x00000E4F
		internal DateTime AddedTime { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002C58 File Offset: 0x00000E58
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002C60 File Offset: 0x00000E60
		internal TimeSpan BlockedTime { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002C69 File Offset: 0x00000E69
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002C71 File Offset: 0x00000E71
		internal int HitCount { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002C7A File Offset: 0x00000E7A
		internal bool IsValid
		{
			get
			{
				return this.AddedTime + this.BlockedTime >= DateTime.UtcNow;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002C98 File Offset: 0x00000E98
		public override string ToString()
		{
			return string.Format("AddedTime: {0}; BlockedType: {1}; BlockedTime: {2}; HitCount: {3}.", new object[]
			{
				this.AddedTime,
				this.BlockedType,
				this.BlockedTime,
				this.HitCount
			});
		}

		// Token: 0x0400001B RID: 27
		private const string StringFormat = "AddedTime: {0}; BlockedType: {1}; BlockedTime: {2}; HitCount: {3}.";
	}
}
