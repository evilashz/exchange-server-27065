using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000400 RID: 1024
	[DataContract]
	public class RemoveInboxRule : ChangeInboxRule
	{
		// Token: 0x170020AC RID: 8364
		// (get) Token: 0x060034A5 RID: 13477 RVA: 0x000A3843 File Offset: 0x000A1A43
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Remove-InboxRule";
			}
		}
	}
}
