using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000256 RID: 598
	internal class DialplanResolver : ICallHandler
	{
		// Token: 0x060011B1 RID: 4529 RVA: 0x0004E158 File Offset: 0x0004C358
		public void HandleCall(CafeRoutingContext context)
		{
			ValidateArgument.NotNull(context, "RoutingContext");
			context.Tracer.Trace("DialplanResolver : TryHandleCall IsAccessProxyCall ={0}", new object[]
			{
				context.IsAccessProxyCall
			});
			if (context.DialPlan == null)
			{
				this.InitializeContext(context);
			}
			if (context.DialPlan == null)
			{
				context.Tracer.Trace("Dialplanresolver : TryHandleCall : Dialplan not found", new object[0]);
				throw CallRejectedException.Create(Strings.NoDialPlanFound, CallEndingReason.DialPlanNotFound, null, new object[0]);
			}
			this.InitializeCallerAndCallee(context);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0004E1E4 File Offset: 0x0004C3E4
		private void InitializeContext(CafeRoutingContext context)
		{
			context.Tracer.Trace("DialplanResolver : InitializeContext", new object[0]);
			ADObjectId gatewayDialPlanId;
			bool gatewayInOnlyOneDialplan;
			context.HuntGroup = HuntGroupUtils.GetHuntGroup(context.PilotNumber, context.Gateway, context.RequestUriOfCall, context.RemotePeer, context.ScopedADConfigurationSession, context.IsSecuredCall, out gatewayDialPlanId, out gatewayInOnlyOneDialplan);
			if (!this.TryInitializeAutoAttendantsIfAny(context, gatewayDialPlanId, gatewayInOnlyOneDialplan))
			{
				this.InitializeDialPlan(context);
			}
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0004E24C File Offset: 0x0004C44C
		private void InitializeDialPlan(CafeRoutingContext context)
		{
			if (context.HuntGroup != null)
			{
				context.Tracer.Trace("DialplanResolver : InitializeDialPlan :context.HuntGroup != null", new object[0]);
				context.DialPlan = context.ScopedADConfigurationSession.GetDialPlanFromId(context.HuntGroup.UMDialPlan);
			}
			else
			{
				context.Tracer.Trace("DialplanResolver : InitializeDialPlan :context.HuntGroup == null", new object[0]);
				UMHuntGroup umhuntGroup = null;
				HuntGroupUtils.TryGetDefaultHuntGroup(context.Gateway, context.PilotNumber, out umhuntGroup);
				if (umhuntGroup == null)
				{
					throw CallRejectedException.Create(Strings.CallFromInvalidHuntGroup(context.PilotNumber, context.RemotePeer.ToString()), CallEndingReason.IncorrectHuntGroup, "Pilot number: {0}. UMIPGateway: {1}.", new object[]
					{
						context.PilotNumber,
						context.RemotePeer.ToString()
					});
				}
				context.Tracer.Trace("DialplanResolver : InitializeDialPlan :context.HuntGroup ={0}", new object[]
				{
					umhuntGroup.Name
				});
				context.HuntGroup = umhuntGroup;
				context.DialPlan = context.ScopedADConfigurationSession.GetDialPlanFromId(umhuntGroup.UMDialPlan);
			}
			this.EnsureDialplanExists(context);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0004E354 File Offset: 0x0004C554
		private void EnsureDialplanExists(CafeRoutingContext context)
		{
			if (context.HuntGroup == null)
			{
				context.Tracer.Trace("Dialplanresolver : EnsureDialplanExists : Huntgroup not found", new object[0]);
				throw CallRejectedException.Create(Strings.CallFromInvalidHuntGroup(context.PilotNumber, context.Gateway.ToString()), CallEndingReason.IncorrectHuntGroup, "Pilot number: {0}. UMIPGateway: {1}.", new object[]
				{
					context.PilotNumber,
					context.Gateway.ToString()
				});
			}
			if (context.DialPlan == null)
			{
				context.Tracer.Trace("Dialplanresolver : EnsureDialplanExists : Dialplan not found for Huntgroup {0}", new object[]
				{
					context.HuntGroup.Name
				});
				throw CallRejectedException.Create(Strings.DialPlanNotFound(context.HuntGroup.UMDialPlan.DistinguishedName), CallEndingReason.DialPlanNotFound, "UM dial plan: {0}.", new object[]
				{
					context.HuntGroup.UMDialPlan.DistinguishedName
				});
			}
			context.Tracer.Trace("Dialplanresolver : EnsureDialplanExists : Dialplan {0} found for Huntgroup {1}", new object[]
			{
				context.DialPlan.Name,
				context.HuntGroup.Name
			});
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0004E468 File Offset: 0x0004C668
		private bool TryInitializeAutoAttendantsIfAny(CafeRoutingContext context, ADObjectId gatewayDialPlanId, bool gatewayInOnlyOneDialplan)
		{
			context.Tracer.Trace("Dialplanresolver : TryInitializeAutoAttendantsIfAny : context.PilotNumber ={0}", new object[]
			{
				context.PilotNumber ?? "null"
			});
			if (!string.IsNullOrEmpty(context.PilotNumber))
			{
				UMAutoAttendant autoAttendant;
				if (context.HuntGroup == null)
				{
					context.Tracer.Trace("Dialplanresolver : TryInitializeAutoAttendantsIfAny : Huntgroup is null", new object[0]);
					autoAttendant = AutoAttendantUtils.GetAutoAttendant(context.PilotNumber, context.RequestUriOfCall, context.Gateway, gatewayInOnlyOneDialplan, context.IsSecuredCall, gatewayDialPlanId, context.ScopedADConfigurationSession);
				}
				else
				{
					context.Tracer.Trace("Dialplanresolver : TryInitializeAutoAttendantsIfAny : Huntgroup ={0}", new object[]
					{
						context.HuntGroup.Name
					});
					autoAttendant = AutoAttendantUtils.GetAutoAttendant(context.PilotNumber, context.HuntGroup, context.RequestUriOfCall, context.IsSecuredCall, context.ScopedADConfigurationSession);
				}
				if (AutoAttendantUtils.IsAutoAttendantUsable(autoAttendant, context.PilotNumber))
				{
					context.AutoAttendant = autoAttendant;
					context.Tracer.Trace("Dialplanresolver : TryInitializeAutoAttendantsIfAny : AutoAttendant ={0}", new object[]
					{
						context.AutoAttendant.Name
					});
					context.DialPlan = context.ScopedADConfigurationSession.GetDialPlanFromId(context.AutoAttendant.UMDialPlan);
					if (context.DialPlan == null)
					{
						context.Tracer.Trace("Dialplanresolver : InitializeAutoAttendantsIfAny : Dialplan not found for AA {0}", new object[]
						{
							context.AutoAttendant.Name
						});
						throw CallRejectedException.Create(Strings.DialPlanNotFound(context.AutoAttendant.UMDialPlan.DistinguishedName), CallEndingReason.DialPlanNotFound, "UM dial plan: {0}.", new object[]
						{
							context.AutoAttendant.UMDialPlan.DistinguishedName
						});
					}
				}
			}
			return context.AutoAttendant != null;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0004E61C File Offset: 0x0004C81C
		private void InitializeCallerAndCallee(CafeRoutingContext context)
		{
			PhoneNumber phoneNumber = null;
			RouterUtils.ParseTelephonyAddress(context.CallInfo.CallingParty, context.DialPlan, true, out phoneNumber);
			context.CallingParty = phoneNumber;
			RouterUtils.ParseSipUri(context.RequestUriOfCall, context.DialPlan, out phoneNumber);
			context.CalledParty = phoneNumber;
			context.Tracer.Trace("Dialplanresolver : InitializeCallerAndCallee : Caller ={0}, Callee ={1}", new object[]
			{
				context.CallingParty,
				context.CalledParty
			});
		}
	}
}
