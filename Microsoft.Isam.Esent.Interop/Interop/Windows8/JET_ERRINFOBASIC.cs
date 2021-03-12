using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Windows8
{
	// Token: 0x0200028A RID: 650
	[Serializable]
	public sealed class JET_ERRINFOBASIC : IContentEquatable<JET_ERRINFOBASIC>, IDeepCloneable<JET_ERRINFOBASIC>
	{
		// Token: 0x06000B6B RID: 2923 RVA: 0x000171A4 File Offset: 0x000153A4
		public JET_ERRINFOBASIC()
		{
			this.rgCategoricalHierarchy = new JET_ERRCAT[8];
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x000171B8 File Offset: 0x000153B8
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x000171C0 File Offset: 0x000153C0
		public JET_err errValue
		{
			[DebuggerStepThrough]
			get
			{
				return this.errorValue;
			}
			set
			{
				this.errorValue = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x000171C9 File Offset: 0x000153C9
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x000171D1 File Offset: 0x000153D1
		public JET_ERRCAT errcat
		{
			[DebuggerStepThrough]
			get
			{
				return this.errorcatMostSpecific;
			}
			set
			{
				this.errorcatMostSpecific = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x000171DA File Offset: 0x000153DA
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x000171E2 File Offset: 0x000153E2
		public JET_ERRCAT[] rgCategoricalHierarchy
		{
			[DebuggerStepThrough]
			get
			{
				return this.arrayCategoricalHierarchy;
			}
			set
			{
				this.arrayCategoricalHierarchy = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x000171EB File Offset: 0x000153EB
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x000171F3 File Offset: 0x000153F3
		public int lSourceLine
		{
			[DebuggerStepThrough]
			get
			{
				return this.sourceLine;
			}
			set
			{
				this.sourceLine = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x000171FC File Offset: 0x000153FC
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x00017204 File Offset: 0x00015404
		public string rgszSourceFile
		{
			[DebuggerStepThrough]
			get
			{
				return this.sourceFile;
			}
			set
			{
				this.sourceFile = value;
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00017210 File Offset: 0x00015410
		public JET_ERRINFOBASIC DeepClone()
		{
			return (JET_ERRINFOBASIC)base.MemberwiseClone();
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0001722C File Offset: 0x0001542C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_ERRINFOBASIC({0}:{1}:{2}:{3})", new object[]
			{
				this.errValue,
				this.errcat,
				this.rgszSourceFile,
				this.lSourceLine
			});
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00017284 File Offset: 0x00015484
		public bool ContentEquals(JET_ERRINFOBASIC other)
		{
			return other != null && (this.errValue == other.errValue && this.errcat == other.errcat && this.lSourceLine == other.lSourceLine && this.rgszSourceFile == other.rgszSourceFile) && Util.ArrayStructEquals<JET_ERRCAT>(this.rgCategoricalHierarchy, other.rgCategoricalHierarchy, (this.rgCategoricalHierarchy == null) ? 0 : this.rgCategoricalHierarchy.Length);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000172FC File Offset: 0x000154FC
		internal NATIVE_ERRINFOBASIC GetNativeErrInfo()
		{
			NATIVE_ERRINFOBASIC result = default(NATIVE_ERRINFOBASIC);
			result.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_ERRINFOBASIC)));
			result.errValue = this.errValue;
			result.errcatMostSpecific = this.errcat;
			result.rgCategoricalHierarchy = new byte[8];
			if (this.rgCategoricalHierarchy != null)
			{
				for (int i = 0; i < this.rgCategoricalHierarchy.Length; i++)
				{
					result.rgCategoricalHierarchy[i] = (byte)this.rgCategoricalHierarchy[i];
				}
			}
			result.lSourceLine = (uint)this.lSourceLine;
			result.rgszSourceFile = this.rgszSourceFile;
			return result;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00017398 File Offset: 0x00015598
		internal void SetFromNative(ref NATIVE_ERRINFOBASIC value)
		{
			this.errValue = value.errValue;
			this.errcat = value.errcatMostSpecific;
			if (value.rgCategoricalHierarchy != null)
			{
				for (int i = 0; i < value.rgCategoricalHierarchy.Length; i++)
				{
					this.rgCategoricalHierarchy[i] = (JET_ERRCAT)value.rgCategoricalHierarchy[i];
				}
			}
			this.lSourceLine = (int)value.lSourceLine;
			this.rgszSourceFile = value.rgszSourceFile;
		}

		// Token: 0x040006C8 RID: 1736
		private JET_err errorValue;

		// Token: 0x040006C9 RID: 1737
		private JET_ERRCAT errorcatMostSpecific;

		// Token: 0x040006CA RID: 1738
		private JET_ERRCAT[] arrayCategoricalHierarchy;

		// Token: 0x040006CB RID: 1739
		private int sourceLine;

		// Token: 0x040006CC RID: 1740
		private string sourceFile;
	}
}
