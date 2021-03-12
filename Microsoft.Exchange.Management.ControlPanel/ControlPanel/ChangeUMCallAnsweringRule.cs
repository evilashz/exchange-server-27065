using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000467 RID: 1127
	[DataContract]
	public class ChangeUMCallAnsweringRule : WebServiceParameters
	{
		// Token: 0x1700229E RID: 8862
		// (get) Token: 0x0600392D RID: 14637 RVA: 0x000AE1D7 File Offset: 0x000AC3D7
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-UMCallAnsweringRule";
			}
		}

		// Token: 0x1700229F RID: 8863
		// (get) Token: 0x0600392E RID: 14638 RVA: 0x000AE1DE File Offset: 0x000AC3DE
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}
	}
}
