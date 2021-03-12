using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000038 RID: 56
	internal class PickupContext : PoisonContext
	{
		// Token: 0x06000141 RID: 321 RVA: 0x000064F4 File Offset: 0x000046F4
		public PickupContext(string pickupFileName) : base(MessageProcessingSource.Pickup)
		{
			this.fileName = pickupFileName;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00006504 File Offset: 0x00004704
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x040000AC RID: 172
		private string fileName;
	}
}
