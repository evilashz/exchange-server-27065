using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000925 RID: 2341
	internal class GetPersonaModernGroupMembership : ServiceCommand<GetPersonaModernGroupMembershipResponse>
	{
		// Token: 0x060043D6 RID: 17366 RVA: 0x000E683B File Offset: 0x000E4A3B
		public GetPersonaModernGroupMembership(CallContext context, GetPersonaModernGroupMembershipRequest request) : base(context)
		{
			this.request = request;
			OwsLogRegistry.Register("GetPersonaModernGroupMembership", typeof(GetPersonaModernGroupMembershipMetadata), new Type[0]);
			request.ValidateRequest();
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x060043D7 RID: 17367 RVA: 0x000E686B File Offset: 0x000E4A6B
		protected IRecipientSession ADSession
		{
			get
			{
				return base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x060043D8 RID: 17368 RVA: 0x000E6880 File Offset: 0x000E4A80
		protected GetPersonaModernGroupMembershipResponse EmptyResponse
		{
			get
			{
				base.CallContext.ProtocolLog.Set(GetPersonaModernGroupMembershipMetadata.GroupCount, 0);
				return new GetPersonaModernGroupMembershipResponse
				{
					Groups = new Persona[0]
				};
			}
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x000E68C0 File Offset: 0x000E4AC0
		protected override GetPersonaModernGroupMembershipResponse InternalExecute()
		{
			ExTraceGlobals.ModernGroupsTracer.TraceDebug<string>((long)this.GetHashCode(), "GetPersonaModernGroupMembership.InternalExecute: Retrieving modern groups for user {0}.", this.request.SmtpAddress);
			ADRecipient adrecipient = this.ADSession.FindByProxyAddress(this.request.ProxyAddress);
			if (adrecipient == null)
			{
				ExTraceGlobals.ModernGroupsTracer.TraceWarning<ProxyAddress>((long)this.GetHashCode(), "GetPersonaModernGroupMembership.InternalExecute: ADRecipient for proxy address {0} was not found.", this.request.ProxyAddress);
				return new GetPersonaModernGroupMembershipResponse();
			}
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.GroupMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, IUnifiedGroupMailboxSchema.UnifiedGroupMembersLink, adrecipient.Id)
			});
			SortBy sortBy = new SortBy(ADRecipientSchema.DisplayName, SortOrder.Ascending);
			List<ADRawEntry> list = this.ADSession.FindRecipient(null, QueryScope.SubTree, filter, sortBy, 0, GetPersonaModernGroupMembership.DefaultGroupProperties).ToList<ADRawEntry>();
			return new GetPersonaModernGroupMembershipResponse
			{
				Groups = list.ConvertAll<Persona>(new Converter<ADRawEntry, Persona>(GetPersonaModernGroupMembership.ConvertGroupMailboxToPersona)).ToArray()
			};
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x000E69C0 File Offset: 0x000E4BC0
		private static Persona ConvertGroupMailboxToPersona(ADRawEntry item)
		{
			string text = item[ADRecipientSchema.DisplayName] as string;
			return new Persona
			{
				DisplayName = text,
				Alias = (item[ADRecipientSchema.Alias] as string),
				PersonaType = PersonType.ModernGroup.ToString(),
				EmailAddress = new EmailAddressWrapper
				{
					Name = (text ?? string.Empty),
					EmailAddress = item[ADRecipientSchema.PrimarySmtpAddress].ToString(),
					RoutingType = "SMTP",
					MailboxType = MailboxHelper.MailboxTypeType.GroupMailbox.ToString()
				}
			};
		}

		// Token: 0x0400278F RID: 10127
		internal static readonly ADPropertyDefinition[] DefaultGroupProperties = new ADPropertyDefinition[]
		{
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.Alias,
			ADRecipientSchema.PrimarySmtpAddress
		};

		// Token: 0x04002790 RID: 10128
		private readonly GetPersonaModernGroupMembershipRequest request;
	}
}
