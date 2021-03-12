using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Security.RightsManagement.StructuredStorage;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x0200098A RID: 2442
	internal sealed class DrmEmailMessageBinding : EncryptedEmailMessageBinding
	{
		// Token: 0x06003517 RID: 13591 RVA: 0x00086CD0 File Offset: 0x00084ED0
		public DrmEmailMessageBinding(string issuanceLicense, SafeRightsManagementHandle decryptorHandle)
		{
			if (string.IsNullOrEmpty(issuanceLicense))
			{
				throw new ArgumentNullException("issuanceLicense");
			}
			if (decryptorHandle == null)
			{
				throw new ArgumentNullException("decryptorHandle");
			}
			if (decryptorHandle.IsInvalid)
			{
				throw new ArgumentException("decryptorHandle is invalid");
			}
			this.encryptorHandle = SafeRightsManagementHandle.InvalidHandle;
			this.decryptorHandle = decryptorHandle;
			this.issuanceLicense = issuanceLicense;
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x00086D30 File Offset: 0x00084F30
		public DrmEmailMessageBinding(string issuanceLicense, SafeRightsManagementHandle encryptorHandle, SafeRightsManagementHandle decryptorHandle)
		{
			if (string.IsNullOrEmpty(issuanceLicense))
			{
				throw new ArgumentNullException("issuanceLicense");
			}
			if (encryptorHandle == null)
			{
				throw new ArgumentNullException("encryptorHandle");
			}
			if (encryptorHandle.IsInvalid)
			{
				throw new ArgumentException("encryptorHandle is invalid");
			}
			if (decryptorHandle == null)
			{
				throw new ArgumentNullException("decryptorHandle");
			}
			if (decryptorHandle.IsInvalid)
			{
				throw new ArgumentException("decryptorHandle is invalid");
			}
			this.issuanceLicense = issuanceLicense;
			this.encryptorHandle = encryptorHandle;
			this.decryptorHandle = decryptorHandle;
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x00086DAD File Offset: 0x00084FAD
		public SafeRightsManagementHandle EncryptorHandle
		{
			get
			{
				return this.encryptorHandle;
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x0600351A RID: 13594 RVA: 0x00086DB5 File Offset: 0x00084FB5
		public SafeRightsManagementHandle DecryptorHandle
		{
			get
			{
				return this.decryptorHandle;
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x00086DBD File Offset: 0x00084FBD
		public string IssuanceLicense
		{
			get
			{
				return this.issuanceLicense;
			}
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x00086DC8 File Offset: 0x00084FC8
		public override IStorage ConvertToEncryptedStorage(IStream stream, bool create)
		{
			IStorage result = null;
			int errorCode = SafeNativeMethods.WrapEncryptedStorage(stream, this.encryptorHandle, this.decryptorHandle, create, out result);
			Marshal.ThrowExceptionForHR(errorCode);
			return result;
		}

		// Token: 0x04002D03 RID: 11523
		private readonly SafeRightsManagementHandle encryptorHandle;

		// Token: 0x04002D04 RID: 11524
		private readonly SafeRightsManagementHandle decryptorHandle;

		// Token: 0x04002D05 RID: 11525
		private readonly string issuanceLicense;
	}
}
