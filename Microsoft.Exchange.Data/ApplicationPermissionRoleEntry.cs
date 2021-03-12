using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000194 RID: 404
	[Serializable]
	public sealed class ApplicationPermissionRoleEntry : RoleEntry, IEquatable<ApplicationPermissionRoleEntry>
	{
		// Token: 0x06000D21 RID: 3361 RVA: 0x00029063 File Offset: 0x00027263
		internal ApplicationPermissionRoleEntry(string name, string[] parameters) : base(name, parameters)
		{
			if (parameters != null && parameters.Length > 0)
			{
				throw new FormatException(DataStrings.ApplicationPermissionRoleEntryParameterNotEmptyException(base.Name));
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0002908C File Offset: 0x0002728C
		internal ApplicationPermissionRoleEntry(string entryString)
		{
			int num = base.ExtractAndSetName(entryString);
			if (num > 0)
			{
				throw new FormatException(DataStrings.ApplicationPermissionRoleEntryParameterNotEmptyException(base.Name));
			}
			base.ExtractAndSetParameters(entryString, num);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000290C9 File Offset: 0x000272C9
		internal ApplicationPermissionRoleEntry()
		{
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000290D1 File Offset: 0x000272D1
		public override string ToADString()
		{
			return base.ToADString('a', null, null);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x000290DD File Offset: 0x000272DD
		public bool Equals(ApplicationPermissionRoleEntry other)
		{
			return base.Equals(other);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000290E6 File Offset: 0x000272E6
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ApplicationPermissionRoleEntry);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x000290F4 File Offset: 0x000272F4
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040007F0 RID: 2032
		internal const char TypeHint = 'a';
	}
}
