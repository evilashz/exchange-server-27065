using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200074A RID: 1866
	[Serializable]
	public class ResubmitRequestId : ObjectId
	{
		// Token: 0x06005AD3 RID: 23251 RVA: 0x0013E2CD File Offset: 0x0013C4CD
		public ResubmitRequestId(long requestIdentity)
		{
			this.requestIdentity = requestIdentity;
		}

		// Token: 0x17001F7F RID: 8063
		// (get) Token: 0x06005AD4 RID: 23252 RVA: 0x0013E2DC File Offset: 0x0013C4DC
		public long ResubmitRequestRowId
		{
			get
			{
				return this.requestIdentity;
			}
		}

		// Token: 0x06005AD5 RID: 23253 RVA: 0x0013E2E4 File Offset: 0x0013C4E4
		public static ResubmitRequestId Parse(string identity)
		{
			return new ResubmitRequestId(long.Parse(identity));
		}

		// Token: 0x06005AD6 RID: 23254 RVA: 0x0013E2F4 File Offset: 0x0013C4F4
		public static bool TryParse(string identity, out ResubmitRequestId resultIdentity)
		{
			long num;
			if (long.TryParse(identity, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				resultIdentity = new ResubmitRequestId(num);
				return true;
			}
			resultIdentity = null;
			return false;
		}

		// Token: 0x06005AD7 RID: 23255 RVA: 0x0013E31F File Offset: 0x0013C51F
		public override byte[] GetBytes()
		{
			return new byte[0];
		}

		// Token: 0x06005AD8 RID: 23256 RVA: 0x0013E328 File Offset: 0x0013C528
		public override string ToString()
		{
			return this.ResubmitRequestRowId.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x04003CE8 RID: 15592
		private readonly long requestIdentity;
	}
}
