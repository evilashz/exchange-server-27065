using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001A2 RID: 418
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RecipientInfoCache : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000FC2 RID: 4034 RVA: 0x00040744 File Offset: 0x0003E944
		private RecipientInfoCache(MailboxSession mailboxSession, string configurationCacheName)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession is null!");
			}
			if (string.IsNullOrEmpty(configurationCacheName))
			{
				throw new ArgumentException("configurationCacheName is null or empty!");
			}
			this.configurationCacheName = configurationCacheName;
			this.GetOrOpenConfiguration(mailboxSession);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0004079D File Offset: 0x0003E99D
		private RecipientInfoCache(UserConfiguration userConfiguration)
		{
			if (userConfiguration == null)
			{
				throw new ArgumentNullException("userConfiguration is null!");
			}
			this.backendUserConfiguration = userConfiguration;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x000407D1 File Offset: 0x0003E9D1
		public StoreObjectId ItemId
		{
			get
			{
				this.CheckDisposed("get_ItemId");
				return this.backendUserConfiguration.Id;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x000407E9 File Offset: 0x0003E9E9
		public ExDateTime LastModifiedTime
		{
			get
			{
				this.CheckDisposed("get_LastModifiedTime");
				return this.lastModifiedTime;
			}
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000407FC File Offset: 0x0003E9FC
		public static RecipientInfoCache Create(MailboxSession mailboxSession, string configurationCacheName)
		{
			return new RecipientInfoCache(mailboxSession, configurationCacheName);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00040805 File Offset: 0x0003EA05
		public static RecipientInfoCache Create(UserConfiguration userConfiguration)
		{
			return new RecipientInfoCache(userConfiguration);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00040810 File Offset: 0x0003EA10
		public List<RecipientInfoCacheEntry> Load(string parentXmlNodeName)
		{
			this.CheckDisposed("Load");
			if (string.IsNullOrEmpty(parentXmlNodeName))
			{
				throw new ArgumentException("parentXmlNodeName is null or Empty");
			}
			List<RecipientInfoCacheEntry> result;
			using (Stream xmlStream = this.backendUserConfiguration.GetXmlStream())
			{
				this.lastModifiedTime = this.backendUserConfiguration.LastModifiedTime;
				result = RecipientInfoCache.Deserialize(xmlStream, parentXmlNodeName, out this.backendCacheVersion);
			}
			return result;
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00040884 File Offset: 0x0003EA84
		public void Save(List<RecipientInfoCacheEntry> entries, string parentNodeName, int cacheSize)
		{
			this.CheckDisposed("Save");
			if (entries == null)
			{
				throw new ArgumentNullException("entries");
			}
			if (string.IsNullOrEmpty(parentNodeName))
			{
				throw new ArgumentException("parentNodeName is null or Empty");
			}
			if (cacheSize <= 0)
			{
				throw new ArgumentException("cacheSize must be greater than zero");
			}
			using (Stream xmlStream = this.backendUserConfiguration.GetXmlStream())
			{
				RecipientInfoCache.Serialize(xmlStream, entries, parentNodeName, cacheSize);
			}
			try
			{
				this.backendUserConfiguration.Save();
			}
			catch (ObjectNotFoundException ex)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, "RecipientInfoCache.Commit: Failed. Exception: {0}", ex.Message);
			}
			catch (QuotaExceededException ex2)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, "RecipientInfoCache.Commit: Failed. Exception: {0}", ex2.Message);
			}
			catch (SaveConflictException ex3)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, "RecipientInfoCache.Commit: Failed. Exception: {0}", ex3.Message);
			}
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0004097C File Offset: 0x0003EB7C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0004098C File Offset: 0x0003EB8C
		public UserConfiguration DetachUserConfiguration()
		{
			this.CheckDisposed("DetachUserConfiguration");
			UserConfiguration result = this.backendUserConfiguration;
			this.backendUserConfiguration = null;
			return result;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000409B3 File Offset: 0x0003EBB3
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RecipientInfoCache>(this);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x000409BB File Offset: 0x0003EBBB
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x000409D0 File Offset: 0x0003EBD0
		private static List<RecipientInfoCacheEntry> Deserialize(Stream stream, string parentNodeName, out int backendCacheVersion)
		{
			backendCacheVersion = 0;
			List<RecipientInfoCacheEntry> result;
			using (XmlReader xmlReader = XmlReader.Create(stream, new XmlReaderSettings
			{
				CloseInput = false,
				CheckCharacters = false,
				IgnoreWhitespace = false
			}))
			{
				List<RecipientInfoCacheEntry> list = new List<RecipientInfoCacheEntry>(100);
				try
				{
					if (xmlReader.Read())
					{
						if (!string.Equals(xmlReader.Name, parentNodeName, StringComparison.OrdinalIgnoreCase))
						{
							throw new CorruptDataException(ServerStrings.InvalidTagName(parentNodeName, xmlReader.Name));
						}
						if (xmlReader.HasAttributes)
						{
							string s = xmlReader["version"];
							if (!int.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out backendCacheVersion))
							{
								throw new CorruptDataException(ServerStrings.VersionNotInteger);
							}
							if (backendCacheVersion < 3)
							{
								return new List<RecipientInfoCacheEntry>(0);
							}
						}
						while (xmlReader.Read() && (xmlReader.NodeType != XmlNodeType.EndElement || !string.Equals(parentNodeName, xmlReader.Name, StringComparison.OrdinalIgnoreCase)))
						{
							list.Add(RecipientInfoCacheEntry.ParseEntry(xmlReader));
						}
						if (xmlReader.Read())
						{
							throw new CorruptDataException(ServerStrings.UnexpectedTag(xmlReader.Name));
						}
					}
				}
				catch (XmlException ex)
				{
					ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, "RecipientInfoCache.Deserialize: Failed. Exception: {0}", ex.Message);
					throw new CorruptDataException(ServerStrings.InvalidXml, ex);
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00040B14 File Offset: 0x0003ED14
		private static void Serialize(Stream xmlStream, List<RecipientInfoCacheEntry> entries, string parentNodeName, int cacheSize)
		{
			if (entries.Count > cacheSize)
			{
				entries.Sort();
				entries = entries.GetRange(entries.Count - cacheSize, cacheSize);
			}
			xmlStream.SetLength(0L);
			xmlStream.Position = 0L;
			using (XmlWriter xmlWriter = XmlWriter.Create(xmlStream, new XmlWriterSettings
			{
				CloseOutput = false,
				OmitXmlDeclaration = true,
				CheckCharacters = false
			}))
			{
				xmlWriter.WriteStartElement(parentNodeName);
				xmlWriter.WriteAttributeString("version", 3.ToString(CultureInfo.InvariantCulture));
				foreach (RecipientInfoCacheEntry recipientInfoCacheEntry in entries)
				{
					recipientInfoCacheEntry.Serialize(xmlWriter);
				}
				xmlWriter.WriteFullEndElement();
			}
			xmlStream.Flush();
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00040BFC File Offset: 0x0003EDFC
		private void GetOrOpenConfiguration(MailboxSession mailboxSession)
		{
			UserConfigurationManager userConfigurationManager = mailboxSession.UserConfigurationManager;
			if (userConfigurationManager == null)
			{
				userConfigurationManager = new UserConfigurationManager(mailboxSession);
			}
			try
			{
				this.backendUserConfiguration = userConfigurationManager.GetMailboxConfiguration(this.configurationCacheName, UserConfigurationTypes.XML);
			}
			catch (ObjectNotFoundException)
			{
				this.backendUserConfiguration = userConfigurationManager.CreateMailboxConfiguration(this.configurationCacheName, UserConfigurationTypes.XML);
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00040C58 File Offset: 0x0003EE58
		private void CheckDisposed(string methodName)
		{
			if (this.disposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00040C7C File Offset: 0x0003EE7C
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.disposed, disposing);
			if (!this.disposed)
			{
				this.disposed = true;
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
				if (this.backendUserConfiguration != null)
				{
					this.backendUserConfiguration.Dispose();
					this.backendUserConfiguration = null;
				}
			}
		}

		// Token: 0x04000869 RID: 2153
		public const string AutoCompleteCacheConfigurationName = "OWA.AutocompleteCache";

		// Token: 0x0400086A RID: 2154
		public const short AutoCompleteCacheCacheSize = 100;

		// Token: 0x0400086B RID: 2155
		public const string AutoCompleteNodeName = "AutoCompleteCache";

		// Token: 0x0400086C RID: 2156
		private const string AutoCompleteCacheVersionName = "version";

		// Token: 0x0400086D RID: 2157
		private const int CacheVersion = 3;

		// Token: 0x0400086E RID: 2158
		private int backendCacheVersion;

		// Token: 0x0400086F RID: 2159
		private UserConfiguration backendUserConfiguration;

		// Token: 0x04000870 RID: 2160
		private string configurationCacheName;

		// Token: 0x04000871 RID: 2161
		private bool disposed;

		// Token: 0x04000872 RID: 2162
		private DisposeTracker disposeTracker;

		// Token: 0x04000873 RID: 2163
		private ExDateTime lastModifiedTime = ExDateTime.MinValue;
	}
}
