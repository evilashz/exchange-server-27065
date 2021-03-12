using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001EA RID: 490
	internal static class Globals
	{
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0006F0FC File Offset: 0x0006D2FC
		public static string OabFolderPath
		{
			get
			{
				if (Globals.oabFolderPath == null)
				{
					lock (Globals.initLock)
					{
						if (Globals.oabFolderPath == null)
						{
							Globals.oabFolderPath = Globals.InitializeOABFolderPath();
						}
					}
				}
				return Globals.oabFolderPath;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0006F154 File Offset: 0x0006D354
		public static string AlternateTempFilePath
		{
			get
			{
				if (Globals.alternateTempFilePath == null)
				{
					lock (Globals.initLock)
					{
						if (Globals.oabFolderPath == null)
						{
							Globals.oabFolderPath = Globals.InitializeOABFolderPath();
						}
						if (Globals.alternateTempFilePath == null)
						{
							Globals.alternateTempFilePath = Globals.InitializeAlternateTempPath();
						}
					}
				}
				return Globals.alternateTempFilePath;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x0006F1BC File Offset: 0x0006D3BC
		public static int MaxCompressionBlockSize
		{
			get
			{
				if (Globals.maxCompressionBlockSize == null)
				{
					lock (Globals.initLock)
					{
						if (Globals.maxCompressionBlockSize == null)
						{
							Globals.maxCompressionBlockSize = new int?(Globals.InitializeMaxCompressionBlockSize());
						}
					}
				}
				return Globals.maxCompressionBlockSize.Value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x0006F228 File Offset: 0x0006D428
		public static int ADQueryPageSize
		{
			get
			{
				if (Globals.adQueryPageSize == null)
				{
					lock (Globals.initLock)
					{
						if (Globals.adQueryPageSize == null)
						{
							Globals.adQueryPageSize = new int?(Globals.InitializeADQueryPageSize());
						}
					}
				}
				return Globals.adQueryPageSize.Value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x0006F294 File Offset: 0x0006D494
		public static int TempFileRecordBatchSize
		{
			get
			{
				if (Globals.tempFileRecordBatchSize == null)
				{
					lock (Globals.initLock)
					{
						if (Globals.tempFileRecordBatchSize == null)
						{
							Globals.tempFileRecordBatchSize = new int?(Globals.InitializeTempFileRecordBatchSize());
						}
					}
				}
				return Globals.tempFileRecordBatchSize.Value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0006F300 File Offset: 0x0006D500
		public static TimeSpan LastRequestedTimeGenerationWindow
		{
			get
			{
				if (Globals.lastRequestedTimeGenerationWindow == null)
				{
					lock (Globals.initLock)
					{
						if (Globals.lastRequestedTimeGenerationWindow == null)
						{
							Globals.lastRequestedTimeGenerationWindow = new TimeSpan?(Globals.InitializeLastRequestedTimeGenerationWindow());
						}
					}
				}
				return Globals.lastRequestedTimeGenerationWindow.Value;
			}
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0006F36C File Offset: 0x0006D56C
		public static Exception RunSafeADOperation(ADOperation adOperation, string details)
		{
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(adOperation, 5);
			if (!adoperationResult.Succeeded)
			{
				OABLogger.LogRecord(TraceType.ErrorTrace, "RunSafeADOperation failed with ErrorCode: {0}, Exception: {1}. Operation Details: {2}", new object[]
				{
					adoperationResult.ErrorCode,
					adoperationResult.Exception,
					string.IsNullOrEmpty(details) ? "null" : details
				});
				return adoperationResult.Exception;
			}
			return null;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0006F3D0 File Offset: 0x0006D5D0
		private static string InitializeOABFolderPath()
		{
			string text = null;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
			{
				text = (registryKey.GetValue("OABGeneratorPath") as string);
			}
			if (text == null || !Directory.Exists(text))
			{
				using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
				{
					text = (registryKey2.GetValue("MsiInstallPath") as string);
				}
				if (text == null)
				{
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						text = currentProcess.MainModule.FileName;
					}
					text = Directory.GetParent(text).FullName;
					DirectoryInfo parent = Directory.GetParent(text);
					text = parent.FullName;
				}
				text = Path.Combine(text, "ClientAccess\\OAB");
			}
			ExTraceGlobals.AssistantTracer.TraceDebug(0L, "Initializing Globals.OABFolderPath to " + text);
			return text;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0006F4D0 File Offset: 0x0006D6D0
		private static string InitializeAlternateTempPath()
		{
			string text = null;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
			{
				text = (registryKey.GetValue("OAB V4 Details File Location") as string);
			}
			if (text == null || !Directory.Exists(text))
			{
				text = Path.Combine(Globals.oabFolderPath, "Temp");
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
			}
			ExTraceGlobals.AssistantTracer.TraceDebug(0L, "Initializing Globals.AlternateTempFilePath to " + text);
			return text;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0006F560 File Offset: 0x0006D760
		private static int InitializeMaxCompressionBlockSize()
		{
			int result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
			{
				int num = 262144;
				num = (int)registryKey.GetValue("OAL V4 Block Size", num);
				num = ((num < 512) ? 512 : ((num > 1048576) ? 1048576 : num));
				ExTraceGlobals.AssistantTracer.TraceDebug(0L, "Initializing Globals.MaxCompressionBlockSize to " + num);
				result = num;
			}
			return result;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0006F5F8 File Offset: 0x0006D7F8
		private static int InitializeADQueryPageSize()
		{
			int result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
			{
				int num = ADGenericPagedReader<ADRawEntry>.DefaultPageSize;
				num = (int)registryKey.GetValue("AD Query Page Size", num);
				num = ((num < 100) ? 100 : ((num > 10000) ? 10000 : num));
				ExTraceGlobals.AssistantTracer.TraceDebug(0L, "Initializing Globals.ADQueryPageSize to " + num);
				result = num;
			}
			return result;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0006F688 File Offset: 0x0006D888
		private static int InitializeTempFileRecordBatchSize()
		{
			int result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
			{
				int num = 250;
				num = (int)registryKey.GetValue("Temp File Record Batch Size", num);
				num = ((num < 25) ? 25 : ((num > 10000) ? 10000 : num));
				ExTraceGlobals.AssistantTracer.TraceDebug(0L, "Initializing Globals.TempFileRecordBatchSize to " + num);
				result = num;
			}
			return result;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0006F718 File Offset: 0x0006D918
		private static TimeSpan InitializeLastRequestedTimeGenerationWindow()
		{
			TimeSpan result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
			{
				int num = 21;
				num = (int)registryKey.GetValue("LastRequestedTime Generation Window", num);
				ExTraceGlobals.AssistantTracer.TraceDebug(0L, "Initializing Globals.LastRequestedTimeGenerationWindow to " + num + " days");
				result = TimeSpan.FromDays((double)num);
			}
			return result;
		}

		// Token: 0x04000B92 RID: 2962
		public const uint AddressTemplateVersion = 1U;

		// Token: 0x04000B93 RID: 2963
		public const int DisplayTemplateCount = 7;

		// Token: 0x04000B94 RID: 2964
		public const uint Win32ErrorInsufficientBuffer = 122U;

		// Token: 0x04000B95 RID: 2965
		public const string OABV4FileName = "data.oab";

		// Token: 0x04000B96 RID: 2966
		public const string OABV4BinPatchFileName = "binpatch.oab";

		// Token: 0x04000B97 RID: 2967
		public const string ManifestFileName = "oab.xml";

		// Token: 0x04000B98 RID: 2968
		public const string UncompressedFileExtension = ".flt";

		// Token: 0x04000B99 RID: 2969
		public const string CompressedFileExtension = ".lzx";

		// Token: 0x04000B9A RID: 2970
		private const int MinimumADQueryPageSize = 100;

		// Token: 0x04000B9B RID: 2971
		private const int MinimumTempFileRecordBatchSize = 25;

		// Token: 0x04000B9C RID: 2972
		private const string OABDirectoryPartialPath = "ClientAccess\\OAB";

		// Token: 0x04000B9D RID: 2973
		private const string OABTempDirectoryPartialPath = "Temp";

		// Token: 0x04000B9E RID: 2974
		private static object initLock = new object();

		// Token: 0x04000B9F RID: 2975
		private static string oabFolderPath;

		// Token: 0x04000BA0 RID: 2976
		private static string alternateTempFilePath;

		// Token: 0x04000BA1 RID: 2977
		private static int? maxCompressionBlockSize;

		// Token: 0x04000BA2 RID: 2978
		private static int? adQueryPageSize;

		// Token: 0x04000BA3 RID: 2979
		private static int? tempFileRecordBatchSize;

		// Token: 0x04000BA4 RID: 2980
		private static TimeSpan? lastRequestedTimeGenerationWindow;

		// Token: 0x04000BA5 RID: 2981
		public static readonly ADPropertyDefinition[] RawEntryPropertyDefinitions = new ADPropertyDefinition[]
		{
			ADObjectSchema.Id
		};
	}
}
