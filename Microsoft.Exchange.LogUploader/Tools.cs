using System;
using System.IO;
using Microsoft.Exchange.LogUploaderProxy;
using Microsoft.Win32;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000027 RID: 39
	internal class Tools
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00009D1C File Offset: 0x00007F1C
		public static int GetRegistryKeyIntValue(string key, string name)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("key", key);
			ArgumentValidator.ThrowIfNullOrEmpty("name", name);
			int registryKeyIntValue;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(key))
			{
				registryKeyIntValue = Tools.GetRegistryKeyIntValue(registryKey, name);
			}
			return registryKeyIntValue;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009D70 File Offset: 0x00007F70
		public static int GetRegistryKeyIntValue(RegistryKey key, string name)
		{
			int result = 0;
			if (key != null)
			{
				object value = key.GetValue(name);
				if (value != null)
				{
					result = (int)value;
				}
			}
			return result;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009D98 File Offset: 0x00007F98
		public static int RandomizeTimeSpan(TimeSpan baseTimeSpan, TimeSpan randomRange)
		{
			int num = (int)randomRange.TotalMilliseconds;
			if (num > 2147483647)
			{
				throw new ArgumentException(string.Format("The randomRange {0} exceeds Int32.Max when converting to milliseconds and can't be used as a random number range", num));
			}
			return (int)baseTimeSpan.TotalMilliseconds + new Random().Next(num);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009DE0 File Offset: 0x00007FE0
		public static bool IsRawProcessingType<T>() where T : LogDataBatch
		{
			return LogDataBatchReflectionCache<T>.IsRawBatch;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009DE8 File Offset: 0x00007FE8
		public static void GetPartitionFromPrefix(string fullFilePath, string prefixBeforePartitionId, out int partitionId, out SplitLogType splitLogType)
		{
			partitionId = -1;
			splitLogType = SplitLogType.Older;
			if (string.IsNullOrWhiteSpace(fullFilePath) || string.IsNullOrWhiteSpace(prefixBeforePartitionId))
			{
				throw new ArgumentException("Null or empty or white space input");
			}
			string fileName = Path.GetFileName(fullFilePath);
			if (string.IsNullOrWhiteSpace(fileName))
			{
				throw new ArgumentException(string.Format("{0} is not a valid file path", fullFilePath));
			}
			if (!fileName.StartsWith(prefixBeforePartitionId, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new ArgumentException(string.Format("file name {0} does not start with prefixBeforePartitionId {1}", fileName, prefixBeforePartitionId));
			}
			string[] array = fileName.Substring(prefixBeforePartitionId.Length).Split(new char[]
			{
				'_'
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length < 2 || !int.TryParse(array[0], out partitionId))
			{
				throw new Exception(string.Format("log file name is not in a expected format of {0}_[PartitionId]_[Newer|Older]_xxx", prefixBeforePartitionId));
			}
			bool flag = false;
			foreach (object obj in Enum.GetValues(typeof(SplitLogType)))
			{
				SplitLogType splitLogType2 = (SplitLogType)obj;
				if (string.Compare(array[1], splitLogType2.ToString(), true) == 0)
				{
					flag = true;
					splitLogType = splitLogType2;
					break;
				}
			}
			if (!flag)
			{
				throw new Exception(string.Format("log file name is not in a expected format of {0}_[PartitionId]_[Newer|Older]_xxx", prefixBeforePartitionId));
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009F20 File Offset: 0x00008120
		public static void DebugAssert(bool condition, string message)
		{
		}

		// Token: 0x04000120 RID: 288
		public const string ServiceName = "MSMessageTracingClient";

		// Token: 0x04000121 RID: 289
		public const string InsufficientResourcesString = "Insufficient system resources exist to complete the requested service";

		// Token: 0x04000122 RID: 290
		public const string ServerRole = "ServerRole";

		// Token: 0x04000123 RID: 291
		public const string SysProbeLogPrefix = "SYSPRB";

		// Token: 0x04000124 RID: 292
		public const string ServiceEnabledRegKey = "System\\CurrentControlSet\\Services\\MSMessageTracingClient";

		// Token: 0x04000125 RID: 293
		public const string ServiceEnabledRegKeyFullPath = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\MSMessageTracingClient";

		// Token: 0x04000126 RID: 294
		public const string ServiceEnabledRegValue = "ServiceEnabled";

		// Token: 0x04000127 RID: 295
		public const string OpticsEnabledRegValue = "OpticsEnabled";

		// Token: 0x04000128 RID: 296
		public const string OpticsEnabledRegKeyFullPath = "HKEY_LOCAL_MACHINE\\OpticsEnabled";

		// Token: 0x04000129 RID: 297
		public static readonly TimeSpan SleepCheckStopRequestInterval = TimeSpan.FromSeconds(5.0);
	}
}
