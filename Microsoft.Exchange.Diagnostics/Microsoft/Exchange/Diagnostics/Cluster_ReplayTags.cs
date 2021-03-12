using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000278 RID: 632
	public struct Cluster_ReplayTags
	{
		// Token: 0x04001099 RID: 4249
		public const int ReplayApi = 0;

		// Token: 0x0400109A RID: 4250
		public const int EseutilWrapper = 1;

		// Token: 0x0400109B RID: 4251
		public const int State = 2;

		// Token: 0x0400109C RID: 4252
		public const int LogReplayer = 3;

		// Token: 0x0400109D RID: 4253
		public const int ReplicaInstance = 4;

		// Token: 0x0400109E RID: 4254
		public const int Cmdlets = 5;

		// Token: 0x0400109F RID: 4255
		public const int ShipLog = 6;

		// Token: 0x040010A0 RID: 4256
		public const int LogCopy = 7;

		// Token: 0x040010A1 RID: 4257
		public const int LogInspector = 8;

		// Token: 0x040010A2 RID: 4258
		public const int ReplayManager = 9;

		// Token: 0x040010A3 RID: 4259
		public const int CReplicaSeeder = 10;

		// Token: 0x040010A4 RID: 4260
		public const int NetShare = 11;

		// Token: 0x040010A5 RID: 4261
		public const int ReplicaVssWriterInterop = 12;

		// Token: 0x040010A6 RID: 4262
		public const int StateLock = 13;

		// Token: 0x040010A7 RID: 4263
		public const int FileChecker = 14;

		// Token: 0x040010A8 RID: 4264
		public const int Cluster = 15;

		// Token: 0x040010A9 RID: 4265
		public const int SeederWrapper = 16;

		// Token: 0x040010AA RID: 4266
		public const int PFD = 17;

		// Token: 0x040010AB RID: 4267
		public const int IncrementalReseeder = 18;

		// Token: 0x040010AC RID: 4268
		public const int Dumpster = 19;

		// Token: 0x040010AD RID: 4269
		public const int CLogShipContext = 20;

		// Token: 0x040010AE RID: 4270
		public const int ClusDBWrite = 21;

		// Token: 0x040010AF RID: 4271
		public const int ReplayConfiguration = 22;

		// Token: 0x040010B0 RID: 4272
		public const int NetPath = 23;

		// Token: 0x040010B1 RID: 4273
		public const int HealthChecks = 24;

		// Token: 0x040010B2 RID: 4274
		public const int ReplayServiceRpc = 25;

		// Token: 0x040010B3 RID: 4275
		public const int ActiveManager = 26;

		// Token: 0x040010B4 RID: 4276
		public const int SeederServer = 27;

		// Token: 0x040010B5 RID: 4277
		public const int SeederClient = 28;

		// Token: 0x040010B6 RID: 4278
		public const int LogTruncater = 29;

		// Token: 0x040010B7 RID: 4279
		public const int FailureItem = 30;

		// Token: 0x040010B8 RID: 4280
		public const int LogCopyServer = 31;

		// Token: 0x040010B9 RID: 4281
		public const int LogCopyClient = 32;

		// Token: 0x040010BA RID: 4282
		public const int TcpChannel = 33;

		// Token: 0x040010BB RID: 4283
		public const int TcpClient = 34;

		// Token: 0x040010BC RID: 4284
		public const int TcpServer = 35;

		// Token: 0x040010BD RID: 4285
		public const int RemoteDataProvider = 36;

		// Token: 0x040010BE RID: 4286
		public const int MonitoredDatabase = 37;

		// Token: 0x040010BF RID: 4287
		public const int NetworkManager = 38;

		// Token: 0x040010C0 RID: 4288
		public const int NetworkChannel = 39;

		// Token: 0x040010C1 RID: 4289
		public const int FaultInjection = 40;

		// Token: 0x040010C2 RID: 4290
		public const int GranularWriter = 41;

		// Token: 0x040010C3 RID: 4291
		public const int GranularReader = 42;

		// Token: 0x040010C4 RID: 4292
		public const int ThirdPartyClient = 43;

		// Token: 0x040010C5 RID: 4293
		public const int ThirdPartyManager = 44;

		// Token: 0x040010C6 RID: 4294
		public const int ThirdPartyService = 45;

		// Token: 0x040010C7 RID: 4295
		public const int ClusterEvents = 46;

		// Token: 0x040010C8 RID: 4296
		public const int AmNetworkMonitor = 47;

		// Token: 0x040010C9 RID: 4297
		public const int AmConfigManager = 48;

		// Token: 0x040010CA RID: 4298
		public const int AmSystemManager = 49;

		// Token: 0x040010CB RID: 4299
		public const int AmServiceMonitor = 50;

		// Token: 0x040010CC RID: 4300
		public const int ServiceOperations = 51;

		// Token: 0x040010CD RID: 4301
		public const int AmServerNameCache = 53;

		// Token: 0x040010CE RID: 4302
		public const int KernelWatchdogTimer = 54;

		// Token: 0x040010CF RID: 4303
		public const int FailureItemHealthMonitor = 55;

		// Token: 0x040010D0 RID: 4304
		public const int ReplayServiceDiagnostics = 56;

		// Token: 0x040010D1 RID: 4305
		public const int LogRepair = 57;

		// Token: 0x040010D2 RID: 4306
		public const int PassiveBlockMode = 58;

		// Token: 0x040010D3 RID: 4307
		public const int LogCopier = 59;

		// Token: 0x040010D4 RID: 4308
		public const int DiskHeartbeat = 60;

		// Token: 0x040010D5 RID: 4309
		public const int Monitoring = 61;

		// Token: 0x040010D6 RID: 4310
		public const int ServerLocatorService = 62;

		// Token: 0x040010D7 RID: 4311
		public const int ServerLocatorServiceClient = 63;

		// Token: 0x040010D8 RID: 4312
		public const int LatencyChecker = 64;

		// Token: 0x040010D9 RID: 4313
		public const int VolumeManager = 65;

		// Token: 0x040010DA RID: 4314
		public const int AutoReseed = 66;

		// Token: 0x040010DB RID: 4315
		public const int DiskReclaimer = 67;

		// Token: 0x040010DC RID: 4316
		public const int ADCache = 68;

		// Token: 0x040010DD RID: 4317
		public const int DbTracker = 69;

		// Token: 0x040010DE RID: 4318
		public const int DatabaseCopyLayout = 70;

		// Token: 0x040010DF RID: 4319
		public const int CompositeKey = 71;

		// Token: 0x040010E0 RID: 4320
		public const int ClusdbKey = 72;

		// Token: 0x040010E1 RID: 4321
		public const int DxStoreKey = 73;

		// Token: 0x040010E2 RID: 4322
		public static Guid guid = new Guid("404a3308-17e1-4ac3-9167-1b09c36850bd");
	}
}
