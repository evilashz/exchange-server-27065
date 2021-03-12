using System;
using System.Collections.Concurrent;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.FfoReporting.Providers
{
	// Token: 0x02000408 RID: 1032
	internal class ServiceLocator : IServiceLocator
	{
		// Token: 0x0600242F RID: 9263 RVA: 0x000906A7 File Offset: 0x0008E8A7
		internal ServiceLocator()
		{
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x000906BA File Offset: 0x0008E8BA
		public static IServiceLocator Current
		{
			get
			{
				return ServiceLocator.hookableInstance.Value;
			}
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000906C6 File Offset: 0x0008E8C6
		internal static IDisposable SetTestHook(IServiceLocator testHook)
		{
			return ServiceLocator.hookableInstance.SetTestHook(testHook);
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000906E8 File Offset: 0x0008E8E8
		private static ServiceLocator CreateWithDefaults()
		{
			ServiceLocator serviceLocator = new ServiceLocator();
			serviceLocator.AddService<IDalProvider>(() => new DalProviderImpl());
			serviceLocator.AddService<ISmtpCheckerProvider>(() => new SmtpCheckerProviderImpl());
			serviceLocator.AddService<IAuthenticationProvider>(() => new AuthenticationProviderImpl());
			return serviceLocator;
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x00090768 File Offset: 0x0008E968
		public TServiceType GetService<TServiceType>()
		{
			Func<object> func;
			if (this.services.TryGetValue(typeof(TServiceType), out func))
			{
				return (TServiceType)((object)func());
			}
			throw new ArgumentException(string.Format("Unknown service: {0}", typeof(TServiceType).Name));
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000907B8 File Offset: 0x0008E9B8
		internal void AddService<TProviderType>(Func<object> activator)
		{
			this.services[typeof(TProviderType)] = activator;
		}

		// Token: 0x04001CD3 RID: 7379
		private static Hookable<IServiceLocator> hookableInstance = Hookable<IServiceLocator>.Create(true, ServiceLocator.CreateWithDefaults());

		// Token: 0x04001CD4 RID: 7380
		private ConcurrentDictionary<Type, Func<object>> services = new ConcurrentDictionary<Type, Func<object>>();
	}
}
