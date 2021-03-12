using System;
using System.Collections;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000055 RID: 85
	internal sealed class XsoConfigurationFolder : IConfigurationFolder, IPromptCounter
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0000C10C File Offset: 0x0000A30C
		internal XsoConfigurationFolder(UMMailboxRecipient mailbox)
		{
			this.mailbox = mailbox;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000C11B File Offset: 0x0000A31B
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000C129 File Offset: 0x0000A329
		public bool IsOof
		{
			get
			{
				return this.GetConfigValue("OofStatus", false);
			}
			set
			{
				this.Dictionary["OofStatus"] = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000C141 File Offset: 0x0000A341
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000C150 File Offset: 0x0000A350
		public MailboxGreetingEnum CurrentMailboxGreetingType
		{
			get
			{
				if (!this.IsOof)
				{
					return MailboxGreetingEnum.Voicemail;
				}
				return MailboxGreetingEnum.Away;
			}
			set
			{
				try
				{
					this.IsOof = (value == MailboxGreetingEnum.Away);
					this.Save();
				}
				catch (StorageTransientException innerException)
				{
					throw new UserConfigurationException(Strings.TransientlyUnableToAccessUserConfiguration(this.mailbox.MailAddress), innerException);
				}
				catch (StoragePermanentException innerException2)
				{
					throw new UserConfigurationException(Strings.PermanentlyUnableToAccessUserConfiguration(this.mailbox.MailAddress), innerException2);
				}
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000C241 File Offset: 0x0000A441
		public string PlayOnPhoneDialString
		{
			get
			{
				object obj = this.Dictionary["PlayOnPhoneDialString"];
				if (obj == null)
				{
					obj = this.mailbox.ADRecipient.UMExtension;
				}
				else if (!(obj is string))
				{
					this.DeleteCorruptedConfiguration();
					obj = this.mailbox.ADRecipient.UMExtension;
				}
				else if (string.IsNullOrEmpty((string)obj))
				{
					obj = this.mailbox.ADRecipient.UMExtension;
				}
				return (string)obj;
			}
			set
			{
				this.Dictionary["PlayOnPhoneDialString"] = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000C254 File Offset: 0x0000A454
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000C368 File Offset: 0x0000A568
		public string TelephoneAccessFolderEmail
		{
			get
			{
				string result;
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.mailbox.CreateSessionLock())
				{
					object obj = this.Dictionary["TelephoneAccessFolderEmail"];
					bool flag = false;
					if (obj == null)
					{
						flag = true;
					}
					else if (!(obj is string))
					{
						this.DeleteCorruptedConfiguration();
						flag = true;
					}
					else if (string.IsNullOrEmpty((string)obj))
					{
						flag = true;
					}
					else
					{
						byte[] entryId = Convert.FromBase64String((string)obj);
						StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(entryId);
						try
						{
							using (Folder.Bind(mailboxSessionLock.Session, storeObjectId))
							{
							}
						}
						catch (ObjectNotFoundException ex)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this, "TelephoneAccessFolderEmail is invalid (id = '{0}'). This could be because it was deleted by the user. Exception: {1}", new object[]
							{
								storeObjectId,
								ex
							});
							flag = true;
						}
					}
					if (flag)
					{
						StoreObjectId defaultFolderId = mailboxSessionLock.Session.GetDefaultFolderId(DefaultFolderType.Inbox);
						obj = Convert.ToBase64String(defaultFolderId.ProviderLevelItemId);
					}
					result = (string)obj;
				}
				return result;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.mailbox.CreateSessionLock())
					{
						byte[] entryId = Convert.FromBase64String(value);
						StoreObjectId folderId = StoreObjectId.FromProviderSpecificId(entryId);
						using (Folder.Bind(mailboxSessionLock.Session, folderId, null))
						{
							this.Dictionary["TelephoneAccessFolderEmail"] = value;
						}
						return;
					}
				}
				this.Dictionary["TelephoneAccessFolderEmail"] = null;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000C42A File Offset: 0x0000A62A
		public VoiceNotificationStatus VoiceNotificationStatus
		{
			get
			{
				object obj = this.Dictionary["VoiceNotificationStatus"];
				if (obj == null)
				{
					obj = VoiceNotificationStatus.EnabledByDefault;
				}
				return (VoiceNotificationStatus)obj;
			}
			set
			{
				this.Dictionary["VoiceNotificationStatus"] = (int)value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000C442 File Offset: 0x0000A642
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000C450 File Offset: 0x0000A650
		public bool UseAsr
		{
			get
			{
				return this.GetConfigValue("UseAsr", true);
			}
			set
			{
				this.Dictionary["UseAsr"] = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000C468 File Offset: 0x0000A668
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000C476 File Offset: 0x0000A676
		public bool ReceivedVoiceMailPreviewEnabled
		{
			get
			{
				return this.GetConfigValue("ReceivedVoiceMailPreviewEnabled", true);
			}
			set
			{
				this.Dictionary["ReceivedVoiceMailPreviewEnabled"] = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000C48E File Offset: 0x0000A68E
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0000C49C File Offset: 0x0000A69C
		public bool SentVoiceMailPreviewEnabled
		{
			get
			{
				return this.GetConfigValue("SentVoiceMailPreviewEnabled", true);
			}
			set
			{
				this.Dictionary["SentVoiceMailPreviewEnabled"] = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000C4C2 File Offset: 0x0000A6C2
		public bool ReadUnreadVoicemailInFIFOOrder
		{
			get
			{
				return this.GetConfigValue("ReadUnreadVoicemailInFIFOOrder", false);
			}
			set
			{
				this.Dictionary["ReadUnreadVoicemailInFIFOOrder"] = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000C4DC File Offset: 0x0000A6DC
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000C52A File Offset: 0x0000A72A
		public MultiValuedProperty<string> BlockedNumbers
		{
			get
			{
				object obj = this.Dictionary["BlockedNumbers"];
				string[] value;
				if (obj == null)
				{
					value = new string[0];
				}
				else if (!(obj is string[]))
				{
					value = new string[0];
					this.DeleteCorruptedConfiguration();
				}
				else
				{
					value = (string[])obj;
				}
				return new MultiValuedProperty<string>(value);
			}
			set
			{
				this.Dictionary["BlockedNumbers"] = ((value != null) ? value.ToArray() : null);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000C548 File Offset: 0x0000A748
		// (set) Token: 0x06000343 RID: 835 RVA: 0x0000C556 File Offset: 0x0000A756
		public bool IsFirstTimeUser
		{
			get
			{
				return this.GetConfigValue("FirstTimeUser", true);
			}
			set
			{
				this.Dictionary["FirstTimeUser"] = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000C570 File Offset: 0x0000A770
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
						this.DeleteCorruptedConfiguration();
						this.dictionary = this.CopyFromUserConfig();
					}
					catch (InvalidOperationException)
					{
						this.DeleteCorruptedConfiguration();
						this.dictionary = this.CopyFromUserConfig();
					}
				}
				return this.dictionary;
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		public GreetingBase OpenNameGreeting()
		{
			if (this.adRecipient == null)
			{
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromADRecipient(this.mailbox.ADRecipient, false);
				this.adRecipient = iadrecipientLookup.LookupByObjectId(this.mailbox.ADRecipient.Id);
			}
			object[] args = new object[]
			{
				this.adRecipient,
				"RecordedName"
			};
			return (GreetingBase)Activator.CreateInstance(XsoConfigurationFolder.CoreTypeLoader.AdGreetingType, BindingFlags.Instance | BindingFlags.NonPublic, null, args, null);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C654 File Offset: 0x0000A854
		public GreetingBase OpenCustomMailboxGreeting(MailboxGreetingEnum gt)
		{
			object[] args = new object[]
			{
				this.mailbox,
				this.GetCustomMailboxGreetingConfigName(gt)
			};
			return (GreetingBase)Activator.CreateInstance(XsoConfigurationFolder.CoreTypeLoader.XsoGreetingType, BindingFlags.Instance | BindingFlags.NonPublic, null, args, null);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000C691 File Offset: 0x0000A891
		public IPassword OpenPassword()
		{
			return new XsoPasswordImpl(this.mailbox);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
		public bool HasCustomMailboxGreeting(MailboxGreetingEnum g)
		{
			bool result;
			try
			{
				result = this.HasNonEmptyStreamConfig("Um.CustomGreetings." + this.GetCustomMailboxGreetingConfigName(g));
			}
			catch (StorageTransientException innerException)
			{
				throw new UserConfigurationException(Strings.TransientlyUnableToAccessUserConfiguration(this.mailbox.MailAddress), innerException);
			}
			catch (StoragePermanentException innerException2)
			{
				throw new UserConfigurationException(Strings.PermanentlyUnableToAccessUserConfiguration(this.mailbox.MailAddress), innerException2);
			}
			return result;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000C720 File Offset: 0x0000A920
		public void RemoveCustomMailboxGreeting(MailboxGreetingEnum g)
		{
			if (!this.DeleteConfiguration("Um.CustomGreetings." + this.GetCustomMailboxGreetingConfigName(g)))
			{
				throw new UserConfigurationException(Strings.UnableToRemoveCustomGreeting(this.mailbox.MailAddress));
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000C756 File Offset: 0x0000A956
		public void Save()
		{
			this.CopyToUserConfig(this.Dictionary);
			this.promptsDirty = false;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000C76C File Offset: 0x0000A96C
		public void SetPromptCount(string promptId, int newCount)
		{
			try
			{
				this.Dictionary["PromptCount_" + promptId] = newCount;
				this.promptsDirty = true;
			}
			catch (LocalizedException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this, "SetPromptCount ignoring LocalizedException le={0}.", new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000C7D4 File Offset: 0x0000A9D4
		public int GetPromptCount(string promptId)
		{
			object obj = this.Dictionary["PromptCount_" + promptId];
			if (obj == null)
			{
				return 0;
			}
			if (!(obj is int))
			{
				this.DeleteCorruptedConfiguration();
				return 0;
			}
			return (int)obj;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000C814 File Offset: 0x0000AA14
		public void SavePromptCount()
		{
			try
			{
				if (this.promptsDirty)
				{
					this.Save();
				}
			}
			catch (StorageTransientException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this, "SavePromptCount Ignoring storage transient exception: {0}.", new object[]
				{
					ex.Message
				});
			}
			catch (QuotaExceededException ex2)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this, "SavePromptCount Ignoring QuotaExceededException: {0}.", new object[]
				{
					ex2.Message
				});
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000C898 File Offset: 0x0000AA98
		private string GetCustomMailboxGreetingConfigName(MailboxGreetingEnum gt)
		{
			switch (gt)
			{
			case MailboxGreetingEnum.Voicemail:
				return "External";
			case MailboxGreetingEnum.Away:
				return "Oof";
			default:
				ExAssert.RetailAssert(false, "Invalid Greeting type {0}", new object[]
				{
					gt.GetType().Name
				});
				return string.Empty;
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000C8F0 File Offset: 0x0000AAF0
		private bool DeleteConfiguration(string configName)
		{
			bool result;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.mailbox.CreateSessionLock())
			{
				OperationResult operationResult = mailboxSessionLock.Session.UserConfigurationManager.DeleteMailboxConfigurations(new string[]
				{
					configName
				});
				result = (operationResult == OperationResult.Succeeded);
			}
			return result;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000C948 File Offset: 0x0000AB48
		private void DeleteCorruptedConfiguration()
		{
			PIIMessage data = PIIMessage.Create(PIIType._SmtpAddress, this.mailbox.MailAddress);
			CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this, data, "Found a corrupted configuration file for user=_SmtpAddress! Deleting!", new object[0]);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CorruptedConfiguration, null, new object[]
			{
				this.mailbox
			});
			this.dictionary = null;
			if (!this.DeleteConfiguration("Um.General"))
			{
				throw new UserConfigurationException(Strings.CorruptedConfigurationCouldNotBeDeleted(this.mailbox.MailAddress));
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000C9CF File Offset: 0x0000ABCF
		private UserConfiguration RebuildConfiguration(MailboxSession s)
		{
			this.DeleteCorruptedConfiguration();
			return s.UserConfigurationManager.CreateMailboxConfiguration("Um.General", UserConfigurationTypes.Dictionary);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		private bool HasNonEmptyStreamConfig(string configName)
		{
			bool result;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.mailbox.CreateSessionLock())
			{
				try
				{
					using (UserConfiguration mailboxConfiguration = mailboxSessionLock.Session.UserConfigurationManager.GetMailboxConfiguration(configName, UserConfigurationTypes.Stream))
					{
						using (Stream stream = mailboxConfiguration.GetStream())
						{
							result = (stream.Length != 0L);
						}
					}
				}
				catch (ObjectNotFoundException)
				{
					result = false;
				}
				catch (CorruptDataException)
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000CA94 File Offset: 0x0000AC94
		private UserConfiguration GetConfig(MailboxSession s)
		{
			UserConfiguration result = null;
			try
			{
				result = s.UserConfigurationManager.GetMailboxConfiguration("Um.General", UserConfigurationTypes.Dictionary);
			}
			catch (ObjectNotFoundException)
			{
				PIIMessage data = PIIMessage.Create(PIIType._SmtpAddress, this.mailbox.MailAddress);
				CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this, data, "Creating UM General configuration folder for user: _SmtpAddress.", new object[]
				{
					this.mailbox.MailAddress
				});
				result = s.UserConfigurationManager.CreateMailboxConfiguration("Um.General", UserConfigurationTypes.Dictionary);
			}
			catch (CorruptDataException)
			{
				result = this.RebuildConfiguration(s);
			}
			catch (InvalidOperationException)
			{
				result = this.RebuildConfiguration(s);
			}
			return result;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000CB44 File Offset: 0x0000AD44
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

		// Token: 0x06000355 RID: 853 RVA: 0x0000CC00 File Offset: 0x0000AE00
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

		// Token: 0x06000356 RID: 854 RVA: 0x0000CCB8 File Offset: 0x0000AEB8
		private bool GetConfigValue(string key, bool defaultValue)
		{
			object obj = this.Dictionary[key];
			if (obj == null)
			{
				return defaultValue;
			}
			if (!(obj is bool))
			{
				this.DeleteCorruptedConfiguration();
				return defaultValue;
			}
			return (bool)obj;
		}

		// Token: 0x04000276 RID: 630
		private UMMailboxRecipient mailbox;

		// Token: 0x04000277 RID: 631
		private IDictionary dictionary;

		// Token: 0x04000278 RID: 632
		private ADRecipient adRecipient;

		// Token: 0x04000279 RID: 633
		private bool promptsDirty;

		// Token: 0x02000056 RID: 86
		internal static class CoreTypeLoader
		{
			// Token: 0x06000357 RID: 855 RVA: 0x0000CCED File Offset: 0x0000AEED
			private static Assembly GetGreetingAssembly()
			{
				return Assembly.LoadFrom(Path.Combine(Utils.GetExchangeBinPath(), "Microsoft.Exchange.UM.UMCore.dll"));
			}

			// Token: 0x0400027A RID: 634
			private const string CoreAssemblyName = "Microsoft.Exchange.UM.UMCore.dll";

			// Token: 0x0400027B RID: 635
			private const string XsoGreetingName = "Microsoft.Exchange.UM.UMCore.XsoGreeting";

			// Token: 0x0400027C RID: 636
			private const string AdGreetingName = "Microsoft.Exchange.UM.UMCore.ADGreeting";

			// Token: 0x0400027D RID: 637
			private const string GlobCfgName = "Microsoft.Exchange.UM.UMCore.GlobCfg";

			// Token: 0x0400027E RID: 638
			internal static readonly Assembly GreetingAssembly = XsoConfigurationFolder.CoreTypeLoader.GetGreetingAssembly();

			// Token: 0x0400027F RID: 639
			internal static readonly Type XsoGreetingType = XsoConfigurationFolder.CoreTypeLoader.GreetingAssembly.GetType("Microsoft.Exchange.UM.UMCore.XsoGreeting", true);

			// Token: 0x04000280 RID: 640
			internal static readonly Type AdGreetingType = XsoConfigurationFolder.CoreTypeLoader.GreetingAssembly.GetType("Microsoft.Exchange.UM.UMCore.ADGreeting", true);

			// Token: 0x04000281 RID: 641
			internal static readonly Type GlobCfgType = XsoConfigurationFolder.CoreTypeLoader.GreetingAssembly.GetType("Microsoft.Exchange.UM.UMCore.GlobCfg", true);
		}
	}
}
