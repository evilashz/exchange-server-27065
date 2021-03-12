using System;
using System.IO;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001E5 RID: 485
	internal class OABLogFileSettings : ActivityContextLogFileSettings
	{
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x0006E224 File Offset: 0x0006C424
		protected override string LogTypeName
		{
			get
			{
				return "OAB Generator Log";
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x0006E22B File Offset: 0x0006C42B
		protected override string LogSubFolderName
		{
			get
			{
				return "OABGeneratorLog";
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x0006E232 File Offset: 0x0006C432
		protected override Trace Tracer
		{
			get
			{
				return OABLogger.OABTracer;
			}
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0006E239 File Offset: 0x0006C439
		internal static OABLogFileSettings Load()
		{
			return new OABLogFileSettings();
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0006E240 File Offset: 0x0006C440
		private OABLogFileSettings()
		{
			base.DirectoryPath = Path.GetFullPath(Path.Combine(base.DirectoryPath, "..\\" + this.LogSubFolderName));
		}

		// Token: 0x04000B70 RID: 2928
		private const string OABLogSubFolderName = "OABGeneratorLog";
	}
}
