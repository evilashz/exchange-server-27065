using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001032 RID: 4146
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveNonProvisionedServerException : LocalizedException
	{
		// Token: 0x0600AFB1 RID: 44977 RVA: 0x00294B99 File Offset: 0x00292D99
		public CannotRemoveNonProvisionedServerException(string server) : base(Strings.CannotRemoveNonProvisionedServerException(server))
		{
			this.server = server;
		}

		// Token: 0x0600AFB2 RID: 44978 RVA: 0x00294BAE File Offset: 0x00292DAE
		public CannotRemoveNonProvisionedServerException(string server, Exception innerException) : base(Strings.CannotRemoveNonProvisionedServerException(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x0600AFB3 RID: 44979 RVA: 0x00294BC4 File Offset: 0x00292DC4
		protected CannotRemoveNonProvisionedServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x0600AFB4 RID: 44980 RVA: 0x00294BEE File Offset: 0x00292DEE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x17003806 RID: 14342
		// (get) Token: 0x0600AFB5 RID: 44981 RVA: 0x00294C09 File Offset: 0x00292E09
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x0400616C RID: 24940
		private readonly string server;
	}
}
