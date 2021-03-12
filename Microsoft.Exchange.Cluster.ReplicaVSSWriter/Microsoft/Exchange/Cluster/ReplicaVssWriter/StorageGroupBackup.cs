using System;
using System.Runtime.InteropServices;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Cluster.ReplicaVssWriter
{
	// Token: 0x0200013D RID: 317
	internal class StorageGroupBackup
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x000019F4 File Offset: 0x00000DF4
		public StorageGroupBackup(string serverName, Guid guidReplica, [MarshalAs(UnmanagedType.U1)] bool fIsPassive)
		{
			this.m_serverName = serverName;
			this.m_guidSGIdentityGuid = guidReplica;
			this.m_fIsPassive = fIsPassive;
			this.m_fComponentBackup = false;
			this.m_fFrozen = false;
			this.m_fSurrogateBackupBegun = false;
			this.m_hccx = null;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000197C File Offset: 0x00000D7C
		public StorageGroupBackup(string serverName, Guid guidReplica, JET_SIGNATURE logfileSignature, long lowestGenerationRequired, long highestGenerationRequired, string destinationLogPath, string logFilePrefix, string logExtension, [MarshalAs(UnmanagedType.U1)] bool fIsPassive)
		{
			this.m_serverName = serverName;
			this.m_guidSGIdentityGuid = guidReplica;
			this.m_fComponentBackup = true;
			this.m_logfileSignature = logfileSignature;
			this.m_lowestGenerationRequired = lowestGenerationRequired;
			this.m_highestGenerationRequired = highestGenerationRequired;
			this.m_fFrozen = false;
			this.m_fSurrogateBackupBegun = false;
			this.m_hccx = null;
			this.m_destinationLogPath = destinationLogPath;
			this.m_logFilePrefix = logFilePrefix;
			this.m_logExtension = logExtension;
			this.m_fIsPassive = fIsPassive;
		}

		// Token: 0x0400026B RID: 619
		public string m_serverName;

		// Token: 0x0400026C RID: 620
		public Guid m_guidSGIdentityGuid;

		// Token: 0x0400026D RID: 621
		public bool m_fComponentBackup;

		// Token: 0x0400026E RID: 622
		public JET_SIGNATURE m_logfileSignature;

		// Token: 0x0400026F RID: 623
		public long m_lowestGenerationRequired;

		// Token: 0x04000270 RID: 624
		public long m_highestGenerationRequired;

		// Token: 0x04000271 RID: 625
		public bool m_fFrozen;

		// Token: 0x04000272 RID: 626
		public bool m_fSurrogateBackupBegun;

		// Token: 0x04000273 RID: 627
		public unsafe void* m_hccx;

		// Token: 0x04000274 RID: 628
		public string m_destinationLogPath;

		// Token: 0x04000275 RID: 629
		public string m_logFilePrefix;

		// Token: 0x04000276 RID: 630
		public string m_logExtension;

		// Token: 0x04000277 RID: 631
		public bool m_fIsPassive;
	}
}
