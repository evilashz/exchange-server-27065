using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002A7 RID: 679
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatacenterMissingHostsPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060022FE RID: 8958 RVA: 0x0004DBAE File Offset: 0x0004BDAE
		public DatacenterMissingHostsPermanentException(string datacenterName) : base(MrsStrings.DatacenterMissingHosts(datacenterName))
		{
			this.datacenterName = datacenterName;
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x0004DBC3 File Offset: 0x0004BDC3
		public DatacenterMissingHostsPermanentException(string datacenterName, Exception innerException) : base(MrsStrings.DatacenterMissingHosts(datacenterName), innerException)
		{
			this.datacenterName = datacenterName;
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x0004DBD9 File Offset: 0x0004BDD9
		protected DatacenterMissingHostsPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.datacenterName = (string)info.GetValue("datacenterName", typeof(string));
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x0004DC03 File Offset: 0x0004BE03
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("datacenterName", this.datacenterName);
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x0004DC1E File Offset: 0x0004BE1E
		public string DatacenterName
		{
			get
			{
				return this.datacenterName;
			}
		}

		// Token: 0x04000FAB RID: 4011
		private readonly string datacenterName;
	}
}
