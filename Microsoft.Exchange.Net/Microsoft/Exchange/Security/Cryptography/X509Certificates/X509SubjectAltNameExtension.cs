using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000AFC RID: 2812
	[ComVisible(false)]
	internal sealed class X509SubjectAltNameExtension : X509Extension
	{
		// Token: 0x06003C68 RID: 15464 RVA: 0x0009CC4C File Offset: 0x0009AE4C
		public X509SubjectAltNameExtension(IEnumerable<string> names, bool critical) : base(WellKnownOid.SubjectAltName, X509SubjectAltNameExtension.EncodeExtension(names), critical)
		{
		}

		// Token: 0x06003C69 RID: 15465 RVA: 0x0009CC60 File Offset: 0x0009AE60
		private X509SubjectAltNameExtension()
		{
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x0009CC68 File Offset: 0x0009AE68
		private X509SubjectAltNameExtension(Oid oid, byte[] rawData, bool critical) : base(oid, rawData, critical)
		{
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06003C6B RID: 15467 RVA: 0x0009CC73 File Offset: 0x0009AE73
		public IList<string> DnsNames
		{
			get
			{
				if (this.names == null)
				{
					this.DecodeExtension();
				}
				return this.names;
			}
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x0009CC89 File Offset: 0x0009AE89
		public static X509SubjectAltNameExtension Create(X509Extension source)
		{
			if (source == null)
			{
				return null;
			}
			return new X509SubjectAltNameExtension(source.Oid, source.RawData, source.Critical);
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x0009CCA7 File Offset: 0x0009AEA7
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x0009CCB0 File Offset: 0x0009AEB0
		private static byte[] EncodeExtension(IEnumerable<string> names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("name");
			}
			int num = 0;
			foreach (string value in names)
			{
				if (!string.IsNullOrEmpty(value))
				{
					num++;
				}
			}
			if (num == 0)
			{
				throw new ArgumentException(NetException.CollectionEmpty, "name");
			}
			uint num2 = (uint)(num * CapiNativeMethods.CertAltNameDnsEntry.MarshalSize);
			SafeHGlobalHandle safeHGlobalHandle = NativeMethods.AllocHGlobal((int)num2);
			IntPtr intPtr = safeHGlobalHandle.DangerousGetHandle();
			foreach (string text in names)
			{
				if (!string.IsNullOrEmpty(text))
				{
					Marshal.StructureToPtr(new CapiNativeMethods.CertAltNameDnsEntry
					{
						Type = CapiNativeMethods.CertAltNameType.DnsName,
						Name = text
					}, intPtr, false);
					intPtr = new IntPtr((long)intPtr + (long)CapiNativeMethods.CertAltNameDnsEntry.MarshalSize);
				}
			}
			CapiNativeMethods.CryptoApiBlob cryptoApiBlob = new CapiNativeMethods.CryptoApiBlob((uint)num, safeHGlobalHandle);
			byte[] array = null;
			num2 = 0U;
			try
			{
				if (!CapiNativeMethods.CryptEncodeObject(CapiNativeMethods.EncodeType.X509Asn | CapiNativeMethods.EncodeType.Pkcs7Asn, WellKnownOid.SubjectAltName.Value, ref cryptoApiBlob, array, ref num2))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				array = new byte[num2];
				if (!CapiNativeMethods.CryptEncodeObject(CapiNativeMethods.EncodeType.X509Asn | CapiNativeMethods.EncodeType.Pkcs7Asn, WellKnownOid.SubjectAltName.Value, ref cryptoApiBlob, array, ref num2))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				intPtr = safeHGlobalHandle.DangerousGetHandle();
				for (int i = 0; i < num; i++)
				{
					Marshal.DestroyStructure(intPtr, typeof(CapiNativeMethods.CertAltNameDnsEntry));
					intPtr = new IntPtr((long)intPtr + (long)CapiNativeMethods.CertAltNameDnsEntry.MarshalSize);
				}
			}
			safeHGlobalHandle.Close();
			return array;
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x0009CE80 File Offset: 0x0009B080
		private void DecodeExtension()
		{
			uint num = 0U;
			SafeHGlobalHandle safeHGlobalHandle = null;
			CapiNativeMethods.DecodeObject(base.Oid.Value, base.RawData, out safeHGlobalHandle, out num);
			CapiNativeMethods.CryptoApiBlob cryptoApiBlob = (CapiNativeMethods.CryptoApiBlob)Marshal.PtrToStructure(safeHGlobalHandle.DangerousGetHandle(), typeof(CapiNativeMethods.CryptoApiBlob));
			List<string> list = new List<string>((int)cryptoApiBlob.Count);
			IntPtr intPtr = cryptoApiBlob.DataPointer;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)cryptoApiBlob.Count))
			{
				CapiNativeMethods.CertAltNameType certAltNameType = (CapiNativeMethods.CertAltNameType)Marshal.ReadInt32(intPtr);
				if (certAltNameType == CapiNativeMethods.CertAltNameType.DnsName)
				{
					CapiNativeMethods.CertAltNameDnsEntry certAltNameDnsEntry = (CapiNativeMethods.CertAltNameDnsEntry)Marshal.PtrToStructure(intPtr, typeof(CapiNativeMethods.CertAltNameDnsEntry));
					if (certAltNameDnsEntry.Type == CapiNativeMethods.CertAltNameType.DnsName)
					{
						list.Add(certAltNameDnsEntry.Name);
					}
				}
				intPtr = (IntPtr)((long)intPtr + (long)Marshal.SizeOf(typeof(CapiNativeMethods.CertAltNameDnsEntry)));
				num2++;
			}
			this.names = list.AsReadOnly();
			safeHGlobalHandle.Dispose();
		}

		// Token: 0x04003537 RID: 13623
		private IList<string> names;
	}
}
