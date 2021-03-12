using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002B6 RID: 694
	[Serializable]
	public sealed class JET_RECPOS : IContentEquatable<JET_RECPOS>, IDeepCloneable<JET_RECPOS>
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00018C1F File Offset: 0x00016E1F
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x00018C27 File Offset: 0x00016E27
		public long centriesLT
		{
			[DebuggerStepThrough]
			get
			{
				return this.entriesBeforeKey;
			}
			set
			{
				this.entriesBeforeKey = value;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00018C30 File Offset: 0x00016E30
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x00018C38 File Offset: 0x00016E38
		public long centriesTotal
		{
			[DebuggerStepThrough]
			get
			{
				return this.totalEntries;
			}
			set
			{
				this.totalEntries = value;
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00018C41 File Offset: 0x00016E41
		public JET_RECPOS DeepClone()
		{
			return (JET_RECPOS)base.MemberwiseClone();
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00018C50 File Offset: 0x00016E50
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_RECPOS({0}/{1})", new object[]
			{
				this.entriesBeforeKey,
				this.totalEntries
			});
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00018C90 File Offset: 0x00016E90
		public bool ContentEquals(JET_RECPOS other)
		{
			return other != null && this.entriesBeforeKey == other.entriesBeforeKey && this.totalEntries == other.totalEntries;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00018CB8 File Offset: 0x00016EB8
		internal NATIVE_RECPOS GetNativeRecpos()
		{
			return checked(new NATIVE_RECPOS
			{
				cbStruct = (uint)NATIVE_RECPOS.Size,
				centriesLT = (uint)this.centriesLT,
				centriesTotal = (uint)this.centriesTotal
			});
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00018CF7 File Offset: 0x00016EF7
		internal void SetFromNativeRecpos(NATIVE_RECPOS value)
		{
			this.centriesLT = (long)(checked((int)value.centriesLT));
			this.centriesTotal = (long)(checked((int)value.centriesTotal));
		}

		// Token: 0x040007EC RID: 2028
		private long entriesBeforeKey;

		// Token: 0x040007ED RID: 2029
		private long totalEntries;
	}
}
