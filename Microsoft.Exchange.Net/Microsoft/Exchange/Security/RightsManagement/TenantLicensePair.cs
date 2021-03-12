using System;
using System.Xml;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000A27 RID: 2599
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TenantLicensePair : CachableItem
	{
		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x060038A2 RID: 14498 RVA: 0x0008FFCD File Offset: 0x0008E1CD
		public SafeRightsManagementHandle EnablingPrincipalRac
		{
			get
			{
				return this.enablingPrincipalRac;
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x060038A3 RID: 14499 RVA: 0x0008FFD5 File Offset: 0x0008E1D5
		public SafeRightsManagementHandle BoundLicenseClc
		{
			get
			{
				return this.boundLicenseClc;
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x060038A4 RID: 14500 RVA: 0x0008FFDD File Offset: 0x0008E1DD
		public bool IsCleanedUp
		{
			get
			{
				return this.isCleanedUp;
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x060038A5 RID: 14501 RVA: 0x0008FFE5 File Offset: 0x0008E1E5
		public override long ItemSize
		{
			get
			{
				return (long)this.size;
			}
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x0008FFF0 File Offset: 0x0008E1F0
		public TenantLicensePair(Guid tenantId, XmlNode[] rac, XmlNode clcNode, string racCertChain, string clcCertChain, DateTime racExpire, DateTime clcExpire, byte version, SafeRightsManagementEnvironmentHandle envHandle, SafeRightsManagementHandle libHandle)
		{
			if (rac == null || rac.Length == 0 || rac[0] == null)
			{
				throw new ArgumentNullException("rac");
			}
			if (envHandle == null)
			{
				throw new ArgumentNullException("envHandle");
			}
			if (libHandle == null)
			{
				throw new ArgumentNullException("libHandle");
			}
			if (string.IsNullOrEmpty(racCertChain))
			{
				racCertChain = DrmClientUtils.ConvertXmlNodeArrayToCertificateChain(rac);
			}
			this.isB2BEntry = string.IsNullOrEmpty(clcCertChain);
			if (!this.isB2BEntry && (clcNode == null || string.IsNullOrEmpty(clcNode.OuterXml)))
			{
				throw new ArgumentException("clcNode should not be null for a non B2B entry", "clcNode");
			}
			this.Rac = rac;
			this.RacExpire = racExpire;
			this.ClcExpire = clcExpire;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				SafeRightsManagementHandle disposable = null;
				SafeRightsManagementHandle safeRightsManagementHandle = DrmClientUtils.CreateEnablingPrincipal(racCertChain, envHandle, libHandle);
				disposeGuard.Add<SafeRightsManagementHandle>(safeRightsManagementHandle);
				if (this.isB2BEntry)
				{
					this.boundLicenseClc = null;
					this.size = racCertChain.Length * 2 + 8 + 16;
				}
				else
				{
					disposable = DrmClientUtils.CreateClcBoundLicense(safeRightsManagementHandle, envHandle, clcCertChain);
					disposeGuard.Add<SafeRightsManagementHandle>(disposable);
					this.size = racCertChain.Length * 2 + 16 + 16;
				}
				if (!this.isB2BEntry)
				{
					DrmClientUtils.ParseGic(rac[0].OuterXml, out this.racDistributionPointIntranet, out this.racDistributionPointExtranet);
					DrmClientUtils.ParseClc(clcNode.OuterXml, out this.clcDistributionPointIntranet, out this.clcDistributionPointExtranet);
					this.size += ((this.racDistributionPointIntranet != null) ? this.racDistributionPointIntranet.OriginalString.Length : 0) + ((this.clcDistributionPointIntranet != null) ? this.clcDistributionPointIntranet.OriginalString.Length : 0) + ((this.racDistributionPointExtranet != null) ? this.racDistributionPointExtranet.OriginalString.Length : 0) + ((this.clcDistributionPointExtranet != null) ? this.clcDistributionPointExtranet.OriginalString.Length : 0);
				}
				this.Version = version;
				this.size++;
				disposeGuard.Success();
				this.enablingPrincipalRac = safeRightsManagementHandle;
				this.boundLicenseClc = disposable;
				this.references = 1;
			}
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x0009023C File Offset: 0x0008E43C
		public bool HasConfigurationChanged(Uri serviceLocation, Uri publishLocation, byte currentVersion)
		{
			return this.Version != currentVersion || (!this.isB2BEntry && ((!TenantLicensePair.IsRmsUriMatch(this.racDistributionPointIntranet, serviceLocation) && !TenantLicensePair.IsRmsUriMatch(this.racDistributionPointExtranet, serviceLocation)) || (!TenantLicensePair.IsRmsUriMatch(this.clcDistributionPointIntranet, publishLocation) && !TenantLicensePair.IsRmsUriMatch(this.clcDistributionPointExtranet, publishLocation))));
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x0009029C File Offset: 0x0008E49C
		public void AddRef()
		{
			lock (this.syncRoot)
			{
				this.references++;
			}
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000902E4 File Offset: 0x0008E4E4
		public void Release()
		{
			lock (this.syncRoot)
			{
				if (this.references <= 0)
				{
					throw new InvalidOperationException("Release called without a corresponding AddRef.");
				}
				this.references--;
				if (this.references == 0)
				{
					this.CloseHandles();
				}
			}
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x00090350 File Offset: 0x0008E550
		private static bool IsRmsUriMatch(Uri uri1, Uri uri2)
		{
			return Uri.Compare(uri1, uri2, UriComponents.SchemeAndServer, UriFormat.UriEscaped, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x00090360 File Offset: 0x0008E560
		private void CloseHandles()
		{
			lock (this.syncRoot)
			{
				if (this.enablingPrincipalRac != null)
				{
					this.enablingPrincipalRac.Close();
					this.enablingPrincipalRac = null;
				}
				if (this.boundLicenseClc != null)
				{
					this.boundLicenseClc.Close();
					this.boundLicenseClc = null;
				}
				this.isCleanedUp = true;
			}
		}

		// Token: 0x04002FF3 RID: 12275
		public readonly XmlNode[] Rac;

		// Token: 0x04002FF4 RID: 12276
		public readonly DateTime RacExpire;

		// Token: 0x04002FF5 RID: 12277
		public readonly DateTime ClcExpire;

		// Token: 0x04002FF6 RID: 12278
		public readonly byte Version;

		// Token: 0x04002FF7 RID: 12279
		private readonly Uri racDistributionPointIntranet;

		// Token: 0x04002FF8 RID: 12280
		private readonly Uri clcDistributionPointIntranet;

		// Token: 0x04002FF9 RID: 12281
		private readonly Uri racDistributionPointExtranet;

		// Token: 0x04002FFA RID: 12282
		private readonly Uri clcDistributionPointExtranet;

		// Token: 0x04002FFB RID: 12283
		private readonly object syncRoot = new object();

		// Token: 0x04002FFC RID: 12284
		private SafeRightsManagementHandle enablingPrincipalRac;

		// Token: 0x04002FFD RID: 12285
		private SafeRightsManagementHandle boundLicenseClc;

		// Token: 0x04002FFE RID: 12286
		private int size;

		// Token: 0x04002FFF RID: 12287
		private int references;

		// Token: 0x04003000 RID: 12288
		private bool isCleanedUp;

		// Token: 0x04003001 RID: 12289
		private bool isB2BEntry;
	}
}
