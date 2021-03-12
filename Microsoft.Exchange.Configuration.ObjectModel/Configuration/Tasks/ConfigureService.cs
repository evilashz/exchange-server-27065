using System;
using System.Management.Automation;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Configuration.Common.LocStrings;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000B6 RID: 182
	public abstract class ConfigureService : ManageServiceBase
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0001AE56 File Offset: 0x00019056
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x0001AE4D File Offset: 0x0001904D
		internal ServiceActionType FirstFailureActionType
		{
			get
			{
				return this.firstFailureActionType;
			}
			set
			{
				this.firstFailureActionType = value;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0001AE67 File Offset: 0x00019067
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x0001AE5E File Offset: 0x0001905E
		internal ServiceActionType SecondFailureActionType
		{
			get
			{
				return this.secondFailureActionType;
			}
			set
			{
				this.secondFailureActionType = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0001AE78 File Offset: 0x00019078
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x0001AE6F File Offset: 0x0001906F
		internal ServiceActionType AllOtherFailuresActionType
		{
			get
			{
				return this.allOtherFailuresActionType;
			}
			set
			{
				this.allOtherFailuresActionType = value;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000744 RID: 1860
		protected abstract string Name { get; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001AE89 File Offset: 0x00019089
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x0001AE80 File Offset: 0x00019080
		protected uint FirstFailureActionDelay
		{
			get
			{
				return this.firstFailureActionDelay;
			}
			set
			{
				this.firstFailureActionDelay = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001AE9A File Offset: 0x0001909A
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x0001AE91 File Offset: 0x00019091
		protected uint SecondFailureActionDelay
		{
			get
			{
				return this.secondFailureActionDelay;
			}
			set
			{
				this.secondFailureActionDelay = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001AEAB File Offset: 0x000190AB
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x0001AEA2 File Offset: 0x000190A2
		protected uint AllOtherFailuresActionDelay
		{
			get
			{
				return this.allOtherFailuresActionDelay;
			}
			set
			{
				this.allOtherFailuresActionDelay = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001AEBC File Offset: 0x000190BC
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x0001AEB3 File Offset: 0x000190B3
		protected uint FailureResetPeriod
		{
			get
			{
				return this.failureResetPeriod;
			}
			set
			{
				this.failureResetPeriod = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001AECD File Offset: 0x000190CD
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0001AEC4 File Offset: 0x000190C4
		protected bool FailureActionsFlag
		{
			get
			{
				return this.failureActionsFlag;
			}
			set
			{
				this.failureActionsFlag = value;
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001AFE0 File Offset: 0x000191E0
		protected void ConfigureFailureActions()
		{
			base.DoNativeServiceTask(this.Name, ServiceAccessFlags.AllAccess, delegate(IntPtr service)
			{
				IntPtr intPtr = IntPtr.Zero;
				TaskLogger.Trace("Configuring failure actions...", new object[0]);
				try
				{
					ServiceFailureActions serviceFailureActions = default(ServiceFailureActions);
					serviceFailureActions.resetPeriod = this.FailureResetPeriod;
					serviceFailureActions.rebootMessage = null;
					serviceFailureActions.command = null;
					serviceFailureActions.actionCount = 3U;
					int num = Marshal.SizeOf(typeof(ServiceAction));
					intPtr = Marshal.AllocHGlobal((int)((long)num * (long)((ulong)serviceFailureActions.actionCount)));
					serviceFailureActions.actions = intPtr;
					this.ConfigureFailureAction(intPtr, num, 0, this.firstFailureActionType, this.firstFailureActionDelay);
					this.ConfigureFailureAction(intPtr, num, 1, this.secondFailureActionType, this.secondFailureActionDelay);
					this.ConfigureFailureAction(intPtr, num, 2, this.allOtherFailuresActionType, this.allOtherFailuresActionDelay);
					if (!NativeMethods.ChangeServiceConfig2(service, ServiceConfigInfoLevels.FailureActions, ref serviceFailureActions))
					{
						base.WriteError(TaskWin32Exception.FromErrorCodeAndVerbose(Marshal.GetLastWin32Error(), Strings.ErrorChangeServiceConfig2(this.Name)), ErrorCategory.WriteError, null);
					}
				}
				finally
				{
					if (IntPtr.Zero != intPtr)
					{
						Marshal.FreeHGlobal(intPtr);
					}
				}
			});
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001B046 File Offset: 0x00019246
		protected void ConfigureServiceSidType()
		{
			base.DoNativeServiceTask(this.Name, ServiceAccessFlags.AllAccess, delegate(IntPtr service)
			{
				ServiceSidActions serviceSidActions = default(ServiceSidActions);
				serviceSidActions.serviceSidType = 0U;
				if (!NativeMethods.ChangeServiceConfig2(service, ServiceConfigInfoLevels.ServiceSid, ref serviceSidActions))
				{
					base.WriteError(TaskWin32Exception.FromErrorCodeAndVerbose(Marshal.GetLastWin32Error(), Strings.ErrorChangeServiceConfig2(this.Name)), ErrorCategory.WriteError, null);
				}
			});
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001B0C3 File Offset: 0x000192C3
		protected void ConfigureFailureActionsFlag()
		{
			base.DoNativeServiceTask(this.Name, ServiceAccessFlags.AllAccess, delegate(IntPtr service)
			{
				TaskLogger.Trace("Configuring failure actions flag...", new object[0]);
				ServiceFailureActionsFlag serviceFailureActionsFlag = default(ServiceFailureActionsFlag);
				serviceFailureActionsFlag.fFailureActionsOnNonCrashFailures = this.failureActionsFlag;
				if (!NativeMethods.ChangeServiceConfig2(service, ServiceConfigInfoLevels.FailureActionsFlag, ref serviceFailureActionsFlag))
				{
					base.WriteError(TaskWin32Exception.FromErrorCodeAndVerbose(Marshal.GetLastWin32Error(), Strings.ErrorChangeServiceConfig2(this.Name)), ErrorCategory.WriteError, null);
				}
			});
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001B0E4 File Offset: 0x000192E4
		private void ConfigureFailureAction(IntPtr actions, int serviceActionDataSize, int index, ServiceActionType failureActionType, uint failureActionDelay)
		{
			Marshal.WriteInt32(actions, index * serviceActionDataSize + (int)Marshal.OffsetOf(typeof(ServiceAction), "actionType"), (int)failureActionType);
			Marshal.WriteInt32(actions, index * serviceActionDataSize + (int)Marshal.OffsetOf(typeof(ServiceAction), "delay"), (int)failureActionDelay);
		}

		// Token: 0x0400019F RID: 415
		private const uint failureActionTryCount = 3U;

		// Token: 0x040001A0 RID: 416
		private uint firstFailureActionDelay;

		// Token: 0x040001A1 RID: 417
		private ServiceActionType firstFailureActionType;

		// Token: 0x040001A2 RID: 418
		private uint secondFailureActionDelay;

		// Token: 0x040001A3 RID: 419
		private ServiceActionType secondFailureActionType;

		// Token: 0x040001A4 RID: 420
		private uint allOtherFailuresActionDelay;

		// Token: 0x040001A5 RID: 421
		private ServiceActionType allOtherFailuresActionType;

		// Token: 0x040001A6 RID: 422
		private uint failureResetPeriod;

		// Token: 0x040001A7 RID: 423
		private bool failureActionsFlag;
	}
}
