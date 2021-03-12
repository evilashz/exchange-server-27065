using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.RpcClientAccess.Monitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.RpcClientAccess
{
	// Token: 0x020001F8 RID: 504
	internal abstract class LocalProbe : MapiProbe
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x0005F578 File Offset: 0x0005D778
		public override void PopulateDefinition<Definition>(Definition definition, Dictionary<string, string> propertyBag)
		{
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			if (probeDefinition == null)
			{
				throw new LocalizedException(Strings.WrongDefinitionType);
			}
			AutoPopulateDefinition autoPopulateDefinition = new AutoPopulateDefinition(this.GetProbeType(), probeDefinition);
			autoPopulateDefinition.ValidateAndAutoFill(propertyBag);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0005F5B4 File Offset: 0x0005D7B4
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			bool flag = this.GetProbeType() == ProbeType.Ctp;
			List<PropertyInformation> list = new List<PropertyInformation>
			{
				new PropertyInformation("Identity", Strings.Identity, true),
				new PropertyInformation("Account", Strings.MonitoringAccount, false),
				new PropertyInformation("AccountDisplayName", Strings.AccountDisplayName, false),
				new PropertyInformation("Endpoint", Strings.Endpoint, false),
				new PropertyInformation("SecondaryEndpoint", Strings.SecondaryEndpoint, false),
				new PropertyInformation("ItemTargetExtension", Strings.ExtensionAttributes, false)
			};
			if (flag)
			{
				list.Add(new PropertyInformation("Password", Strings.MonitoringAccountPassword, false));
			}
			return list;
		}

		// Token: 0x06000E30 RID: 3632
		protected abstract ProbeType GetProbeType();

		// Token: 0x020001F9 RID: 505
		public class DeepTest : LocalProbe
		{
			// Token: 0x06000E32 RID: 3634 RVA: 0x0005F69C File Offset: 0x0005D89C
			protected override bool ShouldCreateRestrictedCredentials()
			{
				return true;
			}

			// Token: 0x06000E33 RID: 3635 RVA: 0x0005F69F File Offset: 0x0005D89F
			protected override ITask CreateTask()
			{
				return new EmsmdbTask(base.Context);
			}

			// Token: 0x06000E34 RID: 3636 RVA: 0x0005F6AC File Offset: 0x0005D8AC
			protected sealed override ProbeType GetProbeType()
			{
				return ProbeType.DeepTest;
			}

			// Token: 0x06000E35 RID: 3637 RVA: 0x0005F6AF File Offset: 0x0005D8AF
			protected override void ProcessTaskException(Exception ex)
			{
				if (MapiProbe.DidProbeFailDueToPassiveMDB(ex))
				{
					base.SetRootCause("Passive");
					return;
				}
				base.ProcessTaskException(ex);
			}
		}

		// Token: 0x020001FA RID: 506
		public class MapiHttpDeepTest : LocalProbe.DeepTest
		{
			// Token: 0x06000E37 RID: 3639 RVA: 0x0005F6D4 File Offset: 0x0005D8D4
			protected override ITask CreateTask()
			{
				return new EmsmdbMapiHttpTask(base.Context);
			}
		}

		// Token: 0x020001FB RID: 507
		public class SelfTest : LocalProbe
		{
			// Token: 0x06000E39 RID: 3641 RVA: 0x0005F6E9 File Offset: 0x0005D8E9
			protected override bool ShouldCreateRestrictedCredentials()
			{
				return true;
			}

			// Token: 0x06000E3A RID: 3642 RVA: 0x0005F6EC File Offset: 0x0005D8EC
			protected override ITask CreateTask()
			{
				return new CompositeTask(base.Context, new ITask[]
				{
					new VerifyRpcProxyTask(base.Context),
					new DummyRpcTask(base.Context)
				});
			}

			// Token: 0x06000E3B RID: 3643 RVA: 0x0005F728 File Offset: 0x0005D928
			protected sealed override ProbeType GetProbeType()
			{
				return ProbeType.SelfTest;
			}
		}

		// Token: 0x020001FC RID: 508
		public class MapiHttpSelfTest : LocalProbe.SelfTest
		{
			// Token: 0x06000E3D RID: 3645 RVA: 0x0005F734 File Offset: 0x0005D934
			protected override ITask CreateTask()
			{
				return new CompositeTask(base.Context, new ITask[]
				{
					new DummyMapiHttpTask(base.Context)
				});
			}
		}

		// Token: 0x020001FD RID: 509
		public class Ctp : LocalProbe
		{
			// Token: 0x06000E3F RID: 3647 RVA: 0x0005F76C File Offset: 0x0005D96C
			protected override ITask CreateTask()
			{
				return new CompositeTask(base.Context, new ITask[]
				{
					new VerifyRpcProxyTask(base.Context),
					new EmsmdbTask(base.Context)
				});
			}

			// Token: 0x06000E40 RID: 3648 RVA: 0x0005F7A8 File Offset: 0x0005D9A8
			protected sealed override ProbeType GetProbeType()
			{
				return ProbeType.Ctp;
			}

			// Token: 0x06000E41 RID: 3649 RVA: 0x0005F7AC File Offset: 0x0005D9AC
			protected override void ProcessTaskException(Exception ex)
			{
				if (base.DidProbeFailDueToDatabaseMountedElsewhere(ex))
				{
					base.SetRootCause("Passive");
					throw new AggregateException(new Exception[]
					{
						ex
					});
				}
				base.ProcessTaskException(ex);
			}
		}

		// Token: 0x020001FE RID: 510
		public class MapiHttpCtp : LocalProbe.Ctp
		{
			// Token: 0x06000E43 RID: 3651 RVA: 0x0005F7F0 File Offset: 0x0005D9F0
			protected override ITask CreateTask()
			{
				return new CompositeTask(base.Context, new ITask[]
				{
					new EmsmdbMapiHttpTask(base.Context)
				});
			}

			// Token: 0x06000E44 RID: 3652 RVA: 0x0005F81E File Offset: 0x0005DA1E
			protected override void ProcessTaskException(Exception ex)
			{
				if (base.DidProbeFailDueToInvalidRequestType(ex))
				{
					base.SetRootCause("MapiHttpVersionMismatch");
					return;
				}
				base.ProcessTaskException(ex);
			}
		}
	}
}
