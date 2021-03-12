using System;
using System.Reflection;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000141 RID: 321
	internal class FsmVariable<T>
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x00026CA5 File Offset: 0x00024EA5
		private FsmVariable(FsmVariable<T>.VariableDelegate d, string variableName)
		{
			this.variableDelegate = d;
			this.variableName = variableName;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00026CBC File Offset: 0x00024EBC
		internal static FsmVariable<T> Create(QualifiedName variableName, ActivityManagerConfig variableScope)
		{
			FsmVariable<T> result = null;
			if (!FsmVariable<T>.TryCreate(variableName, variableScope, out result))
			{
				throw new FsmConfigurationException(Strings.InvalidVariable(variableName.FullName));
			}
			return result;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00026CF0 File Offset: 0x00024EF0
		internal static bool TryCreate(QualifiedName variableName, ActivityManagerConfig variableScope, out FsmVariable<T> fsmVariable)
		{
			fsmVariable = null;
			FsmVariable<T>.VariableDelegate variableDelegate = FsmVariable<T>.FindVariableDelegate(variableName, variableScope);
			if (variableDelegate != null)
			{
				fsmVariable = new FsmVariable<T>(variableDelegate, variableName.ShortName);
				return true;
			}
			return false;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00026D1C File Offset: 0x00024F1C
		internal T GetValue(ActivityManager m)
		{
			return this.variableDelegate(m, this.variableName);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00026D30 File Offset: 0x00024F30
		private static FsmVariable<T>.VariableDelegate FindVariableDelegate(QualifiedName variableName, ActivityManagerConfig variableScope)
		{
			FsmVariable<T>.VariableDelegate variableDelegate = null;
			object obj = null;
			string key = variableName + ":" + typeof(T).ToString();
			if (FsmVariableCache.Instance.TryGetValue(key, out obj))
			{
				variableDelegate = (obj as FsmVariable<T>.VariableDelegate);
			}
			else
			{
				while (variableScope != null && string.Compare(variableScope.ClassName, variableName.Namespace, StringComparison.OrdinalIgnoreCase) != 0)
				{
					variableScope = variableScope.ManagerConfig;
				}
				Type fsmProxyType = FsmVariable<T>.GetFsmProxyType(variableScope);
				MethodInfo method = fsmProxyType.GetMethod(variableName.ShortName, BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.NonPublic, null, FsmVariable<T>.parameterArray, null);
				if (null != method)
				{
					variableDelegate = (FsmVariable<T>.VariableDelegate)Delegate.CreateDelegate(typeof(FsmVariable<T>.VariableDelegate), method, false);
				}
			}
			if (variableDelegate != null)
			{
				FsmVariableCache.Instance[key] = variableDelegate;
			}
			return variableDelegate;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00026DE4 File Offset: 0x00024FE4
		private static Type GetFsmProxyType(ActivityManagerConfig variableScope)
		{
			Type result;
			if (variableScope == null)
			{
				result = GlobalActivityManager.ConfigClass.CoreAssembly.GetType("Microsoft.Exchange.UM.Fsm." + typeof(GlobalActivityManager).Name);
			}
			else
			{
				result = variableScope.FsmProxyType;
			}
			return result;
		}

		// Token: 0x040008C2 RID: 2242
		private static readonly Type[] parameterArray = new Type[]
		{
			typeof(ActivityManager),
			typeof(string)
		};

		// Token: 0x040008C3 RID: 2243
		private FsmVariable<T>.VariableDelegate variableDelegate;

		// Token: 0x040008C4 RID: 2244
		private string variableName;

		// Token: 0x02000142 RID: 322
		// (Invoke) Token: 0x060008FA RID: 2298
		internal delegate T VariableDelegate(ActivityManager m, string variableName);
	}
}
