using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000866 RID: 2150
	internal class ActivationListener : MarshalByRefObject, IActivator
	{
		// Token: 0x06005BED RID: 23533 RVA: 0x00141CA8 File Offset: 0x0013FEA8
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06005BEE RID: 23534 RVA: 0x00141CAB File Offset: 0x0013FEAB
		// (set) Token: 0x06005BEF RID: 23535 RVA: 0x00141CAE File Offset: 0x0013FEAE
		public virtual IActivator NextActivator
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06005BF0 RID: 23536 RVA: 0x00141CB5 File Offset: 0x0013FEB5
		public virtual ActivatorLevel Level
		{
			[SecurityCritical]
			get
			{
				return ActivatorLevel.AppDomain;
			}
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x00141CBC File Offset: 0x0013FEBC
		[SecurityCritical]
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null || RemotingServices.IsTransparentProxy(ctorMsg))
			{
				throw new ArgumentNullException("ctorMsg");
			}
			ctorMsg.Properties["Permission"] = "allowed";
			string activationTypeName = ctorMsg.ActivationTypeName;
			if (!RemotingConfigHandler.IsActivationAllowed(activationTypeName))
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_PermissionDenied"), ctorMsg.ActivationTypeName));
			}
			Type activationType = ctorMsg.ActivationType;
			if (activationType == null)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), ctorMsg.ActivationTypeName));
			}
			return ActivationServices.GetActivator().Activate(ctorMsg);
		}
	}
}
