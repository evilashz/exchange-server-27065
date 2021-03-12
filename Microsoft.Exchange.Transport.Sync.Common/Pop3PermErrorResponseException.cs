using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000045 RID: 69
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3PermErrorResponseException : LocalizedException
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x00005F2E File Offset: 0x0000412E
		public Pop3PermErrorResponseException(string command, string response) : base(Strings.Pop3PermErrorResponseException(command, response))
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005F4B File Offset: 0x0000414B
		public Pop3PermErrorResponseException(string command, string response, Exception innerException) : base(Strings.Pop3PermErrorResponseException(command, response), innerException)
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00005F6C File Offset: 0x0000416C
		protected Pop3PermErrorResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.command = (string)info.GetValue("command", typeof(string));
			this.response = (string)info.GetValue("response", typeof(string));
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00005FC1 File Offset: 0x000041C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("command", this.command);
			info.AddValue("response", this.response);
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00005FED File Offset: 0x000041ED
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00005FF5 File Offset: 0x000041F5
		public string Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040000F2 RID: 242
		private readonly string command;

		// Token: 0x040000F3 RID: 243
		private readonly string response;
	}
}
