using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Policy
{
	// Token: 0x0200031C RID: 796
	internal sealed class AssemblyEvidenceFactory : IRuntimeEvidenceFactory
	{
		// Token: 0x060028A5 RID: 10405 RVA: 0x00095B6E File Offset: 0x00093D6E
		private AssemblyEvidenceFactory(RuntimeAssembly targetAssembly, PEFileEvidenceFactory peFileFactory)
		{
			this.m_targetAssembly = targetAssembly;
			this.m_peFileFactory = peFileFactory;
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060028A6 RID: 10406 RVA: 0x00095B84 File Offset: 0x00093D84
		internal SafePEFileHandle PEFile
		{
			[SecurityCritical]
			get
			{
				return this.m_peFileFactory.PEFile;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x00095B91 File Offset: 0x00093D91
		public IEvidenceFactory Target
		{
			get
			{
				return this.m_targetAssembly;
			}
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x00095B9C File Offset: 0x00093D9C
		public EvidenceBase GenerateEvidence(Type evidenceType)
		{
			EvidenceBase evidenceBase = this.m_peFileFactory.GenerateEvidence(evidenceType);
			if (evidenceBase != null)
			{
				return evidenceBase;
			}
			if (evidenceType == typeof(GacInstalled))
			{
				return this.GenerateGacEvidence();
			}
			if (evidenceType == typeof(Hash))
			{
				return this.GenerateHashEvidence();
			}
			if (evidenceType == typeof(PermissionRequestEvidence))
			{
				return this.GeneratePermissionRequestEvidence();
			}
			if (evidenceType == typeof(StrongName))
			{
				return this.GenerateStrongNameEvidence();
			}
			return null;
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x00095C20 File Offset: 0x00093E20
		private GacInstalled GenerateGacEvidence()
		{
			if (!this.m_targetAssembly.GlobalAssemblyCache)
			{
				return null;
			}
			this.m_peFileFactory.FireEvidenceGeneratedEvent(EvidenceTypeGenerated.Gac);
			return new GacInstalled();
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x00095C42 File Offset: 0x00093E42
		private Hash GenerateHashEvidence()
		{
			if (this.m_targetAssembly.IsDynamic)
			{
				return null;
			}
			this.m_peFileFactory.FireEvidenceGeneratedEvent(EvidenceTypeGenerated.Hash);
			return new Hash(this.m_targetAssembly);
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x00095C6C File Offset: 0x00093E6C
		[SecuritySafeCritical]
		private PermissionRequestEvidence GeneratePermissionRequestEvidence()
		{
			PermissionSet permissionSet = null;
			PermissionSet permissionSet2 = null;
			PermissionSet permissionSet3 = null;
			AssemblyEvidenceFactory.GetAssemblyPermissionRequests(this.m_targetAssembly.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref permissionSet), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref permissionSet2), JitHelpers.GetObjectHandleOnStack<PermissionSet>(ref permissionSet3));
			if (permissionSet != null || permissionSet2 != null || permissionSet3 != null)
			{
				return new PermissionRequestEvidence(permissionSet, permissionSet2, permissionSet3);
			}
			return null;
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x00095CB8 File Offset: 0x00093EB8
		[SecuritySafeCritical]
		private StrongName GenerateStrongNameEvidence()
		{
			byte[] array = null;
			string name = null;
			ushort major = 0;
			ushort minor = 0;
			ushort build = 0;
			ushort revision = 0;
			AssemblyEvidenceFactory.GetStrongNameInformation(this.m_targetAssembly.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<byte[]>(ref array), JitHelpers.GetStringHandleOnStack(ref name), out major, out minor, out build, out revision);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return new StrongName(new StrongNamePublicKeyBlob(array), name, new Version((int)major, (int)minor, (int)build, (int)revision), this.m_targetAssembly);
		}

		// Token: 0x060028AD RID: 10413
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetAssemblyPermissionRequests(RuntimeAssembly assembly, ObjectHandleOnStack retMinimumPermissions, ObjectHandleOnStack retOptionalPermissions, ObjectHandleOnStack retRefusedPermissions);

		// Token: 0x060028AE RID: 10414 RVA: 0x00095D1F File Offset: 0x00093F1F
		public IEnumerable<EvidenceBase> GetFactorySuppliedEvidence()
		{
			return this.m_peFileFactory.GetFactorySuppliedEvidence();
		}

		// Token: 0x060028AF RID: 10415
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetStrongNameInformation(RuntimeAssembly assembly, ObjectHandleOnStack retPublicKeyBlob, StringHandleOnStack retSimpleName, out ushort majorVersion, out ushort minorVersion, out ushort build, out ushort revision);

		// Token: 0x060028B0 RID: 10416 RVA: 0x00095D2C File Offset: 0x00093F2C
		[SecurityCritical]
		private static Evidence UpgradeSecurityIdentity(Evidence peFileEvidence, RuntimeAssembly targetAssembly)
		{
			peFileEvidence.Target = new AssemblyEvidenceFactory(targetAssembly, peFileEvidence.Target as PEFileEvidenceFactory);
			HostSecurityManager hostSecurityManager = AppDomain.CurrentDomain.HostSecurityManager;
			if ((hostSecurityManager.Flags & HostSecurityManagerOptions.HostAssemblyEvidence) == HostSecurityManagerOptions.HostAssemblyEvidence)
			{
				peFileEvidence = hostSecurityManager.ProvideAssemblyEvidence(targetAssembly, peFileEvidence);
				if (peFileEvidence == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Policy_NullHostEvidence", new object[]
					{
						hostSecurityManager.GetType().FullName,
						targetAssembly.FullName
					}));
				}
			}
			return peFileEvidence;
		}

		// Token: 0x0400106A RID: 4202
		private PEFileEvidenceFactory m_peFileFactory;

		// Token: 0x0400106B RID: 4203
		private RuntimeAssembly m_targetAssembly;
	}
}
