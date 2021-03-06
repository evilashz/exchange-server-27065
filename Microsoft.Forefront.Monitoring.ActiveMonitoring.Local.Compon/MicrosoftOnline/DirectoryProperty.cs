using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000113 RID: 275
	[XmlInclude(typeof(DirectoryPropertyGuid))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleMailNickname))]
	[XmlInclude(typeof(DirectoryPropertyXmlStrongAuthenticationPolicy))]
	[XmlInclude(typeof(DirectoryPropertyXmlAsymmetricKey))]
	[XmlInclude(typeof(DirectoryPropertyXmlEncryptedSecretKey))]
	[XmlInclude(typeof(DirectoryPropertyXmlKeyDescription))]
	[XmlInclude(typeof(DirectoryPropertyXmlRightsManagementUserKey))]
	[XmlInclude(typeof(DirectoryPropertyXmlRightsManagementUserKeySingle))]
	[XmlInclude(typeof(DirectoryPropertyXmlRightsManagementTenantKey))]
	[XmlInclude(typeof(DirectoryPropertyXmlRightsManagementTenantConfiguration))]
	[XmlInclude(typeof(DirectoryPropertyXmlRightsManagementTenantConfigurationSingle))]
	[XmlInclude(typeof(DirectoryPropertyXmlAppAddress))]
	[XmlInclude(typeof(DirectoryPropertyXmlAuthorizedParty))]
	[XmlInclude(typeof(DirectoryPropertyXmlThrottleLimit))]
	[XmlInclude(typeof(DirectoryPropertyXmlTaskSetScopeReference))]
	[XmlInclude(typeof(DirectoryPropertyXmlSupportRole))]
	[XmlInclude(typeof(DirectoryPropertyXmlServiceInstanceMap))]
	[XmlInclude(typeof(DirectoryPropertyXmlValidationError))]
	[XmlInclude(typeof(DirectoryPropertyXmlServiceOriginatedResource))]
	[XmlInclude(typeof(DirectoryPropertyXmlServiceInstanceInfo))]
	[XmlInclude(typeof(DirectoryPropertyXmlServiceInfo))]
	[XmlInclude(typeof(DirectoryPropertyXmlServiceEndpoint))]
	[XmlInclude(typeof(DirectoryPropertyXmlSearchForUsers))]
	[XmlInclude(typeof(DirectoryPropertyXmlProvisionedPlan))]
	[XmlInclude(typeof(DirectoryPropertyXmlPropagationTask))]
	[XmlInclude(typeof(DirectoryPropertyXmlMigrationDetail))]
	[XmlInclude(typeof(DirectoryPropertyXmlLicenseUnitsDetail))]
	[XmlInclude(typeof(DirectoryPropertyXmlLicenseUnitsDetailSingle))]
	[XmlInclude(typeof(DirectoryPropertyXmlGeographicLocation))]
	[XmlInclude(typeof(DirectoryPropertyXmlDirSyncStatus))]
	[XmlInclude(typeof(DirectoryPropertyXmlDatacenterRedirection))]
	[XmlInclude(typeof(DirectoryPropertyXmlCredential))]
	[XmlInclude(typeof(DirectoryPropertyXmlContextMoveWatermarks))]
	[XmlInclude(typeof(DirectoryPropertyXmlContextMoveWatermarksSingle))]
	[XmlInclude(typeof(DirectoryPropertyXmlContextMoveStatus))]
	[XmlInclude(typeof(DirectoryPropertyXmlContextMoveStatusSingle))]
	[XmlInclude(typeof(DirectoryPropertyXmlCompanyVerifiedDomain))]
	[XmlInclude(typeof(DirectoryPropertyXmlCompanyPartnership))]
	[XmlInclude(typeof(DirectoryPropertyXmlCompanyPartnershipSingle))]
	[XmlInclude(typeof(DirectoryPropertyXmlCompanyUnverifiedDomain))]
	[XmlInclude(typeof(DirectoryPropertyXmlAssignedPlan))]
	[XmlInclude(typeof(DirectoryPropertyXmlAssignedLicense))]
	[XmlInclude(typeof(DirectoryPropertyXmlAny))]
	[XmlInclude(typeof(DirectoryPropertyXmlAnySingle))]
	[XmlInclude(typeof(DirectoryPropertyString))]
	[XmlInclude(typeof(DirectoryPropertyServicePrincipalName))]
	[XmlInclude(typeof(DirectoryPropertyProxyAddresses))]
	[XmlInclude(typeof(DirectoryPropertyStringLength2To500))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To1123))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To1024))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To512))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To256))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To64))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To40))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To3))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To2048))]
	[XmlInclude(typeof(DirectoryPropertyStringSingle))]
	[XmlInclude(typeof(DirectoryPropertyTargetAddress))]
	[XmlInclude(typeof(DirectoryPropertyXmlSharedKeyReference))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To2048))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To1123))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To1024))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To512))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To500))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To454))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To256))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To128))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To64))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To40))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To20))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To16))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To6))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To3))]
	[XmlInclude(typeof(DirectoryPropertyReference))]
	[XmlInclude(typeof(DirectoryPropertyReferenceServicePlan))]
	[XmlInclude(typeof(DirectoryPropertyReferenceContact))]
	[XmlInclude(typeof(DirectoryPropertyReferenceContactSingle))]
	[XmlInclude(typeof(DirectoryPropertyReferenceAddressList))]
	[XmlInclude(typeof(DirectoryPropertyReferenceAddressListSingle))]
	[XmlInclude(typeof(DirectoryPropertyReferenceAny))]
	[XmlInclude(typeof(DirectoryPropertyReferenceAnySingle))]
	[XmlInclude(typeof(DirectoryPropertyInt64))]
	[XmlInclude(typeof(DirectoryPropertyInt64Single))]
	[XmlInclude(typeof(DirectoryPropertyInt32))]
	[XmlInclude(typeof(DirectoryPropertyInt32Single))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0Max65535))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMax1))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0Max3))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0Max2))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0Max1))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0))]
	[XmlInclude(typeof(DirectoryPropertyGuidSingle))]
	[XmlInclude(typeof(DirectoryPropertyDateTime))]
	[XmlInclude(typeof(DirectoryPropertyDateTimeSingle))]
	[XmlInclude(typeof(DirectoryPropertyBoolean))]
	[XmlInclude(typeof(DirectoryPropertyBooleanSingle))]
	[XmlInclude(typeof(DirectoryPropertyBinary))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingle))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To102400))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To32000))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To12000))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To8000))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To4000))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To256))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To195))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To128))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To28))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength8))]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyXmlAlternativeSecurityId))]
	[XmlInclude(typeof(DirectoryPropertyXml))]
	[XmlInclude(typeof(DirectoryPropertyXmlStrongAuthenticationMethod))]
	[Serializable]
	public abstract class DirectoryProperty
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x000201CD File Offset: 0x0001E3CD
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x000201D5 File Offset: 0x0001E3D5
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x0400043B RID: 1083
		private XmlAttribute[] anyAttrField;
	}
}
