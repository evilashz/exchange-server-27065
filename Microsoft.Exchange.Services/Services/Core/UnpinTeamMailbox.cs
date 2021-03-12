using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000390 RID: 912
	internal sealed class UnpinTeamMailbox : SingleStepServiceCommand<UnpinTeamMailboxRequest, ServiceResultNone>
	{
		// Token: 0x06001985 RID: 6533 RVA: 0x00091414 File Offset: 0x0008F614
		public UnpinTeamMailbox(CallContext callContext, UnpinTeamMailboxRequest request) : base(callContext, request)
		{
			this.request = request;
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly((base.CallContext.AccessingPrincipal == null) ? OrganizationId.ForestWideOrgId : base.CallContext.AccessingPrincipal.MailboxInfo.OrganizationId);
			this.readWriteSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, null, CultureInfo.InvariantCulture.LCID, false, ConsistencyMode.IgnoreInvalid, null, sessionSettings, 55, ".ctor", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\UnpinTeamMailbox.cs");
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0009148C File Offset: 0x0008F68C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new UnpinTeamMailboxResponseMessage(base.Result.Code, base.Result.Error);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x000914B8 File Offset: 0x0008F6B8
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			ServiceResult<ServiceResultNone> result = new ServiceResult<ServiceResultNone>(new ServiceResultNone());
			ADUser aduser;
			Exception ex;
			if (!this.TryResolveUser(this.request.EmailAddress, out aduser, out ex) || (!TeamMailbox.IsLocalTeamMailbox(aduser) && !TeamMailbox.IsRemoteTeamMailbox(aduser)))
			{
				result = new ServiceResult<ServiceResultNone>(new ServiceError((ex == null) ? string.Empty : ex.Message, ResponseCodeType.ErrorTeamMailboxNotFound, 0, ExchangeVersion.Exchange2012));
			}
			else
			{
				Exception ex2 = null;
				ADUser aduser2 = (ADUser)this.readWriteSession.FindByProxyAddress(ProxyAddress.Parse(base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString()));
				if (aduser2 == null)
				{
					ex2 = new Exception("executingUser cannot be found or resolved!");
				}
				else
				{
					try
					{
						bool flag = false;
						if (aduser2.TeamMailboxShowInClientList.Contains(aduser.Id))
						{
							aduser2.TeamMailboxShowInClientList.Remove(aduser.Id);
							flag = true;
						}
						for (int i = 0; i < aduser2.TeamMailboxShowInClientList.Count; i++)
						{
							ADObjectId adobjectId = aduser2.TeamMailboxShowInClientList[i];
							ADUser aduser3 = this.readWriteSession.FindADUserByObjectId(adobjectId);
							if (aduser3 == null || !TeamMailbox.FromDataObject(aduser3).Active)
							{
								aduser2.TeamMailboxShowInClientList.Remove(adobjectId);
								i--;
								flag = true;
							}
						}
						if (flag)
						{
							this.readWriteSession.Save(aduser2);
						}
					}
					catch (TransientException ex3)
					{
						ex2 = ex3;
					}
					catch (DataSourceOperationException ex4)
					{
						ex2 = ex4;
					}
				}
				if (ex2 != null)
				{
					result = new ServiceResult<ServiceResultNone>(new ServiceError(ex2.Message, ResponseCodeType.ErrorTeamMailboxErrorUnknown, 0, ExchangeVersion.Exchange2012));
				}
			}
			return result;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00091660 File Offset: 0x0008F860
		private bool TryResolveUser(EmailAddressWrapper emailAddress, out ADUser user, out Exception ex)
		{
			ex = null;
			user = null;
			if (emailAddress == null)
			{
				ex = new ArgumentNullException("emailAddress");
			}
			else if (string.Equals(emailAddress.RoutingType, "EX", StringComparison.OrdinalIgnoreCase))
			{
				user = (ADUser)this.readWriteSession.FindByLegacyExchangeDN(emailAddress.EmailAddress);
			}
			else if (SmtpAddress.IsValidSmtpAddress(emailAddress.EmailAddress))
			{
				user = (ADUser)this.readWriteSession.FindByProxyAddress(ProxyAddress.Parse(emailAddress.EmailAddress));
			}
			else
			{
				ex = new ArgumentException("Cannot get internal address for caller; identity: " + emailAddress.EmailAddress);
			}
			return user != null;
		}

		// Token: 0x04001121 RID: 4385
		private readonly UnpinTeamMailboxRequest request;

		// Token: 0x04001122 RID: 4386
		private readonly IRecipientSession readWriteSession;
	}
}
