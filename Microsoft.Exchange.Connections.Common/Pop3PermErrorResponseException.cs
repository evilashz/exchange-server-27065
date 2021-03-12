using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000051 RID: 81
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3PermErrorResponseException : LocalizedException
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00004652 File Offset: 0x00002852
		public Pop3PermErrorResponseException(string command, string response) : base(CXStrings.Pop3PermErrorResponseMsg(command, response))
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000466F File Offset: 0x0000286F
		public Pop3PermErrorResponseException(string command, string response, Exception innerException) : base(CXStrings.Pop3PermErrorResponseMsg(command, response), innerException)
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00004690 File Offset: 0x00002890
		protected Pop3PermErrorResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.command = (string)info.GetValue("command", typeof(string));
			this.response = (string)info.GetValue("response", typeof(string));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000046E5 File Offset: 0x000028E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("command", this.command);
			info.AddValue("response", this.response);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00004711 File Offset: 0x00002911
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00004719 File Offset: 0x00002919
		public string Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040000EB RID: 235
		private readonly string command;

		// Token: 0x040000EC RID: 236
		private readonly string response;
	}
}
