using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess
{
	// Token: 0x020001AD RID: 429
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidResponseException : TransportException
	{
		// Token: 0x06000E8B RID: 3723 RVA: 0x000352D9 File Offset: 0x000334D9
		public InvalidResponseException(string channel, string error) : base(Strings.InvalidResponseException(channel, error))
		{
			this.channel = channel;
			this.error = error;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000352FB File Offset: 0x000334FB
		public InvalidResponseException(string channel, string error, Exception innerException) : base(Strings.InvalidResponseException(channel, error), innerException)
		{
			this.channel = channel;
			this.error = error;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00035320 File Offset: 0x00033520
		protected InvalidResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.channel = (string)info.GetValue("channel", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00035375 File Offset: 0x00033575
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("channel", this.channel);
			info.AddValue("error", this.error);
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x000353A1 File Offset: 0x000335A1
		public string Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x000353A9 File Offset: 0x000335A9
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04000787 RID: 1927
		private readonly string channel;

		// Token: 0x04000788 RID: 1928
		private readonly string error;
	}
}
