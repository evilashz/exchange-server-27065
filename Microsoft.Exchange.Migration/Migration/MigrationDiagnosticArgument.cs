using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000057 RID: 87
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationDiagnosticArgument : DiagnosableArgument
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
		public MigrationDiagnosticArgument(string argument)
		{
			base.Initialize(argument);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000F704 File Offset: 0x0000D904
		protected override void InitializeSchema(Dictionary<string, Type> schema)
		{
			schema["user"] = typeof(string);
			schema["organization"] = typeof(string);
			schema["partition"] = typeof(string);
			schema["storage"] = typeof(bool);
			schema["status"] = typeof(string);
			schema["maxsize"] = typeof(int);
			schema["type"] = typeof(string);
			schema["batch"] = typeof(string);
			schema["reports"] = typeof(bool);
			schema["endpoints"] = typeof(bool);
			schema["reportid"] = typeof(string);
			schema["attachmentid"] = typeof(string);
			schema["verbose"] = typeof(bool);
			schema["slotid"] = typeof(Guid);
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000F837 File Offset: 0x0000DA37
		protected override bool FailOnMissingArgument
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000F83C File Offset: 0x0000DA3C
		public override XElement RunDiagnosticOperation(Func<XElement> operation)
		{
			XElement result;
			try
			{
				result = operation();
			}
			catch (MigrationMailboxNotFoundOnServerException ex)
			{
				result = new XElement("error", "mailbox moved to " + ex.HostServer);
			}
			catch (TransientException ex2)
			{
				result = new XElement("error", "Encountered exception: " + ex2.Message);
			}
			catch (MigrationDataCorruptionException ex3)
			{
				result = new XElement("error", "Encountered data corruption exception: " + ex3.Message);
			}
			catch (InvalidDataException ex4)
			{
				result = new XElement("error", "Encountered exception: " + ex4.Message);
			}
			catch (MigrationPermanentException ex5)
			{
				result = new XElement("error", "Encountered exception: " + ex5.Message);
			}
			catch (StoragePermanentException ex6)
			{
				result = new XElement("error", "Encountered exception: " + ex6.Message);
			}
			catch (DiagnosticArgumentException ex7)
			{
				result = new XElement("error", "Encountered exception: " + ex7.Message);
			}
			return result;
		}

		// Token: 0x04000145 RID: 325
		public const string VerboseArgument = "verbose";

		// Token: 0x04000146 RID: 326
		public const string UserArgument = "user";

		// Token: 0x04000147 RID: 327
		public const string OrganizationArgument = "organization";

		// Token: 0x04000148 RID: 328
		public const string PartitionArgument = "partition";

		// Token: 0x04000149 RID: 329
		public const string StorageArgument = "storage";

		// Token: 0x0400014A RID: 330
		public const string StatusArgument = "status";

		// Token: 0x0400014B RID: 331
		public const string MaxSizeArgument = "maxsize";

		// Token: 0x0400014C RID: 332
		public const string TypeArgument = "type";

		// Token: 0x0400014D RID: 333
		public const string BatchNameArgument = "batch";

		// Token: 0x0400014E RID: 334
		public const string ReportsArgument = "reports";

		// Token: 0x0400014F RID: 335
		public const string EndpointsArgument = "endpoints";

		// Token: 0x04000150 RID: 336
		public const string ReportIdArgument = "reportid";

		// Token: 0x04000151 RID: 337
		public const string AttachmentIdArgument = "attachmentid";

		// Token: 0x04000152 RID: 338
		public const string SlotIdArgument = "slotid";
	}
}
