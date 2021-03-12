using System;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200059B RID: 1435
	public class DataContractBinding : Binding
	{
		// Token: 0x17002578 RID: 9592
		// (get) Token: 0x060041C7 RID: 16839 RVA: 0x000C8125 File Offset: 0x000C6325
		// (set) Token: 0x060041C8 RID: 16840 RVA: 0x000C812D File Offset: 0x000C632D
		public string ContractType { get; set; }

		// Token: 0x17002579 RID: 9593
		// (get) Token: 0x060041C9 RID: 16841 RVA: 0x000C8136 File Offset: 0x000C6336
		public BindingDictionary Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x000C8140 File Offset: 0x000C6340
		public override string ToJavaScript(IControlResolver resolver)
		{
			string arg = string.IsNullOrEmpty(this.ContractType) ? "Object" : this.ContractType;
			return string.Format("new {0}({1},{2})", base.GetType().Name, arg, this.Bindings.ToJavaScript(resolver));
		}

		// Token: 0x04002B64 RID: 11108
		private BindingDictionary bindings = new BindingDictionary();
	}
}
