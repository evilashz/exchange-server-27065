using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x02000057 RID: 87
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WorkTypeDefinition : IComparable<WorkTypeDefinition>
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0001A1B8 File Offset: 0x000183B8
		internal WorkTypeDefinition(WorkType workType, TimeSpan timeTillSyncDue, byte weight, bool isSyncNow)
		{
			if (weight <= 0)
			{
				throw new ArgumentOutOfRangeException("weight");
			}
			this.workType = workType;
			this.timeTillSyncDue = timeTillSyncDue;
			this.weight = weight;
			this.isSyncNow = isSyncNow;
			this.isLightLoad = WorkTypeManager.IsLightWeightWorkType(workType);
			this.isOneOff = WorkTypeManager.IsOneOffWorkType(workType);
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0001A20F File Offset: 0x0001840F
		internal WorkType WorkType
		{
			get
			{
				return this.workType;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0001A217 File Offset: 0x00018417
		internal TimeSpan TimeTillSyncDue
		{
			get
			{
				return this.timeTillSyncDue;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0001A21F File Offset: 0x0001841F
		internal byte Weight
		{
			get
			{
				return this.weight;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0001A227 File Offset: 0x00018427
		internal bool IsSyncNow
		{
			get
			{
				return this.isSyncNow;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0001A22F File Offset: 0x0001842F
		internal bool IsLightLoad
		{
			get
			{
				return this.isLightLoad;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001A237 File Offset: 0x00018437
		internal bool IsOneOff
		{
			get
			{
				return this.isOneOff;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001A23F File Offset: 0x0001843F
		public int CompareTo(WorkTypeDefinition workTypeDefinition)
		{
			if (this.workType == workTypeDefinition.WorkType)
			{
				return 0;
			}
			if (this.Weight <= workTypeDefinition.Weight)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001A264 File Offset: 0x00018464
		internal void AddDiagnosticInfoTo(XElement parentElement)
		{
			parentElement.Add(new XElement("workType", this.workType.ToString()));
			parentElement.Add(new XElement("timeTillSyncDue", this.timeTillSyncDue.ToString()));
			parentElement.Add(new XElement("weight", this.weight.ToString()));
		}

		// Token: 0x0400024F RID: 591
		private readonly WorkType workType;

		// Token: 0x04000250 RID: 592
		private readonly TimeSpan timeTillSyncDue;

		// Token: 0x04000251 RID: 593
		private readonly byte weight;

		// Token: 0x04000252 RID: 594
		private readonly bool isSyncNow;

		// Token: 0x04000253 RID: 595
		private readonly bool isLightLoad;

		// Token: 0x04000254 RID: 596
		private readonly bool isOneOff;
	}
}
