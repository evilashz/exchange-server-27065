using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000FC RID: 252
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TimeoutErrorTransientException : TransientException
	{
		// Token: 0x06000682 RID: 1666 RVA: 0x00016961 File Offset: 0x00014B61
		public TimeoutErrorTransientException(string serviceURI, LocalizedString exceptionMessage) : base(NetServerException.TimeoutError(serviceURI, exceptionMessage))
		{
			this.serviceURI = serviceURI;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001697E File Offset: 0x00014B7E
		public TimeoutErrorTransientException(string serviceURI, LocalizedString exceptionMessage, Exception innerException) : base(NetServerException.TimeoutError(serviceURI, exceptionMessage), innerException)
		{
			this.serviceURI = serviceURI;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001699C File Offset: 0x00014B9C
		protected TimeoutErrorTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceURI = (string)info.GetValue("serviceURI", typeof(string));
			this.exceptionMessage = (LocalizedString)info.GetValue("exceptionMessage", typeof(LocalizedString));
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000169F1 File Offset: 0x00014BF1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceURI", this.serviceURI);
			info.AddValue("exceptionMessage", this.exceptionMessage);
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00016A22 File Offset: 0x00014C22
		public string ServiceURI
		{
			get
			{
				return this.serviceURI;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00016A2A File Offset: 0x00014C2A
		public LocalizedString ExceptionMessage
		{
			get
			{
				return this.exceptionMessage;
			}
		}

		// Token: 0x04000521 RID: 1313
		private readonly string serviceURI;

		// Token: 0x04000522 RID: 1314
		private readonly LocalizedString exceptionMessage;
	}
}
