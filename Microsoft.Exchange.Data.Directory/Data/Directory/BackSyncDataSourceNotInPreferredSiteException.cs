using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AB9 RID: 2745
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BackSyncDataSourceNotInPreferredSiteException : DataSourceTransientException
	{
		// Token: 0x0600805B RID: 32859 RVA: 0x001A520B File Offset: 0x001A340B
		public BackSyncDataSourceNotInPreferredSiteException(string domainController) : base(DirectoryStrings.BackSyncDataSourceInDifferentSiteMessage(domainController))
		{
			this.domainController = domainController;
		}

		// Token: 0x0600805C RID: 32860 RVA: 0x001A5220 File Offset: 0x001A3420
		public BackSyncDataSourceNotInPreferredSiteException(string domainController, Exception innerException) : base(DirectoryStrings.BackSyncDataSourceInDifferentSiteMessage(domainController), innerException)
		{
			this.domainController = domainController;
		}

		// Token: 0x0600805D RID: 32861 RVA: 0x001A5236 File Offset: 0x001A3436
		protected BackSyncDataSourceNotInPreferredSiteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domainController = (string)info.GetValue("domainController", typeof(string));
		}

		// Token: 0x0600805E RID: 32862 RVA: 0x001A5260 File Offset: 0x001A3460
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domainController", this.domainController);
		}

		// Token: 0x17002ECE RID: 11982
		// (get) Token: 0x0600805F RID: 32863 RVA: 0x001A527B File Offset: 0x001A347B
		public string DomainController
		{
			get
			{
				return this.domainController;
			}
		}

		// Token: 0x040055A8 RID: 21928
		private readonly string domainController;
	}
}
