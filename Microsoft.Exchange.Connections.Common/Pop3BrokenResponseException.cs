using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200004A RID: 74
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3BrokenResponseException : LocalizedException
	{
		// Token: 0x0600016A RID: 362 RVA: 0x000043CC File Offset: 0x000025CC
		public Pop3BrokenResponseException(string command, string response) : base(CXStrings.Pop3BrokenResponseMsg(command, response))
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000043E9 File Offset: 0x000025E9
		public Pop3BrokenResponseException(string command, string response, Exception innerException) : base(CXStrings.Pop3BrokenResponseMsg(command, response), innerException)
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004408 File Offset: 0x00002608
		protected Pop3BrokenResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.command = (string)info.GetValue("command", typeof(string));
			this.response = (string)info.GetValue("response", typeof(string));
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000445D File Offset: 0x0000265D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("command", this.command);
			info.AddValue("response", this.response);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00004489 File Offset: 0x00002689
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00004491 File Offset: 0x00002691
		public string Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040000E7 RID: 231
		private readonly string command;

		// Token: 0x040000E8 RID: 232
		private readonly string response;
	}
}
