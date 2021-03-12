using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000298 RID: 664
	[Serializable]
	public sealed class JET_INDEXCREATE : IContentEquatable<JET_INDEXCREATE>, IDeepCloneable<JET_INDEXCREATE>
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x00017606 File Offset: 0x00015806
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x0001760E File Offset: 0x0001580E
		public JET_err err
		{
			[DebuggerStepThrough]
			get
			{
				return this.errorCode;
			}
			set
			{
				this.errorCode = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x00017617 File Offset: 0x00015817
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x0001761F File Offset: 0x0001581F
		public string szIndexName
		{
			[DebuggerStepThrough]
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x00017628 File Offset: 0x00015828
		// (set) Token: 0x06000B92 RID: 2962 RVA: 0x00017630 File Offset: 0x00015830
		public string szKey
		{
			[DebuggerStepThrough]
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00017639 File Offset: 0x00015839
		// (set) Token: 0x06000B94 RID: 2964 RVA: 0x00017641 File Offset: 0x00015841
		public int cbKey
		{
			[DebuggerStepThrough]
			get
			{
				return this.keyLength;
			}
			set
			{
				this.keyLength = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0001764A File Offset: 0x0001584A
		// (set) Token: 0x06000B96 RID: 2966 RVA: 0x00017652 File Offset: 0x00015852
		public CreateIndexGrbit grbit
		{
			[DebuggerStepThrough]
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0001765B File Offset: 0x0001585B
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x00017663 File Offset: 0x00015863
		public int ulDensity
		{
			[DebuggerStepThrough]
			get
			{
				return this.density;
			}
			set
			{
				this.density = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0001766C File Offset: 0x0001586C
		// (set) Token: 0x06000B9A RID: 2970 RVA: 0x00017674 File Offset: 0x00015874
		public JET_UNICODEINDEX pidxUnicode
		{
			[DebuggerStepThrough]
			get
			{
				return this.unicodeOptions;
			}
			set
			{
				this.unicodeOptions = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0001767D File Offset: 0x0001587D
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x00017685 File Offset: 0x00015885
		public int cbVarSegMac
		{
			[DebuggerStepThrough]
			get
			{
				return this.maxSegmentLength;
			}
			set
			{
				this.maxSegmentLength = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0001768E File Offset: 0x0001588E
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x00017696 File Offset: 0x00015896
		public JET_CONDITIONALCOLUMN[] rgconditionalcolumn
		{
			[DebuggerStepThrough]
			get
			{
				return this.conditionalColumns;
			}
			set
			{
				this.conditionalColumns = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0001769F File Offset: 0x0001589F
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x000176A7 File Offset: 0x000158A7
		public int cConditionalColumn
		{
			[DebuggerStepThrough]
			get
			{
				return this.numConditionalColumns;
			}
			set
			{
				this.numConditionalColumns = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x000176B0 File Offset: 0x000158B0
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x000176B8 File Offset: 0x000158B8
		public int cbKeyMost
		{
			[DebuggerStepThrough]
			get
			{
				return this.maximumKeyLength;
			}
			set
			{
				this.maximumKeyLength = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x000176C1 File Offset: 0x000158C1
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x000176C9 File Offset: 0x000158C9
		public JET_SPACEHINTS pSpaceHints
		{
			[DebuggerStepThrough]
			get
			{
				return this.spaceHints;
			}
			set
			{
				this.spaceHints = value;
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000176D4 File Offset: 0x000158D4
		public JET_INDEXCREATE DeepClone()
		{
			JET_INDEXCREATE jet_INDEXCREATE = (JET_INDEXCREATE)base.MemberwiseClone();
			jet_INDEXCREATE.pidxUnicode = ((this.pidxUnicode == null) ? null : this.pidxUnicode.DeepClone());
			this.conditionalColumns = Util.DeepCloneArray<JET_CONDITIONALCOLUMN>(this.conditionalColumns);
			return jet_INDEXCREATE;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0001771C File Offset: 0x0001591C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_INDEXCREATE({0}:{1})", new object[]
			{
				this.szIndexName,
				this.szKey
			});
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00017754 File Offset: 0x00015954
		public bool ContentEquals(JET_INDEXCREATE other)
		{
			if (other == null)
			{
				return false;
			}
			this.CheckMembersAreValid();
			other.CheckMembersAreValid();
			return this.err == other.err && this.szIndexName == other.szIndexName && this.szKey == other.szKey && this.cbKey == other.cbKey && this.grbit == other.grbit && this.ulDensity == other.ulDensity && this.cbVarSegMac == other.cbVarSegMac && this.cbKeyMost == other.cbKeyMost && this.IsUnicodeIndexEqual(other) && this.AreConditionalColumnsEqual(other);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00017800 File Offset: 0x00015A00
		internal void CheckMembersAreValid()
		{
			if (this.szIndexName == null)
			{
				throw new ArgumentNullException("szIndexName");
			}
			if (this.szKey == null)
			{
				throw new ArgumentNullException("szKey");
			}
			if (this.cbKey > checked(this.szKey.Length + 1))
			{
				throw new ArgumentOutOfRangeException("cbKey", this.cbKey, "cannot be greater than the length of szKey");
			}
			if (this.cbKey < 0)
			{
				throw new ArgumentOutOfRangeException("cbKey", this.cbKey, "cannot be negative");
			}
			if (this.ulDensity < 0)
			{
				throw new ArgumentOutOfRangeException("ulDensity", this.ulDensity, "cannot be negative");
			}
			if (this.cbKeyMost < 0)
			{
				throw new ArgumentOutOfRangeException("cbKeyMost", this.cbKeyMost, "cannot be negative");
			}
			if (this.cbVarSegMac < 0)
			{
				throw new ArgumentOutOfRangeException("cbVarSegMac", this.cbVarSegMac, "cannot be negative");
			}
			if ((this.cConditionalColumn > 0 && this.rgconditionalcolumn == null) || (this.cConditionalColumn > 0 && this.cConditionalColumn > this.rgconditionalcolumn.Length))
			{
				throw new ArgumentOutOfRangeException("cConditionalColumn", this.cConditionalColumn, "cannot be greater than the length of rgconditionalcolumn");
			}
			if (this.cConditionalColumn < 0)
			{
				throw new ArgumentOutOfRangeException("cConditionalColumn", this.cConditionalColumn, "cannot be negative");
			}
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0001795C File Offset: 0x00015B5C
		internal NATIVE_INDEXCREATE GetNativeIndexcreate()
		{
			this.CheckMembersAreValid();
			return checked(new NATIVE_INDEXCREATE
			{
				cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_INDEXCREATE)),
				cbKey = (uint)this.cbKey,
				grbit = (uint)this.grbit,
				ulDensity = (uint)this.ulDensity,
				cbVarSegMac = new IntPtr(this.cbVarSegMac),
				cConditionalColumn = (uint)this.cConditionalColumn
			});
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x000179D8 File Offset: 0x00015BD8
		internal NATIVE_INDEXCREATE1 GetNativeIndexcreate1()
		{
			NATIVE_INDEXCREATE1 result = default(NATIVE_INDEXCREATE1);
			result.indexcreate = this.GetNativeIndexcreate();
			checked
			{
				result.indexcreate.cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_INDEXCREATE1));
				if (this.cbKeyMost != 0)
				{
					result.cbKeyMost = (uint)this.cbKeyMost;
					result.indexcreate.grbit = (result.indexcreate.grbit | 32768U);
				}
				return result;
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00017A48 File Offset: 0x00015C48
		internal NATIVE_INDEXCREATE2 GetNativeIndexcreate2()
		{
			NATIVE_INDEXCREATE2 result = default(NATIVE_INDEXCREATE2);
			result.indexcreate1 = this.GetNativeIndexcreate1();
			result.indexcreate1.indexcreate.cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_INDEXCREATE2)));
			return result;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00017A8C File Offset: 0x00015C8C
		internal void SetFromNativeIndexCreate(NATIVE_INDEXCREATE2 value)
		{
			this.SetFromNativeIndexCreate(value.indexcreate1);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00017A9B File Offset: 0x00015C9B
		internal void SetFromNativeIndexCreate(NATIVE_INDEXCREATE1 value)
		{
			this.SetFromNativeIndexCreate(value.indexcreate);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00017AAA File Offset: 0x00015CAA
		internal void SetFromNativeIndexCreate(NATIVE_INDEXCREATE value)
		{
			this.err = (JET_err)value.err;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00017AB9 File Offset: 0x00015CB9
		private bool IsUnicodeIndexEqual(JET_INDEXCREATE other)
		{
			if (this.pidxUnicode != null)
			{
				return this.pidxUnicode.ContentEquals(other.pidxUnicode);
			}
			return null == other.pidxUnicode;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00017AE0 File Offset: 0x00015CE0
		private bool AreConditionalColumnsEqual(JET_INDEXCREATE other)
		{
			if (this.cConditionalColumn != other.cConditionalColumn)
			{
				return false;
			}
			for (int i = 0; i < this.cConditionalColumn; i++)
			{
				if (!this.rgconditionalcolumn[i].ContentEquals(other.rgconditionalcolumn[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00017B28 File Offset: 0x00015D28
		internal NATIVE_INDEXCREATE3 GetNativeIndexcreate3()
		{
			this.CheckMembersAreValid();
			NATIVE_INDEXCREATE3 result = default(NATIVE_INDEXCREATE3);
			checked
			{
				result.cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_INDEXCREATE3));
				result.cbKey = (uint)this.cbKey * 2U;
				result.grbit = (uint)this.grbit;
				result.ulDensity = (uint)this.ulDensity;
				result.cbVarSegMac = new IntPtr(this.cbVarSegMac);
				result.cConditionalColumn = (uint)this.cConditionalColumn;
				if (this.cbKeyMost != 0)
				{
					result.cbKeyMost = (uint)this.cbKeyMost;
					result.grbit |= 32768U;
				}
				return result;
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00017BCF File Offset: 0x00015DCF
		internal void SetFromNativeIndexCreate(NATIVE_INDEXCREATE3 value)
		{
			this.err = (JET_err)value.err;
		}

		// Token: 0x04000714 RID: 1812
		private string name;

		// Token: 0x04000715 RID: 1813
		private string key;

		// Token: 0x04000716 RID: 1814
		private int keyLength;

		// Token: 0x04000717 RID: 1815
		private CreateIndexGrbit options;

		// Token: 0x04000718 RID: 1816
		private int density;

		// Token: 0x04000719 RID: 1817
		private JET_UNICODEINDEX unicodeOptions;

		// Token: 0x0400071A RID: 1818
		private int maxSegmentLength;

		// Token: 0x0400071B RID: 1819
		private JET_CONDITIONALCOLUMN[] conditionalColumns;

		// Token: 0x0400071C RID: 1820
		private int numConditionalColumns;

		// Token: 0x0400071D RID: 1821
		private JET_err errorCode;

		// Token: 0x0400071E RID: 1822
		private int maximumKeyLength;

		// Token: 0x0400071F RID: 1823
		private JET_SPACEHINTS spaceHints;
	}
}
