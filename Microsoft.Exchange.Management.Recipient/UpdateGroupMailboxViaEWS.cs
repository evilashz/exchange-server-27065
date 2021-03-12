using System;
using System.Linq;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000052 RID: 82
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UpdateGroupMailboxViaEWS : UpdateGroupMailboxBase
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x00016403 File Offset: 0x00014603
		public UpdateGroupMailboxViaEWS(ADUser group, ADUser executingUser, Uri endpointUrl, GroupMailboxConfigurationActionType forceActionMask, ADUser[] addedMembers, ADUser[] removedMembers, int? permissionsVersion) : base(group, executingUser, forceActionMask, permissionsVersion)
		{
			this.endpointUrl = endpointUrl;
			this.addedMembers = addedMembers;
			this.removedMembers = removedMembers;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00016428 File Offset: 0x00014628
		private string ExecutingUserSmtpAddress
		{
			get
			{
				if (this.executingUser == null)
				{
					return null;
				}
				return this.executingUser.PrimarySmtpAddress.ToString();
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00016458 File Offset: 0x00014658
		public override void Execute()
		{
			using (UpdateGroupMailboxEwsBinding updateGroupMailboxEwsBinding = this.CreateUpdateGroupMailboxEwsBinding())
			{
				UpdateGroupMailboxType request = this.CreateUpdateGroupMailboxType();
				UpdateGroupMailboxResponseType response = updateGroupMailboxEwsBinding.ExecuteUpdateGroupMailboxWithRetry(request);
				this.ProcessResponse(response);
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000164C1 File Offset: 0x000146C1
		private static string[] GetUserSmtpAddresses(ADUser[] users)
		{
			if (users == null)
			{
				return null;
			}
			return (from user in users
			select user.PrimarySmtpAddress.ToString()).ToArray<string>();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000164F0 File Offset: 0x000146F0
		private UpdateGroupMailboxEwsBinding CreateUpdateGroupMailboxEwsBinding()
		{
			UpdateGroupMailboxEwsBinding result;
			using (new StopwatchPerformanceTracker("UpdateGroupMailboxViaEWS.CreateUpdateGroupMailboxEwsBinding", GenericCmdletInfoDataLogger.Instance))
			{
				result = new UpdateGroupMailboxEwsBinding(this.group, this.endpointUrl);
			}
			return result;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00016540 File Offset: 0x00014740
		private UpdateGroupMailboxType CreateUpdateGroupMailboxType()
		{
			return new UpdateGroupMailboxType
			{
				GroupSmtpAddress = this.group.PrimarySmtpAddress.ToString(),
				DomainController = this.group.OriginatingServer,
				ExecutingUserSmtpAddress = this.ExecutingUserSmtpAddress,
				ForceConfigurationAction = this.forceActionMask,
				MemberIdentifierType = GroupMemberIdentifierType.SmtpAddress,
				MemberIdentifierTypeSpecified = true,
				AddedMembers = UpdateGroupMailboxViaEWS.GetUserSmtpAddresses(this.addedMembers),
				RemovedMembers = UpdateGroupMailboxViaEWS.GetUserSmtpAddresses(this.removedMembers),
				PermissionsVersionSpecified = (this.permissionsVersion != null),
				PermissionsVersion = ((this.permissionsVersion != null) ? this.permissionsVersion.Value : 0)
			};
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001660C File Offset: 0x0001480C
		private void ProcessResponse(UpdateGroupMailboxResponseType response)
		{
			using (new StopwatchPerformanceTracker("UpdateGroupMailboxViaEWS.ProcessResponse", GenericCmdletInfoDataLogger.Instance))
			{
				if (response == null || response.ResponseMessages == null || response.ResponseMessages.Items == null || response.ResponseMessages.Items.Length == 0)
				{
					UpdateGroupMailboxViaEWS.Tracer.TraceError((long)this.GetHashCode(), "Empty Response");
					base.Error = "Empty Response";
				}
				else
				{
					ResponseMessageType responseMessageType = response.ResponseMessages.Items[0];
					if (responseMessageType.ResponseClass == ResponseClassType.Success)
					{
						UpdateGroupMailboxViaEWS.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "UpdateGroupMailbox succeeded. Group={0}, MessageText={1}", this.group.ExternalDirectoryObjectId, responseMessageType.MessageText);
					}
					else
					{
						UpdateGroupMailboxViaEWS.Tracer.TraceError<ResponseClassType, ResponseCodeType, string>((long)this.GetHashCode(), "UpdateGroupMailbox failed. ResponseClass={0}, ResponseCode={1}, MessageText={2}", responseMessageType.ResponseClass, responseMessageType.ResponseCode, responseMessageType.MessageText);
						base.Error = responseMessageType.MessageText;
						base.ResponseCode = new ResponseCodeType?(responseMessageType.ResponseCode);
					}
				}
			}
		}

		// Token: 0x04000147 RID: 327
		private static readonly Trace Tracer = ExTraceGlobals.CmdletsTracer;

		// Token: 0x04000148 RID: 328
		private readonly Uri endpointUrl;

		// Token: 0x04000149 RID: 329
		private readonly ADUser[] addedMembers;

		// Token: 0x0400014A RID: 330
		private readonly ADUser[] removedMembers;
	}
}
