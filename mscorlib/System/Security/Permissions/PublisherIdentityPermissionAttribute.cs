using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002D1 RID: 721
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class PublisherIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060025CD RID: 9677 RVA: 0x00088478 File Offset: 0x00086678
		public PublisherIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
			this.m_x509cert = null;
			this.m_certFile = null;
			this.m_signedFile = null;
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x00088496 File Offset: 0x00086696
		// (set) Token: 0x060025CF RID: 9679 RVA: 0x0008849E File Offset: 0x0008669E
		public string X509Certificate
		{
			get
			{
				return this.m_x509cert;
			}
			set
			{
				this.m_x509cert = value;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060025D0 RID: 9680 RVA: 0x000884A7 File Offset: 0x000866A7
		// (set) Token: 0x060025D1 RID: 9681 RVA: 0x000884AF File Offset: 0x000866AF
		public string CertFile
		{
			get
			{
				return this.m_certFile;
			}
			set
			{
				this.m_certFile = value;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x000884B8 File Offset: 0x000866B8
		// (set) Token: 0x060025D3 RID: 9683 RVA: 0x000884C0 File Offset: 0x000866C0
		public string SignedFile
		{
			get
			{
				return this.m_signedFile;
			}
			set
			{
				this.m_signedFile = value;
			}
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000884CC File Offset: 0x000866CC
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new PublisherIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.m_x509cert != null)
			{
				return new PublisherIdentityPermission(new X509Certificate(Hex.DecodeHexString(this.m_x509cert)));
			}
			if (this.m_certFile != null)
			{
				return new PublisherIdentityPermission(System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(this.m_certFile));
			}
			if (this.m_signedFile != null)
			{
				return new PublisherIdentityPermission(System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromSignedFile(this.m_signedFile));
			}
			return new PublisherIdentityPermission(PermissionState.None);
		}

		// Token: 0x04000E50 RID: 3664
		private string m_x509cert;

		// Token: 0x04000E51 RID: 3665
		private string m_certFile;

		// Token: 0x04000E52 RID: 3666
		private string m_signedFile;
	}
}
