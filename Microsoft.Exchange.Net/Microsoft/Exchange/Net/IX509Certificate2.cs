using System;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C1E RID: 3102
	internal interface IX509Certificate2 : IEquatable<IX509Certificate2>
	{
		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x060043D6 RID: 17366
		X509Certificate2 Certificate { get; }

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x060043D7 RID: 17367
		IntPtr Handle { get; }

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x060043D8 RID: 17368
		string Issuer { get; }

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x060043D9 RID: 17369
		string Subject { get; }

		// Token: 0x060043DA RID: 17370
		byte[] Export(X509ContentType contentType);

		// Token: 0x060043DB RID: 17371
		byte[] Export(X509ContentType contentType, SecureString password);

		// Token: 0x060043DC RID: 17372
		byte[] Export(X509ContentType contentType, string password);

		// Token: 0x060043DD RID: 17373
		byte[] GetCertHash();

		// Token: 0x060043DE RID: 17374
		string GetCertHashString();

		// Token: 0x060043DF RID: 17375
		string GetEffectiveDateString();

		// Token: 0x060043E0 RID: 17376
		string GetExpirationDateString();

		// Token: 0x060043E1 RID: 17377
		string GetFormat();

		// Token: 0x060043E2 RID: 17378
		int GetHashCode();

		// Token: 0x060043E3 RID: 17379
		string GetKeyAlgorithm();

		// Token: 0x060043E4 RID: 17380
		byte[] GetKeyAlgorithmParameters();

		// Token: 0x060043E5 RID: 17381
		string GetKeyAlgorithmParametersString();

		// Token: 0x060043E6 RID: 17382
		byte[] GetPublicKey();

		// Token: 0x060043E7 RID: 17383
		string GetPublicKeyString();

		// Token: 0x060043E8 RID: 17384
		byte[] GetRawCertData();

		// Token: 0x060043E9 RID: 17385
		string GetRawCertDataString();

		// Token: 0x060043EA RID: 17386
		byte[] GetSerialNumber();

		// Token: 0x060043EB RID: 17387
		string GetSerialNumberString();

		// Token: 0x060043EC RID: 17388
		void Import(byte[] rawData);

		// Token: 0x060043ED RID: 17389
		void Import(string fileName);

		// Token: 0x060043EE RID: 17390
		void Import(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags);

		// Token: 0x060043EF RID: 17391
		void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags);

		// Token: 0x060043F0 RID: 17392
		void Import(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags);

		// Token: 0x060043F1 RID: 17393
		void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags);

		// Token: 0x060043F2 RID: 17394
		void Reset();

		// Token: 0x060043F3 RID: 17395
		string ToString();

		// Token: 0x060043F4 RID: 17396
		string ToString(bool fVerbose);

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x060043F5 RID: 17397
		// (set) Token: 0x060043F6 RID: 17398
		bool Archived { get; set; }

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x060043F7 RID: 17399
		X509ExtensionCollection Extensions { get; }

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x060043F8 RID: 17400
		// (set) Token: 0x060043F9 RID: 17401
		string FriendlyName { get; set; }

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x060043FA RID: 17402
		bool HasPrivateKey { get; }

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x060043FB RID: 17403
		X500DistinguishedName IssuerName { get; }

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x060043FC RID: 17404
		DateTime NotAfter { get; }

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x060043FD RID: 17405
		DateTime NotBefore { get; }

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x060043FE RID: 17406
		// (set) Token: 0x060043FF RID: 17407
		AsymmetricAlgorithm PrivateKey { get; set; }

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x06004400 RID: 17408
		PublicKey PublicKey { get; }

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06004401 RID: 17409
		byte[] RawData { get; }

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06004402 RID: 17410
		string SerialNumber { get; }

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06004403 RID: 17411
		Oid SignatureAlgorithm { get; }

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06004404 RID: 17412
		X500DistinguishedName SubjectName { get; }

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06004405 RID: 17413
		string Thumbprint { get; }

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06004406 RID: 17414
		int Version { get; }

		// Token: 0x06004407 RID: 17415
		bool Verify();
	}
}
