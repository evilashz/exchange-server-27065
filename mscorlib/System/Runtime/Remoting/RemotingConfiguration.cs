using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting
{
	// Token: 0x02000792 RID: 1938
	[ComVisible(true)]
	public static class RemotingConfiguration
	{
		// Token: 0x060054B4 RID: 21684 RVA: 0x0012C262 File Offset: 0x0012A462
		[SecuritySafeCritical]
		[Obsolete("Use System.Runtime.Remoting.RemotingConfiguration.Configure(string fileName, bool ensureSecurity) instead.", false)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void Configure(string filename)
		{
			RemotingConfiguration.Configure(filename, false);
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x0012C26B File Offset: 0x0012A46B
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void Configure(string filename, bool ensureSecurity)
		{
			RemotingConfigHandler.DoConfiguration(filename, ensureSecurity);
			RemotingServices.InternalSetRemoteActivationConfigured();
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x060054B6 RID: 21686 RVA: 0x0012C279 File Offset: 0x0012A479
		// (set) Token: 0x060054B7 RID: 21687 RVA: 0x0012C289 File Offset: 0x0012A489
		public static string ApplicationName
		{
			get
			{
				if (!RemotingConfigHandler.HasApplicationNameBeenSet())
				{
					return null;
				}
				return RemotingConfigHandler.ApplicationName;
			}
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				RemotingConfigHandler.ApplicationName = value;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x060054B8 RID: 21688 RVA: 0x0012C291 File Offset: 0x0012A491
		public static string ApplicationId
		{
			[SecurityCritical]
			get
			{
				return Identity.AppDomainUniqueId;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x060054B9 RID: 21689 RVA: 0x0012C298 File Offset: 0x0012A498
		public static string ProcessId
		{
			[SecurityCritical]
			get
			{
				return Identity.ProcessGuid;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x060054BA RID: 21690 RVA: 0x0012C29F File Offset: 0x0012A49F
		// (set) Token: 0x060054BB RID: 21691 RVA: 0x0012C2A6 File Offset: 0x0012A4A6
		public static CustomErrorsModes CustomErrorsMode
		{
			get
			{
				return RemotingConfigHandler.CustomErrorsMode;
			}
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				RemotingConfigHandler.CustomErrorsMode = value;
			}
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x0012C2B0 File Offset: 0x0012A4B0
		public static bool CustomErrorsEnabled(bool isLocalRequest)
		{
			switch (RemotingConfiguration.CustomErrorsMode)
			{
			case CustomErrorsModes.On:
				return true;
			case CustomErrorsModes.Off:
				return false;
			case CustomErrorsModes.RemoteOnly:
				return !isLocalRequest;
			default:
				return true;
			}
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x0012C2E4 File Offset: 0x0012A4E4
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterActivatedServiceType(Type type)
		{
			ActivatedServiceTypeEntry entry = new ActivatedServiceTypeEntry(type);
			RemotingConfiguration.RegisterActivatedServiceType(entry);
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x0012C2FE File Offset: 0x0012A4FE
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
		{
			RemotingConfigHandler.RegisterActivatedServiceType(entry);
			if (!RemotingConfiguration.s_ListeningForActivationRequests)
			{
				RemotingConfiguration.s_ListeningForActivationRequests = true;
				ActivationServices.StartListeningForRemoteRequests();
			}
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x0012C31C File Offset: 0x0012A51C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterWellKnownServiceType(Type type, string objectUri, WellKnownObjectMode mode)
		{
			WellKnownServiceTypeEntry entry = new WellKnownServiceTypeEntry(type, objectUri, mode);
			RemotingConfiguration.RegisterWellKnownServiceType(entry);
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x0012C338 File Offset: 0x0012A538
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
		{
			RemotingConfigHandler.RegisterWellKnownServiceType(entry);
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x0012C340 File Offset: 0x0012A540
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterActivatedClientType(Type type, string appUrl)
		{
			ActivatedClientTypeEntry entry = new ActivatedClientTypeEntry(type, appUrl);
			RemotingConfiguration.RegisterActivatedClientType(entry);
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x0012C35B File Offset: 0x0012A55B
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
		{
			RemotingConfigHandler.RegisterActivatedClientType(entry);
			RemotingServices.InternalSetRemoteActivationConfigured();
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x0012C368 File Offset: 0x0012A568
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterWellKnownClientType(Type type, string objectUrl)
		{
			WellKnownClientTypeEntry entry = new WellKnownClientTypeEntry(type, objectUrl);
			RemotingConfiguration.RegisterWellKnownClientType(entry);
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x0012C383 File Offset: 0x0012A583
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
		{
			RemotingConfigHandler.RegisterWellKnownClientType(entry);
			RemotingServices.InternalSetRemoteActivationConfigured();
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x0012C390 File Offset: 0x0012A590
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
		{
			return RemotingConfigHandler.GetRegisteredActivatedServiceTypes();
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x0012C397 File Offset: 0x0012A597
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
		{
			return RemotingConfigHandler.GetRegisteredWellKnownServiceTypes();
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x0012C39E File Offset: 0x0012A59E
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
		{
			return RemotingConfigHandler.GetRegisteredActivatedClientTypes();
		}

		// Token: 0x060054C8 RID: 21704 RVA: 0x0012C3A5 File Offset: 0x0012A5A5
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
		{
			return RemotingConfigHandler.GetRegisteredWellKnownClientTypes();
		}

		// Token: 0x060054C9 RID: 21705 RVA: 0x0012C3AC File Offset: 0x0012A5AC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(Type svrType)
		{
			if (svrType == null)
			{
				throw new ArgumentNullException("svrType");
			}
			RuntimeType runtimeType = svrType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			return RemotingConfigHandler.IsRemotelyActivatedClientType(runtimeType);
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x0012C3F3 File Offset: 0x0012A5F3
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(string typeName, string assemblyName)
		{
			return RemotingConfigHandler.IsRemotelyActivatedClientType(typeName, assemblyName);
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x0012C3FC File Offset: 0x0012A5FC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static WellKnownClientTypeEntry IsWellKnownClientType(Type svrType)
		{
			if (svrType == null)
			{
				throw new ArgumentNullException("svrType");
			}
			RuntimeType runtimeType = svrType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			return RemotingConfigHandler.IsWellKnownClientType(runtimeType);
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x0012C443 File Offset: 0x0012A643
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static WellKnownClientTypeEntry IsWellKnownClientType(string typeName, string assemblyName)
		{
			return RemotingConfigHandler.IsWellKnownClientType(typeName, assemblyName);
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x0012C44C File Offset: 0x0012A64C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static bool IsActivationAllowed(Type svrType)
		{
			RuntimeType runtimeType = svrType as RuntimeType;
			if (svrType != null && runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			return RemotingConfigHandler.IsActivationAllowed(runtimeType);
		}

		// Token: 0x040026C1 RID: 9921
		private static volatile bool s_ListeningForActivationRequests;
	}
}
