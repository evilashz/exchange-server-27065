using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200004D RID: 77
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3ErrorResponseException : LocalizedException
	{
		// Token: 0x06000178 RID: 376 RVA: 0x000044F7 File Offset: 0x000026F7
		public Pop3ErrorResponseException(string command, string response) : base(CXStrings.Pop3ErrorResponseMsg(command, response))
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004514 File Offset: 0x00002714
		public Pop3ErrorResponseException(string command, string response, Exception innerException) : base(CXStrings.Pop3ErrorResponseMsg(command, response), innerException)
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004534 File Offset: 0x00002734
		protected Pop3ErrorResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.command = (string)info.GetValue("command", typeof(string));
			this.response = (string)info.GetValue("response", typeof(string));
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004589 File Offset: 0x00002789
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("command", this.command);
			info.AddValue("response", this.response);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000045B5 File Offset: 0x000027B5
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000045BD File Offset: 0x000027BD
		public string Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040000E9 RID: 233
		private readonly string command;

		// Token: 0x040000EA RID: 234
		private readonly string response;
	}
}
