using System;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x02000200 RID: 512
	internal struct OperationKey : IEquatable<OperationKey>
	{
		// Token: 0x06000F0C RID: 3852 RVA: 0x0003D46C File Offset: 0x0003B66C
		public OperationKey(ActivityOperationType activityOperationType, string instance)
		{
			this.ActivityOperationType = activityOperationType;
			this.Instance = instance;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0003D47C File Offset: 0x0003B67C
		public override int GetHashCode()
		{
			return ((this.Instance != null) ? this.Instance.GetHashCode() : 0) ^ (int)this.ActivityOperationType;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0003D49B File Offset: 0x0003B69B
		public override bool Equals(object obj)
		{
			return obj is OperationKey && this.Equals((OperationKey)obj);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0003D4B3 File Offset: 0x0003B6B3
		public bool Equals(OperationKey other)
		{
			return this.ActivityOperationType == other.ActivityOperationType && 0 == string.Compare(this.Instance, other.Instance, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000AAF RID: 2735
		public readonly ActivityOperationType ActivityOperationType;

		// Token: 0x04000AB0 RID: 2736
		public readonly string Instance;
	}
}
