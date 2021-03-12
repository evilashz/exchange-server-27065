using System;
using System.IO;
using System.Threading;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x02000803 RID: 2051
	internal class ComWorkerConfiguration
	{
		// Token: 0x06002B0F RID: 11023 RVA: 0x0005E35C File Offset: 0x0005C55C
		public ComWorkerConfiguration(string processPath, string cmdlineParams, Guid workerGuid, ComWorkerConfiguration.RunAsFlag flags, Mutex processLaunchMutex, int memoryLimit, int lifeTimeLimit, int workerAllocationTimeout, int maxTransactions, int transactionTimeout, int idleTimeout)
		{
			if (!File.Exists(processPath))
			{
				throw new ArgumentException("Can not find worker process path", "processPath");
			}
			if (processLaunchMutex != null && processLaunchMutex.SafeWaitHandle.IsInvalid)
			{
				throw new ArgumentException("Invalid Mutex for COM object activation", "processLaunchMutex");
			}
			if (memoryLimit < 0)
			{
				throw new ArgumentException("Invalid max worker process memory limit", "memoryLimit");
			}
			if (lifeTimeLimit < 0)
			{
				throw new ArgumentException("Invalid max worker process memory limit", "memoryLimit");
			}
			if (workerAllocationTimeout <= 0)
			{
				throw new ArgumentException("Invalid request allocation timeout", "workerAllocationTimeout");
			}
			if (maxTransactions <= 0)
			{
				throw new ArgumentException("Invalid max number of transactions per process", "maxTransactions");
			}
			if (transactionTimeout <= 0)
			{
				throw new ArgumentException("Invalid transaction timeout", "transactionTimeout");
			}
			if (idleTimeout < 0)
			{
				throw new ArgumentException("Invalid value for idle timeout", "idleTimeout");
			}
			this.workerMemoryLimit = memoryLimit;
			this.workerAllocationTimeout = workerAllocationTimeout;
			this.workerLifetimeLimit = lifeTimeLimit;
			this.maxTransactionsPerProcess = maxTransactions;
			this.transactionTimeout = transactionTimeout;
			this.workerProcessPath = processPath;
			this.cmdlineParams = cmdlineParams;
			this.flags = flags;
			this.processLaunchMutex = processLaunchMutex;
			this.workerGuid = workerGuid;
			this.workerIdleTimeout = idleTimeout;
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x0005E47C File Offset: 0x0005C67C
		public int WorkerMemoryLimit
		{
			get
			{
				return this.workerMemoryLimit;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06002B11 RID: 11025 RVA: 0x0005E484 File Offset: 0x0005C684
		public int WorkerAllocationTimeout
		{
			get
			{
				return this.workerAllocationTimeout;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x0005E48C File Offset: 0x0005C68C
		public int WorkerLifetimeLimit
		{
			get
			{
				return this.workerLifetimeLimit;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x0005E494 File Offset: 0x0005C694
		public int WorkerIdleTimeout
		{
			get
			{
				return this.workerIdleTimeout;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x0005E49C File Offset: 0x0005C69C
		public int MaxTransactionsPerProcess
		{
			get
			{
				return this.maxTransactionsPerProcess;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06002B15 RID: 11029 RVA: 0x0005E4A4 File Offset: 0x0005C6A4
		public int TransactionTimeout
		{
			get
			{
				return this.transactionTimeout;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06002B16 RID: 11030 RVA: 0x0005E4AC File Offset: 0x0005C6AC
		public string WorkerProcessPath
		{
			get
			{
				return this.workerProcessPath;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002B17 RID: 11031 RVA: 0x0005E4B4 File Offset: 0x0005C6B4
		public bool RunAsLocalService
		{
			get
			{
				return (this.flags & ComWorkerConfiguration.RunAsFlag.RunAsLocalService) == ComWorkerConfiguration.RunAsFlag.RunAsLocalService;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06002B18 RID: 11032 RVA: 0x0005E4C1 File Offset: 0x0005C6C1
		public bool MayRunUnderAnotherJobObject
		{
			get
			{
				return (this.flags & ComWorkerConfiguration.RunAsFlag.MayRunUnderAnotherJobObject) == ComWorkerConfiguration.RunAsFlag.MayRunUnderAnotherJobObject;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x0005E4CE File Offset: 0x0005C6CE
		public Mutex ProcessLaunchMutex
		{
			get
			{
				return this.processLaunchMutex;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06002B1A RID: 11034 RVA: 0x0005E4D6 File Offset: 0x0005C6D6
		public Guid WorkerGuid
		{
			get
			{
				return this.workerGuid;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x0005E4DE File Offset: 0x0005C6DE
		public string ExtraCommandLineParameters
		{
			get
			{
				return this.cmdlineParams;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06002B1C RID: 11036 RVA: 0x0005E4E6 File Offset: 0x0005C6E6
		public Type WorkerType
		{
			get
			{
				if (this.workerType == null)
				{
					this.workerType = Type.GetTypeFromCLSID(this.workerGuid);
				}
				return this.workerType;
			}
		}

		// Token: 0x04002573 RID: 9587
		private int workerMemoryLimit;

		// Token: 0x04002574 RID: 9588
		private int workerAllocationTimeout;

		// Token: 0x04002575 RID: 9589
		private int workerLifetimeLimit;

		// Token: 0x04002576 RID: 9590
		private int workerIdleTimeout;

		// Token: 0x04002577 RID: 9591
		private int maxTransactionsPerProcess;

		// Token: 0x04002578 RID: 9592
		private int transactionTimeout;

		// Token: 0x04002579 RID: 9593
		private string workerProcessPath;

		// Token: 0x0400257A RID: 9594
		private Mutex processLaunchMutex;

		// Token: 0x0400257B RID: 9595
		private Guid workerGuid;

		// Token: 0x0400257C RID: 9596
		private Type workerType;

		// Token: 0x0400257D RID: 9597
		private ComWorkerConfiguration.RunAsFlag flags;

		// Token: 0x0400257E RID: 9598
		private string cmdlineParams;

		// Token: 0x02000804 RID: 2052
		[Flags]
		public enum RunAsFlag
		{
			// Token: 0x04002580 RID: 9600
			None = 0,
			// Token: 0x04002581 RID: 9601
			RunAsLocalService = 1,
			// Token: 0x04002582 RID: 9602
			MayRunUnderAnotherJobObject = 2
		}
	}
}
