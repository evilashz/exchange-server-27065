using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000C5 RID: 197
	public class AttachmentPolicy
	{
		// Token: 0x06000729 RID: 1833 RVA: 0x00037F60 File Offset: 0x00036160
		internal AttachmentPolicy(string[] blockFileTypes, string[] blockMimeTypes, string[] forceSaveFileTypes, string[] forceSaveMimeTypes, string[] allowFileTypes, string[] allowMimeTypes, AttachmentPolicy.Level treatUnknownTypeAs, bool directFileAccessOnPublicComputersEnabled, bool directFileAccessOnPrivateComputersEnabled, bool forceWebReadyDocumentViewingFirstOnPublicComputers, bool forceWebReadyDocumentViewingFirstOnPrivateComputers, bool webReadyDocumentViewingOnPublicComputersEnabled, bool webReadyDocumentViewingOnPrivateComputersEnabled, string[] webReadyFileTypes, string[] webReadyMimeTypes, string[] webReadyDocumentViewingSupportedFileTypes, string[] webReadyDocumentViewingSupportedMimeTypes, bool webReadyDocumentViewingForAllSupportedTypes)
		{
			this.treatUnknownTypeAs = treatUnknownTypeAs;
			this.directFileAccessOnPublicComputersEnabled = directFileAccessOnPublicComputersEnabled;
			this.directFileAccessOnPrivateComputersEnabled = directFileAccessOnPrivateComputersEnabled;
			this.forceWebReadyDocumentViewingFirstOnPublicComputers = forceWebReadyDocumentViewingFirstOnPublicComputers;
			this.forceWebReadyDocumentViewingFirstOnPrivateComputers = forceWebReadyDocumentViewingFirstOnPrivateComputers;
			this.webReadyDocumentViewingOnPublicComputersEnabled = webReadyDocumentViewingOnPublicComputersEnabled;
			this.webReadyDocumentViewingOnPrivateComputersEnabled = webReadyDocumentViewingOnPrivateComputersEnabled;
			this.webReadyFileTypes = webReadyFileTypes;
			Array.Sort<string>(this.webReadyFileTypes);
			this.webReadyMimeTypes = webReadyMimeTypes;
			Array.Sort<string>(this.webReadyMimeTypes);
			this.webReadyDocumentViewingSupportedFileTypes = webReadyDocumentViewingSupportedFileTypes;
			Array.Sort<string>(this.webReadyDocumentViewingSupportedFileTypes);
			this.webReadyDocumentViewingSupportedMimeTypes = webReadyDocumentViewingSupportedMimeTypes;
			Array.Sort<string>(this.webReadyDocumentViewingSupportedMimeTypes);
			this.webReadyDocumentViewingForAllSupportedTypes = webReadyDocumentViewingForAllSupportedTypes;
			this.fileTypeLevels = AttachmentPolicy.LoadDictionary(blockFileTypes, forceSaveFileTypes, allowFileTypes);
			this.mimeTypeLevels = AttachmentPolicy.LoadDictionary(blockMimeTypes, forceSaveMimeTypes, allowMimeTypes);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00038020 File Offset: 0x00036220
		private static SortedDictionary<string, AttachmentPolicy.Level> LoadDictionary(string[] block, string[] forceSave, string[] allow)
		{
			string[][] array = new string[3][];
			AttachmentPolicy.Level[] array2 = new AttachmentPolicy.Level[3];
			array[1] = block;
			array[2] = forceSave;
			array[0] = allow;
			array2[1] = AttachmentPolicy.Level.Block;
			array2[2] = AttachmentPolicy.Level.ForceSave;
			array2[0] = AttachmentPolicy.Level.Allow;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					array[i] = new string[0];
				}
			}
			SortedDictionary<string, AttachmentPolicy.Level> sortedDictionary = new SortedDictionary<string, AttachmentPolicy.Level>(StringComparer.OrdinalIgnoreCase);
			for (int j = 0; j <= 2; j++)
			{
				for (int k = 0; k < array[j].Length; k++)
				{
					string key = array[j][k];
					if (!sortedDictionary.ContainsKey(key))
					{
						sortedDictionary.Add(key, array2[j]);
					}
				}
			}
			return sortedDictionary;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000380C0 File Offset: 0x000362C0
		public AttachmentPolicy.Level GetLevel(string attachmentType, AttachmentPolicy.TypeSignifier typeSignifier)
		{
			AttachmentPolicy.Level result = AttachmentPolicy.Level.Unknown;
			switch (typeSignifier)
			{
			case AttachmentPolicy.TypeSignifier.File:
				result = AttachmentPolicy.FindLevel(this.fileTypeLevels, attachmentType);
				break;
			case AttachmentPolicy.TypeSignifier.Mime:
				result = AttachmentPolicy.FindLevel(this.mimeTypeLevels, attachmentType);
				break;
			}
			return result;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00038100 File Offset: 0x00036300
		public bool Contains(string key, AttachmentPolicy.LookupTypeSignifer signifer)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			switch (signifer)
			{
			case AttachmentPolicy.LookupTypeSignifer.FileArray:
				return Array.BinarySearch<string>(this.webReadyFileTypes, key, StringComparer.OrdinalIgnoreCase) >= 0;
			case AttachmentPolicy.LookupTypeSignifer.MimeArray:
				return Array.BinarySearch<string>(this.webReadyMimeTypes, key, StringComparer.OrdinalIgnoreCase) >= 0;
			case AttachmentPolicy.LookupTypeSignifer.SupportedFileArray:
				return Array.BinarySearch<string>(this.webReadyDocumentViewingSupportedFileTypes, key, StringComparer.OrdinalIgnoreCase) >= 0;
			case AttachmentPolicy.LookupTypeSignifer.SupportedMimeArray:
				return Array.BinarySearch<string>(this.webReadyDocumentViewingSupportedMimeTypes, key, StringComparer.OrdinalIgnoreCase) >= 0;
			default:
				return false;
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00038198 File Offset: 0x00036398
		private static AttachmentPolicy.Level FindLevel(SortedDictionary<string, AttachmentPolicy.Level> dictionary, string attachmentType)
		{
			AttachmentPolicy.Level result;
			if (!dictionary.TryGetValue(attachmentType, out result))
			{
				return AttachmentPolicy.Level.Unknown;
			}
			return result;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x000381B3 File Offset: 0x000363B3
		public static int MaxEmbeddedDepth
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x000381B7 File Offset: 0x000363B7
		public AttachmentPolicy.Level TreatUnknownTypeAs
		{
			get
			{
				return this.treatUnknownTypeAs;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x000381BF File Offset: 0x000363BF
		public bool DirectFileAccessEnabled
		{
			get
			{
				if (UserContextManager.GetUserContext().IsPublicLogon)
				{
					return this.directFileAccessOnPublicComputersEnabled;
				}
				return this.directFileAccessOnPrivateComputersEnabled;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x000381DA File Offset: 0x000363DA
		public bool WebReadyDocumentViewingEnable
		{
			get
			{
				if (UserContextManager.GetUserContext().IsPublicLogon)
				{
					return this.webReadyDocumentViewingOnPublicComputersEnabled;
				}
				return this.webReadyDocumentViewingOnPrivateComputersEnabled;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x000381F5 File Offset: 0x000363F5
		public bool ForceWebReadyDocumentViewingFirst
		{
			get
			{
				if (UserContextManager.GetUserContext().IsPublicLogon)
				{
					return this.forceWebReadyDocumentViewingFirstOnPublicComputers;
				}
				return this.forceWebReadyDocumentViewingFirstOnPrivateComputers;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00038210 File Offset: 0x00036410
		public bool WebReadyDocumentViewingForAllSupportedTypes
		{
			get
			{
				return this.webReadyDocumentViewingForAllSupportedTypes;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00038218 File Offset: 0x00036418
		public string[] WebReadyFileTypes
		{
			get
			{
				return this.webReadyFileTypes;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00038220 File Offset: 0x00036420
		public string[] WebReadyDocumentViewingSupportedFileTypes
		{
			get
			{
				return this.webReadyDocumentViewingSupportedFileTypes;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00038228 File Offset: 0x00036428
		public SortedDictionary<string, AttachmentPolicy.Level>.Enumerator FileTypeLevels
		{
			get
			{
				return this.fileTypeLevels.GetEnumerator();
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00038235 File Offset: 0x00036435
		public SortedDictionary<string, AttachmentPolicy.Level>.Enumerator MimeTypeLevels
		{
			get
			{
				return this.mimeTypeLevels.GetEnumerator();
			}
		}

		// Token: 0x040004E2 RID: 1250
		private const int MaxEmbeddedMessageDepth = 16;

		// Token: 0x040004E3 RID: 1251
		private AttachmentPolicy.Level treatUnknownTypeAs;

		// Token: 0x040004E4 RID: 1252
		private bool directFileAccessOnPrivateComputersEnabled;

		// Token: 0x040004E5 RID: 1253
		private bool directFileAccessOnPublicComputersEnabled;

		// Token: 0x040004E6 RID: 1254
		private bool webReadyDocumentViewingOnPrivateComputersEnabled;

		// Token: 0x040004E7 RID: 1255
		private bool webReadyDocumentViewingOnPublicComputersEnabled;

		// Token: 0x040004E8 RID: 1256
		private bool forceWebReadyDocumentViewingFirstOnPrivateComputers;

		// Token: 0x040004E9 RID: 1257
		private bool forceWebReadyDocumentViewingFirstOnPublicComputers;

		// Token: 0x040004EA RID: 1258
		private bool webReadyDocumentViewingForAllSupportedTypes;

		// Token: 0x040004EB RID: 1259
		private string[] webReadyFileTypes;

		// Token: 0x040004EC RID: 1260
		private string[] webReadyMimeTypes;

		// Token: 0x040004ED RID: 1261
		private string[] webReadyDocumentViewingSupportedMimeTypes;

		// Token: 0x040004EE RID: 1262
		private string[] webReadyDocumentViewingSupportedFileTypes;

		// Token: 0x040004EF RID: 1263
		private SortedDictionary<string, AttachmentPolicy.Level> fileTypeLevels;

		// Token: 0x040004F0 RID: 1264
		private SortedDictionary<string, AttachmentPolicy.Level> mimeTypeLevels;

		// Token: 0x020000C6 RID: 198
		public enum TypeSignifier
		{
			// Token: 0x040004F2 RID: 1266
			File,
			// Token: 0x040004F3 RID: 1267
			Mime
		}

		// Token: 0x020000C7 RID: 199
		public enum LookupTypeSignifer
		{
			// Token: 0x040004F5 RID: 1269
			FileArray,
			// Token: 0x040004F6 RID: 1270
			MimeArray,
			// Token: 0x040004F7 RID: 1271
			SupportedFileArray,
			// Token: 0x040004F8 RID: 1272
			SupportedMimeArray
		}

		// Token: 0x020000C8 RID: 200
		public enum Level
		{
			// Token: 0x040004FA RID: 1274
			None,
			// Token: 0x040004FB RID: 1275
			Block,
			// Token: 0x040004FC RID: 1276
			ForceSave,
			// Token: 0x040004FD RID: 1277
			Allow,
			// Token: 0x040004FE RID: 1278
			Unknown
		}

		// Token: 0x020000C9 RID: 201
		private enum LevelPrecedence
		{
			// Token: 0x04000500 RID: 1280
			First,
			// Token: 0x04000501 RID: 1281
			Allow = 0,
			// Token: 0x04000502 RID: 1282
			Block,
			// Token: 0x04000503 RID: 1283
			ForceSave,
			// Token: 0x04000504 RID: 1284
			Last = 2
		}
	}
}
