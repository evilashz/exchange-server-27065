using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x02000037 RID: 55
	internal abstract class EdgeSyncValidator
	{
		// Token: 0x06000143 RID: 323 RVA: 0x0000780C File Offset: 0x00005A0C
		public EdgeSyncValidator(ReplicationTopology topology)
		{
			this.topology = topology;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000781B File Offset: 0x00005A1B
		public ReplicationTopology Topology
		{
			get
			{
				return this.topology;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00007823 File Offset: 0x00005A23
		// (set) Token: 0x06000146 RID: 326 RVA: 0x0000782B File Offset: 0x00005A2B
		public Unlimited<uint> MaxReportedLength
		{
			get
			{
				return this.maxReportedLength;
			}
			set
			{
				this.maxReportedLength = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00007834 File Offset: 0x00005A34
		// (set) Token: 0x06000148 RID: 328 RVA: 0x0000783C File Offset: 0x00005A3C
		public EdgeSyncValidator.ProgressUpdate ProgressMethod
		{
			get
			{
				return this.progressMethod;
			}
			set
			{
				this.progressMethod = value;
			}
		}

		// Token: 0x06000149 RID: 329
		public abstract EdgeConfigStatus Validate(EdgeConnectionInfo subscription);

		// Token: 0x0600014A RID: 330 RVA: 0x00007845 File Offset: 0x00005A45
		public virtual void LoadValidationInfo()
		{
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007847 File Offset: 0x00005A47
		public virtual void UnloadValidationInfo()
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000784C File Offset: 0x00005A4C
		public bool CompareBytes(byte[] array1, byte[] array2)
		{
			if (array1 == array2)
			{
				return true;
			}
			if (array1 == null || array2 == null)
			{
				return false;
			}
			int num = Math.Min(array1.Length, array2.Length);
			for (int i = 0; i < num; i++)
			{
				if (!array1[i].Equals(array2[i]))
				{
					return false;
				}
			}
			return array1.Length == array2.Length;
		}

		// Token: 0x040000F2 RID: 242
		private ReplicationTopology topology;

		// Token: 0x040000F3 RID: 243
		private Unlimited<uint> maxReportedLength;

		// Token: 0x040000F4 RID: 244
		private EdgeSyncValidator.ProgressUpdate progressMethod;

		// Token: 0x02000038 RID: 56
		// (Invoke) Token: 0x0600014E RID: 334
		public delegate void ProgressUpdate(LocalizedString title, LocalizedString updateDescription);
	}
}
