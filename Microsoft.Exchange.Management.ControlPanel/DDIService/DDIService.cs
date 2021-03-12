using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;
using Microsoft.Office.CompliancePolicy.Tasks;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000122 RID: 290
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class DDIService : IDDIService
	{
		// Token: 0x0600204D RID: 8269 RVA: 0x000614C8 File Offset: 0x0005F6C8
		public static bool UseDDIService(WebServiceReference service)
		{
			return service != null && !string.IsNullOrEmpty(service.ServiceUrl) && service.ServiceUrl.IndexOf("DDIService.svc", StringComparison.OrdinalIgnoreCase) != -1;
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x000614F4 File Offset: 0x0005F6F4
		internal static void AddKnownType(ICustomAttributeProvider provider, Type type)
		{
			List<Type> list = (List<Type>)DDIService.GetKnownTypes(provider);
			list.Add(type);
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x00061514 File Offset: 0x0005F714
		private static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
		{
			return DDIService.KnownTypes.Value;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00061520 File Offset: 0x0005F720
		public PowerShellResults<JsonDictionary<object>> GetList(DDIParameters filter, SortOptions sort)
		{
			DDIServiceHelper ddiserviceHelper = this.CreateDDIServiceHelper();
			return ddiserviceHelper.GetList(filter, sort);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x0006153C File Offset: 0x0005F73C
		public PowerShellResults<JsonDictionary<object>> GetObject(Identity identity)
		{
			DDIServiceHelper ddiserviceHelper = this.CreateDDIServiceHelper();
			return ddiserviceHelper.GetObject(identity);
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00061558 File Offset: 0x0005F758
		public PowerShellResults<JsonDictionary<object>> GetObjectOnDemand(Identity identity, string workflow)
		{
			DDIServiceHelper ddiserviceHelper = this.CreateDDIServiceHelper(workflow);
			return ddiserviceHelper.GetObjectOnDemand(identity);
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x00061574 File Offset: 0x0005F774
		public PowerShellResults<JsonDictionary<object>> GetObjectForNew(Identity identity)
		{
			DDIServiceHelper ddiserviceHelper = this.CreateDDIServiceHelper();
			return ddiserviceHelper.GetObjectForNew(identity);
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x00061590 File Offset: 0x0005F790
		public PowerShellResults<JsonDictionary<object>> SetObject(Identity identity, DDIParameters properties)
		{
			DDIServiceHelper ddiserviceHelper = this.CreateDDIServiceHelper();
			return ddiserviceHelper.SetObject(identity, properties);
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x000615AC File Offset: 0x0005F7AC
		public PowerShellResults<JsonDictionary<object>> GetProgress(string progressId)
		{
			DDIServiceHelper ddiserviceHelper = this.CreateDDIServiceHelper();
			return ddiserviceHelper.GetProgress(progressId, HttpContext.Current != null && "GetList".Equals(HttpContext.Current.Request.QueryString["AyncMethod"], StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000615F8 File Offset: 0x0005F7F8
		public PowerShellResults Cancel(string progressId)
		{
			DDIServiceHelper ddiserviceHelper = this.CreateDDIServiceHelper();
			return ddiserviceHelper.Cancel(progressId);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x00061614 File Offset: 0x0005F814
		public PowerShellResults<JsonDictionary<object>> NewObject(DDIParameters properties)
		{
			DDIServiceHelper ddiserviceHelper = this.CreateDDIServiceHelper();
			return ddiserviceHelper.NewObject(properties);
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x00061630 File Offset: 0x0005F830
		public PowerShellResults RemoveObjects(Identity[] identities, DDIParameters parameters)
		{
			DDIServiceHelper ddiserviceHelper = this.CreateDDIServiceHelper();
			return ddiserviceHelper.RemoveObjects(identities, parameters);
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x0006164C File Offset: 0x0005F84C
		public PowerShellResults<JsonDictionary<object>> MultiObjectExecute(Identity[] identities, DDIParameters parameters)
		{
			DDIServiceHelper ddiserviceHelper = this.CreateDDIServiceHelper();
			return ddiserviceHelper.MultiObjectExecute(identities, parameters);
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x00061668 File Offset: 0x0005F868
		public PowerShellResults<JsonDictionary<object>> SingleObjectExecute(Identity identity, DDIParameters properties)
		{
			DDIServiceHelper ddiserviceHelper = this.CreateDDIServiceHelper();
			return ddiserviceHelper.SingleObjectExecute(identity, properties);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x00061684 File Offset: 0x0005F884
		public void InitializeOperationContext(string serviceUrl)
		{
			if (string.IsNullOrEmpty(serviceUrl))
			{
				throw new ArgumentNullException("serviceUrl");
			}
			Uri uri = new Uri(serviceUrl, UriKind.RelativeOrAbsolute);
			if (!uri.IsAbsoluteUri)
			{
				uri = new Uri(new Uri("http://localhost"), serviceUrl);
			}
			UriTemplate uriTemplate = new UriTemplate("*");
			Uri baseAddress = new Uri(uri.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped));
			UriTemplateMatch uriTemplateMatch = uriTemplate.Match(baseAddress, uri);
			this.schema = uriTemplateMatch.QueryParameters["schema"];
			this.workflow = uriTemplateMatch.QueryParameters["workflow"];
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x00061714 File Offset: 0x0005F914
		private DDIServiceHelper CreateDDIServiceHelper()
		{
			return this.CreateDDIServiceHelper(null);
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x0006171D File Offset: 0x0005F91D
		private DDIServiceHelper CreateDDIServiceHelper(string workflow)
		{
			this.GetSchemaAndWorkflowFromRequest();
			return new DDIServiceHelper(this.schema, workflow ?? this.workflow);
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x0006173C File Offset: 0x0005F93C
		private void GetSchemaAndWorkflowFromRequest()
		{
			if (WebOperationContext.Current != null)
			{
				UriTemplateMatch uriTemplateMatch = WebOperationContext.Current.IncomingRequest.UriTemplateMatch;
				this.schema = uriTemplateMatch.QueryParameters["schema"];
				if (string.IsNullOrEmpty(this.schema))
				{
					throw new FaultException(new ArgumentNullException("schema").Message);
				}
				this.workflow = uriTemplateMatch.QueryParameters["workflow"];
			}
		}

		// Token: 0x04001CC9 RID: 7369
		internal const string GetListString = "GetList";

		// Token: 0x04001CCA RID: 7370
		private const string AyncMethodString = "AyncMethod";

		// Token: 0x04001CCB RID: 7371
		private const string DDIServiceSVC = "DDIService.svc";

		// Token: 0x04001CCC RID: 7372
		public const string SchemaParameter = "schema";

		// Token: 0x04001CCD RID: 7373
		public const string WorkflowParameter = "workflow";

		// Token: 0x04001CCE RID: 7374
		public static readonly string DDIPath = Path.Combine(ConfigurationContext.Setup.InstallPath, "ClientAccess\\ecp\\DDI");

		// Token: 0x04001CCF RID: 7375
		private string schema;

		// Token: 0x04001CD0 RID: 7376
		private string workflow;

		// Token: 0x04001CD1 RID: 7377
		internal static LazilyInitialized<List<Type>> KnownTypes = new LazilyInitialized<List<Type>>(delegate()
		{
			List<Type> list = new List<Type>();
			foreach (Type type in typeof(DDIService).Assembly.GetTypes())
			{
				if (type.IsDataContract() || type.GetCustomAttributes(typeof(SerializableAttribute), false).Length > 0)
				{
					if (!type.IsGenericTypeDefinition)
					{
						list.Add(type);
					}
					else
					{
						Type[] genericArguments = type.GetGenericArguments();
						Type[] array = new Type[genericArguments.Length];
						int num = 0;
						while (num < genericArguments.Length && !(genericArguments[num].BaseType != typeof(object)))
						{
							array[num] = typeof(object);
							num++;
						}
						if (num == genericArguments.Length - 1)
						{
							list.Add(type.MakeGenericType(array));
						}
					}
				}
			}
			list.Add(typeof(ADObjectId));
			list.Add(typeof(List<OwaMailboxPolicyFeatureInfo>));
			list.Add(typeof(List<AdObjectResolverRow>));
			list.Add(typeof(List<RecipientObjectResolverRow>));
			list.Add(typeof(List<AcePermissionRecipientRow>));
			list.Add(typeof(List<MailCertificate>));
			list.Add(typeof(List<SharingDomain>));
			list.Add(typeof(List<object>));
			list.Add(typeof(ValidatorInfo[]));
			list.Add(typeof(Identity[]));
			list.Add(typeof(ErrorRecord[]));
			list.Add(typeof(string[]));
			list.Add(typeof(ProxyAddressCollection));
			list.Add(typeof(SmtpProxyAddress));
			list.Add(typeof(SmtpProxyAddressPrefix));
			list.Add(typeof(EumProxyAddress));
			list.Add(typeof(EumProxyAddressPrefix));
			list.Add(typeof(CustomProxyAddress));
			list.Add(typeof(CustomProxyAddressPrefix));
			list.Add(typeof(ProxyAddress));
			list.Add(typeof(CountryInfo));
			list.Add(typeof(ProxyAddressTemplate));
			list.Add(typeof(List<SharingRuleEntry>));
			list.Add(typeof(List<PublicFolderPermissionInfo>));
			list.Add(typeof(SmtpDomain));
			list.Add(typeof(PowerShellResults<JsonDictionary<object>>));
			list.Add(typeof(List<IPAddressEntry>));
			list.Add(typeof(List<DomainEntry>));
			list.Add(typeof(MultiValuedProperty<IPRange>));
			list.Add(typeof(ADMultiValuedProperty<ADObjectId>));
			list.Add(typeof(MultiValuedProperty<SmtpDomain>));
			list.Add(typeof(bool[]));
			list.Add(typeof(EnhancedTimeSpan));
			list.Add(typeof(List<RetentionPolicyTagResolverRow>));
			list.Add(typeof(List<ServerResolverRow>));
			list.Add(typeof(List<MigrationEndpointObject>));
			list.Add(typeof(string[][]));
			list.Add(typeof(List<RecipientObjectResolverRow>));
			list.Add(typeof(MigrationReportGroupDetails[]));
			list.Add(typeof(PeopleIdentity[]));
			list.Add(typeof(PeopleIdentity));
			list.Add(typeof(List<DropDownItemData>));
			list.Add(typeof(UMDialingRuleEntry[]));
			list.Add(typeof(UMDialingRuleGroupRow[]));
			list.Add(typeof(MultiValuedProperty<UMNumberingPlanFormat>));
			list.Add(typeof(MultiValuedProperty<CustomMenuKeyMapping>));
			list.Add(typeof(MultiValuedProperty<HolidaySchedule>));
			list.Add(typeof(List<UMAAHolidaySchedule>));
			list.Add(typeof(List<UMAAMenuKeyMapping>));
			list.Add(typeof(ScheduleInterval[]));
			list.Add(typeof(DataClassificationService.LanguageSetting[]));
			list.Add(typeof(Fingerprint[]));
			list.Add(typeof(JsonDictionary<object>[]));
			list.Add(typeof(SecurityPrincipalPickerObject[]));
			list.Add(typeof(PolicyApplyStatus));
			list.Add(typeof(MultiValuedProperty<PolicyDistributionErrorDetails>));
			list.Add(typeof(List<UHPolicyDistributionErrorDetails>));
			list.Add(typeof(MultiValuedProperty<BindingMetadata>));
			list.Add(typeof(List<UHExchangeBinding>));
			list.Add(typeof(List<UHSharepointBinding>));
			list.Add(typeof(HoldDurationHint));
			list.Add(typeof(DateTime?));
			list.Add(typeof(Unlimited<int>?));
			return list;
		});
	}
}
