using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000C6 RID: 198
	internal class JetTableClassInfo
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x000293CA File Offset: 0x000275CA
		public JetTableClassInfo(string name, JET_param param, OpenTableGrbit grbit)
		{
			this.name = name;
			this.jetParam = param;
			this.openTableGrbit = grbit;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x000293E7 File Offset: 0x000275E7
		public static IDictionary<TableClass, JetTableClassInfo> Classes
		{
			get
			{
				return JetTableClassInfo.classes;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x000293EE File Offset: 0x000275EE
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x000293F6 File Offset: 0x000275F6
		public JET_param JetParam
		{
			get
			{
				return this.jetParam;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x000293FE File Offset: 0x000275FE
		public OpenTableGrbit OpenTableGrbit
		{
			get
			{
				return this.openTableGrbit;
			}
		}

		// Token: 0x040002FC RID: 764
		private static readonly JetTableClassInfo miscClassInfo = new JetTableClassInfo("Misc", (JET_param)143, OpenTableGrbit.TableClass7);

		// Token: 0x040002FD RID: 765
		private static readonly Dictionary<TableClass, JetTableClassInfo> classes = new Dictionary<TableClass, JetTableClassInfo>
		{
			{
				TableClass.LazyIndex,
				new JetTableClassInfo("LazyIndex", (JET_param)137, OpenTableGrbit.TableClass1)
			},
			{
				TableClass.Message,
				new JetTableClassInfo("Message", (JET_param)138, OpenTableGrbit.TableClass2)
			},
			{
				TableClass.Attachment,
				new JetTableClassInfo("Attachment", (JET_param)139, OpenTableGrbit.TableClass3)
			},
			{
				TableClass.Folder,
				new JetTableClassInfo("Folder", (JET_param)140, OpenTableGrbit.TableClass4)
			},
			{
				TableClass.PseudoIndexMaintenance,
				new JetTableClassInfo("PseudoIndexMaintenance", (JET_param)141, OpenTableGrbit.TableClass5)
			},
			{
				TableClass.Events,
				new JetTableClassInfo("Events", (JET_param)142, OpenTableGrbit.TableClass6)
			},
			{
				TableClass.DeliveredTo,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.ExtendedPropertyNameMapping,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.Globals,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.InferenceLog,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.Mailbox,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.MailboxIdentity,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.PerUser,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.PseudoIndexControl,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.PseudoIndexDefinition,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.ReceiveFolder,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.ReceiveFolder2,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.ReplidGuidMap,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.SearchQueue,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.TimedEvents,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.Tombstone,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.UpgradeHistory,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.Watermarks,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.UserInfo,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.ApplyOperatorSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.CategorizedTableOperatorSuiteHeader,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.CategorizedTableOperatorSuiteLeaf,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.CategorizedTableOperatorSuiteMessage,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.ColumnSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.ColumnSuite2,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.CommonDataRowSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.ConnectionSuiteHelper,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.CountOperatorSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.DataRowSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.DeleteOperatorSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.IndexAndOperatorSuite2,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.IndexAndOperatorSuite3,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.IndexNotOperatorSuite2,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.IndexNotOperatorSuite3,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.IndexOrOperatorSuite2,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.IndexOrOperatorSuite3,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.InsertOperatorSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.InsertOperatorSuite2,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.InsertOperatorSuite3,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.JetColumnSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.JetTableSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.JoinOperatorSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.JoinOperatorSuite2,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.OrdinalPositionOperatorSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.OrdinalPositionOperatorSuite2,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.Partitioned,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.PreReadOperatorSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.ReaderSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.ReaderSuite2,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.SearchCriteriaSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.SortOperatorSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.SqlConnectionSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.TableOperatorSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.TableOperatorSuite2,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.TableOperatorSuite3,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.TableOperatorSuite4,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.Unknown,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.UpdateOperatorSuite,
				JetTableClassInfo.miscClassInfo
			},
			{
				TableClass.DistinctOperatorSuite,
				JetTableClassInfo.miscClassInfo
			}
		};

		// Token: 0x040002FE RID: 766
		private readonly string name;

		// Token: 0x040002FF RID: 767
		private readonly OpenTableGrbit openTableGrbit;

		// Token: 0x04000300 RID: 768
		private readonly JET_param jetParam;
	}
}
