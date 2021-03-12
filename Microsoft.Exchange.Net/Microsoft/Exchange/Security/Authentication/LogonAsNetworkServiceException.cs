using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x020000EE RID: 238
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LogonAsNetworkServiceException : LocalizedException
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x000161B6 File Offset: 0x000143B6
		public LogonAsNetworkServiceException(string error) : base(NetException.LogonAsNetworkServiceFailed(error))
		{
			this.error = error;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000161CB File Offset: 0x000143CB
		public LogonAsNetworkServiceException(string error, Exception innerException) : base(NetException.LogonAsNetworkServiceFailed(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000161E1 File Offset: 0x000143E1
		protected LogonAsNetworkServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001620B File Offset: 0x0001440B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00016226 File Offset: 0x00014426
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040004FF RID: 1279
		private readonly string error;
	}
}
