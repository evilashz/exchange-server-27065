using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000101 RID: 257
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidDataTransientException : TransientException
	{
		// Token: 0x060006A0 RID: 1696 RVA: 0x00016D82 File Offset: 0x00014F82
		public InvalidDataTransientException(string serviceURI, LocalizedString exceptionMessage) : base(NetServerException.InvalidDataError(serviceURI, exceptionMessage))
		{
			this.serviceURI = serviceURI;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00016D9F File Offset: 0x00014F9F
		public InvalidDataTransientException(string serviceURI, LocalizedString exceptionMessage, Exception innerException) : base(NetServerException.InvalidDataError(serviceURI, exceptionMessage), innerException)
		{
			this.serviceURI = serviceURI;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00016DC0 File Offset: 0x00014FC0
		protected InvalidDataTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceURI = (string)info.GetValue("serviceURI", typeof(string));
			this.exceptionMessage = (LocalizedString)info.GetValue("exceptionMessage", typeof(LocalizedString));
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00016E15 File Offset: 0x00015015
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceURI", this.serviceURI);
			info.AddValue("exceptionMessage", this.exceptionMessage);
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00016E46 File Offset: 0x00015046
		public string ServiceURI
		{
			get
			{
				return this.serviceURI;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00016E4E File Offset: 0x0001504E
		public LocalizedString ExceptionMessage
		{
			get
			{
				return this.exceptionMessage;
			}
		}

		// Token: 0x0400052B RID: 1323
		private readonly string serviceURI;

		// Token: 0x0400052C RID: 1324
		private readonly LocalizedString exceptionMessage;
	}
}
