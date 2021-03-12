using System;
using System.Diagnostics;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.EseRepl.Common;
using Microsoft.Exchange.EseRepl.Configuration;
using Microsoft.Practices.Unity;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200000C RID: 12
	internal static class Dependencies
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002E15 File Offset: 0x00001015
		public static IUnityContainer Container
		{
			get
			{
				return Dependencies.container;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002E1C File Offset: 0x0000101C
		public static IRegistryReader RegistryReader
		{
			get
			{
				return Dependencies.container.Resolve<IRegistryReader>();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002E28 File Offset: 0x00001028
		public static IRegistryWriter RegistryWriter
		{
			get
			{
				return Dependencies.container.Resolve<IRegistryWriter>();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002E34 File Offset: 0x00001034
		public static ITracer DagNetChooserTracer
		{
			get
			{
				return Dependencies.container.Resolve<ITracer>(0.ToString());
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002E4B File Offset: 0x0000104B
		public static ITracer DagNetEnvironmentTracer
		{
			get
			{
				return Dependencies.container.Resolve<ITracer>(1.ToString());
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002E62 File Offset: 0x00001062
		public static IEseReplConfig Config
		{
			get
			{
				return Dependencies.container.Resolve<IEseReplConfig>();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002E6E File Offset: 0x0000106E
		public static ISerialization Serializer
		{
			get
			{
				return Dependencies.container.Resolve<ISerialization>();
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002E7A File Offset: 0x0000107A
		public static ITcpConnector TcpConnector
		{
			get
			{
				return Dependencies.tcpConnector;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E81 File Offset: 0x00001081
		internal static void OverrideTcpConnector(ITcpConnector conn)
		{
			Dependencies.tcpConnector = conn;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002E89 File Offset: 0x00001089
		private static IAssert Assert
		{
			get
			{
				return Dependencies.container.Resolve<IAssert>();
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E95 File Offset: 0x00001095
		[Conditional("DEBUG")]
		public static void AssertDbg(bool condition, string formatString, params object[] parameters)
		{
			Dependencies.Assert.Debug(condition, formatString, parameters);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002EA4 File Offset: 0x000010A4
		[Conditional("DEBUG")]
		public static void AssertDbg(bool condition)
		{
			Dependencies.Assert.Debug(condition, "No info for this assert", new object[0]);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002EBC File Offset: 0x000010BC
		public static void AssertRtl(bool condition, string formatString, params object[] parameters)
		{
			Dependencies.Assert.Retail(condition, formatString, parameters);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002ECB File Offset: 0x000010CB
		public static void RegisterAll()
		{
			Dependencies.container.Dispose();
			Dependencies.container = EseReplDependencies.Initialize();
			Dependencies.tcpConnector = new TcpConnector();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002EEB File Offset: 0x000010EB
		public static void UnregisterAll()
		{
			Dependencies.container.Dispose();
			Dependencies.container = new UnityContainer();
		}

		// Token: 0x04000031 RID: 49
		private static IUnityContainer container = EseReplDependencies.Initialize();

		// Token: 0x04000032 RID: 50
		private static ITcpConnector tcpConnector = new TcpConnector();
	}
}
