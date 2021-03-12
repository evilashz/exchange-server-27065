using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F2C RID: 3884
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotConnectToClusterErrorException : LocalizedException
	{
		// Token: 0x0600AAD1 RID: 43729 RVA: 0x0028E0F5 File Offset: 0x0028C2F5
		public CouldNotConnectToClusterErrorException(string machineName, string error) : base(Strings.CouldNotConnectToClusterError(machineName, error))
		{
			this.machineName = machineName;
			this.error = error;
		}

		// Token: 0x0600AAD2 RID: 43730 RVA: 0x0028E112 File Offset: 0x0028C312
		public CouldNotConnectToClusterErrorException(string machineName, string error, Exception innerException) : base(Strings.CouldNotConnectToClusterError(machineName, error), innerException)
		{
			this.machineName = machineName;
			this.error = error;
		}

		// Token: 0x0600AAD3 RID: 43731 RVA: 0x0028E130 File Offset: 0x0028C330
		protected CouldNotConnectToClusterErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.machineName = (string)info.GetValue("machineName", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600AAD4 RID: 43732 RVA: 0x0028E185 File Offset: 0x0028C385
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("machineName", this.machineName);
			info.AddValue("error", this.error);
		}

		// Token: 0x1700373E RID: 14142
		// (get) Token: 0x0600AAD5 RID: 43733 RVA: 0x0028E1B1 File Offset: 0x0028C3B1
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x1700373F RID: 14143
		// (get) Token: 0x0600AAD6 RID: 43734 RVA: 0x0028E1B9 File Offset: 0x0028C3B9
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040060A4 RID: 24740
		private readonly string machineName;

		// Token: 0x040060A5 RID: 24741
		private readonly string error;
	}
}
