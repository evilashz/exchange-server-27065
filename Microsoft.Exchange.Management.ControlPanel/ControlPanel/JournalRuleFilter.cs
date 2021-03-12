using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000411 RID: 1041
	[DataContract]
	public class JournalRuleFilter : WebServiceParameters
	{
		// Token: 0x170020CF RID: 8399
		// (get) Token: 0x06003503 RID: 13571 RVA: 0x000A52D6 File Offset: 0x000A34D6
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Get-JournalRule";
			}
		}

		// Token: 0x170020D0 RID: 8400
		// (get) Token: 0x06003504 RID: 13572 RVA: 0x000A52DD File Offset: 0x000A34DD
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}
	}
}
