using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ABProviderFramework;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000A9 RID: 169
	[Serializable]
	internal sealed class ADABObjectId : ABObjectId
	{
		// Token: 0x0600095A RID: 2394 RVA: 0x00036CD2 File Offset: 0x00034ED2
		public ADABObjectId(ADObjectId activeDirectoryObjectId)
		{
			if (activeDirectoryObjectId == null)
			{
				throw new ArgumentNullException("activeDirectoryObjectId");
			}
			this.activeDirectoryObjectId = activeDirectoryObjectId;
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x00036CEF File Offset: 0x00034EEF
		public ADObjectId NativeId
		{
			get
			{
				return this.activeDirectoryObjectId;
			}
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00036CF7 File Offset: 0x00034EF7
		public override byte[] GetBytes()
		{
			return this.activeDirectoryObjectId.GetBytes();
		}

		// Token: 0x040005DD RID: 1501
		private ADObjectId activeDirectoryObjectId;
	}
}
