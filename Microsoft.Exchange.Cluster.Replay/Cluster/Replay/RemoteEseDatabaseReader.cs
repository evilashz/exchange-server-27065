using System;
using System.Globalization;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.EseRepl;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000130 RID: 304
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class RemoteEseDatabaseReader : IEseDatabaseReader, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x000331F5 File Offset: 0x000313F5
		public RemoteEseDatabaseReader(string serverName, Guid databaseGuid, string databaseName, string databasePath)
		{
			this.m_disposeTracker = this.GetDisposeTracker();
			this.m_serverName = serverName;
			this.m_pageSize = 425984807L;
			this.m_databaseGuid = databaseGuid;
			this.m_databaseName = databaseName;
			this.m_databasePath = databasePath;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00033232 File Offset: 0x00031432
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RemoteEseDatabaseReader>(this);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0003323A File Offset: 0x0003143A
		public void SuppressDisposeTracker()
		{
			if (this.m_disposeTracker != null)
			{
				this.m_disposeTracker.Suppress();
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0003324F File Offset: 0x0003144F
		public void Dispose()
		{
			if (!this.m_isDisposed)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00033268 File Offset: 0x00031468
		public void Dispose(bool disposing)
		{
			lock (this)
			{
				if (!this.m_isDisposed)
				{
					if (disposing)
					{
						if (this.m_disposeTracker != null)
						{
							this.m_disposeTracker.Dispose();
						}
						this.CloseChannel();
					}
					this.m_isDisposed = true;
				}
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000332C8 File Offset: 0x000314C8
		public void ForceNewLog()
		{
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug((long)this.GetHashCode(), "rolling a log file");
			Exception ex = null;
			try
			{
				this.GetChannel();
				SeedPageReaderRollLogFileRequest seedPageReaderRollLogFileRequest = new SeedPageReaderRollLogFileRequest(this.m_channel, this.m_databaseGuid, this.m_databaseName, this.m_databasePath);
				seedPageReaderRollLogFileRequest.Send();
				NetworkChannelMessage message = this.m_channel.GetMessage();
				if (!(message is SeedPageReaderRollLogFileReply))
				{
					this.m_channel.ThrowUnexpectedMessage(message);
				}
			}
			catch (NetworkRemoteException ex2)
			{
				ex = ex2;
			}
			catch (NetworkTransportException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ReplayEventLogConstants.Tuple_ForceNewLogError.LogEvent(string.Empty, new object[]
				{
					this.m_databaseName,
					this.m_serverName,
					ex
				});
				throw ex;
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0003339C File Offset: 0x0003159C
		public long ReadPageSize()
		{
			this.m_pageSize = 3L;
			ExTraceGlobals.IncrementalReseederTracer.TraceDebug((long)this.GetHashCode(), "fetching the page size");
			Exception ex = null;
			try
			{
				this.GetChannel();
				SeedPageReaderPageSizeRequest seedPageReaderPageSizeRequest = new SeedPageReaderPageSizeRequest(this.m_channel, this.m_databaseGuid, this.m_databaseName, this.m_databasePath);
				seedPageReaderPageSizeRequest.Send();
				NetworkChannelMessage message = this.m_channel.GetMessage();
				SeedPageReaderPageSizeReply seedPageReaderPageSizeReply = message as SeedPageReaderPageSizeReply;
				if (seedPageReaderPageSizeReply == null)
				{
					this.m_channel.ThrowUnexpectedMessage(message);
				}
				this.m_pageSize = seedPageReaderPageSizeReply.PageSize;
			}
			catch (NetworkRemoteException ex2)
			{
				ex = ex2;
			}
			catch (NetworkTransportException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ReplayEventLogConstants.Tuple_ReadPageSizeError.LogEvent(string.Empty, new object[]
				{
					this.m_databaseName,
					this.m_serverName,
					ex
				});
				throw ex;
			}
			return this.m_pageSize;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0003348C File Offset: 0x0003168C
		public byte[] ReadOnePage(long pageNumber, out long lowGen, out long highGen)
		{
			byte[] result = null;
			if (pageNumber < 1L)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, "pageNumber is {0}, must be >= 1 ", new object[]
				{
					pageNumber
				}));
			}
			lowGen = (long)((ulong)-1);
			highGen = 0L;
			try
			{
				this.GetChannel();
				SeedPageReaderSinglePageRequest seedPageReaderSinglePageRequest = new SeedPageReaderSinglePageRequest(this.m_channel, this.m_databaseGuid, this.m_databaseName, this.m_databasePath, (uint)pageNumber);
				seedPageReaderSinglePageRequest.Send();
				NetworkChannelMessage message = this.m_channel.GetMessage();
				SeedPageReaderSinglePageReply seedPageReaderSinglePageReply = message as SeedPageReaderSinglePageReply;
				if (seedPageReaderSinglePageReply == null)
				{
					this.m_channel.ThrowUnexpectedMessage(message);
				}
				this.m_pageSize = (long)seedPageReaderSinglePageReply.PageBytes.Length;
				lowGen = seedPageReaderSinglePageReply.LowGeneration;
				highGen = seedPageReaderSinglePageReply.HighGeneration;
				result = seedPageReaderSinglePageReply.PageBytes;
			}
			catch (NetworkRemoteException ex)
			{
				ReplayEventLogConstants.Tuple_ReadOnePageError.LogEvent(string.Empty, new object[]
				{
					pageNumber,
					this.m_databaseName,
					this.m_serverName,
					ex
				});
				throw;
			}
			catch (NetworkTransportException ex2)
			{
				ReplayEventLogConstants.Tuple_ReadOnePageError.LogEvent(string.Empty, new object[]
				{
					pageNumber,
					this.m_databaseName,
					this.m_serverName,
					ex2
				});
				throw;
			}
			return result;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000335EC File Offset: 0x000317EC
		public void StartSendingPages(long[] pageList)
		{
			DiagCore.RetailAssert(this.m_pageList == null, "single use only", new object[0]);
			this.m_pageList = pageList;
			this.GetChannel();
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.SendPageNumbersToSource));
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00033628 File Offset: 0x00031828
		private void SendPageNumbersToSource(object dummy)
		{
			Exception ex = null;
			IOBuffer iobuffer = null;
			int num = this.m_pageList.Length;
			try
			{
				this.GetChannel();
				SeedPageReaderMultiplePageRequest seedPageReaderMultiplePageRequest = new SeedPageReaderMultiplePageRequest(this.m_channel, this.m_databaseGuid, this.m_databaseName, this.m_databasePath, (long)this.m_pageList.Length);
				seedPageReaderMultiplePageRequest.Send();
				iobuffer = IOBufferPool.Allocate();
				iobuffer.AppendOffset = 0;
				foreach (long num2 in this.m_pageList)
				{
					if (iobuffer.RemainingSpace >= 4)
					{
						DiagCore.RetailAssert(num2 <= 2147483647L, "ese uses 31bit page numbers but {0} was passed", new object[]
						{
							num2
						});
						ExBitConverter.Write((uint)num2, iobuffer.Buffer, iobuffer.AppendOffset);
						iobuffer.AppendOffset += 4;
					}
					else
					{
						this.m_channel.Write(iobuffer.Buffer, 0, iobuffer.AppendOffset);
						iobuffer.AppendOffset = 0;
					}
				}
				if (iobuffer.AppendOffset > 0)
				{
					this.m_channel.Write(iobuffer.Buffer, 0, iobuffer.AppendOffset);
				}
			}
			catch (NetworkRemoteException ex2)
			{
				ex = ex2;
			}
			catch (NetworkTransportException ex3)
			{
				ex = ex3;
			}
			finally
			{
				this.m_pageList = null;
				if (iobuffer != null)
				{
					IOBufferPool.Free(iobuffer);
				}
			}
			if (ex != null)
			{
				ReplayCrimsonEvents.SendPageNumbersToSourceFailed.Log<Guid, string, int, Exception>(this.m_databaseGuid, this.DatabaseName, num, ex);
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000337D0 File Offset: 0x000319D0
		public byte[] ReadNextPage(long expectedPageNum, out long lowGen, out long highGen)
		{
			if (expectedPageNum < 1L)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, "pageNumber is {0}, must be >= 1 ", new object[]
				{
					expectedPageNum
				}));
			}
			lowGen = (long)((ulong)-1);
			highGen = 0L;
			NetworkChannelMessage message = this.m_channel.GetMessage();
			SeedPageReaderSinglePageReply seedPageReaderSinglePageReply = message as SeedPageReaderSinglePageReply;
			if (seedPageReaderSinglePageReply == null)
			{
				this.m_channel.ThrowUnexpectedMessage(message);
			}
			if (seedPageReaderSinglePageReply.PageNumber != expectedPageNum)
			{
				this.m_channel.ThrowUnexpectedMessage(message);
			}
			lowGen = seedPageReaderSinglePageReply.LowGeneration;
			highGen = seedPageReaderSinglePageReply.HighGeneration;
			return seedPageReaderSinglePageReply.PageBytes;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00033860 File Offset: 0x00031A60
		private void GetChannel()
		{
			if (this.m_channel == null)
			{
				NetworkPath netPath = NetworkManager.ChooseNetworkPath(this.m_serverName, null, NetworkPath.ConnectionPurpose.Seeding);
				this.m_channel = NetworkChannel.Connect(netPath, TcpChannel.GetDefaultTimeoutInMs(), false);
			}
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00033895 File Offset: 0x00031A95
		private void CloseChannel()
		{
			if (this.m_channel != null)
			{
				this.m_channel.Close();
				this.m_channel = null;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x000338B1 File Offset: 0x00031AB1
		public long PageSize
		{
			get
			{
				return this.m_pageSize;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x000338B9 File Offset: 0x00031AB9
		public string DatabaseName
		{
			get
			{
				return this.m_databaseName;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x000338C1 File Offset: 0x00031AC1
		public Guid DatabaseGuid
		{
			get
			{
				return this.m_databaseGuid;
			}
		}

		// Token: 0x040004D2 RID: 1234
		private DisposeTracker m_disposeTracker;

		// Token: 0x040004D3 RID: 1235
		private bool m_isDisposed;

		// Token: 0x040004D4 RID: 1236
		private NetworkChannel m_channel;

		// Token: 0x040004D5 RID: 1237
		private string m_serverName;

		// Token: 0x040004D6 RID: 1238
		private Guid m_databaseGuid;

		// Token: 0x040004D7 RID: 1239
		private long m_pageSize;

		// Token: 0x040004D8 RID: 1240
		private string m_databaseName;

		// Token: 0x040004D9 RID: 1241
		private string m_databasePath;

		// Token: 0x040004DA RID: 1242
		private long[] m_pageList;
	}
}
