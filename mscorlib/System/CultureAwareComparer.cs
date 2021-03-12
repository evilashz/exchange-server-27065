using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000076 RID: 118
	[Serializable]
	internal sealed class CultureAwareComparer : StringComparer, IWellKnownStringEqualityComparer
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x000139AF File Offset: 0x00011BAF
		internal CultureAwareComparer(CultureInfo culture, bool ignoreCase)
		{
			this._compareInfo = culture.CompareInfo;
			this._ignoreCase = ignoreCase;
			this._options = (ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000139D7 File Offset: 0x00011BD7
		internal CultureAwareComparer(CompareInfo compareInfo, bool ignoreCase)
		{
			this._compareInfo = compareInfo;
			this._ignoreCase = ignoreCase;
			this._options = (ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000139FA File Offset: 0x00011BFA
		internal CultureAwareComparer(CompareInfo compareInfo, CompareOptions options)
		{
			this._compareInfo = compareInfo;
			this._options = options;
			this._ignoreCase = ((options & CompareOptions.IgnoreCase) == CompareOptions.IgnoreCase || (options & CompareOptions.OrdinalIgnoreCase) == CompareOptions.OrdinalIgnoreCase);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00013A2D File Offset: 0x00011C2D
		public override int Compare(string x, string y)
		{
			this.EnsureInitialization();
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
			return this._compareInfo.Compare(x, y, this._options);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00013A58 File Offset: 0x00011C58
		public override bool Equals(string x, string y)
		{
			this.EnsureInitialization();
			return x == y || (x != null && y != null && this._compareInfo.Compare(x, y, this._options) == 0);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00013A84 File Offset: 0x00011C84
		public override int GetHashCode(string obj)
		{
			this.EnsureInitialization();
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return this._compareInfo.GetHashCodeOfString(obj, this._options);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00013AAC File Offset: 0x00011CAC
		public override bool Equals(object obj)
		{
			this.EnsureInitialization();
			CultureAwareComparer cultureAwareComparer = obj as CultureAwareComparer;
			return cultureAwareComparer != null && this._ignoreCase == cultureAwareComparer._ignoreCase && this._compareInfo.Equals(cultureAwareComparer._compareInfo) && this._options == cultureAwareComparer._options;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00013B00 File Offset: 0x00011D00
		public override int GetHashCode()
		{
			this.EnsureInitialization();
			int hashCode = this._compareInfo.GetHashCode();
			if (!this._ignoreCase)
			{
				return hashCode;
			}
			return ~hashCode;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00013B2B File Offset: 0x00011D2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void EnsureInitialization()
		{
			if (this._initializing)
			{
				this._options |= (this._ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
				this._initializing = false;
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00013B55 File Offset: 0x00011D55
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this._initializing = true;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00013B5E File Offset: 0x00011D5E
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.EnsureInitialization();
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00013B66 File Offset: 0x00011D66
		IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
		{
			return new CultureAwareRandomizedComparer(this._compareInfo, this._ignoreCase);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00013B79 File Offset: 0x00011D79
		IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
		{
			return this;
		}

		// Token: 0x04000292 RID: 658
		private CompareInfo _compareInfo;

		// Token: 0x04000293 RID: 659
		private bool _ignoreCase;

		// Token: 0x04000294 RID: 660
		[OptionalField]
		private CompareOptions _options;

		// Token: 0x04000295 RID: 661
		[NonSerialized]
		private bool _initializing;
	}
}
