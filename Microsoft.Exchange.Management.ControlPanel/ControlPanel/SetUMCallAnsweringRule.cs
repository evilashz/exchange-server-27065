using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000465 RID: 1125
	[DataContract]
	public class SetUMCallAnsweringRule : UMCallAnsweringRuleParameters
	{
		// Token: 0x1700229C RID: 8860
		// (get) Token: 0x06003928 RID: 14632 RVA: 0x000AE19A File Offset: 0x000AC39A
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Set-UMCallAnsweringRule";
			}
		}
	}
}
