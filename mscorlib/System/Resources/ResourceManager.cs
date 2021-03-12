using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Resources
{
	// Token: 0x0200036A RID: 874
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ResourceManager
	{
		// Token: 0x06002BF8 RID: 11256 RVA: 0x000A588C File Offset: 0x000A3A8C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Init()
		{
			this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000A58A0 File Offset: 0x000A3AA0
		protected ResourceManager()
		{
			this.Init();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			ResourceManager.ResourceManagerMediator mediator = new ResourceManager.ResourceManagerMediator(this);
			this.resourceGroveler = new ManifestBasedResourceGroveler(mediator);
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000A58D8 File Offset: 0x000A3AD8
		private ResourceManager(string baseName, string resourceDir, Type usingResourceSet)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (resourceDir == null)
			{
				throw new ArgumentNullException("resourceDir");
			}
			this.BaseNameField = baseName;
			this.moduleDir = resourceDir;
			this._userResourceSet = usingResourceSet;
			this.ResourceSets = new Hashtable();
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			this.UseManifest = false;
			ResourceManager.ResourceManagerMediator mediator = new ResourceManager.ResourceManagerMediator(this);
			this.resourceGroveler = new FileBasedResourceGroveler(mediator);
			if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled())
			{
				CultureInfo invariantCulture = CultureInfo.InvariantCulture;
				string resourceFileName = this.GetResourceFileName(invariantCulture);
				if (this.resourceGroveler.HasNeutralResources(invariantCulture, resourceFileName))
				{
					FrameworkEventSource.Log.ResourceManagerNeutralResourcesFound(this.BaseNameField, this.MainAssembly, resourceFileName);
					return;
				}
				FrameworkEventSource.Log.ResourceManagerNeutralResourcesNotFound(this.BaseNameField, this.MainAssembly, resourceFileName);
			}
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000A59B8 File Offset: 0x000A3BB8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceManager(string baseName, Assembly assembly)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (null == assembly)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!(assembly is RuntimeAssembly))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
			}
			this.MainAssembly = assembly;
			this.BaseNameField = baseName;
			this.SetAppXConfiguration();
			this.CommonAssemblyInit();
			this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
			if (assembly == typeof(object).Assembly && this.m_callingAssembly != assembly)
			{
				this.m_callingAssembly = null;
			}
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000A5A5C File Offset: 0x000A3C5C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceManager(string baseName, Assembly assembly, Type usingResourceSet)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (null == assembly)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!(assembly is RuntimeAssembly))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
			}
			this.MainAssembly = assembly;
			this.BaseNameField = baseName;
			if (usingResourceSet != null && usingResourceSet != ResourceManager._minResourceSet && !usingResourceSet.IsSubclassOf(ResourceManager._minResourceSet))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ResMgrNotResSet"), "usingResourceSet");
			}
			this._userResourceSet = usingResourceSet;
			this.CommonAssemblyInit();
			this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
			if (assembly == typeof(object).Assembly && this.m_callingAssembly != assembly)
			{
				this.m_callingAssembly = null;
			}
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000A5B38 File Offset: 0x000A3D38
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceManager(Type resourceSource)
		{
			if (null == resourceSource)
			{
				throw new ArgumentNullException("resourceSource");
			}
			if (!(resourceSource is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			this._locationInfo = resourceSource;
			this.MainAssembly = this._locationInfo.Assembly;
			this.BaseNameField = resourceSource.Name;
			this.SetAppXConfiguration();
			this.CommonAssemblyInit();
			this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
			if (this.MainAssembly == typeof(object).Assembly && this.m_callingAssembly != this.MainAssembly)
			{
				this.m_callingAssembly = null;
			}
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000A5BED File Offset: 0x000A3DED
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this._resourceSets = null;
			this.resourceGroveler = null;
			this._lastUsedResourceCache = null;
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000A5C04 File Offset: 0x000A3E04
		[SecuritySafeCritical]
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			ResourceManager.ResourceManagerMediator mediator = new ResourceManager.ResourceManagerMediator(this);
			if (this.UseManifest)
			{
				this.resourceGroveler = new ManifestBasedResourceGroveler(mediator);
			}
			else
			{
				this.resourceGroveler = new FileBasedResourceGroveler(mediator);
			}
			if (this.m_callingAssembly == null)
			{
				this.m_callingAssembly = (RuntimeAssembly)this._callingAssembly;
			}
			if (this.UseManifest && this._neutralResourcesCulture == null)
			{
				this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, ref this._fallbackLoc);
			}
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000A5C96 File Offset: 0x000A3E96
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this._callingAssembly = this.m_callingAssembly;
			this.UseSatelliteAssem = this.UseManifest;
			this.ResourceSets = new Hashtable();
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000A5CBC File Offset: 0x000A3EBC
		[SecuritySafeCritical]
		private void CommonAssemblyInit()
		{
			if (!this._bUsingModernResourceManagement)
			{
				this.UseManifest = true;
				this._resourceSets = new Dictionary<string, ResourceSet>();
				this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
				this._fallbackLoc = UltimateResourceFallbackLocation.MainAssembly;
				ResourceManager.ResourceManagerMediator mediator = new ResourceManager.ResourceManagerMediator(this);
				this.resourceGroveler = new ManifestBasedResourceGroveler(mediator);
			}
			this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, ref this._fallbackLoc);
			if (!this._bUsingModernResourceManagement)
			{
				if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled())
				{
					CultureInfo invariantCulture = CultureInfo.InvariantCulture;
					string resourceFileName = this.GetResourceFileName(invariantCulture);
					if (this.resourceGroveler.HasNeutralResources(invariantCulture, resourceFileName))
					{
						FrameworkEventSource.Log.ResourceManagerNeutralResourcesFound(this.BaseNameField, this.MainAssembly, resourceFileName);
					}
					else
					{
						string resName = resourceFileName;
						if (this._locationInfo != null && this._locationInfo.Namespace != null)
						{
							resName = this._locationInfo.Namespace + Type.Delimiter.ToString() + resourceFileName;
						}
						FrameworkEventSource.Log.ResourceManagerNeutralResourcesNotFound(this.BaseNameField, this.MainAssembly, resName);
					}
				}
				this.ResourceSets = new Hashtable();
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x000A5DD8 File Offset: 0x000A3FD8
		public virtual string BaseName
		{
			get
			{
				return this.BaseNameField;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x000A5DE0 File Offset: 0x000A3FE0
		// (set) Token: 0x06002C04 RID: 11268 RVA: 0x000A5DE8 File Offset: 0x000A3FE8
		public virtual bool IgnoreCase
		{
			get
			{
				return this._ignoreCase;
			}
			set
			{
				this._ignoreCase = value;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x000A5DF1 File Offset: 0x000A3FF1
		public virtual Type ResourceSetType
		{
			get
			{
				if (!(this._userResourceSet == null))
				{
					return this._userResourceSet;
				}
				return typeof(RuntimeResourceSet);
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06002C06 RID: 11270 RVA: 0x000A5E12 File Offset: 0x000A4012
		// (set) Token: 0x06002C07 RID: 11271 RVA: 0x000A5E1A File Offset: 0x000A401A
		protected UltimateResourceFallbackLocation FallbackLocation
		{
			get
			{
				return this._fallbackLoc;
			}
			set
			{
				this._fallbackLoc = value;
			}
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x000A5E24 File Offset: 0x000A4024
		public virtual void ReleaseAllResources()
		{
			if (FrameworkEventSource.IsInitialized)
			{
				FrameworkEventSource.Log.ResourceManagerReleasingResources(this.BaseNameField, this.MainAssembly);
			}
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			Dictionary<string, ResourceSet> obj = resourceSets;
			lock (obj)
			{
				IDictionaryEnumerator dictionaryEnumerator = resourceSets.GetEnumerator();
				IDictionaryEnumerator dictionaryEnumerator2 = null;
				if (this.ResourceSets != null)
				{
					dictionaryEnumerator2 = this.ResourceSets.GetEnumerator();
				}
				this.ResourceSets = new Hashtable();
				while (dictionaryEnumerator.MoveNext())
				{
					((ResourceSet)dictionaryEnumerator.Value).Close();
				}
				if (dictionaryEnumerator2 != null)
				{
					while (dictionaryEnumerator2.MoveNext())
					{
						((ResourceSet)dictionaryEnumerator2.Value).Close();
					}
				}
			}
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x000A5EFC File Offset: 0x000A40FC
		public static ResourceManager CreateFileBasedResourceManager(string baseName, string resourceDir, Type usingResourceSet)
		{
			return new ResourceManager(baseName, resourceDir, usingResourceSet);
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x000A5F08 File Offset: 0x000A4108
		protected virtual string GetResourceFileName(CultureInfo culture)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			stringBuilder.Append(this.BaseNameField);
			if (!culture.HasInvariantCultureName)
			{
				CultureInfo.VerifyCultureName(culture.Name, true);
				stringBuilder.Append('.');
				stringBuilder.Append(culture.Name);
			}
			stringBuilder.Append(".resources");
			return stringBuilder.ToString();
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x000A5F6C File Offset: 0x000A416C
		internal ResourceSet GetFirstResourceSet(CultureInfo culture)
		{
			if (this._neutralResourcesCulture != null && culture.Name == this._neutralResourcesCulture.Name)
			{
				culture = CultureInfo.InvariantCulture;
			}
			if (this._lastUsedResourceCache != null)
			{
				ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
				lock (lastUsedResourceCache)
				{
					if (culture.Name == this._lastUsedResourceCache.lastCultureName)
					{
						return this._lastUsedResourceCache.lastResourceSet;
					}
				}
			}
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			ResourceSet resourceSet = null;
			if (resourceSets != null)
			{
				Dictionary<string, ResourceSet> obj = resourceSets;
				lock (obj)
				{
					resourceSets.TryGetValue(culture.Name, out resourceSet);
				}
			}
			if (resourceSet != null)
			{
				if (this._lastUsedResourceCache != null)
				{
					ResourceManager.CultureNameResourceSetPair lastUsedResourceCache2 = this._lastUsedResourceCache;
					lock (lastUsedResourceCache2)
					{
						this._lastUsedResourceCache.lastCultureName = culture.Name;
						this._lastUsedResourceCache.lastResourceSet = resourceSet;
					}
				}
				return resourceSet;
			}
			return null;
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000A60A0 File Offset: 0x000A42A0
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public virtual ResourceSet GetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			if (resourceSets != null)
			{
				Dictionary<string, ResourceSet> obj = resourceSets;
				lock (obj)
				{
					ResourceSet result;
					if (resourceSets.TryGetValue(culture.Name, out result))
					{
						return result;
					}
				}
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (this.UseManifest && culture.HasInvariantCultureName)
			{
				string resourceFileName = this.GetResourceFileName(culture);
				RuntimeAssembly runtimeAssembly = (RuntimeAssembly)this.MainAssembly;
				Stream manifestResourceStream = runtimeAssembly.GetManifestResourceStream(this._locationInfo, resourceFileName, this.m_callingAssembly == this.MainAssembly, ref stackCrawlMark);
				if (createIfNotExists && manifestResourceStream != null)
				{
					ResourceSet result = ((ManifestBasedResourceGroveler)this.resourceGroveler).CreateResourceSet(manifestResourceStream, this.MainAssembly);
					ResourceManager.AddResourceSet(resourceSets, culture.Name, ref result);
					return result;
				}
			}
			return this.InternalGetResourceSet(culture, createIfNotExists, tryParents);
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x000A6190 File Offset: 0x000A4390
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.InternalGetResourceSet(culture, createIfNotExists, tryParents, ref stackCrawlMark);
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x000A61AC File Offset: 0x000A43AC
		[SecurityCritical]
		private ResourceSet InternalGetResourceSet(CultureInfo requestedCulture, bool createIfNotExists, bool tryParents, ref StackCrawlMark stackMark)
		{
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			ResourceSet resourceSet = null;
			CultureInfo cultureInfo = null;
			Dictionary<string, ResourceSet> obj = resourceSets;
			lock (obj)
			{
				if (resourceSets.TryGetValue(requestedCulture.Name, out resourceSet))
				{
					if (FrameworkEventSource.IsInitialized)
					{
						FrameworkEventSource.Log.ResourceManagerFoundResourceSetInCache(this.BaseNameField, this.MainAssembly, requestedCulture.Name);
					}
					return resourceSet;
				}
			}
			ResourceFallbackManager resourceFallbackManager = new ResourceFallbackManager(requestedCulture, this._neutralResourcesCulture, tryParents);
			foreach (CultureInfo cultureInfo2 in resourceFallbackManager)
			{
				if (FrameworkEventSource.IsInitialized)
				{
					FrameworkEventSource.Log.ResourceManagerLookingForResourceSet(this.BaseNameField, this.MainAssembly, cultureInfo2.Name);
				}
				Dictionary<string, ResourceSet> obj2 = resourceSets;
				lock (obj2)
				{
					if (resourceSets.TryGetValue(cultureInfo2.Name, out resourceSet))
					{
						if (FrameworkEventSource.IsInitialized)
						{
							FrameworkEventSource.Log.ResourceManagerFoundResourceSetInCache(this.BaseNameField, this.MainAssembly, cultureInfo2.Name);
						}
						if (requestedCulture != cultureInfo2)
						{
							cultureInfo = cultureInfo2;
						}
						break;
					}
				}
				resourceSet = this.resourceGroveler.GrovelForResourceSet(cultureInfo2, resourceSets, tryParents, createIfNotExists, ref stackMark);
				if (resourceSet != null)
				{
					cultureInfo = cultureInfo2;
					break;
				}
			}
			if (resourceSet != null && cultureInfo != null)
			{
				foreach (CultureInfo cultureInfo3 in resourceFallbackManager)
				{
					ResourceManager.AddResourceSet(resourceSets, cultureInfo3.Name, ref resourceSet);
					if (cultureInfo3 == cultureInfo)
					{
						break;
					}
				}
			}
			return resourceSet;
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x000A6374 File Offset: 0x000A4574
		private static void AddResourceSet(Dictionary<string, ResourceSet> localResourceSets, string cultureName, ref ResourceSet rs)
		{
			lock (localResourceSets)
			{
				ResourceSet resourceSet;
				if (localResourceSets.TryGetValue(cultureName, out resourceSet))
				{
					if (resourceSet != rs)
					{
						if (!localResourceSets.ContainsValue(rs))
						{
							rs.Dispose();
						}
						rs = resourceSet;
					}
				}
				else
				{
					localResourceSets.Add(cultureName, rs);
				}
			}
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x000A63D8 File Offset: 0x000A45D8
		protected static Version GetSatelliteContractVersion(Assembly a)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a", Environment.GetResourceString("ArgumentNull_Assembly"));
			}
			string text = null;
			if (a.ReflectionOnly)
			{
				foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(a))
				{
					if (customAttributeData.Constructor.DeclaringType == typeof(SatelliteContractVersionAttribute))
					{
						text = (string)customAttributeData.ConstructorArguments[0].Value;
						break;
					}
				}
				if (text == null)
				{
					return null;
				}
			}
			else
			{
				object[] customAttributes = a.GetCustomAttributes(typeof(SatelliteContractVersionAttribute), false);
				if (customAttributes.Length == 0)
				{
					return null;
				}
				text = ((SatelliteContractVersionAttribute)customAttributes[0]).Version;
			}
			Version result;
			try
			{
				result = new Version(text);
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				if (a == typeof(object).Assembly)
				{
					return null;
				}
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidSatelliteContract_Asm_Ver", new object[]
				{
					a.ToString(),
					text
				}), innerException);
			}
			return result;
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x000A650C File Offset: 0x000A470C
		[SecuritySafeCritical]
		protected static CultureInfo GetNeutralResourcesLanguage(Assembly a)
		{
			UltimateResourceFallbackLocation ultimateResourceFallbackLocation = UltimateResourceFallbackLocation.MainAssembly;
			return ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(a, ref ultimateResourceFallbackLocation);
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000A6528 File Offset: 0x000A4728
		internal static bool CompareNames(string asmTypeName1, string typeName2, AssemblyName asmName2)
		{
			int num = asmTypeName1.IndexOf(',');
			if (((num == -1) ? asmTypeName1.Length : num) != typeName2.Length)
			{
				return false;
			}
			if (string.Compare(asmTypeName1, 0, typeName2, 0, typeName2.Length, StringComparison.Ordinal) != 0)
			{
				return false;
			}
			if (num == -1)
			{
				return true;
			}
			while (char.IsWhiteSpace(asmTypeName1[++num]))
			{
			}
			AssemblyName assemblyName = new AssemblyName(asmTypeName1.Substring(num));
			if (string.Compare(assemblyName.Name, asmName2.Name, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (string.Compare(assemblyName.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (assemblyName.CultureInfo != null && asmName2.CultureInfo != null && assemblyName.CultureInfo.LCID != asmName2.CultureInfo.LCID)
			{
				return false;
			}
			byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
			byte[] publicKeyToken2 = asmName2.GetPublicKeyToken();
			if (publicKeyToken != null && publicKeyToken2 != null)
			{
				if (publicKeyToken.Length != publicKeyToken2.Length)
				{
					return false;
				}
				for (int i = 0; i < publicKeyToken.Length; i++)
				{
					if (publicKeyToken[i] != publicKeyToken2[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000A6620 File Offset: 0x000A4820
		[SecuritySafeCritical]
		private string GetStringFromPRI(string stringName, string startingCulture, string neutralResourcesCulture)
		{
			if (stringName.Length == 0)
			{
				return null;
			}
			return this._WinRTResourceManager.GetString(stringName, string.IsNullOrEmpty(startingCulture) ? null : startingCulture, string.IsNullOrEmpty(neutralResourcesCulture) ? null : neutralResourcesCulture);
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x000A6660 File Offset: 0x000A4860
		[SecurityCritical]
		internal static WindowsRuntimeResourceManagerBase GetWinRTResourceManager()
		{
			Type type = Type.GetType("System.Resources.WindowsRuntimeResourceManager, System.Runtime.WindowsRuntime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", true);
			return (WindowsRuntimeResourceManagerBase)Activator.CreateInstance(type, true);
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000A6688 File Offset: 0x000A4888
		[SecuritySafeCritical]
		private bool ShouldUseSatelliteAssemblyResourceLookupUnderAppX(RuntimeAssembly resourcesAssembly)
		{
			return resourcesAssembly.IsFrameworkAssembly();
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x000A66A0 File Offset: 0x000A48A0
		[SecuritySafeCritical]
		private void SetAppXConfiguration()
		{
			bool flag = false;
			RuntimeAssembly runtimeAssembly = (RuntimeAssembly)this.MainAssembly;
			if (runtimeAssembly == null)
			{
				runtimeAssembly = this.m_callingAssembly;
			}
			if (runtimeAssembly != null && runtimeAssembly != typeof(object).Assembly && AppDomain.IsAppXModel() && !AppDomain.IsAppXNGen)
			{
				ResourceManager.s_IsAppXModel = true;
				string text = (this._locationInfo == null) ? this.BaseNameField : this._locationInfo.FullName;
				if (text == null)
				{
					text = string.Empty;
				}
				WindowsRuntimeResourceManagerBase windowsRuntimeResourceManagerBase = null;
				bool flag2 = false;
				if (AppDomain.IsAppXDesignMode())
				{
					windowsRuntimeResourceManagerBase = ResourceManager.GetWinRTResourceManager();
					try
					{
						PRIExceptionInfo priexceptionInfo;
						flag2 = windowsRuntimeResourceManagerBase.Initialize(runtimeAssembly.Location, text, out priexceptionInfo);
						flag = !flag2;
					}
					catch (Exception ex)
					{
						flag = true;
						if (ex.IsTransient)
						{
							throw;
						}
					}
				}
				if (!flag)
				{
					this._bUsingModernResourceManagement = !this.ShouldUseSatelliteAssemblyResourceLookupUnderAppX(runtimeAssembly);
					if (this._bUsingModernResourceManagement)
					{
						if (windowsRuntimeResourceManagerBase != null && flag2)
						{
							this._WinRTResourceManager = windowsRuntimeResourceManagerBase;
							this._PRIonAppXInitialized = true;
							return;
						}
						this._WinRTResourceManager = ResourceManager.GetWinRTResourceManager();
						try
						{
							this._PRIonAppXInitialized = this._WinRTResourceManager.Initialize(runtimeAssembly.Location, text, out this._PRIExceptionInfo);
						}
						catch (FileNotFoundException)
						{
						}
						catch (Exception ex2)
						{
							if (ex2.HResult != -2147009761)
							{
								throw;
							}
						}
					}
				}
			}
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000A6814 File Offset: 0x000A4A14
		[__DynamicallyInvokable]
		public virtual string GetString(string name)
		{
			return this.GetString(name, null);
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000A6820 File Offset: 0x000A4A20
		[__DynamicallyInvokable]
		public virtual string GetString(string name, CultureInfo culture)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (ResourceManager.s_IsAppXModel && culture == CultureInfo.CurrentUICulture)
			{
				culture = null;
			}
			if (!this._bUsingModernResourceManagement)
			{
				if (culture == null)
				{
					culture = Thread.CurrentThread.GetCurrentUICultureNoAppX();
				}
				if (FrameworkEventSource.IsInitialized)
				{
					FrameworkEventSource.Log.ResourceManagerLookupStarted(this.BaseNameField, this.MainAssembly, culture.Name);
				}
				ResourceSet resourceSet = this.GetFirstResourceSet(culture);
				if (resourceSet != null)
				{
					string @string = resourceSet.GetString(name, this._ignoreCase);
					if (@string != null)
					{
						return @string;
					}
				}
				ResourceFallbackManager resourceFallbackManager = new ResourceFallbackManager(culture, this._neutralResourcesCulture, true);
				foreach (CultureInfo cultureInfo in resourceFallbackManager)
				{
					ResourceSet resourceSet2 = this.InternalGetResourceSet(cultureInfo, true, true);
					if (resourceSet2 == null)
					{
						break;
					}
					if (resourceSet2 != resourceSet)
					{
						string string2 = resourceSet2.GetString(name, this._ignoreCase);
						if (string2 != null)
						{
							if (this._lastUsedResourceCache != null)
							{
								ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
								lock (lastUsedResourceCache)
								{
									this._lastUsedResourceCache.lastCultureName = cultureInfo.Name;
									this._lastUsedResourceCache.lastResourceSet = resourceSet2;
								}
							}
							return string2;
						}
						resourceSet = resourceSet2;
					}
				}
				if (FrameworkEventSource.IsInitialized)
				{
					FrameworkEventSource.Log.ResourceManagerLookupFailed(this.BaseNameField, this.MainAssembly, culture.Name);
				}
				return null;
			}
			if (this._PRIonAppXInitialized)
			{
				return this.GetStringFromPRI(name, (culture == null) ? null : culture.Name, this._neutralResourcesCulture.Name);
			}
			if (this._PRIExceptionInfo != null && this._PRIExceptionInfo._PackageSimpleName != null && this._PRIExceptionInfo._ResWFile != null)
			{
				throw new MissingManifestResourceException(Environment.GetResourceString("MissingManifestResource_ResWFileNotLoaded", new object[]
				{
					this._PRIExceptionInfo._ResWFile,
					this._PRIExceptionInfo._PackageSimpleName
				}));
			}
			throw new MissingManifestResourceException(Environment.GetResourceString("MissingManifestResource_NoPRIresources"));
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x000A6A34 File Offset: 0x000A4C34
		public virtual object GetObject(string name)
		{
			return this.GetObject(name, null, true);
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x000A6A3F File Offset: 0x000A4C3F
		public virtual object GetObject(string name, CultureInfo culture)
		{
			return this.GetObject(name, culture, true);
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x000A6A4C File Offset: 0x000A4C4C
		private object GetObject(string name, CultureInfo culture, bool wrapUnmanagedMemStream)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (ResourceManager.s_IsAppXModel && culture == CultureInfo.CurrentUICulture)
			{
				culture = null;
			}
			if (culture == null)
			{
				culture = Thread.CurrentThread.GetCurrentUICultureNoAppX();
			}
			if (FrameworkEventSource.IsInitialized)
			{
				FrameworkEventSource.Log.ResourceManagerLookupStarted(this.BaseNameField, this.MainAssembly, culture.Name);
			}
			ResourceSet resourceSet = this.GetFirstResourceSet(culture);
			if (resourceSet != null)
			{
				object @object = resourceSet.GetObject(name, this._ignoreCase);
				if (@object != null)
				{
					UnmanagedMemoryStream unmanagedMemoryStream = @object as UnmanagedMemoryStream;
					if (unmanagedMemoryStream != null && wrapUnmanagedMemStream)
					{
						return new UnmanagedMemoryStreamWrapper(unmanagedMemoryStream);
					}
					return @object;
				}
			}
			ResourceFallbackManager resourceFallbackManager = new ResourceFallbackManager(culture, this._neutralResourcesCulture, true);
			foreach (CultureInfo cultureInfo in resourceFallbackManager)
			{
				ResourceSet resourceSet2 = this.InternalGetResourceSet(cultureInfo, true, true);
				if (resourceSet2 == null)
				{
					break;
				}
				if (resourceSet2 != resourceSet)
				{
					object object2 = resourceSet2.GetObject(name, this._ignoreCase);
					if (object2 != null)
					{
						if (this._lastUsedResourceCache != null)
						{
							ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
							lock (lastUsedResourceCache)
							{
								this._lastUsedResourceCache.lastCultureName = cultureInfo.Name;
								this._lastUsedResourceCache.lastResourceSet = resourceSet2;
							}
						}
						UnmanagedMemoryStream unmanagedMemoryStream2 = object2 as UnmanagedMemoryStream;
						if (unmanagedMemoryStream2 != null && wrapUnmanagedMemStream)
						{
							return new UnmanagedMemoryStreamWrapper(unmanagedMemoryStream2);
						}
						return object2;
					}
					else
					{
						resourceSet = resourceSet2;
					}
				}
			}
			if (FrameworkEventSource.IsInitialized)
			{
				FrameworkEventSource.Log.ResourceManagerLookupFailed(this.BaseNameField, this.MainAssembly, culture.Name);
			}
			return null;
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x000A6C04 File Offset: 0x000A4E04
		[ComVisible(false)]
		public UnmanagedMemoryStream GetStream(string name)
		{
			return this.GetStream(name, null);
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x000A6C10 File Offset: 0x000A4E10
		[ComVisible(false)]
		public UnmanagedMemoryStream GetStream(string name, CultureInfo culture)
		{
			object @object = this.GetObject(name, culture, false);
			UnmanagedMemoryStream unmanagedMemoryStream = @object as UnmanagedMemoryStream;
			if (unmanagedMemoryStream == null && @object != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotStream_Name", new object[]
				{
					name
				}));
			}
			return unmanagedMemoryStream;
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000A6C50 File Offset: 0x000A4E50
		[SecurityCritical]
		private bool TryLookingForSatellite(CultureInfo lookForCulture)
		{
			if (!ResourceManager._checkedConfigFile)
			{
				lock (this)
				{
					if (!ResourceManager._checkedConfigFile)
					{
						ResourceManager._checkedConfigFile = true;
						ResourceManager._installedSatelliteInfo = this.GetSatelliteAssembliesFromConfig();
					}
				}
			}
			if (ResourceManager._installedSatelliteInfo == null)
			{
				return true;
			}
			string[] array = (string[])ResourceManager._installedSatelliteInfo[this.MainAssembly.FullName];
			if (array == null)
			{
				return true;
			}
			int num = Array.IndexOf<string>(array, lookForCulture.Name);
			if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled())
			{
				if (num < 0)
				{
					FrameworkEventSource.Log.ResourceManagerCultureNotFoundInConfigFile(this.BaseNameField, this.MainAssembly, lookForCulture.Name);
				}
				else
				{
					FrameworkEventSource.Log.ResourceManagerCultureFoundInConfigFile(this.BaseNameField, this.MainAssembly, lookForCulture.Name);
				}
			}
			return num >= 0;
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x000A6D40 File Offset: 0x000A4F40
		[SecurityCritical]
		private Hashtable GetSatelliteAssembliesFromConfig()
		{
			string configurationFileInternal = AppDomain.CurrentDomain.FusionStore.ConfigurationFileInternal;
			if (configurationFileInternal == null)
			{
				return null;
			}
			if (configurationFileInternal.Length >= 2 && (configurationFileInternal[1] == Path.VolumeSeparatorChar || (configurationFileInternal[0] == Path.DirectorySeparatorChar && configurationFileInternal[1] == Path.DirectorySeparatorChar)) && !File.InternalExists(configurationFileInternal))
			{
				return null;
			}
			ConfigTreeParser configTreeParser = new ConfigTreeParser();
			string configPath = "/configuration/satelliteassemblies";
			ConfigNode configNode = null;
			try
			{
				configNode = configTreeParser.Parse(configurationFileInternal, configPath, true);
			}
			catch (Exception)
			{
			}
			if (configNode == null)
			{
				return null;
			}
			Hashtable hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
			foreach (ConfigNode configNode2 in configNode.Children)
			{
				if (!string.Equals(configNode2.Name, "assembly"))
				{
					throw new ApplicationException(Environment.GetResourceString("XMLSyntax_InvalidSyntaxSatAssemTag", new object[]
					{
						Path.GetFileName(configurationFileInternal),
						configNode2.Name
					}));
				}
				if (configNode2.Attributes.Count == 0)
				{
					throw new ApplicationException(Environment.GetResourceString("XMLSyntax_InvalidSyntaxSatAssemTagNoAttr", new object[]
					{
						Path.GetFileName(configurationFileInternal)
					}));
				}
				DictionaryEntry dictionaryEntry = configNode2.Attributes[0];
				string text = (string)dictionaryEntry.Value;
				if (!object.Equals(dictionaryEntry.Key, "name") || string.IsNullOrEmpty(text) || configNode2.Attributes.Count > 1)
				{
					throw new ApplicationException(Environment.GetResourceString("XMLSyntax_InvalidSyntaxSatAssemTagBadAttr", new object[]
					{
						Path.GetFileName(configurationFileInternal),
						dictionaryEntry.Key,
						dictionaryEntry.Value
					}));
				}
				ArrayList arrayList = new ArrayList(5);
				foreach (ConfigNode configNode3 in configNode2.Children)
				{
					if (configNode3.Value != null)
					{
						arrayList.Add(configNode3.Value);
					}
				}
				string[] array = new string[arrayList.Count];
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = (string)arrayList[i];
					array[i] = text2;
					if (FrameworkEventSource.IsInitialized)
					{
						FrameworkEventSource.Log.ResourceManagerAddingCultureFromConfigFile(this.BaseNameField, this.MainAssembly, text2);
					}
				}
				hashtable.Add(text, array);
			}
			return hashtable;
		}

		// Token: 0x0400117A RID: 4474
		protected string BaseNameField;

		// Token: 0x0400117B RID: 4475
		[Obsolete("call InternalGetResourceSet instead")]
		protected Hashtable ResourceSets;

		// Token: 0x0400117C RID: 4476
		[NonSerialized]
		private Dictionary<string, ResourceSet> _resourceSets;

		// Token: 0x0400117D RID: 4477
		private string moduleDir;

		// Token: 0x0400117E RID: 4478
		protected Assembly MainAssembly;

		// Token: 0x0400117F RID: 4479
		private Type _locationInfo;

		// Token: 0x04001180 RID: 4480
		private Type _userResourceSet;

		// Token: 0x04001181 RID: 4481
		private CultureInfo _neutralResourcesCulture;

		// Token: 0x04001182 RID: 4482
		[NonSerialized]
		private ResourceManager.CultureNameResourceSetPair _lastUsedResourceCache;

		// Token: 0x04001183 RID: 4483
		private bool _ignoreCase;

		// Token: 0x04001184 RID: 4484
		private bool UseManifest;

		// Token: 0x04001185 RID: 4485
		[OptionalField(VersionAdded = 1)]
		private bool UseSatelliteAssem;

		// Token: 0x04001186 RID: 4486
		private static volatile Hashtable _installedSatelliteInfo;

		// Token: 0x04001187 RID: 4487
		private static volatile bool _checkedConfigFile;

		// Token: 0x04001188 RID: 4488
		[OptionalField]
		private UltimateResourceFallbackLocation _fallbackLoc;

		// Token: 0x04001189 RID: 4489
		[OptionalField]
		private Version _satelliteContractVersion;

		// Token: 0x0400118A RID: 4490
		[OptionalField]
		private bool _lookedForSatelliteContractVersion;

		// Token: 0x0400118B RID: 4491
		[OptionalField(VersionAdded = 1)]
		private Assembly _callingAssembly;

		// Token: 0x0400118C RID: 4492
		[OptionalField(VersionAdded = 4)]
		private RuntimeAssembly m_callingAssembly;

		// Token: 0x0400118D RID: 4493
		[NonSerialized]
		private IResourceGroveler resourceGroveler;

		// Token: 0x0400118E RID: 4494
		public static readonly int MagicNumber = -1091581234;

		// Token: 0x0400118F RID: 4495
		public static readonly int HeaderVersionNumber = 1;

		// Token: 0x04001190 RID: 4496
		private static readonly Type _minResourceSet = typeof(ResourceSet);

		// Token: 0x04001191 RID: 4497
		internal static readonly string ResReaderTypeName = typeof(ResourceReader).FullName;

		// Token: 0x04001192 RID: 4498
		internal static readonly string ResSetTypeName = typeof(RuntimeResourceSet).FullName;

		// Token: 0x04001193 RID: 4499
		internal static readonly string MscorlibName = typeof(ResourceReader).Assembly.FullName;

		// Token: 0x04001194 RID: 4500
		internal const string ResFileExtension = ".resources";

		// Token: 0x04001195 RID: 4501
		internal const int ResFileExtensionLength = 10;

		// Token: 0x04001196 RID: 4502
		internal static readonly int DEBUG = 0;

		// Token: 0x04001197 RID: 4503
		private static volatile bool s_IsAppXModel;

		// Token: 0x04001198 RID: 4504
		[NonSerialized]
		private bool _bUsingModernResourceManagement;

		// Token: 0x04001199 RID: 4505
		[SecurityCritical]
		[NonSerialized]
		private WindowsRuntimeResourceManagerBase _WinRTResourceManager;

		// Token: 0x0400119A RID: 4506
		[NonSerialized]
		private bool _PRIonAppXInitialized;

		// Token: 0x0400119B RID: 4507
		[NonSerialized]
		private PRIExceptionInfo _PRIExceptionInfo;

		// Token: 0x02000B2D RID: 2861
		internal class CultureNameResourceSetPair
		{
			// Token: 0x0400335B RID: 13147
			public string lastCultureName;

			// Token: 0x0400335C RID: 13148
			public ResourceSet lastResourceSet;
		}

		// Token: 0x02000B2E RID: 2862
		internal class ResourceManagerMediator
		{
			// Token: 0x06006ADA RID: 27354 RVA: 0x00171257 File Offset: 0x0016F457
			internal ResourceManagerMediator(ResourceManager rm)
			{
				if (rm == null)
				{
					throw new ArgumentNullException("rm");
				}
				this._rm = rm;
			}

			// Token: 0x17001222 RID: 4642
			// (get) Token: 0x06006ADB RID: 27355 RVA: 0x00171274 File Offset: 0x0016F474
			internal string ModuleDir
			{
				get
				{
					return this._rm.moduleDir;
				}
			}

			// Token: 0x17001223 RID: 4643
			// (get) Token: 0x06006ADC RID: 27356 RVA: 0x00171281 File Offset: 0x0016F481
			internal Type LocationInfo
			{
				get
				{
					return this._rm._locationInfo;
				}
			}

			// Token: 0x17001224 RID: 4644
			// (get) Token: 0x06006ADD RID: 27357 RVA: 0x0017128E File Offset: 0x0016F48E
			internal Type UserResourceSet
			{
				get
				{
					return this._rm._userResourceSet;
				}
			}

			// Token: 0x17001225 RID: 4645
			// (get) Token: 0x06006ADE RID: 27358 RVA: 0x0017129B File Offset: 0x0016F49B
			internal string BaseNameField
			{
				get
				{
					return this._rm.BaseNameField;
				}
			}

			// Token: 0x17001226 RID: 4646
			// (get) Token: 0x06006ADF RID: 27359 RVA: 0x001712A8 File Offset: 0x0016F4A8
			// (set) Token: 0x06006AE0 RID: 27360 RVA: 0x001712B5 File Offset: 0x0016F4B5
			internal CultureInfo NeutralResourcesCulture
			{
				get
				{
					return this._rm._neutralResourcesCulture;
				}
				set
				{
					this._rm._neutralResourcesCulture = value;
				}
			}

			// Token: 0x06006AE1 RID: 27361 RVA: 0x001712C3 File Offset: 0x0016F4C3
			internal string GetResourceFileName(CultureInfo culture)
			{
				return this._rm.GetResourceFileName(culture);
			}

			// Token: 0x17001227 RID: 4647
			// (get) Token: 0x06006AE2 RID: 27362 RVA: 0x001712D1 File Offset: 0x0016F4D1
			// (set) Token: 0x06006AE3 RID: 27363 RVA: 0x001712DE File Offset: 0x0016F4DE
			internal bool LookedForSatelliteContractVersion
			{
				get
				{
					return this._rm._lookedForSatelliteContractVersion;
				}
				set
				{
					this._rm._lookedForSatelliteContractVersion = value;
				}
			}

			// Token: 0x17001228 RID: 4648
			// (get) Token: 0x06006AE4 RID: 27364 RVA: 0x001712EC File Offset: 0x0016F4EC
			// (set) Token: 0x06006AE5 RID: 27365 RVA: 0x001712F9 File Offset: 0x0016F4F9
			internal Version SatelliteContractVersion
			{
				get
				{
					return this._rm._satelliteContractVersion;
				}
				set
				{
					this._rm._satelliteContractVersion = value;
				}
			}

			// Token: 0x06006AE6 RID: 27366 RVA: 0x00171307 File Offset: 0x0016F507
			internal Version ObtainSatelliteContractVersion(Assembly a)
			{
				return ResourceManager.GetSatelliteContractVersion(a);
			}

			// Token: 0x17001229 RID: 4649
			// (get) Token: 0x06006AE7 RID: 27367 RVA: 0x0017130F File Offset: 0x0016F50F
			// (set) Token: 0x06006AE8 RID: 27368 RVA: 0x0017131C File Offset: 0x0016F51C
			internal UltimateResourceFallbackLocation FallbackLoc
			{
				get
				{
					return this._rm.FallbackLocation;
				}
				set
				{
					this._rm._fallbackLoc = value;
				}
			}

			// Token: 0x1700122A RID: 4650
			// (get) Token: 0x06006AE9 RID: 27369 RVA: 0x0017132A File Offset: 0x0016F52A
			internal RuntimeAssembly CallingAssembly
			{
				get
				{
					return this._rm.m_callingAssembly;
				}
			}

			// Token: 0x1700122B RID: 4651
			// (get) Token: 0x06006AEA RID: 27370 RVA: 0x00171337 File Offset: 0x0016F537
			internal RuntimeAssembly MainAssembly
			{
				get
				{
					return (RuntimeAssembly)this._rm.MainAssembly;
				}
			}

			// Token: 0x1700122C RID: 4652
			// (get) Token: 0x06006AEB RID: 27371 RVA: 0x00171349 File Offset: 0x0016F549
			internal string BaseName
			{
				get
				{
					return this._rm.BaseName;
				}
			}

			// Token: 0x06006AEC RID: 27372 RVA: 0x00171356 File Offset: 0x0016F556
			[SecurityCritical]
			internal bool TryLookingForSatellite(CultureInfo lookForCulture)
			{
				return this._rm.TryLookingForSatellite(lookForCulture);
			}

			// Token: 0x0400335D RID: 13149
			private ResourceManager _rm;
		}
	}
}
