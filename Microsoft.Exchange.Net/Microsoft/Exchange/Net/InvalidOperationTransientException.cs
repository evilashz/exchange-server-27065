using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000FE RID: 254
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidOperationTransientException : TransientException
	{
		// Token: 0x0600068E RID: 1678 RVA: 0x00016B06 File Offset: 0x00014D06
		public InvalidOperationTransientException(string serviceURI, LocalizedString exceptionMessage) : base(NetServerException.InvalidOperationError(serviceURI, exceptionMessage))
		{
			this.serviceURI = serviceURI;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00016B23 File Offset: 0x00014D23
		public InvalidOperationTransientException(string serviceURI, LocalizedString exceptionMessage, Exception innerException) : base(NetServerException.InvalidOperationError(serviceURI, exceptionMessage), innerException)
		{
			this.serviceURI = serviceURI;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00016B44 File Offset: 0x00014D44
		protected InvalidOperationTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceURI = (string)info.GetValue("serviceURI", typeof(string));
			this.exceptionMessage = (LocalizedString)info.GetValue("exceptionMessage", typeof(LocalizedString));
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00016B99 File Offset: 0x00014D99
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceURI", this.serviceURI);
			info.AddValue("exceptionMessage", this.exceptionMessage);
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00016BCA File Offset: 0x00014DCA
		public string ServiceURI
		{
			get
			{
				return this.serviceURI;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00016BD2 File Offset: 0x00014DD2
		public LocalizedString ExceptionMessage
		{
			get
			{
				return this.exceptionMessage;
			}
		}

		// Token: 0x04000525 RID: 1317
		private readonly string serviceURI;

		// Token: 0x04000526 RID: 1318
		private readonly LocalizedString exceptionMessage;
	}
}
