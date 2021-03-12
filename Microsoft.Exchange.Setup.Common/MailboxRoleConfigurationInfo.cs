using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000043 RID: 67
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxRoleConfigurationInfo : MailboxBaseConfigurationInfo
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000AFE4 File Offset: 0x000091E4
		public override string Name
		{
			get
			{
				return "MailboxRole";
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000AFEB File Offset: 0x000091EB
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.MailboxRoleDisplayName;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000AFF2 File Offset: 0x000091F2
		public override decimal Size
		{
			get
			{
				return RequiredDiskSpaceStatistics.MailboxRole;
			}
		}
	}
}
