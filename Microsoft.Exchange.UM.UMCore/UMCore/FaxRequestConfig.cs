using System;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001BD RID: 445
	internal class FaxRequestConfig : ActivityConfig
	{
		// Token: 0x06000CF7 RID: 3319 RVA: 0x00038FD4 File Offset: 0x000371D4
		internal FaxRequestConfig(ActivityManagerConfig manager) : base(manager)
		{
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00038FDD File Offset: 0x000371DD
		internal override void Load(XmlNode rootNode)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Loading a new FaxRequestConfig from XML.", new object[0]);
			base.Load(rootNode);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00038FFC File Offset: 0x000371FC
		internal override ActivityBase CreateActivity(ActivityManager manager)
		{
			return new FaxRequest(manager, this);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00039008 File Offset: 0x00037208
		protected override void LoadComplete()
		{
			if (!ActivityConfig.TransitionMap.ContainsKey(ActivityConfig.BuildTransitionMapKey(this, "faxRequestAccepted")))
			{
				CallIdTracer.TraceError(ExTraceGlobals.StateMachineTracer, this, "Fax Activity id={0} doesn't have a FaxRequestAccepted Transition", new object[]
				{
					base.ActivityId
				});
				throw new FsmConfigurationException(Strings.FaxRequestActivityWithoutFaxRequestAccepted(base.ActivityId));
			}
		}
	}
}
