using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001072 RID: 4210
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagNetworkDistinctSubnetListException : LocalizedException
	{
		// Token: 0x0600B11B RID: 45339 RVA: 0x00297735 File Offset: 0x00295935
		public DagNetworkDistinctSubnetListException(string subnetId1, string subnetId2) : base(Strings.DagNetworkDistinctSubnetListError(subnetId1, subnetId2))
		{
			this.subnetId1 = subnetId1;
			this.subnetId2 = subnetId2;
		}

		// Token: 0x0600B11C RID: 45340 RVA: 0x00297752 File Offset: 0x00295952
		public DagNetworkDistinctSubnetListException(string subnetId1, string subnetId2, Exception innerException) : base(Strings.DagNetworkDistinctSubnetListError(subnetId1, subnetId2), innerException)
		{
			this.subnetId1 = subnetId1;
			this.subnetId2 = subnetId2;
		}

		// Token: 0x0600B11D RID: 45341 RVA: 0x00297770 File Offset: 0x00295970
		protected DagNetworkDistinctSubnetListException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.subnetId1 = (string)info.GetValue("subnetId1", typeof(string));
			this.subnetId2 = (string)info.GetValue("subnetId2", typeof(string));
		}

		// Token: 0x0600B11E RID: 45342 RVA: 0x002977C5 File Offset: 0x002959C5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("subnetId1", this.subnetId1);
			info.AddValue("subnetId2", this.subnetId2);
		}

		// Token: 0x17003870 RID: 14448
		// (get) Token: 0x0600B11F RID: 45343 RVA: 0x002977F1 File Offset: 0x002959F1
		public string SubnetId1
		{
			get
			{
				return this.subnetId1;
			}
		}

		// Token: 0x17003871 RID: 14449
		// (get) Token: 0x0600B120 RID: 45344 RVA: 0x002977F9 File Offset: 0x002959F9
		public string SubnetId2
		{
			get
			{
				return this.subnetId2;
			}
		}

		// Token: 0x040061D6 RID: 25046
		private readonly string subnetId1;

		// Token: 0x040061D7 RID: 25047
		private readonly string subnetId2;
	}
}
