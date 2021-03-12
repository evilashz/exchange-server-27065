using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001095 RID: 4245
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToGetServiceStatusForNodeException : LocalizedException
	{
		// Token: 0x0600B1E6 RID: 45542 RVA: 0x0029909D File Offset: 0x0029729D
		public FailedToGetServiceStatusForNodeException(string server, string error) : base(Strings.FailedToGetServiceStatusForNodeException(server, error))
		{
			this.server = server;
			this.error = error;
		}

		// Token: 0x0600B1E7 RID: 45543 RVA: 0x002990BA File Offset: 0x002972BA
		public FailedToGetServiceStatusForNodeException(string server, string error, Exception innerException) : base(Strings.FailedToGetServiceStatusForNodeException(server, error), innerException)
		{
			this.server = server;
			this.error = error;
		}

		// Token: 0x0600B1E8 RID: 45544 RVA: 0x002990D8 File Offset: 0x002972D8
		protected FailedToGetServiceStatusForNodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600B1E9 RID: 45545 RVA: 0x0029912D File Offset: 0x0029732D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
			info.AddValue("error", this.error);
		}

		// Token: 0x170038AF RID: 14511
		// (get) Token: 0x0600B1EA RID: 45546 RVA: 0x00299159 File Offset: 0x00297359
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x170038B0 RID: 14512
		// (get) Token: 0x0600B1EB RID: 45547 RVA: 0x00299161 File Offset: 0x00297361
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04006215 RID: 25109
		private readonly string server;

		// Token: 0x04006216 RID: 25110
		private readonly string error;
	}
}
