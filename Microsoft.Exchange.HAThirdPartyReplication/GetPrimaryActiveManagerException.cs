using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x0200001D RID: 29
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GetPrimaryActiveManagerException : ThirdPartyReplicationException
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003511 File Offset: 0x00001711
		public GetPrimaryActiveManagerException(string reason) : base(ThirdPartyReplication.GetPamError(reason))
		{
			this.reason = reason;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000352B File Offset: 0x0000172B
		public GetPrimaryActiveManagerException(string reason, Exception innerException) : base(ThirdPartyReplication.GetPamError(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003546 File Offset: 0x00001746
		protected GetPrimaryActiveManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003570 File Offset: 0x00001770
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000358B File Offset: 0x0000178B
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x0400002A RID: 42
		private readonly string reason;
	}
}
