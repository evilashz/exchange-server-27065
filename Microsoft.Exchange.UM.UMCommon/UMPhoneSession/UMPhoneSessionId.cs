using System;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.UM.UMPhoneSession
{
	// Token: 0x0200012D RID: 301
	[Serializable]
	public class UMPhoneSessionId : ObjectId
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00025C8D File Offset: 0x00023E8D
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00025C95 File Offset: 0x00023E95
		public string RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
			private set
			{
				this.rawIdentity = value;
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00025C9E File Offset: 0x00023E9E
		public UMPhoneSessionId(string sessionId)
		{
			this.RawIdentity = sessionId;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00025CAD File Offset: 0x00023EAD
		public override byte[] GetBytes()
		{
			return Encoding.UTF8.GetBytes(this.RawIdentity);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00025CBF File Offset: 0x00023EBF
		public override string ToString()
		{
			return this.RawIdentity;
		}

		// Token: 0x04000564 RID: 1380
		private string rawIdentity;
	}
}
