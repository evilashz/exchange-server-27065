using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002CE RID: 718
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoServersAndOutofServerScopeException : LocalizedException
	{
		// Token: 0x06001970 RID: 6512 RVA: 0x0005D261 File Offset: 0x0005B461
		public NoServersAndOutofServerScopeException(string databaseid, string serverid) : base(Strings.ErrorNoServersAndOutofServerScope(databaseid, serverid))
		{
			this.databaseid = databaseid;
			this.serverid = serverid;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0005D27E File Offset: 0x0005B47E
		public NoServersAndOutofServerScopeException(string databaseid, string serverid, Exception innerException) : base(Strings.ErrorNoServersAndOutofServerScope(databaseid, serverid), innerException)
		{
			this.databaseid = databaseid;
			this.serverid = serverid;
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0005D29C File Offset: 0x0005B49C
		protected NoServersAndOutofServerScopeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseid = (string)info.GetValue("databaseid", typeof(string));
			this.serverid = (string)info.GetValue("serverid", typeof(string));
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0005D2F1 File Offset: 0x0005B4F1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseid", this.databaseid);
			info.AddValue("serverid", this.serverid);
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x0005D31D File Offset: 0x0005B51D
		public string Databaseid
		{
			get
			{
				return this.databaseid;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x0005D325 File Offset: 0x0005B525
		public string Serverid
		{
			get
			{
				return this.serverid;
			}
		}

		// Token: 0x0400099D RID: 2461
		private readonly string databaseid;

		// Token: 0x0400099E RID: 2462
		private readonly string serverid;
	}
}
