using System;
using System.ComponentModel;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200063B RID: 1595
	public class TaskCommand : Command
	{
		// Token: 0x0600460A RID: 17930 RVA: 0x000D3B26 File Offset: 0x000D1D26
		public TaskCommand()
		{
			this.SelectionMode = SelectionMode.SupportsMultipleSelection;
			this.IsLongRunning = false;
		}

		// Token: 0x170026FF RID: 9983
		// (get) Token: 0x0600460B RID: 17931 RVA: 0x000D3B47 File Offset: 0x000D1D47
		// (set) Token: 0x0600460C RID: 17932 RVA: 0x000D3B4F File Offset: 0x000D1D4F
		[DefaultValue(SelectionMode.SupportsMultipleSelection)]
		public override SelectionMode SelectionMode { get; set; }

		// Token: 0x17002700 RID: 9984
		// (get) Token: 0x0600460D RID: 17933 RVA: 0x000D3B58 File Offset: 0x000D1D58
		// (set) Token: 0x0600460E RID: 17934 RVA: 0x000D3B60 File Offset: 0x000D1D60
		[DefaultValue("")]
		public virtual string ActionName { get; set; }

		// Token: 0x17002701 RID: 9985
		// (get) Token: 0x0600460F RID: 17935 RVA: 0x000D3B69 File Offset: 0x000D1D69
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public BindingCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17002702 RID: 9986
		// (get) Token: 0x06004610 RID: 17936 RVA: 0x000D3B71 File Offset: 0x000D1D71
		// (set) Token: 0x06004611 RID: 17937 RVA: 0x000D3B79 File Offset: 0x000D1D79
		[DefaultValue(false)]
		public bool IsLongRunning { get; set; }

		// Token: 0x17002703 RID: 9987
		// (get) Token: 0x06004612 RID: 17938 RVA: 0x000D3B82 File Offset: 0x000D1D82
		// (set) Token: 0x06004613 RID: 17939 RVA: 0x000D3B8A File Offset: 0x000D1D8A
		[DefaultValue("")]
		[Localizable(true)]
		public string InProgressDescription { get; set; }

		// Token: 0x17002704 RID: 9988
		// (get) Token: 0x06004614 RID: 17940 RVA: 0x000D3B93 File Offset: 0x000D1D93
		// (set) Token: 0x06004615 RID: 17941 RVA: 0x000D3B9B File Offset: 0x000D1D9B
		[Localizable(true)]
		[DefaultValue("")]
		public string StoppedDescription { get; set; }

		// Token: 0x17002705 RID: 9989
		// (get) Token: 0x06004616 RID: 17942 RVA: 0x000D3BA4 File Offset: 0x000D1DA4
		// (set) Token: 0x06004617 RID: 17943 RVA: 0x000D3BAC File Offset: 0x000D1DAC
		[DefaultValue("")]
		[Localizable(true)]
		public string CompletedDescription { get; set; }

		// Token: 0x04002F56 RID: 12118
		private BindingCollection parameters = new BindingCollection();
	}
}
