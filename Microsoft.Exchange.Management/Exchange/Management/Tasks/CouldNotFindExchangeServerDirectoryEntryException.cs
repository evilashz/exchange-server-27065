using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E11 RID: 3601
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotFindExchangeServerDirectoryEntryException : LocalizedException
	{
		// Token: 0x0600A556 RID: 42326 RVA: 0x00285DC1 File Offset: 0x00283FC1
		public CouldNotFindExchangeServerDirectoryEntryException(string server) : base(Strings.CouldNotFindExchangeServerDirectoryEntryException(server))
		{
			this.server = server;
		}

		// Token: 0x0600A557 RID: 42327 RVA: 0x00285DD6 File Offset: 0x00283FD6
		public CouldNotFindExchangeServerDirectoryEntryException(string server, Exception innerException) : base(Strings.CouldNotFindExchangeServerDirectoryEntryException(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x0600A558 RID: 42328 RVA: 0x00285DEC File Offset: 0x00283FEC
		protected CouldNotFindExchangeServerDirectoryEntryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x0600A559 RID: 42329 RVA: 0x00285E16 File Offset: 0x00284016
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x1700362F RID: 13871
		// (get) Token: 0x0600A55A RID: 42330 RVA: 0x00285E31 File Offset: 0x00284031
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04005F95 RID: 24469
		private readonly string server;
	}
}
