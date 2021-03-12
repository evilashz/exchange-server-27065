using System;
using System.Collections.Generic;
using System.Deployment.Internal.Isolation.Manifest;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Text;

namespace System
{
	// Token: 0x0200009E RID: 158
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Serializable]
	public sealed class AppDomainSetup : IAppDomainSetup
	{
		// Token: 0x060008E8 RID: 2280 RVA: 0x0001D668 File Offset: 0x0001B868
		[SecuritySafeCritical]
		internal AppDomainSetup(AppDomainSetup copy, bool copyDomainBoundData)
		{
			string[] value = this.Value;
			if (copy != null)
			{
				string[] value2 = copy.Value;
				int num = this._Entries.Length;
				int num2 = value2.Length;
				int num3 = (num2 < num) ? num2 : num;
				for (int i = 0; i < num3; i++)
				{
					value[i] = value2[i];
				}
				if (num3 < num)
				{
					for (int j = num3; j < num; j++)
					{
						value[j] = null;
					}
				}
				this._LoaderOptimization = copy._LoaderOptimization;
				this._AppDomainInitializerArguments = copy.AppDomainInitializerArguments;
				this._ActivationArguments = copy.ActivationArguments;
				this._ApplicationTrust = copy._ApplicationTrust;
				if (copyDomainBoundData)
				{
					this._AppDomainInitializer = copy.AppDomainInitializer;
				}
				else
				{
					this._AppDomainInitializer = null;
				}
				this._ConfigurationBytes = copy.GetConfigurationBytes();
				this._DisableInterfaceCache = copy._DisableInterfaceCache;
				this._AppDomainManagerAssembly = copy.AppDomainManagerAssembly;
				this._AppDomainManagerType = copy.AppDomainManagerType;
				this._AptcaVisibleAssemblies = copy.PartialTrustVisibleAssemblies;
				if (copy._CompatFlags != null)
				{
					this.SetCompatibilitySwitches(copy._CompatFlags.Keys);
				}
				if (copy._AppDomainSortingSetupInfo != null)
				{
					this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo(copy._AppDomainSortingSetupInfo);
				}
				this._TargetFrameworkName = copy._TargetFrameworkName;
				this._UseRandomizedStringHashing = copy._UseRandomizedStringHashing;
				return;
			}
			this._LoaderOptimization = LoaderOptimization.NotSpecified;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001D7B0 File Offset: 0x0001B9B0
		public AppDomainSetup()
		{
			this._LoaderOptimization = LoaderOptimization.NotSpecified;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001D7BF File Offset: 0x0001B9BF
		public AppDomainSetup(ActivationContext activationContext) : this(new ActivationArguments(activationContext))
		{
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001D7D0 File Offset: 0x0001B9D0
		[SecuritySafeCritical]
		public AppDomainSetup(ActivationArguments activationArguments)
		{
			if (activationArguments == null)
			{
				throw new ArgumentNullException("activationArguments");
			}
			this._LoaderOptimization = LoaderOptimization.NotSpecified;
			this.ActivationArguments = activationArguments;
			string entryPointFullPath = CmsUtils.GetEntryPointFullPath(activationArguments);
			if (!string.IsNullOrEmpty(entryPointFullPath))
			{
				this.SetupDefaults(entryPointFullPath, false);
				return;
			}
			this.ApplicationBase = activationArguments.ActivationContext.ApplicationDirectory;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001D828 File Offset: 0x0001BA28
		internal void SetupDefaults(string imageLocation, bool imageLocationAlreadyNormalized = false)
		{
			char[] anyOf = new char[]
			{
				'\\',
				'/'
			};
			int num = imageLocation.LastIndexOfAny(anyOf);
			if (num == -1)
			{
				this.ApplicationName = imageLocation;
			}
			else
			{
				this.ApplicationName = imageLocation.Substring(num + 1);
				string text = imageLocation.Substring(0, num + 1);
				if (imageLocationAlreadyNormalized)
				{
					this.Value[0] = text;
				}
				else
				{
					this.ApplicationBase = text;
				}
			}
			this.ConfigurationFile = this.ApplicationName + AppDomainSetup.ConfigurationExtension;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0001D8A0 File Offset: 0x0001BAA0
		internal string[] Value
		{
			get
			{
				if (this._Entries == null)
				{
					this._Entries = new string[18];
				}
				return this._Entries;
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001D8BD File Offset: 0x0001BABD
		internal string GetUnsecureApplicationBase()
		{
			return this.Value[0];
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0001D8C7 File Offset: 0x0001BAC7
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0001D8CF File Offset: 0x0001BACF
		public string AppDomainManagerAssembly
		{
			get
			{
				return this._AppDomainManagerAssembly;
			}
			set
			{
				this._AppDomainManagerAssembly = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0001D8D8 File Offset: 0x0001BAD8
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0001D8E0 File Offset: 0x0001BAE0
		public string AppDomainManagerType
		{
			get
			{
				return this._AppDomainManagerType;
			}
			set
			{
				this._AppDomainManagerType = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0001D8E9 File Offset: 0x0001BAE9
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x0001D8F1 File Offset: 0x0001BAF1
		public string[] PartialTrustVisibleAssemblies
		{
			get
			{
				return this._AptcaVisibleAssemblies;
			}
			set
			{
				if (value != null)
				{
					this._AptcaVisibleAssemblies = (string[])value.Clone();
					Array.Sort<string>(this._AptcaVisibleAssemblies, StringComparer.OrdinalIgnoreCase);
					return;
				}
				this._AptcaVisibleAssemblies = null;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0001D91F File Offset: 0x0001BB1F
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x0001D92E File Offset: 0x0001BB2E
		public string ApplicationBase
		{
			[SecuritySafeCritical]
			get
			{
				return this.VerifyDir(this.GetUnsecureApplicationBase(), false);
			}
			set
			{
				this.Value[0] = this.NormalizePath(value, false);
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001D940 File Offset: 0x0001BB40
		[SecuritySafeCritical]
		private string NormalizePath(string path, bool useAppBase)
		{
			if (path == null)
			{
				return null;
			}
			if (!useAppBase)
			{
				path = URLString.PreProcessForExtendedPathRemoval(false, path, false);
			}
			int num = path.Length;
			if (num == 0)
			{
				return null;
			}
			bool flag = false;
			if (num > 7 && string.Compare(path, 0, "file:", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
			{
				int num2;
				if (path[6] == '\\')
				{
					if (path[7] == '\\' || path[7] == '/')
					{
						if (num > 8 && (path[8] == '\\' || path[8] == '/'))
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
						}
						num2 = 8;
					}
					else
					{
						num2 = 5;
						flag = true;
					}
				}
				else if (path[7] == '/')
				{
					num2 = 8;
				}
				else
				{
					if (num > 8 && path[7] == '\\' && path[8] == '\\')
					{
						num2 = 7;
					}
					else
					{
						num2 = 5;
						StringBuilder stringBuilder = new StringBuilder(num);
						for (int i = 0; i < num; i++)
						{
							char c = path[i];
							if (c == '/')
							{
								stringBuilder.Append('\\');
							}
							else
							{
								stringBuilder.Append(c);
							}
						}
						path = stringBuilder.ToString();
					}
					flag = true;
				}
				path = path.Substring(num2);
				num -= num2;
			}
			bool flag2;
			if (flag || (num > 1 && (path[0] == '/' || path[0] == '\\') && (path[1] == '/' || path[1] == '\\')))
			{
				flag2 = false;
			}
			else
			{
				int num3 = path.IndexOf(':') + 1;
				flag2 = (num3 == 0 || num <= num3 + 1 || (path[num3] != '/' && path[num3] != '\\') || (path[num3 + 1] != '/' && path[num3 + 1] != '\\'));
			}
			if (flag2)
			{
				if (useAppBase && (num == 1 || path[1] != ':'))
				{
					string text = this.Value[0];
					if (text == null || text.Length == 0)
					{
						throw new MemberAccessException(Environment.GetResourceString("AppDomain_AppBaseNotSet"));
					}
					StringBuilder stringBuilder2 = StringBuilderCache.Acquire(16);
					bool flag3 = false;
					if (path[0] == '/' || path[0] == '\\')
					{
						string text2 = AppDomain.NormalizePath(text, false);
						text2 = text2.Substring(0, PathInternal.GetRootLength(text2));
						if (text2.Length == 0)
						{
							int j = text.IndexOf(":/", StringComparison.Ordinal);
							if (j == -1)
							{
								j = text.IndexOf(":\\", StringComparison.Ordinal);
							}
							int length = text.Length;
							for (j++; j < length; j++)
							{
								if (text[j] != '/' && text[j] != '\\')
								{
									break;
								}
							}
							while (j < length && text[j] != '/' && text[j] != '\\')
							{
								j++;
							}
							text2 = text.Substring(0, j);
						}
						stringBuilder2.Append(text2);
						flag3 = true;
					}
					else
					{
						stringBuilder2.Append(text);
					}
					int num4 = stringBuilder2.Length - 1;
					if (stringBuilder2[num4] != '/' && stringBuilder2[num4] != '\\')
					{
						if (!flag3)
						{
							if (text.IndexOf(":/", StringComparison.Ordinal) == -1)
							{
								stringBuilder2.Append('\\');
							}
							else
							{
								stringBuilder2.Append('/');
							}
						}
					}
					else if (flag3)
					{
						stringBuilder2.Remove(num4, 1);
					}
					stringBuilder2.Append(path);
					path = StringBuilderCache.GetStringAndRelease(stringBuilder2);
				}
				else
				{
					path = AppDomain.NormalizePath(path, true);
				}
			}
			return path;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001DCA0 File Offset: 0x0001BEA0
		private bool IsFilePath(string path)
		{
			return path[1] == ':' || (path[0] == '\\' && path[1] == '\\');
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0001DCC7 File Offset: 0x0001BEC7
		internal static string ApplicationBaseKey
		{
			get
			{
				return "APPBASE";
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0001DCCE File Offset: 0x0001BECE
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x0001DCDF File Offset: 0x0001BEDF
		public string ConfigurationFile
		{
			[SecuritySafeCritical]
			get
			{
				return this.VerifyDir(this.Value[1], true);
			}
			set
			{
				this.Value[1] = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0001DCEA File Offset: 0x0001BEEA
		internal string ConfigurationFileInternal
		{
			get
			{
				return this.NormalizePath(this.Value[1], true);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0001DCFB File Offset: 0x0001BEFB
		internal static string ConfigurationFileKey
		{
			get
			{
				return "APP_CONFIG_FILE";
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001DD02 File Offset: 0x0001BF02
		public byte[] GetConfigurationBytes()
		{
			if (this._ConfigurationBytes == null)
			{
				return null;
			}
			return (byte[])this._ConfigurationBytes.Clone();
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001DD1E File Offset: 0x0001BF1E
		public void SetConfigurationBytes(byte[] value)
		{
			this._ConfigurationBytes = value;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0001DD27 File Offset: 0x0001BF27
		private static string ConfigurationBytesKey
		{
			get
			{
				return "APP_CONFIG_BLOB";
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001DD2E File Offset: 0x0001BF2E
		internal Dictionary<string, object> GetCompatibilityFlags()
		{
			return this._CompatFlags;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001DD38 File Offset: 0x0001BF38
		public void SetCompatibilitySwitches(IEnumerable<string> switches)
		{
			if (this._AppDomainSortingSetupInfo != null)
			{
				this._AppDomainSortingSetupInfo._useV2LegacySorting = false;
				this._AppDomainSortingSetupInfo._useV4LegacySorting = false;
			}
			this._UseRandomizedStringHashing = false;
			if (switches != null)
			{
				this._CompatFlags = new Dictionary<string, object>();
				using (IEnumerator<string> enumerator = switches.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						if (StringComparer.OrdinalIgnoreCase.Equals("NetFx40_Legacy20SortingBehavior", text))
						{
							if (this._AppDomainSortingSetupInfo == null)
							{
								this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo();
							}
							this._AppDomainSortingSetupInfo._useV2LegacySorting = true;
						}
						if (StringComparer.OrdinalIgnoreCase.Equals("NetFx45_Legacy40SortingBehavior", text))
						{
							if (this._AppDomainSortingSetupInfo == null)
							{
								this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo();
							}
							this._AppDomainSortingSetupInfo._useV4LegacySorting = true;
						}
						if (StringComparer.OrdinalIgnoreCase.Equals("UseRandomizedStringHashAlgorithm", text))
						{
							this._UseRandomizedStringHashing = true;
						}
						this._CompatFlags.Add(text, null);
					}
					return;
				}
			}
			this._CompatFlags = null;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0001DE48 File Offset: 0x0001C048
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x0001DE50 File Offset: 0x0001C050
		public string TargetFrameworkName
		{
			get
			{
				return this._TargetFrameworkName;
			}
			set
			{
				this._TargetFrameworkName = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0001DE59 File Offset: 0x0001C059
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x0001DE61 File Offset: 0x0001C061
		internal bool CheckedForTargetFrameworkName
		{
			get
			{
				return this._CheckedForTargetFrameworkName;
			}
			set
			{
				this._CheckedForTargetFrameworkName = value;
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001DE6C File Offset: 0x0001C06C
		[SecurityCritical]
		public void SetNativeFunction(string functionName, int functionVersion, IntPtr functionPointer)
		{
			if (functionName == null)
			{
				throw new ArgumentNullException("functionName");
			}
			if (functionPointer == IntPtr.Zero)
			{
				throw new ArgumentNullException("functionPointer");
			}
			if (string.IsNullOrWhiteSpace(functionName))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NPMSInvalidName"), "functionName");
			}
			if (functionVersion < 1)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_MinSortingVersion", new object[]
				{
					1,
					functionName
				}));
			}
			if (this._AppDomainSortingSetupInfo == null)
			{
				this._AppDomainSortingSetupInfo = new AppDomainSortingSetupInfo();
			}
			if (string.Equals(functionName, "IsNLSDefinedString", StringComparison.OrdinalIgnoreCase))
			{
				this._AppDomainSortingSetupInfo._pfnIsNLSDefinedString = functionPointer;
			}
			if (string.Equals(functionName, "CompareStringEx", StringComparison.OrdinalIgnoreCase))
			{
				this._AppDomainSortingSetupInfo._pfnCompareStringEx = functionPointer;
			}
			if (string.Equals(functionName, "LCMapStringEx", StringComparison.OrdinalIgnoreCase))
			{
				this._AppDomainSortingSetupInfo._pfnLCMapStringEx = functionPointer;
			}
			if (string.Equals(functionName, "FindNLSStringEx", StringComparison.OrdinalIgnoreCase))
			{
				this._AppDomainSortingSetupInfo._pfnFindNLSStringEx = functionPointer;
			}
			if (string.Equals(functionName, "CompareStringOrdinal", StringComparison.OrdinalIgnoreCase))
			{
				this._AppDomainSortingSetupInfo._pfnCompareStringOrdinal = functionPointer;
			}
			if (string.Equals(functionName, "GetNLSVersionEx", StringComparison.OrdinalIgnoreCase))
			{
				this._AppDomainSortingSetupInfo._pfnGetNLSVersionEx = functionPointer;
			}
			if (string.Equals(functionName, "FindStringOrdinal", StringComparison.OrdinalIgnoreCase))
			{
				this._AppDomainSortingSetupInfo._pfnFindStringOrdinal = functionPointer;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0001DFAC File Offset: 0x0001C1AC
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x0001DFC0 File Offset: 0x0001C1C0
		public string DynamicBase
		{
			[SecuritySafeCritical]
			get
			{
				return this.VerifyDir(this.Value[2], true);
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					this.Value[2] = null;
					return;
				}
				if (this.ApplicationName == null)
				{
					throw new MemberAccessException(Environment.GetResourceString("AppDomain_RequireApplicationName"));
				}
				StringBuilder stringBuilder = new StringBuilder(this.NormalizePath(value, false));
				stringBuilder.Append('\\');
				string value2 = ParseNumbers.IntToString(this.ApplicationName.GetLegacyNonRandomizedHashCode(), 16, 8, '0', 256);
				stringBuilder.Append(value2);
				this.Value[2] = stringBuilder.ToString();
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0001E03A File Offset: 0x0001C23A
		internal static string DynamicBaseKey
		{
			get
			{
				return "DYNAMIC_BASE";
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0001E041 File Offset: 0x0001C241
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x0001E04F File Offset: 0x0001C24F
		public bool DisallowPublisherPolicy
		{
			get
			{
				return this.Value[11] != null;
			}
			set
			{
				if (value)
				{
					this.Value[11] = "true";
					return;
				}
				this.Value[11] = null;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0001E06D File Offset: 0x0001C26D
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x0001E07B File Offset: 0x0001C27B
		public bool DisallowBindingRedirects
		{
			get
			{
				return this.Value[13] != null;
			}
			set
			{
				if (value)
				{
					this.Value[13] = "true";
					return;
				}
				this.Value[13] = null;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0001E099 File Offset: 0x0001C299
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0001E0A7 File Offset: 0x0001C2A7
		public bool DisallowCodeDownload
		{
			get
			{
				return this.Value[12] != null;
			}
			set
			{
				if (value)
				{
					this.Value[12] = "true";
					return;
				}
				this.Value[12] = null;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0001E0C5 File Offset: 0x0001C2C5
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x0001E0D3 File Offset: 0x0001C2D3
		public bool DisallowApplicationBaseProbing
		{
			get
			{
				return this.Value[14] != null;
			}
			set
			{
				if (value)
				{
					this.Value[14] = "true";
					return;
				}
				this.Value[14] = null;
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001E0F1 File Offset: 0x0001C2F1
		[SecurityCritical]
		private string VerifyDir(string dir, bool normalize)
		{
			if (dir != null)
			{
				if (dir.Length == 0)
				{
					dir = null;
				}
				else
				{
					if (normalize)
					{
						dir = this.NormalizePath(dir, true);
					}
					if (this.IsFilePath(dir))
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[]
						{
							dir
						}, false, false).Demand();
					}
				}
			}
			return dir;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001E134 File Offset: 0x0001C334
		[SecurityCritical]
		private void VerifyDirList(string dirs)
		{
			if (dirs != null)
			{
				string[] array = dirs.Split(new char[]
				{
					';'
				});
				int num = array.Length;
				for (int i = 0; i < num; i++)
				{
					this.VerifyDir(array[i], true);
				}
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0001E174 File Offset: 0x0001C374
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0001E194 File Offset: 0x0001C394
		internal string DeveloperPath
		{
			[SecurityCritical]
			get
			{
				string text = this.Value[3];
				this.VerifyDirList(text);
				return text;
			}
			set
			{
				if (value == null)
				{
					this.Value[3] = null;
					return;
				}
				string[] array = value.Split(new char[]
				{
					';'
				});
				int num = array.Length;
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				bool flag = false;
				for (int i = 0; i < num; i++)
				{
					if (array[i].Length != 0)
					{
						if (flag)
						{
							stringBuilder.Append(";");
						}
						else
						{
							flag = true;
						}
						stringBuilder.Append(Path.GetFullPathInternal(array[i]));
					}
				}
				string stringAndRelease = StringBuilderCache.GetStringAndRelease(stringBuilder);
				if (stringAndRelease.Length == 0)
				{
					this.Value[3] = null;
					return;
				}
				this.Value[3] = stringAndRelease;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0001E231 File Offset: 0x0001C431
		internal static string DisallowPublisherPolicyKey
		{
			get
			{
				return "DISALLOW_APP";
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0001E238 File Offset: 0x0001C438
		internal static string DisallowCodeDownloadKey
		{
			get
			{
				return "CODE_DOWNLOAD_DISABLED";
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0001E23F File Offset: 0x0001C43F
		internal static string DisallowBindingRedirectsKey
		{
			get
			{
				return "DISALLOW_APP_REDIRECTS";
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0001E246 File Offset: 0x0001C446
		internal static string DeveloperPathKey
		{
			get
			{
				return "DEV_PATH";
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0001E24D File Offset: 0x0001C44D
		internal static string DisallowAppBaseProbingKey
		{
			get
			{
				return "DISALLOW_APP_BASE_PROBING";
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0001E254 File Offset: 0x0001C454
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x0001E25E File Offset: 0x0001C45E
		public string ApplicationName
		{
			get
			{
				return this.Value[4];
			}
			set
			{
				this.Value[4] = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0001E269 File Offset: 0x0001C469
		internal static string ApplicationNameKey
		{
			get
			{
				return "APP_NAME";
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0001E270 File Offset: 0x0001C470
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x0001E278 File Offset: 0x0001C478
		[XmlIgnoreMember]
		public AppDomainInitializer AppDomainInitializer
		{
			get
			{
				return this._AppDomainInitializer;
			}
			set
			{
				this._AppDomainInitializer = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0001E281 File Offset: 0x0001C481
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x0001E289 File Offset: 0x0001C489
		public string[] AppDomainInitializerArguments
		{
			get
			{
				return this._AppDomainInitializerArguments;
			}
			set
			{
				this._AppDomainInitializerArguments = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0001E292 File Offset: 0x0001C492
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x0001E29A File Offset: 0x0001C49A
		[XmlIgnoreMember]
		public ActivationArguments ActivationArguments
		{
			get
			{
				return this._ActivationArguments;
			}
			set
			{
				this._ActivationArguments = value;
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001E2A4 File Offset: 0x0001C4A4
		internal ApplicationTrust InternalGetApplicationTrust()
		{
			if (this._ApplicationTrust == null)
			{
				return null;
			}
			SecurityElement element = SecurityElement.FromString(this._ApplicationTrust);
			ApplicationTrust applicationTrust = new ApplicationTrust();
			applicationTrust.FromXml(element);
			return applicationTrust;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001E2D5 File Offset: 0x0001C4D5
		internal void InternalSetApplicationTrust(ApplicationTrust value)
		{
			if (value != null)
			{
				this._ApplicationTrust = value.ToXml().ToString();
				return;
			}
			this._ApplicationTrust = null;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001E2F3 File Offset: 0x0001C4F3
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x0001E2FB File Offset: 0x0001C4FB
		[XmlIgnoreMember]
		public ApplicationTrust ApplicationTrust
		{
			get
			{
				return this.InternalGetApplicationTrust();
			}
			set
			{
				this.InternalSetApplicationTrust(value);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0001E304 File Offset: 0x0001C504
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x0001E322 File Offset: 0x0001C522
		public string PrivateBinPath
		{
			[SecuritySafeCritical]
			get
			{
				string text = this.Value[5];
				this.VerifyDirList(text);
				return text;
			}
			set
			{
				this.Value[5] = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0001E32D File Offset: 0x0001C52D
		internal static string PrivateBinPathKey
		{
			get
			{
				return "PRIVATE_BINPATH";
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0001E334 File Offset: 0x0001C534
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x0001E33E File Offset: 0x0001C53E
		public string PrivateBinPathProbe
		{
			get
			{
				return this.Value[6];
			}
			set
			{
				this.Value[6] = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0001E349 File Offset: 0x0001C549
		internal static string PrivateBinPathProbeKey
		{
			get
			{
				return "BINPATH_PROBE_ONLY";
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0001E350 File Offset: 0x0001C550
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x0001E36E File Offset: 0x0001C56E
		public string ShadowCopyDirectories
		{
			[SecuritySafeCritical]
			get
			{
				string text = this.Value[7];
				this.VerifyDirList(text);
				return text;
			}
			set
			{
				this.Value[7] = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0001E379 File Offset: 0x0001C579
		internal static string ShadowCopyDirectoriesKey
		{
			get
			{
				return "SHADOW_COPY_DIRS";
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0001E380 File Offset: 0x0001C580
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x0001E38A File Offset: 0x0001C58A
		public string ShadowCopyFiles
		{
			get
			{
				return this.Value[8];
			}
			set
			{
				if (value != null && string.Compare(value, "true", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.Value[8] = value;
					return;
				}
				this.Value[8] = null;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0001E3B0 File Offset: 0x0001C5B0
		internal static string ShadowCopyFilesKey
		{
			get
			{
				return "FORCE_CACHE_INSTALL";
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0001E3B7 File Offset: 0x0001C5B7
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x0001E3C9 File Offset: 0x0001C5C9
		public string CachePath
		{
			[SecuritySafeCritical]
			get
			{
				return this.VerifyDir(this.Value[9], false);
			}
			set
			{
				this.Value[9] = this.NormalizePath(value, false);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0001E3DC File Offset: 0x0001C5DC
		internal static string CachePathKey
		{
			get
			{
				return "CACHE_BASE";
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0001E3E3 File Offset: 0x0001C5E3
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x0001E3F5 File Offset: 0x0001C5F5
		public string LicenseFile
		{
			[SecuritySafeCritical]
			get
			{
				return this.VerifyDir(this.Value[10], true);
			}
			set
			{
				this.Value[10] = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0001E401 File Offset: 0x0001C601
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x0001E409 File Offset: 0x0001C609
		public LoaderOptimization LoaderOptimization
		{
			get
			{
				return this._LoaderOptimization;
			}
			set
			{
				this._LoaderOptimization = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0001E412 File Offset: 0x0001C612
		internal static string LoaderOptimizationKey
		{
			get
			{
				return "LOADER_OPTIMIZATION";
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0001E419 File Offset: 0x0001C619
		internal static string ConfigurationExtension
		{
			get
			{
				return ".config";
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0001E420 File Offset: 0x0001C620
		internal static string PrivateBinPathEnvironmentVariable
		{
			get
			{
				return "RELPATH";
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001E427 File Offset: 0x0001C627
		internal static string RuntimeConfigurationFile
		{
			get
			{
				return "config\\machine.config";
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0001E42E File Offset: 0x0001C62E
		internal static string MachineConfigKey
		{
			get
			{
				return "MACHINE_CONFIG";
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0001E435 File Offset: 0x0001C635
		internal static string HostBindingKey
		{
			get
			{
				return "HOST_CONFIG";
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001E43C File Offset: 0x0001C63C
		[SecurityCritical]
		internal bool UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation FieldValue, string FieldKey, string UpdatedField, IntPtr fusionContext, AppDomainSetup oldADS)
		{
			string text = this.Value[(int)FieldValue];
			string b = (oldADS == null) ? null : oldADS.Value[(int)FieldValue];
			if (text != b)
			{
				AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, (UpdatedField == null) ? text : UpdatedField);
				return true;
			}
			return false;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001E47E File Offset: 0x0001C67E
		[SecurityCritical]
		internal void UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation FieldValue, string FieldKey, IntPtr fusionContext, AppDomainSetup oldADS)
		{
			if (this.Value[(int)FieldValue] != null)
			{
				AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, "true");
				return;
			}
			if (oldADS != null && oldADS.Value[(int)FieldValue] != null)
			{
				AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, "false");
			}
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001E4B4 File Offset: 0x0001C6B4
		[SecurityCritical]
		internal static bool ByteArraysAreDifferent(byte[] A, byte[] B)
		{
			int num = A.Length;
			if (num != B.Length)
			{
				return true;
			}
			for (int i = 0; i < num; i++)
			{
				if (A[i] != B[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001E4E4 File Offset: 0x0001C6E4
		[SecurityCritical]
		internal static void UpdateByteArrayContextPropertyIfNeeded(byte[] NewArray, byte[] OldArray, string FieldKey, IntPtr fusionContext)
		{
			if ((NewArray != null && OldArray == null) || (NewArray == null && OldArray != null) || (NewArray != null && OldArray != null && AppDomainSetup.ByteArraysAreDifferent(NewArray, OldArray)))
			{
				AppDomainSetup.UpdateContextProperty(fusionContext, FieldKey, NewArray);
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001E50C File Offset: 0x0001C70C
		[SecurityCritical]
		internal void SetupFusionContext(IntPtr fusionContext, AppDomainSetup oldADS)
		{
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ApplicationBaseValue, AppDomainSetup.ApplicationBaseKey, null, fusionContext, oldADS);
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.PrivateBinPathValue, AppDomainSetup.PrivateBinPathKey, null, fusionContext, oldADS);
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DevPathValue, AppDomainSetup.DeveloperPathKey, null, fusionContext, oldADS);
			this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowPublisherPolicyValue, AppDomainSetup.DisallowPublisherPolicyKey, fusionContext, oldADS);
			this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowCodeDownloadValue, AppDomainSetup.DisallowCodeDownloadKey, fusionContext, oldADS);
			this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowBindingRedirectsValue, AppDomainSetup.DisallowBindingRedirectsKey, fusionContext, oldADS);
			this.UpdateBooleanContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DisallowAppBaseProbingValue, AppDomainSetup.DisallowAppBaseProbingKey, fusionContext, oldADS);
			if (this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ShadowCopyFilesValue, AppDomainSetup.ShadowCopyFilesKey, this.ShadowCopyFiles, fusionContext, oldADS))
			{
				if (this.Value[7] == null)
				{
					this.ShadowCopyDirectories = this.BuildShadowCopyDirectories();
				}
				this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ShadowCopyDirectoriesValue, AppDomainSetup.ShadowCopyDirectoriesKey, null, fusionContext, oldADS);
			}
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.CachePathValue, AppDomainSetup.CachePathKey, null, fusionContext, oldADS);
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.PrivateBinPathProbeValue, AppDomainSetup.PrivateBinPathProbeKey, this.PrivateBinPathProbe, fusionContext, oldADS);
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ConfigurationFileValue, AppDomainSetup.ConfigurationFileKey, null, fusionContext, oldADS);
			AppDomainSetup.UpdateByteArrayContextPropertyIfNeeded(this._ConfigurationBytes, (oldADS == null) ? null : oldADS.GetConfigurationBytes(), AppDomainSetup.ConfigurationBytesKey, fusionContext);
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.ApplicationNameValue, AppDomainSetup.ApplicationNameKey, this.ApplicationName, fusionContext, oldADS);
			this.UpdateContextPropertyIfNeeded(AppDomainSetup.LoaderInformation.DynamicBaseValue, AppDomainSetup.DynamicBaseKey, null, fusionContext, oldADS);
			AppDomainSetup.UpdateContextProperty(fusionContext, AppDomainSetup.MachineConfigKey, RuntimeEnvironment.GetRuntimeDirectoryImpl() + AppDomainSetup.RuntimeConfigurationFile);
			string hostBindingFile = RuntimeEnvironment.GetHostBindingFile();
			if (hostBindingFile != null || oldADS != null)
			{
				AppDomainSetup.UpdateContextProperty(fusionContext, AppDomainSetup.HostBindingKey, hostBindingFile);
			}
		}

		// Token: 0x06000947 RID: 2375
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UpdateContextProperty(IntPtr fusionContext, string key, object value);

		// Token: 0x06000948 RID: 2376 RVA: 0x0001E66C File Offset: 0x0001C86C
		internal static int Locate(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return -1;
			}
			char c = s[0];
			if (c <= 'L')
			{
				switch (c)
				{
				case 'A':
					if (s == "APP_CONFIG_FILE")
					{
						return 1;
					}
					if (s == "APP_NAME")
					{
						return 4;
					}
					if (s == "APPBASE")
					{
						return 0;
					}
					if (s == "APP_CONFIG_BLOB")
					{
						return 15;
					}
					break;
				case 'B':
					if (s == "BINPATH_PROBE_ONLY")
					{
						return 6;
					}
					break;
				case 'C':
					if (s == "CACHE_BASE")
					{
						return 9;
					}
					if (s == "CODE_DOWNLOAD_DISABLED")
					{
						return 12;
					}
					break;
				case 'D':
					if (s == "DEV_PATH")
					{
						return 3;
					}
					if (s == "DYNAMIC_BASE")
					{
						return 2;
					}
					if (s == "DISALLOW_APP")
					{
						return 11;
					}
					if (s == "DISALLOW_APP_REDIRECTS")
					{
						return 13;
					}
					if (s == "DISALLOW_APP_BASE_PROBING")
					{
						return 14;
					}
					break;
				case 'E':
					break;
				case 'F':
					if (s == "FORCE_CACHE_INSTALL")
					{
						return 8;
					}
					break;
				default:
					if (c == 'L')
					{
						if (s == "LICENSE_FILE")
						{
							return 10;
						}
					}
					break;
				}
			}
			else if (c != 'P')
			{
				if (c == 'S')
				{
					if (s == "SHADOW_COPY_DIRS")
					{
						return 7;
					}
				}
			}
			else if (s == "PRIVATE_BINPATH")
			{
				return 5;
			}
			return -1;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001E7D4 File Offset: 0x0001C9D4
		private string BuildShadowCopyDirectories()
		{
			string text = this.Value[5];
			if (text == null)
			{
				return null;
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			string text2 = this.Value[0];
			if (text2 != null)
			{
				char[] separator = new char[]
				{
					';'
				};
				string[] array = text.Split(separator);
				int num = array.Length;
				bool flag = text2[text2.Length - 1] != '/' && text2[text2.Length - 1] != '\\';
				if (num == 0)
				{
					stringBuilder.Append(text2);
					if (flag)
					{
						stringBuilder.Append('\\');
					}
					stringBuilder.Append(text);
				}
				else
				{
					for (int i = 0; i < num; i++)
					{
						stringBuilder.Append(text2);
						if (flag)
						{
							stringBuilder.Append('\\');
						}
						stringBuilder.Append(array[i]);
						if (i < num - 1)
						{
							stringBuilder.Append(';');
						}
					}
				}
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0001E8B9 File Offset: 0x0001CAB9
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0001E8C1 File Offset: 0x0001CAC1
		public bool SandboxInterop
		{
			get
			{
				return this._DisableInterfaceCache;
			}
			set
			{
				this._DisableInterfaceCache = value;
			}
		}

		// Token: 0x040003A6 RID: 934
		private string[] _Entries;

		// Token: 0x040003A7 RID: 935
		private LoaderOptimization _LoaderOptimization;

		// Token: 0x040003A8 RID: 936
		private string _AppBase;

		// Token: 0x040003A9 RID: 937
		[OptionalField(VersionAdded = 2)]
		private AppDomainInitializer _AppDomainInitializer;

		// Token: 0x040003AA RID: 938
		[OptionalField(VersionAdded = 2)]
		private string[] _AppDomainInitializerArguments;

		// Token: 0x040003AB RID: 939
		[OptionalField(VersionAdded = 2)]
		private ActivationArguments _ActivationArguments;

		// Token: 0x040003AC RID: 940
		[OptionalField(VersionAdded = 2)]
		private string _ApplicationTrust;

		// Token: 0x040003AD RID: 941
		[OptionalField(VersionAdded = 2)]
		private byte[] _ConfigurationBytes;

		// Token: 0x040003AE RID: 942
		[OptionalField(VersionAdded = 3)]
		private bool _DisableInterfaceCache;

		// Token: 0x040003AF RID: 943
		[OptionalField(VersionAdded = 4)]
		private string _AppDomainManagerAssembly;

		// Token: 0x040003B0 RID: 944
		[OptionalField(VersionAdded = 4)]
		private string _AppDomainManagerType;

		// Token: 0x040003B1 RID: 945
		[OptionalField(VersionAdded = 4)]
		private string[] _AptcaVisibleAssemblies;

		// Token: 0x040003B2 RID: 946
		[OptionalField(VersionAdded = 4)]
		private Dictionary<string, object> _CompatFlags;

		// Token: 0x040003B3 RID: 947
		[OptionalField(VersionAdded = 5)]
		private string _TargetFrameworkName;

		// Token: 0x040003B4 RID: 948
		[NonSerialized]
		internal AppDomainSortingSetupInfo _AppDomainSortingSetupInfo;

		// Token: 0x040003B5 RID: 949
		[OptionalField(VersionAdded = 5)]
		private bool _CheckedForTargetFrameworkName;

		// Token: 0x040003B6 RID: 950
		[OptionalField(VersionAdded = 5)]
		private bool _UseRandomizedStringHashing;

		// Token: 0x02000AA2 RID: 2722
		[Serializable]
		internal enum LoaderInformation
		{
			// Token: 0x04003025 RID: 12325
			ApplicationBaseValue,
			// Token: 0x04003026 RID: 12326
			ConfigurationFileValue,
			// Token: 0x04003027 RID: 12327
			DynamicBaseValue,
			// Token: 0x04003028 RID: 12328
			DevPathValue,
			// Token: 0x04003029 RID: 12329
			ApplicationNameValue,
			// Token: 0x0400302A RID: 12330
			PrivateBinPathValue,
			// Token: 0x0400302B RID: 12331
			PrivateBinPathProbeValue,
			// Token: 0x0400302C RID: 12332
			ShadowCopyDirectoriesValue,
			// Token: 0x0400302D RID: 12333
			ShadowCopyFilesValue,
			// Token: 0x0400302E RID: 12334
			CachePathValue,
			// Token: 0x0400302F RID: 12335
			LicenseFileValue,
			// Token: 0x04003030 RID: 12336
			DisallowPublisherPolicyValue,
			// Token: 0x04003031 RID: 12337
			DisallowCodeDownloadValue,
			// Token: 0x04003032 RID: 12338
			DisallowBindingRedirectsValue,
			// Token: 0x04003033 RID: 12339
			DisallowAppBaseProbingValue,
			// Token: 0x04003034 RID: 12340
			ConfigurationBytesValue,
			// Token: 0x04003035 RID: 12341
			LoaderMaximum = 18
		}
	}
}
