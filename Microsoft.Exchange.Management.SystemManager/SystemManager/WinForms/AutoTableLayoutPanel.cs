using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001AA RID: 426
	public class AutoTableLayoutPanel : TableLayoutPanel
	{
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x000428E5 File Offset: 0x00040AE5
		// (set) Token: 0x060010C1 RID: 4289 RVA: 0x000428ED File Offset: 0x00040AED
		[DefaultValue(false)]
		[Description("Enable the ability to support auto layout")]
		[Category("Layout")]
		public bool AutoLayout
		{
			get
			{
				return this.autoLayout;
			}
			set
			{
				if (this.autoLayout != value)
				{
					this.autoLayout = value;
					if (value)
					{
						base.PerformLayout(this, "AutoLayout");
					}
				}
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0004290E File Offset: 0x00040B0E
		protected override void OnControlAdded(ControlEventArgs e)
		{
			this.needAdjustLayout = true;
			e.Control.VisibleChanged += this.Control_VisibleChanged;
			base.OnControlAdded(e);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00042935 File Offset: 0x00040B35
		private void Control_VisibleChanged(object sender, EventArgs e)
		{
			this.needAdjustLayout = true;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0004293E File Offset: 0x00040B3E
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			this.needAdjustLayout = true;
			e.Control.VisibleChanged -= this.Control_VisibleChanged;
			base.OnControlRemoved(e);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00042965 File Offset: 0x00040B65
		protected override void OnFontChanged(EventArgs e)
		{
			this.needAdjustLayout = true;
			base.OnFontChanged(e);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00042975 File Offset: 0x00040B75
		public AutoTableLayoutPanel()
		{
			base.Name = "AutoTableLayoutPanel";
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x000429AB File Offset: 0x00040BAB
		protected override void OnLayout(LayoutEventArgs levent)
		{
			if (this.autoLayout && this.needAdjustLayout)
			{
				this.AdjustLayout(levent);
				this.UpdateTLP();
				this.needAdjustLayout = false;
			}
			base.OnLayout(levent);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x000429D8 File Offset: 0x00040BD8
		private void AdjustLayout(LayoutEventArgs e)
		{
			AlignUnitsCollection alignUnitsCollectionFromTLP = AlignUnitsCollection.GetAlignUnitsCollectionFromTLP(this);
			foreach (IAlignRule alignRule in AlignSettings.RulesList)
			{
				alignRule.Apply(alignUnitsCollectionFromTLP);
			}
			foreach (AlignUnit alignUnit in alignUnitsCollectionFromTLP.Units)
			{
				alignUnit.Control.Margin = LayoutHelper.Scale(alignUnit.ResultMargin + new Padding(0, alignUnitsCollectionFromTLP.RowDeltaValue[alignUnit.Row], 0, 0), this.scaleFactor);
			}
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00042A9C File Offset: 0x00040C9C
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
			this.scaleFactor = factor;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00042AB0 File Offset: 0x00040CB0
		private void UpdateTLP()
		{
			this.Dock = DockStyle.Top;
			this.AutoSize = true;
			this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.Margin = Padding.Empty;
			this.refreshPadding();
			for (int i = 0; i < base.RowCount; i++)
			{
				if (ContainerType.Control != this.containerType)
				{
					base.RowStyles[i].SizeType = SizeType.AutoSize;
				}
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00042B0F File Offset: 0x00040D0F
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x00042B17 File Offset: 0x00040D17
		[Category("Layout")]
		[Description("Enable the ability to set pre-defined Padding")]
		[DefaultValue(ContainerType.Control)]
		public ContainerType ContainerType
		{
			get
			{
				return this.containerType;
			}
			set
			{
				this.containerType = value;
				if (value != ContainerType.Control)
				{
					base.SuspendLayout();
					this.refreshPadding();
					base.ResumeLayout(false);
					base.PerformLayout();
				}
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00042B40 File Offset: 0x00040D40
		private void refreshPadding()
		{
			switch (this.containerType)
			{
			case ContainerType.Wizard:
				this.defaultPadding = new Padding(0, 8, 16, 0);
				break;
			case ContainerType.PropertyPage:
				this.defaultPadding = new Padding(13, 12, 16, 12);
				break;
			case ContainerType.Control:
				this.defaultPadding = this.Padding;
				break;
			}
			this.Padding = this.defaultPadding;
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x00042BA7 File Offset: 0x00040DA7
		// (set) Token: 0x060010CF RID: 4303 RVA: 0x00042BAF File Offset: 0x00040DAF
		public new Padding Padding
		{
			get
			{
				return this.originPadding;
			}
			set
			{
				this.originPadding = value;
				base.Padding = LayoutHelper.RTLPadding(this.originPadding, this);
			}
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00042BCA File Offset: 0x00040DCA
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			base.Padding = LayoutHelper.RTLPadding(this.originPadding, this);
		}

		// Token: 0x04000684 RID: 1668
		private bool autoLayout;

		// Token: 0x04000685 RID: 1669
		private bool needAdjustLayout = true;

		// Token: 0x04000686 RID: 1670
		private SizeF scaleFactor = new SizeF(1f, 1f);

		// Token: 0x04000687 RID: 1671
		private ContainerType containerType = ContainerType.Control;

		// Token: 0x04000688 RID: 1672
		private Padding defaultPadding;

		// Token: 0x04000689 RID: 1673
		private Padding originPadding;
	}
}
