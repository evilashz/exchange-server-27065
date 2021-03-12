using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000102 RID: 258
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class QuotaExceededPermanentException : LocalizedException
	{
		// Token: 0x060006A6 RID: 1702 RVA: 0x00016E56 File Offset: 0x00015056
		public QuotaExceededPermanentException(string serviceURI, LocalizedString exceptionMessage) : base(NetServerException.QuotaExceededError(serviceURI, exceptionMessage))
		{
			this.serviceURI = serviceURI;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00016E73 File Offset: 0x00015073
		public QuotaExceededPermanentException(string serviceURI, LocalizedString exceptionMessage, Exception innerException) : base(NetServerException.QuotaExceededError(serviceURI, exceptionMessage), innerException)
		{
			this.serviceURI = serviceURI;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00016E94 File Offset: 0x00015094
		protected QuotaExceededPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceURI = (string)info.GetValue("serviceURI", typeof(string));
			this.exceptionMessage = (LocalizedString)info.GetValue("exceptionMessage", typeof(LocalizedString));
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00016EE9 File Offset: 0x000150E9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceURI", this.serviceURI);
			info.AddValue("exceptionMessage", this.exceptionMessage);
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00016F1A File Offset: 0x0001511A
		public string ServiceURI
		{
			get
			{
				return this.serviceURI;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00016F22 File Offset: 0x00015122
		public LocalizedString ExceptionMessage
		{
			get
			{
				return this.exceptionMessage;
			}
		}

		// Token: 0x0400052D RID: 1325
		private readonly string serviceURI;

		// Token: 0x0400052E RID: 1326
		private readonly LocalizedString exceptionMessage;
	}
}
