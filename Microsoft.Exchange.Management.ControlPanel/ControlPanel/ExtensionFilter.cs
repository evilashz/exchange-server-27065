using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001DD RID: 477
	[DataContract]
	public class ExtensionFilter : WebServiceParameters
	{
		// Token: 0x17001B9B RID: 7067
		// (get) Token: 0x06002592 RID: 9618 RVA: 0x000735FB File Offset: 0x000717FB
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Get-App";
			}
		}

		// Token: 0x17001B9C RID: 7068
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x00073602 File Offset: 0x00071802
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}
	}
}
