using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICoreItem : ICoreObject, ICoreState, IValidatable, IDisposeTrackable, IDisposable, ILocationIdentifierController
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004AE RID: 1198
		CoreAttachmentCollection AttachmentCollection { get; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004AF RID: 1199
		CoreRecipientCollection Recipients { get; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004B0 RID: 1200
		MapiMessage MapiMessage { get; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004B1 RID: 1201
		bool IsReadOnly { get; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004B2 RID: 1202
		// (set) Token: 0x060004B3 RID: 1203
		PropertyBagSaveFlags SaveFlags { get; set; }

		// Token: 0x060004B4 RID: 1204
		void OpenAsReadWrite();

		// Token: 0x060004B5 RID: 1205
		ConflictResolutionResult Save(SaveMode saveMode);

		// Token: 0x060004B6 RID: 1206
		ConflictResolutionResult Flush(SaveMode saveMode);

		// Token: 0x060004B7 RID: 1207
		void OpenAttachmentCollection();

		// Token: 0x060004B8 RID: 1208
		void OpenAttachmentCollection(ICoreItem owner);

		// Token: 0x060004B9 RID: 1209
		CoreRecipientCollection GetRecipientCollection(bool forceOpen);

		// Token: 0x060004BA RID: 1210
		void DisposeAttachmentCollection();

		// Token: 0x060004BB RID: 1211
		ConflictResolutionResult InternalFlush(SaveMode saveMode, CoreItemOperation operation, CallbackContext callbackContext);

		// Token: 0x060004BC RID: 1212
		ConflictResolutionResult InternalFlush(SaveMode saveMode, CallbackContext callbackContext);

		// Token: 0x060004BD RID: 1213
		ConflictResolutionResult InternalSave(SaveMode saveMode, CallbackContext callbackContext);

		// Token: 0x060004BE RID: 1214
		void SaveRecipients();

		// Token: 0x060004BF RID: 1215
		void AbandonRecipientChanges();

		// Token: 0x060004C0 RID: 1216
		void Submit();

		// Token: 0x060004C1 RID: 1217
		void Submit(SubmitMessageFlags submitFlags);

		// Token: 0x060004C2 RID: 1218
		void TransportSend(out PropertyDefinition[] properties, out object[] values);

		// Token: 0x060004C3 RID: 1219
		PropertyError[] CopyItem(ICoreItem destinationItem, CopyPropertiesFlags copyPropertiesFlags, CopySubObjects copySubObjects, NativeStorePropertyDefinition[] excludeProperties);

		// Token: 0x060004C4 RID: 1220
		PropertyError[] CopyProperties(ICoreItem destinationItem, CopyPropertiesFlags copyPropertiesFlags, NativeStorePropertyDefinition[] includeProperties);

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004C5 RID: 1221
		bool IsAttachmentCollectionLoaded { get; }

		// Token: 0x060004C6 RID: 1222
		void Reload();

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004C7 RID: 1223
		bool AreOptionalAutoloadPropertiesLoaded { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004C8 RID: 1224
		// (set) Token: 0x060004C9 RID: 1225
		ICoreItem TopLevelItem { get; set; }

		// Token: 0x060004CA RID: 1226
		void SetIrresolvableChange();

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060004CB RID: 1227
		// (remove) Token: 0x060004CC RID: 1228
		event Action BeforeSend;

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004CD RID: 1229
		Body Body { get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004CE RID: 1230
		ItemCharsetDetector CharsetDetector { get; }

		// Token: 0x17000106 RID: 262
		// (set) Token: 0x060004CF RID: 1231
		int PreferredInternetCodePageForShiftJis { set; }

		// Token: 0x17000107 RID: 263
		// (set) Token: 0x060004D0 RID: 1232
		int RequiredCoverage { set; }

		// Token: 0x060004D1 RID: 1233
		void GetCharsetDetectionData(StringBuilder stringBuilder, CharsetDetectionDataFlags flags);

		// Token: 0x060004D2 RID: 1234
		void SetCoreItemContext(ICoreItemContext context);

		// Token: 0x060004D3 RID: 1235
		void SetReadFlag(int flags, bool deferErrors);
	}
}
