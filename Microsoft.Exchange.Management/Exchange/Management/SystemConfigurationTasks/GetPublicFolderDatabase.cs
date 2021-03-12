using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000987 RID: 2439
	[Cmdlet("Get", "PublicFolderDatabase", DefaultParameterSetName = "Identity")]
	public sealed class GetPublicFolderDatabase : GetDatabaseTask<PublicFolderDatabase>, IPFTreeTask
	{
		// Token: 0x170019E9 RID: 6633
		// (get) Token: 0x06005700 RID: 22272 RVA: 0x00167948 File Offset: 0x00165B48
		OrganizationId IPFTreeTask.CurrentOrganizationId
		{
			get
			{
				return base.CurrentOrganizationId;
			}
		}

		// Token: 0x170019EA RID: 6634
		// (get) Token: 0x06005701 RID: 22273 RVA: 0x00167950 File Offset: 0x00165B50
		ADObjectId IPFTreeTask.RootOrgContainerId
		{
			get
			{
				return base.RootOrgContainerId;
			}
		}

		// Token: 0x170019EB RID: 6635
		// (get) Token: 0x06005702 RID: 22274 RVA: 0x00167958 File Offset: 0x00165B58
		IConfigDataProvider IPFTreeTask.DataSession
		{
			get
			{
				return base.DataSession;
			}
		}

		// Token: 0x170019EC RID: 6636
		// (get) Token: 0x06005703 RID: 22275 RVA: 0x00167960 File Offset: 0x00165B60
		ITopologyConfigurationSession IPFTreeTask.GlobalConfigSession
		{
			get
			{
				return base.GlobalConfigSession;
			}
		}

		// Token: 0x170019ED RID: 6637
		// (get) Token: 0x06005704 RID: 22276 RVA: 0x00167968 File Offset: 0x00165B68
		OrganizationId IPFTreeTask.ExecutingUserOrganizationId
		{
			get
			{
				return base.ExecutingUserOrganizationId;
			}
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x00167970 File Offset: 0x00165B70
		OrganizationId IPFTreeTask.ResolveCurrentOrganization()
		{
			return OrganizationId.ForestWideOrgId;
		}

		// Token: 0x06005706 RID: 22278 RVA: 0x00167977 File Offset: 0x00165B77
		T IPFTreeTask.GetDataObject<T>(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, LocalizedString? notFoundError, LocalizedString? multipleFoundError)
		{
			return (T)((object)base.GetDataObject<T>(id, session, rootID, notFoundError, multipleFoundError));
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x0016798B File Offset: 0x00165B8B
		void IPFTreeTask.WriteVerbose(LocalizedString text)
		{
			base.WriteVerbose(text);
		}

		// Token: 0x06005708 RID: 22280 RVA: 0x00167994 File Offset: 0x00165B94
		void IPFTreeTask.WriteWarning(LocalizedString text)
		{
			this.WriteWarning(text);
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x0016799D File Offset: 0x00165B9D
		void IPFTreeTask.WriteError(Exception exception, ErrorCategory category, object target)
		{
			base.WriteError(exception, category, target);
		}

		// Token: 0x170019EE RID: 6638
		// (get) Token: 0x0600570A RID: 22282 RVA: 0x001679A8 File Offset: 0x00165BA8
		// (set) Token: 0x0600570B RID: 22283 RVA: 0x001679BF File Offset: 0x00165BBF
		[ValidateNotNullOrEmpty]
		[Parameter]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x170019EF RID: 6639
		// (get) Token: 0x0600570C RID: 22284 RVA: 0x001679D2 File Offset: 0x00165BD2
		public override SwitchParameter IncludePreExchange2013
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019F0 RID: 6640
		// (get) Token: 0x0600570D RID: 22285 RVA: 0x001679DC File Offset: 0x00165BDC
		protected override QueryFilter InternalFilter
		{
			get
			{
				PFTreeManagement pftreeManagement = new PFTreeManagement(this);
				QueryFilter queryFilter;
				if (this.Organization != null)
				{
					if (pftreeManagement.PFTree != null)
					{
						queryFilter = new ComparisonFilter(ComparisonOperator.Equal, PublicFolderDatabaseSchema.PublicFolderHierarchy, pftreeManagement.PFTree.Id);
					}
					else
					{
						queryFilter = new NotFilter(new ExistsFilter(PublicFolderDatabaseSchema.PublicFolderHierarchy));
					}
				}
				else
				{
					queryFilter = new ExistsFilter(PublicFolderDatabaseSchema.PublicFolderHierarchy);
				}
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.InternalFilter,
					queryFilter
				});
			}
		}

		// Token: 0x0600570E RID: 22286 RVA: 0x00167A54 File Offset: 0x00165C54
		protected override void WriteResult(IConfigurable dataObject)
		{
			PublicFolderDatabase publicFolderDatabase = (PublicFolderDatabase)dataObject;
			if (!publicFolderDatabase.IsReadOnly)
			{
				Server server = publicFolderDatabase.GetServer();
				if (server == null)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorDBOwningServerNotFound(publicFolderDatabase.Identity.ToString())), ErrorCategory.InvalidOperation, publicFolderDatabase.Identity);
				}
				publicFolderDatabase.UseCustomReferralServerList = server.UseCustomReferralServerList;
				foreach (ServerCostPair serverCostPair in server.CustomReferralServerList)
				{
					ServerIdParameter serverIdParameter = new ServerIdParameter(new ADObjectId(null, serverCostPair.ServerGuid));
					IEnumerable<Server> objects = serverIdParameter.GetObjects<Server>(null, base.DataSession);
					IEnumerator<Server> enumerator2 = objects.GetEnumerator();
					Server server2 = null;
					if (enumerator2.MoveNext())
					{
						server2 = enumerator2.Current;
						if (enumerator2.MoveNext())
						{
							server2 = null;
						}
					}
					if (server2 == null)
					{
						publicFolderDatabase.CustomReferralServerList.Add(serverCostPair);
					}
					else
					{
						publicFolderDatabase.CustomReferralServerList.Add(new ServerCostPair(server2.Guid, server2.Name, serverCostPair.Cost));
					}
				}
				publicFolderDatabase.ResetChangeTracking();
			}
			base.WriteResult(dataObject);
		}

		// Token: 0x06005710 RID: 22288 RVA: 0x00167B8C File Offset: 0x00165D8C
		Fqdn IPFTreeTask.get_DomainController()
		{
			return base.DomainController;
		}
	}
}
