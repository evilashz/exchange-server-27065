using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.AddressFinder
{
	// Token: 0x02000010 RID: 16
	internal class MapiAddressFinder : IAddressFinder
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002BC4 File Offset: 0x00000DC4
		IRoutingKey[] IAddressFinder.Find(AddressFinderSource source, IAddressFinderDiagnostics diagnostics)
		{
			AddressFinderHelper.ThrowIfNull(source, diagnostics);
			List<IRoutingKey> list = new List<IRoutingKey>();
			IRoutingKey routingKey = MapiAddressFinder.FindByMailboxId(source.QueryString["mailboxId"], diagnostics);
			if (routingKey != null)
			{
				list.Add(routingKey);
			}
			routingKey = MapiAddressFinder.FindBySmtpAddress(source.QueryString["smtpAddress"], diagnostics);
			if (routingKey != null)
			{
				list.Add(routingKey);
			}
			if (list.Count == 0)
			{
				return AddressFinderHelper.EmptyRoutingKeyArray;
			}
			return list.ToArray();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C34 File Offset: 0x00000E34
		private static IRoutingKey FindBySmtpAddress(string smtpAddress, IAddressFinderDiagnostics diagnostics)
		{
			if (string.IsNullOrEmpty(smtpAddress))
			{
				return null;
			}
			if (!SmtpAddress.IsValidSmtpAddress(smtpAddress))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<string>(0L, "[MapiAddressFinder::FindBySmtpAddress]: Malformed smtpAddress {0}.", smtpAddress);
				return null;
			}
			IRoutingKey routingKey = new SmtpRoutingKey(new SmtpAddress(smtpAddress));
			diagnostics.AddRoutingkey(routingKey, "SmtpAddressInQueryString");
			return routingKey;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C80 File Offset: 0x00000E80
		private static IRoutingKey FindByMailboxId(string mailboxId, IAddressFinderDiagnostics diagnostics)
		{
			if (string.IsNullOrEmpty(mailboxId))
			{
				return null;
			}
			if (!SmtpAddress.IsValidSmtpAddress(mailboxId))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<string>(0L, "[MapiAddressFinder::FindByMailboxId]: Malformed mailboxId {0}.", mailboxId);
				return null;
			}
			Guid guid = Guid.Empty;
			string tenantDomain = string.Empty;
			try
			{
				SmtpAddress smtpAddress = new SmtpAddress(mailboxId);
				guid = new Guid(smtpAddress.Local);
				tenantDomain = smtpAddress.Domain;
			}
			catch (FormatException arg)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<string, FormatException>(0L, "[MapiAddressFinder::FindByMailboxId]: Caught exception: Reason {0}; Exception {1}.", string.Format("Invalid mailboxGuid {0}", guid), arg);
				return null;
			}
			IRoutingKey routingKey = new MailboxGuidRoutingKey(guid, tenantDomain);
			diagnostics.AddRoutingkey(routingKey, "MailboxGuidInQueryString");
			return routingKey;
		}

		// Token: 0x0400001B RID: 27
		private const string UseMailboxOfAuthenticatedUserParameter = "useMailboxOfAuthenticatedUser";

		// Token: 0x0400001C RID: 28
		private const string MailboxIdParameter = "mailboxId";

		// Token: 0x0400001D RID: 29
		private const string SmtpAddressParameter = "smtpAddress";
	}
}
