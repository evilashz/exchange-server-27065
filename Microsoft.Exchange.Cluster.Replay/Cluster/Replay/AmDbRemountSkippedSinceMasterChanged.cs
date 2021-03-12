using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000473 RID: 1139
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbRemountSkippedSinceMasterChanged : AmDbActionException
	{
		// Token: 0x06002BD9 RID: 11225 RVA: 0x000BE279 File Offset: 0x000BC479
		public AmDbRemountSkippedSinceMasterChanged(string dbName, string currentActive, string prevActive) : base(ReplayStrings.AmDbRemountSkippedSinceMasterChanged(dbName, currentActive, prevActive))
		{
			this.dbName = dbName;
			this.currentActive = currentActive;
			this.prevActive = prevActive;
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000BE2A3 File Offset: 0x000BC4A3
		public AmDbRemountSkippedSinceMasterChanged(string dbName, string currentActive, string prevActive, Exception innerException) : base(ReplayStrings.AmDbRemountSkippedSinceMasterChanged(dbName, currentActive, prevActive), innerException)
		{
			this.dbName = dbName;
			this.currentActive = currentActive;
			this.prevActive = prevActive;
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000BE2D0 File Offset: 0x000BC4D0
		protected AmDbRemountSkippedSinceMasterChanged(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.currentActive = (string)info.GetValue("currentActive", typeof(string));
			this.prevActive = (string)info.GetValue("prevActive", typeof(string));
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000BE345 File Offset: 0x000BC545
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("currentActive", this.currentActive);
			info.AddValue("prevActive", this.prevActive);
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x000BE382 File Offset: 0x000BC582
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x000BE38A File Offset: 0x000BC58A
		public string CurrentActive
		{
			get
			{
				return this.currentActive;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x000BE392 File Offset: 0x000BC592
		public string PrevActive
		{
			get
			{
				return this.prevActive;
			}
		}

		// Token: 0x040014B4 RID: 5300
		private readonly string dbName;

		// Token: 0x040014B5 RID: 5301
		private readonly string currentActive;

		// Token: 0x040014B6 RID: 5302
		private readonly string prevActive;
	}
}
