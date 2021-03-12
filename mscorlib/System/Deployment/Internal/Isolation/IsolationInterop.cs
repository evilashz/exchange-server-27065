using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000687 RID: 1671
	internal static class IsolationInterop
	{
		// Token: 0x06004EFD RID: 20221 RVA: 0x00118BEA File Offset: 0x00116DEA
		[SecuritySafeCritical]
		public static Store GetUserStore()
		{
			return new Store(IsolationInterop.GetUserStore(0U, IntPtr.Zero, ref IsolationInterop.IID_IStore) as IStore);
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06004EFE RID: 20222 RVA: 0x00118C08 File Offset: 0x00116E08
		public static IIdentityAuthority IdentityAuthority
		{
			[SecuritySafeCritical]
			get
			{
				if (IsolationInterop._idAuth == null)
				{
					object synchObject = IsolationInterop._synchObject;
					lock (synchObject)
					{
						if (IsolationInterop._idAuth == null)
						{
							IsolationInterop._idAuth = IsolationInterop.GetIdentityAuthority();
						}
					}
				}
				return IsolationInterop._idAuth;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06004EFF RID: 20223 RVA: 0x00118C68 File Offset: 0x00116E68
		public static IAppIdAuthority AppIdAuthority
		{
			[SecuritySafeCritical]
			get
			{
				if (IsolationInterop._appIdAuth == null)
				{
					object synchObject = IsolationInterop._synchObject;
					lock (synchObject)
					{
						if (IsolationInterop._appIdAuth == null)
						{
							IsolationInterop._appIdAuth = IsolationInterop.GetAppIdAuthority();
						}
					}
				}
				return IsolationInterop._appIdAuth;
			}
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x00118CC8 File Offset: 0x00116EC8
		[SecuritySafeCritical]
		internal static IActContext CreateActContext(IDefinitionAppId AppId)
		{
			IsolationInterop.CreateActContextParameters createActContextParameters;
			createActContextParameters.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParameters));
			createActContextParameters.Flags = 16U;
			createActContextParameters.CustomStoreList = IntPtr.Zero;
			createActContextParameters.CultureFallbackList = IntPtr.Zero;
			createActContextParameters.ProcessorArchitectureList = IntPtr.Zero;
			createActContextParameters.Source = IntPtr.Zero;
			createActContextParameters.ProcArch = 0;
			IsolationInterop.CreateActContextParametersSource createActContextParametersSource;
			createActContextParametersSource.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParametersSource));
			createActContextParametersSource.Flags = 0U;
			createActContextParametersSource.SourceType = 1U;
			createActContextParametersSource.Data = IntPtr.Zero;
			IsolationInterop.CreateActContextParametersSourceDefinitionAppid createActContextParametersSourceDefinitionAppid;
			createActContextParametersSourceDefinitionAppid.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParametersSourceDefinitionAppid));
			createActContextParametersSourceDefinitionAppid.Flags = 0U;
			createActContextParametersSourceDefinitionAppid.AppId = AppId;
			IActContext result;
			try
			{
				createActContextParametersSource.Data = createActContextParametersSourceDefinitionAppid.ToIntPtr();
				createActContextParameters.Source = createActContextParametersSource.ToIntPtr();
				result = (IsolationInterop.CreateActContext(ref createActContextParameters) as IActContext);
			}
			finally
			{
				if (createActContextParametersSource.Data != IntPtr.Zero)
				{
					IsolationInterop.CreateActContextParametersSourceDefinitionAppid.Destroy(createActContextParametersSource.Data);
					createActContextParametersSource.Data = IntPtr.Zero;
				}
				if (createActContextParameters.Source != IntPtr.Zero)
				{
					IsolationInterop.CreateActContextParametersSource.Destroy(createActContextParameters.Source);
					createActContextParameters.Source = IntPtr.Zero;
				}
			}
			return result;
		}

		// Token: 0x06004F01 RID: 20225
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object CreateActContext(ref IsolationInterop.CreateActContextParameters Params);

		// Token: 0x06004F02 RID: 20226
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object CreateCMSFromXml([In] byte[] buffer, [In] uint bufferSize, [In] IManifestParseErrorCallback Callback, [In] ref Guid riid);

		// Token: 0x06004F03 RID: 20227
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object ParseManifest([MarshalAs(UnmanagedType.LPWStr)] [In] string pszManifestPath, [In] IManifestParseErrorCallback pIManifestParseErrorCallback, [In] ref Guid riid);

		// Token: 0x06004F04 RID: 20228
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		private static extern object GetUserStore([In] uint Flags, [In] IntPtr hToken, [In] ref Guid riid);

		// Token: 0x06004F05 RID: 20229
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern IIdentityAuthority GetIdentityAuthority();

		// Token: 0x06004F06 RID: 20230
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern IAppIdAuthority GetAppIdAuthority();

		// Token: 0x06004F07 RID: 20231 RVA: 0x00118E14 File Offset: 0x00117014
		internal static Guid GetGuidOfType(Type type)
		{
			GuidAttribute guidAttribute = (GuidAttribute)Attribute.GetCustomAttribute(type, typeof(GuidAttribute), false);
			return new Guid(guidAttribute.Value);
		}

		// Token: 0x040021B8 RID: 8632
		private static object _synchObject = new object();

		// Token: 0x040021B9 RID: 8633
		private static volatile IIdentityAuthority _idAuth = null;

		// Token: 0x040021BA RID: 8634
		private static volatile IAppIdAuthority _appIdAuth = null;

		// Token: 0x040021BB RID: 8635
		public const string IsolationDllName = "clr.dll";

		// Token: 0x040021BC RID: 8636
		public static Guid IID_ICMS = IsolationInterop.GetGuidOfType(typeof(ICMS));

		// Token: 0x040021BD RID: 8637
		public static Guid IID_IDefinitionIdentity = IsolationInterop.GetGuidOfType(typeof(IDefinitionIdentity));

		// Token: 0x040021BE RID: 8638
		public static Guid IID_IManifestInformation = IsolationInterop.GetGuidOfType(typeof(IManifestInformation));

		// Token: 0x040021BF RID: 8639
		public static Guid IID_IEnumSTORE_ASSEMBLY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY));

		// Token: 0x040021C0 RID: 8640
		public static Guid IID_IEnumSTORE_ASSEMBLY_FILE = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));

		// Token: 0x040021C1 RID: 8641
		public static Guid IID_IEnumSTORE_CATEGORY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY));

		// Token: 0x040021C2 RID: 8642
		public static Guid IID_IEnumSTORE_CATEGORY_INSTANCE = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_INSTANCE));

		// Token: 0x040021C3 RID: 8643
		public static Guid IID_IEnumSTORE_DEPLOYMENT_METADATA = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_DEPLOYMENT_METADATA));

		// Token: 0x040021C4 RID: 8644
		public static Guid IID_IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY));

		// Token: 0x040021C5 RID: 8645
		public static Guid IID_IStore = IsolationInterop.GetGuidOfType(typeof(IStore));

		// Token: 0x040021C6 RID: 8646
		public static Guid GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING = new Guid("2ec93463-b0c3-45e1-8364-327e96aea856");

		// Token: 0x040021C7 RID: 8647
		public static Guid SXS_INSTALL_REFERENCE_SCHEME_SXS_STRONGNAME_SIGNED_PRIVATE_ASSEMBLY = new Guid("3ab20ac0-67e8-4512-8385-a487e35df3da");

		// Token: 0x02000C2B RID: 3115
		internal struct CreateActContextParameters
		{
			// Token: 0x040036CA RID: 14026
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040036CB RID: 14027
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040036CC RID: 14028
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr CustomStoreList;

			// Token: 0x040036CD RID: 14029
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr CultureFallbackList;

			// Token: 0x040036CE RID: 14030
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr ProcessorArchitectureList;

			// Token: 0x040036CF RID: 14031
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr Source;

			// Token: 0x040036D0 RID: 14032
			[MarshalAs(UnmanagedType.U2)]
			public ushort ProcArch;

			// Token: 0x02000CD8 RID: 3288
			[Flags]
			public enum CreateFlags
			{
				// Token: 0x0400387A RID: 14458
				Nothing = 0,
				// Token: 0x0400387B RID: 14459
				StoreListValid = 1,
				// Token: 0x0400387C RID: 14460
				CultureListValid = 2,
				// Token: 0x0400387D RID: 14461
				ProcessorFallbackListValid = 4,
				// Token: 0x0400387E RID: 14462
				ProcessorValid = 8,
				// Token: 0x0400387F RID: 14463
				SourceValid = 16,
				// Token: 0x04003880 RID: 14464
				IgnoreVisibility = 32
			}
		}

		// Token: 0x02000C2C RID: 3116
		internal struct CreateActContextParametersSource
		{
			// Token: 0x06006F4C RID: 28492 RVA: 0x0017E838 File Offset: 0x0017CA38
			[SecurityCritical]
			public IntPtr ToIntPtr()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf<IsolationInterop.CreateActContextParametersSource>(this));
				Marshal.StructureToPtr<IsolationInterop.CreateActContextParametersSource>(this, intPtr, false);
				return intPtr;
			}

			// Token: 0x06006F4D RID: 28493 RVA: 0x0017E864 File Offset: 0x0017CA64
			[SecurityCritical]
			public static void Destroy(IntPtr p)
			{
				Marshal.DestroyStructure(p, typeof(IsolationInterop.CreateActContextParametersSource));
				Marshal.FreeCoTaskMem(p);
			}

			// Token: 0x040036D1 RID: 14033
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040036D2 RID: 14034
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040036D3 RID: 14035
			[MarshalAs(UnmanagedType.U4)]
			public uint SourceType;

			// Token: 0x040036D4 RID: 14036
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr Data;

			// Token: 0x02000CD9 RID: 3289
			[Flags]
			public enum SourceFlags
			{
				// Token: 0x04003882 RID: 14466
				Definition = 1,
				// Token: 0x04003883 RID: 14467
				Reference = 2
			}
		}

		// Token: 0x02000C2D RID: 3117
		internal struct CreateActContextParametersSourceDefinitionAppid
		{
			// Token: 0x06006F4E RID: 28494 RVA: 0x0017E87C File Offset: 0x0017CA7C
			[SecurityCritical]
			public IntPtr ToIntPtr()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf<IsolationInterop.CreateActContextParametersSourceDefinitionAppid>(this));
				Marshal.StructureToPtr<IsolationInterop.CreateActContextParametersSourceDefinitionAppid>(this, intPtr, false);
				return intPtr;
			}

			// Token: 0x06006F4F RID: 28495 RVA: 0x0017E8A8 File Offset: 0x0017CAA8
			[SecurityCritical]
			public static void Destroy(IntPtr p)
			{
				Marshal.DestroyStructure(p, typeof(IsolationInterop.CreateActContextParametersSourceDefinitionAppid));
				Marshal.FreeCoTaskMem(p);
			}

			// Token: 0x040036D5 RID: 14037
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040036D6 RID: 14038
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040036D7 RID: 14039
			public IDefinitionAppId AppId;
		}
	}
}
