using System;
using System.Threading;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x02000883 RID: 2179
	internal sealed class CancelableMservAsyncResult : ICancelableAsyncResult, IAsyncResult
	{
		// Token: 0x06002E63 RID: 11875 RVA: 0x0006666C File Offset: 0x0006486C
		public CancelableMservAsyncResult(ICancelableAsyncResult internalResult, OutstandingAsyncReadConfig readConfig, object asyncStateOverride)
		{
			if (internalResult == null)
			{
				throw new ArgumentNullException("internalResult");
			}
			if (readConfig == null)
			{
				throw new ArgumentNullException("readConfig");
			}
			if (asyncStateOverride == null)
			{
				throw new ArgumentNullException("asyncStateOverride");
			}
			this.internalResult = internalResult;
			this.readConfig = readConfig;
			this.asyncStateOverride = asyncStateOverride;
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06002E64 RID: 11876 RVA: 0x000666BE File Offset: 0x000648BE
		public ICancelableAsyncResult InternalResult
		{
			get
			{
				return this.internalResult;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x000666C6 File Offset: 0x000648C6
		public OutstandingAsyncReadConfig ReadConfig
		{
			get
			{
				return this.readConfig;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06002E66 RID: 11878 RVA: 0x000666CE File Offset: 0x000648CE
		public object AsyncState
		{
			get
			{
				return this.asyncStateOverride;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x000666D6 File Offset: 0x000648D6
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.internalResult.AsyncWaitHandle;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06002E68 RID: 11880 RVA: 0x000666E3 File Offset: 0x000648E3
		public bool IsCompleted
		{
			get
			{
				return this.internalResult.IsCompleted;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x000666F0 File Offset: 0x000648F0
		public bool IsCanceled
		{
			get
			{
				return this.internalResult.IsCanceled;
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06002E6A RID: 11882 RVA: 0x000666FD File Offset: 0x000648FD
		public bool CompletedSynchronously
		{
			get
			{
				return this.internalResult.CompletedSynchronously;
			}
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x0006670A File Offset: 0x0006490A
		public void Cancel()
		{
			this.internalResult.Cancel();
		}

		// Token: 0x04002868 RID: 10344
		private readonly ICancelableAsyncResult internalResult;

		// Token: 0x04002869 RID: 10345
		private readonly OutstandingAsyncReadConfig readConfig;

		// Token: 0x0400286A RID: 10346
		private readonly object asyncStateOverride;
	}
}
