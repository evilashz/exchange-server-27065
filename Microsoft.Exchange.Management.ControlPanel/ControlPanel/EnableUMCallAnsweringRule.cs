using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200046A RID: 1130
	[DataContract]
	public class EnableUMCallAnsweringRule : ChangeUMCallAnsweringRule
	{
		// Token: 0x170022A2 RID: 8866
		// (get) Token: 0x06003934 RID: 14644 RVA: 0x000AE20B File Offset: 0x000AC40B
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Enable-UMCallAnsweringRule";
			}
		}
	}
}
