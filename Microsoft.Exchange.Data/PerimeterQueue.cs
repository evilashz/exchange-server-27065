using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200017E RID: 382
	public struct PerimeterQueue
	{
		// Token: 0x06000C91 RID: 3217 RVA: 0x00026E92 File Offset: 0x00025092
		public PerimeterQueue(int forestId, string forestName, int value, string additionalInfo)
		{
			this.forestIdentity = forestId;
			this.forestName = forestName;
			this.value = value;
			this.additionalInfo = additionalInfo;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x00026EB1 File Offset: 0x000250B1
		public int ForestIdentity
		{
			get
			{
				return this.forestIdentity;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00026EB9 File Offset: 0x000250B9
		public string Name
		{
			get
			{
				if (!string.IsNullOrEmpty(this.additionalInfo))
				{
					return this.forestName + " - " + this.additionalInfo;
				}
				return this.forestName;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00026EE5 File Offset: 0x000250E5
		public int Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00026EED File Offset: 0x000250ED
		public override string ToString()
		{
			return this.Name + " : " + this.Value;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00026F0C File Offset: 0x0002510C
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x0400078B RID: 1931
		private int forestIdentity;

		// Token: 0x0400078C RID: 1932
		private string forestName;

		// Token: 0x0400078D RID: 1933
		private int value;

		// Token: 0x0400078E RID: 1934
		private string additionalInfo;
	}
}
