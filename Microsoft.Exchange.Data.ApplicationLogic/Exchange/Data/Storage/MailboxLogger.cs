using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200013F RID: 319
	internal sealed class MailboxLogger : DisposeTrackableBase, IEnumerable<TextReader>, IEnumerable
	{
		// Token: 0x06000D11 RID: 3345 RVA: 0x000369F9 File Offset: 0x00034BF9
		public MailboxLogger(MailboxSession mailboxSession, string protocolName) : this(mailboxSession, protocolName, null)
		{
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00036A04 File Offset: 0x00034C04
		public MailboxLogger(MailboxSession mailboxSession, string protocolName, string clientName)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (string.IsNullOrEmpty(protocolName))
			{
				throw new ArgumentNullException("protocolName");
			}
			this.Mailbox = mailboxSession;
			string displayName;
			if (string.IsNullOrEmpty(clientName))
			{
				displayName = protocolName;
			}
			else
			{
				displayName = protocolName + " " + clientName;
			}
			lock (this.Mailbox)
			{
				bool isConnected = this.Mailbox.IsConnected;
				try
				{
					if (!isConnected)
					{
						this.Mailbox.Connect();
					}
					using (Folder folder = Folder.Create(this.Mailbox, this.Mailbox.GetDefaultFolderId(DefaultFolderType.Configuration), StoreObjectType.Folder, displayName, CreateMode.OpenIfExists))
					{
						if (folder.Id == null)
						{
							folder.Save();
							folder.Load();
						}
						this.logFolderId = folder.Id.ObjectId;
						this.LogsExist = (folder.ItemCount > 0);
					}
				}
				catch (LocalizedException lastError)
				{
					this.LastError = lastError;
				}
				finally
				{
					if (!isConnected)
					{
						try
						{
							this.Mailbox.Disconnect();
						}
						catch (LocalizedException)
						{
						}
					}
				}
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x00036B54 File Offset: 0x00034D54
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x00036B5C File Offset: 0x00034D5C
		public MailboxSession Mailbox { get; set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00036B65 File Offset: 0x00034D65
		// (set) Token: 0x06000D16 RID: 3350 RVA: 0x00036B6D File Offset: 0x00034D6D
		public bool LogsExist { get; private set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00036B76 File Offset: 0x00034D76
		// (set) Token: 0x06000D18 RID: 3352 RVA: 0x00036B7E File Offset: 0x00034D7E
		public LocalizedException LastError { get; private set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00036B87 File Offset: 0x00034D87
		private byte[] TempBuffer
		{
			get
			{
				if (this.tempBuffer == null)
				{
					if (MailboxLogger.bufferPool == null)
					{
						MailboxLogger.bufferPool = new BufferPool(30720);
					}
					this.tempBuffer = MailboxLogger.bufferPool.Acquire();
				}
				return this.tempBuffer;
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00036BC0 File Offset: 0x00034DC0
		public void WriteLog(string logString)
		{
			if (logString == null)
			{
				throw new ArgumentNullException("logString");
			}
			if (this.LastError != null)
			{
				return;
			}
			lock (this.Mailbox)
			{
				byte[] array = null;
				if (Encoding.Unicode.GetByteCount(logString) <= this.TempBuffer.Length)
				{
					this.tempBufferDataSize = Encoding.Unicode.GetBytes(logString, 0, logString.Length, this.TempBuffer, 0);
				}
				else
				{
					array = Encoding.Unicode.GetBytes(logString);
				}
				this.streamType = MailboxLogger.StreamType.Unicode;
				this.InternalCreateNewLog(array, 0, (array == null) ? 0 : array.Length);
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00036C70 File Offset: 0x00034E70
		public void WriteLog(byte[] logBuffer)
		{
			if (logBuffer == null)
			{
				throw new ArgumentNullException("logBuffer");
			}
			if (this.LastError != null)
			{
				return;
			}
			this.WriteLog(logBuffer, 0, logBuffer.Length);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00036C94 File Offset: 0x00034E94
		public void WriteLog(byte[] logBuffer, int offset, int size)
		{
			if (logBuffer == null)
			{
				throw new ArgumentNullException("logBuffer");
			}
			if (offset < 0 || offset > logBuffer.Length)
			{
				throw new ArgumentException("offset");
			}
			if (size < 0 || offset + size > logBuffer.Length)
			{
				throw new ArgumentException("size");
			}
			if (this.LastError != null)
			{
				return;
			}
			lock (this.Mailbox)
			{
				this.streamType = MailboxLogger.StreamType.Ascii;
				this.InternalCreateNewLog(logBuffer, offset, size);
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00036D24 File Offset: 0x00034F24
		public void AppendLog(string logString)
		{
			if (logString == null)
			{
				throw new ArgumentNullException("logString");
			}
			if (this.LastError != null)
			{
				return;
			}
			Encoding encoding;
			switch (this.streamType)
			{
			case MailboxLogger.StreamType.Ascii:
				encoding = Encoding.ASCII;
				break;
			case MailboxLogger.StreamType.Unicode:
				encoding = Encoding.Unicode;
				break;
			default:
				throw new InvalidOperationException("Unknown streamType");
			}
			lock (this.Mailbox)
			{
				if (this.tempBufferDataSize + encoding.GetByteCount(logString) < this.TempBuffer.Length)
				{
					this.tempBufferDataSize += encoding.GetBytes(logString, 0, logString.Length, this.TempBuffer, this.tempBufferDataSize);
				}
				else
				{
					byte[] bytes = encoding.GetBytes(logString);
					this.InternalWrite(bytes, 0, bytes.Length);
				}
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00036E00 File Offset: 0x00035000
		public void AppendLog(byte[] logBuffer)
		{
			if (logBuffer == null)
			{
				throw new ArgumentNullException("logBuffer");
			}
			this.AppendLog(logBuffer, 0, logBuffer.Length);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00036E1C File Offset: 0x0003501C
		public void AppendLog(byte[] logBuffer, int offset, int size)
		{
			if (logBuffer == null)
			{
				throw new ArgumentNullException("logBuffer");
			}
			if (offset < 0 || offset > logBuffer.Length)
			{
				throw new ArgumentException("offset");
			}
			if (size < 0 || offset + size > logBuffer.Length)
			{
				throw new ArgumentException("size");
			}
			if (this.streamType != MailboxLogger.StreamType.Ascii)
			{
				throw new InvalidOperationException("The log was not opened as ASCII");
			}
			if (this.LastError != null)
			{
				return;
			}
			lock (this.Mailbox)
			{
				if (this.tempBufferDataSize + size < this.TempBuffer.Length)
				{
					Array.Copy(logBuffer, offset, this.TempBuffer, this.tempBufferDataSize, size);
					this.tempBufferDataSize += size;
				}
				else
				{
					this.InternalWrite(logBuffer, offset, size);
				}
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00036EEC File Offset: 0x000350EC
		public void Flush()
		{
			if (this.LastError != null)
			{
				return;
			}
			lock (this.Mailbox)
			{
				if (this.tempBufferDataSize != 0)
				{
					this.InternalWrite(null, 0, 0);
				}
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00036F44 File Offset: 0x00035144
		public void ClearOldLogs(int maxNumberOfLogs, long maxTotalLogSize)
		{
			if (maxNumberOfLogs < 0)
			{
				throw new ArgumentException(string.Format("maxNumberOfLogs value {0} is invalid", maxNumberOfLogs));
			}
			if (maxTotalLogSize < 0L)
			{
				throw new ArgumentException(string.Format("maxTotalLogSize value {0} is invalid", maxTotalLogSize));
			}
			if (this.LastError != null)
			{
				return;
			}
			ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(3890621757U, ref maxNumberOfLogs);
			lock (this.Mailbox)
			{
				bool isConnected = this.Mailbox.IsConnected;
				try
				{
					if (!isConnected)
					{
						this.Mailbox.Connect();
					}
					if (maxNumberOfLogs == 0 || maxTotalLogSize == 0L)
					{
						this.Mailbox.DeleteAllObjects(DeleteItemFlags.SoftDelete, this.logFolderId);
						this.LogsExist = false;
					}
					else
					{
						int num = 0;
						long num2 = 0L;
						using (Folder folder = Folder.Bind(this.Mailbox, this.logFolderId))
						{
							List<StoreObjectId> list = new List<StoreObjectId>(folder.ItemCount);
							using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, MailboxLogger.SortBySubjectD, MailboxLogger.DeleteQueryProperties))
							{
								object[][] rows;
								do
								{
									rows = queryResult.GetRows(10000);
									for (int i = 0; i < rows.Length; i++)
									{
										num++;
										num2 += (long)((int)rows[i][1]);
										if (num > maxNumberOfLogs / 2 || num2 > maxTotalLogSize / 2L)
										{
											list.Add(((VersionedId)rows[i][0]).ObjectId);
										}
									}
								}
								while (rows.Length != 0);
							}
							if (num > maxNumberOfLogs || num2 > maxTotalLogSize)
							{
								for (int j = 0; j < list.Count; j += 256)
								{
									int num3 = Math.Min(256, list.Count - j);
									this.Mailbox.Delete(DeleteItemFlags.SoftDelete, list.GetRange(j, num3).ToArray());
									num -= num3;
								}
							}
							this.LogsExist = (num > 0);
						}
					}
				}
				catch (LocalizedException lastError)
				{
					this.LastError = lastError;
				}
				finally
				{
					if (!isConnected)
					{
						try
						{
							this.Mailbox.Disconnect();
						}
						catch (LocalizedException)
						{
						}
					}
				}
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0003768C File Offset: 0x0003588C
		public IEnumerator<TextReader> GetEnumerator()
		{
			MailboxLogger.<GetEnumerator>d__8 <GetEnumerator>d__ = new MailboxLogger.<GetEnumerator>d__8(0);
			<GetEnumerator>d__.<>4__this = this;
			return <GetEnumerator>d__;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000376A8 File Offset: 0x000358A8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000376B0 File Offset: 0x000358B0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxLogger>(this);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x000376B8 File Offset: 0x000358B8
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.tempBuffer != null)
			{
				MailboxLogger.bufferPool.Release(this.tempBuffer);
				this.tempBuffer = null;
			}
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000376DC File Offset: 0x000358DC
		private void InternalWrite(Item logItem, bool createNew, byte[] newData, int offset, int size, byte[] preamble)
		{
			try
			{
				logItem.OpenAsReadWrite();
				PropertyOpenMode openMode = PropertyOpenMode.Modify;
				if (createNew)
				{
					openMode = PropertyOpenMode.Create;
				}
				using (Stream stream = logItem.OpenPropertyStream(ItemSchema.ProtocolLog, openMode))
				{
					if (!createNew)
					{
						stream.Seek(0L, SeekOrigin.End);
					}
					if (preamble != null)
					{
						stream.Write(preamble, 0, preamble.Length);
					}
					if (this.tempBufferDataSize > 0)
					{
						stream.Write(this.TempBuffer, 0, this.tempBufferDataSize);
						this.tempBufferDataSize = 0;
					}
					if (newData != null && size > 0)
					{
						stream.Write(newData, offset, size);
					}
				}
				logItem.Save(SaveMode.NoConflictResolution);
			}
			catch (ObjectNotFoundException)
			{
				this.InternalCreateNewLog(newData, offset, size);
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00037798 File Offset: 0x00035998
		private void InternalWrite(byte[] newData, int offset, int size)
		{
			if (this.currentLogId != null)
			{
				bool isConnected = this.Mailbox.IsConnected;
				try
				{
					try
					{
						if (!isConnected)
						{
							this.Mailbox.Connect();
						}
						using (Item item = Item.Bind(this.Mailbox, this.currentLogId))
						{
							this.InternalWrite(item, false, newData, offset, size, null);
						}
					}
					catch (LocalizedException lastError)
					{
						this.LastError = lastError;
					}
					return;
				}
				finally
				{
					if (!isConnected)
					{
						try
						{
							this.Mailbox.Disconnect();
						}
						catch (LocalizedException)
						{
						}
					}
				}
			}
			this.InternalCreateNewLog(newData, offset, size);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00037850 File Offset: 0x00035A50
		private void InternalCreateNewLog(byte[] newData, int offset, int size)
		{
			bool isConnected = this.Mailbox.IsConnected;
			try
			{
				if (!isConnected)
				{
					this.Mailbox.Connect();
				}
				string subject = ExDateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss.ffff", DateTimeFormatInfo.InvariantInfo);
				using (MessageItem messageItem = MessageItem.Create(this.Mailbox, this.logFolderId))
				{
					messageItem.Subject = subject;
					PolicyTagHelper.SetRetentionProperties(messageItem, ExDateTime.UtcNow.AddDays(5.0), 5);
					byte[] preamble = null;
					switch (this.streamType)
					{
					case MailboxLogger.StreamType.Ascii:
						preamble = Encoding.ASCII.GetPreamble();
						break;
					case MailboxLogger.StreamType.Unicode:
						preamble = Encoding.Unicode.GetPreamble();
						break;
					}
					this.InternalWrite(messageItem, true, newData, offset, size, preamble);
					messageItem.Load();
					this.currentLogId = messageItem.Id.ObjectId;
					this.LogsExist = true;
				}
			}
			catch (LocalizedException lastError)
			{
				this.LastError = lastError;
			}
			finally
			{
				if (!isConnected)
				{
					try
					{
						this.Mailbox.Disconnect();
					}
					catch (LocalizedException)
					{
					}
				}
			}
		}

		// Token: 0x040006F6 RID: 1782
		private const int LogRetentionInDays = 5;

		// Token: 0x040006F7 RID: 1783
		private const int MaxBulkSize = 256;

		// Token: 0x040006F8 RID: 1784
		private const int Id = 0;

		// Token: 0x040006F9 RID: 1785
		private const int Size = 1;

		// Token: 0x040006FA RID: 1786
		private static readonly SortBy[] SortBySubjectA = new SortBy[]
		{
			new SortBy(ItemSchema.Subject, SortOrder.Ascending)
		};

		// Token: 0x040006FB RID: 1787
		private static readonly SortBy[] SortBySubjectD = new SortBy[]
		{
			new SortBy(ItemSchema.Subject, SortOrder.Descending)
		};

		// Token: 0x040006FC RID: 1788
		private static readonly PropertyDefinition[] DeleteQueryProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.Size
		};

		// Token: 0x040006FD RID: 1789
		private static readonly PropertyDefinition[] ListQueryProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.ProtocolLog
		};

		// Token: 0x040006FE RID: 1790
		private static readonly PropertyDefinition[] LoadProperties = new PropertyDefinition[]
		{
			ItemSchema.ProtocolLog
		};

		// Token: 0x040006FF RID: 1791
		private static BufferPool bufferPool;

		// Token: 0x04000700 RID: 1792
		private StoreObjectId logFolderId;

		// Token: 0x04000701 RID: 1793
		private StoreObjectId currentLogId;

		// Token: 0x04000702 RID: 1794
		private MailboxLogger.StreamType streamType;

		// Token: 0x04000703 RID: 1795
		private byte[] tempBuffer;

		// Token: 0x04000704 RID: 1796
		private int tempBufferDataSize;

		// Token: 0x02000140 RID: 320
		private enum StreamType
		{
			// Token: 0x04000709 RID: 1801
			Ascii,
			// Token: 0x0400070A RID: 1802
			Unicode
		}
	}
}
