using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000459 RID: 1113
	[DataContract]
	public class SetTransportRule : TransportRuleParameters
	{
		// Token: 0x17002281 RID: 8833
		// (get) Token: 0x060038DB RID: 14555 RVA: 0x000AAFBA File Offset: 0x000A91BA
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Set-TransportRule";
			}
		}

		// Token: 0x17002282 RID: 8834
		// (get) Token: 0x060038DC RID: 14556 RVA: 0x000AAFC1 File Offset: 0x000A91C1
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}
	}
}
