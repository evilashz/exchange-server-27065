using System;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005C7 RID: 1479
	public class EcpRangeValidator : RangeValidator, IEcpValidator
	{
		// Token: 0x17002608 RID: 9736
		// (get) Token: 0x06004313 RID: 17171 RVA: 0x000CB709 File Offset: 0x000C9909
		public string DefaultErrorMessage
		{
			get
			{
				return string.Format(Strings.RangeValidatorErrorMessage, base.MinimumValue, base.MaximumValue);
			}
		}

		// Token: 0x17002609 RID: 9737
		// (get) Token: 0x06004314 RID: 17172 RVA: 0x000CB726 File Offset: 0x000C9926
		public virtual string TypeId
		{
			get
			{
				return "Range";
			}
		}
	}
}
