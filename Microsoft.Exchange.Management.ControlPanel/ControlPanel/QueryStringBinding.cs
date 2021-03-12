using System;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200058E RID: 1422
	public class QueryStringBinding : StaticBinding
	{
		// Token: 0x1700256E RID: 9582
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x000C7CF8 File Offset: 0x000C5EF8
		// (set) Token: 0x0600419E RID: 16798 RVA: 0x000C7D00 File Offset: 0x000C5F00
		public string QueryStringField { get; set; }

		// Token: 0x1700256F RID: 9583
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x000C7D09 File Offset: 0x000C5F09
		public override bool HasValue
		{
			get
			{
				return this.GetInternalValue() != null;
			}
		}

		// Token: 0x17002570 RID: 9584
		// (get) Token: 0x060041A0 RID: 16800 RVA: 0x000C7D18 File Offset: 0x000C5F18
		// (set) Token: 0x060041A1 RID: 16801 RVA: 0x000C7D61 File Offset: 0x000C5F61
		public override object Value
		{
			get
			{
				object internalValue = this.GetInternalValue();
				if (internalValue != null)
				{
					return internalValue;
				}
				if (base.Optional && !string.IsNullOrEmpty(base.DefaultValue))
				{
					return base.DefaultValue;
				}
				throw new BadQueryParameterException(this.QueryStringField ?? string.Empty);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002571 RID: 9585
		// (get) Token: 0x060041A2 RID: 16802 RVA: 0x000C7D68 File Offset: 0x000C5F68
		protected string QueryStringValue
		{
			get
			{
				return HttpContext.Current.Request[this.QueryStringField];
			}
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x000C7D7F File Offset: 0x000C5F7F
		protected virtual object GetInternalValue()
		{
			return this.QueryStringValue;
		}
	}
}
