using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004E7 RID: 1255
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MonitoringADConfigStaleException : MonitoringADConfigException
	{
		// Token: 0x06002E6F RID: 11887 RVA: 0x000C3615 File Offset: 0x000C1815
		public MonitoringADConfigStaleException(string age, string maxTTL, string lastError) : base(ReplayStrings.MonitoringADConfigStaleException(age, maxTTL, lastError))
		{
			this.age = age;
			this.maxTTL = maxTTL;
			this.lastError = lastError;
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000C363F File Offset: 0x000C183F
		public MonitoringADConfigStaleException(string age, string maxTTL, string lastError, Exception innerException) : base(ReplayStrings.MonitoringADConfigStaleException(age, maxTTL, lastError), innerException)
		{
			this.age = age;
			this.maxTTL = maxTTL;
			this.lastError = lastError;
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000C366C File Offset: 0x000C186C
		protected MonitoringADConfigStaleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.age = (string)info.GetValue("age", typeof(string));
			this.maxTTL = (string)info.GetValue("maxTTL", typeof(string));
			this.lastError = (string)info.GetValue("lastError", typeof(string));
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x000C36E1 File Offset: 0x000C18E1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("age", this.age);
			info.AddValue("maxTTL", this.maxTTL);
			info.AddValue("lastError", this.lastError);
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x000C371E File Offset: 0x000C191E
		public string Age
		{
			get
			{
				return this.age;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06002E74 RID: 11892 RVA: 0x000C3726 File Offset: 0x000C1926
		public string MaxTTL
		{
			get
			{
				return this.maxTTL;
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06002E75 RID: 11893 RVA: 0x000C372E File Offset: 0x000C192E
		public string LastError
		{
			get
			{
				return this.lastError;
			}
		}

		// Token: 0x0400157A RID: 5498
		private readonly string age;

		// Token: 0x0400157B RID: 5499
		private readonly string maxTTL;

		// Token: 0x0400157C RID: 5500
		private readonly string lastError;
	}
}
