using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A0D RID: 2573
	[Cmdlet("New", "GlobalLocatorServiceDomain", DefaultParameterSetName = "ExternalDirectoryOrganizationIdParameterSet", SupportsShouldProcess = true)]
	public sealed class NewGlobalLocatorServiceDomain : ManageGlobalLocatorServiceBase
	{
		// Token: 0x17001BA5 RID: 7077
		// (get) Token: 0x06005C4A RID: 23626 RVA: 0x001852D0 File Offset: 0x001834D0
		// (set) Token: 0x06005C4B RID: 23627 RVA: 0x001852E7 File Offset: 0x001834E7
		[Parameter(Mandatory = true)]
		[ValidateNotNull]
		public SmtpDomain DomainName
		{
			get
			{
				return (SmtpDomain)base.Fields["DomainName"];
			}
			set
			{
				base.Fields["DomainName"] = value;
			}
		}

		// Token: 0x17001BA6 RID: 7078
		// (get) Token: 0x06005C4C RID: 23628 RVA: 0x001852FA File Offset: 0x001834FA
		// (set) Token: 0x06005C4D RID: 23629 RVA: 0x00185311 File Offset: 0x00183511
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public GlsDomainFlags DomainFlags
		{
			get
			{
				return (GlsDomainFlags)base.Fields["DomainFlags"];
			}
			set
			{
				base.Fields["DomainFlags"] = value;
			}
		}

		// Token: 0x17001BA7 RID: 7079
		// (get) Token: 0x06005C4E RID: 23630 RVA: 0x00185329 File Offset: 0x00183529
		// (set) Token: 0x06005C4F RID: 23631 RVA: 0x00185340 File Offset: 0x00183540
		[ValidateNotNull]
		[Parameter(Mandatory = true)]
		public DomainKeyType DomainType
		{
			get
			{
				return (DomainKeyType)base.Fields["DomainType"];
			}
			set
			{
				base.Fields["DomainType"] = value;
			}
		}

		// Token: 0x17001BA8 RID: 7080
		// (get) Token: 0x06005C50 RID: 23632 RVA: 0x00185358 File Offset: 0x00183558
		// (set) Token: 0x06005C51 RID: 23633 RVA: 0x0018536F File Offset: 0x0018356F
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
		public bool DomainInUse
		{
			get
			{
				return (bool)base.Fields["DomainInUse"];
			}
			set
			{
				base.Fields["DomainInUse"] = value;
			}
		}

		// Token: 0x17001BA9 RID: 7081
		// (get) Token: 0x06005C52 RID: 23634 RVA: 0x00185388 File Offset: 0x00183588
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string id;
				if (base.Fields.IsModified("ExternalDirectoryOrganizationId"))
				{
					id = ((Guid)base.Fields["ExternalDirectoryOrganizationId"]).ToString();
				}
				else
				{
					id = ((SmtpDomain)base.Fields["DomainName"]).Domain;
				}
				return Strings.ConfirmationMessageNewGls("Domain", id);
			}
		}

		// Token: 0x06005C53 RID: 23635 RVA: 0x001853F4 File Offset: 0x001835F4
		protected override void InternalProcessRecord()
		{
			GlobalLocatorServiceDomain globalLocatorServiceDomain = new GlobalLocatorServiceDomain();
			List<KeyValuePair<DomainProperty, PropertyValue>> list = new List<KeyValuePair<DomainProperty, PropertyValue>>();
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			BitVector32 bitVector = default(BitVector32);
			bitVector[1] = false;
			globalLocatorServiceDomain.ExternalDirectoryOrganizationId = (Guid)base.Fields["ExternalDirectoryOrganizationId"];
			globalLocatorServiceDomain.DomainName = (SmtpDomain)base.Fields["DomainName"];
			globalLocatorServiceDomain.DomainInUse = (base.Fields.IsModified("DomainInUse") && (bool)base.Fields["DomainInUse"]);
			list.Add(new KeyValuePair<DomainProperty, PropertyValue>(DomainProperty.ExoDomainInUse, new PropertyValue(globalLocatorServiceDomain.DomainInUse)));
			if (base.Fields.IsModified("DomainFlags"))
			{
				globalLocatorServiceDomain.DomainFlags = new GlsDomainFlags?((GlsDomainFlags)base.Fields["DomainFlags"]);
				if ((globalLocatorServiceDomain.DomainFlags & GlsDomainFlags.Nego2Enabled) == GlsDomainFlags.Nego2Enabled)
				{
					bitVector[1] = true;
				}
				if ((globalLocatorServiceDomain.DomainFlags & GlsDomainFlags.OAuth2ClientProfileEnabled) == GlsDomainFlags.OAuth2ClientProfileEnabled)
				{
					bitVector[2] = true;
				}
			}
			list.Add(new KeyValuePair<DomainProperty, PropertyValue>(DomainProperty.ExoFlags, new PropertyValue(bitVector.Data)));
			glsDirectorySession.AddAcceptedDomain(globalLocatorServiceDomain.ExternalDirectoryOrganizationId, globalLocatorServiceDomain.DomainName.Domain, (DomainKeyType)base.Fields["DomainType"], list.ToArray());
			base.WriteObject(globalLocatorServiceDomain);
		}
	}
}
