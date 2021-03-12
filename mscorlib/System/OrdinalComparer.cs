using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x02000078 RID: 120
	[Serializable]
	internal sealed class OrdinalComparer : StringComparer, IWellKnownStringEqualityComparer
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x00013CDB File Offset: 0x00011EDB
		internal OrdinalComparer(bool ignoreCase)
		{
			this._ignoreCase = ignoreCase;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00013CEA File Offset: 0x00011EEA
		public override int Compare(string x, string y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			if (this._ignoreCase)
			{
				return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
			}
			return string.CompareOrdinal(x, y);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00013D14 File Offset: 0x00011F14
		public override bool Equals(string x, string y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (this._ignoreCase)
			{
				return x.Length == y.Length && string.Compare(x, y, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return x.Equals(y);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00013D4F File Offset: 0x00011F4F
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._ignoreCase)
			{
				return TextInfo.GetHashCodeOrdinalIgnoreCase(obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00013D74 File Offset: 0x00011F74
		public override bool Equals(object obj)
		{
			OrdinalComparer ordinalComparer = obj as OrdinalComparer;
			return ordinalComparer != null && this._ignoreCase == ordinalComparer._ignoreCase;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00013D9C File Offset: 0x00011F9C
		public override int GetHashCode()
		{
			string text = "OrdinalComparer";
			int hashCode = text.GetHashCode();
			if (!this._ignoreCase)
			{
				return hashCode;
			}
			return ~hashCode;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00013DC2 File Offset: 0x00011FC2
		IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
		{
			return new OrdinalRandomizedComparer(this._ignoreCase);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00013DCF File Offset: 0x00011FCF
		IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
		{
			return this;
		}

		// Token: 0x04000299 RID: 665
		private bool _ignoreCase;
	}
}
