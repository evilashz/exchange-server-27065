using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Office.CompliancePolicy.ComplianceData;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.ApplicationLogic.Compliance
{
	// Token: 0x020000C7 RID: 199
	internal class ExFolderComplianceItemContainer : ExComplianceItemContainer
	{
		// Token: 0x06000865 RID: 2149 RVA: 0x00021E3B File Offset: 0x0002003B
		internal ExFolderComplianceItemContainer(MailboxSession session, ComplianceItemContainer parent, Folder folder)
		{
			this.session = session;
			this.folder = folder;
			this.parent = parent;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x00021E58 File Offset: 0x00020058
		public override bool HasItems
		{
			get
			{
				return this.folder.ItemCount > 0;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00021E68 File Offset: 0x00020068
		public override string Id
		{
			get
			{
				return this.folder.StoreObjectId.ToString();
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x00021E7C File Offset: 0x0002007C
		public override List<ComplianceItemContainer> Ancestors
		{
			get
			{
				if (this.parents == null)
				{
					this.parents = new List<ComplianceItemContainer>();
					for (ExFolderComplianceItemContainer exFolderComplianceItemContainer = this; exFolderComplianceItemContainer != null; exFolderComplianceItemContainer = (exFolderComplianceItemContainer.parent as ExFolderComplianceItemContainer))
					{
						this.parents.Add(this.parent);
					}
				}
				return this.parents;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x00021EC6 File Offset: 0x000200C6
		public override bool SupportsAssociation
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00021ECD File Offset: 0x000200CD
		public override bool SupportsBinding
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x00021ED4 File Offset: 0x000200D4
		public override string Template
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x00021EDB File Offset: 0x000200DB
		internal override ComplianceItemPagedReader ComplianceItemPagedReader
		{
			get
			{
				return new ExFolderSearchComplianceItemPagedReader(this);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00021EE3 File Offset: 0x000200E3
		internal override MailboxSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00021EEB File Offset: 0x000200EB
		internal Folder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00021EF4 File Offset: 0x000200F4
		public override void ForEachChildContainer(Action<ComplianceItemContainer> containerHandler, Func<ComplianceItemContainer, Exception, bool> exHandler)
		{
			if (this.folder.HasSubfolders)
			{
				QueryResult queryResult = this.folder.FolderQuery(FolderQueryFlags.None, null, null, ExMailboxComplianceItemContainer.FolderDataColumns);
				using (FolderEnumerator folderEnumerator = new FolderEnumerator(queryResult, this.folder, this.folder.GetProperties(ExMailboxComplianceItemContainer.FolderDataColumns)))
				{
					while (folderEnumerator != null && folderEnumerator.MoveNext())
					{
						for (int i = 0; i < folderEnumerator.Current.Count; i++)
						{
							VersionedId versionedId = folderEnumerator.Current[i][0] as VersionedId;
							if (versionedId != null)
							{
								Folder folder = Folder.Bind(this.session, versionedId.ObjectId);
								if (this.folder.StoreObjectId != folder.StoreObjectId)
								{
									using (ExFolderComplianceItemContainer exFolderComplianceItemContainer = new ExFolderComplianceItemContainer(this.session, this, folder))
									{
										try
										{
											containerHandler(exFolderComplianceItemContainer);
										}
										catch (Exception arg)
										{
											exHandler(exFolderComplianceItemContainer, arg);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00022018 File Offset: 0x00020218
		public override bool SupportsPolicy(PolicyScenario scenario)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002201F File Offset: 0x0002021F
		public override void UpdatePolicy(Dictionary<PolicyScenario, List<PolicyRuleConfig>> rules)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00022026 File Offset: 0x00020226
		public override void AddPolicy(PolicyDefinitionConfig definition, PolicyRuleConfig rule)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0002202D File Offset: 0x0002022D
		public override void RemovePolicy(Guid id, PolicyScenario scenario)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00022034 File Offset: 0x00020234
		public override bool HasPolicy(Guid id, PolicyScenario scenario)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0002203B File Offset: 0x0002023B
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.folder != null)
			{
				this.folder.Dispose();
			}
		}

		// Token: 0x040003BA RID: 954
		private MailboxSession session;

		// Token: 0x040003BB RID: 955
		private Folder folder;

		// Token: 0x040003BC RID: 956
		private ComplianceItemContainer parent;

		// Token: 0x040003BD RID: 957
		private List<ComplianceItemContainer> parents;
	}
}
