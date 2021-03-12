using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IADDatabase : IADObjectCommon
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005E RID: 94
		ReplicationType ReplicationType { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005F RID: 95
		EdbFilePath EdbFilePath { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000060 RID: 96
		NonRootLocalLongFullPath LogFolderPath { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000061 RID: 97
		NonRootLocalLongFullPath SystemFolderPath { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000062 RID: 98
		bool Recovery { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000063 RID: 99
		bool AutoDagExcludeFromMonitoring { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000064 RID: 100
		ADObjectId HostServerForPreference1 { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000065 RID: 101
		IADDatabaseCopy[] DatabaseCopies { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000066 RID: 102
		IADDatabaseCopy[] AllDatabaseCopies { get; }

		// Token: 0x06000067 RID: 103
		IADDatabaseCopy GetDatabaseCopy(string serverShortName);

		// Token: 0x06000068 RID: 104
		void ExcludeDatabaseCopyFromProperties(string hostServerToExclude);

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000069 RID: 105
		ADObjectId Server { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006A RID: 106
		ADObjectId[] Servers { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006B RID: 107
		bool MountAtStartup { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006C RID: 108
		bool DatabaseCreated { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006D RID: 109
		bool AllowFileRestore { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006E RID: 110
		string DistinguishedName { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006F RID: 111
		string LogFilePrefix { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000070 RID: 112
		bool IsPublicFolderDatabase { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000071 RID: 113
		bool IsMailboxDatabase { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000072 RID: 114
		bool CircularLoggingEnabled { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000073 RID: 115
		string ExchangeLegacyDN { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000074 RID: 116
		string RpcClientAccessServerLegacyDN { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000075 RID: 117
		ADObjectId MailboxPublicFolderDatabase { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000076 RID: 118
		bool IsExchange2009OrLater { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000077 RID: 119
		ADObjectId MasterServerOrAvailabilityGroup { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000078 RID: 120
		string DatabaseGroup { get; }
	}
}
