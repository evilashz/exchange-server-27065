using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.GroupMailbox.Common
{
	// Token: 0x0200005C RID: 92
	internal static class Strings
	{
		// Token: 0x0600030E RID: 782 RVA: 0x00011A58 File Offset: 0x0000FC58
		static Strings()
		{
			Strings.stringIDs.Add(676830401U, "CannotEscalateForNonMember");
			Strings.stringIDs.Add(2693957296U, "MaixmumNumberOfMailboxAssociationsForUserReached");
			Strings.stringIDs.Add(1226541993U, "CannotPinGroupForNonMember");
			Strings.stringIDs.Add(2475272174U, "MaxSubscriptionsForGroupReached");
			Strings.stringIDs.Add(1872174711U, "JoinRequestMessageNoAttachedBodyPrefix");
			Strings.stringIDs.Add(3131285730U, "JoinRequestMessageAttachedBodyPrefix");
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00011B0C File Offset: 0x0000FD0C
		public static LocalizedString ErrorUnableToAddExternalUser(string externalUser)
		{
			return new LocalizedString("ErrorUnableToAddExternalUser", Strings.ResourceManager, new object[]
			{
				externalUser
			});
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00011B34 File Offset: 0x0000FD34
		public static LocalizedString CannotEscalateForNonMember
		{
			get
			{
				return new LocalizedString("CannotEscalateForNonMember", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00011B4C File Offset: 0x0000FD4C
		public static LocalizedString JoinRequestMessageFooterTextWithLink(string link)
		{
			return new LocalizedString("JoinRequestMessageFooterTextWithLink", Strings.ResourceManager, new object[]
			{
				link
			});
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00011B74 File Offset: 0x0000FD74
		public static LocalizedString RpcReplicationCallFailed(int error)
		{
			return new LocalizedString("RpcReplicationCallFailed", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00011BA4 File Offset: 0x0000FDA4
		public static LocalizedString JoinRequestMessageSubject(string user, string group)
		{
			return new LocalizedString("JoinRequestMessageSubject", Strings.ResourceManager, new object[]
			{
				user,
				group
			});
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00011BD0 File Offset: 0x0000FDD0
		public static LocalizedString MaixmumNumberOfMailboxAssociationsForUserReached
		{
			get
			{
				return new LocalizedString("MaixmumNumberOfMailboxAssociationsForUserReached", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00011BE7 File Offset: 0x0000FDE7
		public static LocalizedString CannotPinGroupForNonMember
		{
			get
			{
				return new LocalizedString("CannotPinGroupForNonMember", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00011C00 File Offset: 0x0000FE00
		public static LocalizedString EwsUrlDiscoveryFailed(string user)
		{
			return new LocalizedString("EwsUrlDiscoveryFailed", Strings.ResourceManager, new object[]
			{
				user
			});
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00011C28 File Offset: 0x0000FE28
		public static LocalizedString MaxSubscriptionsForGroupReached
		{
			get
			{
				return new LocalizedString("MaxSubscriptionsForGroupReached", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00011C40 File Offset: 0x0000FE40
		public static LocalizedString WarningUnableToSendWelcomeMessage(string exception)
		{
			return new LocalizedString("WarningUnableToSendWelcomeMessage", Strings.ResourceManager, new object[]
			{
				exception
			});
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00011C68 File Offset: 0x0000FE68
		public static LocalizedString JoinRequestMessageNoAttachedBodyPrefix
		{
			get
			{
				return new LocalizedString("JoinRequestMessageNoAttachedBodyPrefix", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00011C80 File Offset: 0x0000FE80
		public static LocalizedString ErrorUnableToConfigureMailbox(string folder, string groupMailbox)
		{
			return new LocalizedString("ErrorUnableToConfigureMailbox", Strings.ResourceManager, new object[]
			{
				folder,
				groupMailbox
			});
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00011CAC File Offset: 0x0000FEAC
		public static LocalizedString JoinRequestMessageHeading(string user, string group)
		{
			return new LocalizedString("JoinRequestMessageHeading", Strings.ResourceManager, new object[]
			{
				user,
				group
			});
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00011CD8 File Offset: 0x0000FED8
		public static LocalizedString JoinRequestMessageAttachedBodyPrefix
		{
			get
			{
				return new LocalizedString("JoinRequestMessageAttachedBodyPrefix", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00011CEF File Offset: 0x0000FEEF
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040001A2 RID: 418
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(6);

		// Token: 0x040001A3 RID: 419
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Data.GroupMailbox.Common.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200005D RID: 93
		public enum IDs : uint
		{
			// Token: 0x040001A5 RID: 421
			CannotEscalateForNonMember = 676830401U,
			// Token: 0x040001A6 RID: 422
			MaixmumNumberOfMailboxAssociationsForUserReached = 2693957296U,
			// Token: 0x040001A7 RID: 423
			CannotPinGroupForNonMember = 1226541993U,
			// Token: 0x040001A8 RID: 424
			MaxSubscriptionsForGroupReached = 2475272174U,
			// Token: 0x040001A9 RID: 425
			JoinRequestMessageNoAttachedBodyPrefix = 1872174711U,
			// Token: 0x040001AA RID: 426
			JoinRequestMessageAttachedBodyPrefix = 3131285730U
		}

		// Token: 0x0200005E RID: 94
		private enum ParamIDs
		{
			// Token: 0x040001AC RID: 428
			ErrorUnableToAddExternalUser,
			// Token: 0x040001AD RID: 429
			JoinRequestMessageFooterTextWithLink,
			// Token: 0x040001AE RID: 430
			RpcReplicationCallFailed,
			// Token: 0x040001AF RID: 431
			JoinRequestMessageSubject,
			// Token: 0x040001B0 RID: 432
			EwsUrlDiscoveryFailed,
			// Token: 0x040001B1 RID: 433
			WarningUnableToSendWelcomeMessage,
			// Token: 0x040001B2 RID: 434
			ErrorUnableToConfigureMailbox,
			// Token: 0x040001B3 RID: 435
			JoinRequestMessageHeading
		}
	}
}
