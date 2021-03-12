using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class ActiveCalls : UMDiagnosticObject
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020F8 File Offset: 0x000002F8
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002100 File Offset: 0x00000300
		public string GatewayId
		{
			get
			{
				return this.gatewayId;
			}
			set
			{
				this.gatewayId = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002109 File Offset: 0x00000309
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002111 File Offset: 0x00000311
		public string ServerId
		{
			get
			{
				return this.serverId;
			}
			set
			{
				this.serverId = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000211A File Offset: 0x0000031A
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002122 File Offset: 0x00000322
		public string DialPlan
		{
			get
			{
				return this.dialPlan;
			}
			set
			{
				this.dialPlan = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000212B File Offset: 0x0000032B
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002133 File Offset: 0x00000333
		public string DialedNumber
		{
			get
			{
				return this.dialedNumber;
			}
			set
			{
				this.dialedNumber = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000213C File Offset: 0x0000033C
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002144 File Offset: 0x00000344
		public string CallType
		{
			get
			{
				return this.callType;
			}
			set
			{
				this.callType = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000214D File Offset: 0x0000034D
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002155 File Offset: 0x00000355
		public string CallingNumber
		{
			get
			{
				return this.callingNumber;
			}
			set
			{
				this.callingNumber = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000215E File Offset: 0x0000035E
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002166 File Offset: 0x00000366
		public string DiversionNumber
		{
			get
			{
				return this.diversionNumber;
			}
			set
			{
				this.diversionNumber = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000216F File Offset: 0x0000036F
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002177 File Offset: 0x00000377
		public string CallState
		{
			get
			{
				return this.callState;
			}
			set
			{
				this.callState = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002180 File Offset: 0x00000380
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002188 File Offset: 0x00000388
		public string AppState
		{
			get
			{
				return this.appState;
			}
			set
			{
				this.appState = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002194 File Offset: 0x00000394
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x04000002 RID: 2
		private string gatewayId;

		// Token: 0x04000003 RID: 3
		private string serverId;

		// Token: 0x04000004 RID: 4
		private string dialPlan;

		// Token: 0x04000005 RID: 5
		private string dialedNumber;

		// Token: 0x04000006 RID: 6
		private string callType;

		// Token: 0x04000007 RID: 7
		private string callingNumber;

		// Token: 0x04000008 RID: 8
		private string diversionNumber;

		// Token: 0x04000009 RID: 9
		private string callState;

		// Token: 0x0400000A RID: 10
		private string appState;
	}
}
