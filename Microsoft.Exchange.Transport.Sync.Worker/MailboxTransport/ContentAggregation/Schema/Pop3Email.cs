using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x020001F3 RID: 499
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class Pop3Email : DisposeTrackableBase, ISyncEmail, ISyncObject, IDisposeTrackable, IDisposable
	{
		// Token: 0x06001097 RID: 4247 RVA: 0x000362FF File Offset: 0x000344FF
		internal Pop3Email(ISyncSourceSession sourceSession, ExDateTime receivedTime, Stream mimeStream)
		{
			this.sourceSession = sourceSession;
			this.receivedTime = receivedTime;
			this.mimeStream = mimeStream;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x0003631C File Offset: 0x0003451C
		public SchemaType Type
		{
			get
			{
				base.CheckDisposed();
				return SchemaType.Email;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x00036325 File Offset: 0x00034525
		public ISyncSourceSession SourceSession
		{
			get
			{
				base.CheckDisposed();
				return this.sourceSession;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x00036333 File Offset: 0x00034533
		public bool? IsRead
		{
			get
			{
				base.CheckDisposed();
				return new bool?(false);
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x00036344 File Offset: 0x00034544
		public SyncMessageResponseType? SyncMessageResponseType
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x00036360 File Offset: 0x00034560
		public string From
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x00036369 File Offset: 0x00034569
		public string Subject
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x00036372 File Offset: 0x00034572
		public ExDateTime? ReceivedTime
		{
			get
			{
				base.CheckDisposed();
				return new ExDateTime?(this.receivedTime);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x00036385 File Offset: 0x00034585
		public string MessageClass
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x00036390 File Offset: 0x00034590
		public Importance? Importance
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x000363AC File Offset: 0x000345AC
		public string ConversationTopic
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x000363B5 File Offset: 0x000345B5
		public string ConversationIndex
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x000363C0 File Offset: 0x000345C0
		public Sensitivity? Sensitivity
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x000363DC File Offset: 0x000345DC
		public int? Size
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x000363F8 File Offset: 0x000345F8
		public bool? HasAttachments
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x00036414 File Offset: 0x00034614
		public bool? IsDraft
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00036430 File Offset: 0x00034630
		public string InternetMessageId
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x0003643C File Offset: 0x0003463C
		public ExDateTime? LastModifiedTime
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x00036458 File Offset: 0x00034658
		public Stream MimeStream
		{
			get
			{
				base.CheckDisposed();
				return this.mimeStream;
			}
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00036466 File Offset: 0x00034666
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00036468 File Offset: 0x00034668
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Pop3Email>(this);
		}

		// Token: 0x04000954 RID: 2388
		private ISyncSourceSession sourceSession;

		// Token: 0x04000955 RID: 2389
		private ExDateTime receivedTime;

		// Token: 0x04000956 RID: 2390
		private Stream mimeStream;
	}
}
