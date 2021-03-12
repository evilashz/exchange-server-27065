using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000159 RID: 345
	internal sealed class HeavyBlockingOperationEventArgs : EventArgs
	{
		// Token: 0x06000A38 RID: 2616 RVA: 0x0002BA4F File Offset: 0x00029C4F
		internal HeavyBlockingOperationEventArgs(IUMHeavyBlockingOperation operation)
		{
			this.operation = operation;
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002BA5E File Offset: 0x00029C5E
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x0002BA66 File Offset: 0x00029C66
		internal TimeSpan Latency
		{
			get
			{
				return this.latency;
			}
			set
			{
				this.latency = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0002BA6F File Offset: 0x00029C6F
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x0002BA77 File Offset: 0x00029C77
		internal Exception Error
		{
			get
			{
				return this.error;
			}
			set
			{
				this.error = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0002BA80 File Offset: 0x00029C80
		internal IUMHeavyBlockingOperation Operation
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0002BA88 File Offset: 0x00029C88
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x0002BA90 File Offset: 0x00029C90
		internal HeavyBlockingOperationCompletionType CompletionType
		{
			get
			{
				return this.completionType;
			}
			set
			{
				this.completionType = value;
			}
		}

		// Token: 0x04000944 RID: 2372
		private HeavyBlockingOperationCompletionType completionType;

		// Token: 0x04000945 RID: 2373
		private IUMHeavyBlockingOperation operation;

		// Token: 0x04000946 RID: 2374
		private Exception error;

		// Token: 0x04000947 RID: 2375
		private TimeSpan latency;
	}
}
