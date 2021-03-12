using System;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004B0 RID: 1200
	internal static class MetabasePropertyTypes
	{
		// Token: 0x020004B1 RID: 1201
		public enum AppIsolated
		{
			// Token: 0x04001F3E RID: 7998
			InProc,
			// Token: 0x04001F3F RID: 7999
			OutProc,
			// Token: 0x04001F40 RID: 8000
			Pooled
		}

		// Token: 0x020004B2 RID: 1202
		public enum AppPoolIdentityType
		{
			// Token: 0x04001F42 RID: 8002
			LocalSystem,
			// Token: 0x04001F43 RID: 8003
			LocalService,
			// Token: 0x04001F44 RID: 8004
			NetworkService,
			// Token: 0x04001F45 RID: 8005
			SpecificUser
		}

		// Token: 0x020004B3 RID: 1203
		[Flags]
		public enum AuthFlags : uint
		{
			// Token: 0x04001F47 RID: 8007
			NoneSet = 0U,
			// Token: 0x04001F48 RID: 8008
			Anonymous = 1U,
			// Token: 0x04001F49 RID: 8009
			Basic = 2U,
			// Token: 0x04001F4A RID: 8010
			Ntlm = 4U,
			// Token: 0x04001F4B RID: 8011
			MD5 = 16U,
			// Token: 0x04001F4C RID: 8012
			Passport = 64U
		}

		// Token: 0x020004B4 RID: 1204
		[Flags]
		public enum DirBrowseFlags : uint
		{
			// Token: 0x04001F4E RID: 8014
			ShowDate = 2U,
			// Token: 0x04001F4F RID: 8015
			ShowTime = 4U,
			// Token: 0x04001F50 RID: 8016
			ShowSize = 8U,
			// Token: 0x04001F51 RID: 8017
			ShowExtension = 16U,
			// Token: 0x04001F52 RID: 8018
			ShowLongDate = 32U,
			// Token: 0x04001F53 RID: 8019
			DisableDirBrowsing = 64U,
			// Token: 0x04001F54 RID: 8020
			EnableDefaultDoc = 1073741824U,
			// Token: 0x04001F55 RID: 8021
			EnableDirBrowsing = 2147483648U
		}

		// Token: 0x020004B5 RID: 1205
		[Flags]
		public enum AccessFlags : uint
		{
			// Token: 0x04001F57 RID: 8023
			NoAccess = 0U,
			// Token: 0x04001F58 RID: 8024
			Read = 1U,
			// Token: 0x04001F59 RID: 8025
			Write = 2U,
			// Token: 0x04001F5A RID: 8026
			Execute = 4U,
			// Token: 0x04001F5B RID: 8027
			Source = 16U,
			// Token: 0x04001F5C RID: 8028
			Script = 512U,
			// Token: 0x04001F5D RID: 8029
			NoRemoteWrite = 1024U,
			// Token: 0x04001F5E RID: 8030
			NoRemoteRead = 4096U,
			// Token: 0x04001F5F RID: 8031
			NoRemoteExecute = 8192U,
			// Token: 0x04001F60 RID: 8032
			NoRemoteScript = 16384U,
			// Token: 0x04001F61 RID: 8033
			NoPhysicalDir = 32768U
		}

		// Token: 0x020004B6 RID: 1206
		[Flags]
		public enum AccessSSLFlags : uint
		{
			// Token: 0x04001F63 RID: 8035
			None = 0U,
			// Token: 0x04001F64 RID: 8036
			AccessSSL = 8U,
			// Token: 0x04001F65 RID: 8037
			AccessSSLNegotiateCert = 32U,
			// Token: 0x04001F66 RID: 8038
			AccessSSLRequireCert = 64U,
			// Token: 0x04001F67 RID: 8039
			AccessSSLMapCert = 128U,
			// Token: 0x04001F68 RID: 8040
			AccessSSL128 = 256U
		}

		// Token: 0x020004B7 RID: 1207
		public enum LogonMethod : uint
		{
			// Token: 0x04001F6A RID: 8042
			InteractiveLogon,
			// Token: 0x04001F6B RID: 8043
			BatchLogon,
			// Token: 0x04001F6C RID: 8044
			NetworkLogon,
			// Token: 0x04001F6D RID: 8045
			ClearTextLogon
		}

		// Token: 0x020004B8 RID: 1208
		[Flags]
		public enum FilterFlags : uint
		{
			// Token: 0x04001F6F RID: 8047
			NotifySecurePort = 1U,
			// Token: 0x04001F70 RID: 8048
			NotifyNonsecurePort = 2U,
			// Token: 0x04001F71 RID: 8049
			NotifyReadRawData = 32768U,
			// Token: 0x04001F72 RID: 8050
			NotifyPreprocHeaders = 16384U,
			// Token: 0x04001F73 RID: 8051
			NotifyAuthentication = 8192U,
			// Token: 0x04001F74 RID: 8052
			NotifyUrlMap = 4096U,
			// Token: 0x04001F75 RID: 8053
			NotifyAccessDenied = 2048U,
			// Token: 0x04001F76 RID: 8054
			NotifySendResponse = 64U,
			// Token: 0x04001F77 RID: 8055
			NotifySendRawData = 1024U,
			// Token: 0x04001F78 RID: 8056
			NotifyLog = 512U,
			// Token: 0x04001F79 RID: 8057
			NotifyEndOfRequest = 128U,
			// Token: 0x04001F7A RID: 8058
			NotifyEndOfNetSession = 256U,
			// Token: 0x04001F7B RID: 8059
			NotifyOrderHigh = 524288U,
			// Token: 0x04001F7C RID: 8060
			NotifyOrderMedium = 262144U,
			// Token: 0x04001F7D RID: 8061
			NotifyOrderLow = 131072U,
			// Token: 0x04001F7E RID: 8062
			NotifyOrderDefault = 131072U
		}

		// Token: 0x020004B9 RID: 1209
		public enum ManagedPipelineMode
		{
			// Token: 0x04001F80 RID: 8064
			Integrated,
			// Token: 0x04001F81 RID: 8065
			Classic
		}
	}
}
