using System;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Sharing
{
	// Token: 0x02000D8C RID: 3468
	[Serializable]
	public class WindowsLiveIdIdentity : ObjectId
	{
		// Token: 0x0600852E RID: 34094 RVA: 0x00220BF4 File Offset: 0x0021EDF4
		internal WindowsLiveIdIdentity(string identity)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("identity");
			}
			this.identity = identity;
		}

		// Token: 0x0600852F RID: 34095 RVA: 0x00220C16 File Offset: 0x0021EE16
		public override byte[] GetBytes()
		{
			return Encoding.Unicode.GetBytes(this.ToString());
		}

		// Token: 0x06008530 RID: 34096 RVA: 0x00220C28 File Offset: 0x0021EE28
		public override string ToString()
		{
			return this.identity;
		}

		// Token: 0x04004055 RID: 16469
		private readonly string identity;
	}
}
