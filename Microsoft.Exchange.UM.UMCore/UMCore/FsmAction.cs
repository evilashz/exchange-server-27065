using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200013F RID: 319
	internal class FsmAction
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x00026BBF File Offset: 0x00024DBF
		private FsmAction(FsmAction.ActionDelegate d, string actionName)
		{
			this.actionDelegate = d;
			this.actionName = actionName;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00026BD5 File Offset: 0x00024DD5
		internal static FsmAction Create(QualifiedName actionName, ActivityManagerConfig actionScope)
		{
			return new FsmAction(FsmAction.FindActionDelegate(actionName, actionScope), actionName.ShortName);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00026BE9 File Offset: 0x00024DE9
		public override string ToString()
		{
			return this.actionName;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00026BF4 File Offset: 0x00024DF4
		internal TransitionBase Execute(ActivityManager m, BaseUMCallSession vo)
		{
			TransitionBase result;
			try
			{
				m.PreActionExecute(vo);
				result = this.actionDelegate(m, this.actionName, vo);
			}
			finally
			{
				m.PostActionExecute(vo);
			}
			return result;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00026C38 File Offset: 0x00024E38
		private static FsmAction.ActionDelegate FindActionDelegate(QualifiedName actionName, ActivityManagerConfig actionScope)
		{
			while (actionScope != null && string.Compare(actionScope.ClassName, actionName.Namespace, StringComparison.OrdinalIgnoreCase) != 0)
			{
				actionScope = actionScope.ManagerConfig;
			}
			FsmAction.ActionDelegate actionDelegate = (FsmAction.ActionDelegate)Delegate.CreateDelegate(typeof(FsmAction.ActionDelegate), actionScope.FsmProxyType, actionName.ShortName, true, false);
			if (actionDelegate == null)
			{
				throw new FsmConfigurationException(Strings.InvalidAction(actionName.FullName));
			}
			return actionDelegate;
		}

		// Token: 0x040008C0 RID: 2240
		private FsmAction.ActionDelegate actionDelegate;

		// Token: 0x040008C1 RID: 2241
		private string actionName;

		// Token: 0x02000140 RID: 320
		// (Invoke) Token: 0x060008EF RID: 2287
		internal delegate TransitionBase ActionDelegate(ActivityManager manager, string variableName, BaseUMCallSession vo);
	}
}
