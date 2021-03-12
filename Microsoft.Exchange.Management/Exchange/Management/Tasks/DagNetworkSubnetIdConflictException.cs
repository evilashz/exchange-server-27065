using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001073 RID: 4211
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagNetworkSubnetIdConflictException : LocalizedException
	{
		// Token: 0x0600B121 RID: 45345 RVA: 0x00297801 File Offset: 0x00295A01
		public DagNetworkSubnetIdConflictException(string subnetId1, string subnetId2) : base(Strings.DagNetworkSubnetIdConflictError(subnetId1, subnetId2))
		{
			this.subnetId1 = subnetId1;
			this.subnetId2 = subnetId2;
		}

		// Token: 0x0600B122 RID: 45346 RVA: 0x0029781E File Offset: 0x00295A1E
		public DagNetworkSubnetIdConflictException(string subnetId1, string subnetId2, Exception innerException) : base(Strings.DagNetworkSubnetIdConflictError(subnetId1, subnetId2), innerException)
		{
			this.subnetId1 = subnetId1;
			this.subnetId2 = subnetId2;
		}

		// Token: 0x0600B123 RID: 45347 RVA: 0x0029783C File Offset: 0x00295A3C
		protected DagNetworkSubnetIdConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.subnetId1 = (string)info.GetValue("subnetId1", typeof(string));
			this.subnetId2 = (string)info.GetValue("subnetId2", typeof(string));
		}

		// Token: 0x0600B124 RID: 45348 RVA: 0x00297891 File Offset: 0x00295A91
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("subnetId1", this.subnetId1);
			info.AddValue("subnetId2", this.subnetId2);
		}

		// Token: 0x17003872 RID: 14450
		// (get) Token: 0x0600B125 RID: 45349 RVA: 0x002978BD File Offset: 0x00295ABD
		public string SubnetId1
		{
			get
			{
				return this.subnetId1;
			}
		}

		// Token: 0x17003873 RID: 14451
		// (get) Token: 0x0600B126 RID: 45350 RVA: 0x002978C5 File Offset: 0x00295AC5
		public string SubnetId2
		{
			get
			{
				return this.subnetId2;
			}
		}

		// Token: 0x040061D8 RID: 25048
		private readonly string subnetId1;

		// Token: 0x040061D9 RID: 25049
		private readonly string subnetId2;
	}
}
