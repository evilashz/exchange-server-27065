using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001069 RID: 4201
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NewDagServerIsNotMailboxServerException : LocalizedException
	{
		// Token: 0x0600B0E6 RID: 45286 RVA: 0x0029706E File Offset: 0x0029526E
		public NewDagServerIsNotMailboxServerException(string serverName) : base(Strings.NewDagServerIsNotMailboxServerException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B0E7 RID: 45287 RVA: 0x00297083 File Offset: 0x00295283
		public NewDagServerIsNotMailboxServerException(string serverName, Exception innerException) : base(Strings.NewDagServerIsNotMailboxServerException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B0E8 RID: 45288 RVA: 0x00297099 File Offset: 0x00295299
		protected NewDagServerIsNotMailboxServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600B0E9 RID: 45289 RVA: 0x002970C3 File Offset: 0x002952C3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x1700385F RID: 14431
		// (get) Token: 0x0600B0EA RID: 45290 RVA: 0x002970DE File Offset: 0x002952DE
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040061C5 RID: 25029
		private readonly string serverName;
	}
}
