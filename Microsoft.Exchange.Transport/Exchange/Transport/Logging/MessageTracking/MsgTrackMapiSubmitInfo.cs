using System;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Logging.MessageTracking
{
	// Token: 0x02000090 RID: 144
	internal class MsgTrackMapiSubmitInfo
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x00014CE8 File Offset: 0x00012EE8
		public MsgTrackMapiSubmitInfo(string mapiEventInfo, IPAddress bridgeheadServerIPAddress, string bridgeheadServerName, IPAddress mailboxServerIPAddress, string mailboxServerName, string sender, string from, string messageId, byte[] itemEntryId, string subject, string latencyData, string diagnosticInfo, bool isRegularSubmission)
		{
			this.mapiEventInfo = mapiEventInfo;
			this.bridgeheadServerIPAddress = bridgeheadServerIPAddress;
			this.bridgeheadServerName = bridgeheadServerName;
			this.mailboxServerIPAddress = mailboxServerIPAddress;
			this.mailboxServerName = mailboxServerName;
			this.sender = sender;
			this.from = from;
			this.messageId = messageId;
			this.itemEntryId = itemEntryId;
			this.subject = subject;
			this.latencyData = latencyData;
			this.diagnosticInfo = diagnosticInfo;
			this.isRegularSubmission = isRegularSubmission;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00014D60 File Offset: 0x00012F60
		public MsgTrackMapiSubmitInfo(string mapiEventInfo, IPAddress bridgeheadServerIPAddress, string bridgeheadServerName, IPAddress mailboxServerIPAddress, string mailboxServerName, string sender, string from, string messageId, byte[] itemEntryId, string subject, string latencyData, string diagnosticInfo, bool isRegularSubmission, Guid externalOrganizationId, OrganizationId organizationId, string[] recipientAddresses, MailDirectionality direction, Guid networkMessageId, IPAddress originalClientIPAddress)
		{
			this.mapiEventInfo = mapiEventInfo;
			this.bridgeheadServerIPAddress = bridgeheadServerIPAddress;
			this.bridgeheadServerName = bridgeheadServerName;
			this.mailboxServerIPAddress = mailboxServerIPAddress;
			this.mailboxServerName = mailboxServerName;
			this.sender = sender;
			this.from = from;
			this.messageId = messageId;
			this.itemEntryId = itemEntryId;
			this.subject = subject;
			this.latencyData = latencyData;
			this.diagnosticInfo = diagnosticInfo;
			this.isRegularSubmission = isRegularSubmission;
			this.externalOrganizationId = externalOrganizationId;
			this.organizationId = organizationId;
			this.recipientAddresses = recipientAddresses;
			this.direction = direction;
			this.directionIsSet = true;
			this.networkMessageId = networkMessageId;
			this.originalClientIPAddress = originalClientIPAddress;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00014E10 File Offset: 0x00013010
		public MsgTrackMapiSubmitInfo(string mapiEventInfo, IPAddress bridgeheadServerIPAddress, string bridgeheadServerName, IPAddress mailboxServerIPAddress, string mailboxServerName, string sender, string messageId, byte[] itemEntryId, string latencyData, string diagnosticInfo, bool isPermanentFailure, bool isRegularSubmission)
		{
			this.mapiEventInfo = mapiEventInfo;
			this.bridgeheadServerIPAddress = bridgeheadServerIPAddress;
			this.bridgeheadServerName = bridgeheadServerName;
			this.mailboxServerIPAddress = mailboxServerIPAddress;
			this.mailboxServerName = mailboxServerName;
			this.sender = sender;
			this.messageId = messageId;
			this.latencyData = latencyData;
			this.diagnosticInfo = diagnosticInfo;
			this.itemEntryId = itemEntryId;
			this.isPermanentFailure = isPermanentFailure;
			this.failed = true;
			this.isRegularSubmission = isRegularSubmission;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00014E88 File Offset: 0x00013088
		public MsgTrackMapiSubmitInfo(string mapiEventInfo, IPAddress bridgeheadServerIPAddress, string bridgeheadServerName, IPAddress mailboxServerIPAddress, string mailboxServerName, string sender, string messageId, byte[] itemEntryId, string latencyData, string diagnosticInfo, bool isPermanentFailure, bool isRegularSubmission, Guid externalOrganizationId, OrganizationId organizationId, string[] recipientAddresses, MailDirectionality direction, Guid networkMessageId)
		{
			this.mapiEventInfo = mapiEventInfo;
			this.bridgeheadServerIPAddress = bridgeheadServerIPAddress;
			this.bridgeheadServerName = bridgeheadServerName;
			this.mailboxServerIPAddress = mailboxServerIPAddress;
			this.mailboxServerName = mailboxServerName;
			this.sender = sender;
			this.messageId = messageId;
			this.latencyData = latencyData;
			this.diagnosticInfo = diagnosticInfo;
			this.itemEntryId = itemEntryId;
			this.isPermanentFailure = isPermanentFailure;
			this.failed = true;
			this.isRegularSubmission = isRegularSubmission;
			this.externalOrganizationId = externalOrganizationId;
			this.organizationId = organizationId;
			this.recipientAddresses = recipientAddresses;
			this.direction = direction;
			this.directionIsSet = true;
			this.networkMessageId = networkMessageId;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00014F2E File Offset: 0x0001312E
		public IPAddress BridgeheadServerIPAddress
		{
			get
			{
				return this.bridgeheadServerIPAddress;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00014F36 File Offset: 0x00013136
		public IPAddress MailboxServerIPAddress
		{
			get
			{
				return this.mailboxServerIPAddress;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00014F3E File Offset: 0x0001313E
		public string BridgeheadServerName
		{
			get
			{
				return this.bridgeheadServerName;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00014F46 File Offset: 0x00013146
		public string MailboxServerName
		{
			get
			{
				return this.mailboxServerName;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00014F4E File Offset: 0x0001314E
		public string Sender
		{
			get
			{
				return this.sender;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00014F56 File Offset: 0x00013156
		public string From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00014F5E File Offset: 0x0001315E
		public string MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00014F66 File Offset: 0x00013166
		public byte[] ItemEntryId
		{
			get
			{
				return this.itemEntryId;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00014F6E File Offset: 0x0001316E
		public string Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00014F76 File Offset: 0x00013176
		public bool Failed
		{
			get
			{
				return this.failed;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00014F7E File Offset: 0x0001317E
		public bool IsPermanentFailure
		{
			get
			{
				return this.isPermanentFailure;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00014F86 File Offset: 0x00013186
		public string LatencyData
		{
			get
			{
				return this.latencyData;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00014F8E File Offset: 0x0001318E
		public string DiagnosticInfo
		{
			get
			{
				return this.diagnosticInfo;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00014F96 File Offset: 0x00013196
		public string MapiEventInfo
		{
			get
			{
				return this.mapiEventInfo;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00014F9E File Offset: 0x0001319E
		public bool IsRegularSubmission
		{
			get
			{
				return this.isRegularSubmission;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00014FA6 File Offset: 0x000131A6
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00014FAE File Offset: 0x000131AE
		public Guid ExternalOrganizationId
		{
			get
			{
				return this.externalOrganizationId;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00014FB6 File Offset: 0x000131B6
		public string[] RecipientAddresses
		{
			get
			{
				return this.recipientAddresses;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00014FBE File Offset: 0x000131BE
		public MailDirectionality Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00014FC6 File Offset: 0x000131C6
		public bool DirectionIsSet
		{
			get
			{
				return this.directionIsSet;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00014FCE File Offset: 0x000131CE
		public Guid NetworkMessageId
		{
			get
			{
				return this.networkMessageId;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00014FD6 File Offset: 0x000131D6
		public IPAddress OriginalClientIPAddress
		{
			get
			{
				return this.originalClientIPAddress;
			}
		}

		// Token: 0x04000285 RID: 645
		private readonly bool directionIsSet;

		// Token: 0x04000286 RID: 646
		private readonly OrganizationId organizationId;

		// Token: 0x04000287 RID: 647
		private readonly Guid externalOrganizationId;

		// Token: 0x04000288 RID: 648
		private readonly string[] recipientAddresses;

		// Token: 0x04000289 RID: 649
		private readonly MailDirectionality direction;

		// Token: 0x0400028A RID: 650
		private readonly Guid networkMessageId;

		// Token: 0x0400028B RID: 651
		private IPAddress bridgeheadServerIPAddress;

		// Token: 0x0400028C RID: 652
		private string bridgeheadServerName;

		// Token: 0x0400028D RID: 653
		private IPAddress mailboxServerIPAddress;

		// Token: 0x0400028E RID: 654
		private string mailboxServerName;

		// Token: 0x0400028F RID: 655
		private string messageId;

		// Token: 0x04000290 RID: 656
		private string subject;

		// Token: 0x04000291 RID: 657
		private string sender;

		// Token: 0x04000292 RID: 658
		private string from;

		// Token: 0x04000293 RID: 659
		private string diagnosticInfo;

		// Token: 0x04000294 RID: 660
		private string latencyData;

		// Token: 0x04000295 RID: 661
		private bool failed;

		// Token: 0x04000296 RID: 662
		private bool isPermanentFailure;

		// Token: 0x04000297 RID: 663
		private string mapiEventInfo;

		// Token: 0x04000298 RID: 664
		private byte[] itemEntryId;

		// Token: 0x04000299 RID: 665
		private bool isRegularSubmission;

		// Token: 0x0400029A RID: 666
		private IPAddress originalClientIPAddress;
	}
}
