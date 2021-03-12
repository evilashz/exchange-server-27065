using System;
using System.Windows.Markup;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200014A RID: 330
	public class VariableReferenceExtension : MarkupExtension
	{
		// Token: 0x06002148 RID: 8520 RVA: 0x00064548 File Offset: 0x00062748
		public VariableReferenceExtension()
		{
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x00064550 File Offset: 0x00062750
		public VariableReferenceExtension(string variable, bool useInput)
		{
			this.Variable = variable;
			this.UseInput = useInput;
		}

		// Token: 0x17001A64 RID: 6756
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x00064566 File Offset: 0x00062766
		// (set) Token: 0x0600214B RID: 8523 RVA: 0x0006456E File Offset: 0x0006276E
		public string Variable { get; set; }

		// Token: 0x17001A65 RID: 6757
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x00064577 File Offset: 0x00062777
		// (set) Token: 0x0600214D RID: 8525 RVA: 0x0006457F File Offset: 0x0006277F
		public bool UseInput { get; set; }

		// Token: 0x0600214E RID: 8526 RVA: 0x00064588 File Offset: 0x00062788
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new VariableReference
			{
				Variable = this.Variable,
				UseInput = this.UseInput
			};
		}
	}
}
