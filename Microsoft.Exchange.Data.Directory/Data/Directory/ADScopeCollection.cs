using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000188 RID: 392
	[Serializable]
	internal class ADScopeCollection : ReadOnlyCollection<ADScope>
	{
		// Token: 0x06001082 RID: 4226 RVA: 0x0004F9C2 File Offset: 0x0004DBC2
		internal ADScopeCollection(IList<ADScope> aDScopes) : base(aDScopes)
		{
			if (aDScopes == null)
			{
				throw new ArgumentNullException("adScopes");
			}
			if (aDScopes.Count == 0)
			{
				throw new ArgumentException("adScopes");
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0004F9EC File Offset: 0x0004DBEC
		private ADScopeCollection() : base(new ADScope[0])
		{
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0004F9FC File Offset: 0x0004DBFC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < base.Count - 1; i++)
			{
				if (base[i] != null)
				{
					stringBuilder.Append(base[i].ToString());
					stringBuilder.Append(",");
				}
			}
			if (base.Count != 0 && base[base.Count - 1] != null)
			{
				stringBuilder.Append(base[base.Count - 1].ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000968 RID: 2408
		internal static readonly ADScopeCollection Empty = new ADScopeCollection();

		// Token: 0x04000969 RID: 2409
		internal static readonly ADScopeCollection[] EmptyArray = new ADScopeCollection[0];
	}
}
