using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000EFA RID: 3834
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotLocateServerException : LocalizedException
	{
		// Token: 0x0600A9CB RID: 43467 RVA: 0x0028C545 File Offset: 0x0028A745
		public CannotLocateServerException(string errorMsg) : base(Strings.CannotLocateServer(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600A9CC RID: 43468 RVA: 0x0028C55A File Offset: 0x0028A75A
		public CannotLocateServerException(string errorMsg, Exception innerException) : base(Strings.CannotLocateServer(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600A9CD RID: 43469 RVA: 0x0028C570 File Offset: 0x0028A770
		protected CannotLocateServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x0600A9CE RID: 43470 RVA: 0x0028C59A File Offset: 0x0028A79A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17003700 RID: 14080
		// (get) Token: 0x0600A9CF RID: 43471 RVA: 0x0028C5B5 File Offset: 0x0028A7B5
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04006066 RID: 24678
		private readonly string errorMsg;
	}
}
