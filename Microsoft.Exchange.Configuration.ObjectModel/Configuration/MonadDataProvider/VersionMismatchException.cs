using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001E1 RID: 481
	[Serializable]
	public class VersionMismatchException : LocalizedException
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x00034DA0 File Offset: 0x00032FA0
		// (set) Token: 0x06001141 RID: 4417 RVA: 0x00034DA8 File Offset: 0x00032FA8
		public SupportedVersionList SupportedVersionList { get; private set; }

		// Token: 0x06001142 RID: 4418 RVA: 0x00034DB1 File Offset: 0x00032FB1
		public VersionMismatchException(LocalizedString message, SupportedVersionList versionList) : base(message)
		{
			this.SupportedVersionList = versionList;
		}
	}
}
