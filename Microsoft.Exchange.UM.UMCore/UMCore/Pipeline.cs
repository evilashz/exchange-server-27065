using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002AF RID: 687
	internal abstract class Pipeline
	{
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x0005988D File Offset: 0x00057A8D
		internal int Count
		{
			get
			{
				return this.MyStages.Length;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060014CA RID: 5322
		protected abstract IPipelineStageFactory[] MyStages { get; }

		// Token: 0x17000525 RID: 1317
		internal IPipelineStageFactory this[int index]
		{
			get
			{
				return this.MyStages[index];
			}
		}
	}
}
