using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IADServer : IADObjectCommon
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007F RID: 127
		string Fqdn { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000080 RID: 128
		bool IsE14OrLater { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000081 RID: 129
		ServerVersion AdminDisplayVersion { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000082 RID: 130
		ServerRole CurrentServerRole { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000083 RID: 131
		ADObjectId ServerSite { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000084 RID: 132
		ADObjectId DatabaseAvailabilityGroup { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000085 RID: 133
		DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000086 RID: 134
		bool DatabaseCopyActivationDisabledAndMoveNow { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000087 RID: 135
		bool AutoDagServerConfigured { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000088 RID: 136
		bool IsMailboxServer { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000089 RID: 137
		ServerEditionType Edition { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600008A RID: 138
		int VersionNumber { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600008B RID: 139
		int? MaximumActiveDatabases { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600008C RID: 140
		int? MaximumPreferredActiveDatabases { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600008D RID: 141
		AutoDatabaseMountDial AutoDatabaseMountDial { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600008E RID: 142
		long? ContinuousReplicationMaxMemoryPerDatabase { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600008F RID: 143
		int MajorVersion { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000090 RID: 144
		bool IsExchange2007OrLater { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000091 RID: 145
		string ExchangeLegacyDN { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000092 RID: 146
		MailboxRelease MailboxRelease { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000093 RID: 147
		MultiValuedProperty<string> ComponentStates { get; }
	}
}
