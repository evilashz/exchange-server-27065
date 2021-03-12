using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000664 RID: 1636
	internal sealed class RoleAssignmentExpansion
	{
		// Token: 0x06003979 RID: 14713 RVA: 0x000F1898 File Offset: 0x000EFA98
		internal RoleAssignmentExpansion(IRecipientSession recSession, OrganizationId organizationId)
		{
			RoleAssignmentExpansion.groupProperties.Add(ADRecipientSchema.MemberOfGroup);
			this.recipientSession = recSession;
			this.adRecipientExpansion = new ADRecipientExpansion(this.recipientSession, true);
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x000F18F4 File Offset: 0x000EFAF4
		internal MultiValuedProperty<FormattedADObjectIdCollection> GetAssignmentChains(ADObjectId root, ADObjectId user)
		{
			if (this.assignmentChainsDictionary.ContainsKey(this.GetKey(root, user)))
			{
				return new MultiValuedProperty<FormattedADObjectIdCollection>(this.assignmentChainsDictionary[this.GetKey(root, user)]);
			}
			return null;
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x000F1928 File Offset: 0x000EFB28
		internal List<ADObjectId> GetEffectiveUsersForRoleAssignment(ExchangeRoleAssignment roleAssignment)
		{
			if (this.usersListLookup.ContainsKey(roleAssignment.User))
			{
				return this.usersListLookup[roleAssignment.User];
			}
			this.usersList = new List<ADObjectId>();
			ADRawEntry adrawEntry = this.recipientSession.ReadADRawEntry(roleAssignment.User, RoleAssignmentExpansion.groupProperties);
			if (adrawEntry != null)
			{
				this.adRecipientExpansion.Expand(adrawEntry, new ADRecipientExpansion.HandleRecipientDelegate(this.OnRecipient), new ADRecipientExpansion.HandleFailureDelegate(this.OnFailure));
				foreach (RoleAssignmentExpansion.UserNode userNode in this.userNodeLookupTable.Values)
				{
					userNode.ResetTraverseIndex();
				}
				this.CalculateAssignmentChains(roleAssignment);
				this.usersListLookup[roleAssignment.User] = this.usersList;
			}
			else
			{
				ExTraceGlobals.ADConfigTracer.TraceError<ADObjectId>(0L, "Error while getting ADRawEntry for User '{0}'", roleAssignment.User);
			}
			return this.usersList;
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x000F1A2C File Offset: 0x000EFC2C
		private void CalculateAssignmentChains(ExchangeRoleAssignment roleAssignment)
		{
			RoleAssignmentExpansion.UserNode node = this.AddORGetNode(roleAssignment.User);
			this.CalculateAssignmentChainsNonRecursive(node);
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x000F1A50 File Offset: 0x000EFC50
		private void CalculateAssignmentChainsNonRecursive(RoleAssignmentExpansion.UserNode node)
		{
			RoleAssignmentExpansion.UserNode userNode = node;
			List<RoleAssignmentExpansion.UserNode> list = new List<RoleAssignmentExpansion.UserNode>();
			list.Add(node);
			while (list.Count > 0)
			{
				while (node.Children.Count > 0)
				{
					RoleAssignmentExpansion.UserNode nextChild = node.GetNextChild();
					if (nextChild == null)
					{
						break;
					}
					node = nextChild;
					list.Add(node);
					if (node.Children.Count == 0)
					{
						List<ADObjectId> list2 = new List<ADObjectId>();
						foreach (RoleAssignmentExpansion.UserNode userNode2 in list)
						{
							list2.Add(userNode2.User);
						}
						list2.Remove(node.User);
						if (this.assignmentChainsDictionary.ContainsKey(this.GetKey(userNode.User, node.User)))
						{
							List<FormattedADObjectIdCollection> list3 = this.assignmentChainsDictionary[this.GetKey(userNode.User, node.User)];
							list3.Add(new FormattedADObjectIdCollection(list2));
						}
						else
						{
							List<FormattedADObjectIdCollection> list4 = new List<FormattedADObjectIdCollection>();
							list4.Add(new FormattedADObjectIdCollection(list2));
							this.assignmentChainsDictionary.Add(this.GetKey(userNode.User, node.User), list4);
						}
					}
				}
				list.Remove(node);
				if (list.Count > 0)
				{
					node = list[list.Count - 1];
				}
			}
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x000F1BB0 File Offset: 0x000EFDB0
		private string GetKey(ADObjectId parent, ADObjectId user)
		{
			return parent.ObjectGuid.ToString() + RoleAssignmentExpansion.CommaSeparator + user.ObjectGuid.ToString();
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x000F1BF0 File Offset: 0x000EFDF0
		private RoleAssignmentExpansion.UserNode AddORGetNode(ADObjectId parentId)
		{
			if (this.userNodeLookupTable.ContainsKey(parentId))
			{
				return this.userNodeLookupTable[parentId];
			}
			RoleAssignmentExpansion.UserNode userNode = new RoleAssignmentExpansion.UserNode(parentId);
			this.userNodeLookupTable[parentId] = userNode;
			return userNode;
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x000F1C30 File Offset: 0x000EFE30
		private ExpansionControl OnRecipient(ADRawEntry recipient, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentExpansionType)
		{
			RoleAssignmentExpansion.UserNode item = this.AddORGetNode(recipient.Id);
			if (parent != null)
			{
				RoleAssignmentExpansion.UserNode userNode = this.AddORGetNode(parent.Id);
				if (!userNode.Children.Contains(item))
				{
					userNode.Children.Add(item);
				}
			}
			if (recipientExpansionType != ExpansionType.GroupMembership && !this.usersList.Contains(recipient.Id))
			{
				this.usersList.Add(recipient.Id);
			}
			if (recipientExpansionType == ExpansionType.None || recipientExpansionType == ExpansionType.GroupMembership)
			{
				return ExpansionControl.Continue;
			}
			return ExpansionControl.Skip;
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x000F1CA8 File Offset: 0x000EFEA8
		private ExpansionControl OnFailure(ExpansionFailure failure, ADRawEntry recipient, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentExpansionType)
		{
			if (failure == ExpansionFailure.LoopDetected)
			{
				return ExpansionControl.Skip;
			}
			return ExpansionControl.Continue;
		}

		// Token: 0x0400261A RID: 9754
		private ADRecipientExpansion adRecipientExpansion;

		// Token: 0x0400261B RID: 9755
		private static List<PropertyDefinition> groupProperties = new List<PropertyDefinition>(ADRecipientExpansion.RequiredProperties);

		// Token: 0x0400261C RID: 9756
		private IRecipientSession recipientSession;

		// Token: 0x0400261D RID: 9757
		private static string CommaSeparator = ":";

		// Token: 0x0400261E RID: 9758
		private List<ADObjectId> usersList;

		// Token: 0x0400261F RID: 9759
		private Dictionary<ADObjectId, RoleAssignmentExpansion.UserNode> userNodeLookupTable = new Dictionary<ADObjectId, RoleAssignmentExpansion.UserNode>();

		// Token: 0x04002620 RID: 9760
		private Dictionary<ADObjectId, List<ADObjectId>> usersListLookup = new Dictionary<ADObjectId, List<ADObjectId>>();

		// Token: 0x04002621 RID: 9761
		private Dictionary<string, List<FormattedADObjectIdCollection>> assignmentChainsDictionary = new Dictionary<string, List<FormattedADObjectIdCollection>>();

		// Token: 0x02000665 RID: 1637
		private sealed class UserNode
		{
			// Token: 0x06003983 RID: 14723 RVA: 0x000F1CCC File Offset: 0x000EFECC
			internal UserNode(ADObjectId user)
			{
				this.userId = user;
				this.children = new List<RoleAssignmentExpansion.UserNode>();
			}

			// Token: 0x17001113 RID: 4371
			// (get) Token: 0x06003984 RID: 14724 RVA: 0x000F1CE6 File Offset: 0x000EFEE6
			internal ADObjectId User
			{
				get
				{
					return this.userId;
				}
			}

			// Token: 0x17001114 RID: 4372
			// (get) Token: 0x06003985 RID: 14725 RVA: 0x000F1CEE File Offset: 0x000EFEEE
			internal List<RoleAssignmentExpansion.UserNode> Children
			{
				get
				{
					return this.children;
				}
			}

			// Token: 0x06003986 RID: 14726 RVA: 0x000F1CF6 File Offset: 0x000EFEF6
			internal RoleAssignmentExpansion.UserNode GetNextChild()
			{
				if (this.index < this.Children.Count)
				{
					this.index++;
					return this.Children[this.index - 1];
				}
				this.index = 0;
				return null;
			}

			// Token: 0x06003987 RID: 14727 RVA: 0x000F1D35 File Offset: 0x000EFF35
			internal void ResetTraverseIndex()
			{
				this.index = 0;
			}

			// Token: 0x04002622 RID: 9762
			private ADObjectId userId;

			// Token: 0x04002623 RID: 9763
			private List<RoleAssignmentExpansion.UserNode> children;

			// Token: 0x04002624 RID: 9764
			private int index;
		}
	}
}
