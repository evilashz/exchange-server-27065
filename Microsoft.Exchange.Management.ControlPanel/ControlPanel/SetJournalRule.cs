using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000413 RID: 1043
	[DataContract]
	public class SetJournalRule : JournalRuleObjectProperties
	{
		// Token: 0x170020D9 RID: 8409
		// (get) Token: 0x06003516 RID: 13590 RVA: 0x000A5516 File Offset: 0x000A3716
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Set-JournalRule";
			}
		}
	}
}
