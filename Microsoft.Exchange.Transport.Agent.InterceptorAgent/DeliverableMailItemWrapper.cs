using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000008 RID: 8
	internal sealed class DeliverableMailItemWrapper : MailItem
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002CC9 File Offset: 0x00000EC9
		public DeliverableMailItemWrapper(DeliverableMailItem m)
		{
			this.mailItem = m;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002CD8 File Offset: 0x00000ED8
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002CE5 File Offset: 0x00000EE5
		public override string OriginalAuthenticator
		{
			get
			{
				return this.mailItem.OriginalAuthenticator;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002CEC File Offset: 0x00000EEC
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002CF9 File Offset: 0x00000EF9
		public override string EnvelopeId
		{
			get
			{
				return this.mailItem.EnvelopeId;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002D00 File Offset: 0x00000F00
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002D0D File Offset: 0x00000F0D
		public override RoutingAddress FromAddress
		{
			get
			{
				return this.mailItem.FromAddress;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002D14 File Offset: 0x00000F14
		public override string OriginatingDomain
		{
			get
			{
				return this.mailItem.OriginatingDomain;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002D21 File Offset: 0x00000F21
		public override EmailMessage Message
		{
			get
			{
				return this.mailItem.Message;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002D2E File Offset: 0x00000F2E
		public override IDictionary<string, object> Properties
		{
			get
			{
				return new Dictionary<string, object>();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002D38 File Offset: 0x00000F38
		public override EnvelopeRecipientCollection Recipients
		{
			get
			{
				return new EnvelopeRecipientCollectionWrapper(this.mailItem.Recipients);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002D57 File Offset: 0x00000F57
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002D64 File Offset: 0x00000F64
		public override DsnFormatRequested DsnFormatRequested
		{
			get
			{
				return this.mailItem.DsnFormatRequested;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002D6B File Offset: 0x00000F6B
		public override DateTime DateTimeReceived
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002D72 File Offset: 0x00000F72
		public override long MimeStreamLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002D79 File Offset: 0x00000F79
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002D80 File Offset: 0x00000F80
		public override DeliveryPriority DeliveryPriority
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002D87 File Offset: 0x00000F87
		public override DeliveryMethod InboundDeliveryMethod
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002D8E File Offset: 0x00000F8E
		public override bool MustDeliver
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002D95 File Offset: 0x00000F95
		public override Guid TenantId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002D9C File Offset: 0x00000F9C
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002DA3 File Offset: 0x00000FA3
		public override string OriginatorOrganization
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002DAA File Offset: 0x00000FAA
		internal override long InternalMessageId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002DB1 File Offset: 0x00000FB1
		internal override Guid NetworkMessageId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002DB8 File Offset: 0x00000FB8
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002DBF File Offset: 0x00000FBF
		internal override Guid SystemProbeId
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002DC6 File Offset: 0x00000FC6
		internal override MessageSnapshotWriter SnapshotWriter
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002DCD File Offset: 0x00000FCD
		internal override bool PipelineTracingEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002DD4 File Offset: 0x00000FD4
		internal override string PipelineTracingPath
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002DDB File Offset: 0x00000FDB
		internal override string InternetMessageId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002DE2 File Offset: 0x00000FE2
		internal override object RecipientCache
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002DE9 File Offset: 0x00000FE9
		internal override bool MimeWriteStreamOpen
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002DF0 File Offset: 0x00000FF0
		internal override bool HasBeenDeferred
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002DF7 File Offset: 0x00000FF7
		internal override bool HasBeenDeleted
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002DFE File Offset: 0x00000FFE
		internal override long CachedMimeStreamLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002E05 File Offset: 0x00001005
		internal override MimeDocument MimeDocument
		{
			get
			{
				return this.mailItem.Message.MimeDocument;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E17 File Offset: 0x00001017
		public override void SetMustDeliver()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E1E File Offset: 0x0000101E
		public override Stream GetMimeReadStream()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E25 File Offset: 0x00001025
		public override Stream GetMimeWriteStream()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E2C File Offset: 0x0000102C
		internal override void RestoreLastSavedMime(string agentName, string eventName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400001D RID: 29
		private DeliverableMailItem mailItem;
	}
}
