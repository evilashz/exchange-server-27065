using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002D1 RID: 721
	[Serializable]
	public class JET_TABLECREATE : IContentEquatable<JET_TABLECREATE>, IDeepCloneable<JET_TABLECREATE>
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0001A4A2 File Offset: 0x000186A2
		// (set) Token: 0x06000D18 RID: 3352 RVA: 0x0001A4AA File Offset: 0x000186AA
		public string szTableName
		{
			[DebuggerStepThrough]
			get
			{
				return this.tableName;
			}
			set
			{
				this.tableName = value;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0001A4B3 File Offset: 0x000186B3
		// (set) Token: 0x06000D1A RID: 3354 RVA: 0x0001A4BB File Offset: 0x000186BB
		public string szTemplateTableName
		{
			[DebuggerStepThrough]
			get
			{
				return this.templateTableName;
			}
			set
			{
				this.templateTableName = value;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x0001A4C4 File Offset: 0x000186C4
		// (set) Token: 0x06000D1C RID: 3356 RVA: 0x0001A4CC File Offset: 0x000186CC
		public int ulPages
		{
			[DebuggerStepThrough]
			get
			{
				return this.initialPageAllocation;
			}
			set
			{
				this.initialPageAllocation = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0001A4D5 File Offset: 0x000186D5
		// (set) Token: 0x06000D1E RID: 3358 RVA: 0x0001A4DD File Offset: 0x000186DD
		public int ulDensity
		{
			[DebuggerStepThrough]
			get
			{
				return this.tableDensity;
			}
			set
			{
				this.tableDensity = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0001A4E6 File Offset: 0x000186E6
		// (set) Token: 0x06000D20 RID: 3360 RVA: 0x0001A4EE File Offset: 0x000186EE
		public JET_COLUMNCREATE[] rgcolumncreate
		{
			[DebuggerStepThrough]
			get
			{
				return this.columnCreates;
			}
			set
			{
				this.columnCreates = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0001A4F7 File Offset: 0x000186F7
		// (set) Token: 0x06000D22 RID: 3362 RVA: 0x0001A4FF File Offset: 0x000186FF
		public int cColumns
		{
			[DebuggerStepThrough]
			get
			{
				return this.columnCreateCount;
			}
			set
			{
				this.columnCreateCount = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0001A508 File Offset: 0x00018708
		// (set) Token: 0x06000D24 RID: 3364 RVA: 0x0001A510 File Offset: 0x00018710
		public JET_INDEXCREATE[] rgindexcreate
		{
			[DebuggerStepThrough]
			get
			{
				return this.indexCreates;
			}
			set
			{
				this.indexCreates = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0001A519 File Offset: 0x00018719
		// (set) Token: 0x06000D26 RID: 3366 RVA: 0x0001A521 File Offset: 0x00018721
		public int cIndexes
		{
			[DebuggerStepThrough]
			get
			{
				return this.indexCreateCount;
			}
			set
			{
				this.indexCreateCount = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0001A52A File Offset: 0x0001872A
		// (set) Token: 0x06000D28 RID: 3368 RVA: 0x0001A532 File Offset: 0x00018732
		public string szCallback
		{
			[DebuggerStepThrough]
			get
			{
				return this.callbackFunction;
			}
			set
			{
				this.callbackFunction = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0001A53B File Offset: 0x0001873B
		// (set) Token: 0x06000D2A RID: 3370 RVA: 0x0001A543 File Offset: 0x00018743
		public JET_cbtyp cbtyp
		{
			[DebuggerStepThrough]
			get
			{
				return this.callbackType;
			}
			set
			{
				this.callbackType = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0001A54C File Offset: 0x0001874C
		// (set) Token: 0x06000D2C RID: 3372 RVA: 0x0001A554 File Offset: 0x00018754
		public CreateTableColumnIndexGrbit grbit
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

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x0001A55D File Offset: 0x0001875D
		// (set) Token: 0x06000D2E RID: 3374 RVA: 0x0001A565 File Offset: 0x00018765
		public JET_SPACEHINTS pSeqSpacehints
		{
			[DebuggerStepThrough]
			get
			{
				return this.seqSpacehints;
			}
			set
			{
				this.seqSpacehints = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0001A56E File Offset: 0x0001876E
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x0001A576 File Offset: 0x00018776
		public JET_SPACEHINTS pLVSpacehints
		{
			[DebuggerStepThrough]
			get
			{
				return this.longValueSpacehints;
			}
			set
			{
				this.longValueSpacehints = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0001A57F File Offset: 0x0001877F
		// (set) Token: 0x06000D32 RID: 3378 RVA: 0x0001A587 File Offset: 0x00018787
		public int cbSeparateLV
		{
			[DebuggerStepThrough]
			get
			{
				return this.separateLvThresholdHint;
			}
			set
			{
				this.separateLvThresholdHint = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0001A590 File Offset: 0x00018790
		// (set) Token: 0x06000D34 RID: 3380 RVA: 0x0001A598 File Offset: 0x00018798
		public JET_TABLEID tableid
		{
			[DebuggerStepThrough]
			get
			{
				return this.tableIdentifier;
			}
			set
			{
				this.tableIdentifier = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0001A5A1 File Offset: 0x000187A1
		// (set) Token: 0x06000D36 RID: 3382 RVA: 0x0001A5A9 File Offset: 0x000187A9
		public int cCreated
		{
			[DebuggerStepThrough]
			get
			{
				return this.objectsCreated;
			}
			set
			{
				this.objectsCreated = value;
			}
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0001A5B4 File Offset: 0x000187B4
		public bool ContentEquals(JET_TABLECREATE other)
		{
			if (other == null)
			{
				return false;
			}
			this.CheckMembersAreValid();
			other.CheckMembersAreValid();
			return this.szTableName == other.szTableName && this.szTemplateTableName == other.szTemplateTableName && this.ulPages == other.ulPages && this.ulDensity == other.ulDensity && this.cColumns == other.cColumns && this.cIndexes == other.cIndexes && this.szCallback == other.szCallback && this.cbtyp == other.cbtyp && this.grbit == other.grbit && this.cbSeparateLV == other.cbSeparateLV && Util.ObjectContentEquals<JET_SPACEHINTS>(this.pSeqSpacehints, other.pSeqSpacehints) && Util.ObjectContentEquals<JET_SPACEHINTS>(this.pLVSpacehints, other.pLVSpacehints) && this.tableid == other.tableid && this.cCreated == other.cCreated && Util.ArrayObjectContentEquals<JET_COLUMNCREATE>(this.rgcolumncreate, other.rgcolumncreate, this.cColumns) && Util.ArrayObjectContentEquals<JET_INDEXCREATE>(this.rgindexcreate, other.rgindexcreate, this.cIndexes);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0001A704 File Offset: 0x00018904
		public JET_TABLECREATE DeepClone()
		{
			JET_TABLECREATE jet_TABLECREATE = (JET_TABLECREATE)base.MemberwiseClone();
			jet_TABLECREATE.rgcolumncreate = Util.DeepCloneArray<JET_COLUMNCREATE>(this.rgcolumncreate);
			jet_TABLECREATE.rgindexcreate = Util.DeepCloneArray<JET_INDEXCREATE>(this.rgindexcreate);
			jet_TABLECREATE.seqSpacehints = ((this.seqSpacehints == null) ? null : this.seqSpacehints.DeepClone());
			jet_TABLECREATE.pLVSpacehints = ((this.pLVSpacehints == null) ? null : this.pLVSpacehints.DeepClone());
			return jet_TABLECREATE;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0001A778 File Offset: 0x00018978
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_TABLECREATE({0}:{1} columns:{2} indices)", new object[]
			{
				this.szTableName,
				this.cColumns,
				this.cIndexes
			});
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0001A7C4 File Offset: 0x000189C4
		internal void CheckMembersAreValid()
		{
			if (this.cColumns < 0)
			{
				throw new ArgumentOutOfRangeException("cColumns", this.cColumns, "cannot be negative");
			}
			if (this.rgcolumncreate != null && this.cColumns > this.rgcolumncreate.Length)
			{
				throw new ArgumentOutOfRangeException("cColumns", this.cColumns, "cannot be greater than rgcolumncreate.Length");
			}
			if (this.rgcolumncreate == null && this.cColumns != 0)
			{
				throw new ArgumentOutOfRangeException("cColumns", this.cColumns, "must be zero when rgcolumncreate is null");
			}
			if (this.cIndexes < 0)
			{
				throw new ArgumentOutOfRangeException("cIndexes", this.cIndexes, "cannot be negative");
			}
			if (this.rgindexcreate != null && this.cIndexes > this.rgindexcreate.Length)
			{
				throw new ArgumentOutOfRangeException("cIndexes", this.cIndexes, "cannot be greater than rgindexcreate.Length");
			}
			if (this.rgindexcreate == null && this.cIndexes != 0)
			{
				throw new ArgumentOutOfRangeException("cIndexes", this.cIndexes, "must be zero when rgindexcreate is null");
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0001A8D8 File Offset: 0x00018AD8
		internal NATIVE_TABLECREATE2 GetNativeTableCreate2()
		{
			this.CheckMembersAreValid();
			return checked(new NATIVE_TABLECREATE2
			{
				cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_TABLECREATE2)),
				szTableName = this.szTableName,
				szTemplateTableName = this.szTemplateTableName,
				ulPages = (uint)this.ulPages,
				ulDensity = (uint)this.ulDensity,
				cColumns = (uint)this.cColumns,
				cIndexes = (uint)this.cIndexes,
				szCallback = this.szCallback,
				cbtyp = this.cbtyp,
				grbit = (uint)this.grbit,
				tableid = this.tableid.Value,
				cCreated = (uint)this.cCreated
			});
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0001A9A8 File Offset: 0x00018BA8
		internal NATIVE_TABLECREATE3 GetNativeTableCreate3()
		{
			this.CheckMembersAreValid();
			return checked(new NATIVE_TABLECREATE3
			{
				cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_TABLECREATE3)),
				szTableName = this.szTableName,
				szTemplateTableName = this.szTemplateTableName,
				ulPages = (uint)this.ulPages,
				ulDensity = (uint)this.ulDensity,
				cColumns = (uint)this.cColumns,
				cIndexes = (uint)this.cIndexes,
				szCallback = this.szCallback,
				cbtyp = this.cbtyp,
				grbit = (uint)this.grbit,
				cbSeparateLV = (uint)this.cbSeparateLV,
				tableid = this.tableid.Value,
				cCreated = (uint)this.cCreated
			});
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0001AA84 File Offset: 0x00018C84
		internal NATIVE_TABLECREATE4 GetNativeTableCreate4()
		{
			this.CheckMembersAreValid();
			return checked(new NATIVE_TABLECREATE4
			{
				cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_TABLECREATE4)),
				szTableName = this.szTableName,
				szTemplateTableName = this.szTemplateTableName,
				ulPages = (uint)this.ulPages,
				ulDensity = (uint)this.ulDensity,
				cColumns = (uint)this.cColumns,
				cIndexes = (uint)this.cIndexes,
				szCallback = this.szCallback,
				cbtyp = this.cbtyp,
				grbit = (uint)this.grbit,
				cbSeparateLV = (uint)this.cbSeparateLV,
				tableid = this.tableid.Value,
				cCreated = (uint)this.cCreated
			});
		}

		// Token: 0x04000899 RID: 2201
		private string tableName;

		// Token: 0x0400089A RID: 2202
		private string templateTableName;

		// Token: 0x0400089B RID: 2203
		private int initialPageAllocation;

		// Token: 0x0400089C RID: 2204
		private int tableDensity;

		// Token: 0x0400089D RID: 2205
		private JET_COLUMNCREATE[] columnCreates;

		// Token: 0x0400089E RID: 2206
		private int columnCreateCount;

		// Token: 0x0400089F RID: 2207
		private JET_INDEXCREATE[] indexCreates;

		// Token: 0x040008A0 RID: 2208
		private int indexCreateCount;

		// Token: 0x040008A1 RID: 2209
		private string callbackFunction;

		// Token: 0x040008A2 RID: 2210
		private JET_cbtyp callbackType;

		// Token: 0x040008A3 RID: 2211
		private CreateTableColumnIndexGrbit options;

		// Token: 0x040008A4 RID: 2212
		private JET_SPACEHINTS seqSpacehints;

		// Token: 0x040008A5 RID: 2213
		private JET_SPACEHINTS longValueSpacehints;

		// Token: 0x040008A6 RID: 2214
		private int separateLvThresholdHint;

		// Token: 0x040008A7 RID: 2215
		[NonSerialized]
		private JET_TABLEID tableIdentifier;

		// Token: 0x040008A8 RID: 2216
		private int objectsCreated;
	}
}
