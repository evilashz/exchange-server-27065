using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess
{
	// Token: 0x020001AE RID: 430
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidRequestException : TransportException
	{
		// Token: 0x06000E91 RID: 3729 RVA: 0x000353B1 File Offset: 0x000335B1
		public InvalidRequestException(string server) : base(Strings.InvalidRequestException(server))
		{
			this.server = server;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000353CB File Offset: 0x000335CB
		public InvalidRequestException(string server, Exception innerException) : base(Strings.InvalidRequestException(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x000353E6 File Offset: 0x000335E6
		protected InvalidRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00035410 File Offset: 0x00033610
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0003542B File Offset: 0x0003362B
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04000789 RID: 1929
		private readonly string server;
	}
}
