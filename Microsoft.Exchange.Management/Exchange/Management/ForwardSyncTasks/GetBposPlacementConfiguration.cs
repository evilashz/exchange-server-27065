using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000348 RID: 840
	[OutputType(new Type[]
	{
		typeof(BposPlacementConfiguration)
	})]
	[Cmdlet("Get", "BposPlacementConfiguration")]
	public sealed class GetBposPlacementConfiguration : Task
	{
		// Token: 0x06001D03 RID: 7427 RVA: 0x00080440 File Offset: 0x0007E640
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			try
			{
				using (OnboardingService onboardingService = new MsoOnboardingService())
				{
					ServiceInstanceMapValue map = null;
					try
					{
						map = onboardingService.GetServiceInstanceMap();
					}
					catch (Exception exception)
					{
						this.WriteError(exception, ErrorCategory.ResourceUnavailable, null, true);
					}
					string configuration = ServiceInstanceMapSerializer.ConvertServiceInstanceMapToXml(map);
					base.WriteObject(new BposPlacementConfiguration(configuration));
				}
			}
			catch (CouldNotCreateMsoOnboardingServiceException exception2)
			{
				this.WriteError(exception2, ErrorCategory.ObjectNotFound, null, true);
			}
			catch (InvalidServiceInstanceMapXmlFormatException exception3)
			{
				this.WriteError(exception3, ErrorCategory.InvalidData, null, true);
			}
			catch (Exception exception4)
			{
				this.WriteError(exception4, ErrorCategory.InvalidOperation, null, true);
			}
		}
	}
}
