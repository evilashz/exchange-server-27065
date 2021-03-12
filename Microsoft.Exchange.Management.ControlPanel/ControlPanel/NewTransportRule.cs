using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200045A RID: 1114
	[DataContract]
	public class NewTransportRule : TransportRuleParameters
	{
		// Token: 0x17002283 RID: 8835
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x000AAFD0 File Offset: 0x000A91D0
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "New-TransportRule";
			}
		}

		// Token: 0x17002284 RID: 8836
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x000AAFD7 File Offset: 0x000A91D7
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}
	}
}
