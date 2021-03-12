using System;
using System.Collections;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000190 RID: 400
	internal class XsoPasswordImpl : IPassword
	{
		// Token: 0x06000D3D RID: 3389 RVA: 0x000315BB File Offset: 0x0002F7BB
		internal XsoPasswordImpl(UMMailboxRecipient mailbox)
		{
			this.mailbox = mailbox;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x000315CC File Offset: 0x0002F7CC
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x00031605 File Offset: 0x0002F805
		public int LockoutCount
		{
			get
			{
				object obj = this.Dictionary["LockoutCount"];
				if (obj == null)
				{
					return 0;
				}
				if (!(obj is int))
				{
					this.DeleteCorruptedPassword();
					return 0;
				}
				return (int)obj;
			}
			set
			{
				this.Dictionary["LockoutCount"] = value;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00031620 File Offset: 0x0002F820
		// (set) Token: 0x06000D41 RID: 3393 RVA: 0x000316C8 File Offset: 0x0002F8C8
		public PasswordBlob CurrentPassword
		{
			get
			{
				if (null == this.cachedCurrentPwd)
				{
					object obj = this.Dictionary["Password"];
					if (obj == null)
					{
						return null;
					}
					if (!(obj is string))
					{
						this.DeleteCorruptedPassword();
						return null;
					}
					string s = (string)this.Dictionary["Password"];
					try
					{
						byte[] blobdata = Convert.FromBase64String(s);
						this.cachedCurrentPwd = new PasswordBlob(blobdata);
					}
					catch (FormatException)
					{
						this.DeleteCorruptedPassword();
						return null;
					}
					catch (UserConfigurationException)
					{
						this.DeleteCorruptedPassword();
						return null;
					}
				}
				return this.cachedCurrentPwd;
			}
			set
			{
				this.Dictionary["Password"] = Convert.ToBase64String(value.Blob);
				this.cachedCurrentPwd = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x000316EC File Offset: 0x0002F8EC
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x0003174F File Offset: 0x0002F94F
		public ExDateTime TimeSet
		{
			get
			{
				object obj = this.Dictionary["PasswordSetTime"];
				if (obj == null)
				{
					return ExDateTime.UtcNow.AddDays(-36501.0);
				}
				if (!(obj is ExDateTime))
				{
					this.DeleteCorruptedPassword();
					return ExDateTime.UtcNow.AddDays(-36501.0);
				}
				return (ExDateTime)obj;
			}
			set
			{
				this.Dictionary["PasswordSetTime"] = value;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00031768 File Offset: 0x0002F968
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x000317FC File Offset: 0x0002F9FC
		public ArrayList OldPasswords
		{
			get
			{
				if (this.cachedOldPwdList == null)
				{
					object obj = this.Dictionary["PreviousPasswords"];
					ArrayList arrayList = null;
					if (obj == null)
					{
						arrayList = new ArrayList();
					}
					else
					{
						if (!(obj is string))
						{
							this.DeleteCorruptedPassword();
							return null;
						}
						try
						{
							byte[] flat = Convert.FromBase64String((string)obj);
							arrayList = this.DeSerializeBlobs(flat);
						}
						catch (FormatException)
						{
							this.DeleteCorruptedPassword();
						}
						catch (UserConfigurationException)
						{
							this.DeleteCorruptedPassword();
						}
					}
					this.cachedOldPwdList = arrayList;
				}
				return this.cachedOldPwdList;
			}
			set
			{
				this.Dictionary["PreviousPasswords"] = Convert.ToBase64String(XsoPasswordImpl.SerializeBlobs(value));
				this.cachedOldPwdList = value;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00031820 File Offset: 0x0002FA20
		private string ConfigurationName
		{
			get
			{
				return "Um.Password";
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00031828 File Offset: 0x0002FA28
		private IDictionary Dictionary
		{
			get
			{
				if (this.dictionary == null)
				{
					try
					{
						this.dictionary = this.CopyFromUserConfig();
					}
					catch (CorruptDataException)
					{
						this.DeleteCorruptedPassword();
					}
					catch (InvalidOperationException)
					{
						this.DeleteCorruptedPassword();
					}
				}
				return this.dictionary;
			}
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00031880 File Offset: 0x0002FA80
		public void Commit()
		{
			this.CopyToUserConfig(this.Dictionary);
			this.cachedCurrentPwd = null;
			this.cachedOldPwdList = null;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0003189C File Offset: 0x0002FA9C
		private static byte[] SerializeBlobs(ArrayList blobs)
		{
			int num = 0;
			int num2 = 0;
			foreach (object obj in blobs)
			{
				PasswordBlob passwordBlob = (PasswordBlob)obj;
				num2 += passwordBlob.Blob.Length;
			}
			byte[] array = new byte[num2];
			foreach (object obj2 in blobs)
			{
				PasswordBlob passwordBlob2 = (PasswordBlob)obj2;
				byte[] blob = passwordBlob2.Blob;
				blob.CopyTo(array, num);
				num += blob.Length;
			}
			return array;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00031964 File Offset: 0x0002FB64
		private UserConfiguration GetConfig(MailboxSession session)
		{
			UserConfiguration result = null;
			try
			{
				result = session.UserConfigurationManager.GetMailboxConfiguration(this.ConfigurationName, UserConfigurationTypes.Dictionary);
			}
			catch (CorruptDataException)
			{
				this.DeleteCorruptedPassword();
			}
			catch (InvalidOperationException)
			{
				this.DeleteCorruptedPassword();
			}
			catch (ObjectNotFoundException)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, "Password file doesn't exist...creating.", new object[0]);
				result = session.UserConfigurationManager.CreateMailboxConfiguration(this.ConfigurationName, UserConfigurationTypes.Dictionary);
			}
			return result;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x000319F0 File Offset: 0x0002FBF0
		private ArrayList DeSerializeBlobs(byte[] flat)
		{
			ArrayList arrayList = new ArrayList();
			int i;
			PasswordBlob passwordBlob;
			for (i = 0; i < flat.Length; i += passwordBlob.Blob.Length)
			{
				passwordBlob = new PasswordBlob(flat, i);
				arrayList.Add(passwordBlob);
			}
			if (i != flat.Length)
			{
				throw new UserConfigurationException(Strings.CorruptedPasswordField(this.mailbox.ToString()));
			}
			return arrayList;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00031A4C File Offset: 0x0002FC4C
		private void DeleteCorruptedPassword()
		{
			PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, this.mailbox.MailAddress);
			CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, data, "Found a corrupted password file for user=_EmailAddress! Deleting!.", new object[0]);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CorruptedPIN, null, new object[]
			{
				this.mailbox
			});
			UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.mailbox.CreateSessionLock();
			try
			{
				mailboxSessionLock.Session.UserConfigurationManager.DeleteMailboxConfigurations(new string[]
				{
					this.ConfigurationName
				});
				throw new UserConfigurationException(Strings.CorruptedPIN(this.mailbox.MailAddress));
			}
			finally
			{
				if (mailboxSessionLock != null)
				{
					((IDisposable)mailboxSessionLock).Dispose();
					goto IL_9E;
				}
				goto IL_9E;
				IL_9E:;
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00031B08 File Offset: 0x0002FD08
		private IDictionary CopyFromUserConfig()
		{
			Hashtable hashtable = new Hashtable();
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.mailbox.CreateSessionLock())
			{
				using (UserConfiguration config = this.GetConfig(mailboxSessionLock.Session))
				{
					IDictionary dictionary = config.GetDictionary();
					foreach (object key in dictionary.Keys)
					{
						hashtable[key] = dictionary[key];
					}
				}
			}
			return hashtable;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00031BC4 File Offset: 0x0002FDC4
		private void CopyToUserConfig(IDictionary srcDictionary)
		{
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.mailbox.CreateSessionLock())
			{
				using (UserConfiguration config = this.GetConfig(mailboxSessionLock.Session))
				{
					IDictionary dictionary = config.GetDictionary();
					foreach (object key in srcDictionary.Keys)
					{
						dictionary[key] = srcDictionary[key];
					}
					config.Save();
				}
			}
		}

		// Token: 0x040006D5 RID: 1749
		private UMMailboxRecipient mailbox;

		// Token: 0x040006D6 RID: 1750
		private IDictionary dictionary;

		// Token: 0x040006D7 RID: 1751
		private PasswordBlob cachedCurrentPwd;

		// Token: 0x040006D8 RID: 1752
		private ArrayList cachedOldPwdList;
	}
}
