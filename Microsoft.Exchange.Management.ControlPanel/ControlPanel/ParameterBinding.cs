using System;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200058D RID: 1421
	public class ParameterBinding : Binding, ISupportServerSideEvaluate
	{
		// Token: 0x1700256D RID: 9581
		// (get) Token: 0x06004199 RID: 16793 RVA: 0x000C7CC7 File Offset: 0x000C5EC7
		// (set) Token: 0x0600419A RID: 16794 RVA: 0x000C7CCF File Offset: 0x000C5ECF
		public virtual object Value { get; set; }

		// Token: 0x0600419B RID: 16795 RVA: 0x000C7CD8 File Offset: 0x000C5ED8
		public override string ToJavaScript(IControlResolver resolver)
		{
			return string.Format("new ParameterBinding({0})", this.Value.ToJsonString(null));
		}
	}
}
