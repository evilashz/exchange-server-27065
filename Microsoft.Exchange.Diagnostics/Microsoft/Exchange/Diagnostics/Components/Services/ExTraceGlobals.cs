using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Services
{
	// Token: 0x02000334 RID: 820
	public static class ExTraceGlobals
	{
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x0004DB8E File Offset: 0x0004BD8E
		public static Trace CalendarAlgorithmTracer
		{
			get
			{
				if (ExTraceGlobals.calendarAlgorithmTracer == null)
				{
					ExTraceGlobals.calendarAlgorithmTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.calendarAlgorithmTracer;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x0004DBAC File Offset: 0x0004BDAC
		public static Trace CalendarDataTracer
		{
			get
			{
				if (ExTraceGlobals.calendarDataTracer == null)
				{
					ExTraceGlobals.calendarDataTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.calendarDataTracer;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0004DBCA File Offset: 0x0004BDCA
		public static Trace CalendarCallTracer
		{
			get
			{
				if (ExTraceGlobals.calendarCallTracer == null)
				{
					ExTraceGlobals.calendarCallTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.calendarCallTracer;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x0004DBE8 File Offset: 0x0004BDE8
		public static Trace CommonAlgorithmTracer
		{
			get
			{
				if (ExTraceGlobals.commonAlgorithmTracer == null)
				{
					ExTraceGlobals.commonAlgorithmTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.commonAlgorithmTracer;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0004DC06 File Offset: 0x0004BE06
		public static Trace FolderAlgorithmTracer
		{
			get
			{
				if (ExTraceGlobals.folderAlgorithmTracer == null)
				{
					ExTraceGlobals.folderAlgorithmTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.folderAlgorithmTracer;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0004DC24 File Offset: 0x0004BE24
		public static Trace FolderDataTracer
		{
			get
			{
				if (ExTraceGlobals.folderDataTracer == null)
				{
					ExTraceGlobals.folderDataTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.folderDataTracer;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0004DC42 File Offset: 0x0004BE42
		public static Trace FolderCallTracer
		{
			get
			{
				if (ExTraceGlobals.folderCallTracer == null)
				{
					ExTraceGlobals.folderCallTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.folderCallTracer;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x0004DC60 File Offset: 0x0004BE60
		public static Trace ItemAlgorithmTracer
		{
			get
			{
				if (ExTraceGlobals.itemAlgorithmTracer == null)
				{
					ExTraceGlobals.itemAlgorithmTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.itemAlgorithmTracer;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0004DC7E File Offset: 0x0004BE7E
		public static Trace ItemDataTracer
		{
			get
			{
				if (ExTraceGlobals.itemDataTracer == null)
				{
					ExTraceGlobals.itemDataTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.itemDataTracer;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x0004DC9C File Offset: 0x0004BE9C
		public static Trace ItemCallTracer
		{
			get
			{
				if (ExTraceGlobals.itemCallTracer == null)
				{
					ExTraceGlobals.itemCallTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.itemCallTracer;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0004DCBB File Offset: 0x0004BEBB
		public static Trace ExceptionTracer
		{
			get
			{
				if (ExTraceGlobals.exceptionTracer == null)
				{
					ExTraceGlobals.exceptionTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.exceptionTracer;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x0004DCDA File Offset: 0x0004BEDA
		public static Trace SessionCacheTracer
		{
			get
			{
				if (ExTraceGlobals.sessionCacheTracer == null)
				{
					ExTraceGlobals.sessionCacheTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.sessionCacheTracer;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x0004DCF9 File Offset: 0x0004BEF9
		public static Trace ExchangePrincipalCacheTracer
		{
			get
			{
				if (ExTraceGlobals.exchangePrincipalCacheTracer == null)
				{
					ExTraceGlobals.exchangePrincipalCacheTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.exchangePrincipalCacheTracer;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x0004DD18 File Offset: 0x0004BF18
		public static Trace SearchTracer
		{
			get
			{
				if (ExTraceGlobals.searchTracer == null)
				{
					ExTraceGlobals.searchTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.searchTracer;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x0004DD37 File Offset: 0x0004BF37
		public static Trace UtilAlgorithmTracer
		{
			get
			{
				if (ExTraceGlobals.utilAlgorithmTracer == null)
				{
					ExTraceGlobals.utilAlgorithmTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.utilAlgorithmTracer;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0004DD56 File Offset: 0x0004BF56
		public static Trace UtilDataTracer
		{
			get
			{
				if (ExTraceGlobals.utilDataTracer == null)
				{
					ExTraceGlobals.utilDataTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.utilDataTracer;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x0004DD75 File Offset: 0x0004BF75
		public static Trace UtilCallTracer
		{
			get
			{
				if (ExTraceGlobals.utilCallTracer == null)
				{
					ExTraceGlobals.utilCallTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.utilCallTracer;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0004DD94 File Offset: 0x0004BF94
		public static Trace ServerToServerAuthZTracer
		{
			get
			{
				if (ExTraceGlobals.serverToServerAuthZTracer == null)
				{
					ExTraceGlobals.serverToServerAuthZTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.serverToServerAuthZTracer;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x0004DDB3 File Offset: 0x0004BFB3
		public static Trace ServiceCommandBaseCallTracer
		{
			get
			{
				if (ExTraceGlobals.serviceCommandBaseCallTracer == null)
				{
					ExTraceGlobals.serviceCommandBaseCallTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.serviceCommandBaseCallTracer;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0004DDD2 File Offset: 0x0004BFD2
		public static Trace ServiceCommandBaseDataTracer
		{
			get
			{
				if (ExTraceGlobals.serviceCommandBaseDataTracer == null)
				{
					ExTraceGlobals.serviceCommandBaseDataTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.serviceCommandBaseDataTracer;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x0004DDF1 File Offset: 0x0004BFF1
		public static Trace FacadeBaseCallTracer
		{
			get
			{
				if (ExTraceGlobals.facadeBaseCallTracer == null)
				{
					ExTraceGlobals.facadeBaseCallTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.facadeBaseCallTracer;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0004DE10 File Offset: 0x0004C010
		public static Trace CreateItemCallTracer
		{
			get
			{
				if (ExTraceGlobals.createItemCallTracer == null)
				{
					ExTraceGlobals.createItemCallTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.createItemCallTracer;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x0004DE2F File Offset: 0x0004C02F
		public static Trace GetItemCallTracer
		{
			get
			{
				if (ExTraceGlobals.getItemCallTracer == null)
				{
					ExTraceGlobals.getItemCallTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.getItemCallTracer;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x0004DE4E File Offset: 0x0004C04E
		public static Trace UpdateItemCallTracer
		{
			get
			{
				if (ExTraceGlobals.updateItemCallTracer == null)
				{
					ExTraceGlobals.updateItemCallTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.updateItemCallTracer;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x0004DE6D File Offset: 0x0004C06D
		public static Trace DeleteItemCallTracer
		{
			get
			{
				if (ExTraceGlobals.deleteItemCallTracer == null)
				{
					ExTraceGlobals.deleteItemCallTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.deleteItemCallTracer;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0004DE8C File Offset: 0x0004C08C
		public static Trace SendItemCallTracer
		{
			get
			{
				if (ExTraceGlobals.sendItemCallTracer == null)
				{
					ExTraceGlobals.sendItemCallTracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.sendItemCallTracer;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0004DEAB File Offset: 0x0004C0AB
		public static Trace MoveCopyCommandBaseCallTracer
		{
			get
			{
				if (ExTraceGlobals.moveCopyCommandBaseCallTracer == null)
				{
					ExTraceGlobals.moveCopyCommandBaseCallTracer = new Trace(ExTraceGlobals.componentGuid, 26);
				}
				return ExTraceGlobals.moveCopyCommandBaseCallTracer;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0004DECA File Offset: 0x0004C0CA
		public static Trace MoveCopyItemCommandBaseCallTracer
		{
			get
			{
				if (ExTraceGlobals.moveCopyItemCommandBaseCallTracer == null)
				{
					ExTraceGlobals.moveCopyItemCommandBaseCallTracer = new Trace(ExTraceGlobals.componentGuid, 27);
				}
				return ExTraceGlobals.moveCopyItemCommandBaseCallTracer;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x0004DEE9 File Offset: 0x0004C0E9
		public static Trace CopyItemCallTracer
		{
			get
			{
				if (ExTraceGlobals.copyItemCallTracer == null)
				{
					ExTraceGlobals.copyItemCallTracer = new Trace(ExTraceGlobals.componentGuid, 28);
				}
				return ExTraceGlobals.copyItemCallTracer;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0004DF08 File Offset: 0x0004C108
		public static Trace MoveItemCallTracer
		{
			get
			{
				if (ExTraceGlobals.moveItemCallTracer == null)
				{
					ExTraceGlobals.moveItemCallTracer = new Trace(ExTraceGlobals.componentGuid, 29);
				}
				return ExTraceGlobals.moveItemCallTracer;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x0004DF27 File Offset: 0x0004C127
		public static Trace CreateFolderCallTracer
		{
			get
			{
				if (ExTraceGlobals.createFolderCallTracer == null)
				{
					ExTraceGlobals.createFolderCallTracer = new Trace(ExTraceGlobals.componentGuid, 30);
				}
				return ExTraceGlobals.createFolderCallTracer;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x0004DF46 File Offset: 0x0004C146
		public static Trace GetFolderCallTracer
		{
			get
			{
				if (ExTraceGlobals.getFolderCallTracer == null)
				{
					ExTraceGlobals.getFolderCallTracer = new Trace(ExTraceGlobals.componentGuid, 31);
				}
				return ExTraceGlobals.getFolderCallTracer;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x0004DF65 File Offset: 0x0004C165
		public static Trace UpdateFolderCallTracer
		{
			get
			{
				if (ExTraceGlobals.updateFolderCallTracer == null)
				{
					ExTraceGlobals.updateFolderCallTracer = new Trace(ExTraceGlobals.componentGuid, 32);
				}
				return ExTraceGlobals.updateFolderCallTracer;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0004DF84 File Offset: 0x0004C184
		public static Trace DeleteFolderCallTracer
		{
			get
			{
				if (ExTraceGlobals.deleteFolderCallTracer == null)
				{
					ExTraceGlobals.deleteFolderCallTracer = new Trace(ExTraceGlobals.componentGuid, 33);
				}
				return ExTraceGlobals.deleteFolderCallTracer;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0004DFA3 File Offset: 0x0004C1A3
		public static Trace MoveCopyFolderCommandBaseCallTracer
		{
			get
			{
				if (ExTraceGlobals.moveCopyFolderCommandBaseCallTracer == null)
				{
					ExTraceGlobals.moveCopyFolderCommandBaseCallTracer = new Trace(ExTraceGlobals.componentGuid, 34);
				}
				return ExTraceGlobals.moveCopyFolderCommandBaseCallTracer;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0004DFC2 File Offset: 0x0004C1C2
		public static Trace CopyFolderCallTracer
		{
			get
			{
				if (ExTraceGlobals.copyFolderCallTracer == null)
				{
					ExTraceGlobals.copyFolderCallTracer = new Trace(ExTraceGlobals.componentGuid, 35);
				}
				return ExTraceGlobals.copyFolderCallTracer;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0004DFE1 File Offset: 0x0004C1E1
		public static Trace MoveFolderCallTracer
		{
			get
			{
				if (ExTraceGlobals.moveFolderCallTracer == null)
				{
					ExTraceGlobals.moveFolderCallTracer = new Trace(ExTraceGlobals.componentGuid, 36);
				}
				return ExTraceGlobals.moveFolderCallTracer;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0004E000 File Offset: 0x0004C200
		public static Trace FindCommandBaseCallTracer
		{
			get
			{
				if (ExTraceGlobals.findCommandBaseCallTracer == null)
				{
					ExTraceGlobals.findCommandBaseCallTracer = new Trace(ExTraceGlobals.componentGuid, 37);
				}
				return ExTraceGlobals.findCommandBaseCallTracer;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x0004E01F File Offset: 0x0004C21F
		public static Trace FindItemCallTracer
		{
			get
			{
				if (ExTraceGlobals.findItemCallTracer == null)
				{
					ExTraceGlobals.findItemCallTracer = new Trace(ExTraceGlobals.componentGuid, 38);
				}
				return ExTraceGlobals.findItemCallTracer;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0004E03E File Offset: 0x0004C23E
		public static Trace FindFolderCallTracer
		{
			get
			{
				if (ExTraceGlobals.findFolderCallTracer == null)
				{
					ExTraceGlobals.findFolderCallTracer = new Trace(ExTraceGlobals.componentGuid, 39);
				}
				return ExTraceGlobals.findFolderCallTracer;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0004E05D File Offset: 0x0004C25D
		public static Trace UtilCommandBaseCallTracer
		{
			get
			{
				if (ExTraceGlobals.utilCommandBaseCallTracer == null)
				{
					ExTraceGlobals.utilCommandBaseCallTracer = new Trace(ExTraceGlobals.componentGuid, 40);
				}
				return ExTraceGlobals.utilCommandBaseCallTracer;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0004E07C File Offset: 0x0004C27C
		public static Trace ExpandDLCallTracer
		{
			get
			{
				if (ExTraceGlobals.expandDLCallTracer == null)
				{
					ExTraceGlobals.expandDLCallTracer = new Trace(ExTraceGlobals.componentGuid, 41);
				}
				return ExTraceGlobals.expandDLCallTracer;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0004E09B File Offset: 0x0004C29B
		public static Trace ResolveNamesCallTracer
		{
			get
			{
				if (ExTraceGlobals.resolveNamesCallTracer == null)
				{
					ExTraceGlobals.resolveNamesCallTracer = new Trace(ExTraceGlobals.componentGuid, 42);
				}
				return ExTraceGlobals.resolveNamesCallTracer;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0004E0BA File Offset: 0x0004C2BA
		public static Trace SubscribeCallTracer
		{
			get
			{
				if (ExTraceGlobals.subscribeCallTracer == null)
				{
					ExTraceGlobals.subscribeCallTracer = new Trace(ExTraceGlobals.componentGuid, 43);
				}
				return ExTraceGlobals.subscribeCallTracer;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x0004E0D9 File Offset: 0x0004C2D9
		public static Trace UnsubscribeCallTracer
		{
			get
			{
				if (ExTraceGlobals.unsubscribeCallTracer == null)
				{
					ExTraceGlobals.unsubscribeCallTracer = new Trace(ExTraceGlobals.componentGuid, 44);
				}
				return ExTraceGlobals.unsubscribeCallTracer;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0004E0F8 File Offset: 0x0004C2F8
		public static Trace GetEventsCallTracer
		{
			get
			{
				if (ExTraceGlobals.getEventsCallTracer == null)
				{
					ExTraceGlobals.getEventsCallTracer = new Trace(ExTraceGlobals.componentGuid, 45);
				}
				return ExTraceGlobals.getEventsCallTracer;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x0004E117 File Offset: 0x0004C317
		public static Trace SubscriptionsTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionsTracer == null)
				{
					ExTraceGlobals.subscriptionsTracer = new Trace(ExTraceGlobals.componentGuid, 46);
				}
				return ExTraceGlobals.subscriptionsTracer;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0004E136 File Offset: 0x0004C336
		public static Trace SubscriptionBaseTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionBaseTracer == null)
				{
					ExTraceGlobals.subscriptionBaseTracer = new Trace(ExTraceGlobals.componentGuid, 47);
				}
				return ExTraceGlobals.subscriptionBaseTracer;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x0004E155 File Offset: 0x0004C355
		public static Trace PushSubscriptionTracer
		{
			get
			{
				if (ExTraceGlobals.pushSubscriptionTracer == null)
				{
					ExTraceGlobals.pushSubscriptionTracer = new Trace(ExTraceGlobals.componentGuid, 48);
				}
				return ExTraceGlobals.pushSubscriptionTracer;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0004E174 File Offset: 0x0004C374
		public static Trace SyncFolderHierarchyCallTracer
		{
			get
			{
				if (ExTraceGlobals.syncFolderHierarchyCallTracer == null)
				{
					ExTraceGlobals.syncFolderHierarchyCallTracer = new Trace(ExTraceGlobals.componentGuid, 49);
				}
				return ExTraceGlobals.syncFolderHierarchyCallTracer;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x0004E193 File Offset: 0x0004C393
		public static Trace SyncFolderItemsCallTracer
		{
			get
			{
				if (ExTraceGlobals.syncFolderItemsCallTracer == null)
				{
					ExTraceGlobals.syncFolderItemsCallTracer = new Trace(ExTraceGlobals.componentGuid, 50);
				}
				return ExTraceGlobals.syncFolderItemsCallTracer;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0004E1B2 File Offset: 0x0004C3B2
		public static Trace SynchronizationTracer
		{
			get
			{
				if (ExTraceGlobals.synchronizationTracer == null)
				{
					ExTraceGlobals.synchronizationTracer = new Trace(ExTraceGlobals.componentGuid, 51);
				}
				return ExTraceGlobals.synchronizationTracer;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x0004E1D1 File Offset: 0x0004C3D1
		public static Trace PerformanceMonitorTracer
		{
			get
			{
				if (ExTraceGlobals.performanceMonitorTracer == null)
				{
					ExTraceGlobals.performanceMonitorTracer = new Trace(ExTraceGlobals.componentGuid, 52);
				}
				return ExTraceGlobals.performanceMonitorTracer;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0004E1F0 File Offset: 0x0004C3F0
		public static Trace ConvertIdCallTracer
		{
			get
			{
				if (ExTraceGlobals.convertIdCallTracer == null)
				{
					ExTraceGlobals.convertIdCallTracer = new Trace(ExTraceGlobals.componentGuid, 53);
				}
				return ExTraceGlobals.convertIdCallTracer;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x0004E20F File Offset: 0x0004C40F
		public static Trace GetDelegateCallTracer
		{
			get
			{
				if (ExTraceGlobals.getDelegateCallTracer == null)
				{
					ExTraceGlobals.getDelegateCallTracer = new Trace(ExTraceGlobals.componentGuid, 54);
				}
				return ExTraceGlobals.getDelegateCallTracer;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0004E22E File Offset: 0x0004C42E
		public static Trace AddDelegateCallTracer
		{
			get
			{
				if (ExTraceGlobals.addDelegateCallTracer == null)
				{
					ExTraceGlobals.addDelegateCallTracer = new Trace(ExTraceGlobals.componentGuid, 55);
				}
				return ExTraceGlobals.addDelegateCallTracer;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x0004E24D File Offset: 0x0004C44D
		public static Trace RemoveDelegateCallTracer
		{
			get
			{
				if (ExTraceGlobals.removeDelegateCallTracer == null)
				{
					ExTraceGlobals.removeDelegateCallTracer = new Trace(ExTraceGlobals.componentGuid, 56);
				}
				return ExTraceGlobals.removeDelegateCallTracer;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0004E26C File Offset: 0x0004C46C
		public static Trace UpdateDelegateCallTracer
		{
			get
			{
				if (ExTraceGlobals.updateDelegateCallTracer == null)
				{
					ExTraceGlobals.updateDelegateCallTracer = new Trace(ExTraceGlobals.componentGuid, 57);
				}
				return ExTraceGlobals.updateDelegateCallTracer;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x0004E28B File Offset: 0x0004C48B
		public static Trace ProxyEvaluatorTracer
		{
			get
			{
				if (ExTraceGlobals.proxyEvaluatorTracer == null)
				{
					ExTraceGlobals.proxyEvaluatorTracer = new Trace(ExTraceGlobals.componentGuid, 58);
				}
				return ExTraceGlobals.proxyEvaluatorTracer;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0004E2AA File Offset: 0x0004C4AA
		public static Trace GetMailTipsCallTracer
		{
			get
			{
				if (ExTraceGlobals.getMailTipsCallTracer == null)
				{
					ExTraceGlobals.getMailTipsCallTracer = new Trace(ExTraceGlobals.componentGuid, 60);
				}
				return ExTraceGlobals.getMailTipsCallTracer;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x0004E2C9 File Offset: 0x0004C4C9
		public static Trace AllRequestsTracer
		{
			get
			{
				if (ExTraceGlobals.allRequestsTracer == null)
				{
					ExTraceGlobals.allRequestsTracer = new Trace(ExTraceGlobals.componentGuid, 61);
				}
				return ExTraceGlobals.allRequestsTracer;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0004E2E8 File Offset: 0x0004C4E8
		public static Trace AuthenticationTracer
		{
			get
			{
				if (ExTraceGlobals.authenticationTracer == null)
				{
					ExTraceGlobals.authenticationTracer = new Trace(ExTraceGlobals.componentGuid, 62);
				}
				return ExTraceGlobals.authenticationTracer;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x0004E307 File Offset: 0x0004C507
		public static Trace WCFTracer
		{
			get
			{
				if (ExTraceGlobals.wCFTracer == null)
				{
					ExTraceGlobals.wCFTracer = new Trace(ExTraceGlobals.componentGuid, 63);
				}
				return ExTraceGlobals.wCFTracer;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0004E326 File Offset: 0x0004C526
		public static Trace GetUserConfigurationCallTracer
		{
			get
			{
				if (ExTraceGlobals.getUserConfigurationCallTracer == null)
				{
					ExTraceGlobals.getUserConfigurationCallTracer = new Trace(ExTraceGlobals.componentGuid, 64);
				}
				return ExTraceGlobals.getUserConfigurationCallTracer;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0004E345 File Offset: 0x0004C545
		public static Trace CreateUserConfigurationCallTracer
		{
			get
			{
				if (ExTraceGlobals.createUserConfigurationCallTracer == null)
				{
					ExTraceGlobals.createUserConfigurationCallTracer = new Trace(ExTraceGlobals.componentGuid, 65);
				}
				return ExTraceGlobals.createUserConfigurationCallTracer;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0004E364 File Offset: 0x0004C564
		public static Trace DeleteUserConfigurationCallTracer
		{
			get
			{
				if (ExTraceGlobals.deleteUserConfigurationCallTracer == null)
				{
					ExTraceGlobals.deleteUserConfigurationCallTracer = new Trace(ExTraceGlobals.componentGuid, 66);
				}
				return ExTraceGlobals.deleteUserConfigurationCallTracer;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0004E383 File Offset: 0x0004C583
		public static Trace UpdateUserConfigurationCallTracer
		{
			get
			{
				if (ExTraceGlobals.updateUserConfigurationCallTracer == null)
				{
					ExTraceGlobals.updateUserConfigurationCallTracer = new Trace(ExTraceGlobals.componentGuid, 67);
				}
				return ExTraceGlobals.updateUserConfigurationCallTracer;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x0004E3A2 File Offset: 0x0004C5A2
		public static Trace ThrottlingTracer
		{
			get
			{
				if (ExTraceGlobals.throttlingTracer == null)
				{
					ExTraceGlobals.throttlingTracer = new Trace(ExTraceGlobals.componentGuid, 68);
				}
				return ExTraceGlobals.throttlingTracer;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x0004E3C1 File Offset: 0x0004C5C1
		public static Trace ExternalUserTracer
		{
			get
			{
				if (ExTraceGlobals.externalUserTracer == null)
				{
					ExTraceGlobals.externalUserTracer = new Trace(ExTraceGlobals.componentGuid, 69);
				}
				return ExTraceGlobals.externalUserTracer;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0004E3E0 File Offset: 0x0004C5E0
		public static Trace GetOrganizationConfigurationCallTracer
		{
			get
			{
				if (ExTraceGlobals.getOrganizationConfigurationCallTracer == null)
				{
					ExTraceGlobals.getOrganizationConfigurationCallTracer = new Trace(ExTraceGlobals.componentGuid, 70);
				}
				return ExTraceGlobals.getOrganizationConfigurationCallTracer;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0004E3FF File Offset: 0x0004C5FF
		public static Trace GetRoomsCallTracer
		{
			get
			{
				if (ExTraceGlobals.getRoomsCallTracer == null)
				{
					ExTraceGlobals.getRoomsCallTracer = new Trace(ExTraceGlobals.componentGuid, 71);
				}
				return ExTraceGlobals.getRoomsCallTracer;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x0004E41E File Offset: 0x0004C61E
		public static Trace GetFederationInformationTracer
		{
			get
			{
				if (ExTraceGlobals.getFederationInformationTracer == null)
				{
					ExTraceGlobals.getFederationInformationTracer = new Trace(ExTraceGlobals.componentGuid, 72);
				}
				return ExTraceGlobals.getFederationInformationTracer;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x0004E43D File Offset: 0x0004C63D
		public static Trace ParticipantLookupBatchingTracer
		{
			get
			{
				if (ExTraceGlobals.participantLookupBatchingTracer == null)
				{
					ExTraceGlobals.participantLookupBatchingTracer = new Trace(ExTraceGlobals.componentGuid, 73);
				}
				return ExTraceGlobals.participantLookupBatchingTracer;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x0004E45C File Offset: 0x0004C65C
		public static Trace AllResponsesTracer
		{
			get
			{
				if (ExTraceGlobals.allResponsesTracer == null)
				{
					ExTraceGlobals.allResponsesTracer = new Trace(ExTraceGlobals.componentGuid, 74);
				}
				return ExTraceGlobals.allResponsesTracer;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0004E47B File Offset: 0x0004C67B
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 75);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0004E49A File Offset: 0x0004C69A
		public static Trace GetInboxRulesCallTracer
		{
			get
			{
				if (ExTraceGlobals.getInboxRulesCallTracer == null)
				{
					ExTraceGlobals.getInboxRulesCallTracer = new Trace(ExTraceGlobals.componentGuid, 76);
				}
				return ExTraceGlobals.getInboxRulesCallTracer;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x0004E4B9 File Offset: 0x0004C6B9
		public static Trace UpdateInboxRulesCallTracer
		{
			get
			{
				if (ExTraceGlobals.updateInboxRulesCallTracer == null)
				{
					ExTraceGlobals.updateInboxRulesCallTracer = new Trace(ExTraceGlobals.componentGuid, 77);
				}
				return ExTraceGlobals.updateInboxRulesCallTracer;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x0004E4D8 File Offset: 0x0004C6D8
		public static Trace GetCASMailboxTracer
		{
			get
			{
				if (ExTraceGlobals.getCASMailboxTracer == null)
				{
					ExTraceGlobals.getCASMailboxTracer = new Trace(ExTraceGlobals.componentGuid, 78);
				}
				return ExTraceGlobals.getCASMailboxTracer;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0004E4F7 File Offset: 0x0004C6F7
		public static Trace FastTransferTracer
		{
			get
			{
				if (ExTraceGlobals.fastTransferTracer == null)
				{
					ExTraceGlobals.fastTransferTracer = new Trace(ExTraceGlobals.componentGuid, 79);
				}
				return ExTraceGlobals.fastTransferTracer;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0004E516 File Offset: 0x0004C716
		public static Trace SyncConversationCallTracer
		{
			get
			{
				if (ExTraceGlobals.syncConversationCallTracer == null)
				{
					ExTraceGlobals.syncConversationCallTracer = new Trace(ExTraceGlobals.componentGuid, 80);
				}
				return ExTraceGlobals.syncConversationCallTracer;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0004E535 File Offset: 0x0004C735
		public static Trace ELCTracer
		{
			get
			{
				if (ExTraceGlobals.eLCTracer == null)
				{
					ExTraceGlobals.eLCTracer = new Trace(ExTraceGlobals.componentGuid, 81);
				}
				return ExTraceGlobals.eLCTracer;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0004E554 File Offset: 0x0004C754
		public static Trace ActivityConverterTracer
		{
			get
			{
				if (ExTraceGlobals.activityConverterTracer == null)
				{
					ExTraceGlobals.activityConverterTracer = new Trace(ExTraceGlobals.componentGuid, 82);
				}
				return ExTraceGlobals.activityConverterTracer;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x0004E573 File Offset: 0x0004C773
		public static Trace SyncPeopleCallTracer
		{
			get
			{
				if (ExTraceGlobals.syncPeopleCallTracer == null)
				{
					ExTraceGlobals.syncPeopleCallTracer = new Trace(ExTraceGlobals.componentGuid, 83);
				}
				return ExTraceGlobals.syncPeopleCallTracer;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x0004E592 File Offset: 0x0004C792
		public static Trace GetCalendarFoldersCallTracer
		{
			get
			{
				if (ExTraceGlobals.getCalendarFoldersCallTracer == null)
				{
					ExTraceGlobals.getCalendarFoldersCallTracer = new Trace(ExTraceGlobals.componentGuid, 84);
				}
				return ExTraceGlobals.getCalendarFoldersCallTracer;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x0004E5B1 File Offset: 0x0004C7B1
		public static Trace GetRemindersCallTracer
		{
			get
			{
				if (ExTraceGlobals.getRemindersCallTracer == null)
				{
					ExTraceGlobals.getRemindersCallTracer = new Trace(ExTraceGlobals.componentGuid, 85);
				}
				return ExTraceGlobals.getRemindersCallTracer;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x0004E5D0 File Offset: 0x0004C7D0
		public static Trace SyncCalendarCallTracer
		{
			get
			{
				if (ExTraceGlobals.syncCalendarCallTracer == null)
				{
					ExTraceGlobals.syncCalendarCallTracer = new Trace(ExTraceGlobals.componentGuid, 86);
				}
				return ExTraceGlobals.syncCalendarCallTracer;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x0004E5EF File Offset: 0x0004C7EF
		public static Trace PerformReminderActionCallTracer
		{
			get
			{
				if (ExTraceGlobals.performReminderActionCallTracer == null)
				{
					ExTraceGlobals.performReminderActionCallTracer = new Trace(ExTraceGlobals.componentGuid, 87);
				}
				return ExTraceGlobals.performReminderActionCallTracer;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x0004E60E File Offset: 0x0004C80E
		public static Trace ProvisionCallTracer
		{
			get
			{
				if (ExTraceGlobals.provisionCallTracer == null)
				{
					ExTraceGlobals.provisionCallTracer = new Trace(ExTraceGlobals.componentGuid, 88);
				}
				return ExTraceGlobals.provisionCallTracer;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x0004E62D File Offset: 0x0004C82D
		public static Trace RenameCalendarGroupCallTracer
		{
			get
			{
				if (ExTraceGlobals.renameCalendarGroupCallTracer == null)
				{
					ExTraceGlobals.renameCalendarGroupCallTracer = new Trace(ExTraceGlobals.componentGuid, 89);
				}
				return ExTraceGlobals.renameCalendarGroupCallTracer;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x0004E64C File Offset: 0x0004C84C
		public static Trace DeleteCalendarGroupCallTracer
		{
			get
			{
				if (ExTraceGlobals.deleteCalendarGroupCallTracer == null)
				{
					ExTraceGlobals.deleteCalendarGroupCallTracer = new Trace(ExTraceGlobals.componentGuid, 90);
				}
				return ExTraceGlobals.deleteCalendarGroupCallTracer;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x0004E66B File Offset: 0x0004C86B
		public static Trace CreateCalendarCallTracer
		{
			get
			{
				if (ExTraceGlobals.createCalendarCallTracer == null)
				{
					ExTraceGlobals.createCalendarCallTracer = new Trace(ExTraceGlobals.componentGuid, 91);
				}
				return ExTraceGlobals.createCalendarCallTracer;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x0004E68A File Offset: 0x0004C88A
		public static Trace RenameCalendarCallTracer
		{
			get
			{
				if (ExTraceGlobals.renameCalendarCallTracer == null)
				{
					ExTraceGlobals.renameCalendarCallTracer = new Trace(ExTraceGlobals.componentGuid, 92);
				}
				return ExTraceGlobals.renameCalendarCallTracer;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x0004E6A9 File Offset: 0x0004C8A9
		public static Trace DeleteCalendarCallTracer
		{
			get
			{
				if (ExTraceGlobals.deleteCalendarCallTracer == null)
				{
					ExTraceGlobals.deleteCalendarCallTracer = new Trace(ExTraceGlobals.componentGuid, 93);
				}
				return ExTraceGlobals.deleteCalendarCallTracer;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0004E6C8 File Offset: 0x0004C8C8
		public static Trace SetCalendarColorCallTracer
		{
			get
			{
				if (ExTraceGlobals.setCalendarColorCallTracer == null)
				{
					ExTraceGlobals.setCalendarColorCallTracer = new Trace(ExTraceGlobals.componentGuid, 94);
				}
				return ExTraceGlobals.setCalendarColorCallTracer;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x0004E6E7 File Offset: 0x0004C8E7
		public static Trace SetCalendarGroupOrderCallTracer
		{
			get
			{
				if (ExTraceGlobals.setCalendarGroupOrderCallTracer == null)
				{
					ExTraceGlobals.setCalendarGroupOrderCallTracer = new Trace(ExTraceGlobals.componentGuid, 95);
				}
				return ExTraceGlobals.setCalendarGroupOrderCallTracer;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x0004E706 File Offset: 0x0004C906
		public static Trace CreateCalendarGroupCallTracer
		{
			get
			{
				if (ExTraceGlobals.createCalendarGroupCallTracer == null)
				{
					ExTraceGlobals.createCalendarGroupCallTracer = new Trace(ExTraceGlobals.componentGuid, 96);
				}
				return ExTraceGlobals.createCalendarGroupCallTracer;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x0004E725 File Offset: 0x0004C925
		public static Trace MoveCalendarCallTracer
		{
			get
			{
				if (ExTraceGlobals.moveCalendarCallTracer == null)
				{
					ExTraceGlobals.moveCalendarCallTracer = new Trace(ExTraceGlobals.componentGuid, 97);
				}
				return ExTraceGlobals.moveCalendarCallTracer;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0004E744 File Offset: 0x0004C944
		public static Trace GetFavoritesCallTracer
		{
			get
			{
				if (ExTraceGlobals.getFavoritesCallTracer == null)
				{
					ExTraceGlobals.getFavoritesCallTracer = new Trace(ExTraceGlobals.componentGuid, 98);
				}
				return ExTraceGlobals.getFavoritesCallTracer;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x0004E763 File Offset: 0x0004C963
		public static Trace UpdateFavoriteFolderCallTracer
		{
			get
			{
				if (ExTraceGlobals.updateFavoriteFolderCallTracer == null)
				{
					ExTraceGlobals.updateFavoriteFolderCallTracer = new Trace(ExTraceGlobals.componentGuid, 99);
				}
				return ExTraceGlobals.updateFavoriteFolderCallTracer;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x0004E782 File Offset: 0x0004C982
		public static Trace GetTimeZoneOffsetsCallTracer
		{
			get
			{
				if (ExTraceGlobals.getTimeZoneOffsetsCallTracer == null)
				{
					ExTraceGlobals.getTimeZoneOffsetsCallTracer = new Trace(ExTraceGlobals.componentGuid, 100);
				}
				return ExTraceGlobals.getTimeZoneOffsetsCallTracer;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0004E7A1 File Offset: 0x0004C9A1
		public static Trace AuthorizationTracer
		{
			get
			{
				if (ExTraceGlobals.authorizationTracer == null)
				{
					ExTraceGlobals.authorizationTracer = new Trace(ExTraceGlobals.componentGuid, 101);
				}
				return ExTraceGlobals.authorizationTracer;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x0004E7C0 File Offset: 0x0004C9C0
		public static Trace SendCalendarSharingInviteCallTracer
		{
			get
			{
				if (ExTraceGlobals.sendCalendarSharingInviteCallTracer == null)
				{
					ExTraceGlobals.sendCalendarSharingInviteCallTracer = new Trace(ExTraceGlobals.componentGuid, 102);
				}
				return ExTraceGlobals.sendCalendarSharingInviteCallTracer;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0004E7DF File Offset: 0x0004C9DF
		public static Trace GetCalendarSharingRecipientInfoCallTracer
		{
			get
			{
				if (ExTraceGlobals.getCalendarSharingRecipientInfoCallTracer == null)
				{
					ExTraceGlobals.getCalendarSharingRecipientInfoCallTracer = new Trace(ExTraceGlobals.componentGuid, 103);
				}
				return ExTraceGlobals.getCalendarSharingRecipientInfoCallTracer;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0004E7FE File Offset: 0x0004C9FE
		public static Trace AddSharedCalendarCallTracer
		{
			get
			{
				if (ExTraceGlobals.addSharedCalendarCallTracer == null)
				{
					ExTraceGlobals.addSharedCalendarCallTracer = new Trace(ExTraceGlobals.componentGuid, 104);
				}
				return ExTraceGlobals.addSharedCalendarCallTracer;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0004E81D File Offset: 0x0004CA1D
		public static Trace FindPeopleCallTracer
		{
			get
			{
				if (ExTraceGlobals.findPeopleCallTracer == null)
				{
					ExTraceGlobals.findPeopleCallTracer = new Trace(ExTraceGlobals.componentGuid, 105);
				}
				return ExTraceGlobals.findPeopleCallTracer;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x0004E83C File Offset: 0x0004CA3C
		public static Trace FindPlacesCallTracer
		{
			get
			{
				if (ExTraceGlobals.findPlacesCallTracer == null)
				{
					ExTraceGlobals.findPlacesCallTracer = new Trace(ExTraceGlobals.componentGuid, 106);
				}
				return ExTraceGlobals.findPlacesCallTracer;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x0004E85B File Offset: 0x0004CA5B
		public static Trace UserPhotosTracer
		{
			get
			{
				if (ExTraceGlobals.userPhotosTracer == null)
				{
					ExTraceGlobals.userPhotosTracer = new Trace(ExTraceGlobals.componentGuid, 107);
				}
				return ExTraceGlobals.userPhotosTracer;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x0004E87A File Offset: 0x0004CA7A
		public static Trace GetPersonaCallTracer
		{
			get
			{
				if (ExTraceGlobals.getPersonaCallTracer == null)
				{
					ExTraceGlobals.getPersonaCallTracer = new Trace(ExTraceGlobals.componentGuid, 108);
				}
				return ExTraceGlobals.getPersonaCallTracer;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0004E899 File Offset: 0x0004CA99
		public static Trace GetExtensibilityContextCallTracer
		{
			get
			{
				if (ExTraceGlobals.getExtensibilityContextCallTracer == null)
				{
					ExTraceGlobals.getExtensibilityContextCallTracer = new Trace(ExTraceGlobals.componentGuid, 109);
				}
				return ExTraceGlobals.getExtensibilityContextCallTracer;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x0004E8B8 File Offset: 0x0004CAB8
		public static Trace SubscribeInternalCalendarCallTracer
		{
			get
			{
				if (ExTraceGlobals.subscribeInternalCalendarCallTracer == null)
				{
					ExTraceGlobals.subscribeInternalCalendarCallTracer = new Trace(ExTraceGlobals.componentGuid, 110);
				}
				return ExTraceGlobals.subscribeInternalCalendarCallTracer;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x0004E8D7 File Offset: 0x0004CAD7
		public static Trace SubscribeInternetCalendarCallTracer
		{
			get
			{
				if (ExTraceGlobals.subscribeInternetCalendarCallTracer == null)
				{
					ExTraceGlobals.subscribeInternetCalendarCallTracer = new Trace(ExTraceGlobals.componentGuid, 111);
				}
				return ExTraceGlobals.subscribeInternetCalendarCallTracer;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x0004E8F6 File Offset: 0x0004CAF6
		public static Trace GetUserAvailabilityInternalCallTracer
		{
			get
			{
				if (ExTraceGlobals.getUserAvailabilityInternalCallTracer == null)
				{
					ExTraceGlobals.getUserAvailabilityInternalCallTracer = new Trace(ExTraceGlobals.componentGuid, 112);
				}
				return ExTraceGlobals.getUserAvailabilityInternalCallTracer;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0004E915 File Offset: 0x0004CB15
		public static Trace ApplyConversationActionCallTracer
		{
			get
			{
				if (ExTraceGlobals.applyConversationActionCallTracer == null)
				{
					ExTraceGlobals.applyConversationActionCallTracer = new Trace(ExTraceGlobals.componentGuid, 113);
				}
				return ExTraceGlobals.applyConversationActionCallTracer;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x0004E934 File Offset: 0x0004CB34
		public static Trace GetCalendarSharingPermissionsCallTracer
		{
			get
			{
				if (ExTraceGlobals.getCalendarSharingPermissionsCallTracer == null)
				{
					ExTraceGlobals.getCalendarSharingPermissionsCallTracer = new Trace(ExTraceGlobals.componentGuid, 114);
				}
				return ExTraceGlobals.getCalendarSharingPermissionsCallTracer;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x0004E953 File Offset: 0x0004CB53
		public static Trace SetCalendarSharingPermissionsCallTracer
		{
			get
			{
				if (ExTraceGlobals.setCalendarSharingPermissionsCallTracer == null)
				{
					ExTraceGlobals.setCalendarSharingPermissionsCallTracer = new Trace(ExTraceGlobals.componentGuid, 115);
				}
				return ExTraceGlobals.setCalendarSharingPermissionsCallTracer;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x0004E972 File Offset: 0x0004CB72
		public static Trace SetCalendarPublishingCallTracer
		{
			get
			{
				if (ExTraceGlobals.setCalendarPublishingCallTracer == null)
				{
					ExTraceGlobals.setCalendarPublishingCallTracer = new Trace(ExTraceGlobals.componentGuid, 116);
				}
				return ExTraceGlobals.setCalendarPublishingCallTracer;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x0004E991 File Offset: 0x0004CB91
		public static Trace UCSTracer
		{
			get
			{
				if (ExTraceGlobals.uCSTracer == null)
				{
					ExTraceGlobals.uCSTracer = new Trace(ExTraceGlobals.componentGuid, 117);
				}
				return ExTraceGlobals.uCSTracer;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x0004E9B0 File Offset: 0x0004CBB0
		public static Trace GetTaskFoldersCallTracer
		{
			get
			{
				if (ExTraceGlobals.getTaskFoldersCallTracer == null)
				{
					ExTraceGlobals.getTaskFoldersCallTracer = new Trace(ExTraceGlobals.componentGuid, 118);
				}
				return ExTraceGlobals.getTaskFoldersCallTracer;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x0004E9CF File Offset: 0x0004CBCF
		public static Trace CreateTaskFolderCallTracer
		{
			get
			{
				if (ExTraceGlobals.createTaskFolderCallTracer == null)
				{
					ExTraceGlobals.createTaskFolderCallTracer = new Trace(ExTraceGlobals.componentGuid, 119);
				}
				return ExTraceGlobals.createTaskFolderCallTracer;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x0004E9EE File Offset: 0x0004CBEE
		public static Trace RenameTaskFolderCallTracer
		{
			get
			{
				if (ExTraceGlobals.renameTaskFolderCallTracer == null)
				{
					ExTraceGlobals.renameTaskFolderCallTracer = new Trace(ExTraceGlobals.componentGuid, 120);
				}
				return ExTraceGlobals.renameTaskFolderCallTracer;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x0004EA0D File Offset: 0x0004CC0D
		public static Trace DeleteTaskFolderCallTracer
		{
			get
			{
				if (ExTraceGlobals.deleteTaskFolderCallTracer == null)
				{
					ExTraceGlobals.deleteTaskFolderCallTracer = new Trace(ExTraceGlobals.componentGuid, 121);
				}
				return ExTraceGlobals.deleteTaskFolderCallTracer;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0004EA2C File Offset: 0x0004CC2C
		public static Trace MasterCategoryListCallTracer
		{
			get
			{
				if (ExTraceGlobals.masterCategoryListCallTracer == null)
				{
					ExTraceGlobals.masterCategoryListCallTracer = new Trace(ExTraceGlobals.componentGuid, 122);
				}
				return ExTraceGlobals.masterCategoryListCallTracer;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x0004EA4B File Offset: 0x0004CC4B
		public static Trace GetCalendarFolderConfigurationCallTracer
		{
			get
			{
				if (ExTraceGlobals.getCalendarFolderConfigurationCallTracer == null)
				{
					ExTraceGlobals.getCalendarFolderConfigurationCallTracer = new Trace(ExTraceGlobals.componentGuid, 123);
				}
				return ExTraceGlobals.getCalendarFolderConfigurationCallTracer;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x0004EA6A File Offset: 0x0004CC6A
		public static Trace OnlineMeetingTracer
		{
			get
			{
				if (ExTraceGlobals.onlineMeetingTracer == null)
				{
					ExTraceGlobals.onlineMeetingTracer = new Trace(ExTraceGlobals.componentGuid, 124);
				}
				return ExTraceGlobals.onlineMeetingTracer;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x0004EA89 File Offset: 0x0004CC89
		public static Trace ModernGroupsTracer
		{
			get
			{
				if (ExTraceGlobals.modernGroupsTracer == null)
				{
					ExTraceGlobals.modernGroupsTracer = new Trace(ExTraceGlobals.componentGuid, 125);
				}
				return ExTraceGlobals.modernGroupsTracer;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0004EAA8 File Offset: 0x0004CCA8
		public static Trace CreateUnifiedMailboxTracer
		{
			get
			{
				if (ExTraceGlobals.createUnifiedMailboxTracer == null)
				{
					ExTraceGlobals.createUnifiedMailboxTracer = new Trace(ExTraceGlobals.componentGuid, 126);
				}
				return ExTraceGlobals.createUnifiedMailboxTracer;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x0004EAC7 File Offset: 0x0004CCC7
		public static Trace AddAggregatedAccountTracer
		{
			get
			{
				if (ExTraceGlobals.addAggregatedAccountTracer == null)
				{
					ExTraceGlobals.addAggregatedAccountTracer = new Trace(ExTraceGlobals.componentGuid, 127);
				}
				return ExTraceGlobals.addAggregatedAccountTracer;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0004EAE6 File Offset: 0x0004CCE6
		public static Trace RemindersTracer
		{
			get
			{
				if (ExTraceGlobals.remindersTracer == null)
				{
					ExTraceGlobals.remindersTracer = new Trace(ExTraceGlobals.componentGuid, 128);
				}
				return ExTraceGlobals.remindersTracer;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0004EB08 File Offset: 0x0004CD08
		public static Trace GetAggregatedAccountTracer
		{
			get
			{
				if (ExTraceGlobals.getAggregatedAccountTracer == null)
				{
					ExTraceGlobals.getAggregatedAccountTracer = new Trace(ExTraceGlobals.componentGuid, 129);
				}
				return ExTraceGlobals.getAggregatedAccountTracer;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0004EB2A File Offset: 0x0004CD2A
		public static Trace RemoveAggregatedAccountTracer
		{
			get
			{
				if (ExTraceGlobals.removeAggregatedAccountTracer == null)
				{
					ExTraceGlobals.removeAggregatedAccountTracer = new Trace(ExTraceGlobals.componentGuid, 130);
				}
				return ExTraceGlobals.removeAggregatedAccountTracer;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x0004EB4C File Offset: 0x0004CD4C
		public static Trace SetAggregatedAccountTracer
		{
			get
			{
				if (ExTraceGlobals.setAggregatedAccountTracer == null)
				{
					ExTraceGlobals.setAggregatedAccountTracer = new Trace(ExTraceGlobals.componentGuid, 131);
				}
				return ExTraceGlobals.setAggregatedAccountTracer;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x0004EB6E File Offset: 0x0004CD6E
		public static Trace WeatherTracer
		{
			get
			{
				if (ExTraceGlobals.weatherTracer == null)
				{
					ExTraceGlobals.weatherTracer = new Trace(ExTraceGlobals.componentGuid, 132);
				}
				return ExTraceGlobals.weatherTracer;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x0004EB90 File Offset: 0x0004CD90
		public static Trace FederatedDirectoryTracer
		{
			get
			{
				if (ExTraceGlobals.federatedDirectoryTracer == null)
				{
					ExTraceGlobals.federatedDirectoryTracer = new Trace(ExTraceGlobals.componentGuid, 133);
				}
				return ExTraceGlobals.federatedDirectoryTracer;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x0004EBB2 File Offset: 0x0004CDB2
		public static Trace GetPeopleIKnowGraphCallTracer
		{
			get
			{
				if (ExTraceGlobals.getPeopleIKnowGraphCallTracer == null)
				{
					ExTraceGlobals.getPeopleIKnowGraphCallTracer = new Trace(ExTraceGlobals.componentGuid, 134);
				}
				return ExTraceGlobals.getPeopleIKnowGraphCallTracer;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0004EBD4 File Offset: 0x0004CDD4
		public static Trace AddEventToMyCalendarTracer
		{
			get
			{
				if (ExTraceGlobals.addEventToMyCalendarTracer == null)
				{
					ExTraceGlobals.addEventToMyCalendarTracer = new Trace(ExTraceGlobals.componentGuid, 135);
				}
				return ExTraceGlobals.addEventToMyCalendarTracer;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0004EBF6 File Offset: 0x0004CDF6
		public static Trace ConversationAggregationTracer
		{
			get
			{
				if (ExTraceGlobals.conversationAggregationTracer == null)
				{
					ExTraceGlobals.conversationAggregationTracer = new Trace(ExTraceGlobals.componentGuid, 136);
				}
				return ExTraceGlobals.conversationAggregationTracer;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x0004EC18 File Offset: 0x0004CE18
		public static Trace IsOffice365DomainTracer
		{
			get
			{
				if (ExTraceGlobals.isOffice365DomainTracer == null)
				{
					ExTraceGlobals.isOffice365DomainTracer = new Trace(ExTraceGlobals.componentGuid, 137);
				}
				return ExTraceGlobals.isOffice365DomainTracer;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x0004EC3A File Offset: 0x0004CE3A
		public static Trace RefreshGALContactsFolderTracer
		{
			get
			{
				if (ExTraceGlobals.refreshGALContactsFolderTracer == null)
				{
					ExTraceGlobals.refreshGALContactsFolderTracer = new Trace(ExTraceGlobals.componentGuid, 138);
				}
				return ExTraceGlobals.refreshGALContactsFolderTracer;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0004EC5C File Offset: 0x0004CE5C
		public static Trace OptionsTracer
		{
			get
			{
				if (ExTraceGlobals.optionsTracer == null)
				{
					ExTraceGlobals.optionsTracer = new Trace(ExTraceGlobals.componentGuid, 139);
				}
				return ExTraceGlobals.optionsTracer;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x0004EC7E File Offset: 0x0004CE7E
		public static Trace OpenTenantManagerTracer
		{
			get
			{
				if (ExTraceGlobals.openTenantManagerTracer == null)
				{
					ExTraceGlobals.openTenantManagerTracer = new Trace(ExTraceGlobals.componentGuid, 140);
				}
				return ExTraceGlobals.openTenantManagerTracer;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0004ECA0 File Offset: 0x0004CEA0
		public static Trace MarkAllItemsAsReadTracer
		{
			get
			{
				if (ExTraceGlobals.markAllItemsAsReadTracer == null)
				{
					ExTraceGlobals.markAllItemsAsReadTracer = new Trace(ExTraceGlobals.componentGuid, 141);
				}
				return ExTraceGlobals.markAllItemsAsReadTracer;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x0004ECC2 File Offset: 0x0004CEC2
		public static Trace GetConversationItemsTracer
		{
			get
			{
				if (ExTraceGlobals.getConversationItemsTracer == null)
				{
					ExTraceGlobals.getConversationItemsTracer = new Trace(ExTraceGlobals.componentGuid, 142);
				}
				return ExTraceGlobals.getConversationItemsTracer;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0004ECE4 File Offset: 0x0004CEE4
		public static Trace GetLikersTracer
		{
			get
			{
				if (ExTraceGlobals.getLikersTracer == null)
				{
					ExTraceGlobals.getLikersTracer = new Trace(ExTraceGlobals.componentGuid, 143);
				}
				return ExTraceGlobals.getLikersTracer;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0004ED06 File Offset: 0x0004CF06
		public static Trace GetUserUnifiedGroupsTracer
		{
			get
			{
				if (ExTraceGlobals.getUserUnifiedGroupsTracer == null)
				{
					ExTraceGlobals.getUserUnifiedGroupsTracer = new Trace(ExTraceGlobals.componentGuid, 144);
				}
				return ExTraceGlobals.getUserUnifiedGroupsTracer;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x0004ED28 File Offset: 0x0004CF28
		public static Trace PeopleICommunicateWithTracer
		{
			get
			{
				if (ExTraceGlobals.peopleICommunicateWithTracer == null)
				{
					ExTraceGlobals.peopleICommunicateWithTracer = new Trace(ExTraceGlobals.componentGuid, 145);
				}
				return ExTraceGlobals.peopleICommunicateWithTracer;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x0004ED4A File Offset: 0x0004CF4A
		public static Trace SyncPersonaContactsBaseTracer
		{
			get
			{
				if (ExTraceGlobals.syncPersonaContactsBaseTracer == null)
				{
					ExTraceGlobals.syncPersonaContactsBaseTracer = new Trace(ExTraceGlobals.componentGuid, 146);
				}
				return ExTraceGlobals.syncPersonaContactsBaseTracer;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x0004ED6C File Offset: 0x0004CF6C
		public static Trace SyncAutoCompleteRecipientsTracer
		{
			get
			{
				if (ExTraceGlobals.syncAutoCompleteRecipientsTracer == null)
				{
					ExTraceGlobals.syncAutoCompleteRecipientsTracer = new Trace(ExTraceGlobals.componentGuid, 147);
				}
				return ExTraceGlobals.syncAutoCompleteRecipientsTracer;
			}
		}

		// Token: 0x0400167B RID: 5755
		private static Guid componentGuid = new Guid("9041df24-db8f-4561-9ce6-75ee8dc21732");

		// Token: 0x0400167C RID: 5756
		private static Trace calendarAlgorithmTracer = null;

		// Token: 0x0400167D RID: 5757
		private static Trace calendarDataTracer = null;

		// Token: 0x0400167E RID: 5758
		private static Trace calendarCallTracer = null;

		// Token: 0x0400167F RID: 5759
		private static Trace commonAlgorithmTracer = null;

		// Token: 0x04001680 RID: 5760
		private static Trace folderAlgorithmTracer = null;

		// Token: 0x04001681 RID: 5761
		private static Trace folderDataTracer = null;

		// Token: 0x04001682 RID: 5762
		private static Trace folderCallTracer = null;

		// Token: 0x04001683 RID: 5763
		private static Trace itemAlgorithmTracer = null;

		// Token: 0x04001684 RID: 5764
		private static Trace itemDataTracer = null;

		// Token: 0x04001685 RID: 5765
		private static Trace itemCallTracer = null;

		// Token: 0x04001686 RID: 5766
		private static Trace exceptionTracer = null;

		// Token: 0x04001687 RID: 5767
		private static Trace sessionCacheTracer = null;

		// Token: 0x04001688 RID: 5768
		private static Trace exchangePrincipalCacheTracer = null;

		// Token: 0x04001689 RID: 5769
		private static Trace searchTracer = null;

		// Token: 0x0400168A RID: 5770
		private static Trace utilAlgorithmTracer = null;

		// Token: 0x0400168B RID: 5771
		private static Trace utilDataTracer = null;

		// Token: 0x0400168C RID: 5772
		private static Trace utilCallTracer = null;

		// Token: 0x0400168D RID: 5773
		private static Trace serverToServerAuthZTracer = null;

		// Token: 0x0400168E RID: 5774
		private static Trace serviceCommandBaseCallTracer = null;

		// Token: 0x0400168F RID: 5775
		private static Trace serviceCommandBaseDataTracer = null;

		// Token: 0x04001690 RID: 5776
		private static Trace facadeBaseCallTracer = null;

		// Token: 0x04001691 RID: 5777
		private static Trace createItemCallTracer = null;

		// Token: 0x04001692 RID: 5778
		private static Trace getItemCallTracer = null;

		// Token: 0x04001693 RID: 5779
		private static Trace updateItemCallTracer = null;

		// Token: 0x04001694 RID: 5780
		private static Trace deleteItemCallTracer = null;

		// Token: 0x04001695 RID: 5781
		private static Trace sendItemCallTracer = null;

		// Token: 0x04001696 RID: 5782
		private static Trace moveCopyCommandBaseCallTracer = null;

		// Token: 0x04001697 RID: 5783
		private static Trace moveCopyItemCommandBaseCallTracer = null;

		// Token: 0x04001698 RID: 5784
		private static Trace copyItemCallTracer = null;

		// Token: 0x04001699 RID: 5785
		private static Trace moveItemCallTracer = null;

		// Token: 0x0400169A RID: 5786
		private static Trace createFolderCallTracer = null;

		// Token: 0x0400169B RID: 5787
		private static Trace getFolderCallTracer = null;

		// Token: 0x0400169C RID: 5788
		private static Trace updateFolderCallTracer = null;

		// Token: 0x0400169D RID: 5789
		private static Trace deleteFolderCallTracer = null;

		// Token: 0x0400169E RID: 5790
		private static Trace moveCopyFolderCommandBaseCallTracer = null;

		// Token: 0x0400169F RID: 5791
		private static Trace copyFolderCallTracer = null;

		// Token: 0x040016A0 RID: 5792
		private static Trace moveFolderCallTracer = null;

		// Token: 0x040016A1 RID: 5793
		private static Trace findCommandBaseCallTracer = null;

		// Token: 0x040016A2 RID: 5794
		private static Trace findItemCallTracer = null;

		// Token: 0x040016A3 RID: 5795
		private static Trace findFolderCallTracer = null;

		// Token: 0x040016A4 RID: 5796
		private static Trace utilCommandBaseCallTracer = null;

		// Token: 0x040016A5 RID: 5797
		private static Trace expandDLCallTracer = null;

		// Token: 0x040016A6 RID: 5798
		private static Trace resolveNamesCallTracer = null;

		// Token: 0x040016A7 RID: 5799
		private static Trace subscribeCallTracer = null;

		// Token: 0x040016A8 RID: 5800
		private static Trace unsubscribeCallTracer = null;

		// Token: 0x040016A9 RID: 5801
		private static Trace getEventsCallTracer = null;

		// Token: 0x040016AA RID: 5802
		private static Trace subscriptionsTracer = null;

		// Token: 0x040016AB RID: 5803
		private static Trace subscriptionBaseTracer = null;

		// Token: 0x040016AC RID: 5804
		private static Trace pushSubscriptionTracer = null;

		// Token: 0x040016AD RID: 5805
		private static Trace syncFolderHierarchyCallTracer = null;

		// Token: 0x040016AE RID: 5806
		private static Trace syncFolderItemsCallTracer = null;

		// Token: 0x040016AF RID: 5807
		private static Trace synchronizationTracer = null;

		// Token: 0x040016B0 RID: 5808
		private static Trace performanceMonitorTracer = null;

		// Token: 0x040016B1 RID: 5809
		private static Trace convertIdCallTracer = null;

		// Token: 0x040016B2 RID: 5810
		private static Trace getDelegateCallTracer = null;

		// Token: 0x040016B3 RID: 5811
		private static Trace addDelegateCallTracer = null;

		// Token: 0x040016B4 RID: 5812
		private static Trace removeDelegateCallTracer = null;

		// Token: 0x040016B5 RID: 5813
		private static Trace updateDelegateCallTracer = null;

		// Token: 0x040016B6 RID: 5814
		private static Trace proxyEvaluatorTracer = null;

		// Token: 0x040016B7 RID: 5815
		private static Trace getMailTipsCallTracer = null;

		// Token: 0x040016B8 RID: 5816
		private static Trace allRequestsTracer = null;

		// Token: 0x040016B9 RID: 5817
		private static Trace authenticationTracer = null;

		// Token: 0x040016BA RID: 5818
		private static Trace wCFTracer = null;

		// Token: 0x040016BB RID: 5819
		private static Trace getUserConfigurationCallTracer = null;

		// Token: 0x040016BC RID: 5820
		private static Trace createUserConfigurationCallTracer = null;

		// Token: 0x040016BD RID: 5821
		private static Trace deleteUserConfigurationCallTracer = null;

		// Token: 0x040016BE RID: 5822
		private static Trace updateUserConfigurationCallTracer = null;

		// Token: 0x040016BF RID: 5823
		private static Trace throttlingTracer = null;

		// Token: 0x040016C0 RID: 5824
		private static Trace externalUserTracer = null;

		// Token: 0x040016C1 RID: 5825
		private static Trace getOrganizationConfigurationCallTracer = null;

		// Token: 0x040016C2 RID: 5826
		private static Trace getRoomsCallTracer = null;

		// Token: 0x040016C3 RID: 5827
		private static Trace getFederationInformationTracer = null;

		// Token: 0x040016C4 RID: 5828
		private static Trace participantLookupBatchingTracer = null;

		// Token: 0x040016C5 RID: 5829
		private static Trace allResponsesTracer = null;

		// Token: 0x040016C6 RID: 5830
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x040016C7 RID: 5831
		private static Trace getInboxRulesCallTracer = null;

		// Token: 0x040016C8 RID: 5832
		private static Trace updateInboxRulesCallTracer = null;

		// Token: 0x040016C9 RID: 5833
		private static Trace getCASMailboxTracer = null;

		// Token: 0x040016CA RID: 5834
		private static Trace fastTransferTracer = null;

		// Token: 0x040016CB RID: 5835
		private static Trace syncConversationCallTracer = null;

		// Token: 0x040016CC RID: 5836
		private static Trace eLCTracer = null;

		// Token: 0x040016CD RID: 5837
		private static Trace activityConverterTracer = null;

		// Token: 0x040016CE RID: 5838
		private static Trace syncPeopleCallTracer = null;

		// Token: 0x040016CF RID: 5839
		private static Trace getCalendarFoldersCallTracer = null;

		// Token: 0x040016D0 RID: 5840
		private static Trace getRemindersCallTracer = null;

		// Token: 0x040016D1 RID: 5841
		private static Trace syncCalendarCallTracer = null;

		// Token: 0x040016D2 RID: 5842
		private static Trace performReminderActionCallTracer = null;

		// Token: 0x040016D3 RID: 5843
		private static Trace provisionCallTracer = null;

		// Token: 0x040016D4 RID: 5844
		private static Trace renameCalendarGroupCallTracer = null;

		// Token: 0x040016D5 RID: 5845
		private static Trace deleteCalendarGroupCallTracer = null;

		// Token: 0x040016D6 RID: 5846
		private static Trace createCalendarCallTracer = null;

		// Token: 0x040016D7 RID: 5847
		private static Trace renameCalendarCallTracer = null;

		// Token: 0x040016D8 RID: 5848
		private static Trace deleteCalendarCallTracer = null;

		// Token: 0x040016D9 RID: 5849
		private static Trace setCalendarColorCallTracer = null;

		// Token: 0x040016DA RID: 5850
		private static Trace setCalendarGroupOrderCallTracer = null;

		// Token: 0x040016DB RID: 5851
		private static Trace createCalendarGroupCallTracer = null;

		// Token: 0x040016DC RID: 5852
		private static Trace moveCalendarCallTracer = null;

		// Token: 0x040016DD RID: 5853
		private static Trace getFavoritesCallTracer = null;

		// Token: 0x040016DE RID: 5854
		private static Trace updateFavoriteFolderCallTracer = null;

		// Token: 0x040016DF RID: 5855
		private static Trace getTimeZoneOffsetsCallTracer = null;

		// Token: 0x040016E0 RID: 5856
		private static Trace authorizationTracer = null;

		// Token: 0x040016E1 RID: 5857
		private static Trace sendCalendarSharingInviteCallTracer = null;

		// Token: 0x040016E2 RID: 5858
		private static Trace getCalendarSharingRecipientInfoCallTracer = null;

		// Token: 0x040016E3 RID: 5859
		private static Trace addSharedCalendarCallTracer = null;

		// Token: 0x040016E4 RID: 5860
		private static Trace findPeopleCallTracer = null;

		// Token: 0x040016E5 RID: 5861
		private static Trace findPlacesCallTracer = null;

		// Token: 0x040016E6 RID: 5862
		private static Trace userPhotosTracer = null;

		// Token: 0x040016E7 RID: 5863
		private static Trace getPersonaCallTracer = null;

		// Token: 0x040016E8 RID: 5864
		private static Trace getExtensibilityContextCallTracer = null;

		// Token: 0x040016E9 RID: 5865
		private static Trace subscribeInternalCalendarCallTracer = null;

		// Token: 0x040016EA RID: 5866
		private static Trace subscribeInternetCalendarCallTracer = null;

		// Token: 0x040016EB RID: 5867
		private static Trace getUserAvailabilityInternalCallTracer = null;

		// Token: 0x040016EC RID: 5868
		private static Trace applyConversationActionCallTracer = null;

		// Token: 0x040016ED RID: 5869
		private static Trace getCalendarSharingPermissionsCallTracer = null;

		// Token: 0x040016EE RID: 5870
		private static Trace setCalendarSharingPermissionsCallTracer = null;

		// Token: 0x040016EF RID: 5871
		private static Trace setCalendarPublishingCallTracer = null;

		// Token: 0x040016F0 RID: 5872
		private static Trace uCSTracer = null;

		// Token: 0x040016F1 RID: 5873
		private static Trace getTaskFoldersCallTracer = null;

		// Token: 0x040016F2 RID: 5874
		private static Trace createTaskFolderCallTracer = null;

		// Token: 0x040016F3 RID: 5875
		private static Trace renameTaskFolderCallTracer = null;

		// Token: 0x040016F4 RID: 5876
		private static Trace deleteTaskFolderCallTracer = null;

		// Token: 0x040016F5 RID: 5877
		private static Trace masterCategoryListCallTracer = null;

		// Token: 0x040016F6 RID: 5878
		private static Trace getCalendarFolderConfigurationCallTracer = null;

		// Token: 0x040016F7 RID: 5879
		private static Trace onlineMeetingTracer = null;

		// Token: 0x040016F8 RID: 5880
		private static Trace modernGroupsTracer = null;

		// Token: 0x040016F9 RID: 5881
		private static Trace createUnifiedMailboxTracer = null;

		// Token: 0x040016FA RID: 5882
		private static Trace addAggregatedAccountTracer = null;

		// Token: 0x040016FB RID: 5883
		private static Trace remindersTracer = null;

		// Token: 0x040016FC RID: 5884
		private static Trace getAggregatedAccountTracer = null;

		// Token: 0x040016FD RID: 5885
		private static Trace removeAggregatedAccountTracer = null;

		// Token: 0x040016FE RID: 5886
		private static Trace setAggregatedAccountTracer = null;

		// Token: 0x040016FF RID: 5887
		private static Trace weatherTracer = null;

		// Token: 0x04001700 RID: 5888
		private static Trace federatedDirectoryTracer = null;

		// Token: 0x04001701 RID: 5889
		private static Trace getPeopleIKnowGraphCallTracer = null;

		// Token: 0x04001702 RID: 5890
		private static Trace addEventToMyCalendarTracer = null;

		// Token: 0x04001703 RID: 5891
		private static Trace conversationAggregationTracer = null;

		// Token: 0x04001704 RID: 5892
		private static Trace isOffice365DomainTracer = null;

		// Token: 0x04001705 RID: 5893
		private static Trace refreshGALContactsFolderTracer = null;

		// Token: 0x04001706 RID: 5894
		private static Trace optionsTracer = null;

		// Token: 0x04001707 RID: 5895
		private static Trace openTenantManagerTracer = null;

		// Token: 0x04001708 RID: 5896
		private static Trace markAllItemsAsReadTracer = null;

		// Token: 0x04001709 RID: 5897
		private static Trace getConversationItemsTracer = null;

		// Token: 0x0400170A RID: 5898
		private static Trace getLikersTracer = null;

		// Token: 0x0400170B RID: 5899
		private static Trace getUserUnifiedGroupsTracer = null;

		// Token: 0x0400170C RID: 5900
		private static Trace peopleICommunicateWithTracer = null;

		// Token: 0x0400170D RID: 5901
		private static Trace syncPersonaContactsBaseTracer = null;

		// Token: 0x0400170E RID: 5902
		private static Trace syncAutoCompleteRecipientsTracer = null;
	}
}
