using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200018F RID: 399
	public class WorkUnitsPanel : CollapsiblePanelsPanel
	{
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0003DB78 File Offset: 0x0003BD78
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x0003DB82 File Offset: 0x0003BD82
		internal bool NeedToUpdateLogicalTopofPanelItems
		{
			get
			{
				return this.needToUpdateLogicalTopofPanelItems;
			}
			set
			{
				this.needToUpdateLogicalTopofPanelItems = value;
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0003DB90 File Offset: 0x0003BD90
		private int GetCountofPanelItemsUpdatedInOneTime()
		{
			int num = WorkUnitsPanel.countofPanelItemsUpdatedInOneTime;
			if (this.templatePanel != null)
			{
				Size size = WorkUnitPanel.MeasureCollapsedSizeForPanelItem(this.templatePanel, null);
				num = Math.Max(base.ClientSize.Height / size.Height + 1, num);
			}
			return num;
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0003DBD8 File Offset: 0x0003BDD8
		private bool AdjustPanelWidthToHideHorizontalScrollBar(int? totalHeight)
		{
			bool result = false;
			int num = base.Size.Width - base.Padding.Horizontal;
			if ((this.templatePanel.Width != num && totalHeight == null) || (totalHeight != null && totalHeight > base.ClientSize.Height))
			{
				num -= SystemInformation.VerticalScrollBarWidth;
			}
			if (this.templatePanel.Width != num)
			{
				this.templatePanel.Width = num;
				for (int i = 0; i < this.panelItems.Count; i++)
				{
					if (this.panelItems[i].Control != null)
					{
						this.panelItems[i].Control.Width = num;
					}
					this.panelItems[i].NeedToUpdateSize = true;
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0003DCC8 File Offset: 0x0003BEC8
		private int UpdateLogicalTopForThisPanelItem(int index, int logicalTop)
		{
			if (this.panelItems[index].LogicalTop != logicalTop)
			{
				this.panelItems[index].LogicalTop = logicalTop;
				this.needToCreateItems = true;
			}
			this.panelItems[index].UpdatePanelItemSize();
			return logicalTop + this.panelItems[index].Size.Height + WorkUnitsPanel.defaultMargin.Vertical;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0003DD3C File Offset: 0x0003BF3C
		private int CalculateLogicalTopofPanelItems(int minCountofPanelItemsToUpdateSize, int minCountOfPanelItemsToUpdateLogicalTop)
		{
			this.NeedToUpdateLogicalTopofPanelItems = false;
			int num = 0;
			int num2 = base.Padding.Top;
			this.needToCreateItems = (this.needToCreateItems || this.panelItems[this.panelItems.Count - 1].NeedToUpdateSize);
			while (num < this.panelItems.Count && (minCountofPanelItemsToUpdateSize > 0 || minCountOfPanelItemsToUpdateLogicalTop > 0))
			{
				if (this.panelItems[num].NeedToUpdateSize)
				{
					minCountofPanelItemsToUpdateSize--;
				}
				num2 = this.UpdateLogicalTopForThisPanelItem(num, num2);
				minCountOfPanelItemsToUpdateLogicalTop--;
				num++;
			}
			if (num != this.panelItems.Count)
			{
				this.UpdateLogicalTopForThisPanelItem(num, num2);
				num = this.panelItems.Count - 1;
				this.UpdateLogicalTopForThisPanelItem(num, num2);
				this.NeedToUpdateLogicalTopofPanelItems = true;
			}
			return num2 - WorkUnitsPanel.defaultMargin.Vertical;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0003DE18 File Offset: 0x0003C018
		private int UpdateLogicalTopofPanelItems(int minCountofPanelItemsToUpdateSize, int minCountOfPanelItemsToUpdateLogicalTop)
		{
			int num = 0;
			if (!this.suspendUpdateLogicalTop && this.panelItems.Count > 0)
			{
				this.suspendUpdateLogicalTop = true;
				try
				{
					if (!this.NeedToUpdateLogicalTopofPanelItems)
					{
						num = this.panelItems[this.panelItems.Count - 1].LogicalTop + this.panelItems[this.panelItems.Count - 1].Size.Height;
					}
					else
					{
						base.SuspendLayout();
						try
						{
							this.AdjustPanelWidthToHideHorizontalScrollBar(null);
							num = this.CalculateLogicalTopofPanelItems(minCountofPanelItemsToUpdateSize, minCountOfPanelItemsToUpdateLogicalTop);
							if (this.AdjustPanelWidthToHideHorizontalScrollBar(new int?(num)))
							{
								num = this.CalculateLogicalTopofPanelItems(minCountofPanelItemsToUpdateSize, minCountOfPanelItemsToUpdateLogicalTop);
							}
						}
						finally
						{
							base.ResumeLayout(false);
						}
					}
				}
				finally
				{
					this.suspendUpdateLogicalTop = false;
				}
			}
			return num;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0003DF00 File Offset: 0x0003C100
		private int GetMaximumHeightofPanelItemsInBestTimes()
		{
			return this.UpdateLogicalTopofPanelItems(1, this.GetCountofPanelItemsUpdatedInOneTime());
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0003DF10 File Offset: 0x0003C110
		private int UpdateLogicalTopForPartPanelItems()
		{
			int num = this.GetCountofPanelItemsUpdatedInOneTime();
			return this.UpdateLogicalTopofPanelItems(num, num);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0003DF2C File Offset: 0x0003C12C
		public WorkUnitsPanel()
		{
			base.Name = "WorkUnitsPanel";
			Application.Idle += this.Application_Idle;
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0003DF66 File Offset: 0x0003C166
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.WorkUnits = null;
				Application.Idle -= this.Application_Idle;
			}
			base.Dispose(disposing);
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0003DF8A File Offset: 0x0003C18A
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x0003DF94 File Offset: 0x0003C194
		[DefaultValue(null)]
		internal IList<WorkUnit> WorkUnits
		{
			get
			{
				return this.workUnits;
			}
			set
			{
				if (value != this.WorkUnits)
				{
					if (this.WorkUnits is IBindingList)
					{
						((IBindingList)this.WorkUnits).ListChanged -= this.WorkUnits_ListChanged;
					}
					this.workUnits = value;
					if (this.WorkUnits is IBindingList)
					{
						((IBindingList)this.WorkUnits).ListChanged += this.WorkUnits_ListChanged;
					}
					if (base.IsHandleCreated)
					{
						this.WorkUnits_ListChanged(this.WorkUnits, new ListChangedEventArgs(ListChangedType.Reset, -1));
					}
				}
			}
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0003E01E File Offset: 0x0003C21E
		protected override void OnLoad(EventArgs e)
		{
			if (this.WorkUnits != null)
			{
				this.WorkUnits_ListChanged(this.WorkUnits, new ListChangedEventArgs(ListChangedType.Reset, -1));
			}
			base.OnLoad(e);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0003E044 File Offset: 0x0003C244
		private void WorkUnits_ListChanged(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case ListChangedType.Reset:
			case ListChangedType.ItemAdded:
			case ListChangedType.ItemDeleted:
			case ListChangedType.ItemMoved:
				if (base.InvokeRequired)
				{
					base.Invoke(new ListChangedEventHandler(this.WorkUnits_ListChanged), new object[]
					{
						sender,
						e
					});
					return;
				}
				this.CreateCollapsiblePanels();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0003E0A4 File Offset: 0x0003C2A4
		private void CreateCollapsiblePanels()
		{
			using (new ControlWaitCursor(this))
			{
				base.SuspendLayout();
				this.suspendUpdateItem = true;
				try
				{
					for (int i = base.CollapsiblePanels.Count - 1; i >= 0; i--)
					{
						base.CollapsiblePanels[i].Dispose();
					}
					base.CollapsiblePanels.Clear();
					this.templatePanel = null;
					for (int j = this.panelItems.Count - 1; j >= 0; j--)
					{
						this.panelItems[j].Dispose();
					}
					this.panelItems.Clear();
					if (this.WorkUnits != null)
					{
						base.TabStop = (this.WorkUnits.Count > 0);
						this.panelItems.Capacity = this.WorkUnits.Count;
						for (int k = 0; k < this.WorkUnits.Count; k++)
						{
							this.panelItems.Add(new WorkUnitPanelItem(this, this.WorkUnits[k]));
						}
						this.EnableVirtualMode();
						this.needToCreateItems = true;
					}
				}
				finally
				{
					this.suspendUpdateItem = false;
					base.ResumeLayout(false);
					base.PerformLayout(this, WorkUnitsPanel.CreatePanelItemLayout);
				}
			}
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0003E208 File Offset: 0x0003C408
		private void EnableVirtualMode()
		{
			this.firstReservedPanelItemIndex = 0;
			this.lastReservedPanelItemIndex = 0;
			this.createdItemIndices.Clear();
			base.AutoScrollPosition = new Point(base.Padding.Left, base.Padding.Top);
			this.lastVerticalScrollValue = base.Padding.Top;
			this.lastWorkUnitsPanelSize = base.Size;
			this.templatePanel = new WorkUnitPanel();
			this.templatePanel.TabStop = false;
			base.CollapsiblePanels.Add(this.templatePanel);
			this.templatePanel.SetBounds(base.Padding.Left, -32768, base.Size.Width - base.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth, this.templatePanel.Height);
			EventHandler value = new EventHandler(this.PanelItem_SizeChanged);
			for (int i = 0; i < this.panelItems.Count; i++)
			{
				this.panelItems[i].SizeChanged += value;
			}
			this.NeedToUpdateLogicalTopofPanelItems = true;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0003E32C File Offset: 0x0003C52C
		private void PanelItem_SizeChanged(object sender, EventArgs e)
		{
			WorkUnitPanelItem workUnitPanelItem = (WorkUnitPanelItem)sender;
			if (workUnitPanelItem.Control != null)
			{
				this.GetMaximumHeightofPanelItemsInBestTimes();
				this.UpdateTopofAffectedPanels(this.panelItems.IndexOf(workUnitPanelItem));
			}
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0003E364 File Offset: 0x0003C564
		private void UpdateTopofAffectedPanels(int startIndex)
		{
			while (startIndex < this.panelItems.Count - 1)
			{
				if (this.panelItems[startIndex].Control != null && this.panelItems[startIndex + 1].Control != null)
				{
					this.panelItems[startIndex + 1].Control.Top = this.panelItems[startIndex].Control.Top + this.panelItems[startIndex].Control.Height + WorkUnitsPanel.defaultMargin.Vertical;
				}
				startIndex++;
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0003E40C File Offset: 0x0003C60C
		private void UpdatePanelsLocation(int totalItemsHeight, int verticalScrollValue)
		{
			this.templatePanel.Top = -32768;
			int i = 0;
			bool flag = true;
			while (i < this.panelItems.Count)
			{
				if (this.panelItems[i].Control != null)
				{
					if (flag || i == this.panelItems.Count - 1)
					{
						this.panelItems[i].Control.Top = this.panelItems[i].LogicalTop - verticalScrollValue;
					}
					else
					{
						this.panelItems[i].Control.Top = -32768;
					}
					this.panelItems[i].Control.TabStop = flag;
				}
				flag = (flag && this.panelItems[i].LogicalTop < totalItemsHeight);
				i++;
			}
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003E4E8 File Offset: 0x0003C6E8
		internal void MeasureItem(WorkUnitPanelItem panelItem, out Size collapsedSize, out Size expandSize)
		{
			base.SuspendLayout();
			this.templatePanel.Top = -32768;
			collapsedSize = WorkUnitPanel.MeasureCollapsedSizeForPanelItem(this.templatePanel, panelItem);
			expandSize = WorkUnitPanel.MeasureExpandedSizeForPanelItem(this.templatePanel, panelItem);
			base.ResumeLayout(false);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0003E538 File Offset: 0x0003C738
		private void EnsurePanelsCreated()
		{
			if (this.suspendUpdateItem || this.panelItems.Count <= 0)
			{
				return;
			}
			this.suspendUpdateItem = true;
			try
			{
				int maximumHeightofPanelItemsInBestTimes = this.GetMaximumHeightofPanelItemsInBestTimes();
				this.needToCreateItems = false;
				int num = maximumHeightofPanelItemsInBestTimes + base.Padding.Bottom - base.ClientSize.Height;
				if (base.VerticalScroll.Value > num)
				{
					base.AutoScrollPosition = new Point(-base.AutoScrollPosition.X, num);
				}
				this.lastVerticalScrollValue = base.VerticalScroll.Value;
				int num2 = this.lastVerticalScrollValue - base.ClientSize.Height;
				int num3 = this.lastVerticalScrollValue + 2 * base.ClientSize.Height;
				if (num3 >= maximumHeightofPanelItemsInBestTimes)
				{
					num3 = maximumHeightofPanelItemsInBestTimes - 1;
				}
				if (num2 < base.Padding.Top)
				{
					num2 = base.Padding.Top;
				}
				this.firstReservedPanelItemIndex = this.GetPanelItemAtPoint(num2);
				this.lastReservedPanelItemIndex = this.GetPanelItemAtPoint(num3);
				for (int i = this.firstReservedPanelItemIndex; i <= this.lastReservedPanelItemIndex; i++)
				{
					this.CreatePanelForItem(i);
				}
				this.CreatePanelForItem(0);
				this.CreatePanelForItem(this.panelItems.Count - 1);
				this.UpdatePanelsLocation(maximumHeightofPanelItemsInBestTimes, this.lastVerticalScrollValue);
				base.AlignStatusLabel();
			}
			finally
			{
				this.suspendUpdateItem = false;
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003E6BC File Offset: 0x0003C8BC
		private WorkUnitPanel CreatePanelForItem(int itemIndex)
		{
			if (this.panelItems[itemIndex].Control == null)
			{
				this.panelItems[itemIndex].BindToControl(this.CreateOrGetCachedWorkUnitPanel());
				this.panelItems[itemIndex].Control.TabIndex = itemIndex + 1;
				this.createdItemIndices.Add(itemIndex);
			}
			return this.panelItems[itemIndex].Control;
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003E72C File Offset: 0x0003C92C
		private WorkUnitPanel CreateOrGetCachedWorkUnitPanel()
		{
			WorkUnitPanel workUnitPanel = null;
			int num = -1;
			foreach (int num2 in this.createdItemIndices)
			{
				if ((num2 < this.firstReservedPanelItemIndex || num2 > this.lastReservedPanelItemIndex) && num2 != 0 && num2 != this.panelItems.Count - 1 && num2 != this.focusedPanelItemIndex && num2 != this.focusedPanelItemIndex - 1 && num2 != this.focusedPanelItemIndex + 1 && this.panelItems[num2].Control != null)
				{
					workUnitPanel = this.panelItems[num2].UnbindControl();
					num = num2;
					break;
				}
			}
			if (num != -1)
			{
				this.createdItemIndices.Remove(num);
			}
			if (workUnitPanel == null)
			{
				workUnitPanel = new WorkUnitPanel();
				workUnitPanel.Width = this.templatePanel.Width;
				base.CollapsiblePanels.Add(workUnitPanel);
			}
			return workUnitPanel;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0003E824 File Offset: 0x0003CA24
		internal void SetFocusedPanel(object panelItem)
		{
			this.focusedPanelItemIndex = this.panelItems.IndexOf((WorkUnitPanelItem)panelItem);
			if (this.focusedPanelItemIndex > 0)
			{
				this.CreatePanelForItem(this.focusedPanelItemIndex - 1);
			}
			if (this.focusedPanelItemIndex < this.panelItems.Count - 1)
			{
				this.CreatePanelForItem(this.focusedPanelItemIndex + 1);
			}
			base.ScrollControlIntoView(this.panelItems[this.focusedPanelItemIndex].Control);
			base.PerformLayout(null, WorkUnitsPanel.CreatePanelItemLayout);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0003E8AC File Offset: 0x0003CAAC
		private int GetPanelItemAtPoint(int logicalTopOfPanelItem)
		{
			int num = 0;
			while (num < this.panelItems.Count - 1 && this.panelItems[num + 1].LogicalTop <= logicalTopOfPanelItem)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0003E8E8 File Offset: 0x0003CAE8
		private void Application_Idle(object sender, EventArgs e)
		{
			if (this.panelItems.Count > 0 && base.Visible)
			{
				this.UpdateLogicalTopForPartPanelItems();
				if (this.needToCreateItems || this.lastVerticalScrollValue != base.VerticalScroll.Value)
				{
					base.PerformLayout(null, WorkUnitsPanel.CreatePanelItemLayout);
				}
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003E93C File Offset: 0x0003CB3C
		protected override void AdjustLocationOfChildControls(LayoutEventArgs levent)
		{
			if (!this.lastWorkUnitsPanelSize.Equals(base.Size) || levent.AffectedProperty.Equals(WorkUnitsPanel.CreatePanelItemLayout))
			{
				if (!this.lastWorkUnitsPanelSize.Equals(base.Size))
				{
					this.NeedToUpdateLogicalTopofPanelItems = true;
					this.lastWorkUnitsPanelSize = base.Size;
				}
				this.EnsurePanelsCreated();
			}
			base.SetScrollState(8, true);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003E9B8 File Offset: 0x0003CBB8
		private void CreatePanelsIfScrollMoreThanOpenPage()
		{
			if (Math.Abs(this.lastVerticalScrollValue - base.VerticalScroll.Value) >= base.ClientSize.Height)
			{
				base.PerformLayout(null, WorkUnitsPanel.CreatePanelItemLayout);
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0003E9F8 File Offset: 0x0003CBF8
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			this.CreatePanelsIfScrollMoreThanOpenPage();
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0003EA07 File Offset: 0x0003CC07
		protected override void OnScroll(ScrollEventArgs se)
		{
			base.OnScroll(se);
			this.CreatePanelsIfScrollMoreThanOpenPage();
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0003EA18 File Offset: 0x0003CC18
		protected override void GetPanelsState(out bool enableExpandAll, out bool enableCollapseAll)
		{
			enableExpandAll = false;
			enableCollapseAll = false;
			for (int i = 0; i < this.panelItems.Count; i++)
			{
				if (this.panelItems[i].IsMinimized)
				{
					enableExpandAll = true;
				}
				else
				{
					enableCollapseAll = true;
				}
				if (enableExpandAll && enableCollapseAll)
				{
					return;
				}
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0003EA68 File Offset: 0x0003CC68
		protected override void SetIsMinimizeInAll(bool collapse)
		{
			using (new ControlWaitCursor(this))
			{
				base.SuspendLayout();
				this.suspendUpdateItem = true;
				try
				{
					for (int i = this.panelItems.Count - 1; i >= 0; i--)
					{
						this.panelItems[i].IsMinimized = collapse;
					}
					base.AutoScrollPosition = new Point(base.Padding.Left, base.Padding.Top);
				}
				finally
				{
					this.suspendUpdateItem = false;
					base.ResumeLayout(false);
					base.PerformLayout(null, WorkUnitsPanel.CreatePanelItemLayout);
				}
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x0003EB24 File Offset: 0x0003CD24
		// (set) Token: 0x06000FFC RID: 4092 RVA: 0x0003EB2C File Offset: 0x0003CD2C
		[DefaultValue(0)]
		public TaskState TaskState
		{
			get
			{
				return this.taskState;
			}
			set
			{
				if (this.TaskState != value)
				{
					base.SuspendLayout();
					try
					{
						using (new ControlWaitCursor(this))
						{
							this.taskState = value;
							this.OnTaskStateChanged(EventArgs.Empty);
							for (int i = 0; i < this.panelItems.Count; i++)
							{
								if (this.panelItems[i].Control != null)
								{
									this.panelItems[i].Control.Refresh();
								}
								this.panelItems[i].NeedToUpdateSize = true;
							}
							this.NeedToUpdateLogicalTopofPanelItems = true;
						}
					}
					finally
					{
						base.ResumeLayout(false);
						base.AlignStatusLabel();
					}
				}
			}
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0003EBF8 File Offset: 0x0003CDF8
		protected virtual void OnTaskStateChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[WorkUnitsPanel.EventTaskStateChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06000FFE RID: 4094 RVA: 0x0003EC26 File Offset: 0x0003CE26
		// (remove) Token: 0x06000FFF RID: 4095 RVA: 0x0003EC39 File Offset: 0x0003CE39
		public event EventHandler TaskStateChanged
		{
			add
			{
				base.Events.AddHandler(WorkUnitsPanel.EventTaskStateChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(WorkUnitsPanel.EventTaskStateChanged, value);
			}
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0003EC4C File Offset: 0x0003CE4C
		public string GetSummaryText()
		{
			base.SuspendLayout();
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				stringBuilder.AppendLine(((WorkUnitCollection)this.WorkUnits).Description);
				stringBuilder.AppendLine(((WorkUnitCollection)this.WorkUnits).ElapsedTimeText);
				stringBuilder.AppendLine();
				for (int i = 0; i < this.panelItems.Count; i++)
				{
					WorkUnitPanel control = this.panelItems[i].Control;
					if (control == null)
					{
						control = this.templatePanel;
						control.WorkUnitPanelItem = this.panelItems[i];
					}
					stringBuilder.AppendLine();
					stringBuilder.AppendLine(control.GetSummaryText());
					stringBuilder.AppendLine();
				}
			}
			finally
			{
				base.ResumeLayout(false);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0003ED20 File Offset: 0x0003CF20
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.SuspendLayout();
			try
			{
				base.OnVisibleChanged(e);
			}
			finally
			{
				base.ResumeLayout();
			}
		}

		// Token: 0x04000631 RID: 1585
		internal static readonly string CreatePanelItemLayout = "CreatePanelItem";

		// Token: 0x04000632 RID: 1586
		private static readonly Padding defaultMargin = new Padding(0, 0, 0, 1);

		// Token: 0x04000633 RID: 1587
		private IList<WorkUnit> workUnits;

		// Token: 0x04000634 RID: 1588
		private List<WorkUnitPanelItem> panelItems = new List<WorkUnitPanelItem>();

		// Token: 0x04000635 RID: 1589
		private List<int> createdItemIndices = new List<int>();

		// Token: 0x04000636 RID: 1590
		private int firstReservedPanelItemIndex;

		// Token: 0x04000637 RID: 1591
		private int lastReservedPanelItemIndex;

		// Token: 0x04000638 RID: 1592
		private int focusedPanelItemIndex;

		// Token: 0x04000639 RID: 1593
		private bool suspendUpdateItem;

		// Token: 0x0400063A RID: 1594
		private bool needToCreateItems;

		// Token: 0x0400063B RID: 1595
		private volatile bool needToUpdateLogicalTopofPanelItems;

		// Token: 0x0400063C RID: 1596
		private bool suspendUpdateLogicalTop;

		// Token: 0x0400063D RID: 1597
		private Size lastWorkUnitsPanelSize;

		// Token: 0x0400063E RID: 1598
		private int lastVerticalScrollValue;

		// Token: 0x0400063F RID: 1599
		private WorkUnitPanel templatePanel;

		// Token: 0x04000640 RID: 1600
		private static int countofPanelItemsUpdatedInOneTime = 100;

		// Token: 0x04000641 RID: 1601
		private TaskState taskState;

		// Token: 0x04000642 RID: 1602
		private static readonly object EventTaskStateChanged = new object();
	}
}
