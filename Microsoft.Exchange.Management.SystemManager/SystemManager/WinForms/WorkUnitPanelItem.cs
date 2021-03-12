using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000190 RID: 400
	internal class WorkUnitPanelItem : IDisposable
	{
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x0003ED7F File Offset: 0x0003CF7F
		// (set) Token: 0x06001004 RID: 4100 RVA: 0x0003ED89 File Offset: 0x0003CF89
		internal bool NeedToUpdateSize
		{
			get
			{
				return this.needToUpdateSize;
			}
			set
			{
				this.needToUpdateSize = value;
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0003ED94 File Offset: 0x0003CF94
		public WorkUnitPanelItem(WorkUnitsPanel ownerControl, WorkUnit workUnit)
		{
			this.ownerControl = ownerControl;
			this.workUnit = workUnit;
			this.WorkUnit.PropertyChanged += this.WorkUnit_PropertyChanged;
			this.NeedToUpdateSize = true;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0003EDC8 File Offset: 0x0003CFC8
		public void BindToControl(WorkUnitPanel control)
		{
			this.control = control;
			this.Control.SuspendLayout();
			this.Control.WorkUnitPanelItem = this;
			this.Control.FastSetIsMinimized(this.IsMinimized);
			this.Control.SizeChanged += this.Control_SizeChanged;
			this.Control.Enter += this.Control_Enter;
			this.Size = this.Control.Size;
			this.Control.IsMinimizedChanged += this.Control_IsMinimizedChanged;
			this.Control.ResumeLayout(true);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0003EE68 File Offset: 0x0003D068
		public WorkUnitPanel UnbindControl()
		{
			this.Control.SuspendLayout();
			this.Control.SizeChanged -= this.Control_SizeChanged;
			this.Control.Enter -= this.Control_Enter;
			this.Control.IsMinimizedChanged -= this.Control_IsMinimizedChanged;
			this.Control.WorkUnitPanelItem = null;
			this.Control.ResumeLayout(false);
			WorkUnitPanel result = this.Control;
			this.control = null;
			return result;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0003EEEC File Offset: 0x0003D0EC
		private void Control_Enter(object sender, EventArgs e)
		{
			this.ownerControl.SetFocusedPanel(this);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0003EEFA File Offset: 0x0003D0FA
		private void Control_IsMinimizedChanged(object sender, EventArgs e)
		{
			this.IsMinimized = this.Control.IsMinimized;
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0003EF0D File Offset: 0x0003D10D
		private void Control_SizeChanged(object sender, EventArgs e)
		{
			this.Size = this.Control.Size;
			this.ownerControl.NeedToUpdateLogicalTopofPanelItems = true;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0003EF2C File Offset: 0x0003D12C
		private void WorkUnit_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.NeedToUpdateSize = true;
			if (this.Control == null)
			{
				this.ownerControl.NeedToUpdateLogicalTopofPanelItems = true;
				return;
			}
			if (this.ownerControl.InvokeRequired)
			{
				this.ownerControl.Invoke(new PropertyChangedEventHandler(this.WorkUnit_PropertyChanged), new object[]
				{
					sender,
					e
				});
				return;
			}
			PropertyChangedEventHandler workUnitPropertyChanged = this.WorkUnitPropertyChanged;
			if (workUnitPropertyChanged != null)
			{
				workUnitPropertyChanged(sender, e);
			}
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0003EFA0 File Offset: 0x0003D1A0
		public void UpdatePanelItemSize()
		{
			if (this.NeedToUpdateSize)
			{
				this.NeedToUpdateSize = false;
				this.ownerControl.MeasureItem(this, out this.collapsedSize, out this.expandedSize);
				if (this.Control == null)
				{
					this.Size = (this.IsMinimized ? this.collapsedSize : this.expandedSize);
				}
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x0003EFF8 File Offset: 0x0003D1F8
		public WorkUnitPanel Control
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x0003F000 File Offset: 0x0003D200
		public WorkUnit WorkUnit
		{
			get
			{
				return this.workUnit;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x0003F008 File Offset: 0x0003D208
		// (set) Token: 0x06001010 RID: 4112 RVA: 0x0003F010 File Offset: 0x0003D210
		public bool IsMinimized
		{
			get
			{
				return this.isMinimized;
			}
			set
			{
				if (this.IsMinimized != value)
				{
					this.isMinimized = value;
					if (this.Control != null)
					{
						this.Control.FastSetIsMinimized(this.IsMinimized);
						return;
					}
					if (!this.NeedToUpdateSize)
					{
						this.Size = (this.IsMinimized ? this.collapsedSize : this.expandedSize);
					}
					this.ownerControl.NeedToUpdateLogicalTopofPanelItems = true;
				}
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x0003F077 File Offset: 0x0003D277
		// (set) Token: 0x06001012 RID: 4114 RVA: 0x0003F07F File Offset: 0x0003D27F
		public int LogicalTop
		{
			get
			{
				return this.logicalTop;
			}
			set
			{
				this.logicalTop = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x0003F088 File Offset: 0x0003D288
		// (set) Token: 0x06001014 RID: 4116 RVA: 0x0003F090 File Offset: 0x0003D290
		public Size Size
		{
			get
			{
				return this.size;
			}
			set
			{
				if (this.Size != value)
				{
					this.size = value;
					if (this.SizeChanged != null)
					{
						this.SizeChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06001015 RID: 4117 RVA: 0x0003F0C0 File Offset: 0x0003D2C0
		// (remove) Token: 0x06001016 RID: 4118 RVA: 0x0003F0F8 File Offset: 0x0003D2F8
		public event PropertyChangedEventHandler WorkUnitPropertyChanged;

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06001017 RID: 4119 RVA: 0x0003F130 File Offset: 0x0003D330
		// (remove) Token: 0x06001018 RID: 4120 RVA: 0x0003F168 File Offset: 0x0003D368
		public event EventHandler SizeChanged;

		// Token: 0x06001019 RID: 4121 RVA: 0x0003F1A0 File Offset: 0x0003D3A0
		public void Dispose()
		{
			if (this.WorkUnit != null)
			{
				this.WorkUnit.PropertyChanged -= this.WorkUnit_PropertyChanged;
				if (this.Control != null)
				{
					Control control = this.UnbindControl();
					control.Dispose();
				}
			}
		}

		// Token: 0x04000643 RID: 1603
		private WorkUnitsPanel ownerControl;

		// Token: 0x04000644 RID: 1604
		private Size collapsedSize;

		// Token: 0x04000645 RID: 1605
		private Size expandedSize;

		// Token: 0x04000646 RID: 1606
		internal volatile bool needToUpdateSize;

		// Token: 0x04000647 RID: 1607
		private WorkUnitPanel control;

		// Token: 0x04000648 RID: 1608
		private WorkUnit workUnit;

		// Token: 0x04000649 RID: 1609
		private bool isMinimized;

		// Token: 0x0400064A RID: 1610
		private int logicalTop;

		// Token: 0x0400064B RID: 1611
		private Size size;
	}
}
