using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000041 RID: 65
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3ErrorResponseException : LocalizedException
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00005DD2 File Offset: 0x00003FD2
		public Pop3ErrorResponseException(string command, string response) : base(Strings.Pop3ErrorResponseException(command, response))
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005DEF File Offset: 0x00003FEF
		public Pop3ErrorResponseException(string command, string response, Exception innerException) : base(Strings.Pop3ErrorResponseException(command, response), innerException)
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005E10 File Offset: 0x00004010
		protected Pop3ErrorResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.command = (string)info.GetValue("command", typeof(string));
			this.response = (string)info.GetValue("response", typeof(string));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00005E65 File Offset: 0x00004065
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("command", this.command);
			info.AddValue("response", this.response);
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00005E91 File Offset: 0x00004091
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00005E99 File Offset: 0x00004099
		public string Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040000F0 RID: 240
		private readonly string command;

		// Token: 0x040000F1 RID: 241
		private readonly string response;
	}
}
