using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.InfoWorker.Common.MailTips;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.OutlookProtection;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.PolicyNudges;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.ClientAccess;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200032A RID: 810
	internal sealed class GetServiceConfiguration : SingleStepServiceCommand<GetServiceConfigurationRequest, ServiceConfigurationResponseMessage[]>
	{
		// Token: 0x060016DE RID: 5854 RVA: 0x000794E8 File Offset: 0x000776E8
		public GetServiceConfiguration(CallContext callContext, GetServiceConfigurationRequest request) : base(callContext, request)
		{
			this.traceId = this.GetHashCode();
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00079530 File Offset: 0x00077730
		private static ExchangePrincipal GetPrincipal(CallContext callContext, EmailAddressWrapper sendingAs)
		{
			ExchangePrincipal exchangePrincipal;
			if (sendingAs != null)
			{
				string emailAddress = sendingAs.EmailAddress;
				exchangePrincipal = ExchangePrincipalCache.GetFromCache(emailAddress, callContext.ADRecipientSessionContext);
			}
			else
			{
				exchangePrincipal = callContext.AccessingPrincipal;
				if (exchangePrincipal == null)
				{
					throw new NonExistentMailboxException((CoreResources.IDs)4088802584U, string.Empty);
				}
			}
			return exchangePrincipal;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x00079578 File Offset: 0x00077778
		internal override ServiceResult<ServiceConfigurationResponseMessage[]> Execute()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			ServiceResult<ServiceConfigurationResponseMessage[]> result;
			try
			{
				result = this.InternalExecute();
			}
			finally
			{
				stopwatch.Stop();
			}
			PerfCounterHelper.UpdateServiceConfigurationResponseTimePerformanceCounter(stopwatch.ElapsedMilliseconds);
			return result;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000795C0 File Offset: 0x000777C0
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetServiceConfigurationResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00079614 File Offset: 0x00077814
		private ServiceResult<ServiceConfigurationResponseMessage[]> InternalExecute()
		{
			foreach (string value in base.Request.ConfigurationTypes)
			{
				if (!Enum.IsDefined(typeof(ServiceConfigurationType), value))
				{
					throw new ServiceArgumentException((CoreResources.IDs)3640136612U);
				}
				ServiceConfigurationType serviceConfigurationType = (ServiceConfigurationType)Enum.Parse(typeof(ServiceConfigurationType), value);
				this.configurationTypes |= serviceConfigurationType;
			}
			ExTraceGlobals.GetOrganizationConfigurationCallTracer.TraceDebug<ServiceConfigurationType>((long)this.GetHashCode(), "Getting organization configuration {0}", this.configurationTypes);
			CachedOrganizationConfiguration cachedOrganizationConfiguration = null;
			if (this.CallerRequested(ServiceConfigurationType.UnifiedMessagingConfiguration))
			{
				if (base.Request.ActingAs != null)
				{
					throw new ServiceArgumentException((CoreResources.IDs)2476021338U);
				}
				this.unifiedMessagingPrincipal = base.CallContext.AccessingPrincipal;
				if (this.unifiedMessagingPrincipal == null)
				{
					throw new NonExistentMailboxException((CoreResources.IDs)4088802584U, string.Empty);
				}
			}
			if (this.CallerRequested(ServiceConfigurationType.MailTips | ServiceConfigurationType.ProtectionRules | ServiceConfigurationType.PolicyNudges))
			{
				ExTraceGlobals.GetOrganizationConfigurationCallTracer.TraceDebug((long)this.GetHashCode(), "Getting organization configuration instance");
				this.GetActor();
				OrganizationId organizationId = (OrganizationId)this.actingAsRecipient[ADObjectSchema.OrganizationId];
				cachedOrganizationConfiguration = CachedOrganizationConfiguration.GetInstance(organizationId, CachedOrganizationConfiguration.ConfigurationTypes.All);
			}
			List<XmlElement> list = new List<XmlElement>();
			if (this.CallerRequested(ServiceConfigurationType.MailTips))
			{
				MailTipsConfiguration mailTipsConfiguration = new MailTipsConfiguration(this.traceId);
				mailTipsConfiguration.Initialize(cachedOrganizationConfiguration, this.actingAsRecipient);
				XmlElement item = this.SerializeMailTipsConfiguration(cachedOrganizationConfiguration.OrganizationConfiguration.Configuration.MailTipsAllTipsEnabled, mailTipsConfiguration, cachedOrganizationConfiguration.Domains);
				list.Add(item);
			}
			if (this.CallerRequested(ServiceConfigurationType.UnifiedMessagingConfiguration))
			{
				XmlElement item2;
				using (UMClientCommon umclientCommon = new UMClientCommon(this.unifiedMessagingPrincipal))
				{
					if (!umclientCommon.IsUMEnabled())
					{
						item2 = this.SerializeUnifiedMessageConfiguration(false, new UMPropertiesEx
						{
							PlayOnPhoneDialString = string.Empty,
							PlayOnPhoneEnabled = false
						});
					}
					else
					{
						UMPropertiesEx umproperties = umclientCommon.GetUMProperties();
						item2 = this.SerializeUnifiedMessageConfiguration(true, umproperties);
					}
				}
				list.Add(item2);
			}
			if (this.CallerRequested(ServiceConfigurationType.ProtectionRules))
			{
				XmlElement item3 = this.SerializeProtectionRulesConfiguration(cachedOrganizationConfiguration.ProtectionRules, cachedOrganizationConfiguration.Domains);
				list.Add(item3);
			}
			if (this.CallerRequested(ServiceConfigurationType.PolicyNudges))
			{
				using (XmlNodeReader xmlNodeReader = new XmlNodeReader(base.Request.ConfigurationRequestDetails))
				{
					xmlNodeReader.MoveToContent();
					PolicyNudgeConfiguration policyNudgeConfiguration = PolicyNudgeConfigurationFactory.Create();
					XElement root = XDocument.Load(xmlNodeReader).Root;
					foreach (XElement xelement in root.DescendantsAndSelf())
					{
						xelement.Name = XNamespace.None.GetName(xelement.Name.LocalName);
						xelement.ReplaceAttributes(xelement.Attributes().Select(delegate(XAttribute a)
						{
							if (!a.IsNamespaceDeclaration)
							{
								return new XAttribute(XNamespace.None.GetName(a.Name.LocalName), a.Value);
							}
							return null;
						}));
					}
					list.Add(policyNudgeConfiguration.SerializeConfiguration(root.Element("PolicyNudges"), cachedOrganizationConfiguration, this.actingAsRecipient.Id, base.XmlDocument));
				}
			}
			return this.CreateResultArray(list);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00079960 File Offset: 0x00077B60
		private void GetActor()
		{
			if (base.Request.ActingAs == null)
			{
				throw new ServiceArgumentException(CoreResources.IDs.ErrorActingAsRequired);
			}
			if (string.IsNullOrEmpty(base.Request.ActingAs.RoutingType))
			{
				throw new ServiceArgumentException((CoreResources.IDs)2292082652U);
			}
			if (string.IsNullOrEmpty(base.Request.ActingAs.EmailAddress))
			{
				throw new ServiceArgumentException((CoreResources.IDs)3538999938U);
			}
			this.actingAsAddress = ProxyAddress.Parse(base.Request.ActingAs.RoutingType ?? string.Empty, base.Request.ActingAs.EmailAddress ?? string.Empty);
			if (this.actingAsAddress is InvalidProxyAddress)
			{
				throw new ServiceArgumentException((CoreResources.IDs)2886782397U);
			}
			try
			{
				this.transportPrincipal = GetServiceConfiguration.GetPrincipal(base.CallContext, base.Request.ActingAs);
				ExTraceGlobals.GetOrganizationConfigurationCallTracer.TraceDebug<ProxyAddress>((long)this.GetHashCode(), "Looking up the actor ", this.actingAsAddress);
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.transportPrincipal.MailboxInfo.OrganizationId), 387, "GetActor", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\GetServiceConfiguration.cs");
				this.actingAsRecipient = MailTipsUtility.GetSender(tenantOrRootOrgRecipientSession, this.actingAsAddress, this.callerProperties);
			}
			catch (SenderNotUniqueException)
			{
				throw new ServiceArgumentException(CoreResources.IDs.ErrorActingAsUserNotUnique);
			}
			catch (SenderNotFoundException)
			{
				throw new ServiceArgumentException(CoreResources.IDs.ErrorActingAsUserNotFound);
			}
			catch (ServicePermanentException)
			{
				throw new ServiceArgumentException(CoreResources.IDs.ErrorActingAsUserNotFound);
			}
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00079B10 File Offset: 0x00077D10
		private bool CallerRequested(ServiceConfigurationType type)
		{
			return (this.configurationTypes & type) != ServiceConfigurationType.None;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00079B20 File Offset: 0x00077D20
		private ServiceResult<ServiceConfigurationResponseMessage[]> CreateResultArray(List<XmlElement> responseElements)
		{
			ServiceConfigurationResponseMessage serviceConfigurationResponseMessage = new ServiceConfigurationResponseMessage(ServiceResultCode.Success, null, responseElements.ToArray());
			ServiceConfigurationResponseMessage[] value = new ServiceConfigurationResponseMessage[]
			{
				serviceConfigurationResponseMessage
			};
			return new ServiceResult<ServiceConfigurationResponseMessage[]>(value);
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00079B4E File Offset: 0x00077D4E
		private string GetOutlookDateTimeString(DateTime dt)
		{
			return dt.ToString("yyyy-MM-dd") + "T" + dt.ToString("HH:mm:ss") + "Z";
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00079B78 File Offset: 0x00077D78
		private XmlElement SerializeMailTipsConfiguration(bool mailTipsEnabled, MailTipsConfiguration mailTipsConfiguration, OrganizationDomains domains)
		{
			XmlElement xmlElement = ServiceXml.CreateElement(base.XmlDocument, "MailTipsConfiguration", "http://schemas.microsoft.com/exchange/services/2006/messages");
			ServiceXml.CreateTextElement(xmlElement, "MailTipsEnabled", XmlConvert.ToString(mailTipsEnabled), "http://schemas.microsoft.com/exchange/services/2006/types");
			ServiceXml.CreateTextElement(xmlElement, "MaxRecipientsPerGetMailTipsRequest", XmlConvert.ToString(50), "http://schemas.microsoft.com/exchange/services/2006/types");
			ServiceXml.CreateTextElement(xmlElement, "MaxMessageSize", XmlConvert.ToString(mailTipsConfiguration.MaxMessageSize), "http://schemas.microsoft.com/exchange/services/2006/types");
			ServiceXml.CreateTextElement(xmlElement, "LargeAudienceThreshold", XmlConvert.ToString(mailTipsConfiguration.LargeAudienceThreshold), "http://schemas.microsoft.com/exchange/services/2006/types");
			ServiceXml.CreateTextElement(xmlElement, "ShowExternalRecipientCount", XmlConvert.ToString(mailTipsConfiguration.ShowExternalRecipientCount), "http://schemas.microsoft.com/exchange/services/2006/types");
			XmlElement parentElement = ServiceXml.CreateElement(xmlElement, "InternalDomains", "http://schemas.microsoft.com/exchange/services/2006/types");
			foreach (SmtpDomainWithSubdomains smtpDomainWithSubdomains in domains.InternalDomains)
			{
				XmlElement xmlElement2 = ServiceXml.CreateTextElement(parentElement, "Domain", string.Empty, "http://schemas.microsoft.com/exchange/services/2006/types");
				xmlElement2.SetAttribute("Name", smtpDomainWithSubdomains.Domain);
				xmlElement2.SetAttribute("IncludeSubdomains", XmlConvert.ToString(smtpDomainWithSubdomains.IncludeSubDomains));
			}
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012))
			{
				ServiceXml.CreateTextElement(xmlElement, "PolicyTipsEnabled", XmlConvert.ToString(mailTipsConfiguration.PolicyTipsEnabled), "http://schemas.microsoft.com/exchange/services/2006/types");
				ServiceXml.CreateTextElement(xmlElement, "LargeAudienceCap", XmlConvert.ToString(1000), "http://schemas.microsoft.com/exchange/services/2006/types");
			}
			return xmlElement;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00079CF4 File Offset: 0x00077EF4
		private XmlElement SerializeUnifiedMessageConfiguration(bool umEnabled, UMPropertiesEx umConfiguration)
		{
			XmlElement xmlElement = ServiceXml.CreateElement(base.XmlDocument, "UnifiedMessagingConfiguration", "http://schemas.microsoft.com/exchange/services/2006/messages");
			ServiceXml.CreateTextElement(xmlElement, "UmEnabled", XmlConvert.ToString(umEnabled), "http://schemas.microsoft.com/exchange/services/2006/types");
			ServiceXml.CreateTextElement(xmlElement, "PlayOnPhoneDialString", umConfiguration.PlayOnPhoneDialString, "http://schemas.microsoft.com/exchange/services/2006/types");
			ServiceXml.CreateTextElement(xmlElement, "PlayOnPhoneEnabled", XmlConvert.ToString(umConfiguration.PlayOnPhoneEnabled), "http://schemas.microsoft.com/exchange/services/2006/types");
			return xmlElement;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00079D64 File Offset: 0x00077F64
		private XmlElement SerializeProtectionRulesConfiguration(IEnumerable<OutlookProtectionRule> rules, OrganizationDomains domains)
		{
			XmlElement xmlElement = ServiceXml.CreateElement(base.XmlDocument, "ProtectionRulesConfiguration", "http://schemas.microsoft.com/exchange/services/2006/messages");
			xmlElement.SetAttribute("RefreshInterval", XmlConvert.ToString(24));
			GetServiceConfiguration.ProtectionRulesSerializer.SerializeRules(xmlElement, rules);
			XmlElement parentElement = ServiceXml.CreateElement(xmlElement, "InternalDomains", "http://schemas.microsoft.com/exchange/services/2006/types");
			string domain = this.transportPrincipal.MailboxInfo.PrimarySmtpAddress.Domain;
			bool flag = false;
			if (rules != null)
			{
				foreach (OutlookProtectionRule outlookProtectionRule in rules)
				{
					if (outlookProtectionRule.Enabled == RuleState.Enabled)
					{
						flag = true;
						break;
					}
				}
			}
			foreach (SmtpDomainWithSubdomains smtpDomainWithSubdomains in domains.InternalDomains)
			{
				if (flag || string.Compare(smtpDomainWithSubdomains.Address, domain, StringComparison.OrdinalIgnoreCase) == 0)
				{
					XmlElement xmlElement2 = ServiceXml.CreateTextElement(parentElement, "Domain", string.Empty, "http://schemas.microsoft.com/exchange/services/2006/types");
					xmlElement2.SetAttribute("Name", smtpDomainWithSubdomains.Domain);
					xmlElement2.SetAttribute("IncludeSubdomains", XmlConvert.ToString(smtpDomainWithSubdomains.IncludeSubDomains));
				}
			}
			return xmlElement;
		}

		// Token: 0x04000F72 RID: 3954
		private const string XmlElementNameServiceConfiguration = "ServiceConfiguration";

		// Token: 0x04000F73 RID: 3955
		private const string XmlElementNameMailTipsEnabled = "MailTipsEnabled";

		// Token: 0x04000F74 RID: 3956
		private const string XmlElementNameMailTipsConfiguration = "MailTipsConfiguration";

		// Token: 0x04000F75 RID: 3957
		private const string XmlElementNameMaxRecipientsPerGetMailTipsRequest = "MaxRecipientsPerGetMailTipsRequest";

		// Token: 0x04000F76 RID: 3958
		private const string XmlElementNameMaxMessageSize = "MaxMessageSize";

		// Token: 0x04000F77 RID: 3959
		private const string XmlElementNameLargeAudienceThreshold = "LargeAudienceThreshold";

		// Token: 0x04000F78 RID: 3960
		private const string XmlElementNameLargeAudienceCap = "LargeAudienceCap";

		// Token: 0x04000F79 RID: 3961
		private const string XmlElementNameShowExternalRecipientCount = "ShowExternalRecipientCount";

		// Token: 0x04000F7A RID: 3962
		private const string XmlElementNamePolicyTipsEnabled = "PolicyTipsEnabled";

		// Token: 0x04000F7B RID: 3963
		private const string XmlElementNameInternalDomains = "InternalDomains";

		// Token: 0x04000F7C RID: 3964
		private const string XmlElementNameDomain = "Domain";

		// Token: 0x04000F7D RID: 3965
		private const string XmlAttributeNameName = "Name";

		// Token: 0x04000F7E RID: 3966
		private const string XmlAttributeNameIncludeSubdomains = "IncludeSubdomains";

		// Token: 0x04000F7F RID: 3967
		private const string XmlElementNameUmEnabled = "UmEnabled";

		// Token: 0x04000F80 RID: 3968
		private const string XmlElementNamePlayOnPhoneDialString = "PlayOnPhoneDialString";

		// Token: 0x04000F81 RID: 3969
		private const string XmlElementNamePlayOnPhoneEnabled = "PlayOnPhoneEnabled";

		// Token: 0x04000F82 RID: 3970
		private const string XmlElementNameProtectionRulesConfiguration = "ProtectionRulesConfiguration";

		// Token: 0x04000F83 RID: 3971
		private const string XmlAttributeNameRefreshInterval = "RefreshInterval";

		// Token: 0x04000F84 RID: 3972
		internal const string XmlElementNameUnifiedMessagingConfiguration = "UnifiedMessagingConfiguration";

		// Token: 0x04000F85 RID: 3973
		private const string XmlElementNamePolicyNudges = "PolicyNudges";

		// Token: 0x04000F86 RID: 3974
		private readonly ADPropertyDefinition[] callerProperties = new ADPropertyDefinition[]
		{
			ADObjectSchema.OrganizationId,
			ADRecipientSchema.MaxSendSize,
			ADObjectSchema.Id
		};

		// Token: 0x04000F87 RID: 3975
		private ProxyAddress actingAsAddress;

		// Token: 0x04000F88 RID: 3976
		private ADRawEntry actingAsRecipient;

		// Token: 0x04000F89 RID: 3977
		private ServiceConfigurationType configurationTypes;

		// Token: 0x04000F8A RID: 3978
		private ExchangePrincipal transportPrincipal;

		// Token: 0x04000F8B RID: 3979
		private ExchangePrincipal unifiedMessagingPrincipal;

		// Token: 0x04000F8C RID: 3980
		private int traceId;

		// Token: 0x0200032B RID: 811
		private static class ProtectionRulesSerializer
		{
			// Token: 0x060016EB RID: 5867 RVA: 0x00079EA8 File Offset: 0x000780A8
			public static void SerializeRules(XmlElement parent, IEnumerable<OutlookProtectionRule> rules)
			{
				XmlElement parent2 = ServiceXml.CreateElement(parent, "Rules", "http://schemas.microsoft.com/exchange/services/2006/types");
				if (rules == null)
				{
					return;
				}
				int num = 1;
				foreach (OutlookProtectionRule outlookProtectionRule in rules)
				{
					if (outlookProtectionRule.Enabled == RuleState.Enabled)
					{
						GetServiceConfiguration.ProtectionRulesSerializer.SerializeRule(parent2, outlookProtectionRule, num);
						num++;
					}
				}
			}

			// Token: 0x060016EC RID: 5868 RVA: 0x00079F14 File Offset: 0x00078114
			private static void SerializeRule(XmlElement parent, OutlookProtectionRule rule, int priority)
			{
				XmlElement xmlElement = ServiceXml.CreateElement(parent, "Rule", "http://schemas.microsoft.com/exchange/services/2006/types");
				xmlElement.SetAttribute("Name", rule.Name);
				xmlElement.SetAttribute("Priority", XmlConvert.ToString(priority));
				xmlElement.SetAttribute("UserOverridable", XmlConvert.ToString(rule.UserOverridable));
				GetServiceConfiguration.ProtectionRulesSerializer.SerializeCondition(xmlElement, rule);
				GetServiceConfiguration.ProtectionRulesSerializer.SerializeAction(xmlElement, rule);
			}

			// Token: 0x060016ED RID: 5869 RVA: 0x00079F78 File Offset: 0x00078178
			private static void SerializeCondition(XmlElement parent, OutlookProtectionRule rule)
			{
				XmlElement parentElement = ServiceXml.CreateElement(parent, "Condition", "http://schemas.microsoft.com/exchange/services/2006/types");
				XmlElement xmlElement = ServiceXml.CreateElement(parentElement, "And", "http://schemas.microsoft.com/exchange/services/2006/types");
				GetServiceConfiguration.ProtectionRulesSerializer.SerializeAllInternalPredicate(xmlElement, rule);
				XmlElement parent2 = ServiceXml.CreateElement(xmlElement, "And", "http://schemas.microsoft.com/exchange/services/2006/types");
				GetServiceConfiguration.ProtectionRulesSerializer.SerializeRecipientIsPredicate(parent2, rule);
				GetServiceConfiguration.ProtectionRulesSerializer.SerializeSenderDepartmentPredicate(parent2, rule);
			}

			// Token: 0x060016EE RID: 5870 RVA: 0x00079FD0 File Offset: 0x000781D0
			private static void SerializeAllInternalPredicate(XmlElement parent, OutlookProtectionRule rule)
			{
				if (rule.GetAllInternalPredicate() == null)
				{
					ServiceXml.CreateElement(parent, "True", "http://schemas.microsoft.com/exchange/services/2006/types");
					return;
				}
				ServiceXml.CreateElement(parent, "AllInternal", "http://schemas.microsoft.com/exchange/services/2006/types");
			}

			// Token: 0x060016EF RID: 5871 RVA: 0x0007A00C File Offset: 0x0007820C
			private static void SerializeRecipientIsPredicate(XmlElement parent, OutlookProtectionRule rule)
			{
				PredicateCondition recipientIsPredicate = rule.GetRecipientIsPredicate();
				if (recipientIsPredicate == null)
				{
					ServiceXml.CreateElement(parent, "True", "http://schemas.microsoft.com/exchange/services/2006/types");
					return;
				}
				XmlElement parent2 = ServiceXml.CreateElement(parent, "RecipientIs", "http://schemas.microsoft.com/exchange/services/2006/types");
				GetServiceConfiguration.ProtectionRulesSerializer.SerializeValues(parent2, recipientIsPredicate.Value);
			}

			// Token: 0x060016F0 RID: 5872 RVA: 0x0007A054 File Offset: 0x00078254
			private static void SerializeSenderDepartmentPredicate(XmlElement parent, OutlookProtectionRule rule)
			{
				PredicateCondition senderDepartmentPredicate = rule.GetSenderDepartmentPredicate();
				if (senderDepartmentPredicate == null)
				{
					ServiceXml.CreateElement(parent, "True", "http://schemas.microsoft.com/exchange/services/2006/types");
					return;
				}
				XmlElement parent2 = ServiceXml.CreateElement(parent, "SenderDepartments", "http://schemas.microsoft.com/exchange/services/2006/types");
				GetServiceConfiguration.ProtectionRulesSerializer.SerializeValues(parent2, senderDepartmentPredicate.Value);
			}

			// Token: 0x060016F1 RID: 5873 RVA: 0x0007A09C File Offset: 0x0007829C
			private static void SerializeValues(XmlElement parent, Value values)
			{
				if (values == null)
				{
					return;
				}
				foreach (string textValue in values.RawValues)
				{
					ServiceXml.CreateTextElement(parent, "Value", textValue, "http://schemas.microsoft.com/exchange/services/2006/types");
				}
			}

			// Token: 0x060016F2 RID: 5874 RVA: 0x0007A100 File Offset: 0x00078300
			private static void SerializeAction(XmlElement parent, OutlookProtectionRule rule)
			{
				RightsProtectMessageAction rightsProtectMessageAction = rule.GetRightsProtectMessageAction();
				if (rightsProtectMessageAction == null)
				{
					return;
				}
				XmlElement xmlElement = ServiceXml.CreateElement(parent, "Action", "http://schemas.microsoft.com/exchange/services/2006/types");
				xmlElement.SetAttribute("Name", "RightsProtectMessage");
				XmlElement xmlElement2 = ServiceXml.CreateElement(xmlElement, "Argument", "http://schemas.microsoft.com/exchange/services/2006/types");
				xmlElement2.SetAttribute("Value", rightsProtectMessageAction.TemplateId);
			}

			// Token: 0x0200032C RID: 812
			private static class ElementNames
			{
				// Token: 0x04000F8E RID: 3982
				public const string Action = "Action";

				// Token: 0x04000F8F RID: 3983
				public const string AllInternal = "AllInternal";

				// Token: 0x04000F90 RID: 3984
				public const string And = "And";

				// Token: 0x04000F91 RID: 3985
				public const string Argument = "Argument";

				// Token: 0x04000F92 RID: 3986
				public const string Condition = "Condition";

				// Token: 0x04000F93 RID: 3987
				public const string RecipientIs = "RecipientIs";

				// Token: 0x04000F94 RID: 3988
				public const string Rule = "Rule";

				// Token: 0x04000F95 RID: 3989
				public const string Rules = "Rules";

				// Token: 0x04000F96 RID: 3990
				public const string SenderDepartments = "SenderDepartments";

				// Token: 0x04000F97 RID: 3991
				public const string True = "True";

				// Token: 0x04000F98 RID: 3992
				public const string Value = "Value";
			}

			// Token: 0x0200032D RID: 813
			private static class AttributeNames
			{
				// Token: 0x04000F99 RID: 3993
				public const string Name = "Name";

				// Token: 0x04000F9A RID: 3994
				public const string Priority = "Priority";

				// Token: 0x04000F9B RID: 3995
				public const string UserOverridable = "UserOverridable";

				// Token: 0x04000F9C RID: 3996
				public const string Value = "Value";
			}

			// Token: 0x0200032E RID: 814
			private static class ActionNames
			{
				// Token: 0x04000F9D RID: 3997
				public const string RightsProtectMessage = "RightsProtectMessage";
			}
		}
	}
}
