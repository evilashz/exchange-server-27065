using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000081 RID: 129
	public class DxStoreAccessExceptionTranslator : WcfExceptionTranslator<IDxStoreAccess>
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x00010F48 File Offset: 0x0000F148
		public override Exception GenerateTransientException(Exception ex)
		{
			throw new DxStoreAccessClientTransientException(ex.Message, ex);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00010F56 File Offset: 0x0000F156
		public override Exception GeneratePermanentException(Exception ex)
		{
			throw new DxStoreAccessClientException(ex.Message, ex);
		}
	}
}
