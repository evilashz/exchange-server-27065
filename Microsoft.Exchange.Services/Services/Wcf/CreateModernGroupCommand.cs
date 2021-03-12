﻿using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.FederatedDirectory;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Office.Server.Directory;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200090C RID: 2316
	internal sealed class CreateModernGroupCommand : ServiceCommand<CreateModernGroupResponse>
	{
		// Token: 0x06004320 RID: 17184 RVA: 0x000E024C File Offset: 0x000DE44C
		public CreateModernGroupCommand(CallContext callContext, CreateModernGroupRequest request) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(request, "CreateModernGroupRequest", "CreateModernGroupCommand::CreateModernGroupCommand");
			this.request = request;
			this.request.Validate();
			OwsLogRegistry.Register("CreateModernGroup", typeof(CreateModernGroupCommandMetadata), new Type[0]);
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x000E029C File Offset: 0x000DE49C
		protected override CreateModernGroupResponse InternalExecute()
		{
			CreateModernGroupResponse result = null;
			Stopwatch stopwatch = new Stopwatch();
			try
			{
				stopwatch.Start();
				result = this.Create();
			}
			finally
			{
				base.CallContext.ProtocolLog.Set(CreateModernGroupCommandMetadata.TotalProcessingTime, stopwatch.Elapsed.TotalSeconds.ToString("n2"));
			}
			return result;
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x000E0304 File Offset: 0x000DE504
		private CreateModernGroupResponse Create()
		{
			ExTraceGlobals.ModernGroupsTracer.TraceDebug<string, SmtpAddress>((long)this.GetHashCode(), "CreateModernGroupCommand.Create: Group: {0}, User: {1}.", this.request.Alias, base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress);
			CreateModernGroupResponse result;
			using (new CorrelationContext())
			{
				IdentityMapping identityMapping = new IdentityMapping(base.CallContext.ADRecipientSessionContext.GetADRecipientSession());
				identityMapping.Prefetch(this.request.AddedMembers);
				identityMapping.Prefetch(this.request.AddedOwners);
				DirectorySession directorySession = FederatedDirectorySessionFactory.Create(base.CallContext.AccessingADUser, base.CallContext.AccessingPrincipal);
				ExchangeDirectorySessionContext exchangeDirectorySessionContext = (ExchangeDirectorySessionContext)directorySession.SessionContext;
				Group group = directorySession.CreateGroup();
				string principalName = new SmtpAddress(this.request.Alias, this.GetDefaultDomain()).ToString();
				group.Alias = this.request.Alias;
				group.DisplayName = this.request.Name;
				group.IsPublic = (this.request.GroupType == ModernGroupObjectType.Public);
				group.PrincipalName = principalName;
				if (!string.IsNullOrEmpty(this.request.Description))
				{
					base.CallContext.ProtocolLog.Set(CreateModernGroupCommandMetadata.DescriptionSpecified, 1);
					group.Description = this.request.Description;
				}
				else
				{
					base.CallContext.ProtocolLog.Set(CreateModernGroupCommandMetadata.DescriptionSpecified, 0);
				}
				identityMapping.AddToRelation(this.request.AddedOwners, group.Owners);
				identityMapping.AddToRelation(this.request.AddedMembers, group.Members);
				base.CallContext.ProtocolLog.Set(CreateModernGroupCommandMetadata.OwnerCount, this.request.AddedOwners.Length);
				base.CallContext.ProtocolLog.Set(CreateModernGroupCommandMetadata.MemberCount, this.request.AddedMembers.Length);
				CreateModernGroupResponse createModernGroupResponse;
				try
				{
					exchangeDirectorySessionContext.CreationDiagnostics.Start();
					group.Commit();
					exchangeDirectorySessionContext.CreationDiagnostics.Stop();
					createModernGroupResponse = new CreateModernGroupResponse(group, null);
				}
				catch (Exception ex)
				{
					if (ex is OutOfMemoryException || ex is StackOverflowException || ex is AccessViolationException || ex is ThreadAbortException || ex is AppDomainUnloadedException)
					{
						throw;
					}
					exchangeDirectorySessionContext.CreationDiagnostics.Stop();
					base.CallContext.ProtocolLog.Set(CreateModernGroupCommandMetadata.Exception, ex);
					base.CallContext.ProtocolLog.Set(CreateModernGroupCommandMetadata.ExceptionType, (ex.InnerException == null) ? ex.GetType() : ex.InnerException.GetType());
					base.CallContext.ProtocolLog.Set(ServiceCommonMetadata.GenericErrors, ex);
					ExTraceGlobals.ModernGroupsTracer.TraceError<SmtpAddress, Exception>((long)this.GetHashCode(), "Exception in CreateModernGroupCommand for User: {0}. Exception:{1}", base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress, ex);
					string text = this.LogExceptionLocationAndGenerateErrorMessage(group, exchangeDirectorySessionContext);
					if (text != null)
					{
						createModernGroupResponse = new CreateModernGroupResponse(text);
					}
					else
					{
						createModernGroupResponse = new CreateModernGroupResponse(group, CoreResources.GetLocalizedString((CoreResources.IDs)3311760175U));
					}
				}
				base.CallContext.ProtocolLog.Set(CreateModernGroupCommandMetadata.CmdletCorrelationId, exchangeDirectorySessionContext.CreationDiagnostics.CmdletLogCorrelationId);
				this.LogExecutionTime(CreateModernGroupCommandMetadata.GroupCreationTime, exchangeDirectorySessionContext.CreationDiagnostics.GroupCreationTime);
				this.LogExecutionTime(CreateModernGroupCommandMetadata.AADIdentityCreationTime, exchangeDirectorySessionContext.CreationDiagnostics.AADIdentityCreationTime);
				this.LogExecutionTime(CreateModernGroupCommandMetadata.MailboxCreationTime, exchangeDirectorySessionContext.CreationDiagnostics.MailboxCreationTime);
				result = createModernGroupResponse;
			}
			return result;
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x000E06D0 File Offset: 0x000DE8D0
		private string LogExceptionLocationAndGenerateErrorMessage(Group group, ExchangeDirectorySessionContext sessionContext)
		{
			if (group.Id == Guid.Empty)
			{
				base.CallContext.ProtocolLog.Set(CreateModernGroupCommandMetadata.ExceptionLocation, "AAD");
				return CoreResources.GetLocalizedString(CoreResources.IDs.AADIdentityCreationFailed);
			}
			if (!sessionContext.CreationDiagnostics.MailboxCreatedSuccessfully)
			{
				base.CallContext.ProtocolLog.Set(CreateModernGroupCommandMetadata.ExceptionLocation, "Mailbox");
				return CoreResources.GetLocalizedString((CoreResources.IDs)2833024077U);
			}
			base.CallContext.ProtocolLog.Set(CreateModernGroupCommandMetadata.ExceptionLocation, "UnknownGroupCreationException");
			return null;
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x000E0774 File Offset: 0x000DE974
		private void LogExecutionTime(CreateModernGroupCommandMetadata type, TimeSpan? timeTook)
		{
			if (timeTook != null)
			{
				base.CallContext.ProtocolLog.Set(type, (long)timeTook.Value.TotalMilliseconds);
			}
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x000E07B8 File Offset: 0x000DE9B8
		private string GetDefaultDomain()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.CallContext.AccessingPrincipal.MailboxInfo.OrganizationId);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, sessionSettings, 232, "GetDefaultDomain", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\jsonservice\\servicecommands\\CreateModernGroupCommand.cs");
			return tenantOrTopologyConfigurationSession.GetDefaultAcceptedDomain().DomainName.Domain;
		}

		// Token: 0x0400271E RID: 10014
		private readonly CreateModernGroupRequest request;
	}
}
