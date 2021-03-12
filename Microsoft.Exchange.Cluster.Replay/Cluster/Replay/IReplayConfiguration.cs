using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200013D RID: 317
	internal interface IReplayConfiguration
	{
		// Token: 0x06000BD8 RID: 3032
		bool ConfigEquals(IReplayConfiguration other, out ReplayConfigChangedFlags changedFlags);

		// Token: 0x06000BD9 RID: 3033
		bool IsSourceMachineEqual(AmServerName sourceServer);

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000BDA RID: 3034
		bool AllowFileRestore { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000BDB RID: 3035
		ReplayConfigType Type { get; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000BDC RID: 3036
		bool IsPassiveCopy { get; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000BDD RID: 3037
		IADDatabase Database { get; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000BDE RID: 3038
		string Name { get; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000BDF RID: 3039
		string DisplayName { get; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000BE0 RID: 3040
		string Identity { get; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000BE1 RID: 3041
		string DatabaseDn { get; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000BE2 RID: 3042
		Guid IdentityGuid { get; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000BE3 RID: 3043
		Guid DatabaseGuid { get; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000BE4 RID: 3044
		string LogFilePrefix { get; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000BE5 RID: 3045
		string LogFileSuffix { get; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000BE6 RID: 3046
		string LogExtension { get; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000BE7 RID: 3047
		string LogInspectorPath { get; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000BE8 RID: 3048
		string E00LogBackupPath { get; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000BE9 RID: 3049
		string DatabaseName { get; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000BEA RID: 3050
		bool IsPublicFolderDatabase { get; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000BEB RID: 3051
		bool DatabaseIsPrivate { get; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000BEC RID: 3052
		bool CircularLoggingEnabled { get; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000BED RID: 3053
		string ServerName { get; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000BEE RID: 3054
		int ServerVersion { get; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000BEF RID: 3055
		ReplayState ReplayState { get; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000BF0 RID: 3056
		string SourceMachine { get; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000BF1 RID: 3057
		string TargetMachine { get; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000BF2 RID: 3058
		EnhancedTimeSpan ReplayLagTime { get; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000BF3 RID: 3059
		EnhancedTimeSpan TruncationLagTime { get; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000BF4 RID: 3060
		bool DatabaseCreated { get; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000BF5 RID: 3061
		string DestinationEdbPath { get; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000BF6 RID: 3062
		string DestinationSystemPath { get; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000BF7 RID: 3063
		string DestinationLogPath { get; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000BF8 RID: 3064
		string SourceEdbPath { get; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000BF9 RID: 3065
		string SourceSystemPath { get; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000BFA RID: 3066
		string SourceLogPath { get; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000BFB RID: 3067
		string AutoDagVolumesRootFolderPath { get; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000BFC RID: 3068
		string AutoDagDatabasesRootFolderPath { get; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000BFD RID: 3069
		int AutoDagDatabaseCopiesPerVolume { get; }

		// Token: 0x06000BFE RID: 3070
		string GetXmlDescription(JET_SIGNATURE logfileSignature);

		// Token: 0x06000BFF RID: 3071
		IADServer GetAdServerObject();

		// Token: 0x06000C00 RID: 3072
		string BuildShortLogfileName(long generation);

		// Token: 0x06000C01 RID: 3073
		string BuildFullLogfileName(long generation);

		// Token: 0x06000C02 RID: 3074
		void UpdateLastLogGeneratedAndEndOfLogInfo(long highestLogGen);
	}
}
