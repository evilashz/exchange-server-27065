using System;
using System.Collections;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Globalization
{
	// Token: 0x0200037C RID: 892
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class CultureInfo : ICloneable, IFormatProvider
	{
		// Token: 0x06002D28 RID: 11560 RVA: 0x000AC57C File Offset: 0x000AA77C
		private static bool Init()
		{
			if (CultureInfo.s_InvariantCultureInfo == null)
			{
				CultureInfo.s_InvariantCultureInfo = new CultureInfo("", false)
				{
					m_isReadOnly = true
				};
			}
			CultureInfo.s_userDefaultCulture = (CultureInfo.s_userDefaultUICulture = CultureInfo.s_InvariantCultureInfo);
			CultureInfo.s_userDefaultCulture = CultureInfo.InitUserDefaultCulture();
			CultureInfo.s_userDefaultUICulture = CultureInfo.InitUserDefaultUICulture();
			return true;
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x000AC5DC File Offset: 0x000AA7DC
		[SecuritySafeCritical]
		private static CultureInfo InitUserDefaultCulture()
		{
			string defaultLocaleName = CultureInfo.GetDefaultLocaleName(1024);
			if (defaultLocaleName == null)
			{
				defaultLocaleName = CultureInfo.GetDefaultLocaleName(2048);
				if (defaultLocaleName == null)
				{
					return CultureInfo.InvariantCulture;
				}
			}
			CultureInfo cultureByName = CultureInfo.GetCultureByName(defaultLocaleName, true);
			cultureByName.m_isReadOnly = true;
			return cultureByName;
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000AC61C File Offset: 0x000AA81C
		private static CultureInfo InitUserDefaultUICulture()
		{
			string userDefaultUILanguage = CultureInfo.GetUserDefaultUILanguage();
			if (userDefaultUILanguage == CultureInfo.UserDefaultCulture.Name)
			{
				return CultureInfo.UserDefaultCulture;
			}
			CultureInfo cultureByName = CultureInfo.GetCultureByName(userDefaultUILanguage, true);
			if (cultureByName == null)
			{
				return CultureInfo.InvariantCulture;
			}
			cultureByName.m_isReadOnly = true;
			return cultureByName;
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x000AC660 File Offset: 0x000AA860
		[SecuritySafeCritical]
		internal static CultureInfo GetCultureInfoForUserPreferredLanguageInAppX()
		{
			if (CultureInfo.ts_IsDoingAppXCultureInfoLookup)
			{
				return null;
			}
			if (AppDomain.IsAppXNGen)
			{
				return null;
			}
			CultureInfo result = null;
			try
			{
				CultureInfo.ts_IsDoingAppXCultureInfoLookup = true;
				if (CultureInfo.s_WindowsRuntimeResourceManager == null)
				{
					CultureInfo.s_WindowsRuntimeResourceManager = ResourceManager.GetWinRTResourceManager();
				}
				result = CultureInfo.s_WindowsRuntimeResourceManager.GlobalResourceContextBestFitCultureInfo;
			}
			finally
			{
				CultureInfo.ts_IsDoingAppXCultureInfoLookup = false;
			}
			return result;
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x000AC6C4 File Offset: 0x000AA8C4
		[SecuritySafeCritical]
		internal static bool SetCultureInfoForUserPreferredLanguageInAppX(CultureInfo ci)
		{
			if (AppDomain.IsAppXNGen)
			{
				return false;
			}
			if (CultureInfo.s_WindowsRuntimeResourceManager == null)
			{
				CultureInfo.s_WindowsRuntimeResourceManager = ResourceManager.GetWinRTResourceManager();
			}
			return CultureInfo.s_WindowsRuntimeResourceManager.SetGlobalResourceContextDefaultCulture(ci);
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000AC6F1 File Offset: 0x000AA8F1
		[__DynamicallyInvokable]
		public CultureInfo(string name) : this(name, true)
		{
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x000AC6FC File Offset: 0x000AA8FC
		public CultureInfo(string name, bool useUserOverride)
		{
			this.cultureID = 127;
			base..ctor();
			if (name == null)
			{
				throw new ArgumentNullException("name", Environment.GetResourceString("ArgumentNull_String"));
			}
			this.m_cultureData = CultureData.GetCultureData(name, useUserOverride);
			if (this.m_cultureData == null)
			{
				throw new CultureNotFoundException("name", name, Environment.GetResourceString("Argument_CultureNotSupported"));
			}
			this.m_name = this.m_cultureData.CultureName;
			this.m_isInherited = (base.GetType() != typeof(CultureInfo));
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x000AC786 File Offset: 0x000AA986
		private CultureInfo(CultureData cultureData)
		{
			this.cultureID = 127;
			base..ctor();
			this.m_cultureData = cultureData;
			this.m_name = cultureData.CultureName;
			this.m_isInherited = false;
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x000AC7B0 File Offset: 0x000AA9B0
		private static CultureInfo CreateCultureInfoNoThrow(string name, bool useUserOverride)
		{
			CultureData cultureData = CultureData.GetCultureData(name, useUserOverride);
			if (cultureData == null)
			{
				return null;
			}
			return new CultureInfo(cultureData);
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000AC7D0 File Offset: 0x000AA9D0
		public CultureInfo(int culture) : this(culture, true)
		{
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000AC7DA File Offset: 0x000AA9DA
		public CultureInfo(int culture, bool useUserOverride)
		{
			this.cultureID = 127;
			base..ctor();
			if (culture < 0)
			{
				throw new ArgumentOutOfRangeException("culture", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			this.InitializeFromCultureId(culture, useUserOverride);
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000AC80C File Offset: 0x000AAA0C
		private void InitializeFromCultureId(int culture, bool useUserOverride)
		{
			if (culture <= 1024)
			{
				if (culture != 0 && culture != 1024)
				{
					goto IL_43;
				}
			}
			else if (culture != 2048 && culture != 3072 && culture != 4096)
			{
				goto IL_43;
			}
			throw new CultureNotFoundException("culture", culture, Environment.GetResourceString("Argument_CultureNotSupported"));
			IL_43:
			this.m_cultureData = CultureData.GetCultureData(culture, useUserOverride);
			this.m_isInherited = (base.GetType() != typeof(CultureInfo));
			this.m_name = this.m_cultureData.CultureName;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000AC898 File Offset: 0x000AAA98
		internal static void CheckDomainSafetyObject(object obj, object container)
		{
			if (obj.GetType().Assembly != typeof(CultureInfo).Assembly)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidOperation_SubclassedObject"), obj.GetType(), container.GetType()));
			}
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000AC8EC File Offset: 0x000AAAEC
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_name == null || CultureInfo.IsAlternateSortLcid(this.cultureID))
			{
				this.InitializeFromCultureId(this.cultureID, this.m_useUserOverride);
			}
			else
			{
				this.m_cultureData = CultureData.GetCultureData(this.m_name, this.m_useUserOverride);
				if (this.m_cultureData == null)
				{
					throw new CultureNotFoundException("m_name", this.m_name, Environment.GetResourceString("Argument_CultureNotSupported"));
				}
			}
			this.m_isInherited = (base.GetType() != typeof(CultureInfo));
			if (base.GetType().Assembly == typeof(CultureInfo).Assembly)
			{
				if (this.textInfo != null)
				{
					CultureInfo.CheckDomainSafetyObject(this.textInfo, this);
				}
				if (this.compareInfo != null)
				{
					CultureInfo.CheckDomainSafetyObject(this.compareInfo, this);
				}
			}
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000AC9C0 File Offset: 0x000AABC0
		private static bool IsAlternateSortLcid(int lcid)
		{
			return lcid == 1034 || (lcid & 983040) != 0;
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000AC9D6 File Offset: 0x000AABD6
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.m_name = this.m_cultureData.CultureName;
			this.m_useUserOverride = this.m_cultureData.UseUserOverride;
			this.cultureID = this.m_cultureData.ILANGUAGE;
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002D38 RID: 11576 RVA: 0x000ACA0B File Offset: 0x000AAC0B
		internal bool IsSafeCrossDomain
		{
			get
			{
				return this.m_isSafeCrossDomain;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x000ACA13 File Offset: 0x000AAC13
		internal int CreatedDomainID
		{
			get
			{
				return this.m_createdDomainID;
			}
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x000ACA1B File Offset: 0x000AAC1B
		internal void StartCrossDomainTracking()
		{
			if (this.m_createdDomainID != 0)
			{
				return;
			}
			if (this.CanSendCrossDomain())
			{
				this.m_isSafeCrossDomain = true;
			}
			Thread.MemoryBarrier();
			this.m_createdDomainID = Thread.GetDomainID();
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x000ACA48 File Offset: 0x000AAC48
		internal bool CanSendCrossDomain()
		{
			bool result = false;
			if (base.GetType() == typeof(CultureInfo))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x000ACA74 File Offset: 0x000AAC74
		internal CultureInfo(string cultureName, string textAndCompareCultureName)
		{
			this.cultureID = 127;
			base..ctor();
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName", Environment.GetResourceString("ArgumentNull_String"));
			}
			this.m_cultureData = CultureData.GetCultureData(cultureName, false);
			if (this.m_cultureData == null)
			{
				throw new CultureNotFoundException("cultureName", cultureName, Environment.GetResourceString("Argument_CultureNotSupported"));
			}
			this.m_name = this.m_cultureData.CultureName;
			CultureInfo cultureInfo = CultureInfo.GetCultureInfo(textAndCompareCultureName);
			this.compareInfo = cultureInfo.CompareInfo;
			this.textInfo = cultureInfo.TextInfo;
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x000ACB04 File Offset: 0x000AAD04
		private static CultureInfo GetCultureByName(string name, bool userOverride)
		{
			try
			{
				return userOverride ? new CultureInfo(name) : CultureInfo.GetCultureInfo(name);
			}
			catch (ArgumentException)
			{
			}
			return null;
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000ACB3C File Offset: 0x000AAD3C
		public static CultureInfo CreateSpecificCulture(string name)
		{
			CultureInfo cultureInfo;
			try
			{
				cultureInfo = new CultureInfo(name);
			}
			catch (ArgumentException)
			{
				cultureInfo = null;
				for (int i = 0; i < name.Length; i++)
				{
					if ('-' == name[i])
					{
						try
						{
							cultureInfo = new CultureInfo(name.Substring(0, i));
							break;
						}
						catch (ArgumentException)
						{
							throw;
						}
					}
				}
				if (cultureInfo == null)
				{
					throw;
				}
			}
			if (!cultureInfo.IsNeutralCulture)
			{
				return cultureInfo;
			}
			return new CultureInfo(cultureInfo.m_cultureData.SSPECIFICCULTURE);
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x000ACBC4 File Offset: 0x000AADC4
		internal static bool VerifyCultureName(string cultureName, bool throwException)
		{
			int i = 0;
			while (i < cultureName.Length)
			{
				char c = cultureName[i];
				if (!char.IsLetterOrDigit(c) && c != '-' && c != '_')
				{
					if (throwException)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidResourceCultureName", new object[]
						{
							cultureName
						}));
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			return true;
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000ACC1C File Offset: 0x000AAE1C
		internal static bool VerifyCultureName(CultureInfo culture, bool throwException)
		{
			return !culture.m_isInherited || CultureInfo.VerifyCultureName(culture.Name, throwException);
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06002D41 RID: 11585 RVA: 0x000ACC34 File Offset: 0x000AAE34
		// (set) Token: 0x06002D42 RID: 11586 RVA: 0x000ACC40 File Offset: 0x000AAE40
		[__DynamicallyInvokable]
		public static CultureInfo CurrentCulture
		{
			[__DynamicallyInvokable]
			get
			{
				return Thread.CurrentThread.CurrentCulture;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (AppDomain.IsAppXModel() && CultureInfo.SetCultureInfoForUserPreferredLanguageInAppX(value))
				{
					return;
				}
				Thread.CurrentThread.CurrentCulture = value;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002D43 RID: 11587 RVA: 0x000ACC6C File Offset: 0x000AAE6C
		internal static CultureInfo UserDefaultCulture
		{
			get
			{
				CultureInfo cultureInfo = CultureInfo.s_userDefaultCulture;
				if (cultureInfo == null)
				{
					CultureInfo.s_userDefaultCulture = CultureInfo.InvariantCulture;
					cultureInfo = CultureInfo.InitUserDefaultCulture();
					CultureInfo.s_userDefaultCulture = cultureInfo;
				}
				return cultureInfo;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002D44 RID: 11588 RVA: 0x000ACCA0 File Offset: 0x000AAEA0
		internal static CultureInfo UserDefaultUICulture
		{
			get
			{
				CultureInfo cultureInfo = CultureInfo.s_userDefaultUICulture;
				if (cultureInfo == null)
				{
					CultureInfo.s_userDefaultUICulture = CultureInfo.InvariantCulture;
					cultureInfo = CultureInfo.InitUserDefaultUICulture();
					CultureInfo.s_userDefaultUICulture = cultureInfo;
				}
				return cultureInfo;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002D45 RID: 11589 RVA: 0x000ACCD3 File Offset: 0x000AAED3
		// (set) Token: 0x06002D46 RID: 11590 RVA: 0x000ACCDF File Offset: 0x000AAEDF
		[__DynamicallyInvokable]
		public static CultureInfo CurrentUICulture
		{
			[__DynamicallyInvokable]
			get
			{
				return Thread.CurrentThread.CurrentUICulture;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (AppDomain.IsAppXModel() && CultureInfo.SetCultureInfoForUserPreferredLanguageInAppX(value))
				{
					return;
				}
				Thread.CurrentThread.CurrentUICulture = value;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06002D47 RID: 11591 RVA: 0x000ACD0C File Offset: 0x000AAF0C
		public static CultureInfo InstalledUICulture
		{
			get
			{
				CultureInfo cultureInfo = CultureInfo.s_InstalledUICultureInfo;
				if (cultureInfo == null)
				{
					string systemDefaultUILanguage = CultureInfo.GetSystemDefaultUILanguage();
					cultureInfo = CultureInfo.GetCultureByName(systemDefaultUILanguage, true);
					if (cultureInfo == null)
					{
						cultureInfo = CultureInfo.InvariantCulture;
					}
					cultureInfo.m_isReadOnly = true;
					CultureInfo.s_InstalledUICultureInfo = cultureInfo;
				}
				return cultureInfo;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002D48 RID: 11592 RVA: 0x000ACD4B File Offset: 0x000AAF4B
		// (set) Token: 0x06002D49 RID: 11593 RVA: 0x000ACD54 File Offset: 0x000AAF54
		[__DynamicallyInvokable]
		public static CultureInfo DefaultThreadCurrentCulture
		{
			[__DynamicallyInvokable]
			get
			{
				return CultureInfo.s_DefaultThreadCurrentCulture;
			}
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
			set
			{
				CultureInfo.s_DefaultThreadCurrentCulture = value;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06002D4A RID: 11594 RVA: 0x000ACD5E File Offset: 0x000AAF5E
		// (set) Token: 0x06002D4B RID: 11595 RVA: 0x000ACD67 File Offset: 0x000AAF67
		[__DynamicallyInvokable]
		public static CultureInfo DefaultThreadCurrentUICulture
		{
			[__DynamicallyInvokable]
			get
			{
				return CultureInfo.s_DefaultThreadCurrentUICulture;
			}
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
			set
			{
				if (value != null)
				{
					CultureInfo.VerifyCultureName(value, true);
				}
				CultureInfo.s_DefaultThreadCurrentUICulture = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x000ACD7C File Offset: 0x000AAF7C
		[__DynamicallyInvokable]
		public static CultureInfo InvariantCulture
		{
			[__DynamicallyInvokable]
			get
			{
				return CultureInfo.s_InvariantCultureInfo;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06002D4D RID: 11597 RVA: 0x000ACD88 File Offset: 0x000AAF88
		[__DynamicallyInvokable]
		public virtual CultureInfo Parent
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				if (this.m_parent == null)
				{
					string sparent = this.m_cultureData.SPARENT;
					CultureInfo cultureInfo;
					if (string.IsNullOrEmpty(sparent))
					{
						cultureInfo = CultureInfo.InvariantCulture;
					}
					else
					{
						cultureInfo = CultureInfo.CreateCultureInfoNoThrow(sparent, this.m_cultureData.UseUserOverride);
						if (cultureInfo == null)
						{
							cultureInfo = CultureInfo.InvariantCulture;
						}
					}
					Interlocked.CompareExchange<CultureInfo>(ref this.m_parent, cultureInfo, null);
				}
				return this.m_parent;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002D4E RID: 11598 RVA: 0x000ACDEA File Offset: 0x000AAFEA
		public virtual int LCID
		{
			get
			{
				return this.m_cultureData.ILANGUAGE;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002D4F RID: 11599 RVA: 0x000ACDF8 File Offset: 0x000AAFF8
		[ComVisible(false)]
		public virtual int KeyboardLayoutId
		{
			get
			{
				return this.m_cultureData.IINPUTLANGUAGEHANDLE;
			}
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000ACE12 File Offset: 0x000AB012
		public static CultureInfo[] GetCultures(CultureTypes types)
		{
			if ((types & CultureTypes.UserCustomCulture) == CultureTypes.UserCustomCulture)
			{
				types |= CultureTypes.ReplacementCultures;
			}
			return CultureData.GetCultures(types);
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06002D51 RID: 11601 RVA: 0x000ACE26 File Offset: 0x000AB026
		[__DynamicallyInvokable]
		public virtual string Name
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_nonSortName == null)
				{
					this.m_nonSortName = this.m_cultureData.SNAME;
					if (this.m_nonSortName == null)
					{
						this.m_nonSortName = string.Empty;
					}
				}
				return this.m_nonSortName;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x000ACE5A File Offset: 0x000AB05A
		internal string SortName
		{
			get
			{
				if (this.m_sortName == null)
				{
					this.m_sortName = this.m_cultureData.SCOMPAREINFO;
				}
				return this.m_sortName;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06002D53 RID: 11603 RVA: 0x000ACE7C File Offset: 0x000AB07C
		[ComVisible(false)]
		public string IetfLanguageTag
		{
			get
			{
				string name = this.Name;
				if (name == "zh-CHT")
				{
					return "zh-Hant";
				}
				if (!(name == "zh-CHS"))
				{
					return this.Name;
				}
				return "zh-Hans";
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06002D54 RID: 11604 RVA: 0x000ACEBE File Offset: 0x000AB0BE
		[__DynamicallyInvokable]
		public virtual string DisplayName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SLOCALIZEDDISPLAYNAME;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002D55 RID: 11605 RVA: 0x000ACECB File Offset: 0x000AB0CB
		[__DynamicallyInvokable]
		public virtual string NativeName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SNATIVEDISPLAYNAME;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x000ACED8 File Offset: 0x000AB0D8
		[__DynamicallyInvokable]
		public virtual string EnglishName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SENGDISPLAYNAME;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x000ACEE5 File Offset: 0x000AB0E5
		[__DynamicallyInvokable]
		public virtual string TwoLetterISOLanguageName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SISO639LANGNAME;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x000ACEF2 File Offset: 0x000AB0F2
		public virtual string ThreeLetterISOLanguageName
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_cultureData.SISO639LANGNAME2;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x000ACEFF File Offset: 0x000AB0FF
		public virtual string ThreeLetterWindowsLanguageName
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_cultureData.SABBREVLANGNAME;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x000ACF0C File Offset: 0x000AB10C
		[__DynamicallyInvokable]
		public virtual CompareInfo CompareInfo
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.compareInfo == null)
				{
					CompareInfo result = this.UseUserOverride ? CultureInfo.GetCultureInfo(this.m_name).CompareInfo : new CompareInfo(this);
					if (!CompatibilitySwitches.IsCompatibilityBehaviorDefined)
					{
						return result;
					}
					this.compareInfo = result;
				}
				return this.compareInfo;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x000ACF5C File Offset: 0x000AB15C
		private RegionInfo Region
		{
			get
			{
				if (this.regionInfo == null)
				{
					RegionInfo regionInfo = new RegionInfo(this.m_cultureData);
					this.regionInfo = regionInfo;
				}
				return this.regionInfo;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000ACF8C File Offset: 0x000AB18C
		[__DynamicallyInvokable]
		public virtual TextInfo TextInfo
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.textInfo == null)
				{
					TextInfo textInfo = new TextInfo(this.m_cultureData);
					textInfo.SetReadOnlyState(this.m_isReadOnly);
					if (!CompatibilitySwitches.IsCompatibilityBehaviorDefined)
					{
						return textInfo;
					}
					this.textInfo = textInfo;
				}
				return this.textInfo;
			}
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000ACFD4 File Offset: 0x000AB1D4
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			if (this == value)
			{
				return true;
			}
			CultureInfo cultureInfo = value as CultureInfo;
			return cultureInfo != null && this.Name.Equals(cultureInfo.Name) && this.CompareInfo.Equals(cultureInfo.CompareInfo);
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000AD019 File Offset: 0x000AB219
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() + this.CompareInfo.GetHashCode();
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000AD032 File Offset: 0x000AB232
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.m_name;
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000AD03A File Offset: 0x000AB23A
		[__DynamicallyInvokable]
		public virtual object GetFormat(Type formatType)
		{
			if (formatType == typeof(NumberFormatInfo))
			{
				return this.NumberFormat;
			}
			if (formatType == typeof(DateTimeFormatInfo))
			{
				return this.DateTimeFormat;
			}
			return null;
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x000AD06F File Offset: 0x000AB26F
		[__DynamicallyInvokable]
		public virtual bool IsNeutralCulture
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.IsNeutralCulture;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x000AD07C File Offset: 0x000AB27C
		[ComVisible(false)]
		public CultureTypes CultureTypes
		{
			get
			{
				CultureTypes cultureTypes = (CultureTypes)0;
				if (this.m_cultureData.IsNeutralCulture)
				{
					cultureTypes |= CultureTypes.NeutralCultures;
				}
				else
				{
					cultureTypes |= CultureTypes.SpecificCultures;
				}
				cultureTypes |= (this.m_cultureData.IsWin32Installed ? CultureTypes.InstalledWin32Cultures : ((CultureTypes)0));
				cultureTypes |= (this.m_cultureData.IsFramework ? CultureTypes.FrameworkCultures : ((CultureTypes)0));
				cultureTypes |= (this.m_cultureData.IsSupplementalCustomCulture ? CultureTypes.UserCustomCulture : ((CultureTypes)0));
				return cultureTypes | (this.m_cultureData.IsReplacementCulture ? (CultureTypes.UserCustomCulture | CultureTypes.ReplacementCultures) : ((CultureTypes)0));
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06002D63 RID: 11619 RVA: 0x000AD0F8 File Offset: 0x000AB2F8
		// (set) Token: 0x06002D64 RID: 11620 RVA: 0x000AD132 File Offset: 0x000AB332
		[__DynamicallyInvokable]
		public virtual NumberFormatInfo NumberFormat
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.numInfo == null)
				{
					this.numInfo = new NumberFormatInfo(this.m_cultureData)
					{
						isReadOnly = this.m_isReadOnly
					};
				}
				return this.numInfo;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Obj"));
				}
				this.VerifyWritable();
				this.numInfo = value;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06002D65 RID: 11621 RVA: 0x000AD15C File Offset: 0x000AB35C
		// (set) Token: 0x06002D66 RID: 11622 RVA: 0x000AD1A1 File Offset: 0x000AB3A1
		[__DynamicallyInvokable]
		public virtual DateTimeFormatInfo DateTimeFormat
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.dateTimeInfo == null)
				{
					DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo(this.m_cultureData, this.Calendar);
					dateTimeFormatInfo.m_isReadOnly = this.m_isReadOnly;
					Thread.MemoryBarrier();
					this.dateTimeInfo = dateTimeFormatInfo;
				}
				return this.dateTimeInfo;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_Obj"));
				}
				this.VerifyWritable();
				this.dateTimeInfo = value;
			}
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000AD1C8 File Offset: 0x000AB3C8
		public void ClearCachedData()
		{
			CultureInfo.s_userDefaultUICulture = null;
			CultureInfo.s_userDefaultCulture = null;
			RegionInfo.s_currentRegionInfo = null;
			TimeZone.ResetTimeZone();
			TimeZoneInfo.ClearCachedData();
			CultureInfo.s_LcidCachedCultures = null;
			CultureInfo.s_NameCachedCultures = null;
			CultureData.ClearCachedData();
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000AD201 File Offset: 0x000AB401
		internal static Calendar GetCalendarInstance(int calType)
		{
			if (calType == 1)
			{
				return new GregorianCalendar();
			}
			return CultureInfo.GetCalendarInstanceRare(calType);
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000AD214 File Offset: 0x000AB414
		internal static Calendar GetCalendarInstanceRare(int calType)
		{
			switch (calType)
			{
			case 2:
			case 9:
			case 10:
			case 11:
			case 12:
				return new GregorianCalendar((GregorianCalendarTypes)calType);
			case 3:
				return new JapaneseCalendar();
			case 4:
				return new TaiwanCalendar();
			case 5:
				return new KoreanCalendar();
			case 6:
				return new HijriCalendar();
			case 7:
				return new ThaiBuddhistCalendar();
			case 8:
				return new HebrewCalendar();
			case 14:
				return new JapaneseLunisolarCalendar();
			case 15:
				return new ChineseLunisolarCalendar();
			case 20:
				return new KoreanLunisolarCalendar();
			case 21:
				return new TaiwanLunisolarCalendar();
			case 22:
				return new PersianCalendar();
			case 23:
				return new UmAlQuraCalendar();
			}
			return new GregorianCalendar();
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x000AD2D8 File Offset: 0x000AB4D8
		[__DynamicallyInvokable]
		public virtual Calendar Calendar
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.calendar == null)
				{
					Calendar defaultCalendar = this.m_cultureData.DefaultCalendar;
					Thread.MemoryBarrier();
					defaultCalendar.SetReadOnlyState(this.m_isReadOnly);
					this.calendar = defaultCalendar;
				}
				return this.calendar;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06002D6B RID: 11627 RVA: 0x000AD318 File Offset: 0x000AB518
		[__DynamicallyInvokable]
		public virtual Calendar[] OptionalCalendars
		{
			[__DynamicallyInvokable]
			get
			{
				int[] calendarIds = this.m_cultureData.CalendarIds;
				Calendar[] array = new Calendar[calendarIds.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = CultureInfo.GetCalendarInstance(calendarIds[i]);
				}
				return array;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x000AD354 File Offset: 0x000AB554
		public bool UseUserOverride
		{
			get
			{
				return this.m_cultureData.UseUserOverride;
			}
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000AD364 File Offset: 0x000AB564
		[SecuritySafeCritical]
		[ComVisible(false)]
		public CultureInfo GetConsoleFallbackUICulture()
		{
			CultureInfo cultureInfo = this.m_consoleFallbackCulture;
			if (cultureInfo == null)
			{
				cultureInfo = CultureInfo.CreateSpecificCulture(this.m_cultureData.SCONSOLEFALLBACKNAME);
				cultureInfo.m_isReadOnly = true;
				this.m_consoleFallbackCulture = cultureInfo;
			}
			return cultureInfo;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000AD39C File Offset: 0x000AB59C
		[__DynamicallyInvokable]
		public virtual object Clone()
		{
			CultureInfo cultureInfo = (CultureInfo)base.MemberwiseClone();
			cultureInfo.m_isReadOnly = false;
			if (!this.m_isInherited)
			{
				if (this.dateTimeInfo != null)
				{
					cultureInfo.dateTimeInfo = (DateTimeFormatInfo)this.dateTimeInfo.Clone();
				}
				if (this.numInfo != null)
				{
					cultureInfo.numInfo = (NumberFormatInfo)this.numInfo.Clone();
				}
			}
			else
			{
				cultureInfo.DateTimeFormat = (DateTimeFormatInfo)this.DateTimeFormat.Clone();
				cultureInfo.NumberFormat = (NumberFormatInfo)this.NumberFormat.Clone();
			}
			if (this.textInfo != null)
			{
				cultureInfo.textInfo = (TextInfo)this.textInfo.Clone();
			}
			if (this.calendar != null)
			{
				cultureInfo.calendar = (Calendar)this.calendar.Clone();
			}
			return cultureInfo;
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000AD46C File Offset: 0x000AB66C
		[__DynamicallyInvokable]
		public static CultureInfo ReadOnly(CultureInfo ci)
		{
			if (ci == null)
			{
				throw new ArgumentNullException("ci");
			}
			if (ci.IsReadOnly)
			{
				return ci;
			}
			CultureInfo cultureInfo = (CultureInfo)ci.MemberwiseClone();
			if (!ci.IsNeutralCulture)
			{
				if (!ci.m_isInherited)
				{
					if (ci.dateTimeInfo != null)
					{
						cultureInfo.dateTimeInfo = DateTimeFormatInfo.ReadOnly(ci.dateTimeInfo);
					}
					if (ci.numInfo != null)
					{
						cultureInfo.numInfo = NumberFormatInfo.ReadOnly(ci.numInfo);
					}
				}
				else
				{
					cultureInfo.DateTimeFormat = DateTimeFormatInfo.ReadOnly(ci.DateTimeFormat);
					cultureInfo.NumberFormat = NumberFormatInfo.ReadOnly(ci.NumberFormat);
				}
			}
			if (ci.textInfo != null)
			{
				cultureInfo.textInfo = TextInfo.ReadOnly(ci.textInfo);
			}
			if (ci.calendar != null)
			{
				cultureInfo.calendar = Calendar.ReadOnly(ci.calendar);
			}
			cultureInfo.m_isReadOnly = true;
			return cultureInfo;
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x000AD53D File Offset: 0x000AB73D
		[__DynamicallyInvokable]
		public bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_isReadOnly;
			}
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000AD545 File Offset: 0x000AB745
		private void VerifyWritable()
		{
			if (this.m_isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x000AD55F File Offset: 0x000AB75F
		internal bool HasInvariantCultureName
		{
			get
			{
				return this.Name == CultureInfo.InvariantCulture.Name;
			}
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000AD578 File Offset: 0x000AB778
		internal static CultureInfo GetCultureInfoHelper(int lcid, string name, string altName)
		{
			Hashtable hashtable = CultureInfo.s_NameCachedCultures;
			if (name != null)
			{
				name = CultureData.AnsiToLower(name);
			}
			if (altName != null)
			{
				altName = CultureData.AnsiToLower(altName);
			}
			CultureInfo cultureInfo;
			if (hashtable == null)
			{
				hashtable = Hashtable.Synchronized(new Hashtable());
			}
			else if (lcid == -1)
			{
				cultureInfo = (CultureInfo)hashtable[name + "�" + altName];
				if (cultureInfo != null)
				{
					return cultureInfo;
				}
			}
			else if (lcid == 0)
			{
				cultureInfo = (CultureInfo)hashtable[name];
				if (cultureInfo != null)
				{
					return cultureInfo;
				}
			}
			Hashtable hashtable2 = CultureInfo.s_LcidCachedCultures;
			if (hashtable2 == null)
			{
				hashtable2 = Hashtable.Synchronized(new Hashtable());
			}
			else if (lcid > 0)
			{
				cultureInfo = (CultureInfo)hashtable2[lcid];
				if (cultureInfo != null)
				{
					return cultureInfo;
				}
			}
			try
			{
				if (lcid != -1)
				{
					if (lcid != 0)
					{
						cultureInfo = new CultureInfo(lcid, false);
					}
					else
					{
						cultureInfo = new CultureInfo(name, false);
					}
				}
				else
				{
					cultureInfo = new CultureInfo(name, altName);
				}
			}
			catch (ArgumentException)
			{
				return null;
			}
			cultureInfo.m_isReadOnly = true;
			if (lcid == -1)
			{
				hashtable[name + "�" + altName] = cultureInfo;
				cultureInfo.TextInfo.SetReadOnlyState(true);
			}
			else
			{
				string text = CultureData.AnsiToLower(cultureInfo.m_name);
				hashtable[text] = cultureInfo;
				if ((cultureInfo.LCID != 4 || !(text == "zh-hans")) && (cultureInfo.LCID != 31748 || !(text == "zh-hant")))
				{
					hashtable2[cultureInfo.LCID] = cultureInfo;
				}
			}
			if (-1 != lcid)
			{
				CultureInfo.s_LcidCachedCultures = hashtable2;
			}
			CultureInfo.s_NameCachedCultures = hashtable;
			return cultureInfo;
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000AD6FC File Offset: 0x000AB8FC
		public static CultureInfo GetCultureInfo(int culture)
		{
			if (culture <= 0)
			{
				throw new ArgumentOutOfRangeException("culture", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(culture, null, null);
			if (cultureInfoHelper == null)
			{
				throw new CultureNotFoundException("culture", culture, Environment.GetResourceString("Argument_CultureNotSupported"));
			}
			return cultureInfoHelper;
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000AD748 File Offset: 0x000AB948
		public static CultureInfo GetCultureInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(0, name, null);
			if (cultureInfoHelper == null)
			{
				throw new CultureNotFoundException("name", name, Environment.GetResourceString("Argument_CultureNotSupported"));
			}
			return cultureInfoHelper;
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x000AD788 File Offset: 0x000AB988
		public static CultureInfo GetCultureInfo(string name, string altName)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (altName == null)
			{
				throw new ArgumentNullException("altName");
			}
			CultureInfo cultureInfoHelper = CultureInfo.GetCultureInfoHelper(-1, name, altName);
			if (cultureInfoHelper == null)
			{
				throw new CultureNotFoundException("name or altName", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_OneOfCulturesNotSupported"), name, altName));
			}
			return cultureInfoHelper;
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x000AD7E0 File Offset: 0x000AB9E0
		public static CultureInfo GetCultureInfoByIetfLanguageTag(string name)
		{
			if (name == "zh-CHT" || name == "zh-CHS")
			{
				throw new CultureNotFoundException("name", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_CultureIetfNotSupported"), name));
			}
			CultureInfo cultureInfo = CultureInfo.GetCultureInfo(name);
			if (cultureInfo.LCID > 65535 || cultureInfo.LCID == 1034)
			{
				throw new CultureNotFoundException("name", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_CultureIetfNotSupported"), name));
			}
			return cultureInfo;
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x000AD869 File Offset: 0x000ABA69
		internal static bool IsTaiwanSku
		{
			get
			{
				if (!CultureInfo.s_haveIsTaiwanSku)
				{
					CultureInfo.s_isTaiwanSku = (CultureInfo.GetSystemDefaultUILanguage() == "zh-TW");
					CultureInfo.s_haveIsTaiwanSku = true;
				}
				return CultureInfo.s_isTaiwanSku;
			}
		}

		// Token: 0x06002D79 RID: 11641
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string nativeGetLocaleInfoEx(string localeName, uint field);

		// Token: 0x06002D7A RID: 11642
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int nativeGetLocaleInfoExInt(string localeName, uint field);

		// Token: 0x06002D7B RID: 11643
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool nativeSetThreadLocale(string localeName);

		// Token: 0x06002D7C RID: 11644 RVA: 0x000AD89C File Offset: 0x000ABA9C
		[SecurityCritical]
		private static string GetDefaultLocaleName(int localeType)
		{
			string result = null;
			if (CultureInfo.InternalGetDefaultLocaleName(localeType, JitHelpers.GetStringHandleOnStack(ref result)))
			{
				return result;
			}
			return string.Empty;
		}

		// Token: 0x06002D7D RID: 11645
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool InternalGetDefaultLocaleName(int localetype, StringHandleOnStack localeString);

		// Token: 0x06002D7E RID: 11646 RVA: 0x000AD8C4 File Offset: 0x000ABAC4
		[SecuritySafeCritical]
		private static string GetUserDefaultUILanguage()
		{
			string result = null;
			if (CultureInfo.InternalGetUserDefaultUILanguage(JitHelpers.GetStringHandleOnStack(ref result)))
			{
				return result;
			}
			return string.Empty;
		}

		// Token: 0x06002D7F RID: 11647
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool InternalGetUserDefaultUILanguage(StringHandleOnStack userDefaultUiLanguage);

		// Token: 0x06002D80 RID: 11648 RVA: 0x000AD8E8 File Offset: 0x000ABAE8
		[SecuritySafeCritical]
		private static string GetSystemDefaultUILanguage()
		{
			string result = null;
			if (CultureInfo.InternalGetSystemDefaultUILanguage(JitHelpers.GetStringHandleOnStack(ref result)))
			{
				return result;
			}
			return string.Empty;
		}

		// Token: 0x06002D81 RID: 11649
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool InternalGetSystemDefaultUILanguage(StringHandleOnStack systemDefaultUiLanguage);

		// Token: 0x04001273 RID: 4723
		internal bool m_isReadOnly;

		// Token: 0x04001274 RID: 4724
		internal CompareInfo compareInfo;

		// Token: 0x04001275 RID: 4725
		internal TextInfo textInfo;

		// Token: 0x04001276 RID: 4726
		[NonSerialized]
		internal RegionInfo regionInfo;

		// Token: 0x04001277 RID: 4727
		internal NumberFormatInfo numInfo;

		// Token: 0x04001278 RID: 4728
		internal DateTimeFormatInfo dateTimeInfo;

		// Token: 0x04001279 RID: 4729
		internal Calendar calendar;

		// Token: 0x0400127A RID: 4730
		[OptionalField(VersionAdded = 1)]
		internal int m_dataItem;

		// Token: 0x0400127B RID: 4731
		[OptionalField(VersionAdded = 1)]
		internal int cultureID;

		// Token: 0x0400127C RID: 4732
		[NonSerialized]
		internal CultureData m_cultureData;

		// Token: 0x0400127D RID: 4733
		[NonSerialized]
		internal bool m_isInherited;

		// Token: 0x0400127E RID: 4734
		[NonSerialized]
		private bool m_isSafeCrossDomain;

		// Token: 0x0400127F RID: 4735
		[NonSerialized]
		private int m_createdDomainID;

		// Token: 0x04001280 RID: 4736
		[NonSerialized]
		private CultureInfo m_consoleFallbackCulture;

		// Token: 0x04001281 RID: 4737
		internal string m_name;

		// Token: 0x04001282 RID: 4738
		[NonSerialized]
		private string m_nonSortName;

		// Token: 0x04001283 RID: 4739
		[NonSerialized]
		private string m_sortName;

		// Token: 0x04001284 RID: 4740
		private static volatile CultureInfo s_userDefaultCulture;

		// Token: 0x04001285 RID: 4741
		private static volatile CultureInfo s_InvariantCultureInfo;

		// Token: 0x04001286 RID: 4742
		private static volatile CultureInfo s_userDefaultUICulture;

		// Token: 0x04001287 RID: 4743
		private static volatile CultureInfo s_InstalledUICultureInfo;

		// Token: 0x04001288 RID: 4744
		private static volatile CultureInfo s_DefaultThreadCurrentUICulture;

		// Token: 0x04001289 RID: 4745
		private static volatile CultureInfo s_DefaultThreadCurrentCulture;

		// Token: 0x0400128A RID: 4746
		private static volatile Hashtable s_LcidCachedCultures;

		// Token: 0x0400128B RID: 4747
		private static volatile Hashtable s_NameCachedCultures;

		// Token: 0x0400128C RID: 4748
		[SecurityCritical]
		private static volatile WindowsRuntimeResourceManagerBase s_WindowsRuntimeResourceManager;

		// Token: 0x0400128D RID: 4749
		[ThreadStatic]
		private static bool ts_IsDoingAppXCultureInfoLookup;

		// Token: 0x0400128E RID: 4750
		[NonSerialized]
		private CultureInfo m_parent;

		// Token: 0x0400128F RID: 4751
		internal const int LOCALE_NEUTRAL = 0;

		// Token: 0x04001290 RID: 4752
		private const int LOCALE_USER_DEFAULT = 1024;

		// Token: 0x04001291 RID: 4753
		private const int LOCALE_SYSTEM_DEFAULT = 2048;

		// Token: 0x04001292 RID: 4754
		internal const int LOCALE_CUSTOM_DEFAULT = 3072;

		// Token: 0x04001293 RID: 4755
		internal const int LOCALE_CUSTOM_UNSPECIFIED = 4096;

		// Token: 0x04001294 RID: 4756
		internal const int LOCALE_INVARIANT = 127;

		// Token: 0x04001295 RID: 4757
		private const int LOCALE_TRADITIONAL_SPANISH = 1034;

		// Token: 0x04001296 RID: 4758
		private static readonly bool init = CultureInfo.Init();

		// Token: 0x04001297 RID: 4759
		private bool m_useUserOverride;

		// Token: 0x04001298 RID: 4760
		private const int LOCALE_SORTID_MASK = 983040;

		// Token: 0x04001299 RID: 4761
		private static volatile bool s_isTaiwanSku;

		// Token: 0x0400129A RID: 4762
		private static volatile bool s_haveIsTaiwanSku;
	}
}
