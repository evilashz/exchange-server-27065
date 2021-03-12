using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000083 RID: 131
	public class DxStoreManagerExceptionTranslator : WcfExceptionTranslator<IDxStoreManager>
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x00010F90 File Offset: 0x0000F190
		public override Exception GenerateTransientException(Exception ex)
		{
			throw new DxStoreManagerClientTransientException(ex.Message, ex);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00010F9E File Offset: 0x0000F19E
		public override Exception GeneratePermanentException(Exception ex)
		{
			throw new DxStoreManagerClientException(ex.Message, ex);
		}
	}
}
