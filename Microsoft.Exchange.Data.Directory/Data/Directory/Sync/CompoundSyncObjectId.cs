using System;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007E4 RID: 2020
	[Serializable]
	public class CompoundSyncObjectId : ObjectId
	{
		// Token: 0x060063F2 RID: 25586 RVA: 0x0015B164 File Offset: 0x00159364
		public CompoundSyncObjectId(SyncObjectId syncObjectId, ServiceInstanceId serviceInstanceId)
		{
			this.SyncObjectId = syncObjectId;
			this.ServiceInstanceId = serviceInstanceId;
		}

		// Token: 0x1700236A RID: 9066
		// (get) Token: 0x060063F3 RID: 25587 RVA: 0x0015B17A File Offset: 0x0015937A
		// (set) Token: 0x060063F4 RID: 25588 RVA: 0x0015B182 File Offset: 0x00159382
		public SyncObjectId SyncObjectId { get; private set; }

		// Token: 0x1700236B RID: 9067
		// (get) Token: 0x060063F5 RID: 25589 RVA: 0x0015B18B File Offset: 0x0015938B
		// (set) Token: 0x060063F6 RID: 25590 RVA: 0x0015B193 File Offset: 0x00159393
		public ServiceInstanceId ServiceInstanceId { get; private set; }

		// Token: 0x060063F7 RID: 25591 RVA: 0x0015B19C File Offset: 0x0015939C
		public override byte[] GetBytes()
		{
			return Encoding.UTF8.GetBytes(this.ToString());
		}

		// Token: 0x060063F8 RID: 25592 RVA: 0x0015B1AE File Offset: 0x001593AE
		public override string ToString()
		{
			return string.Format("{0}\\{1}", this.ServiceInstanceId, this.SyncObjectId);
		}
	}
}
