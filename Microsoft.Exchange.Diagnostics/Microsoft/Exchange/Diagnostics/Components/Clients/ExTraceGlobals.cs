using System;

namespace Microsoft.Exchange.Diagnostics.Components.Clients
{
	// Token: 0x02000337 RID: 823
	public static class ExTraceGlobals
	{
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x0004F218 File Offset: 0x0004D418
		public static Trace CoreTracer
		{
			get
			{
				if (ExTraceGlobals.coreTracer == null)
				{
					ExTraceGlobals.coreTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.coreTracer;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x0004F236 File Offset: 0x0004D436
		public static Trace CoreCallTracer
		{
			get
			{
				if (ExTraceGlobals.coreCallTracer == null)
				{
					ExTraceGlobals.coreCallTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.coreCallTracer;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0004F254 File Offset: 0x0004D454
		public static Trace CoreDataTracer
		{
			get
			{
				if (ExTraceGlobals.coreDataTracer == null)
				{
					ExTraceGlobals.coreDataTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.coreDataTracer;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0004F272 File Offset: 0x0004D472
		public static Trace UserContextTracer
		{
			get
			{
				if (ExTraceGlobals.userContextTracer == null)
				{
					ExTraceGlobals.userContextTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.userContextTracer;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x0004F290 File Offset: 0x0004D490
		public static Trace UserContextCallTracer
		{
			get
			{
				if (ExTraceGlobals.userContextCallTracer == null)
				{
					ExTraceGlobals.userContextCallTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.userContextCallTracer;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x0004F2AE File Offset: 0x0004D4AE
		public static Trace UserContextDataTracer
		{
			get
			{
				if (ExTraceGlobals.userContextDataTracer == null)
				{
					ExTraceGlobals.userContextDataTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.userContextDataTracer;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x0004F2CC File Offset: 0x0004D4CC
		public static Trace OehTracer
		{
			get
			{
				if (ExTraceGlobals.oehTracer == null)
				{
					ExTraceGlobals.oehTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.oehTracer;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x0004F2EA File Offset: 0x0004D4EA
		public static Trace OehCallTracer
		{
			get
			{
				if (ExTraceGlobals.oehCallTracer == null)
				{
					ExTraceGlobals.oehCallTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.oehCallTracer;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x0004F308 File Offset: 0x0004D508
		public static Trace OehDataTracer
		{
			get
			{
				if (ExTraceGlobals.oehDataTracer == null)
				{
					ExTraceGlobals.oehDataTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.oehDataTracer;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x0004F326 File Offset: 0x0004D526
		public static Trace ThemesTracer
		{
			get
			{
				if (ExTraceGlobals.themesTracer == null)
				{
					ExTraceGlobals.themesTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.themesTracer;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x0004F345 File Offset: 0x0004D545
		public static Trace ThemesCallTracer
		{
			get
			{
				if (ExTraceGlobals.themesCallTracer == null)
				{
					ExTraceGlobals.themesCallTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.themesCallTracer;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x0004F364 File Offset: 0x0004D564
		public static Trace ThemesDataTracer
		{
			get
			{
				if (ExTraceGlobals.themesDataTracer == null)
				{
					ExTraceGlobals.themesDataTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.themesDataTracer;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x0004F383 File Offset: 0x0004D583
		public static Trace SmallIconTracer
		{
			get
			{
				if (ExTraceGlobals.smallIconTracer == null)
				{
					ExTraceGlobals.smallIconTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.smallIconTracer;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x0004F3A2 File Offset: 0x0004D5A2
		public static Trace SmallIconCallTracer
		{
			get
			{
				if (ExTraceGlobals.smallIconCallTracer == null)
				{
					ExTraceGlobals.smallIconCallTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.smallIconCallTracer;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x0004F3C1 File Offset: 0x0004D5C1
		public static Trace SmallIconDataTracer
		{
			get
			{
				if (ExTraceGlobals.smallIconDataTracer == null)
				{
					ExTraceGlobals.smallIconDataTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.smallIconDataTracer;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0004F3E0 File Offset: 0x0004D5E0
		public static Trace FormsRegistryCallTracer
		{
			get
			{
				if (ExTraceGlobals.formsRegistryCallTracer == null)
				{
					ExTraceGlobals.formsRegistryCallTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.formsRegistryCallTracer;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x0004F3FF File Offset: 0x0004D5FF
		public static Trace FormsRegistryDataTracer
		{
			get
			{
				if (ExTraceGlobals.formsRegistryDataTracer == null)
				{
					ExTraceGlobals.formsRegistryDataTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.formsRegistryDataTracer;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0004F41E File Offset: 0x0004D61E
		public static Trace FormsRegistryTracer
		{
			get
			{
				if (ExTraceGlobals.formsRegistryTracer == null)
				{
					ExTraceGlobals.formsRegistryTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.formsRegistryTracer;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x0004F43D File Offset: 0x0004D63D
		public static Trace UserOptionsTracer
		{
			get
			{
				if (ExTraceGlobals.userOptionsTracer == null)
				{
					ExTraceGlobals.userOptionsTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.userOptionsTracer;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0004F45C File Offset: 0x0004D65C
		public static Trace UserOptionsCallTracer
		{
			get
			{
				if (ExTraceGlobals.userOptionsCallTracer == null)
				{
					ExTraceGlobals.userOptionsCallTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.userOptionsCallTracer;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0004F47B File Offset: 0x0004D67B
		public static Trace UserOptionsDataTracer
		{
			get
			{
				if (ExTraceGlobals.userOptionsDataTracer == null)
				{
					ExTraceGlobals.userOptionsDataTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.userOptionsDataTracer;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x0004F49A File Offset: 0x0004D69A
		public static Trace MailTracer
		{
			get
			{
				if (ExTraceGlobals.mailTracer == null)
				{
					ExTraceGlobals.mailTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.mailTracer;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0004F4B9 File Offset: 0x0004D6B9
		public static Trace MailCallTracer
		{
			get
			{
				if (ExTraceGlobals.mailCallTracer == null)
				{
					ExTraceGlobals.mailCallTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.mailCallTracer;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0004F4D8 File Offset: 0x0004D6D8
		public static Trace MailDataTracer
		{
			get
			{
				if (ExTraceGlobals.mailDataTracer == null)
				{
					ExTraceGlobals.mailDataTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.mailDataTracer;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x0004F4F7 File Offset: 0x0004D6F7
		public static Trace CalendarTracer
		{
			get
			{
				if (ExTraceGlobals.calendarTracer == null)
				{
					ExTraceGlobals.calendarTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.calendarTracer;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0004F516 File Offset: 0x0004D716
		public static Trace CalendarCallTracer
		{
			get
			{
				if (ExTraceGlobals.calendarCallTracer == null)
				{
					ExTraceGlobals.calendarCallTracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.calendarCallTracer;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x0004F535 File Offset: 0x0004D735
		public static Trace CalendarDataTracer
		{
			get
			{
				if (ExTraceGlobals.calendarDataTracer == null)
				{
					ExTraceGlobals.calendarDataTracer = new Trace(ExTraceGlobals.componentGuid, 26);
				}
				return ExTraceGlobals.calendarDataTracer;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0004F554 File Offset: 0x0004D754
		public static Trace ContactsTracer
		{
			get
			{
				if (ExTraceGlobals.contactsTracer == null)
				{
					ExTraceGlobals.contactsTracer = new Trace(ExTraceGlobals.componentGuid, 27);
				}
				return ExTraceGlobals.contactsTracer;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x0004F573 File Offset: 0x0004D773
		public static Trace ContactsCallTracer
		{
			get
			{
				if (ExTraceGlobals.contactsCallTracer == null)
				{
					ExTraceGlobals.contactsCallTracer = new Trace(ExTraceGlobals.componentGuid, 28);
				}
				return ExTraceGlobals.contactsCallTracer;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0004F592 File Offset: 0x0004D792
		public static Trace ContactsDataTracer
		{
			get
			{
				if (ExTraceGlobals.contactsDataTracer == null)
				{
					ExTraceGlobals.contactsDataTracer = new Trace(ExTraceGlobals.componentGuid, 29);
				}
				return ExTraceGlobals.contactsDataTracer;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x0004F5B1 File Offset: 0x0004D7B1
		public static Trace DocumentsTracer
		{
			get
			{
				if (ExTraceGlobals.documentsTracer == null)
				{
					ExTraceGlobals.documentsTracer = new Trace(ExTraceGlobals.componentGuid, 30);
				}
				return ExTraceGlobals.documentsTracer;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x0004F5D0 File Offset: 0x0004D7D0
		public static Trace DocumentsCallTracer
		{
			get
			{
				if (ExTraceGlobals.documentsCallTracer == null)
				{
					ExTraceGlobals.documentsCallTracer = new Trace(ExTraceGlobals.componentGuid, 31);
				}
				return ExTraceGlobals.documentsCallTracer;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x0004F5EF File Offset: 0x0004D7EF
		public static Trace DocumentsDataTracer
		{
			get
			{
				if (ExTraceGlobals.documentsDataTracer == null)
				{
					ExTraceGlobals.documentsDataTracer = new Trace(ExTraceGlobals.componentGuid, 32);
				}
				return ExTraceGlobals.documentsDataTracer;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x0004F60E File Offset: 0x0004D80E
		public static Trace RequestTracer
		{
			get
			{
				if (ExTraceGlobals.requestTracer == null)
				{
					ExTraceGlobals.requestTracer = new Trace(ExTraceGlobals.componentGuid, 33);
				}
				return ExTraceGlobals.requestTracer;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x0004F62D File Offset: 0x0004D82D
		public static Trace PerformanceTracer
		{
			get
			{
				if (ExTraceGlobals.performanceTracer == null)
				{
					ExTraceGlobals.performanceTracer = new Trace(ExTraceGlobals.componentGuid, 34);
				}
				return ExTraceGlobals.performanceTracer;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x0004F64C File Offset: 0x0004D84C
		public static Trace DirectoryTracer
		{
			get
			{
				if (ExTraceGlobals.directoryTracer == null)
				{
					ExTraceGlobals.directoryTracer = new Trace(ExTraceGlobals.componentGuid, 35);
				}
				return ExTraceGlobals.directoryTracer;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x0004F66B File Offset: 0x0004D86B
		public static Trace DirectoryCallTracer
		{
			get
			{
				if (ExTraceGlobals.directoryCallTracer == null)
				{
					ExTraceGlobals.directoryCallTracer = new Trace(ExTraceGlobals.componentGuid, 36);
				}
				return ExTraceGlobals.directoryCallTracer;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x0004F68A File Offset: 0x0004D88A
		public static Trace ExceptionTracer
		{
			get
			{
				if (ExTraceGlobals.exceptionTracer == null)
				{
					ExTraceGlobals.exceptionTracer = new Trace(ExTraceGlobals.componentGuid, 37);
				}
				return ExTraceGlobals.exceptionTracer;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x0004F6A9 File Offset: 0x0004D8A9
		public static Trace UnifiedMessagingTracer
		{
			get
			{
				if (ExTraceGlobals.unifiedMessagingTracer == null)
				{
					ExTraceGlobals.unifiedMessagingTracer = new Trace(ExTraceGlobals.componentGuid, 38);
				}
				return ExTraceGlobals.unifiedMessagingTracer;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x0004F6C8 File Offset: 0x0004D8C8
		public static Trace TranscodingTracer
		{
			get
			{
				if (ExTraceGlobals.transcodingTracer == null)
				{
					ExTraceGlobals.transcodingTracer = new Trace(ExTraceGlobals.componentGuid, 39);
				}
				return ExTraceGlobals.transcodingTracer;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x0004F6E7 File Offset: 0x0004D8E7
		public static Trace NotificationsCallTracer
		{
			get
			{
				if (ExTraceGlobals.notificationsCallTracer == null)
				{
					ExTraceGlobals.notificationsCallTracer = new Trace(ExTraceGlobals.componentGuid, 40);
				}
				return ExTraceGlobals.notificationsCallTracer;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x0004F706 File Offset: 0x0004D906
		public static Trace SpellcheckCallTracer
		{
			get
			{
				if (ExTraceGlobals.spellcheckCallTracer == null)
				{
					ExTraceGlobals.spellcheckCallTracer = new Trace(ExTraceGlobals.componentGuid, 41);
				}
				return ExTraceGlobals.spellcheckCallTracer;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x0004F725 File Offset: 0x0004D925
		public static Trace ProxyCallTracer
		{
			get
			{
				if (ExTraceGlobals.proxyCallTracer == null)
				{
					ExTraceGlobals.proxyCallTracer = new Trace(ExTraceGlobals.componentGuid, 42);
				}
				return ExTraceGlobals.proxyCallTracer;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x0004F744 File Offset: 0x0004D944
		public static Trace ProxyTracer
		{
			get
			{
				if (ExTraceGlobals.proxyTracer == null)
				{
					ExTraceGlobals.proxyTracer = new Trace(ExTraceGlobals.componentGuid, 43);
				}
				return ExTraceGlobals.proxyTracer;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x0004F763 File Offset: 0x0004D963
		public static Trace ProxyDataTracer
		{
			get
			{
				if (ExTraceGlobals.proxyDataTracer == null)
				{
					ExTraceGlobals.proxyDataTracer = new Trace(ExTraceGlobals.componentGuid, 44);
				}
				return ExTraceGlobals.proxyDataTracer;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x0004F782 File Offset: 0x0004D982
		public static Trace ProxyRequestTracer
		{
			get
			{
				if (ExTraceGlobals.proxyRequestTracer == null)
				{
					ExTraceGlobals.proxyRequestTracer = new Trace(ExTraceGlobals.componentGuid, 45);
				}
				return ExTraceGlobals.proxyRequestTracer;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x0004F7A1 File Offset: 0x0004D9A1
		public static Trace TasksTracer
		{
			get
			{
				if (ExTraceGlobals.tasksTracer == null)
				{
					ExTraceGlobals.tasksTracer = new Trace(ExTraceGlobals.componentGuid, 46);
				}
				return ExTraceGlobals.tasksTracer;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x0004F7C0 File Offset: 0x0004D9C0
		public static Trace TasksCallTracer
		{
			get
			{
				if (ExTraceGlobals.tasksCallTracer == null)
				{
					ExTraceGlobals.tasksCallTracer = new Trace(ExTraceGlobals.componentGuid, 47);
				}
				return ExTraceGlobals.tasksCallTracer;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0004F7DF File Offset: 0x0004D9DF
		public static Trace TasksDataTracer
		{
			get
			{
				if (ExTraceGlobals.tasksDataTracer == null)
				{
					ExTraceGlobals.tasksDataTracer = new Trace(ExTraceGlobals.componentGuid, 48);
				}
				return ExTraceGlobals.tasksDataTracer;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x0004F7FE File Offset: 0x0004D9FE
		public static Trace WebPartRequestTracer
		{
			get
			{
				if (ExTraceGlobals.webPartRequestTracer == null)
				{
					ExTraceGlobals.webPartRequestTracer = new Trace(ExTraceGlobals.componentGuid, 49);
				}
				return ExTraceGlobals.webPartRequestTracer;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x0004F81D File Offset: 0x0004DA1D
		public static Trace ConfigurationManagerTracer
		{
			get
			{
				if (ExTraceGlobals.configurationManagerTracer == null)
				{
					ExTraceGlobals.configurationManagerTracer = new Trace(ExTraceGlobals.componentGuid, 50);
				}
				return ExTraceGlobals.configurationManagerTracer;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x0004F83C File Offset: 0x0004DA3C
		public static Trace ChangePasswordTracer
		{
			get
			{
				if (ExTraceGlobals.changePasswordTracer == null)
				{
					ExTraceGlobals.changePasswordTracer = new Trace(ExTraceGlobals.componentGuid, 51);
				}
				return ExTraceGlobals.changePasswordTracer;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x0004F85B File Offset: 0x0004DA5B
		public static Trace LiveIdAuthenticationModuleTracer
		{
			get
			{
				if (ExTraceGlobals.liveIdAuthenticationModuleTracer == null)
				{
					ExTraceGlobals.liveIdAuthenticationModuleTracer = new Trace(ExTraceGlobals.componentGuid, 52);
				}
				return ExTraceGlobals.liveIdAuthenticationModuleTracer;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x0004F87A File Offset: 0x0004DA7A
		public static Trace PolicyConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.policyConfigurationTracer == null)
				{
					ExTraceGlobals.policyConfigurationTracer = new Trace(ExTraceGlobals.componentGuid, 53);
				}
				return ExTraceGlobals.policyConfigurationTracer;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x0004F899 File Offset: 0x0004DA99
		public static Trace CoBrandingTracer
		{
			get
			{
				if (ExTraceGlobals.coBrandingTracer == null)
				{
					ExTraceGlobals.coBrandingTracer = new Trace(ExTraceGlobals.componentGuid, 54);
				}
				return ExTraceGlobals.coBrandingTracer;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x0004F8B8 File Offset: 0x0004DAB8
		public static Trace IrmTracer
		{
			get
			{
				if (ExTraceGlobals.irmTracer == null)
				{
					ExTraceGlobals.irmTracer = new Trace(ExTraceGlobals.componentGuid, 55);
				}
				return ExTraceGlobals.irmTracer;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x0004F8D7 File Offset: 0x0004DAD7
		public static Trace InstantMessagingTracer
		{
			get
			{
				if (ExTraceGlobals.instantMessagingTracer == null)
				{
					ExTraceGlobals.instantMessagingTracer = new Trace(ExTraceGlobals.componentGuid, 56);
				}
				return ExTraceGlobals.instantMessagingTracer;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x0004F8F6 File Offset: 0x0004DAF6
		public static Trace AttachmentHandlingTracer
		{
			get
			{
				if (ExTraceGlobals.attachmentHandlingTracer == null)
				{
					ExTraceGlobals.attachmentHandlingTracer = new Trace(ExTraceGlobals.componentGuid, 57);
				}
				return ExTraceGlobals.attachmentHandlingTracer;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x0004F915 File Offset: 0x0004DB15
		public static Trace SpeechRecognitionTracer
		{
			get
			{
				if (ExTraceGlobals.speechRecognitionTracer == null)
				{
					ExTraceGlobals.speechRecognitionTracer = new Trace(ExTraceGlobals.componentGuid, 58);
				}
				return ExTraceGlobals.speechRecognitionTracer;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x0004F934 File Offset: 0x0004DB34
		public static Trace AnonymousServiceCommandTracer
		{
			get
			{
				if (ExTraceGlobals.anonymousServiceCommandTracer == null)
				{
					ExTraceGlobals.anonymousServiceCommandTracer = new Trace(ExTraceGlobals.componentGuid, 59);
				}
				return ExTraceGlobals.anonymousServiceCommandTracer;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x0004F953 File Offset: 0x0004DB53
		public static Trace AppcacheManifestHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.appcacheManifestHandlerTracer == null)
				{
					ExTraceGlobals.appcacheManifestHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 60);
				}
				return ExTraceGlobals.appcacheManifestHandlerTracer;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0004F972 File Offset: 0x0004DB72
		public static Trace DefaultPageHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.defaultPageHandlerTracer == null)
				{
					ExTraceGlobals.defaultPageHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 61);
				}
				return ExTraceGlobals.defaultPageHandlerTracer;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x0004F991 File Offset: 0x0004DB91
		public static Trace GetPersonaPhotoTracer
		{
			get
			{
				if (ExTraceGlobals.getPersonaPhotoTracer == null)
				{
					ExTraceGlobals.getPersonaPhotoTracer = new Trace(ExTraceGlobals.componentGuid, 62);
				}
				return ExTraceGlobals.getPersonaPhotoTracer;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x0004F9B0 File Offset: 0x0004DBB0
		public static Trace MobileDevicePolicyTracer
		{
			get
			{
				if (ExTraceGlobals.mobileDevicePolicyTracer == null)
				{
					ExTraceGlobals.mobileDevicePolicyTracer = new Trace(ExTraceGlobals.componentGuid, 63);
				}
				return ExTraceGlobals.mobileDevicePolicyTracer;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x0004F9CF File Offset: 0x0004DBCF
		public static Trace GetPersonaOrganizationHierarchyTracer
		{
			get
			{
				if (ExTraceGlobals.getPersonaOrganizationHierarchyTracer == null)
				{
					ExTraceGlobals.getPersonaOrganizationHierarchyTracer = new Trace(ExTraceGlobals.componentGuid, 64);
				}
				return ExTraceGlobals.getPersonaOrganizationHierarchyTracer;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x0004F9EE File Offset: 0x0004DBEE
		public static Trace GetGroupTracer
		{
			get
			{
				if (ExTraceGlobals.getGroupTracer == null)
				{
					ExTraceGlobals.getGroupTracer = new Trace(ExTraceGlobals.componentGuid, 65);
				}
				return ExTraceGlobals.getGroupTracer;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x0004FA0D File Offset: 0x0004DC0D
		public static Trace ConnectedAccountsTracer
		{
			get
			{
				if (ExTraceGlobals.connectedAccountsTracer == null)
				{
					ExTraceGlobals.connectedAccountsTracer = new Trace(ExTraceGlobals.componentGuid, 66);
				}
				return ExTraceGlobals.connectedAccountsTracer;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0004FA2C File Offset: 0x0004DC2C
		public static Trace AppWipeTracer
		{
			get
			{
				if (ExTraceGlobals.appWipeTracer == null)
				{
					ExTraceGlobals.appWipeTracer = new Trace(ExTraceGlobals.componentGuid, 67);
				}
				return ExTraceGlobals.appWipeTracer;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x0004FA4B File Offset: 0x0004DC4B
		public static Trace OnlineMeetingTracer
		{
			get
			{
				if (ExTraceGlobals.onlineMeetingTracer == null)
				{
					ExTraceGlobals.onlineMeetingTracer = new Trace(ExTraceGlobals.componentGuid, 68);
				}
				return ExTraceGlobals.onlineMeetingTracer;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0004FA6A File Offset: 0x0004DC6A
		public static Trace InlineExploreTracer
		{
			get
			{
				if (ExTraceGlobals.inlineExploreTracer == null)
				{
					ExTraceGlobals.inlineExploreTracer = new Trace(ExTraceGlobals.componentGuid, 69);
				}
				return ExTraceGlobals.inlineExploreTracer;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0004FA89 File Offset: 0x0004DC89
		public static Trace RemoveFavoriteTracer
		{
			get
			{
				if (ExTraceGlobals.removeFavoriteTracer == null)
				{
					ExTraceGlobals.removeFavoriteTracer = new Trace(ExTraceGlobals.componentGuid, 70);
				}
				return ExTraceGlobals.removeFavoriteTracer;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x0004FAA8 File Offset: 0x0004DCA8
		public static Trace CobaltTracer
		{
			get
			{
				if (ExTraceGlobals.cobaltTracer == null)
				{
					ExTraceGlobals.cobaltTracer = new Trace(ExTraceGlobals.componentGuid, 71);
				}
				return ExTraceGlobals.cobaltTracer;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x0004FAC7 File Offset: 0x0004DCC7
		public static Trace PeopleIKnowNotificationsTracer
		{
			get
			{
				if (ExTraceGlobals.peopleIKnowNotificationsTracer == null)
				{
					ExTraceGlobals.peopleIKnowNotificationsTracer = new Trace(ExTraceGlobals.componentGuid, 72);
				}
				return ExTraceGlobals.peopleIKnowNotificationsTracer;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x0004FAE6 File Offset: 0x0004DCE6
		public static Trace OwaPerTenantCacheTracer
		{
			get
			{
				if (ExTraceGlobals.owaPerTenantCacheTracer == null)
				{
					ExTraceGlobals.owaPerTenantCacheTracer = new Trace(ExTraceGlobals.componentGuid, 73);
				}
				return ExTraceGlobals.owaPerTenantCacheTracer;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x0004FB05 File Offset: 0x0004DD05
		public static Trace AdfsAuthModuleTracer
		{
			get
			{
				if (ExTraceGlobals.adfsAuthModuleTracer == null)
				{
					ExTraceGlobals.adfsAuthModuleTracer = new Trace(ExTraceGlobals.componentGuid, 74);
				}
				return ExTraceGlobals.adfsAuthModuleTracer;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x0004FB24 File Offset: 0x0004DD24
		public static Trace SessionDataHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.sessionDataHandlerTracer == null)
				{
					ExTraceGlobals.sessionDataHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 75);
				}
				return ExTraceGlobals.sessionDataHandlerTracer;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x0004FB43 File Offset: 0x0004DD43
		public static Trace GetPersonaNotesTracer
		{
			get
			{
				if (ExTraceGlobals.getPersonaNotesTracer == null)
				{
					ExTraceGlobals.getPersonaNotesTracer = new Trace(ExTraceGlobals.componentGuid, 76);
				}
				return ExTraceGlobals.getPersonaNotesTracer;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x0004FB62 File Offset: 0x0004DD62
		public static Trace CryptoTracer
		{
			get
			{
				if (ExTraceGlobals.cryptoTracer == null)
				{
					ExTraceGlobals.cryptoTracer = new Trace(ExTraceGlobals.componentGuid, 77);
				}
				return ExTraceGlobals.cryptoTracer;
			}
		}

		// Token: 0x04001717 RID: 5911
		private static Guid componentGuid = new Guid("1758fd24-1153-4624-96f6-742b18fc0372");

		// Token: 0x04001718 RID: 5912
		private static Trace coreTracer = null;

		// Token: 0x04001719 RID: 5913
		private static Trace coreCallTracer = null;

		// Token: 0x0400171A RID: 5914
		private static Trace coreDataTracer = null;

		// Token: 0x0400171B RID: 5915
		private static Trace userContextTracer = null;

		// Token: 0x0400171C RID: 5916
		private static Trace userContextCallTracer = null;

		// Token: 0x0400171D RID: 5917
		private static Trace userContextDataTracer = null;

		// Token: 0x0400171E RID: 5918
		private static Trace oehTracer = null;

		// Token: 0x0400171F RID: 5919
		private static Trace oehCallTracer = null;

		// Token: 0x04001720 RID: 5920
		private static Trace oehDataTracer = null;

		// Token: 0x04001721 RID: 5921
		private static Trace themesTracer = null;

		// Token: 0x04001722 RID: 5922
		private static Trace themesCallTracer = null;

		// Token: 0x04001723 RID: 5923
		private static Trace themesDataTracer = null;

		// Token: 0x04001724 RID: 5924
		private static Trace smallIconTracer = null;

		// Token: 0x04001725 RID: 5925
		private static Trace smallIconCallTracer = null;

		// Token: 0x04001726 RID: 5926
		private static Trace smallIconDataTracer = null;

		// Token: 0x04001727 RID: 5927
		private static Trace formsRegistryCallTracer = null;

		// Token: 0x04001728 RID: 5928
		private static Trace formsRegistryDataTracer = null;

		// Token: 0x04001729 RID: 5929
		private static Trace formsRegistryTracer = null;

		// Token: 0x0400172A RID: 5930
		private static Trace userOptionsTracer = null;

		// Token: 0x0400172B RID: 5931
		private static Trace userOptionsCallTracer = null;

		// Token: 0x0400172C RID: 5932
		private static Trace userOptionsDataTracer = null;

		// Token: 0x0400172D RID: 5933
		private static Trace mailTracer = null;

		// Token: 0x0400172E RID: 5934
		private static Trace mailCallTracer = null;

		// Token: 0x0400172F RID: 5935
		private static Trace mailDataTracer = null;

		// Token: 0x04001730 RID: 5936
		private static Trace calendarTracer = null;

		// Token: 0x04001731 RID: 5937
		private static Trace calendarCallTracer = null;

		// Token: 0x04001732 RID: 5938
		private static Trace calendarDataTracer = null;

		// Token: 0x04001733 RID: 5939
		private static Trace contactsTracer = null;

		// Token: 0x04001734 RID: 5940
		private static Trace contactsCallTracer = null;

		// Token: 0x04001735 RID: 5941
		private static Trace contactsDataTracer = null;

		// Token: 0x04001736 RID: 5942
		private static Trace documentsTracer = null;

		// Token: 0x04001737 RID: 5943
		private static Trace documentsCallTracer = null;

		// Token: 0x04001738 RID: 5944
		private static Trace documentsDataTracer = null;

		// Token: 0x04001739 RID: 5945
		private static Trace requestTracer = null;

		// Token: 0x0400173A RID: 5946
		private static Trace performanceTracer = null;

		// Token: 0x0400173B RID: 5947
		private static Trace directoryTracer = null;

		// Token: 0x0400173C RID: 5948
		private static Trace directoryCallTracer = null;

		// Token: 0x0400173D RID: 5949
		private static Trace exceptionTracer = null;

		// Token: 0x0400173E RID: 5950
		private static Trace unifiedMessagingTracer = null;

		// Token: 0x0400173F RID: 5951
		private static Trace transcodingTracer = null;

		// Token: 0x04001740 RID: 5952
		private static Trace notificationsCallTracer = null;

		// Token: 0x04001741 RID: 5953
		private static Trace spellcheckCallTracer = null;

		// Token: 0x04001742 RID: 5954
		private static Trace proxyCallTracer = null;

		// Token: 0x04001743 RID: 5955
		private static Trace proxyTracer = null;

		// Token: 0x04001744 RID: 5956
		private static Trace proxyDataTracer = null;

		// Token: 0x04001745 RID: 5957
		private static Trace proxyRequestTracer = null;

		// Token: 0x04001746 RID: 5958
		private static Trace tasksTracer = null;

		// Token: 0x04001747 RID: 5959
		private static Trace tasksCallTracer = null;

		// Token: 0x04001748 RID: 5960
		private static Trace tasksDataTracer = null;

		// Token: 0x04001749 RID: 5961
		private static Trace webPartRequestTracer = null;

		// Token: 0x0400174A RID: 5962
		private static Trace configurationManagerTracer = null;

		// Token: 0x0400174B RID: 5963
		private static Trace changePasswordTracer = null;

		// Token: 0x0400174C RID: 5964
		private static Trace liveIdAuthenticationModuleTracer = null;

		// Token: 0x0400174D RID: 5965
		private static Trace policyConfigurationTracer = null;

		// Token: 0x0400174E RID: 5966
		private static Trace coBrandingTracer = null;

		// Token: 0x0400174F RID: 5967
		private static Trace irmTracer = null;

		// Token: 0x04001750 RID: 5968
		private static Trace instantMessagingTracer = null;

		// Token: 0x04001751 RID: 5969
		private static Trace attachmentHandlingTracer = null;

		// Token: 0x04001752 RID: 5970
		private static Trace speechRecognitionTracer = null;

		// Token: 0x04001753 RID: 5971
		private static Trace anonymousServiceCommandTracer = null;

		// Token: 0x04001754 RID: 5972
		private static Trace appcacheManifestHandlerTracer = null;

		// Token: 0x04001755 RID: 5973
		private static Trace defaultPageHandlerTracer = null;

		// Token: 0x04001756 RID: 5974
		private static Trace getPersonaPhotoTracer = null;

		// Token: 0x04001757 RID: 5975
		private static Trace mobileDevicePolicyTracer = null;

		// Token: 0x04001758 RID: 5976
		private static Trace getPersonaOrganizationHierarchyTracer = null;

		// Token: 0x04001759 RID: 5977
		private static Trace getGroupTracer = null;

		// Token: 0x0400175A RID: 5978
		private static Trace connectedAccountsTracer = null;

		// Token: 0x0400175B RID: 5979
		private static Trace appWipeTracer = null;

		// Token: 0x0400175C RID: 5980
		private static Trace onlineMeetingTracer = null;

		// Token: 0x0400175D RID: 5981
		private static Trace inlineExploreTracer = null;

		// Token: 0x0400175E RID: 5982
		private static Trace removeFavoriteTracer = null;

		// Token: 0x0400175F RID: 5983
		private static Trace cobaltTracer = null;

		// Token: 0x04001760 RID: 5984
		private static Trace peopleIKnowNotificationsTracer = null;

		// Token: 0x04001761 RID: 5985
		private static Trace owaPerTenantCacheTracer = null;

		// Token: 0x04001762 RID: 5986
		private static Trace adfsAuthModuleTracer = null;

		// Token: 0x04001763 RID: 5987
		private static Trace sessionDataHandlerTracer = null;

		// Token: 0x04001764 RID: 5988
		private static Trace getPersonaNotesTracer = null;

		// Token: 0x04001765 RID: 5989
		private static Trace cryptoTracer = null;
	}
}
