using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200002B RID: 43
	internal class ClassificationDictionary<TValue> : Dictionary<WorkloadClassification, TValue>
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public ClassificationDictionary() : this(null)
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public ClassificationDictionary(Func<WorkloadClassification, TValue> initialize)
		{
			foreach (WorkloadClassification workloadClassification in this.Classifications)
			{
				base[workloadClassification] = ((initialize != null) ? initialize(workloadClassification) : default(TValue));
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00006E4C File Offset: 0x0000504C
		public IEnumerable<WorkloadClassification> Classifications
		{
			get
			{
				return ClassificationDictionary<TValue>.classifications;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006E5C File Offset: 0x0000505C
		private static WorkloadClassification[] GetClassifications()
		{
			return (from WorkloadClassification classification in Enum.GetValues(typeof(WorkloadClassification))
			where classification != WorkloadClassification.Unknown
			select classification).ToArray<WorkloadClassification>();
		}

		// Token: 0x040000CD RID: 205
		private static WorkloadClassification[] classifications = ClassificationDictionary<TValue>.GetClassifications();
	}
}
