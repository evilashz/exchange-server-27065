using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x02000584 RID: 1412
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Assembly))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[Serializable]
	public abstract class Assembly : _Assembly, IEvidenceFactory, ICustomAttributeProvider, ISerializable
	{
		// Token: 0x06004254 RID: 16980 RVA: 0x000F5DC9 File Offset: 0x000F3FC9
		public static string CreateQualifiedName(string assemblyName, string typeName)
		{
			return typeName + ", " + assemblyName;
		}

		// Token: 0x06004255 RID: 16981 RVA: 0x000F5DD8 File Offset: 0x000F3FD8
		public static Assembly GetAssembly(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Module module = type.Module;
			if (module == null)
			{
				return null;
			}
			return module.Assembly;
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x000F5E11 File Offset: 0x000F4011
		[__DynamicallyInvokable]
		public static bool operator ==(Assembly left, Assembly right)
		{
			return left == right || (left != null && right != null && !(left is RuntimeAssembly) && !(right is RuntimeAssembly) && left.Equals(right));
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x000F5E38 File Offset: 0x000F4038
		[__DynamicallyInvokable]
		public static bool operator !=(Assembly left, Assembly right)
		{
			return !(left == right);
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x000F5E44 File Offset: 0x000F4044
		[__DynamicallyInvokable]
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x000F5E4D File Offset: 0x000F404D
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x000F5E58 File Offset: 0x000F4058
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFrom(string assemblyFile)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadFrom(assemblyFile, null, null, AssemblyHashAlgorithm.None, false, false, ref stackCrawlMark);
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x000F5E74 File Offset: 0x000F4074
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly ReflectionOnlyLoadFrom(string assemblyFile)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadFrom(assemblyFile, null, null, AssemblyHashAlgorithm.None, true, false, ref stackCrawlMark);
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x000F5E90 File Offset: 0x000F4090
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of LoadFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadFrom(assemblyFile, securityEvidence, null, AssemblyHashAlgorithm.None, false, false, ref stackCrawlMark);
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x000F5EAC File Offset: 0x000F40AC
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of LoadFrom which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadFrom(assemblyFile, securityEvidence, hashValue, hashAlgorithm, false, false, ref stackCrawlMark);
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x000F5EC8 File Offset: 0x000F40C8
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadFrom(string assemblyFile, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadFrom(assemblyFile, null, hashValue, hashAlgorithm, false, false, ref stackCrawlMark);
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x000F5EE4 File Offset: 0x000F40E4
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly UnsafeLoadFrom(string assemblyFile)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadFrom(assemblyFile, null, null, AssemblyHashAlgorithm.None, false, true, ref stackCrawlMark);
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x000F5F00 File Offset: 0x000F4100
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(string assemblyString)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoad(assemblyString, null, ref stackCrawlMark, false);
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x000F5F1C File Offset: 0x000F411C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static Type GetType_Compat(string assemblyString, string typeName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			RuntimeAssembly runtimeAssembly;
			AssemblyName assemblyName = RuntimeAssembly.CreateAssemblyName(assemblyString, false, out runtimeAssembly);
			if (runtimeAssembly == null)
			{
				if (assemblyName.ContentType == AssemblyContentType.WindowsRuntime)
				{
					return Type.GetType(typeName + ", " + assemblyString, true, false);
				}
				runtimeAssembly = RuntimeAssembly.InternalLoadAssemblyName(assemblyName, null, null, ref stackCrawlMark, true, false, false);
			}
			return runtimeAssembly.GetType(typeName, true, false);
		}

		// Token: 0x06004262 RID: 16994 RVA: 0x000F5F74 File Offset: 0x000F4174
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly ReflectionOnlyLoad(string assemblyString)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoad(assemblyString, null, ref stackCrawlMark, true);
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x000F5F90 File Offset: 0x000F4190
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(string assemblyString, Evidence assemblySecurity)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoad(assemblyString, assemblySecurity, ref stackCrawlMark, false);
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x000F5FAC File Offset: 0x000F41AC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(AssemblyName assemblyRef)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, null, null, ref stackCrawlMark, true, false, false);
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x000F5FC8 File Offset: 0x000F41C8
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, assemblySecurity, null, ref stackCrawlMark, true, false, false);
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x000F5FE4 File Offset: 0x000F41E4
		[SecuritySafeCritical]
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadWithPartialName(string partialName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.LoadWithPartialNameInternal(partialName, null, ref stackCrawlMark);
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x000F5FFC File Offset: 0x000F41FC
		[SecuritySafeCritical]
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.LoadWithPartialNameInternal(partialName, securityEvidence, ref stackCrawlMark);
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x000F6014 File Offset: 0x000F4214
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(byte[] rawAssembly)
		{
			AppDomain.CheckLoadByteArraySupported();
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.nLoadImage(rawAssembly, null, null, ref stackCrawlMark, false, false, SecurityContextSource.CurrentAssembly);
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x000F6038 File Offset: 0x000F4238
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly ReflectionOnlyLoad(byte[] rawAssembly)
		{
			AppDomain.CheckReflectionOnlyLoadSupported();
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.nLoadImage(rawAssembly, null, null, ref stackCrawlMark, true, false, SecurityContextSource.CurrentAssembly);
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x000F605C File Offset: 0x000F425C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
		{
			AppDomain.CheckLoadByteArraySupported();
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, null, ref stackCrawlMark, false, false, SecurityContextSource.CurrentAssembly);
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x000F6080 File Offset: 0x000F4280
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, SecurityContextSource securityContextSource)
		{
			AppDomain.CheckLoadByteArraySupported();
			if (securityContextSource < SecurityContextSource.CurrentAppDomain || securityContextSource > SecurityContextSource.CurrentAssembly)
			{
				throw new ArgumentOutOfRangeException("securityContextSource");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, null, ref stackCrawlMark, false, false, securityContextSource);
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x000F60B4 File Offset: 0x000F42B4
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static Assembly LoadImageSkipIntegrityCheck(byte[] rawAssembly, byte[] rawSymbolStore, SecurityContextSource securityContextSource)
		{
			AppDomain.CheckLoadByteArraySupported();
			if (securityContextSource < SecurityContextSource.CurrentAppDomain || securityContextSource > SecurityContextSource.CurrentAssembly)
			{
				throw new ArgumentOutOfRangeException("securityContextSource");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, null, ref stackCrawlMark, false, true, securityContextSource);
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x000F60E8 File Offset: 0x000F42E8
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of Load which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
		{
			AppDomain.CheckLoadByteArraySupported();
			if (securityEvidence != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				Zone hostEvidence = securityEvidence.GetHostEvidence<Zone>();
				if (hostEvidence == null || hostEvidence.SecurityZone != SecurityZone.MyComputer)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
				}
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.nLoadImage(rawAssembly, rawSymbolStore, securityEvidence, ref stackCrawlMark, false, false, SecurityContextSource.CurrentAssembly);
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x000F613A File Offset: 0x000F433A
		[SecuritySafeCritical]
		public static Assembly LoadFile(string path)
		{
			AppDomain.CheckLoadFileSupported();
			new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, path).Demand();
			return RuntimeAssembly.nLoadFile(path, null);
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x000F6155 File Offset: 0x000F4355
		[SecuritySafeCritical]
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of LoadFile which does not take an Evidence parameter. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence)]
		public static Assembly LoadFile(string path, Evidence securityEvidence)
		{
			AppDomain.CheckLoadFileSupported();
			if (securityEvidence != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
			}
			new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, path).Demand();
			return RuntimeAssembly.nLoadFile(path, securityEvidence);
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x000F6190 File Offset: 0x000F4390
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly GetExecutingAssembly()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.GetExecutingAssembly(ref stackCrawlMark);
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x000F61A8 File Offset: 0x000F43A8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Assembly GetCallingAssembly()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCallersCaller;
			return RuntimeAssembly.GetExecutingAssembly(ref stackCrawlMark);
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x000F61C0 File Offset: 0x000F43C0
		[SecuritySafeCritical]
		public static Assembly GetEntryAssembly()
		{
			AppDomainManager appDomainManager = AppDomain.CurrentDomain.DomainManager;
			if (appDomainManager == null)
			{
				appDomainManager = new AppDomainManager();
			}
			return appDomainManager.EntryAssembly;
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06004273 RID: 17011 RVA: 0x000F61E7 File Offset: 0x000F43E7
		// (remove) Token: 0x06004274 RID: 17012 RVA: 0x000F61EE File Offset: 0x000F43EE
		public virtual event ModuleResolveEventHandler ModuleResolve
		{
			[SecurityCritical]
			add
			{
				throw new NotImplementedException();
			}
			[SecurityCritical]
			remove
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06004275 RID: 17013 RVA: 0x000F61F5 File Offset: 0x000F43F5
		public virtual string CodeBase
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06004276 RID: 17014 RVA: 0x000F61FC File Offset: 0x000F43FC
		public virtual string EscapedCodeBase
		{
			[SecuritySafeCritical]
			get
			{
				return AssemblyName.EscapeCodeBase(this.CodeBase);
			}
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x000F6209 File Offset: 0x000F4409
		[__DynamicallyInvokable]
		public virtual AssemblyName GetName()
		{
			return this.GetName(false);
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x000F6212 File Offset: 0x000F4412
		public virtual AssemblyName GetName(bool copiedName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06004279 RID: 17017 RVA: 0x000F6219 File Offset: 0x000F4419
		[__DynamicallyInvokable]
		public virtual string FullName
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x000F6220 File Offset: 0x000F4420
		[__DynamicallyInvokable]
		public virtual MethodInfo EntryPoint
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x000F6227 File Offset: 0x000F4427
		Type _Assembly.GetType()
		{
			return base.GetType();
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x000F622F File Offset: 0x000F442F
		[__DynamicallyInvokable]
		public virtual Type GetType(string name)
		{
			return this.GetType(name, false, false);
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x000F623A File Offset: 0x000F443A
		[__DynamicallyInvokable]
		public virtual Type GetType(string name, bool throwOnError)
		{
			return this.GetType(name, throwOnError, false);
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x000F6245 File Offset: 0x000F4445
		[__DynamicallyInvokable]
		public virtual Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600427F RID: 17023 RVA: 0x000F624C File Offset: 0x000F444C
		[__DynamicallyInvokable]
		public virtual IEnumerable<Type> ExportedTypes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetExportedTypes();
			}
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x000F6254 File Offset: 0x000F4454
		[__DynamicallyInvokable]
		public virtual Type[] GetExportedTypes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06004281 RID: 17025 RVA: 0x000F625C File Offset: 0x000F445C
		[__DynamicallyInvokable]
		public virtual IEnumerable<TypeInfo> DefinedTypes
		{
			[__DynamicallyInvokable]
			get
			{
				Type[] types = this.GetTypes();
				TypeInfo[] array = new TypeInfo[types.Length];
				for (int i = 0; i < types.Length; i++)
				{
					TypeInfo typeInfo = types[i].GetTypeInfo();
					if (typeInfo == null)
					{
						throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoTypeInfo", new object[]
						{
							types[i].FullName
						}));
					}
					array[i] = typeInfo;
				}
				return array;
			}
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x000F62C0 File Offset: 0x000F44C0
		[__DynamicallyInvokable]
		public virtual Type[] GetTypes()
		{
			Module[] modules = this.GetModules(false);
			int num = modules.Length;
			int num2 = 0;
			Type[][] array = new Type[num][];
			for (int i = 0; i < num; i++)
			{
				array[i] = modules[i].GetTypes();
				num2 += array[i].Length;
			}
			int num3 = 0;
			Type[] array2 = new Type[num2];
			for (int j = 0; j < num; j++)
			{
				int num4 = array[j].Length;
				Array.Copy(array[j], 0, array2, num3, num4);
				num3 += num4;
			}
			return array2;
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x000F6344 File Offset: 0x000F4544
		[__DynamicallyInvokable]
		public virtual Stream GetManifestResourceStream(Type type, string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004284 RID: 17028 RVA: 0x000F634B File Offset: 0x000F454B
		[__DynamicallyInvokable]
		public virtual Stream GetManifestResourceStream(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x000F6352 File Offset: 0x000F4552
		public virtual Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x000F6359 File Offset: 0x000F4559
		public virtual Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06004287 RID: 17031 RVA: 0x000F6360 File Offset: 0x000F4560
		public virtual Evidence Evidence
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06004288 RID: 17032 RVA: 0x000F6367 File Offset: 0x000F4567
		public virtual PermissionSet PermissionSet
		{
			[SecurityCritical]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06004289 RID: 17033 RVA: 0x000F636E File Offset: 0x000F456E
		public bool IsFullyTrusted
		{
			[SecuritySafeCritical]
			get
			{
				return this.PermissionSet.IsUnrestricted();
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x0600428A RID: 17034 RVA: 0x000F637B File Offset: 0x000F457B
		public virtual SecurityRuleSet SecurityRuleSet
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x000F6382 File Offset: 0x000F4582
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x0600428C RID: 17036 RVA: 0x000F638C File Offset: 0x000F458C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual Module ManifestModule
		{
			[__DynamicallyInvokable]
			get
			{
				RuntimeAssembly runtimeAssembly = this as RuntimeAssembly;
				if (runtimeAssembly != null)
				{
					return runtimeAssembly.ManifestModule;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x0600428D RID: 17037 RVA: 0x000F63B5 File Offset: 0x000F45B5
		[__DynamicallyInvokable]
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x000F63BD File Offset: 0x000F45BD
		[__DynamicallyInvokable]
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x000F63C4 File Offset: 0x000F45C4
		[__DynamicallyInvokable]
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x000F63CB File Offset: 0x000F45CB
		[__DynamicallyInvokable]
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x000F63D2 File Offset: 0x000F45D2
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06004292 RID: 17042 RVA: 0x000F63D9 File Offset: 0x000F45D9
		[ComVisible(false)]
		public virtual bool ReflectionOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x000F63E0 File Offset: 0x000F45E0
		public Module LoadModule(string moduleName, byte[] rawModule)
		{
			return this.LoadModule(moduleName, rawModule, null);
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x000F63EB File Offset: 0x000F45EB
		public virtual Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x000F63F2 File Offset: 0x000F45F2
		public object CreateInstance(string typeName)
		{
			return this.CreateInstance(typeName, false, BindingFlags.Instance | BindingFlags.Public, null, null, null, null);
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x000F6402 File Offset: 0x000F4602
		public object CreateInstance(string typeName, bool ignoreCase)
		{
			return this.CreateInstance(typeName, ignoreCase, BindingFlags.Instance | BindingFlags.Public, null, null, null, null);
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x000F6414 File Offset: 0x000F4614
		public virtual object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			return Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06004298 RID: 17048 RVA: 0x000F6445 File Offset: 0x000F4645
		[__DynamicallyInvokable]
		public virtual IEnumerable<Module> Modules
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetLoadedModules(true);
			}
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x000F644E File Offset: 0x000F464E
		public Module[] GetLoadedModules()
		{
			return this.GetLoadedModules(false);
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x000F6457 File Offset: 0x000F4657
		public virtual Module[] GetLoadedModules(bool getResourceModules)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x000F645E File Offset: 0x000F465E
		[__DynamicallyInvokable]
		public Module[] GetModules()
		{
			return this.GetModules(false);
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x000F6467 File Offset: 0x000F4667
		public virtual Module[] GetModules(bool getResourceModules)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x000F646E File Offset: 0x000F466E
		public virtual Module GetModule(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x000F6475 File Offset: 0x000F4675
		public virtual FileStream GetFile(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x000F647C File Offset: 0x000F467C
		public virtual FileStream[] GetFiles()
		{
			return this.GetFiles(false);
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x000F6485 File Offset: 0x000F4685
		public virtual FileStream[] GetFiles(bool getResourceModules)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x000F648C File Offset: 0x000F468C
		[__DynamicallyInvokable]
		public virtual string[] GetManifestResourceNames()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x000F6493 File Offset: 0x000F4693
		public virtual AssemblyName[] GetReferencedAssemblies()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x000F649A File Offset: 0x000F469A
		[__DynamicallyInvokable]
		public virtual ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x000F64A4 File Offset: 0x000F46A4
		[__DynamicallyInvokable]
		public override string ToString()
		{
			string fullName = this.FullName;
			if (fullName == null)
			{
				return base.ToString();
			}
			return fullName;
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060042A5 RID: 17061 RVA: 0x000F64C3 File Offset: 0x000F46C3
		public virtual string Location
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x060042A6 RID: 17062 RVA: 0x000F64CA File Offset: 0x000F46CA
		[ComVisible(false)]
		public virtual string ImageRuntimeVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x060042A7 RID: 17063 RVA: 0x000F64D1 File Offset: 0x000F46D1
		public virtual bool GlobalAssemblyCache
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x060042A8 RID: 17064 RVA: 0x000F64D8 File Offset: 0x000F46D8
		[ComVisible(false)]
		public virtual long HostContext
		{
			get
			{
				RuntimeAssembly runtimeAssembly = this as RuntimeAssembly;
				if (runtimeAssembly != null)
				{
					return runtimeAssembly.HostContext;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x000F6501 File Offset: 0x000F4701
		[__DynamicallyInvokable]
		public virtual bool IsDynamic
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}
	}
}
