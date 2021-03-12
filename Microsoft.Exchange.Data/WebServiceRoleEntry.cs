using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000196 RID: 406
	[Serializable]
	public sealed class WebServiceRoleEntry : RoleEntry, IEquatable<WebServiceRoleEntry>
	{
		// Token: 0x06000D2E RID: 3374 RVA: 0x00029163 File Offset: 0x00027363
		internal WebServiceRoleEntry(string name, string[] parameters) : base(name, parameters)
		{
			if (parameters != null && parameters.Length > 0)
			{
				throw new FormatException(DataStrings.WebServiceRoleEntryParameterNotEmptyException(base.Name));
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0002918C File Offset: 0x0002738C
		internal WebServiceRoleEntry(string entryString)
		{
			int num = base.ExtractAndSetName(entryString);
			if (num > 0)
			{
				throw new FormatException(DataStrings.WebServiceRoleEntryParameterNotEmptyException(base.Name));
			}
			base.ExtractAndSetParameters(entryString, num);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x000291C9 File Offset: 0x000273C9
		internal WebServiceRoleEntry()
		{
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x000291D1 File Offset: 0x000273D1
		public override string ToADString()
		{
			return base.ToADString('w', null, null);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x000291DD File Offset: 0x000273DD
		public bool Equals(WebServiceRoleEntry other)
		{
			return base.Equals(other);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000291E6 File Offset: 0x000273E6
		public override bool Equals(object obj)
		{
			return this.Equals(obj as WebServiceRoleEntry);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x000291F4 File Offset: 0x000273F4
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040007F2 RID: 2034
		internal const char TypeHint = 'w';
	}
}
