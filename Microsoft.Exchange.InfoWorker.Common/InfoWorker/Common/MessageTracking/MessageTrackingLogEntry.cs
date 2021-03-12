using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002CD RID: 717
	internal class MessageTrackingLogEntry
	{
		// Token: 0x0600140F RID: 5135 RVA: 0x0005E698 File Offset: 0x0005C898
		private MessageTrackingLogEntry(MessageTrackingLogRow rowData)
		{
			this.rowData = rowData;
			string text = Names<MessageTrackingEvent>.Map[(int)this.rowData.EventId];
			if (this.rowData.EventId == MessageTrackingEvent.TRANSFER || (this.rowData.EventId == MessageTrackingEvent.RESUBMIT && this.rowData.Source == MessageTrackingSource.REDUNDANCY))
			{
				long num = 0L;
				if (this.rowData.References == null || this.rowData.References.Length != 1 || !long.TryParse(this.rowData.References[0], out num))
				{
					TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "RelatedInternalId could not be parsed for TRANSFER event from server {0}", new object[]
					{
						this.Server
					});
				}
				this.relatedMailItemId = num;
				return;
			}
			if (this.rowData.EventId == MessageTrackingEvent.RECEIVE)
			{
				if (this.rowData.Source == MessageTrackingSource.SMTP && string.IsNullOrEmpty(this.rowData.ClientHostName) && string.IsNullOrEmpty(this.rowData.ClientIP))
				{
					TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "No ClientHostName or ClientIP for SMTP RECEIVE from server {0}", new object[]
					{
						this.Server
					});
					return;
				}
			}
			else
			{
				if (this.rowData.EventId == MessageTrackingEvent.SEND && this.rowData.Source == MessageTrackingSource.SMTP)
				{
					if (string.IsNullOrEmpty(this.rowData.ServerHostName) && string.IsNullOrEmpty(this.rowData.ServerIP))
					{
						TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "No ServerHostName or ServerIP for SMTP SEND from server {0}", new object[]
						{
							this.Server
						});
					}
					this.ProcessOutboundProxyTargetServer();
					return;
				}
				if (this.rowData.EventId == MessageTrackingEvent.DELIVER && this.rowData.Source == MessageTrackingSource.STOREDRIVER && this.rowData.RecipientStatuses != null)
				{
					if (this.rowData.RecipientAddresses == null || this.rowData.RecipientAddresses.Length != this.rowData.RecipientStatuses.Length)
					{
						TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "Mismatched RecipientAddress and RecipientStatus counts for STOREDRIVER DELIVER from server {0}", new object[]
						{
							this.Server
						});
					}
					for (int i = 0; i < this.rowData.RecipientAddresses.Length; i++)
					{
						if (!string.IsNullOrEmpty(this.rowData.RecipientStatuses[i]))
						{
							if (this.recipientFolders == null)
							{
								this.recipientFolders = new Dictionary<string, string>(this.rowData.RecipientAddresses.Length);
							}
							this.recipientFolders.Add(this.rowData.RecipientAddresses[i], this.rowData.RecipientStatuses[i]);
						}
					}
					return;
				}
				if (this.rowData.EventId == MessageTrackingEvent.INITMESSAGECREATED)
				{
					if (this.rowData.References == null || this.rowData.References.Length < 1 || string.IsNullOrEmpty(this.rowData.References[0]) || !SmtpAddress.IsValidSmtpAddress(this.rowData.Context))
					{
						TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "No RelatedMessageId for INITMESSAGECREATED from server {0}", new object[]
						{
							this.Server
						});
					}
					this.arbitrationMailboxAddress = SmtpAddress.Parse(this.rowData.Context);
					return;
				}
				if (this.rowData.EventId == MessageTrackingEvent.MODERATORAPPROVE || this.rowData.EventId == MessageTrackingEvent.MODERATORREJECT || this.rowData.EventId == MessageTrackingEvent.MODERATIONEXPIRE)
				{
					if (this.rowData.References == null || this.rowData.References.Length < 1 || string.IsNullOrEmpty(this.rowData.References[0]))
					{
						TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "No RelatedMessageId for {0}", new object[]
						{
							text,
							this.Server
						});
						return;
					}
				}
				else if (this.rowData.EventId == MessageTrackingEvent.HAREDIRECT && this.rowData.Source == MessageTrackingSource.SMTP)
				{
					this.FixupHARedirectEvent();
				}
			}
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0005EA48 File Offset: 0x0005CC48
		private void FixupHARedirectEvent()
		{
			string text = string.Empty;
			Match match = MessageTrackingLogEntry.serverNameFromHAContext.Match(this.rowData.Context);
			if (match.Success)
			{
				text = match.Groups[1].Value;
			}
			else
			{
				TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "Failed to get destination server value from HAREDIRECT event. MessageId: {0}. Server: {1}. Value {2}", new object[]
				{
					this.rowData.MessageId,
					this.Server,
					this.rowData.Context
				});
			}
			this.clientHostName = this.rowData.ServerHostName;
			this.serverHostName = text;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0005EAE0 File Offset: 0x0005CCE0
		private void ProcessOutboundProxyTargetServer()
		{
			string customData = this.rowData.GetCustomData<string>(MessageTrackingLogEntry.OutboundProxyTargetHostNamePropertyName, string.Empty);
			if (!string.IsNullOrEmpty(customData))
			{
				this.serverHostName = customData;
			}
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0005EB14 File Offset: 0x0005CD14
		public static bool TryCreateFromCursor(LogSearchCursor cursor, string server, TrackingErrorCollection errors, out MessageTrackingLogEntry entry)
		{
			entry = null;
			MessageTrackingLogRow messageTrackingLogRow;
			if (MessageTrackingLogRow.TryRead(server, cursor, MessageTrackingLogEntry.allRows, errors, out messageTrackingLogRow))
			{
				entry = new MessageTrackingLogEntry(messageTrackingLogRow);
				return true;
			}
			return false;
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0005EB40 File Offset: 0x0005CD40
		private static HashSet<MessageTrackingEvent> CreateTerminalEventSet()
		{
			return new HashSet<MessageTrackingEvent>
			{
				MessageTrackingEvent.SUBMIT,
				MessageTrackingEvent.TRANSFER,
				MessageTrackingEvent.SEND,
				MessageTrackingEvent.DELIVER,
				MessageTrackingEvent.DUPLICATEDELIVER,
				MessageTrackingEvent.FAIL,
				MessageTrackingEvent.INITMESSAGECREATED,
				MessageTrackingEvent.MODERATORAPPROVE,
				MessageTrackingEvent.MODERATORREJECT,
				MessageTrackingEvent.MODERATIONEXPIRE,
				MessageTrackingEvent.PROCESS,
				MessageTrackingEvent.RESUBMIT,
				MessageTrackingEvent.HAREDIRECT
			};
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0005EBC6 File Offset: 0x0005CDC6
		public MessageTrackingLogEntry Clone()
		{
			return (MessageTrackingLogEntry)base.MemberwiseClone();
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0005EBD4 File Offset: 0x0005CDD4
		private static Dictionary<string, MimeRecipientType> CreateMimeRecipientTypeDictionary()
		{
			return new Dictionary<string, MimeRecipientType>(4, StringComparer.OrdinalIgnoreCase)
			{
				{
					"Unknown",
					MimeRecipientType.Unknown
				},
				{
					"To",
					MimeRecipientType.To
				},
				{
					"Cc",
					MimeRecipientType.Cc
				},
				{
					"Bcc",
					MimeRecipientType.Bcc
				}
			};
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0005EC20 File Offset: 0x0005CE20
		public ServerInfo GetNextHopServer()
		{
			if (this.nextHopServer == null)
			{
				if (string.IsNullOrEmpty(this.NextHopFqdnOrName))
				{
					return ServerInfo.NotFound;
				}
				this.nextHopServer = new ServerInfo?(ServerCache.Instance.FindMailboxOrHubServer(this.NextHopFqdnOrName, 32UL));
			}
			return this.nextHopServer.Value;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0005EC78 File Offset: 0x0005CE78
		public bool IsNextHopCrossSite(DirectoryContext directoryContext)
		{
			if (this.nextHopIsCrossSite == null)
			{
				ADObjectId localServerSiteId = ServerCache.Instance.GetLocalServerSiteId(directoryContext);
				ServerInfo serverInfo = this.GetNextHopServer();
				if (serverInfo.Status != ServerStatus.NotFound && serverInfo.ServerSiteId != null && !serverInfo.ServerSiteId.Equals(localServerSiteId))
				{
					this.nextHopIsCrossSite = new bool?(true);
				}
				else
				{
					this.nextHopIsCrossSite = new bool?(false);
				}
			}
			return this.nextHopIsCrossSite.Value;
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0005ECEC File Offset: 0x0005CEEC
		public bool SharesRowDataWithEntry(MessageTrackingLogEntry otherEntry)
		{
			return otherEntry != null && this.rowData == otherEntry.rowData;
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0005ED01 File Offset: 0x0005CF01
		public DateTime Time
		{
			get
			{
				return this.rowData.DateTime;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x0005ED0E File Offset: 0x0005CF0E
		public string MessageId
		{
			get
			{
				return this.rowData.MessageId;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0005ED1B File Offset: 0x0005CF1B
		public string Server
		{
			get
			{
				return this.rowData.ServerFqdn;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0005ED28 File Offset: 0x0005CF28
		public long InternalMessageId
		{
			get
			{
				return this.rowData.InternalMessageId;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0005ED35 File Offset: 0x0005CF35
		// (set) Token: 0x0600141E RID: 5150 RVA: 0x0005ED54 File Offset: 0x0005CF54
		public string[] RecipientAddresses
		{
			get
			{
				if (this.submitRecipientAddresses != null)
				{
					return this.submitRecipientAddresses;
				}
				return this.rowData.RecipientAddresses;
			}
			set
			{
				if (this.EventId != MessageTrackingEvent.SUBMIT && this.EventId != MessageTrackingEvent.MODERATORAPPROVE && this.EventId != MessageTrackingEvent.MODERATORREJECT && (this.EventId != MessageTrackingEvent.EXPAND || !"Federated Delivery Encryption Agent".Equals(this.SourceContext, StringComparison.OrdinalIgnoreCase)))
				{
					throw new InvalidOperationException("Recipient addresses can only be set for SUBMIT, moderator decision and federated delivery events, for all others log-data must be the source");
				}
				this.submitRecipientAddresses = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0005EDAD File Offset: 0x0005CFAD
		// (set) Token: 0x06001420 RID: 5152 RVA: 0x0005EDB5 File Offset: 0x0005CFB5
		public string RecipientAddress
		{
			get
			{
				return this.recipientAddress;
			}
			set
			{
				this.recipientAddress = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x0005EDBE File Offset: 0x0005CFBE
		// (set) Token: 0x06001422 RID: 5154 RVA: 0x0005EDC6 File Offset: 0x0005CFC6
		public string RootAddress
		{
			get
			{
				return this.rootAddress;
			}
			set
			{
				this.rootAddress = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x0005EDCF File Offset: 0x0005CFCF
		public MessageTrackingEvent EventId
		{
			get
			{
				return this.rowData.EventId;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x0005EDDC File Offset: 0x0005CFDC
		public MessageTrackingSource Source
		{
			get
			{
				return this.rowData.Source;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0005EDE9 File Offset: 0x0005CFE9
		public string SourceContext
		{
			get
			{
				return this.rowData.Context;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0005EDF6 File Offset: 0x0005CFF6
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x0005EDFE File Offset: 0x0005CFFE
		public EventTree ProcessedBy
		{
			get
			{
				return this.processedBy;
			}
			set
			{
				this.processedBy = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0005EE07 File Offset: 0x0005D007
		public virtual long ServerLogKeyMailItemId
		{
			get
			{
				if (this.EventId == MessageTrackingEvent.TRANSFER || (this.EventId == MessageTrackingEvent.RESUBMIT && this.Source == MessageTrackingSource.REDUNDANCY))
				{
					return this.relatedMailItemId;
				}
				return this.InternalMessageId;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0005EE34 File Offset: 0x0005D034
		public string NextHopFqdnOrName
		{
			get
			{
				if (string.IsNullOrEmpty(this.serverHostName))
				{
					return this.rowData.ServerHostName;
				}
				return this.serverHostName;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0005EE55 File Offset: 0x0005D055
		public string ServerIP
		{
			get
			{
				return this.rowData.ServerIP;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0005EE62 File Offset: 0x0005D062
		public string ClientHostName
		{
			get
			{
				if (string.IsNullOrEmpty(this.clientHostName))
				{
					return this.rowData.ClientHostName;
				}
				return this.clientHostName;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0005EE83 File Offset: 0x0005D083
		public string ClientIP
		{
			get
			{
				return this.rowData.ClientIP;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0005EE90 File Offset: 0x0005D090
		public string Subject
		{
			get
			{
				return this.rowData.MessageSubject;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0005EE9D File Offset: 0x0005D09D
		public string SenderAddress
		{
			get
			{
				return this.rowData.SenderAddress;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0005EEAA File Offset: 0x0005D0AA
		// (set) Token: 0x06001430 RID: 5168 RVA: 0x0005EEB2 File Offset: 0x0005D0B2
		public long RelatedMailItemId
		{
			get
			{
				return this.relatedMailItemId;
			}
			set
			{
				this.relatedMailItemId = value;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0005EEBB File Offset: 0x0005D0BB
		public string RelatedRecipientAddress
		{
			get
			{
				return this.rowData.RelatedRecipientAddress;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0005EEC8 File Offset: 0x0005D0C8
		public string Folder
		{
			get
			{
				if (this.recipientAddress == null || this.recipientFolders == null)
				{
					return null;
				}
				string result = null;
				this.recipientFolders.TryGetValue(this.recipientAddress, out result);
				return result;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0005EEFE File Offset: 0x0005D0FE
		public string[] RecipientStatuses
		{
			get
			{
				return this.rowData.RecipientStatuses;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0005EF0B File Offset: 0x0005D10B
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x0005EF13 File Offset: 0x0005D113
		public string RecipientStatus
		{
			get
			{
				return this.recipientStatus;
			}
			set
			{
				this.recipientStatus = value;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0005EF1C File Offset: 0x0005D11C
		public string InitMessageId
		{
			get
			{
				return this.rowData.References[0];
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0005EF2B File Offset: 0x0005D12B
		public SmtpAddress ArbitrationMailboxAddress
		{
			get
			{
				return this.arbitrationMailboxAddress;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0005EF33 File Offset: 0x0005D133
		public string OrigMessageId
		{
			get
			{
				return this.rowData.References[0];
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x0005EF42 File Offset: 0x0005D142
		// (set) Token: 0x0600143A RID: 5178 RVA: 0x0005EF4A File Offset: 0x0005D14A
		public bool HiddenRecipient
		{
			get
			{
				return this.hiddenRecipient;
			}
			set
			{
				this.hiddenRecipient = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x0005EF53 File Offset: 0x0005D153
		// (set) Token: 0x0600143C RID: 5180 RVA: 0x0005EF5B File Offset: 0x0005D15B
		public bool? BccRecipient
		{
			get
			{
				return this.bccRecipient;
			}
			set
			{
				this.bccRecipient = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0005EF64 File Offset: 0x0005D164
		// (set) Token: 0x0600143E RID: 5182 RVA: 0x0005EF6C File Offset: 0x0005D16C
		public string FederatedDeliveryAddress
		{
			get
			{
				return this.federatedDeliveryAddress;
			}
			set
			{
				this.federatedDeliveryAddress = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0005EF75 File Offset: 0x0005D175
		public bool IsTerminalEvent
		{
			get
			{
				return MessageTrackingLogEntry.terminalEventTypes.Contains(this.EventId);
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0005EF87 File Offset: 0x0005D187
		public KeyValuePair<string, object>[] CustomData
		{
			get
			{
				return this.rowData.CustomData;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0005EF94 File Offset: 0x0005D194
		public string TenantId
		{
			get
			{
				return this.rowData.TenantId;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0005EFA1 File Offset: 0x0005D1A1
		public bool IsEntryCompatible
		{
			get
			{
				return this.rowData.IsLogCompatible;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0005EFAE File Offset: 0x0005D1AE
		internal MessageTrackingLogRow LogRow
		{
			get
			{
				return this.rowData;
			}
		}

		// Token: 0x04000D3B RID: 3387
		private static HashSet<MessageTrackingEvent> terminalEventTypes = MessageTrackingLogEntry.CreateTerminalEventSet();

		// Token: 0x04000D3C RID: 3388
		private static Dictionary<string, MimeRecipientType> mimeRecipientTypes = MessageTrackingLogEntry.CreateMimeRecipientTypeDictionary();

		// Token: 0x04000D3D RID: 3389
		private static BitArray allRows = new BitArray(MessageTrackingLogRow.FieldCount, true);

		// Token: 0x04000D3E RID: 3390
		private static Regex serverNameFromHAContext = new Regex("(^.+)(?==250)");

		// Token: 0x04000D3F RID: 3391
		private static string OutboundProxyTargetHostNamePropertyName = "OutboundProxyTargetHostName";

		// Token: 0x04000D40 RID: 3392
		private MessageTrackingLogRow rowData;

		// Token: 0x04000D41 RID: 3393
		private long relatedMailItemId;

		// Token: 0x04000D42 RID: 3394
		private string recipientAddress;

		// Token: 0x04000D43 RID: 3395
		private string rootAddress;

		// Token: 0x04000D44 RID: 3396
		private string[] submitRecipientAddresses;

		// Token: 0x04000D45 RID: 3397
		private string federatedDeliveryAddress;

		// Token: 0x04000D46 RID: 3398
		private string recipientStatus;

		// Token: 0x04000D47 RID: 3399
		private Dictionary<string, string> recipientFolders;

		// Token: 0x04000D48 RID: 3400
		private ServerInfo? nextHopServer;

		// Token: 0x04000D49 RID: 3401
		private SmtpAddress arbitrationMailboxAddress;

		// Token: 0x04000D4A RID: 3402
		private bool? nextHopIsCrossSite;

		// Token: 0x04000D4B RID: 3403
		private EventTree processedBy;

		// Token: 0x04000D4C RID: 3404
		private bool hiddenRecipient;

		// Token: 0x04000D4D RID: 3405
		private bool? bccRecipient = null;

		// Token: 0x04000D4E RID: 3406
		private string serverHostName;

		// Token: 0x04000D4F RID: 3407
		private string clientHostName;

		// Token: 0x04000D50 RID: 3408
		public static readonly Dictionary<string, MimeRecipientType> RecipientAddressTypeGetter = new Dictionary<string, MimeRecipientType>
		{
			{
				"To",
				MimeRecipientType.To
			},
			{
				"Cc",
				MimeRecipientType.Cc
			},
			{
				"Bcc",
				MimeRecipientType.Bcc
			},
			{
				"Unknown",
				MimeRecipientType.Unknown
			}
		};
	}
}
