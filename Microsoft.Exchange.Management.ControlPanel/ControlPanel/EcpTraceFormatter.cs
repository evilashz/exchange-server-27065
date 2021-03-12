using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000555 RID: 1365
	public class EcpTraceFormatter<T>
	{
		// Token: 0x06003FDB RID: 16347 RVA: 0x000C0FD0 File Offset: 0x000BF1D0
		public EcpTraceFormatter(T o)
		{
			this.innerObject = o;
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x000C0FDF File Offset: 0x000BF1DF
		public override string ToString()
		{
			return EcpTraceHelper.GetTraceString(this.innerObject);
		}

		// Token: 0x04002A72 RID: 10866
		private T innerObject;
	}
}
