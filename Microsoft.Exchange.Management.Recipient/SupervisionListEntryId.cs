using System;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200010E RID: 270
	[Serializable]
	public sealed class SupervisionListEntryId : ObjectId
	{
		// Token: 0x0600135E RID: 4958 RVA: 0x00047B48 File Offset: 0x00045D48
		public SupervisionListEntryId(SupervisionListEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			this.id = string.Concat(new string[]
			{
				entry.EntryName,
				",",
				entry.Tag,
				",",
				entry.RecipientType.ToString()
			});
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00047BB1 File Offset: 0x00045DB1
		public override string ToString()
		{
			return this.id;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00047BB9 File Offset: 0x00045DB9
		public override byte[] GetBytes()
		{
			return Encoding.Unicode.GetBytes(this.id);
		}

		// Token: 0x040003CF RID: 975
		private readonly string id;
	}
}
