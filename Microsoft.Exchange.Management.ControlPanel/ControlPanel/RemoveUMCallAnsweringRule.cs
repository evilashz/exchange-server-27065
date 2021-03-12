using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000468 RID: 1128
	[DataContract]
	public class RemoveUMCallAnsweringRule : ChangeUMCallAnsweringRule
	{
		// Token: 0x170022A0 RID: 8864
		// (get) Token: 0x06003930 RID: 14640 RVA: 0x000AE1ED File Offset: 0x000AC3ED
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Remove-UMCallAnsweringRule";
			}
		}
	}
}
