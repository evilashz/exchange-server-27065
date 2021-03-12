using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004E3 RID: 1251
	[DataContract]
	public class DeletedMailboxFilter : WebServiceParameters
	{
		// Token: 0x17002401 RID: 9217
		// (get) Token: 0x06003CFC RID: 15612 RVA: 0x000B715F File Offset: 0x000B535F
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-RemovedMailbox";
			}
		}

		// Token: 0x17002402 RID: 9218
		// (get) Token: 0x06003CFD RID: 15613 RVA: 0x000B7166 File Offset: 0x000B5366
		public override string RbacScope
		{
			get
			{
				return string.Empty;
			}
		}
	}
}
