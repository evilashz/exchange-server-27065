using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000143 RID: 323
	public class PSCommandWrapperFactory : IPSCommandWrapperFactory
	{
		// Token: 0x06002131 RID: 8497 RVA: 0x00064334 File Offset: 0x00062534
		private PSCommandWrapperFactory()
		{
		}

		// Token: 0x17001A60 RID: 6752
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x0006433C File Offset: 0x0006253C
		// (set) Token: 0x06002133 RID: 8499 RVA: 0x00064343 File Offset: 0x00062543
		public static IPSCommandWrapperFactory Instance { get; internal set; } = new PSCommandWrapperFactory();

		// Token: 0x06002134 RID: 8500 RVA: 0x0006434B File Offset: 0x0006254B
		public IPSCommandWrapper CreatePSCommand()
		{
			return new PSCommandWrapper(new PSCommand());
		}
	}
}
