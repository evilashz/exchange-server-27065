using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000326 RID: 806
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PhotoRetrievalFailedUnauthorizedAccessException : LocalizedException
	{
		// Token: 0x06001916 RID: 6422 RVA: 0x00073B86 File Offset: 0x00071D86
		public PhotoRetrievalFailedUnauthorizedAccessException(string innerExceptionMessage) : base(Strings.PhotoRetrievalFailedUnauthorizedAccessError(innerExceptionMessage))
		{
			this.innerExceptionMessage = innerExceptionMessage;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00073B9B File Offset: 0x00071D9B
		public PhotoRetrievalFailedUnauthorizedAccessException(string innerExceptionMessage, Exception innerException) : base(Strings.PhotoRetrievalFailedUnauthorizedAccessError(innerExceptionMessage), innerException)
		{
			this.innerExceptionMessage = innerExceptionMessage;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00073BB1 File Offset: 0x00071DB1
		protected PhotoRetrievalFailedUnauthorizedAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.innerExceptionMessage = (string)info.GetValue("innerExceptionMessage", typeof(string));
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00073BDB File Offset: 0x00071DDB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("innerExceptionMessage", this.innerExceptionMessage);
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x00073BF6 File Offset: 0x00071DF6
		public string InnerExceptionMessage
		{
			get
			{
				return this.innerExceptionMessage;
			}
		}

		// Token: 0x04001142 RID: 4418
		private readonly string innerExceptionMessage;
	}
}
