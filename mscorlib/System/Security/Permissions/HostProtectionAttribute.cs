using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002B8 RID: 696
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class HostProtectionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060024F9 RID: 9465 RVA: 0x00086D97 File Offset: 0x00084F97
		public HostProtectionAttribute() : base(SecurityAction.LinkDemand)
		{
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x00086DA0 File Offset: 0x00084FA0
		public HostProtectionAttribute(SecurityAction action) : base(action)
		{
			if (action != SecurityAction.LinkDemand)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"));
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x00086DBD File Offset: 0x00084FBD
		// (set) Token: 0x060024FC RID: 9468 RVA: 0x00086DC5 File Offset: 0x00084FC5
		public HostProtectionResource Resources
		{
			get
			{
				return this.m_resources;
			}
			set
			{
				this.m_resources = value;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060024FD RID: 9469 RVA: 0x00086DCE File Offset: 0x00084FCE
		// (set) Token: 0x060024FE RID: 9470 RVA: 0x00086DDB File Offset: 0x00084FDB
		public bool Synchronization
		{
			get
			{
				return (this.m_resources & HostProtectionResource.Synchronization) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.Synchronization) : (this.m_resources & ~HostProtectionResource.Synchronization));
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x00086DF9 File Offset: 0x00084FF9
		// (set) Token: 0x06002500 RID: 9472 RVA: 0x00086E06 File Offset: 0x00085006
		public bool SharedState
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SharedState) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SharedState) : (this.m_resources & ~HostProtectionResource.SharedState));
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06002501 RID: 9473 RVA: 0x00086E24 File Offset: 0x00085024
		// (set) Token: 0x06002502 RID: 9474 RVA: 0x00086E31 File Offset: 0x00085031
		public bool ExternalProcessMgmt
		{
			get
			{
				return (this.m_resources & HostProtectionResource.ExternalProcessMgmt) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.ExternalProcessMgmt) : (this.m_resources & ~HostProtectionResource.ExternalProcessMgmt));
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x00086E4F File Offset: 0x0008504F
		// (set) Token: 0x06002504 RID: 9476 RVA: 0x00086E5C File Offset: 0x0008505C
		public bool SelfAffectingProcessMgmt
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SelfAffectingProcessMgmt) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SelfAffectingProcessMgmt) : (this.m_resources & ~HostProtectionResource.SelfAffectingProcessMgmt));
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x00086E7A File Offset: 0x0008507A
		// (set) Token: 0x06002506 RID: 9478 RVA: 0x00086E88 File Offset: 0x00085088
		public bool ExternalThreading
		{
			get
			{
				return (this.m_resources & HostProtectionResource.ExternalThreading) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.ExternalThreading) : (this.m_resources & ~HostProtectionResource.ExternalThreading));
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x00086EA7 File Offset: 0x000850A7
		// (set) Token: 0x06002508 RID: 9480 RVA: 0x00086EB5 File Offset: 0x000850B5
		public bool SelfAffectingThreading
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SelfAffectingThreading) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SelfAffectingThreading) : (this.m_resources & ~HostProtectionResource.SelfAffectingThreading));
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x00086ED4 File Offset: 0x000850D4
		// (set) Token: 0x0600250A RID: 9482 RVA: 0x00086EE2 File Offset: 0x000850E2
		[ComVisible(true)]
		public bool SecurityInfrastructure
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SecurityInfrastructure) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SecurityInfrastructure) : (this.m_resources & ~HostProtectionResource.SecurityInfrastructure));
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x00086F01 File Offset: 0x00085101
		// (set) Token: 0x0600250C RID: 9484 RVA: 0x00086F12 File Offset: 0x00085112
		public bool UI
		{
			get
			{
				return (this.m_resources & HostProtectionResource.UI) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.UI) : (this.m_resources & ~HostProtectionResource.UI));
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600250D RID: 9485 RVA: 0x00086F37 File Offset: 0x00085137
		// (set) Token: 0x0600250E RID: 9486 RVA: 0x00086F48 File Offset: 0x00085148
		public bool MayLeakOnAbort
		{
			get
			{
				return (this.m_resources & HostProtectionResource.MayLeakOnAbort) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.MayLeakOnAbort) : (this.m_resources & ~HostProtectionResource.MayLeakOnAbort));
			}
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x00086F6D File Offset: 0x0008516D
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new HostProtectionPermission(PermissionState.Unrestricted);
			}
			return new HostProtectionPermission(this.m_resources);
		}

		// Token: 0x04000DE2 RID: 3554
		private HostProtectionResource m_resources;
	}
}
