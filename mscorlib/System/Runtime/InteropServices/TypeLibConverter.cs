using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.TCEAdapterGen;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200094C RID: 2380
	[Guid("F1C3BF79-C3E4-11d3-88E7-00902754C43A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public sealed class TypeLibConverter : ITypeLibConverter
	{
		// Token: 0x0600614A RID: 24906 RVA: 0x0014C51C File Offset: 0x0014A71C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, int flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, bool unsafeInterfaces)
		{
			return this.ConvertTypeLibToAssembly(typeLib, asmFileName, unsafeInterfaces ? TypeLibImporterFlags.UnsafeInterfaces : TypeLibImporterFlags.None, notifySink, publicKey, keyPair, null, null);
		}

		// Token: 0x0600614B RID: 24907 RVA: 0x0014C544 File Offset: 0x0014A744
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, string asmNamespace, Version asmVersion)
		{
			if (typeLib == null)
			{
				throw new ArgumentNullException("typeLib");
			}
			if (asmFileName == null)
			{
				throw new ArgumentNullException("asmFileName");
			}
			if (notifySink == null)
			{
				throw new ArgumentNullException("notifySink");
			}
			if (string.Empty.Equals(asmFileName))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileName"), "asmFileName");
			}
			if (asmFileName.Length > 260)
			{
				throw new ArgumentException(Environment.GetResourceString("IO.PathTooLong"), asmFileName);
			}
			if ((flags & TypeLibImporterFlags.PrimaryInteropAssembly) != TypeLibImporterFlags.None && publicKey == null && keyPair == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_PIAMustBeStrongNamed"));
			}
			ArrayList arrayList = null;
			AssemblyNameFlags asmNameFlags = AssemblyNameFlags.None;
			AssemblyName assemblyNameFromTypelib = TypeLibConverter.GetAssemblyNameFromTypelib(typeLib, asmFileName, publicKey, keyPair, asmVersion, asmNameFlags);
			AssemblyBuilder assemblyBuilder = TypeLibConverter.CreateAssemblyForTypeLib(typeLib, asmFileName, assemblyNameFromTypelib, (flags & TypeLibImporterFlags.PrimaryInteropAssembly) > TypeLibImporterFlags.None, (flags & TypeLibImporterFlags.ReflectionOnlyLoading) > TypeLibImporterFlags.None, (flags & TypeLibImporterFlags.NoDefineVersionResource) > TypeLibImporterFlags.None);
			string fileName = Path.GetFileName(asmFileName);
			ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(fileName, fileName);
			if (asmNamespace == null)
			{
				asmNamespace = assemblyNameFromTypelib.Name;
			}
			TypeLibConverter.TypeResolveHandler typeResolveHandler = new TypeLibConverter.TypeResolveHandler(moduleBuilder, notifySink);
			AppDomain domain = Thread.GetDomain();
			ResolveEventHandler value = new ResolveEventHandler(typeResolveHandler.ResolveEvent);
			ResolveEventHandler value2 = new ResolveEventHandler(typeResolveHandler.ResolveAsmEvent);
			ResolveEventHandler value3 = new ResolveEventHandler(typeResolveHandler.ResolveROAsmEvent);
			domain.TypeResolve += value;
			domain.AssemblyResolve += value2;
			domain.ReflectionOnlyAssemblyResolve += value3;
			TypeLibConverter.nConvertTypeLibToMetadata(typeLib, assemblyBuilder.InternalAssembly, moduleBuilder.InternalModule, asmNamespace, flags, typeResolveHandler, out arrayList);
			TypeLibConverter.UpdateComTypesInAssembly(assemblyBuilder, moduleBuilder);
			if (arrayList.Count > 0)
			{
				new TCEAdapterGenerator().Process(moduleBuilder, arrayList);
			}
			domain.TypeResolve -= value;
			domain.AssemblyResolve -= value2;
			domain.ReflectionOnlyAssemblyResolve -= value3;
			return assemblyBuilder;
		}

		// Token: 0x0600614C RID: 24908 RVA: 0x0014C6E0 File Offset: 0x0014A8E0
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public object ConvertAssemblyToTypeLib(Assembly assembly, string strTypeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink)
		{
			AssemblyBuilder assemblyBuilder = assembly as AssemblyBuilder;
			RuntimeAssembly assembly2;
			if (assemblyBuilder != null)
			{
				assembly2 = assemblyBuilder.InternalAssembly;
			}
			else
			{
				assembly2 = (assembly as RuntimeAssembly);
			}
			return TypeLibConverter.nConvertAssemblyToTypeLib(assembly2, strTypeLibName, flags, notifySink);
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x0014C718 File Offset: 0x0014A918
		public bool GetPrimaryInteropAssembly(Guid g, int major, int minor, int lcid, out string asmName, out string asmCodeBase)
		{
			string name = "{" + g.ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
			string name2 = major.ToString("x", CultureInfo.InvariantCulture) + "." + minor.ToString("x", CultureInfo.InvariantCulture);
			asmName = null;
			asmCodeBase = null;
			using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("TypeLib", false))
			{
				if (registryKey != null)
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(name))
					{
						if (registryKey2 != null)
						{
							using (RegistryKey registryKey3 = registryKey2.OpenSubKey(name2, false))
							{
								if (registryKey3 != null)
								{
									asmName = (string)registryKey3.GetValue("PrimaryInteropAssemblyName");
									asmCodeBase = (string)registryKey3.GetValue("PrimaryInteropAssemblyCodeBase");
								}
							}
						}
					}
				}
			}
			return asmName != null;
		}

		// Token: 0x0600614E RID: 24910 RVA: 0x0014C82C File Offset: 0x0014AA2C
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static AssemblyBuilder CreateAssemblyForTypeLib(object typeLib, string asmFileName, AssemblyName asmName, bool bPrimaryInteropAssembly, bool bReflectionOnly, bool bNoDefineVersionResource)
		{
			AppDomain domain = Thread.GetDomain();
			string text = null;
			if (asmFileName != null)
			{
				text = Path.GetDirectoryName(asmFileName);
				if (string.IsNullOrEmpty(text))
				{
					text = null;
				}
			}
			AssemblyBuilderAccess access;
			if (bReflectionOnly)
			{
				access = AssemblyBuilderAccess.ReflectionOnly;
			}
			else
			{
				access = AssemblyBuilderAccess.RunAndSave;
			}
			List<CustomAttributeBuilder> list = new List<CustomAttributeBuilder>();
			ConstructorInfo constructor = typeof(SecurityRulesAttribute).GetConstructor(new Type[]
			{
				typeof(SecurityRuleSet)
			});
			CustomAttributeBuilder item = new CustomAttributeBuilder(constructor, new object[]
			{
				SecurityRuleSet.Level2
			});
			list.Add(item);
			AssemblyBuilder assemblyBuilder = domain.DefineDynamicAssembly(asmName, access, text, false, list);
			TypeLibConverter.SetGuidAttributeOnAssembly(assemblyBuilder, typeLib);
			TypeLibConverter.SetImportedFromTypeLibAttrOnAssembly(assemblyBuilder, typeLib);
			if (bNoDefineVersionResource)
			{
				TypeLibConverter.SetTypeLibVersionAttribute(assemblyBuilder, typeLib);
			}
			else
			{
				TypeLibConverter.SetVersionInformation(assemblyBuilder, typeLib, asmName);
			}
			if (bPrimaryInteropAssembly)
			{
				TypeLibConverter.SetPIAAttributeOnAssembly(assemblyBuilder, typeLib);
			}
			return assemblyBuilder;
		}

		// Token: 0x0600614F RID: 24911 RVA: 0x0014C8E8 File Offset: 0x0014AAE8
		[SecurityCritical]
		internal static AssemblyName GetAssemblyNameFromTypelib(object typeLib, string asmFileName, byte[] publicKey, StrongNameKeyPair keyPair, Version asmVersion, AssemblyNameFlags asmNameFlags)
		{
			string text = null;
			string text2 = null;
			int num = 0;
			string text3 = null;
			ITypeLib typeLib2 = (ITypeLib)typeLib;
			typeLib2.GetDocumentation(-1, out text, out text2, out num, out text3);
			if (asmFileName == null)
			{
				asmFileName = text;
			}
			else
			{
				string fileName = Path.GetFileName(asmFileName);
				string extension = Path.GetExtension(asmFileName);
				if (!".dll".Equals(extension, StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileExtension"));
				}
				asmFileName = fileName.Substring(0, fileName.Length - ".dll".Length);
			}
			if (asmVersion == null)
			{
				int major;
				int minor;
				Marshal.GetTypeLibVersion(typeLib2, out major, out minor);
				asmVersion = new Version(major, minor, 0, 0);
			}
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Init(asmFileName, publicKey, null, asmVersion, null, AssemblyHashAlgorithm.None, AssemblyVersionCompatibility.SameMachine, null, asmNameFlags, keyPair);
			return assemblyName;
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x0014C9AC File Offset: 0x0014ABAC
		private static void UpdateComTypesInAssembly(AssemblyBuilder asmBldr, ModuleBuilder modBldr)
		{
			AssemblyBuilderData assemblyData = asmBldr.m_assemblyData;
			Type[] types = modBldr.GetTypes();
			int num = types.Length;
			for (int i = 0; i < num; i++)
			{
				assemblyData.AddPublicComType(types[i]);
			}
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x0014C9E0 File Offset: 0x0014ABE0
		[SecurityCritical]
		private static void SetGuidAttributeOnAssembly(AssemblyBuilder asmBldr, object typeLib)
		{
			Type[] types = new Type[]
			{
				typeof(string)
			};
			ConstructorInfo constructor = typeof(GuidAttribute).GetConstructor(types);
			object[] constructorArgs = new object[]
			{
				Marshal.GetTypeLibGuid((ITypeLib)typeLib).ToString()
			};
			CustomAttributeBuilder customAttribute = new CustomAttributeBuilder(constructor, constructorArgs);
			asmBldr.SetCustomAttribute(customAttribute);
		}

		// Token: 0x06006152 RID: 24914 RVA: 0x0014CA48 File Offset: 0x0014AC48
		[SecurityCritical]
		private static void SetImportedFromTypeLibAttrOnAssembly(AssemblyBuilder asmBldr, object typeLib)
		{
			Type[] types = new Type[]
			{
				typeof(string)
			};
			ConstructorInfo constructor = typeof(ImportedFromTypeLibAttribute).GetConstructor(types);
			string typeLibName = Marshal.GetTypeLibName((ITypeLib)typeLib);
			object[] constructorArgs = new object[]
			{
				typeLibName
			};
			CustomAttributeBuilder customAttribute = new CustomAttributeBuilder(constructor, constructorArgs);
			asmBldr.SetCustomAttribute(customAttribute);
		}

		// Token: 0x06006153 RID: 24915 RVA: 0x0014CAA4 File Offset: 0x0014ACA4
		[SecurityCritical]
		private static void SetTypeLibVersionAttribute(AssemblyBuilder asmBldr, object typeLib)
		{
			Type[] types = new Type[]
			{
				typeof(int),
				typeof(int)
			};
			ConstructorInfo constructor = typeof(TypeLibVersionAttribute).GetConstructor(types);
			int num;
			int num2;
			Marshal.GetTypeLibVersion((ITypeLib)typeLib, out num, out num2);
			object[] constructorArgs = new object[]
			{
				num,
				num2
			};
			CustomAttributeBuilder customAttribute = new CustomAttributeBuilder(constructor, constructorArgs);
			asmBldr.SetCustomAttribute(customAttribute);
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x0014CB20 File Offset: 0x0014AD20
		[SecurityCritical]
		private static void SetVersionInformation(AssemblyBuilder asmBldr, object typeLib, AssemblyName asmName)
		{
			string arg = null;
			string text = null;
			int num = 0;
			string text2 = null;
			ITypeLib typeLib2 = (ITypeLib)typeLib;
			typeLib2.GetDocumentation(-1, out arg, out text, out num, out text2);
			string product = string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("TypeLibConverter_ImportedTypeLibProductName"), arg);
			asmBldr.DefineVersionInfoResource(product, asmName.Version.ToString(), null, null, null);
			TypeLibConverter.SetTypeLibVersionAttribute(asmBldr, typeLib);
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x0014CB84 File Offset: 0x0014AD84
		[SecurityCritical]
		private static void SetPIAAttributeOnAssembly(AssemblyBuilder asmBldr, object typeLib)
		{
			IntPtr zero = IntPtr.Zero;
			ITypeLib typeLib2 = (ITypeLib)typeLib;
			int num = 0;
			int num2 = 0;
			Type[] types = new Type[]
			{
				typeof(int),
				typeof(int)
			};
			ConstructorInfo constructor = typeof(PrimaryInteropAssemblyAttribute).GetConstructor(types);
			try
			{
				typeLib2.GetLibAttr(out zero);
				TYPELIBATTR typelibattr = (TYPELIBATTR)Marshal.PtrToStructure(zero, typeof(TYPELIBATTR));
				num = (int)typelibattr.wMajorVerNum;
				num2 = (int)typelibattr.wMinorVerNum;
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					typeLib2.ReleaseTLibAttr(zero);
				}
			}
			object[] constructorArgs = new object[]
			{
				num,
				num2
			};
			CustomAttributeBuilder customAttribute = new CustomAttributeBuilder(constructor, constructorArgs);
			asmBldr.SetCustomAttribute(customAttribute);
		}

		// Token: 0x06006156 RID: 24918
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nConvertTypeLibToMetadata(object typeLib, RuntimeAssembly asmBldr, RuntimeModule modBldr, string nameSpace, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, out ArrayList eventItfInfoList);

		// Token: 0x06006157 RID: 24919
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object nConvertAssemblyToTypeLib(RuntimeAssembly assembly, string strTypeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink);

		// Token: 0x06006158 RID: 24920
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void LoadInMemoryTypeByName(RuntimeModule module, string className);

		// Token: 0x04002B23 RID: 11043
		private const string s_strTypeLibAssemblyTitlePrefix = "TypeLib ";

		// Token: 0x04002B24 RID: 11044
		private const string s_strTypeLibAssemblyDescPrefix = "Assembly generated from typelib ";

		// Token: 0x04002B25 RID: 11045
		private const int MAX_NAMESPACE_LENGTH = 1024;

		// Token: 0x02000C64 RID: 3172
		private class TypeResolveHandler : ITypeLibImporterNotifySink
		{
			// Token: 0x06006FEE RID: 28654 RVA: 0x0018074A File Offset: 0x0017E94A
			public TypeResolveHandler(ModuleBuilder mod, ITypeLibImporterNotifySink userSink)
			{
				this.m_Module = mod;
				this.m_UserSink = userSink;
			}

			// Token: 0x06006FEF RID: 28655 RVA: 0x0018076B File Offset: 0x0017E96B
			public void ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg)
			{
				this.m_UserSink.ReportEvent(eventKind, eventCode, eventMsg);
			}

			// Token: 0x06006FF0 RID: 28656 RVA: 0x0018077C File Offset: 0x0017E97C
			public Assembly ResolveRef(object typeLib)
			{
				Assembly assembly = this.m_UserSink.ResolveRef(typeLib);
				if (assembly == null)
				{
					throw new ArgumentNullException();
				}
				RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
				if (runtimeAssembly == null)
				{
					AssemblyBuilder assemblyBuilder = assembly as AssemblyBuilder;
					if (assemblyBuilder != null)
					{
						runtimeAssembly = assemblyBuilder.InternalAssembly;
					}
				}
				if (runtimeAssembly == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
				}
				this.m_AsmList.Add(runtimeAssembly);
				return runtimeAssembly;
			}

			// Token: 0x06006FF1 RID: 28657 RVA: 0x001807F4 File Offset: 0x0017E9F4
			[SecurityCritical]
			public Assembly ResolveEvent(object sender, ResolveEventArgs args)
			{
				try
				{
					TypeLibConverter.LoadInMemoryTypeByName(this.m_Module.GetNativeHandle(), args.Name);
					return this.m_Module.Assembly;
				}
				catch (TypeLoadException ex)
				{
					if (ex.ResourceId != -2146233054)
					{
						throw;
					}
				}
				foreach (RuntimeAssembly runtimeAssembly in this.m_AsmList)
				{
					try
					{
						runtimeAssembly.GetType(args.Name, true, false);
						return runtimeAssembly;
					}
					catch (TypeLoadException ex2)
					{
						if (ex2._HResult != -2146233054)
						{
							throw;
						}
					}
				}
				return null;
			}

			// Token: 0x06006FF2 RID: 28658 RVA: 0x001808B8 File Offset: 0x0017EAB8
			public Assembly ResolveAsmEvent(object sender, ResolveEventArgs args)
			{
				foreach (RuntimeAssembly runtimeAssembly in this.m_AsmList)
				{
					if (string.Compare(runtimeAssembly.FullName, args.Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return runtimeAssembly;
					}
				}
				return null;
			}

			// Token: 0x06006FF3 RID: 28659 RVA: 0x00180920 File Offset: 0x0017EB20
			public Assembly ResolveROAsmEvent(object sender, ResolveEventArgs args)
			{
				foreach (RuntimeAssembly runtimeAssembly in this.m_AsmList)
				{
					if (string.Compare(runtimeAssembly.FullName, args.Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return runtimeAssembly;
					}
				}
				string assemblyString = AppDomain.CurrentDomain.ApplyPolicy(args.Name);
				return Assembly.ReflectionOnlyLoad(assemblyString);
			}

			// Token: 0x04003770 RID: 14192
			private ModuleBuilder m_Module;

			// Token: 0x04003771 RID: 14193
			private ITypeLibImporterNotifySink m_UserSink;

			// Token: 0x04003772 RID: 14194
			private List<RuntimeAssembly> m_AsmList = new List<RuntimeAssembly>();
		}
	}
}
