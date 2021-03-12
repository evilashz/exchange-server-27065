using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000325 RID: 805
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PhotoRetrievalFailedWin32Exception : LocalizedException
	{
		// Token: 0x06001911 RID: 6417 RVA: 0x00073B0E File Offset: 0x00071D0E
		public PhotoRetrievalFailedWin32Exception(string innerExceptionMessage) : base(Strings.PhotoRetrievalFailedWin32Error(innerExceptionMessage))
		{
			this.innerExceptionMessage = innerExceptionMessage;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00073B23 File Offset: 0x00071D23
		public PhotoRetrievalFailedWin32Exception(string innerExceptionMessage, Exception innerException) : base(Strings.PhotoRetrievalFailedWin32Error(innerExceptionMessage), innerException)
		{
			this.innerExceptionMessage = innerExceptionMessage;
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00073B39 File Offset: 0x00071D39
		protected PhotoRetrievalFailedWin32Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.innerExceptionMessage = (string)info.GetValue("innerExceptionMessage", typeof(string));
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00073B63 File Offset: 0x00071D63
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("innerExceptionMessage", this.innerExceptionMessage);
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x00073B7E File Offset: 0x00071D7E
		public string InnerExceptionMessage
		{
			get
			{
				return this.innerExceptionMessage;
			}
		}

		// Token: 0x04001141 RID: 4417
		private readonly string innerExceptionMessage;
	}
}
