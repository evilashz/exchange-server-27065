using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002AF RID: 687
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorNoCASServersInSitePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600232B RID: 9003 RVA: 0x0004E124 File Offset: 0x0004C324
		public ErrorNoCASServersInSitePermanentException(string site, string minVersion) : base(MrsStrings.ErrorNoCASServersInSite(site, minVersion))
		{
			this.site = site;
			this.minVersion = minVersion;
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x0004E141 File Offset: 0x0004C341
		public ErrorNoCASServersInSitePermanentException(string site, string minVersion, Exception innerException) : base(MrsStrings.ErrorNoCASServersInSite(site, minVersion), innerException)
		{
			this.site = site;
			this.minVersion = minVersion;
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x0004E160 File Offset: 0x0004C360
		protected ErrorNoCASServersInSitePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.site = (string)info.GetValue("site", typeof(string));
			this.minVersion = (string)info.GetValue("minVersion", typeof(string));
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x0004E1B5 File Offset: 0x0004C3B5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("site", this.site);
			info.AddValue("minVersion", this.minVersion);
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x0004E1E1 File Offset: 0x0004C3E1
		public string Site
		{
			get
			{
				return this.site;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x0004E1E9 File Offset: 0x0004C3E9
		public string MinVersion
		{
			get
			{
				return this.minVersion;
			}
		}

		// Token: 0x04000FB8 RID: 4024
		private readonly string site;

		// Token: 0x04000FB9 RID: 4025
		private readonly string minVersion;
	}
}
