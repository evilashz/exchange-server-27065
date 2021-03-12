using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000216 RID: 534
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidAudioStreamException : LocalizedException
	{
		// Token: 0x06001127 RID: 4391 RVA: 0x00039B46 File Offset: 0x00037D46
		public InvalidAudioStreamException(string msg) : base(Strings.InvalidAudioStreamException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00039B5B File Offset: 0x00037D5B
		public InvalidAudioStreamException(string msg, Exception innerException) : base(Strings.InvalidAudioStreamException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00039B71 File Offset: 0x00037D71
		protected InvalidAudioStreamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00039B9B File Offset: 0x00037D9B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x00039BB6 File Offset: 0x00037DB6
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x0400088D RID: 2189
		private readonly string msg;
	}
}
