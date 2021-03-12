using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020001A9 RID: 425
	internal class TransportMailItemWrapper : MailItem, ITransportMailItemWrapperFacade
	{
		// Token: 0x06001348 RID: 4936 RVA: 0x0004D2CC File Offset: 0x0004B4CC
		public TransportMailItemWrapper(TransportMailItem mailItem, IMExSession mexSession, bool canAddRecipients)
		{
			this.mailItem = mailItem;
			this.mexSession = mexSession;
			this.canAddRecipients = canAddRecipients;
			this.snapshotWriter = new MessageSnapshotWriter(this.mailItem.SnapshotWriterState);
			this.pipelineTracingEnabled = mailItem.PipelineTracingEnabled;
			this.pipelineTracingPath = mailItem.PipelineTracingPath;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0004D322 File Offset: 0x0004B522
		public TransportMailItemWrapper(TransportMailItem mailItem, bool canAddRecipients) : this(mailItem, null, canAddRecipients)
		{
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0004D32D File Offset: 0x0004B52D
		ITransportMailItemFacade ITransportMailItemWrapperFacade.TransportMailItem
		{
			get
			{
				return this.TransportMailItem;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x0004D335 File Offset: 0x0004B535
		// (set) Token: 0x0600134C RID: 4940 RVA: 0x0004D348 File Offset: 0x0004B548
		public override string OriginalAuthenticator
		{
			get
			{
				this.ThrowIfDeferred();
				return this.mailItem.Auth;
			}
			set
			{
				this.ThrowIfDeferred();
				this.mailItem.Auth = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x0004D35C File Offset: 0x0004B55C
		public override DateTime DateTimeReceived
		{
			get
			{
				this.ThrowIfDeferred();
				return this.mailItem.DateReceived;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x0004D36F File Offset: 0x0004B56F
		// (set) Token: 0x0600134F RID: 4943 RVA: 0x0004D382 File Offset: 0x0004B582
		public override string EnvelopeId
		{
			get
			{
				this.ThrowIfDeferred();
				return this.mailItem.EnvId;
			}
			set
			{
				this.ThrowIfDeferred();
				this.mailItem.EnvId = value;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x0004D396 File Offset: 0x0004B596
		// (set) Token: 0x06001351 RID: 4945 RVA: 0x0004D3A9 File Offset: 0x0004B5A9
		public override RoutingAddress FromAddress
		{
			get
			{
				this.ThrowIfDeferred();
				return this.mailItem.From;
			}
			set
			{
				this.ThrowIfDeferred();
				this.mailItem.From = value;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x0004D3BD File Offset: 0x0004B5BD
		public override string OriginatingDomain
		{
			get
			{
				this.ThrowIfDeferred();
				return this.mailItem.HeloDomain;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x0004D3D0 File Offset: 0x0004B5D0
		public override EmailMessage Message
		{
			get
			{
				this.ThrowIfDeferred();
				if (this.mailItem.ExposeMessage)
				{
					return this.mailItem.Message;
				}
				return null;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0004D3F2 File Offset: 0x0004B5F2
		public override long MimeStreamLength
		{
			get
			{
				this.ThrowIfDeferred();
				return this.mailItem.GetCurrrentMimeSize();
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x0004D405 File Offset: 0x0004B605
		public override IDictionary<string, object> Properties
		{
			get
			{
				this.ThrowIfDeferred();
				return this.mailItem.ExtendedPropertyDictionary;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x0004D418 File Offset: 0x0004B618
		public override EnvelopeRecipientCollection Recipients
		{
			get
			{
				this.ThrowIfDeferred();
				if (this.recipients == null)
				{
					this.recipients = new MailRecipientCollectionWrapper(this.mailItem, this.mexSession, this.canAddRecipients);
				}
				return this.recipients;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x0004D44B File Offset: 0x0004B64B
		// (set) Token: 0x06001358 RID: 4952 RVA: 0x0004D463 File Offset: 0x0004B663
		public override DsnFormatRequested DsnFormatRequested
		{
			get
			{
				this.ThrowIfDeferred();
				return EnumConverter.InternalToPublic(this.mailItem.DsnFormat);
			}
			set
			{
				this.ThrowIfDeferred();
				this.mailItem.DsnFormat = EnumConverter.PublicToInternal(value);
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x0004D47C File Offset: 0x0004B67C
		// (set) Token: 0x0600135A RID: 4954 RVA: 0x0004D48F File Offset: 0x0004B68F
		public override DeliveryPriority DeliveryPriority
		{
			get
			{
				this.ThrowIfDeferred();
				return ((IQueueItem)this.mailItem).Priority;
			}
			set
			{
				this.ThrowIfDeferred();
				((IQueueItem)this.mailItem).Priority = value;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x0004D4A3 File Offset: 0x0004B6A3
		public override DeliveryMethod InboundDeliveryMethod
		{
			get
			{
				this.ThrowIfDeferred();
				return TransportMailItemWrapper.GetInboundDeliveryMethod(this.mailItem);
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x0004D4B6 File Offset: 0x0004B6B6
		internal override string InternetMessageId
		{
			get
			{
				if (this.mailItem != null)
				{
					return this.mailItem.InternetMessageId;
				}
				return null;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x0004D4CD File Offset: 0x0004B6CD
		internal override Guid NetworkMessageId
		{
			get
			{
				return this.mailItem.NetworkMessageId;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0004D4DA File Offset: 0x0004B6DA
		// (set) Token: 0x0600135F RID: 4959 RVA: 0x0004D4F5 File Offset: 0x0004B6F5
		internal override Guid SystemProbeId
		{
			get
			{
				if (this.mailItem != null)
				{
					return this.mailItem.SystemProbeId;
				}
				return Guid.Empty;
			}
			set
			{
				this.mailItem.SystemProbeId = value;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0004D503 File Offset: 0x0004B703
		internal override long InternalMessageId
		{
			get
			{
				this.ThrowIfDeferred();
				return this.mailItem.RecordId;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x0004D516 File Offset: 0x0004B716
		internal override MessageSnapshotWriter SnapshotWriter
		{
			get
			{
				return this.snapshotWriter;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x0004D51E File Offset: 0x0004B71E
		internal override bool PipelineTracingEnabled
		{
			get
			{
				return this.pipelineTracingEnabled;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0004D526 File Offset: 0x0004B726
		internal override string PipelineTracingPath
		{
			get
			{
				return this.pipelineTracingPath;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x0004D52E File Offset: 0x0004B72E
		internal override MimeDocument MimeDocument
		{
			get
			{
				this.ThrowIfDeferred();
				return this.mailItem.MimeDocument;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x0004D541 File Offset: 0x0004B741
		internal override object RecipientCache
		{
			get
			{
				this.ThrowIfDeferred();
				return this.mailItem.ADRecipientCache;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0004D554 File Offset: 0x0004B754
		internal override bool MimeWriteStreamOpen
		{
			get
			{
				return this.mailItem != null && this.mailItem.MimeWriteStreamOpen;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x0004D578 File Offset: 0x0004B778
		// (set) Token: 0x06001368 RID: 4968 RVA: 0x0004D580 File Offset: 0x0004B780
		internal TransportMailItem TransportMailItem
		{
			get
			{
				return this.mailItem;
			}
			set
			{
				this.mailItem = value;
				this.recipients = null;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0004D590 File Offset: 0x0004B790
		internal override bool HasBeenDeferred
		{
			get
			{
				return null == this.mailItem;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0004D59B File Offset: 0x0004B79B
		internal override bool HasBeenDeleted
		{
			get
			{
				this.ThrowIfDeferred();
				return !this.mailItem.IsActive;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0004D5B1 File Offset: 0x0004B7B1
		internal override long CachedMimeStreamLength
		{
			get
			{
				this.ThrowIfDeferred();
				return this.mailItem.MimeSize;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x0004D5C4 File Offset: 0x0004B7C4
		public override bool MustDeliver
		{
			get
			{
				this.ThrowIfDeferred();
				return this.mailItem.RetryDeliveryIfRejected;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0004D5D7 File Offset: 0x0004B7D7
		public override Guid TenantId
		{
			get
			{
				this.ThrowIfDeferred();
				return TransportMailItemWrapper.GetTenantId(this.mailItem);
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x0004D5EA File Offset: 0x0004B7EA
		// (set) Token: 0x0600136F RID: 4975 RVA: 0x0004D621 File Offset: 0x0004B821
		public override string OriginatorOrganization
		{
			get
			{
				this.ThrowIfDeferred();
				if (!this.mailItem.ExposeMessageHeaders && !ConfigurationComponent.IsFrontEndTransportProcess(Components.Configuration))
				{
					throw new InvalidOperationException("Property 'Oorg' isn't accessible prior to the OnEndOfHeaders protocol event");
				}
				return this.mailItem.Oorg;
			}
			set
			{
				this.ThrowIfDeferred();
				if (!this.mailItem.ExposeMessageHeaders && !ConfigurationComponent.IsFrontEndTransportProcess(Components.Configuration))
				{
					throw new InvalidOperationException("Property 'Oorg' isn't accessible prior to the OnEndOfHeaders protocol event");
				}
				this.mailItem.Oorg = value;
			}
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0004D65C File Offset: 0x0004B85C
		public static DeliveryMethod GetInboundDeliveryMethod(IReadOnlyMailItem mailItem)
		{
			if (string.IsNullOrEmpty(mailItem.ReceiveConnectorName))
			{
				return DeliveryMethod.Unknown;
			}
			string value = "SMTP:";
			if (mailItem.ReceiveConnectorName.StartsWith(value, StringComparison.Ordinal))
			{
				return DeliveryMethod.Smtp;
			}
			if (mailItem.ReceiveConnectorName == "FromLocal")
			{
				return DeliveryMethod.Mailbox;
			}
			return DeliveryMethod.File;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0004D6A4 File Offset: 0x0004B8A4
		public static Guid GetTenantId(IReadOnlyMailItem mailItem)
		{
			if (mailItem.OrganizationId == null || mailItem.OrganizationId.ConfigurationUnit == null)
			{
				return Guid.Empty;
			}
			return mailItem.OrganizationId.ConfigurationUnit.ObjectGuid;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0004D6D8 File Offset: 0x0004B8D8
		public static Stream GetMimeReadStream(IReadOnlyMailItem mailItem, ref List<Stream> openedStreams)
		{
			Stream stream = mailItem.OpenMimeReadStream();
			if (openedStreams == null)
			{
				openedStreams = new List<Stream>();
			}
			openedStreams.Add(stream);
			return stream;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0004D700 File Offset: 0x0004B900
		public static void CloseStreams(ref List<Stream> openedStreams)
		{
			if (openedStreams != null)
			{
				foreach (Stream stream in openedStreams)
				{
					stream.Close();
				}
				openedStreams = null;
			}
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0004D758 File Offset: 0x0004B958
		public override void SetMustDeliver()
		{
			this.ThrowIfDeferred();
			this.mailItem.SetMustDeliver();
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0004D76C File Offset: 0x0004B96C
		public override Stream GetMimeReadStream()
		{
			this.ThrowIfDeferred();
			if (!this.mailItem.ExposeMessage)
			{
				return null;
			}
			if (this.mailItem.MimeWriteStreamOpen)
			{
				throw new InvalidOperationException(Strings.MimeWriteStreamOpen);
			}
			return TransportMailItemWrapper.GetMimeReadStream(this.mailItem, ref this.openedReadStreams);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0004D7BC File Offset: 0x0004B9BC
		public override Stream GetMimeWriteStream()
		{
			this.ThrowIfDeferred();
			if (!this.mailItem.ExposeMessage)
			{
				return null;
			}
			if (this.mailItem.MimeWriteStreamOpen)
			{
				throw new InvalidOperationException(Strings.MimeWriteStreamOpen);
			}
			return this.mailItem.OpenMimeWriteStream();
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0004D7FB File Offset: 0x0004B9FB
		internal override void RestoreLastSavedMime(string agentName, string eventName)
		{
			this.mailItem.RestoreLastSavedMime(agentName, eventName);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0004D80C File Offset: 0x0004BA0C
		public override string ToString()
		{
			this.ThrowIfDeferred();
			return string.Format(CultureInfo.InvariantCulture, "TransportMailItem(hash={0},id={1})", new object[]
			{
				this.mailItem.GetHashCode(),
				this.mailItem.RecordId
			});
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0004D85C File Offset: 0x0004BA5C
		internal void CloseWrapper()
		{
			TransportMailItemWrapper.CloseStreams(ref this.openedReadStreams);
			this.recipients = null;
			this.mailItem = null;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0004D877 File Offset: 0x0004BA77
		private void ThrowIfDeferred()
		{
			if (this.HasBeenDeferred)
			{
				throw new InvalidOperationException(Strings.MailItemDeferred);
			}
		}

		// Token: 0x04000A01 RID: 2561
		protected TransportMailItem mailItem;

		// Token: 0x04000A02 RID: 2562
		private List<Stream> openedReadStreams;

		// Token: 0x04000A03 RID: 2563
		private MailRecipientCollectionWrapper recipients;

		// Token: 0x04000A04 RID: 2564
		private readonly bool canAddRecipients;

		// Token: 0x04000A05 RID: 2565
		private readonly MessageSnapshotWriter snapshotWriter;

		// Token: 0x04000A06 RID: 2566
		private readonly IMExSession mexSession;

		// Token: 0x04000A07 RID: 2567
		private readonly bool pipelineTracingEnabled;

		// Token: 0x04000A08 RID: 2568
		private readonly string pipelineTracingPath;
	}
}
