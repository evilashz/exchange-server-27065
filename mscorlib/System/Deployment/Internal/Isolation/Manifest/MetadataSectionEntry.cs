using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006E3 RID: 1763
	[StructLayout(LayoutKind.Sequential)]
	internal class MetadataSectionEntry : IDisposable
	{
		// Token: 0x06004FDA RID: 20442 RVA: 0x001193A8 File Offset: 0x001175A8
		~MetadataSectionEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x001193D8 File Offset: 0x001175D8
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x001193E4 File Offset: 0x001175E4
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.ManifestHash != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.ManifestHash);
				this.ManifestHash = IntPtr.Zero;
			}
			if (this.MvidValue != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.MvidValue);
				this.MvidValue = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04002301 RID: 8961
		public uint SchemaVersion;

		// Token: 0x04002302 RID: 8962
		public uint ManifestFlags;

		// Token: 0x04002303 RID: 8963
		public uint UsagePatterns;

		// Token: 0x04002304 RID: 8964
		public IDefinitionIdentity CdfIdentity;

		// Token: 0x04002305 RID: 8965
		[MarshalAs(UnmanagedType.LPWStr)]
		public string LocalPath;

		// Token: 0x04002306 RID: 8966
		public uint HashAlgorithm;

		// Token: 0x04002307 RID: 8967
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr ManifestHash;

		// Token: 0x04002308 RID: 8968
		public uint ManifestHashSize;

		// Token: 0x04002309 RID: 8969
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ContentType;

		// Token: 0x0400230A RID: 8970
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RuntimeImageVersion;

		// Token: 0x0400230B RID: 8971
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr MvidValue;

		// Token: 0x0400230C RID: 8972
		public uint MvidValueSize;

		// Token: 0x0400230D RID: 8973
		public DescriptionMetadataEntry DescriptionData;

		// Token: 0x0400230E RID: 8974
		public DeploymentMetadataEntry DeploymentData;

		// Token: 0x0400230F RID: 8975
		public DependentOSMetadataEntry DependentOSData;

		// Token: 0x04002310 RID: 8976
		[MarshalAs(UnmanagedType.LPWStr)]
		public string defaultPermissionSetID;

		// Token: 0x04002311 RID: 8977
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RequestedExecutionLevel;

		// Token: 0x04002312 RID: 8978
		public bool RequestedExecutionLevelUIAccess;

		// Token: 0x04002313 RID: 8979
		public IReferenceIdentity ResourceTypeResourcesDependency;

		// Token: 0x04002314 RID: 8980
		public IReferenceIdentity ResourceTypeManifestResourcesDependency;

		// Token: 0x04002315 RID: 8981
		[MarshalAs(UnmanagedType.LPWStr)]
		public string KeyInfoElement;

		// Token: 0x04002316 RID: 8982
		public CompatibleFrameworksMetadataEntry CompatibleFrameworksData;
	}
}
