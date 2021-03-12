using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Office.Server.Directory;
using Microsoft.Office.Server.Directory.Adapter;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x0200007D RID: 125
	internal sealed class NullAadAdaptor : BaseAdaptor, IPrincipalDirectoryAdapter, IDirectoryAdapter
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public override void Initialize(NameValueCollection parameters)
		{
			base.Parameters = parameters;
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection.Add("graphBaseURL", "https://graph.msol-test.com");
			AzureActiveDirectoryAdapter azureActiveDirectoryAdapter = new AzureActiveDirectoryAdapter();
			azureActiveDirectoryAdapter.Initialize(nameValueCollection);
			base.AdapterId = azureActiveDirectoryAdapter.AdapterId;
			base.ServiceName = azureActiveDirectoryAdapter.ServiceName;
			base.PropertyTypes = azureActiveDirectoryAdapter.PropertyTypes;
			base.ResourceTypes = azureActiveDirectoryAdapter.ResourceTypes;
			base.RelationTypes = azureActiveDirectoryAdapter.RelationTypes;
			BaseAdaptor.Tracer.TraceDebug<NullAadAdaptor>((long)this.GetHashCode(), "NullAadAdaptor.Initialize called: schema initialized with: {0}", this);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000FB1A File Offset: 0x0000DD1A
		public override void RemoveDirectoryObject(DirectoryObjectAccessor directoryObjectAccessor)
		{
			BaseAdaptor.Tracer.TraceDebug((long)this.GetHashCode(), "NullAadAdaptor.RemoveDirectoryObject called.");
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000FB94 File Offset: 0x0000DD94
		public override void CommitDirectoryObject(DirectoryObjectAccessor directoryObjectAccessor)
		{
			BaseAdaptor.Tracer.TraceDebug((long)this.GetHashCode(), "NullAadAdaptor.CommitDirectoryObject called.");
			DirectoryObjectState directoryObjectState = directoryObjectAccessor.GetState(base.ServiceName) as DirectoryObjectState;
			ExchangeDirectorySessionContext exchangeDirectorySessionContext = (ExchangeDirectorySessionContext)directoryObjectAccessor.DirectoryObject.DirectorySession.SessionContext;
			IRecipientSession recipientSession = NullAadAdaptor.GetRecipientSession(exchangeDirectorySessionContext);
			IConfigurationSession configurationSession = NullAadAdaptor.GetConfigurationSession(exchangeDirectorySessionContext);
			if (directoryObjectState != null && directoryObjectState.IsNew)
			{
				Guid guid = Guid.NewGuid();
				exchangeDirectorySessionContext.MockEnternalDirectoryObjectId = true;
				directoryObjectAccessor.SetObjectId(guid);
				Property property = directoryObjectAccessor.GetProperty("DisplayName");
				string preferredAlias = (string)property.Value;
				Property property2 = directoryObjectAccessor.GetProperty("Alias");
				string text;
				if (property2 != null && property2.Value != null)
				{
					text = (string)property2.Value;
				}
				else
				{
					text = RecipientTaskHelper.GenerateUniqueAlias(recipientSession, exchangeDirectorySessionContext.AccessingPrincipal.MailboxInfo.OrganizationId, preferredAlias, new Task.TaskVerboseLoggingDelegate(this.WriteVerbose));
					directoryObjectAccessor.SetProperty("Alias", text, false);
				}
				Property property3 = directoryObjectAccessor.GetProperty("PrincipalName");
				string text2;
				if (property3 != null && property3.Value != null)
				{
					text2 = (string)property3.Value;
				}
				else
				{
					text2 = RecipientTaskHelper.GenerateUniqueUserPrincipalName(recipientSession, text, configurationSession.GetDefaultAcceptedDomain().DomainName.Domain, new Task.TaskVerboseLoggingDelegate(this.WriteVerbose));
					directoryObjectAccessor.SetProperty("PrincipalName", text2, false);
				}
				BaseAdaptor.Tracer.TraceDebug<Guid, string, string>((long)this.GetHashCode(), "NullAadAdaptor.CommitDirectoryObject(): id={0}, alias={1}, principalName={2}", guid, text, text2);
			}
			else
			{
				ADUser adUser = recipientSession.FindADUserByExternalDirectoryObjectId(directoryObjectAccessor.DirectoryObject.Id.ToString());
				if (adUser != null)
				{
					IEnumerable<Property> changes = directoryObjectAccessor.GetChanges<Property>();
					NullAadAdaptor.ProcessSetProperty("DisplayName", delegate(string value)
					{
						adUser.DisplayName = value;
					}, changes);
					NullAadAdaptor.ProcessSetProperty("Description", delegate(string value)
					{
						adUser.Description = new string[]
						{
							value
						};
					}, changes);
					NullAadAdaptor.ProcessSetProperty("Alias", delegate(string value)
					{
						adUser.Alias = value;
					}, changes);
					NullAadAdaptor.ProcessSetProperty("Mail", delegate(string value)
					{
						adUser.PrimarySmtpAddress = new SmtpAddress(value);
					}, changes);
					recipientSession.Save(adUser);
				}
			}
			if (directoryObjectState != null)
			{
				directoryObjectState.Version += 1L;
				directoryObjectState.IsCommitted = true;
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000FE06 File Offset: 0x0000E006
		public Guid GetDirectoryObjectId(DirectorySession directorySession, string propertyName, object propertyValue, DirectoryObjectType directoryObjectType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000FE0D File Offset: 0x0000E00D
		public IEnumerable<DirectoryObject> GetDirectoryObjects(DirectoryObjectFactory factory, string propertyName, IEnumerable<object> propertyValues, DirectoryObjectType directoryObjectType, RequestSchema requestSchema)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000FE14 File Offset: 0x0000E014
		private void WriteVerbose(LocalizedString localizedString)
		{
			BaseAdaptor.Tracer.TraceDebug<LocalizedString>((long)this.GetHashCode(), "NullAadAdaptor.WriteVerbose(): {0}", localizedString);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000FE30 File Offset: 0x0000E030
		public override void LoadDirectoryObjectData(DirectoryObjectAccessor directoryObjectAccessor, RequestSchema requestSchema, out IDirectoryObjectState state)
		{
			BaseAdaptor.Tracer.TraceDebug((long)this.GetHashCode(), "NullAadAdaptor.LoadDirectoryObjectData called.");
			ExchangeDirectorySessionContext sessionContext = (ExchangeDirectorySessionContext)directoryObjectAccessor.DirectoryObject.DirectorySession.SessionContext;
			IRecipientSession recipientSession = NullAadAdaptor.GetRecipientSession(sessionContext);
			ADUser aduser = recipientSession.FindADUserByExternalDirectoryObjectId(directoryObjectAccessor.DirectoryObject.Id.ToString());
			if (aduser != null)
			{
				NullAadAdaptor.ProcessGetProperty("DisplayName", aduser.DisplayName, directoryObjectAccessor, requestSchema);
				NullAadAdaptor.ProcessGetProperty("Description", (aduser.Description == null || aduser.Description.Count == 0) ? null : aduser.Description[0], directoryObjectAccessor, requestSchema);
				NullAadAdaptor.ProcessGetProperty("Alias", aduser.Alias, directoryObjectAccessor, requestSchema);
				NullAadAdaptor.ProcessGetProperty("Mail", (aduser.PrimarySmtpAddress == SmtpAddress.Empty) ? null : aduser.PrimarySmtpAddress.ToString(), directoryObjectAccessor, requestSchema);
				NullAadAdaptor.ProcessGetRelation("AllowAccessTo", (aduser.ModernGroupType == ModernGroupObjectType.Public) ? NullAadAdaptor.EveryoneGroupId : Guid.Empty, directoryObjectAccessor, requestSchema);
			}
			state = new DirectoryObjectState
			{
				Version = 1L
			};
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000FF70 File Offset: 0x0000E170
		private static void ProcessGetRelation(string relationName, Guid relationValue, DirectoryObjectAccessor directoryObjectAccessor, RequestSchema requestSchema)
		{
			if (requestSchema.IncludeAllRelations || requestSchema.Relations.Any((RelationRequestSchema x) => x.Name.Equals(relationName, StringComparison.OrdinalIgnoreCase)))
			{
				RelationSetAccessor relationSet = directoryObjectAccessor.GetRelationSet(relationName);
				if (relationSet != null)
				{
					relationSet.AddRelation(relationValue);
				}
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000FFC3 File Offset: 0x0000E1C3
		private static void ProcessGetProperty(string propertyName, string propertyValue, DirectoryObjectAccessor directoryObjectAccessor, RequestSchema requestSchema)
		{
			if (propertyValue != null && (requestSchema.IncludeAllProperties || requestSchema.Properties.Contains(propertyName, StringComparer.OrdinalIgnoreCase)))
			{
				directoryObjectAccessor.SetProperty(propertyName, propertyValue, false);
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000FFF0 File Offset: 0x0000E1F0
		private static void ProcessSetProperty(string propertyName, Action<string> setValue, IEnumerable<Property> propertyChanges)
		{
			if (propertyChanges != null)
			{
				foreach (Property property in propertyChanges)
				{
					if (StringComparer.OrdinalIgnoreCase.Equals(property.GetPropertyType().Name, propertyName))
					{
						setValue((string)property.Value);
						break;
					}
				}
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00010060 File Offset: 0x0000E260
		private static IRecipientSession GetRecipientSession(ExchangeDirectorySessionContext sessionContext)
		{
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.IgnoreInvalid, sessionContext.AccessingPrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), 265, "GetRecipientSession", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\FederatedDirectory\\NullAadAdaptor.cs");
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00010092 File Offset: 0x0000E292
		private static IConfigurationSession GetConfigurationSession(ExchangeDirectorySessionContext sessionContext)
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionContext.AccessingPrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), 273, "GetConfigurationSession", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\FederatedDirectory\\NullAadAdaptor.cs");
		}

		// Token: 0x040005C7 RID: 1479
		private static readonly Guid EveryoneGroupId = new Guid("{C41554C4-1734-4462-9544-5E5542F2EB1C}");
	}
}
