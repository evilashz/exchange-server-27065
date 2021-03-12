using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001056 RID: 4182
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagFswSharePointsToWrongDirectoryException : LocalizedException
	{
		// Token: 0x0600B07B RID: 45179 RVA: 0x00296392 File Offset: 0x00294592
		public DagFswSharePointsToWrongDirectoryException(string share, string server, string currentdirectory, string witnessdirectory) : base(Strings.DagFswSharePointsToWrongDirectoryException(share, server, currentdirectory, witnessdirectory))
		{
			this.share = share;
			this.server = server;
			this.currentdirectory = currentdirectory;
			this.witnessdirectory = witnessdirectory;
		}

		// Token: 0x0600B07C RID: 45180 RVA: 0x002963C1 File Offset: 0x002945C1
		public DagFswSharePointsToWrongDirectoryException(string share, string server, string currentdirectory, string witnessdirectory, Exception innerException) : base(Strings.DagFswSharePointsToWrongDirectoryException(share, server, currentdirectory, witnessdirectory), innerException)
		{
			this.share = share;
			this.server = server;
			this.currentdirectory = currentdirectory;
			this.witnessdirectory = witnessdirectory;
		}

		// Token: 0x0600B07D RID: 45181 RVA: 0x002963F4 File Offset: 0x002945F4
		protected DagFswSharePointsToWrongDirectoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.share = (string)info.GetValue("share", typeof(string));
			this.server = (string)info.GetValue("server", typeof(string));
			this.currentdirectory = (string)info.GetValue("currentdirectory", typeof(string));
			this.witnessdirectory = (string)info.GetValue("witnessdirectory", typeof(string));
		}

		// Token: 0x0600B07E RID: 45182 RVA: 0x0029648C File Offset: 0x0029468C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("share", this.share);
			info.AddValue("server", this.server);
			info.AddValue("currentdirectory", this.currentdirectory);
			info.AddValue("witnessdirectory", this.witnessdirectory);
		}

		// Token: 0x17003840 RID: 14400
		// (get) Token: 0x0600B07F RID: 45183 RVA: 0x002964E5 File Offset: 0x002946E5
		public string Share
		{
			get
			{
				return this.share;
			}
		}

		// Token: 0x17003841 RID: 14401
		// (get) Token: 0x0600B080 RID: 45184 RVA: 0x002964ED File Offset: 0x002946ED
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17003842 RID: 14402
		// (get) Token: 0x0600B081 RID: 45185 RVA: 0x002964F5 File Offset: 0x002946F5
		public string Currentdirectory
		{
			get
			{
				return this.currentdirectory;
			}
		}

		// Token: 0x17003843 RID: 14403
		// (get) Token: 0x0600B082 RID: 45186 RVA: 0x002964FD File Offset: 0x002946FD
		public string Witnessdirectory
		{
			get
			{
				return this.witnessdirectory;
			}
		}

		// Token: 0x040061A6 RID: 24998
		private readonly string share;

		// Token: 0x040061A7 RID: 24999
		private readonly string server;

		// Token: 0x040061A8 RID: 25000
		private readonly string currentdirectory;

		// Token: 0x040061A9 RID: 25001
		private readonly string witnessdirectory;
	}
}
