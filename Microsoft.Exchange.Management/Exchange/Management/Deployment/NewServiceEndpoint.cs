using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000215 RID: 533
	[Cmdlet("New", "ServiceEndpoint")]
	public sealed class NewServiceEndpoint : NewSystemConfigurationObjectTask<ADServiceConnectionPoint>
	{
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x00050544 File Offset: 0x0004E744
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x0005057E File Offset: 0x0004E77E
		[Parameter(Mandatory = false)]
		public Uri Url
		{
			get
			{
				if (this.DataObject.ServiceBindingInformation != null && this.DataObject.ServiceBindingInformation.Count > 0)
				{
					return new Uri(this.DataObject.ServiceBindingInformation[0]);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.DataObject.ServiceBindingInformation = new MultiValuedProperty<string>();
					this.DataObject.ServiceBindingInformation.Add(value.ToString());
				}
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x000505B0 File Offset: 0x0004E7B0
		// (set) Token: 0x06001244 RID: 4676 RVA: 0x00050648 File Offset: 0x0004E848
		[Parameter(Mandatory = false)]
		public string UrlTemplate
		{
			get
			{
				if (this.DataObject.Keywords != null && this.DataObject.Keywords.Count > 0)
				{
					foreach (string text in this.DataObject.Keywords)
					{
						if (text.StartsWith(ServiceEndpointContainer.UriTemplateKey, StringComparison.OrdinalIgnoreCase))
						{
							return text.Substring(ServiceEndpointContainer.UriTemplateKey.Length);
						}
					}
				}
				return null;
			}
			set
			{
				string text = (value != null) ? value.Trim() : null;
				if (!string.IsNullOrEmpty(text))
				{
					if (this.DataObject.Keywords == null)
					{
						this.DataObject.Keywords = new MultiValuedProperty<string>();
					}
					this.DataObject.Keywords.Add(ServiceEndpointContainer.UriTemplateKey + text);
				}
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x000506A4 File Offset: 0x0004E8A4
		// (set) Token: 0x06001246 RID: 4678 RVA: 0x0005073C File Offset: 0x0004E93C
		[Parameter(Mandatory = false)]
		public string Token
		{
			get
			{
				if (this.DataObject.Keywords != null && this.DataObject.Keywords.Count > 0)
				{
					foreach (string text in this.DataObject.Keywords)
					{
						if (text.StartsWith(ServiceEndpointContainer.TokenKey, StringComparison.OrdinalIgnoreCase))
						{
							return text.Substring(ServiceEndpointContainer.TokenKey.Length);
						}
					}
				}
				return null;
			}
			set
			{
				string text = (value != null) ? value.Trim() : null;
				if (!string.IsNullOrEmpty(text))
				{
					if (this.DataObject.Keywords == null)
					{
						this.DataObject.Keywords = new MultiValuedProperty<string>();
					}
					this.DataObject.Keywords.Add(ServiceEndpointContainer.TokenKey + text);
				}
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x00050798 File Offset: 0x0004E998
		// (set) Token: 0x06001248 RID: 4680 RVA: 0x00050830 File Offset: 0x0004EA30
		[Parameter(Mandatory = false)]
		public string CertificateSubjectName
		{
			get
			{
				if (this.DataObject.Keywords != null && this.DataObject.Keywords.Count > 0)
				{
					foreach (string text in this.DataObject.Keywords)
					{
						if (text.StartsWith(ServiceEndpointContainer.CertSubjectKey, StringComparison.OrdinalIgnoreCase))
						{
							return text.Substring(ServiceEndpointContainer.CertSubjectKey.Length);
						}
					}
				}
				return null;
			}
			set
			{
				string text = (value != null) ? value.Trim() : null;
				if (!string.IsNullOrEmpty(text))
				{
					if (this.DataObject.Keywords == null)
					{
						this.DataObject.Keywords = new MultiValuedProperty<string>();
					}
					this.DataObject.Keywords.Add(ServiceEndpointContainer.CertSubjectKey + text);
				}
			}
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0005088C File Offset: 0x0004EA8C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			ADObjectId childId = base.RootOrgContainerId.GetChildId(ServiceEndpointContainer.DefaultName).GetChildId(this.DataObject.Name);
			this.DataObject.SetId(childId);
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x000508D8 File Offset: 0x0004EAD8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			ADServiceConnectionPoint adserviceConnectionPoint = configurationSession.Read<ADServiceConnectionPoint>(this.DataObject.Id);
			if (adserviceConnectionPoint != null)
			{
				adserviceConnectionPoint.ServiceBindingInformation = this.DataObject.ServiceBindingInformation;
				adserviceConnectionPoint.Keywords = this.DataObject.Keywords;
				configurationSession.Save(adserviceConnectionPoint);
				base.WriteObject(adserviceConnectionPoint);
			}
			else
			{
				base.InternalProcessRecord();
				adserviceConnectionPoint = configurationSession.Read<ADServiceConnectionPoint>(this.DataObject.Id);
			}
			base.WriteVerbose(Strings.VerboseUpdatedServiceEndpoint(adserviceConnectionPoint.Name, adserviceConnectionPoint.OriginatingServer));
			TaskLogger.LogExit();
		}
	}
}
