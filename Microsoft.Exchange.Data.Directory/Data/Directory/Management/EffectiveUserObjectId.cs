using System;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000700 RID: 1792
	[Serializable]
	internal class EffectiveUserObjectId : ObjectId
	{
		// Token: 0x06005476 RID: 21622 RVA: 0x00131E1D File Offset: 0x0013001D
		internal EffectiveUserObjectId(ADObjectId originalId, ADObjectId effectiveUser)
		{
			this.effectiveUserId = effectiveUser;
			this.identity = originalId;
		}

		// Token: 0x06005477 RID: 21623 RVA: 0x00131E33 File Offset: 0x00130033
		public override byte[] GetBytes()
		{
			return this.identity.GetBytes();
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x00131E40 File Offset: 0x00130040
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.identity.ToString());
			stringBuilder.Append('\\');
			stringBuilder.Append(this.effectiveUserId.Name);
			return stringBuilder.ToString();
		}

		// Token: 0x040038A6 RID: 14502
		private ADObjectId effectiveUserId;

		// Token: 0x040038A7 RID: 14503
		private ADObjectId identity;
	}
}
