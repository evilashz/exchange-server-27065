using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000049 RID: 73
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3UnknownResponseException : LocalizedException
	{
		// Token: 0x060001FB RID: 507 RVA: 0x0000608A File Offset: 0x0000428A
		public Pop3UnknownResponseException(string command, string response) : base(Strings.Pop3UnknownResponseException(command, response))
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000060A7 File Offset: 0x000042A7
		public Pop3UnknownResponseException(string command, string response, Exception innerException) : base(Strings.Pop3UnknownResponseException(command, response), innerException)
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000060C8 File Offset: 0x000042C8
		protected Pop3UnknownResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.command = (string)info.GetValue("command", typeof(string));
			this.response = (string)info.GetValue("response", typeof(string));
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000611D File Offset: 0x0000431D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("command", this.command);
			info.AddValue("response", this.response);
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00006149 File Offset: 0x00004349
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00006151 File Offset: 0x00004351
		public string Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040000F4 RID: 244
		private readonly string command;

		// Token: 0x040000F5 RID: 245
		private readonly string response;
	}
}
