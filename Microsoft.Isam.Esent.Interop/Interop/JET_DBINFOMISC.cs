using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public sealed class JET_DBINFOMISC : IEquatable<JET_DBINFOMISC>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00008E35 File Offset: 0x00007035
		// (set) Token: 0x06000355 RID: 853 RVA: 0x00008E3D File Offset: 0x0000703D
		public int ulIncrementalReseedCount
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulIncrementalReseedCount;
			}
			internal set
			{
				this._ulIncrementalReseedCount = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00008E46 File Offset: 0x00007046
		// (set) Token: 0x06000357 RID: 855 RVA: 0x00008E4E File Offset: 0x0000704E
		public JET_LOGTIME logtimeIncrementalReseed
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimeIncrementalReseed;
			}
			internal set
			{
				this._logtimeIncrementalReseed = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00008E57 File Offset: 0x00007057
		// (set) Token: 0x06000359 RID: 857 RVA: 0x00008E5F File Offset: 0x0000705F
		public int ulIncrementalReseedCountOld
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulIncrementalReseedCountOld;
			}
			internal set
			{
				this._ulIncrementalReseedCountOld = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00008E68 File Offset: 0x00007068
		// (set) Token: 0x0600035B RID: 859 RVA: 0x00008E70 File Offset: 0x00007070
		public int ulPagePatchCount
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulPagePatchCount;
			}
			internal set
			{
				this._ulPagePatchCount = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00008E79 File Offset: 0x00007079
		// (set) Token: 0x0600035D RID: 861 RVA: 0x00008E81 File Offset: 0x00007081
		public JET_LOGTIME logtimePagePatch
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimePagePatch;
			}
			internal set
			{
				this._logtimePagePatch = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00008E8A File Offset: 0x0000708A
		// (set) Token: 0x0600035F RID: 863 RVA: 0x00008E92 File Offset: 0x00007092
		public int ulPagePatchCountOld
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulPagePatchCountOld;
			}
			internal set
			{
				this._ulPagePatchCountOld = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00008E9B File Offset: 0x0000709B
		// (set) Token: 0x06000361 RID: 865 RVA: 0x00008EA3 File Offset: 0x000070A3
		public JET_LOGTIME logtimeChecksumPrev
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimeChecksumPrev;
			}
			internal set
			{
				this._logtimeChecksumPrev = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00008EAC File Offset: 0x000070AC
		// (set) Token: 0x06000363 RID: 867 RVA: 0x00008EB4 File Offset: 0x000070B4
		public JET_LOGTIME logtimeChecksumStart
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimeChecksumStart;
			}
			internal set
			{
				this._logtimeChecksumStart = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00008EBD File Offset: 0x000070BD
		// (set) Token: 0x06000365 RID: 869 RVA: 0x00008EC5 File Offset: 0x000070C5
		public int cpgDatabaseChecked
		{
			[DebuggerStepThrough]
			get
			{
				return this._cpgDatabaseChecked;
			}
			internal set
			{
				this._cpgDatabaseChecked = value;
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00008ED0 File Offset: 0x000070D0
		internal void SetFromNativeDbinfoMisc6(ref NATIVE_DBINFOMISC6 native)
		{
			this.SetFromNativeDbinfoMisc(ref native.dbinfo4);
			this.cpgDatabaseChecked = (int)native.cpgDatabaseChecked;
			this.ulIncrementalReseedCount = (int)native.ulIncrementalReseedCount;
			this.ulIncrementalReseedCountOld = (int)native.ulIncrementalReseedCountOld;
			this.ulPagePatchCount = (int)native.ulPagePatchCount;
			this.ulPagePatchCountOld = (int)native.ulPagePatchCountOld;
			this.logtimeChecksumPrev = native.logtimeChecksumPrev;
			this.logtimeChecksumStart = native.logtimeChecksumStart;
			this.logtimeIncrementalReseed = native.logtimeIncrementalReseed;
			this.logtimePagePatch = native.logtimePagePatch;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00008F58 File Offset: 0x00007158
		internal NATIVE_DBINFOMISC6 GetNativeDbinfomisc6()
		{
			return new NATIVE_DBINFOMISC6
			{
				dbinfo4 = this.GetNativeDbinfomisc4(),
				cpgDatabaseChecked = (uint)this._cpgDatabaseChecked,
				ulIncrementalReseedCount = (uint)this._ulIncrementalReseedCount,
				ulIncrementalReseedCountOld = (uint)this._ulIncrementalReseedCountOld,
				ulPagePatchCount = (uint)this._ulPagePatchCount,
				ulPagePatchCountOld = (uint)this._ulPagePatchCountOld,
				logtimeChecksumPrev = this._logtimeChecksumPrev,
				logtimeChecksumStart = this._logtimeChecksumStart,
				logtimeIncrementalReseed = this._logtimeIncrementalReseed,
				logtimePagePatch = this._logtimePagePatch
			};
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00008FF0 File Offset: 0x000071F0
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00008FF8 File Offset: 0x000071F8
		public JET_LOGTIME logtimeLastReAttach
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimeLastReAttach;
			}
			internal set
			{
				this._logtimeLastReAttach = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00009001 File Offset: 0x00007201
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00009009 File Offset: 0x00007209
		public JET_LGPOS lgposLastReAttach
		{
			[DebuggerStepThrough]
			get
			{
				return this._lgposLastReAttach;
			}
			internal set
			{
				this._lgposLastReAttach = value;
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00009012 File Offset: 0x00007212
		internal void SetFromNativeDbinfoMisc(ref NATIVE_DBINFOMISC7 native)
		{
			this.SetFromNativeDbinfoMisc6(ref native.dbinfo6);
			this.logtimeLastReAttach = native.logtimeLastReAttach;
			this.lgposLastReAttach = native.lgposLastReAttach;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00009038 File Offset: 0x00007238
		internal NATIVE_DBINFOMISC7 GetNativeDbinfomisc7()
		{
			return new NATIVE_DBINFOMISC7
			{
				dbinfo6 = this.GetNativeDbinfomisc6(),
				logtimeLastReAttach = this._logtimeLastReAttach,
				lgposLastReAttach = this._lgposLastReAttach
			};
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00009078 File Offset: 0x00007278
		private void NotYetPublishedEquals(JET_DBINFOMISC other, ref bool notYetPublishedEquals)
		{
			notYetPublishedEquals = (this._cpgDatabaseChecked == other._cpgDatabaseChecked && this._ulIncrementalReseedCount == other._ulIncrementalReseedCount && this._ulIncrementalReseedCountOld == other._ulIncrementalReseedCountOld && this._ulPagePatchCount == other._ulPagePatchCount && this._ulPagePatchCountOld == other._ulPagePatchCountOld && this._logtimeChecksumPrev == other._logtimeChecksumPrev && this._logtimeChecksumStart == other._logtimeChecksumStart && this._logtimeIncrementalReseed == other._logtimeIncrementalReseed && this._logtimePagePatch == other._logtimePagePatch && this._logtimeLastReAttach == other._logtimeLastReAttach && this._lgposLastReAttach == other._lgposLastReAttach);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000914C File Offset: 0x0000734C
		private void AddNotYetPublishedHashCodes(IList<int> hashCodes)
		{
			hashCodes.Add(this._cpgDatabaseChecked);
			hashCodes.Add(this._ulIncrementalReseedCount);
			hashCodes.Add(this._ulIncrementalReseedCountOld);
			hashCodes.Add(this._ulPagePatchCount);
			hashCodes.Add(this._ulPagePatchCountOld);
			hashCodes.Add(this._logtimeChecksumPrev.GetHashCode());
			hashCodes.Add(this._logtimeChecksumStart.GetHashCode());
			hashCodes.Add(this._logtimeIncrementalReseed.GetHashCode());
			hashCodes.Add(this._logtimePagePatch.GetHashCode());
			hashCodes.Add(this._logtimeLastReAttach.GetHashCode());
			hashCodes.Add(this._lgposLastReAttach.GetHashCode());
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000921F File Offset: 0x0000741F
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00009227 File Offset: 0x00007427
		public int ulVersion
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulVersion;
			}
			internal set
			{
				this._ulVersion = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00009230 File Offset: 0x00007430
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00009238 File Offset: 0x00007438
		public int ulUpdate
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulUpdate;
			}
			internal set
			{
				this._ulUpdate = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00009241 File Offset: 0x00007441
		// (set) Token: 0x06000375 RID: 885 RVA: 0x00009249 File Offset: 0x00007449
		public JET_SIGNATURE signDb
		{
			[DebuggerStepThrough]
			get
			{
				return this._signDb;
			}
			internal set
			{
				this._signDb = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00009252 File Offset: 0x00007452
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000925A File Offset: 0x0000745A
		public JET_dbstate dbstate
		{
			[DebuggerStepThrough]
			get
			{
				return this._dbstate;
			}
			internal set
			{
				this._dbstate = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00009263 File Offset: 0x00007463
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000926B File Offset: 0x0000746B
		public JET_LGPOS lgposConsistent
		{
			[DebuggerStepThrough]
			get
			{
				return this._lgposConsistent;
			}
			internal set
			{
				this._lgposConsistent = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00009274 File Offset: 0x00007474
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000927C File Offset: 0x0000747C
		public JET_LOGTIME logtimeConsistent
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimeConsistent;
			}
			internal set
			{
				this._logtimeConsistent = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00009285 File Offset: 0x00007485
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000928D File Offset: 0x0000748D
		public JET_LOGTIME logtimeAttach
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimeAttach;
			}
			internal set
			{
				this._logtimeAttach = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00009296 File Offset: 0x00007496
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0000929E File Offset: 0x0000749E
		public JET_LGPOS lgposAttach
		{
			[DebuggerStepThrough]
			get
			{
				return this._lgposAttach;
			}
			internal set
			{
				this._lgposAttach = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000380 RID: 896 RVA: 0x000092A7 File Offset: 0x000074A7
		// (set) Token: 0x06000381 RID: 897 RVA: 0x000092AF File Offset: 0x000074AF
		public JET_LOGTIME logtimeDetach
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimeDetach;
			}
			internal set
			{
				this._logtimeDetach = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000382 RID: 898 RVA: 0x000092B8 File Offset: 0x000074B8
		// (set) Token: 0x06000383 RID: 899 RVA: 0x000092C0 File Offset: 0x000074C0
		public JET_LGPOS lgposDetach
		{
			[DebuggerStepThrough]
			get
			{
				return this._lgposDetach;
			}
			internal set
			{
				this._lgposDetach = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000384 RID: 900 RVA: 0x000092C9 File Offset: 0x000074C9
		// (set) Token: 0x06000385 RID: 901 RVA: 0x000092D1 File Offset: 0x000074D1
		public JET_SIGNATURE signLog
		{
			[DebuggerStepThrough]
			get
			{
				return this._signLog;
			}
			internal set
			{
				this._signLog = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000386 RID: 902 RVA: 0x000092DA File Offset: 0x000074DA
		// (set) Token: 0x06000387 RID: 903 RVA: 0x000092E2 File Offset: 0x000074E2
		public JET_BKINFO bkinfoFullPrev
		{
			[DebuggerStepThrough]
			get
			{
				return this._bkinfoFullPrev;
			}
			internal set
			{
				this._bkinfoFullPrev = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000388 RID: 904 RVA: 0x000092EB File Offset: 0x000074EB
		// (set) Token: 0x06000389 RID: 905 RVA: 0x000092F3 File Offset: 0x000074F3
		public JET_BKINFO bkinfoIncPrev
		{
			[DebuggerStepThrough]
			get
			{
				return this._bkinfoIncPrev;
			}
			internal set
			{
				this._bkinfoIncPrev = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000092FC File Offset: 0x000074FC
		// (set) Token: 0x0600038B RID: 907 RVA: 0x00009304 File Offset: 0x00007504
		public JET_BKINFO bkinfoFullCur
		{
			[DebuggerStepThrough]
			get
			{
				return this._bkinfoFullCur;
			}
			internal set
			{
				this._bkinfoFullCur = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000930D File Offset: 0x0000750D
		// (set) Token: 0x0600038D RID: 909 RVA: 0x00009315 File Offset: 0x00007515
		public bool fShadowingDisabled
		{
			[DebuggerStepThrough]
			get
			{
				return this._fShadowingDisabled;
			}
			internal set
			{
				this._fShadowingDisabled = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000931E File Offset: 0x0000751E
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00009326 File Offset: 0x00007526
		public bool fUpgradeDb
		{
			[DebuggerStepThrough]
			get
			{
				return this._fUpgradeDb;
			}
			internal set
			{
				this._fUpgradeDb = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000932F File Offset: 0x0000752F
		// (set) Token: 0x06000391 RID: 913 RVA: 0x00009337 File Offset: 0x00007537
		public int dwMajorVersion
		{
			[DebuggerStepThrough]
			get
			{
				return this._dwMajorVersion;
			}
			internal set
			{
				this._dwMajorVersion = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00009340 File Offset: 0x00007540
		// (set) Token: 0x06000393 RID: 915 RVA: 0x00009348 File Offset: 0x00007548
		public int dwMinorVersion
		{
			[DebuggerStepThrough]
			get
			{
				return this._dwMinorVersion;
			}
			internal set
			{
				this._dwMinorVersion = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00009351 File Offset: 0x00007551
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00009359 File Offset: 0x00007559
		public int dwBuildNumber
		{
			[DebuggerStepThrough]
			get
			{
				return this._dwBuildNumber;
			}
			internal set
			{
				this._dwBuildNumber = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00009362 File Offset: 0x00007562
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000936A File Offset: 0x0000756A
		public int lSPNumber
		{
			[DebuggerStepThrough]
			get
			{
				return this._lSPNumber;
			}
			internal set
			{
				this._lSPNumber = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00009373 File Offset: 0x00007573
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000937B File Offset: 0x0000757B
		public int cbPageSize
		{
			[DebuggerStepThrough]
			get
			{
				return this._cbPageSize;
			}
			internal set
			{
				this._cbPageSize = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00009384 File Offset: 0x00007584
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000938C File Offset: 0x0000758C
		public int genMinRequired
		{
			[DebuggerStepThrough]
			get
			{
				return this._genMinRequired;
			}
			internal set
			{
				this._genMinRequired = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00009395 File Offset: 0x00007595
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000939D File Offset: 0x0000759D
		public int genMaxRequired
		{
			[DebuggerStepThrough]
			get
			{
				return this._genMaxRequired;
			}
			internal set
			{
				this._genMaxRequired = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600039E RID: 926 RVA: 0x000093A6 File Offset: 0x000075A6
		// (set) Token: 0x0600039F RID: 927 RVA: 0x000093AE File Offset: 0x000075AE
		public JET_LOGTIME logtimeGenMaxCreate
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimeGenMaxCreate;
			}
			internal set
			{
				this._logtimeGenMaxCreate = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000093B7 File Offset: 0x000075B7
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x000093BF File Offset: 0x000075BF
		public int ulRepairCount
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulRepairCount;
			}
			internal set
			{
				this._ulRepairCount = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x000093C8 File Offset: 0x000075C8
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x000093D0 File Offset: 0x000075D0
		public JET_LOGTIME logtimeRepair
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimeRepair;
			}
			internal set
			{
				this._logtimeRepair = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000093D9 File Offset: 0x000075D9
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x000093E1 File Offset: 0x000075E1
		public int ulRepairCountOld
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulRepairCountOld;
			}
			internal set
			{
				this._ulRepairCountOld = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x000093EA File Offset: 0x000075EA
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x000093F2 File Offset: 0x000075F2
		public int ulECCFixSuccess
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulECCFixSuccess;
			}
			internal set
			{
				this._ulECCFixSuccess = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x000093FB File Offset: 0x000075FB
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00009403 File Offset: 0x00007603
		public JET_LOGTIME logtimeECCFixSuccess
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimeECCFixSuccess;
			}
			internal set
			{
				this._logtimeECCFixSuccess = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000940C File Offset: 0x0000760C
		// (set) Token: 0x060003AB RID: 939 RVA: 0x00009414 File Offset: 0x00007614
		public int ulECCFixSuccessOld
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulECCFixSuccessOld;
			}
			internal set
			{
				this._ulECCFixSuccessOld = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000941D File Offset: 0x0000761D
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00009425 File Offset: 0x00007625
		public int ulECCFixFail
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulECCFixFail;
			}
			internal set
			{
				this._ulECCFixFail = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000942E File Offset: 0x0000762E
		// (set) Token: 0x060003AF RID: 943 RVA: 0x00009436 File Offset: 0x00007636
		public JET_LOGTIME logtimeECCFixFail
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimeECCFixFail;
			}
			internal set
			{
				this._logtimeECCFixFail = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000943F File Offset: 0x0000763F
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x00009447 File Offset: 0x00007647
		public int ulECCFixFailOld
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulECCFixFailOld;
			}
			internal set
			{
				this._ulECCFixFailOld = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00009450 File Offset: 0x00007650
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00009458 File Offset: 0x00007658
		public int ulBadChecksum
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulBadChecksum;
			}
			internal set
			{
				this._ulBadChecksum = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00009461 File Offset: 0x00007661
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x00009469 File Offset: 0x00007669
		public JET_LOGTIME logtimeBadChecksum
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimeBadChecksum;
			}
			internal set
			{
				this._logtimeBadChecksum = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00009472 File Offset: 0x00007672
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000947A File Offset: 0x0000767A
		public int ulBadChecksumOld
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulBadChecksumOld;
			}
			internal set
			{
				this._ulBadChecksumOld = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00009483 File Offset: 0x00007683
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000948B File Offset: 0x0000768B
		public int genCommitted
		{
			[DebuggerStepThrough]
			get
			{
				return this._genCommitted;
			}
			internal set
			{
				this._genCommitted = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00009494 File Offset: 0x00007694
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000949C File Offset: 0x0000769C
		public JET_BKINFO bkinfoCopyPrev
		{
			[DebuggerStepThrough]
			get
			{
				return this._bkinfoCopyPrev;
			}
			internal set
			{
				this._bkinfoCopyPrev = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060003BC RID: 956 RVA: 0x000094A5 File Offset: 0x000076A5
		// (set) Token: 0x060003BD RID: 957 RVA: 0x000094AD File Offset: 0x000076AD
		public JET_BKINFO bkinfoDiffPrev
		{
			[DebuggerStepThrough]
			get
			{
				return this._bkinfoDiffPrev;
			}
			internal set
			{
				this._bkinfoDiffPrev = value;
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000094B8 File Offset: 0x000076B8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_DBINFOMISC({0})", new object[]
			{
				this._signDb
			});
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000094EC File Offset: 0x000076EC
		public override int GetHashCode()
		{
			int[] collection = new int[]
			{
				this._ulVersion,
				this._ulUpdate,
				this._signDb.GetHashCode(),
				this._dbstate.GetHashCode(),
				this._lgposConsistent.GetHashCode(),
				this._logtimeConsistent.GetHashCode(),
				this._logtimeAttach.GetHashCode(),
				this._lgposAttach.GetHashCode(),
				this._logtimeDetach.GetHashCode(),
				this._lgposDetach.GetHashCode(),
				this._signLog.GetHashCode(),
				this._bkinfoFullPrev.GetHashCode(),
				this._bkinfoIncPrev.GetHashCode(),
				this._bkinfoFullCur.GetHashCode(),
				this._fShadowingDisabled.GetHashCode(),
				this._fUpgradeDb.GetHashCode(),
				this._dwMajorVersion,
				this._dwMinorVersion,
				this._dwBuildNumber,
				this._lSPNumber,
				this._cbPageSize,
				this._genMinRequired,
				this._genMaxRequired,
				this._logtimeGenMaxCreate.GetHashCode(),
				this._ulRepairCount,
				this._logtimeRepair.GetHashCode(),
				this._ulRepairCountOld,
				this._ulECCFixSuccess,
				this._logtimeECCFixSuccess.GetHashCode(),
				this._ulECCFixSuccessOld,
				this._ulECCFixFail,
				this._logtimeECCFixFail.GetHashCode(),
				this._ulECCFixFailOld,
				this._ulBadChecksum,
				this._logtimeBadChecksum.GetHashCode(),
				this._ulBadChecksumOld,
				this._genCommitted,
				this._bkinfoCopyPrev.GetHashCode(),
				this._bkinfoDiffPrev.GetHashCode()
			};
			List<int> list = new List<int>(collection);
			this.AddNotYetPublishedHashCodes(list);
			return Util.CalculateHashCode(list);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000976E File Offset: 0x0000796E
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_DBINFOMISC)obj);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00009794 File Offset: 0x00007994
		public bool Equals(JET_DBINFOMISC other)
		{
			if (other == null)
			{
				return false;
			}
			bool flag = true;
			this.NotYetPublishedEquals(other, ref flag);
			return flag && this._ulVersion == other._ulVersion && this._ulUpdate == other._ulUpdate && this._signDb == other._signDb && this._dbstate == other._dbstate && this._lgposConsistent == other._lgposConsistent && this._logtimeConsistent == other._logtimeConsistent && this._logtimeAttach == other._logtimeAttach && this._lgposAttach == other._lgposAttach && this._logtimeDetach == other._logtimeDetach && this._lgposDetach == other._lgposDetach && this._signLog == other._signLog && this._bkinfoFullPrev == other._bkinfoFullPrev && this._bkinfoIncPrev == other._bkinfoIncPrev && this._bkinfoFullCur == other._bkinfoFullCur && this._fShadowingDisabled == other._fShadowingDisabled && this._fUpgradeDb == other._fUpgradeDb && this._dwMajorVersion == other._dwMajorVersion && this._dwMinorVersion == other._dwMinorVersion && this._dwBuildNumber == other._dwBuildNumber && this._lSPNumber == other._lSPNumber && this._cbPageSize == other._cbPageSize && this._genMinRequired == other._genMinRequired && this._genMaxRequired == other._genMaxRequired && this._logtimeGenMaxCreate == other._logtimeGenMaxCreate && this._ulRepairCount == other._ulRepairCount && this._logtimeRepair == other._logtimeRepair && this._ulRepairCountOld == other._ulRepairCountOld && this._ulECCFixSuccess == other._ulECCFixSuccess && this._logtimeECCFixSuccess == other._logtimeECCFixSuccess && this._ulECCFixSuccessOld == other._ulECCFixSuccessOld && this._ulECCFixFail == other._ulECCFixFail && this._logtimeECCFixFail == other._logtimeECCFixFail && this._ulECCFixFailOld == other._ulECCFixFailOld && this._ulBadChecksum == other._ulBadChecksum && this._logtimeBadChecksum == other._logtimeBadChecksum && this._ulBadChecksumOld == other._ulBadChecksumOld && this._genCommitted == other._genCommitted && this._bkinfoCopyPrev == other._bkinfoCopyPrev && this._bkinfoDiffPrev == other._bkinfoDiffPrev;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00009A90 File Offset: 0x00007C90
		internal void SetFromNativeDbinfoMisc(ref NATIVE_DBINFOMISC native)
		{
			this._ulVersion = (int)native.ulVersion;
			this._ulUpdate = (int)native.ulUpdate;
			this._signDb = new JET_SIGNATURE(native.signDb);
			this._dbstate = (JET_dbstate)native.dbstate;
			this._lgposConsistent = native.lgposConsistent;
			this._logtimeConsistent = native.logtimeConsistent;
			this._logtimeAttach = native.logtimeAttach;
			this._lgposAttach = native.lgposAttach;
			this._logtimeDetach = native.logtimeDetach;
			this._lgposDetach = native.lgposDetach;
			this._signLog = new JET_SIGNATURE(native.signLog);
			this._bkinfoFullPrev = native.bkinfoFullPrev;
			this._bkinfoIncPrev = native.bkinfoIncPrev;
			this._bkinfoFullCur = native.bkinfoFullCur;
			this._fShadowingDisabled = (0U != native.fShadowingDisabled);
			this._fUpgradeDb = (0U != native.fUpgradeDb);
			this._dwMajorVersion = (int)native.dwMajorVersion;
			this._dwMinorVersion = (int)native.dwMinorVersion;
			this._dwBuildNumber = (int)native.dwBuildNumber;
			this._lSPNumber = (int)native.lSPNumber;
			this._cbPageSize = (int)native.cbPageSize;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00009BB0 File Offset: 0x00007DB0
		internal void SetFromNativeDbinfoMisc(ref NATIVE_DBINFOMISC4 native)
		{
			this.SetFromNativeDbinfoMisc(ref native.dbinfo);
			this._genMinRequired = (int)native.genMinRequired;
			this._genMaxRequired = (int)native.genMaxRequired;
			this._logtimeGenMaxCreate = native.logtimeGenMaxCreate;
			this._ulRepairCount = (int)native.ulRepairCount;
			this._logtimeRepair = native.logtimeRepair;
			this._ulRepairCountOld = (int)native.ulRepairCountOld;
			this._ulECCFixSuccess = (int)native.ulECCFixSuccess;
			this._logtimeECCFixSuccess = native.logtimeECCFixSuccess;
			this._ulECCFixSuccessOld = (int)native.ulECCFixSuccessOld;
			this._ulECCFixFail = (int)native.ulECCFixFail;
			this._logtimeECCFixFail = native.logtimeECCFixFail;
			this._ulECCFixFailOld = (int)native.ulECCFixFailOld;
			this._ulBadChecksum = (int)native.ulBadChecksum;
			this._logtimeBadChecksum = native.logtimeBadChecksum;
			this._ulBadChecksumOld = (int)native.ulBadChecksumOld;
			this._genCommitted = (int)native.genCommitted;
			this._bkinfoCopyPrev = native.bkinfoCopyPrev;
			this._bkinfoDiffPrev = native.bkinfoDiffPrev;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00009CA4 File Offset: 0x00007EA4
		internal NATIVE_DBINFOMISC GetNativeDbinfomisc()
		{
			return new NATIVE_DBINFOMISC
			{
				ulVersion = (uint)this._ulVersion,
				ulUpdate = (uint)this._ulUpdate,
				signDb = this._signDb.GetNativeSignature(),
				dbstate = (uint)this._dbstate,
				lgposConsistent = this._lgposConsistent,
				logtimeConsistent = this._logtimeConsistent,
				logtimeAttach = this._logtimeAttach,
				lgposAttach = this._lgposAttach,
				logtimeDetach = this._logtimeDetach,
				lgposDetach = this._lgposDetach,
				signLog = this._signLog.GetNativeSignature(),
				bkinfoFullPrev = this._bkinfoFullPrev,
				bkinfoIncPrev = this._bkinfoIncPrev,
				bkinfoFullCur = this._bkinfoFullCur,
				fShadowingDisabled = (this._fShadowingDisabled ? 1U : 0U),
				fUpgradeDb = (this._fUpgradeDb ? 1U : 0U),
				dwMajorVersion = (uint)this._dwMajorVersion,
				dwMinorVersion = (uint)this._dwMinorVersion,
				dwBuildNumber = (uint)this._dwBuildNumber,
				lSPNumber = (uint)this._lSPNumber,
				cbPageSize = (uint)this._cbPageSize
			};
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00009DE4 File Offset: 0x00007FE4
		internal NATIVE_DBINFOMISC4 GetNativeDbinfomisc4()
		{
			return new NATIVE_DBINFOMISC4
			{
				dbinfo = this.GetNativeDbinfomisc(),
				genMinRequired = (uint)this._genMinRequired,
				genMaxRequired = (uint)this._genMaxRequired,
				logtimeGenMaxCreate = this._logtimeGenMaxCreate,
				ulRepairCount = (uint)this._ulRepairCount,
				logtimeRepair = this._logtimeRepair,
				ulRepairCountOld = (uint)this._ulRepairCountOld,
				ulECCFixSuccess = (uint)this._ulECCFixSuccess,
				logtimeECCFixSuccess = this._logtimeECCFixSuccess,
				ulECCFixSuccessOld = (uint)this._ulECCFixSuccessOld,
				ulECCFixFail = (uint)this._ulECCFixFail,
				logtimeECCFixFail = this._logtimeECCFixFail,
				ulECCFixFailOld = (uint)this._ulECCFixFailOld,
				ulBadChecksum = (uint)this._ulBadChecksum,
				logtimeBadChecksum = this._logtimeBadChecksum,
				ulBadChecksumOld = (uint)this._ulBadChecksumOld,
				genCommitted = (uint)this._genCommitted,
				bkinfoCopyPrev = this._bkinfoCopyPrev,
				bkinfoDiffPrev = this._bkinfoDiffPrev
			};
		}

		// Token: 0x0400008F RID: 143
		private int _ulIncrementalReseedCount;

		// Token: 0x04000090 RID: 144
		private JET_LOGTIME _logtimeIncrementalReseed;

		// Token: 0x04000091 RID: 145
		private int _ulIncrementalReseedCountOld;

		// Token: 0x04000092 RID: 146
		private int _ulPagePatchCount;

		// Token: 0x04000093 RID: 147
		private JET_LOGTIME _logtimePagePatch;

		// Token: 0x04000094 RID: 148
		private int _ulPagePatchCountOld;

		// Token: 0x04000095 RID: 149
		private JET_LOGTIME _logtimeChecksumPrev;

		// Token: 0x04000096 RID: 150
		private JET_LOGTIME _logtimeChecksumStart;

		// Token: 0x04000097 RID: 151
		private int _cpgDatabaseChecked;

		// Token: 0x04000098 RID: 152
		private JET_LOGTIME _logtimeLastReAttach;

		// Token: 0x04000099 RID: 153
		private JET_LGPOS _lgposLastReAttach;

		// Token: 0x0400009A RID: 154
		private int _ulVersion;

		// Token: 0x0400009B RID: 155
		private int _ulUpdate;

		// Token: 0x0400009C RID: 156
		private JET_SIGNATURE _signDb;

		// Token: 0x0400009D RID: 157
		private JET_dbstate _dbstate;

		// Token: 0x0400009E RID: 158
		private JET_LGPOS _lgposConsistent;

		// Token: 0x0400009F RID: 159
		private JET_LOGTIME _logtimeConsistent;

		// Token: 0x040000A0 RID: 160
		private JET_LOGTIME _logtimeAttach;

		// Token: 0x040000A1 RID: 161
		private JET_LGPOS _lgposAttach;

		// Token: 0x040000A2 RID: 162
		private JET_LOGTIME _logtimeDetach;

		// Token: 0x040000A3 RID: 163
		private JET_LGPOS _lgposDetach;

		// Token: 0x040000A4 RID: 164
		private JET_SIGNATURE _signLog;

		// Token: 0x040000A5 RID: 165
		private JET_BKINFO _bkinfoFullPrev;

		// Token: 0x040000A6 RID: 166
		private JET_BKINFO _bkinfoIncPrev;

		// Token: 0x040000A7 RID: 167
		private JET_BKINFO _bkinfoFullCur;

		// Token: 0x040000A8 RID: 168
		private bool _fShadowingDisabled;

		// Token: 0x040000A9 RID: 169
		private bool _fUpgradeDb;

		// Token: 0x040000AA RID: 170
		private int _dwMajorVersion;

		// Token: 0x040000AB RID: 171
		private int _dwMinorVersion;

		// Token: 0x040000AC RID: 172
		private int _dwBuildNumber;

		// Token: 0x040000AD RID: 173
		private int _lSPNumber;

		// Token: 0x040000AE RID: 174
		private int _cbPageSize;

		// Token: 0x040000AF RID: 175
		private int _genMinRequired;

		// Token: 0x040000B0 RID: 176
		private int _genMaxRequired;

		// Token: 0x040000B1 RID: 177
		private JET_LOGTIME _logtimeGenMaxCreate;

		// Token: 0x040000B2 RID: 178
		private int _ulRepairCount;

		// Token: 0x040000B3 RID: 179
		private JET_LOGTIME _logtimeRepair;

		// Token: 0x040000B4 RID: 180
		private int _ulRepairCountOld;

		// Token: 0x040000B5 RID: 181
		private int _ulECCFixSuccess;

		// Token: 0x040000B6 RID: 182
		private JET_LOGTIME _logtimeECCFixSuccess;

		// Token: 0x040000B7 RID: 183
		private int _ulECCFixSuccessOld;

		// Token: 0x040000B8 RID: 184
		private int _ulECCFixFail;

		// Token: 0x040000B9 RID: 185
		private JET_LOGTIME _logtimeECCFixFail;

		// Token: 0x040000BA RID: 186
		private int _ulECCFixFailOld;

		// Token: 0x040000BB RID: 187
		private int _ulBadChecksum;

		// Token: 0x040000BC RID: 188
		private JET_LOGTIME _logtimeBadChecksum;

		// Token: 0x040000BD RID: 189
		private int _ulBadChecksumOld;

		// Token: 0x040000BE RID: 190
		private int _genCommitted;

		// Token: 0x040000BF RID: 191
		private JET_BKINFO _bkinfoCopyPrev;

		// Token: 0x040000C0 RID: 192
		private JET_BKINFO _bkinfoDiffPrev;
	}
}
