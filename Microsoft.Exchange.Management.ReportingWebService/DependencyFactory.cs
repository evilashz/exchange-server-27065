using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Xml;
using Microsoft.Exchange.Management.ReportingWebService.PowerShell;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x0200001C RID: 28
	internal class DependencyFactory
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003380 File Offset: 0x00001580
		public static void RegisterTestCreator<T>(T delegator)
		{
			Type typeFromHandle = typeof(T);
			if (DependencyFactory.creators.ContainsKey(typeFromHandle))
			{
				DependencyFactory.creators[typeFromHandle] = delegator;
				return;
			}
			DependencyFactory.creators.Add(typeFromHandle, delegator);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000033C8 File Offset: 0x000015C8
		public static void UnRegisterTestCreator<T>()
		{
			Type typeFromHandle = typeof(T);
			if (DependencyFactory.creators.ContainsKey(typeFromHandle))
			{
				DependencyFactory.creators[typeFromHandle] = null;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000033FC File Offset: 0x000015FC
		public static IEntity CreateEntity(string name, TaskInvocationInfo taskInvocationInfo, Dictionary<string, List<string>> reportPropertyCmdletParamsMap, IReportAnnotation annotation)
		{
			CreateEntityDelegate @delegate = DependencyFactory.GetDelegate<CreateEntityDelegate>();
			if (@delegate != null)
			{
				return @delegate(name, taskInvocationInfo, reportPropertyCmdletParamsMap, annotation);
			}
			return new Entity(name, taskInvocationInfo, reportPropertyCmdletParamsMap, annotation);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003428 File Offset: 0x00001628
		public static IReportingDataSource CreateReportingDataSource(IPrincipal principal)
		{
			CreateReportingDataSourceDelegate @delegate = DependencyFactory.GetDelegate<CreateReportingDataSourceDelegate>();
			if (@delegate != null)
			{
				return @delegate();
			}
			return new ReportingDataSource(principal);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000344C File Offset: 0x0000164C
		public static IReportAnnotation CreateReportAnnotation(XmlNode annotationNode)
		{
			CreateReportAnnotationDelegate @delegate = DependencyFactory.GetDelegate<CreateReportAnnotationDelegate>();
			if (@delegate != null)
			{
				return @delegate(annotationNode);
			}
			return ReportAnnotation.Load(annotationNode);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003470 File Offset: 0x00001670
		public static IPSCommandWrapper CreatePSCommandWrapper()
		{
			CreatePSCommandWrapperDelegate @delegate = DependencyFactory.GetDelegate<CreatePSCommandWrapperDelegate>();
			if (@delegate != null)
			{
				return @delegate();
			}
			return new PSCommandWrapper();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003494 File Offset: 0x00001694
		public static IPSCommandResolver CreatePSCommandResolver(IEnumerable<string> snapIns)
		{
			CreatePSCommandResolverDelegate @delegate = DependencyFactory.GetDelegate<CreatePSCommandResolverDelegate>();
			if (@delegate != null)
			{
				return @delegate(snapIns);
			}
			return new ExchangeCommandResolver(snapIns);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000034B8 File Offset: 0x000016B8
		private static TDelegate GetDelegate<TDelegate>() where TDelegate : class
		{
			Type typeFromHandle = typeof(TDelegate);
			if (DependencyFactory.creators.ContainsKey(typeFromHandle) && DependencyFactory.creators[typeFromHandle] != null)
			{
				return (TDelegate)((object)DependencyFactory.creators[typeFromHandle]);
			}
			return default(TDelegate);
		}

		// Token: 0x0400003D RID: 61
		private static readonly Dictionary<Type, object> creators = new Dictionary<Type, object>();

		// Token: 0x0400003E RID: 62
		private static readonly Type createEntityDelegateType = typeof(CreateEntityDelegate);

		// Token: 0x0400003F RID: 63
		private static readonly Type createReportingDataSourceDelegateType = typeof(CreateReportingDataSourceDelegate);

		// Token: 0x04000040 RID: 64
		private static readonly Type createReportAnnotationDelegateType = typeof(CreateReportAnnotationDelegate);

		// Token: 0x04000041 RID: 65
		private static readonly Type createPSCommandWrapperDelegateType = typeof(CreatePSCommandWrapperDelegate);

		// Token: 0x04000042 RID: 66
		private static readonly Type createPSCommandResolverDelegateType = typeof(CreatePSCommandResolverDelegate);
	}
}
