using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200003D RID: 61
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3BrokenResponseException : LocalizedException
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x00005C76 File Offset: 0x00003E76
		public Pop3BrokenResponseException(string command, string response) : base(Strings.Pop3BrokenResponseException(command, response))
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00005C93 File Offset: 0x00003E93
		public Pop3BrokenResponseException(string command, string response, Exception innerException) : base(Strings.Pop3BrokenResponseException(command, response), innerException)
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005CB4 File Offset: 0x00003EB4
		protected Pop3BrokenResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.command = (string)info.GetValue("command", typeof(string));
			this.response = (string)info.GetValue("response", typeof(string));
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005D09 File Offset: 0x00003F09
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("command", this.command);
			info.AddValue("response", this.response);
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00005D35 File Offset: 0x00003F35
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00005D3D File Offset: 0x00003F3D
		public string Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040000EE RID: 238
		private readonly string command;

		// Token: 0x040000EF RID: 239
		private readonly string response;
	}
}
