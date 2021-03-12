using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001A1 RID: 417
	public class AlignUnitsCollection
	{
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x00041324 File Offset: 0x0003F524
		// (set) Token: 0x0600108C RID: 4236 RVA: 0x0004132C File Offset: 0x0003F52C
		public int[] RowDeltaValue { get; set; }

		// Token: 0x0600108D RID: 4237 RVA: 0x00041338 File Offset: 0x0003F538
		private bool GetControlVisible(Control control)
		{
			ISite site = control.Site;
			if (site != null && site.DesignMode)
			{
				IDesignerHost designerHost = site.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					try
					{
						ControlDesigner target = designerHost.GetDesigner(control) as ControlDesigner;
						BindingFlags invokeAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;
						object obj = typeof(ControlDesigner).InvokeMember("ShadowProperties", invokeAttr, null, target, null);
						return (bool)obj.GetType().InvokeMember("Item", invokeAttr, null, obj, new object[]
						{
							"Visible"
						});
					}
					catch (Exception)
					{
						return control.Visible;
					}
				}
			}
			return control.Visible;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x000413FC File Offset: 0x0003F5FC
		private IList<int> GetInvisibleRowsWithControl(TableLayoutPanel panel)
		{
			List<int> list = new List<int>();
			foreach (object obj in panel.Controls)
			{
				Control control = (Control)obj;
				int row = panel.GetRow(control);
				if (row >= 0 && !list.Contains(row))
				{
					list.Add(row);
				}
			}
			foreach (object obj2 in panel.Controls)
			{
				Control control2 = (Control)obj2;
				if (this.GetControlVisible(control2))
				{
					list.Remove(panel.GetRow(control2));
				}
			}
			return list;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x000414D8 File Offset: 0x0003F6D8
		private IDictionary<int, int> GetRowsMapping(TableLayoutPanel panel)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			IList<int> invisibleRowsWithControl = this.GetInvisibleRowsWithControl(panel);
			int num = 0;
			for (int i = 0; i < panel.RowCount; i++)
			{
				if (invisibleRowsWithControl.Contains(i))
				{
					num++;
				}
				else
				{
					dictionary[i] = i - num;
				}
			}
			return dictionary;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00041520 File Offset: 0x0003F720
		private AlignUnitsCollection(TableLayoutPanel panel)
		{
			IDictionary<int, int> rowsMapping = this.GetRowsMapping(panel);
			this.unitsCollection = new AlignUnit[rowsMapping.Count, panel.ColumnCount];
			this.RowDeltaValue = new int[rowsMapping.Count];
			foreach (object obj in panel.Controls)
			{
				Control control = (Control)obj;
				int row = panel.GetRow(control);
				int column = panel.GetColumn(control);
				if (this.GetControlVisible(control) && row >= 0 && column >= 0)
				{
					int num = rowsMapping[row];
					int num2 = column;
					AlignUnit alignUnit = new AlignUnit(control, panel.GetRowSpan(control), panel.GetColumnSpan(control), num, num2);
					for (int i = 0; i < alignUnit.ColumnSpan; i++)
					{
						this.unitsCollection[num, num2 + i] = alignUnit;
					}
					this.unitsList.Add(alignUnit);
					if (!this.rowUnitsDictionary.ContainsKey(num))
					{
						this.rowUnitsDictionary[num] = new List<AlignUnit>();
					}
					this.rowUnitsDictionary[num].Add(alignUnit);
				}
			}
			for (int j = 0; j < this.RowCount; j++)
			{
				if (!this.rowUnitsDictionary.ContainsKey(j))
				{
					this.rowUnitsDictionary[j] = new List<AlignUnit>();
				}
				else
				{
					this.rowUnitsDictionary[j].Sort();
				}
				this.RowDeltaValue[j] = AlignSettings.DefaultVertical;
			}
			this.UpdateCompareMargin();
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x000416E8 File Offset: 0x0003F8E8
		public static AlignUnitsCollection GetAlignUnitsCollectionFromTLP(TableLayoutPanel tlp)
		{
			return new AlignUnitsCollection(tlp);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x000416F0 File Offset: 0x0003F8F0
		private void UpdateCompareMargin()
		{
			foreach (AlignUnit alignUnit in this.Units)
			{
				AlignMappingEntry mappingEntry = AlignSettings.GetMappingEntry(alignUnit.Control.GetType());
				if (mappingEntry != AlignMappingEntry.Empty)
				{
					alignUnit.CompareMargin = mappingEntry.CompareMargin;
					alignUnit.InlinedMargin = mappingEntry.InlinedMargin;
					if (alignUnit.Control.Height < mappingEntry.DefaultHeight && mappingEntry.DefaultHeight > 0)
					{
						int num = mappingEntry.DefaultHeight - alignUnit.Control.Height;
						if (num >= alignUnit.CompareMargin.Vertical)
						{
							alignUnit.CompareMargin = Padding.Empty;
						}
						else
						{
							int num2 = num * alignUnit.CompareMargin.Top / alignUnit.CompareMargin.Vertical;
							alignUnit.CompareMargin -= new Padding(0, num2, 0, num - num2);
						}
					}
				}
				else
				{
					alignUnit.CompareMargin = Padding.Empty;
					alignUnit.InlinedMargin = Padding.Empty;
				}
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00041820 File Offset: 0x0003FA20
		public int RowCount
		{
			get
			{
				return this.unitsCollection.GetLength(0);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x0004182E File Offset: 0x0003FA2E
		public int ColumnCount
		{
			get
			{
				return this.unitsCollection.GetLength(1);
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0004183C File Offset: 0x0003FA3C
		public AlignUnit GetUnitFromPosition(int row, int column)
		{
			if (row >= 0 && row < this.RowCount && column >= 0 && column < this.ColumnCount)
			{
				return this.unitsCollection[row, column];
			}
			return null;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00041868 File Offset: 0x0003FA68
		public AlignUnit GetMaxUnitInRow(int row)
		{
			AlignUnit alignUnit = null;
			foreach (AlignUnit alignUnit2 in this.GetAlignUnitsInRow(row))
			{
				if (alignUnit == null || alignUnit2.CompareMargin.Vertical > alignUnit.CompareMargin.Vertical)
				{
					alignUnit = alignUnit2;
				}
			}
			return alignUnit;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000418D8 File Offset: 0x0003FAD8
		public AlignUnit GetMinUnitInRow(int row)
		{
			AlignUnit alignUnit = null;
			foreach (AlignUnit alignUnit2 in this.GetAlignUnitsInRow(row))
			{
				if (alignUnit == null || alignUnit2.CompareMargin.Vertical < alignUnit.CompareMargin.Vertical)
				{
					alignUnit = alignUnit2;
				}
			}
			return alignUnit;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00041948 File Offset: 0x0003FB48
		public IList<AlignUnit> GetAlignUnitsInRow(int row)
		{
			if (!this.rowUnitsDictionary.ContainsKey(row))
			{
				this.rowUnitsDictionary[row] = new List<AlignUnit>();
			}
			return this.rowUnitsDictionary[row];
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x00041975 File Offset: 0x0003FB75
		public IList<AlignUnit> Units
		{
			get
			{
				return this.unitsList;
			}
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0004197D File Offset: 0x0003FB7D
		public AlignUnit GetOffsetUnit(AlignUnit unit, int row, int col)
		{
			if (row > 0)
			{
				row += unit.RowSpan - 1;
			}
			if (col > 0)
			{
				col += unit.ColumnSpan - 1;
			}
			return this.GetUnitFromPosition(unit.Row + row, unit.Column + col);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x000419B8 File Offset: 0x0003FBB8
		public IList<AlignUnit> GetOffsetUnits(AlignUnit unit, int row, int col)
		{
			List<AlignUnit> list = new List<AlignUnit>();
			if (row > 0)
			{
				row += unit.RowSpan - 1;
			}
			if (col > 0)
			{
				col += unit.ColumnSpan - 1;
			}
			for (int i = 0; i < unit.ColumnSpan; i++)
			{
				AlignUnit unitFromPosition = this.GetUnitFromPosition(unit.Row + row, unit.Column + col + i);
				if (unitFromPosition != null && !list.Contains(unitFromPosition))
				{
					list.Add(unitFromPosition);
				}
			}
			return list;
		}

		// Token: 0x04000670 RID: 1648
		private AlignUnit[,] unitsCollection;

		// Token: 0x04000671 RID: 1649
		private IDictionary<int, List<AlignUnit>> rowUnitsDictionary = new Dictionary<int, List<AlignUnit>>();

		// Token: 0x04000672 RID: 1650
		private IList<AlignUnit> unitsList = new List<AlignUnit>();
	}
}
