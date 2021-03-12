using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000029 RID: 41
	internal abstract class RUSProvisioningHandler : ProvisioningHandlerBase
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00006B81 File Offset: 0x00004D81
		public RUSProvisioningHandler()
		{
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00006B8C File Offset: 0x00004D8C
		protected PartitionId PartitionId
		{
			get
			{
				if (this.partitionId == null)
				{
					AccountPartitionIdParameter accountPartitionIdParameter = (AccountPartitionIdParameter)base.UserSpecifiedParameters["AccountPartition"];
					if (accountPartitionIdParameter != null)
					{
						this.partitionId = RecipientTaskHelper.ResolvePartitionId(accountPartitionIdParameter, new Task.TaskErrorLoggingDelegate(this.WriteErrorToTaskErrorHandler));
					}
				}
				return this.partitionId;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006BDE File Offset: 0x00004DDE
		private void WriteErrorToTaskErrorHandler(Exception exception, ErrorCategory category, object unused)
		{
			base.WriteError((LocalizedException)exception, (ExchangeErrorCategory)category);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006BF4 File Offset: 0x00004DF4
		public sealed override bool UpdateAffectedIConfigurable(IConfigurable writeableIConfigurable)
		{
			if (writeableIConfigurable == null)
			{
				throw new ArgumentNullException("writeableIConfigurable");
			}
			ExTraceGlobals.RusTracer.TraceDebug<string, string>((long)this.GetHashCode(), "RUSProvisioningHandler.UpdateAffectedIConfigurable: writeableIConfigurable={0}, TaskName={1}", writeableIConfigurable.Identity.ToString(), base.TaskName);
			ADObject adobject;
			if (writeableIConfigurable is ADPresentationObject)
			{
				adobject = ((ADPresentationObject)writeableIConfigurable).DataObject;
			}
			else
			{
				adobject = (ADObject)writeableIConfigurable;
			}
			ADRecipient adrecipient = adobject as ADRecipient;
			return adrecipient != null && !string.IsNullOrEmpty(adrecipient.Alias) && this.UpdateRecipient(adrecipient);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006C77 File Offset: 0x00004E77
		protected virtual bool UpdateRecipient(ADRecipient recipient)
		{
			return false;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006C7C File Offset: 0x00004E7C
		public sealed override ProvisioningValidationError[] Validate(IConfigurable readOnlyADObject)
		{
			if (readOnlyADObject == null)
			{
				return null;
			}
			List<ProvisioningValidationError> list = new List<ProvisioningValidationError>();
			this.Validate(readOnlyADObject, list);
			return list.ToArray();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006CA2 File Offset: 0x00004EA2
		protected virtual void Validate(IConfigurable readOnlyADObject, List<ProvisioningValidationError> errors)
		{
		}

		// Token: 0x0400009E RID: 158
		private PartitionId partitionId;
	}
}
