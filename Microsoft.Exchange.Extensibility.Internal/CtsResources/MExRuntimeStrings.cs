using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.CtsResources
{
	// Token: 0x0200006A RID: 106
	internal static class MExRuntimeStrings
	{
		// Token: 0x06000352 RID: 850 RVA: 0x000113DC File Offset: 0x0000F5DC
		static MExRuntimeStrings()
		{
			MExRuntimeStrings.stringIDs.Add(3834762168U, "InvalidConcurrentInvoke");
			MExRuntimeStrings.stringIDs.Add(496466864U, "FailedToReadDataCenterMode");
			MExRuntimeStrings.stringIDs.Add(137784932U, "InvalidAgentFactoryType");
			MExRuntimeStrings.stringIDs.Add(1310430585U, "InvalidConfiguration");
			MExRuntimeStrings.stringIDs.Add(3602164712U, "TooManyInvokes");
			MExRuntimeStrings.stringIDs.Add(716036182U, "InvalidState");
			MExRuntimeStrings.stringIDs.Add(1872988198U, "InvalidEndInvoke");
			MExRuntimeStrings.stringIDs.Add(3581140767U, "InvalidAgentAssemblyPath");
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000353 RID: 851 RVA: 0x000114B7 File Offset: 0x0000F6B7
		public static string InvalidConcurrentInvoke
		{
			get
			{
				return MExRuntimeStrings.ResourceManager.GetString("InvalidConcurrentInvoke");
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000114C8 File Offset: 0x0000F6C8
		public static string DuplicateAgentName(string agentName)
		{
			return string.Format(MExRuntimeStrings.ResourceManager.GetString("DuplicateAgentName"), agentName);
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000114DF File Offset: 0x0000F6DF
		public static string FailedToReadDataCenterMode
		{
			get
			{
				return MExRuntimeStrings.ResourceManager.GetString("FailedToReadDataCenterMode");
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000356 RID: 854 RVA: 0x000114F0 File Offset: 0x0000F6F0
		public static string InvalidAgentFactoryType
		{
			get
			{
				return MExRuntimeStrings.ResourceManager.GetString("InvalidAgentFactoryType");
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00011501 File Offset: 0x0000F701
		public static string AgentFault(string agent, string topic)
		{
			return string.Format(MExRuntimeStrings.ResourceManager.GetString("AgentFault"), agent, topic);
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00011519 File Offset: 0x0000F719
		public static string InvalidConfiguration
		{
			get
			{
				return MExRuntimeStrings.ResourceManager.GetString("InvalidConfiguration");
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0001152A File Offset: 0x0000F72A
		public static string TooManyInvokes
		{
			get
			{
				return MExRuntimeStrings.ResourceManager.GetString("TooManyInvokes");
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001153B File Offset: 0x0000F73B
		public static string InvalidConfigurationFile(string filePath)
		{
			return string.Format(MExRuntimeStrings.ResourceManager.GetString("InvalidConfigurationFile"), filePath);
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00011552 File Offset: 0x0000F752
		public static string InvalidState
		{
			get
			{
				return MExRuntimeStrings.ResourceManager.GetString("InvalidState");
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00011563 File Offset: 0x0000F763
		public static string InvalidTypeInConfiguration(string type, string assembly, string error)
		{
			return string.Format(MExRuntimeStrings.ResourceManager.GetString("InvalidTypeInConfiguration"), type, assembly, error);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001157C File Offset: 0x0000F77C
		public static string MissingConfigurationFile(string filePath)
		{
			return string.Format(MExRuntimeStrings.ResourceManager.GetString("MissingConfigurationFile"), filePath);
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00011593 File Offset: 0x0000F793
		public static string InvalidEndInvoke
		{
			get
			{
				return MExRuntimeStrings.ResourceManager.GetString("InvalidEndInvoke");
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000115A4 File Offset: 0x0000F7A4
		public static string AgentCreationFailure(string agent, string error)
		{
			return string.Format(MExRuntimeStrings.ResourceManager.GetString("AgentCreationFailure"), agent, error);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000115BC File Offset: 0x0000F7BC
		public static string InvalidAgentAssemblyPath
		{
			get
			{
				return MExRuntimeStrings.ResourceManager.GetString("InvalidAgentAssemblyPath");
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000115CD File Offset: 0x0000F7CD
		public static string GetLocalizedString(MExRuntimeStrings.IDs key)
		{
			return MExRuntimeStrings.ResourceManager.GetString(MExRuntimeStrings.stringIDs[(uint)key]);
		}

		// Token: 0x04000421 RID: 1057
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(8);

		// Token: 0x04000422 RID: 1058
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.CtsResources.MExRuntimeStrings", typeof(MExRuntimeStrings).GetTypeInfo().Assembly);

		// Token: 0x0200006B RID: 107
		public enum IDs : uint
		{
			// Token: 0x04000424 RID: 1060
			InvalidConcurrentInvoke = 3834762168U,
			// Token: 0x04000425 RID: 1061
			FailedToReadDataCenterMode = 496466864U,
			// Token: 0x04000426 RID: 1062
			InvalidAgentFactoryType = 137784932U,
			// Token: 0x04000427 RID: 1063
			InvalidConfiguration = 1310430585U,
			// Token: 0x04000428 RID: 1064
			TooManyInvokes = 3602164712U,
			// Token: 0x04000429 RID: 1065
			InvalidState = 716036182U,
			// Token: 0x0400042A RID: 1066
			InvalidEndInvoke = 1872988198U,
			// Token: 0x0400042B RID: 1067
			InvalidAgentAssemblyPath = 3581140767U
		}

		// Token: 0x0200006C RID: 108
		private enum ParamIDs
		{
			// Token: 0x0400042D RID: 1069
			DuplicateAgentName,
			// Token: 0x0400042E RID: 1070
			AgentFault,
			// Token: 0x0400042F RID: 1071
			InvalidConfigurationFile,
			// Token: 0x04000430 RID: 1072
			InvalidTypeInConfiguration,
			// Token: 0x04000431 RID: 1073
			MissingConfigurationFile,
			// Token: 0x04000432 RID: 1074
			AgentCreationFailure
		}
	}
}
