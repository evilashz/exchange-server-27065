using System;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000011 RID: 17
	internal abstract class MigrationComponent : IDiagnosable
	{
		// Token: 0x0600004E RID: 78 RVA: 0x0000300B File Offset: 0x0000120B
		internal MigrationComponent(string name, WaitHandle stopEvent)
		{
			this.Name = name;
			this.StopEvent = stopEvent;
			this.DiagnosticInfo = new MigrationComponentDiagnosticInfo();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000302C File Offset: 0x0000122C
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00003034 File Offset: 0x00001234
		internal MigrationComponentDiagnosticInfo DiagnosticInfo { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000303D File Offset: 0x0000123D
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00003045 File Offset: 0x00001245
		internal string Name { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000304E File Offset: 0x0000124E
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00003056 File Offset: 0x00001256
		private protected WaitHandle StopEvent { protected get; private set; }

		// Token: 0x06000055 RID: 85 RVA: 0x0000305F File Offset: 0x0000125F
		public string GetDiagnosticComponentName()
		{
			return this.Name;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003068 File Offset: 0x00001268
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return new XElement(this.GetDiagnosticComponentName(), new object[]
			{
				new XAttribute("name", this.Name),
				new XElement("duration", this.DiagnosticInfo.Duration),
				new XElement("lastRunTime", this.DiagnosticInfo.LastRunTime.UniversalTime),
				new XElement("lastWorkTime", this.DiagnosticInfo.LastWorkTime.UniversalTime)
			});
		}

		// Token: 0x06000057 RID: 87
		internal abstract bool ShouldProcess();

		// Token: 0x06000058 RID: 88
		internal abstract bool Process(IMigrationJobCache data);
	}
}
