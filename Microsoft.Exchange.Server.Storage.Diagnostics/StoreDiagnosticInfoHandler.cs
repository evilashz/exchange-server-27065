using System;
using System.Xml.Linq;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000014 RID: 20
	internal abstract class StoreDiagnosticInfoHandler : IDiagnosable
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00005041 File Offset: 0x00003241
		protected StoreDiagnosticInfoHandler(string componentName)
		{
			this.componentName = componentName;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000506C File Offset: 0x0000326C
		public string GetDiagnosticComponentName()
		{
			StoreDiagnosticInfoHandler.<>c__DisplayClass1 CS$<>8__locals1 = new StoreDiagnosticInfoHandler.<>c__DisplayClass1();
			CS$<>8__locals1.<>4__this = this;
			ExecutionDiagnostics executionDiagnostics = new ExecutionDiagnostics();
			CS$<>8__locals1.result = null;
			WatsonOnUnhandledException.Guard(executionDiagnostics, new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<GetDiagnosticComponentName>b__0)));
			return CS$<>8__locals1.result;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000050CC File Offset: 0x000032CC
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			StoreDiagnosticInfoHandler.<>c__DisplayClass4 CS$<>8__locals1 = new StoreDiagnosticInfoHandler.<>c__DisplayClass4();
			CS$<>8__locals1.parameters = parameters;
			CS$<>8__locals1.<>4__this = this;
			ExecutionDiagnostics executionDiagnostics = new ExecutionDiagnostics();
			CS$<>8__locals1.result = null;
			WatsonOnUnhandledException.Guard(executionDiagnostics, new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<GetDiagnosticInfo>b__3)));
			return CS$<>8__locals1.result;
		}

		// Token: 0x060000A7 RID: 167
		public abstract XElement GetDiagnosticQuery(DiagnosableParameters parameters);

		// Token: 0x060000A8 RID: 168 RVA: 0x00005112 File Offset: 0x00003312
		public void Register()
		{
			ProcessAccessManager.RegisterComponent(this);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000511A File Offset: 0x0000331A
		public void Deregister()
		{
			ProcessAccessManager.UnregisterComponent(this);
		}

		// Token: 0x0400008A RID: 138
		private readonly string componentName;
	}
}
