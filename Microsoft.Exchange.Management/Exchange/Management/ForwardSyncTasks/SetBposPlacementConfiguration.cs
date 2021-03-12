using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000377 RID: 887
	[Cmdlet("Set", "BposPlacementConfiguration", SupportsShouldProcess = true)]
	public sealed class SetBposPlacementConfiguration : Task
	{
		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x0008636C File Offset: 0x0008456C
		// (set) Token: 0x06001F16 RID: 7958 RVA: 0x00086383 File Offset: 0x00084583
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public string Configuration
		{
			get
			{
				return (string)base.Fields["Configuration"];
			}
			set
			{
				base.Fields["Configuration"] = value;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x00086396 File Offset: 0x00084596
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetServiceInstanceMap;
			}
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x000863A0 File Offset: 0x000845A0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				try
				{
					this.map = ServiceInstanceMapSerializer.ConvertXmlToServiceInstanceMap(this.Configuration);
				}
				catch (InvalidServiceInstanceMapXmlFormatException exception)
				{
					this.WriteError(exception, ErrorCategory.InvalidData, null, true);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x000863F8 File Offset: 0x000845F8
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			using (OnboardingService onboardingService = new MsoOnboardingService())
			{
				ResultCode? result = null;
				try
				{
					result = new ResultCode?(onboardingService.SetServiceInstanceMap(this.map));
				}
				catch (Exception exception)
				{
					this.WriteError(exception, ErrorCategory.ResourceUnavailable, null, true);
				}
				if (result != null && result.Value == ResultCode.Success)
				{
					this.Configuration = ServiceInstanceMapSerializer.ConvertServiceInstanceMapToXml(this.map);
					base.WriteObject(new BposPlacementConfiguration(this.Configuration));
				}
				else
				{
					string errorStringForResultcode = MsoOnboardingService.GetErrorStringForResultcode(result);
					this.WriteError(new CouldNotUpdateServiceInstanceMapException(errorStringForResultcode), ErrorCategory.InvalidData, null, true);
				}
			}
		}

		// Token: 0x04001955 RID: 6485
		private ServiceInstanceMapValue map;
	}
}
