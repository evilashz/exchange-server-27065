using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F37 RID: 3895
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRunMonitoringTaskRemotelyException : LocalizedException
	{
		// Token: 0x0600AB0D RID: 43789 RVA: 0x0028E7D1 File Offset: 0x0028C9D1
		public CannotRunMonitoringTaskRemotelyException(string remoteServerName) : base(Strings.CannotRunMonitoringTaskRemotelyException(remoteServerName))
		{
			this.remoteServerName = remoteServerName;
		}

		// Token: 0x0600AB0E RID: 43790 RVA: 0x0028E7E6 File Offset: 0x0028C9E6
		public CannotRunMonitoringTaskRemotelyException(string remoteServerName, Exception innerException) : base(Strings.CannotRunMonitoringTaskRemotelyException(remoteServerName), innerException)
		{
			this.remoteServerName = remoteServerName;
		}

		// Token: 0x0600AB0F RID: 43791 RVA: 0x0028E7FC File Offset: 0x0028C9FC
		protected CannotRunMonitoringTaskRemotelyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.remoteServerName = (string)info.GetValue("remoteServerName", typeof(string));
		}

		// Token: 0x0600AB10 RID: 43792 RVA: 0x0028E826 File Offset: 0x0028CA26
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("remoteServerName", this.remoteServerName);
		}

		// Token: 0x1700374E RID: 14158
		// (get) Token: 0x0600AB11 RID: 43793 RVA: 0x0028E841 File Offset: 0x0028CA41
		public string RemoteServerName
		{
			get
			{
				return this.remoteServerName;
			}
		}

		// Token: 0x040060B4 RID: 24756
		private readonly string remoteServerName;
	}
}
