using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200003E RID: 62
	internal static class ProtocolBaseStrings
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x00011768 File Offset: 0x0000F968
		static ProtocolBaseStrings()
		{
			ProtocolBaseStrings.stringIDs.Add(1139742230U, "UsageText");
			ProtocolBaseStrings.stringIDs.Add(1766818386U, "NotAvailable");
			ProtocolBaseStrings.stringIDs.Add(2966948847U, "ProcessNotResponding");
			ProtocolBaseStrings.stringIDs.Add(527123233U, "InvalidNamesSubject");
			ProtocolBaseStrings.stringIDs.Add(2964959188U, "SystemFromDisplayName");
			ProtocolBaseStrings.stringIDs.Add(1017415836U, "DuplicateFoldersSubject");
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001181B File Offset: 0x0000FA1B
		public static string FileNotFound(string fileName)
		{
			return string.Format(ProtocolBaseStrings.ResourceManager.GetString("FileNotFound"), fileName);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00011832 File Offset: 0x0000FA32
		public static string DuplicateFoldersBody(string dupes)
		{
			return string.Format(ProtocolBaseStrings.ResourceManager.GetString("DuplicateFoldersBody"), dupes);
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00011849 File Offset: 0x0000FA49
		public static string UsageText
		{
			get
			{
				return ProtocolBaseStrings.ResourceManager.GetString("UsageText");
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001185C File Offset: 0x0000FA5C
		public static string NonRenderableMessage(string subject, string displayName, string mailAddress, string sentDate)
		{
			return string.Format(ProtocolBaseStrings.ResourceManager.GetString("NonRenderableMessage"), new object[]
			{
				subject,
				displayName,
				mailAddress,
				sentDate
			});
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00011895 File Offset: 0x0000FA95
		public static string InvalidNamesBody(string names)
		{
			return string.Format(ProtocolBaseStrings.ResourceManager.GetString("InvalidNamesBody"), names);
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x000118AC File Offset: 0x0000FAAC
		public static string NotAvailable
		{
			get
			{
				return ProtocolBaseStrings.ResourceManager.GetString("NotAvailable");
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000118BD File Offset: 0x0000FABD
		public static string ProcessNotResponding
		{
			get
			{
				return ProtocolBaseStrings.ResourceManager.GetString("ProcessNotResponding");
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000118CE File Offset: 0x0000FACE
		public static string NonRenderableSubject(string id)
		{
			return string.Format(ProtocolBaseStrings.ResourceManager.GetString("NonRenderableSubject"), id);
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x000118E5 File Offset: 0x0000FAE5
		public static string InvalidNamesSubject
		{
			get
			{
				return ProtocolBaseStrings.ResourceManager.GetString("InvalidNamesSubject");
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x000118F6 File Offset: 0x0000FAF6
		public static string SystemFromDisplayName
		{
			get
			{
				return ProtocolBaseStrings.ResourceManager.GetString("SystemFromDisplayName");
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00011907 File Offset: 0x0000FB07
		public static string DuplicateFoldersSubject
		{
			get
			{
				return ProtocolBaseStrings.ResourceManager.GetString("DuplicateFoldersSubject");
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00011918 File Offset: 0x0000FB18
		public static string GetLocalizedString(ProtocolBaseStrings.IDs key)
		{
			return ProtocolBaseStrings.ResourceManager.GetString(ProtocolBaseStrings.stringIDs[(uint)key]);
		}

		// Token: 0x0400020B RID: 523
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(6);

		// Token: 0x0400020C RID: 524
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.PopImap.Core.ProtocolBaseStrings", typeof(ProtocolBaseStrings).GetTypeInfo().Assembly);

		// Token: 0x0200003F RID: 63
		public enum IDs : uint
		{
			// Token: 0x0400020E RID: 526
			UsageText = 1139742230U,
			// Token: 0x0400020F RID: 527
			NotAvailable = 1766818386U,
			// Token: 0x04000210 RID: 528
			ProcessNotResponding = 2966948847U,
			// Token: 0x04000211 RID: 529
			InvalidNamesSubject = 527123233U,
			// Token: 0x04000212 RID: 530
			SystemFromDisplayName = 2964959188U,
			// Token: 0x04000213 RID: 531
			DuplicateFoldersSubject = 1017415836U
		}

		// Token: 0x02000040 RID: 64
		private enum ParamIDs
		{
			// Token: 0x04000215 RID: 533
			FileNotFound,
			// Token: 0x04000216 RID: 534
			DuplicateFoldersBody,
			// Token: 0x04000217 RID: 535
			NonRenderableMessage,
			// Token: 0x04000218 RID: 536
			InvalidNamesBody,
			// Token: 0x04000219 RID: 537
			NonRenderableSubject
		}
	}
}
