using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Isam.Esent.Interop.Unpublished;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public class JET_TESTHOOKTESTINJECTION : IContentEquatable<JET_TESTHOOKTESTINJECTION>, IDeepCloneable<JET_TESTHOOKTESTINJECTION>
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000BC49 File Offset: 0x00009E49
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x0000BC51 File Offset: 0x00009E51
		public uint ulID
		{
			[DebuggerStepThrough]
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000BC5A File Offset: 0x00009E5A
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x0000BC62 File Offset: 0x00009E62
		public IntPtr pv
		{
			[DebuggerStepThrough]
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0000BC6B File Offset: 0x00009E6B
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0000BC73 File Offset: 0x00009E73
		public TestHookInjectionType type
		{
			[DebuggerStepThrough]
			get
			{
				return this.injectionType;
			}
			set
			{
				this.injectionType = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000BC7C File Offset: 0x00009E7C
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0000BC84 File Offset: 0x00009E84
		public uint ulProbability
		{
			[DebuggerStepThrough]
			get
			{
				return this.injectionProbability;
			}
			set
			{
				this.injectionProbability = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000BC8D File Offset: 0x00009E8D
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0000BC95 File Offset: 0x00009E95
		public TestInjectionGrbit grbit
		{
			[DebuggerStepThrough]
			get
			{
				return this.injectionGrbit;
			}
			set
			{
				this.injectionGrbit = value;
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000BCA0 File Offset: 0x00009EA0
		public bool ContentEquals(JET_TESTHOOKTESTINJECTION other)
		{
			if (other == null)
			{
				return false;
			}
			bool flag = this.ulID == other.ulID && this.type == other.type && this.ulProbability == other.ulProbability && this.grbit == other.grbit;
			if (flag)
			{
				switch (this.type)
				{
				case TestHookInjectionType.Fault:
				case TestHookInjectionType.ConfigOverride:
					flag = (this.pv == other.pv);
					break;
				}
			}
			return flag;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000BD20 File Offset: 0x00009F20
		public JET_TESTHOOKTESTINJECTION DeepClone()
		{
			JET_TESTHOOKTESTINJECTION jet_TESTHOOKTESTINJECTION = (JET_TESTHOOKTESTINJECTION)base.MemberwiseClone();
			switch (this.type)
			{
			case TestHookInjectionType.Fault:
			case TestHookInjectionType.ConfigOverride:
				jet_TESTHOOKTESTINJECTION.pv = this.pv;
				break;
			}
			return jet_TESTHOOKTESTINJECTION;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000BD60 File Offset: 0x00009F60
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_TESTHOOKTESTINJECTION({0}:{1}:{2})", new object[]
			{
				this.ulID,
				this.type,
				this.pv
			});
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000BDB0 File Offset: 0x00009FB0
		internal NATIVE_TESTHOOKTESTINJECTION GetNativeTestHookInjection()
		{
			return new NATIVE_TESTHOOKTESTINJECTION
			{
				cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_TESTHOOKTESTINJECTION))),
				ulID = this.ulID,
				pv = this.pv,
				type = (int)this.type,
				ulProbability = this.ulProbability,
				grbit = (uint)this.grbit
			};
		}

		// Token: 0x040001F1 RID: 497
		private uint id;

		// Token: 0x040001F2 RID: 498
		private IntPtr context;

		// Token: 0x040001F3 RID: 499
		private TestHookInjectionType injectionType;

		// Token: 0x040001F4 RID: 500
		private uint injectionProbability;

		// Token: 0x040001F5 RID: 501
		private TestInjectionGrbit injectionGrbit;
	}
}
