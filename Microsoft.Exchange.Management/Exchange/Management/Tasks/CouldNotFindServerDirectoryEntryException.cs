using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DF3 RID: 3571
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotFindServerDirectoryEntryException : LocalizedException
	{
		// Token: 0x0600A4B8 RID: 42168 RVA: 0x00284CAC File Offset: 0x00282EAC
		public CouldNotFindServerDirectoryEntryException(string server) : base(Strings.CouldNotFindServerDirectoryEntryException(server))
		{
			this.server = server;
		}

		// Token: 0x0600A4B9 RID: 42169 RVA: 0x00284CC1 File Offset: 0x00282EC1
		public CouldNotFindServerDirectoryEntryException(string server, Exception innerException) : base(Strings.CouldNotFindServerDirectoryEntryException(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x0600A4BA RID: 42170 RVA: 0x00284CD7 File Offset: 0x00282ED7
		protected CouldNotFindServerDirectoryEntryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x0600A4BB RID: 42171 RVA: 0x00284D01 File Offset: 0x00282F01
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x17003609 RID: 13833
		// (get) Token: 0x0600A4BC RID: 42172 RVA: 0x00284D1C File Offset: 0x00282F1C
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04005F6F RID: 24431
		private readonly string server;
	}
}
