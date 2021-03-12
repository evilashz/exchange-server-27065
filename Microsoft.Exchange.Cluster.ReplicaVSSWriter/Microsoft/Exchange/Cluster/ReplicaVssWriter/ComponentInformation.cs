using System;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Cluster.ReplicaVssWriter
{
	// Token: 0x0200013F RID: 319
	internal class ComponentInformation
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00001A8C File Offset: 0x00000E8C
		public ComponentInformation()
		{
			this.m_uIndex = 0U;
			this.m_fPostRestore = false;
			this.m_fIncrementalBackupSet = false;
			this.m_fSelectedForRestore = false;
			this.m_fPrivateMdb = false;
			this.m_fSubComponentsExplicitlySelected = false;
			this.m_fLogsSelectedForRestore = false;
			this.m_fDatabaseFileSelectedForRestore = false;
			this.m_fRemappedGuid = false;
			this.m_fLogsRelocated = false;
			this.m_fCheckpointRelocated = false;
			this.m_fCircularLoggingInBackupSet = false;
			this.m_fCircularLoggingInDBTarget = false;
			this.m_fLegacyRequestor = false;
			this.m_fIncrementalRestore = false;
			this.m_fAdditionalRestores = false;
			this.m_fRunRecovery = false;
			this.m_fEDBRelocated = false;
			this.m_fEDBRenamed = false;
			this.m_status = (VSS_FILE_RESTORE_STATUS)0;
			this.m_rstscen = VssRestoreScenario.rstscenUnknown;
		}

		// Token: 0x04000280 RID: 640
		public static string File = "File";

		// Token: 0x04000281 RID: 641
		public static string Logs = "Logs";

		// Token: 0x04000282 RID: 642
		public static string LogsWildCard = "*.log";

		// Token: 0x04000283 RID: 643
		public static string ReservedLogsWildCard = "*.jrs";

		// Token: 0x04000284 RID: 644
		public static string LogsExtension = ".log";

		// Token: 0x04000285 RID: 645
		public static string CheckpointExtension = ".chk";

		// Token: 0x04000286 RID: 646
		public static string RestoreLogs = "_restoredLogs";

		// Token: 0x04000287 RID: 647
		public static string RestoreEnv = "restore.env";

		// Token: 0x04000288 RID: 648
		public static string TempLogfile = "tmp.log";

		// Token: 0x04000289 RID: 649
		public static string VersionStamp = "VERSION_STAMP";

		// Token: 0x0400028A RID: 650
		public static string SupportedVersion = "15";

		// Token: 0x0400028B RID: 651
		public static string DatabaseGuid = "DATABASE_GUID";

		// Token: 0x0400028C RID: 652
		public static string DatabaseGuidOriginal = "DATABASE_GUID_ORIGINAL";

		// Token: 0x0400028D RID: 653
		public static string DatabaseGuidTarget = "DATABASE_GUID_TARGET";

		// Token: 0x0400028E RID: 654
		public static string SystemPathOriginal = "SYSTEM_PATH_ORIGINAL";

		// Token: 0x0400028F RID: 655
		public static string SystemPathTarget = "SYSTEM_PATH_TARGET";

		// Token: 0x04000290 RID: 656
		public static string LogSignatureId = "LOG_SIGNATURE_ID";

		// Token: 0x04000291 RID: 657
		public static string LogSignatureTimestamp = "LOG_SIGNATURE_TIMESTAMP";

		// Token: 0x04000292 RID: 658
		public static string LogBaseName = "LOG_BASE_NAME";

		// Token: 0x04000293 RID: 659
		public static string LogPathOriginal = "LOG_PATH_ORIGINAL";

		// Token: 0x04000294 RID: 660
		public static string LogPathTarget = "LOG_PATH_TARGET";

		// Token: 0x04000295 RID: 661
		public static string CircularLogging = "CIRCULAR_LOGGING";

		// Token: 0x04000296 RID: 662
		public static string Recovery = "RECOVERY";

		// Token: 0x04000297 RID: 663
		public static string RestoreEnvironment = "<?xml version='1.0'?><DATABASE_RESTORE_ENVIRONMENT></DATABASE_RESTORE_ENVIRONMENT>";

		// Token: 0x04000298 RID: 664
		public static string PrivateMdb = "PRIVATE_MDB";

		// Token: 0x04000299 RID: 665
		public static string EdbLocationOriginal = "EDB_LOCATION_ORIGINAL";

		// Token: 0x0400029A RID: 666
		public static string EdbLocationTarget = "EDB_LOCATION_TARGET";

		// Token: 0x0400029B RID: 667
		public static string EdbFilenameOriginal = "EDB_FILENAME_ORIGINAL";

		// Token: 0x0400029C RID: 668
		public static string EdbFilenameTarget = "EDB_FILENAME_TARGET";

		// Token: 0x0400029D RID: 669
		public static string YES = "Yes";

		// Token: 0x0400029E RID: 670
		public static string NO = "No";

		// Token: 0x0400029F RID: 671
		public uint m_uIndex;

		// Token: 0x040002A0 RID: 672
		public bool m_fPostRestore;

		// Token: 0x040002A1 RID: 673
		public bool m_fIncrementalBackupSet;

		// Token: 0x040002A2 RID: 674
		public bool m_fSelectedForRestore;

		// Token: 0x040002A3 RID: 675
		public bool m_fPrivateMdb;

		// Token: 0x040002A4 RID: 676
		public bool m_fSubComponentsExplicitlySelected;

		// Token: 0x040002A5 RID: 677
		public bool m_fLogsSelectedForRestore;

		// Token: 0x040002A6 RID: 678
		public bool m_fDatabaseFileSelectedForRestore;

		// Token: 0x040002A7 RID: 679
		public bool m_fRemappedGuid;

		// Token: 0x040002A8 RID: 680
		public bool m_fLogsRelocated;

		// Token: 0x040002A9 RID: 681
		public bool m_fCheckpointRelocated;

		// Token: 0x040002AA RID: 682
		public bool m_fCircularLoggingInBackupSet;

		// Token: 0x040002AB RID: 683
		public bool m_fCircularLoggingInDBTarget;

		// Token: 0x040002AC RID: 684
		public bool m_fLegacyRequestor;

		// Token: 0x040002AD RID: 685
		public bool m_fIncrementalRestore;

		// Token: 0x040002AE RID: 686
		public bool m_fAdditionalRestores;

		// Token: 0x040002AF RID: 687
		public bool m_fRunRecovery;

		// Token: 0x040002B0 RID: 688
		public bool m_fEDBRelocated;

		// Token: 0x040002B1 RID: 689
		public bool m_fEDBRenamed;

		// Token: 0x040002B2 RID: 690
		public VSS_FILE_RESTORE_STATUS m_status;

		// Token: 0x040002B3 RID: 691
		public VssRestoreScenario m_rstscen;

		// Token: 0x040002B4 RID: 692
		public ValueType m_guidDBOriginal;

		// Token: 0x040002B5 RID: 693
		public ValueType m_guidDBTarget;

		// Token: 0x040002B6 RID: 694
		public string m_logBaseName;

		// Token: 0x040002B7 RID: 695
		public string m_logPathOriginal;

		// Token: 0x040002B8 RID: 696
		public string m_logPathTarget;

		// Token: 0x040002B9 RID: 697
		public string m_systemPathOriginal;

		// Token: 0x040002BA RID: 698
		public string m_systemPathTarget;

		// Token: 0x040002BB RID: 699
		public string m_edbFilenameOriginal;

		// Token: 0x040002BC RID: 700
		public string m_edbFilenameTarget;

		// Token: 0x040002BD RID: 701
		public string m_edbLocationOriginal;

		// Token: 0x040002BE RID: 702
		public string m_edbLocationTarget;

		// Token: 0x040002BF RID: 703
		public string m_displayNameTarget;

		// Token: 0x040002C0 RID: 704
		public string m_restoreEnv;

		// Token: 0x040002C1 RID: 705
		public string m_restoreEnvXml;

		// Token: 0x040002C2 RID: 706
		public JET_SIGNATURE m_signLog;
	}
}
