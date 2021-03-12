using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.InfoWorker.Common.TopN
{
	// Token: 0x0200027B RID: 635
	internal class TopNConfiguration
	{
		// Token: 0x0600122A RID: 4650 RVA: 0x000549BD File Offset: 0x00052BBD
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "TopNConfiguration for user " + this.mailboxSession.MailboxOwner + ". ";
			}
			return this.toString;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x000549ED File Offset: 0x00052BED
		internal TopNConfiguration(MailboxSession mailboxSession)
		{
			this.mailboxSession = mailboxSession;
			this.lastScanTime = ExDateTime.MinValue;
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00054A07 File Offset: 0x00052C07
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00054A0F File Offset: 0x00052C0F
		// (set) Token: 0x0600122E RID: 4654 RVA: 0x00054A17 File Offset: 0x00052C17
		internal int Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x00054A20 File Offset: 0x00052C20
		// (set) Token: 0x06001230 RID: 4656 RVA: 0x00054A28 File Offset: 0x00052C28
		internal ExDateTime LastScanTime
		{
			get
			{
				return this.lastScanTime;
			}
			set
			{
				this.lastScanTime = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x00054A31 File Offset: 0x00052C31
		// (set) Token: 0x06001232 RID: 4658 RVA: 0x00054A39 File Offset: 0x00052C39
		internal bool ScanRequested
		{
			get
			{
				return this.scanRequested;
			}
			set
			{
				this.scanRequested = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x00054A42 File Offset: 0x00052C42
		// (set) Token: 0x06001234 RID: 4660 RVA: 0x00054A4A File Offset: 0x00052C4A
		internal KeyValuePair<string, int>[] WordFrequency
		{
			get
			{
				return this.wordFrequency;
			}
			set
			{
				this.wordFrequency = value;
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00054A54 File Offset: 0x00052C54
		internal bool ReadWordFrequencyMap()
		{
			bool result;
			using (UserConfiguration userConfiguration = this.OpenMessage(true))
			{
				if (userConfiguration == null)
				{
					TopNConfiguration.Tracer.TraceError<TopNConfiguration>((long)this.GetHashCode(), "{0}: FAI could not be opened or created.", this);
					result = false;
				}
				else
				{
					using (Stream stream = userConfiguration.GetStream())
					{
						Exception ex = null;
						try
						{
							Type[] allowList = new Type[]
							{
								typeof(KeyValuePair<string, int>)
							};
							this.wordFrequency = (KeyValuePair<string, int>[])SafeSerialization.SafeBinaryFormatterDeserializeWithAllowList(stream, allowList, null);
						}
						catch (SafeSerialization.BlockedTypeException ex2)
						{
							ex = ex2;
						}
						catch (ArgumentNullException ex3)
						{
							ex = ex3;
						}
						catch (SerializationException ex4)
						{
							ex = ex4;
						}
						catch (Exception ex5)
						{
							ex = ex5;
						}
						if (ex != null)
						{
							TopNConfiguration.Tracer.TraceError<TopNConfiguration, Exception>((long)this.GetHashCode(), "{0}: FAI message is corrupt. Exception: {1}", this, ex);
							result = false;
						}
						else
						{
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00054B68 File Offset: 0x00052D68
		internal bool ReadMetaData()
		{
			bool result;
			using (UserConfiguration userConfiguration = this.OpenMessage(true))
			{
				if (userConfiguration == null)
				{
					TopNConfiguration.Tracer.TraceError<TopNConfiguration>((long)this.GetHashCode(), "{0}: FAI could not be opened or created.", this);
					result = false;
				}
				else
				{
					IDictionary dictionary = userConfiguration.GetDictionary();
					if (dictionary == null)
					{
						TopNConfiguration.Tracer.TraceError<TopNConfiguration>((long)this.GetHashCode(), "{0}: No meta data in FAI item. Item may be corrupt.", this);
						result = false;
					}
					else
					{
						Exception ex = null;
						try
						{
							if (dictionary["Version"] is string)
							{
								this.version = (int)dictionary["Version"];
							}
							if (dictionary["LastScanTime"] is ExDateTime)
							{
								this.lastScanTime = (ExDateTime)dictionary["LastScanTime"];
							}
							if (!this.IsMailboxExtendedPropertySupported())
							{
								if (dictionary["ScanRequested"] is bool)
								{
									this.scanRequested = (bool)dictionary["ScanRequested"];
								}
							}
							else
							{
								object mailboxExtendedProperty = this.GetMailboxExtendedProperty();
								if (mailboxExtendedProperty is PropertyError)
								{
									TopNConfiguration.Tracer.TraceError<TopNConfiguration, object>((long)this.GetHashCode(), "{0}: Extended Mailbox property returned property error {1}", this, mailboxExtendedProperty);
									return false;
								}
								this.ScanRequested = (bool)mailboxExtendedProperty;
							}
						}
						catch (CorruptDataException ex2)
						{
							ex = ex2;
						}
						if (ex != null)
						{
							TopNConfiguration.Tracer.TraceError<TopNConfiguration, Exception>((long)this.GetHashCode(), "{0}: FAI message is corrupt. Exception: {1}", this, ex);
							result = false;
						}
						else
						{
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00054CF4 File Offset: 0x00052EF4
		internal bool Save(bool onlyMetaData)
		{
			bool result;
			using (UserConfiguration userConfiguration = this.OpenMessage(true))
			{
				if (userConfiguration == null)
				{
					TopNConfiguration.Tracer.TraceError<TopNConfiguration>((long)this.GetHashCode(), "{0}: Save() failed because FAI could not be opened or created.", this);
					result = false;
				}
				else
				{
					IDictionary dictionary = userConfiguration.GetDictionary();
					dictionary["Version"] = this.version;
					dictionary["LastScanTime"] = this.lastScanTime;
					if (!this.IsMailboxExtendedPropertySupported())
					{
						dictionary["ScanRequested"] = this.scanRequested;
					}
					if (onlyMetaData)
					{
						result = this.SaveMessage(userConfiguration);
					}
					else
					{
						using (Stream stream = userConfiguration.GetStream())
						{
							IFormatter formatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
							Exception ex = null;
							try
							{
								formatter.Serialize(stream, this.wordFrequency);
							}
							catch (ArgumentNullException ex2)
							{
								ex = ex2;
							}
							catch (SerializationException ex3)
							{
								ex = ex3;
							}
							if (ex != null)
							{
								TopNConfiguration.Tracer.TraceError<TopNConfiguration, Exception>((long)this.GetHashCode(), "{0}: Failed to serialize word frequency data. Exception: {1}", this, ex);
								return false;
							}
						}
						result = this.SaveMessage(userConfiguration);
					}
				}
			}
			return result;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00054E34 File Offset: 0x00053034
		internal void Delete()
		{
			StoreId defaultFolderId = this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			this.mailboxSession.UserConfigurationManager.DeleteFolderConfigurations(defaultFolderId, new string[]
			{
				"TopNWords.Data"
			});
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00054E70 File Offset: 0x00053070
		private UserConfiguration OpenMessage(bool createIfMissingOrCorrupt)
		{
			UserConfiguration userConfiguration = null;
			StoreId defaultFolderId = this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			Exception ex = null;
			try
			{
				userConfiguration = this.mailboxSession.UserConfigurationManager.GetFolderConfiguration("TopNWords.Data", UserConfigurationTypes.Stream | UserConfigurationTypes.Dictionary, defaultFolderId);
			}
			catch (ObjectNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (CorruptDataException ex3)
			{
				ex = ex3;
			}
			if (userConfiguration == null)
			{
				TopNConfiguration.Tracer.TraceDebug<string, Exception>(0L, "FAI message '{0}' is missing or corrupt. Exception: {1}", "TopNWords.Data", ex);
				if (createIfMissingOrCorrupt)
				{
					if (ex is CorruptDataException)
					{
						this.mailboxSession.UserConfigurationManager.DeleteFolderConfigurations(defaultFolderId, new string[]
						{
							"TopNWords.Data"
						});
					}
					userConfiguration = this.mailboxSession.UserConfigurationManager.CreateFolderConfiguration("TopNWords.Data", UserConfigurationTypes.Stream | UserConfigurationTypes.Dictionary, defaultFolderId);
				}
			}
			return userConfiguration;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00054F34 File Offset: 0x00053134
		private bool SaveMessage(UserConfiguration config)
		{
			Exception ex = null;
			try
			{
				config.Save();
				if (this.IsMailboxExtendedPropertySupported())
				{
					this.SetMailboxExtendedProperty();
				}
			}
			catch (ObjectExistedException ex2)
			{
				ex = ex2;
			}
			catch (SaveConflictException ex3)
			{
				ex = ex3;
			}
			catch (QuotaExceededException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				TopNConfiguration.Tracer.TraceError<TopNConfiguration, Exception>((long)this.GetHashCode(), "{0}: could not be saved. Exception: {1}", this, ex);
				return false;
			}
			return true;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00054FB0 File Offset: 0x000531B0
		private void SetMailboxExtendedProperty()
		{
			object mailboxExtendedProperty = this.GetMailboxExtendedProperty();
			if (mailboxExtendedProperty is PropertyError || (bool)mailboxExtendedProperty != this.ScanRequested)
			{
				this.MailboxSession.Mailbox[MailboxSchema.IsTopNEnabled] = this.ScanRequested;
				this.MailboxSession.Mailbox.Save();
				this.MailboxSession.Mailbox.Load();
			}
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0005501C File Offset: 0x0005321C
		private object GetMailboxExtendedProperty()
		{
			this.MailboxSession.Mailbox.Load(new PropertyDefinition[]
			{
				MailboxSchema.IsTopNEnabled
			});
			return this.MailboxSession.Mailbox.TryGetProperty(MailboxSchema.IsTopNEnabled);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00055060 File Offset: 0x00053260
		private bool IsMailboxExtendedPropertySupported()
		{
			ServerVersion serverVersion = new ServerVersion(this.mailboxSession.MailboxOwner.MailboxInfo.Location.ServerVersion);
			return serverVersion.Major > 14 || (serverVersion.Major == 14 && serverVersion.Minor > 1) || (serverVersion.Major == 14 && serverVersion.Minor == 1 && serverVersion.Build > 116);
		}

		// Token: 0x04000BE8 RID: 3048
		internal const string MessageClass = "TopNWords.Data";

		// Token: 0x04000BE9 RID: 3049
		internal const UserConfigurationTypes Types = UserConfigurationTypes.Stream | UserConfigurationTypes.Dictionary;

		// Token: 0x04000BEA RID: 3050
		internal const string VersionKey = "Version";

		// Token: 0x04000BEB RID: 3051
		internal const string LastScanTimeKey = "LastScanTime";

		// Token: 0x04000BEC RID: 3052
		internal const string ScanRequestedKey = "ScanRequested";

		// Token: 0x04000BED RID: 3053
		private const int MailboxMajorVersion = 14;

		// Token: 0x04000BEE RID: 3054
		private const int MailboxMinorVersion = 1;

		// Token: 0x04000BEF RID: 3055
		private const int MailboxBuildNumber = 116;

		// Token: 0x04000BF0 RID: 3056
		internal static readonly TimeSpan UpdateInterval = TimeSpan.FromDays(30.0);

		// Token: 0x04000BF1 RID: 3057
		private MailboxSession mailboxSession;

		// Token: 0x04000BF2 RID: 3058
		private int version;

		// Token: 0x04000BF3 RID: 3059
		private ExDateTime lastScanTime;

		// Token: 0x04000BF4 RID: 3060
		private bool scanRequested;

		// Token: 0x04000BF5 RID: 3061
		private KeyValuePair<string, int>[] wordFrequency;

		// Token: 0x04000BF6 RID: 3062
		private string toString;

		// Token: 0x04000BF7 RID: 3063
		protected static readonly Trace Tracer = ExTraceGlobals.TopNTracer;
	}
}
