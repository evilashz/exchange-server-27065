using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200005E RID: 94
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class EasCommandFailedTransientException : ConnectionsTransientException
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00004C4D File Offset: 0x00002E4D
		public EasCommandFailedTransientException(string responseStatus, string httpStatus) : base(CXStrings.EasCommandFailed(responseStatus, httpStatus))
		{
			this.responseStatus = responseStatus;
			this.httpStatus = httpStatus;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00004C6A File Offset: 0x00002E6A
		public EasCommandFailedTransientException(string responseStatus, string httpStatus, Exception innerException) : base(CXStrings.EasCommandFailed(responseStatus, httpStatus), innerException)
		{
			this.responseStatus = responseStatus;
			this.httpStatus = httpStatus;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00004C88 File Offset: 0x00002E88
		protected EasCommandFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.responseStatus = (string)info.GetValue("responseStatus", typeof(string));
			this.httpStatus = (string)info.GetValue("httpStatus", typeof(string));
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00004CDD File Offset: 0x00002EDD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("responseStatus", this.responseStatus);
			info.AddValue("httpStatus", this.httpStatus);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00004D09 File Offset: 0x00002F09
		public string ResponseStatus
		{
			get
			{
				return this.responseStatus;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00004D11 File Offset: 0x00002F11
		public string HttpStatus
		{
			get
			{
				return this.httpStatus;
			}
		}

		// Token: 0x040000F7 RID: 247
		private readonly string responseStatus;

		// Token: 0x040000F8 RID: 248
		private readonly string httpStatus;
	}
}
