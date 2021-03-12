using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200108C RID: 4236
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskServerIPAddressSameAsStaticIPAddressException : LocalizedException
	{
		// Token: 0x0600B1AD RID: 45485 RVA: 0x00298891 File Offset: 0x00296A91
		public DagTaskServerIPAddressSameAsStaticIPAddressException(string serverName, string conflict, string dag) : base(Strings.DagTaskServerIPAddressSameAsStaticIPAddress(serverName, conflict, dag))
		{
			this.serverName = serverName;
			this.conflict = conflict;
			this.dag = dag;
		}

		// Token: 0x0600B1AE RID: 45486 RVA: 0x002988B6 File Offset: 0x00296AB6
		public DagTaskServerIPAddressSameAsStaticIPAddressException(string serverName, string conflict, string dag, Exception innerException) : base(Strings.DagTaskServerIPAddressSameAsStaticIPAddress(serverName, conflict, dag), innerException)
		{
			this.serverName = serverName;
			this.conflict = conflict;
			this.dag = dag;
		}

		// Token: 0x0600B1AF RID: 45487 RVA: 0x002988E0 File Offset: 0x00296AE0
		protected DagTaskServerIPAddressSameAsStaticIPAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.conflict = (string)info.GetValue("conflict", typeof(string));
			this.dag = (string)info.GetValue("dag", typeof(string));
		}

		// Token: 0x0600B1B0 RID: 45488 RVA: 0x00298955 File Offset: 0x00296B55
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("conflict", this.conflict);
			info.AddValue("dag", this.dag);
		}

		// Token: 0x1700389A RID: 14490
		// (get) Token: 0x0600B1B1 RID: 45489 RVA: 0x00298992 File Offset: 0x00296B92
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x1700389B RID: 14491
		// (get) Token: 0x0600B1B2 RID: 45490 RVA: 0x0029899A File Offset: 0x00296B9A
		public string Conflict
		{
			get
			{
				return this.conflict;
			}
		}

		// Token: 0x1700389C RID: 14492
		// (get) Token: 0x0600B1B3 RID: 45491 RVA: 0x002989A2 File Offset: 0x00296BA2
		public string Dag
		{
			get
			{
				return this.dag;
			}
		}

		// Token: 0x04006200 RID: 25088
		private readonly string serverName;

		// Token: 0x04006201 RID: 25089
		private readonly string conflict;

		// Token: 0x04006202 RID: 25090
		private readonly string dag;
	}
}
