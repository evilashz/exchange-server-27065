using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000195 RID: 405
	[Serializable]
	public sealed class UnknownRoleEntry : RoleEntry, IEquatable<UnknownRoleEntry>
	{
		// Token: 0x06000D28 RID: 3368 RVA: 0x000290FC File Offset: 0x000272FC
		internal UnknownRoleEntry(string entryString)
		{
			this.typeHint = entryString[0];
			int paramIndex = base.ExtractAndSetName(entryString);
			base.ExtractAndSetParameters(entryString, paramIndex);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0002912C File Offset: 0x0002732C
		internal UnknownRoleEntry()
		{
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00029134 File Offset: 0x00027334
		public override string ToADString()
		{
			return base.ToADString(this.typeHint, null, null);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00029144 File Offset: 0x00027344
		public bool Equals(UnknownRoleEntry other)
		{
			return base.Equals(other);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0002914D File Offset: 0x0002734D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UnknownRoleEntry);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0002915B File Offset: 0x0002735B
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040007F1 RID: 2033
		private readonly char typeHint;
	}
}
