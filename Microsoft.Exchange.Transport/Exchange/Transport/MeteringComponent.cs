using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Metering;
using Microsoft.Exchange.Data.Metering.Throttling;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200002E RID: 46
	internal class MeteringComponent : IMeteringComponent, ITransportComponent, IDiagnosable
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x000046C4 File Offset: 0x000028C4
		public MeteringComponent() : this(() => DateTime.UtcNow)
		{
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000046E9 File Offset: 0x000028E9
		public MeteringComponent(Func<DateTime> currentTimeProvider)
		{
			this.currentTimeProvider = currentTimeProvider;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000046F8 File Offset: 0x000028F8
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00004700 File Offset: 0x00002900
		public ICountTracker<MeteredEntity, MeteredCount> Metering { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004709 File Offset: 0x00002909
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00004711 File Offset: 0x00002911
		public ICountTrackerDiagnostics<MeteredEntity, MeteredCount> MeteringDiagnostics { get; private set; }

		// Token: 0x060000F7 RID: 247 RVA: 0x0000471A File Offset: 0x0000291A
		public void SetLoadtimeDependencies(ICountTrackerConfig config, Trace tracer)
		{
			this.config = config;
			this.tracer = tracer;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000472A File Offset: 0x0000292A
		public void Load()
		{
			this.MeteringDiagnostics = new CountTrackerDiagnostics<MeteredEntity, MeteredCount>();
			this.Metering = new CountTracker<MeteredEntity, MeteredCount>(this.config, this.MeteringDiagnostics, this.tracer, this.currentTimeProvider);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000475A File Offset: 0x0000295A
		public void Unload()
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000475C File Offset: 0x0000295C
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000475F File Offset: 0x0000295F
		public string GetDiagnosticComponentName()
		{
			return "MeteringComponent";
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004768 File Offset: 0x00002968
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(((IDiagnosable)this).GetDiagnosticComponentName());
			bool flag = parameters.Argument.Equals("config", StringComparison.InvariantCultureIgnoreCase);
			bool flag2 = !flag || parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			if (parameters.Argument.StartsWith("Tenant-", StringComparison.InvariantCultureIgnoreCase))
			{
				xelement.Add(this.Metering.GetDiagnosticInfo(new SimpleEntityName<MeteredEntity>(MeteredEntity.Tenant, parameters.Argument.Substring("Tenant-".Length))));
			}
			else if (parameters.Argument.StartsWith("Sender-", StringComparison.InvariantCultureIgnoreCase))
			{
				xelement.Add(this.Metering.GetDiagnosticInfo(new SimpleEntityName<MeteredEntity>(MeteredEntity.Sender, parameters.Argument.Substring("Sender-".Length))));
			}
			else
			{
				this.Metering.GetDiagnosticInfo(parameters.Argument, xelement);
			}
			if (flag)
			{
				xelement.Add(TransportAppConfig.GetDiagnosticInfoForType(this.config));
			}
			if (flag2)
			{
				xelement.Add(new XElement("help", string.Format("Supported arguments: verbose, help, config, {0}'tenantguid', {1}'sender'", "Tenant-", "Sender-")));
			}
			return xelement;
		}

		// Token: 0x0400006F RID: 111
		private ICountTrackerConfig config;

		// Token: 0x04000070 RID: 112
		private Trace tracer;

		// Token: 0x04000071 RID: 113
		private Func<DateTime> currentTimeProvider;
	}
}
