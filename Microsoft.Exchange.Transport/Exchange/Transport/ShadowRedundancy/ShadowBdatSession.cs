using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000390 RID: 912
	internal class ShadowBdatSession : ShadowSession
	{
		// Token: 0x06002853 RID: 10323 RVA: 0x0009D52F File Offset: 0x0009B72F
		public ShadowBdatSession(ISmtpInSession inSession, ShadowRedundancyManager shadowRedundancyManager, ShadowHubPickerBase hubPicker, ISmtpOutConnectionHandler connectionHandler) : base(inSession, shadowRedundancyManager, hubPicker, connectionHandler)
		{
			this.commandQueue = new Queue<ShadowBdatSession.BdatCommandRecord>();
			this.writeQueue = new Queue<WriteRecord>();
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x0009D554 File Offset: 0x0009B754
		public override void PrepareForNewCommand(BaseDataSmtpCommand newCommand)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowBdatSession.PrepareForNewCommand");
			base.DropBreadcrumb(ShadowBreadcrumbs.PrepareForCommand);
			BdatSmtpCommand bdatSmtpCommand = newCommand as BdatSmtpCommand;
			if (bdatSmtpCommand == null)
			{
				throw new ArgumentException("newCommand must be of type BdatSmtpCommand");
			}
			if (this.proxyLayer == null)
			{
				throw new InvalidOperationException("BeginWrite called without calling BeginOpen");
			}
			if (bdatSmtpCommand.IsBlob)
			{
				throw new InvalidOperationException("Shadow session does not support transferring of Messagecontext information");
			}
			lock (this.syncObject)
			{
				ShadowBdatSession.BdatCommandRecord bdatCommandRecord = new ShadowBdatSession.BdatCommandRecord(bdatSmtpCommand.ChunkSize, bdatSmtpCommand.IsLastChunk);
				if (this.currentCommand == null)
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowBdatSession directly passing command to proxy layer");
					this.RegisterCommand(bdatCommandRecord);
				}
				else
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<long, bool>((long)this.GetHashCode(), "ShadowBdatSession enqueuing command: {0} isLast:{1}", bdatCommandRecord.ChunkSize, bdatCommandRecord.IsLastChunk);
					base.DropBreadcrumb(ShadowBreadcrumbs.WriteQueuingCommand);
					this.commandQueue.Enqueue(bdatCommandRecord);
				}
			}
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x0009D654 File Offset: 0x0009B854
		protected override void LoadNewProxyLayer()
		{
			this.proxyLayer = new InboundBdatProxyLayer(this.inSession.SessionId, this.inSession.ClientEndPoint, this.inSession.HelloDomain, this.inSession.AdvertisedEhloOptions, 0U, this.inSession.TransportMailItem, true, new INextHopServer[0], ByteQuantifiedSize.FromMB(1UL).ToBytes(), this.inSession.LogSession, this.inSession.SmtpInServer.SmtpOutConnectionHandler, false, Permission.None, AuthenticationSource.Anonymous, this.inSession.SmtpInServer.InboundProxyDestinationTracker);
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x0009D6E8 File Offset: 0x0009B8E8
		protected override void WriteInternal(byte[] buffer, int offset, int count, bool seenEod)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowBdatSession.WriteInternal");
			base.DropBreadcrumb(ShadowBreadcrumbs.WriteInternal);
			if (this.currentCommand == null)
			{
				throw new InvalidOperationException("WriteInternal called before PrepareForNewCommand");
			}
			lock (this.syncObject)
			{
				if (this.commandQueue.Count == 0 && !base.HasPendingProxyCallback)
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowBdatSession directly writing to shadow");
					base.WriteToProxy(buffer, offset, count, seenEod, true);
				}
				else
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowBdatSession enqueuing write");
					base.DropBreadcrumb(ShadowBreadcrumbs.WriteQueuingBuffer);
					WriteRecord item = new WriteRecord(buffer, offset, count, seenEod);
					this.writeQueue.Enqueue(item);
				}
			}
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x0009D7C0 File Offset: 0x0009B9C0
		protected override void WriteProxyDataComplete(SmtpResponse response)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowBdatSession.WriteProxyDataComplete");
			lock (this.syncObject)
			{
				base.WriteProxyDataComplete(response);
				if (response.Equals(SmtpResponse.Empty))
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "WriteProxyDataComplete handling acked write");
					if (this.writeQueue.Count > 0)
					{
						ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "WriteProxyDataComplete dequeuing write and sending to shadow");
						base.DropBreadcrumb(ShadowBreadcrumbs.WriteDequeuingBuffer);
						base.WriteToProxy(this.writeQueue.Dequeue());
					}
				}
				else if (response.SmtpResponseType == SmtpResponseType.Success)
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "WriteProxyDataComplete handling acked command");
					if (this.commandQueue.Count > 0)
					{
						ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "WriteProxyDataComplete dequeuing and registering command");
						base.DropBreadcrumb(ShadowBreadcrumbs.WriteDequeuingCommand);
						this.RegisterCommand(this.commandQueue.Dequeue());
					}
					else
					{
						ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "WriteProxyDataComplete has no pending commands");
						this.currentCommand = null;
					}
					if (this.writeQueue.Count > 0)
					{
						ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "WriteProxyDataComplete dequeuing write and sending to shadow");
						base.DropBreadcrumb(ShadowBreadcrumbs.WriteDequeuingBuffer);
						base.WriteToProxy(this.writeQueue.Dequeue());
					}
				}
			}
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x0009D948 File Offset: 0x0009BB48
		private void RegisterCommand(ShadowBdatSession.BdatCommandRecord nextBdatCommand)
		{
			if (base.IsClosed)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSession is aborted, skipping RegisterCommand");
				return;
			}
			this.currentCommand = nextBdatCommand;
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<long, bool>((long)this.GetHashCode(), "Registering new BDAT command: {0} isLast:{1}", nextBdatCommand.ChunkSize, nextBdatCommand.IsLastChunk);
			base.DropBreadcrumb(ShadowBreadcrumbs.WriteNewBdatCommand);
			(this.proxyLayer as InboundBdatProxyLayer).CreateNewCommand(nextBdatCommand.ChunkSize, nextBdatCommand.ChunkSize, nextBdatCommand.IsLastChunk);
		}

		// Token: 0x04001461 RID: 5217
		protected Queue<WriteRecord> writeQueue;

		// Token: 0x04001462 RID: 5218
		protected Queue<ShadowBdatSession.BdatCommandRecord> commandQueue;

		// Token: 0x04001463 RID: 5219
		private ShadowBdatSession.BdatCommandRecord currentCommand;

		// Token: 0x02000391 RID: 913
		internal class BdatCommandRecord
		{
			// Token: 0x17000C30 RID: 3120
			// (get) Token: 0x06002859 RID: 10329 RVA: 0x0009D9C7 File Offset: 0x0009BBC7
			// (set) Token: 0x0600285A RID: 10330 RVA: 0x0009D9CF File Offset: 0x0009BBCF
			public bool IsLastChunk { get; private set; }

			// Token: 0x17000C31 RID: 3121
			// (get) Token: 0x0600285B RID: 10331 RVA: 0x0009D9D8 File Offset: 0x0009BBD8
			// (set) Token: 0x0600285C RID: 10332 RVA: 0x0009D9E0 File Offset: 0x0009BBE0
			public long ChunkSize { get; private set; }

			// Token: 0x0600285D RID: 10333 RVA: 0x0009D9E9 File Offset: 0x0009BBE9
			public BdatCommandRecord(long chunkSize, bool isLastChunk)
			{
				this.ChunkSize = chunkSize;
				this.IsLastChunk = isLastChunk;
			}
		}
	}
}
