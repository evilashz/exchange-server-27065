using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x0200059B RID: 1435
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ProcessCrashingException : LocalizedException
	{
		// Token: 0x060026A3 RID: 9891 RVA: 0x000DDC51 File Offset: 0x000DBE51
		public ProcessCrashingException(string serviceName, string server) : base(Strings.ProcessCrashing(serviceName, server))
		{
			this.serviceName = serviceName;
			this.server = server;
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x000DDC6E File Offset: 0x000DBE6E
		public ProcessCrashingException(string serviceName, string server, Exception innerException) : base(Strings.ProcessCrashing(serviceName, server), innerException)
		{
			this.serviceName = serviceName;
			this.server = server;
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x000DDC8C File Offset: 0x000DBE8C
		protected ProcessCrashingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceName = (string)info.GetValue("serviceName", typeof(string));
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000DDCE1 File Offset: 0x000DBEE1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceName", this.serviceName);
			info.AddValue("server", this.server);
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060026A7 RID: 9895 RVA: 0x000DDD0D File Offset: 0x000DBF0D
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060026A8 RID: 9896 RVA: 0x000DDD15 File Offset: 0x000DBF15
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04001C73 RID: 7283
		private readonly string serviceName;

		// Token: 0x04001C74 RID: 7284
		private readonly string server;
	}
}
