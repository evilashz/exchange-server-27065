using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000100 RID: 256
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CommunicationErrorPermanentException : LocalizedException
	{
		// Token: 0x0600069A RID: 1690 RVA: 0x00016CAE File Offset: 0x00014EAE
		public CommunicationErrorPermanentException(string serviceURI, LocalizedString exceptionMessage) : base(NetServerException.CommunicationError(serviceURI, exceptionMessage))
		{
			this.serviceURI = serviceURI;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00016CCB File Offset: 0x00014ECB
		public CommunicationErrorPermanentException(string serviceURI, LocalizedString exceptionMessage, Exception innerException) : base(NetServerException.CommunicationError(serviceURI, exceptionMessage), innerException)
		{
			this.serviceURI = serviceURI;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00016CEC File Offset: 0x00014EEC
		protected CommunicationErrorPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceURI = (string)info.GetValue("serviceURI", typeof(string));
			this.exceptionMessage = (LocalizedString)info.GetValue("exceptionMessage", typeof(LocalizedString));
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00016D41 File Offset: 0x00014F41
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceURI", this.serviceURI);
			info.AddValue("exceptionMessage", this.exceptionMessage);
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00016D72 File Offset: 0x00014F72
		public string ServiceURI
		{
			get
			{
				return this.serviceURI;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00016D7A File Offset: 0x00014F7A
		public LocalizedString ExceptionMessage
		{
			get
			{
				return this.exceptionMessage;
			}
		}

		// Token: 0x04000529 RID: 1321
		private readonly string serviceURI;

		// Token: 0x0400052A RID: 1322
		private readonly LocalizedString exceptionMessage;
	}
}
