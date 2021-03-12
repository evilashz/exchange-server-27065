using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Cluster;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200088A RID: 2186
	internal class DagNetworkConfigDataProvider : IConfigDataProvider
	{
		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x06004BFC RID: 19452 RVA: 0x0013B780 File Offset: 0x00139980
		public string TargetServer
		{
			get
			{
				return this.m_targetServer;
			}
		}

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x06004BFD RID: 19453 RVA: 0x0013B788 File Offset: 0x00139988
		public DatabaseAvailabilityGroup TargetDag
		{
			get
			{
				return this.m_dag;
			}
		}

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x06004BFE RID: 19454 RVA: 0x0013B790 File Offset: 0x00139990
		public DagNetworkConfiguration NetworkConfig
		{
			get
			{
				return this.m_netConfig;
			}
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x0013B798 File Offset: 0x00139998
		public DagNetworkConfigDataProvider(IConfigurationSession adSession, string targetServer, DatabaseAvailabilityGroup dag)
		{
			this.m_adSession = adSession;
			this.m_targetServer = targetServer;
			this.m_dag = dag;
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x0013B7B5 File Offset: 0x001399B5
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			return this.m_dagNet;
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x0013B7BD File Offset: 0x001399BD
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			return (IEnumerable<T>)this.Find(filter, rootId, deepSearch, sortBy);
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x0013B7CF File Offset: 0x001399CF
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			return this.Find(filter, rootId, deepSearch, sortBy).ToArray();
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x0013B7E4 File Offset: 0x001399E4
		private List<DatabaseAvailabilityGroupNetwork> Find(QueryFilter queryFilter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			List<DatabaseAvailabilityGroupNetwork> list = new List<DatabaseAvailabilityGroupNetwork>();
			DagNetworkQueryFilter dagNetworkQueryFilter = null;
			if (queryFilter != null)
			{
				dagNetworkQueryFilter = (queryFilter as DagNetworkQueryFilter);
			}
			IEnumerable<DatabaseAvailabilityGroup> enumerable;
			if (this.TargetDag != null)
			{
				enumerable = new DatabaseAvailabilityGroup[]
				{
					this.TargetDag
				};
			}
			else
			{
				string identity = "*";
				if (dagNetworkQueryFilter != null && dagNetworkQueryFilter.NamesToMatch != null && dagNetworkQueryFilter.NamesToMatch.DagName != null)
				{
					identity = dagNetworkQueryFilter.NamesToMatch.DagName;
				}
				DatabaseAvailabilityGroupIdParameter databaseAvailabilityGroupIdParameter = DatabaseAvailabilityGroupIdParameter.Parse(identity);
				enumerable = databaseAvailabilityGroupIdParameter.GetObjects<DatabaseAvailabilityGroup>(null, this.m_adSession);
			}
			if (enumerable != null)
			{
				Regex regex = null;
				if (dagNetworkQueryFilter != null && dagNetworkQueryFilter.NamesToMatch != null && dagNetworkQueryFilter.NamesToMatch.NetName != null)
				{
					string pattern = Wildcard.ConvertToRegexPattern(dagNetworkQueryFilter.NamesToMatch.NetName);
					regex = new Regex(pattern, RegexOptions.IgnoreCase);
				}
				foreach (DatabaseAvailabilityGroup databaseAvailabilityGroup in enumerable)
				{
					DagNetworkConfiguration dagNetworkConfiguration = (this.m_targetServer == null) ? DagNetworkRpc.GetDagNetworkConfig(databaseAvailabilityGroup) : DagNetworkRpc.GetDagNetworkConfig(this.m_targetServer);
					if (dagNetworkConfiguration != null && dagNetworkConfiguration.Networks != null)
					{
						this.m_netConfig = dagNetworkConfiguration;
						foreach (DatabaseAvailabilityGroupNetwork databaseAvailabilityGroupNetwork in dagNetworkConfiguration.Networks)
						{
							if (regex == null || regex.IsMatch(databaseAvailabilityGroupNetwork.Name))
							{
								DagNetworkObjectId identity2 = new DagNetworkObjectId(databaseAvailabilityGroup.Name, databaseAvailabilityGroupNetwork.Name);
								databaseAvailabilityGroupNetwork.SetIdentity(identity2);
								databaseAvailabilityGroupNetwork.ResetChangeTracking();
								list.Add(databaseAvailabilityGroupNetwork);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x0013B978 File Offset: 0x00139B78
		private DatabaseAvailabilityGroup GetDagByName(string dagName)
		{
			if (this.m_dag == null || this.m_dag.Name != dagName)
			{
				this.m_dag = DagTaskHelper.ReadDagByName(dagName, this.m_adSession);
			}
			return this.m_dag;
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x0013B9AD File Offset: 0x00139BAD
		public DagNetworkConfiguration ReadNetConfig(DatabaseAvailabilityGroup dag)
		{
			this.m_netConfig = DagNetworkRpc.GetDagNetworkConfig(dag);
			return this.m_netConfig;
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x0013B9C4 File Offset: 0x00139BC4
		public void Save(IConfigurable instance)
		{
			DatabaseAvailabilityGroupNetwork databaseAvailabilityGroupNetwork = (DatabaseAvailabilityGroupNetwork)instance;
			DagNetworkObjectId dagNetworkObjectId = (DagNetworkObjectId)databaseAvailabilityGroupNetwork.Identity;
			SetDagNetworkRequest setDagNetworkRequest = new SetDagNetworkRequest();
			if (dagNetworkObjectId == null)
			{
				setDagNetworkRequest.Name = databaseAvailabilityGroupNetwork.Name;
			}
			else
			{
				setDagNetworkRequest.Name = dagNetworkObjectId.NetName;
			}
			if (databaseAvailabilityGroupNetwork.IsChanged(DatabaseAvailabilityGroupNetworkSchema.Name))
			{
				setDagNetworkRequest.NewName = databaseAvailabilityGroupNetwork.Name;
			}
			if (databaseAvailabilityGroupNetwork.IsChanged(DatabaseAvailabilityGroupNetworkSchema.Description))
			{
				setDagNetworkRequest.Description = databaseAvailabilityGroupNetwork.Description;
			}
			if (databaseAvailabilityGroupNetwork.IsChanged(DatabaseAvailabilityGroupNetworkSchema.ReplicationEnabled))
			{
				setDagNetworkRequest.ReplicationEnabled = databaseAvailabilityGroupNetwork.ReplicationEnabled;
			}
			if (databaseAvailabilityGroupNetwork.IsChanged(DatabaseAvailabilityGroupNetworkSchema.IgnoreNetwork))
			{
				setDagNetworkRequest.IgnoreNetwork = databaseAvailabilityGroupNetwork.IgnoreNetwork;
			}
			if (databaseAvailabilityGroupNetwork.IsChanged(DatabaseAvailabilityGroupNetworkSchema.Subnets))
			{
				setDagNetworkRequest.SubnetListIsSet = true;
				foreach (DatabaseAvailabilityGroupNetworkSubnet databaseAvailabilityGroupNetworkSubnet in databaseAvailabilityGroupNetwork.Subnets)
				{
					setDagNetworkRequest.Subnets.Add(databaseAvailabilityGroupNetworkSubnet.SubnetId, null);
				}
			}
			this.GetDagByName(dagNetworkObjectId.DagName);
			DagNetworkRpc.SetDagNetwork(this.m_dag, setDagNetworkRequest);
			DagNetworkObjectId identity = new DagNetworkObjectId(this.m_dag.Name, databaseAvailabilityGroupNetwork.Name);
			databaseAvailabilityGroupNetwork.SetIdentity(identity);
			databaseAvailabilityGroupNetwork.ResetChangeTracking();
			this.m_dagNet = databaseAvailabilityGroupNetwork;
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x0013BB18 File Offset: 0x00139D18
		public void Delete(IConfigurable instance)
		{
			DatabaseAvailabilityGroupNetwork databaseAvailabilityGroupNetwork = (DatabaseAvailabilityGroupNetwork)instance;
			DagNetworkObjectId dagNetworkObjectId = (DagNetworkObjectId)databaseAvailabilityGroupNetwork.Identity;
			this.GetDagByName(dagNetworkObjectId.DagName);
			RemoveDagNetworkRequest removeDagNetworkRequest = new RemoveDagNetworkRequest();
			removeDagNetworkRequest.Name = dagNetworkObjectId.NetName;
			DagNetworkRpc.RemoveDagNetwork(this.m_dag, removeDagNetworkRequest);
		}

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x06004C08 RID: 19464 RVA: 0x0013BB63 File Offset: 0x00139D63
		public string Source
		{
			get
			{
				if (this.m_dag != null)
				{
					return this.m_dag.Name;
				}
				return null;
			}
		}

		// Token: 0x04002D6D RID: 11629
		private IConfigurationSession m_adSession;

		// Token: 0x04002D6E RID: 11630
		private readonly string m_targetServer;

		// Token: 0x04002D6F RID: 11631
		private DatabaseAvailabilityGroup m_dag;

		// Token: 0x04002D70 RID: 11632
		private DatabaseAvailabilityGroupNetwork m_dagNet;

		// Token: 0x04002D71 RID: 11633
		private DagNetworkConfiguration m_netConfig;
	}
}
