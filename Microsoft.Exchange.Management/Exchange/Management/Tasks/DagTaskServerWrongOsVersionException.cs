using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001090 RID: 4240
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskServerWrongOsVersionException : LocalizedException
	{
		// Token: 0x0600B1C5 RID: 45509 RVA: 0x00298BBD File Offset: 0x00296DBD
		public DagTaskServerWrongOsVersionException(string serverName) : base(Strings.DagTaskServerWrongOsVersionException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B1C6 RID: 45510 RVA: 0x00298BD2 File Offset: 0x00296DD2
		public DagTaskServerWrongOsVersionException(string serverName, Exception innerException) : base(Strings.DagTaskServerWrongOsVersionException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B1C7 RID: 45511 RVA: 0x00298BE8 File Offset: 0x00296DE8
		protected DagTaskServerWrongOsVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600B1C8 RID: 45512 RVA: 0x00298C12 File Offset: 0x00296E12
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x170038A2 RID: 14498
		// (get) Token: 0x0600B1C9 RID: 45513 RVA: 0x00298C2D File Offset: 0x00296E2D
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04006208 RID: 25096
		private readonly string serverName;
	}
}
