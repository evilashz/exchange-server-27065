using System;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000587 RID: 1415
	public abstract class WrappedBinding : Binding
	{
		// Token: 0x06004188 RID: 16776 RVA: 0x000C7BCB File Offset: 0x000C5DCB
		protected WrappedBinding(Binding binding)
		{
			this.binding = binding;
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x000C7BDA File Offset: 0x000C5DDA
		public override string ToJavaScript(IControlResolver resolver)
		{
			return string.Format("new {0}({1})", base.GetType().Name, this.binding.ToJavaScript(resolver));
		}

		// Token: 0x04002B59 RID: 11097
		private Binding binding;
	}
}
