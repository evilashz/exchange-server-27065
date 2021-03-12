using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Diagnostics;
using Microsoft.Exchange.Server.Storage.WorkerManager;

namespace Microsoft.Exchange.Server.Storage.Service
{
	// Token: 0x02000004 RID: 4
	internal sealed class StoreServiceDiagnosticHandler : StoreDiagnosticInfoHandler
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000298C File Offset: 0x00000B8C
		private StoreServiceDiagnosticHandler() : base("ManagedStoreService")
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002999 File Offset: 0x00000B99
		public static StoreDiagnosticInfoHandler Instance
		{
			get
			{
				if (StoreServiceDiagnosticHandler.instance == null)
				{
					StoreServiceDiagnosticHandler.instance = new StoreServiceDiagnosticHandler();
				}
				return StoreServiceDiagnosticHandler.instance;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000029B4 File Offset: 0x00000BB4
		public override XElement GetDiagnosticQuery(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement("Workers");
			foreach (IWorkerProcess workerProcess in WorkerManager.Instance.GetActiveWorkers())
			{
				xelement.Add(new XElement("Worker", new object[]
				{
					new XAttribute("MdbGuid", workerProcess.InstanceId),
					new XAttribute("ProcessId", workerProcess.ProcessId)
				}));
			}
			return xelement;
		}

		// Token: 0x04000009 RID: 9
		private static StoreDiagnosticInfoHandler instance;
	}
}
