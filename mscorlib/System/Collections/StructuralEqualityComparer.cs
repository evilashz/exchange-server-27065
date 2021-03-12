using System;

namespace System.Collections
{
	// Token: 0x0200047B RID: 1147
	[Serializable]
	internal class StructuralEqualityComparer : IEqualityComparer
	{
		// Token: 0x060037DF RID: 14303 RVA: 0x000D5AEC File Offset: 0x000D3CEC
		public bool Equals(object x, object y)
		{
			if (x == null)
			{
				return y == null;
			}
			IStructuralEquatable structuralEquatable = x as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.Equals(y, this);
			}
			return y != null && x.Equals(y);
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000D5B24 File Offset: 0x000D3D24
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			IStructuralEquatable structuralEquatable = obj as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.GetHashCode(this);
			}
			return obj.GetHashCode();
		}
	}
}
