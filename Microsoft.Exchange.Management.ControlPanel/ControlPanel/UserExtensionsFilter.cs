using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200046D RID: 1133
	[DataContract]
	public class UserExtensionsFilter : WebServiceParameters
	{
		// Token: 0x170022A7 RID: 8871
		// (get) Token: 0x0600394A RID: 14666 RVA: 0x000AE5FC File Offset: 0x000AC7FC
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Get-UMMailbox";
			}
		}

		// Token: 0x170022A8 RID: 8872
		// (get) Token: 0x0600394B RID: 14667 RVA: 0x000AE603 File Offset: 0x000AC803
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}
	}
}
