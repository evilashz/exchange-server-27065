using System;
using System.Globalization;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000173 RID: 371
	internal class DDIVMockedExchangeRunspaceConfiguration : ExchangeRunspaceConfiguration
	{
		// Token: 0x06002225 RID: 8741 RVA: 0x00066F27 File Offset: 0x00065127
		internal override bool TryGetExecutingUserId(out ADObjectId executingUserId)
		{
			executingUserId = new ADObjectId();
			return executingUserId != null;
		}

		// Token: 0x17001A8F RID: 6799
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x00066F38 File Offset: 0x00065138
		internal override string ExecutingUserDisplayName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17001A90 RID: 6800
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x00066F3F File Offset: 0x0006513F
		internal override MultiValuedProperty<CultureInfo> ExecutingUserLanguages
		{
			get
			{
				return new MultiValuedProperty<CultureInfo>();
			}
		}
	}
}
