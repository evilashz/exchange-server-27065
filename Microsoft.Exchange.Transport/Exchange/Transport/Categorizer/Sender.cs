using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001FA RID: 506
	internal class Sender
	{
		// Token: 0x0600167A RID: 5754 RVA: 0x0005BDFE File Offset: 0x00059FFE
		public Sender(IReadOnlyMailItem mailItem)
		{
			this.mailItem = mailItem;
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0005BE19 File Offset: 0x0005A019
		public Microsoft.Exchange.Data.Directory.Recipient.RecipientType? RecipientType
		{
			get
			{
				return this.GetProperty<Microsoft.Exchange.Data.Directory.Recipient.RecipientType>("Microsoft.Exchange.Transport.DirectoryData.RecipientType");
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x0005BE26 File Offset: 0x0005A026
		public ADObjectId ObjectId
		{
			get
			{
				return this.GetProperty<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.Sender.Id", null);
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x0005BE34 File Offset: 0x0005A034
		public string DistinguishedName
		{
			get
			{
				return this.GetProperty<string>("Microsoft.Exchange.Transport.DirectoryData.Sender.DistinguishedName", null);
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x0005BE44 File Offset: 0x0005A044
		public Unlimited<ByteQuantifiedSize> MaxSendSize
		{
			get
			{
				ulong bytesValue;
				if (this.mailItem.ExtendedProperties.TryGetValue<ulong>("Microsoft.Exchange.Transport.DirectoryData.Sender.MaxSendSize", out bytesValue))
				{
					ByteQuantifiedSize limitedValue = ByteQuantifiedSize.FromBytes(bytesValue);
					return new Unlimited<ByteQuantifiedSize>(limitedValue);
				}
				return Unlimited<ByteQuantifiedSize>.UnlimitedValue;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x0005BE80 File Offset: 0x0005A080
		public string PrimarySmtpAddress
		{
			get
			{
				return this.mailItem.From.ToString();
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x0005BEA8 File Offset: 0x0005A0A8
		public Unlimited<int> RecipientLimits
		{
			get
			{
				int limitedValue;
				if (this.mailItem.ExtendedProperties.TryGetValue<int>("Microsoft.Exchange.Transport.DirectoryData.Sender.RecipientLimits", out limitedValue))
				{
					return new Unlimited<int>(limitedValue);
				}
				return Unlimited<int>.UnlimitedValue;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x0005BEDA File Offset: 0x0005A0DA
		public ExternalOofOptions UserExternalOofOptions
		{
			get
			{
				return (ExternalOofOptions)this.GetProperty<int>("Microsoft.Exchange.Transport.DirectoryData.Sender.ExternalOofOptions", 1);
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x0005BEE8 File Offset: 0x0005A0E8
		public string ExternalEmailAddress
		{
			get
			{
				return this.GetProperty<string>("Microsoft.Exchange.Transport.DirectoryData.ExternalEmailAddress", null);
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0005BEF8 File Offset: 0x0005A0F8
		public Unlimited<int> EffectiveRecipientLimit
		{
			get
			{
				if (!this.RecipientLimits.IsUnlimited)
				{
					return this.RecipientLimits;
				}
				return this.mailItem.TransportSettings.MaxRecipientEnvelopeLimit;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0005BF2C File Offset: 0x0005A12C
		public bool AllowExternalOofs
		{
			get
			{
				return this.UserExternalOofOptions == ExternalOofOptions.External;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0005BF38 File Offset: 0x0005A138
		public RoutingAddress? EmailAddress
		{
			get
			{
				if (!this.loadedEmailAddress)
				{
					EmailRecipient sender = this.mailItem.Message.Sender;
					if (sender == null)
					{
						ExTraceGlobals.ResolverTracer.TraceDebug((long)this.GetHashCode(), "EmailMessage can't find a (valid) P2 sender address");
					}
					else
					{
						ExTraceGlobals.ResolverTracer.TraceDebug<string>((long)this.GetHashCode(), "P2 sender from EmailMessage is {0}", sender.SmtpAddress);
						RoutingAddress value = new RoutingAddress(sender.SmtpAddress);
						if (!value.IsValid)
						{
							ExTraceGlobals.ResolverTracer.TraceDebug<string>((long)this.GetHashCode(), "P2 sender address \"{0}\" is invalid", sender.SmtpAddress);
						}
						else
						{
							this.emailAddress = new RoutingAddress?(value);
						}
					}
					this.loadedEmailAddress = true;
				}
				return this.emailAddress;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0005BFE8 File Offset: 0x0005A1E8
		public bool InternalOrgSender
		{
			get
			{
				Microsoft.Exchange.Data.Directory.Recipient.RecipientType valueOrDefault = this.RecipientType.GetValueOrDefault();
				Microsoft.Exchange.Data.Directory.Recipient.RecipientType? recipientType;
				if (recipientType != null)
				{
					switch (valueOrDefault)
					{
					case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.Invalid:
					case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.Contact:
					case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailContact:
						return false;
					case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.User:
					case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.UserMailbox:
					case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailUser:
					{
						string externalEmailAddress = this.ExternalEmailAddress;
						return externalEmailAddress == null;
					}
					case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.Group:
					case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailUniversalDistributionGroup:
					case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailUniversalSecurityGroup:
					case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailNonUniversalGroup:
					case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.DynamicDistributionGroup:
					case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.PublicFolder:
					case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.PublicDatabase:
					case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.SystemAttendantMailbox:
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0005C05F File Offset: 0x0005A25F
		internal ProxyAddress P1Address
		{
			get
			{
				return Sender.GetInnermostAddress(this.mailItem.From);
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x0005C074 File Offset: 0x0005A274
		internal ProxyAddress P2Address
		{
			get
			{
				if (this.EmailAddress != null)
				{
					return Sender.GetInnermostAddress(this.EmailAddress.Value);
				}
				return null;
			}
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0005C0A8 File Offset: 0x0005A2A8
		public static void Resolve(ADRawEntry p1Sender, ADRawEntry p2Sender, TransportMailItem mailItem)
		{
			bool flag;
			if (mailItem.ExtendedProperties.TryGetValue<bool>("Microsoft.Exchange.Transport.Sender.Resolved", out flag) && flag)
			{
				return;
			}
			Sender.ResolveReversePath(p1Sender, mailItem);
			Sender.ResolveP2Sender(p2Sender, mailItem);
			mailItem.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.Sender.Resolved", true);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0005C0EC File Offset: 0x0005A2EC
		public RoutingAddress GetPurportedResponsibleAddress()
		{
			return Sender.GetPurportedResponsibleAddress(this.mailItem.RootPart.Headers);
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0005C103 File Offset: 0x0005A303
		internal static RoutingAddress GetPurportedResponsibleAddress(HeaderList headerList)
		{
			return Util.GetPurportedResponsibleAddress(headerList);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x0005C10B File Offset: 0x0005A30B
		internal static ProxyAddress GetInnermostAddress(RoutingAddress outer)
		{
			return Sender.GetInnermostAddress(outer, Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0005C124 File Offset: 0x0005A324
		internal static ProxyAddress GetInnermostAddress(RoutingAddress outer, string firstOrgDefaultDomainName)
		{
			if (outer.Equals(RoutingAddress.NullReversePath))
			{
				ExTraceGlobals.ResolverTracer.TraceDebug(0L, "Null reverse path");
				return null;
			}
			ProxyAddress proxyAddress;
			if (Resolver.TryDeencapsulate(outer, firstOrgDefaultDomainName, out proxyAddress))
			{
				if (proxyAddress.Prefix == ProxyAddressPrefix.Smtp)
				{
					ExTraceGlobals.ResolverTracer.TraceDebug(0L, "address contains SMTP-SMTP encapsulation");
					proxyAddress = null;
				}
			}
			else
			{
				proxyAddress = new SmtpProxyAddress(outer.ToString(), false);
			}
			return proxyAddress;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0005C19C File Offset: 0x0005A39C
		private static void ResolveReversePath(ADRawEntry senderEntry, TransportMailItem mailItem)
		{
			ExTraceGlobals.ResolverTracer.TraceDebug<string>(0L, "resolving P1 reverse path {0}", mailItem.From.ToString());
			if (senderEntry == null)
			{
				ExTraceGlobals.ResolverTracer.TraceDebug(0L, "P1 reverse path not found in the directory");
				return;
			}
			RoutingAddress primarySmtpAddress = Resolver.GetPrimarySmtpAddress(senderEntry);
			if (RoutingAddress.Empty == primarySmtpAddress)
			{
				ExTraceGlobals.ResolverTracer.TraceDebug(0L, "P1 reverse path doesn't have a primary SMTP address");
				return;
			}
			if (!primarySmtpAddress.IsValid)
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<string>(0L, "P1 reverse path address is invalid: \"{0}\"", primarySmtpAddress.ToString());
				return;
			}
			ExTraceGlobals.ResolverTracer.TraceDebug<string>(0L, "P1 reverse path is now {0}", primarySmtpAddress.ToString());
			mailItem.From = primarySmtpAddress;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0005C256 File Offset: 0x0005A456
		private static void ResolveP2Sender(ADRawEntry p2Sender, TransportMailItem mailItem)
		{
			ExTraceGlobals.ResolverTracer.TraceDebug(0L, "resolving P2 sender");
			if (p2Sender == null)
			{
				ExTraceGlobals.ResolverTracer.TraceDebug(0L, "P2 sender not found in the directory");
				return;
			}
			SenderSchema.Set(p2Sender, mailItem);
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0005C285 File Offset: 0x0005A485
		private T GetProperty<T>(string name, T defaultValue)
		{
			return this.mailItem.ExtendedProperties.GetValue<T>(name, defaultValue);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0005C29C File Offset: 0x0005A49C
		private T? GetProperty<T>(string name) where T : struct
		{
			T value;
			if (!this.mailItem.ExtendedProperties.TryGetValue<T>(name, out value))
			{
				return null;
			}
			return new T?(value);
		}

		// Token: 0x04000B51 RID: 2897
		public const string Resolved = "Microsoft.Exchange.Transport.Sender.Resolved";

		// Token: 0x04000B52 RID: 2898
		public static readonly RoutingAddress NoPRA = Util.NoPRA;

		// Token: 0x04000B53 RID: 2899
		private IReadOnlyMailItem mailItem;

		// Token: 0x04000B54 RID: 2900
		private RoutingAddress? emailAddress = null;

		// Token: 0x04000B55 RID: 2901
		private bool loadedEmailAddress;
	}
}
