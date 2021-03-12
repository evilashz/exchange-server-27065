using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001A3 RID: 419
	public class RelativeControlAlignRule : IAlignRule
	{
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x00041A29 File Offset: 0x0003FC29
		public IList<RelativeControlAlignRule.Condition> Conditions
		{
			get
			{
				return this.conditionsList;
			}
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00041A31 File Offset: 0x0003FC31
		public RelativeControlAlignRule(int value, IList<RelativeControlAlignRule.Condition> list)
		{
			this.deltaValue = value;
			this.conditionsList = list;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00041A47 File Offset: 0x0003FC47
		public RelativeControlAlignRule(int value) : this(value, new List<RelativeControlAlignRule.Condition>())
		{
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00041A58 File Offset: 0x0003FC58
		public void Apply(AlignUnitsCollection units)
		{
			foreach (AlignUnit alignUnit in units.Units)
			{
				if (units.RowDeltaValue[alignUnit.Row] > this.deltaValue && this.Match(alignUnit, units))
				{
					units.RowDeltaValue[alignUnit.Row] = this.deltaValue;
				}
			}
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00041AD0 File Offset: 0x0003FCD0
		private bool Match(AlignUnit unit, AlignUnitsCollection collection)
		{
			foreach (RelativeControlAlignRule.Condition condition in this.conditionsList)
			{
				AlignUnit offsetUnit = collection.GetOffsetUnit(unit, condition.OffsetRow, condition.OffSetColumn);
				if (!this.IsConditionMatch(condition, offsetUnit))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00041B3C File Offset: 0x0003FD3C
		private bool IsConditionMatch(RelativeControlAlignRule.Condition condition, AlignUnit unit)
		{
			foreach (Type type in condition.ExcludedTypes)
			{
				if (this.IsTypeMatch(type, unit))
				{
					return false;
				}
			}
			foreach (Type type2 in condition.IncludedTypes)
			{
				if (this.IsTypeMatch(type2, unit))
				{
					return true;
				}
			}
			return condition.IncludedTypes.Count == 0;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00041BE8 File Offset: 0x0003FDE8
		private bool IsTypeMatch(Type type, AlignUnit unit)
		{
			if (!(type == null))
			{
				return unit != null && type.IsAssignableFrom(unit.Control.GetType());
			}
			return unit == null;
		}

		// Token: 0x04000674 RID: 1652
		private IList<RelativeControlAlignRule.Condition> conditionsList;

		// Token: 0x04000675 RID: 1653
		private int deltaValue;

		// Token: 0x020001A4 RID: 420
		public class Condition
		{
			// Token: 0x170003F0 RID: 1008
			// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00041C0E File Offset: 0x0003FE0E
			// (set) Token: 0x060010A5 RID: 4261 RVA: 0x00041C16 File Offset: 0x0003FE16
			public int OffsetRow { get; private set; }

			// Token: 0x170003F1 RID: 1009
			// (get) Token: 0x060010A6 RID: 4262 RVA: 0x00041C1F File Offset: 0x0003FE1F
			// (set) Token: 0x060010A7 RID: 4263 RVA: 0x00041C27 File Offset: 0x0003FE27
			public int OffSetColumn { get; private set; }

			// Token: 0x170003F2 RID: 1010
			// (get) Token: 0x060010A8 RID: 4264 RVA: 0x00041C30 File Offset: 0x0003FE30
			// (set) Token: 0x060010A9 RID: 4265 RVA: 0x00041C38 File Offset: 0x0003FE38
			public IList<Type> IncludedTypes { get; private set; }

			// Token: 0x170003F3 RID: 1011
			// (get) Token: 0x060010AA RID: 4266 RVA: 0x00041C41 File Offset: 0x0003FE41
			// (set) Token: 0x060010AB RID: 4267 RVA: 0x00041C49 File Offset: 0x0003FE49
			public IList<Type> ExcludedTypes { get; private set; }

			// Token: 0x060010AC RID: 4268 RVA: 0x00041C52 File Offset: 0x0003FE52
			public Condition(int row, int column, IList<Type> includedTypes, IList<Type> excludedTypes)
			{
				this.OffSetColumn = column;
				this.OffsetRow = row;
				this.IncludedTypes = includedTypes;
				this.ExcludedTypes = excludedTypes;
			}

			// Token: 0x060010AD RID: 4269 RVA: 0x00041C77 File Offset: 0x0003FE77
			public Condition(int row, int column, IList<Type> includedTypes) : this(row, column, includedTypes, new List<Type>())
			{
			}
		}
	}
}
