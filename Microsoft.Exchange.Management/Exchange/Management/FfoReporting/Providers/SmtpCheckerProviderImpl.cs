using System;
using System.Collections;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting.Providers
{
	// Token: 0x02000409 RID: 1033
	internal class SmtpCheckerProviderImpl : ISmtpCheckerProvider
	{
		// Token: 0x06002439 RID: 9273 RVA: 0x000907E2 File Offset: 0x0008E9E2
		internal SmtpCheckerProviderImpl() : this(new Func<string, object>(SmtpCheckerProviderImpl.CreateTargetObject))
		{
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000907F6 File Offset: 0x0008E9F6
		internal SmtpCheckerProviderImpl(Func<string, object> activator)
		{
			this.createTargetFunction = activator;
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00090808 File Offset: 0x0008EA08
		public IEnumerable GetMxRecords(Fqdn domain, IConfigDataProvider configDataProvider)
		{
			return this.GetData("Microsoft.Exchange.Hygiene.Reporting.SMTPVerificationTests.VerifyMxRecord", configDataProvider, new object[]
			{
				domain
			});
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x00090830 File Offset: 0x0008EA30
		public IEnumerable GetOutboundConnectors(Fqdn domain, IConfigDataProvider configDataProvider)
		{
			return this.GetData("Microsoft.Exchange.Hygiene.Reporting.SMTPVerificationTests.VerifyOutboundConnector", configDataProvider, new object[]
			{
				domain
			});
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x00090858 File Offset: 0x0008EA58
		public IEnumerable GetServiceDeliveries(SmtpAddress recipient, IConfigDataProvider configDataProvider)
		{
			return this.GetData("Microsoft.Exchange.Hygiene.Reporting.SMTPVerificationTests.VerifyServiceDelivery", configDataProvider, new object[]
			{
				recipient
			});
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x00090882 File Offset: 0x0008EA82
		private static object CreateTargetObject(string targetInstanceTypeName)
		{
			return Activator.CreateInstance("Microsoft.Exchange.Hygiene.Reporting.SMTPVerificationTests", targetInstanceTypeName).Unwrap();
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x00090894 File Offset: 0x0008EA94
		private IEnumerable GetData(string smtpDataTypeName, IConfigDataProvider configDataProvider, params object[] parameters)
		{
			object obj = this.createTargetFunction(smtpDataTypeName);
			Type type = obj.GetType();
			PropertyInfo property = type.GetProperty("ConfigSession");
			if (property != null)
			{
				property.SetValue(obj, configDataProvider);
			}
			MethodInfo method = type.GetMethod("Execute");
			return (IEnumerable)Schema.Utilities.Invoke(method, obj, parameters);
		}

		// Token: 0x04001CD8 RID: 7384
		private readonly Func<string, object> createTargetFunction;
	}
}
