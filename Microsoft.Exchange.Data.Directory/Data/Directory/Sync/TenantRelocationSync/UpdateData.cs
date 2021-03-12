using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x02000801 RID: 2049
	internal class UpdateData
	{
		// Token: 0x06006511 RID: 25873 RVA: 0x00160464 File Offset: 0x0015E664
		internal UpdateData(RequestType RequestType)
		{
			this.RequestType = RequestType;
			this.ShadowMetadata = new List<PropertyMetaData>();
		}

		// Token: 0x170023D1 RID: 9169
		// (get) Token: 0x06006512 RID: 25874 RVA: 0x0016047E File Offset: 0x0015E67E
		internal bool HasUpdates
		{
			get
			{
				return this.SourceUserConfigXMLStatus != UpdateData.SourceStatus.None || this.ShadowMetadata.Count != 0 || this.IsNtSecurityDescriptorChanged;
			}
		}

		// Token: 0x04004322 RID: 17186
		internal readonly RequestType RequestType;

		// Token: 0x04004323 RID: 17187
		internal bool IsNtSecurityDescriptorChanged;

		// Token: 0x04004324 RID: 17188
		internal List<PropertyMetaData> ShadowMetadata;

		// Token: 0x04004325 RID: 17189
		internal string SourceUserConfigXML;

		// Token: 0x04004326 RID: 17190
		internal UpdateData.SourceStatus SourceUserConfigXMLStatus;

		// Token: 0x02000802 RID: 2050
		internal enum SourceStatus
		{
			// Token: 0x04004328 RID: 17192
			None,
			// Token: 0x04004329 RID: 17193
			Updated,
			// Token: 0x0400432A RID: 17194
			Removed
		}
	}
}
