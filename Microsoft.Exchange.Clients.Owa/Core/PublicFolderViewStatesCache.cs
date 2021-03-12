using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000226 RID: 550
	internal class PublicFolderViewStatesCache
	{
		// Token: 0x0600128A RID: 4746 RVA: 0x00070B9F File Offset: 0x0006ED9F
		private PublicFolderViewStatesCache(UserContext userContext)
		{
			this.cacheEntries = new Dictionary<string, PublicFolderViewStatesEntry>();
			this.userContext = userContext;
			this.Load();
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00070BBF File Offset: 0x0006EDBF
		internal static PublicFolderViewStatesCache GetInstance(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (userContext.PublicFolderViewStatesCache == null && userContext.PublicFolderViewStatesCache == null)
			{
				userContext.PublicFolderViewStatesCache = new PublicFolderViewStatesCache(userContext);
			}
			return userContext.PublicFolderViewStatesCache;
		}

		// Token: 0x17000513 RID: 1299
		internal PublicFolderViewStatesEntry this[string folderId]
		{
			get
			{
				if (this.CacheEntryExists(folderId))
				{
					return this.cacheEntries[folderId];
				}
				return null;
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00070C0C File Offset: 0x0006EE0C
		private void RenderXml(TextWriter writer)
		{
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(writer))
			{
				xmlTextWriter.WriteStartElement("PublicFolderViewStates");
				PublicFolderViewStatesEntry[] array = new PublicFolderViewStatesEntry[this.cacheEntries.Count];
				this.cacheEntries.Values.CopyTo(array, 0);
				Array.Sort<PublicFolderViewStatesEntry>(array, new PublicFolderViewStatesEntry.LastAccessDateTimeTicksComparer());
				int num = 0;
				while (num < array.Length && num < 50)
				{
					array[num].RenderEntryXml(xmlTextWriter, "Entry");
					num++;
				}
				xmlTextWriter.WriteFullEndElement();
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00070C9C File Offset: 0x0006EE9C
		internal void ClearCache()
		{
			this.cacheEntries.Clear();
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00070CA9 File Offset: 0x0006EEA9
		internal void AddEntry(string folderId, PublicFolderViewStatesEntry newEntry)
		{
			while (this.cacheEntries.Count >= 50 && this.RemoveTheOldestEntry())
			{
			}
			this.cacheEntries.Add(folderId, newEntry);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00070CD4 File Offset: 0x0006EED4
		private bool RemoveTheOldestEntry()
		{
			PublicFolderViewStatesEntry publicFolderViewStatesEntry = null;
			foreach (PublicFolderViewStatesEntry publicFolderViewStatesEntry2 in this.cacheEntries.Values)
			{
				if (publicFolderViewStatesEntry == null || publicFolderViewStatesEntry.LastAccessedDateTimeTicks > publicFolderViewStatesEntry2.LastAccessedDateTimeTicks)
				{
					publicFolderViewStatesEntry = publicFolderViewStatesEntry2;
				}
			}
			if (publicFolderViewStatesEntry != null)
			{
				this.cacheEntries.Remove(publicFolderViewStatesEntry.FolderId);
				return true;
			}
			return false;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00070D54 File Offset: 0x0006EF54
		private UserConfiguration GetUserConfiguration()
		{
			UserConfiguration userConfiguration = null;
			try
			{
				userConfiguration = this.userContext.MailboxSession.UserConfigurationManager.GetMailboxConfiguration("OWA.PublicFolderViewStates", UserConfigurationTypes.XML);
			}
			catch (ObjectNotFoundException)
			{
				userConfiguration = this.userContext.MailboxSession.UserConfigurationManager.CreateMailboxConfiguration("OWA.PublicFolderViewStates", UserConfigurationTypes.XML);
				try
				{
					userConfiguration.Save();
				}
				catch (QuotaExceededException ex)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "PublicFolderViewStatesCache.GetUserConfiguration: Failed. Exception: {0}", ex.Message);
				}
				catch (SaveConflictException ex2)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "PublicFolderViewStatesCache.GetUserConfiguration: Failed. Exception: {0}", ex2.Message);
				}
			}
			return userConfiguration;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00070E08 File Offset: 0x0006F008
		internal void Commit()
		{
			this.LoadAndMerge();
			this.InternalCommit();
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00070E18 File Offset: 0x0006F018
		private void InternalCommit()
		{
			using (UserConfiguration userConfiguration = this.GetUserConfiguration())
			{
				using (Stream xmlStream = userConfiguration.GetXmlStream())
				{
					xmlStream.SetLength(0L);
					using (StreamWriter streamWriter = new StreamWriter(xmlStream))
					{
						this.RenderXml(streamWriter);
					}
				}
				try
				{
					userConfiguration.Save();
				}
				catch (ObjectNotFoundException ex)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "PublicFolderViewStatesCache.Commit: Failed. Exception: {0}", ex.Message);
				}
				catch (QuotaExceededException ex2)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "PublicFolderViewStatesCache.Commit: Failed. Exception: {0}", ex2.Message);
				}
				catch (SaveConflictException ex3)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "PublicFolderViewStatesCache.Commit: Failed. Exception: {0}", ex3.Message);
				}
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00070F14 File Offset: 0x0006F114
		internal void Load()
		{
			this.ClearCache();
			this.LoadAndMerge();
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00070F24 File Offset: 0x0006F124
		private void LoadAndMerge()
		{
			using (UserConfiguration userConfiguration = this.GetUserConfiguration())
			{
				using (Stream xmlStream = userConfiguration.GetXmlStream())
				{
					if (xmlStream != null && xmlStream.Length > 0L)
					{
						using (XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(xmlStream))
						{
							this.Parse(xmlTextReader);
							goto IL_3B;
						}
					}
					this.ClearCache();
					IL_3B:;
				}
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00070FAC File Offset: 0x0006F1AC
		internal bool CacheEntryExists(string folderId)
		{
			return this.cacheEntries.ContainsKey(folderId);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00070FBC File Offset: 0x0006F1BC
		private void Parse(XmlTextReader reader)
		{
			try
			{
				reader.WhitespaceHandling = WhitespaceHandling.All;
				this.state = PublicFolderViewStatesCache.XmlParseState.Start;
				while (this.state != PublicFolderViewStatesCache.XmlParseState.Finished && reader.Read())
				{
					switch (this.state)
					{
					case PublicFolderViewStatesCache.XmlParseState.Start:
						this.ParseStart(reader);
						break;
					case PublicFolderViewStatesCache.XmlParseState.Root:
						this.ParseRoot(reader);
						break;
					case PublicFolderViewStatesCache.XmlParseState.Child:
						this.ParseChild(reader);
						break;
					}
				}
			}
			catch (XmlException)
			{
				this.ClearCache();
				this.InternalCommit();
			}
			catch (OwaPublicFolderViewStatesParseException)
			{
				this.ClearCache();
				this.InternalCommit();
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0007105C File Offset: 0x0006F25C
		private void ParseStart(XmlTextReader reader)
		{
			if (XmlNodeType.Element != reader.NodeType || !string.Equals("PublicFolderViewStates", reader.Name, StringComparison.OrdinalIgnoreCase))
			{
				this.ThrowParserException(reader);
				return;
			}
			if (reader.IsEmptyElement)
			{
				this.state = PublicFolderViewStatesCache.XmlParseState.Finished;
				return;
			}
			this.state = PublicFolderViewStatesCache.XmlParseState.Root;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0007109C File Offset: 0x0006F29C
		private void ParseRoot(XmlTextReader reader)
		{
			if (reader.NodeType == XmlNodeType.Element)
			{
				if (reader.IsEmptyElement)
				{
					this.ThrowParserException(reader);
					return;
				}
				if (!string.Equals("Entry", reader.Name, StringComparison.OrdinalIgnoreCase))
				{
					this.ThrowParserException(reader);
					return;
				}
				if (this.cacheEntries.Count < 100)
				{
					PublicFolderViewStatesEntry publicFolderViewStatesEntry = new PublicFolderViewStatesEntry();
					publicFolderViewStatesEntry.ParseEntry(reader);
					PublicFolderViewStatesEntry publicFolderViewStatesEntry2 = this[publicFolderViewStatesEntry.FolderId];
					if (publicFolderViewStatesEntry2 != null)
					{
						if (!publicFolderViewStatesEntry2.Dirty)
						{
							long lastAccessedDateTimeTicks = publicFolderViewStatesEntry2.LastAccessedDateTimeTicks;
							if (publicFolderViewStatesEntry.LastAccessedDateTimeTicks < lastAccessedDateTimeTicks)
							{
								publicFolderViewStatesEntry.LastAccessedDateTimeTicks = lastAccessedDateTimeTicks;
							}
							this.cacheEntries[publicFolderViewStatesEntry.FolderId] = publicFolderViewStatesEntry;
						}
					}
					else
					{
						this.cacheEntries.Add(publicFolderViewStatesEntry.FolderId, publicFolderViewStatesEntry);
					}
					this.state = PublicFolderViewStatesCache.XmlParseState.Child;
					return;
				}
			}
			else
			{
				if (reader.NodeType == XmlNodeType.EndElement && string.Equals("PublicFolderViewStates", reader.Name, StringComparison.OrdinalIgnoreCase))
				{
					this.state = PublicFolderViewStatesCache.XmlParseState.Finished;
					return;
				}
				this.ThrowParserException(reader);
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00071188 File Offset: 0x0006F388
		private void ParseChild(XmlTextReader reader)
		{
			if (reader.NodeType == XmlNodeType.EndElement && string.Equals(reader.Name, "Entry", StringComparison.OrdinalIgnoreCase))
			{
				this.state = PublicFolderViewStatesCache.XmlParseState.Root;
				return;
			}
			this.ThrowParserException(reader);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000711B8 File Offset: 0x0006F3B8
		private void ThrowParserException(XmlTextReader reader)
		{
			throw new OwaPublicFolderViewStatesParseException(string.Format(CultureInfo.InvariantCulture, "Invalid request. Line number: {0} Position: {1}.{2}", new object[]
			{
				reader.LineNumber.ToString(CultureInfo.InvariantCulture),
				reader.LinePosition.ToString(CultureInfo.InvariantCulture),
				"Parsing PublicFolderViewStates error."
			}), null, this);
		}

		// Token: 0x04000CAF RID: 3247
		private const string PublicFolderViewStatesParamName = "PublicFolderViewStates";

		// Token: 0x04000CB0 RID: 3248
		private const string PublicFolderViewStatesEntryParamName = "Entry";

		// Token: 0x04000CB1 RID: 3249
		private const int MaxSavedEntryNumber = 50;

		// Token: 0x04000CB2 RID: 3250
		private const int MaxMergedEntryNumber = 100;

		// Token: 0x04000CB3 RID: 3251
		private const string ConfigurationName = "OWA.PublicFolderViewStates";

		// Token: 0x04000CB4 RID: 3252
		private UserContext userContext;

		// Token: 0x04000CB5 RID: 3253
		private Dictionary<string, PublicFolderViewStatesEntry> cacheEntries;

		// Token: 0x04000CB6 RID: 3254
		private PublicFolderViewStatesCache.XmlParseState state;

		// Token: 0x02000227 RID: 551
		private enum XmlParseState
		{
			// Token: 0x04000CB8 RID: 3256
			Start,
			// Token: 0x04000CB9 RID: 3257
			Root,
			// Token: 0x04000CBA RID: 3258
			Child,
			// Token: 0x04000CBB RID: 3259
			Finished
		}
	}
}
