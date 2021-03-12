using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200005F RID: 95
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class EasCommandFailedPermanentException : ConnectionsPermanentException
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00004D19 File Offset: 0x00002F19
		public EasCommandFailedPermanentException(string responseStatus, string httpStatus) : base(CXStrings.EasCommandFailed(responseStatus, httpStatus))
		{
			this.responseStatus = responseStatus;
			this.httpStatus = httpStatus;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00004D36 File Offset: 0x00002F36
		public EasCommandFailedPermanentException(string responseStatus, string httpStatus, Exception innerException) : base(CXStrings.EasCommandFailed(responseStatus, httpStatus), innerException)
		{
			this.responseStatus = responseStatus;
			this.httpStatus = httpStatus;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00004D54 File Offset: 0x00002F54
		protected EasCommandFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.responseStatus = (string)info.GetValue("responseStatus", typeof(string));
			this.httpStatus = (string)info.GetValue("httpStatus", typeof(string));
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00004DA9 File Offset: 0x00002FA9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("responseStatus", this.responseStatus);
			info.AddValue("httpStatus", this.httpStatus);
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00004DD5 File Offset: 0x00002FD5
		public string ResponseStatus
		{
			get
			{
				return this.responseStatus;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00004DDD File Offset: 0x00002FDD
		public string HttpStatus
		{
			get
			{
				return this.httpStatus;
			}
		}

		// Token: 0x040000F9 RID: 249
		private readonly string responseStatus;

		// Token: 0x040000FA RID: 250
		private readonly string httpStatus;
	}
}
