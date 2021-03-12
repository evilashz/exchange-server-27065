using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000229 RID: 553
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct RowEntry
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0003046F File Offset: 0x0002E66F
		public RowEntry.RowOp RowFlags
		{
			get
			{
				return this.rowOp;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00030477 File Offset: 0x0002E677
		public ICollection<PropValue> Values
		{
			get
			{
				return this.propValues;
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0003047F File Offset: 0x0002E67F
		public static RowEntry Add(ICollection<PropValue> propValues)
		{
			return new RowEntry(RowEntry.RowOp.Add, propValues);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00030488 File Offset: 0x0002E688
		public static RowEntry Modify(ICollection<PropValue> propValues)
		{
			return new RowEntry(RowEntry.RowOp.Modify, propValues);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00030491 File Offset: 0x0002E691
		public static RowEntry Remove(ICollection<PropValue> propValues)
		{
			return new RowEntry(RowEntry.RowOp.Remove, propValues);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0003049A File Offset: 0x0002E69A
		public static RowEntry Empty()
		{
			return new RowEntry(RowEntry.RowOp.Empty, Array<PropValue>.Empty);
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x000304A7 File Offset: 0x0002E6A7
		internal bool IsEmpty
		{
			get
			{
				return RowEntry.RowOp.Empty == this.rowOp && 0 == this.propValues.Count;
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x000304C2 File Offset: 0x0002E6C2
		private RowEntry(RowEntry.RowOp rowOp, ICollection<PropValue> propValues)
		{
			this.rowOp = rowOp;
			this.propValues = propValues;
		}

		// Token: 0x04000FB6 RID: 4022
		private readonly RowEntry.RowOp rowOp;

		// Token: 0x04000FB7 RID: 4023
		private readonly ICollection<PropValue> propValues;

		// Token: 0x0200022A RID: 554
		public enum RowOp
		{
			// Token: 0x04000FB9 RID: 4025
			Add = 1,
			// Token: 0x04000FBA RID: 4026
			Modify,
			// Token: 0x04000FBB RID: 4027
			Remove = 4,
			// Token: 0x04000FBC RID: 4028
			Empty
		}
	}
}
