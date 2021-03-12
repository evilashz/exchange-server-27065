using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using Microsoft.Exchange.Net.WebApplicationClient;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000112 RID: 274
	internal class AppConfigGlsReader : IGlobalLocatorServiceReader
	{
		// Token: 0x06000B8E RID: 2958 RVA: 0x00035209 File Offset: 0x00033409
		public static bool AppConfigOverrideExists()
		{
			return ConfigurationManager.AppSettings["GlsFindDomainOverride"] != null;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00035220 File Offset: 0x00033420
		public AppConfigGlsReader()
		{
			AppConfigGlsReader.ParseAndConstructResultObjects(ConfigurationManager.AppSettings["GlsFindDomainOverride"], out this.findDomainResult, out this.findDomainsResult, out this.findTenantResult);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0003524E File Offset: 0x0003344E
		public bool TenantExists(Guid tenantId, Namespace[] ns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00035255 File Offset: 0x00033455
		public bool DomainExists(SmtpDomain domain, Namespace[] ns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0003525C File Offset: 0x0003345C
		public FindTenantResult FindTenant(Guid tenantId, TenantProperty[] tenantProperties)
		{
			return this.findTenantResult;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00035264 File Offset: 0x00033464
		public FindDomainResult FindDomain(SmtpDomain domain, DomainProperty[] domainProperties, TenantProperty[] tenantProperties)
		{
			return this.findDomainResult;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0003526C File Offset: 0x0003346C
		public FindDomainsResult FindDomains(SmtpDomain[] domains, DomainProperty[] domainProperties, TenantProperty[] tenantProperties)
		{
			return this.findDomainsResult;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00035274 File Offset: 0x00033474
		public IAsyncResult BeginTenantExists(Guid tenantId, Namespace[] ns, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0003527B File Offset: 0x0003347B
		public IAsyncResult BeginDomainExists(SmtpDomain domain, Namespace[] ns, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00035282 File Offset: 0x00033482
		public IAsyncResult BeginFindTenant(Guid tenantId, TenantProperty[] tenantProperties, AsyncCallback callback, object asyncState)
		{
			return AppConfigGlsReader.BeginAsyncOperation(callback, asyncState);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0003528C File Offset: 0x0003348C
		public IAsyncResult BeginFindDomain(SmtpDomain domain, DomainProperty[] domainProperties, TenantProperty[] tenantProperties, AsyncCallback callback, object asyncState)
		{
			return AppConfigGlsReader.BeginAsyncOperation(callback, asyncState);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00035297 File Offset: 0x00033497
		public IAsyncResult BeginFindDomains(SmtpDomain[] domains, DomainProperty[] domainProperties, TenantProperty[] tenantProperties, AsyncCallback callback, object asyncState)
		{
			return AppConfigGlsReader.BeginAsyncOperation(callback, asyncState);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000352A2 File Offset: 0x000334A2
		public bool EndTenantExists(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000352A9 File Offset: 0x000334A9
		public bool EndDomainExists(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000352B0 File Offset: 0x000334B0
		public FindTenantResult EndFindTenant(IAsyncResult asyncResult)
		{
			return this.findTenantResult;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000352B8 File Offset: 0x000334B8
		public FindDomainResult EndFindDomain(IAsyncResult asyncResult)
		{
			return this.findDomainResult;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000352C0 File Offset: 0x000334C0
		public FindDomainsResult EndFindDomains(IAsyncResult asyncResult)
		{
			return this.findDomainsResult;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x000352C8 File Offset: 0x000334C8
		private static IAsyncResult BeginAsyncOperation(AsyncCallback callback, object asyncState)
		{
			AsyncResult asyncResult = new AsyncResult(callback, asyncState);
			if (callback != null)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(AppConfigGlsReader.CallbackCallerThreadStart), new Tuple<AsyncCallback, IAsyncResult>(callback, asyncResult));
			}
			return asyncResult;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x000352FC File Offset: 0x000334FC
		private static void CallbackCallerThreadStart(object state)
		{
			Tuple<AsyncCallback, IAsyncResult> tuple = (Tuple<AsyncCallback, IAsyncResult>)state;
			AsyncCallback item = tuple.Item1;
			IAsyncResult item2 = tuple.Item2;
			item(item2);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00035328 File Offset: 0x00033528
		private static void ParseAndConstructResultObjects(string rawAppConfigValue, out FindDomainResult findDomainResult, out FindDomainsResult findDomainsResult, out FindTenantResult findTenantResult)
		{
			if (string.IsNullOrEmpty(rawAppConfigValue))
			{
				throw new ArgumentNullException("rawAppConfigValue");
			}
			char[] separator = new char[]
			{
				' '
			};
			char[] separator2 = new char[]
			{
				':'
			};
			char[] separator3 = new char[]
			{
				'='
			};
			char[] separator4 = new char[]
			{
				','
			};
			char[] trimChars = new char[]
			{
				'[',
				']'
			};
			string[] array = rawAppConfigValue.Split(separator);
			string text = array[0];
			string text2 = array[1];
			string text3 = array[2];
			string[] array2 = text.Split(separator2);
			string[] array3 = text2.Split(separator2);
			string[] array4 = text3.Split(separator2);
			string text4 = array2[0];
			string text5 = array3[0];
			string text6 = array4[0];
			string g = array2[1];
			string text7 = array3[1];
			string text8 = array4[1];
			AppConfigGlsReader.Assert(text4.Equals("TenantId", StringComparison.OrdinalIgnoreCase), "incorrect key name for TenantId");
			AppConfigGlsReader.Assert(text5.Equals("TenantProperties", StringComparison.OrdinalIgnoreCase), "incorrect key name for TenantProperties");
			AppConfigGlsReader.Assert(text6.Equals("DomainProperties", StringComparison.OrdinalIgnoreCase), "incorrect key name for DomainProperties");
			Guid tenantId = new Guid(g);
			IDictionary<DomainProperty, PropertyValue> dictionary = new Dictionary<DomainProperty, PropertyValue>();
			IDictionary<TenantProperty, PropertyValue> dictionary2 = new Dictionary<TenantProperty, PropertyValue>();
			text7 = text7.Trim(trimChars);
			text8 = text8.Trim(trimChars);
			if (text7 != string.Empty)
			{
				string[] array5 = text7.Split(separator4);
				foreach (string text9 in array5)
				{
					string[] array7 = text9.Split(separator3);
					string name = array7[0];
					string rawStringValue = array7[1];
					TenantProperty tenantProperty = TenantProperty.Get(name);
					dictionary2.Add(tenantProperty, PropertyValue.Create(rawStringValue, tenantProperty));
				}
			}
			if (text8 != string.Empty)
			{
				string[] array8 = text8.Split(separator4);
				foreach (string text10 in array8)
				{
					string[] array10 = text10.Split(separator3);
					string name2 = array10[0];
					string rawStringValue2 = array10[1];
					DomainProperty domainProperty = DomainProperty.Get(name2);
					dictionary.Add(domainProperty, PropertyValue.Create(rawStringValue2, domainProperty));
				}
			}
			findDomainResult = new FindDomainResult("domainName", tenantId, dictionary2, dictionary);
			findDomainsResult = new FindDomainsResult(new FindDomainResult[]
			{
				findDomainResult
			});
			findTenantResult = new FindTenantResult(dictionary2);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00035573 File Offset: 0x00033773
		private static void Assert(bool condition, string message)
		{
			if (!condition)
			{
				throw new FormatException(message);
			}
		}

		// Token: 0x040005CA RID: 1482
		private const string AppConfigOverrideKeyName = "GlsFindDomainOverride";

		// Token: 0x040005CB RID: 1483
		private FindDomainResult findDomainResult;

		// Token: 0x040005CC RID: 1484
		private FindDomainsResult findDomainsResult;

		// Token: 0x040005CD RID: 1485
		private FindTenantResult findTenantResult;
	}
}
