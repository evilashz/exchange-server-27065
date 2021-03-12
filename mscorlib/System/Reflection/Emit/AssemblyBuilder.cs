using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Reflection.Emit
{
	// Token: 0x020005FD RID: 1533
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_AssemblyBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class AssemblyBuilder : Assembly, _AssemblyBuilder
	{
		// Token: 0x060047F2 RID: 18418
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeModule GetInMemoryAssemblyModule(RuntimeAssembly assembly);

		// Token: 0x060047F3 RID: 18419 RVA: 0x001034F2 File Offset: 0x001016F2
		[SecurityCritical]
		private Module nGetInMemoryAssemblyModule()
		{
			return AssemblyBuilder.GetInMemoryAssemblyModule(this.GetNativeHandle());
		}

		// Token: 0x060047F4 RID: 18420
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeModule GetOnDiskAssemblyModule(RuntimeAssembly assembly);

		// Token: 0x060047F5 RID: 18421 RVA: 0x00103500 File Offset: 0x00101700
		[SecurityCritical]
		private ModuleBuilder GetOnDiskAssemblyModuleBuilder()
		{
			if (this.m_onDiskAssemblyModuleBuilder == null)
			{
				Module onDiskAssemblyModule = AssemblyBuilder.GetOnDiskAssemblyModule(this.InternalAssembly.GetNativeHandle());
				ModuleBuilder moduleBuilder = new ModuleBuilder(this, (InternalModuleBuilder)onDiskAssemblyModule);
				moduleBuilder.Init("RefEmit_OnDiskManifestModule", null, 0);
				this.m_onDiskAssemblyModuleBuilder = moduleBuilder;
			}
			return this.m_onDiskAssemblyModuleBuilder;
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x00103554 File Offset: 0x00101754
		internal ModuleBuilder GetModuleBuilder(InternalModuleBuilder module)
		{
			object syncRoot = this.SyncRoot;
			ModuleBuilder result;
			lock (syncRoot)
			{
				foreach (ModuleBuilder moduleBuilder in this.m_assemblyData.m_moduleBuilderList)
				{
					if (moduleBuilder.InternalModule == module)
					{
						return moduleBuilder;
					}
				}
				if (this.m_onDiskAssemblyModuleBuilder != null && this.m_onDiskAssemblyModuleBuilder.InternalModule == module)
				{
					result = this.m_onDiskAssemblyModuleBuilder;
				}
				else
				{
					if (!(this.m_manifestModuleBuilder.InternalModule == module))
					{
						throw new ArgumentException("module");
					}
					result = this.m_manifestModuleBuilder;
				}
			}
			return result;
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060047F7 RID: 18423 RVA: 0x00103634 File Offset: 0x00101834
		internal object SyncRoot
		{
			get
			{
				return this.InternalAssembly.SyncRoot;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060047F8 RID: 18424 RVA: 0x00103641 File Offset: 0x00101841
		internal InternalAssemblyBuilder InternalAssembly
		{
			get
			{
				return this.m_internalAssemblyBuilder;
			}
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x00103649 File Offset: 0x00101849
		internal RuntimeAssembly GetNativeHandle()
		{
			return this.InternalAssembly.GetNativeHandle();
		}

		// Token: 0x060047FA RID: 18426 RVA: 0x00103656 File Offset: 0x00101856
		[SecurityCritical]
		internal Version GetVersion()
		{
			return this.InternalAssembly.GetVersion();
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060047FB RID: 18427 RVA: 0x00103663 File Offset: 0x00101863
		internal bool ProfileAPICheck
		{
			get
			{
				return this.m_profileAPICheck;
			}
		}

		// Token: 0x060047FC RID: 18428 RVA: 0x0010366C File Offset: 0x0010186C
		[SecurityCritical]
		internal AssemblyBuilder(AppDomain domain, AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, ref StackCrawlMark stackMark, IEnumerable<CustomAttributeBuilder> unsafeAssemblyAttributes, SecurityContextSource securityContextSource)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (access != AssemblyBuilderAccess.Run && access != AssemblyBuilderAccess.Save && access != AssemblyBuilderAccess.RunAndSave && access != AssemblyBuilderAccess.ReflectionOnly && access != AssemblyBuilderAccess.RunAndCollect)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)access
				}), "access");
			}
			if (securityContextSource < SecurityContextSource.CurrentAppDomain || securityContextSource > SecurityContextSource.CurrentAssembly)
			{
				throw new ArgumentOutOfRangeException("securityContextSource");
			}
			name = (AssemblyName)name.Clone();
			if (name.KeyPair != null)
			{
				name.SetPublicKey(name.KeyPair.PublicKey);
			}
			if (evidence != null)
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			if (access == AssemblyBuilderAccess.RunAndCollect)
			{
				new PermissionSet(PermissionState.Unrestricted).Demand();
			}
			List<CustomAttributeBuilder> list = null;
			DynamicAssemblyFlags dynamicAssemblyFlags = DynamicAssemblyFlags.None;
			byte[] array = null;
			byte[] array2 = null;
			if (unsafeAssemblyAttributes != null)
			{
				list = new List<CustomAttributeBuilder>(unsafeAssemblyAttributes);
				foreach (CustomAttributeBuilder customAttributeBuilder in list)
				{
					if (customAttributeBuilder.m_con.DeclaringType == typeof(SecurityTransparentAttribute))
					{
						dynamicAssemblyFlags |= DynamicAssemblyFlags.Transparent;
					}
					else if (customAttributeBuilder.m_con.DeclaringType == typeof(SecurityCriticalAttribute))
					{
						SecurityCriticalScope securityCriticalScope = SecurityCriticalScope.Everything;
						if (customAttributeBuilder.m_constructorArgs != null && customAttributeBuilder.m_constructorArgs.Length == 1 && customAttributeBuilder.m_constructorArgs[0] is SecurityCriticalScope)
						{
							securityCriticalScope = (SecurityCriticalScope)customAttributeBuilder.m_constructorArgs[0];
						}
						dynamicAssemblyFlags |= DynamicAssemblyFlags.Critical;
						if (securityCriticalScope == SecurityCriticalScope.Everything)
						{
							dynamicAssemblyFlags |= DynamicAssemblyFlags.AllCritical;
						}
					}
					else if (customAttributeBuilder.m_con.DeclaringType == typeof(SecurityRulesAttribute))
					{
						array = new byte[customAttributeBuilder.m_blob.Length];
						Array.Copy(customAttributeBuilder.m_blob, array, array.Length);
					}
					else if (customAttributeBuilder.m_con.DeclaringType == typeof(SecurityTreatAsSafeAttribute))
					{
						dynamicAssemblyFlags |= DynamicAssemblyFlags.TreatAsSafe;
					}
					else if (customAttributeBuilder.m_con.DeclaringType == typeof(AllowPartiallyTrustedCallersAttribute))
					{
						dynamicAssemblyFlags |= DynamicAssemblyFlags.Aptca;
						array2 = new byte[customAttributeBuilder.m_blob.Length];
						Array.Copy(customAttributeBuilder.m_blob, array2, array2.Length);
					}
				}
			}
			this.m_internalAssemblyBuilder = (InternalAssemblyBuilder)AssemblyBuilder.nCreateDynamicAssembly(domain, name, evidence, ref stackMark, requiredPermissions, optionalPermissions, refusedPermissions, array, array2, access, dynamicAssemblyFlags, securityContextSource);
			this.m_assemblyData = new AssemblyBuilderData(this.m_internalAssemblyBuilder, name.Name, access, dir);
			this.m_assemblyData.AddPermissionRequests(requiredPermissions, optionalPermissions, refusedPermissions);
			if (AppDomain.ProfileAPICheck)
			{
				RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
				if (executingAssembly != null && !executingAssembly.IsFrameworkAssembly())
				{
					this.m_profileAPICheck = true;
				}
			}
			this.InitManifestModule();
			if (list != null)
			{
				foreach (CustomAttributeBuilder customAttribute in list)
				{
					this.SetCustomAttribute(customAttribute);
				}
			}
		}

		// Token: 0x060047FD RID: 18429 RVA: 0x00103984 File Offset: 0x00101B84
		[SecurityCritical]
		private void InitManifestModule()
		{
			InternalModuleBuilder internalModuleBuilder = (InternalModuleBuilder)this.nGetInMemoryAssemblyModule();
			this.m_manifestModuleBuilder = new ModuleBuilder(this, internalModuleBuilder);
			this.m_manifestModuleBuilder.Init("RefEmit_InMemoryManifestModule", null, 0);
			this.m_fManifestModuleUsedAsDefinedModule = false;
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x001039C4 File Offset: 0x00101BC4
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return AssemblyBuilder.InternalDefineDynamicAssembly(name, access, null, null, null, null, null, ref stackCrawlMark, null, SecurityContextSource.CurrentAssembly);
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x001039E4 File Offset: 0x00101BE4
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return AssemblyBuilder.InternalDefineDynamicAssembly(name, access, null, null, null, null, null, ref stackCrawlMark, assemblyAttributes, SecurityContextSource.CurrentAssembly);
		}

		// Token: 0x06004800 RID: 18432
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Assembly nCreateDynamicAssembly(AppDomain domain, AssemblyName name, Evidence identity, ref StackCrawlMark stackMark, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, byte[] securityRulesBlob, byte[] aptcaBlob, AssemblyBuilderAccess access, DynamicAssemblyFlags flags, SecurityContextSource securityContextSource);

		// Token: 0x06004801 RID: 18433 RVA: 0x00103A04 File Offset: 0x00101C04
		[SecurityCritical]
		internal static AssemblyBuilder InternalDefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, ref StackCrawlMark stackMark, IEnumerable<CustomAttributeBuilder> unsafeAssemblyAttributes, SecurityContextSource securityContextSource)
		{
			if (evidence != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
			}
			Type typeFromHandle = typeof(AssemblyBuilder.AssemblyBuilderLock);
			AssemblyBuilder result;
			lock (typeFromHandle)
			{
				result = new AssemblyBuilder(AppDomain.CurrentDomain, name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, ref stackMark, unsafeAssemblyAttributes, securityContextSource);
			}
			return result;
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x00103A80 File Offset: 0x00101C80
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ModuleBuilder DefineDynamicModule(string name)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.DefineDynamicModuleInternal(name, false, ref stackCrawlMark);
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x00103A9C File Offset: 0x00101C9C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ModuleBuilder DefineDynamicModule(string name, bool emitSymbolInfo)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.DefineDynamicModuleInternal(name, emitSymbolInfo, ref stackCrawlMark);
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x00103AB8 File Offset: 0x00101CB8
		[SecurityCritical]
		private ModuleBuilder DefineDynamicModuleInternal(string name, bool emitSymbolInfo, ref StackCrawlMark stackMark)
		{
			object syncRoot = this.SyncRoot;
			ModuleBuilder result;
			lock (syncRoot)
			{
				result = this.DefineDynamicModuleInternalNoLock(name, emitSymbolInfo, ref stackMark);
			}
			return result;
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x00103B00 File Offset: 0x00101D00
		[SecurityCritical]
		private ModuleBuilder DefineDynamicModuleInternalNoLock(string name, bool emitSymbolInfo, ref StackCrawlMark stackMark)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), "name");
			}
			ISymbolWriter symbolWriter = null;
			IntPtr underlyingWriter = 0;
			this.m_assemblyData.CheckNameConflict(name);
			ModuleBuilder moduleBuilder;
			if (this.m_fManifestModuleUsedAsDefinedModule)
			{
				int tkFile;
				InternalModuleBuilder internalModuleBuilder = (InternalModuleBuilder)AssemblyBuilder.DefineDynamicModule(this.InternalAssembly, emitSymbolInfo, name, name, ref stackMark, ref underlyingWriter, true, out tkFile);
				moduleBuilder = new ModuleBuilder(this, internalModuleBuilder);
				moduleBuilder.Init(name, null, tkFile);
			}
			else
			{
				this.m_manifestModuleBuilder.ModifyModuleName(name);
				moduleBuilder = this.m_manifestModuleBuilder;
				if (emitSymbolInfo)
				{
					underlyingWriter = ModuleBuilder.nCreateISymWriterForDynamicModule(moduleBuilder.InternalModule, name);
				}
			}
			if (emitSymbolInfo)
			{
				Assembly assembly = this.LoadISymWrapper();
				Type type = assembly.GetType("System.Diagnostics.SymbolStore.SymWriter", true, false);
				if (type != null && !type.IsVisible)
				{
					type = null;
				}
				if (type == null)
				{
					throw new TypeLoadException(Environment.GetResourceString("MissingType", new object[]
					{
						"SymWriter"
					}));
				}
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
				try
				{
					new PermissionSet(PermissionState.Unrestricted).Assert();
					symbolWriter = (ISymbolWriter)Activator.CreateInstance(type);
					symbolWriter.SetUnderlyingWriter(underlyingWriter);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			moduleBuilder.SetSymWriter(symbolWriter);
			this.m_assemblyData.AddModule(moduleBuilder);
			if (moduleBuilder == this.m_manifestModuleBuilder)
			{
				this.m_fManifestModuleUsedAsDefinedModule = true;
			}
			return moduleBuilder;
		}

		// Token: 0x06004806 RID: 18438 RVA: 0x00103C90 File Offset: 0x00101E90
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ModuleBuilder DefineDynamicModule(string name, string fileName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.DefineDynamicModuleInternal(name, fileName, false, ref stackCrawlMark);
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x00103CAC File Offset: 0x00101EAC
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.DefineDynamicModuleInternal(name, fileName, emitSymbolInfo, ref stackCrawlMark);
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x00103CC8 File Offset: 0x00101EC8
		[SecurityCritical]
		private ModuleBuilder DefineDynamicModuleInternal(string name, string fileName, bool emitSymbolInfo, ref StackCrawlMark stackMark)
		{
			object syncRoot = this.SyncRoot;
			ModuleBuilder result;
			lock (syncRoot)
			{
				result = this.DefineDynamicModuleInternalNoLock(name, fileName, emitSymbolInfo, ref stackMark);
			}
			return result;
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x00103D10 File Offset: 0x00101F10
		[SecurityCritical]
		private ModuleBuilder DefineDynamicModuleInternalNoLock(string name, string fileName, bool emitSymbolInfo, ref StackCrawlMark stackMark)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), "name");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "fileName");
			}
			if (!string.Equals(fileName, Path.GetFileName(fileName)))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotSimpleFileName"), "fileName");
			}
			if (this.m_assemblyData.m_access == AssemblyBuilderAccess.Run)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_BadPersistableModuleInTransientAssembly"));
			}
			if (this.m_assemblyData.m_isSaved)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotAlterAssembly"));
			}
			ISymbolWriter symbolWriter = null;
			IntPtr underlyingWriter = 0;
			this.m_assemblyData.CheckNameConflict(name);
			this.m_assemblyData.CheckFileNameConflict(fileName);
			int tkFile;
			InternalModuleBuilder internalModuleBuilder = (InternalModuleBuilder)AssemblyBuilder.DefineDynamicModule(this.InternalAssembly, emitSymbolInfo, name, fileName, ref stackMark, ref underlyingWriter, false, out tkFile);
			ModuleBuilder moduleBuilder = new ModuleBuilder(this, internalModuleBuilder);
			moduleBuilder.Init(name, fileName, tkFile);
			if (emitSymbolInfo)
			{
				Assembly assembly = this.LoadISymWrapper();
				Type type = assembly.GetType("System.Diagnostics.SymbolStore.SymWriter", true, false);
				if (type != null && !type.IsVisible)
				{
					type = null;
				}
				if (type == null)
				{
					throw new TypeLoadException(Environment.GetResourceString("MissingType", new object[]
					{
						"SymWriter"
					}));
				}
				try
				{
					new PermissionSet(PermissionState.Unrestricted).Assert();
					symbolWriter = (ISymbolWriter)Activator.CreateInstance(type);
					symbolWriter.SetUnderlyingWriter(underlyingWriter);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			moduleBuilder.SetSymWriter(symbolWriter);
			this.m_assemblyData.AddModule(moduleBuilder);
			return moduleBuilder;
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x00103EE4 File Offset: 0x001020E4
		private Assembly LoadISymWrapper()
		{
			if (this.m_assemblyData.m_ISymWrapperAssembly != null)
			{
				return this.m_assemblyData.m_ISymWrapperAssembly;
			}
			Assembly assembly = Assembly.Load("ISymWrapper, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			this.m_assemblyData.m_ISymWrapperAssembly = assembly;
			return assembly;
		}

		// Token: 0x0600480B RID: 18443 RVA: 0x00103F28 File Offset: 0x00102128
		internal void CheckContext(params Type[][] typess)
		{
			if (typess == null)
			{
				return;
			}
			foreach (Type[] array in typess)
			{
				if (array != null)
				{
					this.CheckContext(array);
				}
			}
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x00103F58 File Offset: 0x00102158
		internal void CheckContext(params Type[] types)
		{
			if (types == null)
			{
				return;
			}
			foreach (Type type in types)
			{
				if (!(type == null))
				{
					if (type.Module == null || type.Module.Assembly == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_TypeNotValid"));
					}
					if (!(type.Module.Assembly == typeof(object).Module.Assembly))
					{
						if (type.Module.Assembly.ReflectionOnly && !this.ReflectionOnly)
						{
							throw new InvalidOperationException(Environment.GetResourceString("Arugment_EmitMixedContext1", new object[]
							{
								type.AssemblyQualifiedName
							}));
						}
						if (!type.Module.Assembly.ReflectionOnly && this.ReflectionOnly)
						{
							throw new InvalidOperationException(Environment.GetResourceString("Arugment_EmitMixedContext2", new object[]
							{
								type.AssemblyQualifiedName
							}));
						}
					}
				}
			}
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x00104058 File Offset: 0x00102258
		public IResourceWriter DefineResource(string name, string description, string fileName)
		{
			return this.DefineResource(name, description, fileName, ResourceAttributes.Public);
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x00104064 File Offset: 0x00102264
		public IResourceWriter DefineResource(string name, string description, string fileName, ResourceAttributes attribute)
		{
			object syncRoot = this.SyncRoot;
			IResourceWriter result;
			lock (syncRoot)
			{
				result = this.DefineResourceNoLock(name, description, fileName, attribute);
			}
			return result;
		}

		// Token: 0x0600480F RID: 18447 RVA: 0x001040AC File Offset: 0x001022AC
		private IResourceWriter DefineResourceNoLock(string name, string description, string fileName, ResourceAttributes attribute)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), name);
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "fileName");
			}
			if (!string.Equals(fileName, Path.GetFileName(fileName)))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotSimpleFileName"), "fileName");
			}
			this.m_assemblyData.CheckResNameConflict(name);
			this.m_assemblyData.CheckFileNameConflict(fileName);
			string text;
			ResourceWriter resourceWriter;
			if (this.m_assemblyData.m_strDir == null)
			{
				text = Path.Combine(Environment.CurrentDirectory, fileName);
				resourceWriter = new ResourceWriter(text);
			}
			else
			{
				text = Path.Combine(this.m_assemblyData.m_strDir, fileName);
				resourceWriter = new ResourceWriter(text);
			}
			text = Path.GetFullPath(text);
			fileName = Path.GetFileName(text);
			this.m_assemblyData.AddResWriter(new ResWriterData(resourceWriter, null, name, fileName, text, attribute));
			return resourceWriter;
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x001041A8 File Offset: 0x001023A8
		public void AddResourceFile(string name, string fileName)
		{
			this.AddResourceFile(name, fileName, ResourceAttributes.Public);
		}

		// Token: 0x06004811 RID: 18449 RVA: 0x001041B4 File Offset: 0x001023B4
		public void AddResourceFile(string name, string fileName, ResourceAttributes attribute)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.AddResourceFileNoLock(name, fileName, attribute);
			}
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x001041F8 File Offset: 0x001023F8
		[SecuritySafeCritical]
		private void AddResourceFileNoLock(string name, string fileName, ResourceAttributes attribute)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), name);
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), fileName);
			}
			if (!string.Equals(fileName, Path.GetFileName(fileName)))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotSimpleFileName"), "fileName");
			}
			this.m_assemblyData.CheckResNameConflict(name);
			this.m_assemblyData.CheckFileNameConflict(fileName);
			string text;
			if (this.m_assemblyData.m_strDir == null)
			{
				text = Path.Combine(Environment.CurrentDirectory, fileName);
			}
			else
			{
				text = Path.Combine(this.m_assemblyData.m_strDir, fileName);
			}
			text = Path.UnsafeGetFullPath(text);
			fileName = Path.GetFileName(text);
			if (!File.UnsafeExists(text))
			{
				throw new FileNotFoundException(Environment.GetResourceString("IO.FileNotFound_FileName", new object[]
				{
					fileName
				}), fileName);
			}
			this.m_assemblyData.AddResWriter(new ResWriterData(null, null, name, fileName, text, attribute));
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x00104303 File Offset: 0x00102503
		public override bool Equals(object obj)
		{
			return this.InternalAssembly.Equals(obj);
		}

		// Token: 0x06004814 RID: 18452 RVA: 0x00104311 File Offset: 0x00102511
		public override int GetHashCode()
		{
			return this.InternalAssembly.GetHashCode();
		}

		// Token: 0x06004815 RID: 18453 RVA: 0x0010431E File Offset: 0x0010251E
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.InternalAssembly.GetCustomAttributes(inherit);
		}

		// Token: 0x06004816 RID: 18454 RVA: 0x0010432C File Offset: 0x0010252C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.InternalAssembly.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x0010433B File Offset: 0x0010253B
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.InternalAssembly.IsDefined(attributeType, inherit);
		}

		// Token: 0x06004818 RID: 18456 RVA: 0x0010434A File Offset: 0x0010254A
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return this.InternalAssembly.GetCustomAttributesData();
		}

		// Token: 0x06004819 RID: 18457 RVA: 0x00104357 File Offset: 0x00102557
		public override string[] GetManifestResourceNames()
		{
			return this.InternalAssembly.GetManifestResourceNames();
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x00104364 File Offset: 0x00102564
		public override FileStream GetFile(string name)
		{
			return this.InternalAssembly.GetFile(name);
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x00104372 File Offset: 0x00102572
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			return this.InternalAssembly.GetFiles(getResourceModules);
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x00104380 File Offset: 0x00102580
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			return this.InternalAssembly.GetManifestResourceStream(type, name);
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x0010438F File Offset: 0x0010258F
		public override Stream GetManifestResourceStream(string name)
		{
			return this.InternalAssembly.GetManifestResourceStream(name);
		}

		// Token: 0x0600481E RID: 18462 RVA: 0x0010439D File Offset: 0x0010259D
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			return this.InternalAssembly.GetManifestResourceInfo(resourceName);
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x0600481F RID: 18463 RVA: 0x001043AB File Offset: 0x001025AB
		public override string Location
		{
			get
			{
				return this.InternalAssembly.Location;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06004820 RID: 18464 RVA: 0x001043B8 File Offset: 0x001025B8
		public override string ImageRuntimeVersion
		{
			get
			{
				return this.InternalAssembly.ImageRuntimeVersion;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06004821 RID: 18465 RVA: 0x001043C5 File Offset: 0x001025C5
		public override string CodeBase
		{
			get
			{
				return this.InternalAssembly.CodeBase;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06004822 RID: 18466 RVA: 0x001043D2 File Offset: 0x001025D2
		public override MethodInfo EntryPoint
		{
			get
			{
				return this.m_assemblyData.m_entryPointMethod;
			}
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x001043DF File Offset: 0x001025DF
		public override Type[] GetExportedTypes()
		{
			return this.InternalAssembly.GetExportedTypes();
		}

		// Token: 0x06004824 RID: 18468 RVA: 0x001043EC File Offset: 0x001025EC
		public override AssemblyName GetName(bool copiedName)
		{
			return this.InternalAssembly.GetName(copiedName);
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06004825 RID: 18469 RVA: 0x001043FA File Offset: 0x001025FA
		public override string FullName
		{
			get
			{
				return this.InternalAssembly.FullName;
			}
		}

		// Token: 0x06004826 RID: 18470 RVA: 0x00104407 File Offset: 0x00102607
		public override Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			return this.InternalAssembly.GetType(name, throwOnError, ignoreCase);
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06004827 RID: 18471 RVA: 0x00104417 File Offset: 0x00102617
		public override Evidence Evidence
		{
			get
			{
				return this.InternalAssembly.Evidence;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06004828 RID: 18472 RVA: 0x00104424 File Offset: 0x00102624
		public override PermissionSet PermissionSet
		{
			[SecurityCritical]
			get
			{
				return this.InternalAssembly.PermissionSet;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06004829 RID: 18473 RVA: 0x00104431 File Offset: 0x00102631
		public override SecurityRuleSet SecurityRuleSet
		{
			get
			{
				return this.InternalAssembly.SecurityRuleSet;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600482A RID: 18474 RVA: 0x0010443E File Offset: 0x0010263E
		public override Module ManifestModule
		{
			get
			{
				return this.m_manifestModuleBuilder.InternalModule;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x0600482B RID: 18475 RVA: 0x0010444B File Offset: 0x0010264B
		public override bool ReflectionOnly
		{
			get
			{
				return this.InternalAssembly.ReflectionOnly;
			}
		}

		// Token: 0x0600482C RID: 18476 RVA: 0x00104458 File Offset: 0x00102658
		public override Module GetModule(string name)
		{
			return this.InternalAssembly.GetModule(name);
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x00104466 File Offset: 0x00102666
		public override AssemblyName[] GetReferencedAssemblies()
		{
			return this.InternalAssembly.GetReferencedAssemblies();
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x0600482E RID: 18478 RVA: 0x00104473 File Offset: 0x00102673
		public override bool GlobalAssemblyCache
		{
			get
			{
				return this.InternalAssembly.GlobalAssemblyCache;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x0600482F RID: 18479 RVA: 0x00104480 File Offset: 0x00102680
		public override long HostContext
		{
			get
			{
				return this.InternalAssembly.HostContext;
			}
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x0010448D File Offset: 0x0010268D
		public override Module[] GetModules(bool getResourceModules)
		{
			return this.InternalAssembly.GetModules(getResourceModules);
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x0010449B File Offset: 0x0010269B
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.InternalAssembly.GetLoadedModules(getResourceModules);
		}

		// Token: 0x06004832 RID: 18482 RVA: 0x001044AC File Offset: 0x001026AC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.InternalAssembly.InternalGetSatelliteAssembly(culture, null, ref stackCrawlMark);
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x001044CC File Offset: 0x001026CC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.InternalAssembly.InternalGetSatelliteAssembly(culture, version, ref stackCrawlMark);
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06004834 RID: 18484 RVA: 0x001044EA File Offset: 0x001026EA
		public override bool IsDynamic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x001044F0 File Offset: 0x001026F0
		public void DefineVersionInfoResource(string product, string productVersion, string company, string copyright, string trademark)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.DefineVersionInfoResourceNoLock(product, productVersion, company, copyright, trademark);
			}
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x00104538 File Offset: 0x00102738
		private void DefineVersionInfoResourceNoLock(string product, string productVersion, string company, string copyright, string trademark)
		{
			if (this.m_assemblyData.m_strResourceFileName != null || this.m_assemblyData.m_resourceBytes != null || this.m_assemblyData.m_nativeVersion != null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
			}
			this.m_assemblyData.m_nativeVersion = new NativeVersionInfo();
			this.m_assemblyData.m_nativeVersion.m_strCopyright = copyright;
			this.m_assemblyData.m_nativeVersion.m_strTrademark = trademark;
			this.m_assemblyData.m_nativeVersion.m_strCompany = company;
			this.m_assemblyData.m_nativeVersion.m_strProduct = product;
			this.m_assemblyData.m_nativeVersion.m_strProductVersion = productVersion;
			this.m_assemblyData.m_hasUnmanagedVersionInfo = true;
			this.m_assemblyData.m_OverrideUnmanagedVersionInfo = true;
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x001045FC File Offset: 0x001027FC
		public void DefineVersionInfoResource()
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.DefineVersionInfoResourceNoLock();
			}
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x0010463C File Offset: 0x0010283C
		private void DefineVersionInfoResourceNoLock()
		{
			if (this.m_assemblyData.m_strResourceFileName != null || this.m_assemblyData.m_resourceBytes != null || this.m_assemblyData.m_nativeVersion != null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
			}
			this.m_assemblyData.m_hasUnmanagedVersionInfo = true;
			this.m_assemblyData.m_nativeVersion = new NativeVersionInfo();
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x0010469C File Offset: 0x0010289C
		public void DefineUnmanagedResource(byte[] resource)
		{
			if (resource == null)
			{
				throw new ArgumentNullException("resource");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.DefineUnmanagedResourceNoLock(resource);
			}
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x001046EC File Offset: 0x001028EC
		private void DefineUnmanagedResourceNoLock(byte[] resource)
		{
			if (this.m_assemblyData.m_strResourceFileName != null || this.m_assemblyData.m_resourceBytes != null || this.m_assemblyData.m_nativeVersion != null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
			}
			this.m_assemblyData.m_resourceBytes = new byte[resource.Length];
			Array.Copy(resource, this.m_assemblyData.m_resourceBytes, resource.Length);
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x00104758 File Offset: 0x00102958
		[SecuritySafeCritical]
		public void DefineUnmanagedResource(string resourceFileName)
		{
			if (resourceFileName == null)
			{
				throw new ArgumentNullException("resourceFileName");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.DefineUnmanagedResourceNoLock(resourceFileName);
			}
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x001047A8 File Offset: 0x001029A8
		[SecurityCritical]
		private void DefineUnmanagedResourceNoLock(string resourceFileName)
		{
			if (this.m_assemblyData.m_strResourceFileName != null || this.m_assemblyData.m_resourceBytes != null || this.m_assemblyData.m_nativeVersion != null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
			}
			string text;
			if (this.m_assemblyData.m_strDir == null)
			{
				text = Path.Combine(Environment.CurrentDirectory, resourceFileName);
			}
			else
			{
				text = Path.Combine(this.m_assemblyData.m_strDir, resourceFileName);
			}
			text = Path.GetFullPath(resourceFileName);
			new FileIOPermission(FileIOPermissionAccess.Read, text).Demand();
			if (!File.Exists(text))
			{
				throw new FileNotFoundException(Environment.GetResourceString("IO.FileNotFound_FileName", new object[]
				{
					resourceFileName
				}), resourceFileName);
			}
			this.m_assemblyData.m_strResourceFileName = text;
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x0010485C File Offset: 0x00102A5C
		public ModuleBuilder GetDynamicModule(string name)
		{
			object syncRoot = this.SyncRoot;
			ModuleBuilder dynamicModuleNoLock;
			lock (syncRoot)
			{
				dynamicModuleNoLock = this.GetDynamicModuleNoLock(name);
			}
			return dynamicModuleNoLock;
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x001048A0 File Offset: 0x00102AA0
		private ModuleBuilder GetDynamicModuleNoLock(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			int count = this.m_assemblyData.m_moduleBuilderList.Count;
			for (int i = 0; i < count; i++)
			{
				ModuleBuilder moduleBuilder = this.m_assemblyData.m_moduleBuilderList[i];
				if (moduleBuilder.m_moduleData.m_strModuleName.Equals(name))
				{
					return moduleBuilder;
				}
			}
			return null;
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x0010491D File Offset: 0x00102B1D
		public void SetEntryPoint(MethodInfo entryMethod)
		{
			this.SetEntryPoint(entryMethod, PEFileKinds.ConsoleApplication);
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x00104928 File Offset: 0x00102B28
		public void SetEntryPoint(MethodInfo entryMethod, PEFileKinds fileKind)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.SetEntryPointNoLock(entryMethod, fileKind);
			}
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x0010496C File Offset: 0x00102B6C
		private void SetEntryPointNoLock(MethodInfo entryMethod, PEFileKinds fileKind)
		{
			if (entryMethod == null)
			{
				throw new ArgumentNullException("entryMethod");
			}
			Module module = entryMethod.Module;
			if (module == null || !this.InternalAssembly.Equals(module.Assembly))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EntryMethodNotDefinedInAssembly"));
			}
			this.m_assemblyData.m_entryPointMethod = entryMethod;
			this.m_assemblyData.m_peFileKind = fileKind;
			ModuleBuilder moduleBuilder = module as ModuleBuilder;
			if (moduleBuilder != null)
			{
				this.m_assemblyData.m_entryPointModule = moduleBuilder;
			}
			else
			{
				this.m_assemblyData.m_entryPointModule = this.GetModuleBuilder((InternalModuleBuilder)module);
			}
			MethodToken methodToken = this.m_assemblyData.m_entryPointModule.GetMethodToken(entryMethod);
			this.m_assemblyData.m_entryPointModule.SetEntryPoint(methodToken);
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x00104A30 File Offset: 0x00102C30
		[SecuritySafeCritical]
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.SetCustomAttributeNoLock(con, binaryAttribute);
			}
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x00104A94 File Offset: 0x00102C94
		[SecurityCritical]
		private void SetCustomAttributeNoLock(ConstructorInfo con, byte[] binaryAttribute)
		{
			TypeBuilder.DefineCustomAttribute(this.m_manifestModuleBuilder, 536870913, this.m_manifestModuleBuilder.GetConstructorToken(con).Token, binaryAttribute, false, typeof(DebuggableAttribute) == con.DeclaringType);
			if (this.m_assemblyData.m_access != AssemblyBuilderAccess.Run)
			{
				this.m_assemblyData.AddCustomAttribute(con, binaryAttribute);
			}
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x00104AF8 File Offset: 0x00102CF8
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.SetCustomAttributeNoLock(customBuilder);
			}
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x00104B48 File Offset: 0x00102D48
		[SecurityCritical]
		private void SetCustomAttributeNoLock(CustomAttributeBuilder customBuilder)
		{
			customBuilder.CreateCustomAttribute(this.m_manifestModuleBuilder, 536870913);
			if (this.m_assemblyData.m_access != AssemblyBuilderAccess.Run)
			{
				this.m_assemblyData.AddCustomAttribute(customBuilder);
			}
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x00104B75 File Offset: 0x00102D75
		public void Save(string assemblyFileName)
		{
			this.Save(assemblyFileName, PortableExecutableKinds.ILOnly, ImageFileMachine.I386);
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x00104B84 File Offset: 0x00102D84
		[SecuritySafeCritical]
		public void Save(string assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.SaveNoLock(assemblyFileName, portableExecutableKind, imageFileMachine);
			}
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x00104BC8 File Offset: 0x00102DC8
		[SecurityCritical]
		private void SaveNoLock(string assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			if (assemblyFileName == null)
			{
				throw new ArgumentNullException("assemblyFileName");
			}
			if (assemblyFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "assemblyFileName");
			}
			if (!string.Equals(assemblyFileName, Path.GetFileName(assemblyFileName)))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotSimpleFileName"), "assemblyFileName");
			}
			int[] array = null;
			int[] array2 = null;
			string text = null;
			try
			{
				if (this.m_assemblyData.m_iCABuilder != 0)
				{
					array = new int[this.m_assemblyData.m_iCABuilder];
				}
				if (this.m_assemblyData.m_iCAs != 0)
				{
					array2 = new int[this.m_assemblyData.m_iCAs];
				}
				if (this.m_assemblyData.m_isSaved)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AssemblyHasBeenSaved", new object[]
					{
						this.InternalAssembly.GetSimpleName()
					}));
				}
				if ((this.m_assemblyData.m_access & AssemblyBuilderAccess.Save) != AssemblyBuilderAccess.Save)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CantSaveTransientAssembly"));
				}
				ModuleBuilder moduleBuilder = this.m_assemblyData.FindModuleWithFileName(assemblyFileName);
				if (moduleBuilder != null)
				{
					this.m_onDiskAssemblyModuleBuilder = moduleBuilder;
					moduleBuilder.m_moduleData.FileToken = 0;
				}
				else
				{
					this.m_assemblyData.CheckFileNameConflict(assemblyFileName);
				}
				if (this.m_assemblyData.m_strDir == null)
				{
					this.m_assemblyData.m_strDir = Environment.CurrentDirectory;
				}
				else if (!Directory.Exists(this.m_assemblyData.m_strDir))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDirectory", new object[]
					{
						this.m_assemblyData.m_strDir
					}));
				}
				assemblyFileName = Path.Combine(this.m_assemblyData.m_strDir, assemblyFileName);
				assemblyFileName = Path.GetFullPath(assemblyFileName);
				new FileIOPermission(FileIOPermissionAccess.Write | FileIOPermissionAccess.Append, assemblyFileName).Demand();
				if (moduleBuilder != null)
				{
					for (int i = 0; i < this.m_assemblyData.m_iCABuilder; i++)
					{
						array[i] = this.m_assemblyData.m_CABuilders[i].PrepareCreateCustomAttributeToDisk(moduleBuilder);
					}
					for (int i = 0; i < this.m_assemblyData.m_iCAs; i++)
					{
						array2[i] = moduleBuilder.InternalGetConstructorToken(this.m_assemblyData.m_CACons[i], true).Token;
					}
					moduleBuilder.PreSave(assemblyFileName, portableExecutableKind, imageFileMachine);
				}
				RuntimeModule assemblyModule = (moduleBuilder != null) ? moduleBuilder.ModuleHandle.GetRuntimeModule() : null;
				AssemblyBuilder.PrepareForSavingManifestToDisk(this.GetNativeHandle(), assemblyModule);
				ModuleBuilder onDiskAssemblyModuleBuilder = this.GetOnDiskAssemblyModuleBuilder();
				if (this.m_assemblyData.m_strResourceFileName != null)
				{
					onDiskAssemblyModuleBuilder.DefineUnmanagedResourceFileInternalNoLock(this.m_assemblyData.m_strResourceFileName);
				}
				else if (this.m_assemblyData.m_resourceBytes != null)
				{
					onDiskAssemblyModuleBuilder.DefineUnmanagedResourceInternalNoLock(this.m_assemblyData.m_resourceBytes);
				}
				else if (this.m_assemblyData.m_hasUnmanagedVersionInfo)
				{
					this.m_assemblyData.FillUnmanagedVersionInfo();
					string text2 = this.m_assemblyData.m_nativeVersion.m_strFileVersion;
					if (text2 == null)
					{
						text2 = this.GetVersion().ToString();
					}
					AssemblyBuilder.CreateVersionInfoResource(assemblyFileName, this.m_assemblyData.m_nativeVersion.m_strTitle, null, this.m_assemblyData.m_nativeVersion.m_strDescription, this.m_assemblyData.m_nativeVersion.m_strCopyright, this.m_assemblyData.m_nativeVersion.m_strTrademark, this.m_assemblyData.m_nativeVersion.m_strCompany, this.m_assemblyData.m_nativeVersion.m_strProduct, this.m_assemblyData.m_nativeVersion.m_strProductVersion, text2, this.m_assemblyData.m_nativeVersion.m_lcid, this.m_assemblyData.m_peFileKind == PEFileKinds.Dll, JitHelpers.GetStringHandleOnStack(ref text));
					onDiskAssemblyModuleBuilder.DefineUnmanagedResourceFileInternalNoLock(text);
				}
				if (moduleBuilder == null)
				{
					for (int i = 0; i < this.m_assemblyData.m_iCABuilder; i++)
					{
						array[i] = this.m_assemblyData.m_CABuilders[i].PrepareCreateCustomAttributeToDisk(onDiskAssemblyModuleBuilder);
					}
					for (int i = 0; i < this.m_assemblyData.m_iCAs; i++)
					{
						array2[i] = onDiskAssemblyModuleBuilder.InternalGetConstructorToken(this.m_assemblyData.m_CACons[i], true).Token;
					}
				}
				int count = this.m_assemblyData.m_moduleBuilderList.Count;
				for (int i = 0; i < count; i++)
				{
					ModuleBuilder moduleBuilder2 = this.m_assemblyData.m_moduleBuilderList[i];
					if (!moduleBuilder2.IsTransient() && moduleBuilder2 != moduleBuilder)
					{
						string text3 = moduleBuilder2.m_moduleData.m_strFileName;
						if (this.m_assemblyData.m_strDir != null)
						{
							text3 = Path.Combine(this.m_assemblyData.m_strDir, text3);
							text3 = Path.GetFullPath(text3);
						}
						new FileIOPermission(FileIOPermissionAccess.Write | FileIOPermissionAccess.Append, text3).Demand();
						moduleBuilder2.m_moduleData.FileToken = AssemblyBuilder.AddFile(this.GetNativeHandle(), moduleBuilder2.m_moduleData.m_strFileName);
						moduleBuilder2.PreSave(text3, portableExecutableKind, imageFileMachine);
						moduleBuilder2.Save(text3, false, portableExecutableKind, imageFileMachine);
						AssemblyBuilder.SetFileHashValue(this.GetNativeHandle(), moduleBuilder2.m_moduleData.FileToken, text3);
					}
				}
				for (int i = 0; i < this.m_assemblyData.m_iPublicComTypeCount; i++)
				{
					Type type = this.m_assemblyData.m_publicComTypeList[i];
					if (type is RuntimeType)
					{
						InternalModuleBuilder module = (InternalModuleBuilder)type.Module;
						ModuleBuilder moduleBuilder3 = this.GetModuleBuilder(module);
						if (moduleBuilder3 != moduleBuilder)
						{
							this.DefineNestedComType(type, moduleBuilder3.m_moduleData.FileToken, type.MetadataToken);
						}
					}
					else
					{
						TypeBuilder typeBuilder = (TypeBuilder)type;
						ModuleBuilder moduleBuilder3 = typeBuilder.GetModuleBuilder();
						if (moduleBuilder3 != moduleBuilder)
						{
							this.DefineNestedComType(type, moduleBuilder3.m_moduleData.FileToken, typeBuilder.MetadataTokenInternal);
						}
					}
				}
				if (onDiskAssemblyModuleBuilder != this.m_manifestModuleBuilder)
				{
					for (int i = 0; i < this.m_assemblyData.m_iCABuilder; i++)
					{
						this.m_assemblyData.m_CABuilders[i].CreateCustomAttribute(onDiskAssemblyModuleBuilder, 536870913, array[i], true);
					}
					for (int i = 0; i < this.m_assemblyData.m_iCAs; i++)
					{
						TypeBuilder.DefineCustomAttribute(onDiskAssemblyModuleBuilder, 536870913, array2[i], this.m_assemblyData.m_CABytes[i], true, false);
					}
				}
				if (this.m_assemblyData.m_RequiredPset != null)
				{
					this.AddDeclarativeSecurity(this.m_assemblyData.m_RequiredPset, SecurityAction.RequestMinimum);
				}
				if (this.m_assemblyData.m_RefusedPset != null)
				{
					this.AddDeclarativeSecurity(this.m_assemblyData.m_RefusedPset, SecurityAction.RequestRefuse);
				}
				if (this.m_assemblyData.m_OptionalPset != null)
				{
					this.AddDeclarativeSecurity(this.m_assemblyData.m_OptionalPset, SecurityAction.RequestOptional);
				}
				count = this.m_assemblyData.m_resWriterList.Count;
				for (int i = 0; i < count; i++)
				{
					ResWriterData resWriterData = null;
					try
					{
						resWriterData = this.m_assemblyData.m_resWriterList[i];
						if (resWriterData.m_resWriter != null)
						{
							new FileIOPermission(FileIOPermissionAccess.Write | FileIOPermissionAccess.Append, resWriterData.m_strFullFileName).Demand();
						}
					}
					finally
					{
						if (resWriterData != null && resWriterData.m_resWriter != null)
						{
							resWriterData.m_resWriter.Close();
						}
					}
					AssemblyBuilder.AddStandAloneResource(this.GetNativeHandle(), resWriterData.m_strName, resWriterData.m_strFileName, resWriterData.m_strFullFileName, (int)resWriterData.m_attribute);
				}
				if (moduleBuilder == null)
				{
					onDiskAssemblyModuleBuilder.DefineNativeResource(portableExecutableKind, imageFileMachine);
					int entryPoint = (this.m_assemblyData.m_entryPointModule != null) ? this.m_assemblyData.m_entryPointModule.m_moduleData.FileToken : 0;
					AssemblyBuilder.SaveManifestToDisk(this.GetNativeHandle(), assemblyFileName, entryPoint, (int)this.m_assemblyData.m_peFileKind, (int)portableExecutableKind, (int)imageFileMachine);
				}
				else
				{
					if (this.m_assemblyData.m_entryPointModule != null && this.m_assemblyData.m_entryPointModule != moduleBuilder)
					{
						moduleBuilder.SetEntryPoint(new MethodToken(this.m_assemblyData.m_entryPointModule.m_moduleData.FileToken));
					}
					moduleBuilder.Save(assemblyFileName, true, portableExecutableKind, imageFileMachine);
				}
				this.m_assemblyData.m_isSaved = true;
			}
			finally
			{
				if (text != null)
				{
					File.Delete(text);
				}
			}
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x001053B0 File Offset: 0x001035B0
		[SecurityCritical]
		private void AddDeclarativeSecurity(PermissionSet pset, SecurityAction action)
		{
			byte[] array = pset.EncodeXml();
			AssemblyBuilder.AddDeclarativeSecurity(this.GetNativeHandle(), action, array, array.Length);
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x001053D4 File Offset: 0x001035D4
		internal bool IsPersistable()
		{
			return (this.m_assemblyData.m_access & AssemblyBuilderAccess.Save) == AssemblyBuilderAccess.Save;
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x001053EC File Offset: 0x001035EC
		[SecurityCritical]
		private int DefineNestedComType(Type type, int tkResolutionScope, int tkTypeDef)
		{
			Type declaringType = type.DeclaringType;
			if (declaringType == null)
			{
				return AssemblyBuilder.AddExportedTypeOnDisk(this.GetNativeHandle(), type.FullName, tkResolutionScope, tkTypeDef, type.Attributes);
			}
			tkResolutionScope = this.DefineNestedComType(declaringType, tkResolutionScope, tkTypeDef);
			return AssemblyBuilder.AddExportedTypeOnDisk(this.GetNativeHandle(), type.Name, tkResolutionScope, tkTypeDef, type.Attributes);
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x00105448 File Offset: 0x00103648
		[SecurityCritical]
		internal int DefineExportedTypeInMemory(Type type, int tkResolutionScope, int tkTypeDef)
		{
			Type declaringType = type.DeclaringType;
			if (declaringType == null)
			{
				return AssemblyBuilder.AddExportedTypeInMemory(this.GetNativeHandle(), type.FullName, tkResolutionScope, tkTypeDef, type.Attributes);
			}
			tkResolutionScope = this.DefineExportedTypeInMemory(declaringType, tkResolutionScope, tkTypeDef);
			return AssemblyBuilder.AddExportedTypeInMemory(this.GetNativeHandle(), type.Name, tkResolutionScope, tkTypeDef, type.Attributes);
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x001054A3 File Offset: 0x001036A3
		private AssemblyBuilder()
		{
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x001054AB File Offset: 0x001036AB
		void _AssemblyBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x001054B2 File Offset: 0x001036B2
		void _AssemblyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004850 RID: 18512 RVA: 0x001054B9 File Offset: 0x001036B9
		void _AssemblyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x001054C0 File Offset: 0x001036C0
		void _AssemblyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004852 RID: 18514
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void DefineDynamicModule(RuntimeAssembly containingAssembly, bool emitSymbolInfo, string name, string filename, StackCrawlMarkHandle stackMark, ref IntPtr pInternalSymWriter, ObjectHandleOnStack retModule, bool fIsTransient, out int tkFile);

		// Token: 0x06004853 RID: 18515 RVA: 0x001054C8 File Offset: 0x001036C8
		[SecurityCritical]
		private static Module DefineDynamicModule(RuntimeAssembly containingAssembly, bool emitSymbolInfo, string name, string filename, ref StackCrawlMark stackMark, ref IntPtr pInternalSymWriter, bool fIsTransient, out int tkFile)
		{
			RuntimeModule result = null;
			AssemblyBuilder.DefineDynamicModule(containingAssembly.GetNativeHandle(), emitSymbolInfo, name, filename, JitHelpers.GetStackCrawlMarkHandle(ref stackMark), ref pInternalSymWriter, JitHelpers.GetObjectHandleOnStack<RuntimeModule>(ref result), fIsTransient, out tkFile);
			return result;
		}

		// Token: 0x06004854 RID: 18516
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void PrepareForSavingManifestToDisk(RuntimeAssembly assembly, RuntimeModule assemblyModule);

		// Token: 0x06004855 RID: 18517
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void SaveManifestToDisk(RuntimeAssembly assembly, string strFileName, int entryPoint, int fileKind, int portableExecutableKind, int ImageFileMachine);

		// Token: 0x06004856 RID: 18518
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int AddFile(RuntimeAssembly assembly, string strFileName);

		// Token: 0x06004857 RID: 18519
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void SetFileHashValue(RuntimeAssembly assembly, int tkFile, string strFullFileName);

		// Token: 0x06004858 RID: 18520
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int AddExportedTypeInMemory(RuntimeAssembly assembly, string strComTypeName, int tkAssemblyRef, int tkTypeDef, TypeAttributes flags);

		// Token: 0x06004859 RID: 18521
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int AddExportedTypeOnDisk(RuntimeAssembly assembly, string strComTypeName, int tkAssemblyRef, int tkTypeDef, TypeAttributes flags);

		// Token: 0x0600485A RID: 18522
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddStandAloneResource(RuntimeAssembly assembly, string strName, string strFileName, string strFullFileName, int attribute);

		// Token: 0x0600485B RID: 18523
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddDeclarativeSecurity(RuntimeAssembly assembly, SecurityAction action, byte[] blob, int length);

		// Token: 0x0600485C RID: 18524
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void CreateVersionInfoResource(string filename, string title, string iconFilename, string description, string copyright, string trademark, string company, string product, string productVersion, string fileVersion, int lcid, bool isDll, StringHandleOnStack retFileName);

		// Token: 0x04001D8E RID: 7566
		internal AssemblyBuilderData m_assemblyData;

		// Token: 0x04001D8F RID: 7567
		private InternalAssemblyBuilder m_internalAssemblyBuilder;

		// Token: 0x04001D90 RID: 7568
		private ModuleBuilder m_manifestModuleBuilder;

		// Token: 0x04001D91 RID: 7569
		private bool m_fManifestModuleUsedAsDefinedModule;

		// Token: 0x04001D92 RID: 7570
		internal const string MANIFEST_MODULE_NAME = "RefEmit_InMemoryManifestModule";

		// Token: 0x04001D93 RID: 7571
		private ModuleBuilder m_onDiskAssemblyModuleBuilder;

		// Token: 0x04001D94 RID: 7572
		private bool m_profileAPICheck;

		// Token: 0x02000C08 RID: 3080
		private class AssemblyBuilderLock
		{
		}
	}
}
