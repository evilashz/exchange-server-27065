using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Office.CompliancePolicy;
using Microsoft.Office.CompliancePolicy.ComplianceData;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.ApplicationLogic.Compliance
{
	// Token: 0x020000C9 RID: 201
	internal class ExMailboxComplianceItemContainer : ExComplianceItemContainer
	{
		// Token: 0x0600087B RID: 2171 RVA: 0x00022119 File Offset: 0x00020319
		internal ExMailboxComplianceItemContainer(MailboxSession session)
		{
			this.mailboxSession = session;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00022128 File Offset: 0x00020328
		internal ExMailboxComplianceItemContainer(IRecipientSession recipientSession, string externalDirectoryObjectId)
		{
			this.recipientSession = recipientSession;
			this.externalDirectoryObjectId = externalDirectoryObjectId;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0002213E File Offset: 0x0002033E
		public override bool HasItems
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00022141 File Offset: 0x00020341
		public override string Id
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00022148 File Offset: 0x00020348
		public override List<ComplianceItemContainer> Ancestors
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x0002214B File Offset: 0x0002034B
		public override bool SupportsAssociation
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00022152 File Offset: 0x00020352
		public override bool SupportsBinding
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00022155 File Offset: 0x00020355
		public override string Template
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0002215C File Offset: 0x0002035C
		internal string ExternalDirectoryObjectId
		{
			get
			{
				if (string.IsNullOrEmpty(this.externalDirectoryObjectId))
				{
					this.PopulateExternalDirectoryObjectIdAndRecipientSessionFromMailboxSession();
				}
				return this.externalDirectoryObjectId;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00022177 File Offset: 0x00020377
		internal IRecipientSession RecipientSession
		{
			get
			{
				if (this.recipientSession == null)
				{
					this.PopulateExternalDirectoryObjectIdAndRecipientSessionFromMailboxSession();
				}
				return this.recipientSession;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00022190 File Offset: 0x00020390
		internal override MailboxSession Session
		{
			get
			{
				if (this.mailboxSession == null)
				{
					if (this.recipientSession == null || string.IsNullOrEmpty(this.externalDirectoryObjectId))
					{
						throw new ArgumentException("Both mailboxSession and recipientSession are null");
					}
					ADUser adUser = ExMailboxComplianceItemContainer.GetAdUser(this.RecipientSession, this.ExternalDirectoryObjectId, true);
					ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(adUser.OrganizationId.ToADSessionSettings(), adUser, RemotingOptions.LocalConnectionsOnly);
					this.mailboxSession = MailboxSession.OpenAsSystemService(mailboxOwner, CultureInfo.CurrentCulture, "Client=UnifiedPolicy");
				}
				return this.mailboxSession;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x00022207 File Offset: 0x00020407
		internal override ComplianceItemPagedReader ComplianceItemPagedReader
		{
			get
			{
				return new ExMailboxSearchComplianceItemPagedReader(this);
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0002220F File Offset: 0x0002040F
		public override bool SupportsPolicy(PolicyScenario scenario)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00022218 File Offset: 0x00020418
		public override void ForEachChildContainer(Action<ComplianceItemContainer> containerHandler, Func<ComplianceItemContainer, Exception, bool> exHandler)
		{
			using (Folder folder = Folder.Bind(this.Session, DefaultFolderType.Root, ExMailboxComplianceItemContainer.FolderDataColumns))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, ExMailboxComplianceItemContainer.FolderDataColumns))
				{
					using (FolderEnumerator folderEnumerator = new FolderEnumerator(queryResult, folder, folder.GetProperties(ExMailboxComplianceItemContainer.FolderDataColumns)))
					{
						while (folderEnumerator != null && folderEnumerator.MoveNext())
						{
							for (int i = 0; i < folderEnumerator.Current.Count; i++)
							{
								VersionedId versionedId = folderEnumerator.Current[i][0] as VersionedId;
								if (versionedId != null)
								{
									using (Folder folder2 = Folder.Bind(this.Session, versionedId.ObjectId))
									{
										ExFolderComplianceItemContainer exFolderComplianceItemContainer = new ExFolderComplianceItemContainer(this.Session, this, folder2);
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

		// Token: 0x06000889 RID: 2185 RVA: 0x00022348 File Offset: 0x00020548
		public override void UpdatePolicy(Dictionary<PolicyScenario, List<PolicyRuleConfig>> rules)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0002234F File Offset: 0x0002054F
		public override void AddPolicy(PolicyDefinitionConfig definition, PolicyRuleConfig rule)
		{
			if (definition.Scenario == PolicyScenario.Hold)
			{
				this.UpdateHold(definition.Identity, true);
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00022367 File Offset: 0x00020567
		public override void RemovePolicy(Guid id, PolicyScenario scenario)
		{
			if (scenario == PolicyScenario.Hold)
			{
				this.UpdateHold(id, false);
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00022378 File Offset: 0x00020578
		public override bool HasPolicy(Guid id, PolicyScenario scenario)
		{
			if (scenario == PolicyScenario.Hold)
			{
				try
				{
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.PartiallyConsistent, this.RecipientSession.SessionSettings, 327, "HasPolicy", "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\Compliance\\ExMailboxComplianceItemContainer.cs");
					ADRecipient adUser = ExMailboxComplianceItemContainer.GetAdUser(tenantOrRootOrgRecipientSession, this.ExternalDirectoryObjectId, true);
					string holdId = ExMailboxComplianceItemContainer.GetHoldId(id);
					return adUser.InPlaceHolds.Contains(holdId);
				}
				catch (Exception arg)
				{
					ExTraceGlobals.StorageTracer.TraceError<string, Exception>(0L, "Failed to find out if mailbox '{0}' has hold. Exception: {1}", this.ExternalDirectoryObjectId, arg);
					throw;
				}
			}
			throw new NotImplementedException("Scenario is not supported: " + scenario.ToString());
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00022420 File Offset: 0x00020620
		internal static string GetHoldId(Guid id)
		{
			return "UniH" + id.ToString();
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0002243C File Offset: 0x0002063C
		private static ADUser GetAdUser(IRecipientSession recipientSession, string scope, bool throwIfNotFound)
		{
			LegacyDN legacyDN;
			ADUser aduser;
			if (LegacyDN.TryParse(scope, out legacyDN))
			{
				aduser = (recipientSession.FindByLegacyExchangeDN(scope) as ADUser);
			}
			else
			{
				aduser = recipientSession.FindADUserByExternalDirectoryObjectId(scope);
			}
			if (aduser == null && throwIfNotFound)
			{
				throw new ComplianceTaskPermanentException("Recipient not found: " + scope, UnifiedPolicyErrorCode.FailedToOpenContainer);
			}
			return aduser;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00022489 File Offset: 0x00020689
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0002248C File Offset: 0x0002068C
		private void PopulateExternalDirectoryObjectIdAndRecipientSessionFromMailboxSession()
		{
			if (this.mailboxSession == null)
			{
				throw new ArgumentException("Both recipientSession and mailboxSession are null");
			}
			this.recipientSession = this.mailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid);
			this.externalDirectoryObjectId = this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000224E8 File Offset: 0x000206E8
		private void UpdateHold(Guid id, bool add)
		{
			try
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.PartiallyConsistent, this.RecipientSession.SessionSettings, 418, "UpdateHold", "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\Compliance\\ExMailboxComplianceItemContainer.cs");
				ADRecipient adUser = ExMailboxComplianceItemContainer.GetAdUser(tenantOrRootOrgRecipientSession, this.ExternalDirectoryObjectId, add);
				string holdId = ExMailboxComplianceItemContainer.GetHoldId(id);
				if (add)
				{
					MailboxDiscoverySearch.AddInPlaceHold(adUser, holdId, tenantOrRootOrgRecipientSession);
				}
				else
				{
					MailboxDiscoverySearch.RemoveInPlaceHold(adUser, holdId, tenantOrRootOrgRecipientSession);
				}
			}
			catch (ComplianceTaskPermanentException)
			{
				throw;
			}
			catch (Exception ex)
			{
				ExTraceGlobals.StorageTracer.TraceError<string, Exception, string>(0L, "Failed to {2} hold on mailbox '{0}'. Exception: {1}", this.ExternalDirectoryObjectId, ex, add ? "place" : "remove");
				throw new ComplianceTaskPermanentException(string.Format("Failed to {0} hold on mailbox '{1}'", add ? "place" : "remove", this.ExternalDirectoryObjectId), ex, UnifiedPolicyErrorCode.Unknown);
			}
		}

		// Token: 0x040003C2 RID: 962
		internal const int FolderVersionedId = 0;

		// Token: 0x040003C3 RID: 963
		internal const string UnifiedHoldIdPrefix = "UniH";

		// Token: 0x040003C4 RID: 964
		internal static readonly PropertyDefinition[] FolderDataColumns = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.ParentItemId,
			StoreObjectSchema.DisplayName
		};

		// Token: 0x040003C5 RID: 965
		private string externalDirectoryObjectId;

		// Token: 0x040003C6 RID: 966
		private IRecipientSession recipientSession;

		// Token: 0x040003C7 RID: 967
		private MailboxSession mailboxSession;
	}
}
