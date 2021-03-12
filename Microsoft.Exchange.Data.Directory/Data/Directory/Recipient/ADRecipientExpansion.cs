using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics.Components.ADRecipientExpansion;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001FE RID: 510
	internal class ADRecipientExpansion
	{
		// Token: 0x06001A99 RID: 6809 RVA: 0x0006F899 File Offset: 0x0006DA99
		public ADRecipientExpansion() : this(OrganizationId.ForestWideOrgId)
		{
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0006F8A6 File Offset: 0x0006DAA6
		public ADRecipientExpansion(IList<PropertyDefinition> additionalProperties) : this(additionalProperties, OrganizationId.ForestWideOrgId)
		{
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x0006F8B4 File Offset: 0x0006DAB4
		public ADRecipientExpansion(OrganizationId scope)
		{
			this.allProperties = ADRecipientExpansion.requiredProperties;
			this.session = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(scope), 248, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Recipient\\ADRecipientExpansion.cs");
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x0006F8ED File Offset: 0x0006DAED
		public ADRecipientExpansion(IRecipientSession session, bool ignoreMailEnabledCase)
		{
			this.ignoreMailEnabledCase = ignoreMailEnabledCase;
			this.allProperties = ADRecipientExpansion.requiredProperties;
			this.session = session;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0006F90E File Offset: 0x0006DB0E
		public ADRecipientExpansion(IRecipientSession session, bool ignoreMailEnabledCase, IList<PropertyDefinition> additionalProperties)
		{
			this.ignoreMailEnabledCase = ignoreMailEnabledCase;
			this.session = session;
			this.SetAdditionalProperties(additionalProperties);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0006F92B File Offset: 0x0006DB2B
		public ADRecipientExpansion(IList<PropertyDefinition> additionalProperties, OrganizationId scope) : this(scope)
		{
			this.SetAdditionalProperties(additionalProperties);
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x0006F93B File Offset: 0x0006DB3B
		public static IList<PropertyDefinition> RequiredProperties
		{
			get
			{
				return ADRecipientExpansion.requiredPropertiesReadOnly;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x0006F942 File Offset: 0x0006DB42
		// (set) Token: 0x06001AA1 RID: 6817 RVA: 0x0006F94A File Offset: 0x0006DB4A
		public ADObjectId SecurityContext
		{
			get
			{
				return this.securityContext;
			}
			set
			{
				this.securityContext = value;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x0006F953 File Offset: 0x0006DB53
		// (set) Token: 0x06001AA3 RID: 6819 RVA: 0x0006F960 File Offset: 0x0006DB60
		public TimeSpan? LdapTimeout
		{
			get
			{
				return this.session.ServerTimeout;
			}
			set
			{
				this.session.ServerTimeout = value;
			}
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0006F970 File Offset: 0x0006DB70
		public void Expand(ADRawEntry recipientToExpand, ADRecipientExpansion.HandleRecipientDelegate handleRecipient, ADRecipientExpansion.HandleFailureDelegate handleFailure)
		{
			if (recipientToExpand == null)
			{
				throw new ArgumentNullException("recipientToExpand");
			}
			ExTraceGlobals.ADExpansionTracer.TraceDebug((long)this.GetHashCode(), "Requested to expand recipient: {0}", new object[]
			{
				recipientToExpand[ADObjectSchema.Id]
			});
			Stack<ADRecipientExpansion.ExpandableEntry> stack = new Stack<ADRecipientExpansion.ExpandableEntry>();
			if (!this.ProcessChild(recipientToExpand, null, handleRecipient, handleFailure, stack))
			{
				return;
			}
			while (stack.Count > 0)
			{
				ADRecipientExpansion.ExpandableEntry expandableEntry = stack.Pop();
				ExTraceGlobals.ADExpansionTracer.TraceDebug((long)this.GetHashCode(), "Expanding recipient: {0}", new object[]
				{
					expandableEntry.Entry[ADObjectSchema.Id]
				});
				if (!this.ExpandEntry(expandableEntry, handleRecipient, handleFailure, stack))
				{
					ExTraceGlobals.ADExpansionTracer.TraceDebug((long)this.GetHashCode(), "Expansion terminated by delegate");
					return;
				}
			}
			ExTraceGlobals.ADExpansionTracer.TraceDebug((long)this.GetHashCode(), "Expansion completed");
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0006FA48 File Offset: 0x0006DC48
		private static ExpansionType GetExpansionType(ADRawEntry entry)
		{
			RecipientType recipientType = (RecipientType)entry[ADRecipientSchema.RecipientType];
			switch (recipientType)
			{
			case RecipientType.Invalid:
			case RecipientType.PublicDatabase:
			case RecipientType.SystemAttendantMailbox:
			case RecipientType.SystemMailbox:
			case RecipientType.Computer:
				return ExpansionType.None;
			case RecipientType.User:
			case RecipientType.UserMailbox:
			case RecipientType.MailUser:
				if (entry[ADRecipientSchema.ExternalEmailAddress] != null)
				{
					return ADRecipientExpansion.GetContactExpansionType(entry);
				}
				return ADRecipientExpansion.GetAlternateRecipientType(entry);
			case RecipientType.Contact:
			case RecipientType.MailContact:
				return ADRecipientExpansion.GetContactExpansionType(entry);
			case RecipientType.Group:
			case RecipientType.MailUniversalDistributionGroup:
			case RecipientType.MailUniversalSecurityGroup:
			case RecipientType.MailNonUniversalGroup:
			case RecipientType.DynamicDistributionGroup:
				return ExpansionType.GroupMembership;
			case RecipientType.PublicFolder:
			case RecipientType.MicrosoftExchange:
				return ADRecipientExpansion.GetAlternateRecipientType(entry);
			default:
				throw new InvalidOperationException("Unknown recipient type: " + recipientType);
			}
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0006FAF8 File Offset: 0x0006DCF8
		private static ExpansionType GetAlternateRecipientType(ADRawEntry recipient)
		{
			if (recipient[ADRecipientSchema.ForwardingAddress] == null)
			{
				return ExpansionType.None;
			}
			if (!(bool)recipient[IADMailStorageSchema.DeliverToMailboxAndForward])
			{
				return ExpansionType.AlternateRecipientForward;
			}
			return ExpansionType.AlternateRecipientDeliverAndForward;
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0006FB2C File Offset: 0x0006DD2C
		private static ExpansionType GetContactExpansionType(ADRawEntry contact)
		{
			MultiValuedProperty<ProxyAddress> multiValuedProperty = (MultiValuedProperty<ProxyAddress>)contact[ADRecipientSchema.EmailAddresses];
			ProxyAddress proxyAddress = (ProxyAddress)contact[ADRecipientSchema.ExternalEmailAddress];
			if (!(proxyAddress == null) && !multiValuedProperty.Contains(proxyAddress))
			{
				return ExpansionType.ContactChain;
			}
			return ExpansionType.None;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0006FB70 File Offset: 0x0006DD70
		private static ExpansionControl InvokeRecipientDelegate(ADRecipientExpansion.HandleRecipientDelegate handleRecipient, ADRawEntry recipient, ExpansionType expansionType, ADRecipientExpansion.ExpandableEntry parent)
		{
			if (handleRecipient == null)
			{
				return ExpansionControl.Continue;
			}
			ExpansionType expansionType2 = (parent == null) ? ExpansionType.None : parent.ExpansionType;
			ExTraceGlobals.ADExpansionTracer.TraceDebug(0L, "Invoking recipient delegate: recipient={0}; expansion-type={1}; parent={2}; parent-expansion-type={3}", new object[]
			{
				recipient[ADObjectSchema.Id],
				ADRecipientExpansion.GetExpansionTypeString(expansionType),
				(parent == null) ? "<null>" : parent.Entry[ADObjectSchema.Id],
				ADRecipientExpansion.GetExpansionTypeString(expansionType2)
			});
			ExpansionControl expansionControl = handleRecipient(recipient, expansionType, (parent == null) ? null : parent.Entry, expansionType2);
			ExTraceGlobals.ADExpansionTracer.TraceDebug<string>(0L, "Delegate returned '{0}'", ADRecipientExpansion.GetExpansionControlString(expansionControl));
			return expansionControl;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0006FC14 File Offset: 0x0006DE14
		private static ExpansionControl InvokeFailureDelegate(ADRecipientExpansion.HandleFailureDelegate handleFailure, ExpansionFailure failure, ADRawEntry recipient, ExpansionType expansionType, ADRecipientExpansion.ExpandableEntry parent)
		{
			if (handleFailure == null)
			{
				return ExpansionControl.Continue;
			}
			ExpansionType expansionType2 = (parent == null) ? ExpansionType.None : parent.ExpansionType;
			ExTraceGlobals.ADExpansionTracer.TraceDebug(0L, "Invoking failure delegate: failure={0}; recipient={1}; expansion-type={2}; parent={3}; parent-expansion-type={4}", new object[]
			{
				ADRecipientExpansion.GetExpansionFailureString(failure),
				recipient[ADObjectSchema.Id],
				ADRecipientExpansion.GetExpansionTypeString(expansionType),
				(parent == null) ? "<null>" : parent.Entry[ADObjectSchema.Id],
				ADRecipientExpansion.GetExpansionTypeString(expansionType2)
			});
			ExpansionControl expansionControl = handleFailure(failure, recipient, expansionType, (parent == null) ? null : parent.Entry, expansionType2);
			ExTraceGlobals.ADExpansionTracer.TraceDebug<string>(0L, "Delegate returned '{0}'", ADRecipientExpansion.GetExpansionControlString(expansionControl));
			return expansionControl;
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0006FCC7 File Offset: 0x0006DEC7
		private static string GetExpansionTypeString(ExpansionType expansionType)
		{
			return ADRecipientExpansion.expansionTypeStrings[(int)expansionType];
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0006FCD0 File Offset: 0x0006DED0
		private static string GetExpansionControlString(ExpansionControl expansionControl)
		{
			return ADRecipientExpansion.expansionControlStrings[(int)expansionControl];
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0006FCD9 File Offset: 0x0006DED9
		private static string GetExpansionFailureString(ExpansionFailure expansionFailure)
		{
			return ADRecipientExpansion.expansionFailureStrings[(int)expansionFailure];
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x0006FCE2 File Offset: 0x0006DEE2
		private static bool InvokeFailureDelegate(ADRecipientExpansion.HandleFailureDelegate handleFailure, ExpansionFailure failure, ADRecipientExpansion.ExpandableEntry recipient)
		{
			return ADRecipientExpansion.InvokeFailureDelegate(handleFailure, failure, recipient.Entry, recipient.ExpansionType, recipient.Parent) != ExpansionControl.Terminate;
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x0006FD04 File Offset: 0x0006DF04
		private void SetAdditionalProperties(IList<PropertyDefinition> additionalProperties)
		{
			if (additionalProperties == null)
			{
				throw new ArgumentNullException("additionalProperties");
			}
			List<PropertyDefinition> list = new List<PropertyDefinition>(additionalProperties.Count + ADRecipientExpansion.RequiredProperties.Count);
			list.AddRange(ADRecipientExpansion.requiredProperties);
			list.AddRange(additionalProperties);
			this.allProperties = list.ToArray();
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x0006FD54 File Offset: 0x0006DF54
		private bool ExpandEntry(ADRecipientExpansion.ExpandableEntry entry, ADRecipientExpansion.HandleRecipientDelegate handleRecipient, ADRecipientExpansion.HandleFailureDelegate handleFailure, Stack<ADRecipientExpansion.ExpandableEntry> expansionStack)
		{
			switch (entry.ExpansionType)
			{
			case ExpansionType.GroupMembership:
				return this.ExpandGroup(entry, handleRecipient, handleFailure, expansionStack);
			case ExpansionType.AlternateRecipientForward:
			case ExpansionType.AlternateRecipientDeliverAndForward:
				return this.ExpandAlternateRecipient(entry, handleRecipient, handleFailure, expansionStack);
			case ExpansionType.ContactChain:
				return this.ExpandContactChain(entry, handleRecipient, handleFailure, expansionStack);
			default:
				throw new InvalidOperationException("Invalid expansion type: " + entry.ExpansionType.ToString());
			}
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x0006FDC8 File Offset: 0x0006DFC8
		private bool ExpandGroup(ADRecipientExpansion.ExpandableEntry group, ADRecipientExpansion.HandleRecipientDelegate handleRecipient, ADRecipientExpansion.HandleFailureDelegate handleFailure, Stack<ADRecipientExpansion.ExpandableEntry> expansionStack)
		{
			ExTraceGlobals.ADExpansionTracer.TraceDebug((long)this.GetHashCode(), "Expanding group: {0}", new object[]
			{
				group.Entry[ADObjectSchema.Id]
			});
			IADDistributionList iaddistributionList = this.session.FindByObjectGuid((Guid)group.Entry[ADObjectSchema.Guid]) as IADDistributionList;
			if (iaddistributionList == null)
			{
				ExTraceGlobals.ADExpansionTracer.TraceError((long)this.GetHashCode(), "Could not find group object by GUID {0}; treating the group as empty", new object[]
				{
					group.Entry[ADObjectSchema.Guid]
				});
				return ADRecipientExpansion.InvokeFailureDelegate(handleFailure, ExpansionFailure.NoMembers, group);
			}
			ADPagedReader<ADRawEntry> adpagedReader = iaddistributionList.Expand(1000, this.allProperties);
			bool flag = false;
			foreach (ADRawEntry child in adpagedReader)
			{
				flag = true;
				if (!this.ProcessChild(child, group, handleRecipient, handleFailure, expansionStack))
				{
					return false;
				}
			}
			if (!flag)
			{
				ExTraceGlobals.ADExpansionTracer.TraceDebug((long)this.GetHashCode(), "Expanded empty group: {0}", new object[]
				{
					group.Entry[ADObjectSchema.Id]
				});
				return ADRecipientExpansion.InvokeFailureDelegate(handleFailure, ExpansionFailure.NoMembers, group);
			}
			return true;
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0006FF18 File Offset: 0x0006E118
		private bool ExpandAlternateRecipient(ADRecipientExpansion.ExpandableEntry recipient, ADRecipientExpansion.HandleRecipientDelegate handleRecipient, ADRecipientExpansion.HandleFailureDelegate handleFailure, Stack<ADRecipientExpansion.ExpandableEntry> expansionStack)
		{
			ExTraceGlobals.ADExpansionTracer.TraceDebug((long)this.GetHashCode(), "Expanding alternate recipient for: {0}", new object[]
			{
				recipient.Entry[ADObjectSchema.Id]
			});
			ADRawEntry adrawEntry = this.session.ReadADRawEntry((ADObjectId)recipient.Entry[ADRecipientSchema.ForwardingAddress], this.allProperties);
			if (adrawEntry == null)
			{
				ExTraceGlobals.ADExpansionTracer.TraceError((long)this.GetHashCode(), "Alternate recipient {0} for recipient {1} not found", new object[]
				{
					recipient.Entry[ADRecipientSchema.ForwardingAddress],
					recipient.Entry[ADObjectSchema.Id]
				});
				return ADRecipientExpansion.InvokeFailureDelegate(handleFailure, ExpansionFailure.AlternateRecipientNotFound, recipient);
			}
			return this.ProcessChild(adrawEntry, recipient, handleRecipient, handleFailure, expansionStack);
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0006FFDC File Offset: 0x0006E1DC
		private bool ExpandContactChain(ADRecipientExpansion.ExpandableEntry contact, ADRecipientExpansion.HandleRecipientDelegate handleRecipient, ADRecipientExpansion.HandleFailureDelegate handleFailure, Stack<ADRecipientExpansion.ExpandableEntry> expansionStack)
		{
			ExTraceGlobals.ADExpansionTracer.TraceDebug((long)this.GetHashCode(), "Expanding possible contact chain for: {0}", new object[]
			{
				contact.Entry[ADObjectSchema.Id]
			});
			ADRawEntry adrawEntry = this.session.FindByProxyAddress((ProxyAddress)contact.Entry[ADRecipientSchema.ExternalEmailAddress], this.allProperties);
			if (handleRecipient != null)
			{
				ExpansionType expansionType = (adrawEntry == null) ? ExpansionType.None : ExpansionType.ContactChain;
				ExpansionControl expansionControl = ADRecipientExpansion.InvokeRecipientDelegate(handleRecipient, contact.Entry, expansionType, contact.Parent);
				if (expansionControl != ExpansionControl.Continue)
				{
					return expansionControl != ExpansionControl.Terminate;
				}
			}
			if (adrawEntry != null)
			{
				ExTraceGlobals.ADExpansionTracer.TraceDebug((long)this.GetHashCode(), "Found chained object: {0}", new object[]
				{
					adrawEntry[ADObjectSchema.Id]
				});
				return this.ProcessChild(adrawEntry, contact, handleRecipient, handleFailure, expansionStack);
			}
			ExTraceGlobals.ADExpansionTracer.TraceDebug((long)this.GetHashCode(), "No contact chain found");
			return true;
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000700C4 File Offset: 0x0006E2C4
		private bool ProcessChild(ADRawEntry child, ADRecipientExpansion.ExpandableEntry parent, ADRecipientExpansion.HandleRecipientDelegate handleRecipient, ADRecipientExpansion.HandleFailureDelegate handleFailure, Stack<ADRecipientExpansion.ExpandableEntry> expansionStack)
		{
			ExpansionFailure failure = ExpansionFailure.NotMailEnabled;
			bool flag = false;
			ExpansionType expansionType = ADRecipientExpansion.GetExpansionType(child);
			ExTraceGlobals.ADExpansionTracer.TraceDebug<object, string>((long)this.GetHashCode(), "Processing recipient {0} with expansion type {1}", child[ADObjectSchema.Id], ADRecipientExpansion.GetExpansionTypeString(expansionType));
			if (!this.ignoreMailEnabledCase && !this.IsMailEnabled(child))
			{
				failure = ExpansionFailure.NotMailEnabled;
				flag = true;
			}
			if (!flag && !this.IsAuthorized(child))
			{
				failure = ExpansionFailure.NotAuthorized;
				flag = true;
			}
			if (!flag && expansionType != ExpansionType.None && this.IsLoopDetected(child, parent))
			{
				failure = ExpansionFailure.LoopDetected;
				flag = true;
			}
			if (flag)
			{
				return ADRecipientExpansion.InvokeFailureDelegate(handleFailure, failure, child, expansionType, parent) != ExpansionControl.Terminate;
			}
			ExpansionControl expansionControl = ExpansionControl.Continue;
			if (expansionType != ExpansionType.ContactChain)
			{
				expansionControl = ADRecipientExpansion.InvokeRecipientDelegate(handleRecipient, child, expansionType, parent);
			}
			if (expansionControl == ExpansionControl.Terminate)
			{
				return false;
			}
			if (expansionControl != ExpansionControl.Skip && expansionType != ExpansionType.None)
			{
				expansionStack.Push(new ADRecipientExpansion.ExpandableEntry(child, expansionType, parent));
				ExTraceGlobals.ADExpansionTracer.TraceDebug((long)this.GetHashCode(), "Recipient {0} pushed on the expansion stack", new object[]
				{
					child[ADObjectSchema.Id]
				});
			}
			return true;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x000701B0 File Offset: 0x0006E3B0
		private bool IsMailEnabled(ADRawEntry entry)
		{
			SmtpAddress value = (SmtpAddress)entry[ADRecipientSchema.PrimarySmtpAddress];
			if (value != SmtpAddress.Empty && value != SmtpAddress.NullReversePath && value.IsValidAddress)
			{
				return true;
			}
			ExTraceGlobals.ADExpansionTracer.TraceDebug((long)this.GetHashCode(), "Recipient {0} is not mail-enabled", new object[]
			{
				entry[ADObjectSchema.Id]
			});
			return false;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00070220 File Offset: 0x0006E420
		private bool IsAuthorized(ADRawEntry entry)
		{
			if (this.securityContext == null)
			{
				return true;
			}
			RestrictionCheckResult restrictionCheckResult = ADRecipientRestriction.CheckDeliveryRestrictionForAuthenticatedSender(this.securityContext, entry, this.session);
			ExTraceGlobals.ADExpansionTracer.TraceDebug<ADObjectId, RestrictionCheckResult, object>((long)this.GetHashCode(), "Sender {0} permission is {1} for recipient {2}", this.securityContext, restrictionCheckResult, entry[ADObjectSchema.Id]);
			return ADRecipientRestriction.Accepted(restrictionCheckResult);
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00070278 File Offset: 0x0006E478
		private bool IsLoopDetected(ADRawEntry entry, ADRecipientExpansion.ExpandableEntry parent)
		{
			while (parent != null)
			{
				if ((Guid)entry[ADObjectSchema.Guid] == (Guid)parent.Entry[ADObjectSchema.Guid])
				{
					ExTraceGlobals.ADExpansionTracer.TraceDebug((long)this.GetHashCode(), "Loop detected for recipient {0}", new object[]
					{
						entry[ADObjectSchema.Id]
					});
					return true;
				}
				parent = parent.Parent;
			}
			return false;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000702ED File Offset: 0x0006E4ED
		private bool IsOnSecurityList(ICollection<ADObjectId> list)
		{
			return list.Contains(this.securityContext);
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000702FC File Offset: 0x0006E4FC
		private bool IsMemberOf(IEnumerable<ADObjectId> groupIdList)
		{
			foreach (ADObjectId adobjectId in groupIdList)
			{
				if (ADRecipient.IsMemberOf(this.securityContext, adobjectId, false, this.session))
				{
					ExTraceGlobals.ADExpansionTracer.TraceDebug<ADObjectId, ADObjectId>((long)this.GetHashCode(), "Sender {0} is a member of group {1}", this.securityContext, adobjectId);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000B90 RID: 2960
		public const int PageSize = 1000;

		// Token: 0x04000B91 RID: 2961
		private static readonly PropertyDefinition[] requiredProperties = new PropertyDefinition[]
		{
			ADObjectSchema.Guid,
			ADObjectSchema.Id,
			ADRecipientSchema.AcceptMessagesOnlyFrom,
			ADRecipientSchema.AcceptMessagesOnlyFromDLMembers,
			ADRecipientSchema.EmailAddresses,
			ADRecipientSchema.ExternalEmailAddress,
			ADRecipientSchema.ForwardingAddress,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.RejectMessagesFrom,
			ADRecipientSchema.RejectMessagesFromDLMembers,
			IADMailStorageSchema.DeliverToMailboxAndForward
		};

		// Token: 0x04000B92 RID: 2962
		private static readonly ReadOnlyCollection<PropertyDefinition> requiredPropertiesReadOnly = new ReadOnlyCollection<PropertyDefinition>(ADRecipientExpansion.requiredProperties);

		// Token: 0x04000B93 RID: 2963
		private static readonly string[] expansionTypeStrings = new string[]
		{
			ExpansionType.None.ToString(),
			ExpansionType.GroupMembership.ToString(),
			ExpansionType.AlternateRecipientForward.ToString(),
			ExpansionType.AlternateRecipientDeliverAndForward.ToString(),
			ExpansionType.ContactChain.ToString()
		};

		// Token: 0x04000B94 RID: 2964
		private static readonly string[] expansionControlStrings = new string[]
		{
			ExpansionControl.Continue.ToString(),
			ExpansionControl.Skip.ToString(),
			ExpansionControl.Terminate.ToString()
		};

		// Token: 0x04000B95 RID: 2965
		private static readonly string[] expansionFailureStrings = new string[]
		{
			ExpansionFailure.AlternateRecipientNotFound.ToString(),
			ExpansionFailure.LoopDetected.ToString(),
			ExpansionFailure.NoMembers.ToString(),
			ExpansionFailure.NotAuthorized.ToString(),
			ExpansionFailure.NotMailEnabled.ToString()
		};

		// Token: 0x04000B96 RID: 2966
		private PropertyDefinition[] allProperties;

		// Token: 0x04000B97 RID: 2967
		private IRecipientSession session;

		// Token: 0x04000B98 RID: 2968
		private ADObjectId securityContext;

		// Token: 0x04000B99 RID: 2969
		private bool ignoreMailEnabledCase;

		// Token: 0x020001FF RID: 511
		// (Invoke) Token: 0x06001ABB RID: 6843
		public delegate ExpansionControl HandleRecipientDelegate(ADRawEntry recipient, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentExpansionType);

		// Token: 0x02000200 RID: 512
		// (Invoke) Token: 0x06001ABF RID: 6847
		public delegate ExpansionControl HandleFailureDelegate(ExpansionFailure failure, ADRawEntry recipient, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentExpansionType);

		// Token: 0x02000201 RID: 513
		private class ExpandableEntry
		{
			// Token: 0x06001AC2 RID: 6850 RVA: 0x000704E2 File Offset: 0x0006E6E2
			public ExpandableEntry(ADRawEntry entry, ExpansionType expansionType, ADRecipientExpansion.ExpandableEntry parent)
			{
				this.entry = entry;
				this.expansionType = expansionType;
				this.parent = parent;
			}

			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x000704FF File Offset: 0x0006E6FF
			public ADRawEntry Entry
			{
				get
				{
					return this.entry;
				}
			}

			// Token: 0x1700064D RID: 1613
			// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x00070507 File Offset: 0x0006E707
			public ExpansionType ExpansionType
			{
				get
				{
					return this.expansionType;
				}
			}

			// Token: 0x1700064E RID: 1614
			// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x0007050F File Offset: 0x0006E70F
			public ADRecipientExpansion.ExpandableEntry Parent
			{
				get
				{
					return this.parent;
				}
			}

			// Token: 0x04000B9A RID: 2970
			private ADRawEntry entry;

			// Token: 0x04000B9B RID: 2971
			private ExpansionType expansionType;

			// Token: 0x04000B9C RID: 2972
			private ADRecipientExpansion.ExpandableEntry parent;
		}
	}
}
