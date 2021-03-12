using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System
{
	// Token: 0x02000096 RID: 150
	internal class AppDomainInitializerInfo
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x00019FC8 File Offset: 0x000181C8
		internal AppDomainInitializerInfo(AppDomainInitializer init)
		{
			this.Info = null;
			if (init == null)
			{
				return;
			}
			List<AppDomainInitializerInfo.ItemInfo> list = new List<AppDomainInitializerInfo.ItemInfo>();
			List<AppDomainInitializer> list2 = new List<AppDomainInitializer>();
			list2.Add(init);
			int num = 0;
			while (list2.Count > num)
			{
				AppDomainInitializer appDomainInitializer = list2[num++];
				Delegate[] invocationList = appDomainInitializer.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					if (!invocationList[i].Method.IsStatic)
					{
						if (invocationList[i].Target != null)
						{
							AppDomainInitializer appDomainInitializer2 = invocationList[i].Target as AppDomainInitializer;
							if (appDomainInitializer2 == null)
							{
								throw new ArgumentException(Environment.GetResourceString("Arg_MustBeStatic"), invocationList[i].Method.ReflectedType.FullName + "::" + invocationList[i].Method.Name);
							}
							list2.Add(appDomainInitializer2);
						}
					}
					else
					{
						list.Add(new AppDomainInitializerInfo.ItemInfo
						{
							TargetTypeAssembly = invocationList[i].Method.ReflectedType.Module.Assembly.FullName,
							TargetTypeName = invocationList[i].Method.ReflectedType.FullName,
							MethodName = invocationList[i].Method.Name
						});
					}
				}
			}
			this.Info = list.ToArray();
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001A12C File Offset: 0x0001832C
		[SecuritySafeCritical]
		internal AppDomainInitializer Unwrap()
		{
			if (this.Info == null)
			{
				return null;
			}
			AppDomainInitializer appDomainInitializer = null;
			new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
			for (int i = 0; i < this.Info.Length; i++)
			{
				Assembly assembly = Assembly.Load(this.Info[i].TargetTypeAssembly);
				AppDomainInitializer appDomainInitializer2 = (AppDomainInitializer)Delegate.CreateDelegate(typeof(AppDomainInitializer), assembly.GetType(this.Info[i].TargetTypeName), this.Info[i].MethodName);
				if (appDomainInitializer == null)
				{
					appDomainInitializer = appDomainInitializer2;
				}
				else
				{
					appDomainInitializer = (AppDomainInitializer)Delegate.Combine(appDomainInitializer, appDomainInitializer2);
				}
			}
			return appDomainInitializer;
		}

		// Token: 0x0400037D RID: 893
		internal AppDomainInitializerInfo.ItemInfo[] Info;

		// Token: 0x02000A9D RID: 2717
		internal class ItemInfo
		{
			// Token: 0x04003017 RID: 12311
			public string TargetTypeAssembly;

			// Token: 0x04003018 RID: 12312
			public string TargetTypeName;

			// Token: 0x04003019 RID: 12313
			public string MethodName;
		}
	}
}
