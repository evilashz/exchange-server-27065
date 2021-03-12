using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000332 RID: 818
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MoveInProgressReservationException : ResourceReservationException
	{
		// Token: 0x060025B6 RID: 9654 RVA: 0x00051FD8 File Offset: 0x000501D8
		public MoveInProgressReservationException(string resourceName, string clientName) : base(MrsStrings.ErrorMoveInProgress(resourceName, clientName))
		{
			this.resourceName = resourceName;
			this.clientName = clientName;
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x00051FF5 File Offset: 0x000501F5
		public MoveInProgressReservationException(string resourceName, string clientName, Exception innerException) : base(MrsStrings.ErrorMoveInProgress(resourceName, clientName), innerException)
		{
			this.resourceName = resourceName;
			this.clientName = clientName;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x00052014 File Offset: 0x00050214
		protected MoveInProgressReservationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.resourceName = (string)info.GetValue("resourceName", typeof(string));
			this.clientName = (string)info.GetValue("clientName", typeof(string));
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x00052069 File Offset: 0x00050269
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("resourceName", this.resourceName);
			info.AddValue("clientName", this.clientName);
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x00052095 File Offset: 0x00050295
		public string ResourceName
		{
			get
			{
				return this.resourceName;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x0005209D File Offset: 0x0005029D
		public string ClientName
		{
			get
			{
				return this.clientName;
			}
		}

		// Token: 0x04001037 RID: 4151
		private readonly string resourceName;

		// Token: 0x04001038 RID: 4152
		private readonly string clientName;
	}
}
