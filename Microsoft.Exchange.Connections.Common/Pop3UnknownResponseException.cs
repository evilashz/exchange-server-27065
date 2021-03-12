using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000055 RID: 85
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3UnknownResponseException : LocalizedException
	{
		// Token: 0x0600019C RID: 412 RVA: 0x000047AE File Offset: 0x000029AE
		public Pop3UnknownResponseException(string command, string response) : base(CXStrings.Pop3UnknownResponseMsg(command, response))
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000047CB File Offset: 0x000029CB
		public Pop3UnknownResponseException(string command, string response, Exception innerException) : base(CXStrings.Pop3UnknownResponseMsg(command, response), innerException)
		{
			this.command = command;
			this.response = response;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000047EC File Offset: 0x000029EC
		protected Pop3UnknownResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.command = (string)info.GetValue("command", typeof(string));
			this.response = (string)info.GetValue("response", typeof(string));
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00004841 File Offset: 0x00002A41
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("command", this.command);
			info.AddValue("response", this.response);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000486D File Offset: 0x00002A6D
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00004875 File Offset: 0x00002A75
		public string Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040000ED RID: 237
		private readonly string command;

		// Token: 0x040000EE RID: 238
		private readonly string response;
	}
}
