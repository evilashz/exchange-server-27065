using System;
using System.Runtime.InteropServices;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000090 RID: 144
	internal static class NativeMethods
	{
		// Token: 0x06000744 RID: 1860
		[DllImport("wevtapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool EvtClearLog(IntPtr sessionHandle, string channelName, string targetPath, int flags);

		// Token: 0x06000745 RID: 1861
		[DllImport("ActiveMonitoringEventMsg.dll", CharSet = CharSet.Unicode)]
		internal static extern int WriteProbeResult(ref NativeMethods.ProbeResultUnmanaged result, ResultSeverityLevel severity);

		// Token: 0x06000746 RID: 1862
		[DllImport("ActiveMonitoringEventMsg.dll", CharSet = CharSet.Unicode)]
		internal static extern int WriteMonitorResult(ref NativeMethods.MonitorResultUnmanaged result, ResultSeverityLevel severity);

		// Token: 0x06000747 RID: 1863
		[DllImport("ActiveMonitoringEventMsg.dll", CharSet = CharSet.Unicode)]
		internal static extern int WriteResponderResult(ref NativeMethods.ResponderResultUnmanaged result, ResultSeverityLevel severity);

		// Token: 0x06000748 RID: 1864
		[DllImport("ActiveMonitoringEventMsg.dll", CharSet = CharSet.Unicode)]
		internal static extern int WriteMaintenanceResult(ref NativeMethods.MaintenanceResultUnmanaged result, ResultSeverityLevel severity);

		// Token: 0x06000749 RID: 1865
		[DllImport("ActiveMonitoringEventMsg.dll", CharSet = CharSet.Unicode)]
		internal static extern int WriteProbeDefinition(ref NativeMethods.ProbeDefinitionUnmanaged definition);

		// Token: 0x0600074A RID: 1866
		[DllImport("ActiveMonitoringEventMsg.dll", CharSet = CharSet.Unicode)]
		internal static extern int WriteMonitorDefinition(ref NativeMethods.MonitorDefinitionUnmanaged definition);

		// Token: 0x0600074B RID: 1867
		[DllImport("ActiveMonitoringEventMsg.dll", CharSet = CharSet.Unicode)]
		internal static extern int WriteResponderDefinition(ref NativeMethods.ResponderDefinitionUnmanaged definition);

		// Token: 0x0600074C RID: 1868
		[DllImport("ActiveMonitoringEventMsg.dll", CharSet = CharSet.Unicode)]
		internal static extern int WriteMaintenanceDefinition(ref NativeMethods.MaintenanceDefinitionUnmanaged definition);

		// Token: 0x0600074D RID: 1869
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern long GetTickCount64();

		// Token: 0x0400048C RID: 1164
		private const string ActiveMonitoringDll = "ActiveMonitoringEventMsg.dll";

		// Token: 0x0400048D RID: 1165
		private const string WindowsEventingApiDll = "wevtapi.dll";

		// Token: 0x02000091 RID: 145
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct ProbeResultUnmanaged
		{
			// Token: 0x0400048E RID: 1166
			internal int ResultId;

			// Token: 0x0400048F RID: 1167
			internal string ServiceName;

			// Token: 0x04000490 RID: 1168
			internal int IsNotified;

			// Token: 0x04000491 RID: 1169
			internal string ResultName;

			// Token: 0x04000492 RID: 1170
			internal int WorkItemId;

			// Token: 0x04000493 RID: 1171
			internal int DeploymentId;

			// Token: 0x04000494 RID: 1172
			internal string MachineName;

			// Token: 0x04000495 RID: 1173
			internal string Error;

			// Token: 0x04000496 RID: 1174
			internal string Exception;

			// Token: 0x04000497 RID: 1175
			internal byte RetryCount;

			// Token: 0x04000498 RID: 1176
			internal string StateAttribute1;

			// Token: 0x04000499 RID: 1177
			internal string StateAttribute2;

			// Token: 0x0400049A RID: 1178
			internal string StateAttribute3;

			// Token: 0x0400049B RID: 1179
			internal string StateAttribute4;

			// Token: 0x0400049C RID: 1180
			internal string StateAttribute5;

			// Token: 0x0400049D RID: 1181
			internal double StateAttribute6;

			// Token: 0x0400049E RID: 1182
			internal double StateAttribute7;

			// Token: 0x0400049F RID: 1183
			internal double StateAttribute8;

			// Token: 0x040004A0 RID: 1184
			internal double StateAttribute9;

			// Token: 0x040004A1 RID: 1185
			internal double StateAttribute10;

			// Token: 0x040004A2 RID: 1186
			internal string StateAttribute11;

			// Token: 0x040004A3 RID: 1187
			internal string StateAttribute12;

			// Token: 0x040004A4 RID: 1188
			internal string StateAttribute13;

			// Token: 0x040004A5 RID: 1189
			internal string StateAttribute14;

			// Token: 0x040004A6 RID: 1190
			internal string StateAttribute15;

			// Token: 0x040004A7 RID: 1191
			internal double StateAttribute16;

			// Token: 0x040004A8 RID: 1192
			internal double StateAttribute17;

			// Token: 0x040004A9 RID: 1193
			internal double StateAttribute18;

			// Token: 0x040004AA RID: 1194
			internal double StateAttribute19;

			// Token: 0x040004AB RID: 1195
			internal double StateAttribute20;

			// Token: 0x040004AC RID: 1196
			internal string StateAttribute21;

			// Token: 0x040004AD RID: 1197
			internal string StateAttribute22;

			// Token: 0x040004AE RID: 1198
			internal string StateAttribute23;

			// Token: 0x040004AF RID: 1199
			internal string StateAttribute24;

			// Token: 0x040004B0 RID: 1200
			internal string StateAttribute25;

			// Token: 0x040004B1 RID: 1201
			internal ResultType ResultType;

			// Token: 0x040004B2 RID: 1202
			internal int ExecutionId;

			// Token: 0x040004B3 RID: 1203
			internal string ExecutionStartTime;

			// Token: 0x040004B4 RID: 1204
			internal string ExecutionEndTime;

			// Token: 0x040004B5 RID: 1205
			internal byte PoisonedCount;

			// Token: 0x040004B6 RID: 1206
			internal string ExtensionXml;

			// Token: 0x040004B7 RID: 1207
			internal double SampleValue;

			// Token: 0x040004B8 RID: 1208
			internal string ExecutionContext;

			// Token: 0x040004B9 RID: 1209
			internal string FailureContext;

			// Token: 0x040004BA RID: 1210
			internal int FailureCategory;

			// Token: 0x040004BB RID: 1211
			internal string ScopeName;

			// Token: 0x040004BC RID: 1212
			internal string ScopeType;

			// Token: 0x040004BD RID: 1213
			internal string HealthSetName;

			// Token: 0x040004BE RID: 1214
			internal string Data;

			// Token: 0x040004BF RID: 1215
			internal int Version;
		}

		// Token: 0x02000092 RID: 146
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct MonitorResultUnmanaged
		{
			// Token: 0x040004C0 RID: 1216
			internal int ResultId;

			// Token: 0x040004C1 RID: 1217
			internal string ServiceName;

			// Token: 0x040004C2 RID: 1218
			internal int IsNotified;

			// Token: 0x040004C3 RID: 1219
			internal string ResultName;

			// Token: 0x040004C4 RID: 1220
			internal int WorkItemId;

			// Token: 0x040004C5 RID: 1221
			internal int DeploymentId;

			// Token: 0x040004C6 RID: 1222
			internal string MachineName;

			// Token: 0x040004C7 RID: 1223
			internal string Error;

			// Token: 0x040004C8 RID: 1224
			internal string Exception;

			// Token: 0x040004C9 RID: 1225
			internal byte RetryCount;

			// Token: 0x040004CA RID: 1226
			internal string StateAttribute1;

			// Token: 0x040004CB RID: 1227
			internal string StateAttribute2;

			// Token: 0x040004CC RID: 1228
			internal string StateAttribute3;

			// Token: 0x040004CD RID: 1229
			internal string StateAttribute4;

			// Token: 0x040004CE RID: 1230
			internal string StateAttribute5;

			// Token: 0x040004CF RID: 1231
			internal double StateAttribute6;

			// Token: 0x040004D0 RID: 1232
			internal double StateAttribute7;

			// Token: 0x040004D1 RID: 1233
			internal double StateAttribute8;

			// Token: 0x040004D2 RID: 1234
			internal double StateAttribute9;

			// Token: 0x040004D3 RID: 1235
			internal double StateAttribute10;

			// Token: 0x040004D4 RID: 1236
			internal ResultType ResultType;

			// Token: 0x040004D5 RID: 1237
			internal int ExecutionId;

			// Token: 0x040004D6 RID: 1238
			internal string ExecutionStartTime;

			// Token: 0x040004D7 RID: 1239
			internal string ExecutionEndTime;

			// Token: 0x040004D8 RID: 1240
			internal byte PoisonedCount;

			// Token: 0x040004D9 RID: 1241
			internal int IsAlert;

			// Token: 0x040004DA RID: 1242
			internal double TotalValue;

			// Token: 0x040004DB RID: 1243
			internal int TotalSampleCount;

			// Token: 0x040004DC RID: 1244
			internal int TotalFailedCount;

			// Token: 0x040004DD RID: 1245
			internal double NewValue;

			// Token: 0x040004DE RID: 1246
			internal int NewSampleCount;

			// Token: 0x040004DF RID: 1247
			internal int NewFailedCount;

			// Token: 0x040004E0 RID: 1248
			internal int LastFailedProbeId;

			// Token: 0x040004E1 RID: 1249
			internal int LastFailedProbeResultId;

			// Token: 0x040004E2 RID: 1250
			internal ServiceHealthStatus HealthState;

			// Token: 0x040004E3 RID: 1251
			internal int HealthStateTransitionId;

			// Token: 0x040004E4 RID: 1252
			internal string HealthStateChangedTime;

			// Token: 0x040004E5 RID: 1253
			internal string FirstAlertObservedTime;

			// Token: 0x040004E6 RID: 1254
			internal string FirstInsufficientSamplesObservedTime;

			// Token: 0x040004E7 RID: 1255
			internal string NewStateAttribute1Value;

			// Token: 0x040004E8 RID: 1256
			internal int NewStateAttribute1Count;

			// Token: 0x040004E9 RID: 1257
			internal double NewStateAttribute1Percent;

			// Token: 0x040004EA RID: 1258
			internal string TotalStateAttribute1Value;

			// Token: 0x040004EB RID: 1259
			internal int TotalStateAttribute1Count;

			// Token: 0x040004EC RID: 1260
			internal double TotalStateAttribute1Percent;

			// Token: 0x040004ED RID: 1261
			internal int NewFailureCategoryValue;

			// Token: 0x040004EE RID: 1262
			internal int NewFailureCategoryCount;

			// Token: 0x040004EF RID: 1263
			internal double NewFailureCategoryPercent;

			// Token: 0x040004F0 RID: 1264
			internal int TotalFailureCategoryValue;

			// Token: 0x040004F1 RID: 1265
			internal int TotalFailureCategoryCount;

			// Token: 0x040004F2 RID: 1266
			internal double TotalFailureCategoryPercent;

			// Token: 0x040004F3 RID: 1267
			internal string ComponentName;

			// Token: 0x040004F4 RID: 1268
			internal int IsHaImpacting;

			// Token: 0x040004F5 RID: 1269
			internal string SourceScope;

			// Token: 0x040004F6 RID: 1270
			internal string TargetScopes;

			// Token: 0x040004F7 RID: 1271
			internal string HaScope;

			// Token: 0x040004F8 RID: 1272
			internal int Version;
		}

		// Token: 0x02000093 RID: 147
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct ResponderResultUnmanaged
		{
			// Token: 0x040004F9 RID: 1273
			internal int ResultId;

			// Token: 0x040004FA RID: 1274
			internal string ServiceName;

			// Token: 0x040004FB RID: 1275
			internal int IsNotified;

			// Token: 0x040004FC RID: 1276
			internal string ResultName;

			// Token: 0x040004FD RID: 1277
			internal int WorkItemId;

			// Token: 0x040004FE RID: 1278
			internal int DeploymentId;

			// Token: 0x040004FF RID: 1279
			internal string MachineName;

			// Token: 0x04000500 RID: 1280
			internal string Error;

			// Token: 0x04000501 RID: 1281
			internal string Exception;

			// Token: 0x04000502 RID: 1282
			internal byte RetryCount;

			// Token: 0x04000503 RID: 1283
			internal string StateAttribute1;

			// Token: 0x04000504 RID: 1284
			internal string StateAttribute2;

			// Token: 0x04000505 RID: 1285
			internal string StateAttribute3;

			// Token: 0x04000506 RID: 1286
			internal string StateAttribute4;

			// Token: 0x04000507 RID: 1287
			internal string StateAttribute5;

			// Token: 0x04000508 RID: 1288
			internal double StateAttribute6;

			// Token: 0x04000509 RID: 1289
			internal double StateAttribute7;

			// Token: 0x0400050A RID: 1290
			internal double StateAttribute8;

			// Token: 0x0400050B RID: 1291
			internal double StateAttribute9;

			// Token: 0x0400050C RID: 1292
			internal double StateAttribute10;

			// Token: 0x0400050D RID: 1293
			internal ResultType ResultType;

			// Token: 0x0400050E RID: 1294
			internal int ExecutionId;

			// Token: 0x0400050F RID: 1295
			internal string ExecutionStartTime;

			// Token: 0x04000510 RID: 1296
			internal string ExecutionEndTime;

			// Token: 0x04000511 RID: 1297
			internal byte PoisonedCount;

			// Token: 0x04000512 RID: 1298
			internal int IsThrottled;

			// Token: 0x04000513 RID: 1299
			internal string ResponseResource;

			// Token: 0x04000514 RID: 1300
			internal string ResponseAction;

			// Token: 0x04000515 RID: 1301
			internal ServiceHealthStatus TargetHealthState;

			// Token: 0x04000516 RID: 1302
			internal int TargetHealthStateTransitionId;

			// Token: 0x04000517 RID: 1303
			internal string FirstAlertObservedTime;

			// Token: 0x04000518 RID: 1304
			internal ServiceRecoveryResult RecoveryResult;

			// Token: 0x04000519 RID: 1305
			internal int IsRecoveryAttempted;

			// Token: 0x0400051A RID: 1306
			internal string CorrelationResultsXml;

			// Token: 0x0400051B RID: 1307
			internal CorrelatedMonitorAction CorrelationAction;

			// Token: 0x0400051C RID: 1308
			internal int Version;
		}

		// Token: 0x02000094 RID: 148
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct MaintenanceResultUnmanaged
		{
			// Token: 0x0400051D RID: 1309
			internal int ResultId;

			// Token: 0x0400051E RID: 1310
			internal string ServiceName;

			// Token: 0x0400051F RID: 1311
			internal int IsNotified;

			// Token: 0x04000520 RID: 1312
			internal string ResultName;

			// Token: 0x04000521 RID: 1313
			internal int WorkItemId;

			// Token: 0x04000522 RID: 1314
			internal int DeploymentId;

			// Token: 0x04000523 RID: 1315
			internal string MachineName;

			// Token: 0x04000524 RID: 1316
			internal string Error;

			// Token: 0x04000525 RID: 1317
			internal string Exception;

			// Token: 0x04000526 RID: 1318
			internal byte RetryCount;

			// Token: 0x04000527 RID: 1319
			internal string StateAttribute1;

			// Token: 0x04000528 RID: 1320
			internal string StateAttribute2;

			// Token: 0x04000529 RID: 1321
			internal string StateAttribute3;

			// Token: 0x0400052A RID: 1322
			internal string StateAttribute4;

			// Token: 0x0400052B RID: 1323
			internal string StateAttribute5;

			// Token: 0x0400052C RID: 1324
			internal double StateAttribute6;

			// Token: 0x0400052D RID: 1325
			internal double StateAttribute7;

			// Token: 0x0400052E RID: 1326
			internal double StateAttribute8;

			// Token: 0x0400052F RID: 1327
			internal double StateAttribute9;

			// Token: 0x04000530 RID: 1328
			internal double StateAttribute10;

			// Token: 0x04000531 RID: 1329
			internal ResultType ResultType;

			// Token: 0x04000532 RID: 1330
			internal int ExecutionId;

			// Token: 0x04000533 RID: 1331
			internal string ExecutionStartTime;

			// Token: 0x04000534 RID: 1332
			internal string ExecutionEndTime;

			// Token: 0x04000535 RID: 1333
			internal byte PoisonedCount;

			// Token: 0x04000536 RID: 1334
			internal int Version;
		}

		// Token: 0x02000095 RID: 149
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct ProbeDefinitionUnmanaged
		{
			// Token: 0x04000537 RID: 1335
			internal int Id;

			// Token: 0x04000538 RID: 1336
			internal string AssemblyPath;

			// Token: 0x04000539 RID: 1337
			internal string TypeName;

			// Token: 0x0400053A RID: 1338
			internal string Name;

			// Token: 0x0400053B RID: 1339
			internal string WorkItemVersion;

			// Token: 0x0400053C RID: 1340
			internal string ServiceName;

			// Token: 0x0400053D RID: 1341
			internal int DeploymentId;

			// Token: 0x0400053E RID: 1342
			internal string ExecutionLocation;

			// Token: 0x0400053F RID: 1343
			internal string CreatedTime;

			// Token: 0x04000540 RID: 1344
			internal int Enabled;

			// Token: 0x04000541 RID: 1345
			internal string TargetPartition;

			// Token: 0x04000542 RID: 1346
			internal string TargetGroup;

			// Token: 0x04000543 RID: 1347
			internal string TargetResource;

			// Token: 0x04000544 RID: 1348
			internal string TargetExtension;

			// Token: 0x04000545 RID: 1349
			internal string TargetVersion;

			// Token: 0x04000546 RID: 1350
			internal int RecurrenceIntervalSeconds;

			// Token: 0x04000547 RID: 1351
			internal int TimeoutSeconds;

			// Token: 0x04000548 RID: 1352
			internal string StartTime;

			// Token: 0x04000549 RID: 1353
			internal string UpdateTime;

			// Token: 0x0400054A RID: 1354
			internal int MaxRetryAttempts;

			// Token: 0x0400054B RID: 1355
			internal string ExtensionAttributes;

			// Token: 0x0400054C RID: 1356
			internal int CreatedById;

			// Token: 0x0400054D RID: 1357
			internal string Account;

			// Token: 0x0400054E RID: 1358
			internal string AccountDisplayName;

			// Token: 0x0400054F RID: 1359
			internal string Endpoint;

			// Token: 0x04000550 RID: 1360
			internal string SecondaryAccount;

			// Token: 0x04000551 RID: 1361
			internal string SecondaryAccountDisplayName;

			// Token: 0x04000552 RID: 1362
			internal string SecondaryEndpoint;

			// Token: 0x04000553 RID: 1363
			internal string ExtensionEndpoints;

			// Token: 0x04000554 RID: 1364
			internal int Version;

			// Token: 0x04000555 RID: 1365
			internal int ExecutionType;
		}

		// Token: 0x02000096 RID: 150
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct MonitorDefinitionUnmanaged
		{
			// Token: 0x04000556 RID: 1366
			internal int Id;

			// Token: 0x04000557 RID: 1367
			internal string AssemblyPath;

			// Token: 0x04000558 RID: 1368
			internal string TypeName;

			// Token: 0x04000559 RID: 1369
			internal string Name;

			// Token: 0x0400055A RID: 1370
			internal string WorkItemVersion;

			// Token: 0x0400055B RID: 1371
			internal string ServiceName;

			// Token: 0x0400055C RID: 1372
			internal int DeploymentId;

			// Token: 0x0400055D RID: 1373
			internal string ExecutionLocation;

			// Token: 0x0400055E RID: 1374
			internal string CreatedTime;

			// Token: 0x0400055F RID: 1375
			internal int Enabled;

			// Token: 0x04000560 RID: 1376
			internal string TargetPartition;

			// Token: 0x04000561 RID: 1377
			internal string TargetGroup;

			// Token: 0x04000562 RID: 1378
			internal string TargetResource;

			// Token: 0x04000563 RID: 1379
			internal string TargetExtension;

			// Token: 0x04000564 RID: 1380
			internal string TargetVersion;

			// Token: 0x04000565 RID: 1381
			internal int RecurrenceIntervalSeconds;

			// Token: 0x04000566 RID: 1382
			internal int TimeoutSeconds;

			// Token: 0x04000567 RID: 1383
			internal string StartTime;

			// Token: 0x04000568 RID: 1384
			internal string UpdateTime;

			// Token: 0x04000569 RID: 1385
			internal int MaxRetryAttempts;

			// Token: 0x0400056A RID: 1386
			internal string ExtensionAttributes;

			// Token: 0x0400056B RID: 1387
			internal string SampleMask;

			// Token: 0x0400056C RID: 1388
			internal int MonitoringIntervalSeconds;

			// Token: 0x0400056D RID: 1389
			internal int MinimumErrorCount;

			// Token: 0x0400056E RID: 1390
			internal double MonitoringThreshold;

			// Token: 0x0400056F RID: 1391
			internal double SecondaryMonitoringThreshold;

			// Token: 0x04000570 RID: 1392
			internal double MonitoringSamplesThreshold;

			// Token: 0x04000571 RID: 1393
			internal int ServicePriority;

			// Token: 0x04000572 RID: 1394
			internal ServiceSeverity ServiceSeverity;

			// Token: 0x04000573 RID: 1395
			internal int IsHaImpacting;

			// Token: 0x04000574 RID: 1396
			internal int CreatedById;

			// Token: 0x04000575 RID: 1397
			internal int InsufficientSamplesIntervalSeconds;

			// Token: 0x04000576 RID: 1398
			internal string StateAttribute1Mask;

			// Token: 0x04000577 RID: 1399
			internal int FailureCategoryMask;

			// Token: 0x04000578 RID: 1400
			internal string ComponentName;

			// Token: 0x04000579 RID: 1401
			internal string StateTransitionsXml;

			// Token: 0x0400057A RID: 1402
			internal int AllowCorrelationToMonitor;

			// Token: 0x0400057B RID: 1403
			internal string ScenarioDescription;

			// Token: 0x0400057C RID: 1404
			internal string SourceScope;

			// Token: 0x0400057D RID: 1405
			internal string TargetScopes;

			// Token: 0x0400057E RID: 1406
			internal string HaScope;

			// Token: 0x0400057F RID: 1407
			internal int Version;
		}

		// Token: 0x02000097 RID: 151
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct ResponderDefinitionUnmanaged
		{
			// Token: 0x04000580 RID: 1408
			internal int Id;

			// Token: 0x04000581 RID: 1409
			internal string AssemblyPath;

			// Token: 0x04000582 RID: 1410
			internal string TypeName;

			// Token: 0x04000583 RID: 1411
			internal string Name;

			// Token: 0x04000584 RID: 1412
			internal string WorkItemVersion;

			// Token: 0x04000585 RID: 1413
			internal string ServiceName;

			// Token: 0x04000586 RID: 1414
			internal int DeploymentId;

			// Token: 0x04000587 RID: 1415
			internal string ExecutionLocation;

			// Token: 0x04000588 RID: 1416
			internal string CreatedTime;

			// Token: 0x04000589 RID: 1417
			internal int Enabled;

			// Token: 0x0400058A RID: 1418
			internal string TargetPartition;

			// Token: 0x0400058B RID: 1419
			internal string TargetGroup;

			// Token: 0x0400058C RID: 1420
			internal string TargetResource;

			// Token: 0x0400058D RID: 1421
			internal string TargetExtension;

			// Token: 0x0400058E RID: 1422
			internal string TargetVersion;

			// Token: 0x0400058F RID: 1423
			internal int RecurrenceIntervalSeconds;

			// Token: 0x04000590 RID: 1424
			internal int TimeoutSeconds;

			// Token: 0x04000591 RID: 1425
			internal string StartTime;

			// Token: 0x04000592 RID: 1426
			internal string UpdateTime;

			// Token: 0x04000593 RID: 1427
			internal int MaxRetryAttempts;

			// Token: 0x04000594 RID: 1428
			internal string ExtensionAttributes;

			// Token: 0x04000595 RID: 1429
			internal string AlertMask;

			// Token: 0x04000596 RID: 1430
			internal int WaitIntervalSeconds;

			// Token: 0x04000597 RID: 1431
			internal int MinimumSecondsBetweenEscalates;

			// Token: 0x04000598 RID: 1432
			internal string EscalationSubject;

			// Token: 0x04000599 RID: 1433
			internal string EscalationMessage;

			// Token: 0x0400059A RID: 1434
			internal string EscalationService;

			// Token: 0x0400059B RID: 1435
			internal string EscalationTeam;

			// Token: 0x0400059C RID: 1436
			internal NotificationServiceClass NotificationServiceClass;

			// Token: 0x0400059D RID: 1437
			internal string DailySchedulePattern;

			// Token: 0x0400059E RID: 1438
			internal int AlwaysEscalateOnMonitorChanges;

			// Token: 0x0400059F RID: 1439
			internal string Endpoint;

			// Token: 0x040005A0 RID: 1440
			internal int CreatedById;

			// Token: 0x040005A1 RID: 1441
			internal string Account;

			// Token: 0x040005A2 RID: 1442
			internal string AlertTypeId;

			// Token: 0x040005A3 RID: 1443
			internal ServiceHealthStatus TargetHealthState;

			// Token: 0x040005A4 RID: 1444
			internal string CorrelatedMonitorsXml;

			// Token: 0x040005A5 RID: 1445
			internal CorrelatedMonitorAction ActionOnCorrelatedMonitors;

			// Token: 0x040005A6 RID: 1446
			internal string ResponderCategory;

			// Token: 0x040005A7 RID: 1447
			internal string ThrottleGroupName;

			// Token: 0x040005A8 RID: 1448
			internal string ThrottlePolicyXml;

			// Token: 0x040005A9 RID: 1449
			internal int UploadScopeNotification;

			// Token: 0x040005AA RID: 1450
			internal int SuppressEscalation;

			// Token: 0x040005AB RID: 1451
			internal int Version;
		}

		// Token: 0x02000098 RID: 152
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct MaintenanceDefinitionUnmanaged
		{
			// Token: 0x040005AC RID: 1452
			internal int MaxRestartRequestAllowedPerHour;

			// Token: 0x040005AD RID: 1453
			internal int Id;

			// Token: 0x040005AE RID: 1454
			internal string AssemblyPath;

			// Token: 0x040005AF RID: 1455
			internal string TypeName;

			// Token: 0x040005B0 RID: 1456
			internal string Name;

			// Token: 0x040005B1 RID: 1457
			internal string WorkItemVersion;

			// Token: 0x040005B2 RID: 1458
			internal string ServiceName;

			// Token: 0x040005B3 RID: 1459
			internal int DeploymentId;

			// Token: 0x040005B4 RID: 1460
			internal string ExecutionLocation;

			// Token: 0x040005B5 RID: 1461
			internal string CreatedTime;

			// Token: 0x040005B6 RID: 1462
			internal int Enabled;

			// Token: 0x040005B7 RID: 1463
			internal string TargetPartition;

			// Token: 0x040005B8 RID: 1464
			internal string TargetGroup;

			// Token: 0x040005B9 RID: 1465
			internal string TargetResource;

			// Token: 0x040005BA RID: 1466
			internal string TargetExtension;

			// Token: 0x040005BB RID: 1467
			internal string TargetVersion;

			// Token: 0x040005BC RID: 1468
			internal int RecurrenceIntervalSeconds;

			// Token: 0x040005BD RID: 1469
			internal int TimeoutSeconds;

			// Token: 0x040005BE RID: 1470
			internal string StartTime;

			// Token: 0x040005BF RID: 1471
			internal string UpdateTime;

			// Token: 0x040005C0 RID: 1472
			internal int MaxRetryAttempts;

			// Token: 0x040005C1 RID: 1473
			internal string ExtensionAttributes;

			// Token: 0x040005C2 RID: 1474
			internal int CreatedById;

			// Token: 0x040005C3 RID: 1475
			internal int Version;
		}
	}
}
