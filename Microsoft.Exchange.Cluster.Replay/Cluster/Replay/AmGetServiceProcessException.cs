using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200047B RID: 1147
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmGetServiceProcessException : AmCommonException
	{
		// Token: 0x06002C04 RID: 11268 RVA: 0x000BE77E File Offset: 0x000BC97E
		public AmGetServiceProcessException(string serviceName, int state, int pid) : base(ReplayStrings.AmGetServiceProcessFailed(serviceName, state, pid))
		{
			this.serviceName = serviceName;
			this.state = state;
			this.pid = pid;
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x000BE7A8 File Offset: 0x000BC9A8
		public AmGetServiceProcessException(string serviceName, int state, int pid, Exception innerException) : base(ReplayStrings.AmGetServiceProcessFailed(serviceName, state, pid), innerException)
		{
			this.serviceName = serviceName;
			this.state = state;
			this.pid = pid;
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000BE7D4 File Offset: 0x000BC9D4
		protected AmGetServiceProcessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceName = (string)info.GetValue("serviceName", typeof(string));
			this.state = (int)info.GetValue("state", typeof(int));
			this.pid = (int)info.GetValue("pid", typeof(int));
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x000BE849 File Offset: 0x000BCA49
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceName", this.serviceName);
			info.AddValue("state", this.state);
			info.AddValue("pid", this.pid);
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06002C08 RID: 11272 RVA: 0x000BE886 File Offset: 0x000BCA86
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000BE88E File Offset: 0x000BCA8E
		public int State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x000BE896 File Offset: 0x000BCA96
		public int Pid
		{
			get
			{
				return this.pid;
			}
		}

		// Token: 0x040014BF RID: 5311
		private readonly string serviceName;

		// Token: 0x040014C0 RID: 5312
		private readonly int state;

		// Token: 0x040014C1 RID: 5313
		private readonly int pid;
	}
}
