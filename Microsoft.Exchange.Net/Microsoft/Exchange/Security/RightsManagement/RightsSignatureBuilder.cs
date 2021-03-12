using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009B5 RID: 2485
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RightsSignatureBuilder : IDisposeTrackable, IDisposable
	{
		// Token: 0x060035E8 RID: 13800 RVA: 0x00088D98 File Offset: 0x00086F98
		public RightsSignatureBuilder(string useLicense, string publishLicense, SafeRightsManagementEnvironmentHandle environmentHandle, DisposableTenantLicensePair tenantLicenses)
		{
			if (string.IsNullOrEmpty(useLicense))
			{
				throw new ArgumentNullException("useLicense");
			}
			if (string.IsNullOrEmpty(publishLicense))
			{
				throw new ArgumentNullException("publishLicense");
			}
			if (environmentHandle == null)
			{
				throw new ArgumentNullException("environmentHandle");
			}
			if (tenantLicenses == null)
			{
				throw new ArgumentNullException("tenantLicenses");
			}
			this.publishLicense = publishLicense;
			this.useLicense = useLicense;
			this.environmentHandle = environmentHandle;
			this.tenantLicensePair = tenantLicenses.Clone();
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x00088E1C File Offset: 0x0008701C
		public byte[] Sign(ContentRight rights, ExDateTime expiryTime, SecurityIdentifier sid)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			this.EnsureInitialized();
			return DrmClientUtils.SignDRMProps(rights, expiryTime, sid, this.encryptor, this.decryptor);
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x00088E6C File Offset: 0x0008706C
		public SafeRightsManagementHandle GetDuplicateDecryptorHandle()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			if (this.decryptor == null)
			{
				throw new InvalidOperationException("Decryptor handle not created");
			}
			SafeRightsManagementHandle result;
			int hr = SafeNativeMethods.DRMDuplicateHandle(this.decryptor, out result);
			Errors.ThrowOnErrorCode(hr);
			return result;
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x00088EB5 File Offset: 0x000870B5
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RightsSignatureBuilder>(this);
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x00088EBD File Offset: 0x000870BD
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x00088ED4 File Offset: 0x000870D4
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.tenantLicensePair != null)
			{
				this.tenantLicensePair.Dispose();
				this.tenantLicensePair = null;
			}
			if (this.encryptor != null)
			{
				this.encryptor.Close();
				this.encryptor = null;
			}
			if (this.decryptor != null)
			{
				this.decryptor.Close();
				this.decryptor = null;
			}
			this.disposed = true;
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x00088F5C File Offset: 0x0008715C
		private void EnsureInitialized()
		{
			if (this.initialized)
			{
				return;
			}
			DrmClientUtils.BindUseLicense(this.useLicense, this.publishLicense, "EDIT", true, this.tenantLicensePair.EnablingPrincipalRac, this.environmentHandle, out this.encryptor, out this.decryptor);
			this.initialized = true;
		}

		// Token: 0x04002E60 RID: 11872
		private SafeRightsManagementHandle encryptor;

		// Token: 0x04002E61 RID: 11873
		private SafeRightsManagementHandle decryptor;

		// Token: 0x04002E62 RID: 11874
		private bool disposed;

		// Token: 0x04002E63 RID: 11875
		private DisposableTenantLicensePair tenantLicensePair;

		// Token: 0x04002E64 RID: 11876
		private SafeRightsManagementEnvironmentHandle environmentHandle;

		// Token: 0x04002E65 RID: 11877
		private string publishLicense;

		// Token: 0x04002E66 RID: 11878
		private string useLicense;

		// Token: 0x04002E67 RID: 11879
		private bool initialized;

		// Token: 0x04002E68 RID: 11880
		private DisposeTracker disposeTracker;
	}
}
