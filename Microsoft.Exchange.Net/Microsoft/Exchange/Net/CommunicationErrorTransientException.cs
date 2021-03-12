using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000FF RID: 255
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CommunicationErrorTransientException : TransientException
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x00016BDA File Offset: 0x00014DDA
		public CommunicationErrorTransientException(string serviceURI, LocalizedString exceptionMessage) : base(NetServerException.CommunicationError(serviceURI, exceptionMessage))
		{
			this.serviceURI = serviceURI;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00016BF7 File Offset: 0x00014DF7
		public CommunicationErrorTransientException(string serviceURI, LocalizedString exceptionMessage, Exception innerException) : base(NetServerException.CommunicationError(serviceURI, exceptionMessage), innerException)
		{
			this.serviceURI = serviceURI;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00016C18 File Offset: 0x00014E18
		protected CommunicationErrorTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceURI = (string)info.GetValue("serviceURI", typeof(string));
			this.exceptionMessage = (LocalizedString)info.GetValue("exceptionMessage", typeof(LocalizedString));
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00016C6D File Offset: 0x00014E6D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceURI", this.serviceURI);
			info.AddValue("exceptionMessage", this.exceptionMessage);
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00016C9E File Offset: 0x00014E9E
		public string ServiceURI
		{
			get
			{
				return this.serviceURI;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00016CA6 File Offset: 0x00014EA6
		public LocalizedString ExceptionMessage
		{
			get
			{
				return this.exceptionMessage;
			}
		}

		// Token: 0x04000527 RID: 1319
		private readonly string serviceURI;

		// Token: 0x04000528 RID: 1320
		private readonly LocalizedString exceptionMessage;
	}
}
