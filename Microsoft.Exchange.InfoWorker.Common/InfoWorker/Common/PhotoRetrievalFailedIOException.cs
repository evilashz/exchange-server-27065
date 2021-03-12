using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000324 RID: 804
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PhotoRetrievalFailedIOException : LocalizedException
	{
		// Token: 0x0600190C RID: 6412 RVA: 0x00073A96 File Offset: 0x00071C96
		public PhotoRetrievalFailedIOException(string innerExceptionMessage) : base(Strings.PhotoRetrievalFailedIOError(innerExceptionMessage))
		{
			this.innerExceptionMessage = innerExceptionMessage;
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00073AAB File Offset: 0x00071CAB
		public PhotoRetrievalFailedIOException(string innerExceptionMessage, Exception innerException) : base(Strings.PhotoRetrievalFailedIOError(innerExceptionMessage), innerException)
		{
			this.innerExceptionMessage = innerExceptionMessage;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00073AC1 File Offset: 0x00071CC1
		protected PhotoRetrievalFailedIOException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.innerExceptionMessage = (string)info.GetValue("innerExceptionMessage", typeof(string));
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00073AEB File Offset: 0x00071CEB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("innerExceptionMessage", this.innerExceptionMessage);
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x00073B06 File Offset: 0x00071D06
		public string InnerExceptionMessage
		{
			get
			{
				return this.innerExceptionMessage;
			}
		}

		// Token: 0x04001140 RID: 4416
		private readonly string innerExceptionMessage;
	}
}
