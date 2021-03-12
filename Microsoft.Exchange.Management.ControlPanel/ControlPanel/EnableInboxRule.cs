using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000402 RID: 1026
	[DataContract]
	public class EnableInboxRule : ChangeInboxRule
	{
		// Token: 0x170020AE RID: 8366
		// (get) Token: 0x060034A9 RID: 13481 RVA: 0x000A3861 File Offset: 0x000A1A61
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Enable-InboxRule";
			}
		}
	}
}
