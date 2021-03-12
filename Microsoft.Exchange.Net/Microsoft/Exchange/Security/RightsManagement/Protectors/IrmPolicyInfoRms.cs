using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement.Protectors
{
	// Token: 0x020009AC RID: 2476
	internal sealed class IrmPolicyInfoRms : I_IrmPolicyInfoRMS, IDisposeTrackable, IDisposable
	{
		// Token: 0x060035B5 RID: 13749 RVA: 0x000879AC File Offset: 0x00085BAC
		public IrmPolicyInfoRms(BindLicenseForDecrypt bindLicenseDelegate)
		{
			if (bindLicenseDelegate == null)
			{
				throw new ArgumentNullException("bindLicenseDelegate");
			}
			this.disposeTracker = this.GetDisposeTracker();
			this.encryptorHandle = SafeRightsManagementHandle.InvalidHandle;
			this.decryptorHandle = null;
			this.issuanceLicense = null;
			this.ownDecryptorHandle = true;
			this.bindLicenseDelegate = bindLicenseDelegate;
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x00087A00 File Offset: 0x00085C00
		public IrmPolicyInfoRms(SafeRightsManagementHandle decryptorHandle, string issuanceLicense)
		{
			if (decryptorHandle == null)
			{
				throw new ArgumentNullException("decryptorHandle");
			}
			if (decryptorHandle.IsInvalid)
			{
				throw new ArgumentException("handle is invalid", "decryptorHandle");
			}
			if (string.IsNullOrEmpty(issuanceLicense))
			{
				throw new ArgumentNullException("issuanceLicense");
			}
			this.disposeTracker = this.GetDisposeTracker();
			this.encryptorHandle = SafeRightsManagementHandle.InvalidHandle;
			this.decryptorHandle = decryptorHandle;
			this.ownDecryptorHandle = false;
			this.issuanceLicense = issuanceLicense;
			this.bindLicenseDelegate = null;
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x00087A80 File Offset: 0x00085C80
		public IrmPolicyInfoRms(SafeRightsManagementHandle encryptorHandle, SafeRightsManagementHandle decryptorHandle, string issuanceLicense)
		{
			if (encryptorHandle == null)
			{
				throw new ArgumentNullException("encryptorHandle");
			}
			if (decryptorHandle == null)
			{
				throw new ArgumentNullException("decryptorHandle");
			}
			if (string.IsNullOrEmpty(issuanceLicense))
			{
				throw new ArgumentNullException("issuanceLicense");
			}
			if (encryptorHandle.IsInvalid)
			{
				throw new ArgumentException("handle is invalid", "encryptorHandle");
			}
			if (decryptorHandle.IsInvalid)
			{
				throw new ArgumentException("handle is invalid", "decryptorHandle");
			}
			this.disposeTracker = this.GetDisposeTracker();
			this.encryptorHandle = encryptorHandle;
			this.decryptorHandle = decryptorHandle;
			this.issuanceLicense = issuanceLicense;
			this.ownDecryptorHandle = false;
			this.bindLicenseDelegate = null;
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x00087B24 File Offset: 0x00085D24
		public int HrGetICrypt(out object piic)
		{
			if (this.irmCrypt == null)
			{
				int errorCode = NativeMethods.HrCreateIrmCrypt(this.encryptorHandle, this.decryptorHandle, out this.irmCrypt);
				Marshal.ThrowExceptionForHR(errorCode);
			}
			piic = this.irmCrypt;
			return 0;
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x00087B60 File Offset: 0x00085D60
		public int HrGetSignedIL(out string pbstrIL)
		{
			pbstrIL = this.issuanceLicense;
			return 0;
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x00087B6B File Offset: 0x00085D6B
		public int HrGetServerId(out string pbstrServerId)
		{
			pbstrServerId = string.Empty;
			return 0;
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x00087B75 File Offset: 0x00085D75
		public int HrGetEULs(IntPtr rgbstrEUL, IntPtr rgbstrId, out uint pcbEULs)
		{
			pcbEULs = 0U;
			return 0;
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x00087B7C File Offset: 0x00085D7C
		public int HrSetSignedIL(string protectedDocumentIssuanceLicense)
		{
			if (string.IsNullOrEmpty(protectedDocumentIssuanceLicense))
			{
				return -2147024809;
			}
			if (this.bindLicenseDelegate != null)
			{
				try
				{
					this.decryptorHandle = this.bindLicenseDelegate(protectedDocumentIssuanceLicense);
				}
				catch (RightsManagementException ex)
				{
					return (int)ex.FailureCode;
				}
				return 0;
			}
			if (string.IsNullOrEmpty(this.issuanceLicense))
			{
				return -2147418113;
			}
			string a;
			string a2;
			try
			{
				DrmClientUtils.GetContentIdFromLicense(this.issuanceLicense, out a, out a2);
			}
			catch (RightsManagementException ex2)
			{
				return (int)ex2.FailureCode;
			}
			string b;
			string b2;
			try
			{
				DrmClientUtils.GetContentIdFromLicense(protectedDocumentIssuanceLicense, out b, out b2);
			}
			catch (RightsManagementException ex3)
			{
				return (int)ex3.FailureCode;
			}
			if (!string.Equals(a, b, StringComparison.OrdinalIgnoreCase) || !string.Equals(a2, b2, StringComparison.OrdinalIgnoreCase))
			{
				return -2147467259;
			}
			return 0;
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x00087C50 File Offset: 0x00085E50
		public int HrSetServerEUL(string bstrEUL)
		{
			return 0;
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x00087C53 File Offset: 0x00085E53
		public int HrGetRightsTemplate(out string pbstrRightsTemplate)
		{
			pbstrRightsTemplate = null;
			return -2147467263;
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x00087C5D File Offset: 0x00085E5D
		public int HrGetListGuid(out string pbstrListGuid)
		{
			pbstrListGuid = "00000000-0000-0000-0000-000000000000";
			return 0;
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x00087C67 File Offset: 0x00085E67
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<IrmPolicyInfoRms>(this);
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x00087C6F File Offset: 0x00085E6F
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x00087C84 File Offset: 0x00085E84
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x00087C94 File Offset: 0x00085E94
		private void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				if (this.irmCrypt != null)
				{
					Marshal.ReleaseComObject(this.irmCrypt);
					this.irmCrypt = null;
				}
				if (this.ownDecryptorHandle && this.decryptorHandle != null)
				{
					this.decryptorHandle.Close();
					this.decryptorHandle = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x04002DCA RID: 11722
		private readonly SafeRightsManagementHandle encryptorHandle;

		// Token: 0x04002DCB RID: 11723
		private readonly string issuanceLicense;

		// Token: 0x04002DCC RID: 11724
		private SafeRightsManagementHandle decryptorHandle;

		// Token: 0x04002DCD RID: 11725
		private bool ownDecryptorHandle;

		// Token: 0x04002DCE RID: 11726
		private DisposeTracker disposeTracker;

		// Token: 0x04002DCF RID: 11727
		private bool disposed;

		// Token: 0x04002DD0 RID: 11728
		private object irmCrypt;

		// Token: 0x04002DD1 RID: 11729
		private BindLicenseForDecrypt bindLicenseDelegate;
	}
}
