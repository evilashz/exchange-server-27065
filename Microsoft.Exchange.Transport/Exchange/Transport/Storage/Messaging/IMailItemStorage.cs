using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000EB RID: 235
	internal interface IMailItemStorage
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000880 RID: 2176
		bool IsNew { get; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000881 RID: 2177
		bool IsDeleted { get; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000882 RID: 2178
		// (set) Token: 0x06000883 RID: 2179
		bool IsActive { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000884 RID: 2180
		// (set) Token: 0x06000885 RID: 2181
		bool IsReadOnly { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000886 RID: 2182
		bool PendingDatabaseUpdates { get; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000887 RID: 2183
		bool IsInAsyncCommit { get; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000888 RID: 2184
		long MsgId { get; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000889 RID: 2185
		// (set) Token: 0x0600088A RID: 2186
		string FromAddress { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600088B RID: 2187
		// (set) Token: 0x0600088C RID: 2188
		string AttributedFromAddress { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600088D RID: 2189
		// (set) Token: 0x0600088E RID: 2190
		DateTime DateReceived { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600088F RID: 2191
		// (set) Token: 0x06000890 RID: 2192
		TimeSpan ExtensionToExpiryDuration { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000891 RID: 2193
		// (set) Token: 0x06000892 RID: 2194
		DsnFormat DsnFormat { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000893 RID: 2195
		// (set) Token: 0x06000894 RID: 2196
		bool IsDiscardPending { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000895 RID: 2197
		// (set) Token: 0x06000896 RID: 2198
		MailDirectionality Directionality { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000897 RID: 2199
		// (set) Token: 0x06000898 RID: 2200
		string HeloDomain { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000899 RID: 2201
		// (set) Token: 0x0600089A RID: 2202
		string Auth { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600089B RID: 2203
		// (set) Token: 0x0600089C RID: 2204
		string EnvId { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600089D RID: 2205
		// (set) Token: 0x0600089E RID: 2206
		BodyType BodyType { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600089F RID: 2207
		// (set) Token: 0x060008A0 RID: 2208
		string Oorg { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060008A1 RID: 2209
		// (set) Token: 0x060008A2 RID: 2210
		string ReceiveConnectorName { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060008A3 RID: 2211
		// (set) Token: 0x060008A4 RID: 2212
		int PoisonCount { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060008A5 RID: 2213
		// (set) Token: 0x060008A6 RID: 2214
		IPAddress SourceIPAddress { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060008A7 RID: 2215
		// (set) Token: 0x060008A8 RID: 2216
		string Subject { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060008A9 RID: 2217
		// (set) Token: 0x060008AA RID: 2218
		string InternetMessageId { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060008AB RID: 2219
		// (set) Token: 0x060008AC RID: 2220
		Guid ShadowMessageId { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060008AD RID: 2221
		// (set) Token: 0x060008AE RID: 2222
		string ShadowServerContext { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060008AF RID: 2223
		// (set) Token: 0x060008B0 RID: 2224
		string ShadowServerDiscardId { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060008B1 RID: 2225
		// (set) Token: 0x060008B2 RID: 2226
		IDataExternalComponent Recipients { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060008B3 RID: 2227
		// (set) Token: 0x060008B4 RID: 2228
		int Scl { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060008B5 RID: 2229
		// (set) Token: 0x060008B6 RID: 2230
		string PerfCounterAttribution { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060008B7 RID: 2231
		IExtendedPropertyCollection ExtendedProperties { get; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060008B8 RID: 2232
		XExch50Blob XExch50Blob { get; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060008B9 RID: 2233
		LazyBytes FastIndexBlob { get; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008BA RID: 2234
		bool IsJournalReport { get; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060008BB RID: 2235
		// (set) Token: 0x060008BC RID: 2236
		List<string> MoveToHosts { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060008BD RID: 2237
		// (set) Token: 0x060008BE RID: 2238
		bool RetryDeliveryIfRejected { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060008BF RID: 2239
		MultiValueHeader TransportPropertiesHeader { get; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060008C0 RID: 2240
		// (set) Token: 0x060008C1 RID: 2241
		DeliveryPriority? Priority { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060008C2 RID: 2242
		// (set) Token: 0x060008C3 RID: 2243
		DeliveryPriority BootloadingPriority { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060008C4 RID: 2244
		// (set) Token: 0x060008C5 RID: 2245
		string PrioritizationReason { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060008C6 RID: 2246
		// (set) Token: 0x060008C7 RID: 2247
		Guid NetworkMessageId { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060008C8 RID: 2248
		// (set) Token: 0x060008C9 RID: 2249
		Guid SystemProbeId { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060008CA RID: 2250
		// (set) Token: 0x060008CB RID: 2251
		RiskLevel RiskLevel { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060008CC RID: 2252
		// (set) Token: 0x060008CD RID: 2253
		MimeDocument MimeDocument { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060008CE RID: 2254
		EmailMessage Message { get; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060008CF RID: 2255
		// (set) Token: 0x060008D0 RID: 2256
		string MimeFrom { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060008D1 RID: 2257
		// (set) Token: 0x060008D2 RID: 2258
		string MimeSenderAddress { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060008D3 RID: 2259
		// (set) Token: 0x060008D4 RID: 2260
		bool MimeNotSequential { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060008D5 RID: 2261
		// (set) Token: 0x060008D6 RID: 2262
		bool FallbackToRawOverride { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060008D7 RID: 2263
		// (set) Token: 0x060008D8 RID: 2264
		Encoding MimeDefaultEncoding { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060008D9 RID: 2265
		bool MimeWriteStreamOpen { get; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060008DA RID: 2266
		// (set) Token: 0x060008DB RID: 2267
		long MimeSize { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060008DC RID: 2268
		MimePart RootPart { get; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060008DD RID: 2269
		// (set) Token: 0x060008DE RID: 2270
		Guid ExternalOrganizationId { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060008DF RID: 2271
		// (set) Token: 0x060008E0 RID: 2272
		string ExoAccountForest { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060008E1 RID: 2273
		// (set) Token: 0x060008E2 RID: 2274
		string ExoTenantContainer { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060008E3 RID: 2275
		// (set) Token: 0x060008E4 RID: 2276
		string ProbeName { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060008E5 RID: 2277
		// (set) Token: 0x060008E6 RID: 2278
		bool PersistProbeTrace { get; set; }

		// Token: 0x060008E7 RID: 2279
		Stream OpenMimeReadStream(bool downConvert);

		// Token: 0x060008E8 RID: 2280
		Stream OpenMimeWriteStream(MimeLimits mimeLimits, bool expectBinaryContent);

		// Token: 0x060008E9 RID: 2281
		void OpenMimeDBWriter(DataTableCursor cursor, bool update, Func<bool> checkpointCallback, out Stream mimeMap, out CreateFixedStream mimeCreateFixedStream);

		// Token: 0x060008EA RID: 2282
		Stream OpenMimeDBReader();

		// Token: 0x060008EB RID: 2283
		MimeDocument LoadMimeFromDB(DecodingOptions decodingOptions);

		// Token: 0x060008EC RID: 2284
		long GetCurrrentMimeSize();

		// Token: 0x060008ED RID: 2285
		long RefreshMimeSize();

		// Token: 0x060008EE RID: 2286
		void UpdateCachedHeaders();

		// Token: 0x060008EF RID: 2287
		void RestoreLastSavedMime();

		// Token: 0x060008F0 RID: 2288
		void ResetMimeParserEohCallback();

		// Token: 0x060008F1 RID: 2289
		void MinimizeMemory();

		// Token: 0x060008F2 RID: 2290
		void Commit(TransactionCommitMode commitMode);

		// Token: 0x060008F3 RID: 2291
		void Materialize(Transaction transaction);

		// Token: 0x060008F4 RID: 2292
		IAsyncResult BeginCommit(AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060008F5 RID: 2293
		bool EndCommit(IAsyncResult ar, out Exception exception);

		// Token: 0x060008F6 RID: 2294
		void MarkToDelete();

		// Token: 0x060008F7 RID: 2295
		void ReleaseFromActive();

		// Token: 0x060008F8 RID: 2296
		IMailItemStorage Clone();

		// Token: 0x060008F9 RID: 2297
		IMailItemStorage CloneCommitted(Action<IMailItemStorage> cloneVisitor);

		// Token: 0x060008FA RID: 2298
		void AtomicChange(Action<IMailItemStorage> changeAction);

		// Token: 0x060008FB RID: 2299
		IMailItemStorage CopyCommitted(Action<IMailItemStorage> copyVisitor);
	}
}
