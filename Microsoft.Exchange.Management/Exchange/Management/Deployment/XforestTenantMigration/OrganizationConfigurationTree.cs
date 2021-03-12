using System;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Management.Automation;
using Interop.ActiveDS;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Deployment.XforestTenantMigration
{
	// Token: 0x02000D83 RID: 3459
	internal sealed class OrganizationConfigurationTree
	{
		// Token: 0x1700294E RID: 10574
		// (get) Token: 0x060084D5 RID: 34005 RVA: 0x0021E705 File Offset: 0x0021C905
		// (set) Token: 0x060084D6 RID: 34006 RVA: 0x0021E70D File Offset: 0x0021C90D
		internal Task.TaskVerboseLoggingDelegate WriteVerboseDelegate { get; set; }

		// Token: 0x1700294F RID: 10575
		// (get) Token: 0x060084D7 RID: 34007 RVA: 0x0021E716 File Offset: 0x0021C916
		// (set) Token: 0x060084D8 RID: 34008 RVA: 0x0021E71E File Offset: 0x0021C91E
		internal Task.TaskWarningLoggingDelegate WriteWarningDelegate { get; set; }

		// Token: 0x17002950 RID: 10576
		// (get) Token: 0x060084D9 RID: 34009 RVA: 0x0021E727 File Offset: 0x0021C927
		// (set) Token: 0x060084DA RID: 34010 RVA: 0x0021E72F File Offset: 0x0021C92F
		internal Task.TaskErrorLoggingDelegate WriteErrorDelegate { get; set; }

		// Token: 0x17002951 RID: 10577
		// (get) Token: 0x060084DB RID: 34011 RVA: 0x0021E738 File Offset: 0x0021C938
		// (set) Token: 0x060084DC RID: 34012 RVA: 0x0021E740 File Offset: 0x0021C940
		public Node Root { get; set; }

		// Token: 0x060084DD RID: 34013 RVA: 0x0021E74C File Offset: 0x0021C94C
		public OrganizationConfigurationTree(DirectoryObjectCollection data)
		{
			if (data == null || data.Count == 0)
			{
				throw new ArgumentNullException("data");
			}
			ADObjectId adobjectId = new ADObjectId(data.ElementAt(0).Properties["distinguishedname"][0].ToString());
			this.Root = new Node(adobjectId.AncestorDN(adobjectId.Depth).ToDNString());
			foreach (DirectoryObject adObject in data)
			{
				this.AddLeaf(this.Root, adObject);
			}
		}

		// Token: 0x060084DE RID: 34014 RVA: 0x0021E8F8 File Offset: 0x0021CAF8
		private void AddLeaf(Node node, DirectoryObject adObject)
		{
			string[] array = DNConvertor.SplitDistinguishedName(adObject.Properties["distinguishedname"][0].ToString(), ',').Reverse<string>().ToArray<string>();
			for (int i = 0; i < array.Length; i++)
			{
				if (string.Compare(node.Name, array[i]) != 0)
				{
					if (!node.Children.ContainsKey(array[i]))
					{
						Node value = new Node(array[i]);
						node.Children.Add(array[i], value);
					}
					node = node.Children[array[i]];
				}
				if (i == array.Length - 1)
				{
					node.Value = adObject;
				}
			}
		}

		// Token: 0x060084DF RID: 34015 RVA: 0x0021E998 File Offset: 0x0021CB98
		public void Replace(Node node, string oldValue, string newValue)
		{
			if (node == null)
			{
				node = this.Root;
			}
			if (node.Value != null)
			{
				node.Value.Replace(oldValue, newValue);
			}
			foreach (Node node2 in node.Children.Values)
			{
				this.Replace(node2, oldValue, newValue);
			}
		}

		// Token: 0x060084E0 RID: 34016 RVA: 0x0021EA14 File Offset: 0x0021CC14
		public void Import(Node node, DirectoryBindingInfo directoryInformation)
		{
			if (node == null)
			{
				node = this.Root;
			}
			if (string.IsNullOrEmpty(OrganizationConfigurationTree.homemta))
			{
				using (DirectoryEntry directoryEntry = directoryInformation.GetDirectoryEntry(Path.Combine(directoryInformation.LdapBasePath, directoryInformation.ConfigurationNamingContextDN)))
				{
					SearchResult searchResult = new DirectorySearcher(directoryEntry)
					{
						Filter = "(CN=Microsoft MTA)"
					}.FindOne();
					if (searchResult != null)
					{
						OrganizationConfigurationTree.homemta = searchResult.Properties["distinguishedName"][0].ToString();
					}
				}
			}
			if (string.IsNullOrEmpty(OrganizationConfigurationTree.homemdb))
			{
				using (DirectoryEntry directoryEntry2 = directoryInformation.GetDirectoryEntry(Path.Combine(directoryInformation.LdapBasePath, directoryInformation.ConfigurationNamingContextDN)))
				{
					SearchResult searchResult2 = new DirectorySearcher(directoryEntry2)
					{
						Filter = "(objectclass=msExchPrivateMDB)"
					}.FindOne();
					if (searchResult2 != null)
					{
						OrganizationConfigurationTree.homemdb = searchResult2.Properties["distinguishedName"][0].ToString();
					}
				}
			}
			if (string.IsNullOrEmpty(OrganizationConfigurationTree.msexchowningserver))
			{
				using (DirectoryEntry directoryEntry3 = directoryInformation.GetDirectoryEntry(Path.Combine(directoryInformation.LdapBasePath, directoryInformation.ConfigurationNamingContextDN)))
				{
					SearchResult searchResult3 = new DirectorySearcher(directoryEntry3)
					{
						Filter = "(msexchowningserver=*)"
					}.FindOne();
					if (searchResult3 != null)
					{
						OrganizationConfigurationTree.msexchowningserver = searchResult3.Properties["msexchowningserver"][0].ToString();
						OrganizationConfigurationTree.msexchmasterserveroravailabilitygroup = searchResult3.Properties["msexchmasterserveroravailabilitygroup"][0].ToString();
					}
				}
			}
			foreach (Node node2 in node.Children.Values)
			{
				if (node2.Value != null && !this.IsNodeExist(node2.Value.DistinguishedName, directoryInformation))
				{
					if (this.IsNodeExist(node2.Value.ParentDistinguishedName, directoryInformation))
					{
						this.WriteVerbose(string.Format("Trying to import node {0}", node2.Value.DistinguishedName));
						using (DirectoryEntry directoryEntry4 = directoryInformation.GetDirectoryEntry(Path.Combine(directoryInformation.LdapBasePath, node2.Value.ParentDistinguishedName.ToString())))
						{
							using (DirectoryEntry directoryEntry5 = directoryEntry4.Children.Add(node2.Name, node2.Value.ObjectClass))
							{
								if (node2.Value.Properties.Contains("samaccountname"))
								{
									node2.Value.Properties["samaccountname"][0] = this.GetUniqueSamAccount(node2.Value.Properties["samaccountname"][0].ToString(), directoryInformation);
								}
								foreach (DirectoryProperty directoryProperty in node2.Value.Properties)
								{
									if (directoryProperty.Name.ToLower() == "homemdb")
									{
										directoryProperty.Values[0] = OrganizationConfigurationTree.homemdb;
									}
									if (directoryProperty.Name.ToLower() == "homemta")
									{
										directoryProperty.Values[0] = OrganizationConfigurationTree.homemta;
									}
									if (directoryProperty.Name.ToLower() == "msexchowningserver" || directoryProperty.Name.ToLower() == "offlineabserver")
									{
										directoryProperty.Values[0] = OrganizationConfigurationTree.msexchowningserver;
									}
									if (directoryProperty.Name.ToLower() == "msexchmasterserveroravailabilitygroup")
									{
										directoryProperty.Values[0] = OrganizationConfigurationTree.msexchmasterserveroravailabilitygroup;
									}
									this.AddObjectProperties(directoryProperty, directoryEntry5, node2, true, directoryInformation);
								}
								try
								{
									directoryEntry5.CommitChanges();
								}
								catch (Exception ex)
								{
									this.WriteError(new Exception(string.Format("Error committing changes to object {0}.  Inner exception was {1}.", node2.Value.DistinguishedName, ex.Message)));
								}
								if (node2.Value.ObjectClass.ToLower() == "user" && node2.Value.Properties["useraccountcontrol"][0].ToString() == "512")
								{
									directoryEntry5.Invoke("SetPassword", new object[]
									{
										Guid.NewGuid().ToString()
									});
									directoryEntry5.Properties["useraccountcontrol"].Value = 512;
									try
									{
										directoryEntry5.CommitChanges();
									}
									catch (Exception ex2)
									{
										this.WriteError(new Exception(string.Format("Error setting temp password on object {0}.  Inner exception was {1}.", node2.Value.DistinguishedName, ex2.Message)));
									}
								}
								foreach (DirectoryProperty property in node2.Value.Properties)
								{
									this.AddObjectProperties(property, directoryEntry5, node2, false, directoryInformation);
									try
									{
										directoryEntry5.CommitChanges();
									}
									catch (Exception ex3)
									{
										this.WriteError(new Exception(string.Format("Error setting non mandatory properties on object {0}.  Inner exception was {1}.", node2.Value.DistinguishedName, ex3.Message)));
									}
								}
							}
							continue;
						}
					}
					this.WriteError(new Exception("Can't import object " + node2.Value.DistinguishedName + " because its parent object does not exist."));
				}
			}
			foreach (Node node3 in node.Children.Values)
			{
				this.Import(node3, directoryInformation);
			}
		}

		// Token: 0x060084E1 RID: 34017 RVA: 0x0021F10C File Offset: 0x0021D30C
		public void UpdateDelayedProperties(Node node, DirectoryBindingInfo directoryInformation)
		{
			if (node == null)
			{
				node = this.Root;
			}
			foreach (Node node2 in node.Children.Values)
			{
				if (node2.Value != null && node2.Value.DelayedProperties.Count<DirectoryProperty>() > 0)
				{
					this.WriteVerbose(string.Format("Updating object {0} with properties that were delayed.", node2.Value.DistinguishedName));
					using (DirectoryEntry directoryEntry = directoryInformation.GetDirectoryEntry(Path.Combine(directoryInformation.LdapBasePath, node2.Value.DistinguishedName.ToString())))
					{
						foreach (DirectoryProperty directoryProperty in node2.Value.DelayedProperties)
						{
							object value = directoryEntry.Properties[directoryProperty.Name].Value;
							directoryEntry.Properties[directoryProperty.Name].Value = null;
							for (int i = 0; i < directoryProperty.Values.Count; i++)
							{
								if (this.IsNodeExist(node2.Value.DelayedProperties[directoryProperty.Name][i].ToString(), directoryInformation))
								{
									if (directoryProperty.Syntax == ActiveDirectorySyntax.Int64)
									{
										this.WriteVerbose(string.Format("Updating Property {0}: {1} ; {2}", directoryProperty.Name, directoryProperty.Syntax, directoryProperty.Values[i]));
										directoryEntry.Properties[directoryProperty.Name].Add(this.GetLargeInteger((long)directoryProperty.Values[i]));
									}
									else
									{
										this.WriteVerbose(string.Format("Updating Property {0}: {1} ; {2}", directoryProperty.Name, directoryProperty.Syntax, directoryProperty.Values[i]));
										directoryEntry.Properties[directoryProperty.Name].Add(directoryProperty.Values[i]);
									}
								}
								else
								{
									this.WriteWarning(string.Format("Could not update property {0} on object {1}.  Distinguished Name {2} was not found on target directory.", directoryProperty.Name, node2.Value.DistinguishedName, directoryProperty.Values[i]));
								}
							}
							if (directoryEntry.Properties[directoryProperty.Name].Value == null)
							{
								directoryEntry.Properties[directoryProperty.Name].Value = value;
							}
						}
						try
						{
							directoryEntry.CommitChanges();
						}
						catch (Exception ex)
						{
							this.WriteError(new Exception(string.Format("Error updating delayed properties on object {0}.  Inner exception was {1}.", node2.Value.DistinguishedName, ex.Message)));
						}
					}
				}
			}
			foreach (Node node3 in node.Children.Values)
			{
				this.UpdateDelayedProperties(node3, directoryInformation);
			}
		}

		// Token: 0x060084E2 RID: 34018 RVA: 0x0021F48C File Offset: 0x0021D68C
		private string GetUniqueSamAccount(string p, DirectoryBindingInfo d)
		{
			DirectoryEntry directoryEntry = d.GetDirectoryEntry(d.LdapBasePath + d.DefaultNamingContextDN);
			using (SearchResultCollection searchResultCollection = new DirectorySearcher(directoryEntry)
			{
				Filter = "(samaccountname=" + p + ")"
			}.FindAll())
			{
				if (searchResultCollection != null && searchResultCollection.Count > 0)
				{
					Random random = new Random();
					p = random.Next(0, int.MaxValue).ToString();
				}
			}
			return p;
		}

		// Token: 0x060084E3 RID: 34019 RVA: 0x0021F51C File Offset: 0x0021D71C
		private void AddObjectProperties(DirectoryProperty property, DirectoryEntry newObject, Node childNode, bool importOnlyRequiredProperties, DirectoryBindingInfo directoryInformation)
		{
			if (!property.IsSystemOnly && !property.IsBackLink && property.Values.Count > 0 && !this.excludedProperties.Contains(property.Name.ToLower()) && property.IsRequired == importOnlyRequiredProperties)
			{
				if (property.IsLink)
				{
					bool flag = false;
					foreach (object obj in property.Values)
					{
						string distinguishedName = (string)obj;
						if (!this.IsNodeExist(distinguishedName, directoryInformation))
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						if (property.IsRequired)
						{
							newObject.Properties[property.Name].Value = directoryInformation.SchemaNamingContextDN;
						}
						childNode.Value.DelayedProperties.Add(property);
						return;
					}
					for (int i = 0; i < property.Values.Count; i++)
					{
						this.WriteVerbose(string.Format("Adding Property {0}: {1} ; {2}", property.Name, property.Syntax, property.Values[i]));
						newObject.Properties[property.Name].Add(property.Values[i]);
					}
					return;
				}
				else
				{
					for (int j = 0; j < property.Values.Count; j++)
					{
						if (property.Syntax == ActiveDirectorySyntax.Int64)
						{
							this.WriteVerbose(string.Format("Adding Property {0}: {1} ; 2", property.Name, property.Syntax, property.Values[j]));
							newObject.Properties[property.Name].Add(this.GetLargeInteger((long)property.Values[j]));
						}
						else
						{
							this.WriteVerbose(string.Format("Adding Property {0}: {1}; {2}", property.Name, property.Syntax, property.Values[j]));
							newObject.Properties[property.Name].Add(property.Values[j]);
						}
					}
				}
			}
		}

		// Token: 0x060084E4 RID: 34020 RVA: 0x0021F74C File Offset: 0x0021D94C
		private bool IsNodeExist(string distinguishedName, DirectoryBindingInfo directoryInformation)
		{
			bool result = false;
			if (distinguishedName == null || string.IsNullOrEmpty(distinguishedName.ToString()))
			{
				return result;
			}
			using (DirectoryEntry directoryEntry = directoryInformation.GetDirectoryEntry(Path.Combine(directoryInformation.LdapBasePath, distinguishedName.ToString())))
			{
				using (DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry))
				{
					try
					{
						directorySearcher.SearchScope = SearchScope.Base;
						using (SearchResultCollection searchResultCollection = directorySearcher.FindAll())
						{
							if (searchResultCollection != null && searchResultCollection.Count > 0)
							{
								result = true;
							}
						}
					}
					catch (InvalidOperationException)
					{
					}
					catch (DirectoryServicesCOMException)
					{
					}
				}
			}
			return result;
		}

		// Token: 0x060084E5 RID: 34021 RVA: 0x0021F818 File Offset: 0x0021DA18
		private object GetLargeInteger(long value)
		{
			return new LargeIntegerClass
			{
				HighPart = (int)(value >> 32),
				LowPart = (int)(value & (long)((ulong)-1))
			};
		}

		// Token: 0x060084E6 RID: 34022 RVA: 0x0021F842 File Offset: 0x0021DA42
		private void WriteVerbose(string message)
		{
			if (this.WriteVerboseDelegate != null)
			{
				this.WriteVerboseDelegate(new LocalizedString(message));
			}
		}

		// Token: 0x060084E7 RID: 34023 RVA: 0x0021F85D File Offset: 0x0021DA5D
		private void WriteWarning(string message)
		{
			if (this.WriteWarningDelegate != null)
			{
				this.WriteWarningDelegate(new LocalizedString(message));
			}
		}

		// Token: 0x060084E8 RID: 34024 RVA: 0x0021F878 File Offset: 0x0021DA78
		private void WriteError(Exception ex)
		{
			if (this.WriteErrorDelegate != null)
			{
				this.WriteErrorDelegate(ex, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x0400403A RID: 16442
		private static string homemta;

		// Token: 0x0400403B RID: 16443
		private static string homemdb;

		// Token: 0x0400403C RID: 16444
		private static string msexchmasterserveroravailabilitygroup;

		// Token: 0x0400403D RID: 16445
		private static string msexchowningserver;

		// Token: 0x0400403E RID: 16446
		private string[] excludedProperties = new string[]
		{
			"objectcategory",
			"cn",
			"adspath",
			"primarygroupid",
			"pwdlastset",
			"lastlogon",
			"lastlogontimestamp",
			"lastlogoff",
			"logoncount",
			"badpwdcount",
			"badpasswordtime",
			"samaccounttype",
			"revision",
			"objectsid",
			"domainreplica",
			"creationtime",
			"modifiedcount",
			"modifiedcountatlastpromotion",
			"nextrid",
			"serverstate",
			"iscriticalsystemobject",
			"dbcspwd",
			"ntpwdhistory",
			"lmpwdhistory",
			"badpasswordtime",
			"supplementalcredentials",
			"useraccountcontrol"
		};
	}
}
