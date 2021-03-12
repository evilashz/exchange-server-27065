using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200022A RID: 554
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class XSOSyncContentConversion
	{
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x000443E1 File Offset: 0x000425E1
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x00044409 File Offset: 0x00042609
		internal static string DefaultDomain
		{
			private get
			{
				if (XSOSyncContentConversion.testDomain != null)
				{
					return XSOSyncContentConversion.testDomain;
				}
				return Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomain.DomainName.Domain;
			}
			set
			{
				XSOSyncContentConversion.testDomain = value;
			}
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00044414 File Offset: 0x00042614
		internal static InboundConversionOptions GetScopedInboundConversionOptions(IRecipientSession recipientSession)
		{
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			return new InboundConversionOptions(recipientSession, XSOSyncContentConversion.DefaultDomain)
			{
				IsSenderTrusted = true,
				ServerSubmittedSecurely = true,
				RecipientCache = null,
				ClearCategories = true,
				Limits = 
				{
					MimeLimits = MimeLimits.Unlimited
				},
				ApplyHeaderFirewall = true
			};
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00044470 File Offset: 0x00042670
		internal static OutboundConversionOptions GetScopedOutboundConversionOptions(IRecipientSession recipientSession)
		{
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			return new OutboundConversionOptions(recipientSession, XSOSyncContentConversion.DefaultDomain)
			{
				IsSenderTrusted = true,
				RecipientCache = null,
				ClearCategories = true,
				Limits = 
				{
					MimeLimits = MimeLimits.Unlimited
				},
				ClearCategories = false,
				AllowPartialStnefConversion = true
			};
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x000444CC File Offset: 0x000426CC
		internal static Stream ConvertItemToMime(Item item, OutboundConversionOptions scopedOutboundConversionOptions)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (scopedOutboundConversionOptions == null)
			{
				throw new ArgumentNullException("scopedOutboundConversionOptions");
			}
			Stream stream = TemporaryStorage.Create();
			bool flag = false;
			try
			{
				ItemConversion.ConvertItemToMime(item, stream, scopedOutboundConversionOptions);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					stream.Dispose();
				}
			}
			stream.Position = 0L;
			return stream;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00044530 File Offset: 0x00042730
		internal static void ConvertAnyMimeToItem(Stream mimeStream, Item item, InboundConversionOptions scopedInboundConversionOptions)
		{
			if (mimeStream == null)
			{
				throw new ArgumentNullException("mimeStream");
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (scopedInboundConversionOptions == null)
			{
				throw new ArgumentNullException("scopedInboundConversionOptions");
			}
			ItemConversion.ConvertAnyMimeToItem(item, mimeStream, scopedInboundConversionOptions);
		}

		// Token: 0x04000A80 RID: 2688
		private static string testDomain;
	}
}
