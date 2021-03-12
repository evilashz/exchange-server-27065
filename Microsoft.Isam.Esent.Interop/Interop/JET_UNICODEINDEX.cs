using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002D5 RID: 725
	[Serializable]
	public sealed class JET_UNICODEINDEX : IContentEquatable<JET_UNICODEINDEX>, IDeepCloneable<JET_UNICODEINDEX>
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0001AB67 File Offset: 0x00018D67
		// (set) Token: 0x06000D40 RID: 3392 RVA: 0x0001AB6F File Offset: 0x00018D6F
		public int lcid
		{
			[DebuggerStepThrough]
			get
			{
				return this.localeId;
			}
			set
			{
				this.localeId = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0001AB78 File Offset: 0x00018D78
		// (set) Token: 0x06000D42 RID: 3394 RVA: 0x0001AB80 File Offset: 0x00018D80
		public string szLocaleName
		{
			[DebuggerStepThrough]
			get
			{
				return this.localeName;
			}
			set
			{
				this.localeName = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x0001AB89 File Offset: 0x00018D89
		// (set) Token: 0x06000D44 RID: 3396 RVA: 0x0001AB91 File Offset: 0x00018D91
		[CLSCompliant(false)]
		public uint dwMapFlags
		{
			[DebuggerStepThrough]
			get
			{
				return this.mapStringFlags;
			}
			set
			{
				this.mapStringFlags = value;
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0001AB9A File Offset: 0x00018D9A
		public JET_UNICODEINDEX DeepClone()
		{
			return (JET_UNICODEINDEX)base.MemberwiseClone();
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0001ABA8 File Offset: 0x00018DA8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_UNICODEINDEX({0}:{1}:0x{2:X})", new object[]
			{
				this.localeId,
				this.localeName,
				this.mapStringFlags
			});
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0001ABF1 File Offset: 0x00018DF1
		public bool ContentEquals(JET_UNICODEINDEX other)
		{
			return other != null && (this.localeId == other.localeId && this.mapStringFlags == other.mapStringFlags) && string.Compare(this.localeName, other.localeName, StringComparison.Ordinal) == 0;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0001AC2C File Offset: 0x00018E2C
		internal NATIVE_UNICODEINDEX GetNativeUnicodeIndex()
		{
			if (!string.IsNullOrEmpty(this.localeName))
			{
				throw new ArgumentException("localeName was specified, but this version of the API does not accept locale names. Use LCIDs or a different API.");
			}
			return new NATIVE_UNICODEINDEX
			{
				lcid = (uint)this.lcid,
				dwMapFlags = this.dwMapFlags
			};
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0001AC78 File Offset: 0x00018E78
		static JET_UNICODEINDEX()
		{
			JET_UNICODEINDEX.LcidToLocales.Add(1033, "en-us");
			JET_UNICODEINDEX.LcidToLocales.Add(1046, "pt-br");
			JET_UNICODEINDEX.LcidToLocales.Add(3084, "fr-ca");
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0001ACCD File Offset: 0x00018ECD
		public string GetEffectiveLocaleName()
		{
			if (!string.IsNullOrEmpty(this.szLocaleName))
			{
				return this.szLocaleName;
			}
			return JET_UNICODEINDEX.LimitedLcidToLocaleNameMapping(this.lcid);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0001ACF0 File Offset: 0x00018EF0
		internal static string LimitedLcidToLocaleNameMapping(int lcid)
		{
			string result;
			JET_UNICODEINDEX.LcidToLocales.TryGetValue(lcid, out result);
			return result;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0001AD0C File Offset: 0x00018F0C
		internal NATIVE_UNICODEINDEX2 GetNativeUnicodeIndex2()
		{
			if (this.lcid != 0 && !JET_UNICODEINDEX.LcidToLocales.ContainsKey(this.lcid))
			{
				throw new ArgumentException("lcid was specified, but this version of the API does not accept LCIDs. Use a locale name or a different API.");
			}
			return new NATIVE_UNICODEINDEX2
			{
				dwMapFlags = this.dwMapFlags
			};
		}

		// Token: 0x040008C5 RID: 2245
		private int localeId;

		// Token: 0x040008C6 RID: 2246
		private string localeName;

		// Token: 0x040008C7 RID: 2247
		private uint mapStringFlags;

		// Token: 0x040008C8 RID: 2248
		private static readonly Dictionary<int, string> LcidToLocales = new Dictionary<int, string>(10);
	}
}
