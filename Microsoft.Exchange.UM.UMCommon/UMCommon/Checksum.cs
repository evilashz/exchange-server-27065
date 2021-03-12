using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000024 RID: 36
	internal class Checksum
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0000858C File Offset: 0x0000678C
		internal Checksum(UMMailboxRecipient user, IPassword pwdImpl)
		{
			this.pwdImpl = pwdImpl;
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromADRecipient(user.ADRecipient, false);
			ADRecipient adrecipient = iadrecipientLookup.LookupByObjectId(user.ADRecipient.Id);
			this.adUser = (adrecipient as ADUser);
			if (this.adUser == null)
			{
				throw new UmUserException(Strings.ADAccessFailed);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000085EC File Offset: 0x000067EC
		internal bool IsValid
		{
			get
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, "Validating checksum.", new object[0]);
				byte[] umpinChecksum = this.adUser.UMPinChecksum;
				byte[] array = this.Calculate();
				if (umpinChecksum == null)
				{
					return false;
				}
				bool flag = umpinChecksum.Length == array.Length;
				int num = 0;
				while (flag && num < umpinChecksum.Length)
				{
					flag = (umpinChecksum[num] == array[num]);
					num++;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, "Checksum.IsValid returning {0}.", new object[]
				{
					flag
				});
				return flag;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00008671 File Offset: 0x00006871
		internal void Update()
		{
			this.Update(this.Calculate());
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00008680 File Offset: 0x00006880
		private void Update(byte[] calculated)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, "Updating checksum.", new object[0]);
			if (this.adUser.UMPinChecksum != calculated)
			{
				this.adUser.UMPinChecksum = calculated;
				this.adUser.Session.Save(this.adUser);
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000086D4 File Offset: 0x000068D4
		private byte[] Calculate()
		{
			SHA1Cng sha1Cng = new SHA1Cng();
			byte[] result;
			using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, sha1Cng, CryptoStreamMode.Write))
			{
				PasswordBlob currentPassword = this.pwdImpl.CurrentPassword;
				if (null != currentPassword)
				{
					cryptoStream.Write(currentPassword.Blob, 0, currentPassword.Blob.Length);
				}
				foreach (object obj in this.pwdImpl.OldPasswords)
				{
					PasswordBlob passwordBlob = (PasswordBlob)obj;
					cryptoStream.Write(passwordBlob.Blob, 0, passwordBlob.Blob.Length);
				}
				long utcTicks = this.pwdImpl.TimeSet.UtcTicks;
				byte[] bytes = BitConverter.GetBytes(utcTicks);
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.FlushFinalBlock();
				byte[] array = new byte[160];
				sha1Cng.Hash.CopyTo(array, 0);
				result = array;
			}
			return result;
		}

		// Token: 0x040000B4 RID: 180
		private ADUser adUser;

		// Token: 0x040000B5 RID: 181
		private IPassword pwdImpl;
	}
}
