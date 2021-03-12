using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000088 RID: 136
	[Serializable]
	internal sealed class GenericObjectId : ObjectId
	{
		// Token: 0x060004F3 RID: 1267 RVA: 0x00010C03 File Offset: 0x0000EE03
		public GenericObjectId(int id)
		{
			this.bytes = BitConverter.GetBytes(id);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00010C17 File Offset: 0x0000EE17
		public GenericObjectId(Guid id)
		{
			this.bytes = id.ToByteArray();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00010C2C File Offset: 0x0000EE2C
		public override byte[] GetBytes()
		{
			return this.bytes;
		}

		// Token: 0x04000336 RID: 822
		private byte[] bytes;
	}
}
