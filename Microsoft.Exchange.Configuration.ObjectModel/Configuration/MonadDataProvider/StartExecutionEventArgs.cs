using System;
using System.Collections;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001DF RID: 479
	internal class StartExecutionEventArgs : RunGuidEventArgs
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x00034B52 File Offset: 0x00032D52
		public IEnumerable Pipeline
		{
			get
			{
				return this.pipeline;
			}
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00034B5A File Offset: 0x00032D5A
		public StartExecutionEventArgs(Guid guid, IEnumerable pipelineInput) : base(guid)
		{
			this.pipeline = pipelineInput;
		}

		// Token: 0x040003D2 RID: 978
		private IEnumerable pipeline;
	}
}
