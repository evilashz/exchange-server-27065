using System;
using System.IO;
using System.Xml;
using Microsoft.Exchange.AirSync.Wbxml;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000A2 RID: 162
	internal struct FolderSyncStateCustomDataInfo
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x00035984 File Offset: 0x00033B84
		internal static void HandlerCustomDataVersioning(FolderSyncState syncState)
		{
			if (syncState == null)
			{
				throw new ArgumentNullException("syncState");
			}
			if (syncState.CustomVersion == null && syncState.SyncStateIsNew)
			{
				return;
			}
			bool flag = true;
			if (syncState.CustomVersion == null || syncState.CustomVersion <= 2 || syncState.CustomVersion > 9)
			{
				flag = false;
			}
			else if (syncState.CustomVersion.Value != 9)
			{
				switch (syncState.CustomVersion.Value)
				{
				case 3:
					syncState["CachedOptionsNode"] = null;
					break;
				case 4:
					break;
				case 5:
					goto IL_117;
				case 6:
					goto IL_12C;
				case 7:
					goto IL_13D;
				case 8:
					goto IL_143;
				default:
					flag = false;
					goto IL_158;
				}
				object obj = syncState[CustomStateDatumType.AirSyncProtocolVersion];
				if (obj is ConstStringData)
				{
					string data = syncState.GetData<ConstStringData, string>(CustomStateDatumType.AirSyncProtocolVersion, null);
					int data2 = 20;
					if (data != null)
					{
						data2 = AirSyncUtility.ParseVersionString(data);
					}
					syncState[CustomStateDatumType.AirSyncProtocolVersion] = new Int32Data(data2);
				}
				IL_117:
				syncState["MaxItems"] = new Int32Data(int.MaxValue);
				IL_12C:
				syncState["ConversationMode"] = new BooleanData(false);
				IL_13D:
				FolderSyncStateCustomDataInfo.ConvertV7StickyOptions(syncState);
				IL_143:
				syncState["Permissions"] = new Int32Data(0);
			}
			IL_158:
			if (!flag)
			{
				syncState.HandleCorruptSyncState();
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00035AF4 File Offset: 0x00033CF4
		private static void ConvertV7StickyOptions(FolderSyncState syncState)
		{
			XmlNode xmlNode = null;
			ByteArrayData byteArrayData = (ByteArrayData)syncState[CustomStateDatumType.CachedOptionsNode];
			if (byteArrayData == null || byteArrayData.Data == null)
			{
				return;
			}
			using (MemoryStream memoryStream = new MemoryStream(byteArrayData.Data))
			{
				using (WbxmlReader wbxmlReader = new WbxmlReader(memoryStream))
				{
					xmlNode = wbxmlReader.ReadXmlDocument().FirstChild;
					if (xmlNode == null)
					{
						return;
					}
				}
			}
			using (MemoryStream memoryStream2 = new MemoryStream(50))
			{
				WbxmlWriter wbxmlWriter = new WbxmlWriter(memoryStream2);
				XmlElement xmlElement = xmlNode.OwnerDocument.CreateElement("Collection", "AirSync:");
				xmlElement.AppendChild(xmlNode);
				wbxmlWriter.WriteXmlDocumentFromElement(xmlElement);
				syncState[CustomStateDatumType.CachedOptionsNode] = new ByteArrayData(memoryStream2.ToArray());
			}
		}

		// Token: 0x040005B0 RID: 1456
		internal const string AirSyncClassType = "AirSyncClassType";

		// Token: 0x040005B1 RID: 1457
		internal const string AirSyncProtocolVersion = "AirSyncProtocolVersion";

		// Token: 0x040005B2 RID: 1458
		internal const string CalendarSyncState = "CalendarSyncState";

		// Token: 0x040005B3 RID: 1459
		internal const string RecoveryCalendarSyncState = "RecoveryCalendarSyncState";

		// Token: 0x040005B4 RID: 1460
		internal const string CalendarMasterItems = "CalendarMasterItems";

		// Token: 0x040005B5 RID: 1461
		internal const string CustomCalendarSyncFilter = "CustomCalendarSyncFilter";

		// Token: 0x040005B6 RID: 1462
		internal const string FilterType = "FilterType";

		// Token: 0x040005B7 RID: 1463
		internal const string MaxItems = "MaxItems";

		// Token: 0x040005B8 RID: 1464
		internal const string ConversationMode = "ConversationMode";

		// Token: 0x040005B9 RID: 1465
		internal const string IdMapping = "IdMapping";

		// Token: 0x040005BA RID: 1466
		internal const string RecoverySyncKey = "RecoverySyncKey";

		// Token: 0x040005BB RID: 1467
		internal const string SupportedTags = "SupportedTags";

		// Token: 0x040005BC RID: 1468
		internal const string SyncKey = "SyncKey";

		// Token: 0x040005BD RID: 1469
		internal const string CachedOptionsNode = "CachedOptionsNode";

		// Token: 0x040005BE RID: 1470
		internal const string Permissions = "Permissions";

		// Token: 0x040005BF RID: 1471
		internal const int Version = 9;

		// Token: 0x040005C0 RID: 1472
		internal const int E12BaseVersion = 2;
	}
}
