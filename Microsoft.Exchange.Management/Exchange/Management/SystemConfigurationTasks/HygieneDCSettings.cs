using System;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A58 RID: 2648
	internal static class HygieneDCSettings
	{
		// Token: 0x06005EE7 RID: 24295 RVA: 0x0018D3C0 File Offset: 0x0018B5C0
		public static bool GetFfoDCPublicIPAddresses(out MultiValuedProperty<IPRange> ffoIPs)
		{
			ffoIPs = null;
			IConfigurable obj = null;
			if (HygieneDCSettings.InvokeFindDCSettingsMethod(out obj))
			{
				ffoIPs = (HygieneDCSettings.ffoDCPublicIPsProperty.GetValue(obj, null) as MultiValuedProperty<IPRange>);
				return true;
			}
			return false;
		}

		// Token: 0x06005EE8 RID: 24296 RVA: 0x0018D3F4 File Offset: 0x0018B5F4
		public static bool GetSettings(out MultiValuedProperty<IPRange> ffoIPs, out MultiValuedProperty<SmtpX509IdentifierEx> ffoSmtpCerts, out MultiValuedProperty<ServiceProviderSettings> serviceProviderSettings)
		{
			ffoIPs = null;
			ffoSmtpCerts = null;
			serviceProviderSettings = null;
			IConfigurable obj = null;
			if (HygieneDCSettings.InvokeFindDCSettingsMethod(out obj))
			{
				ffoIPs = (HygieneDCSettings.ffoDCPublicIPsProperty.GetValue(obj, null) as MultiValuedProperty<IPRange>);
				ffoSmtpCerts = (HygieneDCSettings.ffoFrontDoorSmtpCertificatesProperty.GetValue(obj, null) as MultiValuedProperty<SmtpX509IdentifierEx>);
				serviceProviderSettings = (HygieneDCSettings.serviceProvidersProperty.GetValue(obj, null) as MultiValuedProperty<ServiceProviderSettings>);
				return true;
			}
			return false;
		}

		// Token: 0x06005EE9 RID: 24297 RVA: 0x0018D454 File Offset: 0x0018B654
		private static bool InvokeFindDCSettingsMethod(out IConfigurable dcSettings)
		{
			dcSettings = null;
			HygieneDCSettings.BuildMethodsAndProperties();
			try
			{
				MethodBase methodBase = HygieneDCSettings.findDCSettingsMethod;
				object obj = HygieneDCSettings.globalConfigSession;
				object[] array = new object[4];
				array[2] = false;
				IConfigurable[] array2 = methodBase.Invoke(obj, array) as IConfigurable[];
				if (array2 != null)
				{
					dcSettings = array2.FirstOrDefault<IConfigurable>();
				}
				if (dcSettings == null)
				{
					TaskLogger.LogError(new Exception("Find<DataCenterSettings> method returned empty result"));
					return false;
				}
			}
			catch (TargetInvocationException ex)
			{
				TaskLogger.LogError(ex.InnerException);
				return false;
			}
			return true;
		}

		// Token: 0x06005EEA RID: 24298 RVA: 0x0018D4D8 File Offset: 0x0018B6D8
		private static void BuildMethodsAndProperties()
		{
			if (HygieneDCSettings.globalConfigSession == null)
			{
				Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.Data");
				Type type = assembly.GetType("Microsoft.Exchange.Hygiene.Data.GlobalConfig.GlobalSystemConfigSession");
				MethodInfo method = type.GetMethod("Find", BindingFlags.Instance | BindingFlags.NonPublic);
				Type type2 = assembly.GetType("Microsoft.Exchange.Hygiene.Data.GlobalConfig.DataCenterSettings");
				HygieneDCSettings.findDCSettingsMethod = method.MakeGenericMethod(new Type[]
				{
					type2
				});
				HygieneDCSettings.ffoDCPublicIPsProperty = type2.GetProperty("FfoDataCenterPublicIPAddresses", BindingFlags.Instance | BindingFlags.Public);
				HygieneDCSettings.ffoFrontDoorSmtpCertificatesProperty = type2.GetProperty("FfoFrontDoorSmtpCertificates", BindingFlags.Instance | BindingFlags.Public);
				HygieneDCSettings.serviceProvidersProperty = type2.GetProperty("ServiceProviders", BindingFlags.Instance | BindingFlags.Public);
				HygieneDCSettings.globalConfigSession = Activator.CreateInstance(type, true);
			}
		}

		// Token: 0x040034FA RID: 13562
		private static object globalConfigSession;

		// Token: 0x040034FB RID: 13563
		private static MethodInfo findDCSettingsMethod;

		// Token: 0x040034FC RID: 13564
		private static PropertyInfo ffoDCPublicIPsProperty;

		// Token: 0x040034FD RID: 13565
		private static PropertyInfo ffoFrontDoorSmtpCertificatesProperty;

		// Token: 0x040034FE RID: 13566
		private static PropertyInfo serviceProvidersProperty;
	}
}
