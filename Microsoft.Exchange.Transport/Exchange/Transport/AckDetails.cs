using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200013F RID: 319
	public sealed class AckDetails : IEquatable<AckDetails>
	{
		// Token: 0x06000E2A RID: 3626 RVA: 0x00034B7C File Offset: 0x00032D7C
		public AckDetails(IPEndPoint remoteEndPoint, string remoteHostName, string sessionId, string connectorId, IPAddress sourceIP) : this(remoteEndPoint, remoteHostName, sessionId, connectorId, sourceIP, null)
		{
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00034B9F File Offset: 0x00032D9F
		public AckDetails(IPEndPoint remoteEndPoint, string remoteHostName, string sessionId, string connectorId, IPAddress sourceIP, DateTime? lastRetryTime)
		{
			this.remoteEndPoint = remoteEndPoint;
			this.remoteHostName = remoteHostName;
			this.sessionId = sessionId;
			this.connectorId = connectorId;
			this.sourceIPAddress = sourceIP;
			this.lastRetryTime = lastRetryTime;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00034BD4 File Offset: 0x00032DD4
		public AckDetails(string remoteHostName)
		{
			this.remoteHostName = remoteHostName;
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00034BE3 File Offset: 0x00032DE3
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00034BEB File Offset: 0x00032DEB
		public string RemoteHostName
		{
			get
			{
				return this.remoteHostName;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x00034BF3 File Offset: 0x00032DF3
		public string SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00034BFB File Offset: 0x00032DFB
		public string ConnectorId
		{
			get
			{
				return this.connectorId;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x00034C03 File Offset: 0x00032E03
		public IPAddress SourceIPAddress
		{
			get
			{
				return this.sourceIPAddress;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00034C0B File Offset: 0x00032E0B
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x00034C13 File Offset: 0x00032E13
		public DateTime? LastRetryTime
		{
			get
			{
				return this.lastRetryTime;
			}
			set
			{
				this.lastRetryTime = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x00034C1C File Offset: 0x00032E1C
		public List<KeyValuePair<string, string>> ExtraEventData
		{
			get
			{
				return this.extraEventData;
			}
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00034C24 File Offset: 0x00032E24
		public void AddEventData(string name, string value)
		{
			if (this.extraEventData == null)
			{
				this.extraEventData = new List<KeyValuePair<string, string>>();
			}
			this.extraEventData.Add(new KeyValuePair<string, string>(name, value));
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00034C4C File Offset: 0x00032E4C
		public override int GetHashCode()
		{
			int num = (this.remoteHostName != null) ? this.remoteHostName.GetHashCode() : 0;
			num = (num * 397 ^ ((this.sessionId != null) ? this.sessionId.GetHashCode() : 0));
			num = (num * 397 ^ ((this.connectorId != null) ? this.connectorId.GetHashCode() : 0));
			num = (num * 397 ^ ((this.remoteEndPoint != null) ? this.remoteEndPoint.GetHashCode() : 0));
			num = (num * 397 ^ ((this.sourceIPAddress != null) ? this.sourceIPAddress.GetHashCode() : 0));
			num = (num * 397 ^ this.lastRetryTime.GetHashCode());
			return num * 397 ^ ((this.extraEventData != null) ? this.extraEventData.GetHashCode() : 0);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00034D26 File Offset: 0x00032F26
		public override bool Equals(object other)
		{
			return this.Equals(other as AckDetails);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00034D34 File Offset: 0x00032F34
		public bool Equals(AckDetails other)
		{
			return !object.ReferenceEquals(other, null) && (object.ReferenceEquals(this, other) || (((this.remoteHostName == null && other.remoteHostName == null) || (this.remoteHostName != null && this.remoteHostName.Equals(other.remoteHostName))) && ((this.sessionId == null && other.sessionId == null) || (this.sessionId != null && this.sessionId.Equals(other.sessionId))) && ((this.connectorId == null && other.connectorId == null) || (this.connectorId != null && this.connectorId.Equals(other.connectorId))) && ((this.remoteEndPoint == null && other.remoteEndPoint == null) || (this.remoteEndPoint != null && this.remoteEndPoint.Equals(other.remoteEndPoint))) && ((this.sourceIPAddress == null && other.sourceIPAddress == null) || (this.sourceIPAddress != null && this.sourceIPAddress.Equals(other.sourceIPAddress))) && ((this.lastRetryTime == null && other.lastRetryTime == null) || (this.lastRetryTime != null && this.lastRetryTime.Equals(other.lastRetryTime))) && ((this.extraEventData == null && other.extraEventData == null) || (this.extraEventData != null && other.extraEventData != null && this.extraEventData.SequenceEqual(other.extraEventData)))));
		}

		// Token: 0x0400061B RID: 1563
		private readonly string remoteHostName;

		// Token: 0x0400061C RID: 1564
		private readonly string sessionId;

		// Token: 0x0400061D RID: 1565
		private readonly string connectorId;

		// Token: 0x0400061E RID: 1566
		private readonly IPEndPoint remoteEndPoint;

		// Token: 0x0400061F RID: 1567
		private readonly IPAddress sourceIPAddress;

		// Token: 0x04000620 RID: 1568
		private DateTime? lastRetryTime;

		// Token: 0x04000621 RID: 1569
		private List<KeyValuePair<string, string>> extraEventData;
	}
}
