using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000683 RID: 1667
	[ParseChildren(true, "Parameters")]
	[ClientScriptResource("WebServiceMethod", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	[DefaultProperty("Parameters")]
	public class WebServiceMethod : ScriptComponent
	{
		// Token: 0x06004805 RID: 18437 RVA: 0x000DB1F6 File Offset: 0x000D93F6
		public WebServiceMethod()
		{
			this.ExceptionHandlers = new List<WebServiceExceptionHandler>();
		}

		// Token: 0x1700279C RID: 10140
		// (get) Token: 0x06004806 RID: 18438 RVA: 0x000DB214 File Offset: 0x000D9414
		// (set) Token: 0x06004807 RID: 18439 RVA: 0x000DB21C File Offset: 0x000D941C
		public string Method { get; set; }

		// Token: 0x1700279D RID: 10141
		// (get) Token: 0x06004808 RID: 18440 RVA: 0x000DB225 File Offset: 0x000D9425
		// (set) Token: 0x06004809 RID: 18441 RVA: 0x000DB22D File Offset: 0x000D942D
		public WebServiceReference ServiceUrl { get; set; }

		// Token: 0x1700279E RID: 10142
		// (get) Token: 0x0600480A RID: 18442 RVA: 0x000DB236 File Offset: 0x000D9436
		// (set) Token: 0x0600480B RID: 18443 RVA: 0x000DB23E File Offset: 0x000D943E
		public bool AlwaysInvokeSave { get; set; }

		// Token: 0x1700279F RID: 10143
		// (get) Token: 0x0600480C RID: 18444 RVA: 0x000DB247 File Offset: 0x000D9447
		// (set) Token: 0x0600480D RID: 18445 RVA: 0x000DB24F File Offset: 0x000D944F
		public string OnClientInvoking { get; set; }

		// Token: 0x170027A0 RID: 10144
		// (get) Token: 0x0600480E RID: 18446 RVA: 0x000DB258 File Offset: 0x000D9458
		// (set) Token: 0x0600480F RID: 18447 RVA: 0x000DB260 File Offset: 0x000D9460
		public string OnClientCompleted { get; set; }

		// Token: 0x170027A1 RID: 10145
		// (get) Token: 0x06004810 RID: 18448 RVA: 0x000DB269 File Offset: 0x000D9469
		// (set) Token: 0x06004811 RID: 18449 RVA: 0x000DB271 File Offset: 0x000D9471
		public string OnClientSucceeded { get; set; }

		// Token: 0x170027A2 RID: 10146
		// (get) Token: 0x06004812 RID: 18450 RVA: 0x000DB27A File Offset: 0x000D947A
		// (set) Token: 0x06004813 RID: 18451 RVA: 0x000DB282 File Offset: 0x000D9482
		public string OnClientFailed { get; set; }

		// Token: 0x170027A3 RID: 10147
		// (get) Token: 0x06004814 RID: 18452 RVA: 0x000DB28B File Offset: 0x000D948B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public BindingCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x170027A4 RID: 10148
		// (get) Token: 0x06004815 RID: 18453 RVA: 0x000DB293 File Offset: 0x000D9493
		// (set) Token: 0x06004816 RID: 18454 RVA: 0x000DB29B File Offset: 0x000D949B
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public WebServiceParameterNames ParameterNames { get; set; }

		// Token: 0x170027A5 RID: 10149
		// (get) Token: 0x06004817 RID: 18455 RVA: 0x000DB2A4 File Offset: 0x000D94A4
		// (set) Token: 0x06004818 RID: 18456 RVA: 0x000DB2AC File Offset: 0x000D94AC
		public List<WebServiceExceptionHandler> ExceptionHandlers { get; private set; }

		// Token: 0x170027A6 RID: 10150
		// (get) Token: 0x06004819 RID: 18457 RVA: 0x000DB2BD File Offset: 0x000D94BD
		public string ExceptionHandlersIDs
		{
			get
			{
				return string.Join(",", (from handler in this.ExceptionHandlers
				select handler.ClientID).ToArray<string>());
			}
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x000DB2F8 File Offset: 0x000D94F8
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddEvent("Invoking", this.OnClientInvoking, true);
			descriptor.AddEvent("Completed", this.OnClientCompleted, true);
			descriptor.AddEvent("Succeeded", this.OnClientSucceeded, true);
			descriptor.AddEvent("Failed", this.OnClientFailed, true);
			descriptor.AddProperty("ParameterNames", this.ParameterNames);
			descriptor.AddProperty("ServiceUrl", EcpUrl.ProcessUrl(this.ServiceUrl.ServiceUrl));
			descriptor.AddProperty("MethodName", this.Method);
			descriptor.AddProperty("ExceptionHandlerIDs", this.ExceptionHandlersIDs);
			descriptor.AddScriptProperty("Parameters", this.Parameters.ToJavaScript(this));
			descriptor.AddProperty("AlwaysInvokeSave", this.AlwaysInvokeSave);
		}

		// Token: 0x04003062 RID: 12386
		private BindingCollection parameters = new BindingCollection();
	}
}
