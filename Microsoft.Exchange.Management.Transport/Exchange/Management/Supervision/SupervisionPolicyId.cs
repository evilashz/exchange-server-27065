using System;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Supervision
{
	// Token: 0x0200008E RID: 142
	[Serializable]
	public sealed class SupervisionPolicyId : ObjectId
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x00013F9D File Offset: 0x0001219D
		public SupervisionPolicyId(string orgname)
		{
			if (string.IsNullOrEmpty(orgname))
			{
				throw new ArgumentNullException("orgname");
			}
			this.id = orgname;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00013FBF File Offset: 0x000121BF
		public override string ToString()
		{
			return this.id;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00013FC7 File Offset: 0x000121C7
		public override byte[] GetBytes()
		{
			return Encoding.Unicode.GetBytes(this.id);
		}

		// Token: 0x040001B1 RID: 433
		private readonly string id;
	}
}
