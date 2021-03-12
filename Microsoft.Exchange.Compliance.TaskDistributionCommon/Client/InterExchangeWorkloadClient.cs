using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Client
{
	// Token: 0x0200000A RID: 10
	public class InterExchangeWorkloadClient : WorkloadClientBase
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002B42 File Offset: 0x00000D42
		public InterExchangeWorkloadClient()
		{
			this.rpsProviderAssembly = new InterExchangeWorkloadClient.RpsProviderAssembly();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002D28 File Offset: 0x00000F28
		protected override Task<IEnumerable<ComplianceMessage>> SendMessageAsyncInternal(IEnumerable<ComplianceMessage> messages)
		{
			return Task.Run<IEnumerable<ComplianceMessage>>(delegate()
			{
				StatusPayload statusPayload = new StatusPayload();
				foreach (ComplianceMessage complianceMessage in messages)
				{
					string tenantName = string.Empty;
					OrganizationId organizationId;
					if (OrganizationId.TryCreateFromBytes(complianceMessage.TenantId, Encoding.UTF8, out organizationId) && organizationId.OrganizationalUnit != null)
					{
						tenantName = organizationId.OrganizationalUnit.Name;
						using (IDisposable disposable = (IDisposable)this.GetRpsProvider(tenantName))
						{
							byte[] value = ComplianceSerializer.Serialize<ComplianceMessage>(ComplianceMessage.Description, complianceMessage);
							PSCommand pscommand = new PSCommand();
							pscommand.AddCommand(new Command("Send-ComplianceMessage", false));
							pscommand.AddParameter("SerializedComplianceMessage", value);
							MethodBase method = this.rpsProviderAssembly.RpsProviderType.GetMethod("Execute", new Type[]
							{
								typeof(PSCommand),
								typeof(TimeSpan)
							});
							object obj = disposable;
							object[] array = new object[2];
							array[0] = pscommand;
							IEnumerable<PSObject> enumerable = (IEnumerable<PSObject>)method.Invoke(obj, array);
							if (enumerable != null && enumerable.Count<PSObject>() == 1 && (bool)enumerable.ToArray<PSObject>()[0].BaseObject)
							{
								statusPayload.QueuedMessages.Add(complianceMessage.MessageId);
							}
						}
					}
				}
				return (IEnumerable<ComplianceMessage>)new ComplianceMessage[]
				{
					new ComplianceMessage
					{
						Payload = ComplianceSerializer.Serialize<StatusPayload>(StatusPayload.Description, statusPayload)
					}
				};
			});
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002D5C File Offset: 0x00000F5C
		private object GetRpsProvider(string tenantName)
		{
			object obj = this.rpsProviderAssembly.RpsProverFactoryType.GetConstructor(Type.EmptyTypes).Invoke(null);
			return this.rpsProviderAssembly.RpsProverFactoryType.GetMethod("CreateProviderForTenant", new Type[]
			{
				typeof(string),
				typeof(ADObjectId),
				typeof(string)
			}).Invoke(obj, new object[]
			{
				"ComplianceTaskDistributor",
				new ADObjectId("DN=" + tenantName),
				"exo"
			});
		}

		// Token: 0x0400000E RID: 14
		private const string RPSDataProviderCallerId = "ComplianceTaskDistributor";

		// Token: 0x0400000F RID: 15
		private InterExchangeWorkloadClient.RpsProviderAssembly rpsProviderAssembly;

		// Token: 0x0200000B RID: 11
		private class RpsProviderAssembly
		{
			// Token: 0x06000013 RID: 19 RVA: 0x00002DFC File Offset: 0x00000FFC
			public RpsProviderAssembly()
			{
				Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.PowershellDataProvider");
				this.RpsProverFactoryType = assembly.GetType("Microsoft.Exchange.Hygiene.Migration.PowershellDataProviderFactory");
				this.RpsProviderType = assembly.GetType("Microsoft.Exchange.Data.IRemotePowershellDataProvider");
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000014 RID: 20 RVA: 0x00002E3C File Offset: 0x0000103C
			// (set) Token: 0x06000015 RID: 21 RVA: 0x00002E44 File Offset: 0x00001044
			public Type RpsProverFactoryType { get; private set; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000016 RID: 22 RVA: 0x00002E4D File Offset: 0x0000104D
			// (set) Token: 0x06000017 RID: 23 RVA: 0x00002E55 File Offset: 0x00001055
			public Type RpsProviderType { get; private set; }

			// Token: 0x04000010 RID: 16
			private const string RemotePowershellProviderAssembly = "Microsoft.Exchange.Hygiene.PowershellDataProvider";
		}
	}
}
