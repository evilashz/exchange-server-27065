using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000495 RID: 1173
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemoteRegistryTimedOutException : TransientException
	{
		// Token: 0x06002C98 RID: 11416 RVA: 0x000BFA42 File Offset: 0x000BDC42
		public RemoteRegistryTimedOutException(string machineName, int secondsTimeout) : base(ReplayStrings.RemoteRegistryTimedOutException(machineName, secondsTimeout))
		{
			this.machineName = machineName;
			this.secondsTimeout = secondsTimeout;
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000BFA5F File Offset: 0x000BDC5F
		public RemoteRegistryTimedOutException(string machineName, int secondsTimeout, Exception innerException) : base(ReplayStrings.RemoteRegistryTimedOutException(machineName, secondsTimeout), innerException)
		{
			this.machineName = machineName;
			this.secondsTimeout = secondsTimeout;
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000BFA80 File Offset: 0x000BDC80
		protected RemoteRegistryTimedOutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.machineName = (string)info.GetValue("machineName", typeof(string));
			this.secondsTimeout = (int)info.GetValue("secondsTimeout", typeof(int));
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000BFAD5 File Offset: 0x000BDCD5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("machineName", this.machineName);
			info.AddValue("secondsTimeout", this.secondsTimeout);
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x000BFB01 File Offset: 0x000BDD01
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000BFB09 File Offset: 0x000BDD09
		public int SecondsTimeout
		{
			get
			{
				return this.secondsTimeout;
			}
		}

		// Token: 0x040014EB RID: 5355
		private readonly string machineName;

		// Token: 0x040014EC RID: 5356
		private readonly int secondsTimeout;
	}
}
