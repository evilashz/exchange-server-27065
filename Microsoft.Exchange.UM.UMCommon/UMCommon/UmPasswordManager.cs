using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000183 RID: 387
	internal class UmPasswordManager
	{
		// Token: 0x06000C4C RID: 3148 RVA: 0x0002D000 File Offset: 0x0002B200
		internal UmPasswordManager(UMMailboxRecipient mailbox)
		{
			this.impl = mailbox.ConfigFolder.OpenPassword();
			this.mailbox = mailbox;
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromADRecipient(this.mailbox.ADUser);
			UMMailboxPolicy policyFromRecipient = iadsystemConfigurationLookup.GetPolicyFromRecipient(this.mailbox.ADUser);
			if (policyFromRecipient == null)
			{
				throw new ADUMUserInvalidUMMailboxPolicyException(mailbox.MailAddress);
			}
			this.policy = new PasswordPolicy(policyFromRecipient);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002D06C File Offset: 0x0002B26C
		internal UmPasswordManager(UMMailboxRecipient mailbox, UMMailboxPolicy mbxPolicy)
		{
			ValidateArgument.NotNull(mailbox, "mailbox");
			ValidateArgument.NotNull(mbxPolicy, "mbxPolicy");
			this.impl = mailbox.ConfigFolder.OpenPassword();
			this.mailbox = mailbox;
			this.policy = new PasswordPolicy(mbxPolicy);
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0002D0B9 File Offset: 0x0002B2B9
		internal bool PinResetNeeded
		{
			get
			{
				return this.policy.LogonFailuresBeforePINReset > 0 && 0 == this.impl.LockoutCount % this.policy.LogonFailuresBeforePINReset;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0002D0E8 File Offset: 0x0002B2E8
		internal bool BadChecksum
		{
			get
			{
				Checksum checksum = new Checksum(this.mailbox, this.impl);
				return !checksum.IsValid;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0002D110 File Offset: 0x0002B310
		internal bool PinWasResetRecently
		{
			get
			{
				TimeSpan timeSpan = ExDateTime.UtcNow - this.UtcTimeSet;
				return timeSpan.TotalHours < 2.0 || timeSpan.TotalDays >= 36500.0;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0002D157 File Offset: 0x0002B357
		internal ExDateTime UtcTimeSet
		{
			get
			{
				return this.impl.TimeSet;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0002D164 File Offset: 0x0002B364
		internal bool IsExpired
		{
			get
			{
				int num = this.policy.DaysBeforeExpiry;
				if (num == 0)
				{
					num = 36500;
				}
				return this.impl.TimeSet.AddDays((double)num) < ExDateTime.UtcNow;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0002D1A8 File Offset: 0x0002B3A8
		internal bool IsLocked
		{
			get
			{
				return this.policy.LogonFailuresBeforeLockout != 0 && (this.impl.LockoutCount >= this.policy.LogonFailuresBeforeLockout || this.GetOfflineLockoutCount() >= this.policy.LogonFailuresBeforeLockout);
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002D1F4 File Offset: 0x0002B3F4
		internal bool Authenticate(EncryptedBuffer digits)
		{
			PIIMessage data = PIIMessage.Create(PIIType._User, this.mailbox);
			if (this.IsLocked)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, data, "Um Logon failed because UmUser=_User has a locked mailbox.", new object[0]);
				return false;
			}
			if (this.BadChecksum)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, data, "Um Logon failed because UmUser=_User has an invalid checksum.", new object[0]);
				return false;
			}
			PasswordBlob passwordBlob = this.impl.CurrentPassword;
			if (null == passwordBlob)
			{
				throw new UserConfigurationException(Strings.CorruptedPasswordField(this.mailbox.ToString()));
			}
			bool flag = passwordBlob.Equals(digits);
			bool result;
			try
			{
				if (!flag)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, data, "Um Logon failed because UmUser=_User typed an incorrect password.", new object[0]);
					this.impl.LockoutCount++;
					this.impl.Commit();
				}
				else
				{
					this.impl.LockoutCount = 0;
					this.ClearOfflineLockoutCount();
					if (string.Compare(passwordBlob.Algorithm, "SHA256", StringComparison.OrdinalIgnoreCase) != 0 || passwordBlob.Iterations != 1000)
					{
						passwordBlob = new PasswordBlob(digits, "SHA256", 1000);
						this.impl.CurrentPassword = passwordBlob;
						this.CommitPasswordAndUpdateChecksum();
					}
					else
					{
						this.impl.Commit();
					}
				}
				result = flag;
			}
			catch (QuotaExceededException)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, "Password manager could not update mailbox lockout count because the user is over quota.", new object[0]);
				if (!flag)
				{
					this.impl.LockoutCount--;
					this.IncrementOfflineLockoutCount();
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002D370 File Offset: 0x0002B570
		internal void SetPassword(EncryptedBuffer digits, bool isExpired, LockOutResetMode lockoutResetMode)
		{
			PIIMessage data = PIIMessage.Create(PIIType._User, this.mailbox);
			CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, data, "In SetPassword for UmUser=_User.", new object[0]);
			PasswordBlob currentPassword = this.impl.CurrentPassword;
			PasswordBlob currentPassword2 = new PasswordBlob(digits, "SHA256", 1000);
			this.impl.CurrentPassword = currentPassword2;
			this.impl.TimeSet = ExDateTime.UtcNow;
			this.impl.TimeSet = (isExpired ? ExDateTime.UtcNow.AddDays(-36501.0) : ExDateTime.UtcNow);
			if (null != currentPassword)
			{
				ArrayList oldPasswords = this.impl.OldPasswords;
				oldPasswords.Add(currentPassword);
				int num = Math.Max(0, this.policy.PreviousPasswordsDisallowed - 1);
				while (oldPasswords.Count > num)
				{
					oldPasswords.RemoveAt(0);
				}
				this.impl.OldPasswords = oldPasswords;
			}
			if (lockoutResetMode == LockOutResetMode.LockedOut)
			{
				this.impl.LockoutCount = this.policy.LogonFailuresBeforeLockout + 1;
			}
			else if (lockoutResetMode == LockOutResetMode.Reset)
			{
				this.impl.LockoutCount = 0;
				this.ClearOfflineLockoutCount();
			}
			this.CommitPasswordAndUpdateChecksum();
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002D491 File Offset: 0x0002B691
		internal void SetPassword(EncryptedBuffer digits)
		{
			this.SetPassword(digits, false, LockOutResetMode.Reset);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002D49C File Offset: 0x0002B69C
		internal void RequirePasswordToChangeAtFirstUse()
		{
			this.impl.TimeSet = ExDateTime.UtcNow.AddDays(-36501.0);
			this.CommitPasswordAndUpdateChecksum();
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002D4D0 File Offset: 0x0002B6D0
		internal bool IsValidPassword(EncryptedBuffer pwd)
		{
			return !this.IsWeak(pwd) && !this.HasAlreadyBeenUsed(pwd);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002D4E9 File Offset: 0x0002B6E9
		internal void UnlockMailbox()
		{
			this.impl.LockoutCount = 0;
			this.ClearOfflineLockoutCount();
			this.impl.Commit();
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002D508 File Offset: 0x0002B708
		internal bool IsWeak(EncryptedBuffer pwd)
		{
			bool result;
			using (SafeBuffer decrypted = pwd.Decrypted)
			{
				if (this.IsTooShort(decrypted.Buffer))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, "password is too short.", new object[0]);
					result = true;
				}
				else if (UmPasswordManager.HasInvalidDigits(decrypted.Buffer))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, "password has invalid digits.", new object[0]);
					result = true;
				}
				else if (this.NotComplex(decrypted.Buffer))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, "password is not complex.", new object[0]);
					result = true;
				}
				else if (this.IsUserExtension(decrypted.Buffer))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, "password is user extension.", new object[0]);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002D5E0 File Offset: 0x0002B7E0
		internal byte[] GenerateValidPassword()
		{
			byte[] array = null;
			int num = 100;
			while (--num > 0)
			{
				array = this.GetRandomPassword(this.policy.MinimumLength);
				EncryptedBuffer pwd = new EncryptedBuffer(array);
				if (this.IsValidPassword(pwd))
				{
					break;
				}
			}
			if (num == 0)
			{
				array = null;
			}
			return array;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002D628 File Offset: 0x0002B828
		private static bool HasInvalidDigits(byte[] pwd)
		{
			foreach (byte b in pwd)
			{
				if (b < 48 || b > 57)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002D65C File Offset: 0x0002B85C
		private byte[] GetRandomPassword(int len)
		{
			byte[] array = new byte[4];
			StringBuilder stringBuilder = new StringBuilder();
			while (stringBuilder.Length < len)
			{
				UmPasswordManager.rng.GetBytes(array);
				stringBuilder.Append((BitConverter.ToUInt32(array, 0) % 10U).ToString(CultureInfo.InvariantCulture));
			}
			string s = stringBuilder.ToString().Substring(0, len);
			return Encoding.ASCII.GetBytes(s);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002D6C4 File Offset: 0x0002B8C4
		private void CommitPasswordAndUpdateChecksum()
		{
			Checksum checksum = new Checksum(this.mailbox, this.impl);
			this.impl.Commit();
			checksum.Update();
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002D6F4 File Offset: 0x0002B8F4
		private bool HasAlreadyBeenUsed(EncryptedBuffer pwd)
		{
			ArrayList oldPasswords = this.impl.OldPasswords;
			int num = this.policy.PreviousPasswordsDisallowed - 1;
			int num2 = oldPasswords.Count - 1;
			while (num2 >= 0 && num > 0)
			{
				PasswordBlob passwordBlob = (PasswordBlob)oldPasswords[num2];
				if (passwordBlob.Equals(pwd))
				{
					return true;
				}
				num2--;
				num--;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, "password not found in old password list.", new object[0]);
			PasswordBlob currentPassword = this.impl.CurrentPassword;
			return !(null == currentPassword) && currentPassword.Equals(pwd);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002D785 File Offset: 0x0002B985
		private bool IsTooShort(byte[] pwd)
		{
			if (pwd.Length < this.policy.MinimumLength)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, "pwd does not meet minimum length policy.", new object[0]);
				return true;
			}
			return false;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002D7B0 File Offset: 0x0002B9B0
		private bool NotComplex(byte[] pwd)
		{
			if (pwd == null || pwd.Length < 2)
			{
				return false;
			}
			short num = (short)pwd[0];
			short num2 = (short)pwd[1];
			int num3 = (int)(num2 - num);
			for (int i = 2; i < pwd.Length; i++)
			{
				num = num2;
				num2 = (short)pwd[i];
				if ((int)(num2 - num) != num3)
				{
					return false;
				}
			}
			return (-1 == num3 || num3 == 0 || 1 == num3) && !this.policy.AllowCommonPatterns;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002D810 File Offset: 0x0002BA10
		private bool IsUserExtension(byte[] pwd)
		{
			if (string.IsNullOrEmpty(this.mailbox.ADRecipient.UMExtension))
			{
				return false;
			}
			byte[] bytes = Encoding.ASCII.GetBytes(this.mailbox.ADRecipient.UMExtension);
			int num = pwd.Length - 1;
			int num2 = bytes.Length - 1;
			while (num >= 0 && num2 >= 0)
			{
				if (pwd[num--] != bytes[num2--])
				{
					return false;
				}
			}
			return 0 > num;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002D880 File Offset: 0x0002BA80
		private int GetOfflineLockoutCount()
		{
			int result = 0;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.mailbox.CreateSessionLock())
			{
				if (UmPasswordManager.offlineLogonFailures.ContainsKey(this.mailbox.ExchangeLegacyDN) && XsoUtil.IsOverReceiveQuota(mailboxSessionLock.Session.Mailbox, 0UL))
				{
					lock (UmPasswordManager.offlineLogonFailures)
					{
						if (!UmPasswordManager.offlineLogonFailures.TryGetValue(this.mailbox.ExchangeLegacyDN, out result))
						{
							result = 0;
						}
						goto IL_78;
					}
				}
				this.ClearOfflineLockoutCount();
				result = 0;
				IL_78:;
			}
			return result;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002D930 File Offset: 0x0002BB30
		private void ClearOfflineLockoutCount()
		{
			lock (UmPasswordManager.offlineLogonFailures)
			{
				UmPasswordManager.offlineLogonFailures.Remove(this.mailbox.ExchangeLegacyDN);
			}
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002D980 File Offset: 0x0002BB80
		private void IncrementOfflineLockoutCount()
		{
			lock (UmPasswordManager.offlineLogonFailures)
			{
				int val = 0;
				if (!UmPasswordManager.offlineLogonFailures.TryGetValue(this.mailbox.ExchangeLegacyDN, out val))
				{
					val = 0;
				}
				UmPasswordManager.offlineLogonFailures[this.mailbox.ExchangeLegacyDN] = Math.Max(val, this.impl.LockoutCount) + 1;
			}
		}

		// Token: 0x040006AF RID: 1711
		private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

		// Token: 0x040006B0 RID: 1712
		private static Dictionary<string, int> offlineLogonFailures = new Dictionary<string, int>();

		// Token: 0x040006B1 RID: 1713
		private IPassword impl;

		// Token: 0x040006B2 RID: 1714
		private UMMailboxRecipient mailbox;

		// Token: 0x040006B3 RID: 1715
		private PasswordPolicy policy;
	}
}
