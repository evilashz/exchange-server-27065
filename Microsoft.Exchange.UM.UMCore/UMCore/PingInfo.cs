using System;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200025E RID: 606
	internal class PingInfo
	{
		// Token: 0x060011D2 RID: 4562 RVA: 0x0004F8DC File Offset: 0x0004DADC
		internal PingInfo(UMSipPeer peer)
		{
			this.Peer = peer;
			this.ResponseText = string.Empty;
			this.Diagnostics = string.Empty;
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0004F901 File Offset: 0x0004DB01
		// (set) Token: 0x060011D4 RID: 4564 RVA: 0x0004F909 File Offset: 0x0004DB09
		internal UMSipPeer Peer { get; private set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0004F914 File Offset: 0x0004DB14
		internal string TargetUri
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "sip:{0}:{1}", new object[]
				{
					this.Peer.Address,
					this.Peer.Port
				});
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x0004F959 File Offset: 0x0004DB59
		// (set) Token: 0x060011D7 RID: 4567 RVA: 0x0004F961 File Offset: 0x0004DB61
		internal int ResponseCode { get; set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x0004F96A File Offset: 0x0004DB6A
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x0004F972 File Offset: 0x0004DB72
		internal string ResponseText
		{
			get
			{
				return this.responseText;
			}
			set
			{
				if (value != null)
				{
					this.responseText = value;
					return;
				}
				this.responseText = string.Empty;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x0004F98A File Offset: 0x0004DB8A
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x0004F992 File Offset: 0x0004DB92
		internal string Diagnostics { get; set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0004F99B File Offset: 0x0004DB9B
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x0004F9A3 File Offset: 0x0004DBA3
		internal Exception Error { get; set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0004F9AC File Offset: 0x0004DBAC
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x0004F9B4 File Offset: 0x0004DBB4
		private ExDateTime? StartTime { get; set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0004F9BD File Offset: 0x0004DBBD
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x0004F9C5 File Offset: 0x0004DBC5
		private ExDateTime? EndTime { get; set; }

		// Token: 0x060011E2 RID: 4578 RVA: 0x0004F9D0 File Offset: 0x0004DBD0
		internal void RecordStartTime()
		{
			this.StartTime = new ExDateTime?(ExDateTime.UtcNow);
			this.EndTime = null;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0004F9FC File Offset: 0x0004DBFC
		internal void RecordStopTime()
		{
			this.EndTime = new ExDateTime?(ExDateTime.UtcNow);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0004FA10 File Offset: 0x0004DC10
		internal TimeSpan GetElaspedTime()
		{
			if (this.EndTime == null || this.StartTime == null)
			{
				throw new InvalidOperationException("PingInfo.RecordStartTime and RecordStopTime must be called before GetElaspedTime");
			}
			return this.EndTime.Value.Subtract(this.StartTime.Value);
		}

		// Token: 0x04000BF4 RID: 3060
		private string responseText;
	}
}
