using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AF3 RID: 2803
	internal static class X509PartialCertificate
	{
		// Token: 0x06003C44 RID: 15428 RVA: 0x0009C730 File Offset: 0x0009A930
		public static byte[] Encode(X509Certificate2 certificate, bool useIssuerAsIdentier)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			byte[] array = CapiNativeMethods.CertPublicKeyInfo.Encode(certificate);
			byte[] serialNumberRawData = CapiNativeMethods.CertInfo.Create(certificate).GetSerialNumberRawData();
			ushort num = (ushort)(array.Length + X509PartialCertificate.HeaderSize);
			X509SubjectKeyIdentifierExtension x509SubjectKeyIdentifierExtension = null;
			X509PartialCertificate.CertificatePackageFlags certificatePackageFlags = X509PartialCertificate.CertificatePackageFlags.MiniCert;
			foreach (X509Extension x509Extension in certificate.Extensions)
			{
				if (string.Equals(x509Extension.Oid.Value, WellKnownOid.SubjectKeyIdentifier.Value, StringComparison.Ordinal))
				{
					x509SubjectKeyIdentifierExtension = (x509Extension as X509SubjectKeyIdentifierExtension);
					break;
				}
			}
			ushort num2;
			if (!useIssuerAsIdentier && x509SubjectKeyIdentifierExtension != null && x509SubjectKeyIdentifierExtension.RawData != null && x509SubjectKeyIdentifierExtension.RawData.Length != 0)
			{
				num2 = (ushort)(x509SubjectKeyIdentifierExtension.RawData.Length + X509PartialCertificate.HeaderSize);
				certificatePackageFlags |= X509PartialCertificate.CertificatePackageFlags.SubjectKeyIdentifier;
			}
			else
			{
				num2 = (ushort)(certificate.IssuerName.RawData.Length + serialNumberRawData.Length + 2 * X509PartialCertificate.HeaderSize);
			}
			ushort num3 = (ushort)((int)(num + num2) + 2 * X509PartialCertificate.HeaderSize);
			byte[] array2 = new byte[(int)num3];
			int num4 = 0;
			num4 += ExBitConverter.Write(num3, array2, num4);
			num4 += ExBitConverter.Write((ushort)certificatePackageFlags, array2, num4);
			X509PartialCertificate.CopyBlobToEncodedCert(array, (ushort)array.Length, array2, ref num4);
			if ((short)(certificatePackageFlags & X509PartialCertificate.CertificatePackageFlags.SubjectKeyIdentifier) == 2)
			{
				X509PartialCertificate.CopyBlobToEncodedCert(x509SubjectKeyIdentifierExtension.RawData, (ushort)x509SubjectKeyIdentifierExtension.RawData.Length, array2, ref num4);
			}
			else
			{
				X509PartialCertificate.CopyBlobToEncodedCert(certificate.IssuerName.RawData, (ushort)certificate.IssuerName.RawData.Length, array2, ref num4);
				X509PartialCertificate.CopyBlobToEncodedCert(serialNumberRawData, (ushort)serialNumberRawData.Length, array2, ref num4);
			}
			return array2;
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x0009C8B0 File Offset: 0x0009AAB0
		public static string GetEmailAdress(X509Certificate2 certificate)
		{
			string result;
			try
			{
				result = CapiNativeMethods.GetCertNameInfo(certificate, 0U, CapiNativeMethods.CertNameType.Email);
			}
			catch (CryptographicException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x0009C8E0 File Offset: 0x0009AAE0
		public static string GetDisplayName(X509Certificate2 certificate)
		{
			string result;
			try
			{
				result = CapiNativeMethods.GetCertNameInfo(certificate, 0U, CapiNativeMethods.CertNameType.Attr);
			}
			catch (CryptographicException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x0009C910 File Offset: 0x0009AB10
		private static void CopyBlobToEncodedCert(byte[] sourceBlob, ushort sourceBlobSize, byte[] destinationBlob, ref int destPosition)
		{
			destPosition += ExBitConverter.Write(sourceBlobSize, destinationBlob, destPosition);
			Array.Copy(sourceBlob, 0, destinationBlob, destPosition, (int)sourceBlobSize);
			destPosition += (int)sourceBlobSize;
		}

		// Token: 0x04003511 RID: 13585
		private static int HeaderSize = Marshal.SizeOf(typeof(ushort));

		// Token: 0x02000AF4 RID: 2804
		[Flags]
		internal enum CertificatePackageFlags : short
		{
			// Token: 0x04003513 RID: 13587
			None = 0,
			// Token: 0x04003514 RID: 13588
			MiniCert = 1,
			// Token: 0x04003515 RID: 13589
			SubjectKeyIdentifier = 2
		}
	}
}
