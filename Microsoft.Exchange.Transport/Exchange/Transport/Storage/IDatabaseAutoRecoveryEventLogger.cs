using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000A8 RID: 168
	internal interface IDatabaseAutoRecoveryEventLogger
	{
		// Token: 0x060005BB RID: 1467
		void LogDatabaseRecoveryActionNone(string databaseInstanceName, string databasePath, string logFilePath);

		// Token: 0x060005BC RID: 1468
		void LogDatabaseRecoveryActionMove(string databaseInstanceName, string databasePath, string databaseMovePath);

		// Token: 0x060005BD RID: 1469
		void LogDatabaseRecoveryActionMove(string databaseInstanceName, string databasePath, string databaseMovePath, string logFilePath, string moveLogFilePath);

		// Token: 0x060005BE RID: 1470
		void LogDatabaseRecoveryActionDelete(string databaseInstanceName, string databasePath);

		// Token: 0x060005BF RID: 1471
		void LogDatabaseRecoveryActionDelete(string databaseInstanceName, string databasePath, string logFilePath);

		// Token: 0x060005C0 RID: 1472
		void LogDatabaseRecoveryActionFailed(string databaseInstanceName, DatabaseRecoveryAction databaseRecoveryAction, string failureReason);

		// Token: 0x060005C1 RID: 1473
		void DatabaseRecoveryActionFailedRegistryAccessDenied(string databaseInstanceName, string registryKeyPath, string errorMessage);

		// Token: 0x060005C2 RID: 1474
		void DataBaseCorruptionDetected(string databaseInstanceName, string registryKeyPath);
	}
}
