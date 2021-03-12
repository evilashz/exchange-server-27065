using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.RightsManagement;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000726 RID: 1830
	internal sealed class RmsTemplateDataProvider : IConfigDataProvider
	{
		// Token: 0x060040E2 RID: 16610 RVA: 0x00109DA1 File Offset: 0x00107FA1
		public RmsTemplateDataProvider(IConfigurationSession adSession) : this(adSession, RmsTemplateType.Distributed, false, null)
		{
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x00109DAD File Offset: 0x00107FAD
		public RmsTemplateDataProvider(IConfigurationSession adSession, RmsTemplateType typeToFetch, bool displayTemplatesIfInternalLicensingDisabled) : this(adSession, typeToFetch, displayTemplatesIfInternalLicensingDisabled, null)
		{
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x00109DBC File Offset: 0x00107FBC
		public RmsTemplateDataProvider(IConfigurationSession adSession, RmsTemplateType typeToFetch, bool displayTemplatesIfInternalLicensingDisabled, RMSTrustedPublishingDomain trustedPublishingDomain)
		{
			if (adSession == null)
			{
				throw new ArgumentNullException("adSession");
			}
			if (adSession.SessionSettings == null)
			{
				throw new ArgumentNullException("adSession.SessionSettings");
			}
			this.adSession = adSession;
			this.orgId = adSession.SessionSettings.CurrentOrganizationId;
			this.typeToFetch = typeToFetch;
			this.displayTemplatesIfInternalLicensingDisabled = displayTemplatesIfInternalLicensingDisabled;
			this.irmConfiguration = IRMConfiguration.Read(this.adSession);
			this.trustedPublishingDomain = trustedPublishingDomain;
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x060040E5 RID: 16613 RVA: 0x00109E30 File Offset: 0x00108030
		public string Source
		{
			get
			{
				if (this.orgId == OrganizationId.ForestWideOrgId)
				{
					try
					{
						try
						{
							RmsClientManager.ADSession = this.adSession;
							Uri rmsserviceLocation = RmsClientManager.GetRMSServiceLocation(this.adSession.SessionSettings.CurrentOrganizationId, ServiceType.ClientLicensor);
							if (rmsserviceLocation != null)
							{
								return rmsserviceLocation.ToString();
							}
						}
						catch (RightsManagementException)
						{
							return null;
						}
						catch (ExchangeConfigurationException)
						{
							return null;
						}
						goto IL_87;
					}
					finally
					{
						RmsClientManager.ADSession = null;
					}
				}
				if (this.irmConfiguration != null && this.irmConfiguration.ServiceLocation != null)
				{
					return this.irmConfiguration.ServiceLocation.ToString();
				}
				IL_87:
				return null;
			}
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x00109EF0 File Offset: 0x001080F0
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			RmsTemplateIdentity rmsTemplateIdentity = identity as RmsTemplateIdentity;
			if (rmsTemplateIdentity == null)
			{
				throw new ArgumentNullException("identity");
			}
			IConfigurable result;
			try
			{
				RmsClientManager.ADSession = this.adSession;
				foreach (RmsTemplate rmsTemplate in this.AcquireRmsTemplates())
				{
					if (rmsTemplate.Id == rmsTemplateIdentity.TemplateId)
					{
						return new RmsTemplatePresentation(rmsTemplate);
					}
				}
				result = null;
			}
			finally
			{
				RmsClientManager.ADSession = null;
			}
			return result;
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x00109F8C File Offset: 0x0010818C
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			IEnumerable<T> enumerable = this.FindPaged<T>(filter, rootId, deepSearch, sortBy, 0);
			LinkedList<IConfigurable> linkedList = new LinkedList<IConfigurable>();
			foreach (T t in enumerable)
			{
				IConfigurable value = t;
				linkedList.AddLast(value);
				if (linkedList.Count >= 1000)
				{
					break;
				}
			}
			IConfigurable[] array = new IConfigurable[linkedList.Count];
			linkedList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x0010A258 File Offset: 0x00108458
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			RmsTemplateQueryFilter templateQueryFilter = filter as RmsTemplateQueryFilter;
			if (templateQueryFilter == null)
			{
				templateQueryFilter = RmsTemplateQueryFilter.MatchAll;
			}
			try
			{
				RmsClientManager.ADSession = this.adSession;
				foreach (RmsTemplate template in this.AcquireRmsTemplates())
				{
					if (templateQueryFilter.Match(template))
					{
						yield return (T)((object)new RmsTemplatePresentation(template));
					}
				}
			}
			finally
			{
				RmsClientManager.ADSession = null;
			}
			yield break;
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x0010A27C File Offset: 0x0010847C
		public void Save(IConfigurable instance)
		{
			if (this.orgId == OrganizationId.ForestWideOrgId)
			{
				throw new NotSupportedException();
			}
			RmsTemplatePresentation rmsTemplatePresentation = instance as RmsTemplatePresentation;
			if (rmsTemplatePresentation == null)
			{
				throw new ArgumentException("passed in instance not of type RmsTemplatePresentation", "instance");
			}
			Guid templateGuid = rmsTemplatePresentation.TemplateGuid;
			RMSTrustedPublishingDomain rmstrustedPublishingDomain = this.FindDefaultTPD();
			if (rmstrustedPublishingDomain == null)
			{
				return;
			}
			if (!MultiValuedPropertyBase.IsNullOrEmpty(rmstrustedPublishingDomain.RMSTemplates))
			{
				string text = null;
				string text2 = null;
				foreach (string text3 in rmstrustedPublishingDomain.RMSTemplates)
				{
					RmsTemplateType rmsTemplateType;
					string text4 = RMUtil.DecompressTemplate(text3, out rmsTemplateType);
					Guid templateGuidFromLicense = DrmClientUtils.GetTemplateGuidFromLicense(text4);
					if (templateGuidFromLicense == templateGuid && rmsTemplateType != rmsTemplatePresentation.Type)
					{
						text = text3;
						text2 = RMUtil.CompressTemplate(text4, rmsTemplatePresentation.Type);
						break;
					}
				}
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
				{
					rmstrustedPublishingDomain.RMSTemplates.Remove(text);
					rmstrustedPublishingDomain.RMSTemplates.Add(text2);
					this.adSession.Save(rmstrustedPublishingDomain);
				}
			}
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x0010A398 File Offset: 0x00108598
		public void Delete(IConfigurable instance)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x0010A3A0 File Offset: 0x001085A0
		private IEnumerable<RmsTemplate> AcquireRmsTemplates()
		{
			if (this.orgId == OrganizationId.ForestWideOrgId)
			{
				return RmsClientManager.AcquireRmsTemplates(this.orgId, true);
			}
			if (this.irmConfiguration == null || (!this.irmConfiguration.InternalLicensingEnabled && !this.displayTemplatesIfInternalLicensingDisabled))
			{
				return DrmEmailConstants.EmptyTemplateArray;
			}
			RMSTrustedPublishingDomain rmstrustedPublishingDomain = this.FindTPD();
			if (rmstrustedPublishingDomain == null)
			{
				return DrmEmailConstants.EmptyTemplateArray;
			}
			List<RmsTemplate> list = null;
			if (!MultiValuedPropertyBase.IsNullOrEmpty(rmstrustedPublishingDomain.RMSTemplates))
			{
				list = new List<RmsTemplate>(rmstrustedPublishingDomain.RMSTemplates.Count + 2);
				using (MultiValuedProperty<string>.Enumerator enumerator = rmstrustedPublishingDomain.RMSTemplates.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string encodedTemplate = enumerator.Current;
						RmsTemplateType type = RmsTemplateType.Archived;
						string templateXrml = RMUtil.DecompressTemplate(encodedTemplate, out type);
						if (this.ShouldFetch(type))
						{
							list.Add(RmsTemplate.CreateServerTemplateFromTemplateDefinition(templateXrml, type));
						}
					}
					goto IL_CE;
				}
			}
			list = new List<RmsTemplate>(2);
			IL_CE:
			if (this.typeToFetch != RmsTemplateType.Archived && rmstrustedPublishingDomain.Default)
			{
				list.Add(RmsTemplate.DoNotForward);
				if (this.irmConfiguration.InternetConfidentialEnabled)
				{
					list.Add(RmsTemplate.InternetConfidential);
				}
			}
			return list;
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x0010A4C0 File Offset: 0x001086C0
		private bool ShouldFetch(RmsTemplateType type)
		{
			return this.typeToFetch == RmsTemplateType.All || this.typeToFetch == type;
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x0010A4D7 File Offset: 0x001086D7
		private RMSTrustedPublishingDomain FindTPD()
		{
			if (this.trustedPublishingDomain == null)
			{
				this.trustedPublishingDomain = this.FindDefaultTPD();
			}
			return this.trustedPublishingDomain;
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x0010A4F4 File Offset: 0x001086F4
		private RMSTrustedPublishingDomain FindDefaultTPD()
		{
			if (this.irmConfiguration == null)
			{
				return null;
			}
			ADPagedReader<RMSTrustedPublishingDomain> adpagedReader = this.adSession.FindPaged<RMSTrustedPublishingDomain>(this.irmConfiguration.Id, QueryScope.OneLevel, null, null, 0);
			foreach (RMSTrustedPublishingDomain rmstrustedPublishingDomain in adpagedReader)
			{
				if (rmstrustedPublishingDomain.Default)
				{
					return rmstrustedPublishingDomain;
				}
			}
			return null;
		}

		// Token: 0x04002918 RID: 10520
		private IConfigurationSession adSession;

		// Token: 0x04002919 RID: 10521
		private OrganizationId orgId;

		// Token: 0x0400291A RID: 10522
		private RmsTemplateType typeToFetch;

		// Token: 0x0400291B RID: 10523
		private readonly bool displayTemplatesIfInternalLicensingDisabled;

		// Token: 0x0400291C RID: 10524
		private IRMConfiguration irmConfiguration;

		// Token: 0x0400291D RID: 10525
		private RMSTrustedPublishingDomain trustedPublishingDomain;
	}
}
