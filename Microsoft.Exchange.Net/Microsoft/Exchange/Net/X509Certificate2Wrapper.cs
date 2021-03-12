using System;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000CAB RID: 3243
	internal class X509Certificate2Wrapper : IX509Certificate2, IEquatable<IX509Certificate2>
	{
		// Token: 0x06004752 RID: 18258 RVA: 0x000C01BA File Offset: 0x000BE3BA
		public X509Certificate2Wrapper(X509Certificate2 certificate)
		{
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			this.certificate = certificate;
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06004753 RID: 18259 RVA: 0x000C01D4 File Offset: 0x000BE3D4
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x000C01DC File Offset: 0x000BE3DC
		public override bool Equals(object other)
		{
			return this.certificate.Equals(other as X509Certificate2Wrapper);
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x000C01EF File Offset: 0x000BE3EF
		public bool Equals(IX509Certificate2 other)
		{
			return this.certificate.Equals(other.Certificate);
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x000C0202 File Offset: 0x000BE402
		public override int GetHashCode()
		{
			return this.certificate.GetHashCode();
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06004757 RID: 18263 RVA: 0x000C020F File Offset: 0x000BE40F
		public IntPtr Handle
		{
			get
			{
				return this.certificate.Handle;
			}
		}

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06004758 RID: 18264 RVA: 0x000C021C File Offset: 0x000BE41C
		public string Issuer
		{
			get
			{
				return this.certificate.Issuer;
			}
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06004759 RID: 18265 RVA: 0x000C0229 File Offset: 0x000BE429
		public string Subject
		{
			get
			{
				return this.certificate.Subject;
			}
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x000C0236 File Offset: 0x000BE436
		public byte[] Export(X509ContentType contentType)
		{
			return this.certificate.Export(contentType);
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x000C0244 File Offset: 0x000BE444
		public byte[] Export(X509ContentType contentType, SecureString password)
		{
			return this.certificate.Export(contentType, password);
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x000C0253 File Offset: 0x000BE453
		public byte[] Export(X509ContentType contentType, string password)
		{
			return this.certificate.Export(contentType, password);
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x000C0262 File Offset: 0x000BE462
		public byte[] GetCertHash()
		{
			return this.certificate.GetCertHash();
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x000C026F File Offset: 0x000BE46F
		public string GetCertHashString()
		{
			return this.certificate.GetCertHashString();
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x000C027C File Offset: 0x000BE47C
		public string GetEffectiveDateString()
		{
			return this.certificate.GetEffectiveDateString();
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x000C0289 File Offset: 0x000BE489
		public string GetExpirationDateString()
		{
			return this.certificate.GetExpirationDateString();
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x000C0296 File Offset: 0x000BE496
		public string GetFormat()
		{
			return this.certificate.GetFormat();
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x000C02A3 File Offset: 0x000BE4A3
		public string GetKeyAlgorithm()
		{
			return this.certificate.GetKeyAlgorithm();
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x000C02B0 File Offset: 0x000BE4B0
		public byte[] GetKeyAlgorithmParameters()
		{
			return this.certificate.GetKeyAlgorithmParameters();
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x000C02BD File Offset: 0x000BE4BD
		public string GetKeyAlgorithmParametersString()
		{
			return this.certificate.GetKeyAlgorithmParametersString();
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x000C02CA File Offset: 0x000BE4CA
		public byte[] GetPublicKey()
		{
			return this.certificate.GetPublicKey();
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x000C02D7 File Offset: 0x000BE4D7
		public string GetPublicKeyString()
		{
			return this.certificate.GetPublicKeyString();
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x000C02E4 File Offset: 0x000BE4E4
		public byte[] GetRawCertData()
		{
			return this.certificate.GetRawCertData();
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x000C02F1 File Offset: 0x000BE4F1
		public string GetRawCertDataString()
		{
			return this.certificate.GetRawCertDataString();
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x000C02FE File Offset: 0x000BE4FE
		public byte[] GetSerialNumber()
		{
			return this.certificate.GetSerialNumber();
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x000C030B File Offset: 0x000BE50B
		public string GetSerialNumberString()
		{
			return this.certificate.GetSerialNumberString();
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x000C0318 File Offset: 0x000BE518
		public void Import(byte[] rawData)
		{
			this.certificate.Import(rawData);
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x000C0326 File Offset: 0x000BE526
		public void Import(string fileName)
		{
			this.certificate.Import(fileName);
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x000C0334 File Offset: 0x000BE534
		public void Import(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.certificate.Import(rawData, password, keyStorageFlags);
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x000C0344 File Offset: 0x000BE544
		public void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.certificate.Import(rawData, password, keyStorageFlags);
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x000C0354 File Offset: 0x000BE554
		public void Import(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.certificate.Import(fileName, password, keyStorageFlags);
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x000C0364 File Offset: 0x000BE564
		public void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.certificate.Import(fileName, password, keyStorageFlags);
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x000C0374 File Offset: 0x000BE574
		public void Reset()
		{
			this.certificate.Reset();
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x000C0381 File Offset: 0x000BE581
		public string ToString(bool fVerbose)
		{
			return this.certificate.ToString(fVerbose);
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06004773 RID: 18291 RVA: 0x000C038F File Offset: 0x000BE58F
		// (set) Token: 0x06004774 RID: 18292 RVA: 0x000C039C File Offset: 0x000BE59C
		public bool Archived
		{
			get
			{
				return this.certificate.Archived;
			}
			set
			{
				this.certificate.Archived = value;
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06004775 RID: 18293 RVA: 0x000C03AA File Offset: 0x000BE5AA
		public X509ExtensionCollection Extensions
		{
			get
			{
				return this.certificate.Extensions;
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06004776 RID: 18294 RVA: 0x000C03B7 File Offset: 0x000BE5B7
		// (set) Token: 0x06004777 RID: 18295 RVA: 0x000C03C4 File Offset: 0x000BE5C4
		public string FriendlyName
		{
			get
			{
				return this.certificate.FriendlyName;
			}
			set
			{
				this.certificate.FriendlyName = value;
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06004778 RID: 18296 RVA: 0x000C03D2 File Offset: 0x000BE5D2
		public bool HasPrivateKey
		{
			get
			{
				return this.certificate.HasPrivateKey;
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06004779 RID: 18297 RVA: 0x000C03DF File Offset: 0x000BE5DF
		public X500DistinguishedName IssuerName
		{
			get
			{
				return this.certificate.IssuerName;
			}
		}

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x0600477A RID: 18298 RVA: 0x000C03EC File Offset: 0x000BE5EC
		public DateTime NotAfter
		{
			get
			{
				return this.certificate.NotAfter;
			}
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x0600477B RID: 18299 RVA: 0x000C03F9 File Offset: 0x000BE5F9
		public DateTime NotBefore
		{
			get
			{
				return this.certificate.NotBefore;
			}
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x0600477C RID: 18300 RVA: 0x000C0406 File Offset: 0x000BE606
		// (set) Token: 0x0600477D RID: 18301 RVA: 0x000C0413 File Offset: 0x000BE613
		public AsymmetricAlgorithm PrivateKey
		{
			get
			{
				return this.certificate.PrivateKey;
			}
			set
			{
				this.certificate.PrivateKey = value;
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x0600477E RID: 18302 RVA: 0x000C0421 File Offset: 0x000BE621
		public PublicKey PublicKey
		{
			get
			{
				return this.certificate.PublicKey;
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x0600477F RID: 18303 RVA: 0x000C042E File Offset: 0x000BE62E
		public byte[] RawData
		{
			get
			{
				return this.certificate.RawData;
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06004780 RID: 18304 RVA: 0x000C043B File Offset: 0x000BE63B
		public string SerialNumber
		{
			get
			{
				return this.certificate.SerialNumber;
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06004781 RID: 18305 RVA: 0x000C0448 File Offset: 0x000BE648
		public Oid SignatureAlgorithm
		{
			get
			{
				return this.certificate.SignatureAlgorithm;
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06004782 RID: 18306 RVA: 0x000C0455 File Offset: 0x000BE655
		public X500DistinguishedName SubjectName
		{
			get
			{
				return this.certificate.SubjectName;
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06004783 RID: 18307 RVA: 0x000C0462 File Offset: 0x000BE662
		public string Thumbprint
		{
			get
			{
				return this.certificate.Thumbprint;
			}
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06004784 RID: 18308 RVA: 0x000C046F File Offset: 0x000BE66F
		public int Version
		{
			get
			{
				return this.certificate.Version;
			}
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x000C047C File Offset: 0x000BE67C
		public bool Verify()
		{
			return this.certificate.Verify();
		}

		// Token: 0x04003C80 RID: 15488
		private readonly X509Certificate2 certificate;
	}
}
