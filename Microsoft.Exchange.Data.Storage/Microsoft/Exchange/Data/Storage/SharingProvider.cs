using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Sharing.ConsumerSharing;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DD5 RID: 3541
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SharingProvider
	{
		// Token: 0x060079C2 RID: 31170 RVA: 0x0021A7B9 File Offset: 0x002189B9
		private SharingProvider(Guid providerGuid, string providerName, string providerUrl, string providerExternalName, SharingProviderHandler providerHandler)
		{
			this.Guid = providerGuid;
			this.Name = providerName;
			this.Url = providerUrl;
			this.ExternalName = providerExternalName;
			this.sharingProviderHandler = providerHandler;
		}

		// Token: 0x1700209E RID: 8350
		// (get) Token: 0x060079C3 RID: 31171 RVA: 0x0021A7E6 File Offset: 0x002189E6
		// (set) Token: 0x060079C4 RID: 31172 RVA: 0x0021A7EE File Offset: 0x002189EE
		internal Guid Guid { get; private set; }

		// Token: 0x1700209F RID: 8351
		// (get) Token: 0x060079C5 RID: 31173 RVA: 0x0021A7F7 File Offset: 0x002189F7
		// (set) Token: 0x060079C6 RID: 31174 RVA: 0x0021A7FF File Offset: 0x002189FF
		internal string Name { get; private set; }

		// Token: 0x170020A0 RID: 8352
		// (get) Token: 0x060079C7 RID: 31175 RVA: 0x0021A808 File Offset: 0x00218A08
		// (set) Token: 0x060079C8 RID: 31176 RVA: 0x0021A810 File Offset: 0x00218A10
		internal string Url { get; private set; }

		// Token: 0x170020A1 RID: 8353
		// (get) Token: 0x060079C9 RID: 31177 RVA: 0x0021A819 File Offset: 0x00218A19
		// (set) Token: 0x060079CA RID: 31178 RVA: 0x0021A821 File Offset: 0x00218A21
		internal string ExternalName { get; private set; }

		// Token: 0x060079CB RID: 31179 RVA: 0x0021A82A File Offset: 0x00218A2A
		public override string ToString()
		{
			return this.ExternalName;
		}

		// Token: 0x060079CC RID: 31180 RVA: 0x0021A834 File Offset: 0x00218A34
		internal static SharingProvider FromExternalName(string externalName)
		{
			if (!string.IsNullOrEmpty(externalName))
			{
				foreach (SharingProvider sharingProvider in SharingProvider.AllSharingProviders)
				{
					if (StringComparer.OrdinalIgnoreCase.Equals(sharingProvider.ExternalName, externalName))
					{
						return sharingProvider;
					}
				}
			}
			return null;
		}

		// Token: 0x060079CD RID: 31181 RVA: 0x0021A87C File Offset: 0x00218A7C
		internal static SharingProvider FromGuid(Guid guid)
		{
			foreach (SharingProvider sharingProvider in SharingProvider.AllSharingProviders)
			{
				if (sharingProvider.Guid == guid)
				{
					return sharingProvider;
				}
			}
			return null;
		}

		// Token: 0x060079CE RID: 31182 RVA: 0x0021A8B8 File Offset: 0x00218AB8
		internal static SharingProvider[] GetCompatibleProviders(Folder folderToShare)
		{
			List<SharingProvider> list = new List<SharingProvider>(SharingProvider.AllSharingProviders.Length);
			if (folderToShare != null)
			{
				foreach (SharingProvider sharingProvider in SharingProvider.AllSharingProviders)
				{
					if (sharingProvider.IsCompatible(folderToShare))
					{
						list.Add(sharingProvider);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060079CF RID: 31183 RVA: 0x0021A904 File Offset: 0x00218B04
		internal static SharingProvider[] GetCompatibleProviders(SharingProvider provider, Folder folderToShare)
		{
			List<SharingProvider> list = new List<SharingProvider>(SharingProvider.AllSharingProviders.Length);
			if (provider != null)
			{
				list.Add(provider);
				if (provider == SharingProvider.SharingProviderInternal)
				{
					if (SharingProvider.SharingProviderExternal.IsCompatible(folderToShare))
					{
						list.Add(SharingProvider.SharingProviderExternal);
					}
					if (SharingProvider.SharingProviderPublish.IsCompatible(folderToShare))
					{
						list.Add(SharingProvider.SharingProviderPublish);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060079D0 RID: 31184 RVA: 0x0021A968 File Offset: 0x00218B68
		internal SharingMessageProvider CreateSharingMessageProvider()
		{
			return new SharingMessageProvider
			{
				Type = this.ExternalName
			};
		}

		// Token: 0x060079D1 RID: 31185 RVA: 0x0021A988 File Offset: 0x00218B88
		internal SharingMessageProvider CreateSharingMessageProvider(SharingContext context)
		{
			Util.ThrowOnNullArgument(context, "context");
			SharingMessageProvider sharingMessageProvider = this.CreateSharingMessageProvider();
			this.sharingProviderHandler.FillSharingMessageProvider(context, sharingMessageProvider);
			return sharingMessageProvider;
		}

		// Token: 0x060079D2 RID: 31186 RVA: 0x0021A9B5 File Offset: 0x00218BB5
		internal void ParseSharingMessageProvider(SharingContext context, SharingMessageProvider sharingMessageProvider)
		{
			Util.ThrowOnNullArgument(context, "context");
			Util.ThrowOnNullArgument(sharingMessageProvider, "sharingMessageProvider");
			this.sharingProviderHandler.ParseSharingMessageProvider(context, sharingMessageProvider);
		}

		// Token: 0x060079D3 RID: 31187 RVA: 0x0021A9DA File Offset: 0x00218BDA
		internal bool IsCompatible(Folder folderToShare)
		{
			return this.sharingProviderHandler.ValidateCompatibility(folderToShare);
		}

		// Token: 0x060079D4 RID: 31188 RVA: 0x0021A9E8 File Offset: 0x00218BE8
		internal CheckRecipientsResults CheckRecipients(ADRecipient mailboxOwner, SharingContext context, string[] recipients)
		{
			Util.ThrowOnNullArgument(context, "context");
			CheckRecipientsResults checkRecipientsResults = this.sharingProviderHandler.CheckRecipients(mailboxOwner, recipients);
			context.AvailableSharingProviders[this] = checkRecipientsResults;
			return checkRecipientsResults;
		}

		// Token: 0x060079D5 RID: 31189 RVA: 0x0021AA1C File Offset: 0x00218C1C
		internal PerformInvitationResults PerformInvitation(MailboxSession mailboxSession, SharingContext context, ValidRecipient[] recipients, IFrontEndLocator frontEndLocator)
		{
			return this.sharingProviderHandler.PerformInvitation(mailboxSession, context, recipients, frontEndLocator);
		}

		// Token: 0x060079D6 RID: 31190 RVA: 0x0021AA2E File Offset: 0x00218C2E
		internal void PerformRevocation(MailboxSession mailboxSession, SharingContext context)
		{
			this.sharingProviderHandler.PerformRevocation(mailboxSession, context);
		}

		// Token: 0x060079D7 RID: 31191 RVA: 0x0021AA3D File Offset: 0x00218C3D
		internal SubscribeResults PerformSubscribe(MailboxSession mailboxSession, SharingContext context)
		{
			return this.sharingProviderHandler.PerformSubscribe(mailboxSession, context);
		}

		// Token: 0x0400540A RID: 21514
		private const string ExchangeBrand = "Microsoft Exchange";

		// Token: 0x0400540B RID: 21515
		private const string ExchangeProviderUrl = "http://www.microsoft.com/exchange/";

		// Token: 0x0400540C RID: 21516
		public static readonly SharingProvider SharingProviderInternal = new SharingProvider(new Guid("{0006F0AE-0000-0000-C000-000000000046}"), "Microsoft Exchange", "http://www.microsoft.com/exchange/", "ms-exchange-internal", new SharingProviderHandlerInternal());

		// Token: 0x0400540D RID: 21517
		public static readonly SharingProvider SharingProviderExternal = new SharingProvider(new Guid("{0006F0C0-0000-0000-C000-000000000046}"), "Microsoft Exchange", "http://www.microsoft.com/exchange/", "ms-exchange-external", new SharingProviderHandlerExternal());

		// Token: 0x0400540E RID: 21518
		public static readonly SharingProvider SharingProviderPublish = new SharingProvider(new Guid("{0006F0AC-0000-0000-C000-000000000046}"), "Microsoft Exchange", "http://www.microsoft.com/exchange/", "ms-exchange-publish", new SharingProviderHandlerPublish());

		// Token: 0x0400540F RID: 21519
		public static readonly SharingProvider SharingProviderPublishReach = new SharingProvider(new Guid("{0006F0AC-0000-0000-C000-000000000046}"), "Microsoft Exchange", "http://www.microsoft.com/exchange/", "ms-exchange-publish", new SharingProviderHandlerPublishReach());

		// Token: 0x04005410 RID: 21520
		public static readonly SharingProvider SharingProviderConsumer = new SharingProvider(new Guid("{45BA1A35-B7D3-48E2-8466-B0E22509629A}"), "Microsoft Exchange", "http://www.microsoft.com/exchange/", "ms-exchange-consumer", new SharingProviderHandlerConsumer());

		// Token: 0x04005411 RID: 21521
		internal static readonly SharingProvider[] AllSharingProviders = new SharingProvider[]
		{
			SharingProvider.SharingProviderConsumer,
			SharingProvider.SharingProviderInternal,
			SharingProvider.SharingProviderExternal,
			SharingProvider.SharingProviderPublish
		};

		// Token: 0x04005412 RID: 21522
		private readonly SharingProviderHandler sharingProviderHandler;
	}
}
