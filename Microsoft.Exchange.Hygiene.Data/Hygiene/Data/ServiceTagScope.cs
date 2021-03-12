using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000A3 RID: 163
	internal class ServiceTagScope : IDisposable
	{
		// Token: 0x06000571 RID: 1393 RVA: 0x00012599 File Offset: 0x00010799
		public ServiceTagScope(string newServiceTag)
		{
			if (newServiceTag != null)
			{
				this.oldServiceTag = DalHelper.ServiceTagContext;
				DalHelper.ServiceTagContext = newServiceTag;
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000125B5 File Offset: 0x000107B5
		public void Dispose()
		{
			if (this.oldServiceTag != null)
			{
				DalHelper.ServiceTagContext = this.oldServiceTag;
				this.oldServiceTag = null;
			}
		}

		// Token: 0x04000366 RID: 870
		private string oldServiceTag;
	}
}
