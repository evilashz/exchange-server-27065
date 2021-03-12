using System;
using System.Management.Automation;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.Extension
{
	// Token: 0x0200000F RID: 15
	internal static class OWAExtensionHelper
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00005954 File Offset: 0x00003B54
		public static void ProcessRecord(Action action, Task.TaskErrorLoggingDelegate handleError, object identity)
		{
			try
			{
				action();
			}
			catch (OwaExtensionOperationException exception)
			{
				handleError(exception, ErrorCategory.InvalidOperation, identity);
			}
			catch (StorageTransientException exception2)
			{
				handleError(exception2, ErrorCategory.WriteError, null);
			}
			catch (StoragePermanentException exception3)
			{
				handleError(exception3, ErrorCategory.WriteError, null);
			}
			catch (XmlException exception4)
			{
				handleError(exception4, ErrorCategory.InvalidData, null);
			}
			catch (XPathException exception5)
			{
				handleError(exception5, ErrorCategory.InvalidData, null);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000059EC File Offset: 0x00003BEC
		internal static AppId CreateOWAExtensionId(Task task, ADObjectId mailboxOwnerId, string displayName, string extensionId)
		{
			AppId result;
			try
			{
				result = new AppId(mailboxOwnerId, displayName, extensionId);
			}
			catch (ArgumentException exception)
			{
				task.WriteError(exception, ErrorCategory.InvalidArgument, null);
				result = null;
			}
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005A24 File Offset: 0x00003C24
		internal static void CleanupOWAExtensionDataProvider(IConfigDataProvider provider)
		{
			XsoMailboxDataProviderBase xsoMailboxDataProviderBase = provider as XsoMailboxDataProviderBase;
			if (xsoMailboxDataProviderBase != null)
			{
				xsoMailboxDataProviderBase.Dispose();
				return;
			}
			OWAAppDataProviderForNonMailboxUser owaappDataProviderForNonMailboxUser = provider as OWAAppDataProviderForNonMailboxUser;
			if (owaappDataProviderForNonMailboxUser != null)
			{
				owaappDataProviderForNonMailboxUser.Dispose();
			}
		}
	}
}
