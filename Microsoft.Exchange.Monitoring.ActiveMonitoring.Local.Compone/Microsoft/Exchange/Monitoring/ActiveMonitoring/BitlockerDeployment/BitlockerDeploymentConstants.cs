using System;
using System.IO;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.BitlockerDeployment
{
	// Token: 0x02000022 RID: 34
	internal static class BitlockerDeploymentConstants
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00009D50 File Offset: 0x00007F50
		public static bool DisableResponders
		{
			get
			{
				int value = BitlockerDeploymentUtility.RegReader.GetValue<int>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\BitlockerDeployment\\Parameters", "BigRedButton", 0);
				return value != 0;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00009D80 File Offset: 0x00007F80
		public static bool WorkflowFve
		{
			get
			{
				string registryValue = BitlockerDeploymentUtility.GetRegistryValue("SOFTWARE\\Microsoft\\ExchangeServer\\BitlockerDeployment", "WorkflowFve");
				int num;
				int.TryParse(registryValue, out num);
				return 0 != num;
			}
		}

		// Token: 0x040000D9 RID: 217
		public const string ParameterRegKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\BitlockerDeployment\\Parameters";

		// Token: 0x040000DA RID: 218
		public const string StateRegKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\BitlockerDeployment\\States";

		// Token: 0x040000DB RID: 219
		public const string BitlockerDeploymentRegKey = "SOFTWARE\\Microsoft\\ExchangeServer\\BitlockerDeployment";

		// Token: 0x040000DC RID: 220
		private const string ResponderDisableMasterSwitch = "BigRedButton";

		// Token: 0x040000DD RID: 221
		private const string WorkflowFveSwitch = "WorkflowFve";

		// Token: 0x040000DE RID: 222
		public const string BitlockerUnlockExecutable = "BitlockerDataVolumeUnlocker.exe";

		// Token: 0x040000DF RID: 223
		public const string ServiceName = "BitlockerDeployment";

		// Token: 0x040000E0 RID: 224
		public const string EncryptionStatusProbeName = "EncryptionStatusProbe";

		// Token: 0x040000E1 RID: 225
		public const string BootVolumeEncryptionStatusProbeName = "BootVolumeEncryptionStatusProbe";

		// Token: 0x040000E2 RID: 226
		public const string EncryptionStatusMonitorName = "BitlockerDeploymentStateMonitor";

		// Token: 0x040000E3 RID: 227
		public const string BootVolumeEncryptionStatusMonitorName = "BitlockerDeploymentBootVolumeStateMonitor";

		// Token: 0x040000E4 RID: 228
		public const string EncryptionStatusResponderName = "BitlockerDeploymentStateResponder";

		// Token: 0x040000E5 RID: 229
		public const string EncryptionSuspendedProbeName = "EncryptionSuspendedProbe";

		// Token: 0x040000E6 RID: 230
		public const string EncryptionSuspendedMonitorName = "BitlockerDeploymentSuspendMonitor";

		// Token: 0x040000E7 RID: 231
		public const string EncryptionSuspendedResponderName = "BitlockerDeploymentSuspendResponder";

		// Token: 0x040000E8 RID: 232
		public const string LockStatusProbeName = "LockStatusProbe";

		// Token: 0x040000E9 RID: 233
		public const string LockStatusMonitorName = "BitlockerDeploymentLockMonitor";

		// Token: 0x040000EA RID: 234
		public const string LockStatusResponderName = "BitlockerDeploymentLockResponder";

		// Token: 0x040000EB RID: 235
		public const string DraProtectorProbeName = "DraProtectorProbe";

		// Token: 0x040000EC RID: 236
		public const string DraDecryptorProbeName = "DraDecryptorProbe";

		// Token: 0x040000ED RID: 237
		public const string DraProtectorMonitorName = "DraProtectorMonitor";

		// Token: 0x040000EE RID: 238
		public const string DraDecryptorMonitorName = "DraDecryptorMonitor";

		// Token: 0x040000EF RID: 239
		public const string MasterPassword = "563563-218372-416746-433752-541937-608069-594110-446754";

		// Token: 0x040000F0 RID: 240
		public const int LockResponderRecurrenceInterval = 1200;

		// Token: 0x040000F1 RID: 241
		public const int LockMonitorEscalateInterval = 1260;

		// Token: 0x040000F2 RID: 242
		public const int SuspendResponderRecurrenceInterval = 14400;

		// Token: 0x040000F3 RID: 243
		public const int HourlyRunningMonitorEscalateInterval = 18000;

		// Token: 0x040000F4 RID: 244
		public const string EscalationTeam = "High Availability";

		// Token: 0x040000F5 RID: 245
		public static string LogPath = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "BitlockerActiveMonitoringLogs");

		// Token: 0x040000F6 RID: 246
		public static string DatacenterFolderPath = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Program Files\\Microsoft\\Exchange Server\\V15\\Datacenter");

		// Token: 0x02000023 RID: 35
		public enum BitlockerEncryptionStates
		{
			// Token: 0x040000F8 RID: 248
			FullyDecrypted,
			// Token: 0x040000F9 RID: 249
			FullyEncrypted,
			// Token: 0x040000FA RID: 250
			EncryptionInProgress,
			// Token: 0x040000FB RID: 251
			DecryptionInProgress,
			// Token: 0x040000FC RID: 252
			EncryptionPaused,
			// Token: 0x040000FD RID: 253
			DecryptionPaused
		}

		// Token: 0x02000024 RID: 36
		public enum BitlokerEncryptionLockStates
		{
			// Token: 0x040000FF RID: 255
			Unlocked,
			// Token: 0x04000100 RID: 256
			Locked
		}

		// Token: 0x02000025 RID: 37
		public enum BitlokerEncryptionProtectionStates
		{
			// Token: 0x04000102 RID: 258
			Unprotected,
			// Token: 0x04000103 RID: 259
			Protected,
			// Token: 0x04000104 RID: 260
			Unknown
		}

		// Token: 0x02000026 RID: 38
		public enum BitlockerKeyProtectorType
		{
			// Token: 0x04000106 RID: 262
			Unknown,
			// Token: 0x04000107 RID: 263
			Tpm,
			// Token: 0x04000108 RID: 264
			ExternalKey,
			// Token: 0x04000109 RID: 265
			NumericalPassword,
			// Token: 0x0400010A RID: 266
			TpmAndPin,
			// Token: 0x0400010B RID: 267
			TpmAndStartupKey,
			// Token: 0x0400010C RID: 268
			TpmAndPinAndStartupKey,
			// Token: 0x0400010D RID: 269
			PublicKey,
			// Token: 0x0400010E RID: 270
			Passphrase,
			// Token: 0x0400010F RID: 271
			TpmCertificate,
			// Token: 0x04000110 RID: 272
			CryptoApiNextGen
		}

		// Token: 0x02000027 RID: 39
		internal enum BitlockerCertificateType
		{
			// Token: 0x04000112 RID: 274
			Dra = 1,
			// Token: 0x04000113 RID: 275
			NonDra
		}
	}
}
