using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000031 RID: 49
	internal sealed class LogonStatisticsSchema : MapiObjectSchema
	{
		// Token: 0x04000101 RID: 257
		public static readonly MapiPropertyDefinition AdapterSpeed = new MapiPropertyDefinition("AdapterSpeed", typeof(uint?), PropTag.ClientAdapterSpeed, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000102 RID: 258
		public static readonly MapiPropertyDefinition ClientIPAddress = new MapiPropertyDefinition("ClientIPAddress", typeof(string), PropTag.ClientIP, MapiPropertyDefinitionFlags.ReadOnly, null, new MapiPropValueExtractorDelegate(CustomizedMapiPropValueConvertor.ExtractIpV4StringFromIpV6Bytes), null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000103 RID: 259
		public static readonly MapiPropertyDefinition ClientMode = new MapiPropertyDefinition("ClientMode", typeof(ClientMode), PropTag.QuotaReceiveThreshold, MapiPropertyDefinitionFlags.ReadOnly, Microsoft.Exchange.Data.Mapi.ClientMode.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000104 RID: 260
		public static readonly MapiPropertyDefinition ClientName = new MapiPropertyDefinition("ClientName", typeof(string), PropTag.ClientMachineName, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000105 RID: 261
		public static readonly MapiPropertyDefinition ClientVersion = new MapiPropertyDefinition("ClientVersion", typeof(string), PropTag.ClientVersion, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000106 RID: 262
		public static readonly MapiPropertyDefinition CodePage = new MapiPropertyDefinition("CodePage", typeof(uint?), PropTag.CodePageId, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000107 RID: 263
		public static readonly MapiPropertyDefinition CurrentOpenAttachments = new MapiPropertyDefinition("CurrentOpenAttachments", typeof(uint?), PropTag.OpenAttachCount, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000108 RID: 264
		public static readonly MapiPropertyDefinition CurrentOpenFolders = new MapiPropertyDefinition("CurrentOpenFolders", typeof(uint?), PropTag.OpenFolderCount, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000109 RID: 265
		public static readonly MapiPropertyDefinition CurrentOpenMessages = new MapiPropertyDefinition("CurrentOpenMessages", typeof(uint?), PropTag.OpenMessageCount, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400010A RID: 266
		public static readonly MapiPropertyDefinition FolderOperationCount = new MapiPropertyDefinition("FolderOperationCount", typeof(uint?), PropTag.FolderOpRate, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400010B RID: 267
		public static readonly MapiPropertyDefinition FullMailboxDirectoryName = new MapiPropertyDefinition("FullMailboxDirectoryName", typeof(string), PropTag.MailboxDN, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400010C RID: 268
		public static readonly MapiPropertyDefinition FullUserDirectoryName = new MapiPropertyDefinition("FullUserDirectoryName", typeof(string), PropTag.UserDN, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400010D RID: 269
		public static readonly MapiPropertyDefinition HostAddress = new MapiPropertyDefinition("HostAddress", typeof(string), PropTag.HostAddress, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400010E RID: 270
		public static readonly MapiPropertyDefinition LastAccessTime = new MapiPropertyDefinition("LastAccessTime", typeof(DateTime?), PropTag.LastOpTime, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400010F RID: 271
		public static readonly MapiPropertyDefinition Latency = new MapiPropertyDefinition("Latency", typeof(uint?), PropTag.ClientLatency, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000110 RID: 272
		public static readonly MapiPropertyDefinition LocaleID = new MapiPropertyDefinition("LocaleID", typeof(uint?), PropTag.LocaleId, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000111 RID: 273
		public static readonly MapiPropertyDefinition LogonTime = new MapiPropertyDefinition("LogonTime", typeof(DateTime?), PropTag.LogonTime, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000112 RID: 274
		public static readonly MapiPropertyDefinition MACAddress = new MapiPropertyDefinition("MACAddress", typeof(string), PropTag.ClientMacAddress, MapiPropertyDefinitionFlags.ReadOnly, null, new MapiPropValueExtractorDelegate(CustomizedMapiPropValueConvertor.ExtractMacAddressStringFromBytes), null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000113 RID: 275
		public static readonly MapiPropertyDefinition MessagingOperationCount = new MapiPropertyDefinition("MessagingOperationCount", typeof(uint?), PropTag.MessagingOpRate, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000114 RID: 276
		public static readonly MapiPropertyDefinition OtherOperationCount = new MapiPropertyDefinition("OtherOperationCount", typeof(uint?), PropTag.OtherOpRate, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000115 RID: 277
		public static readonly MapiPropertyDefinition ProgressOperationCount = new MapiPropertyDefinition("ProgressOperationCount", typeof(uint?), PropTag.ProgressOpRate, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000116 RID: 278
		public static readonly MapiPropertyDefinition RPCCallsSucceeded = new MapiPropertyDefinition("RPCCallsSucceeded", typeof(uint?), PropTag.ClientRpcsSucceeded, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000117 RID: 279
		public static readonly MapiPropertyDefinition StreamOperationCount = new MapiPropertyDefinition("StreamOperationCount", typeof(uint?), PropTag.StreamOpRate, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000118 RID: 280
		public static readonly MapiPropertyDefinition TableOperationCount = new MapiPropertyDefinition("TableOperationCount", typeof(uint?), PropTag.TableOpRate, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000119 RID: 281
		public static readonly MapiPropertyDefinition TotalOperationCount = new MapiPropertyDefinition("TotalOperationCount", typeof(uint?), PropTag.TotalOpRate, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400011A RID: 282
		public static readonly MapiPropertyDefinition TransferOperationCount = new MapiPropertyDefinition("TransferOperationCount", typeof(uint?), PropTag.TransferOpRate, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400011B RID: 283
		public static readonly MapiPropertyDefinition UserName = new MapiPropertyDefinition("UserName", typeof(string), PropTag.UserDisplayName, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400011C RID: 284
		public static readonly MapiPropertyDefinition Windows2000Account = new MapiPropertyDefinition("Windows2000Account", typeof(string), PropTag.NTUserName, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400011D RID: 285
		public static readonly MapiPropertyDefinition ApplicationId = new MapiPropertyDefinition("ApplicationId", typeof(string), PropTag.ApplicationId, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400011E RID: 286
		public static readonly MapiPropertyDefinition SessionId = new MapiPropertyDefinition("SessionId", typeof(long?), PropTag.SessionId, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
