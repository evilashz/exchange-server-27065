using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AD9 RID: 2777
	[Cmdlet("Get", "OfflineAddressBook", DefaultParameterSetName = "Identity")]
	public sealed class GetOfflineAddressBook : GetMultitenancySystemConfigurationObjectTask<OfflineAddressBookIdParameter, OfflineAddressBook>
	{
		// Token: 0x060062AD RID: 25261 RVA: 0x0019BC50 File Offset: 0x00199E50
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(OrgContainerNotFoundException).IsInstanceOfType(exception) || typeof(TenantOrgContainerNotFoundException).IsInstanceOfType(exception);
		}

		// Token: 0x17001DEF RID: 7663
		// (get) Token: 0x060062AE RID: 25262 RVA: 0x0019BC86 File Offset: 0x00199E86
		// (set) Token: 0x060062AF RID: 25263 RVA: 0x0019BC9D File Offset: 0x00199E9D
		[Parameter(Mandatory = true, ParameterSetName = "Server", ValueFromPipeline = true)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17001DF0 RID: 7664
		// (get) Token: 0x060062B0 RID: 25264 RVA: 0x0019BCB0 File Offset: 0x00199EB0
		protected override QueryFilter InternalFilter
		{
			get
			{
				return this.internalFilter ?? base.InternalFilter;
			}
		}

		// Token: 0x17001DF1 RID: 7665
		// (get) Token: 0x060062B1 RID: 25265 RVA: 0x0019BCC2 File Offset: 0x00199EC2
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060062B2 RID: 25266 RVA: 0x0019BCC8 File Offset: 0x00199EC8
		protected override void InternalValidate()
		{
			if (this.Server != null)
			{
				Server server = (Server)base.GetDataObject<Server>(this.Server, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
				this.internalFilter = new ComparisonFilter(ComparisonOperator.Equal, OfflineAddressBookSchema.Server, server.Id);
			}
			base.InternalValidate();
		}

		// Token: 0x060062B3 RID: 25267 RVA: 0x0019BD40 File Offset: 0x00199F40
		protected override void WriteResult(IConfigurable dataObject)
		{
			OfflineAddressBook offlineAddressBook = (OfflineAddressBook)dataObject;
			offlineAddressBook.ResolveConfiguredAttributes();
			base.WriteResult(new OfflineAddressBookPresentationObject(offlineAddressBook));
		}

		// Token: 0x040035E2 RID: 13794
		private QueryFilter internalFilter;
	}
}
