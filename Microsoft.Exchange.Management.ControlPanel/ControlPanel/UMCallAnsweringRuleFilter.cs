using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000463 RID: 1123
	[DataContract]
	public class UMCallAnsweringRuleFilter : WebServiceParameters
	{
		// Token: 0x17002291 RID: 8849
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x000ADE99 File Offset: 0x000AC099
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Get-UMCallAnsweringRule";
			}
		}

		// Token: 0x17002292 RID: 8850
		// (get) Token: 0x06003910 RID: 14608 RVA: 0x000ADEA0 File Offset: 0x000AC0A0
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}
	}
}
