using System;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Common.Extensions;
using Microsoft.Exchange.Diagnostics.Components.EseRepl;
using Microsoft.Exchange.EseRepl.Common;
using Microsoft.Practices.Unity;

namespace Microsoft.Exchange.EseRepl.Configuration
{
	// Token: 0x02000003 RID: 3
	internal class EseReplDependencies
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002108 File Offset: 0x00000308
		public static IUnityContainer Initialize()
		{
			Assert instance = Assert.Instance;
			Assert.Initialize(instance);
			return new UnityContainer().RegisterInstance<IAssert>(instance).RegisterInstance<IRegistryReader>(RegistryReader.Instance).RegisterInstance<IRegistryWriter>(RegistryWriter.Instance).RegisterInstance<ISerialization>(Serialization.Instance).RegisterInstance<ITracer>(0.ToString(), new Tracer(ExTraceGlobals.DagNetChooserTracer)).RegisterInstance<ITracer>(1.ToString(), new Tracer(ExTraceGlobals.DagNetEnvironmentTracer)).RegisterInstance<IEseReplConfig>(EseReplConfig.Instance);
		}
	}
}
