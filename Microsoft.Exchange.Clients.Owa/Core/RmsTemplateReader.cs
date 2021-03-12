using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200023A RID: 570
	internal sealed class RmsTemplateReader
	{
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00077380 File Offset: 0x00075580
		internal bool IsInternalLicensingEnabled
		{
			get
			{
				bool result;
				try
				{
					result = RmsClientManager.IRMConfig.IsInternalLicensingEnabledForTenant(this.organizationId);
				}
				catch (ExchangeConfigurationException innerException)
				{
					throw new RightsManagementTransientException(ServerStrings.RmExceptionGenericMessage, innerException);
				}
				catch (RightsManagementException ex)
				{
					if (ex.IsPermanent)
					{
						throw new RightsManagementPermanentException(ServerStrings.RmExceptionGenericMessage, ex);
					}
					throw new RightsManagementTransientException(ServerStrings.RmExceptionGenericMessage, ex);
				}
				return result;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x000773EC File Offset: 0x000755EC
		internal bool IsExternalLicensingEnabled
		{
			get
			{
				bool result;
				try
				{
					result = RmsClientManager.IRMConfig.IsExternalLicensingEnabledForTenant(this.organizationId);
				}
				catch (ExchangeConfigurationException innerException)
				{
					throw new RightsManagementTransientException(ServerStrings.RmExceptionGenericMessage, innerException);
				}
				catch (RightsManagementException ex)
				{
					if (ex.IsPermanent)
					{
						throw new RightsManagementPermanentException(ServerStrings.RmExceptionGenericMessage, ex);
					}
					throw new RightsManagementTransientException(ServerStrings.RmExceptionGenericMessage, ex);
				}
				return result;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00077458 File Offset: 0x00075658
		internal bool TemplateAcquisitionFailed
		{
			get
			{
				return this.errorAcquiringTemplates;
			}
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00077460 File Offset: 0x00075660
		internal RmsTemplateReader(OrganizationId organizationId)
		{
			this.organizationId = organizationId;
			this.errorAcquiringTemplates = false;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00077478 File Offset: 0x00075678
		public IEnumerable<RmsTemplate> GetRmsTemplates()
		{
			this.errorAcquiringTemplates = false;
			if (this.IsInternalLicensingEnabled)
			{
				try
				{
					return RmsClientManager.AcquireRmsTemplates(this.organizationId, false);
				}
				catch (ExchangeConfigurationException arg)
				{
					ExTraceGlobals.CoreTracer.TraceError<ExchangeConfigurationException>(0L, "ExchangeConfigurationException while loading RMS templates: {0}", arg);
					this.errorAcquiringTemplates = true;
				}
				catch (RightsManagementException arg2)
				{
					ExTraceGlobals.CoreTracer.TraceError<RightsManagementException>(0L, "RightsManagementException while Loading RMS templates: {0}", arg2);
					this.errorAcquiringTemplates = true;
				}
			}
			return RmsTemplateReader.EmptyRmsTemplateList;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00077500 File Offset: 0x00075700
		public RmsTemplate LookupRmsTemplate(Guid guid)
		{
			if (guid == Guid.Empty)
			{
				return null;
			}
			foreach (RmsTemplate rmsTemplate in this.GetRmsTemplates())
			{
				if (rmsTemplate.Id == guid)
				{
					return rmsTemplate;
				}
			}
			return null;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0007756C File Offset: 0x0007576C
		internal string GetDescription(Guid guid, CultureInfo locale)
		{
			ComplianceReader.ThrowOnNullArgument(locale, "locale");
			return this.GetDescription(this.LookupRmsTemplate(guid), locale);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00077588 File Offset: 0x00075788
		private string GetDescription(RmsTemplate template, CultureInfo locale)
		{
			ComplianceReader.ThrowOnNullArgument(locale, "locale");
			string result = string.Empty;
			if (template != null)
			{
				string name = template.GetName(locale);
				string description = template.GetDescription(locale);
				if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
				{
					if (!string.IsNullOrEmpty(name))
					{
						result = name;
					}
					else if (!string.IsNullOrEmpty(description))
					{
						result = description;
					}
				}
				else
				{
					result = name + " - " + description;
				}
			}
			return result;
		}

		// Token: 0x04000D3A RID: 3386
		private static readonly List<RmsTemplate> EmptyRmsTemplateList = new List<RmsTemplate>();

		// Token: 0x04000D3B RID: 3387
		private readonly OrganizationId organizationId;

		// Token: 0x04000D3C RID: 3388
		private bool errorAcquiringTemplates;
	}
}
