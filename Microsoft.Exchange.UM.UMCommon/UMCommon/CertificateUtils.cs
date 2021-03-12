using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Cryptography;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000023 RID: 35
	internal static class CertificateUtils
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00008383 File Offset: 0x00006583
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000838A File Offset: 0x0000658A
		internal static X509Certificate2 UMCertificate { get; set; }

		// Token: 0x06000218 RID: 536 RVA: 0x00008394 File Offset: 0x00006594
		internal static X509Certificate2 FindCertByThumbprint(string thumbprint)
		{
			X509Certificate2 result = null;
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.ReadOnly);
			try
			{
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
				result = ((x509Certificate2Collection.Count > 0) ? x509Certificate2Collection[0] : null);
			}
			finally
			{
				x509Store.Close();
			}
			return result;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000083F0 File Offset: 0x000065F0
		internal static bool TryFindCertificateByThumbprint(string thumbprint, out X509Certificate2 cert)
		{
			cert = null;
			try
			{
				cert = CertificateUtils.FindCertByThumbprint(thumbprint);
				return cert != null;
			}
			catch (CryptographicException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.UMCertificateTracer, 0, ex.ToString(), new object[0]);
			}
			catch (SecurityException ex2)
			{
				CallIdTracer.TraceError(ExTraceGlobals.UMCertificateTracer, 0, ex2.ToString(), new object[0]);
			}
			return false;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008470 File Offset: 0x00006670
		internal static X509Certificate2 GetCertificateByThumbprintOrServerCertificate(string thumbprint)
		{
			X509Certificate2 result = null;
			UMServer umserver;
			if (string.IsNullOrEmpty(thumbprint) && Utils.TryGetUMServerConfig(Utils.GetLocalHostFqdn(), out umserver))
			{
				thumbprint = umserver.UMCertificateThumbprint;
			}
			if (!string.IsNullOrEmpty(thumbprint))
			{
				CertificateUtils.TryFindCertificateByThumbprint(thumbprint, out result);
			}
			return result;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000084B0 File Offset: 0x000066B0
		internal static void ExportCertToDiskFile(X509Certificate2 cert, string filePath)
		{
			using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			{
				byte[] inArray = cert.Export(X509ContentType.Cert);
				string s = Convert.ToBase64String(inArray);
				byte[] bytes = Encoding.ASCII.GetBytes(s);
				fileStream.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008508 File Offset: 0x00006708
		internal static string GetSubjectFqdn(X509Certificate2 cert)
		{
			return CapiNativeMethods.GetCertNameInfo(cert, 0U, CapiNativeMethods.CertNameType.Attr);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008512 File Offset: 0x00006712
		internal static bool IsSelfSignedCertificate(X509Certificate2 cert)
		{
			return TlsCertificateInfo.IsSelfSignedCertificate(cert);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000851C File Offset: 0x0000671C
		internal static bool IsExpired(X509Certificate2 cert)
		{
			ExDateTime dt = new ExDateTime(ExTimeZone.CurrentTimeZone, cert.NotAfter);
			return ExDateTime.Compare(dt, ExDateTime.Now) <= 0;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000854C File Offset: 0x0000674C
		internal static TimeSpan TimeToExpire(X509Certificate2 cert)
		{
			ValidateArgument.NotNull(cert, "cert");
			ExDateTime exDateTime = new ExDateTime(ExTimeZone.CurrentTimeZone, cert.NotAfter);
			return exDateTime.Subtract(ExDateTime.Now);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008582 File Offset: 0x00006782
		internal static void CopyCertToRootStore(X509Certificate2 cert)
		{
			TlsCertificateInfo.TryInstallCertificateInTrustedRootCA(cert);
		}
	}
}
