using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.Psws;
using Microsoft.Exchange.PswsClient;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020004EC RID: 1260
	public static class DistributionGroupHelper
	{
		// Token: 0x06003D11 RID: 15633 RVA: 0x000B73E0 File Offset: 0x000B55E0
		public static string GenerateGroupTypeText(object groupType)
		{
			if (groupType == DBNull.Value)
			{
				return string.Empty;
			}
			RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)groupType;
			if (recipientTypeDetails <= RecipientTypeDetails.MailNonUniversalGroup)
			{
				if (recipientTypeDetails == RecipientTypeDetails.MailUniversalDistributionGroup || recipientTypeDetails == RecipientTypeDetails.MailNonUniversalGroup)
				{
					return Strings.DistributionGroupText;
				}
			}
			else
			{
				if (recipientTypeDetails == RecipientTypeDetails.MailUniversalSecurityGroup)
				{
					return Strings.SecurityGroupText;
				}
				if (recipientTypeDetails == RecipientTypeDetails.DynamicDistributionGroup)
				{
					return Strings.DynamicDistributionGroupText;
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x000B7460 File Offset: 0x000B5660
		public static void GetSDOPostAction(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			dataRow["DisplayedManagedBy"] = DistributionGroupHelper.ResolveRecipientDisplayNamesForSDO(dataRow["ManagedBy"]);
			dataRow["DisplayedModeratedBy"] = DistributionGroupHelper.ResolveRecipientDisplayNamesForSDO(dataRow["ModeratedBy"]);
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x000B74B8 File Offset: 0x000B56B8
		private static MultiValuedProperty<string> ResolveRecipientDisplayNamesForSDO(object value)
		{
			List<string> list = new List<string>();
			if (value != null)
			{
				bool flag = false;
				List<ADObjectId> list2 = new List<ADObjectId>();
				ADObjectId adobjectId = value as ADObjectId;
				if (adobjectId != null)
				{
					list2.Add(adobjectId);
				}
				else
				{
					IEnumerable<ADObjectId> enumerable = value as IEnumerable<ADObjectId>;
					if (enumerable != null)
					{
						foreach (ADObjectId item in enumerable)
						{
							if (list2.Count == 3)
							{
								flag = true;
								break;
							}
							list2.Add(item);
						}
					}
				}
				List<RecipientObjectResolverRow> source = RecipientObjectResolver.Instance.ResolveObjects(list2).ToList<RecipientObjectResolverRow>();
				list.AddRange(from resolvedRecipient in source
				select resolvedRecipient.DisplayName);
				if (flag)
				{
					list.Add(Strings.EllipsisText);
				}
			}
			return new MultiValuedProperty<string>(list);
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x000B75A0 File Offset: 0x000B57A0
		public static void FilterEntSendAsPermission(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.Rows[0]["SendAsPermissions"] = MailboxPropertiesHelper.FindRecipientsWithSendAsPermissionEnt(store.GetDataObject("ADPermissions") as IEnumerable<object>, null);
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x000B75CE File Offset: 0x000B57CE
		public static void FilterCloudSendAsPermission(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.Rows[0]["SendAsPermissions"] = MailboxPropertiesHelper.FindRecipientsWithSendAsPermissionCloud(store.GetDataObject("RecipientPermission") as IEnumerable<object>);
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x000B75FC File Offset: 0x000B57FC
		public static void GetObjectForNewPostAction(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			if (DDIUtil.IsInRole("Mailbox"))
			{
				ADObjectId executingUserId = EacRbacPrincipal.Instance.ExecutingUserId;
				dataRow["ManagedBy"] = new MultiValuedProperty<ADObjectId>(new object[]
				{
					executingUserId
				});
			}
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x000B7658 File Offset: 0x000B5858
		public static void NewObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] results)
		{
			if (results.Length == 0 || dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			PowerShellResults powerShellResults = results[0];
			if (powerShellResults.Succeeded && string.Compare((string)inputRow["Name"], (string)dataRow["DisplayName"]) != 0)
			{
				string text = Strings.GroupNameWithNamingPolciy((string)dataRow["DisplayName"]);
				powerShellResults.Informations = new string[]
				{
					text
				};
			}
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x000B76E5 File Offset: 0x000B58E5
		public static void GetDDGObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			EmailAddressPolicy.GetObjectPostAction(inputRow, dataTable, store);
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000B76F0 File Offset: 0x000B58F0
		public static void GetFFOGroupList(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			try
			{
				table.BeginLoadData();
				GetRecipientCmdlet getRecipientCmdlet = new GetRecipientCmdlet
				{
					Filter = inputRow["SearchText"].ToString().ToRecipeintFilterString("((RecipientTypeDetails -eq 'MailUniversalDistributionGroup') -or (RecipientTypeDetails -eq 'MailUniversalSecurityGroup'))"),
					Properties = "Identity,DisplayName,PrimarySmtpAddress,RecipientTypeDetails"
				};
				getRecipientCmdlet.Authenticator = PswsAuthenticator.Create();
				getRecipientCmdlet.HostServerName = AppConfigLoader.GetConfigStringValue("PswsHostName", null);
				foreach (Recipient recipient in getRecipientCmdlet.Run())
				{
					DataRow dataRow = table.NewRow();
					dataRow["Identity"] = new Identity(recipient.Guid.ToString());
					dataRow["DisplayName"] = recipient.DisplayName;
					dataRow["PrimarySmtpAddress"] = recipient.PrimarySmtpAddress;
					dataRow["RecipientTypeDetails"] = DistributionGroupHelper.GenerateGroupTypeText(Enum.Parse(typeof(RecipientTypeDetails), recipient.RecipientTypeDetails, true));
					dataRow["GroupType"] = recipient.RecipientTypeDetails;
					table.Rows.Add(dataRow);
				}
			}
			finally
			{
				table.EndLoadData();
			}
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000B784C File Offset: 0x000B5A4C
		public static void NewFFOGroup(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			NewDistributionGroupCmdlet newDistributionGroupCmdlet = new NewDistributionGroupCmdlet();
			newDistributionGroupCmdlet.Authenticator = PswsAuthenticator.Create();
			newDistributionGroupCmdlet.HostServerName = AppConfigLoader.GetConfigStringValue("PswsHostName", null);
			newDistributionGroupCmdlet.Name = inputRow["Name"].ToString();
			newDistributionGroupCmdlet.DisplayName = inputRow["Name"].ToString();
			newDistributionGroupCmdlet.Alias = inputRow["Alias"].ToString();
			newDistributionGroupCmdlet.PrimarySmtpAddress = inputRow["PrimarySmtpAddress"].ToString();
			newDistributionGroupCmdlet.Notes = inputRow["Notes"].ToString();
			string[] managedBy = (from o in (object[])inputRow["ManagedBy"]
			select ((Identity)o).DisplayName).ToArray<string>();
			newDistributionGroupCmdlet.ManagedBy = managedBy;
			string[] members = (from o in (object[])inputRow["Members"]
			select ((Identity)o).DisplayName).ToArray<string>();
			newDistributionGroupCmdlet.Members = members;
			newDistributionGroupCmdlet.Type = inputRow["GroupType"].ToString();
			newDistributionGroupCmdlet.Run();
			if (!string.IsNullOrEmpty(newDistributionGroupCmdlet.Error))
			{
				throw new Exception(newDistributionGroupCmdlet.Error);
			}
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000B79A0 File Offset: 0x000B5BA0
		public static void RemoveFFOGroup(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			RemoveDistributionGroupCmdlet removeDistributionGroupCmdlet = new RemoveDistributionGroupCmdlet();
			removeDistributionGroupCmdlet.Authenticator = PswsAuthenticator.Create();
			removeDistributionGroupCmdlet.HostServerName = AppConfigLoader.GetConfigStringValue("PswsHostName", null);
			removeDistributionGroupCmdlet.Identity = ((Identity)inputRow["Identity"]).RawIdentity;
			removeDistributionGroupCmdlet.Run();
			if (!string.IsNullOrEmpty(removeDistributionGroupCmdlet.Error))
			{
				throw new Exception(removeDistributionGroupCmdlet.Error);
			}
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x000B7A30 File Offset: 0x000B5C30
		public static void SetFFOGroup(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			SetDistributionGroupCmdlet setDistributionGroupCmdlet = new SetDistributionGroupCmdlet();
			setDistributionGroupCmdlet.Authenticator = PswsAuthenticator.Create();
			setDistributionGroupCmdlet.HostServerName = AppConfigLoader.GetConfigStringValue("PswsHostName", null);
			setDistributionGroupCmdlet.Identity = ((Identity)inputRow["Identity"]).RawIdentity;
			if (!DBNull.Value.Equals(inputRow["DisplayName"]))
			{
				setDistributionGroupCmdlet.DisplayName = inputRow["DisplayName"].ToString();
			}
			if (!DBNull.Value.Equals(inputRow["Alias"]))
			{
				setDistributionGroupCmdlet.Alias = inputRow["Alias"].ToString();
			}
			if (!DBNull.Value.Equals(inputRow["PrimarySmtpAddress"]))
			{
				setDistributionGroupCmdlet.PrimarySmtpAddress = inputRow["PrimarySmtpAddress"].ToString();
			}
			if (!DBNull.Value.Equals(inputRow["ManagedBy"]))
			{
				string[] managedBy = (from o in (object[])inputRow["ManagedBy"]
				select ((Identity)o).DisplayName).ToArray<string>();
				setDistributionGroupCmdlet.ManagedBy = managedBy;
			}
			setDistributionGroupCmdlet.Run();
			if (!string.IsNullOrEmpty(setDistributionGroupCmdlet.Error))
			{
				throw new Exception(setDistributionGroupCmdlet.Error);
			}
			if (!DBNull.Value.Equals(inputRow["MembersRemoved"]) || !DBNull.Value.Equals(inputRow["MembersAdded"]))
			{
				string[] members = (from o in (object[])inputRow["Members"]
				select ((Identity)o).DisplayName).ToArray<string>();
				UpdateDistributionGroupMemberCmdlet updateDistributionGroupMemberCmdlet = new UpdateDistributionGroupMemberCmdlet();
				updateDistributionGroupMemberCmdlet.Authenticator = PswsAuthenticator.Create();
				updateDistributionGroupMemberCmdlet.HostServerName = AppConfigLoader.GetConfigStringValue("PswsHostName", null);
				updateDistributionGroupMemberCmdlet.Identity = ((Identity)inputRow["Identity"]).RawIdentity;
				updateDistributionGroupMemberCmdlet.Members = members;
				updateDistributionGroupMemberCmdlet.Run();
				if (!string.IsNullOrEmpty(updateDistributionGroupMemberCmdlet.Error))
				{
					throw new Exception(updateDistributionGroupMemberCmdlet.Error);
				}
			}
			if (!DBNull.Value.Equals(inputRow["Notes"]))
			{
				SetGroupCmdlet setGroupCmdlet = new SetGroupCmdlet();
				setGroupCmdlet.Authenticator = PswsAuthenticator.Create();
				setGroupCmdlet.HostServerName = AppConfigLoader.GetConfigStringValue("PswsHostName", null);
				setGroupCmdlet.Identity = ((Identity)inputRow["Identity"]).RawIdentity;
				setGroupCmdlet.Notes = inputRow["Notes"].ToString();
				if (!DBNull.Value.Equals(inputRow["ManagedBy"]))
				{
					string[] managedBy2 = (from o in (object[])inputRow["ManagedBy"]
					select ((Identity)o).DisplayName).ToArray<string>();
					setGroupCmdlet.ManagedBy = managedBy2;
				}
				setGroupCmdlet.Run();
				if (!string.IsNullOrEmpty(setGroupCmdlet.Error))
				{
					throw new Exception(setGroupCmdlet.Error);
				}
			}
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x000B7D38 File Offset: 0x000B5F38
		public static void GetFFOSDOItem(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			GetDistributionGroupCmdlet getDistributionGroupCmdlet = new GetDistributionGroupCmdlet
			{
				Identity = ((Identity)inputRow["Identity"]).RawIdentity
			};
			getDistributionGroupCmdlet.Authenticator = PswsAuthenticator.Create();
			getDistributionGroupCmdlet.HostServerName = AppConfigLoader.GetConfigStringValue("PswsHostName", null);
			DistributionGroup[] array = getDistributionGroupCmdlet.Run();
			if (array == null || array.Length == 0)
			{
				throw new ExArgumentOutOfRangeException();
			}
			DistributionGroup distributionGroup = array.First<DistributionGroup>();
			DataRow dataRow = table.Rows[0];
			dataRow["Identity"] = new Identity(distributionGroup.Guid.ToString());
			dataRow["DisplayName"] = distributionGroup.DisplayName;
			dataRow["PrimarySmtpAddress"] = distributionGroup.PrimarySmtpAddress;
			dataRow["RecipientTypeDetails"] = DistributionGroupHelper.GenerateGroupTypeText(Enum.Parse(typeof(RecipientTypeDetails), distributionGroup.RecipientTypeDetails, true));
			dataRow["GroupType"] = DistributionGroupHelper.GenerateGroupTypeText(Enum.Parse(typeof(RecipientTypeDetails), distributionGroup.RecipientTypeDetails, true));
			dataRow["Notes"] = distributionGroup.Notes;
			if (distributionGroup.ManagedBy == null || distributionGroup.ManagedBy.Length == 0)
			{
				dataRow["DisplayedManagedBy"] = null;
			}
			else
			{
				string[] value = (from o in distributionGroup.ManagedBy
				select o.Name).ToArray<string>();
				dataRow["DisplayedManagedBy"] = value;
			}
			GetGroupCmdlet getGroupCmdlet = new GetGroupCmdlet
			{
				Identity = ((Identity)inputRow["Identity"]).RawIdentity
			};
			getGroupCmdlet.Authenticator = PswsAuthenticator.Create();
			getGroupCmdlet.HostServerName = AppConfigLoader.GetConfigStringValue("PswsHostName", null);
			Group[] array2 = getGroupCmdlet.Run();
			if (array2 == null || array2.Length == 0)
			{
				throw new ExArgumentOutOfRangeException();
			}
			Group group = array2.First<Group>();
			if (group.Members == null || group.Members.Length == 0)
			{
				dataRow["MemberCount"] = 0;
			}
			else
			{
				dataRow["MemberCount"] = group.Members.Length;
			}
			dataRow["Notes"] = group.Notes;
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x000B801C File Offset: 0x000B621C
		public static void GetFFOItem(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			GetDistributionGroupCmdlet getDistributionGroupCmdlet = new GetDistributionGroupCmdlet
			{
				Identity = ((Identity)inputRow["Identity"]).RawIdentity
			};
			getDistributionGroupCmdlet.Authenticator = PswsAuthenticator.Create();
			getDistributionGroupCmdlet.HostServerName = AppConfigLoader.GetConfigStringValue("PswsHostName", null);
			DistributionGroup[] array = getDistributionGroupCmdlet.Run();
			if (array == null || array.Length == 0)
			{
				throw new ExArgumentOutOfRangeException();
			}
			DistributionGroup distributionGroup = array.First<DistributionGroup>();
			DataRow dataRow = table.Rows[0];
			dataRow["Identity"] = new Identity(distributionGroup.Guid.ToString());
			dataRow["DisplayName"] = distributionGroup.DisplayName;
			dataRow["Alias"] = distributionGroup.Alias;
			dataRow["PrimarySmtpAddress"] = distributionGroup.PrimarySmtpAddress;
			dataRow["Notes"] = distributionGroup.Notes;
			if (distributionGroup.ManagedBy == null || distributionGroup.ManagedBy.Length == 0)
			{
				dataRow["ManagedBy"] = null;
			}
			else
			{
				dataRow["ManagedBy"] = (from g in distributionGroup.ManagedBy
				select new JsonDictionary<object>(new Dictionary<string, object>
				{
					{
						"__type",
						"JsonDictionaryOfanyType:#Microsoft.Exchange.Management.ControlPanel"
					},
					{
						"Identity",
						new Identity(g.Name, g.Name)
					},
					{
						"DisplayName",
						g.Name
					},
					{
						"Name",
						g.Name
					}
				})).ToArray<JsonDictionary<object>>();
			}
			dataRow["EmailAddresses"] = distributionGroup.EmailAddresses;
			GetGroupCmdlet getGroupCmdlet = new GetGroupCmdlet
			{
				Identity = ((Identity)inputRow["Identity"]).RawIdentity
			};
			getGroupCmdlet.Authenticator = PswsAuthenticator.Create();
			getGroupCmdlet.HostServerName = AppConfigLoader.GetConfigStringValue("PswsHostName", null);
			Group[] array2 = getGroupCmdlet.Run();
			if (array2 == null || array2.Length == 0)
			{
				throw new ExArgumentOutOfRangeException();
			}
			Group group = array2.First<Group>();
			if (group.Members == null || group.Members.Length == 0)
			{
				dataRow["Members"] = null;
			}
			else
			{
				dataRow["Members"] = (from g in @group.Members
				select new JsonDictionary<object>(new Dictionary<string, object>
				{
					{
						"__type",
						"JsonDictionaryOfanyType:#Microsoft.Exchange.Management.ControlPanel"
					},
					{
						"Identity",
						new Identity(g)
					},
					{
						"DisplayName",
						g
					},
					{
						"Name",
						g
					}
				})).ToArray<JsonDictionary<object>>();
			}
			dataRow["Notes"] = group.Notes;
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x000B8230 File Offset: 0x000B6430
		public static void GetFFOForNew(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			ADObjectId executingUserId = EacRbacPrincipal.Instance.ExecutingUserId;
			dataRow["ManagedBy"] = new JsonDictionary<object>[]
			{
				new JsonDictionary<object>(new Dictionary<string, object>
				{
					{
						"__type",
						"JsonDictionaryOfanyType:#Microsoft.Exchange.Management.ControlPanel"
					},
					{
						"Identity",
						new Identity(executingUserId.Name, executingUserId.Name)
					},
					{
						"DisplayName",
						executingUserId.Name
					},
					{
						"Name",
						executingUserId.Name
					}
				})
			};
		}

		// Token: 0x040027DF RID: 10207
		private const string PswsHostName = "PswsHostName";

		// Token: 0x040027E0 RID: 10208
		private const string PropertiesList = "Identity,DisplayName,PrimarySmtpAddress,RecipientTypeDetails";

		// Token: 0x040027E1 RID: 10209
		private const int DisplayedRecipientsNumberForSDO = 3;

		// Token: 0x040027E2 RID: 10210
		public static readonly string NewObjectWorkflowOutput = "DisplayName,PrimarySmtpAddress,RecipientTypeDetails,Identity,GroupType,Name,EmailAddressesTxt,Alias,OrganizationalUnit,WhenChanged,EmailAddressPolicyEnabled,HiddenFromAddressListsEnabled";
	}
}
