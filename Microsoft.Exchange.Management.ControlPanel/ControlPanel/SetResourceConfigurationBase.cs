using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000AD RID: 173
	[DataContract]
	public class SetResourceConfigurationBase : SetObjectProperties
	{
		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x0005865D File Offset: 0x0005685D
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-CalendarProcessing";
			}
		}

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x00058664 File Offset: 0x00056864
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}
	}
}
