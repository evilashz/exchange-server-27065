using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Common;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000065 RID: 101
	public class MailboxShape
	{
		// Token: 0x060005BB RID: 1467 RVA: 0x0000FB14 File Offset: 0x0000DD14
		public MailboxShape(long messagesPerFolderCountWarningQuota, long messagesPerFolderCountReceiveQuota, long dumpsterMessagesPerFolderCountWarningQuota, long dumpsterMessagesPerFolderCountReceiveQuota, long folderHierarchyChildrenCountWarningQuota, long folderHierarchyChildrenCountReceiveQuota, long folderHierarchyDepthWarningQuota, long folderHierarchyDepthReceiveQuota, long foldersCountWarningQuota, long foldersCountReceiveQuota, long namedPropertiesCountQuota)
		{
			this.messagesPerFolderCountWarningQuota = messagesPerFolderCountWarningQuota;
			this.messagesPerFolderCountReceiveQuota = messagesPerFolderCountReceiveQuota;
			this.dumpsterMessagesPerFolderCountWarningQuota = dumpsterMessagesPerFolderCountWarningQuota;
			this.dumpsterMessagesPerFolderCountReceiveQuota = dumpsterMessagesPerFolderCountReceiveQuota;
			this.folderHierarchyChildrenCountWarningQuota = folderHierarchyChildrenCountWarningQuota;
			this.folderHierarchyChildrenCountReceiveQuota = folderHierarchyChildrenCountReceiveQuota;
			this.folderHierarchyDepthWarningQuota = folderHierarchyDepthWarningQuota;
			this.folderHierarchyDepthReceiveQuota = folderHierarchyDepthReceiveQuota;
			this.foldersCountWarningQuota = foldersCountWarningQuota;
			this.foldersCountReceiveQuota = foldersCountReceiveQuota;
			this.namedPropertiesCountQuota = namedPropertiesCountQuota;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000FB7C File Offset: 0x0000DD7C
		public long MessagesPerFolderCountWarningQuota
		{
			get
			{
				return this.messagesPerFolderCountWarningQuota;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000FB84 File Offset: 0x0000DD84
		public long MessagesPerFolderCountReceiveQuota
		{
			get
			{
				return this.messagesPerFolderCountReceiveQuota;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0000FB8C File Offset: 0x0000DD8C
		public long DumpsterMessagesPerFolderCountWarningQuota
		{
			get
			{
				return this.dumpsterMessagesPerFolderCountWarningQuota;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000FB94 File Offset: 0x0000DD94
		public long DumpsterMessagesPerFolderCountReceiveQuota
		{
			get
			{
				return this.dumpsterMessagesPerFolderCountReceiveQuota;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0000FB9C File Offset: 0x0000DD9C
		public long FolderHierarchyChildrenCountWarningQuota
		{
			get
			{
				return this.folderHierarchyChildrenCountWarningQuota;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
		public long FolderHierarchyChildrenCountReceiveQuota
		{
			get
			{
				return this.folderHierarchyChildrenCountReceiveQuota;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		public long FolderHierarchyDepthWarningQuota
		{
			get
			{
				return this.folderHierarchyDepthWarningQuota;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000FBB4 File Offset: 0x0000DDB4
		public long FolderHierarchyDepthReceiveQuota
		{
			get
			{
				return this.folderHierarchyDepthReceiveQuota;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0000FBBC File Offset: 0x0000DDBC
		public long FoldersCountWarningQuota
		{
			get
			{
				return this.foldersCountWarningQuota;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
		public long FoldersCountReceiveQuota
		{
			get
			{
				return this.foldersCountReceiveQuota;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000FBCC File Offset: 0x0000DDCC
		public long NamedPropertiesCountQuota
		{
			get
			{
				return this.namedPropertiesCountQuota;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		public static bool TryParse(string input, out Dictionary<Guid, MailboxShape> mailboxShapeConfiguration)
		{
			mailboxShapeConfiguration = new Dictionary<Guid, MailboxShape>();
			if (string.IsNullOrEmpty(input))
			{
				if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ConfigurationTracer.TraceInformation<string>(0, 0L, "No mailbox shape configuration in: {0}", input);
				}
				return true;
			}
			string[] array = input.Split(new char[]
			{
				';'
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 0)
			{
				if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ConfigurationTracer.TraceInformation<string>(0, 0L, "No mailbox shape configuration in: {0}", input);
				}
				return true;
			}
			foreach (string text in array)
			{
				if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ConfigurationTracer.TraceInformation<string>(0, 0L, "Try parse mailbox shape configuration: {0}", text);
				}
				Guid guid;
				MailboxShape value;
				if (!MailboxShape.TryParseSingleInstance(text, out guid, out value))
				{
					if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Failed to parse mailbox shape configuration: {0}", text);
					}
					mailboxShapeConfiguration = null;
					return false;
				}
				try
				{
					mailboxShapeConfiguration.Add(guid, value);
				}
				catch (ArgumentException exception)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
					if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.ConfigurationTracer.TraceError<Guid>(0L, "Skip overwrite for mailbox {0}", guid);
					}
				}
			}
			return true;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000FD14 File Offset: 0x0000DF14
		internal static bool TryParseSingleInstance(string input, out Guid mailboxGuid, out MailboxShape mailboxShape)
		{
			mailboxGuid = Guid.Empty;
			mailboxShape = null;
			string[] array = input.Split(null, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 9 && array.Length != 12)
			{
				if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Failed parsing mailbox configuration: {0}", input);
				}
				return false;
			}
			if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ExTraceGlobals.ConfigurationTracer.TraceInformation<string>(0, 0L, "Parsing mailbox configuration: {0}", input);
			}
			if (!Guid.TryParse(array[0], out mailboxGuid))
			{
				if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Failed parsing: {0} as Guid", array[0]);
				}
				return false;
			}
			long num;
			if (!long.TryParse(array[1], out num))
			{
				if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Failed parsing: {0} as long", array[1]);
				}
				return false;
			}
			long num2;
			if (!long.TryParse(array[2], out num2))
			{
				if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Failed parsing: {0} as long", array[2]);
				}
				return false;
			}
			long num3;
			if (!long.TryParse(array[3], out num3))
			{
				if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Failed parsing: {0} as long", array[3]);
				}
				return false;
			}
			long num4;
			if (!long.TryParse(array[4], out num4))
			{
				if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Failed parsing: {0} as long", array[4]);
				}
				return false;
			}
			long num5;
			if (!long.TryParse(array[5], out num5))
			{
				if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Failed parsing: {0} as long", array[5]);
				}
				return false;
			}
			long num6;
			if (!long.TryParse(array[6], out num6))
			{
				if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Failed parsing: {0} as long", array[6]);
				}
				return false;
			}
			long num7;
			if (!long.TryParse(array[7], out num7))
			{
				if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Failed parsing: {0} as long", array[7]);
				}
				return false;
			}
			long num8;
			if (!long.TryParse(array[8], out num8))
			{
				if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Failed parsing: {0} as long", array[8]);
				}
				return false;
			}
			long num9;
			long num10;
			long num11;
			if (array.Length > 9)
			{
				if (!long.TryParse(array[9], out num9))
				{
					if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Failed parsing: {0} as long", array[9]);
					}
					return false;
				}
				if (!long.TryParse(array[10], out num10))
				{
					if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Failed parsing: {0} as long", array[10]);
					}
					return false;
				}
				if (!long.TryParse(array[11], out num11))
				{
					if (ExTraceGlobals.ConfigurationTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.ConfigurationTracer.TraceError<string>(0L, "Failed parsing: {0} as long", array[11]);
					}
					return false;
				}
			}
			else
			{
				num9 = (ConfigurationSchema.FoldersCountWarningQuota.Value.IsUnlimited ? long.MaxValue : ConfigurationSchema.FoldersCountWarningQuota.Value.Value);
				num10 = (ConfigurationSchema.FoldersCountReceiveQuota.Value.IsUnlimited ? long.MaxValue : ConfigurationSchema.FoldersCountReceiveQuota.Value.Value);
				num11 = (long)((ulong)ConfigurationSchema.MAPINamedPropsQuota.Value);
			}
			mailboxShape = new MailboxShape(num, num2, num3, num4, num5, num6, num7, num8, num9, num10, num11);
			return true;
		}

		// Token: 0x040005CA RID: 1482
		private readonly long messagesPerFolderCountWarningQuota;

		// Token: 0x040005CB RID: 1483
		private readonly long messagesPerFolderCountReceiveQuota;

		// Token: 0x040005CC RID: 1484
		private readonly long dumpsterMessagesPerFolderCountWarningQuota;

		// Token: 0x040005CD RID: 1485
		private readonly long dumpsterMessagesPerFolderCountReceiveQuota;

		// Token: 0x040005CE RID: 1486
		private readonly long folderHierarchyChildrenCountWarningQuota;

		// Token: 0x040005CF RID: 1487
		private readonly long folderHierarchyChildrenCountReceiveQuota;

		// Token: 0x040005D0 RID: 1488
		private readonly long folderHierarchyDepthWarningQuota;

		// Token: 0x040005D1 RID: 1489
		private readonly long folderHierarchyDepthReceiveQuota;

		// Token: 0x040005D2 RID: 1490
		private readonly long foldersCountWarningQuota;

		// Token: 0x040005D3 RID: 1491
		private readonly long foldersCountReceiveQuota;

		// Token: 0x040005D4 RID: 1492
		private readonly long namedPropertiesCountQuota;
	}
}
