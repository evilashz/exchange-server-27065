using System;
using System.Diagnostics;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.FederatedDirectory;
using Microsoft.Exchange.Net.AAD;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000942 RID: 2370
	internal class ValidateModernGroupAliasCommand : ServiceCommand<ValidateModernGroupAliasResponse>
	{
		// Token: 0x0600448F RID: 17551 RVA: 0x000EC5C8 File Offset: 0x000EA7C8
		public ValidateModernGroupAliasCommand(CallContext callContext, ValidateModernGroupAliasRequest request) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(request, "ValidateModernGroupAliasRequest", "ValidateModernGroupAliasCommand::ValidateModernGroupAliasCommand");
			this.request = request;
			OwsLogRegistry.Register("ValidateModernGroupAlias", typeof(ValidateModernGroupAliasMetadata), new Type[0]);
			WarmupGroupManagementDependency.WarmUpAsyncIfRequired(base.CallContext.AccessingPrincipal);
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x000EC620 File Offset: 0x000EA820
		protected override ValidateModernGroupAliasResponse InternalExecute()
		{
			IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			if (!ValidateModernGroupInputHelper.IsSmtpAddressUnique(adrecipientSession, this.request.Alias, this.request.Domain))
			{
				return new ValidateModernGroupAliasResponse(this.request.Alias, false);
			}
			if (!ValidateModernGroupInputHelper.IsAliasValid(adrecipientSession, base.CallContext.ADRecipientSessionContext.OrganizationId, this.request.Alias, new Task.TaskVerboseLoggingDelegate(this.LogHandler), new Task.ErrorLoggerDelegate(this.WriteError), ExchangeErrorCategory.Client))
			{
				return new ValidateModernGroupAliasResponse(this.request.Alias, false);
			}
			if (!ValidateModernGroupInputHelper.IsNameUnique(adrecipientSession, base.CallContext.ADRecipientSessionContext.OrganizationId, this.request.Alias, new Task.TaskVerboseLoggingDelegate(this.LogHandler), new Task.ErrorLoggerDelegate(this.WriteError), ExchangeErrorCategory.Client))
			{
				return new ValidateModernGroupAliasResponse(this.request.Alias, false);
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			AADClient aadclient = AADClientFactory.Create(base.CallContext.ADRecipientSessionContext.OrganizationId, GraphProxyVersions.Version14);
			if (aadclient != null)
			{
				try
				{
					bool flag = aadclient.IsAliasUnique(this.request.Alias);
					base.CallContext.ProtocolLog.Set(ValidateModernGroupAliasMetadata.AADQueryTime, stopwatch.Elapsed);
					if (!flag)
					{
						return new ValidateModernGroupAliasResponse(this.request.Alias, false);
					}
				}
				catch (AADException ex)
				{
					base.CallContext.ProtocolLog.Set(ValidateModernGroupAliasMetadata.AADQueryTime, stopwatch.Elapsed);
					ExTraceGlobals.ModernGroupsTracer.TraceError<SmtpAddress, AADException>((long)this.GetHashCode(), "ValidateModernGroupAliasCommand: User: {0}. Exception: {1}. AADClient.IsAliasUnique failed", base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress, ex);
					base.CallContext.ProtocolLog.Set(ValidateModernGroupAliasMetadata.Exception, ex);
				}
			}
			return new ValidateModernGroupAliasResponse(this.request.Alias, true);
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x000EC818 File Offset: 0x000EAA18
		private void LogHandler(LocalizedString message)
		{
			ExTraceGlobals.ModernGroupsTracer.TraceDebug<SmtpAddress, LocalizedString>((long)this.GetHashCode(), "ValidateModernGroupAliasCommand: User: {0}. Message: {1}", base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress, message);
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x000EC848 File Offset: 0x000EAA48
		private void WriteError(LocalizedException exception, ExchangeErrorCategory category, object target)
		{
			ExTraceGlobals.ModernGroupsTracer.TraceError((long)this.GetHashCode(), "ValidateModernGroupAliasCommand: User: {0}. Exception: {1}. Category: {2}. Target: {3}", new object[]
			{
				base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress,
				exception,
				category,
				target
			});
			base.CallContext.ProtocolLog.Set(ValidateModernGroupAliasMetadata.Exception, exception);
		}

		// Token: 0x040027F6 RID: 10230
		private readonly ValidateModernGroupAliasRequest request;
	}
}
