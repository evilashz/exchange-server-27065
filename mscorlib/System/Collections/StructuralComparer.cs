using System;

namespace System.Collections
{
	// Token: 0x0200047C RID: 1148
	[Serializable]
	internal class StructuralComparer : IComparer
	{
		// Token: 0x060037E2 RID: 14306 RVA: 0x000D5B58 File Offset: 0x000D3D58
		public int Compare(object x, object y)
		{
			if (x == null)
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				IStructuralComparable structuralComparable = x as IStructuralComparable;
				if (structuralComparable != null)
				{
					return structuralComparable.CompareTo(y, this);
				}
				return Comparer.Default.Compare(x, y);
			}
		}
	}
}
