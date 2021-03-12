using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Exchange.UM.UMService.Exceptions;

namespace Microsoft.Exchange.UM.UMService
{
	// Token: 0x0200022A RID: 554
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UMServiceControlChannelException : UMServiceBaseException
	{
		// Token: 0x06001195 RID: 4501 RVA: 0x0003A89A File Offset: 0x00038A9A
		public UMServiceControlChannelException(int port, string errorMessage) : base(Strings.UMServiceControlChannelException(port, errorMessage))
		{
			this.port = port;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0003A8BC File Offset: 0x00038ABC
		public UMServiceControlChannelException(int port, string errorMessage, Exception innerException) : base(Strings.UMServiceControlChannelException(port, errorMessage), innerException)
		{
			this.port = port;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0003A8E0 File Offset: 0x00038AE0
		protected UMServiceControlChannelException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.port = (int)info.GetValue("port", typeof(int));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0003A935 File Offset: 0x00038B35
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("port", this.port);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0003A961 File Offset: 0x00038B61
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0003A969 File Offset: 0x00038B69
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x040008C7 RID: 2247
		private readonly int port;

		// Token: 0x040008C8 RID: 2248
		private readonly string errorMessage;
	}
}
