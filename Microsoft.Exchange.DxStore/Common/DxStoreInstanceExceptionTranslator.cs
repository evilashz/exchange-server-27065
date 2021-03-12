using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000082 RID: 130
	public class DxStoreInstanceExceptionTranslator : WcfExceptionTranslator<IDxStoreInstance>
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x00010F6C File Offset: 0x0000F16C
		public override Exception GenerateTransientException(Exception ex)
		{
			throw new DxStoreInstanceClientTransientException(ex.Message, ex);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00010F7A File Offset: 0x0000F17A
		public override Exception GeneratePermanentException(Exception ex)
		{
			throw new DxStoreInstanceClientException(ex.Message, ex);
		}
	}
}
