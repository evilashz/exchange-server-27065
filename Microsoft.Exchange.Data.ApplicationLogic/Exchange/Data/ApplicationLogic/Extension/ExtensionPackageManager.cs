using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x020000FA RID: 250
	public static class ExtensionPackageManager
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x0002920C File Offset: 0x0002740C
		public static string GetFaiName(string extensionId, string version)
		{
			return "ClientExtension" + '.' + ExtensionDataHelper.FormatExtensionId(extensionId).Replace("-", string.Empty);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00029234 File Offset: 0x00027434
		internal static StoreObjectId GetExtensionFolderId(MailboxSession mailboxSession)
		{
			return mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
		}

		// Token: 0x04000529 RID: 1321
		private const string ExtensionFaiPrefix = "ClientExtension";

		// Token: 0x0400052A RID: 1322
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;
	}
}
