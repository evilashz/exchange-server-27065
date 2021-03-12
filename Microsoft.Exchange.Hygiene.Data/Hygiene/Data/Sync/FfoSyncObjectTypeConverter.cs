using System;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x02000219 RID: 537
	internal static class FfoSyncObjectTypeConverter
	{
		// Token: 0x0600162F RID: 5679 RVA: 0x0004500C File Offset: 0x0004320C
		public static DirectoryObjectClass FromFfoType(FfoSyncObjectType ffoType)
		{
			string name = Enum.GetName(typeof(FfoSyncObjectType), ffoType);
			return (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), name);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x00045044 File Offset: 0x00043244
		public static FfoSyncObjectType ToFfoType(DirectoryObjectClass msodsType)
		{
			string name = Enum.GetName(typeof(DirectoryObjectClass), msodsType);
			return (FfoSyncObjectType)Enum.Parse(typeof(FfoSyncObjectType), name);
		}
	}
}
