using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x020001AD RID: 429
	internal class AdTagData
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00030D1E File Offset: 0x0002EF1E
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x00030D26 File Offset: 0x0002EF26
		internal RetentionTag Tag
		{
			get
			{
				return this.policyTag;
			}
			set
			{
				this.policyTag = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00030D2F File Offset: 0x0002EF2F
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x00030D37 File Offset: 0x0002EF37
		internal SortedDictionary<Guid, ContentSetting> ContentSettings
		{
			get
			{
				return this.contentSettings;
			}
			set
			{
				this.contentSettings = value;
			}
		}

		// Token: 0x04000877 RID: 2167
		private RetentionTag policyTag;

		// Token: 0x04000878 RID: 2168
		private SortedDictionary<Guid, ContentSetting> contentSettings;
	}
}
