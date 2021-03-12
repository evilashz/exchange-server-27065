using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Common.Internal;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x02000039 RID: 57
	internal abstract class ConfigValidator : EdgeSyncValidator
	{
		// Token: 0x06000151 RID: 337 RVA: 0x0000789D File Offset: 0x00005A9D
		public ConfigValidator(ReplicationTopology topology, string configObjectName) : base(topology)
		{
			this.searchScope = SearchScope.Subtree;
			this.orgConfigObjectList = new Dictionary<string, ExSearchResultEntry>();
			this.orgAdRootPath = DistinguishedName.RemoveLeafRelativeDistinguishedNames(base.Topology.LocalHub.DistinguishedName, 4);
			this.configObjectName = configObjectName;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000078DB File Offset: 0x00005ADB
		// (set) Token: 0x06000153 RID: 339 RVA: 0x000078E3 File Offset: 0x00005AE3
		public bool UseChangedDate
		{
			get
			{
				return this.useChangedDate;
			}
			set
			{
				this.useChangedDate = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000078EC File Offset: 0x00005AEC
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000078F4 File Offset: 0x00005AF4
		protected string ConfigDirectoryPath
		{
			get
			{
				return this.configDirectoryPath;
			}
			set
			{
				this.configDirectoryPath = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000078FD File Offset: 0x00005AFD
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00007905 File Offset: 0x00005B05
		protected string AdamRootPath
		{
			get
			{
				return this.adamRootPath;
			}
			set
			{
				this.adamRootPath = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000790E File Offset: 0x00005B0E
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00007916 File Offset: 0x00005B16
		protected string OrgAdRootPath
		{
			get
			{
				return this.orgAdRootPath;
			}
			set
			{
				this.orgAdRootPath = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000791F File Offset: 0x00005B1F
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00007927 File Offset: 0x00005B27
		protected string ValidationObjectName
		{
			get
			{
				return this.configObjectName;
			}
			set
			{
				this.configObjectName = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600015C RID: 348
		protected abstract string[] PayloadAttributes { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00007930 File Offset: 0x00005B30
		protected virtual string[] ReadAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00007933 File Offset: 0x00005B33
		protected virtual IDirectorySession DataSession
		{
			get
			{
				return base.Topology.ConfigSession;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00007940 File Offset: 0x00005B40
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00007948 File Offset: 0x00005B48
		protected SearchScope SearchScope
		{
			get
			{
				return this.searchScope;
			}
			set
			{
				this.searchScope = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00007951 File Offset: 0x00005B51
		protected EdgeConnectionInfo CurrentEdgeConnection
		{
			get
			{
				return this.currentEdgeConnection;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00007959 File Offset: 0x00005B59
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00007961 File Offset: 0x00005B61
		protected string LdapQuery
		{
			get
			{
				return this.ldapQuery;
			}
			set
			{
				this.ldapQuery = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000796C File Offset: 0x00005B6C
		protected virtual string ADSearchPath
		{
			get
			{
				return DistinguishedName.Concatinate(new string[]
				{
					this.configDirectoryPath,
					this.orgAdRootPath
				});
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00007998 File Offset: 0x00005B98
		protected virtual string ADAMSearchPath
		{
			get
			{
				return DistinguishedName.Concatinate(new string[]
				{
					this.configDirectoryPath,
					this.adamRootPath
				});
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000079C4 File Offset: 0x00005BC4
		protected virtual string ADLdapQuery
		{
			get
			{
				return this.ldapQuery;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000079CC File Offset: 0x00005BCC
		protected virtual string ADAMLdapQuery
		{
			get
			{
				return this.ldapQuery;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000079D4 File Offset: 0x00005BD4
		public override EdgeConfigStatus Validate(EdgeConnectionInfo subscription)
		{
			this.SaveEdgeConnection(subscription);
			EdgeConfigStatus edgeConfigStatus = new EdgeConfigStatus();
			try
			{
				List<string> list = ConfigValidator.CombineAttributes(this.PayloadAttributes, this.ReadAttributes);
				Dictionary<string, ExSearchResultEntry> adentries = this.GetADEntries();
				bool flag = true;
				int num = 0;
				uint num2 = 0U;
				this.adamRootPath = DistinguishedName.Concatinate(new string[]
				{
					"CN=First Organization,CN=Microsoft Exchange,CN=Services",
					subscription.EdgeConnection.AdamConfigurationNamingContext
				});
				if (base.ProgressMethod != null)
				{
					base.ProgressMethod(Strings.LoadingADAMComparisonList(this.configObjectName, subscription.EdgeServer.Name), Strings.LoadedADAMObjectCount(num));
				}
				foreach (ExSearchResultEntry exSearchResultEntry in subscription.EdgeConnection.PagedScan(this.ADAMSearchPath, this.ADAMLdapQuery, this.searchScope, list.ToArray()))
				{
					string adamRelativePath = this.GetAdamRelativePath(exSearchResultEntry);
					if (adentries.ContainsKey(adamRelativePath))
					{
						if (this.Filter(adentries[adamRelativePath]))
						{
							if (!this.CompareAttributes(exSearchResultEntry, adentries[adamRelativePath], this.PayloadAttributes) && !this.IsInChangeWindow(adentries[adamRelativePath]))
							{
								if (base.MaxReportedLength.IsUnlimited || (ulong)base.MaxReportedLength.Value > (ulong)((long)edgeConfigStatus.ConflictObjects.Count))
								{
									edgeConfigStatus.ConflictObjects.Add(new ADObjectId(adentries[adamRelativePath].DistinguishedName));
								}
								flag = false;
							}
							else
							{
								num2 += 1U;
							}
						}
						else if (this.FilterEdge(exSearchResultEntry))
						{
							if (base.MaxReportedLength.IsUnlimited || (ulong)base.MaxReportedLength.Value > (ulong)((long)edgeConfigStatus.EdgeOnlyObjects.Count))
							{
								edgeConfigStatus.EdgeOnlyObjects.Add(new ADObjectId(exSearchResultEntry.DistinguishedName));
							}
							flag = false;
						}
						adentries.Remove(adamRelativePath);
					}
					else if (this.FilterEdge(exSearchResultEntry))
					{
						if (base.MaxReportedLength.IsUnlimited || (ulong)base.MaxReportedLength.Value > (ulong)((long)edgeConfigStatus.EdgeOnlyObjects.Count))
						{
							edgeConfigStatus.EdgeOnlyObjects.Add(new ADObjectId(exSearchResultEntry.DistinguishedName));
						}
						flag = false;
					}
					if (num % 500 == 0 && base.ProgressMethod != null)
					{
						base.ProgressMethod(Strings.LoadingADAMComparisonList(this.configObjectName, subscription.EdgeServer.Name), Strings.LoadedADAMObjectCount(num));
					}
					num++;
				}
				if (base.ProgressMethod != null)
				{
					base.ProgressMethod(Strings.LoadingADAMComparisonList(this.configObjectName, subscription.EdgeServer.Name), Strings.LoadedADAMObjectCount(num));
				}
				foreach (ExSearchResultEntry exSearchResultEntry2 in adentries.Values)
				{
					if (this.Filter(exSearchResultEntry2) && !this.IsInChangeWindow(exSearchResultEntry2))
					{
						if (base.MaxReportedLength.IsUnlimited || (ulong)base.MaxReportedLength.Value > (ulong)((long)edgeConfigStatus.OrgOnlyObjects.Count))
						{
							edgeConfigStatus.OrgOnlyObjects.Add(new ADObjectId(exSearchResultEntry2.DistinguishedName));
						}
						flag = false;
					}
				}
				edgeConfigStatus.SyncStatus = (flag ? SyncStatus.Synchronized : SyncStatus.NotSynchronized);
				edgeConfigStatus.SynchronizedObjects = num2;
			}
			catch (ExDirectoryException)
			{
				edgeConfigStatus.SyncStatus = SyncStatus.DirectoryError;
			}
			return edgeConfigStatus;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007DC4 File Offset: 0x00005FC4
		public override void LoadValidationInfo()
		{
			List<string> list = ConfigValidator.CombineAttributes(this.PayloadAttributes, this.ReadAttributes);
			Connection orgAdConnection = null;
			try
			{
				ADObjectId rootId = null;
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					orgAdConnection = new Connection(this.DataSession.GetReadConnection(null, ref rootId));
				}, 3);
				if (!adoperationResult.Succeeded)
				{
					throw new ExDirectoryException("Unable to get read connection", adoperationResult.Exception);
				}
				int num = 0;
				if (base.ProgressMethod != null)
				{
					base.ProgressMethod(Strings.LoadingADComparisonList(this.configObjectName), Strings.LoadedADObjectCount(num));
				}
				if (this.UseChangedDate)
				{
					list.Add("whenChanged");
				}
				foreach (ExSearchResultEntry exSearchResultEntry in orgAdConnection.PagedScan(this.ADSearchPath, this.ADLdapQuery, this.searchScope, list.ToArray()))
				{
					string adrelativePath = this.GetADRelativePath(exSearchResultEntry);
					this.orgConfigObjectList.Add(adrelativePath, exSearchResultEntry);
					if (num % 500 == 0 && base.ProgressMethod != null)
					{
						base.ProgressMethod(Strings.LoadingADComparisonList(this.configObjectName), Strings.LoadedADObjectCount(num));
					}
					num++;
				}
				if (base.ProgressMethod != null)
				{
					base.ProgressMethod(Strings.LoadingADComparisonList(this.configObjectName), Strings.LoadedADObjectCount(num));
				}
			}
			finally
			{
				if (orgAdConnection != null)
				{
					orgAdConnection.Dispose();
					orgAdConnection = null;
				}
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007F84 File Offset: 0x00006184
		public override void UnloadValidationInfo()
		{
			this.orgConfigObjectList.Clear();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007F94 File Offset: 0x00006194
		protected static List<string> CombineAttributes(string[] originalAttributes, string[] appendedAttributes)
		{
			List<string> list = new List<string>();
			if (originalAttributes != null)
			{
				list.AddRange(originalAttributes);
			}
			if (appendedAttributes != null)
			{
				list.AddRange(appendedAttributes);
			}
			return list;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007FBC File Offset: 0x000061BC
		protected bool IsInChangeWindow(ExSearchResultEntry entry)
		{
			if (this.useChangedDate && entry.Attributes.ContainsKey("whenChanged"))
			{
				try
				{
					DateTime t = DateTime.ParseExact((string)entry.Attributes["whenChanged"][0], "yyyyMMddHHmmss.fZ", CultureInfo.InvariantCulture);
					if (this.currentEdgeConnection.LastSynchronizedDate < t && this.syncInScheduleWindow)
					{
						return true;
					}
				}
				catch (FormatException)
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008048 File Offset: 0x00006248
		protected virtual string GetADRelativePath(ExSearchResultEntry searchEntry)
		{
			return DistinguishedName.MakeRelativePath(searchEntry.DistinguishedName.ToLower(), this.ADSearchPath.ToLower());
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008065 File Offset: 0x00006265
		protected virtual string GetAdamRelativePath(ExSearchResultEntry searchEntry)
		{
			return DistinguishedName.MakeRelativePath(searchEntry.DistinguishedName.ToLower(), this.ADAMSearchPath.ToLower());
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008082 File Offset: 0x00006282
		protected virtual bool Filter(ExSearchResultEntry entry)
		{
			return true;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008085 File Offset: 0x00006285
		protected virtual bool FilterEdge(ExSearchResultEntry entry)
		{
			return true;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008088 File Offset: 0x00006288
		protected virtual bool CompareAttributes(ExSearchResultEntry edgeEntry, ExSearchResultEntry hubEntry, string[] copyAttributes)
		{
			int i = 0;
			while (i < copyAttributes.Length)
			{
				string key = copyAttributes[i];
				bool flag = edgeEntry.Attributes.ContainsKey(key);
				bool flag2 = hubEntry.Attributes.ContainsKey(key);
				bool result;
				if (flag != flag2)
				{
					result = false;
				}
				else
				{
					if (!flag || this.CompareAttributeValues(edgeEntry.Attributes[key], hubEntry.Attributes[key]))
					{
						i++;
						continue;
					}
					result = false;
				}
				return result;
			}
			return true;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000080FD File Offset: 0x000062FD
		protected bool IsEntryContainer(ExSearchResultEntry entry)
		{
			return entry.ObjectClass == "msExchContainer" || entry.ObjectClass == "container";
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008128 File Offset: 0x00006328
		private List<string> GetAttributeValues(DirectoryAttribute attribute)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < attribute.Count; i++)
			{
				if (attribute[i].GetType() == typeof(byte[]))
				{
					byte[] array = (byte[])attribute[i];
					StringBuilder stringBuilder = new StringBuilder(array.Length * 2);
					foreach (byte b in array)
					{
						stringBuilder.Append(b.ToString("X2"));
					}
					list.Add(stringBuilder.ToString());
				}
				else
				{
					list.Add(attribute[i].ToString());
				}
			}
			list.Sort();
			return list;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000081E0 File Offset: 0x000063E0
		protected bool CompareAttributeValues(DirectoryAttribute edgeAttribute, DirectoryAttribute hubAttribute)
		{
			if (edgeAttribute.Count != hubAttribute.Count)
			{
				return false;
			}
			if (edgeAttribute.Count == 1)
			{
				return this.CompareValues(edgeAttribute[0], hubAttribute[0]);
			}
			List<string> attributeValues = this.GetAttributeValues(hubAttribute);
			List<string> attributeValues2 = this.GetAttributeValues(edgeAttribute);
			for (int i = 0; i < attributeValues.Count; i++)
			{
				if (attributeValues[i] != attributeValues2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008254 File Offset: 0x00006454
		private bool CompareValues(object edgeValue, object hubValue)
		{
			Type type = edgeValue.GetType();
			Type type2 = hubValue.GetType();
			if (type != type2)
			{
				return false;
			}
			if (type == typeof(byte[]))
			{
				return base.CompareBytes((byte[])edgeValue, (byte[])hubValue);
			}
			return edgeValue.Equals(hubValue);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000082A8 File Offset: 0x000064A8
		private Dictionary<string, ExSearchResultEntry> GetADEntries()
		{
			Dictionary<string, ExSearchResultEntry> dictionary = new Dictionary<string, ExSearchResultEntry>();
			foreach (KeyValuePair<string, ExSearchResultEntry> keyValuePair in this.orgConfigObjectList)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return dictionary;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008310 File Offset: 0x00006510
		private void SaveEdgeConnection(EdgeConnectionInfo subscription)
		{
			this.currentEdgeConnection = subscription;
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 751, "SaveEdgeConnection", "f:\\15.00.1497\\sources\\dev\\EdgeSync\\src\\Common\\Validation\\ConfigValidator.cs");
			EdgeSyncServiceConfig edgeSyncServiceConfig = topologyConfigurationSession.Read<EdgeSyncServiceConfig>(topologyConfigurationSession.GetLocalSite().Id.GetChildId("EdgeSyncService"));
			this.syncInScheduleWindow = (edgeSyncServiceConfig.ConfigurationSyncInterval > DateTime.UtcNow.Subtract(subscription.LastSynchronizedDate));
		}

		// Token: 0x040000F5 RID: 245
		private const int UpdateCount = 500;

		// Token: 0x040000F6 RID: 246
		private string configDirectoryPath;

		// Token: 0x040000F7 RID: 247
		private string adamRootPath;

		// Token: 0x040000F8 RID: 248
		private string orgAdRootPath;

		// Token: 0x040000F9 RID: 249
		private string configObjectName;

		// Token: 0x040000FA RID: 250
		private SearchScope searchScope;

		// Token: 0x040000FB RID: 251
		private EdgeConnectionInfo currentEdgeConnection;

		// Token: 0x040000FC RID: 252
		private string ldapQuery;

		// Token: 0x040000FD RID: 253
		private Dictionary<string, ExSearchResultEntry> orgConfigObjectList;

		// Token: 0x040000FE RID: 254
		private bool useChangedDate;

		// Token: 0x040000FF RID: 255
		private bool syncInScheduleWindow;
	}
}
