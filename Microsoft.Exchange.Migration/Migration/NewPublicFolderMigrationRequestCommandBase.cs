using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200016D RID: 365
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class NewPublicFolderMigrationRequestCommandBase : NewMrsRequestCommandBase
	{
		// Token: 0x06001184 RID: 4484 RVA: 0x0004A106 File Offset: 0x00048306
		protected NewPublicFolderMigrationRequestCommandBase(string cmdletName, ICollection<Type> exceptionsToIgnore) : base(cmdletName, exceptionsToIgnore)
		{
		}

		// Token: 0x1700053A RID: 1338
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x0004A110 File Offset: 0x00048310
		public PSCredential RemoteCredential
		{
			set
			{
				base.AddParameter("RemoteCredential", value);
			}
		}

		// Token: 0x1700053B RID: 1339
		// (set) Token: 0x06001186 RID: 4486 RVA: 0x0004A11E File Offset: 0x0004831E
		public string OutlookAnywhereHostName
		{
			set
			{
				base.AddParameter("OutlookAnywhereHostName", value);
			}
		}

		// Token: 0x1700053C RID: 1340
		// (set) Token: 0x06001187 RID: 4487 RVA: 0x0004A12C File Offset: 0x0004832C
		public string RemoteMailboxLegacyDN
		{
			set
			{
				base.AddParameter("RemoteMailboxLegacyDN", value);
			}
		}

		// Token: 0x1700053D RID: 1341
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x0004A13A File Offset: 0x0004833A
		public string RemoteMailboxServerLegacyDN
		{
			set
			{
				base.AddParameter("RemoteMailboxServerLegacyDN", value);
			}
		}

		// Token: 0x1700053E RID: 1342
		// (set) Token: 0x06001189 RID: 4489 RVA: 0x0004A148 File Offset: 0x00048348
		public DatabaseIdParameter SourceDatabase
		{
			set
			{
				base.AddParameter("SourceDatabase", value);
			}
		}

		// Token: 0x1700053F RID: 1343
		// (set) Token: 0x0600118A RID: 4490 RVA: 0x0004A156 File Offset: 0x00048356
		public AuthenticationMethod AuthenticationMethod
		{
			set
			{
				base.AddParameter("AuthenticationMethod", value);
			}
		}

		// Token: 0x0400060E RID: 1550
		private const string RemoteMailboxLegacyDNParameter = "RemoteMailboxLegacyDN";

		// Token: 0x0400060F RID: 1551
		private const string RemoteServerLegacyDNParameter = "RemoteMailboxServerLegacyDN";

		// Token: 0x04000610 RID: 1552
		internal const string AuthenticationMethodParameter = "AuthenticationMethod";

		// Token: 0x04000611 RID: 1553
		private const string SourceDatabaseParameter = "SourceDatabase";
	}
}
