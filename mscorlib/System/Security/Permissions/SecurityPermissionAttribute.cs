using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002CB RID: 715
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002593 RID: 9619 RVA: 0x00087FA5 File Offset: 0x000861A5
		public SecurityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06002594 RID: 9620 RVA: 0x00087FAE File Offset: 0x000861AE
		// (set) Token: 0x06002595 RID: 9621 RVA: 0x00087FB6 File Offset: 0x000861B6
		public SecurityPermissionFlag Flags
		{
			get
			{
				return this.m_flag;
			}
			set
			{
				this.m_flag = value;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x00087FBF File Offset: 0x000861BF
		// (set) Token: 0x06002597 RID: 9623 RVA: 0x00087FCC File Offset: 0x000861CC
		public bool Assertion
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.Assertion) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.Assertion) : (this.m_flag & ~SecurityPermissionFlag.Assertion));
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x00087FEA File Offset: 0x000861EA
		// (set) Token: 0x06002599 RID: 9625 RVA: 0x00087FF7 File Offset: 0x000861F7
		public bool UnmanagedCode
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.UnmanagedCode) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.UnmanagedCode) : (this.m_flag & ~SecurityPermissionFlag.UnmanagedCode));
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600259A RID: 9626 RVA: 0x00088015 File Offset: 0x00086215
		// (set) Token: 0x0600259B RID: 9627 RVA: 0x00088022 File Offset: 0x00086222
		public bool SkipVerification
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.SkipVerification) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.SkipVerification) : (this.m_flag & ~SecurityPermissionFlag.SkipVerification));
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600259C RID: 9628 RVA: 0x00088040 File Offset: 0x00086240
		// (set) Token: 0x0600259D RID: 9629 RVA: 0x0008804D File Offset: 0x0008624D
		public bool Execution
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.Execution) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.Execution) : (this.m_flag & ~SecurityPermissionFlag.Execution));
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600259E RID: 9630 RVA: 0x0008806B File Offset: 0x0008626B
		// (set) Token: 0x0600259F RID: 9631 RVA: 0x00088079 File Offset: 0x00086279
		public bool ControlThread
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlThread) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlThread) : (this.m_flag & ~SecurityPermissionFlag.ControlThread));
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060025A0 RID: 9632 RVA: 0x00088098 File Offset: 0x00086298
		// (set) Token: 0x060025A1 RID: 9633 RVA: 0x000880A6 File Offset: 0x000862A6
		public bool ControlEvidence
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlEvidence) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlEvidence) : (this.m_flag & ~SecurityPermissionFlag.ControlEvidence));
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000880C5 File Offset: 0x000862C5
		// (set) Token: 0x060025A3 RID: 9635 RVA: 0x000880D3 File Offset: 0x000862D3
		public bool ControlPolicy
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlPolicy) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlPolicy) : (this.m_flag & ~SecurityPermissionFlag.ControlPolicy));
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x000880F2 File Offset: 0x000862F2
		// (set) Token: 0x060025A5 RID: 9637 RVA: 0x00088103 File Offset: 0x00086303
		public bool SerializationFormatter
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.SerializationFormatter) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.SerializationFormatter) : (this.m_flag & ~SecurityPermissionFlag.SerializationFormatter));
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x00088128 File Offset: 0x00086328
		// (set) Token: 0x060025A7 RID: 9639 RVA: 0x00088139 File Offset: 0x00086339
		public bool ControlDomainPolicy
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlDomainPolicy) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlDomainPolicy) : (this.m_flag & ~SecurityPermissionFlag.ControlDomainPolicy));
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x0008815E File Offset: 0x0008635E
		// (set) Token: 0x060025A9 RID: 9641 RVA: 0x0008816F File Offset: 0x0008636F
		public bool ControlPrincipal
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlPrincipal) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlPrincipal) : (this.m_flag & ~SecurityPermissionFlag.ControlPrincipal));
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x00088194 File Offset: 0x00086394
		// (set) Token: 0x060025AB RID: 9643 RVA: 0x000881A5 File Offset: 0x000863A5
		public bool ControlAppDomain
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlAppDomain) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlAppDomain) : (this.m_flag & ~SecurityPermissionFlag.ControlAppDomain));
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x000881CA File Offset: 0x000863CA
		// (set) Token: 0x060025AD RID: 9645 RVA: 0x000881DB File Offset: 0x000863DB
		public bool RemotingConfiguration
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.RemotingConfiguration) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.RemotingConfiguration) : (this.m_flag & ~SecurityPermissionFlag.RemotingConfiguration));
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060025AE RID: 9646 RVA: 0x00088200 File Offset: 0x00086400
		// (set) Token: 0x060025AF RID: 9647 RVA: 0x00088211 File Offset: 0x00086411
		[ComVisible(true)]
		public bool Infrastructure
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.Infrastructure) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.Infrastructure) : (this.m_flag & ~SecurityPermissionFlag.Infrastructure));
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x00088236 File Offset: 0x00086436
		// (set) Token: 0x060025B1 RID: 9649 RVA: 0x00088247 File Offset: 0x00086447
		public bool BindingRedirects
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.BindingRedirects) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.BindingRedirects) : (this.m_flag & ~SecurityPermissionFlag.BindingRedirects));
			}
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x0008826C File Offset: 0x0008646C
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			return new SecurityPermission(this.m_flag);
		}

		// Token: 0x04000E47 RID: 3655
		private SecurityPermissionFlag m_flag;
	}
}
