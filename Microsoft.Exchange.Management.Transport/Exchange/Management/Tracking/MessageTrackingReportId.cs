using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.Common.MessageTracking;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x020000A3 RID: 163
	[Serializable]
	public class MessageTrackingReportId : IIdentityParameter
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x00016BD3 File Offset: 0x00014DD3
		public MessageTrackingReportId()
		{
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00016BDC File Offset: 0x00014DDC
		public static MessageTrackingReportId Parse(string identity)
		{
			MessageTrackingReportId messageTrackingReportId;
			if (!MessageTrackingReportId.TryParse(identity, out messageTrackingReportId))
			{
				return null;
			}
			return new MessageTrackingReportId(messageTrackingReportId);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00016BFB File Offset: 0x00014DFB
		public override string ToString()
		{
			return this.internalMessageTrackingReportId.ToString();
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00016C08 File Offset: 0x00014E08
		public string MessageId
		{
			get
			{
				return this.internalMessageTrackingReportId.MessageId;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00016C15 File Offset: 0x00014E15
		public long InternalMessageId
		{
			get
			{
				return this.internalMessageTrackingReportId.InternalMessageId;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00016C22 File Offset: 0x00014E22
		public string Server
		{
			get
			{
				return this.internalMessageTrackingReportId.Server;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00016C2F File Offset: 0x00014E2F
		public SmtpAddress Mailbox
		{
			get
			{
				return this.internalMessageTrackingReportId.Mailbox;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00016C3C File Offset: 0x00014E3C
		public Guid UserGuid
		{
			get
			{
				return this.internalMessageTrackingReportId.UserGuid;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00016C49 File Offset: 0x00014E49
		public bool IsSender
		{
			get
			{
				return this.internalMessageTrackingReportId.IsSender;
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00016C58 File Offset: 0x00014E58
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00016C70 File Offset: 0x00014E70
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00016C77 File Offset: 0x00014E77
		public void Initialize(ObjectId objectId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00016C7E File Offset: 0x00014E7E
		public string RawIdentity
		{
			get
			{
				return this.internalMessageTrackingReportId.ToString();
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00016C8B File Offset: 0x00014E8B
		internal MessageTrackingReportId InternalMessageTrackingReportId
		{
			get
			{
				return this.internalMessageTrackingReportId;
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00016C93 File Offset: 0x00014E93
		internal MessageTrackingReportId(MessageTrackingReportId internalMessageTrackingReportId)
		{
			this.internalMessageTrackingReportId = internalMessageTrackingReportId;
		}

		// Token: 0x0400020E RID: 526
		private MessageTrackingReportId internalMessageTrackingReportId;
	}
}
