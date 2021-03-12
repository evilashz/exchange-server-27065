using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000103 RID: 259
	internal class OperationSideDataContext : DataContext
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x00012953 File Offset: 0x00010B53
		private OperationSideDataContext(ExceptionSide side)
		{
			this.Side = side;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00012962 File Offset: 0x00010B62
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x0001296A File Offset: 0x00010B6A
		public ExceptionSide Side { get; private set; }

		// Token: 0x06000947 RID: 2375 RVA: 0x00012974 File Offset: 0x00010B74
		public static OperationSideDataContext GetContext(ExceptionSide? side)
		{
			if (side == ExceptionSide.Source)
			{
				return OperationSideDataContext.Source;
			}
			if (side == ExceptionSide.Target)
			{
				return OperationSideDataContext.Target;
			}
			return null;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x000129BE File Offset: 0x00010BBE
		public override string ToString()
		{
			return string.Format("OperationSide: {0}", this.Side.ToString());
		}

		// Token: 0x0400056A RID: 1386
		public static readonly OperationSideDataContext Source = new OperationSideDataContext(ExceptionSide.Source);

		// Token: 0x0400056B RID: 1387
		public static readonly OperationSideDataContext Target = new OperationSideDataContext(ExceptionSide.Target);
	}
}
