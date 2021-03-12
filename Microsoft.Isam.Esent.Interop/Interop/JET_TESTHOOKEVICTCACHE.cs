using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Isam.Esent.Interop.Unpublished;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	public class JET_TESTHOOKEVICTCACHE : IContentEquatable<JET_TESTHOOKEVICTCACHE>, IDeepCloneable<JET_TESTHOOKEVICTCACHE>
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000BAF4 File Offset: 0x00009CF4
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x0000BAFC File Offset: 0x00009CFC
		public JET_DBID ulTargetContext
		{
			[DebuggerStepThrough]
			get
			{
				return this.targetContext;
			}
			set
			{
				this.targetContext = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000BB05 File Offset: 0x00009D05
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x0000BB0D File Offset: 0x00009D0D
		public int ulTargetData
		{
			[DebuggerStepThrough]
			get
			{
				return this.targetData;
			}
			set
			{
				this.targetData = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000BB16 File Offset: 0x00009D16
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0000BB1E File Offset: 0x00009D1E
		public EvictCacheGrbit grbit
		{
			[DebuggerStepThrough]
			get
			{
				return this.evictGrbit;
			}
			set
			{
				this.evictGrbit = value;
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000BB28 File Offset: 0x00009D28
		public bool ContentEquals(JET_TESTHOOKEVICTCACHE other)
		{
			return other != null && (this.ulTargetContext == other.ulTargetContext && this.ulTargetData == other.ulTargetData) && this.grbit == other.grbit;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000BB70 File Offset: 0x00009D70
		public JET_TESTHOOKEVICTCACHE DeepClone()
		{
			return (JET_TESTHOOKEVICTCACHE)base.MemberwiseClone();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000BB8C File Offset: 0x00009D8C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_TESTHOOKEVICTCACHE({0}:{1}:{2})", new object[]
			{
				this.ulTargetContext,
				this.ulTargetData,
				this.grbit
			});
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000BBDC File Offset: 0x00009DDC
		internal NATIVE_TESTHOOKEVICTCACHE GetNativeTestHookEvictCache()
		{
			return new NATIVE_TESTHOOKEVICTCACHE
			{
				cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_TESTHOOKEVICTCACHE))),
				ulTargetContext = (IntPtr)((long)((ulong)this.ulTargetContext.Value)),
				ulTargetData = (IntPtr)this.ulTargetData,
				grbit = checked((uint)this.grbit)
			};
		}

		// Token: 0x040001E8 RID: 488
		private JET_DBID targetContext;

		// Token: 0x040001E9 RID: 489
		private int targetData;

		// Token: 0x040001EA RID: 490
		private EvictCacheGrbit evictGrbit;
	}
}
