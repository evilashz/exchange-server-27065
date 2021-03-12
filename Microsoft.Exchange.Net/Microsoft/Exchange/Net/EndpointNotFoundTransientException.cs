using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000FD RID: 253
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EndpointNotFoundTransientException : TransientException
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x00016A32 File Offset: 0x00014C32
		public EndpointNotFoundTransientException(string serviceURI, LocalizedString exceptionMessage) : base(NetServerException.EndpointNotFoundError(serviceURI, exceptionMessage))
		{
			this.serviceURI = serviceURI;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00016A4F File Offset: 0x00014C4F
		public EndpointNotFoundTransientException(string serviceURI, LocalizedString exceptionMessage, Exception innerException) : base(NetServerException.EndpointNotFoundError(serviceURI, exceptionMessage), innerException)
		{
			this.serviceURI = serviceURI;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00016A70 File Offset: 0x00014C70
		protected EndpointNotFoundTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceURI = (string)info.GetValue("serviceURI", typeof(string));
			this.exceptionMessage = (LocalizedString)info.GetValue("exceptionMessage", typeof(LocalizedString));
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00016AC5 File Offset: 0x00014CC5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceURI", this.serviceURI);
			info.AddValue("exceptionMessage", this.exceptionMessage);
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00016AF6 File Offset: 0x00014CF6
		public string ServiceURI
		{
			get
			{
				return this.serviceURI;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00016AFE File Offset: 0x00014CFE
		public LocalizedString ExceptionMessage
		{
			get
			{
				return this.exceptionMessage;
			}
		}

		// Token: 0x04000523 RID: 1315
		private readonly string serviceURI;

		// Token: 0x04000524 RID: 1316
		private readonly LocalizedString exceptionMessage;
	}
}
