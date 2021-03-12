using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;
using Microsoft.Win32;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002AE RID: 686
	[ComVisible(true)]
	[Serializable]
	public class X509Certificate : IDisposable, IDeserializationCallback, ISerializable
	{
		// Token: 0x06002449 RID: 9289 RVA: 0x00083FB7 File Offset: 0x000821B7
		[SecuritySafeCritical]
		private void Init()
		{
			this.m_safeCertContext = SafeCertContextHandle.InvalidHandle;
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x00083FC4 File Offset: 0x000821C4
		public X509Certificate()
		{
			this.Init();
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x00083FD2 File Offset: 0x000821D2
		public X509Certificate(byte[] data) : this()
		{
			if (data != null && data.Length != 0)
			{
				this.LoadCertificateFromBlob(data, null, X509KeyStorageFlags.DefaultKeySet);
			}
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x00083FEA File Offset: 0x000821EA
		public X509Certificate(byte[] rawData, string password) : this()
		{
			this.LoadCertificateFromBlob(rawData, password, X509KeyStorageFlags.DefaultKeySet);
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x00083FFB File Offset: 0x000821FB
		public X509Certificate(byte[] rawData, SecureString password) : this()
		{
			this.LoadCertificateFromBlob(rawData, password, X509KeyStorageFlags.DefaultKeySet);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x0008400C File Offset: 0x0008220C
		public X509Certificate(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags) : this()
		{
			this.LoadCertificateFromBlob(rawData, password, keyStorageFlags);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x0008401D File Offset: 0x0008221D
		public X509Certificate(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags) : this()
		{
			this.LoadCertificateFromBlob(rawData, password, keyStorageFlags);
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x0008402E File Offset: 0x0008222E
		[SecuritySafeCritical]
		public X509Certificate(string fileName) : this()
		{
			this.LoadCertificateFromFile(fileName, null, X509KeyStorageFlags.DefaultKeySet);
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x0008403F File Offset: 0x0008223F
		[SecuritySafeCritical]
		public X509Certificate(string fileName, string password) : this()
		{
			this.LoadCertificateFromFile(fileName, password, X509KeyStorageFlags.DefaultKeySet);
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x00084050 File Offset: 0x00082250
		[SecuritySafeCritical]
		public X509Certificate(string fileName, SecureString password) : this()
		{
			this.LoadCertificateFromFile(fileName, password, X509KeyStorageFlags.DefaultKeySet);
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x00084061 File Offset: 0x00082261
		[SecuritySafeCritical]
		public X509Certificate(string fileName, string password, X509KeyStorageFlags keyStorageFlags) : this()
		{
			this.LoadCertificateFromFile(fileName, password, keyStorageFlags);
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x00084072 File Offset: 0x00082272
		[SecuritySafeCritical]
		public X509Certificate(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags) : this()
		{
			this.LoadCertificateFromFile(fileName, password, keyStorageFlags);
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x00084083 File Offset: 0x00082283
		[SecurityCritical]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public X509Certificate(IntPtr handle) : this()
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidHandle"), "handle");
			}
			X509Utils.DuplicateCertContext(handle, this.m_safeCertContext);
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000840B9 File Offset: 0x000822B9
		[SecuritySafeCritical]
		public X509Certificate(X509Certificate cert) : this()
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			if (cert.m_safeCertContext.pCertContext != IntPtr.Zero)
			{
				this.m_safeCertContext = cert.GetCertContextForCloning();
				this.m_certContextCloned = true;
			}
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000840FC File Offset: 0x000822FC
		public X509Certificate(SerializationInfo info, StreamingContext context) : this()
		{
			byte[] array = (byte[])info.GetValue("RawData", typeof(byte[]));
			if (array != null)
			{
				this.LoadCertificateFromBlob(array, null, X509KeyStorageFlags.DefaultKeySet);
			}
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x00084136 File Offset: 0x00082336
		public static X509Certificate CreateFromCertFile(string filename)
		{
			return new X509Certificate(filename);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x0008413E File Offset: 0x0008233E
		public static X509Certificate CreateFromSignedFile(string filename)
		{
			return new X509Certificate(filename);
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x00084146 File Offset: 0x00082346
		[ComVisible(false)]
		public IntPtr Handle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.m_safeCertContext.pCertContext;
			}
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00084153 File Offset: 0x00082353
		[SecuritySafeCritical]
		[Obsolete("This method has been deprecated.  Please use the Subject property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual string GetName()
		{
			this.ThrowIfContextInvalid();
			return X509Utils._GetSubjectInfo(this.m_safeCertContext, 2U, true);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00084168 File Offset: 0x00082368
		[SecuritySafeCritical]
		[Obsolete("This method has been deprecated.  Please use the Issuer property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual string GetIssuerName()
		{
			this.ThrowIfContextInvalid();
			return X509Utils._GetIssuerName(this.m_safeCertContext, true);
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x0008417C File Offset: 0x0008237C
		[SecuritySafeCritical]
		public virtual byte[] GetSerialNumber()
		{
			this.ThrowIfContextInvalid();
			if (this.m_serialNumber == null)
			{
				this.m_serialNumber = X509Utils._GetSerialNumber(this.m_safeCertContext);
			}
			return (byte[])this.m_serialNumber.Clone();
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000841AD File Offset: 0x000823AD
		public virtual string GetSerialNumberString()
		{
			return this.SerialNumber;
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000841B5 File Offset: 0x000823B5
		[SecuritySafeCritical]
		public virtual byte[] GetKeyAlgorithmParameters()
		{
			this.ThrowIfContextInvalid();
			if (this.m_publicKeyParameters == null)
			{
				this.m_publicKeyParameters = X509Utils._GetPublicKeyParameters(this.m_safeCertContext);
			}
			return (byte[])this.m_publicKeyParameters.Clone();
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000841E6 File Offset: 0x000823E6
		[SecuritySafeCritical]
		public virtual string GetKeyAlgorithmParametersString()
		{
			this.ThrowIfContextInvalid();
			return Hex.EncodeHexString(this.GetKeyAlgorithmParameters());
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000841F9 File Offset: 0x000823F9
		[SecuritySafeCritical]
		public virtual string GetKeyAlgorithm()
		{
			this.ThrowIfContextInvalid();
			if (this.m_publicKeyOid == null)
			{
				this.m_publicKeyOid = X509Utils._GetPublicKeyOid(this.m_safeCertContext);
			}
			return this.m_publicKeyOid;
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x00084220 File Offset: 0x00082420
		[SecuritySafeCritical]
		public virtual byte[] GetPublicKey()
		{
			this.ThrowIfContextInvalid();
			if (this.m_publicKeyValue == null)
			{
				this.m_publicKeyValue = X509Utils._GetPublicKeyValue(this.m_safeCertContext);
			}
			return (byte[])this.m_publicKeyValue.Clone();
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x00084251 File Offset: 0x00082451
		public virtual string GetPublicKeyString()
		{
			return Hex.EncodeHexString(this.GetPublicKey());
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x0008425E File Offset: 0x0008245E
		[SecuritySafeCritical]
		public virtual byte[] GetRawCertData()
		{
			return this.RawData;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x00084266 File Offset: 0x00082466
		public virtual string GetRawCertDataString()
		{
			return Hex.EncodeHexString(this.GetRawCertData());
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x00084273 File Offset: 0x00082473
		public virtual byte[] GetCertHash()
		{
			this.SetThumbprint();
			return (byte[])this.m_thumbprint.Clone();
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x0008428C File Offset: 0x0008248C
		[SecuritySafeCritical]
		public virtual byte[] GetCertHash(HashAlgorithmName hashAlgorithm)
		{
			this.ThrowIfContextInvalid();
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(Environment.GetResourceString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
			}
			byte[] result;
			using (HashAlgorithm hashAlgorithm2 = CryptoConfig.CreateFromName(hashAlgorithm.Name) as HashAlgorithm)
			{
				if (hashAlgorithm2 == null || hashAlgorithm2 is KeyedHashAlgorithm)
				{
					throw new CryptographicException(-1073741275);
				}
				byte[] rawData = this.m_rawData;
				if (rawData == null)
				{
					rawData = this.RawData;
				}
				result = hashAlgorithm2.ComputeHash(rawData);
			}
			return result;
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x00084320 File Offset: 0x00082520
		public virtual string GetCertHashString()
		{
			this.SetThumbprint();
			return Hex.EncodeHexString(this.m_thumbprint);
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x00084334 File Offset: 0x00082534
		public virtual string GetCertHashString(HashAlgorithmName hashAlgorithm)
		{
			byte[] certHash = this.GetCertHash(hashAlgorithm);
			return Hex.EncodeHexString(certHash);
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x00084350 File Offset: 0x00082550
		public virtual string GetEffectiveDateString()
		{
			return this.NotBefore.ToString();
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x0008436C File Offset: 0x0008256C
		public virtual string GetExpirationDateString()
		{
			return this.NotAfter.ToString();
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x00084388 File Offset: 0x00082588
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (!(obj is X509Certificate))
			{
				return false;
			}
			X509Certificate other = (X509Certificate)obj;
			return this.Equals(other);
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000843B0 File Offset: 0x000825B0
		[SecuritySafeCritical]
		public virtual bool Equals(X509Certificate other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.m_safeCertContext.IsInvalid)
			{
				return other.m_safeCertContext.IsInvalid;
			}
			return this.Issuer.Equals(other.Issuer) && this.SerialNumber.Equals(other.SerialNumber);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x00084408 File Offset: 0x00082608
		[SecuritySafeCritical]
		public override int GetHashCode()
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				return 0;
			}
			this.SetThumbprint();
			int num = 0;
			int num2 = 0;
			while (num2 < this.m_thumbprint.Length && num2 < 4)
			{
				num = (num << 8 | (int)this.m_thumbprint[num2]);
				num2++;
			}
			return num;
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x00084451 File Offset: 0x00082651
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x0008445C File Offset: 0x0008265C
		[SecuritySafeCritical]
		public virtual string ToString(bool fVerbose)
		{
			if (!fVerbose || this.m_safeCertContext.IsInvalid)
			{
				return base.GetType().FullName;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[Subject]" + Environment.NewLine + "  ");
			stringBuilder.Append(this.Subject);
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Issuer]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(this.Issuer);
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Serial Number]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(this.SerialNumber);
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Not Before]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(X509Certificate.FormatDate(this.NotBefore));
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Not After]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(X509Certificate.FormatDate(this.NotAfter));
			stringBuilder.Append(string.Concat(new string[]
			{
				Environment.NewLine,
				Environment.NewLine,
				"[Thumbprint]",
				Environment.NewLine,
				"  "
			}));
			stringBuilder.Append(this.GetCertHashString());
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x00084634 File Offset: 0x00082834
		protected static string FormatDate(DateTime date)
		{
			CultureInfo cultureInfo = CultureInfo.CurrentCulture;
			if (!cultureInfo.DateTimeFormat.Calendar.IsValidDay(date.Year, date.Month, date.Day, 0))
			{
				if (cultureInfo.DateTimeFormat.Calendar is UmAlQuraCalendar)
				{
					cultureInfo = (cultureInfo.Clone() as CultureInfo);
					cultureInfo.DateTimeFormat.Calendar = new HijriCalendar();
				}
				else
				{
					cultureInfo = CultureInfo.InvariantCulture;
				}
			}
			return date.ToString(cultureInfo);
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000846AD File Offset: 0x000828AD
		public virtual string GetFormat()
		{
			return "X509";
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x000846B4 File Offset: 0x000828B4
		public string Issuer
		{
			[SecuritySafeCritical]
			get
			{
				this.ThrowIfContextInvalid();
				if (this.m_issuerName == null)
				{
					this.m_issuerName = X509Utils._GetIssuerName(this.m_safeCertContext, false);
				}
				return this.m_issuerName;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x000846DC File Offset: 0x000828DC
		public string Subject
		{
			[SecuritySafeCritical]
			get
			{
				this.ThrowIfContextInvalid();
				if (this.m_subjectName == null)
				{
					this.m_subjectName = X509Utils._GetSubjectInfo(this.m_safeCertContext, 2U, false);
				}
				return this.m_subjectName;
			}
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x00084705 File Offset: 0x00082905
		[SecurityCritical]
		[ComVisible(false)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual void Import(byte[] rawData)
		{
			this.Reset();
			this.LoadCertificateFromBlob(rawData, null, X509KeyStorageFlags.DefaultKeySet);
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x00084716 File Offset: 0x00082916
		[SecurityCritical]
		[ComVisible(false)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			this.LoadCertificateFromBlob(rawData, password, keyStorageFlags);
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x00084727 File Offset: 0x00082927
		[SecurityCritical]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual void Import(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			this.LoadCertificateFromBlob(rawData, password, keyStorageFlags);
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x00084738 File Offset: 0x00082938
		[SecurityCritical]
		[ComVisible(false)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual void Import(string fileName)
		{
			this.Reset();
			this.LoadCertificateFromFile(fileName, null, X509KeyStorageFlags.DefaultKeySet);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x00084749 File Offset: 0x00082949
		[SecurityCritical]
		[ComVisible(false)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			this.LoadCertificateFromFile(fileName, password, keyStorageFlags);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x0008475A File Offset: 0x0008295A
		[SecurityCritical]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual void Import(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			this.LoadCertificateFromFile(fileName, password, keyStorageFlags);
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x0008476B File Offset: 0x0008296B
		[SecuritySafeCritical]
		[ComVisible(false)]
		public virtual byte[] Export(X509ContentType contentType)
		{
			return this.ExportHelper(contentType, null);
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x00084775 File Offset: 0x00082975
		[SecuritySafeCritical]
		[ComVisible(false)]
		public virtual byte[] Export(X509ContentType contentType, string password)
		{
			return this.ExportHelper(contentType, password);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x0008477F File Offset: 0x0008297F
		[SecuritySafeCritical]
		public virtual byte[] Export(X509ContentType contentType, SecureString password)
		{
			return this.ExportHelper(contentType, password);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x0008478C File Offset: 0x0008298C
		[SecurityCritical]
		[ComVisible(false)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual void Reset()
		{
			this.m_subjectName = null;
			this.m_issuerName = null;
			this.m_serialNumber = null;
			this.m_publicKeyParameters = null;
			this.m_publicKeyValue = null;
			this.m_publicKeyOid = null;
			this.m_rawData = null;
			this.m_thumbprint = null;
			this.m_notBefore = DateTime.MinValue;
			this.m_notAfter = DateTime.MinValue;
			if (!this.m_safeCertContext.IsInvalid)
			{
				if (!this.m_certContextCloned)
				{
					this.m_safeCertContext.Dispose();
				}
				this.m_safeCertContext = SafeCertContextHandle.InvalidHandle;
			}
			this.m_certContextCloned = false;
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x00084819 File Offset: 0x00082A19
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x00084822 File Offset: 0x00082A22
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Reset();
			}
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x0008482D File Offset: 0x00082A2D
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				info.AddValue("RawData", null);
				return;
			}
			info.AddValue("RawData", this.RawData);
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x0008485A File Offset: 0x00082A5A
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x0008485C File Offset: 0x00082A5C
		internal SafeCertContextHandle CertContext
		{
			[SecurityCritical]
			get
			{
				return this.m_safeCertContext;
			}
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x00084864 File Offset: 0x00082A64
		[SecurityCritical]
		internal SafeCertContextHandle GetCertContextForCloning()
		{
			this.m_certContextCloned = true;
			return this.m_safeCertContext;
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x00084873 File Offset: 0x00082A73
		[SecurityCritical]
		private void ThrowIfContextInvalid()
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHandle"), "m_safeCertContext");
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x00084898 File Offset: 0x00082A98
		private DateTime NotAfter
		{
			[SecuritySafeCritical]
			get
			{
				this.ThrowIfContextInvalid();
				if (this.m_notAfter == DateTime.MinValue)
				{
					Win32Native.FILE_TIME file_TIME = default(Win32Native.FILE_TIME);
					X509Utils._GetDateNotAfter(this.m_safeCertContext, ref file_TIME);
					this.m_notAfter = DateTime.FromFileTime(file_TIME.ToTicks());
				}
				return this.m_notAfter;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x000848EC File Offset: 0x00082AEC
		private DateTime NotBefore
		{
			[SecuritySafeCritical]
			get
			{
				this.ThrowIfContextInvalid();
				if (this.m_notBefore == DateTime.MinValue)
				{
					Win32Native.FILE_TIME file_TIME = default(Win32Native.FILE_TIME);
					X509Utils._GetDateNotBefore(this.m_safeCertContext, ref file_TIME);
					this.m_notBefore = DateTime.FromFileTime(file_TIME.ToTicks());
				}
				return this.m_notBefore;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x0008493E File Offset: 0x00082B3E
		private byte[] RawData
		{
			[SecurityCritical]
			get
			{
				this.ThrowIfContextInvalid();
				if (this.m_rawData == null)
				{
					this.m_rawData = X509Utils._GetCertRawData(this.m_safeCertContext);
				}
				return (byte[])this.m_rawData.Clone();
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06002489 RID: 9353 RVA: 0x0008496F File Offset: 0x00082B6F
		private string SerialNumber
		{
			[SecuritySafeCritical]
			get
			{
				this.ThrowIfContextInvalid();
				if (this.m_serialNumber == null)
				{
					this.m_serialNumber = X509Utils._GetSerialNumber(this.m_safeCertContext);
				}
				return Hex.EncodeHexStringFromInt(this.m_serialNumber);
			}
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x0008499B File Offset: 0x00082B9B
		[SecuritySafeCritical]
		private void SetThumbprint()
		{
			this.ThrowIfContextInvalid();
			if (this.m_thumbprint == null)
			{
				this.m_thumbprint = X509Utils._GetThumbprint(this.m_safeCertContext);
			}
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000849BC File Offset: 0x00082BBC
		[SecurityCritical]
		private byte[] ExportHelper(X509ContentType contentType, object password)
		{
			switch (contentType)
			{
			case X509ContentType.Cert:
			case X509ContentType.SerializedCert:
				break;
			case X509ContentType.Pfx:
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.Open | KeyContainerPermissionFlags.Export);
				keyContainerPermission.Demand();
				break;
			}
			default:
				throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_InvalidContentType"));
			}
			IntPtr intPtr = IntPtr.Zero;
			byte[] array = null;
			SafeCertStoreHandle safeCertStoreHandle = X509Utils.ExportCertToMemoryStore(this);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				intPtr = X509Utils.PasswordToHGlobalUni(password);
				array = X509Utils._ExportCertificatesToBlob(safeCertStoreHandle, contentType, intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
				safeCertStoreHandle.Dispose();
			}
			if (array == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_ExportFailed"));
			}
			return array;
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x00084A64 File Offset: 0x00082C64
		[SecuritySafeCritical]
		private void LoadCertificateFromBlob(byte[] rawData, object password, X509KeyStorageFlags keyStorageFlags)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EmptyOrNullArray"), "rawData");
			}
			X509ContentType x509ContentType = X509Utils.MapContentType(X509Utils._QueryCertBlobType(rawData));
			if (x509ContentType == X509ContentType.Pfx && (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) == X509KeyStorageFlags.PersistKeySet)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.Create);
				keyContainerPermission.Demand();
			}
			uint dwFlags = X509Utils.MapKeyStorageFlags(keyStorageFlags);
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				intPtr = X509Utils.PasswordToHGlobalUni(password);
				X509Utils.LoadCertFromBlob(rawData, intPtr, dwFlags, (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) != X509KeyStorageFlags.DefaultKeySet, this.m_safeCertContext);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
			}
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x00084B0C File Offset: 0x00082D0C
		[SecurityCritical]
		private void LoadCertificateFromFile(string fileName, object password, X509KeyStorageFlags keyStorageFlags)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			string fullPathInternal = Path.GetFullPathInternal(fileName);
			new FileIOPermission(FileIOPermissionAccess.Read, fullPathInternal).Demand();
			X509ContentType x509ContentType = X509Utils.MapContentType(X509Utils._QueryCertFileType(fileName));
			if (x509ContentType == X509ContentType.Pfx && (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) == X509KeyStorageFlags.PersistKeySet)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.Create);
				keyContainerPermission.Demand();
			}
			uint dwFlags = X509Utils.MapKeyStorageFlags(keyStorageFlags);
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				intPtr = X509Utils.PasswordToHGlobalUni(password);
				X509Utils.LoadCertFromFile(fileName, intPtr, dwFlags, (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) != X509KeyStorageFlags.DefaultKeySet, this.m_safeCertContext);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
			}
		}

		// Token: 0x04000DA5 RID: 3493
		private const string m_format = "X509";

		// Token: 0x04000DA6 RID: 3494
		private string m_subjectName;

		// Token: 0x04000DA7 RID: 3495
		private string m_issuerName;

		// Token: 0x04000DA8 RID: 3496
		private byte[] m_serialNumber;

		// Token: 0x04000DA9 RID: 3497
		private byte[] m_publicKeyParameters;

		// Token: 0x04000DAA RID: 3498
		private byte[] m_publicKeyValue;

		// Token: 0x04000DAB RID: 3499
		private string m_publicKeyOid;

		// Token: 0x04000DAC RID: 3500
		private byte[] m_rawData;

		// Token: 0x04000DAD RID: 3501
		private byte[] m_thumbprint;

		// Token: 0x04000DAE RID: 3502
		private DateTime m_notBefore;

		// Token: 0x04000DAF RID: 3503
		private DateTime m_notAfter;

		// Token: 0x04000DB0 RID: 3504
		[SecurityCritical]
		private SafeCertContextHandle m_safeCertContext;

		// Token: 0x04000DB1 RID: 3505
		private bool m_certContextCloned;

		// Token: 0x04000DB2 RID: 3506
		internal const X509KeyStorageFlags KeyStorageFlagsAll = X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet;
	}
}
