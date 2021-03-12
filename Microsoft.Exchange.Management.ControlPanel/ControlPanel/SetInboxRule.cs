using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003FD RID: 1021
	[DataContract]
	public class SetInboxRule : InboxRuleParameters
	{
		// Token: 0x170020A5 RID: 8357
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x000A377F File Offset: 0x000A197F
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Set-InboxRule";
			}
		}
	}
}
