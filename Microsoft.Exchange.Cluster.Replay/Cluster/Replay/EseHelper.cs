using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000E8 RID: 232
	internal static class EseHelper
	{
		// Token: 0x0600095A RID: 2394 RVA: 0x0002BD24 File Offset: 0x00029F24
		public static void GlobalInit()
		{
			int num = 32768;
			lock (EseHelper.globalInitLock)
			{
				if (!EseHelper.g_fGlobalInitCalled)
				{
					string paramString = null;
					SystemParameters.EnableAdvanced = true;
					SystemParameters.DatabasePageSize = num;
					SystemParameters.MaxInstances = 1000;
					if (RegistryParameters.LogInspectorReadSize > 0)
					{
						Api.JetSetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, (JET_param)164, RegistryParameters.LogInspectorReadSize, paramString);
					}
					int paramValue = 33554432 / num;
					Api.JetSetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, JET_param.CacheSizeMin, paramValue, paramString);
					Api.JetSetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, (JET_param)36, 1, paramString);
					Api.JetSetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, JET_param.ExceptionAction, 2, paramString);
					Api.JetSetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, (JET_param)53, 1000, paramString);
					EseHelper.g_fGlobalInitCalled = true;
				}
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0002BE0C File Offset: 0x0002A00C
		public static bool CreateTempLog(string baseName, string logfilePath, out Exception exception)
		{
			string path = baseName + ".log";
			string path2 = baseName + "tmp.log";
			string text = Path.Combine(logfilePath, path2);
			bool result = false;
			exception = null;
			try
			{
				if (!File.Exists(Path.Combine(logfilePath, path)) && !File.Exists(text))
				{
					if (!Directory.Exists(logfilePath))
					{
						Directory.CreateDirectory(logfilePath, ObjectSecurity.ExchangeFolderSecurity);
						ExTraceGlobals.EseutilWrapperTracer.TraceDebug<string>(0L, "Directory created: {0}", logfilePath);
						ExTraceGlobals.PFDTracer.TracePfd<int, string>(0L, "PFD CRS {0} Directory created: {1}", 24733, logfilePath);
					}
					using (File.Create(text))
					{
					}
					ExTraceGlobals.EseutilWrapperTracer.TraceDebug<string>(0L, "E00tmp log file created: {0}", text);
					ExTraceGlobals.PFDTracer.TracePfd<int, string>(0L, "PFD CRS {0} E00tmp log file created: {1}", 16541, text);
				}
				result = true;
			}
			catch (IOException ex)
			{
				exception = ex;
				ExTraceGlobals.EseutilWrapperTracer.TraceError<IOException>(0L, "Failed to CreateTempLog : {0}", ex);
			}
			catch (UnauthorizedAccessException ex2)
			{
				exception = ex2;
				ExTraceGlobals.EseutilWrapperTracer.TraceError<UnauthorizedAccessException>(0L, "Failed to CreateTempLog : {0}", ex2);
			}
			return result;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0002BF38 File Offset: 0x0002A138
		public static long DumpCheckpoint(string checkpointFile)
		{
			int logGenFromCheckpointFileInfo = UnpublishedApi.GetLogGenFromCheckpointFileInfo(checkpointFile);
			return (long)logGenFromCheckpointFileInfo;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0002BF50 File Offset: 0x0002A150
		public static bool IsLogfileSubset(string logfile1, string logfile2, string temporaryDirectory, EseLogRecordPosition lastRec1, EseLogRecordPosition lastRec2)
		{
			EseLogRecord[] logRecords = EseHelper.GetLogRecords(logfile1, temporaryDirectory);
			EseLogRecord[] logRecords2 = EseHelper.GetLogRecords(logfile2, temporaryDirectory);
			if (lastRec1 != null)
			{
				EseLogRecordPosition lastLogRecordPosition = EseHelper.GetLastLogRecordPosition(logRecords);
				if (lastLogRecordPosition != null)
				{
					lastRec1.LogPos = lastLogRecordPosition.LogPos;
					lastRec1.LogRecordLength = lastLogRecordPosition.LogRecordLength;
					lastRec1.LogSectorSize = lastLogRecordPosition.LogSectorSize;
				}
			}
			if (lastRec2 != null)
			{
				EseLogRecordPosition lastLogRecordPosition2 = EseHelper.GetLastLogRecordPosition(logRecords2);
				if (lastLogRecordPosition2 != null)
				{
					lastRec2.LogPos = lastLogRecordPosition2.LogPos;
					lastRec2.LogRecordLength = lastLogRecordPosition2.LogRecordLength;
					lastRec2.LogSectorSize = lastLogRecordPosition2.LogSectorSize;
				}
			}
			if (logRecords2.Length > logRecords.Length)
			{
				ExTraceGlobals.EseutilWrapperTracer.TraceDebug(0L, "IsLogfileSubset({0},{1}) returns false. {0} has {2} records. {1} has {3} records.", new object[]
				{
					logfile1,
					logfile2,
					logRecords.Length,
					logRecords2.Length
				});
				return false;
			}
			for (int i = 0; i < logRecords2.Length - 1; i++)
			{
				bool flag = true;
				if ((!(logRecords[i] is EseChecksumRecord) || !(logRecords2[i] is EseChecksumRecord)) && logRecords[i].ToString() != logRecords2[i].ToString())
				{
					flag = false;
				}
				if (!flag)
				{
					ExTraceGlobals.EseutilWrapperTracer.TraceDebug(0L, "IsLogfileSubset({0},{1}) returns false. {0} has {2} records. {1} has {3} records. The differing log records are {4} and {5}. Differing position is {6}.", new object[]
					{
						logfile1,
						logfile2,
						logRecords.Length,
						logRecords2.Length,
						logRecords[i],
						logRecords2[i],
						i
					});
					return false;
				}
			}
			if (!(logRecords2[logRecords2.Length - 1] is EseEofRecord))
			{
				ExTraceGlobals.EseutilWrapperTracer.TraceDebug(0L, "IsLogfileSubset({0},{1}) returns false. {0} has {2} records. {1} has {3} records. File {1} does not end in an EOF record. The last record is {4}.", new object[]
				{
					logfile1,
					logfile2,
					logRecords.Length,
					logRecords2.Length,
					logRecords2[logRecords2.Length - 1]
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0002C120 File Offset: 0x0002A320
		public static bool IsLogfileEqual(string logfile1, string logfile2, string temporaryDirectory, EseLogRecordPosition lastRec1, EseLogRecordPosition lastRec2)
		{
			EseLogRecord[] logRecords = EseHelper.GetLogRecords(logfile1, temporaryDirectory);
			EseLogRecord[] logRecords2 = EseHelper.GetLogRecords(logfile2, temporaryDirectory);
			if (lastRec1 != null)
			{
				EseLogRecordPosition lastLogRecordPosition = EseHelper.GetLastLogRecordPosition(logRecords);
				if (lastLogRecordPosition != null)
				{
					lastRec1.LogPos = lastLogRecordPosition.LogPos;
					lastRec1.LogRecordLength = lastLogRecordPosition.LogRecordLength;
					lastRec1.LogSectorSize = lastLogRecordPosition.LogSectorSize;
				}
			}
			if (lastRec2 != null)
			{
				EseLogRecordPosition lastLogRecordPosition2 = EseHelper.GetLastLogRecordPosition(logRecords2);
				if (lastLogRecordPosition2 != null)
				{
					lastRec2.LogPos = lastLogRecordPosition2.LogPos;
					lastRec2.LogRecordLength = lastLogRecordPosition2.LogRecordLength;
					lastRec2.LogSectorSize = lastLogRecordPosition2.LogSectorSize;
				}
			}
			if (logRecords2.Length != logRecords.Length)
			{
				ExTraceGlobals.EseutilWrapperTracer.TraceDebug(0L, "IsLogfileEqual({0},{1}) returns false. {0} has {2} records. {1} has {3} records.", new object[]
				{
					logfile1,
					logfile2,
					logRecords.Length,
					logRecords2.Length
				});
				return false;
			}
			for (int i = 0; i < logRecords.Length; i++)
			{
				if (logRecords[i].ToString() != logRecords2[i].ToString())
				{
					ExTraceGlobals.EseutilWrapperTracer.TraceDebug(0L, "IsLogfileEqual({0},{1}) returns false. {0} has {2} records. {1} has {3} records. The differing log records are {4} and {5}. Differing position is {6}.", new object[]
					{
						logfile1,
						logfile2,
						logRecords.Length,
						logRecords2.Length,
						logRecords[i],
						logRecords2[i],
						i
					});
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0002C270 File Offset: 0x0002A470
		public static EseLogRecordPosition GetLastLogRecordPosition(string logfileName, string temporaryDirectory, out Exception ex)
		{
			EseLogRecordPosition result = null;
			ex = null;
			try
			{
				EseLogRecord[] logRecords = EseHelper.GetLogRecords(logfileName, temporaryDirectory);
				if (logRecords.Length > 0)
				{
					result = EseHelper.GetLastLogRecordPosition(logRecords);
				}
			}
			catch (EsentErrorException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			catch (UnauthorizedAccessException ex4)
			{
				ex = ex4;
			}
			return result;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0002C2D8 File Offset: 0x0002A4D8
		public static EseLogRecordPosition GetLastLogRecordPosition(EseLogRecord[] records)
		{
			EseLogRecordPosition eseLogRecordPosition = null;
			if (records.Length > 0)
			{
				int logSectorSize = 0;
				EseLogHeaderRecord eseLogHeaderRecord = records[0] as EseLogHeaderRecord;
				if (eseLogHeaderRecord != null)
				{
					logSectorSize = eseLogHeaderRecord.SectorSize;
				}
				logSectorSize = EseLogPos.CheckSectorSize(logSectorSize);
				eseLogRecordPosition = new EseLogRecordPosition();
				eseLogRecordPosition.LogSectorSize = logSectorSize;
				for (int i = records.Length - 1; i >= 1; i--)
				{
					if (records[i].LogPos != null)
					{
						eseLogRecordPosition.LogPos = records[i].LogPos;
						eseLogRecordPosition.LogRecordLength = records[i].LogRecSize;
						break;
					}
				}
			}
			return eseLogRecordPosition;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0002C350 File Offset: 0x0002A550
		public static bool IsDatabaseConsistent(string database)
		{
			JET_DBINFOMISC jet_DBINFOMISC;
			Api.JetGetDatabaseFileInfo(database, out jet_DBINFOMISC, JET_DbInfo.Misc);
			return JET_dbstate.CleanShutdown == jet_DBINFOMISC.dbstate;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002C370 File Offset: 0x0002A570
		public static bool IsDatabaseDirty(string database)
		{
			JET_DBINFOMISC jet_DBINFOMISC;
			Api.JetGetDatabaseFileInfo(database, out jet_DBINFOMISC, JET_DbInfo.Misc);
			return JET_dbstate.DirtyShutdown == jet_DBINFOMISC.dbstate;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0002C390 File Offset: 0x0002A590
		public static bool IsIncrementalReseedPossible(string database, out JET_DBINFOMISC dbInfo)
		{
			Api.JetGetDatabaseFileInfo(database, out dbInfo, JET_DbInfo.Misc);
			return EseHelper.IsIncrementalReseedPossible(dbInfo);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0002C3A4 File Offset: 0x0002A5A4
		public static bool IsIncrementalReseedPossible(JET_DBINFOMISC dbInfo)
		{
			JET_dbstate dbstate = dbInfo.dbstate;
			return dbstate == JET_dbstate.DirtyShutdown || dbstate == (JET_dbstate)7 || dbstate == JET_dbstate.CleanShutdown || dbstate == (JET_dbstate)6;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0002C3CC File Offset: 0x0002A5CC
		public static bool IsIncrementalReseedInProgress(string database)
		{
			JET_DBINFOMISC dbInfo;
			Api.JetGetDatabaseFileInfo(database, out dbInfo, JET_DbInfo.Misc);
			return EseHelper.IsIncrementalReseedInProgress(dbInfo);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0002C3E9 File Offset: 0x0002A5E9
		public static bool IsIncrementalReseedInProgress(JET_DBINFOMISC dbInfo)
		{
			return dbInfo.dbstate == (JET_dbstate)6;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0002C3F4 File Offset: 0x0002A5F4
		public static bool IsV1IncrementalReseedSupported(string database)
		{
			JET_DBINFOMISC jet_DBINFOMISC;
			Api.JetGetDatabaseFileInfo(database, out jet_DBINFOMISC, JET_DbInfo.Misc);
			JET_dbstate dbstate = jet_DBINFOMISC.dbstate;
			return dbstate == JET_dbstate.DirtyShutdown || dbstate == JET_dbstate.CleanShutdown;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0002C420 File Offset: 0x0002A620
		public static long GetLogfileGeneration(string logfilePath)
		{
			JET_LOGINFOMISC jet_LOGINFOMISC;
			return EseHelper.GetLogfileGeneration(logfilePath, out jet_LOGINFOMISC);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0002C435 File Offset: 0x0002A635
		public static long GetLogfileGeneration(string logfilePath, out JET_LOGINFOMISC logInfo)
		{
			UnpublishedApi.JetGetLogFileInfo(logfilePath, out logInfo, JET_LogInfo.Misc2);
			return (long)logInfo.ulGeneration;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002C448 File Offset: 0x0002A648
		public static long GetLogfileGenerationFromFilePath(string logfilePath, string logfilePrefix)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(logfilePath);
			string s = fileNameWithoutExtension.Substring(logfilePrefix.Length);
			long maxValue;
			if (!long.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out maxValue))
			{
				maxValue = long.MaxValue;
			}
			return maxValue;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0002C488 File Offset: 0x0002A688
		public static string MakeCheckpointFileName(string logfilePrefix)
		{
			return logfilePrefix + ".chk";
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0002C498 File Offset: 0x0002A698
		public static string MakeLogfileName(string logfilePrefix, string logfileSuffix, long generation)
		{
			if (0L == generation)
			{
				return EseHelper.MakeLogfileName(logfilePrefix, logfileSuffix);
			}
			return logfilePrefix + string.Format(CultureInfo.InvariantCulture, "{0:X8}", new object[]
			{
				generation
			}) + logfileSuffix;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0002C4D9 File Offset: 0x0002A6D9
		public static string MakeLogfileName(string logfilePrefix, string logfileSuffix)
		{
			return logfilePrefix + logfileSuffix;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0002C4E4 File Offset: 0x0002A6E4
		public static string MakeLogFilePath(IReplayConfiguration config, long generation, string directory)
		{
			string logfileSuffix = "." + config.LogExtension;
			string path = EseHelper.MakeLogfileName(config.LogFilePrefix, logfileSuffix, generation);
			return Path.Combine(directory, path);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0002C51C File Offset: 0x0002A71C
		public static bool GetGenerationNumberFromFilename(string filename, string prefix, out long generation)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
			return long.TryParse(fileNameWithoutExtension.Substring(prefix.Length), NumberStyles.HexNumber, null, out generation);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0002C548 File Offset: 0x0002A748
		public static EseLogRecord[] GetLogRecords(string logfile, string temporaryDirectory)
		{
			EseLogRecord[] result;
			using (TemporaryFile temporaryFile = EseHelper.CreateTemporaryFile(temporaryDirectory))
			{
				EseHelper.DumpLogRecordsToFile(logfile, temporaryFile);
				result = EseHelper.ParseLogfileDump(temporaryFile);
			}
			return result;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0002C598 File Offset: 0x0002A798
		internal static void DumpLogRecordsToFile(string logfile, string outputFile)
		{
			ExTraceGlobals.EseutilWrapperTracer.TraceDebug<string, string>(0L, "Dumping logfile {0} to output file {1}", logfile, outputFile);
			string basename = EseHelper.BaseNameFromLogfileName(logfile);
			LogVerifier logVerifier = new LogVerifier(basename, outputFile);
			try
			{
				logVerifier.Dump(logfile);
			}
			finally
			{
				logVerifier.Term();
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0002C5E8 File Offset: 0x0002A7E8
		private static EseLogRecord[] ParseLogfileDump(string dumpFile)
		{
			EseLogRecord[] result;
			using (FileStream fileStream = new FileStream(dumpFile, FileMode.Open, FileAccess.Read, FileShare.None, 65536, FileOptions.DeleteOnClose | FileOptions.SequentialScan))
			{
				using (StreamReader streamReader = new StreamReader(fileStream, Encoding.Unicode))
				{
					List<EseLogRecord> list = new List<EseLogRecord>();
					string input;
					while ((input = streamReader.ReadLine()) != null)
					{
						list.Add(EseLogRecord.Parse(input));
					}
					result = list.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0002C674 File Offset: 0x0002A874
		private static string CreateTemporaryFile(string directory)
		{
			string text = Path.Combine(directory, Path.GetRandomFileName());
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}
			using (File.Create(text, 8, FileOptions.None, ObjectSecurity.TemporaryFileSecurity))
			{
			}
			return text;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0002C6C8 File Offset: 0x0002A8C8
		internal static string BaseNameFromLogfileName(string logfile)
		{
			string fileName = Path.GetFileName(logfile);
			string text = fileName.Substring(0, 3);
			return text.ToLowerInvariant();
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0002C6EB File Offset: 0x0002A8EB
		internal static void InternalTest()
		{
			EseHelper.InternalTestBaseNameFromLogfileName();
			EseHelper.InternalTestNameGeneration();
			EseHelper.InternalTestParseLogfileDump();
			EseHelper.InternalTestParseLogfileDumpBadData();
			EseHelper.InternalTestCreateTempLog();
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0002C708 File Offset: 0x0002A908
		private static void InternalTestCreateTempLog()
		{
			string randomFileName = Path.GetRandomFileName();
			string text = "e13";
			string destFileName = Path.Combine(randomFileName, "e13.log");
			string text2 = Path.Combine(randomFileName, "e13tmp.log");
			DiagCore.RetailAssert(!File.Exists(text2), "file {0} shouldn't exist", new object[]
			{
				text2
			});
			Exception ex;
			bool condition = EseHelper.CreateTempLog(text, randomFileName, out ex);
			DiagCore.RetailAssert(condition, "CreateTempLog({0},{1}) returned false", new object[]
			{
				text,
				randomFileName
			});
			DiagCore.RetailAssert(File.Exists(text2), "file {0} should exist", new object[]
			{
				text2
			});
			condition = EseHelper.CreateTempLog(text, randomFileName, out ex);
			DiagCore.RetailAssert(condition, "CreateTempLog({0},{1}) returned false", new object[]
			{
				text,
				randomFileName
			});
			DiagCore.RetailAssert(File.Exists(text2), "file {0} should exist", new object[]
			{
				text2
			});
			File.Move(text2, destFileName);
			condition = EseHelper.CreateTempLog(text, randomFileName, out ex);
			DiagCore.RetailAssert(condition, "CreateTempLog({0},{1}) returned false", new object[]
			{
				text,
				randomFileName
			});
			DiagCore.RetailAssert(!File.Exists(text2), "file {0} shouldn't exist", new object[]
			{
				text2
			});
			Directory.Delete(randomFileName, true);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0002C84C File Offset: 0x0002AA4C
		private static void InternalTestBaseNameFromLogfileName(string logfileName, string expectedBaseName)
		{
			string text = EseHelper.BaseNameFromLogfileName(logfileName);
			DiagCore.RetailAssert(expectedBaseName == text, "BaseNameFromLogfileName({0}) returned {1}, expected {2}", new object[]
			{
				logfileName,
				text,
				expectedBaseName
			});
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0002C885 File Offset: 0x0002AA85
		private static void InternalTestBaseNameFromLogfileName()
		{
			EseHelper.InternalTestBaseNameFromLogfileName("e00.log", "e00");
			EseHelper.InternalTestBaseNameFromLogfileName("e0912345678.log", "e09");
			EseHelper.InternalTestBaseNameFromLogfileName("c:\\Data\\Files\\E07.log", "e07");
			EseHelper.InternalTestBaseNameFromLogfileName("z:\\Log Directory\\w060000AFC.log", "w06");
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0002C8C4 File Offset: 0x0002AAC4
		private static void InternalTestNameGeneration()
		{
			ExTraceGlobals.EseutilWrapperTracer.TraceDebug(0L, "EseHelper.InternalTestNameGeneration");
			string text = EseHelper.MakeCheckpointFileName("E09");
			DiagCore.RetailAssert("E09.chk" == text, "MakeCheckpointFileName returned {0}", new object[]
			{
				text
			});
			text = EseHelper.MakeLogfileName("E00", ".jtx", 5670691L);
			DiagCore.RetailAssert("E0000568723.jtx" == text, "MakeLogfileName returned {0}", new object[]
			{
				text
			});
			text = EseHelper.MakeLogfileName("E01", ".log");
			DiagCore.RetailAssert("E01.log" == text, "MakeLogfileName returned {0}", new object[]
			{
				text
			});
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0002C978 File Offset: 0x0002AB78
		private static void InternalTestParseLogfileDump()
		{
			ExTraceGlobals.EseutilWrapperTracer.TraceDebug(0L, "EseHelper.InternalTestParseLogfileDump");
			string value = string.Concat(new string[]
			{
				"LHGI, Create time:12/14/2005 15:30:42 Rand:9483281 Computer:, 00004B9E, 01/23/2006 09:09:02, 01/23/2006 09:06:33, 7.3704.8, 0x0",
				Environment.NewLine,
				"LHGI, Create time:01/28/2006 16:32:56 Rand:277109190 Computer:, 00000001, 01/28/2006 16:32:56, 00/00/1900 00:00:00, 7.3704.8, 0x1",
				Environment.NewLine,
				"LHAI, 1, D:\\StoreA\\MDB1\\priv1.edb",
				Environment.NewLine,
				"LRDI, 0000000075CBABC3, CreateDB , 1, C:\\temp3\\bar.edb",
				Environment.NewLine,
				"LRDI, 0000000075FBABC3, AttachDB , 2, bar.edb",
				Environment.NewLine,
				"LRDI, 0047000075CBABC3, DetachDB , 6, Z:\\temp3\\database files\\bar.edb",
				Environment.NewLine,
				"LRCI, 514F9292BBAD5F0A",
				Environment.NewLine,
				"LRPI, 00000000EF1FB879, Insert   , 005FE92F, 0000036E, 00000001, FFFFFFFFFFFFFFFF, 0000000008DCDFD0",
				Environment.NewLine,
				"LRMI, 00000000D01F91B3, McroComit",
				Environment.NewLine,
				"LRMI, 00000000D01F91B3, McroComit",
				Environment.NewLine,
				"LTEL",
				Environment.NewLine,
				string.Empty
			});
			string text = EseHelper.CreateTemporaryFile(Path.GetTempPath());
			DiagCore.RetailAssert(File.Exists(text), "Temporary file {0} doesn't exist.", new object[]
			{
				text
			});
			using (StreamWriter streamWriter = new StreamWriter(text, false, Encoding.Unicode))
			{
				streamWriter.Write(value);
			}
			EseLogRecord[] array = EseHelper.ParseLogfileDump(text);
			DiagCore.RetailAssert(!File.Exists(text), "File {0} should have been deleted.", new object[]
			{
				text
			});
			foreach (EseLogRecord eseLogRecord in array)
			{
				string value2 = eseLogRecord.ToString();
				DiagCore.RetailAssert(!string.IsNullOrEmpty(value2), "Empty string returned from EseLogRecord.ToString().", new object[0]);
			}
			int num = 0;
			EseLogHeaderRecord eseLogHeaderRecord = array[num++] as EseLogHeaderRecord;
			DiagCore.RetailAssert(eseLogHeaderRecord.Signature == "Create time:12/14/2005 15:30:42 Rand:9483281 Computer:", "EseLogHeaderRecord.Signature not correct.", new object[0]);
			DiagCore.RetailAssert(eseLogHeaderRecord.Generation == 19358L, "EseLogHeaderRecord.Generation not correct.", new object[0]);
			DateTime value3 = new DateTime(2006, 1, 23, 9, 9, 2);
			DiagCore.RetailAssert(eseLogHeaderRecord.CreationTime.Equals(value3), "EseLogHeaderRecord.CreationTime not correct.", new object[0]);
			value3 = new DateTime(2006, 1, 23, 9, 6, 33);
			DiagCore.RetailAssert(eseLogHeaderRecord.PreviousGenerationCreationTime.Equals(value3), "EseLogHeaderRecord.PreviousGenerationCreationTime not correct.", new object[0]);
			DiagCore.RetailAssert(eseLogHeaderRecord.LogFormatVersion == "7.3704.8", "EseLogHeaderRecord.LogFormatVersion not correct.", new object[0]);
			DiagCore.RetailAssert(!eseLogHeaderRecord.IsCircularLoggingOn, "EseLogHeaderRecord.IsCircularLoggingOn not correct.", new object[0]);
			eseLogHeaderRecord = (array[num++] as EseLogHeaderRecord);
			DiagCore.RetailAssert(eseLogHeaderRecord.PreviousGenerationCreationTime.Equals(DateTime.MinValue), "EseLogHeaderRecord.PreviousGenerationCreationTime not correct.", new object[0]);
			DiagCore.RetailAssert(eseLogHeaderRecord.IsCircularLoggingOn, "EseLogHeaderRecord.IsCircularLoggingOn not correct.", new object[0]);
			EseAttachInfoRecord eseAttachInfoRecord = array[num++] as EseAttachInfoRecord;
			DiagCore.RetailAssert(eseAttachInfoRecord.DatabaseId == 1, "EseAttachInfoRecord.DatabaseId not correct.", new object[0]);
			DiagCore.RetailAssert(eseAttachInfoRecord.Database == "D:\\StoreA\\MDB1\\priv1.edb", "EseAttachInfoRecord.Database not correct.", new object[0]);
			EseDatabaseFileRecord eseDatabaseFileRecord = array[num++] as EseDatabaseFileRecord;
			DiagCore.RetailAssert(eseDatabaseFileRecord.Checksum == 1976282051UL, "EseDatabaseFileRecord.Checksum not correct.", new object[0]);
			DiagCore.RetailAssert(eseDatabaseFileRecord.DatabaseId == 1, "EseDatabaseFileRecord.DatabaseId not correct.", new object[0]);
			DiagCore.RetailAssert(eseDatabaseFileRecord.Database == "C:\\temp3\\bar.edb", "EseDatabaseFileRecord.Database not correct.", new object[0]);
			DiagCore.RetailAssert(eseDatabaseFileRecord.Operation == DatabaseOperation.Create, "EseDatabaseFileRecord.Operation not correct.", new object[0]);
			eseDatabaseFileRecord = (array[num++] as EseDatabaseFileRecord);
			DiagCore.RetailAssert(eseDatabaseFileRecord.Operation == DatabaseOperation.Attach, "EseDatabaseFileRecord.Operation not correct.", new object[0]);
			eseDatabaseFileRecord = (array[num++] as EseDatabaseFileRecord);
			DiagCore.RetailAssert(eseDatabaseFileRecord.Operation == DatabaseOperation.Detach, "EseDatabaseFileRecord.Operation not correct.", new object[0]);
			DiagCore.RetailAssert(eseDatabaseFileRecord.Database == "Z:\\temp3\\database files\\bar.edb", "EseDatabaseFileRecord.Database not correct.", new object[0]);
			EseChecksumRecord eseChecksumRecord = array[num++] as EseChecksumRecord;
			DiagCore.RetailAssert(eseChecksumRecord.Checksum == 5859062799143886602UL, "EseChecksumRecord.Checksum not correct.", new object[0]);
			EsePageRecord esePageRecord = array[num++] as EsePageRecord;
			DiagCore.RetailAssert(esePageRecord.Checksum == (ulong)-283133831, "EsePageRecord.Checksum not correct.", new object[0]);
			DiagCore.RetailAssert(esePageRecord.Operation == "Insert", "EsePageRecord.Operation not correct.", new object[0]);
			DiagCore.RetailAssert(esePageRecord.PageNumber == 6285615L, "EsePageRecord.PageNumber not correct.", new object[0]);
			DiagCore.RetailAssert(esePageRecord.ObjectId == 878L, "EsePageRecord.ObjectId not correct.", new object[0]);
			DiagCore.RetailAssert(esePageRecord.DatabaseId == 1L, "EsePageRecord.DatabaseId not correct.", new object[0]);
			DiagCore.RetailAssert(esePageRecord.DbtimeBefore == ulong.MaxValue, "EsePageRecord.DbtimeBefore not correct.", new object[0]);
			DiagCore.RetailAssert(esePageRecord.DbtimeAfter == 148692944UL, "EsePageRecord.DbtimeAfter not correct.", new object[0]);
			EseMiscRecord eseMiscRecord = array[num++] as EseMiscRecord;
			DiagCore.RetailAssert(eseMiscRecord.Checksum == (ulong)-803237453, "EseMiscRecord.Checksum not correct.", new object[0]);
			DiagCore.RetailAssert(eseMiscRecord.Operation == "McroComit", "EseMiscRecord.Operation not correct.", new object[0]);
			DiagCore.RetailAssert(array[num].ToString() == array[num - 1].ToString(), "Records not equal.", new object[0]);
			DiagCore.RetailAssert(array[num].ToString() != array[num - 2].ToString(), "Records equal.", new object[0]);
			EseLogRecord eseLogRecord2 = array[num++];
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0002CFA4 File Offset: 0x0002B1A4
		private static void InternalTestParseLogfileDumpBadData()
		{
			ExTraceGlobals.EseutilWrapperTracer.TraceDebug(0L, "EseHelper.InternalTestParseLogfileDumpBadData");
			string value = "LHGX, Create time:12/14/2005 15:30:42 Rand:9483281 Computer:, 00004B9E, 01/23/2006 09:09:02, 01/23/2006 09:06:33, 7.3704.8, 0x0" + Environment.NewLine + string.Empty;
			string value2 = "LRPI, 00000000EF1FB879, Insert   , 005FE92F, THIS IS BAD DATA, 0000036E, 00000001, 0000000008DCDFA0, 0000000008DCDFD0" + Environment.NewLine + string.Empty;
			string text = EseHelper.CreateTemporaryFile(Path.GetTempPath());
			DiagCore.RetailAssert(File.Exists(text), "Temporary file {0} doesn't exist.", new object[]
			{
				text
			});
			using (StreamWriter streamWriter = new StreamWriter(text, false, Encoding.Unicode))
			{
				streamWriter.Write(value);
			}
			try
			{
				EseHelper.ParseLogfileDump(text);
				DiagCore.RetailAssert(false, "Should have thrown an EseutilParseErrorException", new object[0]);
			}
			catch (EseutilParseErrorException)
			{
			}
			DiagCore.RetailAssert(!File.Exists(text), "File {0} should have been deleted.", new object[]
			{
				text
			});
			text = EseHelper.CreateTemporaryFile(Path.GetTempPath());
			DiagCore.RetailAssert(File.Exists(text), "Temporary file {0} doesn't exist.", new object[]
			{
				text
			});
			using (StreamWriter streamWriter2 = new StreamWriter(text, false, Encoding.Unicode))
			{
				streamWriter2.Write(value2);
			}
			try
			{
				EseHelper.ParseLogfileDump(text);
				DiagCore.RetailAssert(false, "Should have thrown an EseutilParseErrorException", new object[0]);
			}
			catch (EseutilParseErrorException)
			{
			}
			DiagCore.RetailAssert(!File.Exists(text), "File {0} should have been deleted.", new object[]
			{
				text
			});
			try
			{
				EseHelper.GetLogRecords(Path.GetRandomFileName(), Path.GetTempPath());
				DiagCore.RetailAssert(false, "Should have thrown EsentErrorException.", new object[0]);
			}
			catch (EsentErrorException)
			{
			}
		}

		// Token: 0x040003E1 RID: 993
		private static bool g_fGlobalInitCalled = false;

		// Token: 0x040003E2 RID: 994
		private static object globalInitLock = new object();
	}
}
