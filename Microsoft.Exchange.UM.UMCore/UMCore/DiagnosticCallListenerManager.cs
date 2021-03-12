using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000113 RID: 275
	internal class DiagnosticCallListenerManager : ActivityManager
	{
		// Token: 0x060007C0 RID: 1984 RVA: 0x0001F61F File Offset: 0x0001D81F
		internal DiagnosticCallListenerManager(ActivityManager manager, DiagnosticCallListenerManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001F629 File Offset: 0x0001D829
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			this.umdialPlan = null;
			base.Manager.WriteVariable("diagnosticTUILogonCheck", false);
			base.Start(vo, refInfo);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001F650 File Offset: 0x0001D850
		internal override void CheckAuthorization(UMSubscriber u)
		{
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001F654 File Offset: 0x0001D854
		internal override void OnMessageReceived(BaseUMCallSession vo, InfoMessage.MessageReceivedEventArgs e)
		{
			base.OnMessageReceived(vo, e);
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "DiagnosticCallListenerManager in  OnMessageReceived.", new object[0]);
			if (!e.Message.ContentType.Equals(CommonConstants.ContentTypeTextPlain) || !Encoding.ASCII.GetString(e.Message.Body).Equals("UM Operation Check"))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "DiagnosticCallListenerManager disconnecting call for error in content type or body of SIP INFO.", new object[0]);
				vo.DisconnectCall();
			}
			string text;
			if (e.Message.Headers.TryGetValue("UMDialPlan", out text) && !string.IsNullOrEmpty(text))
			{
				this.umdialPlan = this.GetFirstOrgDialPlanFromName(text);
				if (this.umdialPlan != null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "DiagnosticCallListenerManager setting DiagnosticTUILogonCheck since found Dialplan info.", new object[0]);
					base.Manager.WriteVariable("diagnosticTUILogonCheck", true);
				}
			}
			InfoMessage infoMessage = new InfoMessage();
			infoMessage.ContentType = CommonConstants.ContentTypeTextPlain;
			infoMessage.Headers["UMTUCFirstResponse"] = "true";
			string text2;
			if (UmServiceGlobals.ArePerfCountersEnabled)
			{
				long rawValue = AvailabilityCounters.TotalQueuedMessages.RawValue;
				text2 = string.Concat(new string[]
				{
					"OK:",
					GeneralCounters.CurrentCalls.RawValue.ToString(CultureInfo.InvariantCulture),
					":",
					rawValue.ToString(CultureInfo.InvariantCulture),
					":",
					PipelineDispatcher.Instance.IsPipelineHealthy ? "1" : "0"
				});
			}
			else
			{
				text2 = "OK:0:0:1";
			}
			infoMessage.Body = Encoding.ASCII.GetBytes(text2);
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "DiagnosticCallListenerManager sending resp = {0}.", new object[]
			{
				text2
			});
			vo.SendMessage(infoMessage);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001F824 File Offset: 0x0001DA24
		internal override void OnMessageSent(BaseUMCallSession vo, EventArgs e)
		{
			base.OnMessageSent(vo, e);
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "DiagnosticCallListenerManager in OnMessageSent.", new object[0]);
			object obj = base.Manager.ReadVariable("diagnosticTUILogonCheck");
			bool flag = obj != null && (bool)obj;
			if (flag)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "DiagnosticCallListenerManager in OnMessageSent , diagnosticTUILogonCheck is set.", new object[0]);
				vo.CurrentCallContext.DialPlan = this.umdialPlan;
				vo.CurrentCallContext.CalleeInfo = null;
				vo.CurrentCallContext.CallerInfo = null;
				vo.CurrentCallContext.CallType = 1;
				vo.CurrentCallContext.CallerId = PhoneNumber.Empty;
				vo.CurrentCallContext.IsTUIDiagnosticCall = true;
			}
			vo.StopPlayback();
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001F8DD File Offset: 0x0001DADD
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DiagnosticCallListenerManager>(this);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001F8E8 File Offset: 0x0001DAE8
		private UMDialPlan GetFirstOrgDialPlanFromName(string dialPlanName)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 202, "GetFirstOrgDialPlanFromName", "f:\\15.00.1497\\sources\\dev\\um\\src\\umcore\\DiagnosticCallListenerManager.cs");
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, dialPlanName);
			ADObjectId descendantId = tenantOrTopologyConfigurationSession.GetOrgContainerId().GetDescendantId(new ADObjectId("CN=UM DialPlan Container", Guid.Empty));
			UMDialPlan[] array = tenantOrTopologyConfigurationSession.Find<UMDialPlan>(descendantId, QueryScope.SubTree, filter, null, 0);
			if (array != null && array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x04000851 RID: 2129
		private UMDialPlan umdialPlan;

		// Token: 0x02000114 RID: 276
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x060007C7 RID: 1991 RVA: 0x0001F957 File Offset: 0x0001DB57
			internal ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x060007C8 RID: 1992 RVA: 0x0001F960 File Offset: 0x0001DB60
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing UMDiagnostic activity manager.", new object[0]);
				return new DiagnosticCallListenerManager(manager, this);
			}
		}
	}
}
