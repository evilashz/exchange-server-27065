using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.CtsResources
{
	// Token: 0x02000091 RID: 145
	internal static class MsgStorageStrings
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0001650C File Offset: 0x0001470C
		static MsgStorageStrings()
		{
			MsgStorageStrings.stringIDs.Add(3471497494U, "CallReadNextProperty");
			MsgStorageStrings.stringIDs.Add(2188699373U, "CorruptData");
			MsgStorageStrings.stringIDs.Add(3520643888U, "RecipientPropertyTooLong");
			MsgStorageStrings.stringIDs.Add(3735744638U, "PropertyValueTruncated");
			MsgStorageStrings.stringIDs.Add(539623025U, "RecipientPropertiesNotStreamable");
			MsgStorageStrings.stringIDs.Add(3819751606U, "NotANamedProperty");
			MsgStorageStrings.stringIDs.Add(819337844U, "NotAMessageAttachment");
			MsgStorageStrings.stringIDs.Add(3801565302U, "ComExceptionThrown");
			MsgStorageStrings.stringIDs.Add(2099708518U, "PropertyLongValue");
			MsgStorageStrings.stringIDs.Add(3821818057U, "NotAnOleAttachment");
			MsgStorageStrings.stringIDs.Add(566373508U, "NonStreamableProperty");
			MsgStorageStrings.stringIDs.Add(188730555U, "LargeNamedPropertyList");
			MsgStorageStrings.stringIDs.Add(3430902968U, "AllPropertiesRead");
			MsgStorageStrings.stringIDs.Add(3394043311U, "NotFound");
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00016660 File Offset: 0x00014860
		public static string FailedCreateStorage(string filename)
		{
			return string.Format(MsgStorageStrings.ResourceManager.GetString("FailedCreateStorage"), filename);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00016677 File Offset: 0x00014877
		public static string UnsupportedPropertyType(string type)
		{
			return string.Format(MsgStorageStrings.ResourceManager.GetString("UnsupportedPropertyType"), type);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001668E File Offset: 0x0001488E
		public static string FailedWrite(string streamName)
		{
			return string.Format(MsgStorageStrings.ResourceManager.GetString("FailedWrite"), streamName);
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000166A5 File Offset: 0x000148A5
		public static string CallReadNextProperty
		{
			get
			{
				return MsgStorageStrings.ResourceManager.GetString("CallReadNextProperty");
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000166B6 File Offset: 0x000148B6
		public static string CorruptData
		{
			get
			{
				return MsgStorageStrings.ResourceManager.GetString("CorruptData");
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000166C7 File Offset: 0x000148C7
		public static string StreamNotSeakable(string className)
		{
			return string.Format(MsgStorageStrings.ResourceManager.GetString("StreamNotSeakable"), className);
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000166DE File Offset: 0x000148DE
		public static string RecipientPropertyTooLong
		{
			get
			{
				return MsgStorageStrings.ResourceManager.GetString("RecipientPropertyTooLong");
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000166EF File Offset: 0x000148EF
		public static string PropertyValueTruncated
		{
			get
			{
				return MsgStorageStrings.ResourceManager.GetString("PropertyValueTruncated");
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00016700 File Offset: 0x00014900
		public static string RecipientPropertiesNotStreamable
		{
			get
			{
				return MsgStorageStrings.ResourceManager.GetString("RecipientPropertiesNotStreamable");
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00016711 File Offset: 0x00014911
		public static string NotANamedProperty
		{
			get
			{
				return MsgStorageStrings.ResourceManager.GetString("NotANamedProperty");
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00016722 File Offset: 0x00014922
		public static string StreamTooBig(string streamName, long streamLength)
		{
			return string.Format(MsgStorageStrings.ResourceManager.GetString("StreamTooBig"), streamName, streamLength);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001673F File Offset: 0x0001493F
		public static string PropertyNotFound(int tag)
		{
			return string.Format(MsgStorageStrings.ResourceManager.GetString("PropertyNotFound"), tag);
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0001675B File Offset: 0x0001495B
		public static string NotAMessageAttachment
		{
			get
			{
				return MsgStorageStrings.ResourceManager.GetString("NotAMessageAttachment");
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0001676C File Offset: 0x0001496C
		public static string ComExceptionThrown
		{
			get
			{
				return MsgStorageStrings.ResourceManager.GetString("ComExceptionThrown");
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0001677D File Offset: 0x0001497D
		public static string PropertyLongValue
		{
			get
			{
				return MsgStorageStrings.ResourceManager.GetString("PropertyLongValue");
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001678E File Offset: 0x0001498E
		public static string FailedRead(string streamName)
		{
			return string.Format(MsgStorageStrings.ResourceManager.GetString("FailedRead"), streamName);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000167A5 File Offset: 0x000149A5
		public static string FailedOpenStorage(string filename)
		{
			return string.Format(MsgStorageStrings.ResourceManager.GetString("FailedOpenStorage"), filename);
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x000167BC File Offset: 0x000149BC
		public static string NotAnOleAttachment
		{
			get
			{
				return MsgStorageStrings.ResourceManager.GetString("NotAnOleAttachment");
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000167CD File Offset: 0x000149CD
		public static string InvalidValueType(Type expected, Type actual)
		{
			return string.Format(MsgStorageStrings.ResourceManager.GetString("InvalidValueType"), expected, actual);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000167E5 File Offset: 0x000149E5
		public static string InvalidPropertyTag(int tag)
		{
			return string.Format(MsgStorageStrings.ResourceManager.GetString("InvalidPropertyTag"), tag);
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00016801 File Offset: 0x00014A01
		public static string NonStreamableProperty
		{
			get
			{
				return MsgStorageStrings.ResourceManager.GetString("NonStreamableProperty");
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00016812 File Offset: 0x00014A12
		public static string LargeNamedPropertyList
		{
			get
			{
				return MsgStorageStrings.ResourceManager.GetString("LargeNamedPropertyList");
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00016823 File Offset: 0x00014A23
		public static string AllPropertiesRead
		{
			get
			{
				return MsgStorageStrings.ResourceManager.GetString("AllPropertiesRead");
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00016834 File Offset: 0x00014A34
		public static string NotFound
		{
			get
			{
				return MsgStorageStrings.ResourceManager.GetString("NotFound");
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00016845 File Offset: 0x00014A45
		public static string GetLocalizedString(MsgStorageStrings.IDs key)
		{
			return MsgStorageStrings.ResourceManager.GetString(MsgStorageStrings.stringIDs[(uint)key]);
		}

		// Token: 0x040004ED RID: 1261
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(14);

		// Token: 0x040004EE RID: 1262
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.CtsResources.MsgStorageStrings", typeof(MsgStorageStrings).GetTypeInfo().Assembly);

		// Token: 0x02000092 RID: 146
		public enum IDs : uint
		{
			// Token: 0x040004F0 RID: 1264
			CallReadNextProperty = 3471497494U,
			// Token: 0x040004F1 RID: 1265
			CorruptData = 2188699373U,
			// Token: 0x040004F2 RID: 1266
			RecipientPropertyTooLong = 3520643888U,
			// Token: 0x040004F3 RID: 1267
			PropertyValueTruncated = 3735744638U,
			// Token: 0x040004F4 RID: 1268
			RecipientPropertiesNotStreamable = 539623025U,
			// Token: 0x040004F5 RID: 1269
			NotANamedProperty = 3819751606U,
			// Token: 0x040004F6 RID: 1270
			NotAMessageAttachment = 819337844U,
			// Token: 0x040004F7 RID: 1271
			ComExceptionThrown = 3801565302U,
			// Token: 0x040004F8 RID: 1272
			PropertyLongValue = 2099708518U,
			// Token: 0x040004F9 RID: 1273
			NotAnOleAttachment = 3821818057U,
			// Token: 0x040004FA RID: 1274
			NonStreamableProperty = 566373508U,
			// Token: 0x040004FB RID: 1275
			LargeNamedPropertyList = 188730555U,
			// Token: 0x040004FC RID: 1276
			AllPropertiesRead = 3430902968U,
			// Token: 0x040004FD RID: 1277
			NotFound = 3394043311U
		}

		// Token: 0x02000093 RID: 147
		private enum ParamIDs
		{
			// Token: 0x040004FF RID: 1279
			FailedCreateStorage,
			// Token: 0x04000500 RID: 1280
			UnsupportedPropertyType,
			// Token: 0x04000501 RID: 1281
			FailedWrite,
			// Token: 0x04000502 RID: 1282
			StreamNotSeakable,
			// Token: 0x04000503 RID: 1283
			StreamTooBig,
			// Token: 0x04000504 RID: 1284
			PropertyNotFound,
			// Token: 0x04000505 RID: 1285
			FailedRead,
			// Token: 0x04000506 RID: 1286
			FailedOpenStorage,
			// Token: 0x04000507 RID: 1287
			InvalidValueType,
			// Token: 0x04000508 RID: 1288
			InvalidPropertyTag
		}
	}
}
