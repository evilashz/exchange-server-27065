using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AC6 RID: 2758
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsafeIdentityFilterNotAllowedException : DataSourceOperationException
	{
		// Token: 0x0600809A RID: 32922 RVA: 0x001A578E File Offset: 0x001A398E
		public UnsafeIdentityFilterNotAllowedException(string filter, string orgId) : base(DirectoryStrings.ErrorUnsafeIdentityFilterNotAllowed(filter, orgId))
		{
			this.filter = filter;
			this.orgId = orgId;
		}

		// Token: 0x0600809B RID: 32923 RVA: 0x001A57AB File Offset: 0x001A39AB
		public UnsafeIdentityFilterNotAllowedException(string filter, string orgId, Exception innerException) : base(DirectoryStrings.ErrorUnsafeIdentityFilterNotAllowed(filter, orgId), innerException)
		{
			this.filter = filter;
			this.orgId = orgId;
		}

		// Token: 0x0600809C RID: 32924 RVA: 0x001A57CC File Offset: 0x001A39CC
		protected UnsafeIdentityFilterNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filter = (string)info.GetValue("filter", typeof(string));
			this.orgId = (string)info.GetValue("orgId", typeof(string));
		}

		// Token: 0x0600809D RID: 32925 RVA: 0x001A5821 File Offset: 0x001A3A21
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filter", this.filter);
			info.AddValue("orgId", this.orgId);
		}

		// Token: 0x17002ED9 RID: 11993
		// (get) Token: 0x0600809E RID: 32926 RVA: 0x001A584D File Offset: 0x001A3A4D
		public string Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x17002EDA RID: 11994
		// (get) Token: 0x0600809F RID: 32927 RVA: 0x001A5855 File Offset: 0x001A3A55
		public string OrgId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x040055B3 RID: 21939
		private readonly string filter;

		// Token: 0x040055B4 RID: 21940
		private readonly string orgId;
	}
}
