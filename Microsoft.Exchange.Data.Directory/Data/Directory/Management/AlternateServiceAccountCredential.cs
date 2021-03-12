using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Security;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Cryptography;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006E2 RID: 1762
	[Serializable]
	public class AlternateServiceAccountCredential : ConfigurableObject, IComparable<AlternateServiceAccountCredential>, IDisposable
	{
		// Token: 0x0600515C RID: 20828 RVA: 0x0012D1B7 File Offset: 0x0012B3B7
		private AlternateServiceAccountCredential(string registryValueName, Exception parseError) : base(new SimpleProviderPropertyBag())
		{
			this.registryValueName = registryValueName;
			this.propertyBag.SetField(SimpleProviderObjectSchema.ExchangeVersion, ExchangeObjectVersion.Exchange2010);
			this.parseError = parseError;
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x0012D1E8 File Offset: 0x0012B3E8
		private AlternateServiceAccountCredential(string registryValueName, Exception parseError, bool canSave, DateTime whenAddedUtc, string domain, string userName, SecureString password) : this(registryValueName, parseError)
		{
			this[AlternateServiceAccountCredentialSchema.WhenAddedUTC] = whenAddedUtc;
			this[AlternateServiceAccountCredentialSchema.Domain] = domain;
			this[AlternateServiceAccountCredentialSchema.UserName] = userName;
			this.password = password;
			this.propertyBag.SetIsReadOnly(!canSave);
		}

		// Token: 0x17001ABA RID: 6842
		// (get) Token: 0x0600515E RID: 20830 RVA: 0x0012D240 File Offset: 0x0012B440
		public string Domain
		{
			get
			{
				return (string)this[AlternateServiceAccountCredentialSchema.Domain];
			}
		}

		// Token: 0x17001ABB RID: 6843
		// (get) Token: 0x0600515F RID: 20831 RVA: 0x0012D252 File Offset: 0x0012B452
		public string UserName
		{
			get
			{
				return (string)this[AlternateServiceAccountCredentialSchema.UserName];
			}
		}

		// Token: 0x17001ABC RID: 6844
		// (get) Token: 0x06005160 RID: 20832 RVA: 0x0012D264 File Offset: 0x0012B464
		public string QualifiedUserName
		{
			get
			{
				return this.Domain + "\\" + this.UserName;
			}
		}

		// Token: 0x17001ABD RID: 6845
		// (get) Token: 0x06005161 RID: 20833 RVA: 0x0012D27C File Offset: 0x0012B47C
		public PSCredential Credential
		{
			get
			{
				if (this.Password == null)
				{
					return null;
				}
				return new PSCredential(this.QualifiedUserName, this.Password);
			}
		}

		// Token: 0x17001ABE RID: 6846
		// (get) Token: 0x06005162 RID: 20834 RVA: 0x0012D299 File Offset: 0x0012B499
		public DateTime? WhenAdded
		{
			get
			{
				return (DateTime?)this[AlternateServiceAccountCredentialSchema.WhenAdded];
			}
		}

		// Token: 0x17001ABF RID: 6847
		// (get) Token: 0x06005163 RID: 20835 RVA: 0x0012D2AB File Offset: 0x0012B4AB
		public DateTime? WhenAddedUTC
		{
			get
			{
				return (DateTime?)this[AlternateServiceAccountCredentialSchema.WhenAddedUTC];
			}
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x0012D2C0 File Offset: 0x0012B4C0
		public override string ToString()
		{
			return (this.WhenAdded != null) ? DirectoryStrings.AlternateServiceAccountCredentialDisplayFormat(this.IsValid ? string.Empty : DirectoryStrings.AlternateServiceAccountCredentialIsInvalid, this.WhenAdded.Value, this.Domain, this.UserName) : DirectoryStrings.AlternateServiceAccountCredentialIsInvalid;
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x0012D324 File Offset: 0x0012B524
		public int CompareTo(AlternateServiceAccountCredential other)
		{
			if (other == null)
			{
				return 1;
			}
			if (this.WhenAdded == null || other.WhenAdded == null)
			{
				return (this.WhenAdded != null).CompareTo(other.WhenAdded != null);
			}
			int num = -this.WhenAdded.Value.CompareTo(other.WhenAdded.Value);
			if (num == 0)
			{
				return this.GetHashCode().CompareTo(other.GetHashCode());
			}
			return num;
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x0012D3C0 File Offset: 0x0012B5C0
		public void Dispose()
		{
			if (this.password != null)
			{
				this.password.Dispose();
				this.password = null;
			}
		}

		// Token: 0x17001AC0 RID: 6848
		// (get) Token: 0x06005167 RID: 20839 RVA: 0x0012D3DC File Offset: 0x0012B5DC
		public override bool IsValid
		{
			get
			{
				return base.IsValid && this.parseError == null;
			}
		}

		// Token: 0x17001AC1 RID: 6849
		// (get) Token: 0x06005168 RID: 20840 RVA: 0x0012D3F1 File Offset: 0x0012B5F1
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return AlternateServiceAccountCredential.schema;
			}
		}

		// Token: 0x17001AC2 RID: 6850
		// (get) Token: 0x06005169 RID: 20841 RVA: 0x0012D3F8 File Offset: 0x0012B5F8
		internal Exception ParseError
		{
			get
			{
				return this.parseError;
			}
		}

		// Token: 0x17001AC3 RID: 6851
		// (get) Token: 0x0600516A RID: 20842 RVA: 0x0012D400 File Offset: 0x0012B600
		internal SecureString Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x0600516B RID: 20843 RVA: 0x0012D408 File Offset: 0x0012B608
		internal static AlternateServiceAccountCredential Create(TimeSpan randomizationTimeStampDelta, PSCredential credential)
		{
			if (credential == null)
			{
				throw new ArgumentNullException("credential");
			}
			AlternateServiceAccountConfiguration.EnsureCanDoCryptoOperations();
			string domain;
			string userName;
			AlternateServiceAccountCredential.ParseQualifiedUserName(credential.UserName, out domain, out userName);
			DateTime whenAddedUtc = DateTime.UtcNow + randomizationTimeStampDelta;
			return new AlternateServiceAccountCredential(AlternateServiceAccountCredential.GetRegistryValueName(whenAddedUtc), null, true, whenAddedUtc, domain, userName, credential.Password);
		}

		// Token: 0x0600516C RID: 20844 RVA: 0x0012D480 File Offset: 0x0012B680
		internal static IEnumerable<AlternateServiceAccountCredential> LoadFromRegistry(RegistryKey rootKey, bool decryptPasswords)
		{
			if (decryptPasswords)
			{
				AlternateServiceAccountConfiguration.EnsureCanDoCryptoOperations();
			}
			return from valueName in rootKey.GetValueNames()
			select AlternateServiceAccountCredential.LoadFromRegistry(rootKey, valueName, decryptPasswords) into credential
			where credential != null
			select credential;
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x0012D4EC File Offset: 0x0012B6EC
		internal static object WhenAddedGetter(IPropertyBag propertyBag)
		{
			DateTime? dateTime = propertyBag[AlternateServiceAccountCredentialSchema.WhenAddedUTC] as DateTime?;
			if (dateTime == null)
			{
				return null;
			}
			return dateTime.Value.ToLocalTime();
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x0012D52E File Offset: 0x0012B72E
		internal void ApplyPassword(SecureString password)
		{
			if (this.password != null)
			{
				throw new InvalidOperationException("Password has already been set");
			}
			this.password = password;
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x0012D54A File Offset: 0x0012B74A
		internal void Remove(RegistryKey rootKey)
		{
			this.propertyBag.SetIsReadOnly(true);
			rootKey.DeleteValue(this.registryValueName, false);
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x0012D568 File Offset: 0x0012B768
		internal bool TryAuthenticate(out SecurityStatus authenticationStatus)
		{
			bool result;
			using (AuthenticationContext authenticationContext = new AuthenticationContext())
			{
				authenticationStatus = authenticationContext.LogonUser(this.QualifiedUserName, this.Password);
				result = (authenticationStatus == SecurityStatus.OK);
			}
			return result;
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x0012D5B4 File Offset: 0x0012B7B4
		internal void SaveToRegistry(RegistryKey rootKey)
		{
			if (this.Password == null)
			{
				throw new InvalidOperationException("SaveToRegistry cannot be called on deserialized instances.");
			}
			base.CheckWritable();
			if (rootKey.GetValue(this.registryValueName) != null)
			{
				throw new DataSourceTransientException(DirectoryStrings.FailedToWriteAlternateServiceAccountConfigToRegistry(AlternateServiceAccountCredential.GetRegistryValueDisplayPath(rootKey.Name, this.registryValueName)));
			}
			string value = string.Format("{0}\\{1}\\{2}", this.Domain, this.UserName, Convert.ToBase64String(CapiNativeMethods.DPAPIEncryptData(this.Password, (CapiNativeMethods.DPAPIFlags)0U)));
			rootKey.SetValue(this.registryValueName, value);
			this.propertyBag.SetIsReadOnly(true);
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x0012D645 File Offset: 0x0012B845
		private static string GetRegistryValueName(DateTime whenAddedUtc)
		{
			return whenAddedUtc.ToString("yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture);
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x0012D658 File Offset: 0x0012B858
		private static string GetRegistryValueDisplayPath(string keyName, string valueName)
		{
			return string.Format("{0}\\@{1}", keyName, valueName);
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x0012D668 File Offset: 0x0012B868
		private static AlternateServiceAccountCredential LoadFromRegistry(RegistryKey rootKey, string valueName, bool decryptPasswords)
		{
			string text = rootKey.GetValue(valueName) as string;
			if (text == null)
			{
				return null;
			}
			DateTime dateTime;
			Match match;
			if (!DateTime.TryParseExact(valueName, "yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime) || !(match = AlternateServiceAccountCredential.regexRegValue.Match(text)).Success)
			{
				return new AlternateServiceAccountCredential(valueName, new DataValidationException(new ObjectValidationError(DirectoryStrings.FailedToParseAlternateServiceAccountCredential, null, AlternateServiceAccountCredential.GetRegistryValueDisplayPath(rootKey.Name, valueName))));
			}
			byte[] array;
			Exception ex;
			try
			{
				array = Convert.FromBase64String(match.Groups["password"].Value);
				ex = null;
			}
			catch (FormatException ex2)
			{
				ex = ex2;
				array = null;
			}
			SecureString secureString = null;
			if (decryptPasswords && array != null)
			{
				try
				{
					secureString = CapiNativeMethods.DPAPIDecryptDataToSecureString(array, (CapiNativeMethods.DPAPIFlags)0U);
					secureString.MakeReadOnly();
				}
				catch (CryptographicException innerException)
				{
					ex = new DataSourceOperationException(DirectoryStrings.FailedToReadAlternateServiceAccountConfigFromRegistry(AlternateServiceAccountCredential.GetRegistryValueDisplayPath(rootKey.Name, valueName)), innerException);
				}
			}
			return new AlternateServiceAccountCredential(valueName, ex, false, dateTime.ToUniversalTime(), match.Groups["domain"].Value, match.Groups["userName"].Value, secureString);
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x0012D790 File Offset: 0x0012B990
		private static void ParseQualifiedUserName(string qualifiedUserName, out string domain, out string userName)
		{
			Match match = AlternateServiceAccountCredential.regexDomainUser.Match(qualifiedUserName);
			if (match.Success)
			{
				domain = match.Groups["domain"].Value;
				userName = match.Groups["userName"].Value;
				return;
			}
			throw new DataValidationException(new PropertyValidationError(DirectoryStrings.AlternateServiceAccountCredentialQualifiedUserNameWrongFormat, AlternateServiceAccountCredentialSchema.UserName, qualifiedUserName));
		}

		// Token: 0x04003727 RID: 14119
		private const string RegistryValueNameFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		// Token: 0x04003728 RID: 14120
		private const string QualifiedUserNamePattern = "(?'domain'[^\\\\]*)\\\\(?'userName'[^\\\\]+)";

		// Token: 0x04003729 RID: 14121
		private static readonly AlternateServiceAccountCredentialSchema schema = new AlternateServiceAccountCredentialSchema();

		// Token: 0x0400372A RID: 14122
		private static readonly Regex regexDomainUser = new Regex("^(?'domain'[^\\\\]*)\\\\(?'userName'[^\\\\]+)$", RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x0400372B RID: 14123
		private static readonly Regex regexRegValue = new Regex("^(?'domain'[^\\\\]*)\\\\(?'userName'[^\\\\]+)\\\\(?'password'.+)$", RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x0400372C RID: 14124
		private readonly Exception parseError;

		// Token: 0x0400372D RID: 14125
		[NonSerialized]
		private readonly string registryValueName;

		// Token: 0x0400372E RID: 14126
		[NonSerialized]
		private SecureString password;

		// Token: 0x0400372F RID: 14127
		internal static readonly StringComparer UserNameComparer = StringComparer.OrdinalIgnoreCase;
	}
}
