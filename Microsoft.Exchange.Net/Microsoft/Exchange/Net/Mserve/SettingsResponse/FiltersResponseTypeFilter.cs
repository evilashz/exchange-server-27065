using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008F3 RID: 2291
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersResponseTypeFilter
	{
		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x000735B1 File Offset: 0x000717B1
		// (set) Token: 0x06003166 RID: 12646 RVA: 0x000735B9 File Offset: 0x000717B9
		public int ExecutionOrder
		{
			get
			{
				return this.executionOrderField;
			}
			set
			{
				this.executionOrderField = value;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x000735C2 File Offset: 0x000717C2
		// (set) Token: 0x06003168 RID: 12648 RVA: 0x000735CA File Offset: 0x000717CA
		public byte Enabled
		{
			get
			{
				return this.enabledField;
			}
			set
			{
				this.enabledField = value;
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x000735D3 File Offset: 0x000717D3
		// (set) Token: 0x0600316A RID: 12650 RVA: 0x000735DB File Offset: 0x000717DB
		public RunWhenType RunWhen
		{
			get
			{
				return this.runWhenField;
			}
			set
			{
				this.runWhenField = value;
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x000735E4 File Offset: 0x000717E4
		// (set) Token: 0x0600316C RID: 12652 RVA: 0x000735EC File Offset: 0x000717EC
		public FiltersResponseTypeFilterCondition Condition
		{
			get
			{
				return this.conditionField;
			}
			set
			{
				this.conditionField = value;
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x000735F5 File Offset: 0x000717F5
		// (set) Token: 0x0600316E RID: 12654 RVA: 0x000735FD File Offset: 0x000717FD
		public FiltersResponseTypeFilterActions Actions
		{
			get
			{
				return this.actionsField;
			}
			set
			{
				this.actionsField = value;
			}
		}

		// Token: 0x04002A81 RID: 10881
		private int executionOrderField;

		// Token: 0x04002A82 RID: 10882
		private byte enabledField;

		// Token: 0x04002A83 RID: 10883
		private RunWhenType runWhenField;

		// Token: 0x04002A84 RID: 10884
		private FiltersResponseTypeFilterCondition conditionField;

		// Token: 0x04002A85 RID: 10885
		private FiltersResponseTypeFilterActions actionsField;
	}
}
