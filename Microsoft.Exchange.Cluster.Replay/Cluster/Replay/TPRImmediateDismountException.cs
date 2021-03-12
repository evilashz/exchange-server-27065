using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003AA RID: 938
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TPRImmediateDismountException : TransientException
	{
		// Token: 0x060027A2 RID: 10146 RVA: 0x000B64A7 File Offset: 0x000B46A7
		public TPRImmediateDismountException(Guid dbId, string reason) : base(ReplayStrings.TPRmmediateDismountFailed(dbId, reason))
		{
			this.dbId = dbId;
			this.reason = reason;
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000B64C4 File Offset: 0x000B46C4
		public TPRImmediateDismountException(Guid dbId, string reason, Exception innerException) : base(ReplayStrings.TPRmmediateDismountFailed(dbId, reason), innerException)
		{
			this.dbId = dbId;
			this.reason = reason;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000B64E4 File Offset: 0x000B46E4
		protected TPRImmediateDismountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbId = (Guid)info.GetValue("dbId", typeof(Guid));
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000B6539 File Offset: 0x000B4739
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbId", this.dbId);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060027A6 RID: 10150 RVA: 0x000B656A File Offset: 0x000B476A
		public Guid DbId
		{
			get
			{
				return this.dbId;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x000B6572 File Offset: 0x000B4772
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040013A1 RID: 5025
		private readonly Guid dbId;

		// Token: 0x040013A2 RID: 5026
		private readonly string reason;
	}
}
