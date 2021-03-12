using System;
using System.IO;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000193 RID: 403
	[Serializable]
	public sealed class ScriptRoleEntry : RoleEntry, IEquatable<ScriptRoleEntry>
	{
		// Token: 0x06000D19 RID: 3353 RVA: 0x00028FCC File Offset: 0x000271CC
		internal ScriptRoleEntry(string name, string[] parameters) : base(name, parameters)
		{
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00028FD8 File Offset: 0x000271D8
		internal ScriptRoleEntry(string entryString)
		{
			int paramIndex = base.ExtractAndSetName(entryString);
			if (-1 != base.Name.IndexOfAny(ScriptRoleEntry.invalidFileNameChars))
			{
				throw new FormatException(DataStrings.ScriptRoleEntryNameInvalidException(base.Name));
			}
			base.ExtractAndSetParameters(entryString, paramIndex);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00029024 File Offset: 0x00027224
		internal ScriptRoleEntry()
		{
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0002902C File Offset: 0x0002722C
		public override string ToADString()
		{
			return base.ToADString('s', null, null);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00029038 File Offset: 0x00027238
		public bool Equals(ScriptRoleEntry other)
		{
			return base.Equals(other);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00029041 File Offset: 0x00027241
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ScriptRoleEntry);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0002904F File Offset: 0x0002724F
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040007EE RID: 2030
		internal const char TypeHint = 's';

		// Token: 0x040007EF RID: 2031
		private static readonly char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
	}
}
