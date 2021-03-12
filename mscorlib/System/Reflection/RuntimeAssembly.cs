using System;
using System.Collections;
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
using System.Security.Util;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace System.Reflection
{
	// Token: 0x02000586 RID: 1414
	[Serializable]
	internal class RuntimeAssembly : Assembly, ICustomQueryInterface
	{
		// Token: 0x060042AA RID: 17066 RVA: 0x000F6504 File Offset: 0x000F4704
		[SecurityCritical]
		CustomQueryInterfaceResult ICustomQueryInterface.GetInterface([In] ref Guid iid, out IntPtr ppv)
		{
			if (iid == typeof(NativeMethods.IDispatch).GUID)
			{
				ppv = Marshal.GetComInterfaceForObject(this, typeof(_Assembly));
				return CustomQueryInterfaceResult.Handled;
			}
			ppv = IntPtr.Zero;
			return CustomQueryInterfaceResult.NotHandled;
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x000F653E File Offset: 0x000F473E
		internal RuntimeAssembly()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060042AC RID: 17068 RVA: 0x000F654C File Offset: 0x000F474C
		// (remove) Token: 0x060042AD RID: 17069 RVA: 0x000F6584 File Offset: 0x000F4784
		[method: SecurityCritical]
		private event ModuleResolveEventHandler _ModuleResolve;

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060042AE RID: 17070 RVA: 0x000F65BC File Offset: 0x000F47BC
		internal int InvocableAttributeCtorToken
		{
			get
			{
				int num = (int)(this.Flags & RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_TOKEN_MASK);
				return num | 100663296;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060042AF RID: 17071 RVA: 0x000F65E0 File Offset: 0x000F47E0
		private RuntimeAssembly.ASSEMBLY_FLAGS Flags
		{
			[SecuritySafeCritical]
			get
			{
				if ((this.m_flags & RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_INITIALIZED) == RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_UNKNOWN)
				{
					RuntimeAssembly.ASSEMBLY_FLAGS assembly_FLAGS = RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_UNKNOWN;
					if (RuntimeAssembly.IsFrameworkAssembly(this.GetName()))
					{
						assembly_FLAGS |= (RuntimeAssembly.ASSEMBLY_FLAGS)100663296U;
						foreach (string strB in RuntimeAssembly.s_unsafeFrameworkAssemblyNames)
						{
							if (string.Compare(this.GetSimpleName(), strB, StringComparison.OrdinalIgnoreCase) == 0)
							{
								assembly_FLAGS &= (RuntimeAssembly.ASSEMBLY_FLAGS)4227858431U;
								break;
							}
						}
						Type type = this.GetType("__DynamicallyInvokableAttribute", false);
						if (type != null)
						{
							ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
							int metadataToken = constructor.MetadataToken;
							assembly_FLAGS |= (RuntimeAssembly.ASSEMBLY_FLAGS)(metadataToken & 16777215);
						}
					}
					else if (this.IsDesignerBindingContext())
					{
						assembly_FLAGS = RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_SAFE_REFLECTION;
					}
					this.m_flags = (assembly_FLAGS | RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_INITIALIZED);
				}
				return this.m_flags;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x000F66A2 File Offset: 0x000F48A2
		internal object SyncRoot
		{
			get
			{
				if (this.m_syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), null);
				}
				return this.m_syncRoot;
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060042B1 RID: 17073 RVA: 0x000F66C4 File Offset: 0x000F48C4
		// (remove) Token: 0x060042B2 RID: 17074 RVA: 0x000F66CD File Offset: 0x000F48CD
		public override event ModuleResolveEventHandler ModuleResolve
		{
			[SecurityCritical]
			add
			{
				this._ModuleResolve += value;
			}
			[SecurityCritical]
			remove
			{
				this._ModuleResolve -= value;
			}
		}

		// Token: 0x060042B3 RID: 17075
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetCodeBase(RuntimeAssembly assembly, bool copiedName, StringHandleOnStack retString);

		// Token: 0x060042B4 RID: 17076 RVA: 0x000F66D8 File Offset: 0x000F48D8
		[SecurityCritical]
		internal string GetCodeBase(bool copiedName)
		{
			string result = null;
			RuntimeAssembly.GetCodeBase(this.GetNativeHandle(), copiedName, JitHelpers.GetStringHandleOnStack(ref result));
			return result;
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060042B5 RID: 17077 RVA: 0x000F66FC File Offset: 0x000F48FC
		public override string CodeBase
		{
			[SecuritySafeCritical]
			get
			{
				string codeBase = this.GetCodeBase(false);
				this.VerifyCodeBaseDiscovery(codeBase);
				return codeBase;
			}
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x000F6719 File Offset: 0x000F4919
		internal RuntimeAssembly GetNativeHandle()
		{
			return this;
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x000F671C File Offset: 0x000F491C
		[SecuritySafeCritical]
		public override AssemblyName GetName(bool copiedName)
		{
			AssemblyName assemblyName = new AssemblyName();
			string codeBase = this.GetCodeBase(copiedName);
			this.VerifyCodeBaseDiscovery(codeBase);
			assemblyName.Init(this.GetSimpleName(), this.GetPublicKey(), null, this.GetVersion(), this.GetLocale(), this.GetHashAlgorithm(), AssemblyVersionCompatibility.SameMachine, codeBase, this.GetFlags() | AssemblyNameFlags.PublicKey, null);
			Module manifestModule = this.ManifestModule;
			if (manifestModule != null && manifestModule.MDStreamVersion > 65536)
			{
				PortableExecutableKinds pek;
				ImageFileMachine ifm;
				this.ManifestModule.GetPEKind(out pek, out ifm);
				assemblyName.SetProcArchIndex(pek, ifm);
			}
			return assemblyName;
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x000F67A8 File Offset: 0x000F49A8
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private string GetNameForConditionalAptca()
		{
			AssemblyName name = this.GetName();
			return name.GetNameWithPublicKey();
		}

		// Token: 0x060042B9 RID: 17081
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetFullName(RuntimeAssembly assembly, StringHandleOnStack retString);

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060042BA RID: 17082 RVA: 0x000F67C4 File Offset: 0x000F49C4
		public override string FullName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_fullname == null)
				{
					string value = null;
					RuntimeAssembly.GetFullName(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref value));
					Interlocked.CompareExchange<string>(ref this.m_fullname, value, null);
				}
				return this.m_fullname;
			}
		}

		// Token: 0x060042BB RID: 17083
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetEntryPoint(RuntimeAssembly assembly, ObjectHandleOnStack retMethod);

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060042BC RID: 17084 RVA: 0x000F6804 File Offset: 0x000F4A04
		public override MethodInfo EntryPoint
		{
			[SecuritySafeCritical]
			get
			{
				IRuntimeMethodInfo runtimeMethodInfo = null;
				RuntimeAssembly.GetEntryPoint(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref runtimeMethodInfo));
				if (runtimeMethodInfo == null)
				{
					return null;
				}
				return (MethodInfo)RuntimeType.GetMethodBase(runtimeMethodInfo);
			}
		}

		// Token: 0x060042BD RID: 17085
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetType(RuntimeAssembly assembly, string name, bool throwOnError, bool ignoreCase, ObjectHandleOnStack type);

		// Token: 0x060042BE RID: 17086 RVA: 0x000F6838 File Offset: 0x000F4A38
		[SecuritySafeCritical]
		public override Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			RuntimeType result = null;
			RuntimeAssembly.GetType(this.GetNativeHandle(), name, throwOnError, ignoreCase, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref result));
			return result;
		}

		// Token: 0x060042BF RID: 17087
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void GetForwardedTypes(RuntimeAssembly assembly, ObjectHandleOnStack retTypes);

		// Token: 0x060042C0 RID: 17088
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetExportedTypes(RuntimeAssembly assembly, ObjectHandleOnStack retTypes);

		// Token: 0x060042C1 RID: 17089 RVA: 0x000F686C File Offset: 0x000F4A6C
		[SecuritySafeCritical]
		public override Type[] GetExportedTypes()
		{
			Type[] result = null;
			RuntimeAssembly.GetExportedTypes(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref result));
			return result;
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060042C2 RID: 17090 RVA: 0x000F6890 File Offset: 0x000F4A90
		public override IEnumerable<TypeInfo> DefinedTypes
		{
			[SecuritySafeCritical]
			get
			{
				List<RuntimeType> list = new List<RuntimeType>();
				RuntimeModule[] modulesInternal = this.GetModulesInternal(true, false);
				for (int i = 0; i < modulesInternal.Length; i++)
				{
					list.AddRange(modulesInternal[i].GetDefinedTypes());
				}
				return list.ToArray();
			}
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x000F68D0 File Offset: 0x000F4AD0
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.GetManifestResourceStream(type, name, false, ref stackCrawlMark);
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x000F68EC File Offset: 0x000F4AEC
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Stream GetManifestResourceStream(string name)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.GetManifestResourceStream(name, ref stackCrawlMark, false);
		}

		// Token: 0x060042C5 RID: 17093
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetEvidence(RuntimeAssembly assembly, ObjectHandleOnStack retEvidence);

		// Token: 0x060042C6 RID: 17094
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern SecurityRuleSet GetSecurityRuleSet(RuntimeAssembly assembly);

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060042C7 RID: 17095 RVA: 0x000F6908 File Offset: 0x000F4B08
		public override Evidence Evidence
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
			get
			{
				Evidence evidenceNoDemand = this.EvidenceNoDemand;
				return evidenceNoDemand.Clone();
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060042C8 RID: 17096 RVA: 0x000F6924 File Offset: 0x000F4B24
		internal Evidence EvidenceNoDemand
		{
			[SecurityCritical]
			get
			{
				Evidence result = null;
				RuntimeAssembly.GetEvidence(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Evidence>(ref result));
				return result;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060042C9 RID: 17097 RVA: 0x000F6948 File Offset: 0x000F4B48
		public override PermissionSet PermissionSet
		{
			[SecurityCritical]
			get
			{
				PermissionSet permissionSet = null;
				PermissionSet permissionSet2 = null;
				this.GetGrantSet(out permissionSet, out permissionSet2);
				if (permissionSet != null)
				{
					return permissionSet.Copy();
				}
				return new PermissionSet(PermissionState.Unrestricted);
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060042CA RID: 17098 RVA: 0x000F6973 File Offset: 0x000F4B73
		public override SecurityRuleSet SecurityRuleSet
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeAssembly.GetSecurityRuleSet(this.GetNativeHandle());
			}
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x000F6980 File Offset: 0x000F4B80
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 6, this.FullName, this);
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060042CC RID: 17100 RVA: 0x000F699E File Offset: 0x000F4B9E
		public override Module ManifestModule
		{
			get
			{
				return RuntimeAssembly.GetManifestModule(this.GetNativeHandle());
			}
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x000F69AB File Offset: 0x000F4BAB
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x000F69C4 File Offset: 0x000F4BC4
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x000F6A18 File Offset: 0x000F4C18
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "caType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x000F6A6A File Offset: 0x000F4C6A
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x000F6A74 File Offset: 0x000F4C74
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static RuntimeAssembly InternalLoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm, bool forIntrospection, bool suppressSecurityChecks, ref StackCrawlMark stackMark)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			if (securityEvidence != null && !AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
			}
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.CodeBase = assemblyFile;
			assemblyName.SetHashControl(hashValue, hashAlgorithm);
			return RuntimeAssembly.InternalLoadAssemblyName(assemblyName, securityEvidence, null, ref stackMark, true, forIntrospection, suppressSecurityChecks);
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x000F6AD2 File Offset: 0x000F4CD2
		[SecurityCritical]
		internal static RuntimeAssembly InternalLoad(string assemblyString, Evidence assemblySecurity, ref StackCrawlMark stackMark, bool forIntrospection)
		{
			return RuntimeAssembly.InternalLoad(assemblyString, assemblySecurity, ref stackMark, IntPtr.Zero, forIntrospection);
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x000F6AE4 File Offset: 0x000F4CE4
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static RuntimeAssembly InternalLoad(string assemblyString, Evidence assemblySecurity, ref StackCrawlMark stackMark, IntPtr pPrivHostBinder, bool forIntrospection)
		{
			RuntimeAssembly runtimeAssembly;
			AssemblyName assemblyRef = RuntimeAssembly.CreateAssemblyName(assemblyString, forIntrospection, out runtimeAssembly);
			if (runtimeAssembly != null)
			{
				return runtimeAssembly;
			}
			return RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, assemblySecurity, null, ref stackMark, pPrivHostBinder, true, forIntrospection, false);
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x000F6B18 File Offset: 0x000F4D18
		[SecurityCritical]
		internal static AssemblyName CreateAssemblyName(string assemblyString, bool forIntrospection, out RuntimeAssembly assemblyFromResolveEvent)
		{
			if (assemblyString == null)
			{
				throw new ArgumentNullException("assemblyString");
			}
			if (assemblyString.Length == 0 || assemblyString[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Format_StringZeroLength"));
			}
			if (forIntrospection)
			{
				AppDomain.CheckReflectionOnlyLoadSupported();
			}
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = assemblyString;
			assemblyName.nInit(out assemblyFromResolveEvent, forIntrospection, true);
			return assemblyName;
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x000F6B73 File Offset: 0x000F4D73
		[SecurityCritical]
		internal static RuntimeAssembly InternalLoadAssemblyName(AssemblyName assemblyRef, Evidence assemblySecurity, RuntimeAssembly reqAssembly, ref StackCrawlMark stackMark, bool throwOnFileNotFound, bool forIntrospection, bool suppressSecurityChecks)
		{
			return RuntimeAssembly.InternalLoadAssemblyName(assemblyRef, assemblySecurity, reqAssembly, ref stackMark, IntPtr.Zero, true, forIntrospection, suppressSecurityChecks);
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x000F6B88 File Offset: 0x000F4D88
		[SecurityCritical]
		internal static RuntimeAssembly InternalLoadAssemblyName(AssemblyName assemblyRef, Evidence assemblySecurity, RuntimeAssembly reqAssembly, ref StackCrawlMark stackMark, IntPtr pPrivHostBinder, bool throwOnFileNotFound, bool forIntrospection, bool suppressSecurityChecks)
		{
			if (assemblyRef == null)
			{
				throw new ArgumentNullException("assemblyRef");
			}
			if (assemblyRef.CodeBase != null)
			{
				AppDomain.CheckLoadFromSupported();
			}
			assemblyRef = (AssemblyName)assemblyRef.Clone();
			if (assemblySecurity != null)
			{
				if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
				}
				if (!suppressSecurityChecks)
				{
					new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
				}
			}
			string text = RuntimeAssembly.VerifyCodeBase(assemblyRef.CodeBase);
			if (text != null && !suppressSecurityChecks)
			{
				if (string.Compare(text, 0, "file:", 0, 5, StringComparison.OrdinalIgnoreCase) != 0)
				{
					IPermission permission = RuntimeAssembly.CreateWebPermission(assemblyRef.EscapedCodeBase);
					permission.Demand();
				}
				else
				{
					URLString urlstring = new URLString(text, true);
					new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, urlstring.GetFileName()).Demand();
				}
			}
			return RuntimeAssembly.nLoad(assemblyRef, text, assemblySecurity, reqAssembly, ref stackMark, pPrivHostBinder, throwOnFileNotFound, forIntrospection, suppressSecurityChecks);
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x000F6C50 File Offset: 0x000F4E50
		[SecuritySafeCritical]
		internal bool IsFrameworkAssembly()
		{
			RuntimeAssembly.ASSEMBLY_FLAGS flags = this.Flags;
			return (flags & RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_FRAMEWORK) > RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_UNKNOWN;
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x000F6C70 File Offset: 0x000F4E70
		internal bool IsSafeForReflection()
		{
			RuntimeAssembly.ASSEMBLY_FLAGS flags = this.Flags;
			return (flags & RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_SAFE_REFLECTION) > RuntimeAssembly.ASSEMBLY_FLAGS.ASSEMBLY_FLAGS_UNKNOWN;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x000F6C8E File Offset: 0x000F4E8E
		[SecuritySafeCritical]
		private bool IsDesignerBindingContext()
		{
			return RuntimeAssembly.nIsDesignerBindingContext(this);
		}

		// Token: 0x060042DA RID: 17114
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool nIsDesignerBindingContext(RuntimeAssembly assembly);

		// Token: 0x060042DB RID: 17115
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeAssembly _nLoad(AssemblyName fileName, string codeBase, Evidence assemblySecurity, RuntimeAssembly locationHint, ref StackCrawlMark stackMark, IntPtr pPrivHostBinder, bool throwOnFileNotFound, bool forIntrospection, bool suppressSecurityChecks);

		// Token: 0x060042DC RID: 17116
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsFrameworkAssembly(AssemblyName assemblyName);

		// Token: 0x060042DD RID: 17117
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsNewPortableAssembly(AssemblyName assemblyName);

		// Token: 0x060042DE RID: 17118 RVA: 0x000F6C98 File Offset: 0x000F4E98
		[SecurityCritical]
		private static RuntimeAssembly nLoad(AssemblyName fileName, string codeBase, Evidence assemblySecurity, RuntimeAssembly locationHint, ref StackCrawlMark stackMark, IntPtr pPrivHostBinder, bool throwOnFileNotFound, bool forIntrospection, bool suppressSecurityChecks)
		{
			return RuntimeAssembly._nLoad(fileName, codeBase, assemblySecurity, locationHint, ref stackMark, pPrivHostBinder, throwOnFileNotFound, forIntrospection, suppressSecurityChecks);
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x000F6CB8 File Offset: 0x000F4EB8
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static RuntimeAssembly LoadWithPartialNameHack(string partialName, bool cropPublicKey)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			AssemblyName assemblyName = new AssemblyName(partialName);
			if (!RuntimeAssembly.IsSimplyNamed(assemblyName))
			{
				if (cropPublicKey)
				{
					assemblyName.SetPublicKey(null);
					assemblyName.SetPublicKeyToken(null);
				}
				if (RuntimeAssembly.IsFrameworkAssembly(assemblyName) || !AppDomain.IsAppXModel())
				{
					AssemblyName assemblyName2 = RuntimeAssembly.EnumerateCache(assemblyName);
					if (assemblyName2 != null)
					{
						return RuntimeAssembly.InternalLoadAssemblyName(assemblyName2, null, null, ref stackCrawlMark, true, false, false);
					}
					return null;
				}
			}
			if (AppDomain.IsAppXModel())
			{
				assemblyName.Version = null;
				return RuntimeAssembly.nLoad(assemblyName, null, null, null, ref stackCrawlMark, IntPtr.Zero, false, false, false);
			}
			return null;
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x000F6D34 File Offset: 0x000F4F34
		[SecurityCritical]
		internal static RuntimeAssembly LoadWithPartialNameInternal(string partialName, Evidence securityEvidence, ref StackCrawlMark stackMark)
		{
			AssemblyName an = new AssemblyName(partialName);
			return RuntimeAssembly.LoadWithPartialNameInternal(an, securityEvidence, ref stackMark);
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x000F6D50 File Offset: 0x000F4F50
		[SecurityCritical]
		internal static RuntimeAssembly LoadWithPartialNameInternal(AssemblyName an, Evidence securityEvidence, ref StackCrawlMark stackMark)
		{
			if (securityEvidence != null)
			{
				if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyImplicit"));
				}
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			AppDomain.CheckLoadWithPartialNameSupported(stackMark);
			RuntimeAssembly result = null;
			try
			{
				result = RuntimeAssembly.nLoad(an, null, securityEvidence, null, ref stackMark, IntPtr.Zero, true, false, false);
			}
			catch (Exception ex)
			{
				if (ex.IsTransient)
				{
					throw ex;
				}
				if (RuntimeAssembly.IsUserError(ex))
				{
					throw;
				}
				if (RuntimeAssembly.IsFrameworkAssembly(an) || !AppDomain.IsAppXModel())
				{
					if (RuntimeAssembly.IsSimplyNamed(an))
					{
						return null;
					}
					AssemblyName assemblyName = RuntimeAssembly.EnumerateCache(an);
					if (assemblyName != null)
					{
						result = RuntimeAssembly.InternalLoadAssemblyName(assemblyName, securityEvidence, null, ref stackMark, true, false, false);
					}
				}
				else
				{
					an.Version = null;
					result = RuntimeAssembly.nLoad(an, null, securityEvidence, null, ref stackMark, IntPtr.Zero, false, false, false);
				}
			}
			return result;
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x000F6E20 File Offset: 0x000F5020
		[SecuritySafeCritical]
		private static bool IsUserError(Exception e)
		{
			return e.HResult == -2146234280;
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x000F6E30 File Offset: 0x000F5030
		private static bool IsSimplyNamed(AssemblyName partialName)
		{
			byte[] array = partialName.GetPublicKeyToken();
			if (array != null && array.Length == 0)
			{
				return true;
			}
			array = partialName.GetPublicKey();
			return array != null && array.Length == 0;
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x000F6E60 File Offset: 0x000F5060
		[SecurityCritical]
		private static AssemblyName EnumerateCache(AssemblyName partialName)
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
			partialName.Version = null;
			ArrayList arrayList = new ArrayList();
			Fusion.ReadCache(arrayList, partialName.FullName, 2U);
			IEnumerator enumerator = arrayList.GetEnumerator();
			AssemblyName assemblyName = null;
			CultureInfo cultureInfo = partialName.CultureInfo;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				AssemblyName assemblyName2 = new AssemblyName((string)obj);
				if (RuntimeAssembly.CulturesEqual(cultureInfo, assemblyName2.CultureInfo))
				{
					if (assemblyName == null)
					{
						assemblyName = assemblyName2;
					}
					else if (assemblyName2.Version > assemblyName.Version)
					{
						assemblyName = assemblyName2;
					}
				}
			}
			return assemblyName;
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x000F6EF0 File Offset: 0x000F50F0
		private static bool CulturesEqual(CultureInfo refCI, CultureInfo defCI)
		{
			bool flag = defCI.Equals(CultureInfo.InvariantCulture);
			if (refCI == null || refCI.Equals(CultureInfo.InvariantCulture))
			{
				return flag;
			}
			return !flag && defCI.Equals(refCI);
		}

		// Token: 0x060042E6 RID: 17126
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsReflectionOnly(RuntimeAssembly assembly);

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x000F6F2A File Offset: 0x000F512A
		[ComVisible(false)]
		public override bool ReflectionOnly
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeAssembly.IsReflectionOnly(this.GetNativeHandle());
			}
		}

		// Token: 0x060042E8 RID: 17128
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void LoadModule(RuntimeAssembly assembly, string moduleName, byte[] rawModule, int cbModule, byte[] rawSymbolStore, int cbSymbolStore, ObjectHandleOnStack retModule);

		// Token: 0x060042E9 RID: 17129 RVA: 0x000F6F38 File Offset: 0x000F5138
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
		public override Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
		{
			RuntimeModule result = null;
			RuntimeAssembly.LoadModule(this.GetNativeHandle(), moduleName, rawModule, (rawModule != null) ? rawModule.Length : 0, rawSymbolStore, (rawSymbolStore != null) ? rawSymbolStore.Length : 0, JitHelpers.GetObjectHandleOnStack<RuntimeModule>(ref result));
			return result;
		}

		// Token: 0x060042EA RID: 17130
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetModule(RuntimeAssembly assembly, string name, ObjectHandleOnStack retModule);

		// Token: 0x060042EB RID: 17131 RVA: 0x000F6F70 File Offset: 0x000F5170
		[SecuritySafeCritical]
		public override Module GetModule(string name)
		{
			Module result = null;
			RuntimeAssembly.GetModule(this.GetNativeHandle(), name, JitHelpers.GetObjectHandleOnStack<Module>(ref result));
			return result;
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x000F6F94 File Offset: 0x000F5194
		[SecuritySafeCritical]
		public override FileStream GetFile(string name)
		{
			RuntimeModule runtimeModule = (RuntimeModule)this.GetModule(name);
			if (runtimeModule == null)
			{
				return null;
			}
			return new FileStream(runtimeModule.GetFullyQualifiedName(), FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x000F6FD0 File Offset: 0x000F51D0
		[SecuritySafeCritical]
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			Module[] modules = this.GetModules(getResourceModules);
			int num = modules.Length;
			FileStream[] array = new FileStream[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new FileStream(((RuntimeModule)modules[i]).GetFullyQualifiedName(), FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
			}
			return array;
		}

		// Token: 0x060042EE RID: 17134
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetManifestResourceNames(RuntimeAssembly assembly);

		// Token: 0x060042EF RID: 17135 RVA: 0x000F701B File Offset: 0x000F521B
		[SecuritySafeCritical]
		public override string[] GetManifestResourceNames()
		{
			return RuntimeAssembly.GetManifestResourceNames(this.GetNativeHandle());
		}

		// Token: 0x060042F0 RID: 17136
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetExecutingAssembly(StackCrawlMarkHandle stackMark, ObjectHandleOnStack retAssembly);

		// Token: 0x060042F1 RID: 17137 RVA: 0x000F7028 File Offset: 0x000F5228
		[SecurityCritical]
		internal static RuntimeAssembly GetExecutingAssembly(ref StackCrawlMark stackMark)
		{
			RuntimeAssembly result = null;
			RuntimeAssembly.GetExecutingAssembly(JitHelpers.GetStackCrawlMarkHandle(ref stackMark), JitHelpers.GetObjectHandleOnStack<RuntimeAssembly>(ref result));
			return result;
		}

		// Token: 0x060042F2 RID: 17138
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AssemblyName[] GetReferencedAssemblies(RuntimeAssembly assembly);

		// Token: 0x060042F3 RID: 17139 RVA: 0x000F704A File Offset: 0x000F524A
		[SecuritySafeCritical]
		public override AssemblyName[] GetReferencedAssemblies()
		{
			return RuntimeAssembly.GetReferencedAssemblies(this.GetNativeHandle());
		}

		// Token: 0x060042F4 RID: 17140
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetManifestResourceInfo(RuntimeAssembly assembly, string resourceName, ObjectHandleOnStack assemblyRef, StringHandleOnStack retFileName, StackCrawlMarkHandle stackMark);

		// Token: 0x060042F5 RID: 17141 RVA: 0x000F7058 File Offset: 0x000F5258
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			RuntimeAssembly containingAssembly = null;
			string containingFileName = null;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			int manifestResourceInfo = RuntimeAssembly.GetManifestResourceInfo(this.GetNativeHandle(), resourceName, JitHelpers.GetObjectHandleOnStack<RuntimeAssembly>(ref containingAssembly), JitHelpers.GetStringHandleOnStack(ref containingFileName), JitHelpers.GetStackCrawlMarkHandle(ref stackCrawlMark));
			if (manifestResourceInfo == -1)
			{
				return null;
			}
			return new ManifestResourceInfo(containingAssembly, containingFileName, (ResourceLocation)manifestResourceInfo);
		}

		// Token: 0x060042F6 RID: 17142
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetLocation(RuntimeAssembly assembly, StringHandleOnStack retString);

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060042F7 RID: 17143 RVA: 0x000F709C File Offset: 0x000F529C
		public override string Location
		{
			[SecuritySafeCritical]
			get
			{
				string text = null;
				RuntimeAssembly.GetLocation(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref text));
				if (text != null)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
				}
				return text;
			}
		}

		// Token: 0x060042F8 RID: 17144
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetImageRuntimeVersion(RuntimeAssembly assembly, StringHandleOnStack retString);

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060042F9 RID: 17145 RVA: 0x000F70D0 File Offset: 0x000F52D0
		[ComVisible(false)]
		public override string ImageRuntimeVersion
		{
			[SecuritySafeCritical]
			get
			{
				string result = null;
				RuntimeAssembly.GetImageRuntimeVersion(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref result));
				return result;
			}
		}

		// Token: 0x060042FA RID: 17146
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsGlobalAssemblyCache(RuntimeAssembly assembly);

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060042FB RID: 17147 RVA: 0x000F70F2 File Offset: 0x000F52F2
		public override bool GlobalAssemblyCache
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeAssembly.IsGlobalAssemblyCache(this.GetNativeHandle());
			}
		}

		// Token: 0x060042FC RID: 17148
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern long GetHostContext(RuntimeAssembly assembly);

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060042FD RID: 17149 RVA: 0x000F70FF File Offset: 0x000F52FF
		public override long HostContext
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeAssembly.GetHostContext(this.GetNativeHandle());
			}
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x000F710C File Offset: 0x000F530C
		private static string VerifyCodeBase(string codebase)
		{
			if (codebase == null)
			{
				return null;
			}
			int length = codebase.Length;
			if (length == 0)
			{
				return null;
			}
			int num = codebase.IndexOf(':');
			if (num != -1 && num + 2 < length && (codebase[num + 1] == '/' || codebase[num + 1] == '\\') && (codebase[num + 2] == '/' || codebase[num + 2] == '\\'))
			{
				return codebase;
			}
			if (length > 2 && codebase[0] == '\\' && codebase[1] == '\\')
			{
				return "file://" + codebase;
			}
			return "file:///" + Path.GetFullPathInternal(codebase);
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x000F71AC File Offset: 0x000F53AC
		[SecurityCritical]
		internal Stream GetManifestResourceStream(Type type, string name, bool skipSecurityCheck, ref StackCrawlMark stackMark)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (type == null)
			{
				if (name == null)
				{
					throw new ArgumentNullException("type");
				}
			}
			else
			{
				string @namespace = type.Namespace;
				if (@namespace != null)
				{
					stringBuilder.Append(@namespace);
					if (name != null)
					{
						stringBuilder.Append(Type.Delimiter);
					}
				}
			}
			if (name != null)
			{
				stringBuilder.Append(name);
			}
			return this.GetManifestResourceStream(stringBuilder.ToString(), ref stackMark, skipSecurityCheck);
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06004300 RID: 17152 RVA: 0x000F7211 File Offset: 0x000F5411
		internal bool IsStrongNameVerified
		{
			[SecurityCritical]
			get
			{
				return RuntimeAssembly.GetIsStrongNameVerified(this.GetNativeHandle());
			}
		}

		// Token: 0x06004301 RID: 17153
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool GetIsStrongNameVerified(RuntimeAssembly assembly);

		// Token: 0x06004302 RID: 17154
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern byte* GetResource(RuntimeAssembly assembly, string resourceName, out ulong length, StackCrawlMarkHandle stackMark, bool skipSecurityCheck);

		// Token: 0x06004303 RID: 17155 RVA: 0x000F7220 File Offset: 0x000F5420
		[SecurityCritical]
		internal unsafe Stream GetManifestResourceStream(string name, ref StackCrawlMark stackMark, bool skipSecurityCheck)
		{
			ulong num = 0UL;
			byte* resource = RuntimeAssembly.GetResource(this.GetNativeHandle(), name, out num, JitHelpers.GetStackCrawlMarkHandle(ref stackMark), skipSecurityCheck);
			if (resource == null)
			{
				return null;
			}
			if (num > 9223372036854775807UL)
			{
				throw new NotImplementedException(Environment.GetResourceString("NotImplemented_ResourcesLongerThan2^63"));
			}
			return new UnmanagedMemoryStream(resource, (long)num, (long)num, FileAccess.Read, true);
		}

		// Token: 0x06004304 RID: 17156
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetVersion(RuntimeAssembly assembly, out int majVer, out int minVer, out int buildNum, out int revNum);

		// Token: 0x06004305 RID: 17157 RVA: 0x000F7274 File Offset: 0x000F5474
		[SecurityCritical]
		internal Version GetVersion()
		{
			int major;
			int minor;
			int build;
			int revision;
			RuntimeAssembly.GetVersion(this.GetNativeHandle(), out major, out minor, out build, out revision);
			return new Version(major, minor, build, revision);
		}

		// Token: 0x06004306 RID: 17158
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetLocale(RuntimeAssembly assembly, StringHandleOnStack retString);

		// Token: 0x06004307 RID: 17159 RVA: 0x000F72A0 File Offset: 0x000F54A0
		[SecurityCritical]
		internal CultureInfo GetLocale()
		{
			string text = null;
			RuntimeAssembly.GetLocale(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref text));
			if (text == null)
			{
				return CultureInfo.InvariantCulture;
			}
			return new CultureInfo(text);
		}

		// Token: 0x06004308 RID: 17160
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FCallIsDynamic(RuntimeAssembly assembly);

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06004309 RID: 17161 RVA: 0x000F72D0 File Offset: 0x000F54D0
		public override bool IsDynamic
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeAssembly.FCallIsDynamic(this.GetNativeHandle());
			}
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x000F72E0 File Offset: 0x000F54E0
		[SecurityCritical]
		private void VerifyCodeBaseDiscovery(string codeBase)
		{
			if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				return;
			}
			if (codeBase != null && string.Compare(codeBase, 0, "file:", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
			{
				URLString urlstring = new URLString(codeBase, true);
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, urlstring.GetFileName()).Demand();
			}
		}

		// Token: 0x0600430B RID: 17163
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetSimpleName(RuntimeAssembly assembly, StringHandleOnStack retSimpleName);

		// Token: 0x0600430C RID: 17164 RVA: 0x000F7324 File Offset: 0x000F5524
		[SecuritySafeCritical]
		internal string GetSimpleName()
		{
			string result = null;
			RuntimeAssembly.GetSimpleName(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref result));
			return result;
		}

		// Token: 0x0600430D RID: 17165
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern AssemblyHashAlgorithm GetHashAlgorithm(RuntimeAssembly assembly);

		// Token: 0x0600430E RID: 17166 RVA: 0x000F7346 File Offset: 0x000F5546
		[SecurityCritical]
		private AssemblyHashAlgorithm GetHashAlgorithm()
		{
			return RuntimeAssembly.GetHashAlgorithm(this.GetNativeHandle());
		}

		// Token: 0x0600430F RID: 17167
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern AssemblyNameFlags GetFlags(RuntimeAssembly assembly);

		// Token: 0x06004310 RID: 17168 RVA: 0x000F7353 File Offset: 0x000F5553
		[SecurityCritical]
		private AssemblyNameFlags GetFlags()
		{
			return RuntimeAssembly.GetFlags(this.GetNativeHandle());
		}

		// Token: 0x06004311 RID: 17169
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetRawBytes(RuntimeAssembly assembly, ObjectHandleOnStack retRawBytes);

		// Token: 0x06004312 RID: 17170 RVA: 0x000F7360 File Offset: 0x000F5560
		[SecuritySafeCritical]
		internal byte[] GetRawBytes()
		{
			byte[] result = null;
			RuntimeAssembly.GetRawBytes(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<byte[]>(ref result));
			return result;
		}

		// Token: 0x06004313 RID: 17171
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetPublicKey(RuntimeAssembly assembly, ObjectHandleOnStack retPublicKey);

		// Token: 0x06004314 RID: 17172 RVA: 0x000F7384 File Offset: 0x000F5584
		[SecurityCritical]
		internal byte[] GetPublicKey()
		{
			byte[] result = null;
			RuntimeAssembly.GetPublicKey(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<byte[]>(ref result));
			return result;
		}

		// Token: 0x06004315 RID: 17173
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetGrantSet(RuntimeAssembly assembly, ObjectHandleOnStack granted, ObjectHandleOnStack denied);

		// Token: 0x06004316 RID: 17174 RVA: 0x000F73A8 File Offset: 0x000F55A8
		[SecurityCritical]
		internal void GetGrantSet(out PermissionSet newGrant, out PermissionSet newDenied)
		{
			PermissionSet permissionSet = null;
			PermissionSet permissionSet2 = null;
			RuntimeAssembly.GetGrantSet(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref permissionSet), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref permissionSet2));
			newGrant = permissionSet;
			newDenied = permissionSet2;
		}

		// Token: 0x06004317 RID: 17175
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsAllSecurityCritical(RuntimeAssembly assembly);

		// Token: 0x06004318 RID: 17176 RVA: 0x000F73D8 File Offset: 0x000F55D8
		[SecuritySafeCritical]
		internal bool IsAllSecurityCritical()
		{
			return RuntimeAssembly.IsAllSecurityCritical(this.GetNativeHandle());
		}

		// Token: 0x06004319 RID: 17177
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsAllSecuritySafeCritical(RuntimeAssembly assembly);

		// Token: 0x0600431A RID: 17178 RVA: 0x000F73E5 File Offset: 0x000F55E5
		[SecuritySafeCritical]
		internal bool IsAllSecuritySafeCritical()
		{
			return RuntimeAssembly.IsAllSecuritySafeCritical(this.GetNativeHandle());
		}

		// Token: 0x0600431B RID: 17179
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsAllPublicAreaSecuritySafeCritical(RuntimeAssembly assembly);

		// Token: 0x0600431C RID: 17180 RVA: 0x000F73F2 File Offset: 0x000F55F2
		[SecuritySafeCritical]
		internal bool IsAllPublicAreaSecuritySafeCritical()
		{
			return RuntimeAssembly.IsAllPublicAreaSecuritySafeCritical(this.GetNativeHandle());
		}

		// Token: 0x0600431D RID: 17181
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsAllSecurityTransparent(RuntimeAssembly assembly);

		// Token: 0x0600431E RID: 17182 RVA: 0x000F73FF File Offset: 0x000F55FF
		[SecuritySafeCritical]
		internal bool IsAllSecurityTransparent()
		{
			return RuntimeAssembly.IsAllSecurityTransparent(this.GetNativeHandle());
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x000F740C File Offset: 0x000F560C
		[SecurityCritical]
		private static void DemandPermission(string codeBase, bool havePath, int demandFlag)
		{
			FileIOPermissionAccess access = FileIOPermissionAccess.PathDiscovery;
			switch (demandFlag)
			{
			case 1:
				access = FileIOPermissionAccess.Read;
				break;
			case 2:
				access = (FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery);
				break;
			case 3:
			{
				IPermission permission = RuntimeAssembly.CreateWebPermission(AssemblyName.EscapeCodeBase(codeBase));
				permission.Demand();
				return;
			}
			}
			if (!havePath)
			{
				URLString urlstring = new URLString(codeBase, true);
				codeBase = urlstring.GetFileName();
			}
			codeBase = Path.GetFullPathInternal(codeBase);
			new FileIOPermission(access, codeBase).Demand();
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x000F7478 File Offset: 0x000F5678
		private static IPermission CreateWebPermission(string codeBase)
		{
			Assembly assembly = Assembly.Load("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			Type type = assembly.GetType("System.Net.NetworkAccess", true);
			IPermission permission = null;
			if (type.IsEnum && type.IsVisible)
			{
				object[] array = new object[2];
				array[0] = (Enum)Enum.Parse(type, "Connect", true);
				if (array[0] != null)
				{
					array[1] = codeBase;
					type = assembly.GetType("System.Net.WebPermission", true);
					if (type.IsVisible)
					{
						permission = (IPermission)Activator.CreateInstance(type, array);
					}
				}
			}
			if (permission == null)
			{
				throw new InvalidOperationException();
			}
			return permission;
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x000F7500 File Offset: 0x000F5700
		[SecurityCritical]
		private RuntimeModule OnModuleResolveEvent(string moduleName)
		{
			ModuleResolveEventHandler moduleResolve = this._ModuleResolve;
			if (moduleResolve == null)
			{
				return null;
			}
			Delegate[] invocationList = moduleResolve.GetInvocationList();
			int num = invocationList.Length;
			for (int i = 0; i < num; i++)
			{
				RuntimeModule runtimeModule = (RuntimeModule)((ModuleResolveEventHandler)invocationList[i])(this, new ResolveEventArgs(moduleName, this));
				if (runtimeModule != null)
				{
					return runtimeModule;
				}
			}
			return null;
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x000F755C File Offset: 0x000F575C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.InternalGetSatelliteAssembly(culture, null, ref stackCrawlMark);
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x000F7578 File Offset: 0x000F5778
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.InternalGetSatelliteAssembly(culture, version, ref stackCrawlMark);
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x000F7594 File Offset: 0x000F5794
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal Assembly InternalGetSatelliteAssembly(CultureInfo culture, Version version, ref StackCrawlMark stackMark)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			string name = this.GetSimpleName() + ".resources";
			return this.InternalGetSatelliteAssembly(name, culture, version, true, ref stackMark);
		}

		// Token: 0x06004325 RID: 17189
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UseRelativeBindForSatellites();

		// Token: 0x06004326 RID: 17190 RVA: 0x000F75CC File Offset: 0x000F57CC
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal RuntimeAssembly InternalGetSatelliteAssembly(string name, CultureInfo culture, Version version, bool throwOnFileNotFound, ref StackCrawlMark stackMark)
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.SetPublicKey(this.GetPublicKey());
			assemblyName.Flags = (this.GetFlags() | AssemblyNameFlags.PublicKey);
			if (version == null)
			{
				assemblyName.Version = this.GetVersion();
			}
			else
			{
				assemblyName.Version = version;
			}
			assemblyName.CultureInfo = culture;
			assemblyName.Name = name;
			RuntimeAssembly runtimeAssembly = null;
			bool flag = AppDomain.IsAppXDesignMode();
			bool flag2 = false;
			if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				flag2 = (this.IsFrameworkAssembly() || RuntimeAssembly.UseRelativeBindForSatellites());
			}
			if (flag || flag2)
			{
				if (this.GlobalAssemblyCache)
				{
					ArrayList arrayList = new ArrayList();
					bool flag3 = false;
					try
					{
						Fusion.ReadCache(arrayList, assemblyName.FullName, 2U);
					}
					catch (Exception ex)
					{
						if (ex.IsTransient)
						{
							throw;
						}
						if (!AppDomain.IsAppXModel())
						{
							flag3 = true;
						}
					}
					if (arrayList.Count > 0 || flag3)
					{
						runtimeAssembly = RuntimeAssembly.nLoad(assemblyName, null, null, this, ref stackMark, IntPtr.Zero, throwOnFileNotFound, false, false);
					}
				}
				else
				{
					string codeBase = this.CodeBase;
					if (codeBase != null && string.Compare(codeBase, 0, "file:", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
					{
						runtimeAssembly = this.InternalProbeForSatelliteAssemblyNextToParentAssembly(assemblyName, name, codeBase, culture, throwOnFileNotFound, flag, ref stackMark);
						if (runtimeAssembly != null && !RuntimeAssembly.IsSimplyNamed(assemblyName))
						{
							AssemblyName name2 = runtimeAssembly.GetName();
							if (!AssemblyName.ReferenceMatchesDefinitionInternal(assemblyName, name2, false))
							{
								runtimeAssembly = null;
							}
						}
					}
					else if (!flag)
					{
						runtimeAssembly = RuntimeAssembly.nLoad(assemblyName, null, null, this, ref stackMark, IntPtr.Zero, throwOnFileNotFound, false, false);
					}
				}
			}
			else
			{
				runtimeAssembly = RuntimeAssembly.nLoad(assemblyName, null, null, this, ref stackMark, IntPtr.Zero, throwOnFileNotFound, false, false);
			}
			if (runtimeAssembly == this || (runtimeAssembly == null && throwOnFileNotFound))
			{
				throw new FileNotFoundException(string.Format(culture, Environment.GetResourceString("IO.FileNotFound_FileName"), assemblyName.Name));
			}
			return runtimeAssembly;
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x000F7780 File Offset: 0x000F5980
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private RuntimeAssembly InternalProbeForSatelliteAssemblyNextToParentAssembly(AssemblyName an, string name, string codeBase, CultureInfo culture, bool throwOnFileNotFound, bool useLoadFile, ref StackCrawlMark stackMark)
		{
			RuntimeAssembly runtimeAssembly = null;
			string text = null;
			if (useLoadFile)
			{
				text = this.Location;
			}
			FileNotFoundException ex = null;
			StringBuilder stringBuilder = new StringBuilder(useLoadFile ? text : codeBase, 0, useLoadFile ? (text.LastIndexOf('\\') + 1) : (codeBase.LastIndexOf('/') + 1), 260);
			stringBuilder.Append(an.CultureInfo.Name);
			stringBuilder.Append(useLoadFile ? '\\' : '/');
			stringBuilder.Append(name);
			stringBuilder.Append(".DLL");
			string text2 = stringBuilder.ToString();
			AssemblyName assemblyName = null;
			if (!useLoadFile)
			{
				assemblyName = new AssemblyName();
				assemblyName.CodeBase = text2;
			}
			try
			{
				try
				{
					runtimeAssembly = (useLoadFile ? RuntimeAssembly.nLoadFile(text2, null) : RuntimeAssembly.nLoad(assemblyName, text2, null, this, ref stackMark, IntPtr.Zero, throwOnFileNotFound, false, false));
				}
				catch (FileNotFoundException)
				{
					ex = new FileNotFoundException(string.Format(culture, Environment.GetResourceString("IO.FileNotFound_FileName"), text2), text2);
					runtimeAssembly = null;
				}
				if (runtimeAssembly == null)
				{
					stringBuilder.Remove(stringBuilder.Length - 4, 4);
					stringBuilder.Append(".EXE");
					text2 = stringBuilder.ToString();
					if (!useLoadFile)
					{
						assemblyName.CodeBase = text2;
					}
					try
					{
						runtimeAssembly = (useLoadFile ? RuntimeAssembly.nLoadFile(text2, null) : RuntimeAssembly.nLoad(assemblyName, text2, null, this, ref stackMark, IntPtr.Zero, false, false, false));
					}
					catch (FileNotFoundException)
					{
						runtimeAssembly = null;
					}
					if (runtimeAssembly == null && throwOnFileNotFound)
					{
						throw ex;
					}
				}
			}
			catch (DirectoryNotFoundException)
			{
				if (throwOnFileNotFound)
				{
					throw;
				}
				runtimeAssembly = null;
			}
			return runtimeAssembly;
		}

		// Token: 0x06004328 RID: 17192
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeAssembly nLoadFile(string path, Evidence evidence);

		// Token: 0x06004329 RID: 17193
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeAssembly nLoadImage(byte[] rawAssembly, byte[] rawSymbolStore, Evidence evidence, ref StackCrawlMark stackMark, bool fIntrospection, bool fSkipIntegrityCheck, SecurityContextSource securityContextSource);

		// Token: 0x0600432A RID: 17194
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetModules(RuntimeAssembly assembly, bool loadIfNotFound, bool getResourceModules, ObjectHandleOnStack retModuleHandles);

		// Token: 0x0600432B RID: 17195 RVA: 0x000F7914 File Offset: 0x000F5B14
		[SecuritySafeCritical]
		private RuntimeModule[] GetModulesInternal(bool loadIfNotFound, bool getResourceModules)
		{
			RuntimeModule[] result = null;
			RuntimeAssembly.GetModules(this.GetNativeHandle(), loadIfNotFound, getResourceModules, JitHelpers.GetObjectHandleOnStack<RuntimeModule[]>(ref result));
			return result;
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x000F7938 File Offset: 0x000F5B38
		public override Module[] GetModules(bool getResourceModules)
		{
			return this.GetModulesInternal(true, getResourceModules);
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x000F7942 File Offset: 0x000F5B42
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.GetModulesInternal(false, getResourceModules);
		}

		// Token: 0x0600432E RID: 17198
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeModule GetManifestModule(RuntimeAssembly assembly);

		// Token: 0x0600432F RID: 17199
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool AptcaCheck(RuntimeAssembly targetAssembly, RuntimeAssembly sourceAssembly);

		// Token: 0x06004330 RID: 17200
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetToken(RuntimeAssembly assembly);

		// Token: 0x04001B3A RID: 6970
		private const uint COR_E_LOADING_REFERENCE_ASSEMBLY = 2148733016U;

		// Token: 0x04001B3C RID: 6972
		private string m_fullname;

		// Token: 0x04001B3D RID: 6973
		private object m_syncRoot;

		// Token: 0x04001B3E RID: 6974
		private IntPtr m_assembly;

		// Token: 0x04001B3F RID: 6975
		private RuntimeAssembly.ASSEMBLY_FLAGS m_flags;

		// Token: 0x04001B40 RID: 6976
		private const string s_localFilePrefix = "file:";

		// Token: 0x04001B41 RID: 6977
		private static string[] s_unsafeFrameworkAssemblyNames = new string[]
		{
			"System.Reflection.Context",
			"Microsoft.VisualBasic"
		};

		// Token: 0x02000C03 RID: 3075
		private enum ASSEMBLY_FLAGS : uint
		{
			// Token: 0x0400364A RID: 13898
			ASSEMBLY_FLAGS_UNKNOWN,
			// Token: 0x0400364B RID: 13899
			ASSEMBLY_FLAGS_INITIALIZED = 16777216U,
			// Token: 0x0400364C RID: 13900
			ASSEMBLY_FLAGS_FRAMEWORK = 33554432U,
			// Token: 0x0400364D RID: 13901
			ASSEMBLY_FLAGS_SAFE_REFLECTION = 67108864U,
			// Token: 0x0400364E RID: 13902
			ASSEMBLY_FLAGS_TOKEN_MASK = 16777215U
		}
	}
}
