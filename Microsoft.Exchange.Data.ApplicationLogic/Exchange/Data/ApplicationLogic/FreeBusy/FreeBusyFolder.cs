using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.FreeBusy
{
	// Token: 0x02000135 RID: 309
	internal static class FreeBusyFolder
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x000342AC File Offset: 0x000324AC
		public static Item CreateFreeBusyItem(PublicFolderSession session, StoreObjectId freeBusyFolderId, string legacyDN)
		{
			return FreeBusyFolder.RetryOnStorageTransientException<Item>(() => FreeBusyFolder.CreateFreeBusyItemInternal(session, freeBusyFolderId, legacyDN));
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00034308 File Offset: 0x00032508
		public static StoreObjectId GetFreeBusyFolderId(PublicFolderSession session, string legacyDN, FreeBusyFolderDisposition disposition)
		{
			return FreeBusyFolder.RetryOnStorageTransientException<StoreObjectId>(() => FreeBusyFolder.GetFreeBusyFolderIdInternal(session, legacyDN, disposition));
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00034344 File Offset: 0x00032544
		internal static T RetryOnStorageTransientException<T>(Func<T> func)
		{
			for (int i = 0; i < 3; i++)
			{
				try
				{
					return func();
				}
				catch (StorageTransientException arg)
				{
					FreeBusyFolder.Tracer.TraceError<StorageTransientException>(0L, "Failed due transient exception, retrying. Exception: {0}.", arg);
				}
			}
			return func();
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00034394 File Offset: 0x00032594
		internal static StoreObjectId GetFreeBusyRootFolderId(PublicFolderSession session)
		{
			StoreObjectId result;
			try
			{
				StoreObjectId storeObjectId = null;
				using (Folder folder = Folder.Bind(session, session.GetNonIpmSubtreeFolderId()))
				{
					storeObjectId = QueryChildFolderByName.Query(folder, "SCHEDULE+ FREE BUSY");
				}
				if (storeObjectId == null)
				{
					FreeBusyFolder.Tracer.TraceError<string>(0L, "Unable to find folder '{0}' in public folders", "SCHEDULE+ FREE BUSY");
				}
				result = storeObjectId;
			}
			catch (StoragePermanentException arg)
			{
				FreeBusyFolder.Tracer.TraceError<string, StoragePermanentException>(0L, "Failed to get folder '{0}' due exception: {1}", "SCHEDULE+ FREE BUSY", arg);
				result = null;
			}
			return result;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00034420 File Offset: 0x00032620
		internal static string GetFreeBusyItemSubject(string legacyDN)
		{
			int num = legacyDN.IndexOf("/cn=", StringComparison.OrdinalIgnoreCase);
			if (num == -1)
			{
				FreeBusyFolder.Tracer.TraceDebug<string>(0L, "Found no /cn= in legacy DN: {0}", legacyDN);
				return null;
			}
			string text = legacyDN.Substring(num);
			return "USER-" + text.ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00034470 File Offset: 0x00032670
		internal static string GetFreeBusyFolderName(string legacyDN)
		{
			int num = legacyDN.IndexOf("/cn=", StringComparison.OrdinalIgnoreCase);
			if (num == -1)
			{
				num = legacyDN.Length;
			}
			string text = legacyDN.Substring(0, num);
			return "EX:" + text.ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000344B4 File Offset: 0x000326B4
		internal static string GetInternalLegacyDN(ADUser user, string targetLegacyDN)
		{
			string x = FreeBusyFolder.GetOrganizationLegacyDN(user.LegacyExchangeDN) + "/ou=External (FYDIBOHF25SPDLT)";
			foreach (ProxyAddress proxyAddress in user.EmailAddresses)
			{
				if (proxyAddress.Prefix == ProxyAddressPrefix.X500)
				{
					string oulegacyDN = FreeBusyFolder.GetOULegacyDN(proxyAddress.AddressString);
					if (oulegacyDN != null && !StringComparer.OrdinalIgnoreCase.Equals(x, oulegacyDN) && FreeBusyFolder.IsGeneratedLegacyDN(proxyAddress.AddressString))
					{
						return proxyAddress.AddressString;
					}
				}
			}
			string oulegacyDN2 = FreeBusyFolder.GetOULegacyDN(targetLegacyDN);
			if (oulegacyDN2 != null)
			{
				return FreeBusyFolder.GenerateInternalLegacyDN(oulegacyDN2);
			}
			return null;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00034574 File Offset: 0x00032774
		internal static string GetExternalLegacyDN(ADUser user)
		{
			string y = FreeBusyFolder.GetOrganizationLegacyDN(user.LegacyExchangeDN) + "/ou=External (FYDIBOHF25SPDLT)";
			foreach (ProxyAddress proxyAddress in user.EmailAddresses)
			{
				if (proxyAddress.Prefix == ProxyAddressPrefix.X500)
				{
					string oulegacyDN = FreeBusyFolder.GetOULegacyDN(proxyAddress.AddressString);
					if (oulegacyDN != null && StringComparer.OrdinalIgnoreCase.Equals(oulegacyDN, y))
					{
						return proxyAddress.AddressString;
					}
				}
			}
			return FreeBusyFolder.GenerateExternalLegacyDN(user.LegacyExchangeDN);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0003461C File Offset: 0x0003281C
		private static string GenerateInternalLegacyDN(string legacyDN)
		{
			return legacyDN + "/cn=Recipients" + FreeBusyFolder.GetNewCnRdn();
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0003462E File Offset: 0x0003282E
		private static string GenerateExternalLegacyDN(string legacyDN)
		{
			return FreeBusyFolder.GetOrganizationLegacyDN(legacyDN) + "/ou=External (FYDIBOHF25SPDLT)/cn=Recipients" + FreeBusyFolder.GetNewCnRdn();
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00034648 File Offset: 0x00032848
		private static string GetNewCnRdn()
		{
			return "/cn=" + Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower();
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00034688 File Offset: 0x00032888
		private static bool IsGeneratedLegacyDN(string legacyDN)
		{
			int num = legacyDN.LastIndexOf("/cn=");
			if (num == -1)
			{
				return false;
			}
			string text = legacyDN.Substring(num + "/cn=".Length);
			return text.Length == FreeBusyFolder.GeneratedLegacyDNValueLength && FreeBusyFolder.GeneratedLegacyDNValueValidator.IsMatch(text);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x000346DC File Offset: 0x000328DC
		private static string GetOrganizationLegacyDN(string legacyDN)
		{
			int num = legacyDN.IndexOf("/ou=", StringComparison.OrdinalIgnoreCase);
			if (num == -1)
			{
				return legacyDN;
			}
			return legacyDN.Substring(0, num);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00034704 File Offset: 0x00032904
		private static string GetOULegacyDN(string legacyDN)
		{
			if (string.IsNullOrEmpty(legacyDN))
			{
				return null;
			}
			int num = legacyDN.IndexOf("/ou=");
			if (num == -1)
			{
				return null;
			}
			int num2 = legacyDN.IndexOf("/", num + "/ou=".Length);
			if (num2 == -1)
			{
				num2 = legacyDN.Length;
			}
			return legacyDN.Substring(0, num2);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00034758 File Offset: 0x00032958
		private static Item CreateFreeBusyItemInternal(PublicFolderSession session, StoreObjectId freeBusyFolderId, string legacyDN)
		{
			string freeBusyItemSubject = FreeBusyFolder.GetFreeBusyItemSubject(legacyDN);
			Item item = Item.Create(session, "IPM.Post", freeBusyFolderId);
			bool flag = false;
			try
			{
				item[ItemSchema.Subject] = freeBusyItemSubject;
				item[FreeBusyItemSchema.ScheduleInfoRecipientLegacyDn] = legacyDN;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					item.Dispose();
				}
			}
			return item;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000347B4 File Offset: 0x000329B4
		private static StoreObjectId GetFreeBusyFolderIdInternal(PublicFolderSession session, string legacyDN, FreeBusyFolderDisposition disposition)
		{
			string freeBusyFolderName = FreeBusyFolder.GetFreeBusyFolderName(legacyDN);
			if (freeBusyFolderName == null)
			{
				FreeBusyFolder.Tracer.TraceError<string>(0L, "Unable to get free/busy folder for mailbox with legacy DN '{0}' because cannot obtain folder name from legacy DN", legacyDN);
				return null;
			}
			StoreObjectId freeBusyRootFolderId = FreeBusyFolder.GetFreeBusyRootFolderId(session);
			if (freeBusyRootFolderId == null)
			{
				return null;
			}
			StoreObjectId result;
			try
			{
				StoreObjectId storeObjectId = null;
				using (Folder folder = Folder.Bind(session, freeBusyRootFolderId))
				{
					storeObjectId = QueryChildFolderByName.Query(folder, freeBusyFolderName);
				}
				if (storeObjectId == null && disposition == FreeBusyFolderDisposition.CreateIfNeeded)
				{
					storeObjectId = FreeBusyFolder.CreateFreeBusyFolder(session, freeBusyRootFolderId, freeBusyFolderName);
				}
				result = storeObjectId;
			}
			catch (StoragePermanentException arg)
			{
				FreeBusyFolder.Tracer.TraceDebug<StoragePermanentException>(0L, "Failed to get free/busy folder due exception: {0}", arg);
				result = null;
			}
			return result;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00034858 File Offset: 0x00032A58
		private static StoreObjectId CreateFreeBusyFolder(PublicFolderSession session, StoreObjectId parentFolder, string folderName)
		{
			Exception ex = null;
			try
			{
				using (Folder folder = Folder.Create(session, parentFolder, StoreObjectType.Folder, folderName, CreateMode.OpenIfExists))
				{
					folder[FolderSchema.ResolveMethod] = (ResolveMethod.LastWriterWins | ResolveMethod.NoConflictNotification);
					FolderSaveResult folderSaveResult = folder.Save();
					if (folderSaveResult.OperationResult == OperationResult.Succeeded)
					{
						folder.Load();
						StoreObjectId objectId = folder.Id.ObjectId;
						FreeBusyFolder.Tracer.TraceDebug<string, StoreObjectId, StoreObjectId>(0L, "Created free/busy folder '{0}' under folder '{1}' with id '{2}'.", folderName, parentFolder, objectId);
						return objectId;
					}
					ex = folderSaveResult.Exception;
				}
			}
			catch (PropertyErrorException ex2)
			{
				ex = ex2;
			}
			catch (CorruptDataException ex3)
			{
				ex = ex3;
			}
			catch (ObjectNotFoundException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				FreeBusyFolder.Tracer.TraceError<string, StoreObjectId, Exception>(0L, "Failed to create free/busy folder '{0}' under folder '{1}' due exception: {2}", folderName, parentFolder, ex);
			}
			return null;
		}

		// Token: 0x040006AB RID: 1707
		public const string OURdnExternalOU = "/ou=External (FYDIBOHF25SPDLT)";

		// Token: 0x040006AC RID: 1708
		private const int OpenSessionRetries = 3;

		// Token: 0x040006AD RID: 1709
		private const string FreeBusyItemSubjectPrefix = "USER-";

		// Token: 0x040006AE RID: 1710
		private const string FreeBusyFolderNamePrefix = "EX:";

		// Token: 0x040006AF RID: 1711
		private const string FreeBusyRootFolderName = "SCHEDULE+ FREE BUSY";

		// Token: 0x040006B0 RID: 1712
		private const int TransientRetries = 3;

		// Token: 0x040006B1 RID: 1713
		private const string ExternalOU = "External (FYDIBOHF25SPDLT)";

		// Token: 0x040006B2 RID: 1714
		private const string OuRdn = "/ou=";

		// Token: 0x040006B3 RID: 1715
		private const string CnRdn = "/cn=";

		// Token: 0x040006B4 RID: 1716
		private const string RecipientsRdn = "/cn=Recipients";

		// Token: 0x040006B5 RID: 1717
		private static readonly Trace Tracer = ExTraceGlobals.FreeBusyTracer;

		// Token: 0x040006B6 RID: 1718
		private static readonly int GeneratedLegacyDNValueLength = Guid.Empty.ToString().Replace("-", string.Empty).Length;

		// Token: 0x040006B7 RID: 1719
		private static readonly Regex GeneratedLegacyDNValueValidator = new Regex("[0-9a-fA-F]*", RegexOptions.Compiled);
	}
}
