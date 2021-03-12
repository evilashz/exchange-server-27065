using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200117B RID: 4475
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToRunServerMonitoringOverrideException : LocalizedException
	{
		// Token: 0x0600B64E RID: 46670 RVA: 0x0029F8CB File Offset: 0x0029DACB
		public FailedToRunServerMonitoringOverrideException(string server, string failure) : base(Strings.FailedToRunServerMonitoringOverride(server, failure))
		{
			this.server = server;
			this.failure = failure;
		}

		// Token: 0x0600B64F RID: 46671 RVA: 0x0029F8E8 File Offset: 0x0029DAE8
		public FailedToRunServerMonitoringOverrideException(string server, string failure, Exception innerException) : base(Strings.FailedToRunServerMonitoringOverride(server, failure), innerException)
		{
			this.server = server;
			this.failure = failure;
		}

		// Token: 0x0600B650 RID: 46672 RVA: 0x0029F908 File Offset: 0x0029DB08
		protected FailedToRunServerMonitoringOverrideException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
			this.failure = (string)info.GetValue("failure", typeof(string));
		}

		// Token: 0x0600B651 RID: 46673 RVA: 0x0029F95D File Offset: 0x0029DB5D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
			info.AddValue("failure", this.failure);
		}

		// Token: 0x1700397F RID: 14719
		// (get) Token: 0x0600B652 RID: 46674 RVA: 0x0029F989 File Offset: 0x0029DB89
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17003980 RID: 14720
		// (get) Token: 0x0600B653 RID: 46675 RVA: 0x0029F991 File Offset: 0x0029DB91
		public string Failure
		{
			get
			{
				return this.failure;
			}
		}

		// Token: 0x040062E5 RID: 25317
		private readonly string server;

		// Token: 0x040062E6 RID: 25318
		private readonly string failure;
	}
}
