using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001034 RID: 4148
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotDelegateEdgeServerAdminException : LocalizedException
	{
		// Token: 0x0600AFBB RID: 44987 RVA: 0x00294C89 File Offset: 0x00292E89
		public CannotDelegateEdgeServerAdminException(string server) : base(Strings.ExceptionCannotDelegateEdgeServerAdmin(server))
		{
			this.server = server;
		}

		// Token: 0x0600AFBC RID: 44988 RVA: 0x00294C9E File Offset: 0x00292E9E
		public CannotDelegateEdgeServerAdminException(string server, Exception innerException) : base(Strings.ExceptionCannotDelegateEdgeServerAdmin(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x0600AFBD RID: 44989 RVA: 0x00294CB4 File Offset: 0x00292EB4
		protected CannotDelegateEdgeServerAdminException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x0600AFBE RID: 44990 RVA: 0x00294CDE File Offset: 0x00292EDE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x17003808 RID: 14344
		// (get) Token: 0x0600AFBF RID: 44991 RVA: 0x00294CF9 File Offset: 0x00292EF9
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x0400616E RID: 24942
		private readonly string server;
	}
}
