using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000401 RID: 1025
	[DataContract]
	public class DisableInboxRule : ChangeInboxRule
	{
		// Token: 0x170020AD RID: 8365
		// (get) Token: 0x060034A7 RID: 13479 RVA: 0x000A3852 File Offset: 0x000A1A52
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Disable-InboxRule";
			}
		}
	}
}
