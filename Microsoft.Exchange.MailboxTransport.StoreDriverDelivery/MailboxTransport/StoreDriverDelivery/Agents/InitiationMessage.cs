using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Storage.Approval;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000067 RID: 103
	internal class InitiationMessage
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x00010DC3 File Offset: 0x0000EFC3
		private InitiationMessage(EmailMessage message)
		{
			this.message = message;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00010DD4 File Offset: 0x0000EFD4
		public IList<RoutingAddress> DecisionMakers
		{
			get
			{
				if (!this.decisionMakersRead)
				{
					string text;
					RoutingAddress[] list;
					if (this.TryGetHeaderValue("X-MS-Exchange-Organization-Approval-Allowed-Decision-Makers", out text) && ApprovalUtils.TryGetDecisionMakers(text, out list))
					{
						this.decisionMakers = new ReadOnlyCollection<RoutingAddress>(list);
					}
					InitiationMessage.diag.TraceDebug((long)this.GetHashCode(), "Missing decision maker or invalid decision makers.");
					this.decisionMakersRead = true;
				}
				return this.decisionMakers;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00010E34 File Offset: 0x0000F034
		public RoutingAddress Requestor
		{
			get
			{
				RoutingAddress routingAddress = RoutingAddress.Empty;
				string address;
				if (!this.TryGetHeaderValue("X-MS-Exchange-Organization-Approval-Requestor", out address))
				{
					return routingAddress;
				}
				routingAddress = (RoutingAddress)address;
				if (!Util.IsValidAddress(routingAddress))
				{
					routingAddress = RoutingAddress.Empty;
				}
				return routingAddress;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00010E6E File Offset: 0x0000F06E
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00010E76 File Offset: 0x0000F076
		public string ApprovalData { get; internal set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00010E7F File Offset: 0x0000F07F
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00010E87 File Offset: 0x0000F087
		public int? MessageItemLocale { get; internal set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00010E90 File Offset: 0x0000F090
		public MimeConstant.ApprovalAllowedAction VotingActions
		{
			get
			{
				string text;
				if (this.TryGetHeaderValue("X-MS-Exchange-Organization-Approval-Allowed-Actions", out text))
				{
					MimeConstant.ApprovalAllowedAction result;
					if (EnumValidator<MimeConstant.ApprovalAllowedAction>.TryParse(text, EnumParseOptions.IgnoreCase, out result))
					{
						return result;
					}
					InitiationMessage.diag.TraceDebug<string>((long)this.GetHashCode(), "Invalid Voting action {0}", text);
				}
				InitiationMessage.diag.TraceDebug((long)this.GetHashCode(), "No Voting action header value");
				return MimeConstant.ApprovalAllowedAction.ApproveReject;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00010EE7 File Offset: 0x0000F0E7
		public string Subject
		{
			get
			{
				return this.message.Subject;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00010EF4 File Offset: 0x0000F0F4
		internal EmailMessage EmailMessage
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00010EFC File Offset: 0x0000F0FC
		internal string ApprovalInitiator
		{
			get
			{
				string result;
				if (this.TryGetHeaderValue("X-MS-Exchange-Organization-Approval-Initiator", out result))
				{
					return result;
				}
				return null;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00010F1C File Offset: 0x0000F11C
		internal bool IsMapiInitiator
		{
			get
			{
				string approvalInitiator = this.ApprovalInitiator;
				return string.IsNullOrEmpty(approvalInitiator) || approvalInitiator.Equals("mapi", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00010F46 File Offset: 0x0000F146
		public static bool TryCreate(EmailMessage message, out InitiationMessage initiationMessage)
		{
			initiationMessage = null;
			if (!InitiationMessage.IsInitiationMessage(message))
			{
				return false;
			}
			initiationMessage = new InitiationMessage(message);
			return true;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00010F60 File Offset: 0x0000F160
		public static bool IsInitiationMessage(EmailMessage message)
		{
			HeaderList headers = message.MimeDocument.RootPart.Headers;
			Header header = headers.FindFirst("X-MS-Exchange-Organization-Approval-Initiator");
			Header header2 = headers.FindFirst("X-MS-Exchange-Organization-Approval-Allowed-Decision-Makers");
			return header != null && header2 != null;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		private bool TryGetHeaderValue(string headerName, out string value)
		{
			HeaderList headers = this.message.MimeDocument.RootPart.Headers;
			TextHeader textHeader = headers.FindFirst(headerName) as TextHeader;
			value = null;
			if (textHeader == null)
			{
				InitiationMessage.diag.TraceDebug<string>((long)this.GetHashCode(), "'{0}' header not found from message.", headerName);
				return false;
			}
			if (!textHeader.TryGetValue(out value) || string.IsNullOrEmpty(value))
			{
				InitiationMessage.diag.TraceDebug<string>((long)this.GetHashCode(), "'{0}' header cannot be read from message.", headerName);
				return false;
			}
			return true;
		}

		// Token: 0x04000207 RID: 519
		public const string MapiApprovalInitiator = "mapi";

		// Token: 0x04000208 RID: 520
		private static readonly Trace diag = ExTraceGlobals.ApprovalAgentTracer;

		// Token: 0x04000209 RID: 521
		private EmailMessage message;

		// Token: 0x0400020A RID: 522
		private bool decisionMakersRead;

		// Token: 0x0400020B RID: 523
		private ReadOnlyCollection<RoutingAddress> decisionMakers;
	}
}
