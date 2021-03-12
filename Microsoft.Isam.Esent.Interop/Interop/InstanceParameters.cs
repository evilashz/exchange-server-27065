using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Isam.Esent.Interop.Unpublished;
using Microsoft.Isam.Esent.Interop.Windows8;
using Microsoft.Isam.Esent.Interop.Windows81;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000083 RID: 131
	public class InstanceParameters
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0000C5BF File Offset: 0x0000A7BF
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0000C5C9 File Offset: 0x0000A7C9
		public JET_INDEXCHECKING EnableIndexCheckingEx
		{
			get
			{
				return (JET_INDEXCHECKING)this.GetIntegerParameter(JET_param.EnableIndexChecking);
			}
			set
			{
				this.SetIntegerParameter(JET_param.EnableIndexChecking, (int)value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000C5D4 File Offset: 0x0000A7D4
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x0000C5E1 File Offset: 0x0000A7E1
		public bool EnableExternalAutoHealing
		{
			get
			{
				return this.GetBoolParameter((JET_param)175);
			}
			set
			{
				this.SetBoolParameter((JET_param)175, value);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000C5EF File Offset: 0x0000A7EF
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x0000C5FC File Offset: 0x0000A7FC
		public bool AggressiveLogRollover
		{
			get
			{
				return this.GetBoolParameter((JET_param)157);
			}
			set
			{
				this.SetBoolParameter((JET_param)157, value);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0000C60A File Offset: 0x0000A80A
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x0000C617 File Offset: 0x0000A817
		public bool EnableHaPublish
		{
			get
			{
				return this.GetBoolParameter((JET_param)168);
			}
			set
			{
				this.SetBoolParameter((JET_param)168, value);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0000C625 File Offset: 0x0000A825
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x0000C632 File Offset: 0x0000A832
		public int CheckpointTooDeep
		{
			get
			{
				return this.GetIntegerParameter((JET_param)154);
			}
			set
			{
				this.SetIntegerParameter((JET_param)154, value);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0000C640 File Offset: 0x0000A840
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0000C64D File Offset: 0x0000A84D
		public int AutomaticShrinkDatabaseFreeSpaceThreshold
		{
			get
			{
				return this.GetIntegerParameter((JET_param)185);
			}
			set
			{
				this.SetIntegerParameter((JET_param)185, value);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0000C65B File Offset: 0x0000A85B
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x0000C668 File Offset: 0x0000A868
		public bool ZeroDatabaseUnusedSpace
		{
			get
			{
				return this.GetBoolParameter((JET_param)191);
			}
			set
			{
				this.SetBoolParameter((JET_param)191, value);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0000C676 File Offset: 0x0000A876
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x0000C683 File Offset: 0x0000A883
		internal IntPtr EmitLogDataCallbackCtx
		{
			get
			{
				return this.GetIntPtrParameter((JET_param)174);
			}
			set
			{
				this.SetIntPtrParameter((JET_param)174, value);
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000C694 File Offset: 0x0000A894
		internal NATIVE_JET_PFNEMITLOGDATA GetEmitLogDataCallback()
		{
			NATIVE_JET_PFNEMITLOGDATA result = null;
			IntPtr intPtrParameter = this.GetIntPtrParameter((JET_param)173);
			if (intPtrParameter != IntPtr.Zero)
			{
				result = (NATIVE_JET_PFNEMITLOGDATA)Marshal.GetDelegateForFunctionPointer(intPtrParameter, typeof(NATIVE_JET_PFNEMITLOGDATA));
			}
			return result;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		internal void SetEmitLogDataCallback(NATIVE_JET_PFNEMITLOGDATA callback)
		{
			IntPtr value;
			if (callback != null)
			{
				value = Marshal.GetFunctionPointerForDelegate(callback);
			}
			else
			{
				value = IntPtr.Zero;
			}
			this.SetIntPtrParameter((JET_param)173, value);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000C6FF File Offset: 0x0000A8FF
		public InstanceParameters(JET_INSTANCE instance)
		{
			this.instance = instance;
			this.sesid = JET_SESID.Nil;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000C719 File Offset: 0x0000A919
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0000C727 File Offset: 0x0000A927
		public string SystemDirectory
		{
			get
			{
				return Util.AddTrailingDirectorySeparator(this.GetStringParameter(JET_param.SystemPath));
			}
			set
			{
				this.SetStringParameter(JET_param.SystemPath, Util.AddTrailingDirectorySeparator(value));
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000C738 File Offset: 0x0000A938
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0000C75A File Offset: 0x0000A95A
		public string TempDirectory
		{
			get
			{
				string stringParameter = this.GetStringParameter(JET_param.TempPath);
				string directoryName = Path.GetDirectoryName(stringParameter);
				return Util.AddTrailingDirectorySeparator(directoryName);
			}
			set
			{
				this.SetStringParameter(JET_param.TempPath, Util.AddTrailingDirectorySeparator(value));
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000C769 File Offset: 0x0000A969
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x0000C777 File Offset: 0x0000A977
		public string LogFileDirectory
		{
			get
			{
				return Util.AddTrailingDirectorySeparator(this.GetStringParameter(JET_param.LogFilePath));
			}
			set
			{
				this.SetStringParameter(JET_param.LogFilePath, Util.AddTrailingDirectorySeparator(value));
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0000C786 File Offset: 0x0000A986
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0000C79E File Offset: 0x0000A99E
		public string AlternateDatabaseRecoveryDirectory
		{
			get
			{
				if (EsentVersion.SupportsServer2003Features)
				{
					return Util.AddTrailingDirectorySeparator(this.GetStringParameter((JET_param)113));
				}
				return null;
			}
			set
			{
				if (EsentVersion.SupportsServer2003Features)
				{
					this.SetStringParameter((JET_param)113, Util.AddTrailingDirectorySeparator(value));
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0000C7B5 File Offset: 0x0000A9B5
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0000C7BE File Offset: 0x0000A9BE
		public string BaseName
		{
			get
			{
				return this.GetStringParameter(JET_param.BaseName);
			}
			set
			{
				this.SetStringParameter(JET_param.BaseName, value);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x0000C7D1 File Offset: 0x0000A9D1
		public string EventSource
		{
			get
			{
				return this.GetStringParameter(JET_param.EventSource);
			}
			set
			{
				this.SetStringParameter(JET_param.EventSource, value);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000C7DB File Offset: 0x0000A9DB
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
		public int MaxSessions
		{
			get
			{
				return this.GetIntegerParameter(JET_param.MaxSessions);
			}
			set
			{
				this.SetIntegerParameter(JET_param.MaxSessions, value);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000C7EE File Offset: 0x0000A9EE
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0000C7F7 File Offset: 0x0000A9F7
		public int MaxOpenTables
		{
			get
			{
				return this.GetIntegerParameter(JET_param.MaxOpenTables);
			}
			set
			{
				this.SetIntegerParameter(JET_param.MaxOpenTables, value);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000C801 File Offset: 0x0000AA01
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0000C80A File Offset: 0x0000AA0A
		public int MaxCursors
		{
			get
			{
				return this.GetIntegerParameter(JET_param.MaxCursors);
			}
			set
			{
				this.SetIntegerParameter(JET_param.MaxCursors, value);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000C814 File Offset: 0x0000AA14
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0000C81E File Offset: 0x0000AA1E
		public int MaxVerPages
		{
			get
			{
				return this.GetIntegerParameter(JET_param.MaxVerPages);
			}
			set
			{
				this.SetIntegerParameter(JET_param.MaxVerPages, value);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000C829 File Offset: 0x0000AA29
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x0000C833 File Offset: 0x0000AA33
		public int PreferredVerPages
		{
			get
			{
				return this.GetIntegerParameter(JET_param.PreferredVerPages);
			}
			set
			{
				this.SetIntegerParameter(JET_param.PreferredVerPages, value);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0000C83E File Offset: 0x0000AA3E
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0000C848 File Offset: 0x0000AA48
		public int VersionStoreTaskQueueMax
		{
			get
			{
				return this.GetIntegerParameter(JET_param.VersionStoreTaskQueueMax);
			}
			set
			{
				this.SetIntegerParameter(JET_param.VersionStoreTaskQueueMax, value);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000C853 File Offset: 0x0000AA53
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0000C85D File Offset: 0x0000AA5D
		public int MaxTemporaryTables
		{
			get
			{
				return this.GetIntegerParameter(JET_param.MaxTemporaryTables);
			}
			set
			{
				this.SetIntegerParameter(JET_param.MaxTemporaryTables, value);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0000C868 File Offset: 0x0000AA68
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0000C872 File Offset: 0x0000AA72
		public int LogFileSize
		{
			get
			{
				return this.GetIntegerParameter(JET_param.LogFileSize);
			}
			set
			{
				this.SetIntegerParameter(JET_param.LogFileSize, value);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0000C87D File Offset: 0x0000AA7D
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0000C887 File Offset: 0x0000AA87
		public int LogBuffers
		{
			get
			{
				return this.GetIntegerParameter(JET_param.LogBuffers);
			}
			set
			{
				this.SetIntegerParameter(JET_param.LogBuffers, value);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000C892 File Offset: 0x0000AA92
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0000C89C File Offset: 0x0000AA9C
		public bool CircularLog
		{
			get
			{
				return this.GetBoolParameter(JET_param.CircularLog);
			}
			set
			{
				this.SetBoolParameter(JET_param.CircularLog, value);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0000C8A7 File Offset: 0x0000AAA7
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x0000C8B1 File Offset: 0x0000AAB1
		public bool CleanupMismatchedLogFiles
		{
			get
			{
				return this.GetBoolParameter(JET_param.CleanupMismatchedLogFiles);
			}
			set
			{
				this.SetBoolParameter(JET_param.CleanupMismatchedLogFiles, value);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0000C8BC File Offset: 0x0000AABC
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0000C8C6 File Offset: 0x0000AAC6
		public int PageTempDBMin
		{
			get
			{
				return this.GetIntegerParameter(JET_param.PageTempDBMin);
			}
			set
			{
				this.SetIntegerParameter(JET_param.PageTempDBMin, value);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000C8D1 File Offset: 0x0000AAD1
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x0000C8DB File Offset: 0x0000AADB
		public int CheckpointDepthMax
		{
			get
			{
				return this.GetIntegerParameter(JET_param.CheckpointDepthMax);
			}
			set
			{
				this.SetIntegerParameter(JET_param.CheckpointDepthMax, value);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000C8E6 File Offset: 0x0000AAE6
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0000C8F0 File Offset: 0x0000AAF0
		public int DbExtensionSize
		{
			get
			{
				return this.GetIntegerParameter(JET_param.DbExtensionSize);
			}
			set
			{
				this.SetIntegerParameter(JET_param.DbExtensionSize, value);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000C8FB File Offset: 0x0000AAFB
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0000C913 File Offset: 0x0000AB13
		public bool Recovery
		{
			get
			{
				return 0 == string.Compare(this.GetStringParameter(JET_param.Recovery), "on", StringComparison.OrdinalIgnoreCase);
			}
			set
			{
				if (value)
				{
					this.SetStringParameter(JET_param.Recovery, "on");
					return;
				}
				this.SetStringParameter(JET_param.Recovery, "off");
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0000C933 File Offset: 0x0000AB33
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0000C93D File Offset: 0x0000AB3D
		public bool EnableOnlineDefrag
		{
			get
			{
				return this.GetBoolParameter(JET_param.EnableOnlineDefrag);
			}
			set
			{
				this.SetBoolParameter(JET_param.EnableOnlineDefrag, value);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000C948 File Offset: 0x0000AB48
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0000C952 File Offset: 0x0000AB52
		public bool EnableIndexChecking
		{
			get
			{
				return this.GetBoolParameter(JET_param.EnableIndexChecking);
			}
			set
			{
				this.SetBoolParameter(JET_param.EnableIndexChecking, value);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000C95D File Offset: 0x0000AB5D
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0000C967 File Offset: 0x0000AB67
		public string EventSourceKey
		{
			get
			{
				return this.GetStringParameter(JET_param.EventSourceKey);
			}
			set
			{
				this.SetStringParameter(JET_param.EventSourceKey, value);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0000C972 File Offset: 0x0000AB72
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0000C97C File Offset: 0x0000AB7C
		public bool NoInformationEvent
		{
			get
			{
				return this.GetBoolParameter(JET_param.NoInformationEvent);
			}
			set
			{
				this.SetBoolParameter(JET_param.NoInformationEvent, value);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000C987 File Offset: 0x0000AB87
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0000C991 File Offset: 0x0000AB91
		public EventLoggingLevels EventLoggingLevel
		{
			get
			{
				return (EventLoggingLevels)this.GetIntegerParameter(JET_param.EventLoggingLevel);
			}
			set
			{
				this.SetIntegerParameter(JET_param.EventLoggingLevel, (int)value);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0000C99C File Offset: 0x0000AB9C
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0000C9A6 File Offset: 0x0000ABA6
		public bool OneDatabasePerSession
		{
			get
			{
				return this.GetBoolParameter(JET_param.OneDatabasePerSession);
			}
			set
			{
				this.SetBoolParameter(JET_param.OneDatabasePerSession, value);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000C9B1 File Offset: 0x0000ABB1
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0000C9BB File Offset: 0x0000ABBB
		public bool CreatePathIfNotExist
		{
			get
			{
				return this.GetBoolParameter(JET_param.CreatePathIfNotExist);
			}
			set
			{
				this.SetBoolParameter(JET_param.CreatePathIfNotExist, value);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000C9C6 File Offset: 0x0000ABC6
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0000C9D9 File Offset: 0x0000ABD9
		public int CachedClosedTables
		{
			get
			{
				if (EsentVersion.SupportsVistaFeatures)
				{
					return this.GetIntegerParameter((JET_param)125);
				}
				return 0;
			}
			set
			{
				if (EsentVersion.SupportsVistaFeatures)
				{
					this.SetIntegerParameter((JET_param)125, value);
				}
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000C9EB File Offset: 0x0000ABEB
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0000CA01 File Offset: 0x0000AC01
		public int WaypointLatency
		{
			get
			{
				if (EsentVersion.SupportsWindows7Features)
				{
					return this.GetIntegerParameter((JET_param)153);
				}
				return 0;
			}
			set
			{
				if (EsentVersion.SupportsWindows7Features)
				{
					this.SetIntegerParameter((JET_param)153, value);
				}
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000CA18 File Offset: 0x0000AC18
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "InstanceParameters (0x{0:x})", new object[]
			{
				this.instance.Value
			});
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000CA4F File Offset: 0x0000AC4F
		private void SetStringParameter(JET_param param, string value)
		{
			Api.JetSetSystemParameter(this.instance, this.sesid, param, 0, value);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000CA68 File Offset: 0x0000AC68
		private string GetStringParameter(JET_param param)
		{
			int num = 0;
			string result;
			Api.JetGetSystemParameter(this.instance, this.sesid, param, ref num, out result, 1024);
			return result;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000CA94 File Offset: 0x0000AC94
		private void SetIntegerParameter(JET_param param, int value)
		{
			Api.JetSetSystemParameter(this.instance, this.sesid, param, value, null);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000CAAC File Offset: 0x0000ACAC
		private int GetIntegerParameter(JET_param param)
		{
			int result = 0;
			string text;
			Api.JetGetSystemParameter(this.instance, this.sesid, param, ref result, out text, 0);
			return result;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
		private void SetBoolParameter(JET_param param, bool value)
		{
			if (value)
			{
				Api.JetSetSystemParameter(this.instance, this.sesid, param, 1, null);
				return;
			}
			Api.JetSetSystemParameter(this.instance, this.sesid, param, 0, null);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000CB04 File Offset: 0x0000AD04
		private bool GetBoolParameter(JET_param param)
		{
			int num = 0;
			string text;
			Api.JetGetSystemParameter(this.instance, this.sesid, param, ref num, out text, 0);
			return num != 0;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000CB32 File Offset: 0x0000AD32
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x0000CB3F File Offset: 0x0000AD3F
		public int MaxTransactionSize
		{
			get
			{
				return this.GetIntegerParameter((JET_param)178);
			}
			set
			{
				this.SetIntegerParameter((JET_param)178, value);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000CB4D File Offset: 0x0000AD4D
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x0000CB5A File Offset: 0x0000AD5A
		public int EnableDbScanInRecovery
		{
			get
			{
				return this.GetIntegerParameter((JET_param)169);
			}
			set
			{
				this.SetIntegerParameter((JET_param)169, value);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0000CB68 File Offset: 0x0000AD68
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x0000CB75 File Offset: 0x0000AD75
		public bool EnableDBScanSerialization
		{
			get
			{
				return this.GetBoolParameter((JET_param)180);
			}
			set
			{
				this.SetBoolParameter((JET_param)180, value);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000CB83 File Offset: 0x0000AD83
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x0000CB90 File Offset: 0x0000AD90
		public int DbScanThrottle
		{
			get
			{
				return this.GetIntegerParameter((JET_param)170);
			}
			set
			{
				this.SetIntegerParameter((JET_param)170, value);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000CB9E File Offset: 0x0000AD9E
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x0000CBAB File Offset: 0x0000ADAB
		public int DbScanIntervalMinSec
		{
			get
			{
				return this.GetIntegerParameter((JET_param)171);
			}
			set
			{
				this.SetIntegerParameter((JET_param)171, value);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0000CBB9 File Offset: 0x0000ADB9
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x0000CBC6 File Offset: 0x0000ADC6
		public int DbScanIntervalMaxSec
		{
			get
			{
				return this.GetIntegerParameter((JET_param)172);
			}
			set
			{
				this.SetIntegerParameter((JET_param)172, value);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000CBD4 File Offset: 0x0000ADD4
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x0000CBE1 File Offset: 0x0000ADE1
		public int CachePriority
		{
			get
			{
				return this.GetIntegerParameter((JET_param)177);
			}
			set
			{
				this.SetIntegerParameter((JET_param)177, value);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000CBEF File Offset: 0x0000ADEF
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0000CBFC File Offset: 0x0000ADFC
		public int PrereadIOMax
		{
			get
			{
				return this.GetIntegerParameter((JET_param)179);
			}
			set
			{
				this.SetIntegerParameter((JET_param)179, value);
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000CC0C File Offset: 0x0000AE0C
		internal NATIVE_JET_PFNDURABLECOMMITCALLBACK GetDurableCommitCallback()
		{
			NATIVE_JET_PFNDURABLECOMMITCALLBACK result = null;
			IntPtr intPtrParameter = this.GetIntPtrParameter((JET_param)187);
			if (intPtrParameter != IntPtr.Zero)
			{
				result = (NATIVE_JET_PFNDURABLECOMMITCALLBACK)Marshal.GetDelegateForFunctionPointer(intPtrParameter, typeof(NATIVE_JET_PFNDURABLECOMMITCALLBACK));
			}
			return result;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000CC4C File Offset: 0x0000AE4C
		internal void SetDurableCommitCallback(NATIVE_JET_PFNDURABLECOMMITCALLBACK callback)
		{
			IntPtr value;
			if (callback != null)
			{
				value = Marshal.GetFunctionPointerForDelegate(callback);
			}
			else
			{
				value = IntPtr.Zero;
			}
			this.SetIntPtrParameter((JET_param)187, value);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000CC78 File Offset: 0x0000AE78
		private IntPtr GetIntPtrParameter(JET_param param)
		{
			IntPtr zero = IntPtr.Zero;
			string text;
			Api.JetGetSystemParameter(this.instance, this.sesid, param, ref zero, out text, 0);
			return zero;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
		private void SetIntPtrParameter(JET_param param, IntPtr value)
		{
			Api.JetSetSystemParameter(this.instance, this.sesid, param, value, null);
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000CCBB File Offset: 0x0000AEBB
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x0000CCC8 File Offset: 0x0000AEC8
		public ShrinkDatabaseGrbit EnableShrinkDatabase
		{
			get
			{
				return (ShrinkDatabaseGrbit)this.GetIntegerParameter((JET_param)184);
			}
			set
			{
				this.SetIntegerParameter((JET_param)184, (int)value);
			}
		}

		// Token: 0x040002C1 RID: 705
		private readonly JET_INSTANCE instance;

		// Token: 0x040002C2 RID: 706
		private readonly JET_SESID sesid;
	}
}
