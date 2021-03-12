using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200006B RID: 107
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class InterfaceIds
	{
		// Token: 0x04000480 RID: 1152
		public static readonly Guid IStreamGuid = new Guid("0000000c-0000-0000-C000-000000000046");

		// Token: 0x04000481 RID: 1153
		public static readonly Guid IStorageGuid = new Guid("0000000b-0000-0000-C000-000000000046");

		// Token: 0x04000482 RID: 1154
		public static readonly Guid IMessageGuid = new Guid("00020307-0000-0000-C000-000000000046");

		// Token: 0x04000483 RID: 1155
		public static readonly Guid IMAPIFolderGuid = new Guid("0002030c-0000-0000-C000-000000000046");

		// Token: 0x04000484 RID: 1156
		public static readonly Guid IAttachGuid = new Guid("00020308-0000-0000-C000-000000000046");

		// Token: 0x04000485 RID: 1157
		public static readonly Guid IMAPIContainerGuid = new Guid("0002030B-0000-0000-C000-000000000046");

		// Token: 0x04000486 RID: 1158
		public static readonly Guid IMsgStoreGuid = new Guid("00020306-0000-0000-C000-000000000046");

		// Token: 0x04000487 RID: 1159
		public static readonly Guid IExchangeModifyTable = new Guid("2d734cb0-53fd-101b-b19d-08002b3056e3");

		// Token: 0x04000488 RID: 1160
		public static readonly Guid IExchangeExportChanges = new Guid("a3ea9cc0-d1b2-11cd-80fc-00aa004bba0b");

		// Token: 0x04000489 RID: 1161
		public static readonly Guid IExchangeExportManifest = new Guid("82D370F5-6F10-457d-99F9-11977856A7AA");

		// Token: 0x0400048A RID: 1162
		public static readonly Guid IExchangeExportManifestEx = new Guid("17E58114-B412-40ac-918C-C0B170DD2026");

		// Token: 0x0400048B RID: 1163
		public static readonly Guid IExchangeExportHierManifestEx = new Guid("2DC76CDD-1AA6-4157-808F-E68D2AD29FE8");

		// Token: 0x0400048C RID: 1164
		public static readonly Guid IExchangeImportContentsChanges = new Guid("f75abfa0-d0e0-11cd-80fc-00aa004bba0b");

		// Token: 0x0400048D RID: 1165
		public static readonly Guid IExchangeImportContentsChanges3 = new Guid("361487fc-888a-4746-8ab3-2a198c91585a");

		// Token: 0x0400048E RID: 1166
		public static readonly Guid IExchangeImportContentsChanges4 = new Guid("F5F9FFFE-D1AF-45d3-B790-E4D489D38B7E");

		// Token: 0x0400048F RID: 1167
		public static readonly Guid IExchangeImportHierarchyChanges = new Guid("85a66cf0-d0e0-11cd-80fc-00aa004bba0b");

		// Token: 0x04000490 RID: 1168
		public static readonly Guid IExchangeImportHierarchyChanges2 = new Guid("7846EDBA-8287-4d76-BD5F-1E0513D10E0C");

		// Token: 0x04000491 RID: 1169
		public static readonly Guid IExchangeMessageConversion = new Guid("3532b360-d114-11cf-a83b-00c04fd65597");

		// Token: 0x04000492 RID: 1170
		public static readonly Guid IExRpcConnection = new Guid("DCBB456B-FBDA-4c0c-BCF2-90EEF6BDCC07");

		// Token: 0x04000493 RID: 1171
		public static readonly Guid IExRpcMessage = new Guid("83BB0082-568A-4227-A830-C1A3844B9331");

		// Token: 0x04000494 RID: 1172
		public static readonly Guid IExRpcFolder = new Guid("E9972C72-4A7D-464c-9350-ADD5ABABF6D8");

		// Token: 0x04000495 RID: 1173
		public static readonly Guid IExRpcTable = new Guid("E2E6C3BD-835E-4921-9F86-A08DBAB67EB7");

		// Token: 0x04000496 RID: 1174
		public static readonly Guid IExRpcMsgStore = new Guid("37FB08C3-F6C8-4de8-B8DA-AB7E41D01ECE");

		// Token: 0x04000497 RID: 1175
		public static readonly Guid ILastErrorInfo = new Guid("42A2AEE7-E53B-49e3-9011-8DF591F16085");

		// Token: 0x04000498 RID: 1176
		public static readonly Guid IExchangeFastTransferEx = new Guid("1AD3079C-5325-4b68-A57E-E8FF2BD58E53");

		// Token: 0x04000499 RID: 1177
		public static readonly Guid IExchangeExportContentsChangesEx = new Guid("C4BB0442-D823-4e4c-81AD-059072399DC5");

		// Token: 0x0400049A RID: 1178
		public static readonly Guid IExchangeExportHierarchyChangesEx = new Guid("72616CCF-43D6-4f02-9486-A23E89965973");

		// Token: 0x0400049B RID: 1179
		public static readonly Guid IFastTransferStream = new Guid("a91d38a5-c92b-45eb-8426-1bfa5b17bc3c");
	}
}
