using System;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Core.Diagnostics
{
	// Token: 0x02000094 RID: 148
	internal interface IDiagnosticsLogConfig : IConfig
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003F6 RID: 1014
		Guid EventLogComponentGuid { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003F7 RID: 1015
		bool IsEnabled { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003F8 RID: 1016
		string ServiceName { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003F9 RID: 1017
		string LogTypeName { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003FA RID: 1018
		string LogFilePath { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003FB RID: 1019
		string LogFilePrefix { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003FC RID: 1020
		string LogComponent { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003FD RID: 1021
		int MaxAge { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003FE RID: 1022
		int MaxDirectorySize { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003FF RID: 1023
		int MaxFileSize { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000400 RID: 1024
		bool IncludeExtendedLogging { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000401 RID: 1025
		int ExtendedLoggingSize { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000402 RID: 1026
		DiagnosticsLoggingTag DiagnosticsLoggingTag { get; }
	}
}
