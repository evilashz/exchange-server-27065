using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000010 RID: 16
	public class DataTableLoaderSet
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00005129 File Offset: 0x00003329
		public DataTableLoaderSet() : this(new DataSet())
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005136 File Offset: 0x00003336
		public DataTableLoaderSet(DataSet dataSet)
		{
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			this.DataSet = dataSet;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005169 File Offset: 0x00003369
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00005171 File Offset: 0x00003371
		public DataSet DataSet { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000517A File Offset: 0x0000337A
		public ReadOnlyCollection<DataTableLoaderConfiguration> DataTableLoaderConfigurations
		{
			get
			{
				if (this.dataTableLoaderConfigurations == null)
				{
					this.dataTableLoaderConfigurations = new ReadOnlyCollection<DataTableLoaderConfiguration>(this.backendDataTableLoaderConfigurations);
				}
				return this.dataTableLoaderConfigurations;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000519C File Offset: 0x0000339C
		public DataTableLoaderConfiguration AddDataTableLoaderConfiguration(string name, IDataTableLoaderCreator dataTableLoaderCreator)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name can not be null or empty.", "name");
			}
			if (dataTableLoaderCreator == null)
			{
				throw new ArgumentNullException("dataTableLoaderCreator");
			}
			DataTableLoaderConfiguration dataTableLoaderConfiguration = new DataTableLoaderConfiguration(name, dataTableLoaderCreator);
			this.backendDataTableLoaderConfigurations.Add(dataTableLoaderConfiguration);
			return dataTableLoaderConfiguration;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000051E4 File Offset: 0x000033E4
		public DataTableLoaderConfiguration GetDataTableLoaderConfiguration(string name)
		{
			foreach (DataTableLoaderConfiguration dataTableLoaderConfiguration in this.DataTableLoaderConfigurations)
			{
				if (dataTableLoaderConfiguration.Name == name)
				{
					return dataTableLoaderConfiguration;
				}
			}
			return null;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005240 File Offset: 0x00003440
		public ReadOnlyCollection<DataTableRelation> DataTableRelations
		{
			get
			{
				if (this.dataTableRelations == null)
				{
					this.dataTableRelations = new ReadOnlyCollection<DataTableRelation>(this.backendDataTableRelations);
				}
				return this.dataTableRelations;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005264 File Offset: 0x00003464
		public void AddDataTableRelation(DataTableRelation relation)
		{
			if (relation == null)
			{
				throw new ArgumentNullException("relation");
			}
			if (!this.backendDataTableRelations.Contains(relation))
			{
				if (relation.DataTableLoaderSet != null && relation.DataTableLoaderSet != this)
				{
					throw new InvalidOperationException(string.Format("the relation '{0}' has been added into anoter DataTableLoaderSet.", relation.RelationName));
				}
				this.backendDataTableRelations.Add(relation);
				relation.DataTableLoaderSet = this;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000052C8 File Offset: 0x000034C8
		public DataTableRelation GetDataTableRelation(string relationName)
		{
			foreach (DataTableRelation dataTableRelation in this.DataTableRelations)
			{
				if (dataTableRelation.RelationName == relationName)
				{
					return dataTableRelation;
				}
			}
			return null;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005324 File Offset: 0x00003524
		private void CreateDataTableLoader(DataTableLoaderConfiguration config)
		{
			if (config.DataTableLoader != null)
			{
				throw new InvalidOperationException(string.Format("DataTableLoader for '{0}' has been created.", config.Name));
			}
			config.CreateDataTableLoader();
			if (this.DataSet.Tables.IndexOf(config.DataTable) == -1)
			{
				this.DataSet.Tables.Add(config.DataTable);
			}
			foreach (DataTableRelation dataTableRelation in this.DataTableRelations)
			{
				if (dataTableRelation.Appliable)
				{
					dataTableRelation.Apply();
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000053CC File Offset: 0x000035CC
		public DataTableLoader GetDataTableLoader(string name)
		{
			DataTableLoaderConfiguration dataTableLoaderConfiguration = this.GetDataTableLoaderConfiguration(name);
			if (dataTableLoaderConfiguration != null)
			{
				if (dataTableLoaderConfiguration.DataTableLoader == null)
				{
					this.CreateDataTableLoader(dataTableLoaderConfiguration);
				}
				return dataTableLoaderConfiguration.DataTableLoader;
			}
			return null;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000053FC File Offset: 0x000035FC
		public DataTableLoader GetRefreshableSource(string dataMember)
		{
			DataTableLoader dataTableLoader = this.GetDataTableLoader(dataMember);
			if (dataTableLoader == null)
			{
				DataTableRelation dataTableRelation = this.GetDataTableRelation(dataMember);
				if (dataTableRelation != null)
				{
					if (dataTableRelation.ParentDataTableLoaderConfiguration.DataTableLoader == null)
					{
						this.CreateDataTableLoader(dataTableRelation.ParentDataTableLoaderConfiguration);
					}
					if (dataTableRelation.ChildDataTableLoaderConfiguration.DataTableLoader == null)
					{
						this.CreateDataTableLoader(dataTableRelation.ChildDataTableLoaderConfiguration);
					}
					dataTableLoader = dataTableRelation.ChildDataTableLoaderConfiguration.DataTableLoader;
				}
			}
			return dataTableLoader;
		}

		// Token: 0x04000037 RID: 55
		private List<DataTableLoaderConfiguration> backendDataTableLoaderConfigurations = new List<DataTableLoaderConfiguration>();

		// Token: 0x04000038 RID: 56
		private ReadOnlyCollection<DataTableLoaderConfiguration> dataTableLoaderConfigurations;

		// Token: 0x04000039 RID: 57
		private List<DataTableRelation> backendDataTableRelations = new List<DataTableRelation>();

		// Token: 0x0400003A RID: 58
		private ReadOnlyCollection<DataTableRelation> dataTableRelations;
	}
}
