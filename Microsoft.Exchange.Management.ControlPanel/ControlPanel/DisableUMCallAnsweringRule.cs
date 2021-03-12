using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000469 RID: 1129
	[DataContract]
	public class DisableUMCallAnsweringRule : ChangeUMCallAnsweringRule
	{
		// Token: 0x170022A1 RID: 8865
		// (get) Token: 0x06003932 RID: 14642 RVA: 0x000AE1FC File Offset: 0x000AC3FC
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Disable-UMCallAnsweringRule";
			}
		}
	}
}
