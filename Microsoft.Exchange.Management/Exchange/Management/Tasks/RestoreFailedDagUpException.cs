using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001046 RID: 4166
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RestoreFailedDagUpException : LocalizedException
	{
		// Token: 0x0600B01C RID: 45084 RVA: 0x00295745 File Offset: 0x00293945
		public RestoreFailedDagUpException(string serverName) : base(Strings.RestoreFailedDagUp(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B01D RID: 45085 RVA: 0x0029575A File Offset: 0x0029395A
		public RestoreFailedDagUpException(string serverName, Exception innerException) : base(Strings.RestoreFailedDagUp(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B01E RID: 45086 RVA: 0x00295770 File Offset: 0x00293970
		protected RestoreFailedDagUpException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600B01F RID: 45087 RVA: 0x0029579A File Offset: 0x0029399A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003821 RID: 14369
		// (get) Token: 0x0600B020 RID: 45088 RVA: 0x002957B5 File Offset: 0x002939B5
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04006187 RID: 24967
		private readonly string serverName;
	}
}
