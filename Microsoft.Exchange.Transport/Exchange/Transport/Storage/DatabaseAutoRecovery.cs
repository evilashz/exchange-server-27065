using System;
using System.Globalization;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Win32;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000A7 RID: 167
	internal sealed class DatabaseAutoRecovery
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x00017444 File Offset: 0x00015644
		public DatabaseAutoRecovery(DatabaseRecoveryAction databaseRecoveryAction, string registryKey, string databasePath, string logFilePath, string instanceName, string internalDatabaseName, int permanentErrorCountThreshold, IDatabaseAutoRecoveryEventLogger eventLogger = null)
		{
			if (string.IsNullOrEmpty(registryKey))
			{
				throw new ArgumentNullException("registryKey");
			}
			if (string.IsNullOrEmpty(databasePath))
			{
				throw new ArgumentNullException("databasePath");
			}
			if (string.IsNullOrEmpty(logFilePath))
			{
				throw new ArgumentNullException("logFilePath");
			}
			if (string.IsNullOrEmpty(instanceName))
			{
				throw new ArgumentNullException("instanceName");
			}
			if (string.IsNullOrEmpty(internalDatabaseName))
			{
				throw new ArgumentNullException("internalDatabaseName");
			}
			if (databaseRecoveryAction != DatabaseRecoveryAction.None && databaseRecoveryAction != DatabaseRecoveryAction.Move && databaseRecoveryAction != DatabaseRecoveryAction.Delete)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value for Database Auto Recovery Action: [{0}]", new object[]
				{
					databaseRecoveryAction
				}), "databaseRecoveryAction");
			}
			this.databaseRecoveryAction = databaseRecoveryAction;
			this.registryKey = registryKey;
			this.databasePath = databasePath;
			this.logFilePath = logFilePath;
			this.instanceName = instanceName;
			this.eventLogger = eventLogger;
			this.internalDatabaseName = internalDatabaseName;
			this.permanentErrorCountThreshold = permanentErrorCountThreshold;
			if (this.eventLogger == null)
			{
				this.eventLogger = new DatabaseAutoRecoveryEventLogger();
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00017540 File Offset: 0x00015740
		public override int GetHashCode()
		{
			return this.databasePath.GetHashCode() ^ this.databaseRecoveryAction.GetHashCode() ^ this.eventLogger.GetHashCode() ^ this.instanceName.GetHashCode() ^ this.internalDatabaseName.GetHashCode() ^ this.logFilePath.GetHashCode() ^ this.registryKey.GetHashCode();
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000175A8 File Offset: 0x000157A8
		public void PerformDatabaseAutoRecoveryIfNeccessary()
		{
			if (!this.IsDatabaseCorrupted())
			{
				return;
			}
			switch (this.databaseRecoveryAction)
			{
			case DatabaseRecoveryAction.None:
				this.eventLogger.LogDatabaseRecoveryActionNone(this.instanceName, this.databasePath, this.logFilePath);
				break;
			case DatabaseRecoveryAction.Move:
				this.MoveDatabaseDirectory();
				break;
			case DatabaseRecoveryAction.Delete:
				this.DeleteDatabaseDirectory();
				break;
			default:
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unsupported Data Recovery Action: {0}", new object[]
				{
					this.databaseRecoveryAction
				}));
			}
			this.ResetDatabaseCorruptionFlag();
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001763C File Offset: 0x0001583C
		public bool SetDatabaseCorruptionFlag()
		{
			return this.SetDatabaseCorruptionFlag(this.permanentErrorCountThreshold);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001764A File Offset: 0x0001584A
		public bool ResetDatabaseCorruptionFlag()
		{
			return this.SetDatabaseCorruptionFlag(0);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00017653 File Offset: 0x00015853
		public int GetDatabaseCorruptionCount()
		{
			return this.GetRegistyValue();
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001765C File Offset: 0x0001585C
		public bool IncrementDatabaseCorruptionCount()
		{
			bool result;
			lock (this)
			{
				result = this.SetDatabaseCorruptionFlag(this.GetRegistyValue() + 1);
			}
			return result;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000176A4 File Offset: 0x000158A4
		private static bool IsMovable(string sourceDirectory, string destDirectory)
		{
			if (!Directory.Exists(sourceDirectory))
			{
				return true;
			}
			if (!Directory.Exists(destDirectory))
			{
				return true;
			}
			string fullPath = Path.GetFullPath(destDirectory);
			string[] files = Directory.GetFiles(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
			string[] array = files;
			int i = 0;
			while (i < array.Length)
			{
				string fileName = array[i];
				FileInfo fileInfo = new FileInfo(fileName);
				bool result;
				if (fileInfo.FullName.Equals(fullPath, StringComparison.OrdinalIgnoreCase))
				{
					result = false;
				}
				else
				{
					if (!File.Exists(Path.Combine(destDirectory, fileInfo.Name)))
					{
						i++;
						continue;
					}
					result = false;
				}
				return result;
			}
			string[] directories = Directory.GetDirectories(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
			foreach (string path in directories)
			{
				if (!fullPath.Equals(Path.GetFullPath(path), StringComparison.OrdinalIgnoreCase))
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(path);
					if (Directory.Exists(Path.Combine(destDirectory, directoryInfo.Name)))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00017790 File Offset: 0x00015990
		private static void MoveDirectory(string sourceDirectory, string destDirectory)
		{
			if (!Directory.Exists(sourceDirectory))
			{
				return;
			}
			if (!Directory.Exists(destDirectory))
			{
				Directory.CreateDirectory(destDirectory);
			}
			string[] files = Directory.GetFiles(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
			foreach (string fileName in files)
			{
				FileInfo fileInfo = new FileInfo(fileName);
				fileInfo.MoveTo(Path.Combine(destDirectory, fileInfo.Name));
			}
			string fullPath = Path.GetFullPath(destDirectory);
			string[] directories = Directory.GetDirectories(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
			foreach (string path in directories)
			{
				if (!fullPath.Equals(Path.GetFullPath(path), StringComparison.OrdinalIgnoreCase))
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(path);
					directoryInfo.MoveTo(Path.Combine(destDirectory, directoryInfo.Name));
				}
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00017858 File Offset: 0x00015A58
		private static void DeleteDirectory(string directoryToDelete)
		{
			if (!Directory.Exists(directoryToDelete))
			{
				return;
			}
			string[] files = Directory.GetFiles(directoryToDelete, "*", SearchOption.TopDirectoryOnly);
			foreach (string path in files)
			{
				File.Delete(path);
			}
			string[] directories = Directory.GetDirectories(directoryToDelete, "*", SearchOption.TopDirectoryOnly);
			foreach (string path2 in directories)
			{
				Directory.Delete(path2);
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000178CC File Offset: 0x00015ACC
		private bool SetDatabaseCorruptionFlag(int value)
		{
			if (value >= this.permanentErrorCountThreshold)
			{
				this.eventLogger.DataBaseCorruptionDetected(this.instanceName, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\" + this.registryKey);
			}
			return this.TrySetRegistryValue(value);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00017900 File Offset: 0x00015B00
		private bool MoveDatabaseDirectory()
		{
			string failureReason = string.Empty;
			try
			{
				string str = string.Format(".old-{0:yyyyMMddHHmmss}", DateTime.UtcNow);
				string text = Path.Combine(this.databasePath, this.internalDatabaseName + str);
				string text2 = Path.Combine(this.logFilePath, this.internalDatabaseName + str);
				if (!DatabaseAutoRecovery.IsMovable(this.databasePath, text))
				{
					this.eventLogger.LogDatabaseRecoveryActionFailed(this.instanceName, DatabaseRecoveryAction.Move, Strings.DatabaseIsNotMovable(this.databasePath, text));
					return false;
				}
				if (!Path.GetFullPath(this.logFilePath).Equals(Path.GetFullPath(this.databasePath), StringComparison.OrdinalIgnoreCase))
				{
					if (!DatabaseAutoRecovery.IsMovable(this.logFilePath, text2))
					{
						this.eventLogger.LogDatabaseRecoveryActionFailed(this.instanceName, DatabaseRecoveryAction.Move, Strings.DatabaseIsNotMovable(this.logFilePath, text2));
						return false;
					}
					DatabaseAutoRecovery.MoveDirectory(this.databasePath, text);
					DatabaseAutoRecovery.MoveDirectory(this.logFilePath, text2);
					this.eventLogger.LogDatabaseRecoveryActionMove(this.instanceName, this.databasePath, text, this.logFilePath, text2);
				}
				else
				{
					DatabaseAutoRecovery.MoveDirectory(this.databasePath, text);
					this.eventLogger.LogDatabaseRecoveryActionMove(this.instanceName, this.databasePath, text);
				}
				return true;
			}
			catch (UnauthorizedAccessException ex)
			{
				failureReason = ex.Message;
			}
			catch (ArgumentException ex2)
			{
				failureReason = ex2.Message;
			}
			catch (PathTooLongException ex3)
			{
				failureReason = ex3.Message;
			}
			catch (DirectoryNotFoundException ex4)
			{
				failureReason = ex4.Message;
			}
			catch (IOException ex5)
			{
				failureReason = ex5.Message;
			}
			this.eventLogger.LogDatabaseRecoveryActionFailed(this.instanceName, DatabaseRecoveryAction.Move, failureReason);
			return false;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00017B20 File Offset: 0x00015D20
		private bool DeleteDatabaseDirectory()
		{
			string failureReason = string.Empty;
			try
			{
				DatabaseAutoRecovery.DeleteDirectory(this.databasePath);
				if (!Path.GetFullPath(this.logFilePath).Equals(Path.GetFullPath(this.databasePath), StringComparison.OrdinalIgnoreCase))
				{
					DatabaseAutoRecovery.DeleteDirectory(this.logFilePath);
					this.eventLogger.LogDatabaseRecoveryActionDelete(this.instanceName, this.databasePath, this.logFilePath);
				}
				else
				{
					this.eventLogger.LogDatabaseRecoveryActionDelete(this.instanceName, this.databasePath);
				}
				return true;
			}
			catch (UnauthorizedAccessException ex)
			{
				failureReason = ex.Message;
			}
			catch (ArgumentException ex2)
			{
				failureReason = ex2.Message;
			}
			catch (PathTooLongException ex3)
			{
				failureReason = ex3.Message;
			}
			catch (DirectoryNotFoundException ex4)
			{
				failureReason = ex4.Message;
			}
			catch (IOException ex5)
			{
				failureReason = ex5.Message;
			}
			this.eventLogger.LogDatabaseRecoveryActionFailed(this.instanceName, DatabaseRecoveryAction.Delete, failureReason);
			return false;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00017C30 File Offset: 0x00015E30
		private bool TrySetRegistryValue(int value)
		{
			try
			{
				Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\", this.registryKey, value, RegistryValueKind.DWord);
				return true;
			}
			catch (UnauthorizedAccessException ex)
			{
				this.eventLogger.DatabaseRecoveryActionFailedRegistryAccessDenied(this.instanceName, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\" + this.registryKey, ex.Message);
			}
			catch (SecurityException ex2)
			{
				this.eventLogger.DatabaseRecoveryActionFailedRegistryAccessDenied(this.instanceName, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\" + this.registryKey, ex2.Message);
			}
			return false;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00017CCC File Offset: 0x00015ECC
		private int GetRegistyValue()
		{
			object obj = null;
			try
			{
				obj = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\", this.registryKey, 0);
			}
			catch (IOException ex)
			{
				ExTraceGlobals.ExpoTracer.TraceError((long)this.GetHashCode(), ex.Message);
			}
			catch (SecurityException ex2)
			{
				this.eventLogger.DatabaseRecoveryActionFailedRegistryAccessDenied(this.instanceName, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\" + this.registryKey, ex2.Message);
			}
			if (!(obj is int))
			{
				return 0;
			}
			return (int)obj;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00017D64 File Offset: 0x00015F64
		private bool IsDatabaseCorrupted()
		{
			return this.GetRegistyValue() >= this.permanentErrorCountThreshold;
		}

		// Token: 0x040002E1 RID: 737
		private const string TransportRegistryPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\";

		// Token: 0x040002E2 RID: 738
		private readonly DatabaseRecoveryAction databaseRecoveryAction;

		// Token: 0x040002E3 RID: 739
		private readonly string registryKey;

		// Token: 0x040002E4 RID: 740
		private readonly string databasePath;

		// Token: 0x040002E5 RID: 741
		private readonly string logFilePath;

		// Token: 0x040002E6 RID: 742
		private readonly string instanceName;

		// Token: 0x040002E7 RID: 743
		private readonly string internalDatabaseName;

		// Token: 0x040002E8 RID: 744
		private readonly int permanentErrorCountThreshold;

		// Token: 0x040002E9 RID: 745
		private IDatabaseAutoRecoveryEventLogger eventLogger;
	}
}
