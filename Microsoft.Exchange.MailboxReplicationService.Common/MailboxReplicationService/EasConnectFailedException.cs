using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200037A RID: 890
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasConnectFailedException : MailboxReplicationTransientException
	{
		// Token: 0x0600271C RID: 10012 RVA: 0x000541FB File Offset: 0x000523FB
		public EasConnectFailedException(string connectStatus, string httpStatus, string smtpAddress) : base(MrsStrings.EasConnectFailed(connectStatus, httpStatus, smtpAddress))
		{
			this.connectStatus = connectStatus;
			this.httpStatus = httpStatus;
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x00054220 File Offset: 0x00052420
		public EasConnectFailedException(string connectStatus, string httpStatus, string smtpAddress, Exception innerException) : base(MrsStrings.EasConnectFailed(connectStatus, httpStatus, smtpAddress), innerException)
		{
			this.connectStatus = connectStatus;
			this.httpStatus = httpStatus;
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x00054248 File Offset: 0x00052448
		protected EasConnectFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.connectStatus = (string)info.GetValue("connectStatus", typeof(string));
			this.httpStatus = (string)info.GetValue("httpStatus", typeof(string));
			this.smtpAddress = (string)info.GetValue("smtpAddress", typeof(string));
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x000542BD File Offset: 0x000524BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("connectStatus", this.connectStatus);
			info.AddValue("httpStatus", this.httpStatus);
			info.AddValue("smtpAddress", this.smtpAddress);
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06002720 RID: 10016 RVA: 0x000542FA File Offset: 0x000524FA
		public string ConnectStatus
		{
			get
			{
				return this.connectStatus;
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06002721 RID: 10017 RVA: 0x00054302 File Offset: 0x00052502
		public string HttpStatus
		{
			get
			{
				return this.httpStatus;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x0005430A File Offset: 0x0005250A
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x0400107D RID: 4221
		private readonly string connectStatus;

		// Token: 0x0400107E RID: 4222
		private readonly string httpStatus;

		// Token: 0x0400107F RID: 4223
		private readonly string smtpAddress;
	}
}
