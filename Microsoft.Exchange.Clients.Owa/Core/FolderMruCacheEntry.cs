using System;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000115 RID: 277
	internal sealed class FolderMruCacheEntry
	{
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x000420F8 File Offset: 0x000402F8
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x00042100 File Offset: 0x00040300
		public StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
			set
			{
				this.folderId = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00042109 File Offset: 0x00040309
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x00042111 File Offset: 0x00040311
		public long LastAccessedDateTimeTicks
		{
			get
			{
				return this.lastAccessedDateTimeTicks;
			}
			set
			{
				this.lastAccessedDateTimeTicks = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0004211A File Offset: 0x0004031A
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x00042122 File Offset: 0x00040322
		public int NumberOfSessionsSinceLastUse
		{
			get
			{
				return this.numberOfSessionsSinceLastUse;
			}
			set
			{
				this.numberOfSessionsSinceLastUse = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0004212B File Offset: 0x0004032B
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x00042133 File Offset: 0x00040333
		public bool UsedInCurrentSession
		{
			get
			{
				return this.usedInCurrentSession;
			}
			set
			{
				this.usedInCurrentSession = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0004213C File Offset: 0x0004033C
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x00042144 File Offset: 0x00040344
		public int UsageCount
		{
			get
			{
				return this.usageCount;
			}
			set
			{
				this.usageCount = value;
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0004214D File Offset: 0x0004034D
		public FolderMruCacheEntry()
		{
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00042155 File Offset: 0x00040355
		public FolderMruCacheEntry(StoreObjectId folderId)
		{
			if (folderId == null)
			{
				throw new ArgumentException("folderId cannot be null");
			}
			this.folderId = folderId;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00042174 File Offset: 0x00040374
		public void RenderEntryXml(XmlTextWriter xmlWriter, string mruEntryName)
		{
			if (xmlWriter == null)
			{
				throw new ArgumentException("xmlWriter cannot be null");
			}
			if (string.IsNullOrEmpty(mruEntryName))
			{
				throw new ArgumentException("mruEntryName cannot be null or empty");
			}
			xmlWriter.WriteStartElement(mruEntryName);
			if (this.folderId != null)
			{
				xmlWriter.WriteAttributeString("folderId", this.folderId.ToBase64String());
			}
			if (this.lastAccessedDateTimeTicks >= 0L)
			{
				xmlWriter.WriteAttributeString("lastAccessedDateTimeTicks", this.lastAccessedDateTimeTicks.ToString());
			}
			if (this.numberOfSessionsSinceLastUse >= 0)
			{
				xmlWriter.WriteAttributeString("numberOfSessionsSinceLastUse", this.numberOfSessionsSinceLastUse.ToString());
			}
			xmlWriter.WriteAttributeString("usedInCurrentSession", this.usedInCurrentSession.ToString());
			if (this.usageCount >= 0)
			{
				xmlWriter.WriteAttributeString("usageCount", this.usageCount.ToString());
			}
			xmlWriter.WriteFullEndElement();
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00042244 File Offset: 0x00040444
		public void ParseEntry(XmlTextReader reader, UserContext userContext)
		{
			if (reader == null)
			{
				throw new ArgumentException("reader cannot be null");
			}
			if (userContext == null)
			{
				throw new ArgumentException("userContext cannot be null");
			}
			try
			{
				if (reader.HasAttributes)
				{
					int i = 0;
					while (i < reader.AttributeCount)
					{
						reader.MoveToAttribute(i);
						string name;
						if ((name = reader.Name) == null)
						{
							goto IL_EA;
						}
						if (!(name == "folderId"))
						{
							if (!(name == "lastAccessedDateTimeTicks"))
							{
								if (!(name == "numberOfSessionsSinceLastUse"))
								{
									if (!(name == "usedInCurrentSession"))
									{
										if (!(name == "usageCount"))
										{
											goto IL_EA;
										}
										this.usageCount = int.Parse(reader.Value);
									}
									else
									{
										this.usedInCurrentSession = Convert.ToBoolean(reader.Value);
									}
								}
								else
								{
									this.numberOfSessionsSinceLastUse = int.Parse(reader.Value);
								}
							}
							else
							{
								this.lastAccessedDateTimeTicks = long.Parse(reader.Value);
							}
						}
						else
						{
							this.folderId = Utilities.CreateStoreObjectId(userContext.MailboxSession, reader.Value);
						}
						IL_F1:
						i++;
						continue;
						IL_EA:
						this.ThrowParserException(reader);
						goto IL_F1;
					}
					reader.MoveToElement();
				}
			}
			catch (FormatException)
			{
				this.ThrowParserException(reader);
			}
			catch (OverflowException)
			{
				this.ThrowParserException(reader);
			}
			catch (OwaInvalidIdFormatException)
			{
				this.ThrowParserException(reader);
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000423A4 File Offset: 0x000405A4
		private void ThrowParserException(XmlTextReader reader)
		{
			string text = null;
			string message = string.Format(CultureInfo.InvariantCulture, "Mru Cache. Invalid request. Line number: {0} Position: {1}.{2}", new object[]
			{
				reader.LineNumber.ToString(CultureInfo.InvariantCulture),
				reader.LinePosition.ToString(CultureInfo.InvariantCulture),
				(text != null) ? (" " + text) : string.Empty
			});
			throw new OwaFolderMruParserException(message, null, this);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00042418 File Offset: 0x00040618
		public void UpdateTimeStamp()
		{
			this.lastAccessedDateTimeTicks = DateTime.UtcNow.Ticks;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00042438 File Offset: 0x00040638
		public void SetInitialUsage()
		{
			this.usageCount = 6;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00042441 File Offset: 0x00040641
		public void DecayUsage(int decay)
		{
			if (this.usageCount != -1)
			{
				this.usageCount -= decay;
				if (this.usageCount < 0)
				{
					this.usageCount = 0;
				}
			}
		}

		// Token: 0x040006AB RID: 1707
		private const string FolderIdAttribute = "folderId";

		// Token: 0x040006AC RID: 1708
		private const string LastAccessedDateTimeTicksAttribute = "lastAccessedDateTimeTicks";

		// Token: 0x040006AD RID: 1709
		private const string NumberOfSessionsSinceLastUseAttribute = "numberOfSessionsSinceLastUse";

		// Token: 0x040006AE RID: 1710
		private const string UsedInCurrentSessionAttribute = "usedInCurrentSession";

		// Token: 0x040006AF RID: 1711
		private const string UsageCountAttribute = "usageCount";

		// Token: 0x040006B0 RID: 1712
		private StoreObjectId folderId;

		// Token: 0x040006B1 RID: 1713
		private long lastAccessedDateTimeTicks;

		// Token: 0x040006B2 RID: 1714
		private int numberOfSessionsSinceLastUse;

		// Token: 0x040006B3 RID: 1715
		private bool usedInCurrentSession;

		// Token: 0x040006B4 RID: 1716
		private int usageCount;
	}
}
