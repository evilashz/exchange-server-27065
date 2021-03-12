using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.FederatedDirectory;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Office.Server.Directory;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000933 RID: 2355
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RemoveModernGroupCommand : ServiceCommand<RemoveModernGroupResponse>
	{
		// Token: 0x06004451 RID: 17489 RVA: 0x000E9F74 File Offset: 0x000E8174
		public RemoveModernGroupCommand(CallContext callContext, RemoveModernGroupRequest request) : base(callContext)
		{
			this.request = request;
			this.request.ValidateRequest();
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06004452 RID: 17490 RVA: 0x000E9F8F File Offset: 0x000E818F
		private GroupMailboxLocator GroupMailboxLocator
		{
			get
			{
				if (this.groupMailboxLocator == null)
				{
					this.groupMailboxLocator = GroupMailboxLocator.Instantiate(this.ADSession, new SmtpProxyAddress(this.request.SmtpAddress, true));
				}
				return this.groupMailboxLocator;
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06004453 RID: 17491 RVA: 0x000E9FC1 File Offset: 0x000E81C1
		private IRecipientSession ADSession
		{
			get
			{
				return base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			}
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x000E9FD4 File Offset: 0x000E81D4
		protected override RemoveModernGroupResponse InternalExecute()
		{
			if (base.CallContext.FeaturesManager != null && base.CallContext.FeaturesManager.IsFeatureSupported("ModernGroupsNewArchitecture"))
			{
				DeleteUnifiedGroupTask deleteUnifiedGroupTask = new DeleteUnifiedGroupTask(base.CallContext.AccessingADUser, base.CallContext.AccessingPrincipal, base.CallContext.ADRecipientSessionContext.GetADRecipientSession());
				deleteUnifiedGroupTask.SmtpAddress = this.request.SmtpAddress;
				if (!deleteUnifiedGroupTask.Run())
				{
					ExTraceGlobals.ModernGroupsTracer.TraceError<UnifiedGroupsTask.UnifiedGroupsAction, Exception>((long)this.GetHashCode(), "RemoveModernGroupCommand.InternalExecute: DeleteUnifiedGroupTask.Run failed. ErrorAction: {0}, ErrorException: {1}", deleteUnifiedGroupTask.ErrorAction, deleteUnifiedGroupTask.ErrorException);
					if (deleteUnifiedGroupTask.ErrorAction == UnifiedGroupsTask.UnifiedGroupsAction.AADDelete)
					{
						throw new InternalServerErrorException(deleteUnifiedGroupTask.ErrorException);
					}
					if (deleteUnifiedGroupTask.ErrorAction == UnifiedGroupsTask.UnifiedGroupsAction.ExchangeDelete)
					{
						base.CallContext.ProtocolLog.Set(ServiceCommonMetadata.GenericErrors, deleteUnifiedGroupTask.ErrorException);
						return new RemoveModernGroupResponse
						{
							ErrorState = UnifiedGroupResponseErrorState.FailedMailbox,
							Error = deleteUnifiedGroupTask.ErrorException.ToString()
						};
					}
				}
			}
			else
			{
				if (!this.IsCurrentUserGroupOwner())
				{
					ExTraceGlobals.ModernGroupsTracer.TraceError<SmtpAddress, string>((long)this.GetHashCode(), "RemoveModernGroupCommand.InternalExecute: user {0} is not an owner of group {1}.", base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress, this.request.SmtpAddress);
					throw new AccessDeniedException(LocalizedString.Empty);
				}
				using (new CorrelationContext())
				{
					IdentityMapping identityMapping = new IdentityMapping(this.ADSession);
					identityMapping.Prefetch(new string[]
					{
						this.request.SmtpAddress
					});
					DirectorySession directorySession = FederatedDirectorySessionFactory.Create(base.CallContext.AccessingADUser, base.CallContext.AccessingPrincipal);
					Guid identityFromSmtpAddress = identityMapping.GetIdentityFromSmtpAddress(this.request.SmtpAddress);
					if (identityFromSmtpAddress == Guid.Empty)
					{
						ExTraceGlobals.ModernGroupsTracer.TraceError<string>((long)this.GetHashCode(), "RemoveModernGroupCommand.InternalExecute: no group found with SMTP address: {0}.", this.request.SmtpAddress);
						throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
					}
					directorySession.RemoveGroup(identityFromSmtpAddress);
				}
			}
			return new RemoveModernGroupResponse();
		}

		// Token: 0x06004455 RID: 17493 RVA: 0x000EA1E4 File Offset: 0x000E83E4
		private bool IsCurrentUserGroupOwner()
		{
			ADUser aduser = this.GroupMailboxLocator.FindAdUser();
			int num = UserMailboxBuilder.AllADProperties.Length + 1;
			PropertyDefinition[] array = new PropertyDefinition[num];
			UserMailboxBuilder.AllADProperties.CopyTo(array, 0);
			array[num - 1] = ADRecipientSchema.ExternalDirectoryObjectId;
			Result<ADRawEntry>[] array2 = this.ADSession.FindByADObjectIds(aduser.Owners.ToArray(), array);
			foreach (Result<ADRawEntry> result in array2)
			{
				if (result.Error != null || result.Data == null)
				{
					ExTraceGlobals.ModernGroupsTracer.TraceError<string>(0L, "GetModernGroup::GetOwnersFromAD. Unable to find an owner in AD: {0}", (result.Error != null) ? result.Error.ToString() : "Result.Data is null");
				}
				else if (base.CallContext.AccessingADUser.ExternalDirectoryObjectId.Equals(result.Data[ADRecipientSchema.ExternalDirectoryObjectId]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040027CC RID: 10188
		private readonly RemoveModernGroupRequest request;

		// Token: 0x040027CD RID: 10189
		private GroupMailboxLocator groupMailboxLocator;
	}
}
