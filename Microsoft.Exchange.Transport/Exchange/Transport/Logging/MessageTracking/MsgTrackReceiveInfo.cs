using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Transport.Logging.MessageTracking
{
	// Token: 0x0200008D RID: 141
	internal class MsgTrackReceiveInfo
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x0001482C File Offset: 0x00012A2C
		public MsgTrackReceiveInfo(string sourceContext) : this(null, string.Empty, null, sourceContext, string.Empty, null, null, string.Empty, string.Empty, string.Empty, null, string.Empty, null, null, string.Empty, null, Guid.Empty)
		{
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00014878 File Offset: 0x00012A78
		public MsgTrackReceiveInfo(string sourceContext, string securityInfo, IList<string> invalidRecipients) : this(null, null, null, sourceContext, null, null, null, securityInfo, string.Empty, string.Empty, invalidRecipients, string.Empty, null, null, string.Empty, null, Guid.Empty)
		{
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000148B8 File Offset: 0x00012AB8
		public MsgTrackReceiveInfo(IPAddress serverIp, string sourceContext, string relatedMessageId, string securityInfo, IList<string> invalidRecipients) : this(null, string.Empty, serverIp, sourceContext, string.Empty, null, relatedMessageId, securityInfo, string.Empty, string.Empty, invalidRecipients, string.Empty, null, null, string.Empty, null, Guid.Empty)
		{
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00014904 File Offset: 0x00012B04
		public MsgTrackReceiveInfo(IPAddress serverIp, long? relatedMailItemId, string securityInfo, IList<string> invalidRecipients) : this(null, string.Empty, serverIp, string.Empty, string.Empty, relatedMailItemId, null, securityInfo, string.Empty, string.Empty, null, string.Empty, null, null, string.Empty, null, Guid.Empty)
		{
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00014948 File Offset: 0x00012B48
		public MsgTrackReceiveInfo(IPAddress clientIP, string clientHostName, IPAddress serverIP, string sourceContext, string securityInfo, string mailboxDatabaseGuid, string submittingMailboxSmtpAddress, byte[] entryId) : this(clientIP, clientHostName, serverIP, sourceContext, string.Empty, null, null, securityInfo, string.Empty, submittingMailboxSmtpAddress, null, mailboxDatabaseGuid, entryId, null, string.Empty, null, Guid.Empty)
		{
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001498C File Offset: 0x00012B8C
		public MsgTrackReceiveInfo(IPAddress clientIP, string clientHostName, IPAddress serverIP, string sourceContext, string connectorId, long? relatedMailItemId) : this(clientIP, clientHostName, serverIP, sourceContext, connectorId, relatedMailItemId, null, string.Empty, Guid.Empty)
		{
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000149B4 File Offset: 0x00012BB4
		public MsgTrackReceiveInfo(IPAddress clientIP, string clientHostName, IPAddress serverIP, string sourceContext, string connectorId, long? relatedMailItemId, IPAddress proxiedClientIPAddress, string proxiedClientHostname, Guid authUserMailboxGuid) : this(clientIP, clientHostName, serverIP, sourceContext, connectorId, relatedMailItemId, null, string.Empty, string.Empty, string.Empty, null, string.Empty, null, proxiedClientIPAddress, proxiedClientHostname, null, authUserMailboxGuid)
		{
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000149F0 File Offset: 0x00012BF0
		public MsgTrackReceiveInfo(IPAddress clientIP, string clientHostName, IPAddress serverIP, string sourceContext, string connectorId, long? relatedMailItemId, string securityInfo) : this(clientIP, clientHostName, serverIP, sourceContext, connectorId, relatedMailItemId, null, securityInfo, string.Empty, string.Empty, null, string.Empty, null, null, string.Empty, null, Guid.Empty)
		{
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00014A2C File Offset: 0x00012C2C
		public MsgTrackReceiveInfo(IPAddress clientIP, string clientHostName, IPAddress serverIP, string sourceContext, string connectorId, long? relatedMailItemId, string securityInfo, string relatedMessageInfo, string submittingMailboxSmtpAddress) : this(clientIP, clientHostName, serverIP, sourceContext, connectorId, relatedMailItemId, securityInfo, relatedMessageInfo, submittingMailboxSmtpAddress, null, string.Empty, null, Guid.Empty)
		{
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00014A5C File Offset: 0x00012C5C
		public MsgTrackReceiveInfo(IPAddress clientIP, string clientHostName, IPAddress serverIP, string sourceContext, string connectorId, long? relatedMailItemId, string securityInfo, string relatedMessageInfo, string submittingMailboxSmtpAddress, IPAddress proxiedClientIPAddress, string proxiedClientHostname, IReadOnlyList<Header> receivedHeaders, Guid authUserMailboxGuid) : this(clientIP, clientHostName, serverIP, sourceContext, connectorId, relatedMailItemId, string.Empty, securityInfo, relatedMessageInfo, submittingMailboxSmtpAddress, null, string.Empty, null, proxiedClientIPAddress, proxiedClientHostname, receivedHeaders, authUserMailboxGuid)
		{
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00014A94 File Offset: 0x00012C94
		private MsgTrackReceiveInfo(IPAddress clientIP, string clientHostName, IPAddress serverIP, string sourceContext, string connectorId, long? relatedMailItemId, string relatedMessageId, string securityInfo, string relatedMessageInfo, string submittingMailboxSmtpAddress, IList<string> invalidRecipients, string mailboxDatabaseGuid, byte[] entryId, IPAddress proxiedClientIP, string proxiedClientHostname, IReadOnlyList<Header> receivedHeaders, Guid authUserMailboxGuid)
		{
			this.clientIPAddress = clientIP;
			this.clientHostName = clientHostName;
			this.serverIPAddress = serverIP;
			this.sourceContext = sourceContext;
			this.connectorId = connectorId;
			this.submittingMailboxSmtpAddress = submittingMailboxSmtpAddress;
			this.relatedMailItemId = relatedMailItemId;
			this.relatedMessageId = relatedMessageId;
			this.securityInfo = securityInfo;
			this.relatedMessageInfo = relatedMessageInfo;
			this.submittingMailboxSmtpAddress = submittingMailboxSmtpAddress;
			this.invalidRecipients = invalidRecipients;
			this.mailboxDatabaseGuid = mailboxDatabaseGuid;
			this.entryId = entryId;
			this.proxiedClientIPAddress = proxiedClientIP;
			this.proxiedClientHostname = proxiedClientHostname;
			this.receivedHeaders = receivedHeaders;
			this.authUserMailboxGuid = authUserMailboxGuid;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00014B40 File Offset: 0x00012D40
		internal IPAddress ClientIPAddress
		{
			get
			{
				return this.clientIPAddress;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00014B48 File Offset: 0x00012D48
		internal IPAddress ServerIPAddress
		{
			get
			{
				return this.serverIPAddress;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00014B50 File Offset: 0x00012D50
		internal string ClientHostname
		{
			get
			{
				return this.clientHostName;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00014B58 File Offset: 0x00012D58
		internal string SourceContext
		{
			get
			{
				return this.sourceContext;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00014B60 File Offset: 0x00012D60
		internal string ConnectorId
		{
			get
			{
				return this.connectorId;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00014B68 File Offset: 0x00012D68
		internal long? RelatedMailItemId
		{
			get
			{
				return this.relatedMailItemId;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00014B70 File Offset: 0x00012D70
		internal string RelatedMessageId
		{
			get
			{
				return this.relatedMessageId;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00014B78 File Offset: 0x00012D78
		internal string SecurityInfo
		{
			get
			{
				return this.securityInfo;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00014B80 File Offset: 0x00012D80
		internal string RelatedMessageInfo
		{
			get
			{
				return this.relatedMessageInfo;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00014B88 File Offset: 0x00012D88
		internal string SubmittingMailboxSmtpAddress
		{
			get
			{
				return this.submittingMailboxSmtpAddress;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00014B90 File Offset: 0x00012D90
		public IList<string> InvalidRecipients
		{
			get
			{
				return this.invalidRecipients;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00014B98 File Offset: 0x00012D98
		public byte[] EntryId
		{
			get
			{
				return this.entryId;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00014BA0 File Offset: 0x00012DA0
		public string MailboxDatabaseGuid
		{
			get
			{
				return this.mailboxDatabaseGuid;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00014BA8 File Offset: 0x00012DA8
		public IPAddress ProxiedClientIPAddress
		{
			get
			{
				return this.proxiedClientIPAddress;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00014BB0 File Offset: 0x00012DB0
		public string ProxiedClientHostname
		{
			get
			{
				return this.proxiedClientHostname;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00014BB8 File Offset: 0x00012DB8
		public IReadOnlyList<Header> ReceivedHeaders
		{
			get
			{
				return this.receivedHeaders;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00014BC0 File Offset: 0x00012DC0
		public Guid AuthUserMailboxGuid
		{
			get
			{
				return this.authUserMailboxGuid;
			}
		}

		// Token: 0x0400026A RID: 618
		private readonly string proxiedClientHostname;

		// Token: 0x0400026B RID: 619
		private readonly IReadOnlyList<Header> receivedHeaders;

		// Token: 0x0400026C RID: 620
		private IPAddress proxiedClientIPAddress;

		// Token: 0x0400026D RID: 621
		private IPAddress clientIPAddress;

		// Token: 0x0400026E RID: 622
		private string clientHostName;

		// Token: 0x0400026F RID: 623
		private IPAddress serverIPAddress;

		// Token: 0x04000270 RID: 624
		private string sourceContext;

		// Token: 0x04000271 RID: 625
		private string connectorId;

		// Token: 0x04000272 RID: 626
		private long? relatedMailItemId = null;

		// Token: 0x04000273 RID: 627
		private string relatedMessageId;

		// Token: 0x04000274 RID: 628
		private string securityInfo;

		// Token: 0x04000275 RID: 629
		private string relatedMessageInfo;

		// Token: 0x04000276 RID: 630
		private string submittingMailboxSmtpAddress;

		// Token: 0x04000277 RID: 631
		private IList<string> invalidRecipients;

		// Token: 0x04000278 RID: 632
		private byte[] entryId;

		// Token: 0x04000279 RID: 633
		private string mailboxDatabaseGuid;

		// Token: 0x0400027A RID: 634
		private Guid authUserMailboxGuid;
	}
}
